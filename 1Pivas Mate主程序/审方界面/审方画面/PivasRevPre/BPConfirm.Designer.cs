namespace PivasRevPre
{
    partial class BPConfirm
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
            this.components = new System.ComponentModel.Container();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvCensorItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDrugAName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDrugBName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvReferenName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCheckResultID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCPRecordID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDrugACode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDrugBCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flpBackPre = new System.Windows.Forms.FlowLayoutPanel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.维护ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddBackPre = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox1.ForeColor = System.Drawing.Color.Gray;
            this.richTextBox1.Location = new System.Drawing.Point(3, 418);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(505, 68);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "描述";
            this.richTextBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.richTextBox1_KeyPress);
            this.richTextBox1.Click += new System.EventHandler(this.richTextBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(14, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 33);
            this.label1.TabIndex = 1;
            this.label1.Text = "退单";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Red;
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(824, 47);
            this.panel1.TabIndex = 4;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(718, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 23);
            this.label8.TabIndex = 4;
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(633, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "当前药师：";
            this.label7.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(295, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "label4";
            this.label4.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Red;
            this.panel2.Location = new System.Drawing.Point(-4, 490);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(825, 10);
            this.panel2.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(535, 433);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "帐号:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(535, 460);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "密码:";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(567, 428);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(93, 21);
            this.txtCode.TabIndex = 0;
            this.txtCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCode_KeyPress);
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(567, 456);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(93, 21);
            this.txtPass.TabIndex = 1;
            this.txtPass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPass_KeyPress);
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToResizeRows = false;
            this.dgvResult.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvResult.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvResult.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.dgvCensorItem,
            this.dgvDrugAName,
            this.dgvDrugBName,
            this.dgvDescription,
            this.dgvReferenName,
            this.dgvCheckResultID,
            this.dgvCPRecordID,
            this.dgvLevel,
            this.dgvDrugACode,
            this.dgvDrugBCode});
            this.dgvResult.Location = new System.Drawing.Point(3, 72);
            this.dgvResult.MultiSelect = false;
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.RowHeadersVisible = false;
            this.dgvResult.RowTemplate.Height = 23;
            this.dgvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResult.Size = new System.Drawing.Size(816, 240);
            this.dgvResult.TabIndex = 8;
            this.dgvResult.DoubleClick += new System.EventHandler(this.dgvResult_DoubleClick);
            this.dgvResult.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResult_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "checked";
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.Width = 30;
            // 
            // dgvCensorItem
            // 
            this.dgvCensorItem.DataPropertyName = "CensorItem";
            this.dgvCensorItem.HeaderText = "审方结果";
            this.dgvCensorItem.Name = "dgvCensorItem";
            this.dgvCensorItem.ReadOnly = true;
            this.dgvCensorItem.Width = 70;
            // 
            // dgvDrugAName
            // 
            this.dgvDrugAName.DataPropertyName = "DrugAName";
            this.dgvDrugAName.HeaderText = "药品A";
            this.dgvDrugAName.Name = "dgvDrugAName";
            this.dgvDrugAName.ReadOnly = true;
            this.dgvDrugAName.Width = 150;
            // 
            // dgvDrugBName
            // 
            this.dgvDrugBName.DataPropertyName = "DrugBName";
            this.dgvDrugBName.HeaderText = "药品B";
            this.dgvDrugBName.Name = "dgvDrugBName";
            this.dgvDrugBName.ReadOnly = true;
            this.dgvDrugBName.Width = 150;
            // 
            // dgvDescription
            // 
            this.dgvDescription.DataPropertyName = "Description";
            this.dgvDescription.HeaderText = "描述";
            this.dgvDescription.Name = "dgvDescription";
            this.dgvDescription.ReadOnly = true;
            this.dgvDescription.Width = 300;
            // 
            // dgvReferenName
            // 
            this.dgvReferenName.DataPropertyName = "ReferenName";
            this.dgvReferenName.HeaderText = "参考文献";
            this.dgvReferenName.Name = "dgvReferenName";
            this.dgvReferenName.ReadOnly = true;
            // 
            // dgvCheckResultID
            // 
            this.dgvCheckResultID.DataPropertyName = "CheckResultID";
            this.dgvCheckResultID.HeaderText = "CheckResultID";
            this.dgvCheckResultID.Name = "dgvCheckResultID";
            this.dgvCheckResultID.ReadOnly = true;
            this.dgvCheckResultID.Visible = false;
            // 
            // dgvCPRecordID
            // 
            this.dgvCPRecordID.DataPropertyName = "CPRecordID";
            this.dgvCPRecordID.HeaderText = "CPRecordID";
            this.dgvCPRecordID.Name = "dgvCPRecordID";
            this.dgvCPRecordID.ReadOnly = true;
            this.dgvCPRecordID.Visible = false;
            // 
            // dgvLevel
            // 
            this.dgvLevel.DataPropertyName = "Level";
            this.dgvLevel.HeaderText = "Level";
            this.dgvLevel.Name = "dgvLevel";
            this.dgvLevel.ReadOnly = true;
            this.dgvLevel.Visible = false;
            // 
            // dgvDrugACode
            // 
            this.dgvDrugACode.DataPropertyName = "DrugACode";
            this.dgvDrugACode.HeaderText = "DrugACode";
            this.dgvDrugACode.Name = "dgvDrugACode";
            this.dgvDrugACode.ReadOnly = true;
            this.dgvDrugACode.Visible = false;
            // 
            // dgvDrugBCode
            // 
            this.dgvDrugBCode.DataPropertyName = "DrugBCode";
            this.dgvDrugBCode.HeaderText = "DrugBCode";
            this.dgvDrugBCode.Name = "dgvDrugBCode";
            this.dgvDrugBCode.ReadOnly = true;
            this.dgvDrugBCode.Visible = false;
            // 
            // flpBackPre
            // 
            this.flpBackPre.AutoScroll = true;
            this.flpBackPre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.flpBackPre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpBackPre.ContextMenuStrip = this.contextMenuStrip1;
            this.flpBackPre.Location = new System.Drawing.Point(3, 339);
            this.flpBackPre.Name = "flpBackPre";
            this.flpBackPre.Size = new System.Drawing.Size(815, 75);
            this.flpBackPre.TabIndex = 9;
            this.flpBackPre.MouseDown += new System.Windows.Forms.MouseEventHandler(this.flpBackPre_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.维护ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // 维护ToolStripMenuItem
            // 
            this.维护ToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.维护ToolStripMenuItem.Name = "维护ToolStripMenuItem";
            this.维护ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.维护ToolStripMenuItem.Text = "维护";
            this.维护ToolStripMenuItem.Click += new System.EventHandler(this.维护ToolStripMenuItem_Click);
            // 
            // btnAddBackPre
            // 
            this.btnAddBackPre.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddBackPre.Location = new System.Drawing.Point(87, 315);
            this.btnAddBackPre.Name = "btnAddBackPre";
            this.btnAddBackPre.Size = new System.Drawing.Size(24, 22);
            this.btnAddBackPre.TabIndex = 10;
            this.btnAddBackPre.Text = "+";
            this.btnAddBackPre.UseVisualStyleBackColor = true;
            this.btnAddBackPre.Click += new System.EventHandler(this.btnAddBackPre_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(161, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "选择需要保存的系统审方结果";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 320);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "人工审核意见";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(174, 53);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "全选";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::PivasRevPre.Properties.Resources.delete_16;
            this.btnCancel.Location = new System.Drawing.Point(744, 424);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(61, 56);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Image = global::PivasRevPre.Properties.Resources.tick_16;
            this.btnOK.Location = new System.Drawing.Point(669, 424);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(61, 56);
            this.btnOK.TabIndex = 2;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.名称});
            this.dataGridView1.Location = new System.Drawing.Point(490, 344);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(248, 128);
            this.dataGridView1.TabIndex = 14;
            this.dataGridView1.Tag = "0";
            this.dataGridView1.Visible = false;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // 名称
            // 
            this.名称.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.名称.DataPropertyName = "describe";
            this.名称.HeaderText = "Column2";
            this.名称.Name = "名称";
            this.名称.ReadOnly = true;
            // 
            // BPConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(820, 494);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnAddBackPre);
            this.Controls.Add(this.flpBackPre);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvResult);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.richTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BPConfirm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CPConfirm";
            this.Load += new System.EventHandler(this.CPConfirm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BPConfirm_MouseDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtCode;
        public System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.FlowLayoutPanel flpBackPre;
        private System.Windows.Forms.Button btnAddBackPre;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCensorItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDrugAName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDrugBName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvReferenName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCheckResultID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCPRecordID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDrugACode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDrugBCode;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 维护ToolStripMenuItem;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 名称;
    }
}