namespace PivasLabelSelect
{
    partial class PrintPreviews
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
            this.previewControlFR = new FastReport.Preview.PreviewControl();
            this.SuspendLayout();
            // 
            // previewControlFR
            // 
            this.previewControlFR.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.previewControlFR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewControlFR.Font = new System.Drawing.Font("宋体", 9F);
            this.previewControlFR.Location = new System.Drawing.Point(0, 0);
            this.previewControlFR.Margin = new System.Windows.Forms.Padding(0);
            this.previewControlFR.Name = "previewControlFR";
            this.previewControlFR.PageOffset = new System.Drawing.Point(10, 10);
            this.previewControlFR.Size = new System.Drawing.Size(640, 486);
            this.previewControlFR.TabIndex = 4;
            this.previewControlFR.UIStyle = FastReport.Utils.UIStyle.Office2007Black;
            // 
            // PrintPreviews
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 486);
            this.Controls.Add(this.previewControlFR);
            this.Name = "PrintPreviews";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打印预览";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PrintPreviews_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private FastReport.Preview.PreviewControl previewControlFR;



    }
}