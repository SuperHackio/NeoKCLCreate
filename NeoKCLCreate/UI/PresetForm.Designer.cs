namespace NeoKCLCreate.UI
{
    partial class PresetForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PresetForm));
            MainTableLayoutPanel = new TableLayoutPanel();
            PreviewPictureBox = new PictureBox();
            InfoLabel = new Label();
            ListPanel = new Panel();
            PresetListBox = new ListBox();
            ApplyButton = new Button();
            DisabledLabel = new Label();
            MainTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PreviewPictureBox).BeginInit();
            ListPanel.SuspendLayout();
            SuspendLayout();
            // 
            // MainTableLayoutPanel
            // 
            MainTableLayoutPanel.ColumnCount = 2;
            MainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.72781F));
            MainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.27219F));
            MainTableLayoutPanel.Controls.Add(PreviewPictureBox, 1, 0);
            MainTableLayoutPanel.Controls.Add(InfoLabel, 1, 1);
            MainTableLayoutPanel.Controls.Add(ListPanel, 0, 0);
            MainTableLayoutPanel.Dock = DockStyle.Fill;
            MainTableLayoutPanel.Location = new Point(0, 0);
            MainTableLayoutPanel.Name = "MainTableLayoutPanel";
            MainTableLayoutPanel.RowCount = 2;
            MainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            MainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            MainTableLayoutPanel.Size = new Size(507, 412);
            MainTableLayoutPanel.TabIndex = 0;
            // 
            // PreviewPictureBox
            // 
            PreviewPictureBox.Dock = DockStyle.Fill;
            PreviewPictureBox.Location = new Point(174, 3);
            PreviewPictureBox.Name = "PreviewPictureBox";
            PreviewPictureBox.Size = new Size(330, 326);
            PreviewPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            PreviewPictureBox.TabIndex = 1;
            PreviewPictureBox.TabStop = false;
            // 
            // InfoLabel
            // 
            InfoLabel.Dock = DockStyle.Fill;
            InfoLabel.Font = new Font("Consolas", 10F);
            InfoLabel.Location = new Point(174, 332);
            InfoLabel.Name = "InfoLabel";
            InfoLabel.Size = new Size(330, 80);
            InfoLabel.TabIndex = 2;
            InfoLabel.Text = "Preset description goes here.";
            // 
            // ListPanel
            // 
            ListPanel.Controls.Add(PresetListBox);
            ListPanel.Controls.Add(ApplyButton);
            ListPanel.Dock = DockStyle.Fill;
            ListPanel.Location = new Point(3, 3);
            ListPanel.Name = "ListPanel";
            MainTableLayoutPanel.SetRowSpan(ListPanel, 2);
            ListPanel.Size = new Size(165, 406);
            ListPanel.TabIndex = 3;
            // 
            // PresetListBox
            // 
            PresetListBox.BorderStyle = BorderStyle.None;
            PresetListBox.Dock = DockStyle.Fill;
            PresetListBox.Font = new Font("Consolas", 10F);
            PresetListBox.FormattingEnabled = true;
            PresetListBox.IntegralHeight = false;
            PresetListBox.ItemHeight = 15;
            PresetListBox.Location = new Point(0, 0);
            PresetListBox.Name = "PresetListBox";
            PresetListBox.Size = new Size(165, 383);
            PresetListBox.TabIndex = 1;
            PresetListBox.SelectedIndexChanged += PresetListBox_SelectedIndexChanged;
            // 
            // ApplyButton
            // 
            ApplyButton.Dock = DockStyle.Bottom;
            ApplyButton.FlatStyle = FlatStyle.Flat;
            ApplyButton.Location = new Point(0, 383);
            ApplyButton.Name = "ApplyButton";
            ApplyButton.Size = new Size(165, 23);
            ApplyButton.TabIndex = 2;
            ApplyButton.Text = "Apply to Selected";
            ApplyButton.UseVisualStyleBackColor = true;
            ApplyButton.Click += ApplyButton_Click;
            // 
            // DisabledLabel
            // 
            DisabledLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DisabledLabel.BorderStyle = BorderStyle.FixedSingle;
            DisabledLabel.Font = new Font("Consolas", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DisabledLabel.Location = new Point(12, 9);
            DisabledLabel.Name = "DisabledLabel";
            DisabledLabel.Size = new Size(483, 394);
            DisabledLabel.TabIndex = 1;
            DisabledLabel.Text = "No Presets Available";
            DisabledLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PresetForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(507, 412);
            Controls.Add(MainTableLayoutPanel);
            Controls.Add(DisabledLabel);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(523, 451);
            Name = "PresetForm";
            Text = "Neo KCL Create - Presets";
            FormClosing += PresetForm_FormClosing;
            MainTableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PreviewPictureBox).EndInit();
            ListPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel MainTableLayoutPanel;
        private PictureBox PreviewPictureBox;
        private Label InfoLabel;
        private Label DisabledLabel;
        private Panel ListPanel;
        private ListBox PresetListBox;
        private Button ApplyButton;
    }
}