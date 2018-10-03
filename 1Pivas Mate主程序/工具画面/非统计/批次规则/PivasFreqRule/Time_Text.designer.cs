namespace PivasFreqRule
{
    partial class Time_Text
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Time_Text));
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel_head = new System.Windows.Forms.Panel();
            this.Panel_Close = new System.Windows.Forms.Panel();
            this.label_标题 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel_head.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(107)))), ((int)(((byte)(225)))));
            this.panel3.Location = new System.Drawing.Point(1, 338);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(292, 2);
            this.panel3.TabIndex = 16;
            // 
            // panel_head
            // 
            this.panel_head.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(107)))), ((int)(((byte)(225)))));
            this.panel_head.Controls.Add(this.Panel_Close);
            this.panel_head.Controls.Add(this.label_标题);
            this.panel_head.Location = new System.Drawing.Point(-1, 0);
            this.panel_head.Name = "panel_head";
            this.panel_head.Size = new System.Drawing.Size(294, 32);
            this.panel_head.TabIndex = 15;
            this.panel_head.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_head_MouseDown);
            // 
            // Panel_Close
            // 
            this.Panel_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Close.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel_Close.BackgroundImage")));
            this.Panel_Close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Panel_Close.Location = new System.Drawing.Point(272, 1);
            this.Panel_Close.Name = "Panel_Close";
            this.Panel_Close.Size = new System.Drawing.Size(20, 26);
            this.Panel_Close.TabIndex = 15;
            this.Panel_Close.Click += new System.EventHandler(this.Panel_Close_Click);
            // 
            // label_标题
            // 
            this.label_标题.ForeColor = System.Drawing.Color.White;
            this.label_标题.Location = new System.Drawing.Point(3, 3);
            this.label_标题.Name = "label_标题";
            this.label_标题.Size = new System.Drawing.Size(100, 29);
            this.label_标题.TabIndex = 0;
            this.label_标题.Text = "更改时间";
            this.label_标题.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(105, 303);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Location = new System.Drawing.Point(145, 36);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(138, 260);
            this.panel2.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(1, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(138, 260);
            this.panel1.TabIndex = 12;
            // 
            // Time_Text
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(292, 340);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel_head);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Time_Text";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Time_Text";
            this.Load += new System.EventHandler(this.Time_Text_Load);
            this.panel_head.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel_head;
        private System.Windows.Forms.Panel Panel_Close;
        private System.Windows.Forms.Label label_标题;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
    }
}