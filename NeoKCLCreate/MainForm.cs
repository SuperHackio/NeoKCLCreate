using Hack.io.BCSV;
using Hack.io.KCL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeoKCLCreate
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            CenterToScreen();

            SoundCodeComboBox.Items.AddRange(KCL.PaEntry.SOUND_CODES);
            FloorCodeComboBox.Items.AddRange(KCL.PaEntry.FLOOR_CODES);
            WallCodeComboBox.Items.AddRange(KCL.PaEntry.WALL_CODES);

            SoundCodeComboBox.SelectedIndex = 0;
            FloorCodeComboBox.SelectedIndex = 0;
            WallCodeComboBox.SelectedIndex = 0;

            PF = new PresetForm(this);
        }

        PresetForm PF;
        WavefrontObj CurrentKCL;
        BCSV CollisionCodes;
        string _lufn;
        string LastUsedFilename
        {
            get => _lufn ?? "";
            set
            {
                if (value is null)
                    Text = "Neo KCL Creator";
                else
                    Text = $"Neo KCL Creator - {new FileInfo(value).Name}";
                _lufn = value;
            }
        }

        /// <summary>
        /// Creates a new KCL from an OBJ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog() { Filter = "Wavefront Obj|*.obj" };
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            CurrentKCL = WavefrontObj.OpenWavefront(ofd.FileName);
            CollisionCodes = KCL.PaEntry.CreateBCSV(CurrentKCL);
            LoadKCLData(CurrentKCL);
            LastUsedFilename = ofd.FileName;
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog() { Filter = "KCollision file|*.kcl" };
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            FileInfo fi = new FileInfo(ofd.FileName);
            string pa = Path.Combine(fi.DirectoryName, fi.Name.Replace(fi.Extension, ".pa"));
            if (!File.Exists(pa))
            {
                MessageBox.Show($"Failed to find {fi.Name.Replace(fi.Extension, ".pa")}!");
                return;
            }



            KCL KCL = new KCL(ofd.FileName);
            CurrentKCL = WavefrontObj.CreateWavefront(KCL);
            CollisionCodes = new BCSV(Path.Combine(fi.DirectoryName, fi.Name.Replace(fi.Extension, ".pa")));
            KCL.PaEntry.ConvertBCSV(ref CollisionCodes);
            LoadKCLData(CurrentKCL);
            LastUsedFilename = ofd.FileName;
        }

        private void LoadKCLData(WavefrontObj obj)
        {
            GroupListBox.Items.Clear();

            for (int i = 0; i < obj.GroupNames.Count; i++)
            {
                GroupListBox.Items.Add(obj.GroupNames[i]);
            }
            if (GroupListBox.Items.Count > 0)
                GroupListBox.SelectedIndex = 0;
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentKCL is null)
                return;
            LastUsedFilename = _lufn;
            Text += " (Building KCL...)";
            SaveBackgroundWorker.RunWorkerAsync(LastUsedFilename);
            LastUsedFilename = _lufn;
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentKCL is null)
                return;
            SaveFileDialog sfd = new SaveFileDialog() { Filter = "KCollision file|*.kcl" };
            if (sfd.ShowDialog() != DialogResult.OK)
                return;
            LastUsedFilename = sfd.FileName;
            Text += " (Building KCL...)";
            SaveBackgroundWorker.RunWorkerAsync(sfd.FileName);
            LastUsedFilename = _lufn;
        }

        private void GenerateKCLData(string filename)
        {
            KCL kcl = new KCL(CurrentKCL, CollisionCodes, (int)MaxTrianglesNumericUpDown.Value, (int)MinCubeSizeNumericUpDown.Value);
            kcl.Save(filename.Replace(".obj",".kcl"));
            CollisionCodes.Save(filename.Replace(".obj", ".pa").Replace(".kcl", ".pa"));

            CurrentKCL = WavefrontObj.CreateWavefront(kcl);
        }

        private void ExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentKCL is null)
                return;

            FileInfo fi = new FileInfo(LastUsedFilename);
            SaveFileDialog sfd = new SaveFileDialog() { Filter = "Wavefront Obj|*.obj", FileName = fi.Name.Replace(fi.Extension, ".obj") };
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            WavefrontObj.SaveWavefront(sfd.FileName, CurrentKCL, CollisionCodes);
            MessageBox.Show("Collision Exported to OBJ!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void GroupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CollisionCodes is null || GroupListBox.SelectedIndex < 0)
                return;

            CameraIDNumericUpDown.Value = ((KCL.PaEntry)CollisionCodes[GroupListBox.SelectedIndex]).CameraID;
            SoundCodeComboBox.SelectedIndex = ((KCL.PaEntry)CollisionCodes[GroupListBox.SelectedIndex]).SoundCode;
            FloorCodeComboBox.SelectedIndex = ((KCL.PaEntry)CollisionCodes[GroupListBox.SelectedIndex]).FloorCode;
            WallCodeComboBox.SelectedIndex = ((KCL.PaEntry)CollisionCodes[GroupListBox.SelectedIndex]).WallCode;
            CameraThroughCheckBox.Checked = ((KCL.PaEntry)CollisionCodes[GroupListBox.SelectedIndex]).CameraThrough > 0;
        }

        private void SaveBackgroundWorker_DoWork(object sender, DoWorkEventArgs e) => GenerateKCLData((string)e.Argument);

        private void SaveBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) => MessageBox.Show("KCL File Generated!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) => e.Cancel = SaveBackgroundWorker.IsBusy;

        private bool IsInvalidCollisionEdit() => CurrentKCL is null || CollisionCodes is null || GroupListBox.SelectedIndex < 0;

        private void CameraIDNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (IsInvalidCollisionEdit())
                return;
            ((KCL.PaEntry)CollisionCodes[GroupListBox.SelectedIndex]).CameraID = (int)CameraIDNumericUpDown.Value;
        }

        private void SoundCodeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsInvalidCollisionEdit())
                return;
            ((KCL.PaEntry)CollisionCodes[GroupListBox.SelectedIndex]).SoundCode = SoundCodeComboBox.SelectedIndex;
        }

        private void FloorCodeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsInvalidCollisionEdit())
                return;
            ((KCL.PaEntry)CollisionCodes[GroupListBox.SelectedIndex]).FloorCode = FloorCodeComboBox.SelectedIndex;
        }

        private void WallCodeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsInvalidCollisionEdit())
                return;
            ((KCL.PaEntry)CollisionCodes[GroupListBox.SelectedIndex]).WallCode = WallCodeComboBox.SelectedIndex;
        }

        private void CameraThroughCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (IsInvalidCollisionEdit())
                return;
            ((KCL.PaEntry)CollisionCodes[GroupListBox.SelectedIndex]).CameraThrough = CameraThroughCheckBox.Checked ? 1 : 0;
        }

        private void ResetOctreeMax_Click(object sender, EventArgs e)
        {
            MaxTrianglesNumericUpDown.Value = 25;
        }

        private void ResetOctreeMin_Click(object sender, EventArgs e)
        {
            MinCubeSizeNumericUpDown.Value = 8;
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsInvalidCollisionEdit())
                return;
            Clipboard.SetText(CollisionCodes[GroupListBox.SelectedIndex].ToClipboard());
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsInvalidCollisionEdit())
                return;
            if (!CollisionCodes[GroupListBox.SelectedIndex].FromClipboard(Clipboard.GetText()))
            {
                MessageBox.Show("The data in the clipboard is not compatable!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PresetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PF.Visible)
                PF.Hide();
            else
                PF.Show();
        }

        public void SetCollisionCode(int camera, int sound, int floor, int wall, bool through)
        {
            if (IsInvalidCollisionEdit())
                return;

            ((KCL.PaEntry)CollisionCodes[GroupListBox.SelectedIndex]).CameraID = camera;
            ((KCL.PaEntry)CollisionCodes[GroupListBox.SelectedIndex]).SoundCode = sound;
            ((KCL.PaEntry)CollisionCodes[GroupListBox.SelectedIndex]).FloorCode = floor;
            ((KCL.PaEntry)CollisionCodes[GroupListBox.SelectedIndex]).WallCode = wall;
            ((KCL.PaEntry)CollisionCodes[GroupListBox.SelectedIndex]).CameraThrough = through ? 1 : 0;
        }
    }
}
