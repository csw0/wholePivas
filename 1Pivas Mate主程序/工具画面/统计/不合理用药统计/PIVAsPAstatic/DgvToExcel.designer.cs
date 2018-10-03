namespace PIVAsPAstatic
{
    partial class DgvToExcel
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
            this.Dgv_Excel = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Excel)).BeginInit();
            this.SuspendLayout();
            // 
            // Dgv_Excel
            // 
            this.Dgv_Excel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_Excel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Dgv_Excel.Location = new System.Drawing.Point(0, 58);
            this.Dgv_Excel.Name = "Dgv_Excel";
            this.Dgv_Excel.ReadOnly = true;
            this.Dgv_Excel.RowTemplate.Height = 23;
            this.Dgv_Excel.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_Excel.Size = new System.Drawing.Size(574, 300);
            this.Dgv_Excel.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(574, 52);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // DgvToExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 358);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Dgv_Excel);
            this.Name = "DgvToExcel";
            this.Text = "生成Excel";
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Excel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView Dgv_Excel;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}