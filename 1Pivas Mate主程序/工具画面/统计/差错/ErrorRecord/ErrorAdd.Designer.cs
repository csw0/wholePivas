namespace ErrorRecord
{
    partial class ErrorAdd
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
            this.labelFind = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.comboFind = new System.Windows.Forms.ComboBox();
            this.labelErrTime = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.richTextDescribe = new System.Windows.Forms.RichTextBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.labelDescribe = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboType = new System.Windows.Forms.ComboBox();
            this.labelErrirID = new System.Windows.Forms.Label();
            this.labelType = new System.Windows.Forms.Label();
            this.textErrorName = new System.Windows.Forms.TextBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboStatus = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelFind
            // 
            this.labelFind.AutoSize = true;
            this.labelFind.Location = new System.Drawing.Point(27, 345);
            this.labelFind.Name = "labelFind";
            this.labelFind.Size = new System.Drawing.Size(89, 12);
            this.labelFind.TabIndex = 10;
            this.labelFind.Text = "发现差错环节：";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(95, 175);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(128, 21);
            this.dateTimePicker1.TabIndex = 5;
            // 
            // comboFind
            // 
            this.comboFind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFind.FormattingEnabled = true;
            this.comboFind.Location = new System.Drawing.Point(119, 342);
            this.comboFind.Name = "comboFind";
            this.comboFind.Size = new System.Drawing.Size(121, 20);
            this.comboFind.TabIndex = 7;
            // 
            // labelErrTime
            // 
            this.labelErrTime.AutoSize = true;
            this.labelErrTime.Location = new System.Drawing.Point(27, 181);
            this.labelErrTime.Name = "labelErrTime";
            this.labelErrTime.Size = new System.Drawing.Size(65, 12);
            this.labelErrTime.TabIndex = 8;
            this.labelErrTime.Text = "差错时间：";
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSave.Location = new System.Drawing.Point(91, 391);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "保  存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // richTextDescribe
            // 
            this.richTextDescribe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextDescribe.Location = new System.Drawing.Point(29, 232);
            this.richTextDescribe.Name = "richTextDescribe";
            this.richTextDescribe.Size = new System.Drawing.Size(395, 96);
            this.richTextDescribe.TabIndex = 6;
            this.richTextDescribe.Text = "";
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClear.Location = new System.Drawing.Point(276, 391);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 9;
            this.buttonClear.Text = "取  消";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // labelDescribe
            // 
            this.labelDescribe.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDescribe.AutoSize = true;
            this.labelDescribe.Location = new System.Drawing.Point(27, 214);
            this.labelDescribe.Name = "labelDescribe";
            this.labelDescribe.Size = new System.Drawing.Size(65, 12);
            this.labelDescribe.TabIndex = 6;
            this.labelDescribe.Text = "差错描述：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(334, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "label2";
            this.label2.Visible = false;
            // 
            // comboType
            // 
            this.comboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboType.FormattingEnabled = true;
            this.comboType.Location = new System.Drawing.Point(289, 105);
            this.comboType.Name = "comboType";
            this.comboType.Size = new System.Drawing.Size(120, 20);
            this.comboType.TabIndex = 2;
            this.comboType.SelectedIndexChanged += new System.EventHandler(this.comboType_SelectedIndexChanged);
            // 
            // labelErrirID
            // 
            this.labelErrirID.AutoSize = true;
            this.labelErrirID.Location = new System.Drawing.Point(27, 143);
            this.labelErrirID.Name = "labelErrirID";
            this.labelErrirID.Size = new System.Drawing.Size(53, 12);
            this.labelErrirID.TabIndex = 15;
            this.labelErrirID.Text = "差错人：";
            // 
            // labelType
            // 
            this.labelType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(221, 108);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(65, 12);
            this.labelType.TabIndex = 4;
            this.labelType.Text = "差错类别：";
            // 
            // textErrorName
            // 
            this.textErrorName.Location = new System.Drawing.Point(95, 140);
            this.textErrorName.Name = "textErrorName";
            this.textErrorName.Size = new System.Drawing.Size(100, 21);
            this.textErrorName.TabIndex = 3;
            this.textErrorName.TextChanged += new System.EventHandler(this.textErrorName_TextChanged);
            this.textErrorName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textErrorName_KeyDown);
            this.textErrorName.Leave += new System.EventHandler(this.textErrorName_Leave);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(27, 108);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(65, 12);
            this.labelStatus.TabIndex = 3;
            this.labelStatus.Text = "差错环节：";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(95, 162);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(116, 76);
            this.listBox1.TabIndex = 4;
            this.listBox1.Visible = false;
            this.listBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBox1_KeyUp);
            this.listBox1.SelectedValueChanged += new System.EventHandler(this.listBox1_SelectedValueChanged);
            this.listBox1.Click += new System.EventHandler(this.listBox1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(125, 69);
            this.textBox1.MaxLength = 19;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(170, 21);
            this.textBox1.TabIndex = 0;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // comboStatus
            // 
            this.comboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboStatus.FormattingEnabled = true;
            this.comboStatus.Location = new System.Drawing.Point(95, 105);
            this.comboStatus.Name = "comboStatus";
            this.comboStatus.Size = new System.Drawing.Size(120, 20);
            this.comboStatus.TabIndex = 1;
            this.comboStatus.SelectedIndexChanged += new System.EventHandler(this.comboStatus_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "瓶签号/医嘱号：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.DarkGreen;
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 18);
            this.label3.TabIndex = 18;
            this.label3.Text = "记录添加：";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.listBox1);
            this.splitContainer1.Panel1.Controls.Add(this.textErrorName);
            this.splitContainer1.Panel1.Controls.Add(this.labelErrirID);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.buttonClear);
            this.splitContainer1.Panel1.Controls.Add(this.buttonSave);
            this.splitContainer1.Panel1.Controls.Add(this.comboFind);
            this.splitContainer1.Panel1.Controls.Add(this.labelFind);
            this.splitContainer1.Panel1.Controls.Add(this.dateTimePicker1);
            this.splitContainer1.Panel1.Controls.Add(this.labelErrTime);
            this.splitContainer1.Panel1.Controls.Add(this.richTextDescribe);
            this.splitContainer1.Panel1.Controls.Add(this.labelDescribe);
            this.splitContainer1.Panel1.Controls.Add(this.comboType);
            this.splitContainer1.Panel1.Controls.Add(this.labelType);
            this.splitContainer1.Panel1.Controls.Add(this.labelStatus);
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            this.splitContainer1.Panel1.Controls.Add(this.comboStatus);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Size = new System.Drawing.Size(834, 435);
            this.splitContainer1.SplitterDistance = 452;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 3;
            // 
            // ErrorAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ErrorAdd";
            this.Size = new System.Drawing.Size(834, 435);
            this.Load += new System.EventHandler(this.ErrorAdd_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboStatus;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.TextBox textErrorName;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.Label labelErrirID;
        private System.Windows.Forms.ComboBox comboType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelDescribe;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.RichTextBox richTextDescribe;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label labelErrTime;
        private System.Windows.Forms.ComboBox comboFind;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label labelFind;
        protected internal System.Windows.Forms.SplitContainer splitContainer1;

    }
}
