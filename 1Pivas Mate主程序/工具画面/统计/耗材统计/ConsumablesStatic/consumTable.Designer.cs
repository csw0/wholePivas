namespace ConsumablesStatic
{
    partial class consumTable
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ConsumablesCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConsumablesName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ConsumablesCode,
            this.ConsumablesName,
            this.Total,
            this.PType,
            this.KType,
            this.HType,
            this.YType});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(634, 493);
            this.dataGridView1.TabIndex = 8;
            // 
            // ConsumablesCode
            // 
            this.ConsumablesCode.DataPropertyName = "耗材编号";
            this.ConsumablesCode.HeaderText = "耗材编号";
            this.ConsumablesCode.Name = "ConsumablesCode";
            this.ConsumablesCode.ReadOnly = true;
            this.ConsumablesCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ConsumablesCode.Visible = false;
            // 
            // ConsumablesName
            // 
            this.ConsumablesName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ConsumablesName.DataPropertyName = "耗材名称";
            this.ConsumablesName.FillWeight = 200F;
            this.ConsumablesName.HeaderText = "耗材名称";
            this.ConsumablesName.Name = "ConsumablesName";
            this.ConsumablesName.ReadOnly = true;
            this.ConsumablesName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Total
            // 
            this.Total.DataPropertyName = "总计";
            this.Total.HeaderText = "总计";
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            this.Total.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Total.Width = 80;
            // 
            // PType
            // 
            this.PType.DataPropertyName = "普通药";
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.PType.DefaultCellStyle = dataGridViewCellStyle1;
            this.PType.HeaderText = "普通药";
            this.PType.Name = "PType";
            this.PType.ReadOnly = true;
            this.PType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PType.Width = 80;
            // 
            // KType
            // 
            this.KType.DataPropertyName = "抗生素";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.KType.DefaultCellStyle = dataGridViewCellStyle2;
            this.KType.HeaderText = "抗生素";
            this.KType.Name = "KType";
            this.KType.ReadOnly = true;
            this.KType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.KType.Width = 80;
            // 
            // HType
            // 
            this.HType.DataPropertyName = "化疗药";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.HType.DefaultCellStyle = dataGridViewCellStyle3;
            this.HType.HeaderText = "化疗药";
            this.HType.Name = "HType";
            this.HType.ReadOnly = true;
            this.HType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.HType.Width = 80;
            // 
            // YType
            // 
            this.YType.DataPropertyName = "营养药";
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.YType.DefaultCellStyle = dataGridViewCellStyle4;
            this.YType.HeaderText = "营养药";
            this.YType.Name = "YType";
            this.YType.ReadOnly = true;
            this.YType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.YType.Width = 80;
            // 
            // consumTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Name = "consumTable";
            this.Size = new System.Drawing.Size(634, 493);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConsumablesCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConsumablesName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn PType;
        private System.Windows.Forms.DataGridViewTextBoxColumn KType;
        private System.Windows.Forms.DataGridViewTextBoxColumn HType;
        private System.Windows.Forms.DataGridViewTextBoxColumn YType;
    }
}
