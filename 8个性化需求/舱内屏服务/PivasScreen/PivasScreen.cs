using System;
using System.Data;
using System.ServiceProcess;
using System.Text;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Configuration;

namespace PivasScreen
{
    public partial class PivasScreen : ServiceBase
    {
        public static DataSet PatientDS = new DataSet();
        public static DataSet DrugPZInfor = new DataSet();
        private ServiceHost sh;
        private bool tf = true;
        string timeSet = ConfigurationManager.AppSettings["TmpSet"].ToString();
        string connstr = ConfigurationManager.AppSettings["ConnenctionString"].ToString();
        string xmlType= ConfigurationManager.AppSettings["XMLType"].ToString();
        public PivasScreen()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                timer1.Interval = Convert.ToInt32(timeSet);
                timer1.Start();
                sh = new ServiceHost(typeof(Service1));
                sh.Open();
            }
            catch (Exception ex)
            {
                string stmp = this.GetType().Assembly.Location;
                stmp = stmp.Substring(0, stmp.LastIndexOf('\\'));//删除文件名     
                System.IO.File.AppendAllText(stmp + "\\PivasScreen.txt", ex.ToString()+"    "+DateTime.Now.ToString() + "\r\n");
            }
        }

        protected override void OnStop()
        {
            try
            {              
                timer1.Stop();
                sh.Close();
            }
            catch (Exception ex)
            {
                string stmp = this.GetType().Assembly.Location;
                stmp = stmp.Substring(0, stmp.LastIndexOf('\\'));//删除文件名     
                System.IO.File.AppendAllText(stmp + "\\PivasScreen.txt", ex.ToString() + "    " + DateTime.Now.ToString() + "\r\n");
            }

        }

        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (tf)
            {               
                tf = false;               
                try
                {
                    if (xmlType == "1")
                    {
                        SpecialLabel();
                    }
                    else
                    {
                        GetLabelInfor();
                        GetDrugInfor();
                    }
                }
                catch (Exception ex)
                {
                    string stmp = this.GetType().Assembly.Location;
                    stmp = stmp.Substring(0, stmp.LastIndexOf('\\'));//删除文件名     
                    System.IO.File.AppendAllText(stmp + "\\PivasScreen.txt", ex.ToString() + "    " + DateTime.Now.ToString() + "\r\n");
                }
                tf = true;
            }
        }

        private void SpecialLabel()
        {
            string sql = " select *from ScreenDetail";
            PatientDS = ExecuteDataTable(sql.ToString());

        
        }

        //20150825100112    
        private void GetLabelInfor()
        {
            StringBuilder str = new StringBuilder();
            str.Append("select distinct dw.WardName,pa.BedNo,pa.PatCode,pa.PatName,pa.Age, [IP],[DemployeeID],[DemployeeName],sd.[DeskNo],sd.LabelNo, ");
            str.Append("sd.[Count],[Result],[Msg],sd.[Remark1],sd.[Remark2],sd.[Remark3],sd.[Remark4] , ivd.DrugCode,ivd.DrugName,ivd.Spec ");
            str.Append(",CONVERT(float, ivd.Dosage) as Dosage,ivd.DosageUnit,ivd.DgNo,dd.UniPreparationID, iv.LabelNo from IVRecord iv ");
            str.Append("  left join IVRecordDetail ivd on ivd.IVRecordID=iv.IVRecordID ");
            str.Append("left join DDrug dd on dd.DrugCode=ivd.DrugCode ");
            str.Append(" left join Patient pa on pa.PatCode=iv.PatCode ");
            str.Append("left join DWard dw on dw.WardCode=pa.WardCode ");
            str.Append(" inner join ScreenDetail sd on sd.LabelNo=iv.LabelNo");
            str.Append(" ");

            str.Append(" ");
            PatientDS = ExecuteDataTable(str.ToString());
        }

        /// <summary>
        /// 获取药品配置信息
        /// </summary>
        private void GetDrugInfor()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("    SELECT distinct dd.UniPreparationID, pm.PreparationDesc FROM KD0102.dbo.PreparationMethods pm   ");
            str.AppendLine("  left join KD0102.dbo.PreparationMethod2Med pmm on pmm.PreparationMethodsID=pm.PreparationMethodsID  ");
            str.AppendLine("  INNER JOIN kd0100.dbo.[UniPrep-Medicine] um ON um.MedicineID=pmm.MedicineID  ");
            str.AppendLine("  inner join DDrug dd on dd.UniPreparationID=um.UniPreparationID ");
            DrugPZInfor = ExecuteDataTable(str.ToString());
        }

        private DataSet ExecuteDataTable(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset);
                    conn.Close();
                    return dataset;
                }
            }
        }
    }
}
