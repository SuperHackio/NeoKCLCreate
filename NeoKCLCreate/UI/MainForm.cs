using DarkModeForms;
using Hack.io.BCSV;
using Hack.io.KCL;
using Hack.io.Utility;
using JeremyAnsel.Media.WavefrontObj;
using NAudio.Vorbis;
using NAudio.Wave;
using NeoKCLCreate.Formats;
using NeoKCLCreate.UI;
using System.Diagnostics.CodeAnalysis;
using static Hack.io.BCSV.BCSV;

namespace NeoKCLCreate;

public partial class MainForm : Form
{
    private const string NAME = "Neo KCL Create";
    private const uint HASH_GROUPNAME = 0x1F0AAD8A;
    private const uint HASH_FLOOR = 0x040D33EC;
    private const uint HASH_WALL = 0x002905EA;
    private const uint HASH_SOUND = 0x04C5D8EF;
    private const uint HASH_PRESET = 0x8EF9CEFF;
    private const string FILTER_KCL = "KCollision file|*.kcl";
    private const string FILTER_OBJ = "Wavefront Obj|*.obj";

    private readonly OpenFileDialog kclofd = new() { Filter = FILTER_KCL };
    private readonly OpenFileDialog objofd = new() { Filter = FILTER_OBJ };
    private readonly SaveFileDialog kclsfd = new() { Filter = FILTER_KCL };
    private readonly SaveFileDialog objsfd = new() { Filter = FILTER_OBJ };
    private readonly string? OpenWithString;
    private readonly BCSV CollisionCodeDefinitionData;
    private readonly PresetForm PresetMenu;

    [AllowNull]
    private string mFileName;
    [AllowNull]
    private string FileName
    {
        get => mFileName;
        set
        {
            mFileName = value;
            Text = $"{NAME} [{UpdateInformation.ApplicationVersion}]";
            if (!string.IsNullOrWhiteSpace(value))
                Text += " - " + value;
        }
    }
    [AllowNull]
    private string SaveFilePath { get; set; }
    [AllowNull]
    private string FilenameSuggestionKCL => FileName + ".kcl";
    [AllowNull]
    private string FilenameSuggestionOBJ => FileName + ".obj";
    [AllowNull]
    private KCL CollisionData { get; set; }
    [AllowNull]
    private PA CollisionCode { get; set; }



    public MainForm(string[] args)
    {
        if (Program.IsGameFileLittleEndian)
            StreamUtil.PushEndianLittle();
        else
            StreamUtil.PushEndianBig();

        InitializeComponent();
        CenterToScreen();

        FileName = null;
        PresetMenu = new(this);
        ProgramColors.ReloadTheme(PresetMenu);

        #region Load Game Options
        StreamUtil.PushEndianBig(); //I'm keeping it Big Endian for the funny

        string CollisionCodeDefinitionPath = Path.Combine(Program.ASSET_PATH, "_CollisionCodeDefinition.bcsv");
        if (!File.Exists(CollisionCodeDefinitionPath))
            throw new FileNotFoundException($"Failed to find {CollisionCodeDefinitionPath}");
        CollisionCodeDefinitionData = new();
        FileUtil.LoadFile(CollisionCodeDefinitionPath, CollisionCodeDefinitionData.Load);

        CollisionCodeToolStripColorComboBox.Items.Clear();
        CollisionCodeToolStripColorComboBox.Items.Add("No Game");

        for (int i = 0; i < CollisionCodeDefinitionData.EntryCount; i++)
        {
            string str = (string)CollisionCodeDefinitionData[i][CollisionCodeDefinitionData[HASH_GROUPNAME]];
            CollisionCodeToolStripColorComboBox.Items.Add(str);
        }

        int idx = Program.Settings.CollisionCodeIndex;
        if (idx < 0 || idx >= CollisionCodeToolStripColorComboBox.Items.Count)
            idx = 0; // Reset if out of range

        CollisionCodeToolStripColorComboBox.SelectedIndex = idx;

        StreamUtil.PopEndian();
        #endregion

        IncludeNamesToolStripComboBox.SelectedIndex = Program.Settings.MaterialNameIncludeIndex;

        ThemeToolStripComboBox.SelectedIndex = Program.Settings.ThemeIndex; //Default to Light for the funny
        ProgramColors.IsDarkMode = ThemeToolStripComboBox.SelectedIndex == 1;

        if (args.Length > 0)
            OpenWithString = args[0];
    }

