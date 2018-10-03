namespace PivasRevPre
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
            this.lblSpec = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPiShi = new System.Windows.Forms.Label();
            this.lblDrugName = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lblSpec
            // 
            this.lblSpec.AutoEllipsis = true;
            this.lblSpec.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSpec.Location = new System.Drawing.Point(259, 1);
            this.lblSpec.Name = "lblSpec";
            this.lblSpec.Size = new System.Drawing.Size(103, 17);
            this.lblSpec.TabIndex = 1;
            this.lblSpec.Text = "规格";
            this.lblSpec.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSpec.Click += new System.EventHandler(this.lblDrugName_Click);
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(365, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "用法";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Click += new System.EventHandler(this.lblDrugName_Click);
            // 
            // lblPiShi
            // 
            this.lblPiShi.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPiShi.Location = new System.Drawing.Point(423, 1);
            this.lblPiShi.Name = "lblPiShi";
            this.lblPiShi.Size = new System.Drawing.Size(44, 17);
            this.lblPiShi.TabIndex = 3;
            this.lblPiShi.Text = "皮";
            this.lblPiShi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPiShi.Click += new System.EventHandler(this.lblDrugName_Click);
            // 
            // lblDrugName
            // 
            this.lblDrugName.AutoEllipsis = true;
            this.lblDrugName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDrugName.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lblDrugName.LinkColor = System.Drawing.Color.Black;
            this.lblDrugName.Location = new System.Drawing.Point(2, 1);
            this.lblDrugName.Name = "lblDrugName";
            this.lblDrugName.Size = new System.Drawing.Size(234, 17);
            this.lblDrugName.TabIndex = 4;
            this.lblDrugName.TabStop = true;
            this.lblDrugName.Text = "药品名";
            this.lblDrugName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblDrugName_LinkClicked);
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
            this.Size = new System.Drawing.Size(470, 17);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSpec;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPiShi;
        private System.Windows.Forms.LinkLabel lblDrugName;
    }
}
