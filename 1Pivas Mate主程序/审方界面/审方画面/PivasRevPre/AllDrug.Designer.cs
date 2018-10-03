namespace PivasRevPre
{
    partial class AllDrug
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
            this.lblName = new System.Windows.Forms.Label();
            this.lblMetric = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoEllipsis = true;
            this.lblName.Location = new System.Drawing.Point(3, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(190, 15);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "药品名称";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblName.Click += new System.EventHandler(this.lblName_Click);
            // 
            // lblMetric
            // 
            this.lblMetric.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMetric.AutoEllipsis = true;
            this.lblMetric.Location = new System.Drawing.Point(189, 0);
            this.lblMetric.Name = "lblMetric";
            this.lblMetric.Size = new System.Drawing.Size(65, 15);
            this.lblMetric.TabIndex = 1;
            this.lblMetric.Text = "规格";
            this.lblMetric.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblMetric.Click += new System.EventHandler(this.lblName_Click);
            // 
            // AllDrug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblMetric);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AllDrug";
            this.Size = new System.Drawing.Size(261, 15);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblMetric;
    }
}
