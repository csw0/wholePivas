namespace PivasNurse
{
    partial class DoctorOpinion
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
            this.rBRun = new System.Windows.Forms.RadioButton();
            this.rBAccept = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.txtDoc = new System.Windows.Forms.TextBox();
            this.txtPas = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rBRun
            // 
            this.rBRun.AutoSize = true;
            this.rBRun.Checked = true;
            this.rBRun.Location = new System.Drawing.Point(40, 55);
            this.rBRun.Name = "rBRun";
            this.rBRun.Size = new System.Drawing.Size(71, 16);
            this.rBRun.TabIndex = 0;
            this.rBRun.TabStop = true;
            this.rBRun.Text = "强制执行";
            this.rBRun.UseVisualStyleBackColor = true;
            this.rBRun.CheckedChanged += new System.EventHandler(this.rBRun_CheckedChanged);
            // 
            // rBAccept
            // 
            this.rBAccept.AutoSize = true;
            this.rBAccept.Location = new System.Drawing.Point(40, 33);
            this.rBAccept.Name = "rBAccept";
            this.rBAccept.Size = new System.Drawing.Size(71, 16);
            this.rBAccept.TabIndex = 1;
            this.rBAccept.Text = "接受退单";
            this.rBAccept.UseVisualStyleBackColor = true;
            this.rBAccept.Click += new System.EventHandler(this.rBAccept_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "说明：";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "执行"});
            this.comboBox1.Location = new System.Drawing.Point(125, 88);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(119, 20);
            this.comboBox1.TabIndex = 3;
            // 
            // txtDoc
            // 
            this.txtDoc.Location = new System.Drawing.Point(125, 132);
            this.txtDoc.Name = "txtDoc";
            this.txtDoc.Size = new System.Drawing.Size(119, 21);
            this.txtDoc.TabIndex = 4;
            // 
            // txtPas
            // 
            this.txtPas.Location = new System.Drawing.Point(125, 162);
            this.txtPas.Name = "txtPas";
            this.txtPas.Size = new System.Drawing.Size(119, 21);
            this.txtPas.TabIndex = 5;
            this.txtPas.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "医师工号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "密码：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(81, 222);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(183, 222);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(179)))), ((int)(((byte)(253)))));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 20);
            this.panel1.TabIndex = 10;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(179)))), ((int)(((byte)(253)))));
            this.panel2.Location = new System.Drawing.Point(-1, 257);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(324, 6);
            this.panel2.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(10, 202);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(310, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "若提示‘医师工号或密码错误’请联系配置中心管理员。";
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(136, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(184, 38);
            this.label5.TabIndex = 13;
            this.label5.Text = "请医师参考审方结果并根据病人实际情况决定此医嘱的执行方式";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Gray;
            this.label6.ForeColor = System.Drawing.Color.Silver;
            this.label6.Location = new System.Drawing.Point(27, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(259, 1);
            this.label6.TabIndex = 14;
            this.label6.Text = "label6";
            // 
            // DoctorOpinion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 262);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPas);
            this.Controls.Add(this.txtDoc);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rBAccept);
            this.Controls.Add(this.rBRun);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DoctorOpinion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DoctorOpinion_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rBRun;
        private System.Windows.Forms.RadioButton rBAccept;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox txtDoc;
        private System.Windows.Forms.TextBox txtPas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}