namespace LabelCheckAuthoritySet
{
    partial class ucAuthorityInfo
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
            this.lblSeqno = new System.Windows.Forms.Label();
            this.lblRuler = new System.Windows.Forms.Label();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblSeqno
            // 
            this.lblSeqno.AutoSize = true;
            this.lblSeqno.Location = new System.Drawing.Point(3, 30);
            this.lblSeqno.Name = "lblSeqno";
            this.lblSeqno.Size = new System.Drawing.Size(29, 12);
            this.lblSeqno.TabIndex = 0;
            this.lblSeqno.Text = "编号";
            this.lblSeqno.Visible = false;
            // 
            // lblRuler
            // 
            this.lblRuler.Location = new System.Drawing.Point(50, 9);
            this.lblRuler.Name = "lblRuler";
            this.lblRuler.Size = new System.Drawing.Size(182, 44);
            this.lblRuler.TabIndex = 1;
            this.lblRuler.Text = "规则";
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(0, 9);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(40, 23);
            this.btnModify.TabIndex = 2;
            this.btnModify.Text = "修改";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(0, 30);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(40, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // ucAuthorityInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.lblRuler);
            this.Controls.Add(this.lblSeqno);
            this.Name = "ucAuthorityInfo";
            this.Size = new System.Drawing.Size(259, 64);
            this.Load += new System.EventHandler(this.ucAuthorityInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSeqno;
        private System.Windows.Forms.Label lblRuler;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Button btnDelete;
    }
}
