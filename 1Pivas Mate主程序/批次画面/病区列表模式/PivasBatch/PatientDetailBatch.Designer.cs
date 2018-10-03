namespace PivasBatch
{
    partial class PatientDetailBatch
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
            this.Panel_Batch = new System.Windows.Forms.Panel();
            this.Label_InsertDt = new System.Windows.Forms.Label();
            this.Label_GroupNo = new System.Windows.Forms.Label();
            this.Label_LabelNo = new System.Windows.Forms.Label();
            this.Label_BatchSaved = new System.Windows.Forms.Label();
            this.Label_FreqName = new System.Windows.Forms.Label();
            this.Panel_DrugInfo = new System.Windows.Forms.Panel();
            this.Panel_DrugInfo_DrugSpec = new System.Windows.Forms.Label();
            this.Panel_DrugInfo_DrugDosage = new System.Windows.Forms.Label();
            this.Panel_DrugInfo_DrugName = new System.Windows.Forms.Label();
            this.Label__ = new System.Windows.Forms.Panel();
            this.Label_Batch = new System.Windows.Forms.Label();
            this.Panel_Batch.SuspendLayout();
            this.Panel_DrugInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Batch
            // 
            this.Panel_Batch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Batch.Controls.Add(this.Label__);
            this.Panel_Batch.Controls.Add(this.Label_InsertDt);
            this.Panel_Batch.Controls.Add(this.Label_GroupNo);
            this.Panel_Batch.Controls.Add(this.Label_LabelNo);
            this.Panel_Batch.Controls.Add(this.Label_Batch);
            this.Panel_Batch.Controls.Add(this.Label_BatchSaved);
            this.Panel_Batch.Controls.Add(this.Label_FreqName);
            this.Panel_Batch.Location = new System.Drawing.Point(0, 0);
            this.Panel_Batch.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Batch.Name = "Panel_Batch";
            this.Panel_Batch.Size = new System.Drawing.Size(475, 24);
            this.Panel_Batch.TabIndex = 3;
            // 
            // Label_InsertDt
            // 
            this.Label_InsertDt.AutoSize = true;
            this.Label_InsertDt.Location = new System.Drawing.Point(485, 6);
            this.Label_InsertDt.Name = "Label_InsertDt";
            this.Label_InsertDt.Size = new System.Drawing.Size(77, 12);
            this.Label_InsertDt.TabIndex = 8;
            this.Label_InsertDt.Text = "瓶签生成时间";
            // 
            // Label_GroupNo
            // 
            this.Label_GroupNo.AutoSize = true;
            this.Label_GroupNo.Location = new System.Drawing.Point(331, 6);
            this.Label_GroupNo.Name = "Label_GroupNo";
            this.Label_GroupNo.Size = new System.Drawing.Size(23, 12);
            this.Label_GroupNo.TabIndex = 7;
            this.Label_GroupNo.Text = "1组";
            // 
            // Label_LabelNo
            // 
            this.Label_LabelNo.AutoSize = true;
            this.Label_LabelNo.Font = new System.Drawing.Font("宋体", 9F);
            this.Label_LabelNo.Location = new System.Drawing.Point(187, 6);
            this.Label_LabelNo.Name = "Label_LabelNo";
            this.Label_LabelNo.Size = new System.Drawing.Size(89, 12);
            this.Label_LabelNo.TabIndex = 6;
            this.Label_LabelNo.Text = "20130815000008";
            // 
            // Label_BatchSaved
            // 
            this.Label_BatchSaved.AutoSize = true;
            this.Label_BatchSaved.BackColor = System.Drawing.Color.Transparent;
            this.Label_BatchSaved.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_BatchSaved.Location = new System.Drawing.Point(81, 6);
            this.Label_BatchSaved.Name = "Label_BatchSaved";
            this.Label_BatchSaved.Size = new System.Drawing.Size(49, 13);
            this.Label_BatchSaved.TabIndex = 4;
            this.Label_BatchSaved.Text = "已发送";
            // 
            // Label_FreqName
            // 
            this.Label_FreqName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.Label_FreqName.Location = new System.Drawing.Point(-2, 1);
            this.Label_FreqName.Name = "Label_FreqName";
            this.Label_FreqName.Size = new System.Drawing.Size(86, 23);
            this.Label_FreqName.TabIndex = 3;
            this.Label_FreqName.Text = "每天12点一次";
            this.Label_FreqName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Panel_DrugInfo
            // 
            this.Panel_DrugInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_DrugInfo.Controls.Add(this.Panel_DrugInfo_DrugSpec);
            this.Panel_DrugInfo.Controls.Add(this.Panel_DrugInfo_DrugDosage);
            this.Panel_DrugInfo.Controls.Add(this.Panel_DrugInfo_DrugName);
            this.Panel_DrugInfo.Location = new System.Drawing.Point(0, 24);
            this.Panel_DrugInfo.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_DrugInfo.Name = "Panel_DrugInfo";
            this.Panel_DrugInfo.Size = new System.Drawing.Size(475, 23);
            this.Panel_DrugInfo.TabIndex = 4;
            // 
            // Panel_DrugInfo_DrugSpec
            // 
            this.Panel_DrugInfo_DrugSpec.Location = new System.Drawing.Point(294, -1);
            this.Panel_DrugInfo_DrugSpec.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_DrugInfo_DrugSpec.Name = "Panel_DrugInfo_DrugSpec";
            this.Panel_DrugInfo_DrugSpec.Size = new System.Drawing.Size(108, 22);
            this.Panel_DrugInfo_DrugSpec.TabIndex = 5;
            this.Panel_DrugInfo_DrugSpec.Text = "250.00mg*100/瓶";
            this.Panel_DrugInfo_DrugSpec.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Panel_DrugInfo_DrugDosage
            // 
            this.Panel_DrugInfo_DrugDosage.Location = new System.Drawing.Point(404, 0);
            this.Panel_DrugInfo_DrugDosage.Name = "Panel_DrugInfo_DrugDosage";
            this.Panel_DrugInfo_DrugDosage.Size = new System.Drawing.Size(62, 22);
            this.Panel_DrugInfo_DrugDosage.TabIndex = 4;
            this.Panel_DrugInfo_DrugDosage.Text = "250ml";
            this.Panel_DrugInfo_DrugDosage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Panel_DrugInfo_DrugName
            // 
            this.Panel_DrugInfo_DrugName.Location = new System.Drawing.Point(20, 1);
            this.Panel_DrugInfo_DrugName.Name = "Panel_DrugInfo_DrugName";
            this.Panel_DrugInfo_DrugName.Size = new System.Drawing.Size(275, 22);
            this.Panel_DrugInfo_DrugName.TabIndex = 3;
            this.Panel_DrugInfo_DrugName.Text = "0.9%氯化钠注射液";
            this.Panel_DrugInfo_DrugName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label__
            // 
            this.Label__.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Label__.BackgroundImage = global::PivasBatch.Properties.Resources.药品横线;
            this.Label__.Location = new System.Drawing.Point(10, 0);
            this.Label__.Name = "Label__";
            this.Label__.Size = new System.Drawing.Size(451, 1);
            this.Label__.TabIndex = 9;
            this.Label__.Text = "label1";
            // 
            // Label_Batch
            // 
            this.Label_Batch.BackColor = System.Drawing.Color.Yellow;
            this.Label_Batch.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Batch.Image = global::PivasBatch.Properties.Resources.批次;
            this.Label_Batch.Location = new System.Drawing.Point(134, 2);
            this.Label_Batch.Name = "Label_Batch";
            this.Label_Batch.Size = new System.Drawing.Size(37, 20);
            this.Label_Batch.TabIndex = 5;
            this.Label_Batch.Text = "1批";
            this.Label_Batch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PatientDetailBatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Panel_DrugInfo);
            this.Controls.Add(this.Panel_Batch);
            this.Name = "PatientDetailBatch";
            this.Size = new System.Drawing.Size(475, 150);
            this.Panel_Batch.ResumeLayout(false);
            this.Panel_Batch.PerformLayout();
            this.Panel_DrugInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel Panel_Batch;
        public System.Windows.Forms.Panel Label__;
        public System.Windows.Forms.Label Label_InsertDt;
        public System.Windows.Forms.Label Label_GroupNo;
        public System.Windows.Forms.Label Label_LabelNo;
        public System.Windows.Forms.Label Label_Batch;
        public System.Windows.Forms.Label Label_BatchSaved;
        public System.Windows.Forms.Label Label_FreqName;
        public System.Windows.Forms.Panel Panel_DrugInfo;
        public System.Windows.Forms.Label Panel_DrugInfo_DrugSpec;
        public System.Windows.Forms.Label Panel_DrugInfo_DrugDosage;
        public System.Windows.Forms.Label Panel_DrugInfo_DrugName;
    }
}
