namespace NeoKCLCreate.UI
{
    partial class ExportProgressForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportProgressForm));
            StatusLabel = new Label();
            MainProgressBar = new ProgressBar();
            ExportBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            SuspendLayout();
            // 
            // StatusLabel
            // 
            StatusLabel.Dock = DockStyle.Top;
            StatusLabel.Location = new Point(0, 0);
            StatusLabel.Name = "StatusLabel";
            StatusLabel.Size = new Size(339, 15);
            StatusLabel.TabIndex = 0;
            StatusLabel.Text = "Performing Epic Task";
            StatusLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // MainProgressBar
            // 
            MainProgressBar.Dock = DockStyle.Fill;
            MainProgressBar.Location = new Point(0, 15);
            MainProgressBar.Name = "MainProgressBar";
            MainProgressBar.Size = new Size(339, 26);
            MainProgressBar.TabIndex = 1;
            // 
            // ImportBackgroundWorker
            // 
            ExportBackgroundWorker.WorkerReportsProgress = true;
            ExportBackgroundWorker.WorkerSupportsCancellation = true;
            ExportBackgroundWorker.DoWork += ExportBackgroundWorker_DoWork;
            ExportBackgroundWorker.ProgressChanged += ExportBackgroundWorker_ProgressChanged;
            ExportBackgroundWorker.RunWorkerCompleted += ExportBackgroundWorker_RunWorkerCompleted;
            // 
            // ExportProgressForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(339, 41);
            Controls.Add(MainProgressBar);
            Controls.Add(StatusLabel);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(355, 80);
            MinimizeBox = false;
            MinimumSize = new Size(355, 80);
            Name = "ExportProgressForm";
            Text = "Neo KCL Create - Exporting...";
            FormClosing += ExportProgressForm_FormClosing;
            Shown += ExportProgressForm_Shown;
            ResumeLayout(false);
        }

        #endregion

        private Label StatusLabel;
        private ProgressBar MainProgressBar;
        private System.ComponentModel.BackgroundWorker ExportBackgroundWorker;
    }
}