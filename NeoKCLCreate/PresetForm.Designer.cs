namespace NeoKCLCreate
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
            this.PresetListBox = new System.Windows.Forms.ListBox();
            this.InfoLabel = new System.Windows.Forms.Label();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PresetListBox
            // 
            this.PresetListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PresetListBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PresetListBox.FormattingEnabled = true;
            this.PresetListBox.IntegralHeight = false;
            this.PresetListBox.ItemHeight = 15;
            this.PresetListBox.Location = new System.Drawing.Point(12, 12);
            this.PresetListBox.Name = "PresetListBox";
            this.PresetListBox.Size = new System.Drawing.Size(173, 247);
            this.PresetListBox.TabIndex = 0;
            this.PresetListBox.SelectedIndexChanged += new System.EventHandler(this.PresetListBox_SelectedIndexChanged);
            // 
            // InfoLabel
            // 
            this.InfoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InfoLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InfoLabel.Location = new System.Drawing.Point(191, 12);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(251, 221);
            this.InfoLabel.TabIndex = 1;
            this.InfoLabel.Text = "[Preset Name]\r\n\r\nCamera: [Camera ID]\r\nSound: [Sound]\r\nFloor: [Floor]\r\nWall: [Wall" +
    "]\r\nCamera Collision: [Camera Through]\r\n\r\nThis is dummy text so that I can sample" +
    " out how this form will look";
            // 
            // ApplyButton
            // 
            this.ApplyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ApplyButton.Location = new System.Drawing.Point(339, 236);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(103, 23);
            this.ApplyButton.TabIndex = 2;
            this.ApplyButton.Text = "Apply";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // PresetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 271);
            this.Controls.Add(this.ApplyButton);
            this.Controls.Add(this.InfoLabel);
            this.Controls.Add(this.PresetListBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(470, 310);
            this.Name = "PresetForm";
            this.Text = "PresetForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PresetForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox PresetListBox;
        private System.Windows.Forms.Label InfoLabel;
        private System.Windows.Forms.Button ApplyButton;
    }
}