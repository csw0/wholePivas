namespace tpnmonitor
{
    partial class frmCalDetail
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title5 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title6 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.lvDrugs = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblResult = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblExpress = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblItem = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chrtParam = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chrtDrug = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.nudDosage = new System.Windows.Forms.NumericUpDown();
            this.btnAddDrug = new System.Windows.Forms.Button();
            this.sptRight = new System.Windows.Forms.Splitter();
            this.lvTPN = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnDelDrug = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chrtParam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chrtDrug)).BeginInit();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDosage)).BeginInit();
            this.SuspendLayout();
            // 
            // lvDrugs
            // 
            this.lvDrugs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lvDrugs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDrugs.FullRowSelect = true;
            this.lvDrugs.GridLines = true;
            this.lvDrugs.HideSelection = false;
            this.lvDrugs.Location = new System.Drawing.Point(0, 62);
            this.lvDrugs.Name = "lvDrugs";
            this.lvDrugs.Size = new System.Drawing.Size(682, 283);
            this.lvDrugs.SmallImageList = this.imageList1;
            this.lvDrugs.TabIndex = 4;
            this.lvDrugs.UseCompatibleStateImageBehavior = false;
            this.lvDrugs.View = System.Windows.Forms.View.Details;
            this.lvDrugs.SelectedIndexChanged += new System.EventHandler(this.lvDrugs_SelectedIndexChanged);
            this.lvDrugs.DoubleClick += new System.EventHandler(this.lvDetail_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "药品";
            this.columnHeader1.Width = 160;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "规格";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "剂量";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader3.Width = 70;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "数量";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader4.Width = 50;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "容积";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(1, 20);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblResult);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblExpress);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblItem);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(682, 62);
            this.panel1.TabIndex = 7;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(66, 33);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(29, 12);
            this.lblResult.TabIndex = 12;
            this.lblResult.Text = "结果";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "计算结果:";
            // 
            // lblExpress
            // 
            this.lblExpress.Location = new System.Drawing.Point(284, 9);
            this.lblExpress.Name = "lblExpress";
            this.lblExpress.Size = new System.Drawing.Size(392, 50);
            this.lblExpress.TabIndex = 10;
            this.lblExpress.Text = "累计";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(222, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "计算方式:";
            // 
            // lblItem
            // 
            this.lblItem.AutoSize = true;
            this.lblItem.Location = new System.Drawing.Point(66, 9);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(41, 12);
            this.lblItem.TabIndex = 8;
            this.lblItem.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "计算项目:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chrtParam);
            this.panel2.Controls.Add(this.chrtDrug);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 345);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(682, 204);
            this.panel2.TabIndex = 8;
            // 
            // chrtParam
            // 
            chartArea5.Name = "ChartArea1";
            this.chrtParam.ChartAreas.Add(chartArea5);
            legend5.Enabled = false;
            legend5.Name = "Legend1";
            this.chrtParam.Legends.Add(legend5);
            this.chrtParam.Location = new System.Drawing.Point(468, 6);
            this.chrtParam.Name = "chrtParam";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.chrtParam.Series.Add(series5);
            this.chrtParam.Size = new System.Drawing.Size(197, 195);
            this.chrtParam.TabIndex = 2;
            this.chrtParam.Text = "chart2";
            title5.Name = "Title1";
            title5.Text = "参数贡献率";
            this.chrtParam.Titles.Add(title5);
            // 
            // chrtDrug
            // 
            chartArea6.Name = "ChartArea1";
            this.chrtDrug.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chrtDrug.Legends.Add(legend6);
            this.chrtDrug.Location = new System.Drawing.Point(0, 6);
            this.chrtDrug.Name = "chrtDrug";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.chrtDrug.Series.Add(series6);
            this.chrtDrug.Size = new System.Drawing.Size(451, 195);
            this.chrtDrug.TabIndex = 1;
            this.chrtDrug.Text = "chart1";
            title6.Name = "Title1";
            title6.Text = "药品贡献率";
            this.chrtDrug.Titles.Add(title6);
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.btnDelDrug);
            this.pnlBody.Controls.Add(this.nudDosage);
            this.pnlBody.Controls.Add(this.btnAddDrug);
            this.pnlBody.Controls.Add(this.lvDrugs);
            this.pnlBody.Controls.Add(this.panel1);
            this.pnlBody.Controls.Add(this.panel2);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(6, 6);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(682, 549);
            this.pnlBody.TabIndex = 9;
            // 
            // nudDosage
            // 
            this.nudDosage.Location = new System.Drawing.Point(264, 123);
            this.nudDosage.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudDosage.Name = "nudDosage";
            this.nudDosage.Size = new System.Drawing.Size(83, 21);
            this.nudDosage.TabIndex = 10;
            this.nudDosage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudDosage.Visible = false;
            this.nudDosage.ValueChanged += new System.EventHandler(this.nudDosage_ValueChanged);
            // 
            // btnAddDrug
            // 
            this.btnAddDrug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddDrug.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddDrug.Location = new System.Drawing.Point(620, 315);
            this.btnAddDrug.Name = "btnAddDrug";
            this.btnAddDrug.Size = new System.Drawing.Size(28, 24);
            this.btnAddDrug.TabIndex = 9;
            this.btnAddDrug.Text = "+";
            this.toolTip1.SetToolTip(this.btnAddDrug, "增加药品");
            this.btnAddDrug.UseVisualStyleBackColor = true;
            this.btnAddDrug.Click += new System.EventHandler(this.btnAddDrug_Click);
            // 
            // sptRight
            // 
            this.sptRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.sptRight.Location = new System.Drawing.Point(688, 6);
            this.sptRight.Name = "sptRight";
            this.sptRight.Size = new System.Drawing.Size(4, 549);
            this.sptRight.TabIndex = 11;
            this.sptRight.TabStop = false;
            // 
            // lvTPN
            // 
            this.lvTPN.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.lvTPN.Dock = System.Windows.Forms.DockStyle.Right;
            this.lvTPN.FullRowSelect = true;
            this.lvTPN.GridLines = true;
            this.lvTPN.Location = new System.Drawing.Point(692, 6);
            this.lvTPN.Name = "lvTPN";
            this.lvTPN.Size = new System.Drawing.Size(286, 549);
            this.lvTPN.SmallImageList = this.imageList1;
            this.lvTPN.TabIndex = 12;
            this.lvTPN.UseCompatibleStateImageBehavior = false;
            this.lvTPN.View = System.Windows.Forms.View.Details;
            this.lvTPN.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvTPN_MouseDoubleClick);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "TPN参数";
            this.columnHeader6.Width = 90;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "值";
            this.columnHeader7.Width = 70;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "正常值";
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "审核";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader9.Width = 40;
            // 
            // btnDelDrug
            // 
            this.btnDelDrug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelDrug.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDelDrug.Location = new System.Drawing.Point(651, 315);
            this.btnDelDrug.Name = "btnDelDrug";
            this.btnDelDrug.Size = new System.Drawing.Size(28, 24);
            this.btnDelDrug.TabIndex = 11;
            this.btnDelDrug.Text = "-";
            this.toolTip1.SetToolTip(this.btnDelDrug, "删除所选药品");
            this.btnDelDrug.UseVisualStyleBackColor = true;
            this.btnDelDrug.Click += new System.EventHandler(this.btnDelDrug_Click);
            // 
            // frmCalDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.sptRight);
            this.Controls.Add(this.lvTPN);
            this.MinimizeBox = false;
            this.Name = "frmCalDetail";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "计算明细";
            this.Load += new System.EventHandler(this.frmCalDetail_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chrtParam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chrtDrug)).EndInit();
            this.pnlBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudDosage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvDrugs;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblExpress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chrtDrug;
        private System.Windows.Forms.DataVisualization.Charting.Chart chrtParam;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Splitter sptRight;
        private System.Windows.Forms.ListView lvTPN;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.Button btnAddDrug;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.NumericUpDown nudDosage;
        private System.Windows.Forms.Button btnDelDrug;
    }
}