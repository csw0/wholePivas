namespace PivasNurse
{
    partial class ShenFangCL
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dgvPre = new System.Windows.Forms.DataGridView();
            this.BedNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Doctor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Explain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PreCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPRecordID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatientCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CDoctorCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DoctorCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.information1 = new PivasNurse.Information();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPre)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(-2, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(503, 1);
            this.label1.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(15, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "处方状态：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.SystemColors.Window;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "全部",
            "系统通过",
            "系统未过",
            "强制执行",
            "接受退单",
            "人工退单"});
            this.comboBox1.Location = new System.Drawing.Point(86, 10);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(141, 20);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
            this.comboBox1.Click += new System.EventHandler(this.comboBox1_Click);
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
            this.GroupNo,
            this.Doctor,
            this.state,
            this.Explain,
            this.PreCode,
            this.CPRecordID,
            this.PatientCode,
            this.CDoctorCode,
            this.DoctorCode});
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
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dgvPre.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPre.RowTemplate.Height = 23;
            this.dgvPre.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPre.Size = new System.Drawing.Size(504, 393);
            this.dgvPre.TabIndex = 351;
            this.dgvPre.SelectionChanged += new System.EventHandler(this.dgvPre_SelectionChanged);
            this.dgvPre.Click += new System.EventHandler(this.dgvPre_Click);
            // 
            // BedNo
            // 
            this.BedNo.DataPropertyName = "BedNo";
            this.BedNo.FillWeight = 10F;
            this.BedNo.HeaderText = "床号";
            this.BedNo.Name = "BedNo";
            this.BedNo.ReadOnly = true;
            this.BedNo.Width = 50;
            // 
            // PatName
            // 
            this.PatName.DataPropertyName = "PatName";
            this.PatName.FillWeight = 15F;
            this.PatName.HeaderText = "姓名";
            this.PatName.Name = "PatName";
            this.PatName.ReadOnly = true;
            this.PatName.Width = 80;
            // 
            // GroupNo
            // 
            this.GroupNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.GroupNo.DataPropertyName = "GroupNo";
            this.GroupNo.FillWeight = 10F;
            this.GroupNo.HeaderText = "分组";
            this.GroupNo.Name = "GroupNo";
            this.GroupNo.ReadOnly = true;
            this.GroupNo.Width = 90;
            // 
            // Doctor
            // 
            this.Doctor.DataPropertyName = "DEmployeeName";
            this.Doctor.FillWeight = 10F;
            this.Doctor.HeaderText = "医师";
            this.Doctor.Name = "Doctor";
            this.Doctor.ReadOnly = true;
            this.Doctor.Width = 80;
            // 
            // state
            // 
            this.state.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.state.DataPropertyName = "state";
            this.state.FillWeight = 15F;
            this.state.HeaderText = "状态";
            this.state.Name = "state";
            this.state.ReadOnly = true;
            // 
            // Explain
            // 
            this.Explain.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Explain.DataPropertyName = "DoctorExplain";
            this.Explain.FillWeight = 30F;
            this.Explain.HeaderText = "药师意见";
            this.Explain.Name = "Explain";
            this.Explain.ReadOnly = true;
            // 
            // PreCode
            // 
            this.PreCode.DataPropertyName = "PrescriptionID";
            this.PreCode.HeaderText = "处方号";
            this.PreCode.Name = "PreCode";
            this.PreCode.ReadOnly = true;
            this.PreCode.Visible = false;
            // 
            // CPRecordID
            // 
            this.CPRecordID.DataPropertyName = "CPRecordID";
            this.CPRecordID.HeaderText = "记录ID";
            this.CPRecordID.Name = "CPRecordID";
            this.CPRecordID.ReadOnly = true;
            this.CPRecordID.Visible = false;
            // 
            // PatientCode
            // 
            this.PatientCode.DataPropertyName = "PatientCode";
            this.PatientCode.HeaderText = "PatientCode";
            this.PatientCode.Name = "PatientCode";
            this.PatientCode.ReadOnly = true;
            this.PatientCode.Visible = false;
            // 
            // CDoctorCode
            // 
            this.CDoctorCode.DataPropertyName = "CDoctorCode";
            this.CDoctorCode.HeaderText = "CDoctorCode";
            this.CDoctorCode.Name = "CDoctorCode";
            this.CDoctorCode.ReadOnly = true;
            this.CDoctorCode.Visible = false;
            // 
            // DoctorCode
            // 
            this.DoctorCode.DataPropertyName = "DoctorCode";
            this.DoctorCode.HeaderText = "DoctorCode";
            this.DoctorCode.Name = "DoctorCode";
            this.DoctorCode.ReadOnly = true;
            this.DoctorCode.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(182)))));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 40);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvPre);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.information1);
            this.splitContainer1.Size = new System.Drawing.Size(886, 393);
            this.splitContainer1.SplitterDistance = 504;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 353;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(182)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(380, 2);
            this.label4.TabIndex = 354;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(179)))), ((int)(((byte)(253)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(179)))), ((int)(((byte)(253)))));
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(179)))), ((int)(((byte)(253)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(193)))), ((int)(((byte)(254)))));
            this.button1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(247, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 32);
            this.button1.TabIndex = 243;
            this.button1.Text = "医师意见";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(214)))), ((int)(((byte)(243)))));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(888, 40);
            this.panel1.TabIndex = 354;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(550, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 12);
            this.label2.TabIndex = 596;
            this.label2.Text = "label2";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(179)))), ((int)(((byte)(253)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(724, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(152, 31);
            this.button2.TabIndex = 595;
            this.button2.Text = "药师审方结果查询";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // information1
            // 
            this.information1.BackColor = System.Drawing.Color.White;
            this.information1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.information1.Location = new System.Drawing.Point(0, 0);
            this.information1.Name = "information1";
            this.information1.Size = new System.Drawing.Size(380, 393);
            this.information1.TabIndex = 352;
            // 
            // ShenFangCL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(246)))));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ShenFangCL";
            this.Size = new System.Drawing.Size(888, 433);
            this.Load += new System.EventHandler(this.ShenFangCL_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPre)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox1;
        private PivasNurse.Information information1;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.DataGridView dgvPre;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn BedNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Doctor;
        private System.Windows.Forms.DataGridViewTextBoxColumn state;
        private System.Windows.Forms.DataGridViewTextBoxColumn Explain;
        private System.Windows.Forms.DataGridViewTextBoxColumn PreCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPRecordID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatientCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CDoctorCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn DoctorCode;
    }
}
