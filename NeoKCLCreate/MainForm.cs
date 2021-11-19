using Hack.io.BCSV;
using Hack.io.KCL;
using Hack.io.RARC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeoKCLCreate
{
    public partial class MainForm : Form
    {
        const string NAME = "Neo KCL Creator";
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
            SetAppStatus(false);
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
                    Text = NAME;
                else
                    Text = $"{NAME} - {new FileInfo(value).Name}";
                _lufn = value;
            }
        }

        public void SetAppStatus(bool toggle)
        {
            OctreeGroupBox.Enabled =
            PaGroupBox.Enabled = toggle;
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

            Text = $"{NAME} - Loading Wavefront Object...";
            Invalidate();
            Update();
            CurrentKCL = WavefrontObj.OpenWavefront(ofd.FileName);
            CollisionCodes = KCL.PaEntry.CreateBCSV(CurrentKCL);
            LoadKCLDataToListBox(CurrentKCL);
            LastUsedFilename = ofd.FileName;
            SetAppStatus(true);
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

            LoadBackgroundWorker.RunWorkerAsync(new object[] { 0, ofd.FileName });
        }

        private (KCL, BCSV) LoadKCL(string file)
        {
            KCL result = new KCL(file, LoadBackgroundWorker);

            LoadBackgroundWorker.ReportProgress(0, 1);
            FileInfo fi = new FileInfo(file);
            BCSV col = new BCSV(Path.Combine(fi.DirectoryName, fi.Name.Replace(fi.Extension, ".pa")));

            return (result, col);
        }

        private void LoadKCLDataToListBox(WavefrontObj obj)
        {
            GroupListBox.BeginUpdate();
            GroupListBox.Items.Clear();

            for (int i = 0; i < obj.GroupNames.Count; i++)
            {
                GroupListBox.Items.Add(obj.GroupNames[i]);
            }
            if (GroupListBox.Items.Count > 0)
                GroupListBox.SelectedIndex = 0;
            GroupListBox.EndUpdate();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentKCL is null)
                return;
            LastUsedFilename = _lufn.Replace(".obj", ".kcl");
            Text += " (Building KCL...)";
            SetAppStatus(false);
            SaveBackgroundWorker.RunWorkerAsync(LastUsedFilename);
            LastUsedFilename = _lufn;
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentKCL is null)
                return;
            FileInfo fi = new FileInfo(LastUsedFilename);
            SaveFileDialog sfd = new SaveFileDialog() { Filter = "KCollision file|*.kcl", FileName = fi.Name.Replace(fi.Extension, ".kcl") };
            if (sfd.ShowDialog() != DialogResult.OK)
                return;
            LastUsedFilename = sfd.FileName;
            Text += " (Building KCL...)";
            SetAppStatus(false);
            SaveBackgroundWorker.RunWorkerAsync(sfd.FileName);
            LastUsedFilename = _lufn;
        }

        private void ExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentKCL is null)
                return;

            FileInfo fi = new FileInfo(LastUsedFilename);
            SaveFileDialog sfd = new SaveFileDialog() { Filter = "Wavefront Obj|*.obj", FileName = fi.Name.Replace(fi.Extension, ".obj") };
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            SetAppStatus(false);
            ExportBackgroundWorker.RunWorkerAsync(sfd.FileName);
        }

        private void GroupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsInvalidCollisionEdit())
                return;

            CameraIDNumericUpDown.Value = ((KCL.PaEntry)CollisionCodes[GroupListBox.SelectedIndex]).CameraID;
            SoundCodeComboBox.SelectedIndex = ((KCL.PaEntry)CollisionCodes[GroupListBox.SelectedIndex]).SoundCode;
            FloorCodeComboBox.SelectedIndex = ((KCL.PaEntry)CollisionCodes[GroupListBox.SelectedIndex]).FloorCode;
            WallCodeComboBox.SelectedIndex = ((KCL.PaEntry)CollisionCodes[GroupListBox.SelectedIndex]).WallCode;
            CameraThroughCheckBox.Checked = ((KCL.PaEntry)CollisionCodes[GroupListBox.SelectedIndex]).CameraThrough > 0;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) => e.Cancel = IsBackgroundWorkerActive();

        private bool IsInvalidCollisionEdit() => CurrentKCL is null || CollisionCodes is null || GroupListBox.SelectedIndex < 0 || IsBackgroundWorkerActive();
        private bool IsBackgroundWorkerActive() => LoadBackgroundWorker.IsBusy || SaveBackgroundWorker.IsBusy || ExportBackgroundWorker.IsBusy;

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

        private void LoadBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] args = (object[])e.Argument;
            if ((int)args[0] == 0)
            {
                e.Result = LoadKCL((string)args[1]);
            }
        }

        private void LoadBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if ((int)e.UserState == 0)
            {
                Text = $"{NAME} - Reading KCL Binary: {e.ProgressPercentage}%";
                TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Normal);
                TaskbarProgress.SetValue(Handle, e.ProgressPercentage, 100);
            }
            if ((int)e.UserState == 1)
            {
                Text = $"{NAME} - Reading Collision Codes...";
                TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Paused);
                TaskbarProgress.SetValue(Handle, 100, 100);
            }
        }

        private void LoadBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            (KCL kcl, BCSV pa) = ((KCL kcl, BCSV pa))e.Result;
            CurrentKCL = WavefrontObj.CreateWavefront(kcl);

            CollisionCodes = pa;
            KCL.PaEntry.ConvertBCSV(ref CollisionCodes);
            LoadKCLDataToListBox(CurrentKCL);
            LastUsedFilename = kcl.FileName;
            TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.NoProgress);
            TaskbarProgress.SetValue(Handle, 0, 100);
            SetAppStatus(true);
        }

        private void SaveBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string filename = (string)e.Argument;
            KCL kcl = new KCL(CurrentKCL, CollisionCodes, (int)MaxTrianglesNumericUpDown.Value, (int)MinCubeSizeNumericUpDown.Value, SaveBackgroundWorker);
            kcl.Save(filename.Replace(".obj", ".kcl"), SaveBackgroundWorker);
            SaveBackgroundWorker.ReportProgress(0, 3);
            CollisionCodes.Save(filename.Replace(".obj", ".pa").Replace(".kcl", ".pa"));

            e.Result = kcl;
        }

        private void SaveBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Normal);
            TaskbarProgress.SetValue(Handle, e.ProgressPercentage, 100);
            if ((int)e.UserState == 0)
            {
                Text = $"{NAME} - Generating KCL Data: {e.ProgressPercentage}%";
            }
            else if ((int)e.UserState == 1)
            {
                Text = $"{NAME} - Generating KCL Octree: {e.ProgressPercentage}%";
            }
            else if ((int)e.UserState == 2)
            {
                Text = $"{NAME} - Writing KCL Binary: {e.ProgressPercentage}%";
            }
            else
            {
                Text = $"{NAME} - Writing Collision Codes...";
            }
        }

        private void SaveBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Error);
                //Text = $"{NAME} - Save Error";
                StackTrace st = new StackTrace(e.Error, true);

                MessageBox.Show($"An error has occoured:\n{e.Error?.Message ??"NO ERROR! THIS IS A TEST MESSAGE!"}\nLine {st.GetFrame(0).GetFileLineNumber()}:\n{e.Error.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                Text = $"{NAME} - Save Complete!";
                MessageBox.Show("KCL File Generated!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                KCL kcl = (KCL)e.Result;
                CurrentKCL = WavefrontObj.CreateWavefront(kcl);
                LastUsedFilename = kcl.FileName;
            }
            TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.NoProgress);
            TaskbarProgress.SetValue(Handle, 0, 100);
            SetAppStatus(true);
        }

        private void ExportBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            WavefrontObj.SaveWavefront((string)e.Argument, CurrentKCL, CollisionCodes, ExportBackgroundWorker);
        }

        private void ExportBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if ((int)e.UserState == 0)
            {
                Text = $"{NAME} - Preparing Obj Export...";
            }
            else
            {
                Text = $"{NAME} - Exporting Obj: {e.ProgressPercentage}%";
            }
            TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Normal);
            TaskbarProgress.SetValue(Handle, e.ProgressPercentage, 100);
        }

        private void ExportBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Collision Exported to OBJ!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LastUsedFilename = LastUsedFilename; //Reset the text
            SetAppStatus(true);
        }


        public static class TaskbarProgress
        {
            public enum TaskbarStates
            {
                NoProgress = 0,
                Indeterminate = 0x1,
                Normal = 0x2,
                Error = 0x4,
                Paused = 0x8
            }

            [ComImport()]
            [Guid("ea1afb91-9e28-4b86-90e9-9e9f8a5eefaf")]
            [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            private interface ITaskbarList3
            {
                // ITaskbarList
                [PreserveSig]
                void HrInit();
                [PreserveSig]
                void AddTab(IntPtr hwnd);
                [PreserveSig]
                void DeleteTab(IntPtr hwnd);
                [PreserveSig]
                void ActivateTab(IntPtr hwnd);
                [PreserveSig]
                void SetActiveAlt(IntPtr hwnd);

                // ITaskbarList2
                [PreserveSig]
                void MarkFullscreenWindow(IntPtr hwnd, [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);

                // ITaskbarList3
                [PreserveSig]
                void SetProgressValue(IntPtr hwnd, UInt64 ullCompleted, UInt64 ullTotal);
                [PreserveSig]
                void SetProgressState(IntPtr hwnd, TaskbarStates state);
            }

            [ComImport()]
            [Guid("56fdf344-fd6d-11d0-958a-006097c9a090")]
            [ClassInterface(ClassInterfaceType.None)]
            private class TaskbarInstance
            {
            }

            private static ITaskbarList3 taskbarInstance = (ITaskbarList3)new TaskbarInstance();
            private static readonly bool taskbarSupported = Environment.OSVersion.Version >= new Version(6, 1);

            public static void SetState(IntPtr windowHandle, TaskbarStates taskbarState)
            {
                if (taskbarSupported) taskbarInstance.SetProgressState(windowHandle, taskbarState);
            }

            public static void SetValue(IntPtr windowHandle, double progressValue, double progressMax)
            {
                if (taskbarSupported) taskbarInstance.SetProgressValue(windowHandle, (ulong)progressValue, (ulong)progressMax);
            }
        }
    }
}
