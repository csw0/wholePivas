using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using FastReport;
using PIVAsCommon.Helper;

namespace PivasIVRPrint
{
    public partial class BQlabelShow : UserControl
    {
        private DB_Help DB = new DB_Help();
        private Report report = new Report();
        private DataTable dtOuvia = new DataTable();
        private DataTable dtMed = new DataTable();
        private UserControlPrint piv;
        private bool locked = false;
        private string labelNo = string.Empty;
        public BQlabelShow()
        {
            InitializeComponent();
        }
        public BQlabelShow(UserControlPrint piv)
        {
            this.piv = piv;
            InitializeComponent();
            CreateTable();
        }

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
            report.Preview = previewControl1;
        }

        private void BQlabelShow_Load(object sender, EventArgs e)
        {
            MShow(string.Empty);
        }
        protected internal void MShow(string labelNo)
        {
            if (!locked)
            {
                if (this.labelNo != labelNo)
                {
                    report.Load(piv.UsageCodeS == "静推" ? ".\\Crystal\\reportByUsageCode.frx" : ".\\Crystal\\report.frx");
                    report.GetParameter("user").Value = piv.userID;
                    this.labelNo = labelNo;
                    Tmrun();
                }
            }
        }

        private void Tmrun()
        {
            try
            {
                locked = true;
                if (!string.IsNullOrEmpty(labelNo))
                {
                    using (DataSet ds = DB.GetPIVAsDB(string.Format("SELECT * FROM ({0} where LabelNo = '{1}')V order by NoName,DosageUnit,Dosage", piv.GetLabelNoSql,labelNo)))
                    {
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            using (DataTable dt = ds.Tables[0].Copy())
                            {
                                string xml = string.Empty;
                                using (DataTable ivd = dtMed.Clone())
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        ivd.Rows.Add(dr["drugname"].ToString().Replace('[', '(').Replace(']', ')'), dr["spec"].ToString().Replace('[', '(').Replace(']', ')').Trim(), (dr["Dosage"].ToString().Contains(".") ? dr["Dosage"].ToString().TrimEnd('0').TrimEnd('.') : dr["Dosage"].ToString().Trim()) + dr["DosageUnit"].ToString().Trim(), Math.Round(double.Parse(dr["DosageSums"].ToString()), 3).ToString(), dr["FormUnit"].ToString(), dr["Remark7"].ToString().Trim(), dr["Remark8"].ToString(), dr["Remark9"].ToString(), dr["Remark10"].ToString(), Equals(dr["PiShi"], true) ? "要皮试" : string.Empty);
                                    }
                                    using (System.IO.StringWriter sw = new System.IO.StringWriter())
                                    {
                                        ivd.WriteXml(sw, XmlWriteMode.WriteSchema, false);
                                        xml = sw.ToString();
                                    }
                                }
                                using (DataTable dts = DB.GetPIVAsDB(string.Format("SELECT [DrugPlusConditionName],[LabelNo] FROM [dbo].[V_DrugAC] where DATEDIFF(DAY,'{0}',InfusionDT)=0 and LabelNo='{1}'", piv.dateTimePicker1.Value.ToString("yyyy-MM-dd"), dt.Rows[0]["LabelNo"].ToString())).Tables[0])
                                {
                                    DataRow dr = dt.Rows[0];
                                    string DrugPlusConditionName = string.Empty;
                                    foreach (DataRow drs in dts.Rows)
                                    {
                                        DrugPlusConditionName = DrugPlusConditionName + drs[0].ToString().Trim() + ",";
                                    }
                                    dtOuvia.Rows.Clear();
                                    dtOuvia.Rows.Add(dr["PatientCode"].ToString().Trim(), dr["PatName"].ToString().Trim(), dr["BedNo"].ToString().Trim(), dr["LabelNo"].ToString().Trim(), "1", string.Empty, dr["WardName"].ToString().Trim(), dr["WardSimName"].ToString().Trim(), dr["Batch"].ToString().Trim(), dr["Age"].ToString().Trim(), dr["AgeSTR"].ToString().Trim(), dr["CaseID"].ToString().Trim(), dr["FreqCode"].ToString().Trim(), dr["UsageCode"].ToString().Trim(), piv.ArrDrugUserCode, piv.PZDrugUserCode, piv.PackDrugUserCode, DrugPlusConditionName.TrimEnd(','), dr["tod"].ToString(), dr["FreqNum"].ToString(), DateTime.Now.Ticks.ToString().Substring(3, 10), (dr["sex"].ToString().Trim() == "1" ? "男" : "女"), dr["CpUser"].ToString(), dr["Remark1"].ToString(), dr["Remark2"].ToString(), dr["Remark3"].ToString(), dr["Remark4"].ToString(), dr["Remark5"].ToString(), dr["Remark6"].ToString(), dr["DeskNo"].ToString(), xml);
                                }
                                report.GetParameter("total").Value = dt.Rows.Count;
                                report.RegisterData(dtOuvia, "ouvia");
                                (report.FindObject("Data1") as FastReport.DataBand).DataSource = report.GetDataSource("ouvia");
                                report.UseFileCache = true;
                                report.DoublePass = false;
                                report.Show();
                            }
                        }
                    }
                }
            }
            catch (KeyNotFoundException)
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                timer1.Start();
            }
        }



        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            locked = false;
            timer1.Stop();
        }
    }
}
