namespace PivasRevPre
{
    partial class BackPreByPerson
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
            this.cbbDrugA = new System.Windows.Forms.ComboBox();
            this.cbbDrugB = new System.Windows.Forms.ComboBox();
            this.cbbCensorItem = new System.Windows.Forms.ComboBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.pnlClose = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // cbbDrugA
            // 
            this.cbbDrugA.FormattingEnabled = true;
            this.cbbDrugA.Location = new System.Drawing.Point(101, 1);
            this.cbbDrugA.Name = "cbbDrugA";
            this.cbbDrugA.Size = new System.Drawing.Size(128, 20);
            this.cbbDrugA.TabIndex = 0;
            this.cbbDrugA.SelectedIndexChanged += new System.EventHandler(this.cbbDrugB_SelectedIndexChanged);
            // 
            // cbbDrugB
            // 
            this.cbbDrugB.FormattingEnabled = true;
            this.cbbDrugB.Location = new System.Drawing.Point(231, 1);
            this.cbbDrugB.Name = "cbbDrugB";
            this.cbbDrugB.Size = new System.Drawing.Size(128, 20);
            this.cbbDrugB.TabIndex = 1;
            this.cbbDrugB.SelectedIndexChanged += new System.EventHandler(this.cbbDrugB_SelectedIndexChanged);
            // 
            // cbbCensorItem
            // 
            this.cbbCensorItem.FormattingEnabled = true;
            this.cbbCensorItem.Location = new System.Drawing.Point(3, 1);
            this.cbbCensorItem.Name = "cbbCensorItem";
            this.cbbCensorItem.Size = new System.Drawing.Size(96, 20);
            this.cbbCensorItem.TabIndex = 2;
            this.cbbCensorItem.SelectedIndexChanged += new System.EventHandler(this.cbbCensorItem_SelectedIndexChanged);
            // 
            // txtDescription
            // 
            this.txtDescription.ForeColor = System.Drawing.Color.Gray;
            this.txtDescription.Location = new System.Drawing.Point(360, 1);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(395, 21);
            this.txtDescription.TabIndex = 3;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            this.txtDescription.Click += new System.EventHandler(this.txtDescription_Click);
            this.txtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescription_KeyDown);
            this.txtDescription.Leave += new System.EventHandler(this.txtDescription_Leave);
            this.txtDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescription_KeyPress);
            this.txtDescription.Enter += new System.EventHandler(this.txtDescription_Enter);
            // 
            // pnlClose
            // 
            this.pnlClose.BackgroundImage = global::PivasRevPre.Properties.Resources.delete_16;
            this.pnlClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlClose.Location = new System.Drawing.Point(758, 1);
            this.pnlClose.Name = "pnlClose";
            this.pnlClose.Size = new System.Drawing.Size(16, 20);
            this.pnlClose.TabIndex = 4;
            this.pnlClose.Click += new System.EventHandler(this.pnlClose_Click);
            // 
            // BackPreByPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlClose);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.cbbCensorItem);
            this.Controls.Add(this.cbbDrugB);
            this.Controls.Add(this.cbbDrugA);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "BackPreByPerson";
            this.Size = new System.Drawing.Size(779, 23);
            this.Load += new System.EventHandler(this.BackPreByPerson_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlClose;
        public System.Windows.Forms.ComboBox cbbDrugA;
        public System.Windows.Forms.ComboBox cbbDrugB;
        public System.Windows.Forms.ComboBox cbbCensorItem;
        public System.Windows.Forms.TextBox txtDescription;
    }
}
