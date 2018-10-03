namespace DeskNo
{
    partial class DeskNoSort
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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.IsOpen = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DeskNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsPTY = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsKSS = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsHLY = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsYYY = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.queren = new System.Windows.Forms.DataGridViewButtonColumn();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IsOpen,
            this.DeskNo,
            this.IsPTY,
            this.IsKSS,
            this.IsHLY,
            this.IsYYY,
            this.queren});
            this.dgv.Location = new System.Drawing.Point(0, 32);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowHeadersWidth = 20;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(307, 305);
            this.dgv.TabIndex = 15;
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            // 
            // IsOpen
            // 
            this.IsOpen.DataPropertyName = "IsOpen";
            this.IsOpen.FillWeight = 46.51162F;
            this.IsOpen.HeaderText = "";
            this.IsOpen.Name = "IsOpen";
            this.IsOpen.Width = 50;
            // 
            // DeskNo
            // 
            this.DeskNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DeskNo.DataPropertyName = "DeskNo";
            this.DeskNo.FillWeight = 2F;
            this.DeskNo.HeaderText = "仓位号";
            this.DeskNo.Name = "DeskNo";
            this.DeskNo.ReadOnly = true;
            // 
            // IsPTY
            // 
            this.IsPTY.DataPropertyName = "IsPTY";
            this.IsPTY.FillWeight = 63.52914F;
            this.IsPTY.HeaderText = "普";
            this.IsPTY.Name = "IsPTY";
            this.IsPTY.Width = 35;
            // 
            // IsKSS
            // 
            this.IsKSS.DataPropertyName = "IsKSS";
            this.IsKSS.FillWeight = 65.91943F;
            this.IsKSS.HeaderText = "抗";
            this.IsKSS.Name = "IsKSS";
            this.IsKSS.Width = 35;
            // 
            // IsHLY
            // 
            this.IsHLY.DataPropertyName = "IsHLY";
            this.IsHLY.FillWeight = 68.91286F;
            this.IsHLY.HeaderText = "化";
            this.IsHLY.Name = "IsHLY";
            this.IsHLY.Width = 35;
            // 
            // IsYYY
            // 
            this.IsYYY.DataPropertyName = "IsYYY";
            this.IsYYY.FillWeight = 72.56962F;
            this.IsYYY.HeaderText = "营";
            this.IsYYY.Name = "IsYYY";
            this.IsYYY.Width = 35;
            // 
            // queren
            // 
            this.queren.DataPropertyName = "a";
            this.queren.FillWeight = 205.1216F;
            this.queren.HeaderText = "确认";
            this.queren.Name = "queren";
            this.queren.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.queren.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.queren.Text = "";
            this.queren.Visible = false;
            this.queren.Width = 60;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.SystemColors.Control;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.checkBox1.Location = new System.Drawing.Point(3, 36);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 17;
            this.checkBox1.Text = "全部";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(318, 339);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(63, 30);
            this.button1.TabIndex = 3;
            this.button1.Text = "确认";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Image = global::DeskNo.Properties.Resources._21;
            this.label1.Location = new System.Drawing.Point(279, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 21);
            this.label1.TabIndex = 16;
            this.label1.Text = "×";
            this.label1.MouseLeave += new System.EventHandler(this.label1_MouseLeave);
            this.label1.Click += new System.EventHandler(this.label1_Click);
            this.label1.MouseHover += new System.EventHandler(this.label1_MouseHover);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel6.BackgroundImage = global::DeskNo.Properties.Resources._3;
            this.panel6.Controls.Add(this.button5);
            this.panel6.Controls.Add(this.button2);
            this.panel6.Controls.Add(this.button4);
            this.panel6.Location = new System.Drawing.Point(1, 337);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(305, 30);
            this.panel6.TabIndex = 14;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(113, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 3;
            this.button5.Text = "删除仓位号";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button2.Location = new System.Drawing.Point(222, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(67, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "排仓位号";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(5, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(74, 23);
            this.button4.TabIndex = 0;
            this.button4.Text = "添加仓位号";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Image = global::DeskNo.Properties.Resources._1;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(307, 32);
            this.label2.TabIndex = 13;
            this.label2.Text = "仓位设置界面";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label2_MouseDown);
            // 
            // DeskNoSort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(307, 368);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DeskNoSort";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "仓位维护";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsOpen;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeskNo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsPTY;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsKSS;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsHLY;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsYYY;
        private System.Windows.Forms.DataGridViewButtonColumn queren;
    }
}

