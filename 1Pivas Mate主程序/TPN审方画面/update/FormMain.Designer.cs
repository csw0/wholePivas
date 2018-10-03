namespace update
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.prgProc = new System.Windows.Forms.ProgressBar();
            this.lblUpFile = new System.Windows.Forms.Label();
            this.lblDownLoadFile = new System.Windows.Forms.Label();
            this.lblDownLoadProc = new System.Windows.Forms.Label();
            this.lblUpProc = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lbFiles = new System.Windows.Forms.ListBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // prgProc
            // 
            this.prgProc.Location = new System.Drawing.Point(12, 32);
            this.prgProc.Name = "prgProc";
            this.prgProc.Size = new System.Drawing.Size(268, 16);
            this.prgProc.TabIndex = 0;
            // 
            // lblUpFile
            // 
            this.lblUpFile.AutoSize = true;
            this.lblUpFile.Location = new System.Drawing.Point(12, 9);
            this.lblUpFile.Name = "lblUpFile";
            this.lblUpFile.Size = new System.Drawing.Size(95, 12);
            this.lblUpFile.TabIndex = 1;
            this.lblUpFile.Text = "本次更新3个文件";
            // 
            // lblDownLoadFile
            // 
            this.lblDownLoadFile.AutoSize = true;
            this.lblDownLoadFile.Location = new System.Drawing.Point(10, 51);
            this.lblDownLoadFile.Name = "lblDownLoadFile";
            this.lblDownLoadFile.Size = new System.Drawing.Size(71, 12);
            this.lblDownLoadFile.TabIndex = 2;
            this.lblDownLoadFile.Text = "正在下载...";
            // 
            // lblDownLoadProc
            // 
            this.lblDownLoadProc.Location = new System.Drawing.Point(180, 51);
            this.lblDownLoadProc.Name = "lblDownLoadProc";
            this.lblDownLoadProc.Size = new System.Drawing.Size(100, 12);
            this.lblDownLoadProc.TabIndex = 3;
            this.lblDownLoadProc.Text = "200/4000";
            this.lblDownLoadProc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUpProc
            // 
            this.lblUpProc.Location = new System.Drawing.Point(180, 9);
            this.lblUpProc.Name = "lblUpProc";
            this.lblUpProc.Size = new System.Drawing.Size(100, 12);
            this.lblUpProc.TabIndex = 4;
            this.lblUpProc.Text = "1/3";
            this.lblUpProc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblInfo
            // 
            this.lblInfo.ForeColor = System.Drawing.Color.Red;
            this.lblInfo.Location = new System.Drawing.Point(10, 74);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(270, 25);
            this.lblInfo.TabIndex = 5;
            this.lblInfo.Text = "label3";
            // 
            // lbFiles
            // 
            this.lbFiles.FormattingEnabled = true;
            this.lbFiles.ItemHeight = 12;
            this.lbFiles.Location = new System.Drawing.Point(12, 102);
            this.lbFiles.Name = "lbFiles";
            this.lbFiles.Size = new System.Drawing.Size(268, 100);
            this.lbFiles.TabIndex = 6;
            this.lbFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbFiles_MouseDoubleClick);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(205, 208);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "更新";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 240);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.lbFiles);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblUpProc);
            this.Controls.Add(this.lblDownLoadProc);
            this.Controls.Add(this.lblDownLoadFile);
            this.Controls.Add(this.lblUpFile);
            this.Controls.Add(this.prgProc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统更新";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar prgProc;
        private System.Windows.Forms.Label lblUpFile;
        private System.Windows.Forms.Label lblDownLoadFile;
        private System.Windows.Forms.Label lblDownLoadProc;
        private System.Windows.Forms.Label lblUpProc;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.ListBox lbFiles;
        private System.Windows.Forms.Button btnUpdate;
    }
}

