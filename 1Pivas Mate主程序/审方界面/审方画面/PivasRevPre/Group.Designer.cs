namespace PivasRevPre
{
    partial class Group
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
            this.lblGroupNo = new System.Windows.Forms.Label();
            this.lblbatch = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblGroupNo
            // 
            this.lblGroupNo.AutoEllipsis = true;
            this.lblGroupNo.Location = new System.Drawing.Point(3, 1);
            this.lblGroupNo.Name = "lblGroupNo";
            this.lblGroupNo.Size = new System.Drawing.Size(68, 12);
            this.lblGroupNo.TabIndex = 0;
            this.lblGroupNo.Text = "药品组名称";
            this.lblGroupNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblGroupNo.Click += new System.EventHandler(this.lblGroupNo_Click);
            // 
            // lblbatch
            // 
            this.lblbatch.Location = new System.Drawing.Point(80, 1);
            this.lblbatch.Name = "lblbatch";
            this.lblbatch.Size = new System.Drawing.Size(37, 12);
            this.lblbatch.TabIndex = 1;
            this.lblbatch.Text = "频次";
            this.lblbatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblbatch.Click += new System.EventHandler(this.lblGroupNo_Click);
            // 
            // Group
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblbatch);
            this.Controls.Add(this.lblGroupNo);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Group";
            this.Size = new System.Drawing.Size(120, 15);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblGroupNo;
        private System.Windows.Forms.Label lblbatch;
    }
}
