namespace SetSyn
{
    partial class USynSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(USynSet));
            this.Label_SynType = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.Panel_Test = new System.Windows.Forms.Panel();
            this.Label_Test = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Txt_InitialCatalog = new System.Windows.Forms.ComboBox();
            this.Txt_DataSource = new System.Windows.Forms.ComboBox();
            this.Txt_UserID = new System.Windows.Forms.ComboBox();
            this.Txt_Password = new System.Windows.Forms.TextBox();
            this.Pic_TypeChange = new System.Windows.Forms.PictureBox();
            this.Label_TypeText = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.uChange1 = new SetSyn.UChange();
            this.panel5.SuspendLayout();
            this.Panel_Test.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_TypeChange)).BeginInit();
            this.SuspendLayout();
            // 
            // Label_SynType
            // 
            this.Label_SynType.AutoSize = true;
            this.Label_SynType.BackColor = System.Drawing.Color.White;
            this.Label_SynType.Font = new System.Drawing.Font("宋体", 11F);
            this.Label_SynType.ForeColor = System.Drawing.Color.Black;
            this.Label_SynType.Location = new System.Drawing.Point(26, 16);
            this.Label_SynType.Name = "Label_SynType";
            this.Label_SynType.Size = new System.Drawing.Size(67, 15);
            this.Label_SynType.TabIndex = 13;
            this.Label_SynType.Text = "同步方式";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 11F);
            this.label5.Location = new System.Drawing.Point(26, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 15);
            this.label5.TabIndex = 26;
            this.label5.Text = "同步SQL";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 11F);
            this.label6.Location = new System.Drawing.Point(26, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 28;
            this.label6.Text = "同步配置";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.BackgroundImage = global::SetSyn.Properties.Resources.设置画面小按钮;
            this.panel5.Controls.Add(this.label3);
            this.panel5.Location = new System.Drawing.Point(323, 108);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(46, 24);
            this.panel5.TabIndex = 24;
            this.panel5.Click += new System.EventHandler(this.Pic_CopySQL_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.Font = new System.Drawing.Font("宋体", 10F);
            this.label3.Location = new System.Drawing.Point(6, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "复制";
            this.label3.Click += new System.EventHandler(this.Pic_CopySQL_Click);
            // 
            // Panel_Test
            // 
            this.Panel_Test.BackgroundImage = global::SetSyn.Properties.Resources.设置画面小按钮;
            this.Panel_Test.Controls.Add(this.Label_Test);
            this.Panel_Test.Location = new System.Drawing.Point(271, 108);
            this.Panel_Test.Name = "Panel_Test";
            this.Panel_Test.Size = new System.Drawing.Size(46, 24);
            this.Panel_Test.TabIndex = 31;
            // 
            // Label_Test
            // 
            this.Label_Test.AutoSize = true;
            this.Label_Test.BackColor = System.Drawing.Color.Transparent;
            this.Label_Test.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label_Test.Font = new System.Drawing.Font("宋体", 10F);
            this.Label_Test.Location = new System.Drawing.Point(6, 5);
            this.Label_Test.Name = "Label_Test";
            this.Label_Test.Size = new System.Drawing.Size(35, 14);
            this.Label_Test.TabIndex = 0;
            this.Label_Test.Text = "测试";
            this.Label_Test.Click += new System.EventHandler(this.Label_Test_Click);
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::SetSyn.Properties.Resources.配置;
            this.panel3.Controls.Add(this.Txt_InitialCatalog);
            this.panel3.Controls.Add(this.Txt_DataSource);
            this.panel3.Controls.Add(this.Txt_UserID);
            this.panel3.Controls.Add(this.Txt_Password);
            this.panel3.Location = new System.Drawing.Point(23, 61);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(346, 46);
            this.panel3.TabIndex = 27;
            // 
            // Txt_InitialCatalog
            // 
            this.Txt_InitialCatalog.BackColor = System.Drawing.Color.White;
            this.Txt_InitialCatalog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Txt_InitialCatalog.Location = new System.Drawing.Point(3, 24);
            this.Txt_InitialCatalog.Name = "Txt_InitialCatalog";
            this.Txt_InitialCatalog.Size = new System.Drawing.Size(167, 20);
            this.Txt_InitialCatalog.TabIndex = 1;
            this.Txt_InitialCatalog.DropDown += new System.EventHandler(this.Txt_InitialCatalog_DropDown);
            this.Txt_InitialCatalog.TextChanged += new System.EventHandler(this.Txt_InitialCatalog_TextChanged);
            this.Txt_InitialCatalog.Click += new System.EventHandler(this.Txt_InitialCatalog_Click);
            // 
            // Txt_DataSource
            // 
            this.Txt_DataSource.BackColor = System.Drawing.Color.White;
            this.Txt_DataSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Txt_DataSource.ForeColor = System.Drawing.Color.Black;
            this.Txt_DataSource.Location = new System.Drawing.Point(3, 1);
            this.Txt_DataSource.Name = "Txt_DataSource";
            this.Txt_DataSource.Size = new System.Drawing.Size(167, 20);
            this.Txt_DataSource.TabIndex = 0;
            this.Txt_DataSource.DropDown += new System.EventHandler(this.Txt_DataSource_DropDown);
            this.Txt_DataSource.TextChanged += new System.EventHandler(this.Txt_DataSource_TextChanged);
            this.Txt_DataSource.Click += new System.EventHandler(this.Txt_DataSource_Click);
            // 
            // Txt_UserID
            // 
            this.Txt_UserID.BackColor = System.Drawing.Color.White;
            this.Txt_UserID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Txt_UserID.Location = new System.Drawing.Point(176, 1);
            this.Txt_UserID.Name = "Txt_UserID";
            this.Txt_UserID.Size = new System.Drawing.Size(166, 20);
            this.Txt_UserID.TabIndex = 2;
            this.Txt_UserID.DropDown += new System.EventHandler(this.Txt_UserID_DropDown);
            this.Txt_UserID.TextChanged += new System.EventHandler(this.Txt_UserID_TextChanged);
            this.Txt_UserID.Click += new System.EventHandler(this.Txt_UserID_Click);
            // 
            // Txt_Password
            // 
            this.Txt_Password.BackColor = System.Drawing.Color.White;
            this.Txt_Password.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Txt_Password.Location = new System.Drawing.Point(178, 27);
            this.Txt_Password.Name = "Txt_Password";
            this.Txt_Password.PasswordChar = '*';
            this.Txt_Password.Size = new System.Drawing.Size(160, 14);
            this.Txt_Password.TabIndex = 3;
            this.Txt_Password.WordWrap = false;
            this.Txt_Password.TextChanged += new System.EventHandler(this.Txt_Password_TextChanged);
            this.Txt_Password.Click += new System.EventHandler(this.Txt_Password_Click);
            // 
            // Pic_TypeChange
            // 
            this.Pic_TypeChange.BackColor = System.Drawing.Color.Transparent;
            this.Pic_TypeChange.Image = global::SetSyn.Properties.Resources.设置下拉框按钮;
            this.Pic_TypeChange.Location = new System.Drawing.Point(337, 10);
            this.Pic_TypeChange.Name = "Pic_TypeChange";
            this.Pic_TypeChange.Size = new System.Drawing.Size(32, 26);
            this.Pic_TypeChange.TabIndex = 20;
            this.Pic_TypeChange.TabStop = false;
            this.Pic_TypeChange.Click += new System.EventHandler(this.Pic_TypeChange_Click);
            // 
            // Label_TypeText
            // 
            this.Label_TypeText.Font = new System.Drawing.Font("宋体", 12F);
            this.Label_TypeText.Image = global::SetSyn.Properties.Resources.设置方式边框;
            this.Label_TypeText.Location = new System.Drawing.Point(19, 8);
            this.Label_TypeText.Name = "Label_TypeText";
            this.Label_TypeText.Size = new System.Drawing.Size(352, 32);
            this.Label_TypeText.TabIndex = 22;
            this.Label_TypeText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(98, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 14);
            this.label1.TabIndex = 32;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(23, 134);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(346, 313);
            this.textBox1.TabIndex = 33;
            // 
            // uChange1
            // 
            this.uChange1.BackColor = System.Drawing.Color.Transparent;
            this.uChange1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uChange1.BackgroundImage")));
            this.uChange1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uChange1.Location = new System.Drawing.Point(219, 37);
            this.uChange1.Name = "uChange1";
            this.uChange1.Size = new System.Drawing.Size(150, 108);
            this.uChange1.TabIndex = 27;
            this.uChange1.Visible = false;
            // 
            // USynSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(227)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.uChange1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.Panel_Test);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Pic_TypeChange);
            this.Controls.Add(this.Label_SynType);
            this.Controls.Add(this.Label_TypeText);
            this.Name = "USynSet";
            this.Size = new System.Drawing.Size(405, 460);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.Panel_Test.ResumeLayout(false);
            this.Panel_Test.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_TypeChange)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Pic_TypeChange;
        private System.Windows.Forms.Label Label_SynType;
        private System.Windows.Forms.ComboBox Txt_DataSource;
        private System.Windows.Forms.ComboBox Txt_UserID;
        private System.Windows.Forms.ComboBox Txt_InitialCatalog;
        private System.Windows.Forms.TextBox Txt_Password;
        private System.Windows.Forms.Label label5;
        private UChange uChange1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel Panel_Test;
        private System.Windows.Forms.Label Label_Test;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        protected internal System.Windows.Forms.Label Label_TypeText;
        private System.Windows.Forms.TextBox textBox1;
    }
}
