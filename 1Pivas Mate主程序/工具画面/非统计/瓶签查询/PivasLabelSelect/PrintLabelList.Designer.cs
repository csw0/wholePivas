namespace PivasLabelSelect
{
    partial class PrintLabelList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintLabelList));
            this.previewQD = new FastReport.Preview.PreviewControl();
            this.report = new FastReport.Report();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mPrint = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.report)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // previewQD
            // 
            this.previewQD.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.previewQD.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.previewQD.Buttons = ((FastReport.PreviewButtons)(((((((((((FastReport.PreviewButtons.Print | FastReport.PreviewButtons.Open) 
            | FastReport.PreviewButtons.Save) 
            | FastReport.PreviewButtons.Find) 
            | FastReport.PreviewButtons.Zoom) 
            | FastReport.PreviewButtons.Outline) 
            | FastReport.PreviewButtons.PageSetup) 
            | FastReport.PreviewButtons.Edit) 
            | FastReport.PreviewButtons.Watermark) 
            | FastReport.PreviewButtons.Navigator) 
            | FastReport.PreviewButtons.Close)));
            this.previewQD.Font = new System.Drawing.Font("宋体", 9F);
            this.previewQD.Location = new System.Drawing.Point(1, 27);
            this.previewQD.Name = "previewQD";
            this.previewQD.PageOffset = new System.Drawing.Point(10, 10);
            this.previewQD.Size = new System.Drawing.Size(724, 413);
            this.previewQD.TabIndex = 0;
            this.previewQD.ToolbarVisible = false;
            this.previewQD.UIStyle = FastReport.Utils.UIStyle.Office2007Black;
            // 
            // report
            // 
            this.report.ReportResourceString = resources.GetString("report.ReportResourceString");
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mPrint});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(725, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mPrint
            // 
            this.mPrint.Name = "mPrint";
            this.mPrint.Size = new System.Drawing.Size(44, 21);
            this.mPrint.Text = "打印";
            this.mPrint.Click += new System.EventHandler(this.mPrint_Click);
            // 
            // PrintPreview1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 439);
            this.Controls.Add(this.previewQD);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PrintPreview1";
            this.Text = "打印清单";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PrintPreview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.report)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

     
        private FastReport.Report report;
        public FastReport.Preview.PreviewControl previewQD;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mPrint;
    }
}