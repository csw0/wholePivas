namespace PivasBatchMX
{
    partial class BatchInfo
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.Panel_DrugInfo = new System.Windows.Forms.Panel();
            this.Panel_DrugInfo_DrugName = new System.Windows.Forms.Label();
            this.Panel_DrugInfo_DrugSpec = new System.Windows.Forms.Label();
            this.Label_Linetop = new System.Windows.Forms.Label();
            this.Panel_DrugInfo_DrugDosage = new System.Windows.Forms.Label();
            this.Label_Linebottom = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Panel_Batch = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.laUsageCode = new System.Windows.Forms.Label();
            this.Label_GroupNo = new System.Windows.Forms.Label();
            this.Label_InsertDt = new System.Windows.Forms.Label();
            this.Label_LabelNo = new System.Windows.Forms.Label();
            this.Label_Batch = new System.Windows.Forms.Label();
            this.LabelLine = new System.Windows.Forms.Panel();
            this.Label_BatchSaved = new System.Windows.Forms.Label();
            this.Label_FreqName = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.Panel_DrugInfo.SuspendLayout();
            this.Panel_Batch.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Panel_DrugInfo);
            this.panel1.Controls.Add(this.Panel_Batch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(842, 27);
            this.panel1.TabIndex = 0;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // Panel_DrugInfo
            // 
            this.Panel_DrugInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.Panel_DrugInfo.Controls.Add(this.Panel_DrugInfo_DrugName);
            this.Panel_DrugInfo.Controls.Add(this.Panel_DrugInfo_DrugSpec);
            this.Panel_DrugInfo.Controls.Add(this.Label_Linetop);
            this.Panel_DrugInfo.Controls.Add(this.Panel_DrugInfo_DrugDosage);
            this.Panel_DrugInfo.Controls.Add(this.Label_Linebottom);
            this.Panel_DrugInfo.Controls.Add(this.label1);
            this.Panel_DrugInfo.Location = new System.Drawing.Point(197, 0);
            this.Panel_DrugInfo.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_DrugInfo.Name = "Panel_DrugInfo";
            this.Panel_DrugInfo.Size = new System.Drawing.Size(371, 24);
            this.Panel_DrugInfo.TabIndex = 3;
            this.Panel_DrugInfo.Click += new System.EventHandler(this.panel1_Click);
            // 
            // Panel_DrugInfo_DrugName
            // 
            this.Panel_DrugInfo_DrugName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_DrugInfo_DrugName.AutoEllipsis = true;
            this.Panel_DrugInfo_DrugName.AutoSize = true;
            this.Panel_DrugInfo_DrugName.Location = new System.Drawing.Point(32, 1);
            this.Panel_DrugInfo_DrugName.MaximumSize = new System.Drawing.Size(500, 22);
            this.Panel_DrugInfo_DrugName.MinimumSize = new System.Drawing.Size(150, 22);
            this.Panel_DrugInfo_DrugName.Name = "Panel_DrugInfo_DrugName";
            this.Panel_DrugInfo_DrugName.Size = new System.Drawing.Size(150, 22);
            this.Panel_DrugInfo_DrugName.TabIndex = 3;
            this.Panel_DrugInfo_DrugName.Text = "0.9%氯化钠注射液";
            this.Panel_DrugInfo_DrugName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.Panel_DrugInfo_DrugName, "123");
            this.Panel_DrugInfo_DrugName.Click += new System.EventHandler(this.panel1_Click);
            // 
            // Panel_DrugInfo_DrugSpec
            // 
            this.Panel_DrugInfo_DrugSpec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_DrugInfo_DrugSpec.AutoEllipsis = true;
            this.Panel_DrugInfo_DrugSpec.ForeColor = System.Drawing.Color.Gray;
            this.Panel_DrugInfo_DrugSpec.Location = new System.Drawing.Point(108, 1);
            this.Panel_DrugInfo_DrugSpec.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_DrugInfo_DrugSpec.Name = "Panel_DrugInfo_DrugSpec";
            this.Panel_DrugInfo_DrugSpec.Size = new System.Drawing.Size(143, 22);
            this.Panel_DrugInfo_DrugSpec.TabIndex = 5;
            this.Panel_DrugInfo_DrugSpec.Text = "250.00mg*100/瓶";
            this.Panel_DrugInfo_DrugSpec.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Panel_DrugInfo_DrugSpec.Click += new System.EventHandler(this.panel1_Click);
            // 
            // Label_Linetop
            // 
            this.Label_Linetop.Image = global::PivasBatchMX.Properties.Resources.边框横线;
            this.Label_Linetop.Location = new System.Drawing.Point(14, -1);
            this.Label_Linetop.Name = "Label_Linetop";
            this.Label_Linetop.Size = new System.Drawing.Size(11, 15);
            this.Label_Linetop.TabIndex = 6;
            this.Label_Linetop.Visible = false;
            this.Label_Linetop.Click += new System.EventHandler(this.panel1_Click);
            // 
            // Panel_DrugInfo_DrugDosage
            // 
            this.Panel_DrugInfo_DrugDosage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_DrugInfo_DrugDosage.AutoEllipsis = true;
            this.Panel_DrugInfo_DrugDosage.AutoSize = true;
            this.Panel_DrugInfo_DrugDosage.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Panel_DrugInfo_DrugDosage.Location = new System.Drawing.Point(253, 4);
            this.Panel_DrugInfo_DrugDosage.Name = "Panel_DrugInfo_DrugDosage";
            this.Panel_DrugInfo_DrugDosage.Size = new System.Drawing.Size(42, 14);
            this.Panel_DrugInfo_DrugDosage.TabIndex = 4;
            this.Panel_DrugInfo_DrugDosage.Text = "250ml";
            this.Panel_DrugInfo_DrugDosage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Panel_DrugInfo_DrugDosage.Click += new System.EventHandler(this.panel1_Click);
            // 
            // Label_Linebottom
            // 
            this.Label_Linebottom.Image = global::PivasBatchMX.Properties.Resources.边框横线;
            this.Label_Linebottom.Location = new System.Drawing.Point(13, 10);
            this.Label_Linebottom.Name = "Label_Linebottom";
            this.Label_Linebottom.Size = new System.Drawing.Size(12, 15);
            this.Label_Linebottom.TabIndex = 6;
            this.Label_Linebottom.Visible = false;
            this.Label_Linebottom.Click += new System.EventHandler(this.panel1_Click);
            // 
            // label1
            // 
            this.label1.Image = global::PivasBatchMX.Properties.Resources.边框竖线;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 7);
            this.label1.TabIndex = 6;
            this.label1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // Panel_Batch
            // 
            this.Panel_Batch.Controls.Add(this.panel2);
            this.Panel_Batch.Controls.Add(this.Label_Batch);
            this.Panel_Batch.Controls.Add(this.LabelLine);
            this.Panel_Batch.Controls.Add(this.Label_BatchSaved);
            this.Panel_Batch.Controls.Add(this.Label_FreqName);
            this.Panel_Batch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Batch.Location = new System.Drawing.Point(0, 0);
            this.Panel_Batch.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Batch.Name = "Panel_Batch";
            this.Panel_Batch.Size = new System.Drawing.Size(842, 27);
            this.Panel_Batch.TabIndex = 2;
            this.Panel_Batch.Click += new System.EventHandler(this.panel1_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.laUsageCode);
            this.panel2.Controls.Add(this.Label_GroupNo);
            this.panel2.Controls.Add(this.Label_InsertDt);
            this.panel2.Controls.Add(this.Label_LabelNo);
            this.panel2.Location = new System.Drawing.Point(567, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(271, 24);
            this.panel2.TabIndex = 11;
            // 
            // laUsageCode
            // 
            this.laUsageCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.laUsageCode.Location = new System.Drawing.Point(184, 2);
            this.laUsageCode.Name = "laUsageCode";
            this.laUsageCode.Size = new System.Drawing.Size(79, 21);
            this.laUsageCode.TabIndex = 10;
            this.laUsageCode.Text = "腹腔化疗";
            this.laUsageCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_GroupNo
            // 
            this.Label_GroupNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_GroupNo.Location = new System.Drawing.Point(97, 1);
            this.Label_GroupNo.Name = "Label_GroupNo";
            this.Label_GroupNo.Size = new System.Drawing.Size(90, 21);
            this.Label_GroupNo.TabIndex = 7;
            this.Label_GroupNo.Text = "1组101244983";
            this.Label_GroupNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_GroupNo.Click += new System.EventHandler(this.panel1_Click);
            // 
            // Label_InsertDt
            // 
            this.Label_InsertDt.Location = new System.Drawing.Point(112, 5);
            this.Label_InsertDt.Name = "Label_InsertDt";
            this.Label_InsertDt.Size = new System.Drawing.Size(143, 12);
            this.Label_InsertDt.TabIndex = 8;
            this.Label_InsertDt.Text = "2013-09-28/瓶签生成时间";
            this.Label_InsertDt.Click += new System.EventHandler(this.panel1_Click);
            // 
            // Label_LabelNo
            // 
            this.Label_LabelNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_LabelNo.AutoSize = true;
            this.Label_LabelNo.Font = new System.Drawing.Font("宋体", 9F);
            this.Label_LabelNo.ForeColor = System.Drawing.Color.Gray;
            this.Label_LabelNo.Location = new System.Drawing.Point(3, 6);
            this.Label_LabelNo.Name = "Label_LabelNo";
            this.Label_LabelNo.Size = new System.Drawing.Size(89, 12);
            this.Label_LabelNo.TabIndex = 6;
            this.Label_LabelNo.Text = "20130815000008";
            this.Label_LabelNo.Click += new System.EventHandler(this.panel1_Click);
            // 
            // Label_Batch
            // 
            this.Label_Batch.BackColor = System.Drawing.Color.Yellow;
            this.Label_Batch.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Batch.Location = new System.Drawing.Point(52, 2);
            this.Label_Batch.Name = "Label_Batch";
            this.Label_Batch.Size = new System.Drawing.Size(64, 23);
            this.Label_Batch.TabIndex = 5;
            this.Label_Batch.Text = "24-k";
            this.Label_Batch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_Batch.Click += new System.EventHandler(this.Label_Batch_Click);
            // 
            // LabelLine
            // 
            this.LabelLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelLine.BackgroundImage = global::PivasBatchMX.Properties.Resources.药品横线;
            this.LabelLine.Location = new System.Drawing.Point(10, 0);
            this.LabelLine.Name = "LabelLine";
            this.LabelLine.Size = new System.Drawing.Size(819, 1);
            this.LabelLine.TabIndex = 9;
            this.LabelLine.Text = "label1";
            this.LabelLine.Click += new System.EventHandler(this.panel1_Click);
            // 
            // Label_BatchSaved
            // 
            this.Label_BatchSaved.BackColor = System.Drawing.Color.Transparent;
            this.Label_BatchSaved.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_BatchSaved.Location = new System.Drawing.Point(-1, 4);
            this.Label_BatchSaved.Name = "Label_BatchSaved";
            this.Label_BatchSaved.Size = new System.Drawing.Size(49, 18);
            this.Label_BatchSaved.TabIndex = 4;
            this.Label_BatchSaved.Text = "已发送";
            this.Label_BatchSaved.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_BatchSaved.Click += new System.EventHandler(this.panel1_Click);
            // 
            // Label_FreqName
            // 
            this.Label_FreqName.AutoEllipsis = true;
            this.Label_FreqName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.Label_FreqName.Location = new System.Drawing.Point(118, 1);
            this.Label_FreqName.Name = "Label_FreqName";
            this.Label_FreqName.Size = new System.Drawing.Size(86, 23);
            this.Label_FreqName.TabIndex = 3;
            this.Label_FreqName.Text = "每天12点一次";
            this.Label_FreqName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_FreqName.Click += new System.EventHandler(this.panel1_Click);
            // 
            // BatchInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "BatchInfo";
            this.Size = new System.Drawing.Size(842, 27);
            this.SizeChanged += new System.EventHandler(this.BatchInfo_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.Panel_DrugInfo.ResumeLayout(false);
            this.Panel_DrugInfo.PerformLayout();
            this.Panel_Batch.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel Panel_DrugInfo;
        private System.Windows.Forms.Label Panel_DrugInfo_DrugDosage;
        private System.Windows.Forms.Label Panel_DrugInfo_DrugSpec;
        private System.Windows.Forms.Label Panel_DrugInfo_DrugName;
        private System.Windows.Forms.Panel Panel_Batch;
        private System.Windows.Forms.Label Label_FreqName;
        private System.Windows.Forms.Label Label_LabelNo;
        private System.Windows.Forms.Label Label_GroupNo;
        private System.Windows.Forms.Label Label_InsertDt;
        private System.Windows.Forms.Panel LabelLine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Label_Linebottom;
        private System.Windows.Forms.Label Label_Linetop;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label Label_BatchSaved;
        private System.Windows.Forms.Label laUsageCode;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Label Label_Batch;


    }
}
