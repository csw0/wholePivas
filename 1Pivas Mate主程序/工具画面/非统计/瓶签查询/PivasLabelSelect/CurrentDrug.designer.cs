namespace PivasLabelSelect
{
    partial class CurrentDrug
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
            this.lblDrugName = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblSpec = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPiShi = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDrugName
            // 
            this.lblDrugName.AutoEllipsis = true;
            this.lblDrugName.AutoSize = true;
            this.lblDrugName.ContextMenuStrip = this.contextMenuStrip1;
            this.lblDrugName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDrugName.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDrugName.ForeColor = System.Drawing.Color.Blue;
            this.lblDrugName.Location = new System.Drawing.Point(-1, 1);
            this.lblDrugName.Name = "lblDrugName";
            this.lblDrugName.Size = new System.Drawing.Size(33, 13);
            this.lblDrugName.TabIndex = 0;
            this.lblDrugName.Text = "药名";
            this.lblDrugName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.lblDrugName, "药品名   点击可查看药品说明书");
            this.lblDrugName.Click += new System.EventHandler(this.lblDrugName_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // lblSpec
            // 
            this.lblSpec.AutoEllipsis = true;
            this.lblSpec.ContextMenuStrip = this.contextMenuStrip1;
            this.lblSpec.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSpec.Location = new System.Drawing.Point(144, 0);
            this.lblSpec.Name = "lblSpec";
            this.lblSpec.Size = new System.Drawing.Size(194, 17);
            this.lblSpec.TabIndex = 1;
            this.lblSpec.Text = "规格";
            this.lblSpec.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblSpec, "药品包装规格");
            // 
            // label2
            // 
            this.label2.ContextMenuStrip = this.contextMenuStrip1;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(348, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "用量";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.label2, "药品用量");
            // 
            // lblPiShi
            // 
            this.lblPiShi.ContextMenuStrip = this.contextMenuStrip1;
            this.lblPiShi.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPiShi.Location = new System.Drawing.Point(437, 0);
            this.lblPiShi.Name = "lblPiShi";
            this.lblPiShi.Size = new System.Drawing.Size(43, 17);
            this.lblPiShi.TabIndex = 3;
            this.lblPiShi.Text = "皮";
            this.lblPiShi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblPiShi, "皮试信息");
            // 
            // CurrentDrug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblDrugName);
            this.Controls.Add(this.lblPiShi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblSpec);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "CurrentDrug";
            this.Size = new System.Drawing.Size(483, 25);
            this.Load += new System.EventHandler(this.CurrentDrug_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDrugName;
        private System.Windows.Forms.Label lblSpec;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPiShi;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
    }
}
