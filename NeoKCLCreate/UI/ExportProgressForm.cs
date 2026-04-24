using Hack.io.KCL;
using Hack.io.Utility;
using JeremyAnsel.Media.WavefrontObj;
using NeoKCLCreate.Formats;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace NeoKCLCreate.UI;

public partial class ExportProgressForm : Form
{
    private readonly KCL mKCL;
    private readonly PA mPA;
    private readonly string mObjName;
    private readonly bool mMode;
    [AllowNull]
    private ObjFile mResultObject;
    [AllowNull]
    private ObjMaterialFile mResultMaterial;


    public ObjFile ResultObject
    {
        get => mResultObject;
        private set => mResultObject = value;
    }
    public ObjMaterialFile ResultMaterial
    {
        get => mResultMaterial;
        private set => mResultMaterial = value;
    }


    public ExportProgressForm(KCL Collision, PA Materials, string ObjName, bool Mode)
    {
        InitializeComponent();
        CenterToParent();

        mKCL = Collision;
        mPA = Materials;
        mObjName = ObjName;
        mMode = Mode;
        mResultObject = null;
        mResultMaterial = null;

        DialogResult = DialogResult.Abort;
    }

    private void ExportProgressForm_Shown(object sender, EventArgs e)
    {
        ExportBackgroundWorker.RunWorkerAsync((mKCL, mPA, mObjName, mMode));
    }

    private void ExportProgressForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (ExportBackgroundWorker.IsBusy)
        {
            ExportBackgroundWorker.CancelAsync();
            e.Cancel = true;
        }
    }

    private void ExportBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
    {
        ExportBackgroundWorker.ReportProgress(0, "Preparing to export...");
        Thread.Sleep(1000);
        (KCL CollisionData, PA CollisionCode, string ObjectName, bool Mode) = ((KCL, PA, string, bool))e.Argument!;

        int ProgressBarValue, ProgressBarMax;


        ObjFile ResultObject = new();
        ObjMaterialFile ResultMaterial = new();

        if (!Mode)
        {
            if (!ExportGeometry())
                goto Cancellation;
        }
        else
        {
            if (!ExportOctree())
                goto Cancellation;
        }


        ProgressBarValue = ProgressBarMax;
        ReportProgress("All Done!");
        Thread.Sleep(900);

        e.Result = (ResultObject, ResultMaterial);
        return;

    Cancellation:
        e.Result = null;
        return;





        void StartProgress(int maxvalue, string format)
        {
            ProgressBarValue = 0;
            ProgressBarMax = maxvalue;

            ReportProgress(format);
            Thread.Sleep(500);
        }

        void ReportProgress(string format)
        {
            ExportBackgroundWorker.ReportProgress((int)(MathUtil.GetPercentOf(ProgressBarValue, ProgressBarMax) * 10), format);
        }

        void AddTriangle(KCL.Prism prism, string matname)
        {
            (Vector3 A, Vector3 B, Vector3 C, Vector3 N) = KCL.Prism.Decompose(prism);

            int PosCount = ResultObject.Vertices.Count + 1;
            int NrmCount = ResultObject.VertexNormals.Count + 1;
            ResultObject.Vertices.Add(new(A.X, A.Y, A.Z));
            ResultObject.Vertices.Add(new(B.X, B.Y, B.Z));
            ResultObject.Vertices.Add(new(C.X, C.Y, C.Z));
            ResultObject.VertexNormals.Add(new(N));

            ObjFace face = new() { ObjectName = ObjectName };
            face.Vertices.Add(new(PosCount + 0, 0, NrmCount));
            face.Vertices.Add(new(PosCount + 1, 0, NrmCount));
            face.Vertices.Add(new(PosCount + 2, 0, NrmCount));
            face.MaterialName = matname;
            ResultObject.Faces.Add(face);
        }
    
        bool ExportGeometry()
        {
            StartProgress(CollisionCode.EntryCount, "Generating Materials {0}%");
            int mod = Math.Max(CollisionCode.EntryCount / 100, 1);
            List<string> MaterialNames = [];
            for (int i = 0; i < CollisionCode.EntryCount; i++)
            {
                if (ExportBackgroundWorker.CancellationPending)
                    return false;

                PA.Entry entry = CollisionCode[i];
                string MatName = $"{entry.Name}|{entry.FloorCode}|{entry.WallCode}|{entry.SoundCode}|{entry.CameraId}|{entry.CameraThrough}";
                MaterialNames.Add(MatName);

                ObjMaterial Material = new(MatName);
                Random rng = new(MatName.GetHashCode());
                Material.DiffuseColor = new(rng.NextSingle(), rng.NextSingle(), rng.NextSingle());
                Material.SpecularExponent = 100;
                ResultMaterial.Materials.Add(Material);

                ProgressBarValue++;
                ReportProgress("Generating Materials {0}%");
                if (i % mod == 0)
                    Thread.Sleep(5);
            }

            StartProgress(CollisionData.PrismList.Count, "Composing Triangles {0}%");
            mod = Math.Max(CollisionData.PrismList.Count / 100, 10);
            for (int i = 0; i < CollisionData.PrismList.Count; i++)
            {
                if (ExportBackgroundWorker.CancellationPending)
                    return false;

                KCL.Prism prism = CollisionData.PrismList[i];
                string matname = MaterialNames[prism.Attribute];
                AddTriangle(prism, matname);

                ProgressBarValue++;
                ReportProgress("Composing Triangles {0}%");
                if (i % mod == 0)
                    Thread.Sleep(5);
            }

            return true;
        }

        bool ExportOctree()
        {
            (int a, int b)[] CubeEdgeIdx = [
                // Bottom face (Z min)
                (0, 1), (1, 2), (2, 3), (3, 0),

                // Top face (Z max)
                (4, 5), (5, 6), (6, 7), (7, 4),

                // Vertical edges
                (0, 4), (1, 5), (2, 6), (3, 7)
            ];


            Vector3 size = CollisionData.RootCellSize;
            var enumerate = OctreeCube.EnumerateOctreeCubes(CollisionData, size, includeBranches: true, includeLeaves: true);
            StartProgress(enumerate.Count(), "Processing Octree {0}%");
            int i = 0;
            foreach (var cube in enumerate)
            {
                int PosCount = ResultObject.Vertices.Count + 1;
                Vector3[] verts = cube.GetVertices();
                foreach (var A in verts)
                    ResultObject.Vertices.Add(new(A.X, A.Y, A.Z));

                foreach ((int a, int b) in CubeEdgeIdx)
                {
                    ObjLine newline = new();
                    newline.Vertices.Add(new(PosCount + a, 0, 0));
                    newline.Vertices.Add(new(PosCount + b, 0, 0));
                    ResultObject.Lines.Add(newline);
                }

                ProgressBarValue++;
                ReportProgress("Processing Octree {0}%");

                if (i % 10 == 0) // updaet the UI every 10 prisms
                    Thread.Sleep(1);
                i++;
            }


            return true;
        }
    }

    private void ExportBackgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
    {
        float progress = e.ProgressPercentage / 10.0f;
        string msg = e.UserState?.ToString() ?? "";
        // Override the message with a cancellation message if a cancel has been requested
        if (ExportBackgroundWorker.CancellationPending)
            msg = "Cancelling...";
        MainProgressBar.Value = Math.Clamp((int)progress, MainProgressBar.Minimum, MainProgressBar.Maximum);
        StatusLabel.Text = string.Format(msg, progress.ToString("0.00"));
    }

    private void ExportBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
    {
        if (e.Cancelled)
        {
            DialogResult = DialogResult.Cancel;
            Close();
            return;
        }

        if (e.Result is not (ObjFile o, ObjMaterialFile m))
        {
            DialogResult = DialogResult.TryAgain;
            Close();
            return;
        }

        (mResultObject, mResultMaterial) = (o, m);

        DialogResult = DialogResult.OK;
        Close();
    }
}

