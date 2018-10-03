namespace PivasLabelSelect
{
    partial class HISJF
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.StartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndDt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SendText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Chargreturn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HISReturn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.msg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remark1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remark2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remark3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remark4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StartTime,
            this.EndDt,
            this.SendText,
            this.Chargreturn,
            this.HISReturn,
            this.msg,
            this.Remark1,
            this.Remark2,
            this.Remark3,
            this.Remark4});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(648, 231);
            this.dataGridView1.TabIndex = 0;
            // 
            // StartTime
            // 
            this.StartTime.DataPropertyName = "BeginTime";
            this.StartTime.HeaderText = "开始时间";
            this.StartTime.Name = "StartTime";
            this.StartTime.ReadOnly = true;
            // 
            // EndDt
            // 
            this.EndDt.DataPropertyName = "EndTime";
            this.EndDt.HeaderText = "结束时间";
            this.EndDt.Name = "EndDt";
            this.EndDt.ReadOnly = true;
            // 
            // SendText
            // 
            this.SendText.DataPropertyName = "Parameters";
            this.SendText.HeaderText = "发送的字段 ";
            this.SendText.Name = "SendText";
            this.SendText.ReadOnly = true;
            this.SendText.Width = 200;
            // 
            // Chargreturn
            // 
            this.Chargreturn.DataPropertyName = "ChargeResult";
            this.Chargreturn.HeaderText = "计费结果";
            this.Chargreturn.Name = "Chargreturn";
            this.Chargreturn.ReadOnly = true;
            // 
            // HISReturn
            // 
            this.HISReturn.DataPropertyName = "HisReturn";
            this.HISReturn.HeaderText = "HIS返回值";
            this.HISReturn.Name = "HISReturn";
            this.HISReturn.ReadOnly = true;
            // 
            // msg
            // 
            this.msg.DataPropertyName = "Msg";
            this.msg.HeaderText = "HIS返回信息";
            this.msg.Name = "msg";
            this.msg.ReadOnly = true;
            this.msg.Width = 200;
            // 
            // Remark1
            // 
            this.Remark1.DataPropertyName = "Remark1";
            this.Remark1.HeaderText = "备注1";
            this.Remark1.Name = "Remark1";
            this.Remark1.ReadOnly = true;
            // 
            // Remark2
            // 
            this.Remark2.DataPropertyName = "Remark2";
            this.Remark2.HeaderText = "备注2";
            this.Remark2.Name = "Remark2";
            this.Remark2.ReadOnly = true;
            // 
            // Remark3
            // 
            this.Remark3.DataPropertyName = "Remark3";
            this.Remark3.HeaderText = "备注3";
            this.Remark3.Name = "Remark3";
            this.Remark3.ReadOnly = true;
            // 
            // Remark4
            // 
            this.Remark4.DataPropertyName = "Remark4";
            this.Remark4.HeaderText = "备注4";
            this.Remark4.Name = "Remark4";
            this.Remark4.ReadOnly = true;
            // 
            // HISJF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 231);
            this.Controls.Add(this.dataGridView1);
            this.Name = "HISJF";
            this.Opacity = 0.9;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "扫描计费结果查询";
            this.Load += new System.EventHandler(this.HISJF_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndDt;
        private System.Windows.Forms.DataGridViewTextBoxColumn SendText;
        private System.Windows.Forms.DataGridViewTextBoxColumn Chargreturn;
        private System.Windows.Forms.DataGridViewTextBoxColumn HISReturn;
        private System.Windows.Forms.DataGridViewTextBoxColumn msg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark4;
    }
}