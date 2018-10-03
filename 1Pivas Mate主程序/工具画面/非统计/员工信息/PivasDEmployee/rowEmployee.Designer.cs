namespace EmployeeManage
{
    partial class rowEmployee
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
            this.Account = new System.Windows.Forms.Label();
            this.Position = new System.Windows.Forms.Label();
            this.EName = new System.Windows.Forms.Label();
            this.ECode = new System.Windows.Forms.Label();
            this.delete = new System.Windows.Forms.PictureBox();
            this.ID = new System.Windows.Forms.Label();
            this.Pas = new System.Windows.Forms.Label();
            this.IsValid = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.delete)).BeginInit();
            this.SuspendLayout();
            // 
            // Account
            // 
            this.Account.Location = new System.Drawing.Point(40, 15);
            this.Account.Name = "Account";
            this.Account.Size = new System.Drawing.Size(70, 12);
            this.Account.TabIndex = 0;
            this.Account.Text = "label1";
            this.Account.Click += new System.EventHandler(this.rowEmployee_Click);
            // 
            // Position
            // 
            this.Position.Location = new System.Drawing.Point(147, 15);
            this.Position.Name = "Position";
            this.Position.Size = new System.Drawing.Size(70, 12);
            this.Position.TabIndex = 1;
            this.Position.Text = "label2";
            this.Position.Click += new System.EventHandler(this.rowEmployee_Click);
            // 
            // EName
            // 
            this.EName.Location = new System.Drawing.Point(252, 15);
            this.EName.Name = "EName";
            this.EName.Size = new System.Drawing.Size(70, 12);
            this.EName.TabIndex = 2;
            this.EName.Text = "label3";
            this.EName.Click += new System.EventHandler(this.rowEmployee_Click);
            // 
            // ECode
            // 
            this.ECode.Location = new System.Drawing.Point(356, 15);
            this.ECode.Name = "ECode";
            this.ECode.Size = new System.Drawing.Size(70, 12);
            this.ECode.TabIndex = 3;
            this.ECode.Text = "label4";
            this.ECode.Click += new System.EventHandler(this.rowEmployee_Click);
            // 
            // delete
            // 
            this.delete.Image = global::EmployeeManage.Properties.Resources.download;
            this.delete.InitialImage = global::EmployeeManage.Properties.Resources.download;
            this.delete.Location = new System.Drawing.Point(575, 11);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(16, 16);
            this.delete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.delete.TabIndex = 4;
            this.delete.TabStop = false;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // ID
            // 
            this.ID.AutoSize = true;
            this.ID.Location = new System.Drawing.Point(552, 0);
            this.ID.Name = "ID";
            this.ID.Size = new System.Drawing.Size(17, 12);
            this.ID.TabIndex = 5;
            this.ID.Text = "id";
            this.ID.Visible = false;
            // 
            // Pas
            // 
            this.Pas.AutoSize = true;
            this.Pas.Location = new System.Drawing.Point(546, 15);
            this.Pas.Name = "Pas";
            this.Pas.Size = new System.Drawing.Size(23, 12);
            this.Pas.TabIndex = 6;
            this.Pas.Text = "pas";
            this.Pas.Visible = false;
            // 
            // IsValid
            // 
            this.IsValid.AutoSize = true;
            this.IsValid.Location = new System.Drawing.Point(469, 15);
            this.IsValid.Name = "IsValid";
            this.IsValid.Size = new System.Drawing.Size(41, 12);
            this.IsValid.TabIndex = 7;
            this.IsValid.Text = "label1";
            // 
            // rowEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(230)))), ((int)(((byte)(168)))));
            this.Controls.Add(this.IsValid);
            this.Controls.Add(this.Pas);
            this.Controls.Add(this.ID);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.Position);
            this.Controls.Add(this.ECode);
            this.Controls.Add(this.Account);
            this.Controls.Add(this.EName);
            this.Name = "rowEmployee";
            this.Size = new System.Drawing.Size(613, 33);
            this.Click += new System.EventHandler(this.rowEmployee_Click);
            ((System.ComponentModel.ISupportInitialize)(this.delete)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Account;
        private System.Windows.Forms.Label Position;
        private System.Windows.Forms.Label EName;
        private System.Windows.Forms.Label ECode;
        private System.Windows.Forms.PictureBox delete;
        private System.Windows.Forms.Label ID;
        private System.Windows.Forms.Label Pas;
        private System.Windows.Forms.Label IsValid;
    }
}
