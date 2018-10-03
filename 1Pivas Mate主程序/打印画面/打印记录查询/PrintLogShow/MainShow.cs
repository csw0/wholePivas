using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FastReport;
using PIVAsCommon.Helper;

namespace PrintLogShow
{
    public partial class MainShow : UserControl
    {
        private DB_Help DB;
        private DataTable dt;
        private bool show;
        private bool isrun;
        private Report report = new Report();
        private string wardname = string.Empty;
        protected internal int selecpint = 0;
        protected internal bool cancon;
        protected internal string useid;
        private string psn = string.Empty;
        protected internal bool printed;


        public MainShow()
        {
            DB = new DB_Help();
            dt = new DataTable();
            dt.Columns.Add("Time", typeof(string));
            dt.Columns.Add("PrinterID", typeof(string));
            dt.Columns.Add("PrintCount", typeof(int));
            dt.Columns.Add("Oldds", typeof(string));
            dt.Columns.Add("DrugQRCode", typeof(string));
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now.Date;
            comboBox2.SelectedIndex = 0;
        }

        private void MainShow_Load(object sender, EventArgs e)
        {
            try
            {
                button1_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                panel5.Location = new Point(220, this.Height);
                panel3.Height = panel2.Height;
                panel4.Height = panel2.Height;
                using (DataSet ds = DB.GetPIVAsDB(string.Format(getSQL("PrintNO"), dateTimePicker1.Value.ToString("yyyy-MM-dd"), comboBox1.SelectedIndex == 0 ? string.Empty : string.Format(" and WardName='{0}'", comboBox1.SelectedItem.ToString()), comboBox2.SelectedIndex == 0 ? string.Empty : " and NOT EXISTS (select 'X' from [dbo].[V_IVPrintLog] V1 where V1.PrintNO=VL.PrintNO AND Batch not like ('%K')) ")))
                {
                    if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    {
                        if (dataGridView1.DataSource != null)
                        {
                            ((DataTable)dataGridView1.DataSource).Clear();
                            ((DataTable)dataGridView2.DataSource).Clear();
                            ((DataTable)dataGridView3.DataSource).Clear();
                            //((DataTable)dataGridView4.DataSource).Clear();
                            ((DataTable)dataGridView5.DataSource).Clear();
                        }

                        MessageBox.Show("当天没有打印数据");
                    }
                    else
                    {
                        string[] str;
                        dt.Clear();
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            str = dr["PrintNO"].ToString().Split('-');
                            dt.Rows.Add(str[3] + ":" + str[4] + ":" + str[5], dr["DEmployeeName"], dr["PrintCount"], dr["PrintNO"], dr["DrugQRCode"]);
                        }
                        dataGridView1.DataSource = dt;
                        dataGridView1_CellClick(sender, new DataGridViewCellEventArgs(0, 0));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private static string getSQL(string MOD)
        {
            StringBuilder sql = new StringBuilder();
            switch (MOD)
            {
                case "PrintNO":
                    sql.Append("SELECT [PrintNO],[DEmployeeName],COUNT(LabelNo) PrintCount,DrugQRCode from [dbo].[V_IVPrintLog] VL WHERE DATEDIFF(DAY,PrintDT,'{0}')=0 {1} {2} group by PrintNO,DEmployeeName,DrugQRCode order by DrugQRCode");
                    break;
                case "LabelNoS":
                    sql.Append("SELECT [LabelNo],[PrintDT],([PrintCount]+1) PrintCount FROM [dbo].[IVRecord_Print] where PrintNO='{0}' order by ID");
                    break;
                case "Drugs":
                    sql.Append("SELECT [Batch],[DrugName],[Spec],sum([DosageSum]) DosageSum FROM [V_IVRecordandDetailS] v,[IVRecord_Print] p where v.LabelNo=p.LabelNo and IsMenstruum=0 and PrintNO='{0}' group by [Batch],[DrugCode],[DrugName],[Spec] order by [Batch],[DrugCode],DosageSum");
                    break;
                case "Drugss":
                    sql.Append("SELECT [Batch],[DrugName],[Spec],sum([DosageSum]) DosageSum FROM [V_IVRecordandDetailS] v,[IVRecord_Print] p where v.LabelNo=p.LabelNo and IsMenstruum=1 and PrintNO='{0}' group by [Batch],[DrugCode],[DrugName],[Spec] order by [Batch],[DrugCode],DosageSum");
                    break;
                case "Drug":
                    sql.Append("SELECT [DrugName],[Spec],[DosageUnit],[DosageSum],[Dosage] FROM [V_IVRecordandDetailS] where LabelNo='{0}'");
                    break;
            }
            return sql.ToString();
        }

        private void panel5_SizeChanged(object sender, EventArgs e)
        {
            dataGridView4.Columns[0].Width = dataGridView4.Columns[1].Width = (panel5.Width - 100) / 2;
        }

        private void panel4_SizeChanged(object sender, EventArgs e)
        {
            //dataGridView3.Columns[0].Width = panel4.Width - 60;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataSet ds = DB.GetPIVAsDB(string.Format(getSQL("LabelNoS"), dataGridView1.SelectedRows[0].Cells["Oldds"].Value.ToString()));
                dataGridView2.DataSource = ds.Tables[0];
                foreach (DataGridViewRow dgvr in dataGridView2.Rows)
                {
                    dgvr.Cells["Indexs"].Value = dgvr.Index + 1;
                }
                dataGridView2.Columns["PrintDT"].DefaultCellStyle.Format = "HH:mm:ss.ff";
                ds = DB.GetPIVAsDB(string.Format(getSQL("Drugs"), dataGridView1.SelectedRows[0].Cells["Oldds"].Value.ToString()));
                dataGridView3.DataSource = ds.Tables[0];
                ds = DB.GetPIVAsDB(string.Format(getSQL("Drugss"), dataGridView1.SelectedRows[0].Cells["Oldds"].Value.ToString()));
                dataGridView5.DataSource = ds.Tables[0];
                ds = DB.GetPIVAsDB(string.Format("select distinct WardName from [dbo].[V_IVPrintLog] where PrintNO ='{0}'", dataGridView1.SelectedRows[0].Cells["Oldds"].Value.ToString()));
                wardname = string.Empty;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    wardname = wardname + dr[0].ToString() + " ";
                }
                ds.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataSet ds = DB.GetPIVAsDB(string.Format(getSQL("Drug"), dataGridView2.SelectedRows[0].Cells["LabelNo"].Value.ToString()));
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dr["DosageUnit"] = dr["Dosage"].ToString().TrimEnd('0').TrimEnd('.') + dr["DosageUnit"];
                }
                dataGridView4.DataSource = ds.Tables[0];
                show = true;
                if (show)
                {
                    panel5.Location = new Point(220, (this.Height - panel5.Height));
                    panel5.Refresh();
                    panel3.Height = panel2.Height - panel5.Height;
                    panel4.Height = panel3.Height;
                    panel3.Refresh();
                    panel5.Refresh();
                }
                ds.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView2_Leave(object sender, EventArgs e)
        {
            show = false;
            if (!show)
            {
                panel3.Height = panel2.Height;
                panel4.Height = panel2.Height;
                panel3.Refresh();
                panel5.Refresh();
                panel5.Location = new Point(220, this.Height);
                panel5.Refresh();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.FindForm().Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new SelecPrint(this).ShowDialog();
            if (!cancon)
                return;
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    string DrugQRCode = dataGridView1.SelectedRows[0].Cells["DrugQRCode"].Value.ToString();
                    DataSet ds = null;
                    DataSet dds = DB.GetPIVAsDB(string.Format("select Batch from dbo.V_IVRecordandDetailS where LabelNo in(select LabelNo from [IVRecord_Print]  where DrugQRCode='{0}' ) and IsMenstruum=1 group by Batch order by Batch", DrugQRCode));
                    //string dgs = string.Empty;
                    report.Preview = previewControl1;
                    if (selecpint == 1)
                    {
                        report.Load(".\\Crystal\\reportStatlog.frx");
                    }
                    else
                    {
                        report.Load(".\\Crystal\\reportStat.frx");
                    }
                    foreach (DataRow dr in dds.Tables[0].Rows)
                    {
                        ds = DB.GetPIVAsDB(string.Format("select DrugCode as '药品编码',[DrugName] as '药品名称',[Spec] as '药品规格',sum(DgNo) as '数量' from dbo.V_IVRecordandDetailS where LabelNo in(select LabelNo from [IVRecord_Print] where DrugQRCode='{0}') and IsMenstruum=1 and Batch='{1}' group by DrugCode,[DrugName],[Spec]", DrugQRCode, dr[0].ToString()));
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            ds.Tables[0].TableName = "MedStat";
                            report.RegisterData(ds, "ds");
                            (report.FindObject("Text1") as FastReport.TextObject).Text = "溶媒";
                            if (selecpint == 1)
                            {
                                (report.FindObject("Text6") as FastReport.TextObject).Text = dateTimePicker1.Value.ToString("yyyy-MM-dd") + " " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                            }
                            (report.FindObject("Text14") as FastReport.TextObject).Text = DrugQRCode.Substring(7, 10);
                            (report.FindObject("Text13") as FastReport.TextObject).Text = wardname;
                            (report.FindObject("Text12") as FastReport.TextObject).Text = dr[0].ToString();
                            (report.FindObject("Barcode1") as FastReport.Barcode.BarcodeObject).Text = DrugQRCode;
                            (report.FindObject("Data1") as FastReport.DataBand).DataSource = report.GetDataSource("MedStat");
                            report.Show();
                            if (string.IsNullOrEmpty(psn))
                            {
                                report.PrintSettings.ShowDialog = true;
                            }
                            else
                            {
                                report.PrintSettings.Printer = psn;
                            }
                            previewControl1.Print();
                            if (report.PrintSettings.ShowDialog)
                            {
                                report.PrintSettings.ShowDialog = false;
                                psn = report.PrintSettings.Printer;
                            }
                            //dgs = dgs + dr[0].ToString() + ",";
                        }
                    }
                    ds = DB.GetPIVAsDB(string.Format("select DrugCode as '药品编码',[DrugName] as '药品名称',[Spec] as '药品规格',sum(DgNo) as '数量' from dbo.V_IVRecordandDetailS where LabelNo in(select LabelNo from [IVRecord_Print] where DrugQRCode='{0}') and IsMenstruum=0 group by DrugCode,[DrugName],[Spec]", DrugQRCode));
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ds.Tables[0].TableName = "MedStat";
                        report.RegisterData(ds, "ds");
                        dds = DB.GetPIVAsDB(string.Format("select Batch from dbo.V_IVRecordandDetailS where LabelNo in(select LabelNo from [IVRecord_Print]  where DrugQRCode='{0}' ) and IsMenstruum=0 group by Batch order by Batch", DrugQRCode));
                        string dgs = string.Empty;
                        foreach (DataRow dr in dds.Tables[0].Rows)
                        {
                            dgs = dgs + dr[0].ToString() + ",";
                        }
                        (report.FindObject("Text1") as FastReport.TextObject).Text = "药品";
                        if (selecpint == 1)
                        {
                            (report.FindObject("Text6") as FastReport.TextObject).Text = dateTimePicker1.Value.ToString("yyyy-MM-dd") + " " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                        }
                        (report.FindObject("Text14") as FastReport.TextObject).Text = DrugQRCode.Substring(7, 10);
                        (report.FindObject("Text13") as FastReport.TextObject).Text = wardname;
                        (report.FindObject("Text12") as FastReport.TextObject).Text = dgs.TrimEnd(',');
                        (report.FindObject("Barcode1") as FastReport.Barcode.BarcodeObject).Text = DrugQRCode;
                        (report.FindObject("Data1") as FastReport.DataBand).DataSource = report.GetDataSource("MedStat");
                        report.Show();
                        report.PrintSettings.Printer = psn;
                        previewControl1.Print();
                        dds.Dispose();
                    }
                    if (selecpint == 0)
                    {
                        DrugQRCode = DrugQRCode.Replace("8888", "9999");
                        StringBuilder sb = new StringBuilder();
                        sb.Append(" select OrderID as TeamNumber,");
                        sb.Append("(select COUNT(distinct p.LabelNo) from dbo.IVRecord_Print p inner join dbo.IVRecord  iv on p.LabelNo=iv.LabelNo and iv.TeamNumber=ivd.OrderID and OrderQRCode='{0}') con,");
                        sb.Append("(select COUNT(distinct p.LabelNo) from dbo.IVRecord_Print p inner join dbo.IVRecord  iv on p.LabelNo=iv.LabelNo and iv.TeamNumber=ivd.OrderID and OrderQRCode='{0}' and iv.Batch like ('%#')) conC,");
                        sb.Append("(select COUNT(distinct p.LabelNo) from dbo.IVRecord_Print p inner join dbo.IVRecord  iv on p.LabelNo=iv.LabelNo and iv.TeamNumber=ivd.OrderID and OrderQRCode='{0}' and iv.Batch like ('%K')) conK,");
                        sb.Append("(select COUNT(distinct p.LabelNo) from dbo.IVRecord_Print p inner join dbo.IVRecord  iv on p.LabelNo=iv.LabelNo and iv.TeamNumber=ivd.OrderID and OrderQRCode='{0}' and iv.Batch not like ('L%')) conNL,");
                        sb.Append("(select COUNT(distinct p.LabelNo) from dbo.IVRecord_Print p inner join dbo.IVRecord  iv on p.LabelNo=iv.LabelNo and iv.TeamNumber=ivd.OrderID and OrderQRCode='{0}' and iv.Batch not like ('L%') and iv.Batch like ('%#')) conNLC,");
                        sb.Append("(select COUNT(distinct p.LabelNo) from dbo.IVRecord_Print p inner join dbo.IVRecord  iv on p.LabelNo=iv.LabelNo and iv.TeamNumber=ivd.OrderID and OrderQRCode='{0}' and iv.Batch not like ('L%') and iv.Batch like ('%K')) conNLK,");
                        sb.Append("(select COUNT(distinct p.LabelNo) from dbo.IVRecord_Print p inner join dbo.IVRecord  iv on p.LabelNo=iv.LabelNo and iv.TeamNumber=ivd.OrderID and OrderQRCode='{0}' and iv.Batch like ('L%')) conL,");
                        sb.Append("(select COUNT(distinct p.LabelNo) from dbo.IVRecord_Print p inner join dbo.IVRecord  iv on p.LabelNo=iv.LabelNo and iv.TeamNumber=ivd.OrderID and OrderQRCode='{0}' and iv.Batch like ('L%#')) conLC,");
                        sb.Append("(select COUNT(distinct p.LabelNo) from dbo.IVRecord_Print p inner join dbo.IVRecord  iv on p.LabelNo=iv.LabelNo and iv.TeamNumber=ivd.OrderID and OrderQRCode='{0}' and iv.Batch like ('L%K')) conLK ");
                        sb.Append(" FROM [DOrder] ivd order by OrderID ");
                        ds = DB.GetPIVAsDB(string.Format(sb.ToString(), DrugQRCode));
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            using (DataSet ws = DB.GetPIVAsDB(string.Format("select distinct WardName from dbo.IVRecord_Print p inner join dbo.IVRecord  ivd on p.LabelNo=ivd.LabelNo where OrderQRCode='{0}'", DrugQRCode)))
                            {
                                string wn = string.Empty;
                                ds.Tables[0].TableName = "MedStat";
                                report.Preview = previewControl1;
                                report.Load(".\\Crystal\\reportBatch.frx");
                                report.RegisterData(ds, "ds");
                                if (ws != null && ws.Tables.Count > 0)
                                {
                                    foreach (DataRow dr in ws.Tables[0].Rows)
                                    {
                                        wn = dr[0].ToString() + "," + wn;
                                    }
                                }
                                (report.FindObject("Barcode1") as FastReport.Barcode.BarcodeObject).Text = DrugQRCode;
                                (report.FindObject("Text26") as FastReport.TextObject).Text = DrugQRCode.Substring(7, 10);
                                (report.FindObject("Text10") as FastReport.TextObject).Text = DB.GetPIVAsDB(string.Format("select DEmployeeCode from dbo.DEmployee where DEmployeeID='{0}'", useid)).Tables[0].Rows[0][0].ToString();
                                (report.FindObject("Text12") as FastReport.TextObject).Text = wn.TrimEnd(',');
                                (report.FindObject("Text21") as FastReport.TextObject).Text = string.Empty;
                                (report.FindObject("Text23") as FastReport.TextObject).Text = string.Empty;
                                (report.FindObject("Text19") as FastReport.TextObject).Text = DB.GetPIVAsDB(string.Format("select COUNT(distinct LabelNo)cont from IVRecord_Print where OrderQRCode='{0}'", DrugQRCode)).Tables[0].Rows[0][0].ToString();
                                (report.FindObject("Data1") as FastReport.DataBand).DataSource = report.GetDataSource("MedStat");

                                report.Show();
                                report.PrintSettings.ShowDialog = false;
                                report.PrintSettings.Printer = psn;
                                previewControl1.Print();
                            }
                        }
                    }
                    ds.Dispose();
                    MessageBox.Show("打印完成");
                }
                else
                {
                    MessageBox.Show("未选择任何打印记录");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool PrintReport()
        {
            if (report.Pages.Count != 0)
            {
                try
                {
                    string printerName = DB.IniReadValuePivas("Printer", "LabelPrinter");
                    PrintDocument printDocument = new PrintDocument();
                    string Mprint = printDocument.PrinterSettings.PrinterName;
                    if (string.IsNullOrEmpty(printerName))
                    {
                        if (printDocument.PrinterSettings.IsValid)
                        {
                            report.PrintSettings.Printer = Mprint;
                            report.PrintSettings.ShowDialog = false;
                            return previewControl1.Print();
                        }
                        else
                        {
                            MessageBox.Show("打印机配置为空且系统默认打印机不可用");
                            return false;
                        }
                    }
                    else
                    {
                        printDocument.PrinterSettings.PrinterName = printerName;
                        if (printDocument.PrinterSettings.IsValid)
                        {
                            report.PrintSettings.Printer = printerName;
                            report.PrintSettings.ShowDialog = false;
                            return previewControl1.Print();
                        }
                        else
                        {
                            MessageBox.Show("当前配置的打印机不可用，若使用系统默认打印机，请将打印机配置为空");
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
            else
                return false;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Thread th = new Thread(() => move(this.FindForm().Location));
            th.IsBackground = true;
            isrun = true;
            th.Start();
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isrun = false;
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            isrun = false;
        }

        private void move(Point point)
        {
            Point p = MousePosition;
            Control.CheckForIllegalCrossThreadCalls = false;
            while (isrun)
            {
                this.FindForm().Location = new Point(point.X + (MousePosition.X - p.X), point.Y + (MousePosition.Y - p.Y));
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                comboBox1.Items.Clear();
                comboBox1.Items.Add("全部病区");
                using (DataSet ds = DB.GetPIVAsDB(string.Format("select iv.WardName,WardSeqNo from IVRecord_Print ivp inner join IVRecord iv on ivp.LabelNo=iv.LabelNo and DATEDIFF(DAY,ivp.PrintDT,'{0}')=0 inner join DWard dd  on dd.wardcode=iv.wardcode order by wardseqno", dateTimePicker1.Value.ToString("yyyy-MM-dd"))))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (!comboBox1.Items.Contains(dr[0].ToString().Trim()))
                                comboBox1.Items.Add(dr[0].ToString().Trim());
                        }
                    }
                }
                comboBox1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new PrintRD(this).ShowDialog();
            if (!printed || dataGridView1.RowCount <= 0 || dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("未选择任何数据！！！");
                return;
            }
            string labelnos = string.Empty;
            System.Collections.Generic.List<string> ls = new System.Collections.Generic.List<string>();
            DataSet ds = DB.GetPIVAsDB(string.Format("SELECT [LabelNo] FROM [dbo].[IVRecord_Print] where DrugQRCode='{0}' order by ID", dataGridView1.SelectedRows[0].Cells["DrugQRCode"].Value.ToString()));
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ls.Add(dr[0].ToString());
                labelnos = labelnos + "'" + dr[0].ToString() + "',";
            }
            if (!string.IsNullOrEmpty(labelnos))
            {
                labelnos = labelnos.TrimEnd(',');
                ds = DB.GetPIVAsDB(string.Format("select LabelNo,[PatientCode] as brdm,[WardName],WardSimName,[Batch],[Age],[AgeSTR],[sex],[CaseID],[FreqCode],[UsageCode],[PatName] as brxm,[BedNo] as zwhm ,[tod],[FreqNum],[Remark1],[Remark2],[Remark3],[Remark4],[Remark5],[Remark6],[CpUser],[DeskNo] from [dbo].[V_IVRecordandDetail] v where LabelNo in({0}) group by LabelNo,[PatientCode],[WardName],WardSimName,[Batch],[Age],[AgeSTR],[sex],[CaseID],[FreqCode],[UsageCode],[PatName],[BedNo],[tod],[FreqNum],[Remark1],[Remark2],[Remark3],[Remark4],[Remark5],[Remark6] ,MarjorDrug,Menstruum,IVRecordID,[CpUser],DrugCount,[DeskNo]", labelnos));
                report.Clear();
                previewControl1.Clear();

                if (ds != null && ds.Tables[0].Rows.Count != 0)
                {
                    int total = ds.Tables[0].Rows.Count;
                    int i = 0;

                    using (DataTable dtOuvia = new DataTable())
                    {
                        using (DataTable dtMed = new DataTable())
                        {
                            //添加父表
                            dtOuvia.TableName = "ouvia";
                            dtOuvia.Columns.Add("brdm", typeof(string));
                            dtOuvia.Columns.Add("brxm", typeof(string));
                            dtOuvia.Columns.Add("zwhm", typeof(string));
                            dtOuvia.Columns.Add("LabelNo", typeof(string));
                            dtOuvia.Columns.Add("num", typeof(string));
                            dtOuvia.Columns.Add("ewm", typeof(string));
                            dtOuvia.Columns.Add("WardName", typeof(string));
                            dtOuvia.Columns.Add("WardSimName", typeof(string));
                            dtOuvia.Columns.Add("Batch", typeof(string));
                            dtOuvia.Columns.Add("Age", typeof(string));
                            dtOuvia.Columns.Add("AgeSTR", typeof(string));
                            dtOuvia.Columns.Add("CaseID", typeof(string));
                            dtOuvia.Columns.Add("FreqCode", typeof(string));
                            dtOuvia.Columns.Add("UsageCode", typeof(string));
                            dtOuvia.Columns.Add("ArrDrugUserCode", typeof(string));
                            dtOuvia.Columns.Add("PZDrugUserCode", typeof(string));
                            dtOuvia.Columns.Add("PackDrugUserCode", typeof(string));
                            dtOuvia.Columns.Add("DrugAC", typeof(string));
                            dtOuvia.Columns.Add("tod", typeof(string));
                            dtOuvia.Columns.Add("FreqNum", typeof(string));
                            dtOuvia.Columns.Add("longdt", typeof(string));
                            dtOuvia.Columns.Add("sex", typeof(string));
                            dtOuvia.Columns.Add("CpUser", typeof(string));
                            dtOuvia.Columns.Add("Remark1", typeof(string));
                            dtOuvia.Columns.Add("Remark2", typeof(string));
                            dtOuvia.Columns.Add("Remark3", typeof(string));
                            dtOuvia.Columns.Add("Remark4", typeof(string));
                            dtOuvia.Columns.Add("Remark5", typeof(string));
                            dtOuvia.Columns.Add("Remark6", typeof(string));
                            dtOuvia.Columns.Add("DeskNo", typeof(string));
                            dtOuvia.Columns.Add("IVD", typeof(string));

                            dtMed.TableName = "Med";
                            dtMed.Columns.Add("ypmc", typeof(string));
                            dtMed.Columns.Add("spec", typeof(string));
                            dtMed.Columns.Add("dose", typeof(string));
                            dtMed.Columns.Add("unit", typeof(string));
                            dtMed.Columns.Add("FormUnit", typeof(string));
                            dtMed.Columns.Add("Remark7", typeof(string));
                            dtMed.Columns.Add("Remark8", typeof(string));
                            dtMed.Columns.Add("Remark9", typeof(string));
                            dtMed.Columns.Add("Remark10", typeof(string));
                            dtMed.Columns.Add("PiShi", typeof(string));

                            using (DataTable dts = DB.GetPIVAsDB(string.Format("SELECT [DrugPlusConditionName],[LabelNo] FROM [dbo].[V_DrugAC] where DATEDIFF(DAY,'{0}',InfusionDT)=0", dateTimePicker1.Value.ToString("yyyy-MM-dd"))).Tables[0])
                            {
                                using (DataSet dss = DB.GetPIVAsDB(string.Format("select LabelNo,drugname as ypmc,spec,[Dosage] as dose,[DosageUnit] as doseu,[DosageSums] as unit ,[FormUnit] ,[Remark7],[Remark8],[Remark9],[Remark10],[PiShi],[CpUser] from [dbo].[V_IVRecordandDetail] where LabelNo in({0}) group by LabelNo,drugname,spec, [Dosage],[DosageUnit],[DosageSums],[FormUnit],[Remark7],[Remark8],[Remark9],[Remark10],[PiShi],NoName,[CpUser] order by NoName,DosageUnit,Dosage", labelnos)))
                                {
                                    foreach (string s in ls)
                                    {
                                        foreach (DataRow dr in ds.Tables[0].Select(string.Format("LabelNo='{0}'", s)))
                                        {
                                            int nu = (++i);
                                            string DrugPlusConditionName = string.Empty;
                                            foreach (DataRow drs in dts.Select(string.Format("LabelNo='{0}'", dr["LabelNo"].ToString())))
                                            {
                                                DrugPlusConditionName = DrugPlusConditionName + drs[0].ToString().Trim() + ",";
                                            }
                                            string xml = string.Empty;
                                            using (DataTable ivd = dtMed.Clone())
                                            {
                                                foreach (DataRow drss in dss.Tables[0].Select(string.Format("LabelNo='{0}'", dr["LabelNo"].ToString())))
                                                {
                                                    ivd.Rows.Add(drss["ypmc"].ToString().Replace('[', '(').Replace(']', ')'), drss["spec"].ToString().Replace('[', '(').Replace(']', ')').Trim(), (drss["dose"].ToString().Contains(".") ? drss["dose"].ToString().TrimEnd('0').TrimEnd('.') : drss["dose"].ToString().Trim()) + drss["doseu"].ToString().Trim(), Math.Round(double.Parse(drss["unit"].ToString()), 3).ToString(), drss["FormUnit"].ToString(), drss["Remark7"].ToString().Trim(), drss["Remark8"].ToString(), drss["Remark9"].ToString(), drss["Remark10"].ToString(), Equals(drss["PiShi"], true) ? "要皮试" : string.Empty);
                                                }
                                                using (System.IO.StringWriter sw = new System.IO.StringWriter())
                                                {
                                                    ivd.WriteXml(sw, XmlWriteMode.WriteSchema, false);
                                                    xml = sw.ToString();
                                                }
                                            }
                                            dtOuvia.Rows.Add(dr["brdm"].ToString().Trim(), dr["brxm"].ToString().Trim(), dr["zwhm"].ToString().Trim(), dr["LabelNo"].ToString().Trim(), nu.ToString(), string.Empty, dr["WardName"].ToString().Trim(), dr["WardSimName"].ToString().Trim(), dr["Batch"].ToString().Trim(), dr["Age"].ToString().Trim(), dr["AgeSTR"].ToString().Trim(), dr["CaseID"].ToString().Trim(), dr["FreqCode"].ToString().Trim(), dr["UsageCode"].ToString().Trim(), string.Empty, string.Empty, string.Empty, DrugPlusConditionName.TrimEnd(','), dr["tod"].ToString(), dr["FreqNum"].ToString(), dataGridView1.SelectedRows[0].Cells["DrugQRCode"].Value.ToString().Substring(7, 10), (dr["sex"].ToString().Trim() == "1" ? "男" : "女"), dr["CpUser"].ToString(), dr["Remark1"].ToString(), dr["Remark2"].ToString(), dr["Remark3"].ToString(), dr["Remark4"].ToString(), dr["Remark5"].ToString(), dr["Remark6"].ToString(), dr["DeskNo"].ToString(), xml);
                                        }
                                    }
                                }
                            }
                            report.Preview = previewControl1;
                            report.Load(".\\Crystal\\report.frx");
                            report.GetParameter("user").Value = useid;
                            report.GetParameter("total").Value = total;
                            report.RegisterData(dtOuvia, "ouvia");
                            (report.FindObject("R") as FastReport.TextObject).Visible = true;
                            (report.FindObject("Data1") as FastReport.DataBand).DataSource = report.GetDataSource("ouvia");
                            report.UseFileCache = true;
                            report.DoublePass = false;
                            report.Show();
                        }
                    }
                    if (PrintReport())
                        MessageBox.Show("打印完成");
                    else
                        MessageBox.Show("打印失败");
                }
            }
        }
    }
}
