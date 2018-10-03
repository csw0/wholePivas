namespace BaiYaoCheck
{
    partial class ChangeDate
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
            this.occdt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartDt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FregCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrugCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrugName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndDt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvPatient = new System.Windows.Forms.DataGridView();
            this.PatientCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BedNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvWard = new System.Windows.Forms.DataGridView();
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.WardCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WardName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDruglist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWard)).BeginInit();
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
            this.occdt,
            this.StartDt,
            this.FregCode,
            this.DrugCode,
            this.DrugName,
            this.Spec,
            this.EndDt});
            this.dgvDruglist.Location = new System.Drawing.Point(288, 0);
            this.dgvDruglist.Name = "dgvDruglist";
            this.dgvDruglist.ReadOnly = true;
            this.dgvDruglist.RowHeadersVisible = false;
            this.dgvDruglist.RowTemplate.Height = 23;
            this.dgvDruglist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDruglist.Size = new System.Drawing.Size(605, 451);
            this.dgvDruglist.TabIndex = 46;
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
            // EndDt
            // 
            this.EndDt.DataPropertyName = "enddt";
            this.EndDt.FillWeight = 15F;
            this.EndDt.HeaderText = "结束时间";
            this.EndDt.Name = "EndDt";
            this.EndDt.ReadOnly = true;
            this.EndDt.Width = 120;
            // 
            // dgvPatient
            // 
            this.dgvPatient.AllowUserToAddRows = false;
            this.dgvPatient.AllowUserToDeleteRows = false;
            this.dgvPatient.AllowUserToResizeRows = false;
            this.dgvPatient.BackgroundColor = System.Drawing.Color.White;
            this.dgvPatient.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvPatient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPatient.ColumnHeadersVisible = false;
            this.dgvPatient.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PatientCode,
            this.PatName,
            this.BedNo,
            this.Type});
            this.dgvPatient.Location = new System.Drawing.Point(188, 0);
            this.dgvPatient.Name = "dgvPatient";
            this.dgvPatient.RowHeadersVisible = false;
            this.dgvPatient.RowTemplate.Height = 23;
            this.dgvPatient.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPatient.Size = new System.Drawing.Size(100, 451);
            this.dgvPatient.TabIndex = 52;
            this.dgvPatient.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPatient_CellContentClick);
            // 
            // PatientCode
            // 
            this.PatientCode.DataPropertyName = "PatientCode";
            this.PatientCode.HeaderText = "病人编号";
            this.PatientCode.Name = "PatientCode";
            this.PatientCode.ReadOnly = true;
            this.PatientCode.Visible = false;
            this.PatientCode.Width = 80;
            // 
            // PatName
            // 
            this.PatName.DataPropertyName = "PatName";
            this.PatName.HeaderText = "患者姓名";
            this.PatName.Name = "PatName";
            this.PatName.ReadOnly = true;
            this.PatName.Width = 80;
            // 
            // BedNo
            // 
            this.BedNo.DataPropertyName = "BedNo";
            this.BedNo.HeaderText = "床号";
            this.BedNo.Name = "BedNo";
            this.BedNo.ReadOnly = true;
            this.BedNo.Visible = false;
            this.BedNo.Width = 60;
            // 
            // Type
            // 
            this.Type.DataPropertyName = "a";
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Visible = false;
            // 
            // dgvWard
            // 
            this.dgvWard.AllowUserToAddRows = false;
            this.dgvWard.AllowUserToDeleteRows = false;
            this.dgvWard.AllowUserToResizeRows = false;
            this.dgvWard.BackgroundColor = System.Drawing.Color.White;
            this.dgvWard.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvWard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWard.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check,
            this.WardCode,
            this.WardName,
            this.Count});
            this.dgvWard.Location = new System.Drawing.Point(1, 0);
            this.dgvWard.Name = "dgvWard";
            this.dgvWard.RowHeadersVisible = false;
            this.dgvWard.RowTemplate.Height = 23;
            this.dgvWard.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWard.Size = new System.Drawing.Size(187, 451);
            this.dgvWard.TabIndex = 51;
            this.dgvWard.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWard_CellContentClick);
            // 
            // check
            // 
            this.check.DataPropertyName = "check";
            this.check.FalseValue = "False";
            this.check.HeaderText = "";
            this.check.Name = "check";
            this.check.Width = 20;
            // 
            // WardCode
            // 
            this.WardCode.DataPropertyName = "WardCode";
            this.WardCode.HeaderText = "病区号";
            this.WardCode.Name = "WardCode";
            this.WardCode.ReadOnly = true;
            this.WardCode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.WardCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.WardCode.Visible = false;
            // 
            // WardName
            // 
            this.WardName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.WardName.DataPropertyName = "WardName";
            this.WardName.HeaderText = "病区名";
            this.WardName.Name = "WardName";
            this.WardName.ReadOnly = true;
            this.WardName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.WardName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Count
            // 
            this.Count.DataPropertyName = "Count";
            this.Count.HeaderText = "数量";
            this.Count.Name = "Count";
            this.Count.ReadOnly = true;
            this.Count.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Count.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Count.Width = 50;
            // 
            // ChangeDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvPatient);
            this.Controls.Add(this.dgvWard);
            this.Controls.Add(this.dgvDruglist);
            this.Name = "ChangeDate";
            this.Size = new System.Drawing.Size(893, 452);
            this.Load += new System.EventHandler(this.ChangeDate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDruglist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDruglist;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecipeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn occdt;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartDt;
        private System.Windows.Forms.DataGridViewTextBoxColumn FregCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndDt;
        private System.Windows.Forms.DataGridView dgvPatient;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatientCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BedNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridView dgvWard;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
        private System.Windows.Forms.DataGridViewTextBoxColumn WardCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn WardName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;

    }
}
