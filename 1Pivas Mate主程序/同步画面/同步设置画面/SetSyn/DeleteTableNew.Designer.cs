namespace SetSyn
{
    partial class DeleteTableNew
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.Truncate = new System.Windows.Forms.Button();
            this.CB1 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CB7 = new System.Windows.Forms.CheckBox();
            this.CB15 = new System.Windows.Forms.CheckBox();
            this.CB5 = new System.Windows.Forms.CheckBox();
            this.CB22 = new System.Windows.Forms.CheckBox();
            this.CB8 = new System.Windows.Forms.CheckBox();
            this.CB20 = new System.Windows.Forms.CheckBox();
            this.CB6 = new System.Windows.Forms.CheckBox();
            this.CB18 = new System.Windows.Forms.CheckBox();
            this.CB17 = new System.Windows.Forms.CheckBox();
            this.CB19 = new System.Windows.Forms.CheckBox();
            this.CB12 = new System.Windows.Forms.CheckBox();
            this.CB2 = new System.Windows.Forms.CheckBox();
            this.CB16 = new System.Windows.Forms.CheckBox();
            this.CB14 = new System.Windows.Forms.CheckBox();
            this.CB3 = new System.Windows.Forms.CheckBox();
            this.CB13 = new System.Windows.Forms.CheckBox();
            this.CB4 = new System.Windows.Forms.CheckBox();
            this.CB23 = new System.Windows.Forms.CheckBox();
            this.CB9 = new System.Windows.Forms.CheckBox();
            this.CB11 = new System.Windows.Forms.CheckBox();
            this.CB21 = new System.Windows.Forms.CheckBox();
            this.CB10 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(26, 10);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "全选";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.checkBox1_MouseClick);
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Truncate
            // 
            this.Truncate.Location = new System.Drawing.Point(298, 409);
            this.Truncate.Name = "Truncate";
            this.Truncate.Size = new System.Drawing.Size(96, 45);
            this.Truncate.TabIndex = 6;
            this.Truncate.Text = "确认清空数据";
            this.Truncate.UseVisualStyleBackColor = true;
            this.Truncate.Click += new System.EventHandler(this.Truncate_Click);
            // 
            // CB1
            // 
            this.CB1.AutoSize = true;
            this.CB1.Location = new System.Drawing.Point(20, 35);
            this.CB1.Name = "CB1";
            this.CB1.Size = new System.Drawing.Size(84, 16);
            this.CB1.TabIndex = 1;
            this.CB1.Tag = "IVRecordDetail";
            this.CB1.Text = "瓶签明细表";
            this.CB1.UseVisualStyleBackColor = true;
            this.CB1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CB1_MouseClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CB7);
            this.groupBox1.Controls.Add(this.CB15);
            this.groupBox1.Controls.Add(this.CB5);
            this.groupBox1.Controls.Add(this.CB22);
            this.groupBox1.Controls.Add(this.CB8);
            this.groupBox1.Controls.Add(this.CB20);
            this.groupBox1.Controls.Add(this.CB6);
            this.groupBox1.Controls.Add(this.CB18);
            this.groupBox1.Controls.Add(this.CB17);
            this.groupBox1.Controls.Add(this.CB19);
            this.groupBox1.Controls.Add(this.CB12);
            this.groupBox1.Controls.Add(this.CB2);
            this.groupBox1.Controls.Add(this.CB16);
            this.groupBox1.Controls.Add(this.CB14);
            this.groupBox1.Controls.Add(this.CB3);
            this.groupBox1.Controls.Add(this.CB13);
            this.groupBox1.Controls.Add(this.CB4);
            this.groupBox1.Controls.Add(this.CB23);
            this.groupBox1.Controls.Add(this.CB9);
            this.groupBox1.Controls.Add(this.CB11);
            this.groupBox1.Controls.Add(this.CB21);
            this.groupBox1.Controls.Add(this.CB10);
            this.groupBox1.Controls.Add(this.CB1);
            this.groupBox1.Location = new System.Drawing.Point(6, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(388, 371);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            // 
            // CB7
            // 
            this.CB7.AutoSize = true;
            this.CB7.Location = new System.Drawing.Point(20, 107);
            this.CB7.Name = "CB7";
            this.CB7.Size = new System.Drawing.Size(84, 16);
            this.CB7.TabIndex = 29;
            this.CB7.Tag = "BPRecord";
            this.CB7.Text = "退方记录表";
            this.CB7.UseVisualStyleBackColor = true;
            this.CB7.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CB1_MouseClick);
            // 
            // CB15
            // 
            this.CB15.AutoSize = true;
            this.CB15.Location = new System.Drawing.Point(173, 207);
            this.CB15.Name = "CB15";
            this.CB15.Size = new System.Drawing.Size(72, 16);
            this.CB15.TabIndex = 37;
            this.CB15.Tag = "OrderMCPRule2";
            this.CB15.Text = "容积规则";
            this.CB15.UseVisualStyleBackColor = true;
            this.CB15.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CB1_MouseClick);
            // 
            // CB5
            // 
            this.CB5.AutoSize = true;
            this.CB5.Location = new System.Drawing.Point(20, 83);
            this.CB5.Name = "CB5";
            this.CB5.Size = new System.Drawing.Size(108, 16);
            this.CB5.TabIndex = 27;
            this.CB5.Tag = "CPDrug";
            this.CB5.Text = "审方药品记录表";
            this.CB5.UseVisualStyleBackColor = true;
            this.CB5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CB1_MouseClick);
            // 
            // CB22
            // 
            this.CB22.AutoSize = true;
            this.CB22.Location = new System.Drawing.Point(20, 314);
            this.CB22.Name = "CB22";
            this.CB22.Size = new System.Drawing.Size(48, 16);
            this.CB22.TabIndex = 44;
            this.CB22.Tag = "DEmployee";
            this.CB22.Text = "员工";
            this.CB22.UseVisualStyleBackColor = true;
            this.CB22.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CB1_MouseClick);
            // 
            // CB8
            // 
            this.CB8.AutoSize = true;
            this.CB8.Location = new System.Drawing.Point(173, 107);
            this.CB8.Name = "CB8";
            this.CB8.Size = new System.Drawing.Size(84, 16);
            this.CB8.TabIndex = 30;
            this.CB8.Tag = "CPRecord";
            this.CB8.Text = "审方记录表";
            this.CB8.UseVisualStyleBackColor = true;
            this.CB8.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CB1_MouseClick);
            // 
            // CB20
            // 
            this.CB20.AutoSize = true;
            this.CB20.Location = new System.Drawing.Point(20, 292);
            this.CB20.Name = "CB20";
            this.CB20.Size = new System.Drawing.Size(48, 16);
            this.CB20.TabIndex = 42;
            this.CB20.Tag = "DWard";
            this.CB20.Text = "病区";
            this.CB20.UseVisualStyleBackColor = true;
            this.CB20.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CB1_MouseClick);
            // 
            // CB6
            // 
            this.CB6.AutoSize = true;
            this.CB6.Location = new System.Drawing.Point(173, 83);
            this.CB6.Name = "CB6";
            this.CB6.Size = new System.Drawing.Size(84, 16);
            this.CB6.TabIndex = 28;
            this.CB6.Tag = "CPResult";
            this.CB6.Text = "审方结果表";
            this.CB6.UseVisualStyleBackColor = true;
            this.CB6.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CB1_MouseClick);
            // 
            // CB18
            // 
            this.CB18.AutoSize = true;
            this.CB18.Location = new System.Drawing.Point(20, 270);
            this.CB18.Name = "CB18";
            this.CB18.Size = new System.Drawing.Size(48, 16);
            this.CB18.TabIndex = 40;
            this.CB18.Tag = "Patient";
            this.CB18.Text = "病人";
            this.CB18.UseVisualStyleBackColor = true;
            this.CB18.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CB1_MouseClick);
            // 
            // CB17
            // 
            this.CB17.AutoSize = true;
            this.CB17.Location = new System.Drawing.Point(173, 229);
            this.CB17.Name = "CB17";
            this.CB17.Size = new System.Drawing.Size(84, 16);
            this.CB17.TabIndex = 39;
            this.CB17.Tag = "QRcodeLog";
            this.CB17.Text = "员工二维码";
            this.CB17.UseVisualStyleBackColor = true;
            this.CB17.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CB1_MouseClick);
            // 
            // CB19
            // 
            this.CB19.AutoSize = true;
            this.CB19.Location = new System.Drawing.Point(173, 270);
            this.CB19.Name = "CB19";
            this.CB19.Size = new System.Drawing.Size(72, 16);
            this.CB19.TabIndex = 41;
            this.CB19.Tag = "DDrug";
            this.CB19.Text = "药品字典";
            this.CB19.UseVisualStyleBackColor = true;
            this.CB19.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CB1_MouseClick);
            // 
            // CB12
            // 
            this.CB12.AutoSize = true;
            this.CB12.Location = new System.Drawing.Point(20, 185);
            this.CB12.Name = "CB12";
            this.CB12.Size = new System.Drawing.Size(96, 16);
            this.CB12.TabIndex = 34;
            this.CB12.Tag = "OrderMPRuleSub";
            this.CB12.Text = "药品优先明细";
            this.CB12.UseVisualStyleBackColor = true;
            this.CB12.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CB1_MouseClick);
            // 
            // CB2
            // 
            this.CB2.AutoSize = true;
            this.CB2.Location = new System.Drawing.Point(173, 35);
            this.CB2.Name = "CB2";
            this.CB2.Size = new System.Drawing.Size(60, 16);
            this.CB2.TabIndex = 24;
            this.CB2.Tag = "IVRecord";
            this.CB2.Text = "瓶签表";
            this.CB2.UseVisualStyleBackColor = true;
            this.CB2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CB1_MouseClick);
            // 
            // CB16
            // 
            this.CB16.AutoSize = true;
            this.CB16.Location = new System.Drawing.Point(20, 229);
            this.CB16.Name = "CB16";
            this.CB16.Size = new System.Drawing.Size(96, 16);
            this.CB16.TabIndex = 38;
            this.CB16.Tag = "DrugTiter";
            this.CB16.Text = "药品浓度指标";
            this.CB16.UseVisualStyleBackColor = true;
            this.CB16.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CB1_MouseClick);
            // 
            // CB14
            // 
            this.CB14.AutoSize = true;
            this.CB14.Location = new System.Drawing.Point(20, 207);
            this.CB14.Name = "CB14";
            this.CB14.Size = new System.Drawing.Size(96, 16);
            this.CB14.TabIndex = 36;
            this.CB14.Tag = "MedDrugPlusCondition";
            this.CB14.Text = "药品附加条件";
            this.CB14.UseVisualStyleBackColor = true;
            this.CB14.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CB1_MouseClick);
            // 
            // CB3
            // 
            this.CB3.AutoSize = true;
            this.CB3.Location = new System.Drawing.Point(20, 59);
            this.CB3.Name = "CB3";
            this.CB3.Size = new System.Drawing.Size(72, 16);
            this.CB3.TabIndex = 25;
            this.CB3.Tag = "UseDrugListTemp";
            this.CB3.Text = "药单临时";
            this.CB3.UseVisualStyleBackColor = true;
            this.CB3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CB1_MouseClick);
            // 
            // CB13
            // 
            this.CB13.AutoSize = true;
            this.CB13.Location = new System.Drawing.Point(173, 185);
            this.CB13.Name = "CB13";
            this.CB13.Size = new System.Drawing.Size(72, 16);
            this.CB13.TabIndex = 35;
            this.CB13.Tag = "OrderMPRule";
            this.CB13.Text = "药品优先";
            this.CB13.UseVisualStyleBackColor = true;
            this.CB13.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CB1_MouseClick);
            // 
            // CB4
            // 
            this.CB4.AutoSize = true;
            this.CB4.Location = new System.Drawing.Point(173, 59);
            this.CB4.Name = "CB4";
            this.CB4.Size = new System.Drawing.Size(48, 16);
            this.CB4.TabIndex = 26;
            this.CB4.Tag = "UseDrugList";
            this.CB4.Text = "药单";
            this.CB4.UseVisualStyleBackColor = true;
            this.CB4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CB1_MouseClick);
            // 
            // CB23
            // 
            this.CB23.AutoSize = true;
            this.CB23.Location = new System.Drawing.Point(173, 314);
            this.CB23.Name = "CB23";
            this.CB23.Size = new System.Drawing.Size(72, 16);
            this.CB23.TabIndex = 45;
            this.CB23.Tag = "DMetric";
            this.CB23.Text = "剂量单位";
            this.CB23.UseVisualStyleBackColor = true;
            this.CB23.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CB1_MouseClick);
            // 
            // CB9
            // 
            this.CB9.AutoSize = true;
            this.CB9.Location = new System.Drawing.Point(20, 131);
            this.CB9.Name = "CB9";
            this.CB9.Size = new System.Drawing.Size(108, 16);
            this.CB9.TabIndex = 31;
            this.CB9.Tag = "PrescriptionTemp";
            this.CB9.Text = "处方同步临时表";
            this.CB9.UseVisualStyleBackColor = true;
            this.CB9.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CB1_MouseClick);
            // 
            // CB11
            // 
            this.CB11.AutoSize = true;
            this.CB11.Location = new System.Drawing.Point(289, 131);
            this.CB11.Name = "CB11";
            this.CB11.Size = new System.Drawing.Size(48, 16);
            this.CB11.TabIndex = 33;
            this.CB11.Tag = "Prescription";
            this.CB11.Text = "处方";
            this.CB11.UseVisualStyleBackColor = true;
            this.CB11.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CB1_MouseClick);
            // 
            // CB21
            // 
            this.CB21.AutoSize = true;
            this.CB21.Location = new System.Drawing.Point(173, 292);
            this.CB21.Name = "CB21";
            this.CB21.Size = new System.Drawing.Size(72, 16);
            this.CB21.TabIndex = 43;
            this.CB21.Tag = "DFreq";
            this.CB21.Text = "频次字典";
            this.CB21.UseVisualStyleBackColor = true;
            this.CB21.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CB1_MouseClick);
            // 
            // CB10
            // 
            this.CB10.AutoSize = true;
            this.CB10.Location = new System.Drawing.Point(173, 131);
            this.CB10.Name = "CB10";
            this.CB10.Size = new System.Drawing.Size(72, 16);
            this.CB10.TabIndex = 32;
            this.CB10.Tag = "PrescriptionDetail";
            this.CB10.Text = "处方明细";
            this.CB10.UseVisualStyleBackColor = true;
            this.CB10.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CB1_MouseClick);
            // 
            // DeleteTableNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Truncate);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.groupBox1);
            this.Name = "DeleteTableNew";
            this.Size = new System.Drawing.Size(400, 460);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button Truncate;
        private System.Windows.Forms.CheckBox CB1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox CB7;
        private System.Windows.Forms.CheckBox CB15;
        private System.Windows.Forms.CheckBox CB5;
        private System.Windows.Forms.CheckBox CB22;
        private System.Windows.Forms.CheckBox CB8;
        private System.Windows.Forms.CheckBox CB20;
        private System.Windows.Forms.CheckBox CB6;
        private System.Windows.Forms.CheckBox CB18;
        private System.Windows.Forms.CheckBox CB17;
        private System.Windows.Forms.CheckBox CB19;
        private System.Windows.Forms.CheckBox CB12;
        private System.Windows.Forms.CheckBox CB2;
        private System.Windows.Forms.CheckBox CB16;
        private System.Windows.Forms.CheckBox CB14;
        private System.Windows.Forms.CheckBox CB3;
        private System.Windows.Forms.CheckBox CB13;
        private System.Windows.Forms.CheckBox CB4;
        private System.Windows.Forms.CheckBox CB23;
        private System.Windows.Forms.CheckBox CB9;
        private System.Windows.Forms.CheckBox CB11;
        private System.Windows.Forms.CheckBox CB21;
        private System.Windows.Forms.CheckBox CB10;
    }
}
