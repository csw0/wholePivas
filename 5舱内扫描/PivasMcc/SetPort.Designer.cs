namespace PivasMcc
{
    partial class SetPort
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtDeskNo = new System.Windows.Forms.TextBox();
            this.txtDeskDes = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.PLC = new System.Windows.Forms.TextBox();
            this.Red = new System.Windows.Forms.TextBox();
            this.Green = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Screen = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "端口号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "配置台号：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "位置描述：";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(100, 12);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(132, 21);
            this.txtPort.TabIndex = 0;
            // 
            // txtDeskNo
            // 
            this.txtDeskNo.Location = new System.Drawing.Point(100, 39);
            this.txtDeskNo.Name = "txtDeskNo";
            this.txtDeskNo.Size = new System.Drawing.Size(130, 21);
            this.txtDeskNo.TabIndex = 1;
            // 
            // txtDeskDes
            // 
            this.txtDeskDes.Location = new System.Drawing.Point(100, 66);
            this.txtDeskDes.Name = "txtDeskDes";
            this.txtDeskDes.Size = new System.Drawing.Size(130, 21);
            this.txtDeskDes.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(34, 207);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(157, 207);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "PLC地址：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "红灯地址：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "绿灯地址：";
            // 
            // PLC
            // 
            this.PLC.Location = new System.Drawing.Point(100, 97);
            this.PLC.Name = "PLC";
            this.PLC.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.PLC.Size = new System.Drawing.Size(129, 21);
            this.PLC.TabIndex = 3;
            this.PLC.TextChanged += new System.EventHandler(this.PLC_TextChanged);
            // 
            // Red
            // 
            this.Red.Location = new System.Drawing.Point(100, 124);
            this.Red.Name = "Red";
            this.Red.Size = new System.Drawing.Size(129, 21);
            this.Red.TabIndex = 4;
            this.Red.TextChanged += new System.EventHandler(this.Red_TextChanged);
            // 
            // Green
            // 
            this.Green.Location = new System.Drawing.Point(100, 151);
            this.Green.Name = "Green";
            this.Green.Size = new System.Drawing.Size(129, 21);
            this.Green.TabIndex = 5;
            this.Green.TextChanged += new System.EventHandler(this.Green_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 178);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "显示屏IP：";
            // 
            // Screen
            // 
            this.Screen.Location = new System.Drawing.Point(100, 178);
            this.Screen.Name = "Screen";
            this.Screen.Size = new System.Drawing.Size(129, 21);
            this.Screen.TabIndex = 15;
            // 
            // SetPort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 246);
            this.Controls.Add(this.Screen);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Green);
            this.Controls.Add(this.Red);
            this.Controls.Add(this.PLC);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtDeskDes);
            this.Controls.Add(this.txtDeskNo);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "SetPort";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "端口设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtDeskNo;
        private System.Windows.Forms.TextBox txtDeskDes;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox PLC;
        private System.Windows.Forms.TextBox Red;
        private System.Windows.Forms.TextBox Green;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox Screen;
    }
}