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
            this.SuspendLayout();
            // 
            // lb_name
            // 
            this.lb_name.ForeColor = System.Drawing.Color.Purple;
            this.lb_name.Location = new System.Drawing.Point(-15, 6);
            this.lb_name.Name = "lb_name";
            this.lb_name.Size = new System.Drawing.Size(81, 12);
            this.lb_name.TabIndex = 0;
            this.lb_name.Text = "lb_name";
            this.lb_name.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lb_time
            // 
            this.lb_time.AutoSize = true;
            this.lb_time.Location = new System.Drawing.Point(165, 6);
            this.lb_time.Name = "lb_time";
            this.lb_time.Size = new System.Drawing.Size(47, 12);
            this.lb_time.TabIndex = 1;
            this.lb_time.Text = "lb_time";
            // 
            // User
            // 
            this.User.AutoSize = true;
            this.User.Location = new System.Drawing.Point(75, 6);
            this.User.Name = "User";
            this.User.Size = new System.Drawing.Size(29, 12);
            this.User.TabIndex = 2;
            this.User.Text = "User";
            // 
            // lb_Type
            // 
            this.lb_Type.ForeColor = System.Drawing.Color.Gray;
            this.lb_Type.Location = new System.Drawing.Point(308, 6);
            this.lb_Type.Name = "lb_Type";
            this.lb_Type.Size = new System.Drawing.Size(41, 12);
            this.lb_Type.TabIndex = 3;
            this.lb_Type.Text = "lb_Type";
            this.lb_Type.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lb_Location
            // 
            this.lb_Location.ForeColor = System.Drawing.Color.Gray;
            this.lb_Location.Location = new System.Drawing.Point(371, 6);
            this.lb_Location.Name = "lb_Location";
            this.lb_Location.Size = new System.Drawing.Size(65, 12);
            this.lb_Location.TabIndex = 4;
            this.lb_Location.Text = "lb_Location";
            // 
            // CancelMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lb_Location);
            this.Controls.Add(this.lb_Type);
            this.Controls.Add(this.User);
            this.Controls.Add(this.lb_time);
            this.Controls.Add(this.lb_name);
            this.Name = "CancelMessage";
            this.Size = new System.Drawing.Size(534, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_name;
        private System.Windows.Forms.Label lb_time;
        private System.Windows.Forms.Label User;
        private System.Windows.Forms.Label lb_Type;
        private System.Windows.Forms.Label lb_Location;
    }
}
