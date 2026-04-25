using Hack.io.KCL;
using Hack.io.Utility;
using JeremyAnsel.Media.WavefrontObj;
using NeoKCLCreate.Formats;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using static NeoKCLCreate.UI.VectorEx;

namespace NeoKCLCreate.UI;

public partial class ImportProgressForm : Form
{
    private readonly ObjFile mObj;
    private readonly ObjMaterialFile? mMtl;
    private readonly float PrismThickness;
    private readonly int ValueRounding;
    private readonly int MaxTrisPerLeaf;
    private readonly int MinLeafSize;
    private readonly int MaxDepth;

    [AllowNull]
    private KCL mResult;
    [AllowNull]
    private PA mResultCodes;


    public KCL Result
    {
        get => mResult;
        private set => mResult = value;
    }
    public PA ResultCodes
    {
        get => mResultCodes;
        private set => mResultCodes = value;
    }


    public ImportProgressForm(ObjFile SourceObject, ObjMaterialFile? SourceMaterial, float PrismThickness, int ValueRounding, int MaxTrisPerLeaf, int MinLeafSize, int MaxDepth)
    {
        InitializeComponent();
        CenterToParent();

        mObj = SourceObject;
        mMtl = SourceMaterial;
        this.PrismThickness = PrismThickness;
        this.ValueRounding = ValueRounding;
        this.MaxTrisPerLeaf = MaxTrisPerLeaf;
        this.MinLeafSize = MinLeafSize;
        this.MaxDepth = MaxDepth;
        mResult = null;
        mResultCodes = null;

        DialogResult = DialogResult.Abort;
    }
    
    [GeneratedRegex(@"([^\|]+)\|([0-9]+)\|([0-9]+)\|([0-9]+)\|([0-9]+)\|([0-9]+)$")]
    private static partial Regex MaterialNameRegex();

    private void ImportProgressForm_Shown(object sender, EventArgs e)
    {
        ImportBackgroundWorker.RunWorkerAsync((mObj, mMtl, PrismThickness, ValueRounding, MaxTrisPerLeaf, MinLeafSize, MaxDepth));
    }

