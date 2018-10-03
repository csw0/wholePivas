namespace synpivasdata
{
    partial class frmMain
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
            this.btnSynDrug = new System.Windows.Forms.Button();
            this.btnPatient = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSynDrug
            // 
            this.btnSynDrug.Location = new System.Drawing.Point(44, 30);
            this.btnSynDrug.Name = "btnSynDrug";
            this.btnSynDrug.Size = new System.Drawing.Size(101, 23);
            this.btnSynDrug.TabIndex = 0;
            this.btnSynDrug.Text = "药品同步";
            this.btnSynDrug.UseVisualStyleBackColor = true;
            this.btnSynDrug.Click += new System.EventHandler(this.btnSynDrug_Click);
            // 
            // btnPatient
            // 
            this.btnPatient.Location = new System.Drawing.Point(44, 78);
            this.btnPatient.Name = "btnPatient";
            this.btnPatient.Size = new System.Drawing.Size(101, 23);
            this.btnPatient.TabIndex = 1;
            this.btnPatient.Text = "患者信息同步";
            this.btnPatient.UseVisualStyleBackColor = true;
            this.btnPatient.Click += new System.EventHandler(this.btnPatient_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.Location = new System.Drawing.Point(12, 127);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(191, 63);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "label1";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(215, 199);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnPatient);
            this.Controls.Add(this.btnSynDrug);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PIVAS数据同步";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSynDrug;
        private System.Windows.Forms.Button btnPatient;
        private System.Windows.Forms.Label lblInfo;
    }
}