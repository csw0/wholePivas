namespace PivasRevPre
{
    partial class DetailDrug
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
            this.lblPiShi = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSpec = new System.Windows.Forms.Label();
            this.lblDrugName = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPiShi
            // 
            this.lblPiShi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPiShi.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPiShi.Location = new System.Drawing.Point(494, 0);
            this.lblPiShi.Name = "lblPiShi";
            this.lblPiShi.Size = new System.Drawing.Size(44, 17);
            this.lblPiShi.TabIndex = 7;
            this.lblPiShi.Text = "皮试";
            this.lblPiShi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPiShi.Click += new System.EventHandler(this.lblDrugName_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(432, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "用法";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Click += new System.EventHandler(this.lblDrugName_Click);
            // 
            // lblSpec
            // 
            this.lblSpec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSpec.AutoEllipsis = true;
            this.lblSpec.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSpec.ForeColor = System.Drawing.Color.Silver;
            this.lblSpec.Location = new System.Drawing.Point(278, 0);
            this.lblSpec.Name = "lblSpec";
            this.lblSpec.Size = new System.Drawing.Size(93, 17);
            this.lblSpec.TabIndex = 5;
            this.lblSpec.Text = "规格";
            this.lblSpec.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSpec.Click += new System.EventHandler(this.lblDrugName_Click);
            // 
            // lblDrugName
            // 
            this.lblDrugName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDrugName.AutoEllipsis = true;
            this.lblDrugName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDrugName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblDrugName.Location = new System.Drawing.Point(33, 0);
            this.lblDrugName.Name = "lblDrugName";
            this.lblDrugName.Size = new System.Drawing.Size(203, 17);
            this.lblDrugName.TabIndex = 4;
            this.lblDrugName.Text = "药名";
            this.lblDrugName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDrugName.Click += new System.EventHandler(this.lblDrugName_Click);
            // 
            // lblQuantity
            // 
            this.lblQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQuantity.Location = new System.Drawing.Point(236, 0);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(42, 17);
            this.lblQuantity.TabIndex = 8;
            this.lblQuantity.Text = "数量";
            this.lblQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoEllipsis = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(371, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "剂量";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Click += new System.EventHandler(this.lblDrugName_Click);
            // 
            // DetailDrug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.lblPiShi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblSpec);
            this.Controls.Add(this.lblDrugName);
            this.Name = "DetailDrug";
            this.Size = new System.Drawing.Size(540, 19);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPiShi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSpec;
        private System.Windows.Forms.Label lblDrugName;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label label1;
    }
}
