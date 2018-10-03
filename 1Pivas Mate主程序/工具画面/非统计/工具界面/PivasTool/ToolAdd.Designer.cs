namespace PivasTool
{
    partial class ToolAdd
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Txt_Name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Txt_ImgName = new System.Windows.Forms.TextBox();
            this.Txt_Path = new System.Windows.Forms.TextBox();
            this.Label_AddImgName = new System.Windows.Forms.Label();
            this.Label_AddPath = new System.Windows.Forms.Label();
            this.Comb_MaxCategories = new System.Windows.Forms.ComboBox();
            this.Comb_MinCategories = new System.Windows.Forms.ComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Panel_YES = new System.Windows.Forms.Panel();
            this.Label_Yes = new System.Windows.Forms.Label();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Txt_Version = new System.Windows.Forms.TextBox();
            this.Panel_YES.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(117, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "添加工具";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F);
            this.label2.Location = new System.Drawing.Point(18, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "工具名称";
            // 
            // Txt_Name
            // 
            this.Txt_Name.Location = new System.Drawing.Point(86, 60);
            this.Txt_Name.Name = "Txt_Name";
            this.Txt_Name.Size = new System.Drawing.Size(178, 21);
            this.Txt_Name.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F);
            this.label3.Location = new System.Drawing.Point(18, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 1;
            this.label3.Text = "工具版本";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10F);
            this.label4.Location = new System.Drawing.Point(18, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 14);
            this.label4.TabIndex = 1;
            this.label4.Text = "所属类别";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10F);
            this.label5.Location = new System.Drawing.Point(18, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 14);
            this.label5.TabIndex = 1;
            this.label5.Text = "工具图片";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10F);
            this.label6.Location = new System.Drawing.Point(18, 189);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 14);
            this.label6.TabIndex = 1;
            this.label6.Text = "选择工具";
            // 
            // Txt_ImgName
            // 
            this.Txt_ImgName.BackColor = System.Drawing.Color.Snow;
            this.Txt_ImgName.Location = new System.Drawing.Point(86, 153);
            this.Txt_ImgName.Name = "Txt_ImgName";
            this.Txt_ImgName.ReadOnly = true;
            this.Txt_ImgName.Size = new System.Drawing.Size(159, 21);
            this.Txt_ImgName.TabIndex = 2;
            // 
            // Txt_Path
            // 
            this.Txt_Path.BackColor = System.Drawing.Color.Snow;
            this.Txt_Path.Location = new System.Drawing.Point(86, 187);
            this.Txt_Path.Name = "Txt_Path";
            this.Txt_Path.ReadOnly = true;
            this.Txt_Path.Size = new System.Drawing.Size(159, 21);
            this.Txt_Path.TabIndex = 2;
            // 
            // Label_AddImgName
            // 
            this.Label_AddImgName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label_AddImgName.Location = new System.Drawing.Point(221, 153);
            this.Label_AddImgName.Name = "Label_AddImgName";
            this.Label_AddImgName.Size = new System.Drawing.Size(24, 21);
            this.Label_AddImgName.TabIndex = 4;
            this.Label_AddImgName.Text = "…";
            this.Label_AddImgName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_AddImgName.Click += new System.EventHandler(this.Label_AddImgName_Click);
            // 
            // Label_AddPath
            // 
            this.Label_AddPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label_AddPath.Location = new System.Drawing.Point(221, 187);
            this.Label_AddPath.Name = "Label_AddPath";
            this.Label_AddPath.Size = new System.Drawing.Size(24, 21);
            this.Label_AddPath.TabIndex = 5;
            this.Label_AddPath.Text = "…";
            this.Label_AddPath.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_AddPath.Click += new System.EventHandler(this.Label_AddPath_Click);
            // 
            // Comb_MaxCategories
            // 
            this.Comb_MaxCategories.FormattingEnabled = true;
            this.Comb_MaxCategories.Items.AddRange(new object[] {
            "基础数据维护",
            "系统运行日志"});
            this.Comb_MaxCategories.Location = new System.Drawing.Point(86, 121);
            this.Comb_MaxCategories.Name = "Comb_MaxCategories";
            this.Comb_MaxCategories.Size = new System.Drawing.Size(91, 20);
            this.Comb_MaxCategories.TabIndex = 2;
            // 
            // Comb_MinCategories
            // 
            this.Comb_MinCategories.FormattingEnabled = true;
            this.Comb_MinCategories.Items.AddRange(new object[] {
            "同步日志",
            "操作日志"});
            this.Comb_MinCategories.Location = new System.Drawing.Point(183, 121);
            this.Comb_MinCategories.Name = "Comb_MinCategories";
            this.Comb_MinCategories.Size = new System.Drawing.Size(81, 20);
            this.Comb_MinCategories.TabIndex = 3;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::PivasTool.Properties.Resources.关闭;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel1.Location = new System.Drawing.Point(279, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(30, 18);
            this.panel1.TabIndex = 6;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // Panel_YES
            // 
            this.Panel_YES.BackColor = System.Drawing.Color.Transparent;
            this.Panel_YES.BackgroundImage = global::PivasTool.Properties.Resources._4;
            this.Panel_YES.Controls.Add(this.Label_Yes);
            this.Panel_YES.Location = new System.Drawing.Point(183, 214);
            this.Panel_YES.Name = "Panel_YES";
            this.Panel_YES.Size = new System.Drawing.Size(61, 24);
            this.Panel_YES.TabIndex = 4;
            this.Panel_YES.Click += new System.EventHandler(this.Panel_YES_Click);
            // 
            // Label_Yes
            // 
            this.Label_Yes.AutoSize = true;
            this.Label_Yes.Font = new System.Drawing.Font("宋体", 10F);
            this.Label_Yes.Location = new System.Drawing.Point(13, 5);
            this.Label_Yes.Name = "Label_Yes";
            this.Label_Yes.Size = new System.Drawing.Size(35, 14);
            this.Label_Yes.TabIndex = 0;
            this.Label_Yes.Text = "确定";
            this.Label_Yes.Click += new System.EventHandler(this.Panel_YES_Click);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // label7
            // 
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(251, 144);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 42);
            this.label7.TabIndex = 7;
            this.label7.Text = "图片在40,48以下";
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(253, 186);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 36);
            this.label8.TabIndex = 7;
            this.label8.Text = "工具必须在当前目录下";
            // 
            // Txt_Version
            // 
            this.Txt_Version.Location = new System.Drawing.Point(86, 90);
            this.Txt_Version.Name = "Txt_Version";
            this.Txt_Version.Size = new System.Drawing.Size(178, 21);
            this.Txt_Version.TabIndex = 1;
            this.Txt_Version.Text = "1.0";
            // 
            // ToolAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(310, 253);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Comb_MinCategories);
            this.Controls.Add(this.Comb_MaxCategories);
            this.Controls.Add(this.Panel_YES);
            this.Controls.Add(this.Label_AddPath);
            this.Controls.Add(this.Label_AddImgName);
            this.Controls.Add(this.Txt_Path);
            this.Controls.Add(this.Txt_ImgName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Txt_Version);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Txt_Name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ToolAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ToolAdd";
            this.Load += new System.EventHandler(this.ToolAdd_Load);
            this.Panel_YES.ResumeLayout(false);
            this.Panel_YES.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label Label_AddImgName;
        private System.Windows.Forms.Label Label_AddPath;
        private System.Windows.Forms.Label Label_Yes;
        private System.Windows.Forms.Panel Panel_YES;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        protected internal System.Windows.Forms.TextBox Txt_Name;
        protected internal System.Windows.Forms.TextBox Txt_ImgName;
        protected internal System.Windows.Forms.TextBox Txt_Path;
        protected internal System.Windows.Forms.ComboBox Comb_MaxCategories;
        protected internal System.Windows.Forms.ComboBox Comb_MinCategories;
        protected internal System.Windows.Forms.TextBox Txt_Version;
    }
}