    private void ImportProgressForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (ImportBackgroundWorker.IsBusy)
        {
            ImportBackgroundWorker.CancelAsync();
            e.Cancel = true;
        }
    }

    private void ImportBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
    {
        ImportBackgroundWorker.ReportProgress(0, "Preparing to import...");
        Thread.Sleep(1000);
        (ObjFile test, ObjMaterialFile? testm, float thick, int round, int maxtri, int minsize, int maxdepth) = ((ObjFile, ObjMaterialFile?, float, int, int, int, int))e.Argument!;

        int ProgressBarValue, ProgressBarMax;

        #region KCL Generation
        List<Vector3> UniquePositions = [];
        List<Vector3> UniqueNormals = [];

        KCL Result = new() { PrismThickness = thick };
        float min_x = float.PositiveInfinity;
        float min_y = float.PositiveInfinity;
        float min_z = float.PositiveInfinity;
        float max_x = float.NegativeInfinity;
        float max_y = float.NegativeInfinity;
        float max_z = float.NegativeInfinity;

        StartProgress(test.Faces.Count, "Generating Prism Data {0}%");
        foreach (ObjFace item in test.Faces)
        {
            KCL.Prism? prismattempt = TryMakePrism(item, out Vector3 A, out Vector3 B, out Vector3 C);

            ProgressBarValue++;
            ReportProgress("Generating Prism Data {0}%");
            if (ImportBackgroundWorker.CancellationPending)
                goto Cancellation;

            if (prismattempt is not KCL.Prism prism)
                continue;
            Result.PrismList.AddIfNotContains(prism);

            UniquePositions.AddIfNotContains(prism.Position);
            UniqueNormals.AddIfNotContains(prism.FaceNormal);
            UniqueNormals.AddIfNotContains(prism.EdgeANormal);
            UniqueNormals.AddIfNotContains(prism.EdgeBNormal);
            UniqueNormals.AddIfNotContains(prism.EdgeCNormal);

            UpdateMinMax(A);
            UpdateMinMax(B);
            UpdateMinMax(C);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            void UpdateMinMax(Vector3 v)
            {
                if (v.X < min_x)
                    min_x = v.X;

                if (v.Y < min_y)
                    min_y = v.Y;

                if (v.Z < min_z)
                    min_z = v.Z;

                if (v.X > max_x)
                    max_x = v.X;

                if (v.Y > max_y)
                    max_y = v.Y;

                if (v.Z > max_z)
                    max_z = v.Z;
            }
        }

        ProgressBarValue = ProgressBarMax;
        if (UniquePositions.Count >= ushort.MaxValue)
        {
            MessageBox.Show("Too many positions!\n\nIf you haven't already, try importing with the Value Rounding set greater than 0. If you still recieve this message after that, then your model is too complex, an needs to be simplified.", "Geometry Overflow", MessageBoxButtons.OK, MessageBoxIcon.Error);
            e.Result = Array.Empty<byte>(); // Won't be used for anything, basically just a signal for when the worker cancels
            goto DoReturnNull;
        }
        if (UniqueNormals.Count >= ushort.MaxValue)
        {
            MessageBox.Show("Too many normals!\n\nIf you haven't already, try importing with the Value Rounding set greater than 0. If you still recieve this message after that, then your model is too complex, an needs to be simplified.", "Geometry Overflow", MessageBoxButtons.OK, MessageBoxIcon.Error);
            e.Result = Array.Empty<byte>(); // Won't be used for anything, basically just a signal for when the worker cancels
            goto DoReturnNull;
        }
        ReportProgress("Calculating Octree Size");


        Vector3 Max = new(max_x, max_y, max_z);
        Vector3 Min = new(min_x, min_y, min_z);
        Vector3 Size = Max - Min;

        uint ExponentX = (uint)GetNext2Exponent(Size.X);
        uint ExponentY = (uint)GetNext2Exponent(Size.Y);
        uint ExponentZ = (uint)GetNext2Exponent(Size.Z);
        int cubeSizePower = (int)Math.Min(ExponentX, Math.Min(ExponentY, ExponentZ));

        bool IsAnyZero = cubeSizePower <= 0;
        if (IsAnyZero)
            cubeSizePower = (int)Math.Max(ExponentX, Math.Max(ExponentY, ExponentZ));

        int cubeSize = 1 << cubeSizePower;
        uint ShiftX = (uint)cubeSizePower;
        uint ShiftY = (uint)(ExponentX - cubeSizePower);
        uint ShiftZ = (uint)(ExponentX - cubeSizePower + ExponentY - cubeSizePower);
        if ((ShiftZ & 0x80000000) != 0)
            ShiftZ = 0;

        uint MaskX = 0xFFFFFFFFu << (int)ExponentX;
        uint MaskY = 0xFFFFFFFFu << (int)ExponentY;
        uint MaskZ = 0xFFFFFFFFu << (int)ExponentZ;

        uint CubeCountX = (uint)Math.Max(1, (1 << (int)ExponentX) / cubeSize),
             CubeCountY = (uint)Math.Max(1, (1 << (int)ExponentY) / cubeSize),
             CubeCountZ = (uint)Math.Max(1, (1 << (int)ExponentZ) / cubeSize);

        Result.CoordinateMask = new(MaskX, MaskY, MaskZ);
        Result.CoordinateShift = new(ShiftX, ShiftY, ShiftZ);
        Result.MinCoords = Min;

        List<int> AllFaceIdx = new(test.Faces.Count + 1);
        for (int i = 0; i < test.Faces.Count; i++)
            AllFaceIdx.Add(i);


        StartProgress((int)(CubeCountX * CubeCountY * CubeCountZ), "Generating Octree {0}%");

        int cubeBlow = 2;
        for (int z = 0; z < CubeCountZ; z++)
        {
            for (int y = 0; y < CubeCountY; y++)
            {
                for (int x = 0; x < CubeCountX; x++)
                {
                    Vector3 cubePosition = Result.MinCoords + ((float)cubeSize) * new Vector3(x, y, z);

                    KCL.IOctreeNode ON = CreateOctreeNode(AllFaceIdx, cubePosition, cubeSize, maxtri, 1048576, minsize, cubeBlow, maxdepth);

                    if (ImportBackgroundWorker.CancellationPending)
                        goto Cancellation;

                    Result.OctreeRoot.Add(ON);

                    ProgressBarValue++;
                    ReportProgress("Generating Octree {0}%");
                }
            }
        }

        ReportProgress("Generating Octree {0}%");
        Thread.Sleep(500);

        int CollapseCount = 0;
        if (!IsAnyZero)
            while(TryCollapseTopLevel(Result))
                CollapseCount++;
        _ = CollapseCount;
        #endregion

        #region PA Generation
        PA ResultCodes = new();

        if (testm is not null)
        {
            StartProgress(testm.Materials.Count, "Generating PA {0}%");
            int i = 0;
            foreach (var item in testm.Materials)
            {
                string matname = item.Name ?? $"Unnamed {i}";

                PA.Entry defaultentry = new() { Parent = ResultCodes, CameraId = 255 };
                ResultCodes.Add(defaultentry);
                var match = MaterialNameRegex().Match(matname);
                if (match.Success)
                {
                    matname = match.Groups[1].Value;

                    if (int.TryParse(match.Groups[2].Value, out int Floor))
                        defaultentry.FloorCode = Floor;

                    if (int.TryParse(match.Groups[3].Value, out int Wall))
                        defaultentry.WallCode = Wall;

                    if (int.TryParse(match.Groups[4].Value, out int Sound))
                        defaultentry.SoundCode = Sound;

                    if (int.TryParse(match.Groups[5].Value, out int CameraId))
                        defaultentry.CameraId = CameraId;

                    if (int.TryParse(match.Groups[6].Value, out int CameraThrough))
                        defaultentry.CameraThrough = CameraThrough;
                }

                defaultentry.Name = matname;
                i++;
                if (testm.Materials.Count < 500)
                    Thread.Sleep(100);

                ProgressBarValue++;
                ReportProgress("Generating PA {0}%");

                if (ImportBackgroundWorker.CancellationPending)
                    goto Cancellation;
            }
        }
        else
        {
            PA.Entry defaultentry = new() { Parent = ResultCodes, Name = "Default_Material", CameraId = 255 };
            ResultCodes.Add(defaultentry);
        }
        Thread.Sleep(100);
        #endregion
        
        if (ImportBackgroundWorker.CancellationPending)
            goto Cancellation;

        ProgressBarValue = ProgressBarMax;
        ReportProgress("All Done!");
        Thread.Sleep(900);

        e.Result = (Result, ResultCodes);
        return;

    Cancellation:
        e.Cancel = true;
    DoReturnNull:
        e.Result = null;
        return;

        // ----------------------------------------------------

        void StartProgress(int maxvalue, string format)
        {
            ProgressBarValue = 0;
            ProgressBarMax = maxvalue;

            ReportProgress(format);
        }

        void ReportProgress(string format)
        {
            ImportBackgroundWorker.ReportProgress((int)(MathUtil.GetPercentOf(ProgressBarValue, ProgressBarMax) * 10), format);
        }

        (Vector3 A, Vector3 B, Vector3 C, Vector3 N) GetFromObj(ObjFace item)
        {
            int idxA = item.Vertices[0].Vertex;
            int idxB = item.Vertices[1].Vertex;
            int idxC = item.Vertices[2].Vertex;
            Vector4 vecA = test.Vertices[idxA - 1].Position.ToVector4();
            Vector4 vecB = test.Vertices[idxB - 1].Position.ToVector4();
            Vector4 vecC = test.Vertices[idxC - 1].Position.ToVector4();
            Vector3 A = new(vecA.X, vecA.Y, vecA.Z);
            Vector3 B = new(vecB.X, vecB.Y, vecB.Z);
            Vector3 C = new(vecC.X, vecC.Y, vecC.Z);

            if (round > 0)
            {
                int roundval = 7 - round;
                A = new(MathF.Round(A.X, roundval), MathF.Round(A.Y, roundval), MathF.Round(A.Z, roundval));
                B = new(MathF.Round(B.X, roundval), MathF.Round(B.Y, roundval), MathF.Round(B.Z, roundval));
                C = new(MathF.Round(C.X, roundval), MathF.Round(C.Y, roundval), MathF.Round(C.Z, roundval));
            }

            Vector3 N = Vector3.Cross(B - A, C - A).Unit();

            return (A, B, C, N);
        }

        KCL.Prism? TryMakePrism(ObjFace item, out Vector3 A, out Vector3 B, out Vector3 C)
        {
            (A, B, C, Vector3 N) = GetFromObj(item);

            if (testm is null)
                return KCL.Prism.Create(A, B, C, N);

            ushort idx = 0;
            string? matnme = item.MaterialName;
            if (matnme is string str)
            {
                for (int i = 0; i < testm.Materials.Count; i++)
                {
                    if (!str.Equals(testm.Materials[i].Name))
                        continue;
                    idx = (ushort)i;
                }
            }
            return KCL.Prism.Create(A, B, C, N, idx);
        }

        KCL.IOctreeNode CreateOctreeNode(List<int> ObjFaceIndexes, Vector3 cubePosition, float cubeSize, int maxTrianglesInCube, int maxCubeSize, int minCubeSize, int cubeBlow, int maxDepth, int depth = 0)
        {
            const int DEPTH_PROGRESS = 3;
            if (ImportBackgroundWorker.CancellationPending)
            {
                ReportProgress("Generating Octree {0}%");
                return null!;
            }

            Vector3 cubeCenter = cubePosition + new Vector3(cubeSize / 2f, cubeSize / 2f, cubeSize / 2f);
            float newsize = cubeSize + cubeBlow;
            Vector3 newPosition = cubeCenter - new Vector3(newsize / 2f, newsize / 2f, newsize / 2f);

            // Collect triangles inside
            List<ushort> containedTriangles = [];
            List<int> containedFaceIds = [];
            foreach (int faceidx in ObjFaceIndexes)
            {
                ObjFace item = test.Faces[faceidx];
                (Vector3 A, Vector3 B, Vector3 C, Vector3 N) = GetFromObj(item);

                if (TriangleCubeOverlap(A, B, C, N, newPosition, cubeSize))
                {
                    KCL.Prism? prismattempt = TryMakePrism(item, out _, out _, out _);

                    if (prismattempt is not KCL.Prism prism)
                        continue;

                    int idx = Result.PrismList.IndexOf(prism);
                    if (idx == -1)
                        Debugger.Break();

                    containedTriangles.Add((ushort)idx);
                    containedFaceIds.Add(faceidx);
                }
            }

            float halfWidth = cubeSize / 2f;
            bool isNewLeaf = cubeSize <= maxCubeSize && containedTriangles.Count <= maxTrianglesInCube || cubeSize <= minCubeSize || depth > maxDepth;


            if (isNewLeaf)
            {
                // This code will sort same-direction facing prisms heightwise
                // and was intended to be used for fixing a bug with thin triangles but it didn't help
                // so I am leaving this here for future me in case I want to allow this as an option or something

                /*
                IEnumerable<IGrouping<(int, int, int), ushort>> groups = containedTriangles
                .GroupBy(i =>
                {
                    var n = Result.PrismList[i].FaceNormal;
                    return (
                        (int)MathF.Round(n.X * 16f),
                        (int)MathF.Round(n.Y * 16f),
                        (int)MathF.Round(n.Z * 16f)
                    );
                });

                containedTriangles = [.. containedTriangles
                    .GroupBy(i =>
                    {
                        var n = Result.PrismList[i].FaceNormal;

                        static int Q(float v) => (int)MathF.Round(v * 16f);

                        return (Q(n.X), Q(n.Y), Q(n.Z));
                    })
                    .SelectMany(g =>
                        g.OrderByDescending(i =>
                        {
                            var p = Result.PrismList[i];
                            return Vector3.Dot(p.Position, p.FaceNormal);
                        })
                        .ThenBy(i => i)
                    )];
                */
                
                
                KCL.OctreeLeaf OL = [];
                OL.AddRange(containedTriangles);
                return OL;
            }


            if (depth < DEPTH_PROGRESS)
            {
                ProgressBarMax += 2 * 2 * 2;
                ReportProgress("Generating Octree {0}%");
            }

            KCL.OctreeBranch OB = [];
            float childCubeSize = cubeSize / 2f;
            for (int z = 0; z < 2; z++)
                for (int y = 0; y < 2; y++)
                    for (int x = 0; x < 2; x++)
                    {
                        Vector3 childCubePosition = cubePosition + childCubeSize * new Vector3(x, y, z);
                        KCL.IOctreeNode newnode = CreateOctreeNode(containedFaceIds, childCubePosition, childCubeSize, maxTrianglesInCube, maxCubeSize, minCubeSize, cubeBlow, maxDepth, depth + 1);
                        OB.Add(newnode);

                        if (depth < DEPTH_PROGRESS)
                        {
                            ProgressBarValue++;
                            ReportProgress("Generating Octree {0}%");
                        }
                    }

            return OB;
        }

        static bool TryCollapseTopLevel(KCL col)
        {
            (int sizeX, int sizeY, int sizeZ) = col.RootGridSize;

            int total = col.OctreeRoot.Count;
            int branchCount = col.OctreeRoot.Count(n => n is KCL.OctreeBranch);

            // If the top level branch ratio is greater than 0.875, space is saved if the top level of nodes is removed
            if (total == 0 || branchCount * 8 < total * 7)
                return false;

            var oldRoot = col.OctreeRoot.ToArray();

            int newSizeX = sizeX * 2;
            int newSizeY = sizeY * 2;
            int newSizeZ = sizeZ * 2;

            List<KCL.IOctreeNode> newRoot = new(newSizeX * newSizeY * newSizeZ);

            for (int z = 0; z < newSizeZ; z++)
                for (int y = 0; y < newSizeY; y++)
                    for (int x = 0; x < newSizeX; x++)
                    {
                        int px = x >> 1;
                        int py = y >> 1;
                        int pz = z >> 1;

                        int parentIndex = px + sizeX * (py + sizeY * pz);
                        KCL.IOctreeNode parent = oldRoot[parentIndex];

                        KCL.IOctreeNode node;

                        if (parent is KCL.OctreeBranch branch)
                        {
                            int cx = x & 1;
                            int cy = y & 1;
                            int cz = z & 1;

                            int childIndex = cx + 2 * (cy + 2 * cz);
                            node = branch[childIndex];
                        }
                        else
                            node = parent;

                        newRoot.Add(node);
                    }

            col.OctreeRoot.Clear();
            col.OctreeRoot.AddRange(newRoot);

            // Update shifts go brr
            col.CoordinateShift = new(
                col.CoordinateShift.X - 1,
                col.CoordinateShift.Y + 1,
                col.CoordinateShift.Z + 2
            );
            return true;
        }
    }

    private void ImportBackgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
    {
        float progress = e.ProgressPercentage / 10.0f;
        string msg = e.UserState?.ToString() ?? "";
        // Override the message with a cancellation message if a cancel has been requested
        if (ImportBackgroundWorker.CancellationPending)
            msg = "Cancelling...";
        MainProgressBar.Value = Math.Clamp((int)progress, MainProgressBar.Minimum, MainProgressBar.Maximum);
        StatusLabel.Text = string.Format(msg, progress.ToString("0.00"));
    }

    private void ImportBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
    {
        if (e.Cancelled)
        {
            DialogResult = DialogResult.Cancel;
            Close();
            return;
        }

        if (e.Result is not (KCL k, PA p))
        {
            DialogResult = DialogResult.TryAgain;
            Close();
            return;
        }

        (mResult, mResultCodes) = (k, p);

        DialogResult = DialogResult.OK;
        Close();
    }
}

