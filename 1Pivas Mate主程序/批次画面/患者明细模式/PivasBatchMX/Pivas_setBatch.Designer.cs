namespace PivasBatchMX
{
    partial class Pivas_setBatch
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
            this.Panel_Batch = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Label_ClickClose = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.FPanel_SetBatch = new System.Windows.Forms.FlowLayoutPanel();
            this.Fpanel_set = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.Panel_KUL = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.Label_L = new System.Windows.Forms.Label();
            this.Label_ = new System.Windows.Forms.Label();
            this.Label_K = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Label_BatchRule = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Panel_Batch.SuspendLayout();
            this.panel1.SuspendLayout();
            this.FPanel_SetBatch.SuspendLayout();
            this.Fpanel_set.SuspendLayout();
            this.Panel_KUL.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Batch
            // 
            this.Panel_Batch.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Panel_Batch.Controls.Add(this.panel1);
            this.Panel_Batch.Controls.Add(this.Label_ClickClose);
            this.Panel_Batch.Controls.Add(this.label1);
            this.Panel_Batch.Controls.Add(this.FPanel_SetBatch);
            this.Panel_Batch.Location = new System.Drawing.Point(1, 1);
            this.Panel_Batch.Name = "Panel_Batch";
            this.Panel_Batch.Size = new System.Drawing.Size(242, 339);
            this.Panel_Batch.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(253)))));
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Location = new System.Drawing.Point(2, 279);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(238, 60);
            this.panel1.TabIndex = 19;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.ForeColor = System.Drawing.Color.Gray;
            this.textBox2.Location = new System.Drawing.Point(5, 32);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(212, 23);
            this.textBox2.TabIndex = 18;
            this.textBox2.Text = "原因说明";
            this.textBox2.Enter += new System.EventHandler(this.textBox2_Enter);
            this.textBox2.Leave += new System.EventHandler(this.textBox2_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(221, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 14);
            this.label4.TabIndex = 16;
            this.label4.Text = "+";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.ForeColor = System.Drawing.Color.Gray;
            this.textBox1.Location = new System.Drawing.Point(7, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(208, 23);
            this.textBox1.TabIndex = 15;
            this.textBox1.Text = "修改原因";
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.IntegralHeight = false;
            this.comboBox1.Items.AddRange(new object[] {
            "请选择修改原因"});
            this.comboBox1.Location = new System.Drawing.Point(4, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(214, 22);
            this.comboBox1.TabIndex = 17;
            // 
            // Label_ClickClose
            // 
            this.Label_ClickClose.BackColor = System.Drawing.Color.PaleTurquoise;
            this.Label_ClickClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label_ClickClose.Image = global::PivasBatchMX.Properties.Resources.K_L;
            this.Label_ClickClose.Location = new System.Drawing.Point(217, 1);
            this.Label_ClickClose.Name = "Label_ClickClose";
            this.Label_ClickClose.Size = new System.Drawing.Size(19, 21);
            this.Label_ClickClose.TabIndex = 7;
            this.Label_ClickClose.Text = "×";
            this.Label_ClickClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label_ClickClose.Click += new System.EventHandler(this.button1_Click);
            this.Label_ClickClose.MouseLeave += new System.EventHandler(this.Label_ClickClose_MouseLeave);
            this.Label_ClickClose.MouseHover += new System.EventHandler(this.Label_ClickClose_MouseHover);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Image = global::PivasBatchMX.Properties.Resources.批次标题;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(241, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "修改批次";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDown);
            // 
            // FPanel_SetBatch
            // 
            this.FPanel_SetBatch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(253)))));
            this.FPanel_SetBatch.Controls.Add(this.Fpanel_set);
            this.FPanel_SetBatch.Controls.Add(this.Panel_KUL);
            this.FPanel_SetBatch.Controls.Add(this.label2);
            this.FPanel_SetBatch.Controls.Add(this.Label_BatchRule);
            this.FPanel_SetBatch.Location = new System.Drawing.Point(1, 26);
            this.FPanel_SetBatch.Margin = new System.Windows.Forms.Padding(0);
            this.FPanel_SetBatch.Name = "FPanel_SetBatch";
            this.FPanel_SetBatch.Size = new System.Drawing.Size(240, 251);
            this.FPanel_SetBatch.TabIndex = 5;
            this.FPanel_SetBatch.Paint += new System.Windows.Forms.PaintEventHandler(this.FPanel_SetBatch_Paint);
            // 
            // Fpanel_set
            // 
            this.Fpanel_set.Controls.Add(this.label3);
            this.Fpanel_set.Location = new System.Drawing.Point(1, 1);
            this.Fpanel_set.Margin = new System.Windows.Forms.Padding(1);
            this.Fpanel_set.Name = "Fpanel_set";
            this.Fpanel_set.Size = new System.Drawing.Size(237, 128);
            this.Fpanel_set.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(230, 1);
            this.label3.TabIndex = 3;
            // 
            // Panel_KUL
            // 
            this.Panel_KUL.Controls.Add(this.label12);
            this.Panel_KUL.Controls.Add(this.label11);
            this.Panel_KUL.Controls.Add(this.Label_L);
            this.Panel_KUL.Controls.Add(this.Label_);
            this.Panel_KUL.Controls.Add(this.Label_K);
            this.Panel_KUL.Location = new System.Drawing.Point(1, 131);
            this.Panel_KUL.Margin = new System.Windows.Forms.Padding(1);
            this.Panel_KUL.Name = "Panel_KUL";
            this.Panel_KUL.Size = new System.Drawing.Size(237, 38);
            this.Panel_KUL.TabIndex = 12;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Silver;
            this.label12.Location = new System.Drawing.Point(6, 34);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(230, 1);
            this.label12.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Silver;
            this.label11.Location = new System.Drawing.Point(6, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(230, 1);
            this.label11.TabIndex = 2;
            // 
            // Label_L
            // 
            this.Label_L.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.Label_L.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label_L.Font = new System.Drawing.Font("宋体", 10F);
            this.Label_L.Location = new System.Drawing.Point(147, 8);
            this.Label_L.Name = "Label_L";
            this.Label_L.Size = new System.Drawing.Size(54, 23);
            this.Label_L.TabIndex = 1;
            this.Label_L.Text = "L";
            this.Label_L.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_L.Click += new System.EventHandler(this.Label_L_Click);
            // 
            // Label_
            // 
            this.Label_.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label_.Font = new System.Drawing.Font("宋体", 10F);
            this.Label_.Location = new System.Drawing.Point(46, 8);
            this.Label_.Name = "Label_";
            this.Label_.Size = new System.Drawing.Size(23, 23);
            this.Label_.TabIndex = 1;
            this.Label_.Text = "#";
            this.Label_.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_.Click += new System.EventHandler(this.Label_L_Click);
            this.Label_.MouseLeave += new System.EventHandler(this.Label_L_MouseLeave);
            this.Label_.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_L_MouseMove);
            // 
            // Label_K
            // 
            this.Label_K.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label_K.Font = new System.Drawing.Font("宋体", 10F);
            this.Label_K.Location = new System.Drawing.Point(104, 8);
            this.Label_K.Name = "Label_K";
            this.Label_K.Size = new System.Drawing.Size(23, 23);
            this.Label_K.TabIndex = 1;
            this.Label_K.Text = "K";
            this.Label_K.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_K.Click += new System.EventHandler(this.Label_L_Click);
            this.Label_K.MouseLeave += new System.EventHandler(this.Label_L_MouseLeave);
            this.Label_K.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_L_MouseMove);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(1, 171);
            this.label2.Margin = new System.Windows.Forms.Padding(1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(236, 26);
            this.label2.TabIndex = 11;
            this.label2.Text = "批次规则";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_BatchRule
            // 
            this.Label_BatchRule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(253)))));
            this.Label_BatchRule.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label_BatchRule.Location = new System.Drawing.Point(1, 199);
            this.Label_BatchRule.Margin = new System.Windows.Forms.Padding(1);
            this.Label_BatchRule.Name = "Label_BatchRule";
            this.Label_BatchRule.Size = new System.Drawing.Size(235, 54);
            this.Label_BatchRule.TabIndex = 10;
            this.Label_BatchRule.Text = "批次规则";
            this.Label_BatchRule.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Pivas_setBatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(242, 344);
            this.Controls.Add(this.Panel_Batch);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Pivas_setBatch";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Pivas_setBatch";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Pivas_setBatch_Load);
            this.Panel_Batch.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.FPanel_SetBatch.ResumeLayout(false);
            this.Fpanel_set.ResumeLayout(false);
            this.Panel_KUL.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Batch;
        public System.Windows.Forms.FlowLayoutPanel FPanel_SetBatch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel Fpanel_set;
        private System.Windows.Forms.Panel Panel_KUL;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Label_BatchRule;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Label_ClickClose;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Label_L;
        private System.Windows.Forms.Label Label_;
        private System.Windows.Forms.Label Label_K;
    }
}