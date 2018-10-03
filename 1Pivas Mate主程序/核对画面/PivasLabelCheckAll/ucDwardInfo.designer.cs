namespace PivasLabelCheckAll
{
    partial class ucDwardInfo
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
            this.cbSelected = new System.Windows.Forms.CheckBox();
            this.lblWardName = new System.Windows.Forms.Label();
            this.LblWardArea = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tboxWardSimName = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbSelected
            // 
            this.cbSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSelected.AutoSize = true;
            this.cbSelected.Checked = true;
            this.cbSelected.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSelected.Location = new System.Drawing.Point(249, 3);
            this.cbSelected.Name = "cbSelected";
            this.cbSelected.Size = new System.Drawing.Size(15, 14);
            this.cbSelected.TabIndex = 0;
            this.cbSelected.UseVisualStyleBackColor = true;
            this.cbSelected.Visible = false;
            this.cbSelected.Click += new System.EventHandler(this.ucDwardInfo_Click);
            // 
            // lblWardName
            // 
            this.lblWardName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWardName.Location = new System.Drawing.Point(0, 0);
            this.lblWardName.Name = "lblWardName";
            this.lblWardName.Size = new System.Drawing.Size(229, 30);
            this.lblWardName.TabIndex = 1;
            this.lblWardName.Text = "病区";
            this.lblWardName.Click += new System.EventHandler(this.ucDwardInfo_Click);
            // 
            // LblWardArea
            // 
            this.LblWardArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LblWardArea.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LblWardArea.Location = new System.Drawing.Point(90, 27);
            this.LblWardArea.Name = "LblWardArea";
            this.LblWardArea.Size = new System.Drawing.Size(139, 23);
            this.LblWardArea.TabIndex = 2;
            this.LblWardArea.Text = "WardArea";
            this.LblWardArea.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LblWardArea.Click += new System.EventHandler(this.ucDwardInfo_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblWardName);
            this.panel1.Controls.Add(this.LblWardArea);
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(229, 49);
            this.panel1.TabIndex = 3;
            this.panel1.Click += new System.EventHandler(this.ucDwardInfo_Click);
            // 
            // tboxWardSimName
            // 
            this.tboxWardSimName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxWardSimName.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tboxWardSimName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tboxWardSimName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tboxWardSimName.Location = new System.Drawing.Point(218, 14);
            this.tboxWardSimName.Multiline = true;
            this.tboxWardSimName.Name = "tboxWardSimName";
            this.tboxWardSimName.Size = new System.Drawing.Size(25, 15);
            this.tboxWardSimName.TabIndex = 2;
            this.tboxWardSimName.Visible = false;
            // 
            // ucDwardInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tboxWardSimName);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbSelected);
            this.Margin = new System.Windows.Forms.Padding(8);
            this.Name = "ucDwardInfo";
            this.Size = new System.Drawing.Size(227, 47);
            this.Load += new System.EventHandler(this.ucDwardInfo_Load);
            this.Click += new System.EventHandler(this.ucDwardInfo_Click);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbSelected;
        private System.Windows.Forms.Label lblWardName;
        private System.Windows.Forms.Label LblWardArea;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tboxWardSimName;
    }
}