    public void New(string path)
    {
        ImportForm IF = new();
        ProgramColors.ReloadTheme(IF);

    TryAgain:
        if (IF.ShowDialog() != DialogResult.OK)
            return;

        FileInfo fi = new(path);
        ObjFile SourceObject;
        try
        {
            SourceObject = ObjFile.FromFile(path);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message+"\n\nPlease validate your .obj file.", "Failed to read Wavefront Object", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        ObjMaterialFile? SourceMaterial = null;
        try
        {
            string mtlname = Path.Combine(fi.DirectoryName!, fi.Name.Replace(fi.Extension, ".mtl"));
            if (File.Exists(mtlname))
                SourceMaterial = ObjMaterialFile.FromFile(mtlname);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + "\n\nPlease validate your .mtl file.", "Failed to read Material Library", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        ImportProgressForm IPF = new(SourceObject, SourceMaterial, IF.PrismThickness, IF.ValueRounding, IF.MaximumTrianglesPerLeaf, IF.MinimumLeafSize, IF.MaximumDepth);
        ProgramColors.ReloadTheme(IPF);

        DialogResult DR = IPF.ShowDialog();
        if (DR == DialogResult.TryAgain)
            goto TryAgain;
        if (DR != DialogResult.OK)
            return;

        CollisionData = IPF.Result;
        CollisionCode = IPF.ResultCodes;

        SaveFilePath = null;
        FileName = fi.Name[..^fi.Extension.Length];

        InitPropertyGrid();
        InitUi();
    }

    public void Open(string path)
    {
        FileInfo fi = new(path);
        string pa = Path.Combine(fi.DirectoryName!, fi.Name.Replace(fi.Extension, ".pa"));
        if (!File.Exists(pa))
        {
            MessageBox.Show($"Unable to find {fi.Name.Replace(fi.Extension, ".pa")}!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        CollisionData = new();
        FileUtil.LoadFile(path, CollisionData.Load);

        CollisionCode = new();
        FileUtil.LoadFile(pa, CollisionCode.Load);

        // If the custom "Name" field is missing, quickly add it with some default names
        if (!CollisionCode.ContainsField(PA.HASH_NAME))
        {
            Field f = new() { AutoRecalc = true, HashName = PA.HASH_NAME, DataType = DataTypes.STRING };
            CollisionCode.Add(f);
            for (int i = 0; i < CollisionCode.EntryCount; i++)
            {
                PA.Entry entry = CollisionCode[i];
                entry.Name = $"Material {i:0000}";
            }
        }

        SaveFilePath = path;
        FileName = fi.Name[..^fi.Extension.Length];

        InitPropertyGrid();
        InitUi();
    }

    /// <summary>
    /// Sets up the KCL editing UI, including restarting it for a new KCL
    /// </summary>
    public void InitUi()
    {
        SuspendLayout();
        MaterialListBox.Items.Clear();

        for (int i = 0; i < CollisionCode.EntryCount; i++)
        {
            PA.Entry entry = CollisionCode[i];
            MaterialListBox.Items.Add(entry.Name);
        }
        if (MaterialListBox.Items.Count > 0)
            MaterialListBox.SelectedIndex = 0;

        SaveToolStripMenuItem.Enabled = SaveAsToolStripMenuItem.Enabled = ExportGeometryToolStripMenuItem.Enabled = ExportOctreeToolStripMenuItem.Enabled = true;
        CrushToolStripMenuItem.Enabled = PresetsToolStripMenuItem.Enabled = true;

        ResumeLayout();
        Program.IsUnsavedChanges = false;
    }

    public void InitPropertyGrid()
    {
        if (CollisionData is null)
            return;

        CollisionCode.FloorCodeOptions = [];
        CollisionCode.WallCodeOptions = [];
        CollisionCode.SoundCodeOptions = [];
        CollisionCode.CameraThroughOptions = [];

        if (CollisionCodeToolStripColorComboBox.SelectedIndex <= 0)
            goto DoEmptyValues;

        if (CollisionCodeToolStripColorComboBox.SelectedIndex > CollisionCodeDefinitionData.EntryCount)
            goto DoEmptyValues;

        int idx = CollisionCodeToolStripColorComboBox.SelectedIndex - 1;
        string FloorName = (string)CollisionCodeDefinitionData[idx][CollisionCodeDefinitionData[HASH_FLOOR]];
        string WallName = (string)CollisionCodeDefinitionData[idx][CollisionCodeDefinitionData[HASH_WALL]];
        string SoundName = (string)CollisionCodeDefinitionData[idx][CollisionCodeDefinitionData[HASH_SOUND]];

        FloorName = Path.Combine(Program.ASSET_PATH, FloorName + ".bcsv");
        WallName = Path.Combine(Program.ASSET_PATH, WallName + ".bcsv");
        SoundName = Path.Combine(Program.ASSET_PATH, SoundName + ".bcsv");

        CollisionCode.FloorCodeOptions = ReadBCSV(FloorName, PA.HASH_FLOOR_CODE, PA.HASH_NAME);
        CollisionCode.WallCodeOptions = ReadBCSV(WallName, PA.HASH_WALL_CODE, PA.HASH_NAME);
        CollisionCode.SoundCodeOptions = ReadBCSV(SoundName, PA.HASH_SOUND_CODE, PA.HASH_NAME);
        CollisionCode.CameraThroughOptions = ["Do Not Pass", "Go Through"];

    DoEmptyValues:
        MaterialSettingsPropertyGrid.Refresh();
    }

    public void UpdatePresetMenu()
    {
        if (CollisionCodeToolStripColorComboBox.SelectedIndex <= 0)
        {
            PresetMenu.UpdatePresetList(null);
            return;
        }
        int idx = CollisionCodeToolStripColorComboBox.SelectedIndex - 1;
        string PresetName = (string)CollisionCodeDefinitionData[idx][CollisionCodeDefinitionData[HASH_PRESET]];
        PresetName = Path.Combine(Program.ASSET_PATH, PresetName + ".bcsv");
        if (!File.Exists(PresetName))
            throw new FileNotFoundException($"Failed to find {PresetName}");
        StreamUtil.PushEndianBig();
        BCSV Data = new();
        FileUtil.LoadFile(PresetName, Data.Load);
        StreamUtil.PopEndian();
        PresetMenu.UpdatePresetList(Data);
    }

    public void UpdateSelectedCodes(int? FloorCode, int? WallCode, int? SoundCode, int? CameraId, int? CameraThrough)
    {
        if (MaterialListBox.SelectedItems.Count <= 0 || CollisionCode is null)
        {
            MaterialSettingsPropertyGrid.SelectedObjects = [];
            return;
        }

        foreach (int item in MaterialListBox.SelectedIndices)
        {
            PA.Entry entry = CollisionCode[item];

            if (FloorCode is int fc)
                entry.FloorCode = fc;

            if (WallCode is int wc)
                entry.WallCode = wc;

            if (SoundCode is int sc)
                entry.SoundCode = sc;

            if (CameraId is int ci)
                entry.CameraId = ci;

            if (CameraThrough is int ct)
                entry.CameraThrough = ct;
        }

        MaterialSettingsPropertyGrid.Refresh();
    }

    private static string[] ReadBCSV(string FilePath, uint Hash, uint Hash2)
    {
        if (!File.Exists(FilePath))
            throw new FileNotFoundException($"Failed to find {FilePath}");

        StreamUtil.PushEndianBig();
        BCSV Data = new();
        FileUtil.LoadFile(FilePath, Data.Load);
        StreamUtil.PopEndian();

        if (!Data.ContainsField(Hash) || !Data.ContainsField(Hash2))
            return [];

        SortedDictionary<int, string> CollisionCodes = [];
        for (int i = 0; i < Data.EntryCount; i++)
        {
            int code = (int)Data[i][Data[Hash]];
            string name = (string)Data[i][Data[Hash2]];
            CollisionCodes.TryAdd(code, name);
        }
        return [.. CollisionCodes.Values];
    }

    private static string? GetSoundFilePath(string SoundName, int val)
    {
        if (!File.Exists(SoundName))
            return null;

        string[] Paths = ReadBCSV(SoundName, PA.HASH_SOUND_CODE, HASH_SOUND);
        if (val < 0 || val >= Paths.Length)
            return null;

        string path = Paths[val];
        if (string.IsNullOrWhiteSpace(path))
            return null;

        return Path.Combine(Program.ASSET_PATH, path);
    }

    private static bool IsDiscardChanges()
        => MessageBox.Show("You have Unsaved Changes, are you sure you want to proceed?", "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;


    private void MainForm_Shown(object sender, EventArgs e)
    {
        if (OpenWithString is null)
            return;

        if (!File.Exists(OpenWithString))
            return;

        if (OpenWithString.EndsWith(".obj"))
            New(OpenWithString);
        else
            Open(OpenWithString);
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (e.CloseReason == CloseReason.UserClosing && Program.IsUnsavedChanges && !IsDiscardChanges())
            e.Cancel = true;
    }

    private void NewToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (Program.IsUnsavedChanges && !IsDiscardChanges())
            return;

        if (objofd.ShowDialog() != DialogResult.OK)
            return;

        New(objofd.FileName);
    }

    private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (Program.IsUnsavedChanges && !IsDiscardChanges())
            return;

        if (kclofd.ShowDialog() != DialogResult.OK)
            return;

        Open(kclofd.FileName);
    }

    private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (CollisionData is null)
            return;

        if (SaveFilePath is null)
        {
            SaveAsToolStripMenuItem_Click(sender, e);
            return;
        }

        FileInfo fi = new(SaveFilePath);
        string KCLPath = SaveFilePath;
        string PAPath = SaveFilePath[..^fi.Extension.Length] + ".pa";

        // The KCL is fully generated on import, so we can just save it directly later.
        // For now, we need to prepare a final BCSV to write

        BCSV FinalPA = new()
        {
            OnSaveFieldCalculator = PA.CalculateFieldData // Thank goodness I programmed this to be a delegate!
        };
        bool IncludeNames = IncludeNamesToolStripComboBox.SelectedIndex == 1;

        List<Field> PAFields = PA.PreMadeFields;
        for (int i = 0; i < PAFields.Count; i++)
        {
            if (i < PAFields.Count - 1 || IncludeNames)
                FinalPA.Add(PAFields[i]);
        }
        for (int i = 0; i < CollisionCode.EntryCount; i++)
        {
            PA.Entry source = CollisionCode[i];
            BCSV.Entry dest = new();
            source.CopyTo(dest);
            if (!IncludeNames && dest.Contains(PA.HASH_NAME))
                dest.Remove(PA.HASH_NAME);
            FinalPA.Add(dest);
        }

        FileUtil.SaveFile(KCLPath, CollisionData.Save);
        FileUtil.SaveFile(PAPath, FinalPA.Save);

        MessageBox.Show("Save Complete!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (CollisionData is null)
            return;

        kclsfd.FileName = FilenameSuggestionKCL;
        if (kclsfd.ShowDialog() != DialogResult.OK)
            return;
        SaveFilePath = kclsfd.FileName;
        SaveToolStripMenuItem_Click(sender, e);
    }

    private void ExportGeometryToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (CollisionData is null)
            return;

        objsfd.FileName = FilenameSuggestionOBJ;
        if (objsfd.ShowDialog() != DialogResult.OK)
            return;

        ExportProgressForm EPF = new(CollisionData, CollisionCode, FileName, false);
        ProgramColors.ReloadTheme(EPF);

        if (EPF.ShowDialog() != DialogResult.OK)
            return;

        FileInfo fi = new(objsfd.FileName);
        string mtlname = Path.Combine(fi.DirectoryName!, fi.Name.Replace(fi.Extension, ".mtl"));

        FileUtil.SaveFile(objsfd.FileName, EPF.ResultObject.WriteTo); // These somehow fit my utility function lol
        FileUtil.SaveFile(mtlname, EPF.ResultMaterial.WriteTo);
    }

    private void ExportOctreeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (CollisionData is null)
            return;

        objsfd.FileName = FilenameSuggestionOBJ;
        if (objsfd.ShowDialog() != DialogResult.OK)
            return;

        ExportProgressForm EPF = new(CollisionData, CollisionCode, FileName, true);
        ProgramColors.ReloadTheme(EPF);

        if (EPF.ShowDialog() != DialogResult.OK)
            return;

        FileUtil.SaveFile(objsfd.FileName, EPF.ResultObject.WriteTo); // These somehow fit my utility function lol
        // No materials for the Octree
    }

    private void CrushToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (CollisionData is null)
            return;

        if (MessageBox.Show("This will merge all materials that have the same properties, except for their name.\n\nThis operation cannot be undone.\n\nContinue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            return;

        List<PA.Entry> UniqueEntries = [];
        for (int p = 0; p < CollisionData.PrismList.Count; p++)
        {
            KCL.Prism prism = CollisionData.PrismList[p];
            ushort attribute = prism.Attribute;

            PA.Entry entry = CollisionCode[attribute];
            int idx = UniqueEntries.Count;
            for (int i = 0; i < UniqueEntries.Count; i++)
            {
                PA.Entry ee = UniqueEntries[i];
                if (ee.FloorCode == entry.FloorCode &&
                    ee.WallCode == entry.WallCode &&
                    ee.SoundCode == entry.SoundCode &&
                    ee.CameraId == entry.CameraId &&
                    ee.CameraThrough == entry.CameraThrough)
                {
                    idx = i;
                    break;
                }
            }

            if (idx == UniqueEntries.Count) // Add
                UniqueEntries.Add(entry);

            prism.Attribute = (ushort)idx;
            CollisionData.PrismList[p] = prism;
        }
        CollisionCode.Clear();
        CollisionCode.AddRange(UniqueEntries);

        InitUi();
    }

    private void PresetsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (CollisionData is null)
            return;

        if (PresetMenu.Visible)
        {
            if (PresetMenu.WindowState == FormWindowState.Minimized)
                PresetMenu.WindowState = FormWindowState.Normal;
            PresetMenu.Focus();
        }
        else
            PresetMenu.Show();
    }

    private void MaterialListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (MaterialListBox.SelectedItems.Count <= 0 || CollisionCode is null)
        {
            MaterialSettingsPropertyGrid.SelectedObjects = [];
            return;
        }

        List<PA.Entry> Selection = new(MaterialListBox.SelectedItems.Count + 1);
        foreach (int item in MaterialListBox.SelectedIndices)
        {
            Selection.Add(CollisionCode[item]);
        }
        MaterialSettingsPropertyGrid.SelectedObjects = [.. Selection];
    }

