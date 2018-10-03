namespace PIVAS_MATE
{
    partial class frmNotify
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNotify));
            this.label1 = new System.Windows.Forms.Label();
            this.lblPass = new System.Windows.Forms.Label();
            this.lblUnPass = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.cbbKeeptime = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbbtime = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblToday = new System.Windows.Forms.Label();
            this.lblnextday = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblUncheck = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblTodaypack = new System.Windows.Forms.Label();
            this.lblnextDaypack = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblTodayback = new System.Windows.Forms.Label();
            this.lblNextdayback = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.Panel_Close = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "审方——";
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point(146, 27);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(11, 12);
            this.lblPass.TabIndex = 2;
            this.lblPass.Text = "0";
            // 
            // lblUnPass
            // 
            this.lblUnPass.AutoSize = true;
            this.lblUnPass.Location = new System.Drawing.Point(146, 44);
            this.lblUnPass.Name = "lblUnPass";
            this.lblUnPass.Size = new System.Drawing.Size(11, 12);
            this.lblUnPass.TabIndex = 3;
            this.lblUnPass.Text = "0";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Controls.Add(this.comboBox3);
            this.panel1.Controls.Add(this.cbbKeeptime);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cbbtime);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(2, 187);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 25);
            this.panel1.TabIndex = 4;
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "30分钟内不提醒",
            "一小时内不提醒",
            "恢复提醒"});
            this.comboBox3.Location = new System.Drawing.Point(197, 3);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(101, 20);
            this.comboBox3.TabIndex = 8;
            this.comboBox3.MouseHover += new System.EventHandler(this.frmNotify_MouseHover);
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            this.comboBox3.MouseLeave += new System.EventHandler(this.frmNotify_MouseLeave);
            // 
            // cbbKeeptime
            // 
            this.cbbKeeptime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbKeeptime.FormattingEnabled = true;
            this.cbbKeeptime.Items.AddRange(new object[] {
            "5",
            "10",
            "15"});
            this.cbbKeeptime.Location = new System.Drawing.Point(135, 3);
            this.cbbKeeptime.Name = "cbbKeeptime";
            this.cbbKeeptime.Size = new System.Drawing.Size(35, 20);
            this.cbbKeeptime.TabIndex = 6;
            this.cbbKeeptime.MouseHover += new System.EventHandler(this.frmNotify_MouseHover);
            this.cbbKeeptime.SelectedIndexChanged += new System.EventHandler(this.cbbtime_SelectedIndexChanged);
            this.cbbKeeptime.MouseLeave += new System.EventHandler(this.frmNotify_MouseLeave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Gray;
            this.label6.Location = new System.Drawing.Point(170, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "秒";
            this.label6.MouseLeave += new System.EventHandler(this.frmNotify_MouseLeave);
            this.label6.MouseHover += new System.EventHandler(this.frmNotify_MouseHover);
            // 
            // cbbtime
            // 
            this.cbbtime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbtime.FormattingEnabled = true;
            this.cbbtime.Items.AddRange(new object[] {
            "1",
            "3",
            "5",
            "10"});
            this.cbbtime.Location = new System.Drawing.Point(27, 3);
            this.cbbtime.Name = "cbbtime";
            this.cbbtime.Size = new System.Drawing.Size(35, 20);
            this.cbbtime.TabIndex = 2;
            this.cbbtime.MouseHover += new System.EventHandler(this.frmNotify_MouseHover);
            this.cbbtime.SelectedIndexChanged += new System.EventHandler(this.cbbtime_SelectedIndexChanged);
            this.cbbtime.MouseLeave += new System.EventHandler(this.frmNotify_MouseLeave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(60, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "分弹出";
            this.label4.MouseLeave += new System.EventHandler(this.frmNotify_MouseLeave);
            this.label4.MouseHover += new System.EventHandler(this.frmNotify_MouseHover);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(0, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "间隔";
            this.label3.MouseLeave += new System.EventHandler(this.frmNotify_MouseLeave);
            this.label3.MouseHover += new System.EventHandler(this.frmNotify_MouseHover);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(109, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "停留";
            this.label5.MouseLeave += new System.EventHandler(this.frmNotify_MouseLeave);
            this.label5.MouseHover += new System.EventHandler(this.frmNotify_MouseHover);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "通过：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(90, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "未过：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "瓶签——";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel4.Location = new System.Drawing.Point(15, 58);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(273, 1);
            this.panel4.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(90, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 9;
            this.label9.Text = "今日：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(90, 77);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 10;
            this.label10.Text = "明日：";
            // 
            // lblToday
            // 
            this.lblToday.AutoSize = true;
            this.lblToday.Location = new System.Drawing.Point(146, 62);
            this.lblToday.Name = "lblToday";
            this.lblToday.Size = new System.Drawing.Size(11, 12);
            this.lblToday.TabIndex = 11;
            this.lblToday.Text = "0";
            // 
            // lblnextday
            // 
            this.lblnextday.AutoSize = true;
            this.lblnextday.Location = new System.Drawing.Point(146, 76);
            this.lblnextday.Name = "lblnextday";
            this.lblnextday.Size = new System.Drawing.Size(11, 12);
            this.lblnextday.TabIndex = 12;
            this.lblnextday.Text = "0";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel5.Location = new System.Drawing.Point(14, 92);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(273, 1);
            this.panel5.TabIndex = 13;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 98);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 14;
            this.label13.Text = "配置——";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(78, 98);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 15;
            this.label14.Text = "未核对：";
            // 
            // lblUncheck
            // 
            this.lblUncheck.AutoSize = true;
            this.lblUncheck.Location = new System.Drawing.Point(146, 98);
            this.lblUncheck.Name = "lblUncheck";
            this.lblUncheck.Size = new System.Drawing.Size(11, 12);
            this.lblUncheck.TabIndex = 16;
            this.lblUncheck.Text = "0";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel6.Location = new System.Drawing.Point(14, 115);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(273, 1);
            this.panel6.TabIndex = 17;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 120);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 18;
            this.label16.Text = "签收——";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(10, 155);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 12);
            this.label17.TabIndex = 19;
            this.label17.Text = "退单——";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel7.Location = new System.Drawing.Point(14, 151);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(273, 1);
            this.panel7.TabIndex = 20;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(90, 119);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 12);
            this.label18.TabIndex = 21;
            this.label18.Text = "今日：";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(90, 135);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(41, 12);
            this.label19.TabIndex = 22;
            this.label19.Text = "明日：";
            // 
            // lblTodaypack
            // 
            this.lblTodaypack.AutoSize = true;
            this.lblTodaypack.Location = new System.Drawing.Point(146, 120);
            this.lblTodaypack.Name = "lblTodaypack";
            this.lblTodaypack.Size = new System.Drawing.Size(11, 12);
            this.lblTodaypack.TabIndex = 23;
            this.lblTodaypack.Text = "0";
            // 
            // lblnextDaypack
            // 
            this.lblnextDaypack.AutoSize = true;
            this.lblnextDaypack.Location = new System.Drawing.Point(146, 136);
            this.lblnextDaypack.Name = "lblnextDaypack";
            this.lblnextDaypack.Size = new System.Drawing.Size(11, 12);
            this.lblnextDaypack.TabIndex = 24;
            this.lblnextDaypack.Text = "0";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(90, 171);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(41, 12);
            this.label22.TabIndex = 25;
            this.label22.Text = "明日：";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(90, 155);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(41, 12);
            this.label23.TabIndex = 26;
            this.label23.Text = "今日：";
            // 
            // lblTodayback
            // 
            this.lblTodayback.AutoSize = true;
            this.lblTodayback.Location = new System.Drawing.Point(146, 155);
            this.lblTodayback.Name = "lblTodayback";
            this.lblTodayback.Size = new System.Drawing.Size(11, 12);
            this.lblTodayback.TabIndex = 27;
            this.lblTodayback.Text = "0";
            // 
            // lblNextdayback
            // 
            this.lblNextdayback.AutoSize = true;
            this.lblNextdayback.Location = new System.Drawing.Point(146, 171);
            this.lblNextdayback.Name = "lblNextdayback";
            this.lblNextdayback.Size = new System.Drawing.Size(11, 12);
            this.lblNextdayback.TabIndex = 28;
            this.lblNextdayback.Text = "0";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(233)))));
            this.label11.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ForeColor = System.Drawing.Color.Gray;
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(304, 23);
            this.label11.TabIndex = 29;
            this.label11.Text = " PIVAS MATE";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label11.MouseLeave += new System.EventHandler(this.frmNotify_MouseLeave);
            this.label11.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label11_MouseDown);
            this.label11.MouseHover += new System.EventHandler(this.frmNotify_MouseHover);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(233)))));
            this.panel8.Location = new System.Drawing.Point(302, 2);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(10, 239);
            this.panel8.TabIndex = 30;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(233)))));
            this.panel9.Location = new System.Drawing.Point(-13, 12);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(16, 224);
            this.panel9.TabIndex = 31;
            // 
            // Panel_Close
            // 
            this.Panel_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(233)))));
            this.Panel_Close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel_Close.BackgroundImage")));
            this.Panel_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Panel_Close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Panel_Close.Location = new System.Drawing.Point(280, -1);
            this.Panel_Close.Name = "Panel_Close";
            this.Panel_Close.Size = new System.Drawing.Size(21, 19);
            this.Panel_Close.TabIndex = 33;
            this.Panel_Close.MouseLeave += new System.EventHandler(this.Panel_Close_MouseLeave);
            this.Panel_Close.Click += new System.EventHandler(this.label12_Click);
            this.Panel_Close.MouseHover += new System.EventHandler(this.Panel_Close_MouseHover);
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(233)))));
            this.panel10.Location = new System.Drawing.Point(-2, 210);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(314, 52);
            this.panel10.TabIndex = 31;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(220, 119);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 34;
            this.label12.Text = "提前：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(267, 119);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(11, 12);
            this.label15.TabIndex = 35;
            this.label15.Text = "0";
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(220, 98);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(41, 12);
            this.label20.TabIndex = 36;
            this.label20.Text = "取消：";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label21.ForeColor = System.Drawing.Color.Red;
            this.label21.Location = new System.Drawing.Point(267, 98);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(11, 12);
            this.label21.TabIndex = 37;
            this.label21.Text = "0";
            this.label21.Click += new System.EventHandler(this.label21_Click);
            // 
            // frmNotify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(305, 212);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.Panel_Close);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblNextdayback);
            this.Controls.Add(this.lblTodayback);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.lblnextDaypack);
            this.Controls.Add(this.lblTodaypack);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.lblUncheck);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.lblnextday);
            this.Controls.Add(this.lblToday);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblUnPass);
            this.Controls.Add(this.lblPass);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmNotify";
            this.Opacity = 0.8;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PIVAS MATE";
            this.MouseLeave += new System.EventHandler(this.frmNotify_MouseLeave);
            this.MouseHover += new System.EventHandler(this.frmNotify_MouseHover);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.Label lblUnPass;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbbtime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbKeeptime;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblToday;
        private System.Windows.Forms.Label lblnextday;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblUncheck;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblTodaypack;
        private System.Windows.Forms.Label lblnextDaypack;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lblTodayback;
        private System.Windows.Forms.Label lblNextdayback;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel Panel_Close;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;


    }
}