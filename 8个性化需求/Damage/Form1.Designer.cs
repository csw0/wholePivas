namespace Damage
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_drug = new System.Windows.Forms.TextBox();
            this.dgv_drug = new System.Windows.Forms.DataGridView();
            this.DrugName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.comb_spec = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tex_count = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_price = new System.Windows.Forms.TextBox();
            this.txt_amount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.timepick_bs = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.bt_ok = new System.Windows.Forms.Button();
            this.bt_cx = new System.Windows.Forms.Button();
            this.comb_reason = new System.Windows.Forms.ComboBox();
            this.comb_zrr = new System.Windows.Forms.ComboBox();
            this.comb_tbr = new System.Windows.Forms.ComboBox();
            this.lb_notice = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_ph = new System.Windows.Forms.TextBox();
            this.bt_reason = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_xq = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_drug)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "药品名称：";
            // 
            // txt_drug
            // 
            this.txt_drug.BackColor = System.Drawing.Color.White;
            this.txt_drug.Location = new System.Drawing.Point(83, 30);
            this.txt_drug.Name = "txt_drug";
            this.txt_drug.Size = new System.Drawing.Size(189, 21);
            this.txt_drug.TabIndex = 1;
            this.txt_drug.TextChanged += new System.EventHandler(this.txt_drug_TextChanged);
            this.txt_drug.Enter += new System.EventHandler(this.txt_drug_Enter);
            // 
            // dgv_drug
            // 
            this.dgv_drug.AllowUserToAddRows = false;
            this.dgv_drug.AllowUserToDeleteRows = false;
            this.dgv_drug.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_drug.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_drug.BackgroundColor = System.Drawing.Color.White;
            this.dgv_drug.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_drug.ColumnHeadersVisible = false;
            this.dgv_drug.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DrugName});
            this.dgv_drug.Location = new System.Drawing.Point(83, 52);
            this.dgv_drug.Name = "dgv_drug";
            this.dgv_drug.ReadOnly = true;
            this.dgv_drug.RowHeadersVisible = false;
            this.dgv_drug.RowTemplate.Height = 23;
            this.dgv_drug.Size = new System.Drawing.Size(189, 123);
            this.dgv_drug.TabIndex = 2;
            this.dgv_drug.Visible = false;
            this.dgv_drug.DoubleClick += new System.EventHandler(this.dgv_drug_DoubleClick);
            // 
            // DrugName
            // 
            this.DrugName.DataPropertyName = "DrugName";
            this.DrugName.HeaderText = "药品名称";
            this.DrugName.Name = "DrugName";
            this.DrugName.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "规  格：";
            // 
            // comb_spec
            // 
            this.comb_spec.FormattingEnabled = true;
            this.comb_spec.Location = new System.Drawing.Point(83, 70);
            this.comb_spec.Name = "comb_spec";
            this.comb_spec.Size = new System.Drawing.Size(189, 20);
            this.comb_spec.TabIndex = 4;
            this.comb_spec.Enter += new System.EventHandler(this.comb_spec_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "数  量：";
            // 
            // tex_count
            // 
            this.tex_count.Location = new System.Drawing.Point(83, 110);
            this.tex_count.Name = "tex_count";
            this.tex_count.Size = new System.Drawing.Size(189, 21);
            this.tex_count.TabIndex = 6;
            this.tex_count.TextChanged += new System.EventHandler(this.tex_count_TextChanged);
            this.tex_count.Enter += new System.EventHandler(this.tex_count_Enter);
            this.tex_count.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "单  价：";
            // 
            // txt_price
            // 
            this.txt_price.Location = new System.Drawing.Point(83, 150);
            this.txt_price.Name = "txt_price";
            this.txt_price.Size = new System.Drawing.Size(189, 21);
            this.txt_price.TabIndex = 8;
            this.txt_price.TextChanged += new System.EventHandler(this.txt_price_TextChanged);
            this.txt_price.Enter += new System.EventHandler(this.txt_price_Enter);
            // 
            // txt_amount
            // 
            this.txt_amount.Location = new System.Drawing.Point(83, 190);
            this.txt_amount.Name = "txt_amount";
            this.txt_amount.ReadOnly = true;
            this.txt_amount.Size = new System.Drawing.Size(189, 21);
            this.txt_amount.TabIndex = 9;
            this.txt_amount.Enter += new System.EventHandler(this.txt_amount_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "金  额：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(326, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "报损时间：";
            // 
            // timepick_bs
            // 
            this.timepick_bs.Location = new System.Drawing.Point(393, 30);
            this.timepick_bs.Name = "timepick_bs";
            this.timepick_bs.Size = new System.Drawing.Size(200, 21);
            this.timepick_bs.TabIndex = 13;
            this.timepick_bs.Enter += new System.EventHandler(this.timepick_bs_Enter);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(326, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "报损原因";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(326, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 15;
            this.label8.Text = "责任人：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(326, 153);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 16;
            this.label9.Text = "填报人：";
            // 
            // bt_ok
            // 
            this.bt_ok.Location = new System.Drawing.Point(529, 227);
            this.bt_ok.Name = "bt_ok";
            this.bt_ok.Size = new System.Drawing.Size(75, 26);
            this.bt_ok.TabIndex = 19;
            this.bt_ok.Text = "提交";
            this.bt_ok.UseVisualStyleBackColor = true;
            this.bt_ok.Click += new System.EventHandler(this.bt_ok_Click);
            // 
            // bt_cx
            // 
            this.bt_cx.Location = new System.Drawing.Point(328, 227);
            this.bt_cx.Name = "bt_cx";
            this.bt_cx.Size = new System.Drawing.Size(75, 26);
            this.bt_cx.TabIndex = 20;
            this.bt_cx.Text = "查询";
            this.bt_cx.UseVisualStyleBackColor = true;
            this.bt_cx.Click += new System.EventHandler(this.bt_cx_Click);
            // 
            // comb_reason
            // 
            this.comb_reason.FormattingEnabled = true;
            this.comb_reason.Location = new System.Drawing.Point(393, 70);
            this.comb_reason.Name = "comb_reason";
            this.comb_reason.Size = new System.Drawing.Size(200, 20);
            this.comb_reason.TabIndex = 21;
            this.comb_reason.Enter += new System.EventHandler(this.comb_reason_Enter);
            this.comb_reason.Leave += new System.EventHandler(this.comb_reason_Leave);
            // 
            // comb_zrr
            // 
            this.comb_zrr.FormattingEnabled = true;
            this.comb_zrr.Location = new System.Drawing.Point(393, 110);
            this.comb_zrr.Name = "comb_zrr";
            this.comb_zrr.Size = new System.Drawing.Size(200, 20);
            this.comb_zrr.TabIndex = 22;
            this.comb_zrr.Enter += new System.EventHandler(this.comb_zrr_Enter);
            // 
            // comb_tbr
            // 
            this.comb_tbr.FormattingEnabled = true;
            this.comb_tbr.Location = new System.Drawing.Point(393, 150);
            this.comb_tbr.Name = "comb_tbr";
            this.comb_tbr.Size = new System.Drawing.Size(200, 20);
            this.comb_tbr.TabIndex = 23;
            this.comb_tbr.Enter += new System.EventHandler(this.comb_tbr_Enter);
            // 
            // lb_notice
            // 
            this.lb_notice.Location = new System.Drawing.Point(413, 195);
            this.lb_notice.Name = "lb_notice";
            this.lb_notice.Size = new System.Drawing.Size(99, 16);
            this.lb_notice.TabIndex = 24;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 234);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 25;
            this.label10.Text = "批  号：";
            // 
            // txt_ph
            // 
            this.txt_ph.Location = new System.Drawing.Point(83, 231);
            this.txt_ph.Name = "txt_ph";
            this.txt_ph.Size = new System.Drawing.Size(189, 21);
            this.txt_ph.TabIndex = 26;
            this.txt_ph.Enter += new System.EventHandler(this.txt_ph_Enter);
            // 
            // bt_reason
            // 
            this.bt_reason.BackColor = System.Drawing.Color.White;
            this.bt_reason.Location = new System.Drawing.Point(599, 68);
            this.bt_reason.Name = "bt_reason";
            this.bt_reason.Size = new System.Drawing.Size(31, 23);
            this.bt_reason.TabIndex = 27;
            this.bt_reason.Text = "+";
            this.bt_reason.UseVisualStyleBackColor = false;
            this.bt_reason.Visible = false;
            this.bt_reason.Click += new System.EventHandler(this.bt_reason_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(326, 193);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 28;
            this.label11.Text = "效  期：";
            // 
            // txt_xq
            // 
            this.txt_xq.Location = new System.Drawing.Point(393, 190);
            this.txt_xq.Name = "txt_xq";
            this.txt_xq.Size = new System.Drawing.Size(200, 21);
            this.txt_xq.TabIndex = 29;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(665, 274);
            this.Controls.Add(this.txt_xq);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.bt_reason);
            this.Controls.Add(this.txt_ph);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lb_notice);
            this.Controls.Add(this.dgv_drug);
            this.Controls.Add(this.comb_tbr);
            this.Controls.Add(this.comb_zrr);
            this.Controls.Add(this.comb_reason);
            this.Controls.Add(this.bt_ok);
            this.Controls.Add(this.bt_cx);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.timepick_bs);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_amount);
            this.Controls.Add(this.txt_price);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tex_count);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comb_spec);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_drug);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "添加";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_drug)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_drug;
        private System.Windows.Forms.DataGridView dgv_drug;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comb_spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tex_count;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_price;
        private System.Windows.Forms.TextBox txt_amount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker timepick_bs;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button bt_ok;
        private System.Windows.Forms.Button bt_cx;
        private System.Windows.Forms.ComboBox comb_reason;
        private System.Windows.Forms.ComboBox comb_zrr;
        private System.Windows.Forms.ComboBox comb_tbr;
        private System.Windows.Forms.Label lb_notice;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_ph;
        private System.Windows.Forms.Button bt_reason;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_xq;
    }
}

