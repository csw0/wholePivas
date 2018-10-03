namespace PivasFreqRule
{
    partial class TimeRule
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label_add = new System.Windows.Forms.Label();
            this.label_freg = new System.Windows.Forms.Label();
            this.label_delete = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(8, 425);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "1.所有批次时间不能重叠";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(3, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(643, 385);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(33, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "批次号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(152, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "给药起始时间";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(335, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "给药结束时间";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(511, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "启用";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Location = new System.Drawing.Point(6, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(629, 1);
            this.panel2.TabIndex = 12;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(643, 30);
            this.panel3.TabIndex = 13;
            // 
            // label_add
            // 
            this.label_add.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(107)))), ((int)(((byte)(225)))));
            this.label_add.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_add.ForeColor = System.Drawing.Color.White;
            this.label_add.Location = new System.Drawing.Point(300, 435);
            this.label_add.Name = "label_add";
            this.label_add.Size = new System.Drawing.Size(53, 23);
            this.label_add.TabIndex = 14;
            this.label_add.Text = "增加";
            this.label_add.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_add.Visible = false;
            this.label_add.MouseLeave += new System.EventHandler(this.label_add_MouseLeave);
            this.label_add.Click += new System.EventHandler(this.label_add_Click);
            this.label_add.MouseHover += new System.EventHandler(this.label_add_MouseHover);
            // 
            // label_freg
            // 
            this.label_freg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(107)))), ((int)(((byte)(225)))));
            this.label_freg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_freg.ForeColor = System.Drawing.Color.White;
            this.label_freg.Location = new System.Drawing.Point(418, 434);
            this.label_freg.Name = "label_freg";
            this.label_freg.Size = new System.Drawing.Size(53, 23);
            this.label_freg.TabIndex = 18;
            this.label_freg.Text = "更新";
            this.label_freg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_freg.Visible = false;
            this.label_freg.MouseLeave += new System.EventHandler(this.label_freg_MouseLeave);
            this.label_freg.Click += new System.EventHandler(this.label_freg_Click_1);
            this.label_freg.MouseHover += new System.EventHandler(this.label_freg_MouseHover);
            // 
            // label_delete
            // 
            this.label_delete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(107)))), ((int)(((byte)(225)))));
            this.label_delete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_delete.ForeColor = System.Drawing.Color.White;
            this.label_delete.Location = new System.Drawing.Point(359, 434);
            this.label_delete.Name = "label_delete";
            this.label_delete.Size = new System.Drawing.Size(53, 23);
            this.label_delete.TabIndex = 19;
            this.label_delete.Text = "删除";
            this.label_delete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_delete.Visible = false;
            this.label_delete.MouseLeave += new System.EventHandler(this.label_delete_MouseLeave);
            this.label_delete.Click += new System.EventHandler(this.label_delete_Click);
            this.label_delete.MouseHover += new System.EventHandler(this.label_delete_MouseHover);
            // 
            // button3
            // 
            this.button3.Image = global::PivasFreqRule.Properties.Resources.save_161;
            this.button3.Location = new System.Drawing.Point(489, 431);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(42, 23);
            this.button3.TabIndex = 22;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.label_freg_Click);
            // 
            // button2
            // 
            this.button2.Image = global::PivasFreqRule.Properties.Resources.plus_161;
            this.button2.Location = new System.Drawing.Point(537, 431);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(42, 23);
            this.button2.TabIndex = 21;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.label_add_Click);
            // 
            // button1
            // 
            this.button1.Image = global::PivasFreqRule.Properties.Resources.delete_16;
            this.button1.Location = new System.Drawing.Point(584, 431);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(42, 23);
            this.button1.TabIndex = 20;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.label_delete_Click);
            // 
            // label6
            // 
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(176, 425);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(173, 18);
            this.label6.TabIndex = 23;
            this.label6.Text = "2.批次累积需要包含24个小时";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(8, 443);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(345, 18);
            this.label7.TabIndex = 24;
            this.label7.Text = "3.建议最后一个批次结束时间为第一个批次开始时间减一分钟";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Silver;
            this.panel4.Location = new System.Drawing.Point(12, 424);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(625, 1);
            this.panel4.TabIndex = 13;
            // 
            // TimeRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label_delete);
            this.Controls.Add(this.label_add);
            this.Controls.Add(this.label_freg);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Name = "TimeRule";
            this.Size = new System.Drawing.Size(646, 473);
            this.Load += new System.EventHandler(this.TimeRule_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label_add;
        private System.Windows.Forms.Label label_freg;
        private System.Windows.Forms.Label label_delete;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel4;
    }
}
