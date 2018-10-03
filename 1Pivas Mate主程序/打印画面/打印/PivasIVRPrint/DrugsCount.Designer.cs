using FastReport.Preview;
using FastReport.Utils;
namespace PivasIVRPrint
{
    partial class DrugsCount
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.previewControlFR = new PreviewControl();
            this.previewControl1 = new PreviewControl();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 600);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.previewControlFR);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 574);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "所有病区汇总";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.previewControl1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 574);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "单个病区汇总";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // previewControlFR
            // 
            this.previewControlFR.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.previewControlFR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewControlFR.Font = new System.Drawing.Font("宋体", 9F);
            this.previewControlFR.Location = new System.Drawing.Point(3, 3);
            this.previewControlFR.Margin = new System.Windows.Forms.Padding(0);
            this.previewControlFR.Name = "previewControlFR";
            this.previewControlFR.Size = new System.Drawing.Size(786, 568);
            this.previewControlFR.TabIndex = 2;
            this.previewControlFR.UIStyle = UIStyle.Office2007Black;
            // 
            // previewControl1
            // 
            this.previewControl1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.previewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewControl1.Font = new System.Drawing.Font("宋体", 9F);
            this.previewControl1.Location = new System.Drawing.Point(3, 3);
            this.previewControl1.Margin = new System.Windows.Forms.Padding(0);
            this.previewControl1.Name = "previewControl1";
            this.previewControl1.Size = new System.Drawing.Size(786, 568);
            this.previewControl1.TabIndex = 3;
            this.previewControl1.UIStyle = UIStyle.Office2007Black;
            // 
            // DrugsCount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "DrugsCount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "统药单";
            this.Load += new System.EventHandler(this.DrugsCount_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private PreviewControl previewControlFR;
        private System.Windows.Forms.TabPage tabPage2;
        private PreviewControl previewControl1;
    }
}