    private void ThemeToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        ProgramColors.IsDarkMode = ThemeToolStripComboBox.SelectedIndex == 1;
        Program.ReloadTheme();
        ProgramColors.ReloadTheme(PresetMenu);
        ProgramColors.ReloadTheme(PlaySoundContextMenuStrip);
        SettingsToolStripMenuItem.HideDropDown();
        Program.Settings.ThemeIndex = ThemeToolStripComboBox.SelectedIndex;
        Program.Settings.OnChanged(sender, e);
    }

    private void IncludeNamesToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        Program.Settings.MaterialNameIncludeIndex = IncludeNamesToolStripComboBox.SelectedIndex;
        Program.Settings.OnChanged(sender, e);
    }

    private void CollisionCodeToolStripColorComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        Program.Settings.CollisionCodeIndex = CollisionCodeToolStripColorComboBox.SelectedIndex;
        Program.Settings.OnChanged(sender, e);
        InitPropertyGrid();
        UpdatePresetMenu();
    }

    private void MaterialSettingsPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {
        if (MaterialListBox.SelectedItems.Count <= 0 || CollisionCode is null)
            return;

        object? newValue = e.ChangedItem?.Value;
        string? propertyName = e.ChangedItem?.PropertyDescriptor?.Name;

        if (newValue is null)
            return;

        if (!newValue.Equals(e.OldValue))
            Program.IsUnsavedChanges = true;

        if (propertyName is null || !propertyName.Equals("Name"))
            return;

        List<int> selectd = [];
        foreach (int item in MaterialListBox.SelectedIndices)
        {
            MaterialListBox.Items[item] = newValue;
            selectd.Add(item);
        }
        MaterialListBox.SelectedIndices.Clear();
        foreach (var item in selectd)
            MaterialListBox.SelectedIndices.Add(item);
    }

    private void PlaySoundContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (CollisionCode is null || CollisionCodeToolStripColorComboBox.SelectedIndex <= 0)
            goto DisableSounds;

        GridItem gi = MaterialSettingsPropertyGrid.SelectedGridItem;
        if (!gi.Label?.Contains("Sound") ?? false)
            goto DisableSounds;

        int idx = CollisionCodeToolStripColorComboBox.SelectedIndex - 1;
        string SoundName = (string)CollisionCodeDefinitionData[idx][CollisionCodeDefinitionData[HASH_SOUND]];
        SoundName = Path.Combine(Program.ASSET_PATH, SoundName + ".bcsv");
        string? ogg = GetSoundFilePath(SoundName, (int?)gi.Value ?? -1);
        if (ogg is null)
            goto DisableSounds;

        if (!File.Exists(ogg))
            goto DisableSounds;

        return;

    DisableSounds:
        e.Cancel = true;
    }

    private void PlaySoundToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (CollisionCode is null || CollisionCodeToolStripColorComboBox.SelectedIndex <= 0)
            return;

        GridItem gi = MaterialSettingsPropertyGrid.SelectedGridItem;
        if (!gi.Label?.Contains("Sound") ?? false)
            return;

        int idx = CollisionCodeToolStripColorComboBox.SelectedIndex - 1;
        string SoundName = (string)CollisionCodeDefinitionData[idx][CollisionCodeDefinitionData[HASH_SOUND]];
        SoundName = Path.Combine(Program.ASSET_PATH, SoundName + ".bcsv");
        string? ogg = GetSoundFilePath(SoundName, (int?)gi.Value ?? -1);
        if (ogg is null)
            return;
        if (!File.Exists(ogg))
            return;

        using var vorbisReader = new VorbisWaveReader(ogg);
        using var outputDevice = new WaveOutEvent();
        outputDevice.Init(vorbisReader);
        outputDevice.Play();
        while (outputDevice.PlaybackState == PlaybackState.Playing)
            Application.DoEvents(); // More stable than I thought it would be
    }
}