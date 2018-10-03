namespace PivasNurse
{
    partial class result
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
            this.durg2 = new System.Windows.Forms.Label();
            this.durg1 = new System.Windows.Forms.Label();
            this.lblNum = new System.Windows.Forms.Label();
            this.pnl5 = new System.Windows.Forms.Panel();
            this.pnl3 = new System.Windows.Forms.Panel();
            this.lblResource = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // durg2
            // 
            this.durg2.AutoSize = true;
            this.durg2.ForeColor = System.Drawing.Color.Gray;
            this.durg2.Location = new System.Drawing.Point(41, 58);
            this.durg2.Name = "durg2";
            this.durg2.Size = new System.Drawing.Size(35, 12);
            this.durg2.TabIndex = 41;
            this.durg2.Text = "药品2";
            // 
            // durg1
            // 
            this.durg1.AutoSize = true;
            this.durg1.ForeColor = System.Drawing.Color.Gray;
            this.durg1.Location = new System.Drawing.Point(41, 39);
            this.durg1.Name = "durg1";
            this.durg1.Size = new System.Drawing.Size(35, 12);
            this.durg1.TabIndex = 42;
            this.durg1.Text = "药品1";
            // 
            // lblNum
            // 
            this.lblNum.Image = global::PivasNurse.Properties.Resources._161;
            this.lblNum.Location = new System.Drawing.Point(7, 2);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(16, 13);
            this.lblNum.TabIndex = 40;
            this.lblNum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnl5
            // 
            this.pnl5.BackgroundImage = global::PivasNurse.Properties.Resources._6;
            this.pnl5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl5.Location = new System.Drawing.Point(284, 17);
            this.pnl5.Name = "pnl5";
            this.pnl5.Size = new System.Drawing.Size(80, 15);
            this.pnl5.TabIndex = 39;
            this.pnl5.Visible = false;
            // 
            // pnl3
            // 
            this.pnl3.BackgroundImage = global::PivasNurse.Properties.Resources._7;
            this.pnl3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl3.Location = new System.Drawing.Point(285, 17);
            this.pnl3.Name = "pnl3";
            this.pnl3.Size = new System.Drawing.Size(48, 15);
            this.pnl3.TabIndex = 38;
            this.pnl3.Visible = false;
            // 
            // lblResource
            // 
            this.lblResource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResource.AutoSize = true;
            this.lblResource.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblResource.ForeColor = System.Drawing.Color.Teal;
            this.lblResource.Location = new System.Drawing.Point(27, 73);
            this.lblResource.Name = "lblResource";
            this.lblResource.Padding = new System.Windows.Forms.Padding(3);
            this.lblResource.Size = new System.Drawing.Size(47, 18);
            this.lblResource.TabIndex = 37;
            this.lblResource.Text = "来源：";
            this.lblResource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDesc
            // 
            this.lblDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDesc.AutoEllipsis = true;
            this.lblDesc.AutoSize = true;
            this.lblDesc.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDesc.Location = new System.Drawing.Point(41, 20);
            this.lblDesc.MaximumSize = new System.Drawing.Size(250, 24);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(29, 12);
            this.lblDesc.TabIndex = 36;
            this.lblDesc.Text = "描述";
            this.lblDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblResult
            // 
            this.lblResult.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblResult.ForeColor = System.Drawing.Color.Maroon;
            this.lblResult.Location = new System.Drawing.Point(27, 3);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(92, 12);
            this.lblResult.TabIndex = 35;
            this.lblResult.Text = "结果";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // result
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.durg2);
            this.Controls.Add(this.durg1);
            this.Controls.Add(this.lblNum);
            this.Controls.Add(this.pnl3);
            this.Controls.Add(this.lblResource);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.pnl5);
            this.Name = "result";
            this.Size = new System.Drawing.Size(371, 100);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label durg2;
        private System.Windows.Forms.Label durg1;
        public System.Windows.Forms.Label lblNum;
        private System.Windows.Forms.Panel pnl5;
        private System.Windows.Forms.Panel pnl3;
        private System.Windows.Forms.Label lblResource;
        public System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.Label lblResult;
    }
}
