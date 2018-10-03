namespace ConsumablesStatic
{
    partial class Print
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Print));
            this.previewQD = new FastReport.Preview.PreviewControl();
            this.report = new FastReport.Report();
            ((System.ComponentModel.ISupportInitialize)(this.report)).BeginInit();
            this.SuspendLayout();
            // 
            // previewQD
            // 
            this.previewQD.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.previewQD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewQD.Font = new System.Drawing.Font("宋体", 9F);
            this.previewQD.Location = new System.Drawing.Point(0, 0);
            this.previewQD.Name = "previewQD";
            this.previewQD.PageOffset = new System.Drawing.Point(10, 10);
            this.previewQD.Size = new System.Drawing.Size(632, 488);
            this.previewQD.TabIndex = 1042;
            this.previewQD.UIStyle = FastReport.Utils.UIStyle.Office2007Black;
            this.previewQD.Visible = false;
            // 
            // report
            // 
            this.report.ReportResourceString = resources.GetString("report.ReportResourceString");
            // 
            // Print
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.previewQD);
            this.Name = "Print";
            this.Size = new System.Drawing.Size(632, 488);
            this.Load += new System.EventHandler(this.Print_Load);
            ((System.ComponentModel.ISupportInitialize)(this.report)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FastReport.Preview.PreviewControl previewQD;
        private FastReport.Report report;
    }
}
