using System.Windows.Forms;
namespace PivasBatch
{
    partial class Pivasbatch
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
            this.Text_SelectText = new System.Windows.Forms.TextBox();
            this.Check_Ivstatus = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlWard = new System.Windows.Forms.FlowLayoutPanel();
            this.Panel_BatchRule = new System.Windows.Forms.FlowLayoutPanel();
            this.Panel_Patient = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Label_Get = new System.Windows.Forms.Panel();
            this.Label_SetUp = new System.Windows.Forms.Panel();
            this.Label_Generate = new System.Windows.Forms.Panel();
            this.Label_GennerationBatch = new System.Windows.Forms.Panel();
            this.Label_Line = new System.Windows.Forms.Panel();
            this.Sp_Info = new System.Windows.Forms.SplitContainer();
            this.dgv_Info = new PivasBatch.ViewInfo();
            this.Label_Show = new System.Windows.Forms.Label();
            this.med1 = new PivasBatch.Med();
            this.Label_show2 = new System.Windows.Forms.Label();
            this.Comb_K_U = new System.Windows.Forms.ComboBox();
            this.Check_IsSame = new System.Windows.Forms.ComboBox();
            this.Check_BatchSaved = new System.Windows.Forms.ComboBox();
            this.DateP_Date = new System.Windows.Forms.DateTimePicker();
            this.Label_down = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Sp_Info.Panel1.SuspendLayout();
            this.Sp_Info.Panel2.SuspendLayout();
            this.Sp_Info.SuspendLayout();
            this.SuspendLayout();
            // 
            // Text_SelectText
            // 
            this.Text_SelectText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.Text_SelectText.Location = new System.Drawing.Point(217, 8);
            this.Text_SelectText.Name = "Text_SelectText";
            this.Text_SelectText.Size = new System.Drawing.Size(132, 21);
            this.Text_SelectText.TabIndex = 21;
            this.Text_SelectText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Text_SelectText_KeyPress);
            // 
            // Check_Ivstatus
            // 
            this.Check_Ivstatus.AutoSize = true;
            this.Check_Ivstatus.Location = new System.Drawing.Point(587, 6);
            this.Check_Ivstatus.Name = "Check_Ivstatus";
            this.Check_Ivstatus.Size = new System.Drawing.Size(60, 16);
            this.Check_Ivstatus.TabIndex = 15;
            this.Check_Ivstatus.Text = "已打印";
            this.Check_Ivstatus.UseVisualStyleBackColor = true;
            this.Check_Ivstatus.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(181, 12);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 12;
            this.label10.Text = "搜索";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "日期";
            // 
            // pnlWard
            // 
            this.pnlWard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlWard.AutoScroll = true;
            this.pnlWard.BackColor = System.Drawing.Color.Transparent;
            this.pnlWard.Location = new System.Drawing.Point(0, 55);
            this.pnlWard.Margin = new System.Windows.Forms.Padding(0);
            this.pnlWard.Name = "pnlWard";
            this.pnlWard.Size = new System.Drawing.Size(172, 438);
            this.pnlWard.TabIndex = 25;
            // 
            // Panel_BatchRule
            // 
            this.Panel_BatchRule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_BatchRule.AutoScroll = true;
            this.Panel_BatchRule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.Panel_BatchRule.Location = new System.Drawing.Point(181, 36);
            this.Panel_BatchRule.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_BatchRule.Name = "Panel_BatchRule";
            this.Panel_BatchRule.Size = new System.Drawing.Size(662, 459);
            this.Panel_BatchRule.TabIndex = 26;
            this.Panel_BatchRule.Visible = false;
            // 
            // Panel_Patient
            // 
            this.Panel_Patient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.Panel_Patient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.Panel_Patient.Location = new System.Drawing.Point(159, 36);
            this.Panel_Patient.Name = "Panel_Patient";
            this.Panel_Patient.Size = new System.Drawing.Size(129, 460);
            this.Panel_Patient.TabIndex = 0;
            this.Panel_Patient.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 28;
            this.label2.Text = "病区";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(101, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 28;
            this.label3.Text = "未发";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(127, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 12);
            this.label4.TabIndex = 28;
            this.label4.Text = "/总";
            // 
            // Label_Get
            // 
            this.Label_Get.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Get.BackgroundImage = global::PivasBatchMX.Properties.Resources.发送;
            this.Label_Get.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Label_Get.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label_Get.Font = new System.Drawing.Font("宋体", 11F);
            this.Label_Get.Location = new System.Drawing.Point(765, 5);
            this.Label_Get.Name = "Label_Get";
            this.Label_Get.Size = new System.Drawing.Size(23, 23);
            this.Label_Get.TabIndex = 20;
            this.toolTip1.SetToolTip(this.Label_Get, "发送批次");
            this.Label_Get.MouseLeave += new System.EventHandler(this.Label_Generate_MouseLeave);
            this.Label_Get.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_Generate_MouseMove);
            this.Label_Get.Click += new System.EventHandler(this.Label_Get_Click);
            // 
            // Label_SetUp
            // 
            this.Label_SetUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_SetUp.BackgroundImage = global::PivasBatchMX.Properties.Resources.设置;
            this.Label_SetUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Label_SetUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label_SetUp.Font = new System.Drawing.Font("宋体", 11F);
            this.Label_SetUp.Location = new System.Drawing.Point(695, 4);
            this.Label_SetUp.Name = "Label_SetUp";
            this.Label_SetUp.Size = new System.Drawing.Size(23, 23);
            this.Label_SetUp.TabIndex = 18;
            this.toolTip1.SetToolTip(this.Label_SetUp, "设置");
            this.Label_SetUp.MouseLeave += new System.EventHandler(this.Label_Generate_MouseLeave);
            this.Label_SetUp.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_Generate_MouseMove);
            this.Label_SetUp.Click += new System.EventHandler(this.Label_SetUp_Click);
            // 
            // Label_Generate
            // 
            this.Label_Generate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Generate.BackgroundImage = global::PivasBatchMX.Properties.Resources.生成;
            this.Label_Generate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Label_Generate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label_Generate.Font = new System.Drawing.Font("宋体", 11F);
            this.Label_Generate.Location = new System.Drawing.Point(653, 5);
            this.Label_Generate.Name = "Label_Generate";
            this.Label_Generate.Size = new System.Drawing.Size(23, 23);
            this.Label_Generate.TabIndex = 17;
            this.toolTip1.SetToolTip(this.Label_Generate, "生成瓶签");
            this.Label_Generate.MouseLeave += new System.EventHandler(this.Label_Generate_MouseLeave);
            this.Label_Generate.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_Generate_MouseMove);
            this.Label_Generate.Click += new System.EventHandler(this.Label_Generate_Click);
            // 
            // Label_GennerationBatch
            // 
            this.Label_GennerationBatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_GennerationBatch.BackgroundImage = global::PivasBatchMX.Properties.Resources.重排;
            this.Label_GennerationBatch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Label_GennerationBatch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label_GennerationBatch.Font = new System.Drawing.Font("宋体", 11F);
            this.Label_GennerationBatch.Location = new System.Drawing.Point(730, 4);
            this.Label_GennerationBatch.Name = "Label_GennerationBatch";
            this.Label_GennerationBatch.Size = new System.Drawing.Size(23, 23);
            this.Label_GennerationBatch.TabIndex = 16;
            this.toolTip1.SetToolTip(this.Label_GennerationBatch, "重排批次");
            this.Label_GennerationBatch.MouseLeave += new System.EventHandler(this.Label_Generate_MouseLeave);
            this.Label_GennerationBatch.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_Generate_MouseMove);
            this.Label_GennerationBatch.Click += new System.EventHandler(this.Label_GennerationBatch_Click);
            // 
            // Label_Line
            // 
            this.Label_Line.BackgroundImage = global::PivasBatchMX.Properties.Resources.阴影;
            this.Label_Line.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Label_Line.Location = new System.Drawing.Point(649, 26);
            this.Label_Line.Name = "Label_Line";
            this.Label_Line.Size = new System.Drawing.Size(23, 6);
            this.Label_Line.TabIndex = 29;
            this.Label_Line.Visible = false;
            // 
            // Sp_Info
            // 
            this.Sp_Info.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Sp_Info.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.Sp_Info.Location = new System.Drawing.Point(184, 36);
            this.Sp_Info.Name = "Sp_Info";
            // 
            // Sp_Info.Panel1
            // 
            this.Sp_Info.Panel1.Controls.Add(this.dgv_Info);
            this.Sp_Info.Panel1MinSize = 0;
            // 
            // Sp_Info.Panel2
            // 
            this.Sp_Info.Panel2.Controls.Add(this.Label_Show);
            this.Sp_Info.Panel2.Controls.Add(this.med1);
            this.Sp_Info.Panel2MinSize = 0;
            this.Sp_Info.Size = new System.Drawing.Size(613, 457);
            this.Sp_Info.SplitterDistance = 239;
            this.Sp_Info.SplitterWidth = 1;
            this.Sp_Info.TabIndex = 31;
            this.Sp_Info.Visible = false;
            // 
            // dgv_Info
            // 
            this.dgv_Info.AutoScroll = true;
            this.dgv_Info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Info.Location = new System.Drawing.Point(0, 0);
            this.dgv_Info.Name = "dgv_Info";
            this.dgv_Info.Size = new System.Drawing.Size(239, 457);
            this.dgv_Info.TabIndex = 30;
            // 
            // Label_Show
            // 
            this.Label_Show.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_Show.BackColor = System.Drawing.Color.White;
            this.Label_Show.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label_Show.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(4)));
            this.Label_Show.Location = new System.Drawing.Point(0, 0);
            this.Label_Show.Name = "Label_Show";
            this.Label_Show.Size = new System.Drawing.Size(11, 458);
            this.Label_Show.TabIndex = 1;
            this.Label_Show.Text = ">";
            this.Label_Show.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_Show.Click += new System.EventHandler(this.Label_Show_Click);
            // 
            // med1
            // 
            this.med1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.med1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.med1.Location = new System.Drawing.Point(0, 0);
            this.med1.Name = "med1";
            this.med1.Size = new System.Drawing.Size(373, 457);
            this.med1.TabIndex = 3;
            // 
            // Label_show2
            // 
            this.Label_show2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_show2.BackColor = System.Drawing.Color.White;
            this.Label_show2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label_show2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(4)));
            this.Label_show2.Location = new System.Drawing.Point(790, 36);
            this.Label_show2.Name = "Label_show2";
            this.Label_show2.Size = new System.Drawing.Size(11, 459);
            this.Label_show2.TabIndex = 1;
            this.Label_show2.Text = "<";
            this.Label_show2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_show2.Click += new System.EventHandler(this.Label_Show_Click);
            // 
            // Comb_K_U
            // 
            this.Comb_K_U.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.Comb_K_U.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Comb_K_U.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Comb_K_U.FormattingEnabled = true;
            this.Comb_K_U.Items.AddRange(new object[] {
            "#",
            "K",
            "L",
            "全部"});
            this.Comb_K_U.Location = new System.Drawing.Point(509, 8);
            this.Comb_K_U.Name = "Comb_K_U";
            this.Comb_K_U.Size = new System.Drawing.Size(57, 20);
            this.Comb_K_U.TabIndex = 32;
            this.Comb_K_U.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Check_IsSame
            // 
            this.Check_IsSame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.Check_IsSame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Check_IsSame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Check_IsSame.FormattingEnabled = true;
            this.Check_IsSame.Items.AddRange(new object[] {
            "未改动",
            "有改动",
            "全部"});
            this.Check_IsSame.Location = new System.Drawing.Point(436, 8);
            this.Check_IsSame.Name = "Check_IsSame";
            this.Check_IsSame.Size = new System.Drawing.Size(70, 20);
            this.Check_IsSame.TabIndex = 32;
            this.Check_IsSame.SelectedIndexChanged += new System.EventHandler(this.Check_IsSame_SelectedIndexChanged);
            // 
            // Check_BatchSaved
            // 
            this.Check_BatchSaved.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.Check_BatchSaved.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Check_BatchSaved.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Check_BatchSaved.FormattingEnabled = true;
            this.Check_BatchSaved.Items.AddRange(new object[] {
            "未发送",
            "已发送"});
            this.Check_BatchSaved.Location = new System.Drawing.Point(363, 8);
            this.Check_BatchSaved.Name = "Check_BatchSaved";
            this.Check_BatchSaved.Size = new System.Drawing.Size(67, 20);
            this.Check_BatchSaved.TabIndex = 32;
            this.Check_BatchSaved.SelectedIndexChanged += new System.EventHandler(this.Check_BatchSaved_SelectedIndexChanged);
            // 
            // DateP_Date
            // 
            this.DateP_Date.CustomFormat = "yyyyMMdd";
            this.DateP_Date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateP_Date.Location = new System.Drawing.Point(30, 8);
            this.DateP_Date.Name = "DateP_Date";
            this.DateP_Date.Size = new System.Drawing.Size(121, 21);
            this.DateP_Date.TabIndex = 33;
            this.DateP_Date.ValueChanged += new System.EventHandler(this.MC_Date2_ValueChanged);
            // 
            // Label_down
            // 
            this.Label_down.AutoSize = true;
            this.Label_down.ForeColor = System.Drawing.Color.Gray;
            this.Label_down.Location = new System.Drawing.Point(151, 13);
            this.Label_down.Name = "Label_down";
            this.Label_down.Size = new System.Drawing.Size(23, 12);
            this.Label_down.TabIndex = 34;
            this.Label_down.Text = "180";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Pivasbatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(210)))), ((int)(((byte)(251)))));
            this.Controls.Add(this.DateP_Date);
            this.Controls.Add(this.Check_BatchSaved);
            this.Controls.Add(this.Check_IsSame);
            this.Controls.Add(this.Comb_K_U);
            this.Controls.Add(this.Label_show2);
            this.Controls.Add(this.Sp_Info);
            this.Controls.Add(this.Label_Line);
            this.Controls.Add(this.Panel_BatchRule);
            this.Controls.Add(this.Panel_Patient);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnlWard);
            this.Controls.Add(this.Text_SelectText);
            this.Controls.Add(this.Label_Get);
            this.Controls.Add(this.Label_SetUp);
            this.Controls.Add(this.Label_Generate);
            this.Controls.Add(this.Label_GennerationBatch);
            this.Controls.Add(this.Check_Ivstatus);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Label_down);
            this.DoubleBuffered = true;
            this.Name = "Pivasbatch";
            this.Size = new System.Drawing.Size(800, 497);
            this.Load += new System.EventHandler(this.Pivasbatch_Load);
            this.SizeChanged += new System.EventHandler(this.Pivasbatch_SizeChanged);
            this.Sp_Info.Panel1.ResumeLayout(false);
            this.Sp_Info.Panel2.ResumeLayout(false);
            this.Sp_Info.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Text_SelectText;
        private System.Windows.Forms.Panel Label_Get;
        private System.Windows.Forms.Panel Label_SetUp;
        private System.Windows.Forms.Panel Label_Generate;
        private System.Windows.Forms.Panel Label_GennerationBatch;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.FlowLayoutPanel pnlWard;
        public System.Windows.Forms.FlowLayoutPanel Panel_BatchRule;
        public Panel Panel_Patient;
        public CheckBox Check_Ivstatus;
        private Label label2;
        private Label label3;
        private Label label4;
        private Panel Label_Line;
        private ToolTip toolTip1;
        public ViewInfo dgv_Info;
       // public Med Med;
        public SplitContainer Sp_Info;
        private Label Label_Show;
        private Label Label_show2;
        public Med med1;
        private ComboBox Comb_K_U;
        private ComboBox Check_IsSame;
        private ComboBox Check_BatchSaved;
        private DateTimePicker DateP_Date;
        private Label Label_down;
        private Timer timer1;
    }
}
