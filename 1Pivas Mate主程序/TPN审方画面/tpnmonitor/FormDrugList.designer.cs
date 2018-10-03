namespace tpnmonitor
{
    partial class frmDrugList
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
            this.components = new System.ComponentModel.Container();
            this.lvDrug = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnOK = new System.Windows.Forms.Button();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // lvDrug
            // 
            this.lvDrug.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvDrug.FullRowSelect = true;
            this.lvDrug.Location = new System.Drawing.Point(8, 37);
            this.lvDrug.MultiSelect = false;
            this.lvDrug.Name = "lvDrug";
            this.lvDrug.Size = new System.Drawing.Size(376, 342);
            this.lvDrug.SmallImageList = this.imageList1;
            this.lvDrug.TabIndex = 0;
            this.lvDrug.UseCompatibleStateImageBehavior = false;
            this.lvDrug.View = System.Windows.Forms.View.Details;
            this.lvDrug.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvDrug_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "药品名";
            this.columnHeader1.Width = 220;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "规格";
            this.columnHeader2.Width = 120;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(305, 385);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定(&Y)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(8, 8);
            this.txtKey.MaxLength = 50;
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(167, 21);
            this.txtKey.TabIndex = 2;
            this.txtKey.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtKey_KeyUp);
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(181, 8);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(46, 23);
            this.btnFind.TabIndex = 3;
            this.btnFind.Text = "查询";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(1, 22);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // frmDrugList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 416);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lvDrug);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDrugList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "药品列表";
            this.Load += new System.EventHandler(this.frmEmpList_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvDrug;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.ImageList imageList1;
    }
}