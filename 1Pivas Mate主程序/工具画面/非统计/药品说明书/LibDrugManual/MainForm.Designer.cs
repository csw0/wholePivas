namespace LibDrugManual
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.Title = new System.Windows.Forms.Label();
            this.SubTitle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DrugNames = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Characters = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Pharmacology = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Dynamics = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.AdapterSymtoms = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.Usages = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.SideEffects = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.Taboos = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.Notion = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.WomenUsages = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.ChildrenUsages = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.OldUsages = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.Interactions = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.Paranormal = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.Specifications = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.Storage = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.Package = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.ValidityPeriod = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.ConfirmNumber = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.Factory = new System.Windows.Forms.TextBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(733, 630);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Resize += new System.EventHandler(this.tabControl1_Resize);
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.flowLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(725, 604);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "标准说明书";
            this.tabPage1.Click += new System.EventHandler(this.SubTitle_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel1.Controls.Add(this.Title);
            this.flowLayoutPanel1.Controls.Add(this.SubTitle);
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.DrugNames);
            this.flowLayoutPanel1.Controls.Add(this.label5);
            this.flowLayoutPanel1.Controls.Add(this.Characters);
            this.flowLayoutPanel1.Controls.Add(this.label7);
            this.flowLayoutPanel1.Controls.Add(this.Pharmacology);
            this.flowLayoutPanel1.Controls.Add(this.label9);
            this.flowLayoutPanel1.Controls.Add(this.Dynamics);
            this.flowLayoutPanel1.Controls.Add(this.label11);
            this.flowLayoutPanel1.Controls.Add(this.AdapterSymtoms);
            this.flowLayoutPanel1.Controls.Add(this.label13);
            this.flowLayoutPanel1.Controls.Add(this.Usages);
            this.flowLayoutPanel1.Controls.Add(this.label15);
            this.flowLayoutPanel1.Controls.Add(this.SideEffects);
            this.flowLayoutPanel1.Controls.Add(this.label17);
            this.flowLayoutPanel1.Controls.Add(this.Taboos);
            this.flowLayoutPanel1.Controls.Add(this.label19);
            this.flowLayoutPanel1.Controls.Add(this.Notion);
            this.flowLayoutPanel1.Controls.Add(this.label21);
            this.flowLayoutPanel1.Controls.Add(this.WomenUsages);
            this.flowLayoutPanel1.Controls.Add(this.label23);
            this.flowLayoutPanel1.Controls.Add(this.ChildrenUsages);
            this.flowLayoutPanel1.Controls.Add(this.label25);
            this.flowLayoutPanel1.Controls.Add(this.OldUsages);
            this.flowLayoutPanel1.Controls.Add(this.label27);
            this.flowLayoutPanel1.Controls.Add(this.Interactions);
            this.flowLayoutPanel1.Controls.Add(this.label29);
            this.flowLayoutPanel1.Controls.Add(this.Paranormal);
            this.flowLayoutPanel1.Controls.Add(this.label31);
            this.flowLayoutPanel1.Controls.Add(this.Specifications);
            this.flowLayoutPanel1.Controls.Add(this.label33);
            this.flowLayoutPanel1.Controls.Add(this.Storage);
            this.flowLayoutPanel1.Controls.Add(this.label35);
            this.flowLayoutPanel1.Controls.Add(this.Package);
            this.flowLayoutPanel1.Controls.Add(this.label37);
            this.flowLayoutPanel1.Controls.Add(this.ValidityPeriod);
            this.flowLayoutPanel1.Controls.Add(this.label39);
            this.flowLayoutPanel1.Controls.Add(this.ConfirmNumber);
            this.flowLayoutPanel1.Controls.Add(this.label41);
            this.flowLayoutPanel1.Controls.Add(this.Factory);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(719, 598);
            this.flowLayoutPanel1.TabIndex = 2;
            this.flowLayoutPanel1.Click += new System.EventHandler(this.SubTitle_Click);
            // 
            // Title
            // 
            this.Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Title.BackColor = System.Drawing.Color.White;
            this.Title.Font = new System.Drawing.Font("宋体", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Title.Location = new System.Drawing.Point(3, 0);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(687, 42);
            this.Title.TabIndex = 2;
            this.Title.Text = "药品说明书";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Title.Click += new System.EventHandler(this.SubTitle_Click);
            // 
            // SubTitle
            // 
            this.SubTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SubTitle.BackColor = System.Drawing.Color.White;
            this.SubTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SubTitle.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SubTitle.Location = new System.Drawing.Point(3, 45);
            this.SubTitle.Multiline = true;
            this.SubTitle.Name = "SubTitle";
            this.SubTitle.ReadOnly = true;
            this.SubTitle.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.SubTitle.Size = new System.Drawing.Size(686, 23);
            this.SubTitle.TabIndex = 3;
            this.SubTitle.Text = "核准日期和修改日期";
            this.SubTitle.Click += new System.EventHandler(this.SubTitle_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoEllipsis = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(3, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(686, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "【药品名称】";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Click += new System.EventHandler(this.SubTitle_Click);
            // 
            // DrugNames
            // 
            this.DrugNames.BackColor = System.Drawing.Color.White;
            this.DrugNames.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DrugNames.Dock = System.Windows.Forms.DockStyle.Top;
            this.DrugNames.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DrugNames.Location = new System.Drawing.Point(3, 97);
            this.DrugNames.Multiline = true;
            this.DrugNames.Name = "DrugNames";
            this.DrugNames.ReadOnly = true;
            this.DrugNames.Size = new System.Drawing.Size(686, 19);
            this.DrugNames.TabIndex = 5;
            this.DrugNames.Text = "label4";
            this.DrugNames.Click += new System.EventHandler(this.SubTitle_Click);
            this.DrugNames.SizeChanged += new System.EventHandler(this.DrugNames_SizeChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoEllipsis = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(3, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(686, 23);
            this.label5.TabIndex = 6;
            this.label5.Text = "【性状】";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Click += new System.EventHandler(this.SubTitle_Click);
            // 
            // Characters
            // 
            this.Characters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Characters.BackColor = System.Drawing.Color.White;
            this.Characters.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Characters.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Characters.Location = new System.Drawing.Point(3, 145);
            this.Characters.Multiline = true;
            this.Characters.Name = "Characters";
            this.Characters.ReadOnly = true;
            this.Characters.Size = new System.Drawing.Size(686, 19);
            this.Characters.TabIndex = 7;
            this.Characters.Text = "label6";
            this.Characters.Click += new System.EventHandler(this.SubTitle_Click);
            this.Characters.SizeChanged += new System.EventHandler(this.DrugNames_SizeChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoEllipsis = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(3, 167);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(686, 23);
            this.label7.TabIndex = 8;
            this.label7.Text = "【药品毒理】";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label7.Click += new System.EventHandler(this.SubTitle_Click);
            // 
            // Pharmacology
            // 
            this.Pharmacology.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Pharmacology.BackColor = System.Drawing.Color.White;
            this.Pharmacology.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Pharmacology.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Pharmacology.Location = new System.Drawing.Point(3, 193);
            this.Pharmacology.Multiline = true;
            this.Pharmacology.Name = "Pharmacology";
            this.Pharmacology.ReadOnly = true;
            this.Pharmacology.Size = new System.Drawing.Size(686, 19);
            this.Pharmacology.TabIndex = 9;
            this.Pharmacology.Text = "label8";
            this.Pharmacology.Click += new System.EventHandler(this.SubTitle_Click);
            this.Pharmacology.SizeChanged += new System.EventHandler(this.DrugNames_SizeChanged);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoEllipsis = true;
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.Blue;
            this.label9.Location = new System.Drawing.Point(3, 215);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(686, 23);
            this.label9.TabIndex = 10;
            this.label9.Text = "【药代动力学】";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label9.Click += new System.EventHandler(this.SubTitle_Click);
            // 
            // Dynamics
            // 
            this.Dynamics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Dynamics.BackColor = System.Drawing.Color.White;
            this.Dynamics.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Dynamics.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Dynamics.Location = new System.Drawing.Point(3, 241);
            this.Dynamics.Multiline = true;
            this.Dynamics.Name = "Dynamics";
            this.Dynamics.ReadOnly = true;
            this.Dynamics.Size = new System.Drawing.Size(686, 19);
            this.Dynamics.TabIndex = 11;
            this.Dynamics.Text = "label10";
            this.Dynamics.Click += new System.EventHandler(this.SubTitle_Click);
            this.Dynamics.SizeChanged += new System.EventHandler(this.DrugNames_SizeChanged);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoEllipsis = true;
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ForeColor = System.Drawing.Color.Blue;
            this.label11.Location = new System.Drawing.Point(3, 263);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(686, 23);
            this.label11.TabIndex = 12;
            this.label11.Text = "【适应症/功能主治】";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label11.Click += new System.EventHandler(this.SubTitle_Click);
            // 
            // AdapterSymtoms
            // 
            this.AdapterSymtoms.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AdapterSymtoms.BackColor = System.Drawing.Color.White;
            this.AdapterSymtoms.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AdapterSymtoms.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AdapterSymtoms.Location = new System.Drawing.Point(3, 289);
            this.AdapterSymtoms.Multiline = true;
            this.AdapterSymtoms.Name = "AdapterSymtoms";
            this.AdapterSymtoms.ReadOnly = true;
            this.AdapterSymtoms.Size = new System.Drawing.Size(686, 19);
            this.AdapterSymtoms.TabIndex = 13;
            this.AdapterSymtoms.Text = "label12";
            this.AdapterSymtoms.Click += new System.EventHandler(this.SubTitle_Click);
            this.AdapterSymtoms.SizeChanged += new System.EventHandler(this.DrugNames_SizeChanged);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoEllipsis = true;
            this.label13.BackColor = System.Drawing.Color.White;
            this.label13.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.Color.Blue;
            this.label13.Location = new System.Drawing.Point(3, 311);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(686, 23);
            this.label13.TabIndex = 14;
            this.label13.Text = "【用法用量】";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label13.Click += new System.EventHandler(this.SubTitle_Click);
            // 
            // Usages
            // 
            this.Usages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Usages.BackColor = System.Drawing.Color.White;
            this.Usages.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Usages.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Usages.Location = new System.Drawing.Point(3, 337);
            this.Usages.Multiline = true;
            this.Usages.Name = "Usages";
            this.Usages.ReadOnly = true;
            this.Usages.Size = new System.Drawing.Size(686, 19);
            this.Usages.TabIndex = 15;
            this.Usages.Text = "label14";
            this.Usages.Click += new System.EventHandler(this.SubTitle_Click);
            this.Usages.SizeChanged += new System.EventHandler(this.DrugNames_SizeChanged);
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoEllipsis = true;
            this.label15.BackColor = System.Drawing.Color.White;
            this.label15.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.ForeColor = System.Drawing.Color.Blue;
            this.label15.Location = new System.Drawing.Point(3, 359);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(686, 23);
            this.label15.TabIndex = 16;
            this.label15.Text = "【不良反应】";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label15.Click += new System.EventHandler(this.SubTitle_Click);
            // 
            // SideEffects
            // 
            this.SideEffects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SideEffects.BackColor = System.Drawing.Color.White;
            this.SideEffects.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SideEffects.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SideEffects.Location = new System.Drawing.Point(3, 385);
            this.SideEffects.Multiline = true;
            this.SideEffects.Name = "SideEffects";
            this.SideEffects.ReadOnly = true;
            this.SideEffects.Size = new System.Drawing.Size(686, 19);
            this.SideEffects.TabIndex = 17;
            this.SideEffects.Text = "label16";
            this.SideEffects.Click += new System.EventHandler(this.SubTitle_Click);
            this.SideEffects.SizeChanged += new System.EventHandler(this.DrugNames_SizeChanged);
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoEllipsis = true;
            this.label17.BackColor = System.Drawing.Color.White;
            this.label17.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.ForeColor = System.Drawing.Color.Blue;
            this.label17.Location = new System.Drawing.Point(3, 407);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(686, 23);
            this.label17.TabIndex = 18;
            this.label17.Text = "【禁忌】";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label17.Click += new System.EventHandler(this.SubTitle_Click);
            // 
            // Taboos
            // 
            this.Taboos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Taboos.BackColor = System.Drawing.Color.White;
            this.Taboos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Taboos.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Taboos.Location = new System.Drawing.Point(3, 433);
            this.Taboos.Multiline = true;
            this.Taboos.Name = "Taboos";
            this.Taboos.ReadOnly = true;
            this.Taboos.Size = new System.Drawing.Size(686, 19);
            this.Taboos.TabIndex = 19;
            this.Taboos.Text = "label18";
            this.Taboos.Click += new System.EventHandler(this.SubTitle_Click);
            this.Taboos.SizeChanged += new System.EventHandler(this.DrugNames_SizeChanged);
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoEllipsis = true;
            this.label19.BackColor = System.Drawing.Color.White;
            this.label19.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.ForeColor = System.Drawing.Color.Blue;
            this.label19.Location = new System.Drawing.Point(3, 455);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(686, 23);
            this.label19.TabIndex = 20;
            this.label19.Text = "【注意事项】";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label19.Click += new System.EventHandler(this.SubTitle_Click);
            // 
            // Notion
            // 
            this.Notion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Notion.BackColor = System.Drawing.Color.White;
            this.Notion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Notion.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Notion.Location = new System.Drawing.Point(3, 481);
            this.Notion.Multiline = true;
            this.Notion.Name = "Notion";
            this.Notion.ReadOnly = true;
            this.Notion.Size = new System.Drawing.Size(686, 19);
            this.Notion.TabIndex = 21;
            this.Notion.Text = "label20";
            this.Notion.Click += new System.EventHandler(this.SubTitle_Click);
            this.Notion.SizeChanged += new System.EventHandler(this.DrugNames_SizeChanged);
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoEllipsis = true;
            this.label21.BackColor = System.Drawing.Color.White;
            this.label21.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.ForeColor = System.Drawing.Color.Blue;
            this.label21.Location = new System.Drawing.Point(3, 503);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(686, 23);
            this.label21.TabIndex = 22;
            this.label21.Text = "【孕妇及哺乳期妇女用药】";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label21.Click += new System.EventHandler(this.SubTitle_Click);
            // 
            // WomenUsages
            // 
            this.WomenUsages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.WomenUsages.BackColor = System.Drawing.Color.White;
            this.WomenUsages.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.WomenUsages.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.WomenUsages.Location = new System.Drawing.Point(3, 529);
            this.WomenUsages.Multiline = true;
            this.WomenUsages.Name = "WomenUsages";
            this.WomenUsages.ReadOnly = true;
            this.WomenUsages.Size = new System.Drawing.Size(686, 19);
            this.WomenUsages.TabIndex = 23;
            this.WomenUsages.Text = "label22";
            this.WomenUsages.Click += new System.EventHandler(this.SubTitle_Click);
            this.WomenUsages.SizeChanged += new System.EventHandler(this.DrugNames_SizeChanged);
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.AutoEllipsis = true;
            this.label23.BackColor = System.Drawing.Color.White;
            this.label23.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label23.ForeColor = System.Drawing.Color.Blue;
            this.label23.Location = new System.Drawing.Point(3, 551);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(686, 23);
            this.label23.TabIndex = 24;
            this.label23.Text = "【儿童用药】";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label23.Click += new System.EventHandler(this.SubTitle_Click);
            // 
            // ChildrenUsages
            // 
            this.ChildrenUsages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ChildrenUsages.BackColor = System.Drawing.Color.White;
            this.ChildrenUsages.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ChildrenUsages.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChildrenUsages.Location = new System.Drawing.Point(3, 577);
            this.ChildrenUsages.Multiline = true;
            this.ChildrenUsages.Name = "ChildrenUsages";
            this.ChildrenUsages.ReadOnly = true;
            this.ChildrenUsages.Size = new System.Drawing.Size(686, 19);
            this.ChildrenUsages.TabIndex = 25;
            this.ChildrenUsages.Text = "label24";
            this.ChildrenUsages.Click += new System.EventHandler(this.SubTitle_Click);
            this.ChildrenUsages.SizeChanged += new System.EventHandler(this.DrugNames_SizeChanged);
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoEllipsis = true;
            this.label25.BackColor = System.Drawing.Color.White;
            this.label25.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label25.ForeColor = System.Drawing.Color.Blue;
            this.label25.Location = new System.Drawing.Point(3, 599);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(686, 23);
            this.label25.TabIndex = 26;
            this.label25.Text = "【老年用药】";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label25.Click += new System.EventHandler(this.SubTitle_Click);
            // 
            // OldUsages
            // 
            this.OldUsages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.OldUsages.BackColor = System.Drawing.Color.White;
            this.OldUsages.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OldUsages.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OldUsages.Location = new System.Drawing.Point(3, 625);
            this.OldUsages.Multiline = true;
            this.OldUsages.Name = "OldUsages";
            this.OldUsages.ReadOnly = true;
            this.OldUsages.Size = new System.Drawing.Size(686, 19);
            this.OldUsages.TabIndex = 27;
            this.OldUsages.Text = "label26";
            this.OldUsages.Click += new System.EventHandler(this.SubTitle_Click);
            this.OldUsages.SizeChanged += new System.EventHandler(this.DrugNames_SizeChanged);
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.AutoEllipsis = true;
            this.label27.BackColor = System.Drawing.Color.White;
            this.label27.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label27.ForeColor = System.Drawing.Color.Blue;
            this.label27.Location = new System.Drawing.Point(3, 647);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(686, 23);
            this.label27.TabIndex = 28;
            this.label27.Text = "【药物相互作用】";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label27.Click += new System.EventHandler(this.SubTitle_Click);
            // 
            // Interactions
            // 
            this.Interactions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Interactions.BackColor = System.Drawing.Color.White;
            this.Interactions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Interactions.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Interactions.Location = new System.Drawing.Point(3, 673);
            this.Interactions.Multiline = true;
            this.Interactions.Name = "Interactions";
            this.Interactions.ReadOnly = true;
            this.Interactions.Size = new System.Drawing.Size(686, 19);
            this.Interactions.TabIndex = 29;
            this.Interactions.Text = "label28";
            this.Interactions.Click += new System.EventHandler(this.SubTitle_Click);
            this.Interactions.SizeChanged += new System.EventHandler(this.DrugNames_SizeChanged);
            // 
            // label29
            // 
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label29.AutoEllipsis = true;
            this.label29.BackColor = System.Drawing.Color.White;
            this.label29.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label29.ForeColor = System.Drawing.Color.Blue;
            this.label29.Location = new System.Drawing.Point(3, 695);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(686, 23);
            this.label29.TabIndex = 30;
            this.label29.Text = "【药物过量】";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label29.Click += new System.EventHandler(this.SubTitle_Click);
            // 
            // Paranormal
            // 
            this.Paranormal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Paranormal.BackColor = System.Drawing.Color.White;
            this.Paranormal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Paranormal.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Paranormal.Location = new System.Drawing.Point(3, 721);
            this.Paranormal.Multiline = true;
            this.Paranormal.Name = "Paranormal";
            this.Paranormal.ReadOnly = true;
            this.Paranormal.Size = new System.Drawing.Size(686, 19);
            this.Paranormal.TabIndex = 31;
            this.Paranormal.Text = "label30";
            this.Paranormal.Click += new System.EventHandler(this.SubTitle_Click);
            this.Paranormal.SizeChanged += new System.EventHandler(this.DrugNames_SizeChanged);
            // 
            // label31
            // 
            this.label31.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label31.AutoEllipsis = true;
            this.label31.BackColor = System.Drawing.Color.White;
            this.label31.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label31.ForeColor = System.Drawing.Color.Blue;
            this.label31.Location = new System.Drawing.Point(3, 743);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(686, 23);
            this.label31.TabIndex = 32;
            this.label31.Text = "【规格】";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label31.Click += new System.EventHandler(this.SubTitle_Click);
            // 
            // Specifications
            // 
            this.Specifications.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Specifications.BackColor = System.Drawing.Color.White;
            this.Specifications.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Specifications.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Specifications.Location = new System.Drawing.Point(3, 769);
            this.Specifications.Multiline = true;
            this.Specifications.Name = "Specifications";
            this.Specifications.ReadOnly = true;
            this.Specifications.Size = new System.Drawing.Size(686, 19);
            this.Specifications.TabIndex = 33;
            this.Specifications.Text = "label32";
            this.Specifications.Click += new System.EventHandler(this.SubTitle_Click);
            this.Specifications.SizeChanged += new System.EventHandler(this.DrugNames_SizeChanged);
            // 
            // label33
            // 
            this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label33.AutoEllipsis = true;
            this.label33.BackColor = System.Drawing.Color.White;
            this.label33.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label33.ForeColor = System.Drawing.Color.Blue;
            this.label33.Location = new System.Drawing.Point(3, 791);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(686, 23);
            this.label33.TabIndex = 34;
            this.label33.Text = "【贮藏】";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label33.Click += new System.EventHandler(this.SubTitle_Click);
            // 
            // Storage
            // 
            this.Storage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Storage.BackColor = System.Drawing.Color.White;
            this.Storage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Storage.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Storage.Location = new System.Drawing.Point(3, 817);
            this.Storage.Multiline = true;
            this.Storage.Name = "Storage";
            this.Storage.ReadOnly = true;
            this.Storage.Size = new System.Drawing.Size(686, 19);
            this.Storage.TabIndex = 35;
            this.Storage.Text = "label34";
            this.Storage.Click += new System.EventHandler(this.SubTitle_Click);
            this.Storage.SizeChanged += new System.EventHandler(this.DrugNames_SizeChanged);
            // 
            // label35
            // 
            this.label35.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label35.AutoEllipsis = true;
            this.label35.BackColor = System.Drawing.Color.White;
            this.label35.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label35.ForeColor = System.Drawing.Color.Blue;
            this.label35.Location = new System.Drawing.Point(3, 839);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(686, 23);
            this.label35.TabIndex = 36;
            this.label35.Text = "【包装】";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label35.Click += new System.EventHandler(this.SubTitle_Click);
            // 
            // Package
            // 
            this.Package.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Package.BackColor = System.Drawing.Color.White;
            this.Package.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Package.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Package.Location = new System.Drawing.Point(3, 865);
            this.Package.Multiline = true;
            this.Package.Name = "Package";
            this.Package.ReadOnly = true;
            this.Package.Size = new System.Drawing.Size(686, 19);
            this.Package.TabIndex = 37;
            this.Package.Text = "label36";
            this.Package.Click += new System.EventHandler(this.SubTitle_Click);
            this.Package.SizeChanged += new System.EventHandler(this.DrugNames_SizeChanged);
            // 
            // label37
            // 
            this.label37.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label37.AutoEllipsis = true;
            this.label37.BackColor = System.Drawing.Color.White;
            this.label37.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label37.ForeColor = System.Drawing.Color.Blue;
            this.label37.Location = new System.Drawing.Point(3, 887);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(686, 23);
            this.label37.TabIndex = 38;
            this.label37.Text = "【有效期】";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label37.Click += new System.EventHandler(this.SubTitle_Click);
            // 
            // ValidityPeriod
            // 
            this.ValidityPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ValidityPeriod.BackColor = System.Drawing.Color.White;
            this.ValidityPeriod.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ValidityPeriod.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ValidityPeriod.Location = new System.Drawing.Point(3, 913);
            this.ValidityPeriod.Multiline = true;
            this.ValidityPeriod.Name = "ValidityPeriod";
            this.ValidityPeriod.ReadOnly = true;
            this.ValidityPeriod.Size = new System.Drawing.Size(686, 19);
            this.ValidityPeriod.TabIndex = 39;
            this.ValidityPeriod.Text = "label38";
            this.ValidityPeriod.Click += new System.EventHandler(this.SubTitle_Click);
            this.ValidityPeriod.SizeChanged += new System.EventHandler(this.DrugNames_SizeChanged);
            // 
            // label39
            // 
            this.label39.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label39.AutoEllipsis = true;
            this.label39.BackColor = System.Drawing.Color.White;
            this.label39.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label39.ForeColor = System.Drawing.Color.Blue;
            this.label39.Location = new System.Drawing.Point(3, 935);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(686, 23);
            this.label39.TabIndex = 40;
            this.label39.Text = "【批准文号】";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label39.Click += new System.EventHandler(this.SubTitle_Click);
            // 
            // ConfirmNumber
            // 
            this.ConfirmNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfirmNumber.BackColor = System.Drawing.Color.White;
            this.ConfirmNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ConfirmNumber.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ConfirmNumber.Location = new System.Drawing.Point(3, 961);
            this.ConfirmNumber.Multiline = true;
            this.ConfirmNumber.Name = "ConfirmNumber";
            this.ConfirmNumber.ReadOnly = true;
            this.ConfirmNumber.Size = new System.Drawing.Size(686, 19);
            this.ConfirmNumber.TabIndex = 41;
            this.ConfirmNumber.Text = "label40";
            this.ConfirmNumber.Click += new System.EventHandler(this.SubTitle_Click);
            this.ConfirmNumber.SizeChanged += new System.EventHandler(this.DrugNames_SizeChanged);
            // 
            // label41
            // 
            this.label41.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label41.AutoEllipsis = true;
            this.label41.BackColor = System.Drawing.Color.White;
            this.label41.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label41.ForeColor = System.Drawing.Color.Blue;
            this.label41.Location = new System.Drawing.Point(3, 983);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(686, 23);
            this.label41.TabIndex = 42;
            this.label41.Text = "【生产企业】";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label41.Click += new System.EventHandler(this.SubTitle_Click);
            // 
            // Factory
            // 
            this.Factory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Factory.BackColor = System.Drawing.Color.White;
            this.Factory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Factory.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Factory.Location = new System.Drawing.Point(3, 1009);
            this.Factory.Multiline = true;
            this.Factory.Name = "Factory";
            this.Factory.ReadOnly = true;
            this.Factory.Size = new System.Drawing.Size(686, 19);
            this.Factory.TabIndex = 43;
            this.Factory.Text = "label42";
            this.Factory.Click += new System.EventHandler(this.SubTitle_Click);
            this.Factory.SizeChanged += new System.EventHandler(this.DrugNames_SizeChanged);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 630);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "药品说明书";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.TextBox SubTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox DrugNames;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Characters;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox Pharmacology;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox Dynamics;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox AdapterSymtoms;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox Usages;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox SideEffects;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox Taboos;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox Notion;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox WomenUsages;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox ChildrenUsages;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox OldUsages;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox Interactions;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox Paranormal;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox Specifications;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox Storage;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TextBox Package;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox ValidityPeriod;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox ConfirmNumber;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox Factory;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

