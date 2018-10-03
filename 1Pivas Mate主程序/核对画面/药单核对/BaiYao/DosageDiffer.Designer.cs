namespace BaiYaoCheck
{
    partial class DosageDiffer
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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.QueRen = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DrugName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dosage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DosageUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QuantityUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.cb1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.QueRen,
            this.DrugName,
            this.Spec,
            this.Dosage,
            this.DosageUnit,
            this.Quantity,
            this.QuantityUnit});
            this.dgv.Location = new System.Drawing.Point(3, 2);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(617, 340);
            this.dgv.TabIndex = 5;
            // 
            // QueRen
            // 
            this.QueRen.HeaderText = " ";
            this.QueRen.Name = "QueRen";
            this.QueRen.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.QueRen.Width = 25;
            // 
            // DrugName
            // 
            this.DrugName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DrugName.HeaderText = "药品名称";
            this.DrugName.Name = "DrugName";
            // 
            // Spec
            // 
            this.Spec.HeaderText = "规格";
            this.Spec.Name = "Spec";
            // 
            // Dosage
            // 
            this.Dosage.HeaderText = "剂量";
            this.Dosage.Name = "Dosage";
            this.Dosage.Width = 80;
            // 
            // DosageUnit
            // 
            this.DosageUnit.HeaderText = "剂量单位";
            this.DosageUnit.Name = "DosageUnit";
            this.DosageUnit.Visible = false;
            this.DosageUnit.Width = 80;
            // 
            // Quantity
            // 
            this.Quantity.HeaderText = "摆药数量";
            this.Quantity.Name = "Quantity";
            this.Quantity.Width = 80;
            // 
            // QuantityUnit
            // 
            this.QuantityUnit.HeaderText = "数量单位";
            this.QuantityUnit.Name = "QuantityUnit";
            this.QuantityUnit.ReadOnly = true;
            this.QuantityUnit.Width = 80;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(649, 95);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 29);
            this.button1.TabIndex = 6;
            this.button1.Text = "确认";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(649, 215);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 29);
            this.button3.TabIndex = 8;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // cb1
            // 
            this.cb1.AutoSize = true;
            this.cb1.Location = new System.Drawing.Point(8, 5);
            this.cb1.Name = "cb1";
            this.cb1.Size = new System.Drawing.Size(15, 14);
            this.cb1.TabIndex = 9;
            this.cb1.UseVisualStyleBackColor = true;
            // 
            // DosageDiffer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cb1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgv);
            this.Name = "DosageDiffer";
            this.Size = new System.Drawing.Size(754, 344);
            this.Load += new System.EventHandler(this.DosageDiffer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewCheckBoxColumn QueRen;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dosage;
        private System.Windows.Forms.DataGridViewTextBoxColumn DosageUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuantityUnit;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox cb1;
    }
}
