namespace PivasLabelSelect
{
    partial class Form2
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
            this.panel5 = new System.Windows.Forms.Panel();
            this.cbbList = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Button();
            this.Lb_Print = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cbbPrinted = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.Control;
            this.panel5.Controls.Add(this.cbbList);
            this.panel5.Controls.Add(this.label20);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.flowLayoutPanel1);
            this.panel5.Controls.Add(this.cbbPrinted);
            this.panel5.Controls.Add(this.label19);
            this.panel5.Controls.Add(this.checkBox2);
            this.panel5.Controls.Add(this.comboBox2);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.comboBox1);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.dateTimePicker1);
            this.panel5.Location = new System.Drawing.Point(0, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(763, 61);
            this.panel5.TabIndex = 1016;
            // 
            // cbbList
            // 
            this.cbbList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbList.FormattingEnabled = true;
            this.cbbList.Items.AddRange(new object[] {
            "<全部>",
            "已打印",
            "未打印"});
            this.cbbList.Location = new System.Drawing.Point(70, 34);
            this.cbbList.Name = "cbbList";
            this.cbbList.Size = new System.Drawing.Size(89, 20);
            this.cbbList.TabIndex = 1046;
            this.cbbList.Visible = false;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(14, 37);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(40, 16);
            this.label20.TabIndex = 1045;
            this.label20.Text = "清单:";
            this.label20.Visible = false;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Silver;
            this.panel6.Controls.Add(this.label1);
            this.panel6.Controls.Add(this.comboBox3);
            this.panel6.Controls.Add(this.checkBox1);
            this.panel6.Location = new System.Drawing.Point(175, 2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(204, 24);
            this.panel6.TabIndex = 1044;
            this.panel6.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Location = new System.Drawing.Point(5, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1033;
            this.label1.Text = "状态：";
            // 
            // comboBox3
            // 
            this.comboBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.ItemHeight = 12;
            this.comboBox3.Items.AddRange(new object[] {
            "<全部>",
            "未打印",
            "已打印",
            "已排药",
            "已进仓",
            "已配置",
            "已出仓",
            "已打包",
            "已退药",
            "已签收",
            "配置取消",
            "提前打包"});
            this.comboBox3.Location = new System.Drawing.Point(47, 2);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(72, 20);
            this.comboBox3.TabIndex = 1034;
            this.comboBox3.Text = "<全部>";
            // 
            // checkBox1
            // 
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(127, 4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 1038;
            this.checkBox1.Text = "仅此状态";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.label5);
            this.flowLayoutPanel1.Controls.Add(this.Lb_Print);
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(165, 28);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(300, 30);
            this.flowLayoutPanel1.TabIndex = 1043;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(179)))), ((int)(((byte)(253)))));
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 26);
            this.label2.TabIndex = 1035;
            this.label2.Text = "核对";
            this.label2.UseVisualStyleBackColor = false;
            this.label2.Visible = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(75, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 26);
            this.label5.TabIndex = 1036;
            this.label5.Text = "退药";
            this.label5.UseVisualStyleBackColor = false;
            // 
            // Lb_Print
            // 
            this.Lb_Print.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(179)))), ((int)(((byte)(253)))));
            this.Lb_Print.Location = new System.Drawing.Point(147, 3);
            this.Lb_Print.Name = "Lb_Print";
            this.Lb_Print.Size = new System.Drawing.Size(66, 26);
            this.Lb_Print.TabIndex = 1037;
            this.Lb_Print.Text = "打印";
            this.Lb_Print.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(179)))), ((int)(((byte)(253)))));
            this.button1.Location = new System.Drawing.Point(219, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 26);
            this.button1.TabIndex = 1039;
            this.button1.Text = "汇总";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            // 
            // cbbPrinted
            // 
            this.cbbPrinted.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPrinted.FormattingEnabled = true;
            this.cbbPrinted.Items.AddRange(new object[] {
            "<全部>",
            "已打印",
            "未打印"});
            this.cbbPrinted.Location = new System.Drawing.Point(677, 5);
            this.cbbPrinted.Name = "cbbPrinted";
            this.cbbPrinted.Size = new System.Drawing.Size(64, 20);
            this.cbbPrinted.TabIndex = 1042;
            this.cbbPrinted.Visible = false;
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(628, 9);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(38, 13);
            this.label19.TabIndex = 1041;
            this.label19.Text = "瓶签:";
            this.label19.Visible = false;
            // 
            // checkBox2
            // 
            this.checkBox2.Location = new System.Drawing.Point(13, 7);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(51, 18);
            this.checkBox2.TabIndex = 1040;
            this.checkBox2.Text = "全选";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "",
            "K",
            "#"});
            this.comboBox2.Location = new System.Drawing.Point(550, 4);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(72, 20);
            this.comboBox2.TabIndex = 1032;
            this.comboBox2.Visible = false;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(515, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 1031;
            this.label4.Text = "筛选:";
            this.label4.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "",
            "<临时>"});
            this.comboBox1.Location = new System.Drawing.Point(433, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(72, 20);
            this.comboBox1.TabIndex = 1030;
            this.comboBox1.Visible = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(394, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 1029;
            this.label3.Text = "批次:";
            this.label3.Visible = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(70, 6);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(89, 21);
            this.dateTimePicker1.TabIndex = 1028;
            this.dateTimePicker1.Visible = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 66);
            this.Controls.Add(this.panel5);
            this.Name = "Form2";
            this.Text = "Form2";
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ComboBox cbbList;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button label2;
        private System.Windows.Forms.Button label5;
        private System.Windows.Forms.Button Lb_Print;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbbPrinted;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}