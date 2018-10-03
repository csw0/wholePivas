namespace PivasFreqRule
{
    partial class DFreg
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.titleDFreg1 = new titleDFreg();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(0, 34);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(647, 385);
            this.panel2.TabIndex = 4;
            // 
            // titleDFreg1
            // 
            this.titleDFreg1.BackColor = System.Drawing.Color.White;
            this.titleDFreg1.Location = new System.Drawing.Point(1, 1);
            this.titleDFreg1.Name = "titleDFreg1";
            this.titleDFreg1.Size = new System.Drawing.Size(646, 33);
            this.titleDFreg1.TabIndex = 6;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.BackgroundImage = global::PivasFreqRule.Properties.Resources.plus_16;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel4.Location = new System.Drawing.Point(593, 429);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(28, 25);
            this.panel4.TabIndex = 20;
            this.toolTip1.SetToolTip(this.panel4, "添加");
            this.panel4.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::PivasFreqRule.Properties.Resources.Refresh;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel3.Location = new System.Drawing.Point(553, 429);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(28, 25);
            this.panel3.TabIndex = 19;
            this.toolTip1.SetToolTip(this.panel3, "更新");
            this.panel3.Click += new System.EventHandler(this.label1_Click);
            // 
            // DFreg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.titleDFreg1);
            this.Controls.Add(this.panel2);
            this.Name = "DFreg";
            this.Size = new System.Drawing.Size(647, 460);
            this.Load += new System.EventHandler(this.PivasDFeg_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private titleDFreg titleDFreg1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
