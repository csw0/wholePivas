namespace SetSyn
{
    partial class frmSetSyn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetSyn));
            this.Label_Head = new System.Windows.Forms.Label();
            this.Panel_U = new System.Windows.Forms.Panel();
            this.Panel_Close = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Panel_Other = new System.Windows.Forms.Panel();
            this.Label_Other = new System.Windows.Forms.Label();
            this.Panel_SynSet = new System.Windows.Forms.Panel();
            this.Label_SynSet = new System.Windows.Forms.Label();
            this.Panel_SaveLog = new System.Windows.Forms.Panel();
            this.Pic_Save_ONOFF = new System.Windows.Forms.PictureBox();
            this.Label_SaveLog = new System.Windows.Forms.Label();
            this.Panel_Repeat = new System.Windows.Forms.Panel();
            this.Pic_R_ONOFF = new System.Windows.Forms.PictureBox();
            this.Label_Repeat = new System.Windows.Forms.Label();
            this.Panel_Cycle = new System.Windows.Forms.Panel();
            this.Label_Cycle = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.Panel_Other.SuspendLayout();
            this.Panel_SynSet.SuspendLayout();
            this.Panel_SaveLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Save_ONOFF)).BeginInit();
            this.Panel_Repeat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_R_ONOFF)).BeginInit();
            this.Panel_Cycle.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label_Head
            // 
            this.Label_Head.BackColor = System.Drawing.Color.Transparent;
            this.Label_Head.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Head.ForeColor = System.Drawing.Color.Black;
            this.Label_Head.Location = new System.Drawing.Point(2, 1);
            this.Label_Head.Name = "Label_Head";
            this.Label_Head.Size = new System.Drawing.Size(597, 31);
            this.Label_Head.TabIndex = 2;
            this.Label_Head.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_Head.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_Head_MouseDown);
            // 
            // Panel_U
            // 
            this.Panel_U.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(227)))), ((int)(((byte)(249)))));
            this.Panel_U.Location = new System.Drawing.Point(195, 35);
            this.Panel_U.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_U.Name = "Panel_U";
            this.Panel_U.Size = new System.Drawing.Size(400, 460);
            this.Panel_U.TabIndex = 5;
            // 
            // Panel_Close
            // 
            this.Panel_Close.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Close.BackgroundImage = global::SetSyn.Properties.Resources.icon_03;
            this.Panel_Close.Location = new System.Drawing.Point(9, 7);
            this.Panel_Close.Name = "Panel_Close";
            this.Panel_Close.Size = new System.Drawing.Size(34, 24);
            this.Panel_Close.TabIndex = 6;
            this.Panel_Close.Click += new System.EventHandler(this.Panel_Close_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::SetSyn.Properties.Resources.分隔线;
            this.panel2.Location = new System.Drawing.Point(194, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1, 462);
            this.panel2.TabIndex = 4;
            // 
            // Panel_Other
            // 
            this.Panel_Other.BackgroundImage = global::SetSyn.Properties.Resources.其它;
            this.Panel_Other.Controls.Add(this.Label_Other);
            this.Panel_Other.Location = new System.Drawing.Point(4, 260);
            this.Panel_Other.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Other.Name = "Panel_Other";
            this.Panel_Other.Size = new System.Drawing.Size(190, 44);
            this.Panel_Other.TabIndex = 3;
            this.Panel_Other.DockChanged += new System.EventHandler(this.Panel_Other_Click);
            this.Panel_Other.Click += new System.EventHandler(this.Panel_Other_Click);
            // 
            // Label_Other
            // 
            this.Label_Other.BackColor = System.Drawing.Color.Transparent;
            this.Label_Other.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.Label_Other.ForeColor = System.Drawing.Color.Black;
            this.Label_Other.Location = new System.Drawing.Point(0, 0);
            this.Label_Other.Name = "Label_Other";
            this.Label_Other.Size = new System.Drawing.Size(190, 42);
            this.Label_Other.TabIndex = 0;
            this.Label_Other.Tag = "5";
            this.Label_Other.Text = "       其它";
            this.Label_Other.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label_Other.Click += new System.EventHandler(this.Panel_Other_Click);
            // 
            // Panel_SynSet
            // 
            this.Panel_SynSet.BackgroundImage = global::SetSyn.Properties.Resources.同步设置;
            this.Panel_SynSet.Controls.Add(this.Label_SynSet);
            this.Panel_SynSet.Location = new System.Drawing.Point(4, 170);
            this.Panel_SynSet.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_SynSet.Name = "Panel_SynSet";
            this.Panel_SynSet.Size = new System.Drawing.Size(190, 44);
            this.Panel_SynSet.TabIndex = 3;
            this.Panel_SynSet.Click += new System.EventHandler(this.Panel_SynSet_Click);
            // 
            // Label_SynSet
            // 
            this.Label_SynSet.BackColor = System.Drawing.Color.Transparent;
            this.Label_SynSet.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.Label_SynSet.ForeColor = System.Drawing.Color.Black;
            this.Label_SynSet.Location = new System.Drawing.Point(1, 0);
            this.Label_SynSet.Name = "Label_SynSet";
            this.Label_SynSet.Size = new System.Drawing.Size(190, 42);
            this.Label_SynSet.TabIndex = 0;
            this.Label_SynSet.Tag = "4";
            this.Label_SynSet.Text = "       同步设置";
            this.Label_SynSet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label_SynSet.Click += new System.EventHandler(this.Panel_SynSet_Click);
            // 
            // Panel_SaveLog
            // 
            this.Panel_SaveLog.BackgroundImage = global::SetSyn.Properties.Resources.保存日志;
            this.Panel_SaveLog.Controls.Add(this.Pic_Save_ONOFF);
            this.Panel_SaveLog.Controls.Add(this.Label_SaveLog);
            this.Panel_SaveLog.Location = new System.Drawing.Point(4, 125);
            this.Panel_SaveLog.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_SaveLog.Name = "Panel_SaveLog";
            this.Panel_SaveLog.Size = new System.Drawing.Size(190, 44);
            this.Panel_SaveLog.TabIndex = 3;
            // 
            // Pic_Save_ONOFF
            // 
            this.Pic_Save_ONOFF.BackColor = System.Drawing.Color.Transparent;
            this.Pic_Save_ONOFF.Image = global::SetSyn.Properties.Resources.开;
            this.Pic_Save_ONOFF.Location = new System.Drawing.Point(135, 13);
            this.Pic_Save_ONOFF.Name = "Pic_Save_ONOFF";
            this.Pic_Save_ONOFF.Size = new System.Drawing.Size(51, 19);
            this.Pic_Save_ONOFF.TabIndex = 1;
            this.Pic_Save_ONOFF.TabStop = false;
            this.Pic_Save_ONOFF.Tag = "0";
            this.Pic_Save_ONOFF.Click += new System.EventHandler(this.Pic_ONOFF_Click);
            // 
            // Label_SaveLog
            // 
            this.Label_SaveLog.BackColor = System.Drawing.Color.Transparent;
            this.Label_SaveLog.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.Label_SaveLog.ForeColor = System.Drawing.Color.Black;
            this.Label_SaveLog.Location = new System.Drawing.Point(0, 0);
            this.Label_SaveLog.Name = "Label_SaveLog";
            this.Label_SaveLog.Size = new System.Drawing.Size(190, 42);
            this.Label_SaveLog.TabIndex = 0;
            this.Label_SaveLog.Tag = "2";
            this.Label_SaveLog.Text = "       保存日志";
            this.Label_SaveLog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label_SaveLog.Click += new System.EventHandler(this.Label_SaveLog_Click);
            // 
            // Panel_Repeat
            // 
            this.Panel_Repeat.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Repeat.BackgroundImage = global::SetSyn.Properties.Resources.失败重复;
            this.Panel_Repeat.Controls.Add(this.Pic_R_ONOFF);
            this.Panel_Repeat.Controls.Add(this.Label_Repeat);
            this.Panel_Repeat.Location = new System.Drawing.Point(4, 80);
            this.Panel_Repeat.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Repeat.Name = "Panel_Repeat";
            this.Panel_Repeat.Size = new System.Drawing.Size(190, 44);
            this.Panel_Repeat.TabIndex = 3;
            // 
            // Pic_R_ONOFF
            // 
            this.Pic_R_ONOFF.BackColor = System.Drawing.Color.Transparent;
            this.Pic_R_ONOFF.Image = global::SetSyn.Properties.Resources.开;
            this.Pic_R_ONOFF.Location = new System.Drawing.Point(135, 13);
            this.Pic_R_ONOFF.Name = "Pic_R_ONOFF";
            this.Pic_R_ONOFF.Size = new System.Drawing.Size(51, 19);
            this.Pic_R_ONOFF.TabIndex = 1;
            this.Pic_R_ONOFF.TabStop = false;
            this.Pic_R_ONOFF.Tag = "0";
            this.Pic_R_ONOFF.Click += new System.EventHandler(this.Pic_R_ONOFF_Click);
            // 
            // Label_Repeat
            // 
            this.Label_Repeat.BackColor = System.Drawing.Color.Transparent;
            this.Label_Repeat.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.Label_Repeat.ForeColor = System.Drawing.Color.Black;
            this.Label_Repeat.Location = new System.Drawing.Point(0, 0);
            this.Label_Repeat.Name = "Label_Repeat";
            this.Label_Repeat.Size = new System.Drawing.Size(190, 42);
            this.Label_Repeat.TabIndex = 0;
            this.Label_Repeat.Tag = "1";
            this.Label_Repeat.Text = "       失败重复";
            this.Label_Repeat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label_Repeat.Click += new System.EventHandler(this.Label_Repeat_Click);
            // 
            // Panel_Cycle
            // 
            this.Panel_Cycle.BackColor = System.Drawing.Color.White;
            this.Panel_Cycle.BackgroundImage = global::SetSyn.Properties.Resources.周期;
            this.Panel_Cycle.Controls.Add(this.Label_Cycle);
            this.Panel_Cycle.Font = new System.Drawing.Font("宋体", 9F);
            this.Panel_Cycle.Location = new System.Drawing.Point(4, 35);
            this.Panel_Cycle.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Cycle.Name = "Panel_Cycle";
            this.Panel_Cycle.Size = new System.Drawing.Size(190, 44);
            this.Panel_Cycle.TabIndex = 3;
            this.Panel_Cycle.Click += new System.EventHandler(this.Panel_Cycle_Click);
            // 
            // Label_Cycle
            // 
            this.Label_Cycle.BackColor = System.Drawing.Color.Transparent;
            this.Label_Cycle.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.Label_Cycle.ForeColor = System.Drawing.Color.Black;
            this.Label_Cycle.Location = new System.Drawing.Point(0, 0);
            this.Label_Cycle.Name = "Label_Cycle";
            this.Label_Cycle.Size = new System.Drawing.Size(190, 42);
            this.Label_Cycle.TabIndex = 0;
            this.Label_Cycle.Tag = "0";
            this.Label_Cycle.Text = "       周期";
            this.Label_Cycle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label_Cycle.Click += new System.EventHandler(this.Panel_Cycle_Click);
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::SetSyn.Properties.Resources.同步设置;
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(4, 215);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(190, 44);
            this.panel3.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 42);
            this.label1.TabIndex = 1;
            this.label1.Tag = "6";
            this.label1.Text = "       数据删除";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // frmSetSyn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Snow;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(600, 500);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.Panel_Close);
            this.Controls.Add(this.Panel_U);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.Panel_Other);
            this.Controls.Add(this.Panel_SynSet);
            this.Controls.Add(this.Panel_SaveLog);
            this.Controls.Add(this.Panel_Repeat);
            this.Controls.Add(this.Panel_Cycle);
            this.Controls.Add(this.Label_Head);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSetSyn";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            this.TransparencyKey = System.Drawing.Color.Snow;
            this.Load += new System.EventHandler(this.SetSynF_Load);
            this.Panel_Other.ResumeLayout(false);
            this.Panel_SynSet.ResumeLayout(false);
            this.Panel_SaveLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Save_ONOFF)).EndInit();
            this.Panel_Repeat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Pic_R_ONOFF)).EndInit();
            this.Panel_Cycle.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Label_Head;
        private System.Windows.Forms.Panel Panel_Cycle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel Panel_Repeat;
        private System.Windows.Forms.Panel Panel_SaveLog;
        private System.Windows.Forms.Panel Panel_SynSet;
        private System.Windows.Forms.Panel Panel_Other;
        private System.Windows.Forms.Label Label_Cycle;
        private System.Windows.Forms.Label Label_Repeat;
        private System.Windows.Forms.Label Label_SaveLog;
        private System.Windows.Forms.Label Label_SynSet;
        private System.Windows.Forms.Label Label_Other;
        private System.Windows.Forms.Panel Panel_U;
        private System.Windows.Forms.Panel Panel_Close;
        private System.Windows.Forms.PictureBox Pic_Save_ONOFF;
        private System.Windows.Forms.PictureBox Pic_R_ONOFF;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;

    }
}

