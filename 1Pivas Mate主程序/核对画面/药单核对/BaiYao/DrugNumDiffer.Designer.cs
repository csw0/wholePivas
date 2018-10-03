namespace BaiYaoCheck
{
    partial class DrugNumDiffer
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
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.RecipeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrugListID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrugName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QuantityUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrugCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrugCountUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv1
            // 
            this.dgv1.AllowUserToAddRows = false;
            this.dgv1.AllowUserToDeleteRows = false;
            this.dgv1.AllowUserToResizeRows = false;
            this.dgv1.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgv1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.RecipeID,
            this.GroupNo,
            this.DrugListID,
            this.DrugName,
            this.Spec,
            this.Quantity,
            this.QuantityUnit,
            this.DrugCount,
            this.DrugCountUnit});
            this.dgv1.Location = new System.Drawing.Point(3, 2);
            this.dgv1.Name = "dgv1";
            this.dgv1.RowHeadersVisible = false;
            this.dgv1.RowTemplate.Height = 23;
            this.dgv1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv1.Size = new System.Drawing.Size(653, 341);
            this.dgv1.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(662, 100);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 28);
            this.button1.TabIndex = 14;
            this.button1.Text = "通过";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(662, 177);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 29);
            this.button2.TabIndex = 15;
            this.button2.Text = "确认";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Select
            // 
            this.Select.HeaderText = " ";
            this.Select.Name = "Select";
            this.Select.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Select.Width = 25;
            // 
            // RecipeID
            // 
            this.RecipeID.HeaderText = "医嘱号";
            this.RecipeID.Name = "RecipeID";
            this.RecipeID.Visible = false;
            // 
            // GroupNo
            // 
            this.GroupNo.HeaderText = "同组标识";
            this.GroupNo.Name = "GroupNo";
            // 
            // DrugListID
            // 
            this.DrugListID.HeaderText = "药单识别";
            this.DrugListID.Name = "DrugListID";
            this.DrugListID.Visible = false;
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
            // Quantity
            // 
            this.Quantity.HeaderText = "摆药数";
            this.Quantity.Name = "Quantity";
            this.Quantity.Width = 70;
            // 
            // QuantityUnit
            // 
            this.QuantityUnit.HeaderText = "摆药单位";
            this.QuantityUnit.Name = "QuantityUnit";
            this.QuantityUnit.ReadOnly = true;
            this.QuantityUnit.Width = 80;
            // 
            // DrugCount
            // 
            this.DrugCount.HeaderText = "HIS药单";
            this.DrugCount.Name = "DrugCount";
            this.DrugCount.Width = 80;
            // 
            // DrugCountUnit
            // 
            this.DrugCountUnit.HeaderText = "HIS单位";
            this.DrugCountUnit.Name = "DrugCountUnit";
            this.DrugCountUnit.Visible = false;
            this.DrugCountUnit.Width = 80;
            // 
            // DrugNumDiffer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgv1);
            this.Name = "DrugNumDiffer";
            this.Size = new System.Drawing.Size(754, 344);
            this.Load += new System.EventHandler(this.DrugNumDiffer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecipeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugListID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuantityUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugCountUnit;
    }
}
