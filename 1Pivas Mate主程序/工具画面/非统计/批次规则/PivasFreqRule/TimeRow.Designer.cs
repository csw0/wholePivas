namespace PivasFreqRule
{
    partial class TimeRow
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
            this.label1 = new System.Windows.Forms.Label();
            this.Start_Time = new System.Windows.Forms.DateTimePicker();
            this.End_Time = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(36, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // Start_Time
            // 
            this.Start_Time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Start_Time.Location = new System.Drawing.Point(3, 8);
            this.Start_Time.Name = "Start_Time";
            this.Start_Time.ShowUpDown = true;
            this.Start_Time.Size = new System.Drawing.Size(86, 21);
            this.Start_Time.TabIndex = 1;
            this.Start_Time.Value = new System.DateTime(2013, 8, 14, 0, 0, 0, 0);
            // 
            // End_Time
            // 
            this.End_Time.CalendarForeColor = System.Drawing.Color.Black;
            this.End_Time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.End_Time.Location = new System.Drawing.Point(6, 8);
            this.End_Time.Name = "End_Time";
            this.End_Time.ShowUpDown = true;
            this.End_Time.Size = new System.Drawing.Size(86, 21);
            this.End_Time.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.Start_Time);
            this.panel1.Location = new System.Drawing.Point(149, -6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(95, 33);
            this.panel1.TabIndex = 4;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.End_Time);
            this.panel2.Location = new System.Drawing.Point(329, -6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(95, 33);
            this.panel2.TabIndex = 5;
            this.panel2.Click += new System.EventHandler(this.panel2_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel4.Location = new System.Drawing.Point(16, 28);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(590, 1);
            this.panel4.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(504, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 23);
            this.label2.TabIndex = 13;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // TimeRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "TimeRow";
            this.Size = new System.Drawing.Size(622, 30);
            this.Load += new System.EventHandler(this.TimeRow_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.DateTimePicker Start_Time;
        public System.Windows.Forms.DateTimePicker End_Time;
        public System.Windows.Forms.Label label1;
    }
}
