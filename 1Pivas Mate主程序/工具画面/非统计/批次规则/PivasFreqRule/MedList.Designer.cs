namespace PivasFreqRule
{
    partial class MedList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MedList));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tvMedlist = new System.Windows.Forms.TreeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvMedlist = new System.Windows.Forms.DataGridView();
            this.btnCancel1 = new System.Windows.Forms.Button();
            this.btnOK1 = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedlist)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(186, 238);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(49, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(239, 238);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(49, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(2, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(312, 289);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tvMedlist);
            this.tabPage1.Controls.Add(this.btnOK);
            this.tabPage1.Controls.Add(this.btnCancel);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(304, 263);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "药品列表";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tvMedlist
            // 
            this.tvMedlist.Location = new System.Drawing.Point(0, 3);
            this.tvMedlist.Name = "tvMedlist";
            this.tvMedlist.Size = new System.Drawing.Size(301, 229);
            this.tvMedlist.TabIndex = 2;
            this.tvMedlist.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvMedlist_NodeMouseClick);
            this.tvMedlist.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvMedlist_AfterExpand);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvMedlist);
            this.tabPage2.Controls.Add(this.btnCancel1);
            this.tabPage2.Controls.Add(this.btnOK1);
            this.tabPage2.Controls.Add(this.txtSearch);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(304, 263);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "搜索";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvMedlist
            // 
            this.dgvMedlist.AllowUserToAddRows = false;
            this.dgvMedlist.AllowUserToResizeRows = false;
            this.dgvMedlist.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvMedlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMedlist.Location = new System.Drawing.Point(4, 30);
            this.dgvMedlist.Name = "dgvMedlist";
            this.dgvMedlist.ReadOnly = true;
            this.dgvMedlist.RowHeadersVisible = false;
            this.dgvMedlist.RowTemplate.Height = 23;
            this.dgvMedlist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMedlist.Size = new System.Drawing.Size(296, 205);
            this.dgvMedlist.TabIndex = 11;
            this.dgvMedlist.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvMedlist_KeyPress);
            // 
            // btnCancel1
            // 
            this.btnCancel1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel1.Location = new System.Drawing.Point(237, 237);
            this.btnCancel1.Name = "btnCancel1";
            this.btnCancel1.Size = new System.Drawing.Size(51, 23);
            this.btnCancel1.TabIndex = 10;
            this.btnCancel1.Text = "取消";
            this.btnCancel1.UseVisualStyleBackColor = true;
            this.btnCancel1.Click += new System.EventHandler(this.btnCancel1_Click);
            // 
            // btnOK1
            // 
            this.btnOK1.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnOK1.Location = new System.Drawing.Point(176, 237);
            this.btnOK1.Name = "btnOK1";
            this.btnOK1.Size = new System.Drawing.Size(55, 23);
            this.btnOK1.TabIndex = 9;
            this.btnOK1.Text = "确定";
            this.btnOK1.UseVisualStyleBackColor = true;
            this.btnOK1.Click += new System.EventHandler(this.btnOK1_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(72, 5);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(228, 21);
            this.txtSearch.TabIndex = 6;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(14, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 14);
            this.label1.TabIndex = 5;
            this.label1.Text = "关键字";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(107)))), ((int)(((byte)(225)))));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(1, -8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(315, 36);
            this.panel1.TabIndex = 3;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(8, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "药品成分添加";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(107)))), ((int)(((byte)(225)))));
            this.panel2.Location = new System.Drawing.Point(-1, 322);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(315, 17);
            this.panel2.TabIndex = 4;
            // 
            // MedList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 326);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MedList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MedList";
            this.Load += new System.EventHandler(this.MedList_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedlist)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnCancel1;
        private System.Windows.Forms.Button btnOK1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView tvMedlist;
        private System.Windows.Forms.DataGridView dgvMedlist;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
    }
}