public readonly record struct OctreeCube(Vector3 Min, Vector3 Max, int Depth, int RootIndex, bool IsLeaf, int PrismCount)
{
    public Vector3[] GetVertices() => [
        new(Min.X, Min.Y, Min.Z),
        new(Max.X, Min.Y, Min.Z),
        new(Max.X, Max.Y, Min.Z),
        new(Min.X, Max.Y, Min.Z),

        new(Min.X, Min.Y, Max.Z),
        new(Max.X, Min.Y, Max.Z),
        new(Max.X, Max.Y, Max.Z),
        new(Min.X, Max.Y, Max.Z),
    ];

    public static IEnumerable<OctreeCube> EnumerateOctreeCubes(KCL Src, Vector3? rootCellSize = null, bool includeBranches = true, bool includeLeaves = true)
    {
        Vector3 cellSize = rootCellSize ?? Vector3.One;
        (int rootX, int rootY, int rootZ) = Src.RootGridSize;

        int expected = rootX * rootY * rootZ;
        int rootCount = Math.Min(expected, Src.OctreeRoot.Count);

        for (int rz = 0; rz < rootZ; rz++)
            for (int ry = 0; ry < rootY; ry++)
                for (int rx = 0; rx < rootX; rx++)
                {
                    int rootIndex = rx + ry * rootX + rz * rootX * rootY;
                    if ((uint)rootIndex >= (uint)rootCount)
                        continue;

                    Vector3 rootMin = Src.MinCoords + new Vector3(rx * cellSize.X, ry * cellSize.Y, rz * cellSize.Z);
                    Vector3 rootMax = rootMin + cellSize;

                    foreach (var cube in EnumerateNodeCubes(Src.OctreeRoot[rootIndex], rootMin, rootMax, 0, rootIndex, includeBranches, includeLeaves))
                        yield return cube;
                }
    }
    private static IEnumerable<OctreeCube> EnumerateNodeCubes(KCL.IOctreeNode node, Vector3 min, Vector3 max, int depth, int rootIndex, bool includeBranches, bool includeLeaves)
    {
        // I think I over-engineered this a bit... lol
        if (node is not KCL.OctreeBranch branch)
        {
            if (includeLeaves)
                yield return new OctreeCube(min, max, depth, rootIndex, true, node.PrismCount);
            yield break;
        }

        if (includeBranches)
            yield return new OctreeCube(min, max, depth, rootIndex, false, node.PrismCount);

        Vector3 mid = (min + max) * 0.5f;
        for (int child = 0; child < branch.Count && child < 8; child++)
        {
            bool xHigh = (child & 1) != 0;
            bool yHigh = (child & 2) != 0;
            bool zHigh = (child & 4) != 0;

            Vector3 cMin = new(
                xHigh ? mid.X : min.X,
                yHigh ? mid.Y : min.Y,
                zHigh ? mid.Z : min.Z);

            Vector3 cMax = new(
                xHigh ? max.X : mid.X,
                yHigh ? max.Y : mid.Y,
                zHigh ? max.Z : mid.Z);

            foreach (var cube in EnumerateNodeCubes(branch[child], cMin, cMax, depth + 1, rootIndex, includeBranches, includeLeaves))
                yield return cube;
        }
    }
}