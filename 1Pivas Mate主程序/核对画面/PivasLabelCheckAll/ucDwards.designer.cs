namespace PivasLabelCheckAll
{
    partial class ucDwards
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
            this.components = new System.ComponentModel.Container();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.cbSelAll = new System.Windows.Forms.CheckBox();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnCloseWard = new System.Windows.Forms.Button();
            this.flpWards = new System.Windows.Forms.FlowLayoutPanel();
            this.timControl = new System.Windows.Forms.Timer(this.components);
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Controls.Add(this.cbSelAll);
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(653, 32);
            this.pnlTop.TabIndex = 0;
            // 
            // cbSelAll
            // 
            this.cbSelAll.AutoSize = true;
            this.cbSelAll.Checked = true;
            this.cbSelAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSelAll.Location = new System.Drawing.Point(14, 9);
            this.cbSelAll.Name = "cbSelAll";
            this.cbSelAll.Size = new System.Drawing.Size(72, 16);
            this.cbSelAll.TabIndex = 0;
            this.cbSelAll.Text = "全部选中";
            this.cbSelAll.UseVisualStyleBackColor = true;
            this.cbSelAll.CheckedChanged += new System.EventHandler(this.cbSelAll_CheckedChanged);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.Controls.Add(this.btnCloseWard);
            this.pnlBottom.Location = new System.Drawing.Point(0, 521);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(653, 33);
            this.pnlBottom.TabIndex = 1;
            // 
            // btnCloseWard
            // 
            this.btnCloseWard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseWard.BackColor = System.Drawing.Color.Transparent;
            this.btnCloseWard.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCloseWard.Location = new System.Drawing.Point(225, 0);
            this.btnCloseWard.Margin = new System.Windows.Forms.Padding(0);
            this.btnCloseWard.Name = "btnCloseWard";
            this.btnCloseWard.Size = new System.Drawing.Size(170, 33);
            this.btnCloseWard.TabIndex = 0;
            this.btnCloseWard.Text = "保     存";
            this.btnCloseWard.UseVisualStyleBackColor = false;
            this.btnCloseWard.Click += new System.EventHandler(this.btnCloseWard_Click);
            // 
            // flpWards
            // 
            this.flpWards.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flpWards.AutoScroll = true;
            this.flpWards.BackColor = System.Drawing.Color.White;
            this.flpWards.Location = new System.Drawing.Point(0, 33);
            this.flpWards.Name = "flpWards";
            this.flpWards.Size = new System.Drawing.Size(653, 482);
            this.flpWards.TabIndex = 2;
            // 
            // ucDwards
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.flpWards);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.Name = "ucDwards";
            this.Size = new System.Drawing.Size(653, 554);
            this.Load += new System.EventHandler(this.ucDwards_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.CheckBox cbSelAll;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnCloseWard;
        private System.Windows.Forms.FlowLayoutPanel flpWards;
        private System.Windows.Forms.Timer timControl;
    }
}
