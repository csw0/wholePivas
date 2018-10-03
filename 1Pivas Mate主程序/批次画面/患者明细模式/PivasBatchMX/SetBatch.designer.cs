namespace PivasBatchMX
{
    partial class SetBatch
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
            this.Panel_TeamNumber = new System.Windows.Forms.Panel();
            this.Label_TeamNumber = new System.Windows.Forms.Label();
            this.Panel_TeamNumber.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_TeamNumber
            // 
            this.Panel_TeamNumber.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel_TeamNumber.Controls.Add(this.Label_TeamNumber);
            this.Panel_TeamNumber.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Panel_TeamNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_TeamNumber.Location = new System.Drawing.Point(0, 0);
            this.Panel_TeamNumber.Margin = new System.Windows.Forms.Padding(5);
            this.Panel_TeamNumber.Name = "Panel_TeamNumber";
            this.Panel_TeamNumber.Size = new System.Drawing.Size(30, 30);
            this.Panel_TeamNumber.TabIndex = 0;
            this.Panel_TeamNumber.Text = "24";
            this.Panel_TeamNumber.MouseLeave += new System.EventHandler(this.Label_TeamNumber_MouseLeave);
            this.Panel_TeamNumber.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_TeamNumber_MouseMove);
            this.Panel_TeamNumber.Click += new System.EventHandler(this.SetBatch_Click);
            // 
            // Label_TeamNumber
            // 
            this.Label_TeamNumber.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label_TeamNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label_TeamNumber.Location = new System.Drawing.Point(0, 0);
            this.Label_TeamNumber.Margin = new System.Windows.Forms.Padding(5, 0, 5, 3);
            this.Label_TeamNumber.Name = "Label_TeamNumber";
            this.Label_TeamNumber.Size = new System.Drawing.Size(30, 30);
            this.Label_TeamNumber.TabIndex = 0;
            this.Label_TeamNumber.Text = "24";
            this.Label_TeamNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_TeamNumber.MouseLeave += new System.EventHandler(this.Label_TeamNumber_MouseLeave);
            this.Label_TeamNumber.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_TeamNumber_MouseMove);
            this.Label_TeamNumber.Click += new System.EventHandler(this.SetBatch_Click);
            // 
            // SetBatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.Panel_TeamNumber);
            this.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.Name = "SetBatch";
            this.Size = new System.Drawing.Size(30, 30);
            this.Click += new System.EventHandler(this.SetBatch_Click);
            this.Panel_TeamNumber.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_TeamNumber;
        private System.Windows.Forms.Label Label_TeamNumber;
    }
}
