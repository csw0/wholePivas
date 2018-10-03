using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using FastReport;

namespace PivasIVRPrint
{
    public partial class printlabel : UserControl
    {
        private UserControlPrint piv;
        private Report report = new Report();
        private DataTable dtOuvia = new DataTable();
        private DataTable dtMed = new DataTable();
        private string WardCodes;
        private string wardname = string.Empty;

        public printlabel(UserControlPrint p, string w)
        {
            WardCodes = w;
            piv = p;
            CreateTable();
            InitializeComponent();
            DataSet wn = piv.dbHelp.GetPIVAsDB(string.Format("select distinct [WardSimName] from DWard {0}", string.IsNullOrEmpty(WardCodes.TrimEnd(',')) ? string.Empty : ("where WardCode in (" + WardCodes.TrimEnd(',') + ")")));
            if (wn != null && wn.Tables.Count > 0)
            {
                foreach (DataRow dr in wn.Tables[0].Rows)
                {
                    wardname = wardname + dr[0].ToString() + " ";
                }
            }
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            if (UserControlPrint.PreviewMode == 3)
            {
                new ShowDg(this).ShowDialog();
                //ShowNewInfo();
            }
        }
        //设计数据表
        private void CreateTable()
        {
            dtOuvia = new DataTable();
            dtMed = new DataTable();
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
        }
        /// <summary>
        /// 显示当日未打印信息
        /// </summary>
        public void ShowNewInfo()
        {
            try
            {
                if (string.IsNullOrEmpty(piv.textValue))
                {
                    //MessageBox.Show(getsql("Pcode") + "    \n" + getsql("Detail"));
                    ShowReport(getsql("Pcode"), getsql("Detail"), DateTime.Now.Ticks.ToString(), new List<string>());

                }
                else
                {
                    //模糊查询打印
                    //MessageBox.Show(getsql("textValue"));
                    ShowReport(getsql("textValue"), getsql("textValueS"), DateTime.Now.Ticks.ToString(), new List<string>());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 显示报表
        /// </summary>
        /// <param name="ds"></param>
        private void ShowReport(string sql, string sql2, string longdt, List<string> ls)
        {
            using (DataSet ds = piv.dbHelp.GetPIVAsDB(sql))
            {
                report.Clear();
                previewControlFR.Clear();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dtOuvia.Rows.Clear();
                    dtMed.Rows.Clear();
                    int total = ds.Tables[0].Rows.Count;
                    int i = 0;

                    using (DataTable dts = piv.dbHelp.GetPIVAsDB(string.Format("SELECT [DrugPlusConditionName],[LabelNo] FROM [dbo].[V_DrugAC] where DATEDIFF(DAY,'{0}',InfusionDT)=0", piv.dateTimePicker1.Value.ToString("yyyy-MM-dd"))).Tables[0])
                    {
                        using (DataSet dss = piv.dbHelp.GetPIVAsDB(sql2))
                        {
                            if (ls.Count == 0)
                            {
                                foreach (DataRow dr in ds.Tables[0].Rows)
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
                                    for (int ks = 0; ks < UserControlPrint.PrintNumber; ks++)
                                    {
                                        dtOuvia.Rows.Add(dr["brdm"].ToString().Trim(), dr["brxm"].ToString().Trim(), dr["zwhm"].ToString().Trim(), dr["LabelNo"].ToString().Trim(), nu.ToString(), string.Empty, dr["WardName"].ToString().Trim(), dr["WardSimName"].ToString().Trim(), dr["Batch"].ToString().Trim(), dr["Age"].ToString().Trim(), dr["AgeSTR"].ToString().Trim(), dr["CaseID"].ToString().Trim(), dr["FreqCode"].ToString().Trim(), dr["UsageCode"].ToString().Trim(), piv.ArrDrugUserCode, piv.PZDrugUserCode, piv.PackDrugUserCode, DrugPlusConditionName.TrimEnd(','), dr["tod"].ToString(), dr["FreqNum"].ToString(), longdt.Substring(3, 10), (dr["sex"].ToString().Trim() == "1" ? "男" : "女"), dr["CpUser"].ToString(), dr["Remark1"].ToString(), dr["Remark2"].ToString(), dr["Remark3"].ToString(), dr["Remark4"].ToString(), dr["Remark5"].ToString(), dr["Remark6"].ToString(), dr["DeskNo"].ToString(), xml);
                                    }
                                }
                            }
                            else
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
                                        for (int ks = 0; ks < UserControlPrint.PrintNumber; ks++)
                                        {
                                            dtOuvia.Rows.Add(dr["brdm"].ToString().Trim(), dr["brxm"].ToString().Trim(), dr["zwhm"].ToString().Trim(), dr["LabelNo"].ToString().Trim(), nu.ToString(), string.Empty, dr["WardName"].ToString().Trim(), dr["WardSimName"].ToString().Trim(), dr["Batch"].ToString().Trim(), dr["Age"].ToString().Trim(), dr["AgeSTR"].ToString().Trim(), dr["CaseID"].ToString().Trim(), dr["FreqCode"].ToString().Trim(), dr["UsageCode"].ToString().Trim(), piv.ArrDrugUserCode, piv.PZDrugUserCode, piv.PackDrugUserCode, DrugPlusConditionName.TrimEnd(','), dr["tod"].ToString(), dr["FreqNum"].ToString(), longdt.Substring(3, 10), (dr["sex"].ToString().Trim() == "1" ? "男" : "女"), dr["CpUser"].ToString(), dr["Remark1"].ToString(), dr["Remark2"].ToString(), dr["Remark3"].ToString(), dr["Remark4"].ToString(), dr["Remark5"].ToString(), dr["Remark6"].ToString(), dr["DeskNo"].ToString(), xml);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    report.Preview = previewControlFR;
                    report.Load(piv.UsageCodeS.Trim() == "静推" ? ".\\Crystal\\reportByUsageCode.frx" : ".\\Crystal\\report.frx");
                    report.GetParameter("user").Value = piv.userID;
                    report.GetParameter("total").Value = total;
                    report.RegisterData(dtOuvia, "ouvia");
                    (report.FindObject("Data1") as FastReport.DataBand).DataSource = report.GetDataSource("ouvia");
                    report.Show();
                }
                else
                {
                    MessageBox.Show("没有符合的数据！！！");
                }
            }
        }

        /// <summary>
        /// 打印报表，将水晶报表的数据打印出来
        /// </summary>
        private bool PrintReport()
        {
            if (!piv.SendPrt)
            {
                return true;
            }
            if (report.Pages.Count != 0)
            {
                if (piv.UsageCodeS.Trim()=="静推")
                {
                    report.PrintSettings.ShowDialog = true;
                    return previewControlFR.Print();
                }
                else
                {
                    string printerName = piv.LabelNoPrint;
                    PrintDocument printDocument = new PrintDocument();
                    string Mprint = printDocument.PrinterSettings.PrinterName;
                    if (string.IsNullOrEmpty(printerName))
                    {
                        if (printDocument.PrinterSettings.IsValid)
                        {
                            report.PrintSettings.Printer = Mprint;
                            report.PrintSettings.ShowDialog = false;
                            report.Print();
                            //if (previewControlFR.Print())
                            //{
                                return true;
                            //}
                            //else
                            //{
                            //    MessageBox.Show("打印机返回错误信息本次打印无效！");
                            //    return false;
                            //}
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
                            report.Print();
                            //if (previewControlFR.Print())
                            //{
                                return true;
                            //}
                            //else
                            //{
                            //    MessageBox.Show("打印机返回错误信息本次打印无效！");
                            //    return false;
                            //}
                        }
                        else
                        {
                            MessageBox.Show("当前配置的打印机不可用，若使用系统默认打印机，请将打印机配置为空");
                            return false;
                        }
                    }
                }
            }
            else
                return false;
        }
        /// <summary>
        /// 打印后更新打印状态
        /// </summary>
        /// 
        private bool UpdatePrintState(string labelNo, string TimeID, string LongDt, List<string> ls)
        {
            //MessageBox.Show(string.Format("exec [dbo].[bl_IVRprintLog] '{0}','{1}','{2}','{3}','{4}','{5}','{6}' ", labelNo, piv.userID, "", TimeID, piv.dateTimePicker1.Value.ToString("yyyy-MM-dd") + " " + piv.comboBox1.SelectedItem + " 批次：" + piv.comboBox2.SelectedItem, "8888" + LongDt, "9999" + LongDt));
            try
            {
                string sb1 = string.Format(" select DEmployeeName from DEmployee where DEmployeeID='{0}' ", piv.userID);
                string sb2 = string.Format(" select IVRecordID,IVStatus,PrintDT,PrinterID,PrinterName from IVRecord where IVStatus=0 and LabelNo in({0})", labelNo);
                string DEmployeeName = piv.dbHelp.GetPIVAsDB(sb1).Tables[0].Rows[0][0].ToString();
                string PrintDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                using (System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection(piv.dbHelp.DatebasePIVAsInfo()))
                {
                    using (System.Data.SqlClient.SqlCommand scm = new System.Data.SqlClient.SqlCommand(sb2, sc))
                    {
                        using (System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter(scm))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                sda.Fill(ds);
                                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                {
                                    ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["IVRecordID"] };
                                    foreach (DataRow dr in ds.Tables[0].Rows)
                                    {
                                        dr["IVStatus"] = 3;
                                        dr["PrintDT"] = PrintDT;
                                        dr["PrinterID"] = piv.userID;
                                        dr["PrinterName"] = DEmployeeName;
                                    }
                                    using (System.Data.SqlClient.SqlCommandBuilder scb = new System.Data.SqlClient.SqlCommandBuilder(sda))
                                    {
                                        scb.GetUpdateCommand();
                                        using (DataSet cds = ds.GetChanges(DataRowState.Modified))
                                        {
                                            int cs = 0;
                                        a: int ret = scb.DataAdapter.Update(cds);
                                            if (ret <= 0)
                                            {
                                                if (cs < 5)
                                                {
                                                    cs = cs + 1;
                                                    piv.NowST = string.Format("更新瓶签失败,正在{0}/5次重试",cs);
                                                    goto a;
                                                }
                                                else
                                                {
                                                    MessageBox.Show("当次打印未成功更新瓶签状态，请作废此批标签！！！");
                                                    return false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    StringBuilder sb = new StringBuilder(2048);
                    sb.AppendLine("IF(OBJECT_ID('IVRecord_Print_AllEmp') is null) BEGIN ");
                    sb.AppendLine("CREATE TABLE [dbo].[IVRecord_Print_AllEmp]([LabelNo] [varchar](32) NOT NULL,[DrugQRCode] [varchar](100) NOT NULL,[ArrDrugUserCode] [varchar](50) NULL,");
                    sb.AppendLine("[PZDrugUserCode] [varchar](50) NULL,[PackDrugUserCode] [varchar](50) NULL, CONSTRAINT [PK_IVRecord_Print_AllEmp] PRIMARY KEY CLUSTERED ([LabelNo] ASC,[DrugQRCode] ASC) ");
                    sb.AppendLine("WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] END ");
                    piv.dbHelp.SetPIVAsDB(sb.ToString());

                    using (System.Data.SqlClient.SqlCommand scm1 = 
                        new System.Data.SqlClient.SqlCommand(
                            string.Format("select * from [IVRecord_Print] where LabelNo in ({0})", labelNo), sc), 
                            scm2 = new System.Data.SqlClient.SqlCommand("select * from [IVRecord_Print_AllEmp] where 1!=1 ", sc))
                    {
                        using (System.Data.SqlClient.SqlDataAdapter sda1 = new System.Data.SqlClient.SqlDataAdapter(scm1), sda2 = new System.Data.SqlClient.SqlDataAdapter(scm2))
                        {
                            using (DataSet ds1 = new DataSet(), ds2 = new DataSet())
                            {
                                sda1.Fill(ds1);
                                sda2.Fill(ds2);
                                ds2.Tables[0].PrimaryKey = new DataColumn[] { ds2.Tables[0].Columns[0], ds2.Tables[0].Columns[1] };
                                foreach (string s in ls)
                                {
                                    DataRow dr = ds1.Tables[0].NewRow();
                                    dr["LabelNo"] = s;
                                    dr["PrintDT"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                    dr["PrintCode"] = piv.userID;
                                    dr["ReprintExplain"] = piv.WhyRePrint;
                                    dr["PrintCount"] = ds1.Tables[0].Select(string.Format("LabelNo='{0}'", s)).Length;
                                    dr["PrintNO"] = TimeID;
                                    dr["Condition"] = piv.dateTimePicker1.Value.ToString("yyyy-MM-dd") + " " + piv.comboBox1.SelectedItem + " 批次：" + piv.comboBox2.SelectedItem;
                                    dr["DrugQRCode"] = "8888" + LongDt;
                                    dr["OrderQRCode"] = "9999" + LongDt;
                                    dr["DrugType"] = 0;
                                    ds1.Tables[0].Rows.Add(dr);

                                    DataRow dre = ds2.Tables[0].NewRow();
                                    dre["LabelNo"] = s;
                                    dre["DrugQRCode"] = "8888" + LongDt;
                                    dre["ArrDrugUserCode"] = piv.ArrDrugUserCode;
                                    dre["PZDrugUserCode"] = piv.PZDrugUserCode;
                                    dre["PackDrugUserCode"] = piv.PackDrugUserCode;
                                    ds2.Tables[0].Rows.Add(dre);
                                }
                                using (System.Data.SqlClient.SqlCommandBuilder scb1 = new System.Data.SqlClient.SqlCommandBuilder(sda1), scb2 = new System.Data.SqlClient.SqlCommandBuilder(sda2))
                                {
                                    scb1.GetInsertCommand();
                                    scb1.DataAdapter.Update(ds1.GetChanges(DataRowState.Added));
                                    scb2.GetInsertCommand();
                                    scb2.DataAdapter.Update(ds2.GetChanges(DataRowState.Added));
                                }
                            }
                        }
                    }
                    piv.dbHelp.SetPIVAsDB(string.Format("UPDATE [IVRecord] SET IVStatus=3,PrinterID='{0}',PrinterName='{1}',PrintDT=GETDATE() WHERE IVStatus=0 AND EXISTS(SELECT 'X' FROM IVRecord_Print IVP WHERE IVP.LabelNo=IVRecord.LabelNo AND DrugQRCode !='' )", piv.userID, DEmployeeName));
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool btnPrint_Click(string TimeID, string LongDt)
        {
            try
            {
                if (PrintReport())
                {
                    //获取打印的瓶签号
                    string lb = string.Empty;
                    List<string> ls = new List<string>();
                    foreach (DataRow dr in dtOuvia.Rows)
                    {
                        if (!ls.Contains(dr["LabelNo"].ToString()))
                        {
                            ls.Add(dr["LabelNo"].ToString());
                            lb = lb + "'" + dr["LabelNo"].ToString() + "',";
                        }
                    }
                    if (!string.IsNullOrEmpty(lb))
                    {
                        if(!UpdatePrintState(lb.TrimEnd(','), TimeID, LongDt, ls))
                        {
                            return false;
                        }
                    }
                    previewControlFR.Clear();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private string getsql(string MOD)
        {
            string sql = "";
            switch (MOD)
            {
                case "LabelNO":
                    sql = string.Format("SELECT distinct LabelNo FROM ({0} where datediff(DAY,InfusionDT,'{1}')=0 "+(piv.checkBox3.CheckState==CheckState.Checked?string.Empty:string.Format(" and iv.WardCode in ({0}) ", WardCodes.TrimEnd(',')))+" {2} and [IVStatus]" + piv.ad + piv.OrderID + piv.isJustOne + piv.Specs + piv.Position + piv.ByUsageCode+")b",piv.GetLabelNoSql, piv.dateTimePicker1.Value.ToString("yyyy-MM-dd"), piv.checkBox1.CheckState == CheckState.Checked ? "" : " and ivd.DrugCode in (" + piv.DrugCodes.TrimEnd(',') + ")");
                    break;
                case "Pcode":
                    sql = string.Format("SELECT distinct LabelNo,[PatientCode] as brdm,[WardName],WardSimName,[Batch],[Age],[AgeSTR],sex,[CaseID],[FreqCode],[UsageCode],[PatName] as brxm,[BedNo] as zwhm ,[tod],[FreqNum],[Remark1],[Remark2],[Remark3],[Remark4],[Remark5],[Remark6],[CpUser],[DeskNo] FROM ({0} where datediff(DAY,InfusionDT,'{1}')=0 " + (piv.checkBox3.CheckState == CheckState.Checked ? string.Empty : string.Format(" and iv.WardCode in ({0}) ", WardCodes.TrimEnd(','))) + " {2} and [IVStatus]" + piv.ad + piv.OrderID + piv.Position + piv.isJustOne + piv.Specs + piv.ByUsageCode + ")b order by LabelNo", piv.GetLabelNoSql,piv.dateTimePicker1.Value.ToString("yyyy-MM-dd"), piv.checkBox1.CheckState == CheckState.Checked ? "" : " and ivd.DrugCode in (" + piv.DrugCodes.TrimEnd(',') + ")");
                    break;
                case "Detail":
                    sql = string.Format("select LabelNo,drugname as ypmc,spec, [Dosage] as dose,[DosageUnit] as doseu,[DosageSums] as unit ,[FormUnit],[Remark7],[Remark8],[Remark9],[Remark10],[PiShi],NoName,[CpUser] from ({0} where datediff(DAY,InfusionDT,'{1}')=0 " + (piv.checkBox3.CheckState == CheckState.Checked ? string.Empty : string.Format(" and iv.WardCode in ({0}) ", WardCodes.TrimEnd(','))) + " {2} and [IVStatus]" + piv.ad + piv.OrderID + piv.Position + piv.isJustOne + piv.Specs + piv.ByUsageCode + ")b order by LabelNo,NoName,DosageUnit,Dosage,[FormUnit]", piv.GetLabelNoSql,piv.dateTimePicker1.Value.ToString("yyyy-MM-dd"), piv.checkBox1.CheckState == CheckState.Checked ? "" : " and ivd.DrugCode in (" + piv.DrugCodes.TrimEnd(',') + ")");
                    break;
                case "PrintOne":
                    sql = string.Format("select LabelNo,[PatientCode] as brdm,[WardName],WardSimName,[Batch],[Age],[AgeSTR],[sex],[CaseID],[FreqCode],[UsageCode],[PatName] as brxm,[BedNo] as zwhm ,[tod],[FreqNum],[Remark1],[Remark2],[Remark3],[Remark4],[Remark5],[Remark6],[CpUser],[DeskNo] from ({0} where LabelNo in({1}))v group by LabelNo,[PatientCode],[WardName],WardSimName,[Batch],[Age],[AgeSTR],[sex],[CaseID],[FreqCode],[UsageCode],[PatName],[BedNo],[tod],[FreqNum],[Remark1],[Remark2],[Remark3],[Remark4],[Remark5],[Remark6] ,MarjorDrug,Menstruum,IVRecordID,[CpUser],DrugCount,[DeskNo] order by {2},DrugCount", piv.GetLabelNoSql,piv.selected, piv.showprint.Replace("溶媒", "Menstruum").Replace("批次", "Batch").Replace("主药", "MarjorDrug").Replace("病区", "WardName").Replace("用量", "(SELECT max([DgNo]) FROM [dbo].[IVRecordDetail] where IVRecordID =v.IVRecordID and DrugCode=MarjorDrug)"));
                    break;
                case "PrintOneS":
                    sql = string.Format("select LabelNo,drugname as ypmc,spec,[Dosage] as dose,[DosageUnit] as doseu,[DosageSums] as unit ,[FormUnit] ,[Remark7],[Remark8],[Remark9],[Remark10],[PiShi],[CpUser] from ({0} where LabelNo in({1}))v order by NoName,DosageUnit,Dosage", piv.GetLabelNoSql,piv.selected);
                    break;
                case "textValue":
                    sql = string.Format("select distinct LabelNo,[PatientCode] as brdm,[WardName],WardSimName,[Batch],[Age],[AgeSTR],sex,[CaseID],[FreqCode],[UsageCode],[PatName] as brxm,[BedNo] as zwhm ,[tod],[FreqNum],[Remark1],[Remark2],[Remark3],[Remark4],[Remark5],[Remark6],[CpUser],[DeskNo] from ({0} where (select MIN(ReturnFromHis) from [IVRecordDetail] a where a.IVRecordID=iv.IVRecordID)=3 and (LabelNo like '%{1}%' or PatientCode like '%{1}%' or BedNo like '%{1}%' or pat.PatName like '%{1}%') and DATEDIFF(DAY,InfusionDT,'{2}')=0 and IVStatus" + piv.ad + piv.OrderID + piv.Position + piv.isJustOne + piv.Specs + piv.ByUsageCode + " {3}) b", piv.GetLabelNoSql,piv.textValue, piv.dateTimePicker1.Value.ToString("yyyy-MM-dd"), string.IsNullOrEmpty(WardCodes.TrimEnd(',')) ? string.Empty : (" and iv.WardCode in (" + WardCodes.TrimEnd(',') + ")"));
                    break;
                case "textValueS":
                    sql = string.Format("select LabelNo,drugname as ypmc,spec, [Dosage] as dose,[DosageUnit] as doseu,[DosageSums] as unit ,[FormUnit],[Remark7],[Remark8],[Remark9],[Remark10],[PiShi],NoName,[CpUser] from ({0} where (select MIN(ReturnFromHis) from IVRecordDetail a where a.IVRecordID=iv.IVRecordID)=3 and (LabelNo like '%{1}%' or PatientCode like '%{1}%' or BedNo like '%{1}%' or pat.PatName like '%{1}%') and DATEDIFF(DAY,InfusionDT,'{2}')=0 and IVStatus" + piv.ad + piv.OrderID + piv.Position + piv.isJustOne + piv.Specs + piv.ByUsageCode + " {3})b order by LabelNo,NoName,DosageUnit,Dosage,[FormUnit]", piv.GetLabelNoSql,piv.textValue, piv.dateTimePicker1.Value.ToString("yyyy-MM-dd"), string.IsNullOrEmpty(WardCodes.TrimEnd(',')) ? string.Empty : (" and iv.WardCode in (" + WardCodes.TrimEnd(',') + ")"));
                    break;
                case "Pcode2":
                    sql = string.Format("SELECT distinct LabelNo,[PatientCode] as brdm,[WardName],WardSimName,[Batch],[Age],[AgeSTR],sex,[CaseID],[FreqCode],[UsageCode],[PatName] as brxm,[BedNo] as zwhm,[tod] ,[FreqNum],[Remark1],[Remark2],[Remark3],[Remark4],[Remark5],[Remark6],[CpUser],[DeskNo] FROM ({0} where (select MIN(ReturnFromHis) from [IVRecordDetail] a where a.IVRecordID=iv.IVRecordID)=3 and datediff(DAY,InfusionDT,'{1}')=0 " + (piv.checkBox3.CheckState == CheckState.Checked ? string.Empty : string.Format(" and iv.WardCode in ({0}) ", WardCodes.TrimEnd(','))) + " {3} and [IVStatus]" + piv.ad + piv.OrderID + piv.Position + piv.isJustOne + piv.Specs + piv.ByUsageCode + " )b order by LabelNo", piv.GetLabelNoSql,piv.dateTimePicker1.Value.ToString("yyyy-MM-dd"), piv.checkBox1.CheckState == CheckState.Checked ? "" : " and ivd.DrugCode in (" + piv.DrugCodes.TrimEnd(',') + ")");
                    break;
                case "Detail2":
                    sql = string.Format("select LabelNo,drugname as ypmc,spec, [Dosage] as dose,[DosageUnit] as doseu,[DosageSums] as unit ,[FormUnit],[Remark7],[Remark8],[Remark9],[Remark10],[PiShi],NoName,[CpUser] from ({0} where (select MIN(ReturnFromHis) from IVRecordDetail a where a.IVRecordID=iv.IVRecordID)=3 and LabelNo in ({1}))b order by LabelNo,NoName,DosageUnit,Dosage,[FormUnit]", piv.GetLabelNoSql,getsql("LabelNO"));
                    break;
            }
            return sql;
        }


        public bool PrintOne(string TimeID, string LongDt, ref List<string> ls)
        {
            try
            {
                ShowReport(getsql("PrintOne"), getsql("PrintOneS"), LongDt, ls);
                if (PrintReport())
                {
                    piv.NowST = "正在写入打印记录";
                    return UpdatePrintState(piv.selected, TimeID, LongDt, ls);
                    //piv.NowST = "打印记录写入完成";
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                piv.NowST = "打印完成";
                return false;
            }
            finally
            {
                piv.selected = string.Empty;
                this.Dispose(true);
            }
        }
        public bool PrintDrugSum(string DrugQRCode)
        {
            try
            {
                DataSet ds = null;
                DataSet dds = piv.dbHelp.GetPIVAsDB(string.Format("select Batch from IVRecord IV inner join IVRecord_Print p on DrugQRCode='{0}' and IV.LabelNo=p.LabelNo inner join IVRecordDetail ivd on iv.IVRecordID=ivd.IVRecordID inner join DDrug dd on IsMenstruum=1 and ivd.DrugCode=dd.DrugCode group by Batch order by Batch", DrugQRCode));
                //string dgs = string.Empty;
                report.Preview = previewControlFR;
                report.Load(".\\Crystal\\reportStat.frx");
                if (UserControlPrint.PrintDrugCount > 1)
                {
                    try
                    {
                        if (dds != null && dds.Tables.Count > 0 && dds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dds.Tables[0].Rows)
                            {
                                ds = piv.dbHelp.GetPIVAsDB(string.Format("select ivd.DrugCode as '药品编码',ivd.[DrugName] as '药品名称',ivd.[Spec] as '药品规格',sum(DgNo) as '数量' from IVRecord IV inner join IVRecord_Print p on DrugQRCode='{0}' and Batch='{1}' and IV.LabelNo=p.LabelNo inner join IVRecordDetail ivd on iv.IVRecordID=ivd.IVRecordID inner join DDrug dd on IsMenstruum=1 and ivd.DrugCode=dd.DrugCode group by ivd.DrugCode,ivd.DrugName,ivd.Spec", DrugQRCode, dr[0].ToString()));
                                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                {
                                    DataTable dt = new DataTable("MedStat");
                                    foreach (DataColumn dc in ds.Tables[0].Columns)
                                    {
                                        dt.Columns.Add(dc.ColumnName, dc.DataType);
                                    }
                                    if (dt.Columns.Count > 0)
                                    {
                                        dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };
                                        if (piv.Menstruum.Count > 0)
                                        {
                                            foreach (string s in piv.Menstruum)
                                            {
                                                if (s.Split(',').Length > 1 && s.Split(',')[0].Trim() == dr["Batch"].ToString().Trim())
                                                {
                                                    DataRow[] dss = ds.Tables[0].Select(string.Format("药品编码='{0}'", s.Split(',')[1].Trim()));
                                                    if (dss.Length > 0)
                                                    {
                                                        dt.Rows.Add(dss[0].ItemArray);
                                                    }
                                                }
                                            }
                                        }
                                        foreach (DataRow drs in ds.Tables[0].Rows)
                                        {
                                            if (!dt.Rows.Contains(drs["药品编码"]))
                                            {
                                                dt.Rows.Add(drs.ItemArray);
                                            }
                                        }
                                        ds = new DataSet();
                                        ds.Tables.Add(dt);
                                        //ds.Tables[0].TableName = "MedStat";
                                        report.RegisterData(ds, "ds");
                                        (report.FindObject("Text1") as FastReport.TextObject).Text = "溶媒";
                                        (report.FindObject("Text14") as FastReport.TextObject).Text = DrugQRCode.Substring(7, 10);
                                        (report.FindObject("Text13") as FastReport.TextObject).Text = wardname.Trim();
                                        (report.FindObject("Text12") as FastReport.TextObject).Text = dr[0].ToString();
                                        (report.FindObject("Barcode1") as FastReport.Barcode.BarcodeObject).Text = DrugQRCode;
                                        (report.FindObject("Data1") as FastReport.DataBand).DataSource = report.GetDataSource("MedStat");
                                        report.Show();
                                        PrintReport();
                                    }
                                    //dgs = dgs + dr[0].ToString() + ",";
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if (UserControlPrint.PrintDrugCount < 3)
                {
                    try
                    {
                        ds = piv.dbHelp.GetPIVAsDB(string.Format("select ivd.DrugCode as '药品编码',ivd.[DrugName] as '药品名称',ivd.[Spec] as '药品规格',sum(DgNo) as '数量' from IVRecord IV inner join IVRecord_Print p on DrugQRCode='{0}' and IV.LabelNo=p.LabelNo inner join IVRecordDetail ivd on iv.IVRecordID=ivd.IVRecordID inner join DDrug dd on IsMenstruum=0 and ivd.DrugCode=dd.DrugCode group by ivd.DrugCode,ivd.DrugName,ivd.Spec", DrugQRCode));
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            ds.Tables[0].TableName = "MedStat";
                            report.RegisterData(ds, "ds");
                            dds = piv.dbHelp.GetPIVAsDB(string.Format("select Batch from IVRecord IV inner join IVRecord_Print p on DrugQRCode='{0}' and IV.LabelNo=p.LabelNo inner join IVRecordDetail ivd on iv.IVRecordID=ivd.IVRecordID inner join DDrug dd on IsMenstruum=0 and ivd.DrugCode=dd.DrugCode group by Batch order by Batch", DrugQRCode));
                            string dgs = string.Empty;
                            foreach (DataRow dr in dds.Tables[0].Rows)
                            {
                                dgs = dgs + dr[0].ToString() + ",";
                            }
                            (report.FindObject("Text1") as FastReport.TextObject).Text = "药品";
                            (report.FindObject("Text14") as FastReport.TextObject).Text = DrugQRCode.Substring(7, 10);
                            (report.FindObject("Text13") as FastReport.TextObject).Text = wardname.Trim();
                            (report.FindObject("Text12") as FastReport.TextObject).Text = dgs.TrimEnd(',');
                            (report.FindObject("Barcode1") as FastReport.Barcode.BarcodeObject).Text = DrugQRCode;
                            (report.FindObject("Data1") as FastReport.DataBand).DataSource = report.GetDataSource("MedStat");
                            report.Show();
                            PrintReport();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                dds.Dispose();
                ds.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                piv.NowST = "打印完成";
                return false;
            }
        }
        public bool PrintBathSum(string DrugQRCode)
        {
            try
            {
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
                //MessageBox.Show(string.Format(sb.ToString(), DrugQRCode));
                using (DataSet ds = piv.dbHelp.GetPIVAsDB(string.Format(sb.ToString(), DrugQRCode)))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        report.Clear();
                        previewControlFR.Clear();
                        ds.Tables[0].TableName = "MedStat";
                        report.Preview = previewControlFR;
                        report.Load(".\\Crystal\\reportBatch.frx");
                        report.RegisterData(ds, "ds");
                        (report.FindObject("Barcode1") as FastReport.Barcode.BarcodeObject).Text = DrugQRCode;
                        (report.FindObject("Text26") as FastReport.TextObject).Text = DrugQRCode.Substring(7, 10);
                        (report.FindObject("Text10") as FastReport.TextObject).Text = piv.dbHelp.GetPIVAsDB(string.Format("select DEmployeeCode from dbo.DEmployee where DEmployeeID='{0}'", piv.userID)).Tables[0].Rows[0][0].ToString();
                        (report.FindObject("Text12") as FastReport.TextObject).Text = wardname.Trim();
                        (report.FindObject("Text21") as FastReport.TextObject).Text = piv.ArrDrugUserCode;
                        (report.FindObject("Text23") as FastReport.TextObject).Text = string.Empty;
                        (report.FindObject("Text19") as FastReport.TextObject).Text = piv.dbHelp.GetPIVAsDB(string.Format("select COUNT(distinct LabelNo)cont from IVRecord_Print where OrderQRCode='{0}'", DrugQRCode)).Tables[0].Rows[0][0].ToString();
                        (report.FindObject("Data1") as FastReport.DataBand).DataSource = report.GetDataSource("MedStat");
                        report.Show();
                        PrintReport();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                piv.NowST = "打印完成";
                return false;
            }
        }
    }
}
