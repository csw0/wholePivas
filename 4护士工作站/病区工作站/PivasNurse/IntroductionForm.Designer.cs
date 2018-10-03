namespace PivasNurse
{
    partial class IntroductionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IntroductionForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.Panel_Close = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(179)))), ((int)(((byte)(253)))));
            this.panel1.Controls.Add(this.Panel_Close);
            this.panel1.Location = new System.Drawing.Point(-3, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(415, 26);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // Panel_Close
            // 
            this.Panel_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Close.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel_Close.BackgroundImage")));
            this.Panel_Close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Panel_Close.Location = new System.Drawing.Point(390, 1);
            this.Panel_Close.Name = "Panel_Close";
            this.Panel_Close.Size = new System.Drawing.Size(20, 20);
            this.Panel_Close.TabIndex = 8;
            this.Panel_Close.MouseLeave += new System.EventHandler(this.Panel_Close_MouseLeave);
            this.Panel_Close.Click += new System.EventHandler(this.Panel_Close_Click);
            this.Panel_Close.MouseHover += new System.EventHandler(this.Panel_Close_MouseHover);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(246)))));
            this.panel2.Location = new System.Drawing.Point(1, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(408, 251);
            this.panel2.TabIndex = 1;
            // 
            // IntroductionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(410, 277);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "IntroductionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Introduction";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel Panel_Close;
        private System.Windows.Forms.Panel panel2;
    }
}