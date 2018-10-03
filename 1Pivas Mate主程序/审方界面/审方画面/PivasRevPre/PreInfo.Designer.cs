namespace PivasRevPre
{
    partial class PreInfo
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreInfo));
            this.lblIncepDT = new System.Windows.Forms.Label();
            this.btnReCheck = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblStart = new System.Windows.Forms.Label();
            this.lblEnd = new System.Windows.Forms.Label();
            this.lblFregCode = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDoctor = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblGroupNo = new System.Windows.Forms.Label();
            this.cbbSelect = new System.Windows.Forms.CheckBox();
            this.lblp = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlPass = new System.Windows.Forms.Panel();
            this.pnlBack = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.btnPass = new System.Windows.Forms.Button();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblIncepDT
            // 
            this.lblIncepDT.AutoEllipsis = true;
            this.lblIncepDT.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblIncepDT.Location = new System.Drawing.Point(454, 5);
            this.lblIncepDT.Name = "lblIncepDT";
            this.lblIncepDT.Size = new System.Drawing.Size(142, 17);
            this.lblIncepDT.TabIndex = 1;
            this.lblIncepDT.Text = "接收";
            this.lblIncepDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIncepDT.Click += new System.EventHandler(this.pnlInfo_Click);
            // 
            // btnReCheck
            // 
            this.btnReCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReCheck.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReCheck.BackgroundImage")));
            this.btnReCheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnReCheck.Location = new System.Drawing.Point(68, 1);
            this.btnReCheck.Name = "btnReCheck";
            this.btnReCheck.Size = new System.Drawing.Size(25, 25);
            this.btnReCheck.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnReCheck, "重审");
            this.btnReCheck.UseVisualStyleBackColor = true;
            this.btnReCheck.Click += new System.EventHandler(this.btnReCheck_Click);
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBack.BackgroundImage")));
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBack.Location = new System.Drawing.Point(36, 1);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(25, 25);
            this.btnBack.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnBack, "退单");
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblStart
            // 
            this.lblStart.AutoEllipsis = true;
            this.lblStart.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStart.Location = new System.Drawing.Point(638, 5);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(144, 17);
            this.lblStart.TabIndex = 5;
            this.lblStart.Text = "开始";
            this.lblStart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStart.Click += new System.EventHandler(this.pnlInfo_Click);
            // 
            // lblEnd
            // 
            this.lblEnd.AutoEllipsis = true;
            this.lblEnd.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEnd.Location = new System.Drawing.Point(824, 5);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(139, 17);
            this.lblEnd.TabIndex = 6;
            this.lblEnd.Text = "结束";
            this.lblEnd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblEnd.Click += new System.EventHandler(this.pnlInfo_Click);
            // 
            // lblFregCode
            // 
            this.lblFregCode.Location = new System.Drawing.Point(-2, 31);
            this.lblFregCode.Name = "lblFregCode";
            this.lblFregCode.Size = new System.Drawing.Size(39, 11);
            this.lblFregCode.TabIndex = 7;
            this.lblFregCode.Text = "频次";
            this.lblFregCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFregCode.Visible = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(412, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "接收:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Click += new System.EventHandler(this.pnlInfo_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(596, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "开始:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Click += new System.EventHandler(this.pnlInfo_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(782, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "结束:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Click += new System.EventHandler(this.pnlInfo_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(123, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 17);
            this.label4.TabIndex = 16;
            this.label4.Text = "医生:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Click += new System.EventHandler(this.pnlInfo_Click);
            // 
            // lblDoctor
            // 
            this.lblDoctor.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDoctor.Location = new System.Drawing.Point(165, 5);
            this.lblDoctor.Name = "lblDoctor";
            this.lblDoctor.Size = new System.Drawing.Size(62, 17);
            this.lblDoctor.TabIndex = 17;
            this.lblDoctor.Text = "医生";
            this.lblDoctor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDoctor.Click += new System.EventHandler(this.pnlInfo_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(227, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 17);
            this.label5.TabIndex = 18;
            this.label5.Text = "组号:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Click += new System.EventHandler(this.pnlInfo_Click);
            // 
            // lblGroupNo
            // 
            this.lblGroupNo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGroupNo.Location = new System.Drawing.Point(269, 5);
            this.lblGroupNo.Name = "lblGroupNo";
            this.lblGroupNo.Size = new System.Drawing.Size(137, 17);
            this.lblGroupNo.TabIndex = 19;
            this.lblGroupNo.Text = "组号";
            this.lblGroupNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblGroupNo.Click += new System.EventHandler(this.pnlInfo_Click);
            // 
            // cbbSelect
            // 
            this.cbbSelect.AutoSize = true;
            this.cbbSelect.Location = new System.Drawing.Point(12, 7);
            this.cbbSelect.Name = "cbbSelect";
            this.cbbSelect.Size = new System.Drawing.Size(15, 14);
            this.cbbSelect.TabIndex = 20;
            this.cbbSelect.UseVisualStyleBackColor = true;
            // 
            // lblp
            // 
            this.lblp.AutoEllipsis = true;
            this.lblp.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblp.Location = new System.Drawing.Point(74, 5);
            this.lblp.Name = "lblp";
            this.lblp.Size = new System.Drawing.Size(49, 17);
            this.lblp.TabIndex = 25;
            this.lblp.Text = "审方人";
            this.lblp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel3.Controls.Add(this.panel9);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.lblp);
            this.panel3.Controls.Add(this.lblIncepDT);
            this.panel3.Controls.Add(this.lblEnd);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.cbbSelect);
            this.panel3.Controls.Add(this.pnlPass);
            this.panel3.Controls.Add(this.lblStart);
            this.panel3.Controls.Add(this.pnlBack);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.lblGroupNo);
            this.panel3.Controls.Add(this.lblDoctor);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Location = new System.Drawing.Point(0, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(968, 24);
            this.panel3.TabIndex = 26;
            // 
            // panel9
            // 
            this.panel9.BackgroundImage = global::PivasRevPre.Properties.Resources.zuoS1;
            this.panel9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel9.Location = new System.Drawing.Point(-2, -3);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(11, 12);
            this.panel9.TabIndex = 26;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::PivasRevPre.Properties.Resources._2;
            this.panel2.Location = new System.Drawing.Point(32, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(13, 16);
            this.panel2.TabIndex = 22;
            this.panel2.Click += new System.EventHandler(this.panel2_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::PivasRevPre.Properties.Resources._1;
            this.panel1.Location = new System.Drawing.Point(32, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(13, 14);
            this.panel1.TabIndex = 21;
            this.panel1.Visible = false;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // pnlPass
            // 
            this.pnlPass.BackgroundImage = global::PivasRevPre.Properties.Resources._3;
            this.pnlPass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlPass.Location = new System.Drawing.Point(53, 5);
            this.pnlPass.Name = "pnlPass";
            this.pnlPass.Size = new System.Drawing.Size(17, 17);
            this.pnlPass.TabIndex = 14;
            this.pnlPass.Visible = false;
            // 
            // pnlBack
            // 
            this.pnlBack.BackgroundImage = global::PivasRevPre.Properties.Resources._4;
            this.pnlBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlBack.Location = new System.Drawing.Point(53, 4);
            this.pnlBack.Name = "pnlBack";
            this.pnlBack.Size = new System.Drawing.Size(18, 18);
            this.pnlBack.TabIndex = 15;
            this.pnlBack.Visible = false;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.panel10);
            this.panel4.Controls.Add(this.btnReCheck);
            this.panel4.Controls.Add(this.btnPass);
            this.panel4.Controls.Add(this.btnBack);
            this.panel4.Location = new System.Drawing.Point(967, -1);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(120, 26);
            this.panel4.TabIndex = 27;
            // 
            // panel10
            // 
            this.panel10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel10.BackgroundImage = global::PivasRevPre.Properties.Resources.youS;
            this.panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel10.Location = new System.Drawing.Point(108, -2);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(12, 12);
            this.panel10.TabIndex = 5;
            // 
            // btnPass
            // 
            this.btnPass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPass.BackgroundImage = global::PivasRevPre.Properties.Resources.pass1;
            this.btnPass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPass.Location = new System.Drawing.Point(4, 1);
            this.btnPass.Name = "btnPass";
            this.btnPass.Size = new System.Drawing.Size(25, 25);
            this.btnPass.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnPass, "审方通过");
            this.btnPass.UseVisualStyleBackColor = true;
            this.btnPass.Click += new System.EventHandler(this.btnPass_Click);
            // 
            // pnlInfo
            // 
            this.pnlInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlInfo.Location = new System.Drawing.Point(1, 26);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(1084, 156);
            this.pnlInfo.TabIndex = 28;
            this.pnlInfo.SizeChanged += new System.EventHandler(this.pnlInfo_SizeChanged);
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.Gray;
            this.panel5.Location = new System.Drawing.Point(-2, 183);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1092, 10);
            this.panel5.TabIndex = 29;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel6.BackColor = System.Drawing.Color.Gray;
            this.panel6.Location = new System.Drawing.Point(-12, 8);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(13, 177);
            this.panel6.TabIndex = 30;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.BackColor = System.Drawing.Color.Gray;
            this.panel7.Location = new System.Drawing.Point(1085, 8);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(10, 188);
            this.panel7.TabIndex = 31;
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel8.BackColor = System.Drawing.Color.Gray;
            this.panel8.Location = new System.Drawing.Point(7, -9);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1072, 10);
            this.panel8.TabIndex = 32;
            // 
            // PreInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.pnlInfo);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.lblFregCode);
            this.Name = "PreInfo";
            this.Size = new System.Drawing.Size(1086, 184);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblIncepDT;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.Label lblFregCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDoctor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblGroupNo;
        public System.Windows.Forms.CheckBox cbbSelect;
        public System.Windows.Forms.Panel pnlPass;
        public System.Windows.Forms.Panel pnlBack;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblp;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel10;
        public System.Windows.Forms.Button btnReCheck;
        public System.Windows.Forms.Button btnBack;
        public System.Windows.Forms.Button btnPass;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
