namespace PivasFreqRule
{
    partial class DFeg
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label_add = new System.Windows.Forms.Label();
            this.label_modify = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(246)))));
            this.panel2.Location = new System.Drawing.Point(1, 57);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(657, 448);
            this.panel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(107)))), ((int)(((byte)(225)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label_add);
            this.panel1.Controls.Add(this.label_modify);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(658, 23);
            this.panel1.TabIndex = 2;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(439, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "更新批次";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label_add
            // 
            this.label_add.AutoSize = true;
            this.label_add.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_add.ForeColor = System.Drawing.Color.White;
            this.label_add.Location = new System.Drawing.Point(581, 5);
            this.label_add.Name = "label_add";
            this.label_add.Size = new System.Drawing.Size(29, 12);
            this.label_add.TabIndex = 16;
            this.label_add.Text = "新增";
            this.label_add.Click += new System.EventHandler(this.button1_Click);
            // 
            // label_modify
            // 
            this.label_modify.AutoSize = true;
            this.label_modify.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_modify.Font = new System.Drawing.Font("宋体", 9F);
            this.label_modify.ForeColor = System.Drawing.Color.White;
            this.label_modify.Location = new System.Drawing.Point(618, 5);
            this.label_modify.Name = "label_modify";
            this.label_modify.Size = new System.Drawing.Size(29, 12);
            this.label_modify.TabIndex = 15;
            this.label_modify.Text = "修改";
            this.label_modify.Click += new System.EventHandler(this.button2_Click);
            // 
            // DFeg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(660, 508);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DFeg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PivasDFreg";
            this.Load += new System.EventHandler(this.PivasDFeg_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_add;
        private System.Windows.Forms.Label label_modify;
        //private titleDFreg titleDFreg1;
        private System.Windows.Forms.Label label1;
        //private titleDFreg titleDFreg2;

    }
}

