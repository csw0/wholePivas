namespace PivasNurse
{
    partial class Information
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.DrugName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FregCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pishi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UniPreparationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label6 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblWard = new System.Windows.Forms.TextBox();
            this.lblBatch = new System.Windows.Forms.TextBox();
            this.Label_DrawerName = new System.Windows.Forms.TextBox();
            this.lblEndDT = new System.Windows.Forms.TextBox();
            this.lblPatient = new System.Windows.Forms.TextBox();
            this.lblDoctor = new System.Windows.Forms.TextBox();
            this.lblStartDT = new System.Windows.Forms.TextBox();
            this.lblBedNo = new System.Windows.Forms.TextBox();
            this.lblWeight = new System.Windows.Forms.TextBox();
            this.lblCaseID = new System.Windows.Forms.TextBox();
            this.lblSex = new System.Windows.Forms.TextBox();
            this.lblAge = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlInfo = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(3, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "病区：";
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(3, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "医生：";
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(234, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "住院号：";
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(246, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "床号：";
            // 
            // label7
            // 
            this.label7.ForeColor = System.Drawing.Color.Gray;
            this.label7.Location = new System.Drawing.Point(16, 119);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "停止日期：";
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.Color.Gray;
            this.label8.Location = new System.Drawing.Point(17, 96);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "开始日期：";
            // 
            // label13
            // 
            this.label13.ForeColor = System.Drawing.Color.Gray;
            this.label13.Location = new System.Drawing.Point(40, 137);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 23;
            this.label13.Text = "频次：";
            // 
            // label9
            // 
            this.label9.ForeColor = System.Drawing.Color.Gray;
            this.label9.Location = new System.Drawing.Point(222, 119);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 6;
            this.label9.Text = "录入：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgv1
            // 
            this.dgv1.AllowUserToAddRows = false;
            this.dgv1.AllowUserToDeleteRows = false;
            this.dgv1.AllowUserToResizeRows = false;
            this.dgv1.BackgroundColor = System.Drawing.Color.White;
            this.dgv1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.ColumnHeadersVisible = false;
            this.dgv1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DrugName,
            this.Spec,
            this.FregCode,
            this.Quantity,
            this.pishi,
            this.UniPreparationID});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv1.GridColor = System.Drawing.Color.White;
            this.dgv1.Location = new System.Drawing.Point(0, 0);
            this.dgv1.Name = "dgv1";
            this.dgv1.ReadOnly = true;
            this.dgv1.RowHeadersVisible = false;
            this.dgv1.RowTemplate.Height = 23;
            this.dgv1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv1.Size = new System.Drawing.Size(386, 106);
            this.dgv1.TabIndex = 30;
            this.dgv1.DoubleClick += new System.EventHandler(this.dgv1_DoubleClick);
            // 
            // DrugName
            // 
            this.DrugName.DataPropertyName = "DrugName";
            this.DrugName.HeaderText = "药品名称";
            this.DrugName.Name = "DrugName";
            this.DrugName.ReadOnly = true;
            this.DrugName.Width = 185;
            // 
            // Spec
            // 
            this.Spec.DataPropertyName = "Spec";
            this.Spec.HeaderText = "规格";
            this.Spec.Name = "Spec";
            this.Spec.ReadOnly = true;
            // 
            // FregCode
            // 
            this.FregCode.DataPropertyName = "FregCode";
            this.FregCode.HeaderText = "用法";
            this.FregCode.Name = "FregCode";
            this.FregCode.ReadOnly = true;
            this.FregCode.Width = 60;
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "Quantity";
            this.Quantity.HeaderText = "用量";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            this.Quantity.Width = 65;
            // 
            // pishi
            // 
            this.pishi.DataPropertyName = "PiShi";
            this.pishi.HeaderText = "皮试";
            this.pishi.Name = "pishi";
            this.pishi.ReadOnly = true;
            this.pishi.Visible = false;
            this.pishi.Width = 60;
            // 
            // UniPreparationID
            // 
            this.UniPreparationID.DataPropertyName = "UniPreparationID";
            this.UniPreparationID.HeaderText = "UniPreparationID";
            this.UniPreparationID.Name = "UniPreparationID";
            this.UniPreparationID.ReadOnly = true;
            this.UniPreparationID.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(182)))));
            this.splitContainer1.Location = new System.Drawing.Point(-1, 163);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.label12);
            this.splitContainer1.Panel1.Controls.Add(this.dgv1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlInfo);
            this.splitContainer1.Size = new System.Drawing.Size(386, 217);
            this.splitContainer1.SplitterDistance = 106;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 32;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(386, 2);
            this.label6.TabIndex = 32;
            this.label6.Text = "label6";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.White;
            this.label12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label12.Location = new System.Drawing.Point(0, 104);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(386, 2);
            this.label12.TabIndex = 31;
            this.label12.Text = "label12";
            // 
            // lblWard
            // 
            this.lblWard.BackColor = System.Drawing.Color.White;
            this.lblWard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblWard.Location = new System.Drawing.Point(40, 49);
            this.lblWard.Name = "lblWard";
            this.lblWard.Size = new System.Drawing.Size(100, 14);
            this.lblWard.TabIndex = 33;
            this.lblWard.Text = "病区";
            // 
            // lblBatch
            // 
            this.lblBatch.AcceptsReturn = true;
            this.lblBatch.BackColor = System.Drawing.Color.White;
            this.lblBatch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblBatch.Location = new System.Drawing.Point(83, 136);
            this.lblBatch.Name = "lblBatch";
            this.lblBatch.Size = new System.Drawing.Size(65, 14);
            this.lblBatch.TabIndex = 0;
            this.lblBatch.Text = "频次";
            // 
            // Label_DrawerName
            // 
            this.Label_DrawerName.BackColor = System.Drawing.Color.White;
            this.Label_DrawerName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Label_DrawerName.Location = new System.Drawing.Point(281, 117);
            this.Label_DrawerName.Name = "Label_DrawerName";
            this.Label_DrawerName.Size = new System.Drawing.Size(85, 14);
            this.Label_DrawerName.TabIndex = 1;
            this.Label_DrawerName.Text = "录入";
            // 
            // lblEndDT
            // 
            this.lblEndDT.BackColor = System.Drawing.Color.White;
            this.lblEndDT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblEndDT.Location = new System.Drawing.Point(82, 117);
            this.lblEndDT.Name = "lblEndDT";
            this.lblEndDT.Size = new System.Drawing.Size(112, 14);
            this.lblEndDT.TabIndex = 2;
            this.lblEndDT.Text = "停止日期";
            // 
            // lblPatient
            // 
            this.lblPatient.BackColor = System.Drawing.Color.White;
            this.lblPatient.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblPatient.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatient.Location = new System.Drawing.Point(34, 17);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(121, 22);
            this.lblPatient.TabIndex = 3;
            this.lblPatient.Text = "患者";
            // 
            // lblDoctor
            // 
            this.lblDoctor.BackColor = System.Drawing.Color.White;
            this.lblDoctor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblDoctor.Location = new System.Drawing.Point(40, 70);
            this.lblDoctor.Name = "lblDoctor";
            this.lblDoctor.Size = new System.Drawing.Size(120, 14);
            this.lblDoctor.TabIndex = 4;
            this.lblDoctor.Text = "医生";
            // 
            // lblStartDT
            // 
            this.lblStartDT.BackColor = System.Drawing.Color.White;
            this.lblStartDT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblStartDT.Location = new System.Drawing.Point(82, 94);
            this.lblStartDT.Name = "lblStartDT";
            this.lblStartDT.Size = new System.Drawing.Size(115, 14);
            this.lblStartDT.TabIndex = 6;
            this.lblStartDT.Text = "开始日期";
            // 
            // lblBedNo
            // 
            this.lblBedNo.BackColor = System.Drawing.Color.White;
            this.lblBedNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblBedNo.Location = new System.Drawing.Point(281, 94);
            this.lblBedNo.Name = "lblBedNo";
            this.lblBedNo.Size = new System.Drawing.Size(78, 14);
            this.lblBedNo.TabIndex = 0;
            this.lblBedNo.Text = "床号";
            // 
            // lblWeight
            // 
            this.lblWeight.BackColor = System.Drawing.Color.White;
            this.lblWeight.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblWeight.Location = new System.Drawing.Point(282, 49);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(36, 14);
            this.lblWeight.TabIndex = 1;
            this.lblWeight.Text = "体重";
            // 
            // lblCaseID
            // 
            this.lblCaseID.BackColor = System.Drawing.Color.White;
            this.lblCaseID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblCaseID.Location = new System.Drawing.Point(281, 73);
            this.lblCaseID.Name = "lblCaseID";
            this.lblCaseID.Size = new System.Drawing.Size(96, 14);
            this.lblCaseID.TabIndex = 3;
            this.lblCaseID.Text = "住院号";
            // 
            // lblSex
            // 
            this.lblSex.BackColor = System.Drawing.Color.White;
            this.lblSex.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblSex.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSex.Location = new System.Drawing.Point(189, 20);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(30, 16);
            this.lblSex.TabIndex = 4;
            this.lblSex.Text = "性别";
            // 
            // lblAge
            // 
            this.lblAge.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblAge.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAge.Location = new System.Drawing.Point(266, 20);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(39, 16);
            this.lblAge.TabIndex = 34;
            this.lblAge.Text = "年龄";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(245, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 21;
            this.label4.Text = "体重：";
            // 
            // pnlInfo
            // 
            this.pnlInfo.AutoScroll = true;
            this.pnlInfo.AutoSize = true;
            this.pnlInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(235)))), ((int)(((byte)(214)))));
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(386, 109);
            this.pnlInfo.TabIndex = 0;
            // 
            // Information
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.lblStartDT);
            this.Controls.Add(this.lblBatch);
            this.Controls.Add(this.lblEndDT);
            this.Controls.Add(this.lblWard);
            this.Controls.Add(this.Label_DrawerName);
            this.Controls.Add(this.lblDoctor);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblSex);
            this.Controls.Add(this.lblPatient);
            this.Controls.Add(this.lblCaseID);
            this.Controls.Add(this.lblWeight);
            this.Controls.Add(this.lblBedNo);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Name = "Information";
            this.Size = new System.Drawing.Size(383, 381);
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dgv1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox lblPatient;
        private System.Windows.Forms.TextBox lblEndDT;
        private System.Windows.Forms.TextBox Label_DrawerName;
        private System.Windows.Forms.TextBox lblBatch;
        private System.Windows.Forms.TextBox lblWard;
        private System.Windows.Forms.TextBox lblDoctor;
        private System.Windows.Forms.TextBox lblStartDT;
        private System.Windows.Forms.TextBox lblBedNo;
        private System.Windows.Forms.TextBox lblWeight;
        private System.Windows.Forms.TextBox lblCaseID;
        private System.Windows.Forms.TextBox lblSex;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn FregCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn pishi;
        private System.Windows.Forms.DataGridViewTextBoxColumn UniPreparationID;
        private System.Windows.Forms.TextBox lblAge;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.FlowLayoutPanel pnlInfo;
    }
}
