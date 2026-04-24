using DarkModeForms;
using Hack.io.BCSV;

namespace NeoKCLCreate.UI;

public partial class PresetForm : Form
{
    private const uint HASH_DESCRIPTION = 0xFC9F2BDC;
    private const uint HASH_PREVIEW = 0x50417BA8;
    private readonly MainForm mMainForm;
    private BCSV? PresetData;



    public PresetForm(MainForm mMainForm)
    {
        InitializeComponent();

        this.mMainForm = mMainForm;

        MainTableLayoutPanel.SetDoubleBuffered();
        ListPanel.SetDoubleBuffered();
        PresetListBox.SetDoubleBuffered();
        PreviewPictureBox.SetDoubleBuffered();

        UpdatePresetList(null);
    }

    public void UpdatePresetList(BCSV? PresetBCSV)
    {
        SuspendLayout();
        if (PresetBCSV is null)
            goto NoPresets;

        if (PresetBCSV.EntryCount == 0)
            goto NoPresets;

        DisabledLabel.Visible = false;
        MainTableLayoutPanel.Visible = MainTableLayoutPanel.Enabled = ApplyButton.Enabled = true;
        PresetData = PresetBCSV;

        PresetListBox.Items.Clear();
        for (int i = 0; i < PresetData.EntryCount; i++)
        {
            BCSV.Entry entry = PresetData[i];
            string name = (string)entry[PresetData[Formats.PA.HASH_NAME]];
            PresetListBox.Items.Add(name);
        }
        if (PresetListBox.Items.Count > 0)
            PresetListBox.SelectedIndex = 0;

        ResumeLayout();
        return;

    NoPresets:
        // No presets if No Game is selected
        MainTableLayoutPanel.Visible = MainTableLayoutPanel.Enabled = ApplyButton.Enabled = false;
        DisabledLabel.Visible = true;
        PresetData = null;
        ResumeLayout();
    }

    private void PresetForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        Hide();
    }

    private void PresetListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (PresetListBox.SelectedIndex < 0 || PresetData is null)
        {
            InfoLabel.Text = "";
            return;
        }

        BCSV.Entry entry = PresetData[PresetListBox.SelectedIndex];
        string desc = (string)entry[PresetData[HASH_DESCRIPTION]];
        InfoLabel.Text = desc;

        string PreviewName = "";
        if (PresetData.ContainsField(HASH_PREVIEW))
            PreviewName = (string)entry[PresetData[HASH_PREVIEW]];

        if (string.IsNullOrWhiteSpace(PreviewName))
            PreviewName = "NoPreview.png";

        if (PreviewPictureBox.Image is not null)
            if (!ReferenceEquals(PreviewPictureBox.Image, PreviewPictureBox.ErrorImage))
                PreviewPictureBox.Image.Dispose();

        string finalpreviewpath = Path.Combine(Program.ASSET_PATH, PreviewName);
        if (File.Exists(finalpreviewpath))
            PreviewPictureBox.Image = Image.FromFile(finalpreviewpath);
        else
            PreviewPictureBox.Image = PreviewPictureBox.ErrorImage;
    }

    private void ApplyButton_Click(object sender, EventArgs e)
    {
        if (mMainForm is not MainForm MF)
            return;

        if (PresetListBox.SelectedIndex < 0 || PresetData is null)
            return;

        BCSV.Entry entry = PresetData[PresetListBox.SelectedIndex];

        int FloorCode = (int)entry[PresetData[Formats.PA.HASH_FLOOR_CODE]];
        int WallCode = (int)entry[PresetData[Formats.PA.HASH_WALL_CODE]];
        int SoundCode = (int)entry[PresetData[Formats.PA.HASH_SOUND_CODE]];
        int CameraId = (int)entry[PresetData[Formats.PA.HASH_CAMERA_ID]];
        int CameraThrough = (int)entry[PresetData[Formats.PA.HASH_CAMERA_THROUGH]];

        MF.UpdateSelectedCodes(FloorCode < 0 ? null : FloorCode, WallCode < 0 ? null : WallCode, SoundCode < 0 ? null : SoundCode, CameraId < 0 ? null : CameraId, CameraThrough < 0 ? null : CameraThrough);
    }
}