namespace qrcode
{
    partial class frmQR
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
            this.picQR = new System.Windows.Forms.PictureBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblTip = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picQR)).BeginInit();
            this.SuspendLayout();
            // 
            // picQR
            // 
            this.picQR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picQR.Dock = System.Windows.Forms.DockStyle.Top;
            this.picQR.Location = new System.Drawing.Point(0, 0);
            this.picQR.Name = "picQR";
            this.picQR.Size = new System.Drawing.Size(284, 230);
            this.picQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picQR.TabIndex = 0;
            this.picQR.TabStop = false;
            this.picQR.DoubleClick += new System.EventHandler(this.picQR_DoubleClick);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(119, 276);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(67, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定(&Y)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblTip
            // 
            this.lblTip.ForeColor = System.Drawing.Color.DimGray;
            this.lblTip.Location = new System.Drawing.Point(8, 233);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(276, 32);
            this.lblTip.TabIndex = 2;
            this.lblTip.Text = "label1";
            this.lblTip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmQR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 311);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.picQR);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQR";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "二维码";
            ((System.ComponentModel.ISupportInitialize)(this.picQR)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picQR;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblTip;
    }
}

