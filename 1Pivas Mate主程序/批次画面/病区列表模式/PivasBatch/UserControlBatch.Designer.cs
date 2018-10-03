using System.Windows.Forms;
namespace PivasBatch
{
    partial class UserControlBatch
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Text_SelectText = new System.Windows.Forms.TextBox();
            this.Panel_BatchRule = new System.Windows.Forms.FlowLayoutPanel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnGennerationBatch = new System.Windows.Forms.Button();
            this.btnSet = new System.Windows.Forms.Button();
            this.Check_BatchSaved = new System.Windows.Forms.ComboBox();
            this.DateP_Date = new System.Windows.Forms.DateTimePicker();
            this.Label_down = new System.Windows.Forms.Label();
            this.Txt_Serch = new System.Windows.Forms.TextBox();
            this.Check_ChongPei = new System.Windows.Forms.CheckBox();
            this.Check_K = new System.Windows.Forms.CheckBox();
            this.Check_L = new System.Windows.Forms.CheckBox();
            this.cbbWardArea = new System.Windows.Forms.ComboBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.cbbMode = new System.Windows.Forms.ComboBox();
            this.Check_Long = new System.Windows.Forms.CheckBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtPatient = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.Sp_Info = new System.Windows.Forms.SplitContainer();
            this.dgv_Info = new PivasBatch.ViewInfo();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Batch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.med1 = new PivasBatch.Med();
            this.Check_IsSame = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.Panel_Patient = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.dgvWard = new System.Windows.Forms.DataGridView();
            this.dgvselect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.WardName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.num1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wardcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wardarea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cblong = new System.Windows.Forms.CheckBox();
            this.cbtemp = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.LCancelSend = new System.Windows.Forms.Label();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnBatchModify = new System.Windows.Forms.Button();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSynDrugList = new System.Windows.Forms.Button();
            this.lblUnSend = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.Sp_Info.Panel1.SuspendLayout();
            this.Sp_Info.Panel2.SuspendLayout();
            this.Sp_Info.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWard)).BeginInit();
            this.panel2.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Text_SelectText
            // 
            this.Text_SelectText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.Text_SelectText.ForeColor = System.Drawing.Color.Gray;
            this.Text_SelectText.Location = new System.Drawing.Point(1, 24);
            this.Text_SelectText.Margin = new System.Windows.Forms.Padding(1);
            this.Text_SelectText.Name = "Text_SelectText";
            this.Text_SelectText.Size = new System.Drawing.Size(219, 21);
            this.Text_SelectText.TabIndex = 211;
            this.Text_SelectText.Text = "姓名(编码)/床号/主药/溶媒/频序/组号";
            this.toolTip1.SetToolTip(this.Text_SelectText, "瓶签查询");
            this.Text_SelectText.Enter += new System.EventHandler(this.Text_SelectText_Enter);
            this.Text_SelectText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Text_SelectText_KeyPress);
            this.Text_SelectText.Leave += new System.EventHandler(this.Text_SelectText_Leave);
            // 
            // Panel_BatchRule
            // 
            this.Panel_BatchRule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_BatchRule.AutoScroll = true;
            this.Panel_BatchRule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.Panel_BatchRule.Location = new System.Drawing.Point(400, 53);
            this.Panel_BatchRule.Name = "Panel_BatchRule";
            this.Panel_BatchRule.Size = new System.Drawing.Size(805, 391);
            this.Panel_BatchRule.TabIndex = 26;
            this.Panel_BatchRule.Visible = false;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.Location = new System.Drawing.Point(822, 27);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(39, 27);
            this.btnGenerate.TabIndex = 222;
            this.btnGenerate.Text = "生成";
            this.toolTip1.SetToolTip(this.btnGenerate, "生成瓶签");
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnGennerationBatch
            // 
            this.btnGennerationBatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGennerationBatch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnGennerationBatch.Location = new System.Drawing.Point(898, 27);
            this.btnGennerationBatch.Name = "btnGennerationBatch";
            this.btnGennerationBatch.Size = new System.Drawing.Size(39, 27);
            this.btnGennerationBatch.TabIndex = 224;
            this.btnGennerationBatch.Text = "重排";
            this.toolTip1.SetToolTip(this.btnGennerationBatch, "重排批次");
            this.btnGennerationBatch.UseVisualStyleBackColor = true;
            this.btnGennerationBatch.Click += new System.EventHandler(this.btnGennerationBatch_Click);
            // 
            // btnSet
            // 
            this.btnSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSet.Image = global::PivasBatch.Properties.Resources.gear_16;
            this.btnSet.Location = new System.Drawing.Point(936, 27);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(39, 27);
            this.btnSet.TabIndex = 223;
            this.toolTip1.SetToolTip(this.btnSet, "设置");
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // Check_BatchSaved
            // 
            this.Check_BatchSaved.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.Check_BatchSaved.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Check_BatchSaved.FormattingEnabled = true;
            this.Check_BatchSaved.Items.AddRange(new object[] {
            "未发送",
            "已发送",
            "已打印"});
            this.Check_BatchSaved.Location = new System.Drawing.Point(6, 7);
            this.Check_BatchSaved.Margin = new System.Windows.Forms.Padding(1);
            this.Check_BatchSaved.Name = "Check_BatchSaved";
            this.Check_BatchSaved.Size = new System.Drawing.Size(84, 20);
            this.Check_BatchSaved.TabIndex = 32;
            this.toolTip1.SetToolTip(this.Check_BatchSaved, "瓶签状态筛选");
            this.Check_BatchSaved.SelectedIndexChanged += new System.EventHandler(this.Check_BatchSaved_SelectedIndexChanged);
            // 
            // DateP_Date
            // 
            this.DateP_Date.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DateP_Date.CustomFormat = "yyyy-MM-dd";
            this.DateP_Date.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DateP_Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateP_Date.Location = new System.Drawing.Point(860, 0);
            this.DateP_Date.Name = "DateP_Date";
            this.DateP_Date.Size = new System.Drawing.Size(114, 26);
            this.DateP_Date.TabIndex = 33;
            this.toolTip1.SetToolTip(this.DateP_Date, "日期");
            this.DateP_Date.ValueChanged += new System.EventHandler(this.MC_Date2_ValueChanged);
            // 
            // Label_down
            // 
            this.Label_down.AutoSize = true;
            this.Label_down.ForeColor = System.Drawing.Color.Gray;
            this.Label_down.Location = new System.Drawing.Point(577, 39);
            this.Label_down.Name = "Label_down";
            this.Label_down.Size = new System.Drawing.Size(23, 12);
            this.Label_down.TabIndex = 34;
            this.Label_down.Text = "180";
            this.toolTip1.SetToolTip(this.Label_down, "屏幕定时刷新");
            // 
            // Txt_Serch
            // 
            this.Txt_Serch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Txt_Serch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.Txt_Serch.ForeColor = System.Drawing.Color.Gray;
            this.Txt_Serch.Location = new System.Drawing.Point(100, 422);
            this.Txt_Serch.Name = "Txt_Serch";
            this.Txt_Serch.Size = new System.Drawing.Size(107, 21);
            this.Txt_Serch.TabIndex = 210;
            this.Txt_Serch.Text = "病区名/简拼";
            this.toolTip1.SetToolTip(this.Txt_Serch, "病区查询");
            this.Txt_Serch.TextChanged += new System.EventHandler(this.Txt_Serch_TextChanged);
            this.Txt_Serch.Enter += new System.EventHandler(this.Txt_Serch_Enter);
            this.Txt_Serch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Txt_Serch_KeyPress);
            this.Txt_Serch.Leave += new System.EventHandler(this.Txt_Serch_Leave);
            // 
            // Check_ChongPei
            // 
            this.Check_ChongPei.AutoSize = true;
            this.Check_ChongPei.Checked = true;
            this.Check_ChongPei.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Check_ChongPei.Location = new System.Drawing.Point(3, 3);
            this.Check_ChongPei.Name = "Check_ChongPei";
            this.Check_ChongPei.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.Check_ChongPei.Size = new System.Drawing.Size(32, 16);
            this.Check_ChongPei.TabIndex = 215;
            this.Check_ChongPei.Text = "#";
            this.toolTip1.SetToolTip(this.Check_ChongPei, "瓶签筛选");
            this.Check_ChongPei.UseVisualStyleBackColor = true;
            this.Check_ChongPei.CheckedChanged += new System.EventHandler(this.Check_ChongPei_CheckedChanged);
            // 
            // Check_K
            // 
            this.Check_K.AutoSize = true;
            this.Check_K.Checked = true;
            this.Check_K.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Check_K.Location = new System.Drawing.Point(47, 3);
            this.Check_K.Margin = new System.Windows.Forms.Padding(9, 3, 3, 3);
            this.Check_K.Name = "Check_K";
            this.Check_K.Size = new System.Drawing.Size(30, 16);
            this.Check_K.TabIndex = 37;
            this.Check_K.Text = "K";
            this.toolTip1.SetToolTip(this.Check_K, "瓶签筛选");
            this.Check_K.UseVisualStyleBackColor = true;
            this.Check_K.CheckedChanged += new System.EventHandler(this.Check_ChongPei_CheckedChanged);
            this.Check_K.Click += new System.EventHandler(this.Check_L_Click);
            // 
            // Check_L
            // 
            this.Check_L.AutoSize = true;
            this.Check_L.Checked = true;
            this.Check_L.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Check_L.Location = new System.Drawing.Point(45, 3);
            this.Check_L.Name = "Check_L";
            this.Check_L.Size = new System.Drawing.Size(30, 16);
            this.Check_L.TabIndex = 36;
            this.Check_L.Text = "L";
            this.toolTip1.SetToolTip(this.Check_L, "瓶签筛选");
            this.Check_L.UseVisualStyleBackColor = true;
            this.Check_L.CheckedChanged += new System.EventHandler(this.Check_ChongPei_CheckedChanged);
            this.Check_L.Click += new System.EventHandler(this.Check_L_Click);
            // 
            // cbbWardArea
            // 
            this.cbbWardArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbbWardArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbWardArea.FormattingEnabled = true;
            this.cbbWardArea.Location = new System.Drawing.Point(1, 423);
            this.cbbWardArea.Name = "cbbWardArea";
            this.cbbWardArea.Size = new System.Drawing.Size(97, 20);
            this.cbbWardArea.TabIndex = 36;
            this.toolTip1.SetToolTip(this.cbbWardArea, "病区组筛选");
            this.cbbWardArea.SelectedIndexChanged += new System.EventHandler(this.cbbWardArea_SelectedIndexChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.Location = new System.Drawing.Point(3, 3);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.checkBox2.Size = new System.Drawing.Size(77, 19);
            this.checkBox2.TabIndex = 218;
            this.checkBox2.Text = "快速修改";
            this.toolTip1.SetToolTip(this.checkBox2, "此项打上勾，可以在列表模式下，直接按键盘修改批次。（修改批次按数字键，冲配与否按K）");
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // cbbMode
            // 
            this.cbbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbMode.FormattingEnabled = true;
            this.cbbMode.Items.AddRange(new object[] {
            "患者明细模式",
            "病区列表模式"});
            this.cbbMode.Location = new System.Drawing.Point(6, 31);
            this.cbbMode.Name = "cbbMode";
            this.cbbMode.Size = new System.Drawing.Size(84, 20);
            this.cbbMode.TabIndex = 217;
            this.toolTip1.SetToolTip(this.cbbMode, "查看模式切换");
            this.cbbMode.SelectedIndexChanged += new System.EventHandler(this.cbbMode_SelectedIndexChanged);
            // 
            // Check_Long
            // 
            this.Check_Long.AutoSize = true;
            this.Check_Long.Checked = true;
            this.Check_Long.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Check_Long.Location = new System.Drawing.Point(3, 3);
            this.Check_Long.Name = "Check_Long";
            this.Check_Long.Size = new System.Drawing.Size(36, 16);
            this.Check_Long.TabIndex = 216;
            this.Check_Long.Text = "长";
            this.toolTip1.SetToolTip(this.Check_Long, "瓶签筛选");
            this.Check_Long.UseVisualStyleBackColor = true;
            this.Check_Long.CheckedChanged += new System.EventHandler(this.Check_ChongPei_CheckedChanged);
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSend.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSend.Location = new System.Drawing.Point(92, 4);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(66, 49);
            this.btnSend.TabIndex = 215;
            this.btnSend.Text = "发送";
            this.toolTip1.SetToolTip(this.btnSend, "批量保存批次，发送后才可在打印画面打印。");
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Location = new System.Drawing.Point(8, 4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(94, 19);
            this.checkBox1.TabIndex = 217;
            this.checkBox1.Text = "病区列表";
            this.toolTip1.SetToolTip(this.checkBox1, "病区全选");
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckStateChanged += new System.EventHandler(this.checkBox1_CheckStateChanged);
            // 
            // txtPatient
            // 
            this.txtPatient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPatient.ForeColor = System.Drawing.Color.Gray;
            this.txtPatient.Location = new System.Drawing.Point(3, 366);
            this.txtPatient.Name = "txtPatient";
            this.txtPatient.Size = new System.Drawing.Size(140, 21);
            this.txtPatient.TabIndex = 225;
            this.txtPatient.Text = "患者姓名/ID/床号";
            this.toolTip1.SetToolTip(this.txtPatient, "患者查询");
            this.txtPatient.Click += new System.EventHandler(this.txtPatient_Click);
            this.txtPatient.Enter += new System.EventHandler(this.txtPatient_Enter);
            this.txtPatient.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPatient_KeyPress);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(860, 27);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(39, 27);
            this.button2.TabIndex = 227;
            this.button2.Text = "差异";
            this.toolTip1.SetToolTip(this.button2, "瓶签药品与处方不一致操作");
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // Sp_Info
            // 
            this.Sp_Info.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Sp_Info.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.Sp_Info.Location = new System.Drawing.Point(355, 55);
            this.Sp_Info.Margin = new System.Windows.Forms.Padding(0);
            this.Sp_Info.Name = "Sp_Info";
            // 
            // Sp_Info.Panel1
            // 
            this.Sp_Info.Panel1.Controls.Add(this.dgv_Info);
            this.Sp_Info.Panel1MinSize = 0;
            // 
            // Sp_Info.Panel2
            // 
            this.Sp_Info.Panel2.Controls.Add(this.button1);
            this.Sp_Info.Panel2.Controls.Add(this.dataGridView2);
            this.Sp_Info.Panel2.Controls.Add(this.med1);
            this.Sp_Info.Panel2MinSize = 0;
            this.Sp_Info.Size = new System.Drawing.Size(767, 385);
            this.Sp_Info.SplitterDistance = 336;
            this.Sp_Info.SplitterWidth = 2;
            this.Sp_Info.TabIndex = 31;
            this.Sp_Info.Visible = false;
            // 
            // dgv_Info
            // 
            this.dgv_Info.AutoScroll = true;
            this.dgv_Info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Info.Location = new System.Drawing.Point(0, 0);
            this.dgv_Info.Margin = new System.Windows.Forms.Padding(0);
            this.dgv_Info.Name = "dgv_Info";
            this.dgv_Info.Size = new System.Drawing.Size(336, 385);
            this.dgv_Info.TabIndex = 30;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(349, 139);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "批次汇总";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
            this.button1.MouseHover += new System.EventHandler(this.button1_MouseHover);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToResizeColumns = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Batch,
            this.count});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView2.Location = new System.Drawing.Point(229, 139);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(118, 136);
            this.dataGridView2.TabIndex = 4;
            this.dataGridView2.Visible = false;
            this.dataGridView2.Leave += new System.EventHandler(this.dataGridView2_Leave);
            // 
            // Batch
            // 
            this.Batch.DataPropertyName = "Batch";
            this.Batch.HeaderText = "批次";
            this.Batch.Name = "Batch";
            this.Batch.Width = 60;
            // 
            // count
            // 
            this.count.DataPropertyName = "count";
            this.count.HeaderText = "数量";
            this.count.Name = "count";
            this.count.Width = 40;
            // 
            // med1
            // 
            this.med1.AutoSize = true;
            this.med1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.med1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.med1.Location = new System.Drawing.Point(0, 0);
            this.med1.Margin = new System.Windows.Forms.Padding(0);
            this.med1.Name = "med1";
            this.med1.Size = new System.Drawing.Size(429, 385);
            this.med1.TabIndex = 3;
            // 
            // Check_IsSame
            // 
            this.Check_IsSame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.Check_IsSame.Dock = System.Windows.Forms.DockStyle.Left;
            this.Check_IsSame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Check_IsSame.FormattingEnabled = true;
            this.Check_IsSame.Items.AddRange(new object[] {
            "未改动",
            "有改动",
            "有/未改动"});
            this.Check_IsSame.Location = new System.Drawing.Point(1, 4);
            this.Check_IsSame.Margin = new System.Windows.Forms.Padding(1, 4, 1, 1);
            this.Check_IsSame.Name = "Check_IsSame";
            this.Check_IsSame.Size = new System.Drawing.Size(80, 20);
            this.Check_IsSame.TabIndex = 32;
            this.Check_IsSame.SelectedIndexChanged += new System.EventHandler(this.Check_IsSame_SelectedIndexChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Panel_Patient
            // 
            this.Panel_Patient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Panel_Patient.BackColor = System.Drawing.Color.White;
            this.Panel_Patient.Location = new System.Drawing.Point(3, 0);
            this.Panel_Patient.Name = "Panel_Patient";
            this.Panel_Patient.Size = new System.Drawing.Size(141, 364);
            this.Panel_Patient.TabIndex = 0;
            this.Panel_Patient.Visible = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.flowLayoutPanel1.Controls.Add(this.Check_ChongPei);
            this.flowLayoutPanel1.Controls.Add(this.Check_K);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(92, 1);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 1, 1, 1);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(102, 21);
            this.flowLayoutPanel1.TabIndex = 35;
            // 
            // dgvWard
            // 
            this.dgvWard.AllowUserToAddRows = false;
            this.dgvWard.AllowUserToResizeColumns = false;
            this.dgvWard.AllowUserToResizeRows = false;
            this.dgvWard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvWard.BackgroundColor = System.Drawing.Color.White;
            this.dgvWard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvWard.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvWard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvWard.ColumnHeadersVisible = false;
            this.dgvWard.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvselect,
            this.WardName,
            this.num1,
            this.sum,
            this.wardcode,
            this.wardarea,
            this.Column1});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvWard.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvWard.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvWard.Location = new System.Drawing.Point(-1, 25);
            this.dgvWard.MultiSelect = false;
            this.dgvWard.Name = "dgvWard";
            this.dgvWard.RowHeadersVisible = false;
            this.dgvWard.RowTemplate.Height = 23;
            this.dgvWard.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWard.Size = new System.Drawing.Size(208, 396);
            this.dgvWard.TabIndex = 38;
            this.dgvWard.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWard_CellClick);
            this.dgvWard.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWard_CellContentClick);
            // 
            // dgvselect
            // 
            this.dgvselect.DataPropertyName = "select";
            this.dgvselect.FalseValue = "False";
            this.dgvselect.HeaderText = "";
            this.dgvselect.MinimumWidth = 10;
            this.dgvselect.Name = "dgvselect";
            this.dgvselect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvselect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvselect.TrueValue = "True";
            this.dgvselect.Width = 30;
            // 
            // WardName
            // 
            this.WardName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.WardName.DataPropertyName = "WardName";
            this.WardName.HeaderText = "病区名";
            this.WardName.Name = "WardName";
            this.WardName.ReadOnly = true;
            // 
            // num1
            // 
            this.num1.DataPropertyName = "UnCheckCount";
            this.num1.HeaderText = "未发";
            this.num1.Name = "num1";
            this.num1.ReadOnly = true;
            this.num1.Width = 30;
            // 
            // sum
            // 
            this.sum.DataPropertyName = "TotalCount";
            this.sum.HeaderText = "总";
            this.sum.Name = "sum";
            this.sum.ReadOnly = true;
            this.sum.Width = 30;
            // 
            // wardcode
            // 
            this.wardcode.DataPropertyName = "WardCode";
            this.wardcode.HeaderText = "编码";
            this.wardcode.Name = "wardcode";
            this.wardcode.ReadOnly = true;
            this.wardcode.Visible = false;
            // 
            // wardarea
            // 
            this.wardarea.DataPropertyName = "WardArea";
            this.wardarea.HeaderText = "病区组";
            this.wardarea.Name = "wardarea";
            this.wardarea.Visible = false;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "SpellCode";
            this.Column1.HeaderText = "简拼";
            this.Column1.Name = "Column1";
            this.Column1.Visible = false;
            // 
            // cblong
            // 
            this.cblong.AutoSize = true;
            this.cblong.Checked = true;
            this.cblong.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cblong.Location = new System.Drawing.Point(3, 49);
            this.cblong.Name = "cblong";
            this.cblong.Size = new System.Drawing.Size(48, 16);
            this.cblong.TabIndex = 212;
            this.cblong.Text = "长期";
            this.cblong.UseVisualStyleBackColor = true;
            this.cblong.Visible = false;
            this.cblong.CheckedChanged += new System.EventHandler(this.cbtemp_CheckedChanged);
            // 
            // cbtemp
            // 
            this.cbtemp.AutoSize = true;
            this.cbtemp.Checked = true;
            this.cbtemp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbtemp.Location = new System.Drawing.Point(57, 49);
            this.cbtemp.Name = "cbtemp";
            this.cbtemp.Size = new System.Drawing.Size(48, 16);
            this.cbtemp.TabIndex = 213;
            this.cbtemp.Text = "临时";
            this.cbtemp.UseVisualStyleBackColor = true;
            this.cbtemp.Visible = false;
            this.cbtemp.CheckedChanged += new System.EventHandler(this.cbtemp_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.flowLayoutPanel6);
            this.panel2.Controls.Add(this.Check_BatchSaved);
            this.panel2.Controls.Add(this.Label_down);
            this.panel2.Controls.Add(this.cbbMode);
            this.panel2.Controls.Add(this.btnSend);
            this.panel2.Controls.Add(this.flowLayoutPanel5);
            this.panel2.Location = new System.Drawing.Point(207, -2);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(583, 56);
            this.panel2.TabIndex = 216;
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.Controls.Add(this.checkBox2);
            this.flowLayoutPanel6.Controls.Add(this.LCancelSend);
            this.flowLayoutPanel6.Controls.Add(this.flowLayoutPanel4);
            this.flowLayoutPanel6.Location = new System.Drawing.Point(162, 29);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Size = new System.Drawing.Size(355, 26);
            this.flowLayoutPanel6.TabIndex = 223;
            // 
            // LCancelSend
            // 
            this.LCancelSend.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LCancelSend.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.LCancelSend.Location = new System.Drawing.Point(86, 0);
            this.LCancelSend.Name = "LCancelSend";
            this.LCancelSend.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.LCancelSend.Size = new System.Drawing.Size(65, 22);
            this.LCancelSend.TabIndex = 220;
            this.LCancelSend.Text = "取消发送";
            this.LCancelSend.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LCancelSend.Visible = false;
            this.LCancelSend.Click += new System.EventHandler(this.LCancelSend_Click);
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.flowLayoutPanel4.Controls.Add(this.checkBox3);
            this.flowLayoutPanel4.Controls.Add(this.checkBox4);
            this.flowLayoutPanel4.Controls.Add(this.checkBox5);
            this.flowLayoutPanel4.Controls.Add(this.checkBox6);
            this.flowLayoutPanel4.Controls.Add(this.checkBox7);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(157, 3);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(195, 21);
            this.flowLayoutPanel4.TabIndex = 219;
            // 
            // checkBox3
            // 
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(3, 3);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(33, 16);
            this.checkBox3.TabIndex = 0;
            this.checkBox3.Text = "普";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.Check_ChongPei_CheckedChanged);
            // 
            // checkBox4
            // 
            this.checkBox4.Checked = true;
            this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox4.Location = new System.Drawing.Point(42, 3);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(33, 16);
            this.checkBox4.TabIndex = 1;
            this.checkBox4.Text = "抗";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.Check_ChongPei_CheckedChanged);
            // 
            // checkBox5
            // 
            this.checkBox5.Checked = true;
            this.checkBox5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox5.Location = new System.Drawing.Point(81, 3);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(33, 16);
            this.checkBox5.TabIndex = 2;
            this.checkBox5.Text = "化";
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.CheckedChanged += new System.EventHandler(this.Check_ChongPei_CheckedChanged);
            // 
            // checkBox6
            // 
            this.checkBox6.Checked = true;
            this.checkBox6.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox6.Location = new System.Drawing.Point(120, 3);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(33, 16);
            this.checkBox6.TabIndex = 3;
            this.checkBox6.Text = "营";
            this.checkBox6.UseVisualStyleBackColor = true;
            this.checkBox6.CheckedChanged += new System.EventHandler(this.Check_ChongPei_CheckedChanged);
            // 
            // checkBox7
            // 
            this.checkBox7.Checked = true;
            this.checkBox7.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox7.Location = new System.Drawing.Point(159, 3);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(33, 16);
            this.checkBox7.TabIndex = 4;
            this.checkBox7.Text = "中";
            this.checkBox7.UseVisualStyleBackColor = true;
            this.checkBox7.CheckedChanged += new System.EventHandler(this.checkBox7_CheckedChanged);
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel5.Controls.Add(this.Check_IsSame);
            this.flowLayoutPanel5.Controls.Add(this.btnBatchModify);
            this.flowLayoutPanel5.Controls.Add(this.flowLayoutPanel3);
            this.flowLayoutPanel5.Location = new System.Drawing.Point(162, 3);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(355, 27);
            this.flowLayoutPanel5.TabIndex = 222;
            // 
            // btnBatchModify
            // 
            this.btnBatchModify.Location = new System.Drawing.Point(85, 3);
            this.btnBatchModify.Name = "btnBatchModify";
            this.btnBatchModify.Size = new System.Drawing.Size(65, 24);
            this.btnBatchModify.TabIndex = 221;
            this.btnBatchModify.Text = "批量修改";
            this.btnBatchModify.UseVisualStyleBackColor = true;
            this.btnBatchModify.Click += new System.EventHandler(this.btnBatchModify_Click);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel3.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel3.Controls.Add(this.flowLayoutPanel1);
            this.flowLayoutPanel3.Controls.Add(this.Text_SelectText);
            this.flowLayoutPanel3.Controls.Add(this.cblong);
            this.flowLayoutPanel3.Controls.Add(this.cbtemp);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(156, 3);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(3, 3, 1, 0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(197, 24);
            this.flowLayoutPanel3.TabIndex = 216;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.flowLayoutPanel2.Controls.Add(this.Check_Long);
            this.flowLayoutPanel2.Controls.Add(this.Check_L);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(1, 1);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(1);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(90, 21);
            this.flowLayoutPanel2.TabIndex = 214;
            // 
            // btnSynDrugList
            // 
            this.btnSynDrugList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSynDrugList.Location = new System.Drawing.Point(784, 27);
            this.btnSynDrugList.Name = "btnSynDrugList";
            this.btnSynDrugList.Size = new System.Drawing.Size(39, 27);
            this.btnSynDrugList.TabIndex = 219;
            this.btnSynDrugList.Text = "同步";
            this.btnSynDrugList.UseVisualStyleBackColor = true;
            this.btnSynDrugList.Click += new System.EventHandler(this.btnSynDrugList_Click);
            // 
            // lblUnSend
            // 
            this.lblUnSend.BackColor = System.Drawing.Color.Transparent;
            this.lblUnSend.Location = new System.Drawing.Point(106, 4);
            this.lblUnSend.Name = "lblUnSend";
            this.lblUnSend.Size = new System.Drawing.Size(35, 19);
            this.lblUnSend.TabIndex = 218;
            this.lblUnSend.Text = "0";
            this.lblUnSend.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            this.lblTotal.BackColor = System.Drawing.Color.Transparent;
            this.lblTotal.Location = new System.Drawing.Point(164, 4);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(40, 18);
            this.lblTotal.TabIndex = 219;
            this.lblTotal.Text = "0";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(147, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 220;
            this.label3.Text = "/";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblUnSend);
            this.panel1.Controls.Add(this.lblTotal);
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(208, 27);
            this.panel1.TabIndex = 221;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.Controls.Add(this.Panel_Patient);
            this.panel3.Controls.Add(this.txtPatient);
            this.panel3.Location = new System.Drawing.Point(207, 53);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(146, 389);
            this.panel3.TabIndex = 226;
            // 
            // timer3
            // 
            this.timer3.Interval = 1000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // UserControlBatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(210)))), ((int)(((byte)(251)))));
            this.Controls.Add(this.btnSynDrugList);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.btnGennerationBatch);
            this.Controls.Add(this.DateP_Date);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.Sp_Info);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbbWardArea);
            this.Controls.Add(this.Txt_Serch);
            this.Controls.Add(this.Panel_BatchRule);
            this.Controls.Add(this.dgvWard);
            this.Controls.Add(this.panel2);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UserControlBatch";
            this.Size = new System.Drawing.Size(976, 444);
            this.Load += new System.EventHandler(this.Pivasbatch_Load);
            this.SizeChanged += new System.EventHandler(this.Pivasbatch_SizeChanged);
            this.Sp_Info.Panel1.ResumeLayout(false);
            this.Sp_Info.Panel2.ResumeLayout(false);
            this.Sp_Info.Panel2.PerformLayout();
            this.Sp_Info.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWard)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.flowLayoutPanel6.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolTip toolTip1;
       // public Med Med;
        public SplitContainer Sp_Info;
        private ComboBox Check_IsSame;
        private ComboBox Check_BatchSaved;
        private DateTimePicker DateP_Date;
        private Label Label_down;
        private Timer timer1;
        private Timer timer2;
        private TextBox Txt_Serch;
        public ViewInfo dgv_Info;
        public Med med1;
        public Panel Panel_Patient;
        private FlowLayoutPanel flowLayoutPanel1;
        private CheckBox Check_L;
        private CheckBox Check_K;
        public TextBox Text_SelectText;
        private ComboBox cbbWardArea;
        private DataGridView dataGridView2;
        private Button button1;
        private DataGridView dgvWard;
        private CheckBox cblong;
        private CheckBox cbtemp;
        private DataGridViewTextBoxColumn Batch;
        private DataGridViewTextBoxColumn count;
        private CheckBox Check_ChongPei;
        private Panel panel2;
        private CheckBox Check_Long;
        private FlowLayoutPanel flowLayoutPanel2;
        private CheckBox checkBox1;
        private Label lblUnSend;
        private Label lblTotal;
        private Label label3;
        private Panel panel1;
        private DataGridViewCheckBoxColumn dgvselect;
        private DataGridViewTextBoxColumn WardName;
        private DataGridViewTextBoxColumn num1;
        private DataGridViewTextBoxColumn sum;
        private DataGridViewTextBoxColumn wardcode;
        private DataGridViewTextBoxColumn wardarea;
        private DataGridViewTextBoxColumn Column1;
        private Button btnGenerate;
        private Button btnSet;
        private Button btnGennerationBatch;
        private Button btnSend;
        private FlowLayoutPanel flowLayoutPanel3;
        private ComboBox cbbMode;
        private TextBox txtPatient;
        private Panel panel3;
        private Button button2;
        private CheckBox checkBox2;
        private Button btnSynDrugList;
        private Timer timer3;
        private FlowLayoutPanel flowLayoutPanel4;
        private CheckBox checkBox3;
        private CheckBox checkBox4;
        private CheckBox checkBox5;
        public FlowLayoutPanel Panel_BatchRule;
        private Label LCancelSend;
        private FlowLayoutPanel flowLayoutPanel6;
        private FlowLayoutPanel flowLayoutPanel5;
        private Button btnBatchModify;
        private CheckBox checkBox6;
        private CheckBox checkBox7;
    }
}
