namespace PivasTool
{
    partial class UMaxControl
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
            this.FPanel_Main = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.FPanel_TreeStrip = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.FPanel_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // FPanel_Main
            // 
            this.FPanel_Main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPanel_Main.AutoScroll = true;
            this.FPanel_Main.Controls.Add(this.label1);
            this.FPanel_Main.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.FPanel_Main.Location = new System.Drawing.Point(196, 3);
            this.FPanel_Main.Margin = new System.Windows.Forms.Padding(0);
            this.FPanel_Main.Name = "FPanel_Main";
            this.FPanel_Main.Size = new System.Drawing.Size(603, 487);
            this.FPanel_Main.TabIndex = 0;
            this.FPanel_Main.WrapContents = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Image = global::PivasTool.Properties.Resources._5;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 1);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // FPanel_TreeStrip
            // 
            this.FPanel_TreeStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.FPanel_TreeStrip.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.FPanel_TreeStrip.Location = new System.Drawing.Point(0, 3);
            this.FPanel_TreeStrip.Margin = new System.Windows.Forms.Padding(0);
            this.FPanel_TreeStrip.Name = "FPanel_TreeStrip";
            this.FPanel_TreeStrip.Size = new System.Drawing.Size(195, 487);
            this.FPanel_TreeStrip.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.BackgroundImage = global::PivasTool.Properties.Resources.分隔线;
            this.pictureBox1.Location = new System.Drawing.Point(195, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1, 485);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // UMaxControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.FPanel_TreeStrip);
            this.Controls.Add(this.FPanel_Main);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UMaxControl";
            this.Size = new System.Drawing.Size(799, 490);
            this.Load += new System.EventHandler(this.UMaxControl_Load);
            this.SizeChanged += new System.EventHandler(this.UMaxControl_SizeChanged);
            this.FPanel_Main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel FPanel_Main;
        private System.Windows.Forms.FlowLayoutPanel FPanel_TreeStrip;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
      //  public static System.Windows.Forms.Button Btn_Change;
        //public static System.Windows.Forms.Label Label_Change;
    }
}
