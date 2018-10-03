namespace PivasFreqRule
{
    partial class MedcineRule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MedcineRule));
            this.Label1 = new System.Windows.Forms.Label();
            this.pnlWard = new System.Windows.Forms.Panel();
            this.pnlMed1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlMed2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAddLow = new System.Windows.Forms.Button();
            this.btnAddUp = new System.Windows.Forms.Button();
            this.Med2Up = new System.Windows.Forms.Button();
            this.Med2Down = new System.Windows.Forms.Button();
            this.Med1Down = new System.Windows.Forms.Button();
            this.Med1Up = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.But_UseAll = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Check_OnlyInse = new System.Windows.Forms.CheckBox();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.ForeColor = System.Drawing.Color.Gray;
            this.Label1.Location = new System.Drawing.Point(6, 36);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(100, 23);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "病区";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlWard
            // 
            this.pnlWard.AutoScroll = true;
            this.pnlWard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlWard.Location = new System.Drawing.Point(5, 62);
            this.pnlWard.Name = "pnlWard";
            this.pnlWard.Size = new System.Drawing.Size(182, 392);
            this.pnlWard.TabIndex = 1;
            // 
            // pnlMed1
            // 
            this.pnlMed1.AutoScroll = true;
            this.pnlMed1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMed1.Location = new System.Drawing.Point(193, 62);
            this.pnlMed1.Name = "pnlMed1";
            this.pnlMed1.Size = new System.Drawing.Size(224, 392);
            this.pnlMed1.TabIndex = 2;
            // 
            // pnlMed2
            // 
            this.pnlMed2.AutoScroll = true;
            this.pnlMed2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMed2.Location = new System.Drawing.Point(420, 62);
            this.pnlMed2.Name = "pnlMed2";
            this.pnlMed2.Size = new System.Drawing.Size(224, 392);
            this.pnlMed2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(187, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "高优先级化学成分/类别:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(416, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "低优先级化学成分/类别:";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(471, 312);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(49, 23);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAddLow
            // 
            this.btnAddLow.Image = global::PivasFreqRule.Properties.Resources.plus_16;
            this.btnAddLow.Location = new System.Drawing.Point(571, 37);
            this.btnAddLow.Name = "btnAddLow";
            this.btnAddLow.Size = new System.Drawing.Size(23, 23);
            this.btnAddLow.TabIndex = 11;
            this.btnAddLow.UseVisualStyleBackColor = true;
            this.btnAddLow.Click += new System.EventHandler(this.btnAddLow_Click);
            // 
            // btnAddUp
            // 
            this.btnAddUp.Image = ((System.Drawing.Image)(resources.GetObject("btnAddUp.Image")));
            this.btnAddUp.Location = new System.Drawing.Point(344, 37);
            this.btnAddUp.Name = "btnAddUp";
            this.btnAddUp.Size = new System.Drawing.Size(23, 23);
            this.btnAddUp.TabIndex = 10;
            this.btnAddUp.UseVisualStyleBackColor = true;
            this.btnAddUp.Click += new System.EventHandler(this.btnAddUp_Click);
            // 
            // Med2Up
            // 
            this.Med2Up.Image = global::PivasFreqRule.Properties.Resources.up_16;
            this.Med2Up.Location = new System.Drawing.Point(594, 37);
            this.Med2Up.Name = "Med2Up";
            this.Med2Up.Size = new System.Drawing.Size(23, 23);
            this.Med2Up.TabIndex = 9;
            this.Med2Up.UseVisualStyleBackColor = true;
            this.Med2Up.Click += new System.EventHandler(this.Med2Up_Click);
            // 
            // Med2Down
            // 
            this.Med2Down.Image = global::PivasFreqRule.Properties.Resources.down_16;
            this.Med2Down.Location = new System.Drawing.Point(617, 37);
            this.Med2Down.Name = "Med2Down";
            this.Med2Down.Size = new System.Drawing.Size(23, 23);
            this.Med2Down.TabIndex = 8;
            this.Med2Down.UseVisualStyleBackColor = true;
            this.Med2Down.Click += new System.EventHandler(this.Med2Down_Click);
            // 
            // Med1Down
            // 
            this.Med1Down.Image = ((System.Drawing.Image)(resources.GetObject("Med1Down.Image")));
            this.Med1Down.Location = new System.Drawing.Point(390, 37);
            this.Med1Down.Name = "Med1Down";
            this.Med1Down.Size = new System.Drawing.Size(23, 23);
            this.Med1Down.TabIndex = 7;
            this.Med1Down.UseVisualStyleBackColor = true;
            this.Med1Down.Click += new System.EventHandler(this.Med1Down_Click);
            // 
            // Med1Up
            // 
            this.Med1Up.Image = ((System.Drawing.Image)(resources.GetObject("Med1Up.Image")));
            this.Med1Up.Location = new System.Drawing.Point(367, 37);
            this.Med1Up.Name = "Med1Up";
            this.Med1Up.Size = new System.Drawing.Size(23, 23);
            this.Med1Up.TabIndex = 6;
            this.Med1Up.UseVisualStyleBackColor = true;
            this.Med1Up.Click += new System.EventHandler(this.Med1Up_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.Check_OnlyInse);
            this.panel5.Controls.Add(this.But_UseAll);
            this.panel5.Location = new System.Drawing.Point(8, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(637, 33);
            this.panel5.TabIndex = 13;
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "当前病区：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(70, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "当前病区";
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
            // MedcineRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.pnlMed2);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAddLow);
            this.Controls.Add(this.btnAddUp);
            this.Controls.Add(this.Med2Up);
            this.Controls.Add(this.Med2Down);
            this.Controls.Add(this.Med1Down);
            this.Controls.Add(this.Med1Up);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnlMed1);
            this.Controls.Add(this.pnlWard);
            this.Controls.Add(this.Label1);
            this.Name = "MedcineRule";
            this.Size = new System.Drawing.Size(655, 459);
            this.Load += new System.EventHandler(this.MedcineRule_Load);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.FlowLayoutPanel pnlMed1;
        private System.Windows.Forms.FlowLayoutPanel pnlMed2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Med1Up;
        private System.Windows.Forms.Button Med1Down;
        private System.Windows.Forms.Button Med2Down;
        private System.Windows.Forms.Button Med2Up;
        private System.Windows.Forms.Button btnAddLow;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAddUp;
        public System.Windows.Forms.Panel pnlWard;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button But_UseAll;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox Check_OnlyInse;
    }
}
