namespace PivasFreqRule
{
    partial class MedcineRow
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblMedName = new System.Windows.Forms.Label();
            this.lblMedID = new System.Windows.Forms.Label();
            this.lblSeqNo = new System.Windows.Forms.Label();
            this.lblMedCode = new System.Windows.Forms.Label();
            this.lblIsClass = new System.Windows.Forms.Label();
            this.pnlDelete = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lblMedName
            // 
            this.lblMedName.AutoEllipsis = true;
            this.lblMedName.BackColor = System.Drawing.Color.Transparent;
            this.lblMedName.Location = new System.Drawing.Point(22, 6);
            this.lblMedName.Name = "lblMedName";
            this.lblMedName.Size = new System.Drawing.Size(190, 19);
            this.lblMedName.TabIndex = 0;
            this.lblMedName.Text = "M";
            this.lblMedName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMedName.DoubleClick += new System.EventHandler(this.Medcine_DoubleClick);
            this.lblMedName.Click += new System.EventHandler(this.MedcineName_Click);
            // 
            // lblMedID
            // 
            this.lblMedID.AutoSize = true;
            this.lblMedID.Location = new System.Drawing.Point(182, 3);
            this.lblMedID.Name = "lblMedID";
            this.lblMedID.Size = new System.Drawing.Size(41, 12);
            this.lblMedID.TabIndex = 1;
            this.lblMedID.Text = "label1";
            this.lblMedID.Visible = false;
            // 
            // lblSeqNo
            // 
            this.lblSeqNo.AutoSize = true;
            this.lblSeqNo.Location = new System.Drawing.Point(182, 6);
            this.lblSeqNo.Name = "lblSeqNo";
            this.lblSeqNo.Size = new System.Drawing.Size(41, 12);
            this.lblSeqNo.TabIndex = 2;
            this.lblSeqNo.Text = "label1";
            this.lblSeqNo.Visible = false;
            // 
            // lblMedCode
            // 
            this.lblMedCode.AutoSize = true;
            this.lblMedCode.Location = new System.Drawing.Point(182, 3);
            this.lblMedCode.Name = "lblMedCode";
            this.lblMedCode.Size = new System.Drawing.Size(41, 12);
            this.lblMedCode.TabIndex = 3;
            this.lblMedCode.Text = "label1";
            this.lblMedCode.Visible = false;
            // 
            // lblIsClass
            // 
            this.lblIsClass.AutoSize = true;
            this.lblIsClass.Location = new System.Drawing.Point(182, 6);
            this.lblIsClass.Name = "lblIsClass";
            this.lblIsClass.Size = new System.Drawing.Size(41, 12);
            this.lblIsClass.TabIndex = 4;
            this.lblIsClass.Text = "label1";
            this.lblIsClass.Visible = false;
            // 
            // pnlDelete
            // 
            this.pnlDelete.BackgroundImage = global::PivasFreqRule.Properties.Resources.delete_161;
            this.pnlDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlDelete.Location = new System.Drawing.Point(3, 8);
            this.pnlDelete.Name = "pnlDelete";
            this.pnlDelete.Size = new System.Drawing.Size(16, 16);
            this.pnlDelete.TabIndex = 5;
            this.pnlDelete.Click += new System.EventHandler(this.pnlDelete_Click);
            // 
            // MedcineRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlDelete);
            this.Controls.Add(this.lblIsClass);
            this.Controls.Add(this.lblMedCode);
            this.Controls.Add(this.lblSeqNo);
            this.Controls.Add(this.lblMedID);
            this.Controls.Add(this.lblMedName);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MedcineRow";
            this.Size = new System.Drawing.Size(216, 30);
            this.DoubleClick += new System.EventHandler(this.Medcine_DoubleClick);
            this.Click += new System.EventHandler(this.MedcineName_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMedName;
        public System.Windows.Forms.Label lblMedID;
        public System.Windows.Forms.Label lblSeqNo;
        private System.Windows.Forms.Label lblMedCode;
        private System.Windows.Forms.Label lblIsClass;
        private System.Windows.Forms.Panel pnlDelete;
    }
}
