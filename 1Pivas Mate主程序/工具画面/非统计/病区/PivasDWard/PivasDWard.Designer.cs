namespace DWardManage
{
    partial class PivasDWard
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PivasDWard));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvWard = new System.Windows.Forms.DataGridView();
            this.px1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.Panel_Close = new System.Windows.Forms.Panel();
            this.Pic_Min = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.WardCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WardName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WardSeqNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WardSimName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WardArea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spellcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.开放 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.LimitSTTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWard)).BeginInit();
            this.px1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.dgvWard);
            this.panel1.Controls.Add(this.px1);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.Panel_Close);
            this.panel1.Controls.Add(this.Pic_Min);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(780, 570);
            this.panel1.TabIndex = 0;
            // 
            // dgvWard
            // 
            this.dgvWard.AllowUserToAddRows = false;
            this.dgvWard.AllowUserToDeleteRows = false;
            this.dgvWard.AllowUserToOrderColumns = true;
            this.dgvWard.AllowUserToResizeRows = false;
            this.dgvWard.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(246)))));
            this.dgvWard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvWard.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            this.dgvWard.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvWard.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvWard.ColumnHeadersHeight = 30;
            this.dgvWard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvWard.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.WardCode,
            this.WardName,
            this.WardSeqNo,
            this.WardSimName,
            this.WardArea,
            this.Spellcode,
            this.开放,
            this.LimitSTTime});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvWard.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvWard.EnableHeadersVisualStyles = false;
            this.dgvWard.Location = new System.Drawing.Point(1, 81);
            this.dgvWard.Name = "dgvWard";
            this.dgvWard.ReadOnly = true;
            this.dgvWard.RowHeadersVisible = false;
            this.dgvWard.RowTemplate.Height = 23;
            this.dgvWard.Size = new System.Drawing.Size(778, 468);
            this.dgvWard.TabIndex = 14;
            this.dgvWard.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWard_CellClick);
            // 
            // px1
            // 
            this.px1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(246)))));
            this.px1.Controls.Add(this.pictureBox1);
            this.px1.Controls.Add(this.pictureBox2);
            this.px1.Location = new System.Drawing.Point(81, 117);
            this.px1.Name = "px1";
            this.px1.Size = new System.Drawing.Size(17, 16);
            this.px1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(14, 14);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DWardManage.Properties.Resources.上1;
            this.pictureBox2.Location = new System.Drawing.Point(0, -4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(15, 18);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(234)))), ((int)(((byte)(142)))));
            this.panel6.BackgroundImage = global::DWardManage.Properties.Resources._5;
            this.panel6.Controls.Add(this.label11);
            this.panel6.Location = new System.Drawing.Point(-12, 547);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(795, 42);
            this.panel6.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(234)))), ((int)(((byte)(142)))));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label11.Location = new System.Drawing.Point(15, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(774, 14);
            this.label11.TabIndex = 14;
            this.label11.Text = "上海博龙智医科技股份有限公司";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Panel_Close
            // 
            this.Panel_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Close.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel_Close.BackgroundImage")));
            this.Panel_Close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Panel_Close.Location = new System.Drawing.Point(758, 0);
            this.Panel_Close.Name = "Panel_Close";
            this.Panel_Close.Size = new System.Drawing.Size(20, 26);
            this.Panel_Close.TabIndex = 12;
            this.Panel_Close.MouseLeave += new System.EventHandler(this.Panel_Close_MouseLeave);
            this.Panel_Close.Click += new System.EventHandler(this.Panel_Close_Click);
            this.Panel_Close.MouseHover += new System.EventHandler(this.Panel_Close_MouseHover);
            // 
            // Pic_Min
            // 
            this.Pic_Min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Pic_Min.BackColor = System.Drawing.Color.Transparent;
            this.Pic_Min.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Pic_Min.BackgroundImage")));
            this.Pic_Min.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Pic_Min.Location = new System.Drawing.Point(736, 0);
            this.Pic_Min.Name = "Pic_Min";
            this.Pic_Min.Size = new System.Drawing.Size(21, 26);
            this.Pic_Min.TabIndex = 11;
            this.Pic_Min.MouseLeave += new System.EventHandler(this.Pic_Min_MouseLeave);
            this.Pic_Min.Click += new System.EventHandler(this.Pic_Min_Click);
            this.Pic_Min.MouseHover += new System.EventHandler(this.Pic_Min_MouseHover);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(218)))), ((int)(((byte)(91)))));
            this.panel3.BackgroundImage = global::DWardManage.Properties.Resources._55;
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(780, 82);
            this.panel3.TabIndex = 2;
            this.panel3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel3_MouseDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(733, 31);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 12);
            this.label10.TabIndex = 9;
            this.label10.Visible = false;
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(692, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 19);
            this.label4.TabIndex = 7;
            this.label4.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(262, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 11;
            this.label9.Text = "管理";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.radioButton2);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.textBox1);
            this.panel4.Controls.Add(this.comboBox2);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.radioButton1);
            this.panel4.Location = new System.Drawing.Point(325, 16);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(351, 63);
            this.panel4.TabIndex = 0;
            this.panel4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel4_MouseDown);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.ForeColor = System.Drawing.SystemColors.Control;
            this.radioButton2.Location = new System.Drawing.Point(84, 34);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(59, 16);
            this.radioButton2.TabIndex = 37;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "不开放\r\n";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(13, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 27;
            this.label6.Text = "是否开放：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(157, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 33;
            this.label8.Text = "模糊查询：";
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(228, 33);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(90, 21);
            this.textBox1.TabIndex = 34;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // comboBox2
            // 
            this.comboBox2.BackColor = System.Drawing.Color.White;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(228, 7);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(90, 20);
            this.comboBox2.TabIndex = 31;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            this.comboBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox2_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(181, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 28;
            this.label7.Text = "分组：";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.ForeColor = System.Drawing.Color.White;
            this.radioButton1.Location = new System.Drawing.Point(84, 8);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(47, 16);
            this.radioButton1.TabIndex = 36;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "开放\r\n";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(412, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 12);
            this.label5.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 23);
            this.label3.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 65);
            this.label1.TabIndex = 0;
            this.label1.Text = "医院病区";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(704, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "筛选";
            this.label2.Visible = false;
            this.label2.MouseLeave += new System.EventHandler(this.label2_MouseLeave);
            this.label2.Click += new System.EventHandler(this.button1_Click);
            this.label2.MouseHover += new System.EventHandler(this.label2_MouseHover);
            // 
            // WardCode
            // 
            this.WardCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.WardCode.DataPropertyName = "WardCode";
            this.WardCode.FillWeight = 15F;
            this.WardCode.HeaderText = "编码";
            this.WardCode.Name = "WardCode";
            this.WardCode.ReadOnly = true;
            // 
            // WardName
            // 
            this.WardName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.WardName.DataPropertyName = "WardName";
            this.WardName.FillWeight = 20F;
            this.WardName.HeaderText = "名称";
            this.WardName.Name = "WardName";
            this.WardName.ReadOnly = true;
            // 
            // WardSeqNo
            // 
            this.WardSeqNo.DataPropertyName = "WardSeqNo";
            this.WardSeqNo.FillWeight = 15F;
            this.WardSeqNo.HeaderText = "排序";
            this.WardSeqNo.Name = "WardSeqNo";
            this.WardSeqNo.ReadOnly = true;
            this.WardSeqNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.WardSeqNo.Width = 60;
            // 
            // WardSimName
            // 
            this.WardSimName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.WardSimName.DataPropertyName = "WardSimName";
            this.WardSimName.FillWeight = 15F;
            this.WardSimName.HeaderText = "简称";
            this.WardSimName.Name = "WardSimName";
            this.WardSimName.ReadOnly = true;
            // 
            // WardArea
            // 
            this.WardArea.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.WardArea.DataPropertyName = "WardArea";
            this.WardArea.FillWeight = 15F;
            this.WardArea.HeaderText = "分组";
            this.WardArea.Name = "WardArea";
            this.WardArea.ReadOnly = true;
            this.WardArea.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Spellcode
            // 
            this.Spellcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Spellcode.DataPropertyName = "Spellcode";
            this.Spellcode.FillWeight = 15F;
            this.Spellcode.HeaderText = "简拼";
            this.Spellcode.Name = "Spellcode";
            this.Spellcode.ReadOnly = true;
            // 
            // 开放
            // 
            this.开放.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.开放.DataPropertyName = "IsOpen";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.NullValue = false;
            this.开放.DefaultCellStyle = dataGridViewCellStyle2;
            this.开放.FillWeight = 10F;
            this.开放.HeaderText = "开放";
            this.开放.Name = "开放";
            this.开放.ReadOnly = true;
            // 
            // LimitSTTime
            // 
            this.LimitSTTime.DataPropertyName = "LimitSTTime";
            this.LimitSTTime.HeaderText = "时间限制";
            this.LimitSTTime.Name = "LimitSTTime";
            this.LimitSTTime.ReadOnly = true;
            this.LimitSTTime.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LimitSTTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LimitSTTime.Width = 80;
            // 
            // PivasDWard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(780, 570);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PivasDWard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "病区";
            this.Load += new System.EventHandler(this.PivasDWard_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWard)).EndInit();
            this.px1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Panel Panel_Close;
        private System.Windows.Forms.Panel Pic_Min;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel px1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.DataGridView dgvWard;
        private System.Windows.Forms.DataGridViewTextBoxColumn WardCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn WardName;
        private System.Windows.Forms.DataGridViewTextBoxColumn WardSeqNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn WardSimName;
        private System.Windows.Forms.DataGridViewTextBoxColumn WardArea;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spellcode;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 开放;
        private System.Windows.Forms.DataGridViewTextBoxColumn LimitSTTime;
    }
}

