namespace NeoKCLCreate
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainFormMenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.PresetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PaGroupBox = new System.Windows.Forms.GroupBox();
            this.CameraThroughCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.WallCodeComboBox = new System.Windows.Forms.ComboBox();
            this.FloorCodeComboBox = new System.Windows.Forms.ComboBox();
            this.SoundCodeComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CameraIDNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.GroupListBox = new System.Windows.Forms.ListBox();
            this.OctreeGroupBox = new System.Windows.Forms.GroupBox();
            this.ResetOctreeMin = new System.Windows.Forms.Button();
            this.ResetOctreeMax = new System.Windows.Forms.Button();
            this.MinCubeSizeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.MaxTrianglesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SaveBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.ExportBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.LoadBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.MainFormMenuStrip.SuspendLayout();
            this.PaGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraIDNumericUpDown)).BeginInit();
            this.OctreeGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinCubeSizeNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxTrianglesNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // MainFormMenuStrip
            // 
            this.MainFormMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.EditToolStripMenuItem});
            this.MainFormMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainFormMenuStrip.Name = "MainFormMenuStrip";
            this.MainFormMenuStrip.Size = new System.Drawing.Size(484, 24);
            this.MainFormMenuStrip.TabIndex = 0;
            this.MainFormMenuStrip.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewToolStripMenuItem,
            this.OpenToolStripMenuItem,
            this.toolStripSeparator,
            this.SaveToolStripMenuItem,
            this.SaveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.ExportToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileToolStripMenuItem.Text = "&File";
            // 
            // NewToolStripMenuItem
            // 
            this.NewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("NewToolStripMenuItem.Image")));
            this.NewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewToolStripMenuItem.Name = "NewToolStripMenuItem";
            this.NewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.NewToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.NewToolStripMenuItem.Text = "&New";
            this.NewToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("OpenToolStripMenuItem.Image")));
            this.OpenToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.OpenToolStripMenuItem.Text = "&Open";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(183, 6);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("SaveToolStripMenuItem.Image")));
            this.SaveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.SaveToolStripMenuItem.Text = "&Save";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // SaveAsToolStripMenuItem
            // 
            this.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem";
            this.SaveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.SaveAsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.SaveAsToolStripMenuItem.Text = "Save &As";
            this.SaveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(183, 6);
            // 
            // ExportToolStripMenuItem
            // 
            this.ExportToolStripMenuItem.Name = "ExportToolStripMenuItem";
            this.ExportToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.ExportToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.ExportToolStripMenuItem.Text = "Export";
            this.ExportToolStripMenuItem.Click += new System.EventHandler(this.ExportToolStripMenuItem_Click);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyToolStripMenuItem,
            this.PasteToolStripMenuItem,
            this.toolStripSeparator4,
            this.PresetsToolStripMenuItem});
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.EditToolStripMenuItem.Text = "&Edit";
            // 
            // CopyToolStripMenuItem
            // 
            this.CopyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("CopyToolStripMenuItem.Image")));
            this.CopyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            this.CopyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.CopyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.CopyToolStripMenuItem.Text = "&Copy";
            this.CopyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // PasteToolStripMenuItem
            // 
            this.PasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("PasteToolStripMenuItem.Image")));
            this.PasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem";
            this.PasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.PasteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.PasteToolStripMenuItem.Text = "&Paste";
            this.PasteToolStripMenuItem.Click += new System.EventHandler(this.PasteToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(149, 6);
            // 
            // PresetsToolStripMenuItem
            // 
            this.PresetsToolStripMenuItem.Name = "PresetsToolStripMenuItem";
            this.PresetsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.PresetsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.PresetsToolStripMenuItem.Text = "Presets";
            this.PresetsToolStripMenuItem.Click += new System.EventHandler(this.PresetsToolStripMenuItem_Click);
            // 
            // PaGroupBox
            // 
            this.PaGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PaGroupBox.Controls.Add(this.CameraThroughCheckBox);
            this.PaGroupBox.Controls.Add(this.label4);
            this.PaGroupBox.Controls.Add(this.WallCodeComboBox);
            this.PaGroupBox.Controls.Add(this.FloorCodeComboBox);
            this.PaGroupBox.Controls.Add(this.SoundCodeComboBox);
            this.PaGroupBox.Controls.Add(this.label2);
            this.PaGroupBox.Controls.Add(this.label3);
            this.PaGroupBox.Controls.Add(this.CameraIDNumericUpDown);
            this.PaGroupBox.Controls.Add(this.label1);
            this.PaGroupBox.Controls.Add(this.GroupListBox);
            this.PaGroupBox.Location = new System.Drawing.Point(12, 112);
            this.PaGroupBox.Name = "PaGroupBox";
            this.PaGroupBox.Size = new System.Drawing.Size(460, 213);
            this.PaGroupBox.TabIndex = 1;
            this.PaGroupBox.TabStop = false;
            this.PaGroupBox.Text = "Collision Codes";
            // 
            // CameraThroughCheckBox
            // 
            this.CameraThroughCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CameraThroughCheckBox.AutoSize = true;
            this.CameraThroughCheckBox.Location = new System.Drawing.Point(232, 126);
            this.CameraThroughCheckBox.Name = "CameraThroughCheckBox";
            this.CameraThroughCheckBox.Size = new System.Drawing.Size(141, 17);
            this.CameraThroughCheckBox.TabIndex = 7;
            this.CameraThroughCheckBox.Text = "Disable Camera Collision";
            this.CameraThroughCheckBox.UseVisualStyleBackColor = true;
            this.CameraThroughCheckBox.CheckedChanged += new System.EventHandler(this.CameraThroughCheckBox_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(157, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Wall Code:";
            // 
            // WallCodeComboBox
            // 
            this.WallCodeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.WallCodeComboBox.DropDownHeight = 104;
            this.WallCodeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WallCodeComboBox.FormattingEnabled = true;
            this.WallCodeComboBox.IntegralHeight = false;
            this.WallCodeComboBox.Location = new System.Drawing.Point(232, 99);
            this.WallCodeComboBox.Name = "WallCodeComboBox";
            this.WallCodeComboBox.Size = new System.Drawing.Size(222, 21);
            this.WallCodeComboBox.TabIndex = 5;
            this.WallCodeComboBox.SelectedIndexChanged += new System.EventHandler(this.WallCodeComboBox_SelectedIndexChanged);
            // 
            // FloorCodeComboBox
            // 
            this.FloorCodeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FloorCodeComboBox.DropDownHeight = 104;
            this.FloorCodeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FloorCodeComboBox.FormattingEnabled = true;
            this.FloorCodeComboBox.IntegralHeight = false;
            this.FloorCodeComboBox.Location = new System.Drawing.Point(232, 72);
            this.FloorCodeComboBox.Name = "FloorCodeComboBox";
            this.FloorCodeComboBox.Size = new System.Drawing.Size(222, 21);
            this.FloorCodeComboBox.TabIndex = 4;
            this.FloorCodeComboBox.SelectedIndexChanged += new System.EventHandler(this.FloorCodeComboBox_SelectedIndexChanged);
            // 
            // SoundCodeComboBox
            // 
            this.SoundCodeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SoundCodeComboBox.DropDownHeight = 104;
            this.SoundCodeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SoundCodeComboBox.DropDownWidth = 160;
            this.SoundCodeComboBox.FormattingEnabled = true;
            this.SoundCodeComboBox.IntegralHeight = false;
            this.SoundCodeComboBox.Location = new System.Drawing.Point(232, 45);
            this.SoundCodeComboBox.Name = "SoundCodeComboBox";
            this.SoundCodeComboBox.Size = new System.Drawing.Size(222, 21);
            this.SoundCodeComboBox.TabIndex = 3;
            this.SoundCodeComboBox.SelectedIndexChanged += new System.EventHandler(this.SoundCodeComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(157, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Sound Code:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(157, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Floor Code:";
            // 
            // CameraIDNumericUpDown
            // 
            this.CameraIDNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CameraIDNumericUpDown.Location = new System.Drawing.Point(232, 19);
            this.CameraIDNumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.CameraIDNumericUpDown.Name = "CameraIDNumericUpDown";
            this.CameraIDNumericUpDown.Size = new System.Drawing.Size(222, 20);
            this.CameraIDNumericUpDown.TabIndex = 2;
            this.CameraIDNumericUpDown.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.CameraIDNumericUpDown.ValueChanged += new System.EventHandler(this.CameraIDNumericUpDown_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(157, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Camera ID:";
            // 
            // GroupListBox
            // 
            this.GroupListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupListBox.FormattingEnabled = true;
            this.GroupListBox.IntegralHeight = false;
            this.GroupListBox.Location = new System.Drawing.Point(6, 19);
            this.GroupListBox.Name = "GroupListBox";
            this.GroupListBox.Size = new System.Drawing.Size(145, 188);
            this.GroupListBox.TabIndex = 0;
            this.GroupListBox.SelectedIndexChanged += new System.EventHandler(this.GroupListBox_SelectedIndexChanged);
            // 
            // OctreeGroupBox
            // 
            this.OctreeGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OctreeGroupBox.Controls.Add(this.ResetOctreeMin);
            this.OctreeGroupBox.Controls.Add(this.ResetOctreeMax);
            this.OctreeGroupBox.Controls.Add(this.MinCubeSizeNumericUpDown);
            this.OctreeGroupBox.Controls.Add(this.MaxTrianglesNumericUpDown);
            this.OctreeGroupBox.Controls.Add(this.label6);
            this.OctreeGroupBox.Controls.Add(this.label5);
            this.OctreeGroupBox.Location = new System.Drawing.Point(12, 27);
            this.OctreeGroupBox.Name = "OctreeGroupBox";
            this.OctreeGroupBox.Size = new System.Drawing.Size(460, 79);
            this.OctreeGroupBox.TabIndex = 2;
            this.OctreeGroupBox.TabStop = false;
            this.OctreeGroupBox.Text = "Octree Settings";
            // 
            // ResetOctreeMin
            // 
            this.ResetOctreeMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ResetOctreeMin.Location = new System.Drawing.Point(401, 45);
            this.ResetOctreeMin.Name = "ResetOctreeMin";
            this.ResetOctreeMin.Size = new System.Drawing.Size(53, 20);
            this.ResetOctreeMin.TabIndex = 5;
            this.ResetOctreeMin.Text = "Reset";
            this.ResetOctreeMin.UseVisualStyleBackColor = true;
            this.ResetOctreeMin.Click += new System.EventHandler(this.ResetOctreeMin_Click);
            // 
            // ResetOctreeMax
            // 
            this.ResetOctreeMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ResetOctreeMax.Location = new System.Drawing.Point(401, 19);
            this.ResetOctreeMax.Name = "ResetOctreeMax";
            this.ResetOctreeMax.Size = new System.Drawing.Size(53, 20);
            this.ResetOctreeMax.TabIndex = 4;
            this.ResetOctreeMax.Text = "Reset";
            this.ResetOctreeMax.UseVisualStyleBackColor = true;
            this.ResetOctreeMax.Click += new System.EventHandler(this.ResetOctreeMax_Click);
            // 
            // MinCubeSizeNumericUpDown
            // 
            this.MinCubeSizeNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MinCubeSizeNumericUpDown.Location = new System.Drawing.Point(131, 45);
            this.MinCubeSizeNumericUpDown.Name = "MinCubeSizeNumericUpDown";
            this.MinCubeSizeNumericUpDown.Size = new System.Drawing.Size(264, 20);
            this.MinCubeSizeNumericUpDown.TabIndex = 3;
            this.MinCubeSizeNumericUpDown.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // MaxTrianglesNumericUpDown
            // 
            this.MaxTrianglesNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MaxTrianglesNumericUpDown.Location = new System.Drawing.Point(131, 19);
            this.MaxTrianglesNumericUpDown.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.MaxTrianglesNumericUpDown.Name = "MaxTrianglesNumericUpDown";
            this.MaxTrianglesNumericUpDown.Size = new System.Drawing.Size(264, 20);
            this.MaxTrianglesNumericUpDown.TabIndex = 2;
            this.MaxTrianglesNumericUpDown.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Min Cube Size";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Max Triangles per Cube";
            // 
            // SaveBackgroundWorker
            // 
            this.SaveBackgroundWorker.WorkerReportsProgress = true;
            this.SaveBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SaveBackgroundWorker_DoWork);
            this.SaveBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.SaveBackgroundWorker_ProgressChanged);
            this.SaveBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.SaveBackgroundWorker_RunWorkerCompleted);
            // 
            // ExportBackgroundWorker
            // 
            this.ExportBackgroundWorker.WorkerReportsProgress = true;
            this.ExportBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ExportBackgroundWorker_DoWork);
            this.ExportBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ExportBackgroundWorker_ProgressChanged);
            this.ExportBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ExportBackgroundWorker_RunWorkerCompleted);
            // 
            // LoadBackgroundWorker
            // 
            this.LoadBackgroundWorker.WorkerReportsProgress = true;
            this.LoadBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.LoadBackgroundWorker_DoWork);
            this.LoadBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.LoadBackgroundWorker_ProgressChanged);
            this.LoadBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.LoadBackgroundWorker_RunWorkerCompleted);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 337);
            this.Controls.Add(this.OctreeGroupBox);
            this.Controls.Add(this.PaGroupBox);
            this.Controls.Add(this.MainFormMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainFormMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(470, 376);
            this.Name = "MainForm";
            this.Text = "Neo KCL Creator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.MainFormMenuStrip.ResumeLayout(false);
            this.MainFormMenuStrip.PerformLayout();
            this.PaGroupBox.ResumeLayout(false);
            this.PaGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraIDNumericUpDown)).EndInit();
            this.OctreeGroupBox.ResumeLayout(false);
            this.OctreeGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinCubeSizeNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxTrianglesNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainFormMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ExportToolStripMenuItem;
        private System.Windows.Forms.GroupBox PaGroupBox;
        private System.Windows.Forms.NumericUpDown CameraIDNumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox GroupListBox;
        private System.Windows.Forms.GroupBox OctreeGroupBox;
        private System.Windows.Forms.CheckBox CameraThroughCheckBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox WallCodeComboBox;
        private System.Windows.Forms.ComboBox FloorCodeComboBox;
        private System.Windows.Forms.ComboBox SoundCodeComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown MinCubeSizeNumericUpDown;
        private System.Windows.Forms.NumericUpDown MaxTrianglesNumericUpDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.ComponentModel.BackgroundWorker SaveBackgroundWorker;
        private System.Windows.Forms.Button ResetOctreeMin;
        private System.Windows.Forms.Button ResetOctreeMax;
        private System.Windows.Forms.ToolStripMenuItem PresetsToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker ExportBackgroundWorker;
        private System.ComponentModel.BackgroundWorker LoadBackgroundWorker;
    }
}

