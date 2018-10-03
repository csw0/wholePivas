namespace PivasFreqRule
{
    partial class DWard
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
            this.Ward = new System.Windows.Forms.Label();
            this.Code = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Ward
            // 
            this.Ward.BackColor = System.Drawing.Color.Transparent;
            this.Ward.Location = new System.Drawing.Point(3, 5);
            this.Ward.Name = "Ward";
            this.Ward.Size = new System.Drawing.Size(153, 23);
            this.Ward.TabIndex = 0;
            this.Ward.Text = "label1";
            this.Ward.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Ward.Click += new System.EventHandler(this.Ward_Click);
            // 
            // Code
            // 
            this.Code.AutoSize = true;
            this.Code.Location = new System.Drawing.Point(115, 10);
            this.Code.Name = "Code";
            this.Code.Size = new System.Drawing.Size(41, 12);
            this.Code.TabIndex = 1;
            this.Code.Text = "label1";
            this.Code.Visible = false;
            // 
            // DWard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Code);
            this.Controls.Add(this.Ward);
            this.Name = "DWard";
            this.Size = new System.Drawing.Size(159, 30);
            this.Click += new System.EventHandler(this.Ward_Click);
            
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Code;
        public System.Windows.Forms.Label Ward;
    }
}
