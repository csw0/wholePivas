namespace PivasBatch
{
    partial class Ward
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
            this.lblWardCode = new System.Windows.Forms.Label();
            this.Label_NotGet = new System.Windows.Forms.Label();
            this.lblWardName = new System.Windows.Forms.Label();
            this.Label_Total = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblWardCode
            // 
            this.lblWardCode.AutoSize = true;
            this.lblWardCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblWardCode.Location = new System.Drawing.Point(64, 6);
            this.lblWardCode.Name = "lblWardCode";
            this.lblWardCode.Size = new System.Drawing.Size(17, 12);
            this.lblWardCode.TabIndex = 5;
            this.lblWardCode.Text = "ID";
            this.lblWardCode.Visible = false;
            this.lblWardCode.Click += new System.EventHandler(this.Word_Click);
            // 
            // Label_NotGet
            // 
            this.Label_NotGet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_NotGet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label_NotGet.Location = new System.Drawing.Point(153, 6);
            this.Label_NotGet.Name = "Label_NotGet";
            this.Label_NotGet.Size = new System.Drawing.Size(36, 12);
            this.Label_NotGet.TabIndex = 4;
            this.Label_NotGet.Text = "未发送";
            this.Label_NotGet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label_NotGet.Click += new System.EventHandler(this.Word_Click);
            // 
            // lblWardName
            // 
            this.lblWardName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblWardName.Location = new System.Drawing.Point(5, 6);
            this.lblWardName.Name = "lblWardName";
            this.lblWardName.Size = new System.Drawing.Size(103, 12);
            this.lblWardName.TabIndex = 3;
            this.lblWardName.Text = "病区名称";
            this.lblWardName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblWardName.Click += new System.EventHandler(this.Word_Click);
            // 
            // Label_Total
            // 
            this.Label_Total.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Total.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label_Total.Location = new System.Drawing.Point(132, 6);
            this.Label_Total.Name = "Label_Total";
            this.Label_Total.Size = new System.Drawing.Size(69, 12);
            this.Label_Total.TabIndex = 4;
            this.Label_Total.Text = "总数";
            this.Label_Total.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_Total.Click += new System.EventHandler(this.Word_Click);
            // 
            // Ward
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.Label_Total);
            this.Controls.Add(this.lblWardCode);
            this.Controls.Add(this.Label_NotGet);
            this.Controls.Add(this.lblWardName);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Ward";
            this.Size = new System.Drawing.Size(185, 24);
            this.Click += new System.EventHandler(this.Word_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_NotGet;
        public System.Windows.Forms.Label lblWardCode;
        public System.Windows.Forms.Label lblWardName;
        public System.Windows.Forms.Label Label_Total;
    }
}
