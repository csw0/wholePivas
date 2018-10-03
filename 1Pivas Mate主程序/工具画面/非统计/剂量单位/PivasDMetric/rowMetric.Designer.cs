namespace DMetricManage
{
    partial class rowMetric
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
            this.lblCode = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.lblPName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.delete = new System.Windows.Forms.PictureBox();
            this.cbbPName = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.delete)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCode
            // 
            this.lblCode.BackColor = System.Drawing.Color.Transparent;
            this.lblCode.Location = new System.Drawing.Point(103, 10);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(100, 12);
            this.lblCode.TabIndex = 0;
            this.lblCode.Text = "label1";
            this.lblCode.Click += new System.EventHandler(this.rowMetric_Click);
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(349, 10);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(100, 12);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "label2";
            this.lblName.Click += new System.EventHandler(this.rowMetric_Click);
            // 
            // lblID
            // 
            this.lblID.Location = new System.Drawing.Point(588, 6);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(140, 29);
            this.lblID.TabIndex = 3;
            this.lblID.Text = "label4";
            this.lblID.Visible = false;
            // 
            // lblPName
            // 
            this.lblPName.Enabled = false;
            this.lblPName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPName.Location = new System.Drawing.Point(583, 0);
            this.lblPName.Name = "lblPName";
            this.lblPName.Size = new System.Drawing.Size(130, 30);
            this.lblPName.TabIndex = 6;
            this.lblPName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPName.Click += new System.EventHandler(this.label3_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.ForeColor = System.Drawing.Color.Silver;
            this.panel1.Location = new System.Drawing.Point(10, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(740, 1);
            this.panel1.TabIndex = 7;
            // 
            // delete
            // 
            this.delete.Image = global::DMetricManage.Properties.Resources.delete_16;
            this.delete.Location = new System.Drawing.Point(37, 6);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(20, 20);
            this.delete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.delete.TabIndex = 4;
            this.delete.TabStop = false;
            this.delete.Visible = false;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // cbbPName
            // 
            this.cbbPName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(246)))));
            this.cbbPName.DropDownHeight = 96;
            this.cbbPName.Enabled = false;
            this.cbbPName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbbPName.FormattingEnabled = true;
            this.cbbPName.IntegralHeight = false;
            this.cbbPName.Location = new System.Drawing.Point(583, 6);
            this.cbbPName.Name = "cbbPName";
            this.cbbPName.Size = new System.Drawing.Size(125, 20);
            this.cbbPName.TabIndex = 5;
            this.cbbPName.Visible = false;
            this.cbbPName.VisibleChanged += new System.EventHandler(this.cbbPName_VisibleChanged);
            this.cbbPName.SelectedIndexChanged += new System.EventHandler(this.cbbPName_SelectedIndexChanged);
            // 
            // rowMetric
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblPName);
            this.Controls.Add(this.cbbPName);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.lblCode);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "rowMetric";
            this.Size = new System.Drawing.Size(760, 33);
            this.Click += new System.EventHandler(this.rowMetric_Click);
            ((System.ComponentModel.ISupportInitialize)(this.delete)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.PictureBox delete;
        private System.Windows.Forms.Label lblPName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbbPName;
    }
}
