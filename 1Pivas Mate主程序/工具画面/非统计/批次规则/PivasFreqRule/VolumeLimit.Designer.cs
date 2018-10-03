namespace PivasFreqRule
{
    partial class VolumeLimit
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
            this.batch = new System.Windows.Forms.Label();
            this.lblLow = new System.Windows.Forms.Label();
            this.lblUp = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.txtLow = new System.Windows.Forms.TextBox();
            this.txtUp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // batch
            // 
            this.batch.Location = new System.Drawing.Point(12, 6);
            this.batch.Name = "batch";
            this.batch.Size = new System.Drawing.Size(63, 12);
            this.batch.TabIndex = 0;
            this.batch.Text = "label1";
            this.batch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLow
            // 
            this.lblLow.Location = new System.Drawing.Point(135, 6);
            this.lblLow.Name = "lblLow";
            this.lblLow.Size = new System.Drawing.Size(71, 12);
            this.lblLow.TabIndex = 1;
            this.lblLow.Text = "label2";
            this.lblLow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblLow.Click += new System.EventHandler(this.lblLow_Click);
            // 
            // lblUp
            // 
            this.lblUp.Location = new System.Drawing.Point(245, 6);
            this.lblUp.Name = "lblUp";
            this.lblUp.Size = new System.Drawing.Size(75, 12);
            this.lblUp.TabIndex = 2;
            this.lblUp.Text = "label3";
            this.lblUp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblUp.Click += new System.EventHandler(this.lblUp_Click);
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(290, -5);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(17, 12);
            this.lblID.TabIndex = 3;
            this.lblID.Text = "ID";
            this.lblID.Visible = false;
            // 
            // txtLow
            // 
            this.txtLow.Location = new System.Drawing.Point(131, 2);
            this.txtLow.Name = "txtLow";
            this.txtLow.Size = new System.Drawing.Size(74, 21);
            this.txtLow.TabIndex = 5;
            this.txtLow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLow.Visible = false;
            this.txtLow.Leave += new System.EventHandler(this.txtLow_Leave);
            this.txtLow.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLow_KeyPress);
            // 
            // txtUp
            // 
            this.txtUp.Location = new System.Drawing.Point(237, 2);
            this.txtUp.Name = "txtUp";
            this.txtUp.Size = new System.Drawing.Size(82, 21);
            this.txtUp.TabIndex = 6;
            this.txtUp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtUp.Visible = false;
            this.txtUp.Leave += new System.EventHandler(this.txtUp_Leave);
            this.txtUp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUp_KeyPress);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(16, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(338, 1);
            this.label1.TabIndex = 7;
            // 
            // VolumeLimit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUp);
            this.Controls.Add(this.txtLow);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.lblUp);
            this.Controls.Add(this.lblLow);
            this.Controls.Add(this.batch);
            this.Name = "VolumeLimit";
            this.Size = new System.Drawing.Size(344, 30);
            this.Click += new System.EventHandler(this.VolumeLimit_Click);
            this.Leave += new System.EventHandler(this.VolumeLimit_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label batch;
        private System.Windows.Forms.Label lblLow;
        private System.Windows.Forms.Label lblUp;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox txtLow;
        private System.Windows.Forms.TextBox txtUp;
        private System.Windows.Forms.Label label1;
    }
}
