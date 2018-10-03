using FastReport.Preview;
using FastReport.Utils;
namespace PivasIVRPrint
{
    partial class BQlabelShow
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
            this.previewControl1 = new FastReport.Preview.PreviewControl();
            this.timer1 = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
            this.SuspendLayout();
            // 
            // previewControl1
            // 
            this.previewControl1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.previewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewControl1.Font = new System.Drawing.Font("宋体", 9F);
            this.previewControl1.Location = new System.Drawing.Point(0, 0);
            this.previewControl1.Name = "previewControl1";
            this.previewControl1.PageOffset = new System.Drawing.Point(10, 10);
            this.previewControl1.Size = new System.Drawing.Size(280, 433);
            this.previewControl1.StatusbarVisible = false;
            this.previewControl1.TabIndex = 0;
            this.previewControl1.ToolbarVisible = false;
            this.previewControl1.UIStyle = FastReport.Utils.UIStyle.Office2007Black;
            // 
            // timer1
            // 
            this.timer1.AutoReset = false;
            this.timer1.Interval = 250D;
            this.timer1.SynchronizingObject = this;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
            // 
            // BQlabelShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.previewControl1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "BQlabelShow";
            this.Size = new System.Drawing.Size(280, 433);
            this.Load += new System.EventHandler(this.BQlabelShow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PreviewControl previewControl1;
        private System.Timers.Timer timer1;

    }
}
