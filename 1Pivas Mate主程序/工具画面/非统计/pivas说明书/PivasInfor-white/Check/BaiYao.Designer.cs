namespace PivasInfor.Check
{
    partial class BaiYao
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.richTextBox5 = new System.Windows.Forms.RichTextBox();
            this.richTextBox4 = new System.Windows.Forms.RichTextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 21);
            this.label1.TabIndex = 41;
            this.label1.Text = "摆药核对";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.richTextBox5);
            this.panel1.Controls.Add(this.richTextBox4);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.richTextBox3);
            this.panel1.Location = new System.Drawing.Point(6, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(747, 794);
            this.panel1.TabIndex = 42;
            // 
            // richTextBox5
            // 
            this.richTextBox5.BackColor = System.Drawing.Color.White;
            this.richTextBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox5.Location = new System.Drawing.Point(36, 734);
            this.richTextBox5.Name = "richTextBox5";
            this.richTextBox5.ReadOnly = true;
            this.richTextBox5.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox5.Size = new System.Drawing.Size(686, 46);
            this.richTextBox5.TabIndex = 67;
            this.richTextBox5.Text = "4, 点击开始扫描，可以在对话框中输入要扫描的瓶签号，也可以利用扫描仪器扫描瓶签上的瓶签号。\n";
            // 
            // richTextBox4
            // 
            this.richTextBox4.BackColor = System.Drawing.Color.White;
            this.richTextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox4.Location = new System.Drawing.Point(28, 609);
            this.richTextBox4.Name = "richTextBox4";
            this.richTextBox4.ReadOnly = true;
            this.richTextBox4.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox4.Size = new System.Drawing.Size(700, 123);
            this.richTextBox4.TabIndex = 66;
            this.richTextBox4.Text = " 3, 界面说明：\n   (1)日期：选择要摆药核对瓶签的日期。\n   (2)批次：在摆药核对时可根据批次不同，来进行扫描。包括：全部批次，临时，长期1#，长期2" +
                "#，长期3#，长期4#。\n   (3)筛选：在前面批次选好之后，再根据以下条件扫描，包括：#冲配，L临时，ST一次用药，K单个药不冲配，U多个药不冲配，K/U不" +
                "冲配的药。\n\n\n\n\n";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::PivasInfor.Properties.Resources._54;
            this.pictureBox2.Location = new System.Drawing.Point(19, 73);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(724, 533);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 65;
            this.pictureBox2.TabStop = false;
            // 
            // richTextBox3
            // 
            this.richTextBox3.BackColor = System.Drawing.Color.White;
            this.richTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox3.Location = new System.Drawing.Point(18, 1);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.ReadOnly = true;
            this.richTextBox3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox3.Size = new System.Drawing.Size(688, 69);
            this.richTextBox3.TabIndex = 64;
            this.richTextBox3.Text = "1, 显示和查看瓶签是否已经摆药核对，并可以查看病人用药信息。\n2, 摆药核对浏览:在主界面功能列表点击PIVAS中心工作站-> 核对-> 摆药核对，打开摆药核对" +
                "界面，如下：\n\n\n";
            // 
            // BaiYao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "BaiYao";
            this.Size = new System.Drawing.Size(756, 1807);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox richTextBox5;
        private System.Windows.Forms.RichTextBox richTextBox4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.RichTextBox richTextBox3;
    }
}
