namespace PivasNurse
{
    partial class PreForLabel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button1 = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dgvPre = new System.Windows.Forms.DataGridView();
            this.LabelNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BedNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Batch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FreqCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IVStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PreCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPRecordID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPre)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(205, 275);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 23);
            this.button1.TabIndex = 584;
            this.button1.Text = "关闭";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(3, 3);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(123, 21);
            this.dateTimePicker1.TabIndex = 585;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // dgvPre
            // 
            this.dgvPre.AllowUserToAddRows = false;
            this.dgvPre.AllowUserToResizeColumns = false;
            this.dgvPre.AllowUserToResizeRows = false;
            this.dgvPre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPre.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(246)))));
            this.dgvPre.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPre.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPre.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LabelNo,
            this.BedNo,
            this.PatName,
            this.Batch,
            this.FreqCode,
            this.IVStatus,
            this.PreCode,
            this.CPRecordID});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPre.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvPre.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(246)))));
            this.dgvPre.Location = new System.Drawing.Point(8, 30);
            this.dgvPre.MultiSelect = false;
            this.dgvPre.Name = "dgvPre";
            this.dgvPre.ReadOnly = true;
            this.dgvPre.RowHeadersVisible = false;
            this.dgvPre.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(246)))));
            this.dgvPre.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvPre.RowTemplate.Height = 23;
            this.dgvPre.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPre.Size = new System.Drawing.Size(435, 243);
            this.dgvPre.TabIndex = 586;
            this.dgvPre.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvPre_MouseDown);
            // 
            // LabelNo
            // 
            this.LabelNo.HeaderText = "瓶签号";
            this.LabelNo.Name = "LabelNo";
            this.LabelNo.ReadOnly = true;
            // 
            // BedNo
            // 
            this.BedNo.HeaderText = "床号";
            this.BedNo.Name = "BedNo";
            this.BedNo.ReadOnly = true;
            this.BedNo.Width = 50;
            // 
            // PatName
            // 
            this.PatName.HeaderText = "姓名";
            this.PatName.Name = "PatName";
            this.PatName.ReadOnly = true;
            this.PatName.Width = 60;
            // 
            // Batch
            // 
            this.Batch.HeaderText = "批次";
            this.Batch.Name = "Batch";
            this.Batch.ReadOnly = true;
            this.Batch.Width = 50;
            // 
            // FreqCode
            // 
            this.FreqCode.HeaderText = "频次";
            this.FreqCode.Name = "FreqCode";
            this.FreqCode.ReadOnly = true;
            this.FreqCode.Width = 50;
            // 
            // IVStatus
            // 
            this.IVStatus.HeaderText = "状态";
            this.IVStatus.Name = "IVStatus";
            this.IVStatus.ReadOnly = true;
            // 
            // PreCode
            // 
            this.PreCode.HeaderText = "处方号";
            this.PreCode.Name = "PreCode";
            this.PreCode.ReadOnly = true;
            this.PreCode.Visible = false;
            // 
            // CPRecordID
            // 
            this.CPRecordID.HeaderText = "记录ID";
            this.CPRecordID.Name = "CPRecordID";
            this.CPRecordID.ReadOnly = true;
            this.CPRecordID.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(246)))));
            this.panel1.Controls.Add(this.dgvPre);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Location = new System.Drawing.Point(1, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(446, 305);
            this.panel1.TabIndex = 587;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(179)))), ((int)(((byte)(253)))));
            this.panel2.Location = new System.Drawing.Point(-3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(461, 16);
            this.panel2.TabIndex = 588;
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDown);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(179)))), ((int)(((byte)(253)))));
            this.panel3.Location = new System.Drawing.Point(-2, 321);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(461, 16);
            this.panel3.TabIndex = 589;
            // 
            // PreForLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(448, 327);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PreForLabel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PreForLabel";
            this.Load += new System.EventHandler(this.PreForLabel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPre)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        public System.Windows.Forms.DataGridView dgvPre;
        private System.Windows.Forms.DataGridViewTextBoxColumn LabelNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn BedNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Batch;
        private System.Windows.Forms.DataGridViewTextBoxColumn FreqCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn IVStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn PreCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPRecordID;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}