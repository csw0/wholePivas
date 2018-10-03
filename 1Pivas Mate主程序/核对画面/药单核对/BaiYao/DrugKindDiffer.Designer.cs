namespace BaiYaoCheck
{
    partial class DrugKindDiffer
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
            this.dgv2 = new System.Windows.Forms.DataGridView();
            this.dgv3 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.a = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.WardCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WardName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BedNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrugCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrugCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrugName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrugCode1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrugName1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv3)).BeginInit();
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
            this.a,
            this.WardCode,
            this.WardName,
            this.PatCode,
            this.patName,
            this.BedNo,
            this.GroupNo,
            this.Quantity,
            this.DrugCount});
            this.dgv1.Location = new System.Drawing.Point(0, 0);
            this.dgv1.Name = "dgv1";
            this.dgv1.ReadOnly = true;
            this.dgv1.RowHeadersVisible = false;
            this.dgv1.RowTemplate.Height = 23;
            this.dgv1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv1.Size = new System.Drawing.Size(251, 451);
            this.dgv1.TabIndex = 16;
            this.dgv1.Click += new System.EventHandler(this.dgv1_Click);
            // 
            // dgv2
            // 
            this.dgv2.AllowUserToAddRows = false;
            this.dgv2.AllowUserToDeleteRows = false;
            this.dgv2.AllowUserToResizeRows = false;
            this.dgv2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv2.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgv2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DrugCode1,
            this.DrugName1,
            this.Spec1});
            this.dgv2.Location = new System.Drawing.Point(251, 282);
            this.dgv2.Name = "dgv2";
            this.dgv2.RowHeadersVisible = false;
            this.dgv2.RowTemplate.Height = 23;
            this.dgv2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv2.Size = new System.Drawing.Size(642, 170);
            this.dgv2.TabIndex = 19;
            // 
            // dgv3
            // 
            this.dgv3.AllowUserToAddRows = false;
            this.dgv3.AllowUserToDeleteRows = false;
            this.dgv3.AllowUserToResizeRows = false;
            this.dgv3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv3.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgv3.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DrugCode,
            this.DrugName,
            this.Spec});
            this.dgv3.Location = new System.Drawing.Point(251, 106);
            this.dgv3.Name = "dgv3";
            this.dgv3.RowHeadersVisible = false;
            this.dgv3.RowTemplate.Height = 23;
            this.dgv3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv3.Size = new System.Drawing.Size(641, 170);
            this.dgv3.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(275, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.TabIndex = 21;
            this.label1.Text = "姓名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(277, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 22;
            this.label2.Text = "床号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(277, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 23;
            this.label3.Text = "病区";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(537, 9);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 24;
            this.checkBox1.Text = "过滤";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(346, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 25;
            this.label4.Text = "编号";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(530, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(161, 12);
            this.label5.TabIndex = 26;
            this.label5.Text = "（去掉有医嘱没药单的情况）";
            // 
            // a
            // 
            this.a.HeaderText = " ";
            this.a.Name = "a";
            this.a.ReadOnly = true;
            this.a.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.a.Visible = false;
            this.a.Width = 25;
            // 
            // WardCode
            // 
            this.WardCode.HeaderText = "病区编号";
            this.WardCode.Name = "WardCode";
            this.WardCode.ReadOnly = true;
            this.WardCode.Visible = false;
            // 
            // WardName
            // 
            this.WardName.HeaderText = "病区名称";
            this.WardName.Name = "WardName";
            this.WardName.ReadOnly = true;
            this.WardName.Visible = false;
            this.WardName.Width = 150;
            // 
            // PatCode
            // 
            this.PatCode.HeaderText = "病人编号 ";
            this.PatCode.Name = "PatCode";
            this.PatCode.ReadOnly = true;
            this.PatCode.Visible = false;
            // 
            // patName
            // 
            this.patName.HeaderText = "病人姓名 ";
            this.patName.Name = "patName";
            this.patName.ReadOnly = true;
            this.patName.Visible = false;
            this.patName.Width = 80;
            // 
            // BedNo
            // 
            this.BedNo.HeaderText = "床号";
            this.BedNo.Name = "BedNo";
            this.BedNo.ReadOnly = true;
            this.BedNo.Visible = false;
            this.BedNo.Width = 80;
            // 
            // GroupNo
            // 
            this.GroupNo.HeaderText = "组号";
            this.GroupNo.Name = "GroupNo";
            this.GroupNo.ReadOnly = true;
            this.GroupNo.Width = 80;
            // 
            // Quantity
            // 
            this.Quantity.HeaderText = "医嘱种类";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            this.Quantity.Width = 80;
            // 
            // DrugCount
            // 
            this.DrugCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DrugCount.HeaderText = "HIS种类";
            this.DrugCount.Name = "DrugCount";
            this.DrugCount.ReadOnly = true;
            // 
            // DrugCode
            // 
            this.DrugCode.DataPropertyName = "DrugCode";
            this.DrugCode.FillWeight = 23.60406F;
            this.DrugCode.HeaderText = "医嘱药品编码";
            this.DrugCode.Name = "DrugCode";
            this.DrugCode.ReadOnly = true;
            this.DrugCode.Width = 150;
            // 
            // DrugName
            // 
            this.DrugName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DrugName.DataPropertyName = "DrugName";
            this.DrugName.FillWeight = 23.60406F;
            this.DrugName.HeaderText = "医嘱药品名称";
            this.DrugName.Name = "DrugName";
            this.DrugName.ReadOnly = true;
            // 
            // Spec
            // 
            this.Spec.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Spec.DataPropertyName = "Spec";
            this.Spec.FillWeight = 252.7919F;
            this.Spec.HeaderText = "医嘱药品规格";
            this.Spec.Name = "Spec";
            this.Spec.ReadOnly = true;
            this.Spec.Width = 120;
            // 
            // DrugCode1
            // 
            this.DrugCode1.DataPropertyName = "DrugCode";
            this.DrugCode1.HeaderText = "摆药药品编码";
            this.DrugCode1.Name = "DrugCode1";
            this.DrugCode1.ReadOnly = true;
            this.DrugCode1.Width = 150;
            // 
            // DrugName1
            // 
            this.DrugName1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DrugName1.DataPropertyName = "DrugName";
            this.DrugName1.HeaderText = "摆药药品名称";
            this.DrugName1.Name = "DrugName1";
            this.DrugName1.ReadOnly = true;
            // 
            // Spec1
            // 
            this.Spec1.DataPropertyName = "Spec";
            this.Spec1.HeaderText = "摆药药品规格";
            this.Spec1.Name = "Spec1";
            this.Spec1.ReadOnly = true;
            this.Spec1.Width = 120;
            // 
            // DrugKindDiffer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgv3);
            this.Controls.Add(this.dgv2);
            this.Controls.Add(this.dgv1);
            this.Name = "DrugKindDiffer";
            this.Size = new System.Drawing.Size(893, 452);
            this.Load += new System.EventHandler(this.DrugKindDiffer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv1;
        private System.Windows.Forms.DataGridView dgv2;
        private System.Windows.Forms.DataGridView dgv3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn a;
        private System.Windows.Forms.DataGridViewTextBoxColumn WardCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn WardName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn patName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BedNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugCode1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugName1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec;
    }
}
