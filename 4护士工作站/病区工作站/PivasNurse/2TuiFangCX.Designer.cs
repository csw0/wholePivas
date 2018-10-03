namespace PivasNurse
{
    partial class TuiFangCX
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dgvPre = new System.Windows.Forms.DataGridView();
            this.Picture = new System.Windows.Forms.DataGridViewImageColumn();
            this.BedNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Doctor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Explain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DoctorOperate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BPR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PreCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BPRecordID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cancelDrugInfo1 = new PivasNurse.CancelDrugInfo();
            this.label5 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.cb1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPre)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(9, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 16);
            this.label7.TabIndex = 408;
            this.label7.Text = "处方日期：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(-5, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(531, 1);
            this.label1.TabIndex = 403;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(182)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(2, 375);
            this.label3.TabIndex = 515;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(79, 16);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(91, 21);
            this.dateTimePicker1.TabIndex = 516;
            this.dateTimePicker1.CloseUp += new System.EventHandler(this.dateTimePicker1_CloseUp);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dgvPre
            // 
            this.dgvPre.AllowUserToAddRows = false;
            this.dgvPre.AllowUserToResizeRows = false;
            this.dgvPre.BackgroundColor = System.Drawing.Color.White;
            this.dgvPre.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPre.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvPre.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPre.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Picture,
            this.BedNo,
            this.PatName,
            this.Doctor,
            this.GroupNo,
            this.PStatus,
            this.Explain,
            this.DoctorOperate,
            this.BPR,
            this.PreCode,
            this.BPRecordID});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPre.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPre.GridColor = System.Drawing.SystemColors.Control;
            this.dgvPre.Location = new System.Drawing.Point(0, 0);
            this.dgvPre.MultiSelect = false;
            this.dgvPre.Name = "dgvPre";
            this.dgvPre.ReadOnly = true;
            this.dgvPre.RowHeadersVisible = false;
            this.dgvPre.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            this.dgvPre.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPre.RowTemplate.Height = 23;
            this.dgvPre.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPre.Size = new System.Drawing.Size(497, 375);
            this.dgvPre.TabIndex = 523;
            this.dgvPre.Sorted += new System.EventHandler(this.dgvPre_Sorted);
            this.dgvPre.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPre_CellClick);
            // 
            // Picture
            // 
            this.Picture.HeaderText = "";
            this.Picture.Name = "Picture";
            this.Picture.ReadOnly = true;
            this.Picture.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Picture.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Picture.Visible = false;
            this.Picture.Width = 20;
            // 
            // BedNo
            // 
            this.BedNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BedNo.DataPropertyName = "BedNo";
            this.BedNo.FillWeight = 18.24788F;
            this.BedNo.HeaderText = "床号";
            this.BedNo.Name = "BedNo";
            this.BedNo.ReadOnly = true;
            // 
            // PatName
            // 
            this.PatName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PatName.DataPropertyName = "PatName";
            this.PatName.FillWeight = 18.24788F;
            this.PatName.HeaderText = "姓名";
            this.PatName.Name = "PatName";
            this.PatName.ReadOnly = true;
            // 
            // Doctor
            // 
            this.Doctor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Doctor.DataPropertyName = "DEmployeeName";
            this.Doctor.FillWeight = 18.24788F;
            this.Doctor.HeaderText = "医师";
            this.Doctor.Name = "Doctor";
            this.Doctor.ReadOnly = true;
            this.Doctor.Visible = false;
            // 
            // GroupNo
            // 
            this.GroupNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.GroupNo.DataPropertyName = "GroupNo";
            this.GroupNo.FillWeight = 33.74107F;
            this.GroupNo.HeaderText = "分组";
            this.GroupNo.Name = "GroupNo";
            this.GroupNo.ReadOnly = true;
            // 
            // PStatus
            // 
            this.PStatus.DataPropertyName = "PStatus";
            this.PStatus.HeaderText = "处方状态";
            this.PStatus.Name = "PStatus";
            this.PStatus.ReadOnly = true;
            // 
            // Explain
            // 
            this.Explain.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Explain.DataPropertyName = "BPExplain";
            this.Explain.FillWeight = 54.74364F;
            this.Explain.HeaderText = "医师意见";
            this.Explain.Name = "Explain";
            this.Explain.ReadOnly = true;
            this.Explain.Visible = false;
            // 
            // DoctorOperate
            // 
            this.DoctorOperate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DoctorOperate.DataPropertyName = "doperate";
            this.DoctorOperate.FillWeight = 26.77165F;
            this.DoctorOperate.HeaderText = "病区处理";
            this.DoctorOperate.Name = "DoctorOperate";
            this.DoctorOperate.ReadOnly = true;
            // 
            // BPR
            // 
            this.BPR.DataPropertyName = "Read";
            this.BPR.HeaderText = "阅读状态";
            this.BPR.Name = "BPR";
            this.BPR.ReadOnly = true;
            this.BPR.Visible = false;
            this.BPR.Width = 70;
            // 
            // PreCode
            // 
            this.PreCode.DataPropertyName = "PrescriptionID";
            this.PreCode.HeaderText = "处方号";
            this.PreCode.Name = "PreCode";
            this.PreCode.ReadOnly = true;
            this.PreCode.Visible = false;
            // 
            // BPRecordID
            // 
            this.BPRecordID.DataPropertyName = "BPRecordID";
            this.BPRecordID.HeaderText = "退方记录ID";
            this.BPRecordID.Name = "BPRecordID";
            this.BPRecordID.ReadOnly = true;
            this.BPRecordID.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(1, 48);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel1.Controls.Add(this.dgvPre);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cancelDrugInfo1);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Size = new System.Drawing.Size(886, 375);
            this.splitContainer1.SplitterDistance = 497;
            this.splitContainer1.TabIndex = 525;
            // 
            // cancelDrugInfo1
            // 
            this.cancelDrugInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cancelDrugInfo1.Location = new System.Drawing.Point(2, 2);
            this.cancelDrugInfo1.Name = "cancelDrugInfo1";
            this.cancelDrugInfo1.Size = new System.Drawing.Size(383, 373);
            this.cancelDrugInfo1.TabIndex = 526;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(182)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(383, 2);
            this.label5.TabIndex = 525;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.checkBox1);
            this.flowLayoutPanel1.Controls.Add(this.checkBox2);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(361, 14);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(145, 24);
            this.flowLayoutPanel1.TabIndex = 528;
            this.flowLayoutPanel1.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(3, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(60, 16);
            this.checkBox1.TabIndex = 526;
            this.checkBox1.Text = "未查阅";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(69, 3);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(60, 16);
            this.checkBox2.TabIndex = 527;
            this.checkBox2.Text = "已查阅";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(529, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 33);
            this.label2.TabIndex = 525;
            this.label2.Text = "说明：您在此画面可以看到所有静配中心药师操作过的医嘱。";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cb1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(888, 48);
            this.panel1.TabIndex = 526;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(855, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 18);
            this.label6.TabIndex = 531;
            this.label6.Text = "×";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // cb1
            // 
            this.cb1.FormattingEnabled = true;
            this.cb1.Items.AddRange(new object[] {
            "全部",
            "人工退方",
            "人工通过"});
            this.cb1.Location = new System.Drawing.Point(224, 16);
            this.cb1.Name = "cb1";
            this.cb1.Size = new System.Drawing.Size(99, 20);
            this.cb1.TabIndex = 529;
            this.cb1.SelectionChangeCommitted += new System.EventHandler(this.cb1_SelectionChangeCommitted);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(186, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 530;
            this.label4.Text = "状态：";
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "图标";
            this.dataGridViewImageColumn1.Image = global::PivasNurse.Properties.Resources._1;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Width = 20;
            // 
            // TuiFangCX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(214)))), ((int)(((byte)(243)))));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "TuiFangCX";
            this.Size = new System.Drawing.Size(888, 424);
            this.Load += new System.EventHandler(this.TuiFangCX_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPre)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.DataGridView dgvPre;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cb1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private CancelDrugInfo cancelDrugInfo1;
        private System.Windows.Forms.DataGridViewImageColumn Picture;
        private System.Windows.Forms.DataGridViewTextBoxColumn BedNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Doctor;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Explain;
        private System.Windows.Forms.DataGridViewTextBoxColumn DoctorOperate;
        private System.Windows.Forms.DataGridViewTextBoxColumn BPR;
        private System.Windows.Forms.DataGridViewTextBoxColumn PreCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn BPRecordID;
    }
}
