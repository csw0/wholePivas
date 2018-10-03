namespace MistakeCollection
{
    partial class Form1
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.选项ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.统计列ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.瓶签号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.错误节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.发现节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.差错人ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.记录人ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.描述ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.差错时间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.记录时间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.统计方法ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.按瓶签时间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.按差错人ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.按记录人ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(73, 3);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(110, 21);
            this.dateTimePicker1.TabIndex = 0;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(212, 3);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(111, 21);
            this.dateTimePicker2.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选项ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(704, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 选项ToolStripMenuItem
            // 
            this.选项ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.统计列ToolStripMenuItem,
            this.统计方法ToolStripMenuItem});
            this.选项ToolStripMenuItem.Name = "选项ToolStripMenuItem";
            this.选项ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.选项ToolStripMenuItem.Text = "选项";
            // 
            // 统计列ToolStripMenuItem
            // 
            this.统计列ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.瓶签号ToolStripMenuItem,
            this.错误节点ToolStripMenuItem,
            this.发现节点ToolStripMenuItem,
            this.差错人ToolStripMenuItem,
            this.记录人ToolStripMenuItem,
            this.描述ToolStripMenuItem,
            this.差错时间ToolStripMenuItem,
            this.记录时间ToolStripMenuItem});
            this.统计列ToolStripMenuItem.Name = "统计列ToolStripMenuItem";
            this.统计列ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.统计列ToolStripMenuItem.Text = "明细列";
            // 
            // 瓶签号ToolStripMenuItem
            // 
            this.瓶签号ToolStripMenuItem.Checked = true;
            this.瓶签号ToolStripMenuItem.CheckOnClick = true;
            this.瓶签号ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.瓶签号ToolStripMenuItem.Name = "瓶签号ToolStripMenuItem";
            this.瓶签号ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.瓶签号ToolStripMenuItem.Text = "瓶签号";
            // 
            // 错误节点ToolStripMenuItem
            // 
            this.错误节点ToolStripMenuItem.Checked = true;
            this.错误节点ToolStripMenuItem.CheckOnClick = true;
            this.错误节点ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.错误节点ToolStripMenuItem.Name = "错误节点ToolStripMenuItem";
            this.错误节点ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.错误节点ToolStripMenuItem.Text = "差错节点";
            // 
            // 发现节点ToolStripMenuItem
            // 
            this.发现节点ToolStripMenuItem.Checked = true;
            this.发现节点ToolStripMenuItem.CheckOnClick = true;
            this.发现节点ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.发现节点ToolStripMenuItem.Name = "发现节点ToolStripMenuItem";
            this.发现节点ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.发现节点ToolStripMenuItem.Text = "发现节点";
            // 
            // 差错人ToolStripMenuItem
            // 
            this.差错人ToolStripMenuItem.Checked = true;
            this.差错人ToolStripMenuItem.CheckOnClick = true;
            this.差错人ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.差错人ToolStripMenuItem.Name = "差错人ToolStripMenuItem";
            this.差错人ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.差错人ToolStripMenuItem.Text = "差错人";
            // 
            // 记录人ToolStripMenuItem
            // 
            this.记录人ToolStripMenuItem.Checked = true;
            this.记录人ToolStripMenuItem.CheckOnClick = true;
            this.记录人ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.记录人ToolStripMenuItem.Name = "记录人ToolStripMenuItem";
            this.记录人ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.记录人ToolStripMenuItem.Text = "记录人";
            // 
            // 描述ToolStripMenuItem
            // 
            this.描述ToolStripMenuItem.Checked = true;
            this.描述ToolStripMenuItem.CheckOnClick = true;
            this.描述ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.描述ToolStripMenuItem.Name = "描述ToolStripMenuItem";
            this.描述ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.描述ToolStripMenuItem.Text = "描述";
            // 
            // 差错时间ToolStripMenuItem
            // 
            this.差错时间ToolStripMenuItem.Checked = true;
            this.差错时间ToolStripMenuItem.CheckOnClick = true;
            this.差错时间ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.差错时间ToolStripMenuItem.Name = "差错时间ToolStripMenuItem";
            this.差错时间ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.差错时间ToolStripMenuItem.Text = "差错时间";
            // 
            // 记录时间ToolStripMenuItem
            // 
            this.记录时间ToolStripMenuItem.Checked = true;
            this.记录时间ToolStripMenuItem.CheckOnClick = true;
            this.记录时间ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.记录时间ToolStripMenuItem.Name = "记录时间ToolStripMenuItem";
            this.记录时间ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.记录时间ToolStripMenuItem.Text = "记录时间";
            // 
            // 统计方法ToolStripMenuItem
            // 
            this.统计方法ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.按瓶签时间ToolStripMenuItem,
            this.按差错人ToolStripMenuItem,
            this.按记录人ToolStripMenuItem});
            this.统计方法ToolStripMenuItem.Name = "统计方法ToolStripMenuItem";
            this.统计方法ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.统计方法ToolStripMenuItem.Text = "统计方法";
            // 
            // 按瓶签时间ToolStripMenuItem
            // 
            this.按瓶签时间ToolStripMenuItem.Checked = true;
            this.按瓶签时间ToolStripMenuItem.CheckOnClick = true;
            this.按瓶签时间ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.按瓶签时间ToolStripMenuItem.Name = "按瓶签时间ToolStripMenuItem";
            this.按瓶签时间ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.按瓶签时间ToolStripMenuItem.Text = "按瓶签时间";
            this.按瓶签时间ToolStripMenuItem.Click += new System.EventHandler(this.按瓶签时间ToolStripMenuItem_Click);
            // 
            // 按差错人ToolStripMenuItem
            // 
            this.按差错人ToolStripMenuItem.CheckOnClick = true;
            this.按差错人ToolStripMenuItem.Name = "按差错人ToolStripMenuItem";
            this.按差错人ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.按差错人ToolStripMenuItem.Text = "按差错人";
            this.按差错人ToolStripMenuItem.Click += new System.EventHandler(this.按差错人ToolStripMenuItem_Click);
            // 
            // 按记录人ToolStripMenuItem
            // 
            this.按记录人ToolStripMenuItem.CheckOnClick = true;
            this.按记录人ToolStripMenuItem.Name = "按记录人ToolStripMenuItem";
            this.按记录人ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.按记录人ToolStripMenuItem.Text = "按记录人";
            this.按记录人ToolStripMenuItem.Click += new System.EventHandler(this.按记录人ToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(189, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "--";
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(12, 30);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(200, 347);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.Click += new System.EventHandler(this.listView1_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(630, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(62, 21);
            this.button1.TabIndex = 5;
            this.button1.Text = "统  计";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.Location = new System.Drawing.Point(218, 30);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(474, 347);
            this.dataGridView1.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 389);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 选项ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 统计列ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 瓶签号ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 错误节点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 发现节点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 差错人ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 记录人ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 描述ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 差错时间ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 记录时间ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 统计方法ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 按瓶签时间ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 按差错人ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 按记录人ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

