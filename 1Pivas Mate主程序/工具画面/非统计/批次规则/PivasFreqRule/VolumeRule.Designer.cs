namespace PivasFreqRule
{
    partial class VolumeRule
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
            this.pnlLimit = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlWard = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Check_OnlyInse = new System.Windows.Forms.CheckBox();
            this.But_UseAll = new System.Windows.Forms.Button();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLimit
            // 
            this.pnlLimit.AutoScroll = true;
            this.pnlLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLimit.Location = new System.Drawing.Point(234, 60);
            this.pnlLimit.Name = "pnlLimit";
            this.pnlLimit.Size = new System.Drawing.Size(401, 349);
            this.pnlLimit.TabIndex = 3;
            // 
            // pnlWard
            // 
            this.pnlWard.AutoScroll = true;
            this.pnlWard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlWard.Location = new System.Drawing.Point(19, 60);
            this.pnlWard.Name = "pnlWard";
            this.pnlWard.Size = new System.Drawing.Size(200, 390);
            this.pnlWard.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(247, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "批次";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(387, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "下限(毫升/ML)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(502, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "上限(毫升/ML)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(234, 418);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(355, 30);
            this.pBar.TabIndex = 9;
            this.pBar.Visible = false;
            // 
            // button1
            // 
            this.button1.Image = global::PivasFreqRule.Properties.Resources.Refresh3;
            this.button1.Location = new System.Drawing.Point(594, 413);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(42, 40);
            this.button1.TabIndex = 8;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(424, 419);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(167, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "当修改批次数量后,请按此键->";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(490, 434);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "刷新各病区数据";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.Check_OnlyInse);
            this.panel5.Controls.Add(this.But_UseAll);
            this.panel5.Location = new System.Drawing.Point(14, 9);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(637, 33);
            this.panel5.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(70, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "当前病区";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "当前病区：";
            // 
            // Check_OnlyInse
            // 
            this.Check_OnlyInse.AutoSize = true;
            this.Check_OnlyInse.Location = new System.Drawing.Point(245, 9);
            this.Check_OnlyInse.Name = "Check_OnlyInse";
            this.Check_OnlyInse.Size = new System.Drawing.Size(96, 16);
            this.Check_OnlyInse.TabIndex = 6;
            this.Check_OnlyInse.Text = "只插增量数据";
            this.Check_OnlyInse.UseVisualStyleBackColor = true;
            // 
            // But_UseAll
            // 
            this.But_UseAll.Location = new System.Drawing.Point(352, 3);
            this.But_UseAll.Name = "But_UseAll";
            this.But_UseAll.Size = new System.Drawing.Size(102, 27);
            this.But_UseAll.TabIndex = 5;
            this.But_UseAll.Text = "应用到所有病区";
            this.But_UseAll.UseVisualStyleBackColor = true;
            this.But_UseAll.Click += new System.EventHandler(this.But_UseAll_Click);
            // 
            // VolumeRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.pBar);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pnlWard);
            this.Controls.Add(this.pnlLimit);
            this.Name = "VolumeRule";
            this.Size = new System.Drawing.Size(726, 465);
            this.Load += new System.EventHandler(this.VolumeRule_Load);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlLimit;
        private System.Windows.Forms.Panel pnlWard;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel5;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox Check_OnlyInse;
        private System.Windows.Forms.Button But_UseAll;
    }
}
