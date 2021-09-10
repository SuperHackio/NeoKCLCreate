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
        }

        WavefrontObj CurrentKCL;
        BCSV CollisionCodes;

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
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentKCL is null)
                return;
            SaveFileDialog sfd = new SaveFileDialog() { Filter = "KCollision file|*.kcl" };
            if (sfd.ShowDialog() != DialogResult.OK)
                return;
        }

        private void ExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentKCL is null)
                return;

            SaveFileDialog sfd = new SaveFileDialog() { Filter = "Wavefront Obj|*.obj" };
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            WavefrontObj.SaveWavefront(sfd.FileName, CurrentKCL);
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
        }
    }
}
