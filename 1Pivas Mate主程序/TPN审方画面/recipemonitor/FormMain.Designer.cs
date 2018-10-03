namespace recipemonitor
{
    partial class FormMain
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
            this.label1 = new System.Windows.Forms.Label();
            this.lvOrders = new System.Windows.Forms.ListView();
            this.time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ward = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bedno = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.age = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.recipeID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.route = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.freq = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.level = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnMonitor = new System.Windows.Forms.Button();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbAll = new System.Windows.Forms.CheckBox();
            this.lvDetail = new System.Windows.Forms.ListView();
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cbSelAll = new System.Windows.Forms.CheckBox();
            this.lblNum = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "TPN审方医嘱:";
            // 
            // lvOrders
            // 
            this.lvOrders.CheckBoxes = true;
            this.lvOrders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.time,
            this.ward,
            this.bedno,
            this.pname,
            this.age,
            this.sex,
            this.recipeID,
            this.route,
            this.freq,
            this.level});
            this.lvOrders.FullRowSelect = true;
            this.lvOrders.Location = new System.Drawing.Point(10, 24);
            this.lvOrders.Name = "lvOrders";
            this.lvOrders.Size = new System.Drawing.Size(441, 190);
            this.lvOrders.TabIndex = 1;
            this.lvOrders.UseCompatibleStateImageBehavior = false;
            this.lvOrders.View = System.Windows.Forms.View.Details;
            this.lvOrders.SelectedIndexChanged += new System.EventHandler(this.lvOrders_SelectedIndexChanged);
            // 
            // time
            // 
            this.time.Text = "医嘱时间";
            this.time.Width = 124;
            // 
            // ward
            // 
            this.ward.Text = "病区";
            this.ward.Width = 100;
            // 
            // bedno
            // 
            this.bedno.Text = "床位";
            // 
            // pname
            // 
            this.pname.Text = "患者姓名";
            this.pname.Width = 70;
            // 
            // age
            // 
            this.age.Text = "年龄";
            this.age.Width = 50;
            // 
            // sex
            // 
            this.sex.Text = "性别";
            this.sex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.sex.Width = 40;
            // 
            // recipeID
            // 
            this.recipeID.Text = "医嘱号";
            // 
            // route
            // 
            this.route.Text = "途径";
            // 
            // freq
            // 
            this.freq.Text = "频次";
            // 
            // level
            // 
            this.level.Text = "审方结果";
            // 
            // btnMonitor
            // 
            this.btnMonitor.Location = new System.Drawing.Point(374, 220);
            this.btnMonitor.Name = "btnMonitor";
            this.btnMonitor.Size = new System.Drawing.Size(75, 23);
            this.btnMonitor.TabIndex = 2;
            this.btnMonitor.Text = "审方";
            this.btnMonitor.UseVisualStyleBackColor = true;
            this.btnMonitor.Click += new System.EventHandler(this.btnMonitor_Click);
            // 
            // txtInfo
            // 
            this.txtInfo.Location = new System.Drawing.Point(10, 374);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ReadOnly = true;
            this.txtInfo.Size = new System.Drawing.Size(442, 54);
            this.txtInfo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 359);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "审方信息:";
            // 
            // cbAll
            // 
            this.cbAll.AutoSize = true;
            this.cbAll.Location = new System.Drawing.Point(338, 4);
            this.cbAll.Name = "cbAll";
            this.cbAll.Size = new System.Drawing.Size(114, 16);
            this.cbAll.TabIndex = 5;
            this.cbAll.Text = "所有在用TPN医嘱";
            this.cbAll.UseVisualStyleBackColor = true;
            this.cbAll.CheckedChanged += new System.EventHandler(this.cbAll_CheckedChanged);
            // 
            // lvDetail
            // 
            this.lvDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13});
            this.lvDetail.FullRowSelect = true;
            this.lvDetail.Location = new System.Drawing.Point(10, 248);
            this.lvDetail.Name = "lvDetail";
            this.lvDetail.Size = new System.Drawing.Size(442, 97);
            this.lvDetail.TabIndex = 6;
            this.lvDetail.UseCompatibleStateImageBehavior = false;
            this.lvDetail.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "药品名称";
            this.columnHeader10.Width = 160;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "规格";
            this.columnHeader11.Width = 100;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "剂量";
            this.columnHeader12.Width = 100;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "数量";
            // 
            // cbSelAll
            // 
            this.cbSelAll.AutoSize = true;
            this.cbSelAll.Location = new System.Drawing.Point(11, 220);
            this.cbSelAll.Name = "cbSelAll";
            this.cbSelAll.Size = new System.Drawing.Size(48, 16);
            this.cbSelAll.TabIndex = 7;
            this.cbSelAll.Text = "全选";
            this.cbSelAll.UseVisualStyleBackColor = true;
            this.cbSelAll.CheckedChanged += new System.EventHandler(this.cbSelAll_CheckedChanged);
            // 
            // lblNum
            // 
            this.lblNum.AutoSize = true;
            this.lblNum.Location = new System.Drawing.Point(91, 8);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(41, 12);
            this.lblNum.TabIndex = 8;
            this.lblNum.Text = "label3";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 440);
            this.Controls.Add(this.lblNum);
            this.Controls.Add(this.cbSelAll);
            this.Controls.Add(this.lvDetail);
            this.Controls.Add(this.cbAll);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.btnMonitor);
            this.Controls.Add(this.lvOrders);
            this.Controls.Add(this.label1);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TPN审方";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lvOrders;
        private System.Windows.Forms.Button btnMonitor;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbAll;
        private System.Windows.Forms.ColumnHeader ward;
        private System.Windows.Forms.ColumnHeader bedno;
        private System.Windows.Forms.ColumnHeader pname;
        private System.Windows.Forms.ColumnHeader age;
        private System.Windows.Forms.ColumnHeader sex;
        private System.Windows.Forms.ColumnHeader recipeID;
        private System.Windows.Forms.ColumnHeader freq;
        private System.Windows.Forms.ColumnHeader route;
        private System.Windows.Forms.ColumnHeader level;
        private System.Windows.Forms.ListView lvDetail;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader time;
        private System.Windows.Forms.CheckBox cbSelAll;
        private System.Windows.Forms.Label lblNum;
    }
}