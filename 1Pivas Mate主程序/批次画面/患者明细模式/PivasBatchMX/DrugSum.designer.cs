namespace PivasBatchMX
{
    partial class DrugSum
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
            this.Label__ = new System.Windows.Forms.Panel();
            this.Panel_Total = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label_Sum = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Panel_Total.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label__
            // 
            this.Label__.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Label__.BackgroundImage = global::PivasBatchMX.Properties.Resources.药品横线;
            this.Label__.Location = new System.Drawing.Point(10, 0);
            this.Label__.Name = "Label__";
            this.Label__.Size = new System.Drawing.Size(490, 1);
            this.Label__.TabIndex = 10;
            this.Label__.Text = "label1";
            this.Label__.Click += new System.EventHandler(this.Label___Click);
            // 
            // Panel_Total
            // 
            this.Panel_Total.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Total.BackColor = System.Drawing.Color.Yellow;
            this.Panel_Total.BackgroundImage = global::PivasBatchMX.Properties.Resources.液体总量;
            this.Panel_Total.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel_Total.Controls.Add(this.label2);
            this.Panel_Total.Controls.Add(this.label_Sum);
            this.Panel_Total.Controls.Add(this.label1);
            this.Panel_Total.Location = new System.Drawing.Point(145, -1);
            this.Panel_Total.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Total.Name = "Panel_Total";
            this.Panel_Total.Size = new System.Drawing.Size(356, 30);
            this.Panel_Total.TabIndex = 6;
            this.Panel_Total.Click += new System.EventHandler(this.Label___Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(243, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 21);
            this.label2.TabIndex = 7;
            this.label2.Text = "空包";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Click += new System.EventHandler(this.Label___Click);
            // 
            // label_Sum
            // 
            this.label_Sum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Sum.AutoSize = true;
            this.label_Sum.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label_Sum.Location = new System.Drawing.Point(150, 7);
            this.label_Sum.Name = "label_Sum";
            this.label_Sum.Size = new System.Drawing.Size(76, 16);
            this.label_Sum.TabIndex = 7;
            this.label_Sum.Text = "液体总量";
            this.label_Sum.Click += new System.EventHandler(this.Label___Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(18, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "液体总量";
            this.label1.Click += new System.EventHandler(this.Label___Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Gray;
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(0, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(503, 1);
            this.label3.TabIndex = 11;
            // 
            // DrugSum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Label__);
            this.Controls.Add(this.Panel_Total);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "DrugSum";
            this.Size = new System.Drawing.Size(503, 29);
            this.Panel_Total.ResumeLayout(false);
            this.Panel_Total.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel Panel_Total;
        public System.Windows.Forms.Label label_Sum;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Panel Label__;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;





    }
}
