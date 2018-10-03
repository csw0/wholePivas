namespace PivasFreqRule
{
    partial class WardDrug
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WardDrug));
            this.pnlWard = new System.Windows.Forms.Panel();
            this.Label1 = new System.Windows.Forms.Label();
            this.pnlMed2 = new System.Windows.Forms.FlowLayoutPanel();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.DrugCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrugName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnAddUp = new System.Windows.Forms.Button();
            this.pnlMed2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlWard
            // 
            this.pnlWard.AutoScroll = true;
            this.pnlWard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlWard.Location = new System.Drawing.Point(8, 62);
            this.pnlWard.Name = "pnlWard";
            this.pnlWard.Size = new System.Drawing.Size(182, 394);
            this.pnlWard.TabIndex = 10;
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label1.ForeColor = System.Drawing.Color.Gray;
            this.Label1.Location = new System.Drawing.Point(11, 40);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(100, 23);
            this.Label1.TabIndex = 9;
            this.Label1.Text = "病区";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlMed2
            // 
            this.pnlMed2.AutoScroll = true;
            this.pnlMed2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMed2.Controls.Add(this.dgv);
            this.pnlMed2.Location = new System.Drawing.Point(195, 62);
            this.pnlMed2.Name = "pnlMed2";
            this.pnlMed2.Size = new System.Drawing.Size(448, 394);
            this.pnlMed2.TabIndex = 11;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Delete,
            this.DrugCode,
            this.DrugName,
            this.Spec});
            this.dgv.GridColor = System.Drawing.Color.White;
            this.dgv.Location = new System.Drawing.Point(3, 3);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(440, 386);
            this.dgv.TabIndex = 0;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            // 
            // Delete
            // 
            this.Delete.HeaderText = "";
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Width = 30;
            // 
            // DrugCode
            // 
            this.DrugCode.HeaderText = "药品编码";
            this.DrugCode.Name = "DrugCode";
            this.DrugCode.ReadOnly = true;
            this.DrugCode.Width = 80;
            // 
            // DrugName
            // 
            this.DrugName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DrugName.HeaderText = "药品名称";
            this.DrugName.Name = "DrugName";
            this.DrugName.ReadOnly = true;
            this.DrugName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DrugName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Spec
            // 
            this.Spec.FillWeight = 30F;
            this.Spec.HeaderText = "药品规格";
            this.Spec.Name = "Spec";
            this.Spec.ReadOnly = true;
            this.Spec.Width = 76;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Location = new System.Drawing.Point(6, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(637, 35);
            this.panel5.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(70, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "当前病区";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "当前病区：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(325, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(275, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "在此处添加的药品，排批次时会自动排为空包（K）";
            // 
            // btnAddUp
            // 
            this.btnAddUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddUp.Image = ((System.Drawing.Image)(resources.GetObject("btnAddUp.Image")));
            this.btnAddUp.Location = new System.Drawing.Point(607, 38);
            this.btnAddUp.Name = "btnAddUp";
            this.btnAddUp.Size = new System.Drawing.Size(32, 23);
            this.btnAddUp.TabIndex = 12;
            this.toolTip1.SetToolTip(this.btnAddUp, "添加药品");
            this.btnAddUp.UseVisualStyleBackColor = true;
            this.btnAddUp.Click += new System.EventHandler(this.btnAddUp_Click);
            // 
            // WardDrug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.btnAddUp);
            this.Controls.Add(this.pnlMed2);
            this.Controls.Add(this.pnlWard);
            this.Controls.Add(this.Label1);
            this.Name = "WardDrug";
            this.Size = new System.Drawing.Size(646, 461);
            this.Load += new System.EventHandler(this.WardDrug_Load);
            this.pnlMed2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Panel pnlWard;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.FlowLayoutPanel pnlMed2;
        private System.Windows.Forms.Panel panel5;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnAddUp;
    }
}
