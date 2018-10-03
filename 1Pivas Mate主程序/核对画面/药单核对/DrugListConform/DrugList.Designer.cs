namespace DrugListConform
{
    partial class DrugList
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.lbDate = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvWard = new System.Windows.Forms.DataGridView();
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.WardCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WardName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvPatient = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.PatientCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BedNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatient)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(210)))), ((int)(((byte)(251)))));
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.lbDate);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(0, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1047, 76);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // textBox1
            // 
            this.textBox1.ForeColor = System.Drawing.Color.Silver;
            this.textBox1.Location = new System.Drawing.Point(317, 51);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(147, 21);
            this.textBox1.TabIndex = 1026;
            this.textBox1.Text = "姓名";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(1019, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 19);
            this.label3.TabIndex = 1025;
            this.label3.Text = "×";
            this.label3.MouseLeave += new System.EventHandler(this.label3_MouseLeave);
            this.label3.Click += new System.EventHandler(this.label3_Click);
            this.label3.MouseHover += new System.EventHandler(this.label3_MouseHover);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(177, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 1021;
            this.label9.Text = "管理";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1018;
            this.label1.Text = "当前日期：";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(218, 55);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 1020;
            this.checkBox1.Text = "全部";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // lbDate
            // 
            this.lbDate.AutoSize = true;
            this.lbDate.Location = new System.Drawing.Point(83, 56);
            this.lbDate.Name = "lbDate";
            this.lbDate.Size = new System.Drawing.Size(65, 12);
            this.lbDate.TabIndex = 1019;
            this.lbDate.Text = "2014-06-24";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(10, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(390, 33);
            this.label2.TabIndex = 1017;
            this.label2.Text = "药单确认";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.dgvWard.Location = new System.Drawing.Point(1, 74);
            this.dgvWard.Name = "dgvWard";
            this.dgvWard.RowHeadersVisible = false;
            this.dgvWard.RowTemplate.Height = 23;
            this.dgvWard.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWard.Size = new System.Drawing.Size(206, 582);
            this.dgvWard.TabIndex = 42;
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
            this.dgvPatient.Location = new System.Drawing.Point(207, 75);
            this.dgvPatient.Name = "dgvPatient";
            this.dgvPatient.RowHeadersVisible = false;
            this.dgvPatient.RowTemplate.Height = 23;
            this.dgvPatient.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPatient.Size = new System.Drawing.Size(110, 581);
            this.dgvPatient.TabIndex = 43;
            this.dgvPatient.Click += new System.EventHandler(this.dgvPatient_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(317, 75);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(730, 581);
            this.panel2.TabIndex = 44;
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
            // DrugList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(1048, 657);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dgvPatient);
            this.Controls.Add(this.dgvWard);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DrugList";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.DrugList_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatient)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label lbDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvWard;
        private System.Windows.Forms.DataGridView dgvPatient;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
        private System.Windows.Forms.DataGridViewTextBoxColumn WardCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn WardName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatientCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BedNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;

    }
}

