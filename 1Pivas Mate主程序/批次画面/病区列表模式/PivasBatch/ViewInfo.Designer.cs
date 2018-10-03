namespace PivasBatch
{
    partial class ViewInfo
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
            this.Dgv_Info = new System.Windows.Forms.DataGridView();
            this.Checkbox = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Info)).BeginInit();
            this.SuspendLayout();
            // 
            // Dgv_Info
            // 
            this.Dgv_Info.AllowUserToAddRows = false;
            this.Dgv_Info.AllowUserToDeleteRows = false;
            this.Dgv_Info.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 11F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.Dgv_Info.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Dgv_Info.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Dgv_Info.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.Dgv_Info.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Dgv_Info.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.Dgv_Info.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.Dgv_Info.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 11F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Dgv_Info.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Dgv_Info.ColumnHeadersHeight = 25;
            this.Dgv_Info.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Dgv_Info.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Checkbox});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 11F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_Info.DefaultCellStyle = dataGridViewCellStyle3;
            this.Dgv_Info.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.Dgv_Info.GridColor = System.Drawing.Color.Black;
            this.Dgv_Info.Location = new System.Drawing.Point(0, 0);
            this.Dgv_Info.Margin = new System.Windows.Forms.Padding(0);
            this.Dgv_Info.MultiSelect = false;
            this.Dgv_Info.Name = "Dgv_Info";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 11F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_Info.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.Dgv_Info.RowHeadersVisible = false;
            this.Dgv_Info.RowHeadersWidth = 25;
            this.Dgv_Info.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("宋体", 11F);
            this.Dgv_Info.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Dgv_Info.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.Dgv_Info.RowTemplate.Height = 24;
            this.Dgv_Info.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_Info.Size = new System.Drawing.Size(613, 451);
            this.Dgv_Info.TabIndex = 32;
            this.Dgv_Info.Sorted += new System.EventHandler(this.Dgv_Info_Sorted);
            this.Dgv_Info.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Info_CellDoubleClick);
            this.Dgv_Info.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Dgv_Info_ColumnHeaderMouseClick);
            this.Dgv_Info.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.Dgv_Info_RowPrePaint);
            this.Dgv_Info.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Info_CellClick);
            this.Dgv_Info.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Dgv_Info_KeyUp);
            // 
            // Checkbox
            // 
            this.Checkbox.FillWeight = 1F;
            this.Checkbox.HeaderText = "";
            this.Checkbox.Name = "Checkbox";
            this.Checkbox.Width = 5;
            // 
            // ViewInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Dgv_Info);
            this.Name = "ViewInfo";
            this.Size = new System.Drawing.Size(613, 451);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Info)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView Dgv_Info;
        private System.Windows.Forms.DataGridViewTextBoxColumn Checkbox;
    }
}
