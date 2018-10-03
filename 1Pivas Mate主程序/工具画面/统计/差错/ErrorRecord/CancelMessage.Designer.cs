namespace PivasLabelSelect
{
    partial class CancelMessage
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
            this.lb_name = new System.Windows.Forms.Label();
            this.lb_time = new System.Windows.Forms.Label();
            this.User = new System.Windows.Forms.Label();
            this.lb_Type = new System.Windows.Forms.Label();
            this.lb_Location = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lb_name
            // 
            this.lb_name.ForeColor = System.Drawing.Color.Purple;
            this.lb_name.Location = new System.Drawing.Point(1, 6);
            this.lb_name.Name = "lb_name";
            this.lb_name.Size = new System.Drawing.Size(81, 12);
            this.lb_name.TabIndex = 0;
            this.lb_name.Text = "lb_name";
            this.lb_name.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lb_time
            // 
            this.lb_time.AutoSize = true;
            this.lb_time.Location = new System.Drawing.Point(174, 6);
            this.lb_time.Name = "lb_time";
            this.lb_time.Size = new System.Drawing.Size(47, 12);
            this.lb_time.TabIndex = 1;
            this.lb_time.Text = "lb_time";
            // 
            // User
            // 
            this.User.AutoSize = true;
            this.User.Location = new System.Drawing.Point(82, 6);
            this.User.Name = "User";
            this.User.Size = new System.Drawing.Size(29, 12);
            this.User.TabIndex = 2;
            this.User.Text = "User";
            // 
            // lb_Type
            // 
            this.lb_Type.ForeColor = System.Drawing.Color.Gray;
            this.lb_Type.Location = new System.Drawing.Point(310, 6);
            this.lb_Type.Name = "lb_Type";
            this.lb_Type.Size = new System.Drawing.Size(41, 12);
            this.lb_Type.TabIndex = 3;
            this.lb_Type.Text = "lb_Type";
            this.lb_Type.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lb_Location
            // 
            this.lb_Location.ForeColor = System.Drawing.Color.Gray;
            this.lb_Location.Location = new System.Drawing.Point(357, 6);
            this.lb_Location.Name = "lb_Location";
            this.lb_Location.Size = new System.Drawing.Size(65, 12);
            this.lb_Location.TabIndex = 4;
            this.lb_Location.Text = "lb_Location";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(3, 6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(492, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Purple;
            this.label1.Location = new System.Drawing.Point(433, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "差错类型:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "label2";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "描述 :";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(65, 27);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(548, 21);
            this.textBox1.TabIndex = 10;
            // 
            // CancelMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.lb_Location);
            this.Controls.Add(this.lb_Type);
            this.Controls.Add(this.User);
            this.Controls.Add(this.lb_time);
            this.Controls.Add(this.lb_name);
            this.Name = "CancelMessage";
            this.Size = new System.Drawing.Size(619, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lb_name;
        public System.Windows.Forms.Label lb_time;
        public System.Windows.Forms.Label User;
        public System.Windows.Forms.Label lb_Type;
        public System.Windows.Forms.Label lb_Location;
        public System.Windows.Forms.CheckBox checkBox1;
        public System.Windows.Forms.ComboBox comboBox1;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox textBox1;
    }
}
