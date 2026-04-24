namespace NeoKCLCreate
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            MainFormMenuStrip = new MenuStrip();
            FileToolStripMenuItem = new ToolStripMenuItem();
            NewToolStripMenuItem = new ToolStripMenuItem();
            OpenToolStripMenuItem = new ToolStripMenuItem();
            SaveToolStripMenuItem = new ToolStripMenuItem();
            SaveAsToolStripMenuItem = new ToolStripMenuItem();
            ExportGeometryToolStripMenuItem = new ToolStripMenuItem();
            ExportOctreeToolStripMenuItem = new ToolStripMenuItem();
            EditToolStripMenuItem = new ToolStripMenuItem();
            CrushToolStripMenuItem = new ToolStripMenuItem();
            PresetsToolStripMenuItem = new ToolStripMenuItem();
            SettingsToolStripMenuItem = new ToolStripMenuItem();
            ThemeToolStripComboBox = new DarkModeForms.ToolStripColorComboBox();
            IncludeNamesToolStripComboBox = new DarkModeForms.ToolStripColorComboBox();
            CollisionCodeToolStripColorComboBox = new DarkModeForms.ToolStripColorComboBox();
            MaterialSettingsPropertyGrid = new PropertyGrid();
            PlaySoundContextMenuStrip = new ContextMenuStrip(components);
            PlaySoundToolStripMenuItem = new ToolStripMenuItem();
            MaterialListBox = new ListBox();
            MainSplitContainer = new SplitContainer();
            MainFormMenuStrip.SuspendLayout();
            PlaySoundContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MainSplitContainer).BeginInit();
            MainSplitContainer.Panel1.SuspendLayout();
            MainSplitContainer.Panel2.SuspendLayout();
            MainSplitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // MainFormMenuStrip
            // 
            MainFormMenuStrip.Items.AddRange(new ToolStripItem[] { FileToolStripMenuItem, EditToolStripMenuItem, SettingsToolStripMenuItem });
            MainFormMenuStrip.Location = new Point(0, 0);
            MainFormMenuStrip.Name = "MainFormMenuStrip";
            MainFormMenuStrip.Size = new Size(495, 24);
            MainFormMenuStrip.TabIndex = 0;
            // 
            // FileToolStripMenuItem
            // 
            FileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { NewToolStripMenuItem, OpenToolStripMenuItem, SaveToolStripMenuItem, SaveAsToolStripMenuItem, ExportGeometryToolStripMenuItem, ExportOctreeToolStripMenuItem });
            FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            FileToolStripMenuItem.Size = new Size(37, 20);
            FileToolStripMenuItem.Text = "&File";
            // 
            // NewToolStripMenuItem
            // 
            NewToolStripMenuItem.Image = Properties.Resources.New;
            NewToolStripMenuItem.Name = "NewToolStripMenuItem";
            NewToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            NewToolStripMenuItem.Size = new Size(218, 22);
            NewToolStripMenuItem.Text = "&New";
            NewToolStripMenuItem.ToolTipText = "Import a Wavefront Object";
            NewToolStripMenuItem.Click += NewToolStripMenuItem_Click;
            // 
            // OpenToolStripMenuItem
            // 
            OpenToolStripMenuItem.Image = Properties.Resources.Open;
            OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            OpenToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            OpenToolStripMenuItem.Size = new Size(218, 22);
            OpenToolStripMenuItem.Text = "&Open";
            OpenToolStripMenuItem.ToolTipText = "Open an existing KCL and PA";
            OpenToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            // 
            // SaveToolStripMenuItem
            // 
            SaveToolStripMenuItem.Enabled = false;
            SaveToolStripMenuItem.Image = Properties.Resources.Save;
            SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            SaveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            SaveToolStripMenuItem.Size = new Size(218, 22);
            SaveToolStripMenuItem.Text = "&Save";
            SaveToolStripMenuItem.ToolTipText = "Save the current KCL and PA";
            SaveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
            // 
            // SaveAsToolStripMenuItem
            // 
            SaveAsToolStripMenuItem.Enabled = false;
            SaveAsToolStripMenuItem.Image = Properties.Resources.SaveAs;
            SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem";
            SaveAsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            SaveAsToolStripMenuItem.Size = new Size(218, 22);
            SaveAsToolStripMenuItem.Text = "Save &As";
            SaveAsToolStripMenuItem.ToolTipText = "Save the current KCL and PA as a new set of files";
            SaveAsToolStripMenuItem.Click += SaveAsToolStripMenuItem_Click;
            // 
            // ExportGeometryToolStripMenuItem
            // 
            ExportGeometryToolStripMenuItem.Enabled = false;
            ExportGeometryToolStripMenuItem.Image = Properties.Resources.ExportGeometry;
            ExportGeometryToolStripMenuItem.Name = "ExportGeometryToolStripMenuItem";
            ExportGeometryToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.E;
            ExportGeometryToolStripMenuItem.Size = new Size(218, 22);
            ExportGeometryToolStripMenuItem.Text = "Export &Geometry";
            ExportGeometryToolStripMenuItem.ToolTipText = "Export the current KCL geometry as a Wavefront Object";
            ExportGeometryToolStripMenuItem.Click += ExportGeometryToolStripMenuItem_Click;
            // 
            // ExportOctreeToolStripMenuItem
            // 
            ExportOctreeToolStripMenuItem.Enabled = false;
            ExportOctreeToolStripMenuItem.Image = Properties.Resources.ExportOctree;
            ExportOctreeToolStripMenuItem.Name = "ExportOctreeToolStripMenuItem";
            ExportOctreeToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.E;
            ExportOctreeToolStripMenuItem.Size = new Size(218, 22);
            ExportOctreeToolStripMenuItem.Text = "&Export Octree";
            ExportOctreeToolStripMenuItem.ToolTipText = "Export the current KCL's Octree data as a Wavefront Object";
            ExportOctreeToolStripMenuItem.Click += ExportOctreeToolStripMenuItem_Click;
            // 
            // EditToolStripMenuItem
            // 
            EditToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { CrushToolStripMenuItem, PresetsToolStripMenuItem });
            EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            EditToolStripMenuItem.Size = new Size(39, 20);
            EditToolStripMenuItem.Text = "&Edit";
            // 
            // CrushToolStripMenuItem
            // 
            CrushToolStripMenuItem.Enabled = false;
            CrushToolStripMenuItem.Image = Properties.Resources.Crush;
            CrushToolStripMenuItem.Name = "CrushToolStripMenuItem";
            CrushToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.C;
            CrushToolStripMenuItem.Size = new Size(184, 22);
            CrushToolStripMenuItem.Text = "&Crush";
            CrushToolStripMenuItem.ToolTipText = "Merges materials with identical properties";
            CrushToolStripMenuItem.Click += CrushToolStripMenuItem_Click;
            // 
            // PresetsToolStripMenuItem
            // 
            PresetsToolStripMenuItem.Enabled = false;
            PresetsToolStripMenuItem.Image = Properties.Resources.Presets;
            PresetsToolStripMenuItem.Name = "PresetsToolStripMenuItem";
            PresetsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.P;
            PresetsToolStripMenuItem.Size = new Size(184, 22);
            PresetsToolStripMenuItem.Text = "&Presets";
            PresetsToolStripMenuItem.ToolTipText = "Opens the Collision Code Presets window";
            PresetsToolStripMenuItem.Click += PresetsToolStripMenuItem_Click;
            // 
            // SettingsToolStripMenuItem
            // 
            SettingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ThemeToolStripComboBox, IncludeNamesToolStripComboBox, CollisionCodeToolStripColorComboBox });
            SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem";
            SettingsToolStripMenuItem.Size = new Size(61, 20);
            SettingsToolStripMenuItem.Text = "&Settings";
            // 
            // ThemeToolStripComboBox
            // 
            ThemeToolStripComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ThemeToolStripComboBox.FlatStyle = FlatStyle.Flat;
            ThemeToolStripComboBox.Items.AddRange(new object[] { "Light Theme", "Dark Theme" });
            ThemeToolStripComboBox.Name = "ThemeToolStripComboBox";
            ThemeToolStripComboBox.Size = new Size(160, 23);
            ThemeToolStripComboBox.ToolTipText = "Changes the program's visuals";
            ThemeToolStripComboBox.SelectedIndexChanged += ThemeToolStripComboBox_SelectedIndexChanged;
            // 
            // IncludeNamesToolStripComboBox
            // 
            IncludeNamesToolStripComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            IncludeNamesToolStripComboBox.FlatStyle = FlatStyle.Flat;
            IncludeNamesToolStripComboBox.Items.AddRange(new object[] { "Exclude Material Names", "Include Material Names" });
            IncludeNamesToolStripComboBox.Name = "IncludeNamesToolStripComboBox";
            IncludeNamesToolStripComboBox.Size = new Size(160, 23);
            IncludeNamesToolStripComboBox.ToolTipText = "Determines if the Material Names will be included\r\nin the PA file on save. This will not be read by the\r\ngame, it is purely for your own organization.";
            IncludeNamesToolStripComboBox.SelectedIndexChanged += IncludeNamesToolStripComboBox_SelectedIndexChanged;
            // 
            // CollisionCodeToolStripColorComboBox
            // 
            CollisionCodeToolStripColorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            CollisionCodeToolStripColorComboBox.FlatStyle = FlatStyle.Flat;
            CollisionCodeToolStripColorComboBox.Name = "CollisionCodeToolStripColorComboBox";
            CollisionCodeToolStripColorComboBox.Size = new Size(160, 23);
            CollisionCodeToolStripColorComboBox.ToolTipText = "Allows you to change what options are presented to\r\nyou when selecting collision codes.\r\n\r\nNo Game will allow you to just put in numbers directly.";
            CollisionCodeToolStripColorComboBox.SelectedIndexChanged += CollisionCodeToolStripColorComboBox_SelectedIndexChanged;
            // 
            // MaterialSettingsPropertyGrid
            // 
            MaterialSettingsPropertyGrid.ContextMenuStrip = PlaySoundContextMenuStrip;
            MaterialSettingsPropertyGrid.Dock = DockStyle.Fill;
            MaterialSettingsPropertyGrid.Location = new Point(0, 0);
            MaterialSettingsPropertyGrid.Name = "MaterialSettingsPropertyGrid";
            MaterialSettingsPropertyGrid.PropertySort = PropertySort.NoSort;
            MaterialSettingsPropertyGrid.Size = new Size(326, 331);
            MaterialSettingsPropertyGrid.TabIndex = 3;
            MaterialSettingsPropertyGrid.ToolbarVisible = false;
            MaterialSettingsPropertyGrid.PropertyValueChanged += MaterialSettingsPropertyGrid_PropertyValueChanged;
            // 
            // PlaySoundContextMenuStrip
            // 
            PlaySoundContextMenuStrip.Items.AddRange(new ToolStripItem[] { PlaySoundToolStripMenuItem });
            PlaySoundContextMenuStrip.Name = "PlaySoundContextMenuStrip";
            PlaySoundContextMenuStrip.Size = new Size(134, 26);
            PlaySoundContextMenuStrip.Opening += PlaySoundContextMenuStrip_Opening;
            // 
            // PlaySoundToolStripMenuItem
            // 
            PlaySoundToolStripMenuItem.Name = "PlaySoundToolStripMenuItem";
            PlaySoundToolStripMenuItem.Size = new Size(133, 22);
            PlaySoundToolStripMenuItem.Text = "Play Sound";
            PlaySoundToolStripMenuItem.Click += PlaySoundToolStripMenuItem_Click;
            // 
            // MaterialListBox
            // 
            MaterialListBox.BorderStyle = BorderStyle.None;
            MaterialListBox.Dock = DockStyle.Fill;
            MaterialListBox.FormattingEnabled = true;
            MaterialListBox.IntegralHeight = false;
            MaterialListBox.ItemHeight = 15;
            MaterialListBox.Location = new Point(0, 0);
            MaterialListBox.Name = "MaterialListBox";
            MaterialListBox.SelectionMode = SelectionMode.MultiExtended;
            MaterialListBox.Size = new Size(165, 331);
            MaterialListBox.TabIndex = 2;
            MaterialListBox.SelectedIndexChanged += MaterialListBox_SelectedIndexChanged;
            // 
            // MainSplitContainer
            // 
            MainSplitContainer.Dock = DockStyle.Fill;
            MainSplitContainer.Location = new Point(0, 24);
            MainSplitContainer.Name = "MainSplitContainer";
            // 
            // MainSplitContainer.Panel1
            // 
            MainSplitContainer.Panel1.Controls.Add(MaterialListBox);
            // 
            // MainSplitContainer.Panel2
            // 
            MainSplitContainer.Panel2.Controls.Add(MaterialSettingsPropertyGrid);
            MainSplitContainer.Size = new Size(495, 331);
            MainSplitContainer.SplitterDistance = 165;
            MainSplitContainer.TabIndex = 4;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(495, 355);
            Controls.Add(MainSplitContainer);
            Controls.Add(MainFormMenuStrip);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = MainFormMenuStrip;
            Name = "MainForm";
            Text = "Neo KCL Create";
            FormClosing += MainForm_FormClosing;
            Shown += MainForm_Shown;
            MainFormMenuStrip.ResumeLayout(false);
            MainFormMenuStrip.PerformLayout();
            PlaySoundContextMenuStrip.ResumeLayout(false);
            MainSplitContainer.Panel1.ResumeLayout(false);
            MainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)MainSplitContainer).EndInit();
            MainSplitContainer.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip MainFormMenuStrip;
        private ToolStripMenuItem FileToolStripMenuItem;
        private ToolStripMenuItem EditToolStripMenuItem;
        private ToolStripMenuItem NewToolStripMenuItem;
        private ToolStripMenuItem OpenToolStripMenuItem;
        private ToolStripMenuItem SaveToolStripMenuItem;
        private ToolStripMenuItem SaveAsToolStripMenuItem;
        private ToolStripMenuItem ExportGeometryToolStripMenuItem;
        private ToolStripMenuItem ExportOctreeToolStripMenuItem;
        private ToolStripMenuItem CrushToolStripMenuItem;
        private ToolStripMenuItem PresetsToolStripMenuItem;
        private ToolStripMenuItem SettingsToolStripMenuItem;
        private DarkModeForms.ToolStripColorComboBox ThemeToolStripComboBox;
        private PropertyGrid MaterialSettingsPropertyGrid;
        private ListBox MaterialListBox;
        private SplitContainer MainSplitContainer;
        private DarkModeForms.ToolStripColorComboBox IncludeNamesToolStripComboBox;
        private DarkModeForms.ToolStripColorComboBox CollisionCodeToolStripColorComboBox;
        private ContextMenuStrip PlaySoundContextMenuStrip;
        private ToolStripMenuItem PlaySoundToolStripMenuItem;
    }
}
