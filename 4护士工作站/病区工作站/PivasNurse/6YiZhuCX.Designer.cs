namespace PivasNurse
{
    partial class YiZhuCX
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvPre = new System.Windows.Forms.DataGridView();
            this.dgvDrug = new System.Windows.Forms.DataGridView();
            this.PrescriptionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FreqName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrugName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dosage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UniPreparationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label12 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cb1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.BedNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Age = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatientCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPRecordID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrug)).BeginInit();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.ForeColor = System.Drawing.Color.Silver;
            this.textBox1.Location = new System.Drawing.Point(54, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(140, 21);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "姓名/床号";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "搜索：";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Location = new System.Drawing.Point(0, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(799, 1);
            this.label6.TabIndex = 581;
            // 
            // dgvPre
            // 
            this.dgvPre.AllowUserToAddRows = false;
            this.dgvPre.AllowUserToResizeRows = false;
            this.dgvPre.BackgroundColor = System.Drawing.Color.White;
            this.dgvPre.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPre.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPre.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPre.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPre.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BedNo,
            this.PatName,
            this.Sex,
            this.Age,
            this.PatientCode,
            this.CPRecordID});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPre.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPre.GridColor = System.Drawing.Color.White;
            this.dgvPre.Location = new System.Drawing.Point(0, 0);
            this.dgvPre.MultiSelect = false;
            this.dgvPre.Name = "dgvPre";
            this.dgvPre.ReadOnly = true;
            this.dgvPre.RowHeadersVisible = false;
            this.dgvPre.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            this.dgvPre.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPre.RowTemplate.Height = 23;
            this.dgvPre.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvPre.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPre.Size = new System.Drawing.Size(291, 396);
            this.dgvPre.TabIndex = 588;
            this.dgvPre.SelectionChanged += new System.EventHandler(this.dgvPre_SelectionChanged);
            this.dgvPre.Click += new System.EventHandler(this.dgvPre_Click);
            // 
            // dgvDrug
            // 
            this.dgvDrug.AllowUserToAddRows = false;
            this.dgvDrug.AllowUserToDeleteRows = false;
            this.dgvDrug.AllowUserToResizeRows = false;
            this.dgvDrug.BackgroundColor = System.Drawing.Color.White;
            this.dgvDrug.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDrug.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDrug.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDrug.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDrug.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PrescriptionID,
            this.FreqName,
            this.DrugName,
            this.Dosage,
            this.Spec,
            this.PStatus,
            this.UniPreparationID});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDrug.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDrug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDrug.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(246)))));
            this.dgvDrug.Location = new System.Drawing.Point(0, 0);
            this.dgvDrug.MultiSelect = false;
            this.dgvDrug.Name = "dgvDrug";
            this.dgvDrug.ReadOnly = true;
            this.dgvDrug.RowHeadersVisible = false;
            this.dgvDrug.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(246)))));
            this.dgvDrug.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvDrug.RowTemplate.Height = 23;
            this.dgvDrug.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDrug.Size = new System.Drawing.Size(509, 396);
            this.dgvDrug.TabIndex = 589;
            this.dgvDrug.DoubleClick += new System.EventHandler(this.dgvDrug_DoubleClick);
            this.dgvDrug.Click += new System.EventHandler(this.dgvDrug_Click);
            // 
            // PrescriptionID
            // 
            this.PrescriptionID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PrescriptionID.FillWeight = 9.445975F;
            this.PrescriptionID.HeaderText = "处方号";
            this.PrescriptionID.Name = "PrescriptionID";
            this.PrescriptionID.ReadOnly = true;
            // 
            // FreqName
            // 
            this.FreqName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FreqName.FillWeight = 11.87817F;
            this.FreqName.HeaderText = "频次";
            this.FreqName.Name = "FreqName";
            this.FreqName.ReadOnly = true;
            // 
            // DrugName
            // 
            this.DrugName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DrugName.FillWeight = 28.33793F;
            this.DrugName.HeaderText = "药品";
            this.DrugName.Name = "DrugName";
            this.DrugName.ReadOnly = true;
            // 
            // Dosage
            // 
            this.Dosage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Dosage.FillWeight = 9.445975F;
            this.Dosage.HeaderText = "用量";
            this.Dosage.Name = "Dosage";
            this.Dosage.ReadOnly = true;
            // 
            // Spec
            // 
            this.Spec.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Spec.FillWeight = 9.445975F;
            this.Spec.HeaderText = "规格";
            this.Spec.Name = "Spec";
            this.Spec.ReadOnly = true;
            // 
            // PStatus
            // 
            this.PStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PStatus.FillWeight = 9.445975F;
            this.PStatus.HeaderText = "状态";
            this.PStatus.Name = "PStatus";
            this.PStatus.ReadOnly = true;
            // 
            // UniPreparationID
            // 
            this.UniPreparationID.HeaderText = "UniPreparationID";
            this.UniPreparationID.Name = "UniPreparationID";
            this.UniPreparationID.ReadOnly = true;
            this.UniPreparationID.Visible = false;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(209, 14);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 592;
            this.label12.Text = "状态：";
            // 
            // comboBox2
            // 
            this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "<全部>",
            "执行中",
            "已暂停",
            "已停止"});
            this.comboBox2.Location = new System.Drawing.Point(247, 9);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(96, 20);
            this.comboBox2.TabIndex = 593;
            this.comboBox2.Text = "执行中";
            this.comboBox2.SelectionChangeCommitted += new System.EventHandler(this.comboBox2_SelectionChangeCommitted);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(214)))), ((int)(((byte)(243)))));
            this.panel1.Controls.Add(this.cb1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.comboBox2);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(799, 40);
            this.panel1.TabIndex = 594;
            // 
            // cb1
            // 
            this.cb1.AutoSize = true;
            this.cb1.Checked = true;
            this.cb1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb1.Location = new System.Drawing.Point(385, 12);
            this.cb1.Name = "cb1";
            this.cb1.Size = new System.Drawing.Size(84, 16);
            this.cb1.TabIndex = 596;
            this.cb1.Text = "今日有药单";
            this.cb1.UseVisualStyleBackColor = true;
            this.cb1.Click += new System.EventHandler(this.cb1_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(182)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(799, 2);
            this.label1.TabIndex = 595;
            this.label1.Text = "label1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(182)))));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 39);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvPre);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvDrug);
            this.splitContainer1.Size = new System.Drawing.Size(802, 396);
            this.splitContainer1.SplitterDistance = 291;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 595;
            // 
            // BedNo
            // 
            this.BedNo.HeaderText = "床号";
            this.BedNo.Name = "BedNo";
            this.BedNo.ReadOnly = true;
            this.BedNo.Width = 45;
            // 
            // PatName
            // 
            this.PatName.HeaderText = "姓名";
            this.PatName.Name = "PatName";
            this.PatName.ReadOnly = true;
            this.PatName.Width = 60;
            // 
            // Sex
            // 
            this.Sex.HeaderText = "性别";
            this.Sex.Name = "Sex";
            this.Sex.ReadOnly = true;
            this.Sex.Width = 45;
            // 
            // Age
            // 
            this.Age.HeaderText = "年龄";
            this.Age.Name = "Age";
            this.Age.ReadOnly = true;
            this.Age.Width = 45;
            // 
            // PatientCode
            // 
            this.PatientCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PatientCode.HeaderText = "住院号";
            this.PatientCode.Name = "PatientCode";
            this.PatientCode.ReadOnly = true;
            // 
            // CPRecordID
            // 
            this.CPRecordID.HeaderText = "记录ID";
            this.CPRecordID.Name = "CPRecordID";
            this.CPRecordID.ReadOnly = true;
            this.CPRecordID.Visible = false;
            // 
            // YiZhuCX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(246)))));
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "YiZhuCX";
            this.Size = new System.Drawing.Size(799, 435);
            this.Load += new System.EventHandler(this.YiZhuCX_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrug)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.DataGridView dgvPre;
        public System.Windows.Forms.DataGridView dgvDrug;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrescriptionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FreqName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dosage;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn PStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn UniPreparationID;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox cb1;
        private System.Windows.Forms.DataGridViewTextBoxColumn BedNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sex;
        private System.Windows.Forms.DataGridViewTextBoxColumn Age;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatientCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPRecordID;
    }
}
