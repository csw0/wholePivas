namespace PivasLabelCheckAll
{
    partial class ucCountInfomation
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
            this.dgvCountInformation = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCountInformation)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCountInformation
            // 
            this.dgvCountInformation.AllowUserToAddRows = false;
            this.dgvCountInformation.AllowUserToDeleteRows = false;
            this.dgvCountInformation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCountInformation.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvCountInformation.BackgroundColor = System.Drawing.Color.White;
            this.dgvCountInformation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCountInformation.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCountInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCountInformation.Location = new System.Drawing.Point(0, 0);
            this.dgvCountInformation.Name = "dgvCountInformation";
            this.dgvCountInformation.ReadOnly = true;
            this.dgvCountInformation.RowHeadersVisible = false;
            this.dgvCountInformation.RowTemplate.Height = 23;
            this.dgvCountInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvCountInformation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCountInformation.Size = new System.Drawing.Size(614, 364);
            this.dgvCountInformation.TabIndex = 0;
            this.dgvCountInformation.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCountInformation_CellDoubleClick);
            // 
            // ucCountInfomation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvCountInformation);
            this.Name = "ucCountInfomation";
            this.Size = new System.Drawing.Size(614, 364);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCountInformation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dgvCountInformation;

    }
}
