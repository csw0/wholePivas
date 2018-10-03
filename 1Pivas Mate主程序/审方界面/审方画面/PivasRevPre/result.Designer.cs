namespace PivasRevPre
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
            this.lblResource = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.pnl5 = new System.Windows.Forms.Panel();
            this.pnl3 = new System.Windows.Forms.Panel();
            this.durg1 = new System.Windows.Forms.Label();
            this.durg2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblResource
            // 
            this.lblResource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResource.AutoSize = true;
            this.lblResource.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblResource.ForeColor = System.Drawing.Color.Black;
            this.lblResource.Location = new System.Drawing.Point(87, 72);
            this.lblResource.Name = "lblResource";
            this.lblResource.Padding = new System.Windows.Forms.Padding(3);
            this.lblResource.Size = new System.Drawing.Size(47, 18);
            this.lblResource.TabIndex = 29;
            this.lblResource.Text = "来源：";
            this.lblResource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblResource.Click += new System.EventHandler(this.result_Click);
            // 
            // lblDesc
            // 
            this.lblDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDesc.AutoEllipsis = true;
            this.lblDesc.AutoSize = true;
            this.lblDesc.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDesc.Location = new System.Drawing.Point(74, 19);
            this.lblDesc.MaximumSize = new System.Drawing.Size(200, 24);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(29, 12);
            this.lblDesc.TabIndex = 28;
            this.lblDesc.Text = "描述";
            this.lblDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDesc.Click += new System.EventHandler(this.result_Click);
            // 
            // lblResult
            // 
            this.lblResult.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblResult.ForeColor = System.Drawing.Color.Black;
            this.lblResult.Location = new System.Drawing.Point(2, 38);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(66, 12);
            this.lblResult.TabIndex = 27;
            this.lblResult.Text = "结果";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblResult.Click += new System.EventHandler(this.result_Click);
            // 
            // pnl5
            // 
            this.pnl5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl5.BackgroundImage = global::PivasRevPre.Properties.Resources._6;
            this.pnl5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl5.Location = new System.Drawing.Point(249, 3);
            this.pnl5.Name = "pnl5";
            this.pnl5.Size = new System.Drawing.Size(77, 15);
            this.pnl5.TabIndex = 32;
            this.pnl5.Visible = false;
            this.pnl5.Click += new System.EventHandler(this.result_Click);
            // 
            // pnl3
            // 
            this.pnl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl3.BackgroundImage = global::PivasRevPre.Properties.Resources._7;
            this.pnl3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl3.Location = new System.Drawing.Point(248, 3);
            this.pnl3.Name = "pnl3";
            this.pnl3.Size = new System.Drawing.Size(45, 15);
            this.pnl3.TabIndex = 30;
            this.pnl3.Visible = false;
            this.pnl3.Click += new System.EventHandler(this.result_Click);
            // 
            // durg1
            // 
            this.durg1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.durg1.AutoSize = true;
            this.durg1.ForeColor = System.Drawing.Color.Black;
            this.durg1.Location = new System.Drawing.Point(75, 38);
            this.durg1.Name = "durg1";
            this.durg1.Size = new System.Drawing.Size(35, 12);
            this.durg1.TabIndex = 34;
            this.durg1.Text = "药品1";
            // 
            // durg2
            // 
            this.durg2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.durg2.AutoSize = true;
            this.durg2.ForeColor = System.Drawing.Color.Black;
            this.durg2.Location = new System.Drawing.Point(75, 57);
            this.durg2.Name = "durg2";
            this.durg2.Size = new System.Drawing.Size(35, 12);
            this.durg2.TabIndex = 34;
            this.durg2.Text = "药品2";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.BackColor = System.Drawing.Color.DarkGray;
            this.label5.Location = new System.Drawing.Point(69, -6);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(2, 163);
            this.label5.TabIndex = 66;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.BackColor = System.Drawing.Color.DarkGray;
            this.label17.Location = new System.Drawing.Point(-37, 111);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(413, 2);
            this.label17.TabIndex = 71;
            // 
            // result
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.durg2);
            this.Controls.Add(this.durg1);
            this.Controls.Add(this.pnl5);
            this.Controls.Add(this.pnl3);
            this.Controls.Add(this.lblResource);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.lblResult);
            this.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.Name = "result";
            this.Size = new System.Drawing.Size(342, 150);
            this.Click += new System.EventHandler(this.result_Click);
            this.SizeChanged += new System.EventHandler(this.result_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblResource;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Panel pnl3;
        private System.Windows.Forms.Panel pnl5;
        private System.Windows.Forms.Label durg1;
        private System.Windows.Forms.Label durg2;
        public System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label17;
    }
}
