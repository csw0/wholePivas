namespace PivasTool
{
    partial class UTreeControl
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
            this.Panel_Tree = new System.Windows.Forms.Panel();
            this.Label_TreeName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Panel_Tree.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Tree
            // 
            this.Panel_Tree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Tree.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Tree.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Panel_Tree.Controls.Add(this.Label_TreeName);
            this.Panel_Tree.Location = new System.Drawing.Point(0, 0);
            this.Panel_Tree.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Tree.Name = "Panel_Tree";
            this.Panel_Tree.Size = new System.Drawing.Size(195, 60);
            this.Panel_Tree.TabIndex = 2;
            this.Panel_Tree.Click += new System.EventHandler(this.Panel_Tree_Click);
            // 
            // Label_TreeName
            // 
            this.Label_TreeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_TreeName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label_TreeName.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.Label_TreeName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label_TreeName.Location = new System.Drawing.Point(29, 7);
            this.Label_TreeName.Margin = new System.Windows.Forms.Padding(0);
            this.Label_TreeName.Name = "Label_TreeName";
            this.Label_TreeName.Size = new System.Drawing.Size(165, 47);
            this.Label_TreeName.TabIndex = 0;
            this.Label_TreeName.Text = "权限管理";
            this.Label_TreeName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label_TreeName.Click += new System.EventHandler(this.Panel_Tree_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Image = global::PivasTool.Properties.Resources.菜单项分隔线;
            this.label1.Location = new System.Drawing.Point(0, 59);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 1);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // UTreeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = global::PivasTool.Properties.Resources.菜单项;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Panel_Tree);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UTreeControl";
            this.Size = new System.Drawing.Size(195, 60);
            this.Panel_Tree.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel Panel_Tree;
        public System.Windows.Forms.Label Label_TreeName;
    }
}
