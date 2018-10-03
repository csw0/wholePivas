namespace PivasNurse
{
    partial class LabelNoSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LabelNoSearch));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvPre = new System.Windows.Forms.DataGridView();
            this.IsQianShou = new System.Windows.Forms.DataGridViewImageColumn();
            this.LabelNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BedNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Batch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FreqCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IVStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PreCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrueStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPRecordID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.informationQS1 = new PivasNurse.InformationQS();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ftp = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cb3 = new System.Windows.Forms.CheckBox();
            this.cb4 = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cb1 = new System.Windows.Forms.CheckBox();
            this.cb2 = new System.Windows.Forms.CheckBox();
            this.dtp = new System.Windows.Forms.DateTimePicker();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rb2 = new System.Windows.Forms.RadioButton();
            this.rb1 = new System.Windows.Forms.RadioButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel5 = new System.Windows.Forms.Panel();
            this.previewQD = new FastReport.Preview.PreviewControl();
            this.report = new FastReport.Report();
            this.button1 = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPre)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.report)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(182)))));
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvPre);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.informationQS1);
            this.splitContainer1.Size = new System.Drawing.Size(886, 361);
            this.splitContainer1.SplitterDistance = 501;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 587;
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
            this.IsQianShou,
            this.LabelNo,
            this.BedNo,
            this.PatName,
            this.Batch,
            this.FreqCode,
            this.IVStatus,
            this.PreCode,
            this.TrueStatus,
            this.CPRecordID});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvPre.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPre.RowTemplate.Height = 23;
            this.dgvPre.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPre.Size = new System.Drawing.Size(501, 361);
            this.dgvPre.TabIndex = 580;
            this.dgvPre.DoubleClick += new System.EventHandler(this.dgvPre_DoubleClick);
            this.dgvPre.SelectionChanged += new System.EventHandler(this.dgvPre_SelectionChanged);
            this.dgvPre.Click += new System.EventHandler(this.dgvPre_Click);
            // 
            // IsQianShou
            // 
            this.IsQianShou.FillWeight = 5F;
            this.IsQianShou.Frozen = true;
            this.IsQianShou.HeaderText = "";
            this.IsQianShou.Name = "IsQianShou";
            this.IsQianShou.ReadOnly = true;
            this.IsQianShou.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsQianShou.Visible = false;
            this.IsQianShou.Width = 20;
            // 
            // LabelNo
            // 
            this.LabelNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LabelNo.FillWeight = 15F;
            this.LabelNo.HeaderText = "瓶签号";
            this.LabelNo.Name = "LabelNo";
            this.LabelNo.ReadOnly = true;
            // 
            // BedNo
            // 
            this.BedNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BedNo.FillWeight = 5F;
            this.BedNo.HeaderText = "床号";
            this.BedNo.Name = "BedNo";
            this.BedNo.ReadOnly = true;
            // 
            // PatName
            // 
            this.PatName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PatName.FillWeight = 10F;
            this.PatName.HeaderText = "姓名";
            this.PatName.Name = "PatName";
            this.PatName.ReadOnly = true;
            // 
            // Batch
            // 
            this.Batch.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Batch.FillWeight = 15F;
            this.Batch.HeaderText = "批次";
            this.Batch.Name = "Batch";
            this.Batch.ReadOnly = true;
            this.Batch.Width = 50;
            // 
            // FreqCode
            // 
            this.FreqCode.FillWeight = 10F;
            this.FreqCode.HeaderText = "频次";
            this.FreqCode.Name = "FreqCode";
            this.FreqCode.ReadOnly = true;
            // 
            // IVStatus
            // 
            this.IVStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IVStatus.FillWeight = 10F;
            this.IVStatus.HeaderText = "状态";
            this.IVStatus.Name = "IVStatus";
            this.IVStatus.ReadOnly = true;
            // 
            // PreCode
            // 
            this.PreCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            this.PreCode.HeaderText = "处方号";
            this.PreCode.Name = "PreCode";
            this.PreCode.ReadOnly = true;
            this.PreCode.Visible = false;
            // 
            // TrueStatus
            // 
            this.TrueStatus.HeaderText = "状态真";
            this.TrueStatus.Name = "TrueStatus";
            this.TrueStatus.ReadOnly = true;
            this.TrueStatus.Visible = false;
            // 
            // CPRecordID
            // 
            this.CPRecordID.HeaderText = "记录ID";
            this.CPRecordID.Name = "CPRecordID";
            this.CPRecordID.ReadOnly = true;
            this.CPRecordID.Visible = false;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(182)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Location = new System.Drawing.Point(0, 359);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(381, 2);
            this.label6.TabIndex = 589;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(381, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(2, 361);
            this.label2.TabIndex = 587;
            // 
            // informationQS1
            // 
            this.informationQS1.AutoScroll = true;
            this.informationQS1.AutoSize = true;
            this.informationQS1.BackColor = System.Drawing.Color.White;
            this.informationQS1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.informationQS1.Location = new System.Drawing.Point(0, 0);
            this.informationQS1.Name = "informationQS1";
            this.informationQS1.Size = new System.Drawing.Size(383, 361);
            this.informationQS1.TabIndex = 585;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.ftp);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.dtp);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(888, 33);
            this.panel1.TabIndex = 588;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(825, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(48, 25);
            this.button2.TabIndex = 611;
            this.button2.Text = "刷新";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(659, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 14);
            this.label3.TabIndex = 610;
            this.label3.Text = "状态";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label12.Location = new System.Drawing.Point(798, 11);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(23, 12);
            this.label12.TabIndex = 589;
            this.label12.Text = "120";
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "全部",
            "已打印",
            "已打包",
            "已签收",
            "提前打包",
            "已退药",
            "配置取消",
            ""});
            this.comboBox1.Location = new System.Drawing.Point(694, 7);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(95, 20);
            this.comboBox1.TabIndex = 609;
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(17, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 14);
            this.label8.TabIndex = 608;
            this.label8.Text = "日期";
            // 
            // ftp
            // 
            this.ftp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ftp.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ftp.Location = new System.Drawing.Point(220, 6);
            this.ftp.Name = "ftp";
            this.ftp.Size = new System.Drawing.Size(231, 23);
            this.ftp.TabIndex = 605;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(183, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 14);
            this.label1.TabIndex = 607;
            this.label1.Text = "批次";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel3.Controls.Add(this.cb3);
            this.panel3.Controls.Add(this.cb4);
            this.panel3.Location = new System.Drawing.Point(546, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(81, 23);
            this.panel3.TabIndex = 601;
            // 
            // cb3
            // 
            this.cb3.AutoSize = true;
            this.cb3.Checked = true;
            this.cb3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb3.Location = new System.Drawing.Point(3, 4);
            this.cb3.Name = "cb3";
            this.cb3.Size = new System.Drawing.Size(30, 16);
            this.cb3.TabIndex = 1;
            this.cb3.Text = "#";
            this.cb3.UseVisualStyleBackColor = true;
            this.cb3.CheckedChanged += new System.EventHandler(this.cb3_CheckedChanged);
            // 
            // cb4
            // 
            this.cb4.AutoSize = true;
            this.cb4.Checked = true;
            this.cb4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb4.Location = new System.Drawing.Point(47, 4);
            this.cb4.Name = "cb4";
            this.cb4.Size = new System.Drawing.Size(30, 16);
            this.cb4.TabIndex = 0;
            this.cb4.Text = "K";
            this.cb4.UseVisualStyleBackColor = true;
            this.cb4.CheckedChanged += new System.EventHandler(this.cb4_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel2.Controls.Add(this.cb1);
            this.panel2.Controls.Add(this.cb2);
            this.panel2.Location = new System.Drawing.Point(461, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(81, 23);
            this.panel2.TabIndex = 604;
            // 
            // cb1
            // 
            this.cb1.AutoSize = true;
            this.cb1.Checked = true;
            this.cb1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb1.Location = new System.Drawing.Point(3, 4);
            this.cb1.Name = "cb1";
            this.cb1.Size = new System.Drawing.Size(36, 16);
            this.cb1.TabIndex = 0;
            this.cb1.Text = "长";
            this.cb1.UseVisualStyleBackColor = true;
            this.cb1.CheckedChanged += new System.EventHandler(this.cb1_CheckedChanged);
            // 
            // cb2
            // 
            this.cb2.AllowDrop = true;
            this.cb2.AutoSize = true;
            this.cb2.Checked = true;
            this.cb2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb2.Location = new System.Drawing.Point(45, 3);
            this.cb2.Name = "cb2";
            this.cb2.Size = new System.Drawing.Size(33, 18);
            this.cb2.TabIndex = 1;
            this.cb2.Text = "L";
            this.cb2.UseVisualStyleBackColor = true;
            this.cb2.CheckedChanged += new System.EventHandler(this.cb2_CheckedChanged);
            // 
            // dtp
            // 
            this.dtp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp.Location = new System.Drawing.Point(58, 7);
            this.dtp.Name = "dtp";
            this.dtp.Size = new System.Drawing.Size(119, 21);
            this.dtp.TabIndex = 600;
            this.dtp.CloseUp += new System.EventHandler(this.dtp_CloseUp);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel4.Controls.Add(this.rb2);
            this.panel4.Controls.Add(this.rb1);
            this.panel4.Location = new System.Drawing.Point(1, 396);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(116, 29);
            this.panel4.TabIndex = 613;
            // 
            // rb2
            // 
            this.rb2.AutoSize = true;
            this.rb2.Location = new System.Drawing.Point(61, 7);
            this.rb2.Name = "rb2";
            this.rb2.Size = new System.Drawing.Size(47, 16);
            this.rb2.TabIndex = 612;
            this.rb2.Text = "报表";
            this.rb2.UseVisualStyleBackColor = true;
            this.rb2.CheckedChanged += new System.EventHandler(this.rb2_CheckedChanged);
            // 
            // rb1
            // 
            this.rb1.AutoSize = true;
            this.rb1.Checked = true;
            this.rb1.Location = new System.Drawing.Point(8, 7);
            this.rb1.Name = "rb1";
            this.rb1.Size = new System.Drawing.Size(47, 16);
            this.rb1.TabIndex = 611;
            this.rb1.TabStop = true;
            this.rb1.Text = "列表";
            this.rb1.UseVisualStyleBackColor = true;
            this.rb1.CheckedChanged += new System.EventHandler(this.rb1_CheckedChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.splitContainer1);
            this.panel5.Location = new System.Drawing.Point(1, 34);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(886, 361);
            this.panel5.TabIndex = 589;
            // 
            // previewQD
            // 
            this.previewQD.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.previewQD.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.previewQD.Buttons = ((FastReport.PreviewButtons)(((((((((((FastReport.PreviewButtons.Print | FastReport.PreviewButtons.Open)
                        | FastReport.PreviewButtons.Save)
                        | FastReport.PreviewButtons.Find)
                        | FastReport.PreviewButtons.Zoom)
                        | FastReport.PreviewButtons.Outline)
                        | FastReport.PreviewButtons.PageSetup)
                        | FastReport.PreviewButtons.Edit)
                        | FastReport.PreviewButtons.Watermark)
                        | FastReport.PreviewButtons.Navigator)
                        | FastReport.PreviewButtons.Close)));
            this.previewQD.Font = new System.Drawing.Font("宋体", 9F);
            this.previewQD.Location = new System.Drawing.Point(93, 36);
            this.previewQD.Name = "previewQD";
            this.previewQD.PageOffset = new System.Drawing.Point(10, 10);
            this.previewQD.Size = new System.Drawing.Size(726, 339);
            this.previewQD.TabIndex = 588;
            this.previewQD.ToolbarVisible = false;
            this.previewQD.UIStyle = FastReport.Utils.UIStyle.Office2007Black;
            this.previewQD.Visible = false;
            // 
            // report
            // 
            this.report.ReportResourceString = resources.GetString("report.ReportResourceString");
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(140, 399);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 614;
            this.button1.Text = "打印";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // LabelNoSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(214)))), ((int)(((byte)(243)))));
            this.Controls.Add(this.previewQD);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Name = "LabelNoSearch";
            this.Size = new System.Drawing.Size(888, 425);
            this.Load += new System.EventHandler(this.LabelNoSearch_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPre)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.report)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.DataGridView dgvPre;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private InformationQS informationQS1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.FlowLayoutPanel ftp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox cb3;
        private System.Windows.Forms.CheckBox cb4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox cb1;
        private System.Windows.Forms.CheckBox cb2;
        private System.Windows.Forms.DateTimePicker dtp;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton rb2;
        private System.Windows.Forms.RadioButton rb1;
        private System.Windows.Forms.Panel panel5;
        private FastReport.Report report;
        private FastReport.Preview.PreviewControl previewQD;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewImageColumn IsQianShou;
        private System.Windows.Forms.DataGridViewTextBoxColumn LabelNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn BedNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Batch;
        private System.Windows.Forms.DataGridViewTextBoxColumn FreqCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn IVStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn PreCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrueStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPRecordID;
        private System.Windows.Forms.Button button2;


    }
}
