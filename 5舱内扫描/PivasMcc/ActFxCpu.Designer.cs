namespace PivasMcc
{
    partial class ActFxCpu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActFxCpu));
            this.axActFXCPU1 = new AxACTPCCOMLib.AxActFXCPU();
            ((System.ComponentModel.ISupportInitialize)(this.axActFXCPU1)).BeginInit();
            this.SuspendLayout();
            // 
            // axActFXCPU1
            // 
            this.axActFXCPU1.Enabled = true;
            this.axActFXCPU1.Location = new System.Drawing.Point(30, 11);
            this.axActFXCPU1.Name = "axActFXCPU1";
            this.axActFXCPU1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axActFXCPU1.OcxState")));
            this.axActFXCPU1.Size = new System.Drawing.Size(32, 32);
            this.axActFXCPU1.TabIndex = 0;
            // 
            // ActFxCpu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.axActFXCPU1);
            this.Name = "ActFxCpu";
            this.Size = new System.Drawing.Size(159, 61);
            ((System.ComponentModel.ISupportInitialize)(this.axActFXCPU1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public AxACTPCCOMLib.AxActFXCPU axActFXCPU1;






    }
}
