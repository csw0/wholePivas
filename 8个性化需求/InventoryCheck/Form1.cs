using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HisInventoryCheck;
using PIVAsCommon.Helper;

namespace InventoryCheck
{
    public partial class Form1 : Form
    {
        DB_Help db = new DB_Help();
        sql mysql = new sql();
        string demployname = string.Empty;
        string DemployId = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }
        public Form1(string demployId)
        {
            InitializeComponent();
            this.DemployId = demployId;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
            //label1.Visible = false;
            DataSet ds = db.GetPIVAsDB("select DEmployeeName from DEmployee where DEmployeeID='" + this.DemployId + "'");
            if (ds != null && 0 < ds.Tables.Count && ds.Tables[0].Rows.Count > 0)
            {
                this.demployname=ds.Tables[0].Rows[0][0].ToString();
            }

        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                //label1.Visible = false;
            }
            else
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
                //label1.Visible = true;
            }
        }
        /// <summary>
        /// 得到当前的药品编码
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string GetDrugCode(DataTable dt)
        {
            string DrugCodes = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DrugCodes = DrugCodes +"'"+ dt.Rows[i]["drugcode"].ToString()+"',";         
            }
            DrugCodes = DrugCodes.TrimEnd(',');
                return DrugCodes;        
        }
        /// <summary>
        /// 打印当日
        /// </summary>
        private void  UsedDrugCount()
        {  DataSet ds=new DataSet();
            DataTable dt1 = new DataTable();
            DataTable dt2 = GetHisCount("");//His数据
            try
            {
                string drugCodes = GetDrugCode(dt2);
             
                if (drugCodes != "")
                {
                    ds = db.GetPIVAsDB(mysql.GetDayDrug(drugCodes));        
                }
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dt1 = ds.Tables[0];    //我们的数据         
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        DataRow[] dr = dt2.Select("drugcode='" + dt1.Rows[i]["DrugCode"].ToString() + "'");
                        if (dr.Length > 0)
                        {                         
                          dt1.Rows[i]["hiscount"] = dr[0].ItemArray[1].ToString();
                          dt1.Rows[i]["scount"] = (Convert.ToInt32(dt1.Rows[i]["hiscount"].ToString()) - Convert.ToInt32(dt1.Rows[i]["dcount"].ToString()));
                        }
                     
                    }

                    printer2(dt1);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
                
        }
        /// <summary>
        /// 调用His接口 
        /// </summary>
        /// <param name="DrugCodes"></param>
        /// <returns></returns>
        private DataTable GetHisCount(string DrugCodes)
        {
            HisInventory hi = new HisInventory();
            DataTable dt = hi.GetHis(DrugCodes);
           
          return dt;
        }
        /// <summary>
        /// 获取药品的历史数据
        /// </summary>
        private void  GetGrugHistory()
        {
            DataTable dt1 = new DataTable();
            DataSet ds = db.GetPIVAsDB(mysql.GetHistoyDrug(dateTimePicker1.Value.ToString(), dateTimePicker2.Value.ToString()));
            if (ds != null && ds.Tables.Count > 0)
            {
                dt1 = ds.Tables[0];
                printer1(dt1);
            }
        }
        /// <summary>
        /// 打印历史数据 
        /// </summary>
        /// <param name="dt"></param>
        private void printer1(DataTable dt)
        {
            previewQD.Visible = true;
            report.Clear();
            report.Preview = previewQD;
            report.Load(".\\Crystal\\InventoryUsed.frx");
            report.GetParameter("PrintDT").Value = dateTimePicker1.Value.ToString("yyyy-MM-dd")+" - "+dateTimePicker2.Value.ToString("yyyy-MM-dd");
            report.GetParameter("Total").Value = dt.Rows.Count;
            report.GetParameter("DemployName").Value = this.demployname;
            report.RegisterData(dt, "dt");
            ((report.FindObject("Data1")) as FastReport.DataBand).DataSource = report.GetDataSource("dt");
            report.Show();
        
        }
        /// <summary>
        /// 打印当日数据
        /// </summary>
        /// <param name="dt"></param>
        private void printer2(DataTable dt)
        {
            previewQD.Visible = true;
            report.Clear();
            report.Preview = previewQD;
            report.Load(".\\Crystal\\InventoryTodayUsed.frx");
            report.GetParameter("PrintDT").Value = DateTime.Now.AddDays(1).ToString("");
            report.GetParameter("Total").Value = dt.Rows.Count;
            report.GetParameter("DemployName").Value = this.demployname;
            report.RegisterData(dt, "dt");
            ((report.FindObject("Data1")) as FastReport.DataBand).DataSource = report.GetDataSource("dt");
            report.Show();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            //string a = mysql.GetDayDrug("");
            if (comboBox1.SelectedIndex == 0)
            {
                UsedDrugCount();
            }
            else
            {
                GetGrugHistory();
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "药品名/药品编码")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "药品名/药品编码";
                textBox1.ForeColor = Color.Silver;
            }
        }

    }
}
