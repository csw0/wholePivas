namespace DrugListConform
{
    partial class CompareTime
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
            this.dgvDruglist = new System.Windows.Forms.DataGridView();
            this.GroupNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecipeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FregCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrugCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrugName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.occdt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartDt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndDt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDruglist)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDruglist
            // 
            this.dgvDruglist.AllowUserToAddRows = false;
            this.dgvDruglist.AllowUserToDeleteRows = false;
            this.dgvDruglist.AllowUserToResizeRows = false;
            this.dgvDruglist.BackgroundColor = System.Drawing.Color.White;
            this.dgvDruglist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDruglist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GroupNo,
            this.RecipeID,
            this.FregCode,
            this.DrugCode,
            this.DrugName,
            this.Spec,
            this.occdt,
            this.StartDt,
            this.EndDt});
            this.dgvDruglist.Location = new System.Drawing.Point(0, 1);
            this.dgvDruglist.Name = "dgvDruglist";
            this.dgvDruglist.ReadOnly = true;
            this.dgvDruglist.RowHeadersVisible = false;
            this.dgvDruglist.RowTemplate.Height = 23;
            this.dgvDruglist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDruglist.Size = new System.Drawing.Size(733, 564);
            this.dgvDruglist.TabIndex = 45;
            this.dgvDruglist.DoubleClick += new System.EventHandler(this.dgvDruglist_DoubleClick);
            // 
            // GroupNo
            // 
            this.GroupNo.DataPropertyName = "GroupNo";
            this.GroupNo.HeaderText = "组号";
            this.GroupNo.Name = "GroupNo";
            this.GroupNo.ReadOnly = true;
            // 
            // RecipeID
            // 
            this.RecipeID.DataPropertyName = "RecipeID";
            this.RecipeID.HeaderText = "药单号";
            this.RecipeID.Name = "RecipeID";
            this.RecipeID.ReadOnly = true;
            this.RecipeID.Width = 140;
            // 
            // FregCode
            // 
            this.FregCode.DataPropertyName = "FregCode";
            this.FregCode.HeaderText = "频次";
            this.FregCode.Name = "FregCode";
            this.FregCode.ReadOnly = true;
            this.FregCode.Width = 80;
            // 
            // DrugCode
            // 
            this.DrugCode.DataPropertyName = "DrugCode";
            this.DrugCode.HeaderText = "药品编码";
            this.DrugCode.Name = "DrugCode";
            this.DrugCode.ReadOnly = true;
            this.DrugCode.Visible = false;
            // 
            // DrugName
            // 
            this.DrugName.DataPropertyName = "DrugName";
            this.DrugName.HeaderText = "药品名称";
            this.DrugName.Name = "DrugName";
            this.DrugName.ReadOnly = true;
            // 
            // Spec
            // 
            this.Spec.DataPropertyName = "Spec";
            this.Spec.HeaderText = "规格";
            this.Spec.Name = "Spec";
            this.Spec.ReadOnly = true;
            this.Spec.Visible = false;
            this.Spec.Width = 80;
            // 
            // occdt
            // 
            this.occdt.DataPropertyName = "OccDT";
            this.occdt.HeaderText = "使用时间 ";
            this.occdt.Name = "occdt";
            this.occdt.ReadOnly = true;
            this.occdt.Width = 120;
            // 
            // StartDt
            // 
            this.StartDt.DataPropertyName = "begindt";
            this.StartDt.HeaderText = "开始时间";
            this.StartDt.Name = "StartDt";
            this.StartDt.ReadOnly = true;
            this.StartDt.Width = 120;
            // 
            // EndDt
            // 
            this.EndDt.DataPropertyName = "enddt";
            this.EndDt.FillWeight = 15F;
            this.EndDt.HeaderText = "结束时间";
            this.EndDt.Name = "EndDt";
            this.EndDt.ReadOnly = true;
            this.EndDt.Width = 120;
            // 
            // CompareTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Controls.Add(this.dgvDruglist);
            this.Name = "CompareTime";
            this.Size = new System.Drawing.Size(734, 567);
            this.Load += new System.EventHandler(this.CompareTime_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDruglist)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDruglist;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecipeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FregCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn occdt;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartDt;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndDt;
    }
}
