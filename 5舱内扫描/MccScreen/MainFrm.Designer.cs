namespace MccScreen
{
    partial class MainFrm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.tbLabel = new System.Windows.Forms.TextBox();
            this.panelLabel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelResult = new System.Windows.Forms.Panel();
            this.lblResult = new System.Windows.Forms.Label();
            this.panelMethod = new System.Windows.Forms.Panel();
            this.listViewConfigMethod = new System.Windows.Forms.ListView();
            this.panelDoctor = new System.Windows.Forms.Panel();
            this.lblTimeMark = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblConfigCount = new System.Windows.Forms.Label();
            this.lblConfigCountMark = new System.Windows.Forms.Label();
            this.lblDoctor = new System.Windows.Forms.Label();
            this.lblDoctorMark = new System.Windows.Forms.Label();
            this.panelPatient = new System.Windows.Forms.Panel();
            this.lblLabelNoMark = new System.Windows.Forms.Label();
            this.lblLabelNo = new System.Windows.Forms.Label();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.lblPatientNameMark = new System.Windows.Forms.Label();
            this.lblWardName = new System.Windows.Forms.Label();
            this.panelDrug = new System.Windows.Forms.Panel();
            this.listViewDrug = new System.Windows.Forms.ListView();
            this.colIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDrugName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDrugSpec = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDrugDose = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDrugCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelBlank = new System.Windows.Forms.Panel();
            this.panelMainClose = new System.Windows.Forms.Panel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panelLogin = new System.Windows.Forms.Panel();
            this.panelLoginTopClose = new System.Windows.Forms.Panel();
            this.panelLoginClose = new System.Windows.Forms.Panel();
            this.lblLoginResult = new System.Windows.Forms.Label();
            this.panelLabel.SuspendLayout();
            this.panelResult.SuspendLayout();
            this.panelMethod.SuspendLayout();
            this.panelDoctor.SuspendLayout();
            this.panelPatient.SuspendLayout();
            this.panelDrug.SuspendLayout();
            this.panelBlank.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelLogin.SuspendLayout();
            this.panelLoginTopClose.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbLabel
            // 
            this.tbLabel.BackColor = System.Drawing.SystemColors.Highlight;
            this.tbLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLabel.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbLabel.ForeColor = System.Drawing.Color.White;
            this.tbLabel.Location = new System.Drawing.Point(241, 0);
            this.tbLabel.Name = "tbLabel";
            this.tbLabel.Size = new System.Drawing.Size(837, 39);
            this.tbLabel.TabIndex = 0;
            this.tbLabel.Text = "20181010123456";
            // 
            // panelLabel
            // 
            this.panelLabel.Controls.Add(this.tbLabel);
            this.panelLabel.Controls.Add(this.label1);
            this.panelLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLabel.Location = new System.Drawing.Point(0, 0);
            this.panelLabel.Name = "panelLabel";
            this.panelLabel.Size = new System.Drawing.Size(1078, 38);
            this.panelLabel.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Highlight;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(241, 38);
            this.label1.TabIndex = 2;
            this.label1.Text = "瓶签扫描:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelResult
            // 
            this.panelResult.BackColor = System.Drawing.Color.White;
            this.panelResult.Controls.Add(this.lblResult);
            this.panelResult.Controls.Add(this.panelLabel);
            this.panelResult.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelResult.Location = new System.Drawing.Point(0, 24);
            this.panelResult.Name = "panelResult";
            this.panelResult.Size = new System.Drawing.Size(1078, 143);
            this.panelResult.TabIndex = 2;
            // 
            // lblResult
            // 
            this.lblResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResult.Font = new System.Drawing.Font("微软雅黑", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblResult.ForeColor = System.Drawing.Color.Green;
            this.lblResult.Location = new System.Drawing.Point(0, 38);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(1078, 105);
            this.lblResult.TabIndex = 0;
            this.lblResult.Text = "计费成功";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelMethod
            // 
            this.panelMethod.Controls.Add(this.listViewConfigMethod);
            this.panelMethod.Controls.Add(this.panelDoctor);
            this.panelMethod.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelMethod.Location = new System.Drawing.Point(0, 255);
            this.panelMethod.Name = "panelMethod";
            this.panelMethod.Size = new System.Drawing.Size(1078, 220);
            this.panelMethod.TabIndex = 3;
            // 
            // listViewConfigMethod
            // 
            this.listViewConfigMethod.BackColor = System.Drawing.Color.White;
            this.listViewConfigMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewConfigMethod.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listViewConfigMethod.Location = new System.Drawing.Point(0, 38);
            this.listViewConfigMethod.Name = "listViewConfigMethod";
            this.listViewConfigMethod.Size = new System.Drawing.Size(1078, 182);
            this.listViewConfigMethod.TabIndex = 1;
            this.listViewConfigMethod.UseCompatibleStateImageBehavior = false;
            this.listViewConfigMethod.View = System.Windows.Forms.View.List;
            // 
            // panelDoctor
            // 
            this.panelDoctor.BackColor = System.Drawing.SystemColors.Highlight;
            this.panelDoctor.Controls.Add(this.lblTimeMark);
            this.panelDoctor.Controls.Add(this.lblTime);
            this.panelDoctor.Controls.Add(this.lblConfigCount);
            this.panelDoctor.Controls.Add(this.lblConfigCountMark);
            this.panelDoctor.Controls.Add(this.lblDoctor);
            this.panelDoctor.Controls.Add(this.lblDoctorMark);
            this.panelDoctor.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDoctor.Location = new System.Drawing.Point(0, 0);
            this.panelDoctor.Name = "panelDoctor";
            this.panelDoctor.Size = new System.Drawing.Size(1078, 38);
            this.panelDoctor.TabIndex = 4;
            // 
            // lblTimeMark
            // 
            this.lblTimeMark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTimeMark.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTimeMark.ForeColor = System.Drawing.Color.White;
            this.lblTimeMark.Location = new System.Drawing.Point(639, 0);
            this.lblTimeMark.Name = "lblTimeMark";
            this.lblTimeMark.Size = new System.Drawing.Size(147, 38);
            this.lblTimeMark.TabIndex = 6;
            this.lblTimeMark.Text = "时间:";
            this.lblTimeMark.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTime
            // 
            this.lblTime.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblTime.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(786, 0);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(292, 38);
            this.lblTime.TabIndex = 5;
            this.lblTime.Text = "16:05:30 10/3";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblConfigCount
            // 
            this.lblConfigCount.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblConfigCount.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblConfigCount.ForeColor = System.Drawing.Color.White;
            this.lblConfigCount.Location = new System.Drawing.Point(517, 0);
            this.lblConfigCount.Name = "lblConfigCount";
            this.lblConfigCount.Size = new System.Drawing.Size(122, 38);
            this.lblConfigCount.TabIndex = 4;
            this.lblConfigCount.Text = "10000";
            this.lblConfigCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblConfigCountMark
            // 
            this.lblConfigCountMark.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblConfigCountMark.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblConfigCountMark.ForeColor = System.Drawing.Color.White;
            this.lblConfigCountMark.Location = new System.Drawing.Point(367, 0);
            this.lblConfigCountMark.Name = "lblConfigCountMark";
            this.lblConfigCountMark.Size = new System.Drawing.Size(150, 38);
            this.lblConfigCountMark.TabIndex = 3;
            this.lblConfigCountMark.Text = "配置量:";
            this.lblConfigCountMark.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDoctor
            // 
            this.lblDoctor.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblDoctor.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDoctor.ForeColor = System.Drawing.Color.White;
            this.lblDoctor.Location = new System.Drawing.Point(115, 0);
            this.lblDoctor.Name = "lblDoctor";
            this.lblDoctor.Size = new System.Drawing.Size(252, 38);
            this.lblDoctor.TabIndex = 2;
            this.lblDoctor.Text = "LaennecSystem";
            this.lblDoctor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDoctorMark
            // 
            this.lblDoctorMark.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblDoctorMark.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDoctorMark.ForeColor = System.Drawing.Color.White;
            this.lblDoctorMark.Location = new System.Drawing.Point(0, 0);
            this.lblDoctorMark.Name = "lblDoctorMark";
            this.lblDoctorMark.Size = new System.Drawing.Size(115, 38);
            this.lblDoctorMark.TabIndex = 1;
            this.lblDoctorMark.Text = "药师:";
            this.lblDoctorMark.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelPatient
            // 
            this.panelPatient.BackColor = System.Drawing.SystemColors.Highlight;
            this.panelPatient.Controls.Add(this.lblLabelNoMark);
            this.panelPatient.Controls.Add(this.lblLabelNo);
            this.panelPatient.Controls.Add(this.lblPatientName);
            this.panelPatient.Controls.Add(this.lblPatientNameMark);
            this.panelPatient.Controls.Add(this.lblWardName);
            this.panelPatient.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPatient.Location = new System.Drawing.Point(0, 0);
            this.panelPatient.Name = "panelPatient";
            this.panelPatient.Size = new System.Drawing.Size(1078, 38);
            this.panelPatient.TabIndex = 5;
            // 
            // lblLabelNoMark
            // 
            this.lblLabelNoMark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLabelNoMark.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLabelNoMark.ForeColor = System.Drawing.Color.White;
            this.lblLabelNoMark.Location = new System.Drawing.Point(640, 0);
            this.lblLabelNoMark.Name = "lblLabelNoMark";
            this.lblLabelNoMark.Size = new System.Drawing.Size(140, 38);
            this.lblLabelNoMark.TabIndex = 5;
            this.lblLabelNoMark.Text = "瓶签:";
            this.lblLabelNoMark.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLabelNo
            // 
            this.lblLabelNo.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblLabelNo.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLabelNo.ForeColor = System.Drawing.Color.White;
            this.lblLabelNo.Location = new System.Drawing.Point(780, 0);
            this.lblLabelNo.Name = "lblLabelNo";
            this.lblLabelNo.Size = new System.Drawing.Size(298, 38);
            this.lblLabelNo.TabIndex = 3;
            this.lblLabelNo.Text = "20181010123456";
            this.lblLabelNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPatientName
            // 
            this.lblPatientName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPatientName.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatientName.ForeColor = System.Drawing.Color.White;
            this.lblPatientName.Location = new System.Drawing.Point(482, 0);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(158, 38);
            this.lblPatientName.TabIndex = 4;
            this.lblPatientName.Text = "王进军骏";
            this.lblPatientName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPatientNameMark
            // 
            this.lblPatientNameMark.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPatientNameMark.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatientNameMark.ForeColor = System.Drawing.Color.White;
            this.lblPatientNameMark.Location = new System.Drawing.Point(367, 0);
            this.lblPatientNameMark.Name = "lblPatientNameMark";
            this.lblPatientNameMark.Size = new System.Drawing.Size(115, 38);
            this.lblPatientNameMark.TabIndex = 1;
            this.lblPatientNameMark.Text = "患者:";
            this.lblPatientNameMark.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWardName
            // 
            this.lblWardName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblWardName.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWardName.ForeColor = System.Drawing.Color.White;
            this.lblWardName.Location = new System.Drawing.Point(0, 0);
            this.lblWardName.Name = "lblWardName";
            this.lblWardName.Size = new System.Drawing.Size(367, 38);
            this.lblWardName.TabIndex = 0;
            this.lblWardName.Text = "神经外科";
            this.lblWardName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelDrug
            // 
            this.panelDrug.Controls.Add(this.listViewDrug);
            this.panelDrug.Controls.Add(this.panelPatient);
            this.panelDrug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDrug.Location = new System.Drawing.Point(0, 167);
            this.panelDrug.Name = "panelDrug";
            this.panelDrug.Size = new System.Drawing.Size(1078, 88);
            this.panelDrug.TabIndex = 6;
            // 
            // listViewDrug
            // 
            this.listViewDrug.BackColor = System.Drawing.Color.White;
            this.listViewDrug.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIndex,
            this.colDrugName,
            this.colDrugSpec,
            this.colDrugDose,
            this.colDrugCount});
            this.listViewDrug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewDrug.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listViewDrug.Location = new System.Drawing.Point(0, 38);
            this.listViewDrug.Name = "listViewDrug";
            this.listViewDrug.Size = new System.Drawing.Size(1078, 50);
            this.listViewDrug.TabIndex = 0;
            this.listViewDrug.UseCompatibleStateImageBehavior = false;
            this.listViewDrug.View = System.Windows.Forms.View.Details;
            // 
            // colIndex
            // 
            this.colIndex.Text = "序号";
            // 
            // colDrugName
            // 
            this.colDrugName.Text = "药品名称";
            // 
            // colDrugSpec
            // 
            this.colDrugSpec.Text = "药品规格";
            // 
            // colDrugDose
            // 
            this.colDrugDose.Text = "药品剂量";
            // 
            // colDrugCount
            // 
            this.colDrugCount.Text = "药品数量";
            // 
            // panelBlank
            // 
            this.panelBlank.BackColor = System.Drawing.SystemColors.Highlight;
            this.panelBlank.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panelBlank.Controls.Add(this.panelMainClose);
            this.panelBlank.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBlank.Location = new System.Drawing.Point(0, 0);
            this.panelBlank.Name = "panelBlank";
            this.panelBlank.Size = new System.Drawing.Size(1078, 24);
            this.panelBlank.TabIndex = 9;
            // 
            // panelMainClose
            // 
            this.panelMainClose.BackgroundImage = global::MccScreen.Properties.Resources.erase;
            this.panelMainClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelMainClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelMainClose.Location = new System.Drawing.Point(1054, 0);
            this.panelMainClose.Name = "panelMainClose";
            this.panelMainClose.Size = new System.Drawing.Size(24, 24);
            this.panelMainClose.TabIndex = 0;
            this.panelMainClose.Click += new System.EventHandler(this.panelMainClose_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.splitter2);
            this.panelMain.Controls.Add(this.splitter1);
            this.panelMain.Controls.Add(this.panelDrug);
            this.panelMain.Controls.Add(this.panelResult);
            this.panelMain.Controls.Add(this.panelBlank);
            this.panelMain.Controls.Add(this.panelMethod);
            this.panelMain.Location = new System.Drawing.Point(0, 12);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1078, 475);
            this.panelMain.TabIndex = 12;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter2.Location = new System.Drawing.Point(0, 167);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(1078, 3);
            this.splitter2.TabIndex = 11;
            this.splitter2.TabStop = false;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 252);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1078, 3);
            this.splitter1.TabIndex = 10;
            this.splitter1.TabStop = false;
            // 
            // panelLogin
            // 
            this.panelLogin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelLogin.BackgroundImage")));
            this.panelLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelLogin.Controls.Add(this.panelLoginTopClose);
            this.panelLogin.Controls.Add(this.lblLoginResult);
            this.panelLogin.Location = new System.Drawing.Point(300, 493);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(377, 117);
            this.panelLogin.TabIndex = 13;
            // 
            // panelLoginTopClose
            // 
            this.panelLoginTopClose.BackColor = System.Drawing.Color.Transparent;
            this.panelLoginTopClose.Controls.Add(this.panelLoginClose);
            this.panelLoginTopClose.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLoginTopClose.Location = new System.Drawing.Point(0, 0);
            this.panelLoginTopClose.Name = "panelLoginTopClose";
            this.panelLoginTopClose.Size = new System.Drawing.Size(377, 32);
            this.panelLoginTopClose.TabIndex = 1;
            // 
            // panelLoginClose
            // 
            this.panelLoginClose.BackgroundImage = global::MccScreen.Properties.Resources.erase;
            this.panelLoginClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelLoginClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelLoginClose.Location = new System.Drawing.Point(345, 0);
            this.panelLoginClose.Name = "panelLoginClose";
            this.panelLoginClose.Size = new System.Drawing.Size(32, 32);
            this.panelLoginClose.TabIndex = 0;
            this.panelLoginClose.Click += new System.EventHandler(this.panelLoginClose_Click);
            // 
            // lblLoginResult
            // 
            this.lblLoginResult.AutoSize = true;
            this.lblLoginResult.BackColor = System.Drawing.Color.Transparent;
            this.lblLoginResult.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLoginResult.ForeColor = System.Drawing.Color.Red;
            this.lblLoginResult.Location = new System.Drawing.Point(21, 36);
            this.lblLoginResult.Name = "lblLoginResult";
            this.lblLoginResult.Size = new System.Drawing.Size(330, 60);
            this.lblLoginResult.TabIndex = 0;
            this.lblLoginResult.Text = "请扫描登录";
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 625);
            this.Controls.Add(this.panelLogin);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainFrm";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFrm_FormClosed);
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.panelLabel.ResumeLayout(false);
            this.panelLabel.PerformLayout();
            this.panelResult.ResumeLayout(false);
            this.panelMethod.ResumeLayout(false);
            this.panelDoctor.ResumeLayout(false);
            this.panelPatient.ResumeLayout(false);
            this.panelDrug.ResumeLayout(false);
            this.panelBlank.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            this.panelLoginTopClose.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbLabel;
        private System.Windows.Forms.Panel panelLabel;
        private System.Windows.Forms.Panel panelResult;
        private System.Windows.Forms.Panel panelMethod;
        private System.Windows.Forms.Panel panelDoctor;
        private System.Windows.Forms.Panel panelPatient;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Panel panelDrug;
        private System.Windows.Forms.Label lblWardName;
        private System.Windows.Forms.Label lblPatientNameMark;
        private System.Windows.Forms.Label lblLabelNo;
        private System.Windows.Forms.ListView listViewDrug;
        private System.Windows.Forms.ColumnHeader colIndex;
        private System.Windows.Forms.ColumnHeader colDrugName;
        private System.Windows.Forms.ColumnHeader colDrugSpec;
        private System.Windows.Forms.ColumnHeader colDrugDose;
        private System.Windows.Forms.ColumnHeader colDrugCount;
        private System.Windows.Forms.Label lblDoctorMark;
        private System.Windows.Forms.Label lblConfigCount;
        private System.Windows.Forms.Label lblConfigCountMark;
        private System.Windows.Forms.Label lblDoctor;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.ListView listViewConfigMethod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Panel panelBlank;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.Label lblLabelNoMark;
        private System.Windows.Forms.Label lblTimeMark;
        private System.Windows.Forms.Label lblLoginResult;
        private System.Windows.Forms.Panel panelLoginTopClose;
        private System.Windows.Forms.Panel panelLoginClose;
        private System.Windows.Forms.Panel panelMainClose;
    }
}

