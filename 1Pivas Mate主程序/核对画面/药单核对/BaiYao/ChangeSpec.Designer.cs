namespace BaiYaoCheck
{
    partial class ChangeSpec
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.PatCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BedNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WardName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecipeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrugCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrugName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PatCode,
            this.PatName,
            this.BedNo,
            this.WardName,
            this.GroupNo,
            this.RecipeNo,
            this.DrugCode,
            this.DrugName,
            this.Spec,
            this.Spec1});
            this.dataGridView1.Location = new System.Drawing.Point(6, 1);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(884, 446);
            this.dataGridView1.TabIndex = 0;
            // 
            // PatCode
            // 
            this.PatCode.DataPropertyName = "PatCode";
            this.PatCode.HeaderText = "病人编码";
            this.PatCode.Name = "PatCode";
            this.PatCode.ReadOnly = true;
            this.PatCode.Visible = false;
            // 
            // PatName
            // 
            this.PatName.DataPropertyName = "PatName";
            this.PatName.HeaderText = "患者姓名";
            this.PatName.Name = "PatName";
            this.PatName.ReadOnly = true;
            // 
            // BedNo
            // 
            this.BedNo.DataPropertyName = "BedNo";
            this.BedNo.HeaderText = "床号";
            this.BedNo.Name = "BedNo";
            this.BedNo.ReadOnly = true;
            this.BedNo.Width = 60;
            // 
            // WardName
            // 
            this.WardName.DataPropertyName = "WardName";
            this.WardName.HeaderText = "病区名称";
            this.WardName.Name = "WardName";
            this.WardName.ReadOnly = true;
            this.WardName.Width = 150;
            // 
            // GroupNo
            // 
            this.GroupNo.DataPropertyName = "GroupNo";
            this.GroupNo.HeaderText = "组号";
            this.GroupNo.Name = "GroupNo";
            this.GroupNo.ReadOnly = true;
            // 
            // RecipeNo
            // 
            this.RecipeNo.DataPropertyName = "RecipeNo";
            this.RecipeNo.HeaderText = "药单号";
            this.RecipeNo.Name = "RecipeNo";
            this.RecipeNo.ReadOnly = true;
            this.RecipeNo.Visible = false;
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
            this.DrugName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DrugName.DataPropertyName = "DrugName";
            this.DrugName.HeaderText = "药品名称";
            this.DrugName.Name = "DrugName";
            this.DrugName.ReadOnly = true;
            // 
            // Spec
            // 
            this.Spec.DataPropertyName = "Spec";
            this.Spec.HeaderText = "药品规格";
            this.Spec.Name = "Spec";
            this.Spec.ReadOnly = true;
            this.Spec.Width = 80;
            // 
            // Spec1
            // 
            this.Spec1.DataPropertyName = "Spec1";
            this.Spec1.HeaderText = "摆药规格";
            this.Spec1.Name = "Spec1";
            this.Spec1.ReadOnly = true;
            this.Spec1.Width = 80;
            // 
            // ChangeSpec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Name = "ChangeSpec";
            this.Size = new System.Drawing.Size(893, 452);
            this.Load += new System.EventHandler(this.ChangeSpec_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BedNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn WardName;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecipeNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec1;
    }
}
