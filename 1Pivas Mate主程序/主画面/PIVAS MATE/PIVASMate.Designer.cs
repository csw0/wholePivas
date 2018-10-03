namespace PIVAS_MATE
{
    partial class PIVASMate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PIVASMate));
            this.TimeManage = new System.Windows.Forms.Timer(this.components);
            this.panelContent = new System.Windows.Forms.Panel();
            this.Panel_Head = new System.Windows.Forms.Panel();
            this.TopHead = new System.Windows.Forms.Panel();
            this.panel_Help = new System.Windows.Forms.Panel();
            this.Panel_Min = new System.Windows.Forms.Panel();
            this.Panel_Max_None = new System.Windows.Forms.Panel();
            this.Label_Count = new System.Windows.Forms.Label();
            this.Panel_Close = new System.Windows.Forms.Panel();
            this.Label_Tool = new System.Windows.Forms.Label();
            this.Label_Check = new System.Windows.Forms.Label();
            this.Label_print = new System.Windows.Forms.Label();
            this.Label_Batch = new System.Windows.Forms.Label();
            this.Label_TPNReview = new System.Windows.Forms.Label();
            this.Label_Checking = new System.Windows.Forms.Label();
            this.Label_Syn = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.NowDate = new System.Windows.Forms.Label();
            this.labelLoginer = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.notify = new System.Windows.Forms.NotifyIcon(this.components);
            this.ContextMenuOfNotify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmenuItemQQ = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBatch = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuDrug = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuWard = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuDose = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuEmployee = new System.Windows.Forms.ToolStripMenuItem();
            this.SearchPre = new System.Windows.Forms.ToolStripMenuItem();
            this.Serarchlabel = new System.Windows.Forms.ToolStripMenuItem();
            this.瓶签操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.提前打包ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.配置取消ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timeremind = new System.Windows.Forms.Timer(this.components);
            this.QQTime1 = new System.Windows.Forms.Timer(this.components);
            this.QQTimer2 = new System.Windows.Forms.Timer(this.components);
            this.Panel_Head.SuspendLayout();
            this.TopHead.SuspendLayout();
            this.panel4.SuspendLayout();
            this.ContextMenuOfNotify.SuspendLayout();
            this.SuspendLayout();
            // 
            // TimeManage
            // 
            this.TimeManage.Enabled = true;
            this.TimeManage.Interval = 1000;
            this.TimeManage.Tick += new System.EventHandler(this.TimeManage_Tick);
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.ForeColor = System.Drawing.Color.Black;
            this.panelContent.Location = new System.Drawing.Point(0, 77);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1160, 498);
            this.panelContent.TabIndex = 8;
            // 
            // Panel_Head
            // 
            this.Panel_Head.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Head.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel_Head.BackgroundImage")));
            this.Panel_Head.Controls.Add(this.TopHead);
            this.Panel_Head.Controls.Add(this.Label_Tool);
            this.Panel_Head.Controls.Add(this.Label_Check);
            this.Panel_Head.Controls.Add(this.Label_print);
            this.Panel_Head.Controls.Add(this.Label_Batch);
            this.Panel_Head.Controls.Add(this.Label_TPNReview);
            this.Panel_Head.Controls.Add(this.Label_Checking);
            this.Panel_Head.Controls.Add(this.Label_Syn);
            this.Panel_Head.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Head.Location = new System.Drawing.Point(0, 0);
            this.Panel_Head.Name = "Panel_Head";
            this.Panel_Head.Size = new System.Drawing.Size(1160, 77);
            this.Panel_Head.TabIndex = 7;
            this.Panel_Head.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopHead_MouseDown);
            // 
            // TopHead
            // 
            this.TopHead.BackColor = System.Drawing.Color.Transparent;
            this.TopHead.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TopHead.BackgroundImage")));
            this.TopHead.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.TopHead.Controls.Add(this.panel_Help);
            this.TopHead.Controls.Add(this.Panel_Min);
            this.TopHead.Controls.Add(this.Panel_Max_None);
            this.TopHead.Controls.Add(this.Label_Count);
            this.TopHead.Controls.Add(this.Panel_Close);
            this.TopHead.Dock = System.Windows.Forms.DockStyle.Right;
            this.TopHead.Location = new System.Drawing.Point(862, 0);
            this.TopHead.Name = "TopHead";
            this.TopHead.Size = new System.Drawing.Size(298, 77);
            this.TopHead.TabIndex = 3;
            this.TopHead.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopHead_MouseDown);
            // 
            // panel_Help
            // 
            this.panel_Help.BackgroundImage = global::PIVAS_MATE.Properties.Resources._37;
            this.panel_Help.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_Help.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel_Help.Location = new System.Drawing.Point(273, 52);
            this.panel_Help.Name = "panel_Help";
            this.panel_Help.Size = new System.Drawing.Size(22, 20);
            this.panel_Help.TabIndex = 9;
            this.panel_Help.Click += new System.EventHandler(this.panel_Help_Click);
            this.panel_Help.MouseLeave += new System.EventHandler(this.panel_Help_MouseLeave);
            this.panel_Help.MouseHover += new System.EventHandler(this.panel_Help_MouseHover);
            // 
            // Panel_Min
            // 
            this.Panel_Min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Min.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Min.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel_Min.BackgroundImage")));
            this.Panel_Min.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Panel_Min.Location = new System.Drawing.Point(233, 0);
            this.Panel_Min.Name = "Panel_Min";
            this.Panel_Min.Size = new System.Drawing.Size(23, 19);
            this.Panel_Min.TabIndex = 6;
            this.Panel_Min.Click += new System.EventHandler(this.Panel_Min_Click);
            this.Panel_Min.MouseLeave += new System.EventHandler(this.Panel_Min_MouseLeave);
            this.Panel_Min.MouseHover += new System.EventHandler(this.Panel_Min_MouseHover);
            // 
            // Panel_Max_None
            // 
            this.Panel_Max_None.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.Panel_Max_None.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Max_None.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Max_None.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel_Max_None.BackgroundImage")));
            this.Panel_Max_None.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Panel_Max_None.Location = new System.Drawing.Point(256, 0);
            this.Panel_Max_None.Name = "Panel_Max_None";
            this.Panel_Max_None.Size = new System.Drawing.Size(21, 19);
            this.Panel_Max_None.TabIndex = 6;
            this.Panel_Max_None.Click += new System.EventHandler(this.Panel_Max_None_Click);
            this.Panel_Max_None.MouseLeave += new System.EventHandler(this.Panel_Max_None_MouseLeave);
            this.Panel_Max_None.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_Max_None_MouseMove);
            // 
            // Label_Count
            // 
            this.Label_Count.BackColor = System.Drawing.Color.Transparent;
            this.Label_Count.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label_Count.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label_Count.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.Label_Count.ForeColor = System.Drawing.Color.White;
            this.Label_Count.Image = ((System.Drawing.Image)(resources.GetObject("Label_Count.Image")));
            this.Label_Count.Location = new System.Drawing.Point(0, 0);
            this.Label_Count.Name = "Label_Count";
            this.Label_Count.Size = new System.Drawing.Size(79, 77);
            this.Label_Count.TabIndex = 8;
            this.Label_Count.Tag = "";
            this.Label_Count.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_Count.Visible = false;
            this.Label_Count.Click += new System.EventHandler(this.Label_Count_Click);
            this.Label_Count.MouseLeave += new System.EventHandler(this.Label_Count_MouseLeave);
            this.Label_Count.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_Count_MouseMove);
            // 
            // Panel_Close
            // 
            this.Panel_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Close.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel_Close.BackgroundImage")));
            this.Panel_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Panel_Close.Location = new System.Drawing.Point(277, 0);
            this.Panel_Close.Name = "Panel_Close";
            this.Panel_Close.Size = new System.Drawing.Size(21, 19);
            this.Panel_Close.TabIndex = 6;
            this.Panel_Close.Click += new System.EventHandler(this.Panel_Close_Click);
            this.Panel_Close.MouseLeave += new System.EventHandler(this.Panel_Close_MouseLeave);
            this.Panel_Close.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_Close_MouseMove);
            // 
            // Label_Tool
            // 
            this.Label_Tool.BackColor = System.Drawing.Color.Transparent;
            this.Label_Tool.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label_Tool.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label_Tool.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.Label_Tool.ForeColor = System.Drawing.Color.White;
            this.Label_Tool.Image = ((System.Drawing.Image)(resources.GetObject("Label_Tool.Image")));
            this.Label_Tool.Location = new System.Drawing.Point(475, 0);
            this.Label_Tool.Name = "Label_Tool";
            this.Label_Tool.Size = new System.Drawing.Size(79, 77);
            this.Label_Tool.TabIndex = 8;
            this.Label_Tool.Tag = "";
            this.Label_Tool.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_Tool.Click += new System.EventHandler(this.Label_Tool_Click);
            this.Label_Tool.MouseLeave += new System.EventHandler(this.Label_Tool_MouseLeave);
            this.Label_Tool.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_Tool_MouseMove);
            // 
            // Label_Check
            // 
            this.Label_Check.BackColor = System.Drawing.Color.Transparent;
            this.Label_Check.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label_Check.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label_Check.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.Label_Check.ForeColor = System.Drawing.Color.White;
            this.Label_Check.Image = ((System.Drawing.Image)(resources.GetObject("Label_Check.Image")));
            this.Label_Check.Location = new System.Drawing.Point(396, 0);
            this.Label_Check.Name = "Label_Check";
            this.Label_Check.Size = new System.Drawing.Size(79, 77);
            this.Label_Check.TabIndex = 8;
            this.Label_Check.Tag = "";
            this.Label_Check.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_Check.Click += new System.EventHandler(this.Label_Check_Click);
            this.Label_Check.MouseLeave += new System.EventHandler(this.Label_Check_MouseLeave);
            this.Label_Check.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_Check_MouseMove);
            // 
            // Label_print
            // 
            this.Label_print.BackColor = System.Drawing.Color.Transparent;
            this.Label_print.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label_print.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label_print.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.Label_print.ForeColor = System.Drawing.Color.White;
            this.Label_print.Image = ((System.Drawing.Image)(resources.GetObject("Label_print.Image")));
            this.Label_print.Location = new System.Drawing.Point(316, 0);
            this.Label_print.Name = "Label_print";
            this.Label_print.Size = new System.Drawing.Size(80, 77);
            this.Label_print.TabIndex = 8;
            this.Label_print.Tag = "";
            this.Label_print.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_print.Click += new System.EventHandler(this.Label_print_Click);
            this.Label_print.MouseLeave += new System.EventHandler(this.Label_print_MouseLeave);
            this.Label_print.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_print_MouseMove);
            // 
            // Label_Batch
            // 
            this.Label_Batch.BackColor = System.Drawing.Color.Transparent;
            this.Label_Batch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label_Batch.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label_Batch.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.Label_Batch.ForeColor = System.Drawing.Color.White;
            this.Label_Batch.Image = ((System.Drawing.Image)(resources.GetObject("Label_Batch.Image")));
            this.Label_Batch.Location = new System.Drawing.Point(237, 0);
            this.Label_Batch.Name = "Label_Batch";
            this.Label_Batch.Size = new System.Drawing.Size(79, 77);
            this.Label_Batch.TabIndex = 8;
            this.Label_Batch.Tag = "";
            this.Label_Batch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_Batch.Click += new System.EventHandler(this.Label_Batch_Click);
            this.Label_Batch.MouseLeave += new System.EventHandler(this.Label_Batch_MouseLeave);
            this.Label_Batch.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_Batch_MouseMove);
            // 
            // Label_TPNReview
            // 
            this.Label_TPNReview.BackColor = System.Drawing.Color.Transparent;
            this.Label_TPNReview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label_TPNReview.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label_TPNReview.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.Label_TPNReview.ForeColor = System.Drawing.Color.White;
            this.Label_TPNReview.Image = global::PIVAS_MATE.Properties.Resources.TPN审方;
            this.Label_TPNReview.Location = new System.Drawing.Point(158, 0);
            this.Label_TPNReview.Name = "Label_TPNReview";
            this.Label_TPNReview.Size = new System.Drawing.Size(79, 77);
            this.Label_TPNReview.TabIndex = 12;
            this.Label_TPNReview.Tag = "";
            this.Label_TPNReview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_TPNReview.Click += new System.EventHandler(this.Label_TPNReview_Click);
            this.Label_TPNReview.MouseLeave += new System.EventHandler(this.Label_TPNReview_MouseLeave);
            this.Label_TPNReview.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_TPNReview_MouseMove);
            // 
            // Label_Checking
            // 
            this.Label_Checking.BackColor = System.Drawing.Color.Transparent;
            this.Label_Checking.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label_Checking.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label_Checking.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.Label_Checking.ForeColor = System.Drawing.Color.White;
            this.Label_Checking.Image = ((System.Drawing.Image)(resources.GetObject("Label_Checking.Image")));
            this.Label_Checking.Location = new System.Drawing.Point(79, 0);
            this.Label_Checking.Name = "Label_Checking";
            this.Label_Checking.Size = new System.Drawing.Size(79, 77);
            this.Label_Checking.TabIndex = 7;
            this.Label_Checking.Tag = "";
            this.Label_Checking.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_Checking.Click += new System.EventHandler(this.Label_Checking_Click);
            this.Label_Checking.MouseLeave += new System.EventHandler(this.Label_Checking_MouseLeave);
            this.Label_Checking.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_Checking_MouseMove);
            // 
            // Label_Syn
            // 
            this.Label_Syn.BackColor = System.Drawing.Color.Transparent;
            this.Label_Syn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label_Syn.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label_Syn.Font = new System.Drawing.Font("宋体", 1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.Label_Syn.ForeColor = System.Drawing.Color.Transparent;
            this.Label_Syn.Image = ((System.Drawing.Image)(resources.GetObject("Label_Syn.Image")));
            this.Label_Syn.Location = new System.Drawing.Point(0, 0);
            this.Label_Syn.Name = "Label_Syn";
            this.Label_Syn.Size = new System.Drawing.Size(79, 77);
            this.Label_Syn.TabIndex = 8;
            this.Label_Syn.Tag = "";
            this.Label_Syn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_Syn.Click += new System.EventHandler(this.Label_Syn_Click);
            this.Label_Syn.MouseLeave += new System.EventHandler(this.Label_Syn_MouseLeave);
            this.Label_Syn.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_Syn_MouseMove);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel4.BackgroundImage")));
            this.panel4.Controls.Add(this.NowDate);
            this.panel4.Controls.Add(this.labelLoginer);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 575);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1160, 22);
            this.panel4.TabIndex = 9;
            // 
            // NowDate
            // 
            this.NowDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.NowDate.AutoSize = true;
            this.NowDate.BackColor = System.Drawing.Color.Transparent;
            this.NowDate.ForeColor = System.Drawing.Color.White;
            this.NowDate.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.NowDate.Location = new System.Drawing.Point(478, 4);
            this.NowDate.Name = "NowDate";
            this.NowDate.Size = new System.Drawing.Size(53, 12);
            this.NowDate.TabIndex = 6;
            this.NowDate.Text = "当前时间";
            this.NowDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelLoginer
            // 
            this.labelLoginer.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelLoginer.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelLoginer.ForeColor = System.Drawing.Color.Silver;
            this.labelLoginer.Location = new System.Drawing.Point(968, 0);
            this.labelLoginer.Name = "labelLoginer";
            this.labelLoginer.Size = new System.Drawing.Size(192, 22);
            this.labelLoginer.TabIndex = 7;
            this.labelLoginer.Text = "登陆者";
            this.labelLoginer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 22);
            this.label2.TabIndex = 8;
            this.label2.Text = "2014-12-15-00001";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // notify
            // 
            this.notify.ContextMenuStrip = this.ContextMenuOfNotify;
            this.notify.Icon = ((System.Drawing.Icon)(resources.GetObject("notify.Icon")));
            this.notify.Text = "PIVAS MATE";
            this.notify.Visible = true;
            this.notify.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notify_MouseDoubleClick);
            // 
            // ContextMenuOfNotify
            // 
            this.ContextMenuOfNotify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmenuItemQQ,
            this.MenuTools,
            this.SearchPre,
            this.Serarchlabel,
            this.瓶签操作ToolStripMenuItem});
            this.ContextMenuOfNotify.Name = "contextMenuStrip1";
            this.ContextMenuOfNotify.Size = new System.Drawing.Size(149, 114);
            // 
            // tsmenuItemQQ
            // 
            this.tsmenuItemQQ.Name = "tsmenuItemQQ";
            this.tsmenuItemQQ.Size = new System.Drawing.Size(148, 22);
            this.tsmenuItemQQ.Text = "局域网QQ";
            this.tsmenuItemQQ.Visible = false;
            this.tsmenuItemQQ.Click += new System.EventHandler(this.tsmenuItemQQ_Click);
            // 
            // MenuTools
            // 
            this.MenuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBatch,
            this.MenuDrug,
            this.MenuWard,
            this.MenuDose,
            this.MenuEmployee});
            this.MenuTools.Name = "MenuTools";
            this.MenuTools.Size = new System.Drawing.Size(148, 22);
            this.MenuTools.Text = "基础数据维护";
            // 
            // MenuBatch
            // 
            this.MenuBatch.Name = "MenuBatch";
            this.MenuBatch.Size = new System.Drawing.Size(124, 22);
            this.MenuBatch.Text = "批次规则";
            this.MenuBatch.Click += new System.EventHandler(this.MenuBatch_Click);
            // 
            // MenuDrug
            // 
            this.MenuDrug.Name = "MenuDrug";
            this.MenuDrug.Size = new System.Drawing.Size(124, 22);
            this.MenuDrug.Text = "药品目录";
            this.MenuDrug.Click += new System.EventHandler(this.MenuDrug_Click);
            // 
            // MenuWard
            // 
            this.MenuWard.Name = "MenuWard";
            this.MenuWard.Size = new System.Drawing.Size(124, 22);
            this.MenuWard.Text = "医院病区";
            this.MenuWard.Click += new System.EventHandler(this.MenuWard_Click);
            // 
            // MenuDose
            // 
            this.MenuDose.Name = "MenuDose";
            this.MenuDose.Size = new System.Drawing.Size(124, 22);
            this.MenuDose.Text = "计量单位";
            this.MenuDose.Click += new System.EventHandler(this.MenuDose_Click);
            // 
            // MenuEmployee
            // 
            this.MenuEmployee.Name = "MenuEmployee";
            this.MenuEmployee.Size = new System.Drawing.Size(124, 22);
            this.MenuEmployee.Text = "医院员工";
            this.MenuEmployee.Click += new System.EventHandler(this.MenuEmployee_Click);
            // 
            // SearchPre
            // 
            this.SearchPre.Name = "SearchPre";
            this.SearchPre.Size = new System.Drawing.Size(148, 22);
            this.SearchPre.Text = "医嘱查询";
            this.SearchPre.Click += new System.EventHandler(this.SearchPre_Click);
            // 
            // Serarchlabel
            // 
            this.Serarchlabel.Name = "Serarchlabel";
            this.Serarchlabel.Size = new System.Drawing.Size(148, 22);
            this.Serarchlabel.Text = "瓶签查询";
            this.Serarchlabel.Click += new System.EventHandler(this.SerarchLabel_Click);
            // 
            // 瓶签操作ToolStripMenuItem
            // 
            this.瓶签操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.提前打包ToolStripMenuItem,
            this.配置取消ToolStripMenuItem});
            this.瓶签操作ToolStripMenuItem.Name = "瓶签操作ToolStripMenuItem";
            this.瓶签操作ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.瓶签操作ToolStripMenuItem.Text = "瓶签操作";
            // 
            // 提前打包ToolStripMenuItem
            // 
            this.提前打包ToolStripMenuItem.Name = "提前打包ToolStripMenuItem";
            this.提前打包ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.提前打包ToolStripMenuItem.Text = "提前打包";
            this.提前打包ToolStripMenuItem.Click += new System.EventHandler(this.提前打包ToolStripMenuItem_Click);
            // 
            // 配置取消ToolStripMenuItem
            // 
            this.配置取消ToolStripMenuItem.Name = "配置取消ToolStripMenuItem";
            this.配置取消ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.配置取消ToolStripMenuItem.Text = "配置取消";
            this.配置取消ToolStripMenuItem.Click += new System.EventHandler(this.配置取消ToolStripMenuItem_Click);
            // 
            // timeremind
            // 
            this.timeremind.Enabled = true;
            this.timeremind.Interval = 1000;
            this.timeremind.Tick += new System.EventHandler(this.timeremind_Tick);
            // 
            // QQTime1
            // 
            this.QQTime1.Interval = 500;
            this.QQTime1.Tick += new System.EventHandler(this.QQTimer1_Tick);
            // 
            // QQTimer2
            // 
            this.QQTimer2.Enabled = true;
            this.QQTimer2.Interval = 600000;
            this.QQTimer2.Tick += new System.EventHandler(this.QQTimer2_Tick);
            // 
            // PIVASMate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(138)))), ((int)(((byte)(219)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1160, 597);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.Panel_Head);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PIVASMate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PIVAS MATE";
            this.Load += new System.EventHandler(this.PIVASMate_Load);
            this.Panel_Head.ResumeLayout(false);
            this.TopHead.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ContextMenuOfNotify.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        //private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        //private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.Timer TimeManage;
        private System.Windows.Forms.Panel Panel_Head;
        private System.Windows.Forms.Panel TopHead;
        private System.Windows.Forms.Panel Panel_Min;
        private System.Windows.Forms.Panel Panel_Max_None;
        private System.Windows.Forms.Panel Panel_Close;
        private System.Windows.Forms.Label Label_Tool;
        private System.Windows.Forms.Label Label_Check;
        private System.Windows.Forms.Label Label_Batch;
        private System.Windows.Forms.Label Label_Checking;
        private System.Windows.Forms.Label Label_Syn;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label NowDate;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label Label_Count;
        private System.Windows.Forms.Label Label_print;
        private System.Windows.Forms.Label labelLoginer;
        private System.Windows.Forms.NotifyIcon notify;
        private System.Windows.Forms.Timer timeremind;
        private System.Windows.Forms.ContextMenuStrip ContextMenuOfNotify;
        private System.Windows.Forms.ToolStripMenuItem SearchPre;
        private System.Windows.Forms.ToolStripMenuItem Serarchlabel;
        private System.Windows.Forms.ToolStripMenuItem MenuTools;
        private System.Windows.Forms.ToolStripMenuItem MenuBatch;
        private System.Windows.Forms.ToolStripMenuItem MenuDrug;
        private System.Windows.Forms.ToolStripMenuItem MenuWard;
        private System.Windows.Forms.ToolStripMenuItem MenuDose;
        private System.Windows.Forms.ToolStripMenuItem MenuEmployee;
        private System.Windows.Forms.ToolStripMenuItem 瓶签操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 提前打包ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 配置取消ToolStripMenuItem;
        private System.Windows.Forms.Panel panel_Help;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer QQTime1;
        private System.Windows.Forms.Timer QQTimer2;
        private System.Windows.Forms.ToolStripMenuItem tsmenuItemQQ;
        private System.Windows.Forms.Label Label_TPNReview;
        //private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        //private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        //private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
    }
}

