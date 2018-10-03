namespace PivasNurse
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cob_word = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_word = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pbox_pwd = new System.Windows.Forms.PictureBox();
            this.txt_pwd = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbox_user = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Panel_Close = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.Label_Set = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_pwd)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(107)))), ((int)(((byte)(225)))));
            this.panel1.Controls.Add(this.cob_word);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txt_word);
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.Panel_Close);
            this.panel1.Location = new System.Drawing.Point(-4, -6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(434, 250);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // cob_word
            // 
            this.cob_word.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cob_word.FormattingEnabled = true;
            this.cob_word.Location = new System.Drawing.Point(237, 85);
            this.cob_word.Name = "cob_word";
            this.cob_word.Size = new System.Drawing.Size(151, 22);
            this.cob_word.TabIndex = 29;
            this.cob_word.SelectionChangeCommitted += new System.EventHandler(this.cob_word_SelectionChangeCommitted);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(362, 202);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 28;
            this.label5.Text = "更改密码";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // txt_word
            // 
            this.txt_word.AutoSize = true;
            this.txt_word.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_word.ForeColor = System.Drawing.SystemColors.Info;
            this.txt_word.Location = new System.Drawing.Point(239, 92);
            this.txt_word.Name = "txt_word";
            this.txt_word.Size = new System.Drawing.Size(53, 12);
            this.txt_word.TabIndex = 23;
            this.txt_word.Tag = "0309";
            this.txt_word.Text = "感染病科";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::PivasNurse.Properties.Resources.Pills;
            this.pictureBox3.Location = new System.Drawing.Point(202, 72);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(34, 35);
            this.pictureBox3.TabIndex = 22;
            this.pictureBox3.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(181, 108);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 21;
            this.label9.Text = "工作站";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 47F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(49, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(191, 103);
            this.label3.TabIndex = 15;
            this.label3.Text = "病区";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Location = new System.Drawing.Point(142, 186);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(214, 26);
            this.panel3.TabIndex = 14;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Silver;
            this.panel5.Controls.Add(this.pbox_pwd);
            this.panel5.Controls.Add(this.txt_pwd);
            this.panel5.Location = new System.Drawing.Point(1, 1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(212, 24);
            this.panel5.TabIndex = 10;
            // 
            // pbox_pwd
            // 
            this.pbox_pwd.BackColor = System.Drawing.Color.White;
            this.pbox_pwd.Image = global::PivasNurse.Properties.Resources._555;
            this.pbox_pwd.Location = new System.Drawing.Point(184, 1);
            this.pbox_pwd.Name = "pbox_pwd";
            this.pbox_pwd.Size = new System.Drawing.Size(27, 22);
            this.pbox_pwd.TabIndex = 11;
            this.pbox_pwd.TabStop = false;
            this.pbox_pwd.MouseLeave += new System.EventHandler(this.pictureBox2_MouseLeave);
            this.pbox_pwd.Click += new System.EventHandler(this.pbox_pwd_Click);
            this.pbox_pwd.MouseHover += new System.EventHandler(this.pictureBox2_MouseHover);
            // 
            // txt_pwd
            // 
            this.txt_pwd.BackColor = System.Drawing.Color.White;
            this.txt_pwd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_pwd.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_pwd.Location = new System.Drawing.Point(1, 1);
            this.txt_pwd.Name = "txt_pwd";
            this.txt_pwd.Size = new System.Drawing.Size(183, 22);
            this.txt_pwd.TabIndex = 9;
            this.txt_pwd.UseSystemPasswordChar = true;
            this.txt_pwd.MouseLeave += new System.EventHandler(this.textBox2_MouseLeave);
            this.txt_pwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Txt_User_KeyPress);
            this.txt_pwd.MouseHover += new System.EventHandler(this.textBox2_MouseHover);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Location = new System.Drawing.Point(142, 150);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(214, 26);
            this.panel4.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.cbox_user);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(213, 26);
            this.panel2.TabIndex = 10;
            // 
            // cbox_user
            // 
            this.cbox_user.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbox_user.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbox_user.FormattingEnabled = true;
            this.cbox_user.Location = new System.Drawing.Point(1, 1);
            this.cbox_user.Name = "cbox_user";
            this.cbox_user.Size = new System.Drawing.Size(211, 24);
            this.cbox_user.TabIndex = 3;
            this.cbox_user.MouseHover += new System.EventHandler(this.cbox_user_MouseHover);
            this.cbox_user.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cbox_user_MouseMove);
            this.cbox_user.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbox_user_KeyPress);
            this.cbox_user.MouseLeave += new System.EventHandler(this.cbox_user_MouseLeave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(107)))), ((int)(((byte)(225)))));
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(15, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "LAENNEC";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // Panel_Close
            // 
            this.Panel_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Close.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel_Close.BackgroundImage")));
            this.Panel_Close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Panel_Close.Location = new System.Drawing.Point(402, 6);
            this.Panel_Close.Name = "Panel_Close";
            this.Panel_Close.Size = new System.Drawing.Size(20, 26);
            this.Panel_Close.TabIndex = 8;
            this.Panel_Close.MouseLeave += new System.EventHandler(this.Panel_Close_MouseLeave);
            this.Panel_Close.Click += new System.EventHandler(this.Panel_Close_Click);
            this.Panel_Close.MouseHover += new System.EventHandler(this.Panel_Close_MouseHover);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(179)))), ((int)(((byte)(253)))));
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(288, 251);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "登录";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.MouseLeave += new System.EventHandler(this.label1_MouseLeave);
            this.label1.Click += new System.EventHandler(this.label1_Click);
            this.label1.MouseHover += new System.EventHandler(this.label1_MouseHover);
            // 
            // Label_Set
            // 
            this.Label_Set.AutoSize = true;
            this.Label_Set.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label_Set.ForeColor = System.Drawing.Color.Gray;
            this.Label_Set.Location = new System.Drawing.Point(12, 257);
            this.Label_Set.Name = "Label_Set";
            this.Label_Set.Size = new System.Drawing.Size(29, 12);
            this.Label_Set.TabIndex = 2;
            this.Label_Set.Text = "设置";
            this.Label_Set.Click += new System.EventHandler(this.Label_Set_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(215)))), ((int)(((byte)(234)))));
            this.ClientSize = new System.Drawing.Size(419, 283);
            this.Controls.Add(this.Label_Set);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_pwd)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel Panel_Close;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox pbox_pwd;
        private System.Windows.Forms.TextBox txt_pwd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txt_word;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label Label_Set;
        private System.Windows.Forms.ComboBox cbox_user;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cob_word;
    }
}