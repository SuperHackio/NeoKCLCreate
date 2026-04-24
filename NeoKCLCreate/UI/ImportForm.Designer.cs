using DarkModeForms;

namespace NeoKCLCreate.UI
{
    partial class ImportForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportForm));
            ConvertButton = new Button();
            ExitButton = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            ValueRoundNumericUpDown = new ColorNumericUpDown();
            ValueRoundLabel = new Label();
            PrismThicknessNumericUpDown = new ColorNumericUpDown();
            PrismThicknessLabel = new Label();
            MaxDepthNumericUpDown = new ColorNumericUpDown();
            MaxDepthLabel = new Label();
            MinLeafSizeNumericUpDown = new ColorNumericUpDown();
            MinLeafLabel = new Label();
            MaxTriLabel = new Label();
            MaxTriNumericUpDown = new ColorNumericUpDown();
            MainToolTip = new ToolTip(components);
            label1 = new Label();
            SubToolTip = new ToolTip(components);
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ValueRoundNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PrismThicknessNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MaxDepthNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MinLeafSizeNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MaxTriNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // ConvertButton
            // 
            ConvertButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ConvertButton.FlatStyle = FlatStyle.Flat;
            ConvertButton.Location = new Point(300, 146);
            ConvertButton.Name = "ConvertButton";
            ConvertButton.Size = new Size(75, 23);
            ConvertButton.TabIndex = 0;
            ConvertButton.Text = "Import";
            SubToolTip.SetToolTip(ConvertButton, "Confirm import settings and begin importing");
            ConvertButton.UseVisualStyleBackColor = true;
            ConvertButton.Click += ConvertButton_Click;
            // 
            // ExitButton
            // 
            ExitButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ExitButton.FlatStyle = FlatStyle.Flat;
            ExitButton.Location = new Point(12, 146);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(75, 23);
            ExitButton.TabIndex = 1;
            ExitButton.Text = "Cancel";
            SubToolTip.SetToolTip(ExitButton, "Cancels the importing process");
            ExitButton.UseVisualStyleBackColor = true;
            ExitButton.Click += ExitButton_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(ValueRoundNumericUpDown, 1, 1);
            tableLayoutPanel1.Controls.Add(ValueRoundLabel, 0, 1);
            tableLayoutPanel1.Controls.Add(PrismThicknessNumericUpDown, 1, 0);
            tableLayoutPanel1.Controls.Add(PrismThicknessLabel, 0, 0);
            tableLayoutPanel1.Controls.Add(MaxDepthNumericUpDown, 1, 4);
            tableLayoutPanel1.Controls.Add(MaxDepthLabel, 0, 4);
            tableLayoutPanel1.Controls.Add(MinLeafSizeNumericUpDown, 1, 3);
            tableLayoutPanel1.Controls.Add(MinLeafLabel, 0, 3);
            tableLayoutPanel1.Controls.Add(MaxTriLabel, 0, 2);
            tableLayoutPanel1.Controls.Add(MaxTriNumericUpDown, 1, 2);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.Size = new Size(363, 128);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // ValueRoundNumericUpDown
            // 
            ValueRoundNumericUpDown.BorderColor = Color.FromArgb(200, 200, 200);
            ValueRoundNumericUpDown.DecimalPlaces = 7;
            ValueRoundNumericUpDown.Dock = DockStyle.Fill;
            ValueRoundNumericUpDown.Location = new Point(233, 28);
            ValueRoundNumericUpDown.Maximum = new decimal(new int[] { 7, 0, 0, 0 });
            ValueRoundNumericUpDown.Name = "ValueRoundNumericUpDown";
            ValueRoundNumericUpDown.Size = new Size(127, 23);
            ValueRoundNumericUpDown.TabIndex = 9;
            // 
            // ValueRoundLabel
            // 
            ValueRoundLabel.Dock = DockStyle.Fill;
            ValueRoundLabel.Location = new Point(3, 25);
            ValueRoundLabel.Name = "ValueRoundLabel";
            ValueRoundLabel.Size = new Size(224, 25);
            ValueRoundLabel.TabIndex = 8;
            ValueRoundLabel.Text = "Geometry > Value Rounding";
            ValueRoundLabel.TextAlign = ContentAlignment.MiddleLeft;
            MainToolTip.SetToolTip(ValueRoundLabel, resources.GetString("ValueRoundLabel.ToolTip"));
            // 
            // PrismThicknessNumericUpDown
            // 
            PrismThicknessNumericUpDown.BorderColor = Color.FromArgb(200, 200, 200);
            PrismThicknessNumericUpDown.DecimalPlaces = 2;
            PrismThicknessNumericUpDown.Dock = DockStyle.Fill;
            PrismThicknessNumericUpDown.Location = new Point(233, 3);
            PrismThicknessNumericUpDown.Name = "PrismThicknessNumericUpDown";
            PrismThicknessNumericUpDown.Size = new Size(127, 23);
            PrismThicknessNumericUpDown.TabIndex = 7;
            PrismThicknessNumericUpDown.Value = new decimal(new int[] { 40, 0, 0, 0 });
            // 
            // PrismThicknessLabel
            // 
            PrismThicknessLabel.Dock = DockStyle.Fill;
            PrismThicknessLabel.Location = new Point(3, 0);
            PrismThicknessLabel.Name = "PrismThicknessLabel";
            PrismThicknessLabel.Size = new Size(224, 25);
            PrismThicknessLabel.TabIndex = 6;
            PrismThicknessLabel.Text = "Geometry > Prism Thickness:";
            PrismThicknessLabel.TextAlign = ContentAlignment.MiddleLeft;
            MainToolTip.SetToolTip(PrismThicknessLabel, "This determines the collision depth when interacting with prisms.\r\n\r\nApplys to all prisms in the file.\r\n\r\nDefault 40.00\r\nNintendo used 40.00");
            // 
            // MaxDepthNumericUpDown
            // 
            MaxDepthNumericUpDown.BorderColor = Color.FromArgb(200, 200, 200);
            MaxDepthNumericUpDown.Dock = DockStyle.Fill;
            MaxDepthNumericUpDown.Location = new Point(233, 103);
            MaxDepthNumericUpDown.Name = "MaxDepthNumericUpDown";
            MaxDepthNumericUpDown.Size = new Size(127, 23);
            MaxDepthNumericUpDown.TabIndex = 5;
            MaxDepthNumericUpDown.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // MaxDepthLabel
            // 
            MaxDepthLabel.Dock = DockStyle.Fill;
            MaxDepthLabel.Location = new Point(3, 100);
            MaxDepthLabel.Name = "MaxDepthLabel";
            MaxDepthLabel.Size = new Size(224, 28);
            MaxDepthLabel.TabIndex = 4;
            MaxDepthLabel.Text = "Octree > Maximum Depth:";
            MaxDepthLabel.TextAlign = ContentAlignment.MiddleLeft;
            MainToolTip.SetToolTip(MaxDepthLabel, resources.GetString("MaxDepthLabel.ToolTip"));
            // 
            // MinLeafSizeNumericUpDown
            // 
            MinLeafSizeNumericUpDown.BorderColor = Color.FromArgb(200, 200, 200);
            MinLeafSizeNumericUpDown.Dock = DockStyle.Fill;
            MinLeafSizeNumericUpDown.Location = new Point(233, 78);
            MinLeafSizeNumericUpDown.Name = "MinLeafSizeNumericUpDown";
            MinLeafSizeNumericUpDown.Size = new Size(127, 23);
            MinLeafSizeNumericUpDown.TabIndex = 3;
            MinLeafSizeNumericUpDown.Value = new decimal(new int[] { 8, 0, 0, 0 });
            // 
            // MinLeafLabel
            // 
            MinLeafLabel.Dock = DockStyle.Fill;
            MinLeafLabel.Location = new Point(3, 75);
            MinLeafLabel.Name = "MinLeafLabel";
            MinLeafLabel.Size = new Size(224, 25);
            MinLeafLabel.TabIndex = 2;
            MinLeafLabel.Text = "Octree > Minimum Leaf Size:";
            MinLeafLabel.TextAlign = ContentAlignment.MiddleLeft;
            MainToolTip.SetToolTip(MinLeafLabel, resources.GetString("MinLeafLabel.ToolTip"));
            // 
            // MaxTriLabel
            // 
            MaxTriLabel.Dock = DockStyle.Fill;
            MaxTriLabel.Location = new Point(3, 50);
            MaxTriLabel.Name = "MaxTriLabel";
            MaxTriLabel.Size = new Size(224, 25);
            MaxTriLabel.TabIndex = 0;
            MaxTriLabel.Text = "Octree > Maximum Triangles per Leaf:";
            MaxTriLabel.TextAlign = ContentAlignment.MiddleLeft;
            MainToolTip.SetToolTip(MaxTriLabel, resources.GetString("MaxTriLabel.ToolTip"));
            // 
            // MaxTriNumericUpDown
            // 
            MaxTriNumericUpDown.BorderColor = Color.FromArgb(200, 200, 200);
            MaxTriNumericUpDown.Dock = DockStyle.Fill;
            MaxTriNumericUpDown.Location = new Point(233, 53);
            MaxTriNumericUpDown.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            MaxTriNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            MaxTriNumericUpDown.Name = "MaxTriNumericUpDown";
            MaxTriNumericUpDown.Size = new Size(127, 23);
            MaxTriNumericUpDown.TabIndex = 1;
            MaxTriNumericUpDown.Value = new decimal(new int[] { 25, 0, 0, 0 });
            // 
            // MainToolTip
            // 
            MainToolTip.IsBalloon = true;
            MainToolTip.ToolTipIcon = ToolTipIcon.Info;
            MainToolTip.ToolTipTitle = "Setting Explanation";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.Font = new Font("Segoe UI", 8F);
            label1.Location = new Point(93, 146);
            label1.Name = "label1";
            label1.Size = new Size(201, 23);
            label1.TabIndex = 3;
            label1.Text = "Hover the above text for hints!";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            SubToolTip.SetToolTip(label1, "I didn't mean hover THIS text...\r\nHover the text next to each setting above!");
            // 
            // ImportForm
            // 
            AcceptButton = ConvertButton;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = ExitButton;
            ClientSize = new Size(387, 181);
            Controls.Add(label1);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(ExitButton);
            Controls.Add(ConvertButton);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(403, 220);
            MinimizeBox = false;
            MinimumSize = new Size(403, 220);
            Name = "ImportForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Neo KCL Create - Import Model";
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ValueRoundNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)PrismThicknessNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)MaxDepthNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)MinLeafSizeNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)MaxTriNumericUpDown).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button ConvertButton;
        private Button ExitButton;
        private TableLayoutPanel tableLayoutPanel1;
        private Label MaxTriLabel;
        private ColorNumericUpDown MaxDepthNumericUpDown;
        private Label MaxDepthLabel;
        private ColorNumericUpDown MinLeafSizeNumericUpDown;
        private Label MinLeafLabel;
        private ColorNumericUpDown MaxTriNumericUpDown;
        private ColorNumericUpDown PrismThicknessNumericUpDown;
        private Label PrismThicknessLabel;
        private ColorNumericUpDown ValueRoundNumericUpDown;
        private Label ValueRoundLabel;
        private ToolTip MainToolTip;
        private Label label1;
        private ToolTip SubToolTip;
    }
}