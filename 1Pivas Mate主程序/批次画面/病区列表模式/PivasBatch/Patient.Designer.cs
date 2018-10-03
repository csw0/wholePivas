namespace PivasBatch
{
    partial class Patient
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
            this.Dgv_Patient = new System.Windows.Forms.DataGridView();
            this.Checkbox = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_PatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_BedNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_PatCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_UnCheckCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_TotalCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_BatchSaved = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_IsSame = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsOpen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WardCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Patient)).BeginInit();
            this.SuspendLayout();
            // 
            // Dgv_Patient
            // 
            this.Dgv_Patient.AllowUserToAddRows = false;
            this.Dgv_Patient.AllowUserToDeleteRows = false;
            this.Dgv_Patient.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.Dgv_Patient.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Dgv_Patient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Dgv_Patient.BackgroundColor = System.Drawing.SystemColors.Control;
            this.Dgv_Patient.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Dgv_Patient.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.Dgv_Patient.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.Dgv_Patient.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.Dgv_Patient.ColumnHeadersHeight = 20;
            this.Dgv_Patient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Dgv_Patient.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Checkbox,
            this.dgv_PatName,
            this.dgv_BedNo,
            this.dgv_PatCode,
            this.dgv_UnCheckCount,
            this.dgv_TotalCount,
            this.dgv_BatchSaved,
            this.dgv_IsSame,
            this.IsOpen,
            this.WardCode});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_Patient.DefaultCellStyle = dataGridViewCellStyle2;
            this.Dgv_Patient.Location = new System.Drawing.Point(0, 0);
            this.Dgv_Patient.Margin = new System.Windows.Forms.Padding(0);
            this.Dgv_Patient.MultiSelect = false;
            this.Dgv_Patient.Name = "Dgv_Patient";
            this.Dgv_Patient.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_Patient.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Dgv_Patient.RowHeadersVisible = false;
            this.Dgv_Patient.RowHeadersWidth = 20;
            this.Dgv_Patient.RowTemplate.Height = 24;
            this.Dgv_Patient.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Dgv_Patient.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_Patient.Size = new System.Drawing.Size(167, 465);
            this.Dgv_Patient.TabIndex = 2;
            this.Dgv_Patient.Sorted += new System.EventHandler(this.Dgv_Patient_Sorted);
            this.Dgv_Patient.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Dgv_Patient_ColumnHeaderMouseClick);
            this.Dgv_Patient.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Patient_CellClick);
            this.Dgv_Patient.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Dgv_Patient_KeyUp);
            // 
            // Checkbox
            // 
            this.Checkbox.HeaderText = "Column1";
            this.Checkbox.Name = "Checkbox";
            this.Checkbox.ReadOnly = true;
            this.Checkbox.Width = 5;
            // 
            // dgv_PatName
            // 
            this.dgv_PatName.HeaderText = "姓名";
            this.dgv_PatName.Name = "dgv_PatName";
            this.dgv_PatName.ReadOnly = true;
            this.dgv_PatName.Width = 60;
            // 
            // dgv_BedNo
            // 
            this.dgv_BedNo.HeaderText = "床号";
            this.dgv_BedNo.Name = "dgv_BedNo";
            this.dgv_BedNo.ReadOnly = true;
            // 
            // dgv_PatCode
            // 
            this.dgv_PatCode.HeaderText = "编码";
            this.dgv_PatCode.Name = "dgv_PatCode";
            this.dgv_PatCode.ReadOnly = true;
            this.dgv_PatCode.Visible = false;
            // 
            // dgv_UnCheckCount
            // 
            this.dgv_UnCheckCount.HeaderText = "Column1";
            this.dgv_UnCheckCount.Name = "dgv_UnCheckCount";
            this.dgv_UnCheckCount.ReadOnly = true;
            this.dgv_UnCheckCount.Visible = false;
            // 
            // dgv_TotalCount
            // 
            this.dgv_TotalCount.HeaderText = "Column1";
            this.dgv_TotalCount.Name = "dgv_TotalCount";
            this.dgv_TotalCount.ReadOnly = true;
            this.dgv_TotalCount.Visible = false;
            // 
            // dgv_BatchSaved
            // 
            this.dgv_BatchSaved.HeaderText = "Column1";
            this.dgv_BatchSaved.Name = "dgv_BatchSaved";
            this.dgv_BatchSaved.ReadOnly = true;
            this.dgv_BatchSaved.Visible = false;
            // 
            // dgv_IsSame
            // 
            this.dgv_IsSame.HeaderText = "Column1";
            this.dgv_IsSame.Name = "dgv_IsSame";
            this.dgv_IsSame.ReadOnly = true;
            this.dgv_IsSame.Visible = false;
            // 
            // IsOpen
            // 
            this.IsOpen.HeaderText = "Column1";
            this.IsOpen.Name = "IsOpen";
            this.IsOpen.ReadOnly = true;
            this.IsOpen.Visible = false;
            // 
            // WardCode
            // 
            this.WardCode.HeaderText = "WardCode";
            this.WardCode.Name = "WardCode";
            this.WardCode.ReadOnly = true;
            this.WardCode.Visible = false;
            // 
            // Patient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.Controls.Add(this.Dgv_Patient);
            this.Name = "Patient";
            this.Size = new System.Drawing.Size(167, 465);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Patient)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView Dgv_Patient;
        private System.Windows.Forms.DataGridViewTextBoxColumn Checkbox;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_PatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_BedNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_PatCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_UnCheckCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_TotalCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_BatchSaved;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_IsSame;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsOpen;
        private System.Windows.Forms.DataGridViewTextBoxColumn WardCode;


    }
}
