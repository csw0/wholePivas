using FastReport.Preview;
namespace PivasIVRPrint
{
    partial class printlabel
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
            previewControlFR = new PreviewControl();
            SuspendLayout();
            // 
            // previewControlFR
            // 
            previewControlFR.BackColor = System.Drawing.SystemColors.AppWorkspace;
            previewControlFR.Dock = System.Windows.Forms.DockStyle.Fill;
            previewControlFR.FastScrolling = true;
            previewControlFR.Font = new System.Drawing.Font("宋体", 9F);
            previewControlFR.Location = new System.Drawing.Point(0, 0);
            previewControlFR.Name = "previewControlFR";
            previewControlFR.PageOffset = new System.Drawing.Point(10, 10);
            previewControlFR.Size = new System.Drawing.Size(550, 435);
            previewControlFR.TabIndex = 0;
            previewControlFR.ToolbarVisible = false;
            previewControlFR.UIStyle = FastReport.Utils.UIStyle.Office2007Black;
            // 
            // printlabel
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(this.previewControlFR);
            Margin = new System.Windows.Forms.Padding(0);
            Name = "printlabel";
            Size = new System.Drawing.Size(550, 435);
            Load += new System.EventHandler(this.Form1_Load);
            ResumeLayout(false);
        }

        #endregion

        private PreviewControl previewControlFR;

    }
}
