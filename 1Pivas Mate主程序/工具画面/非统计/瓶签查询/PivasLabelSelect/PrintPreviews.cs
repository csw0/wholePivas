using FastReport;
using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace PivasLabelSelect
{
    public partial class PrintPreviews : Form
    {
        private DB_Help db = new DB_Help();
        private Report report = new Report();
        private DataTable LabelTol = new DataTable(); //存放当天所有的瓶签
        private DataTable DrugPlusCondition = new DataTable(); //存放所有瓶签的附加条件
        private DataTable dtOuvia = new DataTable();
        private DataTable dtMed = new DataTable();
        string labelnos = string.Empty;
        int labelsum = 10; //每个模块瓶签数目
        string date = "";
        
        public PrintPreviews(string labelnos,string date )
        {
           
            InitializeComponent();
            this.labelnos = labelnos;
            this.date = date;
            
        }

        private void PrintPreviews_Load(object sender, EventArgs e)
        {          
            try
            {
                //TaskFactory taskFactory = new TaskFactory();
                //List<Task> taskList=new List<Task>();
                //Wait wde = new Wait();
                //Action act = () =>
                //{                 
                //    wde.ShowDialog();
                //};
                //Task task = taskFactory.StartNew(act);//启动线程  开启一个task
                //taskList.Add(task);
           
                //Task task1 = taskFactory.StartNew(TotalLabel);//启动线程  开启一个task
                //taskList.Add(task1);
                //taskList.Add(taskFactory.ContinueWhenAny(taskList.ToArray(), t =>
                //{
                    //wde.Close();
                    TotalLabel();
                    CreateTable();
                    TableJoin(labelnos);
                    ShowReport();
                //}));
             
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

          
        }
        /// <summary>
        /// 取得当天的所有瓶签
        /// </summary>
        private void TotalLabel()
        {
            string sql = string.Format("select *from V_IVRecordPrintPreview where DATEDIFF(DD,InfusionDT,'{0}')=0", this.date);
            LabelTol = db.GetPIVAsDB(sql).Tables[0];

            string sql1 =string.Format(" select *from V_PlusCondition where DATEDIFF(dd, InfusionDT,'{0}')=0 ",this.date);
            DrugPlusCondition = db.GetPIVAsDB(sql1).Tables[0];
        }
     
        private void ShowReport()
        {
            report.Preview = previewControlFR;
            report.Load(".\\Crystal\\PrintPreview.frx");
            report.GetParameter("user").Value = "9999";
            report.GetParameter("total").Value =11;
            report.RegisterData(dtOuvia, "ouvia");
            (report.FindObject("Data1") as FastReport.DataBand).DataSource = report.GetDataSource("ouvia");
            report.Show();
        
        }
      
        /// <summary>
        /// 设计报表所需字段
        /// </summary>
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

            dtOuvia.Columns.Add("pz_id", typeof(string));
            dtOuvia.Columns.Add("pz_name", typeof(string));
            dtOuvia.Columns.Add("dabao_id", typeof(string));
            dtOuvia.Columns.Add("dabao_name", typeof(string));
            dtOuvia.Columns.Add("paiyaoPrint", typeof(string));
            dtOuvia.Columns.Add("paizhiPrint", typeof(string));
            dtOuvia.Columns.Add("dabaoPrint", typeof(string));

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
        /// 显示报表
        /// </summary>
        /// <param name="ds"></param>
        private void TableJoin(string labelnos1)
        {
            //report.Clear();
            //previewControlFR.Clear();
            string[] labelno = labelnos1.Split(',');
           
            for (int i = 0; i < labelno.Length; i++)
            {              
                    dtMed.Rows.Clear();

                    string DrugPlusConditionName = string.Empty;
                    foreach (DataRow drs in DrugPlusCondition.Select(string.Format("LabelNo ='{0}'", labelno[i])))
                    {
                        DrugPlusConditionName = DrugPlusConditionName + drs[0].ToString().Trim() + ",";
                    }
                DataTable dt = LabelTol.Clone();
                foreach (DataRow drs in LabelTol.Select("LabelNo =" + labelno[i]))
                {
                    dt.Rows.Add(drs.ItemArray);
                }
                string xml = string.Empty;
                foreach (DataRow drss in dt.Rows)
                {
                    dtMed.Rows.Add(drss["DrugName"].ToString().Replace('[', '(').Replace(']', ')'), drss["spec"].ToString().Replace('[', '(').Replace(']', ')').Trim(), (drss["dosage"].ToString().Contains(".") ? drss["dosage"].ToString().TrimEnd('0').TrimEnd('.') : drss["dosage"].ToString().Trim()) + drss["DosageUnit"].ToString().Trim(), Math.Round(double.Parse(drss["DosageSums"].ToString()), 3).ToString(), drss["FormUnit"].ToString(), drss["Remark7"].ToString().Trim(), drss["Remark8"].ToString(), drss["Remark9"].ToString(), drss["Remark10"].ToString(), Equals(drss["PiShi"], true) ? "要皮试" : string.Empty);
                }
                using (System.IO.StringWriter sw = new System.IO.StringWriter())
                {
                    dtMed.WriteXml(sw, XmlWriteMode.WriteSchema, false);
                    xml = sw.ToString();
                }
                DataRow dr = dt.Rows[0];
                dtOuvia.Rows.Add(dr["PatientCode"].ToString().Trim()
                                  , dr["PatName"].ToString().Trim()
                                  , dr["BedNo"].ToString().Trim(), dr["LabelNo"].ToString().Trim()
                                  , i.ToString(), string.Empty
                                  , dr["WardName"].ToString().Trim()
                                  , dr["WardSimName"].ToString().Trim()
                                  , dr["Batch"].ToString().Trim()
                                  , dr["Age"].ToString().Trim()
                                  , dr["AgeSTR"].ToString().Trim()
                                  , dr["CaseID"].ToString().Trim()
                                  , dr["FreqCode"].ToString().Trim()
                                  , dr["UsageCode"].ToString().Trim()
                                  , "", "", ""
                                  , DrugPlusConditionName
                                  , dr["tod"].ToString(), dr["FreqNum"].ToString()
                                  , DateTime.Now.ToString("yyyyMMdd")
                                  , (dr["sex"].ToString().Trim() == "1" ? "男" : "女")
                                  , dr["CpUser"].ToString()

                                  , dr["pz_id"].ToString()
                                  , dr["pz_name"].ToString()
                                  , dr["dabao_id"].ToString()
                                  , dr["dabao_name"].ToString()
                                  , dr["paiyaoPrint"].ToString()
                                  , dr["paizhiPrint"].ToString()
                                  , dr["dabaoPrint"].ToString()

                                  , dr["Remark1"].ToString()
                                  , dr["Remark2"].ToString()
                                  , dr["Remark3"].ToString()
                                  , dr["Remark4"].ToString()
                                  , dr["Remark5"].ToString()
                                  , dr["Remark6"].ToString()
                                  , dr["DeskNo"].ToString()
                                  , xml
                                  );
              
            }
            //st.Stop();        
        }
  
    }
}
