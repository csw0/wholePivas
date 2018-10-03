namespace PivasBatchDiff
{
    partial class upateIVRecord
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(upateIVRecord));
            this.dgvWard = new System.Windows.Forms.DataGridView();
            this.dgvselect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.WardName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wardcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dgv_Info = new System.Windows.Forms.DataGridView();
            this.全部 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.lbDate = new System.Windows.Forms.Label();
            this.DgvDrug = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.Panel_Close = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.printForm1 = new Microsoft.VisualBasic.PowerPacks.Printing.PrintForm(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Info)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvDrug)).BeginInit();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvWard
            // 
            this.dgvWard.AllowUserToAddRows = false;
            this.dgvWard.AllowUserToResizeRows = false;
            this.dgvWard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvWard.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvWard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvWard.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvWard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWard.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvselect,
            this.WardName,
            this.Column1,
            this.wardcode});
            this.dgvWard.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvWard.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvWard.Location = new System.Drawing.Point(2, 73);
            this.dgvWard.MultiSelect = false;
            this.dgvWard.Name = "dgvWard";
            this.dgvWard.ReadOnly = true;
            this.dgvWard.RowHeadersVisible = false;
            this.dgvWard.RowTemplate.Height = 23;
            this.dgvWard.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWard.Size = new System.Drawing.Size(202, 545);
            this.dgvWard.TabIndex = 39;
            this.dgvWard.Click += new System.EventHandler(this.dgvWard_Click);
            this.dgvWard.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWard_CellContentClick);
            // 
            // dgvselect
            // 
            this.dgvselect.DataPropertyName = "select";
            this.dgvselect.FalseValue = "False";
            this.dgvselect.HeaderText = "";
            this.dgvselect.Name = "dgvselect";
            this.dgvselect.ReadOnly = true;
            this.dgvselect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvselect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvselect.TrueValue = "True";
            this.dgvselect.Width = 20;
            // 
            // WardName
            // 
            this.WardName.DataPropertyName = "WardName";
            this.WardName.HeaderText = "病区名";
            this.WardName.Name = "WardName";
            this.WardName.ReadOnly = true;
            this.WardName.Width = 120;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Count";
            this.Column1.HeaderText = "数量";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 55;
            // 
            // wardcode
            // 
            this.wardcode.DataPropertyName = "WardCode";
            this.wardcode.HeaderText = "编码";
            this.wardcode.Name = "wardcode";
            this.wardcode.ReadOnly = true;
            this.wardcode.Visible = false;
            // 
            // Dgv_Info
            // 
            this.Dgv_Info.AllowUserToAddRows = false;
            this.Dgv_Info.AllowUserToDeleteRows = false;
            this.Dgv_Info.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 11F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.Dgv_Info.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.Dgv_Info.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Dgv_Info.BackgroundColor = System.Drawing.Color.White;
            this.Dgv_Info.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Dgv_Info.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.Dgv_Info.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.Dgv_Info.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 11F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Dgv_Info.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Dgv_Info.ColumnHeadersHeight = 25;
            this.Dgv_Info.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Dgv_Info.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.全部});
            this.Dgv_Info.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 11F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_Info.DefaultCellStyle = dataGridViewCellStyle4;
            this.Dgv_Info.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.Dgv_Info.GridColor = System.Drawing.Color.Black;
            this.Dgv_Info.Location = new System.Drawing.Point(0, 0);
            this.Dgv_Info.Margin = new System.Windows.Forms.Padding(0);
            this.Dgv_Info.MultiSelect = false;
            this.Dgv_Info.Name = "Dgv_Info";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 11F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_Info.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.Dgv_Info.RowHeadersVisible = false;
            this.Dgv_Info.RowHeadersWidth = 25;
            this.Dgv_Info.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("宋体", 11F);
            this.Dgv_Info.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Dgv_Info.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.Dgv_Info.RowTemplate.Height = 24;
            this.Dgv_Info.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Dgv_Info.Size = new System.Drawing.Size(346, 545);
            this.Dgv_Info.TabIndex = 40;
            this.Dgv_Info.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Info_CellClick);
            this.Dgv_Info.Click += new System.EventHandler(this.Dgv_Info_Click);
            // 
            // 全部
            // 
            this.全部.HeaderText = "";
            this.全部.Name = "全部";
            this.全部.Width = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 41;
            this.label1.Text = "当前日期：";
            // 
            // lbDate
            // 
            this.lbDate.AutoSize = true;
            this.lbDate.Location = new System.Drawing.Point(74, 56);
            this.lbDate.Name = "lbDate";
            this.lbDate.Size = new System.Drawing.Size(65, 12);
            this.lbDate.TabIndex = 42;
            this.lbDate.Text = "2014-06-24";
            // 
            // DgvDrug
            // 
            this.DgvDrug.AllowUserToAddRows = false;
            this.DgvDrug.AllowUserToDeleteRows = false;
            this.DgvDrug.AllowUserToResizeRows = false;
            this.DgvDrug.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvDrug.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.DgvDrug.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DgvDrug.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.DgvDrug.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvDrug.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvDrug.DefaultCellStyle = dataGridViewCellStyle6;
            this.DgvDrug.Location = new System.Drawing.Point(0, -1);
            this.DgvDrug.MultiSelect = false;
            this.DgvDrug.Name = "DgvDrug";
            this.DgvDrug.ReadOnly = true;
            this.DgvDrug.RowHeadersVisible = false;
            this.DgvDrug.RowTemplate.Height = 23;
            this.DgvDrug.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvDrug.Size = new System.Drawing.Size(345, 545);
            this.DgvDrug.TabIndex = 43;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(590, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 23);
            this.button1.TabIndex = 44;
            this.button1.Text = "通过";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(209, 55);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 1015;
            this.checkBox1.Text = "全部";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // Panel_Close
            // 
            this.Panel_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Close.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Close.BackgroundImage = global::PivasBatchDiff.Properties.Resources._4;
            this.Panel_Close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Panel_Close.Location = new System.Drawing.Point(879, 1);
            this.Panel_Close.Name = "Panel_Close";
            this.Panel_Close.Size = new System.Drawing.Size(20, 25);
            this.Panel_Close.TabIndex = 1014;
            this.Panel_Close.MouseLeave += new System.EventHandler(this.Panel_Close_MouseLeave);
            this.Panel_Close.Click += new System.EventHandler(this.Panel_Close_Click);
            this.Panel_Close.MouseHover += new System.EventHandler(this.Panel_Close_MouseHover);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Panel_Close);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.lbDate);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(901, 70);
            this.panel1.TabIndex = 1016;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(790, 43);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(67, 23);
            this.button3.TabIndex = 1019;
            this.button3.Text = "废弃";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(690, 43);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(67, 23);
            this.button2.TabIndex = 1018;
            this.button2.Text = "补足";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(312, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 1017;
            this.label9.Text = "管理";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(29, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(312, 32);
            this.label2.TabIndex = 1016;
            this.label2.Text = "瓶签药品不匹配确认";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(206, 73);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Dgv_Info);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.DgvDrug);
            this.splitContainer1.Size = new System.Drawing.Size(693, 545);
            this.splitContainer1.SplitterDistance = 346;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 1017;
            // 
            // printForm1
            // 
            this.printForm1.DocumentName = "document";
            this.printForm1.Form = this;
            this.printForm1.PrintAction = System.Drawing.Printing.PrintAction.PrintToPrinter;
            this.printForm1.PrinterSettings = ((System.Drawing.Printing.PrinterSettings)(resources.GetObject("printForm1.PrinterSettings")));
            this.printForm1.PrintFileName = null;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.toolStripMenuItem1.Text = "刷新";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // upateIVRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(210)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(901, 620);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.dgvWard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "upateIVRecord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "瓶签修改";
            this.Load += new System.EventHandler(this.updateIVRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Info)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvDrug)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvWard;
        public System.Windows.Forms.DataGridView Dgv_Info;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbDate;
        private System.Windows.Forms.DataGridView DgvDrug;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel Panel_Close;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvselect;
        private System.Windows.Forms.DataGridViewTextBoxColumn WardName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn wardcode;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 全部;
        private System.Windows.Forms.Label label9;
        private Microsoft.VisualBasic.PowerPacks.Printing.PrintForm printForm1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}

