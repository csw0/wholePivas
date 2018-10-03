namespace TransTuiYao
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.timerSendToYTJ = new System.Windows.Forms.Timer(this.components);
            this.btnGetDrugData = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timerClearTable = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 120000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer4
            // 
            this.timer4.Enabled = true;
            this.timer4.Interval = 1200;
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // timerSendToYTJ
            // 
            this.timerSendToYTJ.Enabled = true;
            this.timerSendToYTJ.Interval = 30000;
            this.timerSendToYTJ.Tick += new System.EventHandler(this.timerSendToYTJ_Tick);
            // 
            // btnGetDrugData
            // 
            this.btnGetDrugData.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnGetDrugData.ForeColor = System.Drawing.Color.Black;
            this.btnGetDrugData.Location = new System.Drawing.Point(0, 0);
            this.btnGetDrugData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGetDrugData.Name = "btnGetDrugData";
            this.btnGetDrugData.Size = new System.Drawing.Size(100, 69);
            this.btnGetDrugData.TabIndex = 0;
            this.btnGetDrugData.Text = "获取药品数据";
            this.btnGetDrugData.UseVisualStyleBackColor = false;
            this.btnGetDrugData.Click += new System.EventHandler(this.btnGetDrugData_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Red;
            this.button2.Location = new System.Drawing.Point(131, 20);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(43, 29);
            this.button2.TabIndex = 1;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnGetDrugData);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(16, 28);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(199, 69);
            this.panel1.TabIndex = 2;
            // 
            // timerClearTable
            // 
            this.timerClearTable.Enabled = true;
            this.timerClearTable.Interval = 3600000;
            this.timerClearTable.Tick += new System.EventHandler(this.timerClearTable_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(379, 106);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "瓶签获取接口3.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer4;
        private System.Windows.Forms.Timer timerSendToYTJ;
        private System.Windows.Forms.Button btnGetDrugData;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timerClearTable;
    }
}