// For some reason I refused to make a Utility file for this...
public static class VectorEx
{
    public static Vector3 Unit(this Vector3 origin) => origin / origin.Length();

    public static int GetNext2Exponent(float value)
    {
        if (value <= 1) return 0;
        return (int)Math.Ceiling(Math.Log(value, 2));
    }

    public static bool TriangleCubeOverlap(Vector3 A, Vector3 B, Vector3 C, Vector3 N, Vector3 Position, float BoxSize)
    {
        float half = BoxSize / 2f;
        //Position is the min pos, so add half the box size
        Position += new Vector3(half, half, half);
        Vector3 v0 = A - Position;
        Vector3 v1 = B - Position;
        Vector3 v2 = C - Position;

        float min = Math.Min(Math.Min(v0.X, v1.X), v2.X);
        float max = Math.Max(Math.Max(v0.X, v1.X), v2.X);
        if (min > half || max < -half)
            return false;
        if (Math.Min(Math.Min(v0.Y, v1.Y), v2.Y) > half || Math.Max(Math.Max(v0.Y, v1.Y), v2.Y) < -half)
            return false;
        if (Math.Min(Math.Min(v0.Z, v1.Z), v2.Z) > half || Math.Max(Math.Max(v0.Z, v1.Z), v2.Z) < -half)
            return false;

        float d = Vector3.Dot(N, v0);
        double r = half * (Math.Abs(N.X) + Math.Abs(N.Y) + Math.Abs(N.Z));
        if (d > r || d < -r)
            return false;

        Vector3 e = v1 - v0;
        if (AxisTest(e.Z, -e.Y, v0.Y, v0.Z, v2.Y, v2.Z, half)) 
            return false;
        if (AxisTest(-e.Z, e.X, v0.X, v0.Z, v2.X, v2.Z, half)) 
            return false;
        if (AxisTest(e.Y, -e.X, v1.X, v1.Y, v2.X, v2.Y, half)) 
            return false;

        e = v2 - v1;
        if (AxisTest(e.Z, -e.Y, v0.Y, v0.Z, v2.Y, v2.Z, half)) 
            return false;
        if (AxisTest(-e.Z, e.X, v0.X, v0.Z, v2.X, v2.Z, half)) 
            return false;
        if (AxisTest(e.Y, -e.X, v0.X, v0.Y, v1.X, v1.Y, half)) 
            return false;

        e = v0 - v2;
        if (AxisTest(e.Z, -e.Y, v0.Y, v0.Z, v1.Y, v1.Z, half)) 
            return false;
        if (AxisTest(-e.Z, e.X, v0.X, v0.Z, v1.X, v1.Z, half)) 
            return false;
        if (AxisTest(e.Y, -e.X, v1.X, v1.Y, v2.X, v2.Y, half)) 
            return false;
        return true;
    }
    private static bool AxisTest(double a1, double a2, double b1, double b2, double c1, double c2, double half)
    {
        var p = a1 * b1 + a2 * b2;
        var q = a1 * c1 + a2 * c2;
        var r = half * (Math.Abs(a1) + Math.Abs(a2));
        return Math.Min(p, q) > r || Math.Max(p, q) < -r;
    }
}