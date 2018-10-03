namespace PivasDDrug
{
    partial class SetDrugTiter
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
            this.Txt_Major = new System.Windows.Forms.TextBox();
            this.Cb_MajorUnit = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Txt_MaxValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Txt_MinValue = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Cb_LargeLevel = new System.Windows.Forms.ComboBox();
            this.Cb_LessLevel = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Txt_LargeResult = new System.Windows.Forms.TextBox();
            this.Txt_LessResult = new System.Windows.Forms.TextBox();
            this.Btn_YES = new System.Windows.Forms.Button();
            this.Btn_Close = new System.Windows.Forms.Button();
            this.Btn_Clear = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "浓度指标";
            // 
            // Txt_Major
            // 
            this.Txt_Major.Location = new System.Drawing.Point(77, 39);
            this.Txt_Major.Name = "Txt_Major";
            this.Txt_Major.Size = new System.Drawing.Size(58, 21);
            this.Txt_Major.TabIndex = 1;
            // 
            // Cb_MajorUnit
            // 
            this.Cb_MajorUnit.FormattingEnabled = true;
            this.Cb_MajorUnit.Items.AddRange(new object[] {
            "g",
            "ug",
            "mg"});
            this.Cb_MajorUnit.Location = new System.Drawing.Point(141, 39);
            this.Cb_MajorUnit.Name = "Cb_MajorUnit";
            this.Cb_MajorUnit.Size = new System.Drawing.Size(55, 20);
            this.Cb_MajorUnit.TabIndex = 2;
            this.Cb_MajorUnit.Text = "g";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Txt_MaxValue);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Txt_MinValue);
            this.groupBox1.Location = new System.Drawing.Point(14, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "浓度范围";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(177, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "mg/ml";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "到";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(177, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "mg/ml";
            // 
            // Txt_MaxValue
            // 
            this.Txt_MaxValue.Location = new System.Drawing.Point(59, 49);
            this.Txt_MaxValue.Name = "Txt_MaxValue";
            this.Txt_MaxValue.Size = new System.Drawing.Size(100, 21);
            this.Txt_MaxValue.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "从";
            // 
            // Txt_MinValue
            // 
            this.Txt_MinValue.Location = new System.Drawing.Point(59, 20);
            this.Txt_MinValue.Name = "Txt_MinValue";
            this.Txt_MinValue.Size = new System.Drawing.Size(100, 21);
            this.Txt_MinValue.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.Cb_LargeLevel);
            this.groupBox2.Controls.Add(this.Cb_LessLevel);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(14, 168);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(362, 94);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "严重程度";
            // 
            // Cb_LargeLevel
            // 
            this.Cb_LargeLevel.FormattingEnabled = true;
            this.Cb_LargeLevel.Items.AddRange(new object[] {
            "★",
            "★★",
            "★★★",
            "★★★★",
            "★★★★★"});
            this.Cb_LargeLevel.Location = new System.Drawing.Point(189, 65);
            this.Cb_LargeLevel.Name = "Cb_LargeLevel";
            this.Cb_LargeLevel.Size = new System.Drawing.Size(120, 20);
            this.Cb_LargeLevel.TabIndex = 2;
            this.Cb_LargeLevel.Text = "★★★";
            // 
            // Cb_LessLevel
            // 
            this.Cb_LessLevel.FormattingEnabled = true;
            this.Cb_LessLevel.Items.AddRange(new object[] {
            "★",
            "★★",
            "★★★",
            "★★★★",
            "★★★★★"});
            this.Cb_LessLevel.Location = new System.Drawing.Point(189, 33);
            this.Cb_LessLevel.Name = "Cb_LessLevel";
            this.Cb_LessLevel.Size = new System.Drawing.Size(120, 20);
            this.Cb_LessLevel.TabIndex = 2;
            this.Cb_LessLevel.Text = "★★★";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(149, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "高于最大浓度值时严重程度";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "低于最小浓度值时严重程度";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.Txt_LargeResult);
            this.groupBox3.Controls.Add(this.Txt_LessResult);
            this.groupBox3.Location = new System.Drawing.Point(14, 268);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(362, 127);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "结果(限制128字)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(209, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "当浓度值[高于]最大浓度指标时会产生";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(209, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "当浓度值[低于]最小浓度指标时会产生";
            // 
            // Txt_LargeResult
            // 
            this.Txt_LargeResult.Location = new System.Drawing.Point(15, 86);
            this.Txt_LargeResult.MaxLength = 256;
            this.Txt_LargeResult.Multiline = true;
            this.Txt_LargeResult.Name = "Txt_LargeResult";
            this.Txt_LargeResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Txt_LargeResult.Size = new System.Drawing.Size(341, 35);
            this.Txt_LargeResult.TabIndex = 0;
            // 
            // Txt_LessResult
            // 
            this.Txt_LessResult.Location = new System.Drawing.Point(15, 32);
            this.Txt_LessResult.MaxLength = 256;
            this.Txt_LessResult.Multiline = true;
            this.Txt_LessResult.Name = "Txt_LessResult";
            this.Txt_LessResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Txt_LessResult.Size = new System.Drawing.Size(341, 35);
            this.Txt_LessResult.TabIndex = 0;
            // 
            // Btn_YES
            // 
            this.Btn_YES.Location = new System.Drawing.Point(301, 54);
            this.Btn_YES.Name = "Btn_YES";
            this.Btn_YES.Size = new System.Drawing.Size(75, 23);
            this.Btn_YES.TabIndex = 4;
            this.Btn_YES.Text = "确定";
            this.Btn_YES.UseVisualStyleBackColor = true;
            this.Btn_YES.Click += new System.EventHandler(this.Btn_YES_Click);
            // 
            // Btn_Close
            // 
            this.Btn_Close.Location = new System.Drawing.Point(301, 85);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(75, 23);
            this.Btn_Close.TabIndex = 4;
            this.Btn_Close.Text = "取消";
            this.Btn_Close.UseVisualStyleBackColor = true;
            // 
            // Btn_Clear
            // 
            this.Btn_Clear.Location = new System.Drawing.Point(301, 118);
            this.Btn_Clear.Name = "Btn_Clear";
            this.Btn_Clear.Size = new System.Drawing.Size(75, 23);
            this.Btn_Clear.TabIndex = 4;
            this.Btn_Clear.Text = "清除";
            this.Btn_Clear.UseVisualStyleBackColor = true;
            this.Btn_Clear.Click += new System.EventHandler(this.Btn_Clear_Click);
            // 
            // SetDrugTiter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::PivasDDrug.Properties.Resources.背景;
            this.ClientSize = new System.Drawing.Size(388, 407);
            this.Controls.Add(this.Btn_Clear);
            this.Controls.Add(this.Btn_Close);
            this.Controls.Add(this.Btn_YES);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Cb_MajorUnit);
            this.Controls.Add(this.Txt_Major);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SetDrugTiter";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SetDrugTiter";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Txt_Major;
        private System.Windows.Forms.ComboBox Cb_MajorUnit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Txt_MinValue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Txt_MaxValue;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox Cb_LargeLevel;
        private System.Windows.Forms.ComboBox Cb_LessLevel;
        private System.Windows.Forms.TextBox Txt_LessResult;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox Txt_LargeResult;
        private System.Windows.Forms.Button Btn_YES;
        private System.Windows.Forms.Button Btn_Close;
        private System.Windows.Forms.Button Btn_Clear;
    }
}