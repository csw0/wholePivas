namespace PivasTool
{
    partial class UMinControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UMinControl));
            this.Label_ToolsName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除此控件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改此控件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label_ToolsName
            // 
            this.Label_ToolsName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_ToolsName.BackColor = System.Drawing.Color.Transparent;
            this.Label_ToolsName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.Label_ToolsName.ForeColor = System.Drawing.Color.DimGray;
            this.Label_ToolsName.Location = new System.Drawing.Point(3, 73);
            this.Label_ToolsName.Name = "Label_ToolsName";
            this.Label_ToolsName.Size = new System.Drawing.Size(94, 16);
            this.Label_ToolsName.TabIndex = 1;
            this.Label_ToolsName.Text = "工具";
            this.Label_ToolsName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_ToolsName.MouseLeave += new System.EventHandler(this.panel1_MouseLeave);
            this.Label_ToolsName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.Label_ToolsName.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel1.ContextMenuStrip = this.contextMenuStrip1;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.Label_ToolsName);
            this.panel1.Location = new System.Drawing.Point(6, 7);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(100, 100);
            this.panel1.TabIndex = 3;
            this.panel1.MouseLeave += new System.EventHandler(this.panel1_MouseLeave);
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.panel1.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Location = new System.Drawing.Point(28, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(43, 48);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseLeave += new System.EventHandler(this.panel1_MouseLeave);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除此控件ToolStripMenuItem,
            this.修改此控件ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(112, 48);
            // 
            // 删除此控件ToolStripMenuItem
            // 
            this.删除此控件ToolStripMenuItem.Name = "删除此控件ToolStripMenuItem";
            this.删除此控件ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.删除此控件ToolStripMenuItem.Text = "删除此控件";
            this.删除此控件ToolStripMenuItem.Click += new System.EventHandler(this.删除此控件ToolStripMenuItem_Click);
            // 
            // 修改此控件ToolStripMenuItem
            // 
            this.修改此控件ToolStripMenuItem.Name = "修改此控件ToolStripMenuItem";
            this.修改此控件ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.修改此控件ToolStripMenuItem.Text = "修改此控件";
            this.修改此控件ToolStripMenuItem.Click += new System.EventHandler(this.修改此控件ToolStripMenuItem_Click);
            // 
            // UMinControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UMinControl";
            this.Size = new System.Drawing.Size(113, 115);
            this.Load += new System.EventHandler(this.UMinControl_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label Label_ToolsName;
        public System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除此控件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改此控件ToolStripMenuItem;

    }
}
