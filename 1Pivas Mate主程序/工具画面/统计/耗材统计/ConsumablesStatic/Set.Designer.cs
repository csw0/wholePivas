namespace ConsumablesStatic
{
    partial class Set
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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.RuleId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConsumablesCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ComnsumableMaterial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.添加耗材ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgv2 = new System.Windows.Forms.DataGridView();
            this.RuleId1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrugName1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConsumablesName1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConsumablesQuantity1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.添加统计条件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv2)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RuleId,
            this.PType,
            this.ConsumablesCode,
            this.ComnsumableMaterial,
            this.ConQuantity});
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(3, 3);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(508, 351);
            this.dgv.TabIndex = 3;
            this.dgv.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_CellMouseDown);
            // 
            // RuleId
            // 
            this.RuleId.DataPropertyName = "RuleId";
            this.RuleId.HeaderText = "";
            this.RuleId.Name = "RuleId";
            this.RuleId.ReadOnly = true;
            this.RuleId.Visible = false;
            // 
            // PType
            // 
            this.PType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PType.DataPropertyName = "drugtype";
            this.PType.HeaderText = "处方类型";
            this.PType.Name = "PType";
            this.PType.ReadOnly = true;
            this.PType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ConsumablesCode
            // 
            this.ConsumablesCode.DataPropertyName = "ConsumablesCode";
            this.ConsumablesCode.HeaderText = "耗材编号";
            this.ConsumablesCode.Name = "ConsumablesCode";
            this.ConsumablesCode.ReadOnly = true;
            this.ConsumablesCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ConsumablesCode.Visible = false;
            // 
            // ComnsumableMaterial
            // 
            this.ComnsumableMaterial.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ComnsumableMaterial.DataPropertyName = "ConsumablesName";
            this.ComnsumableMaterial.HeaderText = "耗材名称";
            this.ComnsumableMaterial.Name = "ComnsumableMaterial";
            this.ComnsumableMaterial.ReadOnly = true;
            this.ComnsumableMaterial.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ConQuantity
            // 
            this.ConQuantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ConQuantity.DataPropertyName = "ConsumablesQuantity";
            this.ConQuantity.HeaderText = "耗材数量";
            this.ConQuantity.Name = "ConQuantity";
            this.ConQuantity.ReadOnly = true;
            this.ConQuantity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加耗材ToolStripMenuItem,
            this.添加配置ToolStripMenuItem,
            this.添加统计条件ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(514, 33);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 添加耗材ToolStripMenuItem
            // 
            this.添加耗材ToolStripMenuItem.Name = "添加耗材ToolStripMenuItem";
            this.添加耗材ToolStripMenuItem.Size = new System.Drawing.Size(100, 29);
            this.添加耗材ToolStripMenuItem.Text = "耗材维护";
            this.添加耗材ToolStripMenuItem.Click += new System.EventHandler(this.添加耗材ToolStripMenuItem_Click);
            // 
            // 添加配置ToolStripMenuItem
            // 
            this.添加配置ToolStripMenuItem.Name = "添加配置ToolStripMenuItem";
            this.添加配置ToolStripMenuItem.Size = new System.Drawing.Size(100, 29);
            this.添加配置ToolStripMenuItem.Text = "添加配置";
            this.添加配置ToolStripMenuItem.Click += new System.EventHandler(this.添加配置ToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(-4, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(522, 385);
            this.tabControl1.TabIndex = 5;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgv);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(514, 357);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "基础耗材";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgv2);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(514, 357);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "特殊耗材";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgv2
            // 
            this.dgv2.AllowUserToAddRows = false;
            this.dgv2.AllowUserToDeleteRows = false;
            this.dgv2.AllowUserToResizeRows = false;
            this.dgv2.BackgroundColor = System.Drawing.Color.White;
            this.dgv2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RuleId1,
            this.DrugName1,
            this.ConsumablesName1,
            this.ConsumablesQuantity1});
            this.dgv2.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgv2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv2.Location = new System.Drawing.Point(3, 3);
            this.dgv2.Name = "dgv2";
            this.dgv2.ReadOnly = true;
            this.dgv2.RowHeadersVisible = false;
            this.dgv2.RowTemplate.Height = 23;
            this.dgv2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv2.Size = new System.Drawing.Size(508, 351);
            this.dgv2.TabIndex = 4;
            this.dgv2.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv2_CellMouseDown);
            // 
            // RuleId1
            // 
            this.RuleId1.HeaderText = "规则编号";
            this.RuleId1.Name = "RuleId1";
            this.RuleId1.ReadOnly = true;
            this.RuleId1.Visible = false;
            // 
            // DrugName1
            // 
            this.DrugName1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DrugName1.FillWeight = 200F;
            this.DrugName1.HeaderText = "药品名称";
            this.DrugName1.Name = "DrugName1";
            this.DrugName1.ReadOnly = true;
            this.DrugName1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ConsumablesName1
            // 
            this.ConsumablesName1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ConsumablesName1.FillWeight = 120F;
            this.ConsumablesName1.HeaderText = "耗材名称";
            this.ConsumablesName1.Name = "ConsumablesName1";
            this.ConsumablesName1.ReadOnly = true;
            this.ConsumablesName1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ConsumablesQuantity1
            // 
            this.ConsumablesQuantity1.HeaderText = "数量";
            this.ConsumablesQuantity1.Name = "ConsumablesQuantity1";
            this.ConsumablesQuantity1.ReadOnly = true;
            this.ConsumablesQuantity1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ConsumablesQuantity1.Width = 70;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // 添加统计条件ToolStripMenuItem
            // 
            this.添加统计条件ToolStripMenuItem.Name = "添加统计条件ToolStripMenuItem";
            this.添加统计条件ToolStripMenuItem.Size = new System.Drawing.Size(100, 29);
            this.添加统计条件ToolStripMenuItem.Text = "统计条件";
            this.添加统计条件ToolStripMenuItem.Click += new System.EventHandler(this.添加统计条件ToolStripMenuItem_Click);
            // 
            // Set
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 413);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(520, 441);
            this.MinimizeBox = false;
            this.Name = "Set";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            this.Load += new System.EventHandler(this.ConsumableNum_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv2)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 添加耗材ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加配置ToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgv2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn RuleId;
        private System.Windows.Forms.DataGridViewTextBoxColumn PType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConsumablesCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComnsumableMaterial;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn RuleId1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugName1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConsumablesName1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConsumablesQuantity1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripMenuItem 添加统计条件ToolStripMenuItem;

    }
}