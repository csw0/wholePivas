namespace SDFYDataImport
{
    partial class frmMain
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
            this.txtProc = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.cbSynByTime = new System.Windows.Forms.CheckBox();
            this.lvPatient = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSyn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtProc
            // 
            this.txtProc.Location = new System.Drawing.Point(12, 37);
            this.txtProc.Multiline = true;
            this.txtProc.Name = "txtProc";
            this.txtProc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtProc.Size = new System.Drawing.Size(330, 65);
            this.txtProc.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(231, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "开始";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(291, 8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(51, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 291);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(330, 110);
            this.textBox2.TabIndex = 3;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(267, 407);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 4;
            this.btnTest.Text = "测试";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.button3_Click);
            // 
            // cbSynByTime
            // 
            this.cbSynByTime.AutoSize = true;
            this.cbSynByTime.Checked = true;
            this.cbSynByTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSynByTime.Location = new System.Drawing.Point(12, 15);
            this.cbSynByTime.Name = "cbSynByTime";
            this.cbSynByTime.Size = new System.Drawing.Size(132, 16);
            this.cbSynByTime.TabIndex = 5;
            this.cbSynByTime.Text = "从上次日期往后同步";
            this.cbSynByTime.UseVisualStyleBackColor = true;
            // 
            // lvPatient
            // 
            this.lvPatient.CheckBoxes = true;
            this.lvPatient.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvPatient.FullRowSelect = true;
            this.lvPatient.Location = new System.Drawing.Point(12, 108);
            this.lvPatient.Name = "lvPatient";
            this.lvPatient.Size = new System.Drawing.Size(330, 148);
            this.lvPatient.TabIndex = 6;
            this.lvPatient.UseCompatibleStateImageBehavior = false;
            this.lvPatient.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "病区";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "床位";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "姓名";
            this.columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "住院号";
            // 
            // btnSyn
            // 
            this.btnSyn.Location = new System.Drawing.Point(267, 262);
            this.btnSyn.Name = "btnSyn";
            this.btnSyn.Size = new System.Drawing.Size(75, 23);
            this.btnSyn.TabIndex = 7;
            this.btnSyn.Text = "同步";
            this.btnSyn.UseVisualStyleBackColor = true;
            this.btnSyn.Click += new System.EventHandler(this.btnSyn_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 442);
            this.Controls.Add(this.btnSyn);
            this.Controls.Add(this.lvPatient);
            this.Controls.Add(this.cbSynByTime);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtProc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "苏大附一数据导入";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtProc;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.CheckBox cbSynByTime;
        private System.Windows.Forms.ListView lvPatient;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button btnSyn;
    }
}