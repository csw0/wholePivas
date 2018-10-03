namespace ItemDamageCount
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvdrug = new System.Windows.Forms.DataGridView();
            this.cbSpec = new System.Windows.Forms.ComboBox();
            this.lblWarn = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lblDamagetime = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.cbReason = new System.Windows.Forms.ComboBox();
            this.txtReport = new System.Windows.Forms.TextBox();
            this.txtRespons = new System.Windows.Forms.TextBox();
            this.lblReport = new System.Windows.Forms.Label();
            this.lblResponsible = new System.Windows.Forms.Label();
            this.lblReason = new System.Windows.Forms.Label();
            this.txtMoney = new System.Windows.Forms.TextBox();
            this.lblMoney = new System.Windows.Forms.Label();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblSpec = new System.Windows.Forms.Label();
            this.txtDrug = new System.Windows.Forms.TextBox();
            this.lblDrug = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvemployee = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DRUGCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DRUGNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SPEC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COUNT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MONEY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Responsibilityer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RESPONSIBILITYID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reporter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REPORTID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DAMAGETIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvdrug)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvemployee)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgvdrug);
            this.groupBox1.Controls.Add(this.cbSpec);
            this.groupBox1.Controls.Add(this.lblWarn);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.lblDamagetime);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.cbReason);
            this.groupBox1.Controls.Add(this.txtReport);
            this.groupBox1.Controls.Add(this.txtRespons);
            this.groupBox1.Controls.Add(this.lblReport);
            this.groupBox1.Controls.Add(this.lblResponsible);
            this.groupBox1.Controls.Add(this.lblReason);
            this.groupBox1.Controls.Add(this.txtMoney);
            this.groupBox1.Controls.Add(this.lblMoney);
            this.groupBox1.Controls.Add(this.txtCount);
            this.groupBox1.Controls.Add(this.lblCount);
            this.groupBox1.Controls.Add(this.lblSpec);
            this.groupBox1.Controls.Add(this.txtDrug);
            this.groupBox1.Controls.Add(this.lblDrug);
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(738, 214);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作区";
            // 
            // dgvdrug
            // 
            this.dgvdrug.AllowUserToAddRows = false;
            this.dgvdrug.AllowUserToDeleteRows = false;
            this.dgvdrug.AllowUserToOrderColumns = true;
            this.dgvdrug.AllowUserToResizeColumns = false;
            this.dgvdrug.AllowUserToResizeRows = false;
            this.dgvdrug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvdrug.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvdrug.BackgroundColor = System.Drawing.Color.White;
            this.dgvdrug.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvdrug.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvdrug.ColumnHeadersVisible = false;
            this.dgvdrug.GridColor = System.Drawing.Color.White;
            this.dgvdrug.Location = new System.Drawing.Point(86, 50);
            this.dgvdrug.MultiSelect = false;
            this.dgvdrug.Name = "dgvdrug";
            this.dgvdrug.ReadOnly = true;
            this.dgvdrug.RowHeadersVisible = false;
            this.dgvdrug.RowTemplate.Height = 23;
            this.dgvdrug.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvdrug.Size = new System.Drawing.Size(188, 108);
            this.dgvdrug.TabIndex = 22;
            this.dgvdrug.Visible = false;
            this.dgvdrug.DoubleClick += new System.EventHandler(this.dgvdrug_DoubleClick);
            // 
            // cbSpec
            // 
            this.cbSpec.FormattingEnabled = true;
            this.cbSpec.Location = new System.Drawing.Point(350, 29);
            this.cbSpec.Name = "cbSpec";
            this.cbSpec.Size = new System.Drawing.Size(121, 20);
            this.cbSpec.TabIndex = 20;
            // 
            // lblWarn
            // 
            this.lblWarn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWarn.AutoSize = true;
            this.lblWarn.Location = new System.Drawing.Point(635, 190);
            this.lblWarn.Name = "lblWarn";
            this.lblWarn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblWarn.Size = new System.Drawing.Size(0, 12);
            this.lblWarn.TabIndex = 19;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(652, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "添加纪录";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblDamagetime
            // 
            this.lblDamagetime.AutoSize = true;
            this.lblDamagetime.Location = new System.Drawing.Point(15, 175);
            this.lblDamagetime.Name = "lblDamagetime";
            this.lblDamagetime.Size = new System.Drawing.Size(65, 12);
            this.lblDamagetime.TabIndex = 16;
            this.lblDamagetime.Text = "损坏时间：";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(86, 169);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(130, 21);
            this.dateTimePicker1.TabIndex = 15;
            // 
            // cbReason
            // 
            this.cbReason.FormattingEnabled = true;
            this.cbReason.Items.AddRange(new object[] {
            "打碎",
            "小车损坏",
            "停药误配",
            "漏液",
            "其他"});
            this.cbReason.Location = new System.Drawing.Point(350, 75);
            this.cbReason.Name = "cbReason";
            this.cbReason.Size = new System.Drawing.Size(121, 20);
            this.cbReason.TabIndex = 14;
            // 
            // txtReport
            // 
            this.txtReport.Location = new System.Drawing.Point(350, 124);
            this.txtReport.Name = "txtReport";
            this.txtReport.Size = new System.Drawing.Size(100, 21);
            this.txtReport.TabIndex = 13;
            this.txtReport.TextChanged += new System.EventHandler(this.txtReport_TextChanged);
            // 
            // txtRespons
            // 
            this.txtRespons.Location = new System.Drawing.Point(86, 124);
            this.txtRespons.Name = "txtRespons";
            this.txtRespons.Size = new System.Drawing.Size(100, 21);
            this.txtRespons.TabIndex = 12;
            this.txtRespons.TextChanged += new System.EventHandler(this.txtRespons_TextChanged);
            // 
            // lblReport
            // 
            this.lblReport.AutoSize = true;
            this.lblReport.Location = new System.Drawing.Point(291, 127);
            this.lblReport.Name = "lblReport";
            this.lblReport.Size = new System.Drawing.Size(53, 12);
            this.lblReport.TabIndex = 11;
            this.lblReport.Text = "填报人：";
            // 
            // lblResponsible
            // 
            this.lblResponsible.AutoSize = true;
            this.lblResponsible.Location = new System.Drawing.Point(27, 127);
            this.lblResponsible.Name = "lblResponsible";
            this.lblResponsible.Size = new System.Drawing.Size(53, 12);
            this.lblResponsible.TabIndex = 10;
            this.lblResponsible.Text = "责任人：";
            // 
            // lblReason
            // 
            this.lblReason.AutoSize = true;
            this.lblReason.Location = new System.Drawing.Point(279, 78);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(65, 12);
            this.lblReason.TabIndex = 8;
            this.lblReason.Text = "报损原因：";
            // 
            // txtMoney
            // 
            this.txtMoney.Location = new System.Drawing.Point(86, 75);
            this.txtMoney.Name = "txtMoney";
            this.txtMoney.Size = new System.Drawing.Size(100, 21);
            this.txtMoney.TabIndex = 7;
            this.txtMoney.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMoney_KeyPress);
            // 
            // lblMoney
            // 
            this.lblMoney.AutoSize = true;
            this.lblMoney.Location = new System.Drawing.Point(39, 78);
            this.lblMoney.Name = "lblMoney";
            this.lblMoney.Size = new System.Drawing.Size(41, 12);
            this.lblMoney.TabIndex = 6;
            this.lblMoney.Text = "金额：";
            // 
            // txtCount
            // 
            this.txtCount.Location = new System.Drawing.Point(536, 29);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(100, 21);
            this.txtCount.TabIndex = 5;
            this.txtCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCount_KeyPress);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(489, 32);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(41, 12);
            this.lblCount.TabIndex = 4;
            this.lblCount.Text = "数量：";
            // 
            // lblSpec
            // 
            this.lblSpec.AutoSize = true;
            this.lblSpec.Location = new System.Drawing.Point(303, 32);
            this.lblSpec.Name = "lblSpec";
            this.lblSpec.Size = new System.Drawing.Size(41, 12);
            this.lblSpec.TabIndex = 2;
            this.lblSpec.Text = "规格：";
            // 
            // txtDrug
            // 
            this.txtDrug.Location = new System.Drawing.Point(86, 29);
            this.txtDrug.Name = "txtDrug";
            this.txtDrug.Size = new System.Drawing.Size(188, 21);
            this.txtDrug.TabIndex = 1;
            this.txtDrug.TextChanged += new System.EventHandler(this.txtDrug_TextChanged);
            // 
            // lblDrug
            // 
            this.lblDrug.AutoSize = true;
            this.lblDrug.Location = new System.Drawing.Point(38, 32);
            this.lblDrug.Name = "lblDrug";
            this.lblDrug.Size = new System.Drawing.Size(41, 12);
            this.lblDrug.TabIndex = 0;
            this.lblDrug.Text = "药品：";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(654, 528);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "删  除";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.LightCyan;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.DRUGCODE,
            this.DRUGNAME,
            this.SPEC,
            this.COUNT,
            this.MONEY,
            this.Reason,
            this.Responsibilityer,
            this.RESPONSIBILITYID,
            this.Reporter,
            this.REPORTID,
            this.DAMAGETIME,
            this.DATE});
            this.dataGridView1.Location = new System.Drawing.Point(2, 218);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(737, 306);
            this.dataGridView1.TabIndex = 1;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(4, 528);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(87, 21);
            this.dateTimePicker2.TabIndex = 2;
            this.dateTimePicker2.Value = new System.DateTime(2016, 7, 21, 0, 0, 0, 0);
            this.dateTimePicker2.CloseUp += new System.EventHandler(this.dateTimePicker2_CloseUp);
            // 
            // dateTimePicker3
            // 
            this.dateTimePicker3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker3.Location = new System.Drawing.Point(116, 528);
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker3.Size = new System.Drawing.Size(83, 21);
            this.dateTimePicker3.TabIndex = 19;
            this.dateTimePicker3.CloseUp += new System.EventHandler(this.dateTimePicker3_CloseUp);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(97, 533);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "-";
            // 
            // dgvemployee
            // 
            this.dgvemployee.AllowUserToAddRows = false;
            this.dgvemployee.AllowUserToDeleteRows = false;
            this.dgvemployee.AllowUserToResizeColumns = false;
            this.dgvemployee.AllowUserToResizeRows = false;
            this.dgvemployee.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvemployee.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvemployee.BackgroundColor = System.Drawing.Color.White;
            this.dgvemployee.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvemployee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvemployee.ColumnHeadersVisible = false;
            this.dgvemployee.GridColor = System.Drawing.Color.White;
            this.dgvemployee.Location = new System.Drawing.Point(352, 148);
            this.dgvemployee.MultiSelect = false;
            this.dgvemployee.Name = "dgvemployee";
            this.dgvemployee.ReadOnly = true;
            this.dgvemployee.RowHeadersVisible = false;
            this.dgvemployee.RowTemplate.Height = 23;
            this.dgvemployee.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvemployee.Size = new System.Drawing.Size(100, 108);
            this.dgvemployee.TabIndex = 23;
            this.dgvemployee.Visible = false;
            this.dgvemployee.DoubleClick += new System.EventHandler(this.dgvemployee_DoubleClick);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // DRUGCODE
            // 
            this.DRUGCODE.DataPropertyName = "Drugcode";
            this.DRUGCODE.HeaderText = "药品代码";
            this.DRUGCODE.Name = "DRUGCODE";
            this.DRUGCODE.ReadOnly = true;
            this.DRUGCODE.Visible = false;
            // 
            // DRUGNAME
            // 
            this.DRUGNAME.DataPropertyName = "Drugname";
            this.DRUGNAME.HeaderText = "药品名称";
            this.DRUGNAME.Name = "DRUGNAME";
            this.DRUGNAME.ReadOnly = true;
            // 
            // SPEC
            // 
            this.SPEC.DataPropertyName = "Spec";
            this.SPEC.HeaderText = "规格";
            this.SPEC.Name = "SPEC";
            this.SPEC.ReadOnly = true;
            // 
            // COUNT
            // 
            this.COUNT.DataPropertyName = "Count";
            this.COUNT.HeaderText = "数量";
            this.COUNT.Name = "COUNT";
            this.COUNT.ReadOnly = true;
            // 
            // MONEY
            // 
            this.MONEY.DataPropertyName = "Money";
            this.MONEY.HeaderText = "金额";
            this.MONEY.Name = "MONEY";
            this.MONEY.ReadOnly = true;
            // 
            // Reason
            // 
            this.Reason.DataPropertyName = "Reason";
            this.Reason.HeaderText = "报损原因";
            this.Reason.Name = "Reason";
            this.Reason.ReadOnly = true;
            // 
            // Responsibilityer
            // 
            this.Responsibilityer.DataPropertyName = "Responsibilityer";
            this.Responsibilityer.HeaderText = "责任人";
            this.Responsibilityer.Name = "Responsibilityer";
            this.Responsibilityer.ReadOnly = true;
            // 
            // RESPONSIBILITYID
            // 
            this.RESPONSIBILITYID.DataPropertyName = "Responsibilityid";
            this.RESPONSIBILITYID.HeaderText = "责任人ID";
            this.RESPONSIBILITYID.Name = "RESPONSIBILITYID";
            this.RESPONSIBILITYID.ReadOnly = true;
            this.RESPONSIBILITYID.Visible = false;
            // 
            // Reporter
            // 
            this.Reporter.DataPropertyName = "Reporter";
            this.Reporter.HeaderText = "填报人";
            this.Reporter.Name = "Reporter";
            this.Reporter.ReadOnly = true;
            // 
            // REPORTID
            // 
            this.REPORTID.DataPropertyName = "Reportid";
            this.REPORTID.HeaderText = "填报人ID";
            this.REPORTID.Name = "REPORTID";
            this.REPORTID.ReadOnly = true;
            this.REPORTID.Visible = false;
            // 
            // DAMAGETIME
            // 
            this.DAMAGETIME.DataPropertyName = "Damagetime";
            this.DAMAGETIME.HeaderText = "损坏时间";
            this.DAMAGETIME.Name = "DAMAGETIME";
            this.DAMAGETIME.ReadOnly = true;
            // 
            // DATE
            // 
            this.DATE.DataPropertyName = "Date";
            this.DATE.HeaderText = "填报日期";
            this.DATE.Name = "DATE";
            this.DATE.ReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 552);
            this.Controls.Add(this.dgvemployee);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker3);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "药物报损";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvdrug)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvemployee)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtMoney;
        private System.Windows.Forms.Label lblMoney;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblSpec;
        private System.Windows.Forms.TextBox txtDrug;
        private System.Windows.Forms.Label lblDrug;
        private System.Windows.Forms.TextBox txtRespons;
        private System.Windows.Forms.Label lblReport;
        private System.Windows.Forms.Label lblResponsible;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.Label lblDamagetime;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox cbReason;
        private System.Windows.Forms.TextBox txtReport;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblWarn;
        private System.Windows.Forms.ComboBox cbSpec;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker3;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DataGridView dgvdrug;
        public System.Windows.Forms.DataGridView dgvemployee;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DRUGCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DRUGNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn SPEC;
        private System.Windows.Forms.DataGridViewTextBoxColumn COUNT;
        private System.Windows.Forms.DataGridViewTextBoxColumn MONEY;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reason;
        private System.Windows.Forms.DataGridViewTextBoxColumn Responsibilityer;
        private System.Windows.Forms.DataGridViewTextBoxColumn RESPONSIBILITYID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reporter;
        private System.Windows.Forms.DataGridViewTextBoxColumn REPORTID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DAMAGETIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATE;
    }
}

