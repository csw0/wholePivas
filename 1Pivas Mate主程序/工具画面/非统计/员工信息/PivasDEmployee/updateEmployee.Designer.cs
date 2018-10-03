namespace EmployeeManage
{
    partial class updateEmployee
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(updateEmployee));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Account = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Pas = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Position = new System.Windows.Forms.TextBox();
            this.EName = new System.Windows.Forms.TextBox();
            this.ECode = new System.Windows.Forms.TextBox();
            this.IsValid = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Panel_Close = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblSave = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(5, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "修改员工信息";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(264, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "登录密码";
            this.label2.Visible = false;
            // 
            // Account
            // 
            this.Account.Enabled = false;
            this.Account.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Account.Location = new System.Drawing.Point(134, 18);
            this.Account.Name = "Account";
            this.Account.Size = new System.Drawing.Size(127, 23);
            this.Account.TabIndex = 1;
            this.Account.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Account_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(41, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 3;
            this.label3.Text = "登录账户";
            // 
            // Pas
            // 
            this.Pas.Enabled = false;
            this.Pas.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Pas.Location = new System.Drawing.Point(267, 16);
            this.Pas.Name = "Pas";
            this.Pas.PasswordChar = '*';
            this.Pas.Size = new System.Drawing.Size(29, 23);
            this.Pas.TabIndex = 2;
            this.Pas.Visible = false;
            this.Pas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Account_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(41, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 14);
            this.label4.TabIndex = 5;
            this.label4.Text = "员工职务";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(41, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 14);
            this.label5.TabIndex = 13;
            this.label5.Text = "员工姓名";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(41, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 14);
            this.label6.TabIndex = 14;
            this.label6.Text = "员工工号";
            // 
            // Position
            // 
            this.Position.Enabled = false;
            this.Position.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Position.Location = new System.Drawing.Point(134, 125);
            this.Position.Name = "Position";
            this.Position.Size = new System.Drawing.Size(127, 23);
            this.Position.TabIndex = 3;
            this.Position.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Account_KeyPress);
            // 
            // EName
            // 
            this.EName.Enabled = false;
            this.EName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.EName.Location = new System.Drawing.Point(134, 54);
            this.EName.Name = "EName";
            this.EName.Size = new System.Drawing.Size(127, 23);
            this.EName.TabIndex = 4;
            this.EName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Account_KeyPress);
            // 
            // ECode
            // 
            this.ECode.Enabled = false;
            this.ECode.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ECode.Location = new System.Drawing.Point(134, 89);
            this.ECode.Name = "ECode";
            this.ECode.Size = new System.Drawing.Size(127, 23);
            this.ECode.TabIndex = 5;
            this.ECode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Account_KeyPress);
            // 
            // IsValid
            // 
            this.IsValid.AutoSize = true;
            this.IsValid.BackColor = System.Drawing.Color.Transparent;
            this.IsValid.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IsValid.Location = new System.Drawing.Point(43, 218);
            this.IsValid.Name = "IsValid";
            this.IsValid.Size = new System.Drawing.Size(82, 18);
            this.IsValid.TabIndex = 18;
            this.IsValid.Text = "账户有效";
            this.IsValid.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(107)))), ((int)(((byte)(225)))));
            this.panel1.Controls.Add(this.Panel_Close);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(305, 33);
            this.panel1.TabIndex = 19;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // Panel_Close
            // 
            this.Panel_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(107)))), ((int)(((byte)(225)))));
            this.Panel_Close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel_Close.BackgroundImage")));
            this.Panel_Close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Panel_Close.Location = new System.Drawing.Point(284, 0);
            this.Panel_Close.Name = "Panel_Close";
            this.Panel_Close.Size = new System.Drawing.Size(20, 26);
            this.Panel_Close.TabIndex = 19;
            this.Panel_Close.MouseLeave += new System.EventHandler(this.Panel_Close_MouseLeave);
            this.Panel_Close.Click += new System.EventHandler(this.panel3_Click);
            this.Panel_Close.MouseHover += new System.EventHandler(this.Panel_Close_MouseHover);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(246)))));
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.lblSave);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.IsValid);
            this.panel2.Controls.Add(this.Account);
            this.panel2.Controls.Add(this.ECode);
            this.panel2.Controls.Add(this.Pas);
            this.panel2.Controls.Add(this.EName);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.Position);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(1, 33);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(305, 279);
            this.panel2.TabIndex = 20;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(210, 246);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(67, 23);
            this.button2.TabIndex = 42;
            this.button2.Text = "初始密码";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(210, 218);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 23);
            this.button1.TabIndex = 41;
            this.button1.Text = "权限>>";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.LocationChanged += new System.EventHandler(this.button1_LocationChanged);
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "未维护人员",
            "静配中心人员",
            "护士",
            "其他"});
            this.comboBox1.Location = new System.Drawing.Point(131, 164);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(130, 22);
            this.comboBox1.TabIndex = 40;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(40, 166);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 14);
            this.label7.TabIndex = 39;
            this.label7.Text = "员工类型";
            // 
            // lblSave
            // 
            this.lblSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(179)))), ((int)(((byte)(253)))));
            this.lblSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSave.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSave.ForeColor = System.Drawing.Color.White;
            this.lblSave.Location = new System.Drawing.Point(131, 218);
            this.lblSave.Name = "lblSave";
            this.lblSave.Size = new System.Drawing.Size(73, 23);
            this.lblSave.TabIndex = 38;
            this.lblSave.Text = "保存";
            this.lblSave.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSave.MouseLeave += new System.EventHandler(this.lblSave_MouseLeave);
            this.lblSave.Click += new System.EventHandler(this.save_Click);
            this.lblSave.MouseHover += new System.EventHandler(this.lblSave_MouseHover);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(107)))), ((int)(((byte)(225)))));
            this.panel4.Location = new System.Drawing.Point(0, 312);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(305, 17);
            this.panel4.TabIndex = 21;
            // 
            // updateEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(306, 315);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "updateEmployee";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "updateDWard";
            this.Load += new System.EventHandler(this.updateEmployee_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Account;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Pas;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Position;
        private System.Windows.Forms.TextBox EName;
        private System.Windows.Forms.TextBox ECode;
        private System.Windows.Forms.CheckBox IsValid;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel Panel_Close;
        private System.Windows.Forms.Label lblSave;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}