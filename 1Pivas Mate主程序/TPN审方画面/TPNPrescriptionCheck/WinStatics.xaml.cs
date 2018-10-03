using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TPNReview
{
    /// <summary>
    /// StatWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WinStatics : Window
    {
        private DataTable tblST = null;
        private Dictionary<string, string> dicEmp = null; //员工编码-名称

        public WinStatics()
        {
            InitializeComponent();

            dpStart.SelectedDate = Convert.ToDateTime(DateTime.Today.ToString("yyyy-MM-01"));
            dpEnd.SelectedDate = DateTime.Today;
            cbbSTType.Items.Add("月");
            cbbSTType.Items.Add("季度");
            cbbSTType.Items.Add("年");
            cbbSTType.Items.Add("日");
            cbbSTType.SelectedIndex = 0;

            this.tblST = new DataTable();
            this.tblST.Columns.Add("STDate", typeof(string));
            this.tblST.Columns.Add("EmpCode", typeof(string));
            this.tblST.Columns.Add("EmpName", typeof(string));
            this.tblST.Columns.Add("OrdersNum", typeof(int));
            this.tblST.Columns.Add("NoteNum", typeof(int));
            this.tblST.Columns.Add("CustomChkNum", typeof(int));
            this.tblST.Columns.Add("CustodyNum", typeof(int));
            this.tblST.Columns.Add("InterveneNum", typeof(int));
            this.tblST.PrimaryKey = new DataColumn[] { this.tblST.Columns["STDate"], this.tblST.Columns["EmpCode"] };

            lvStat.ItemsSource = this.tblST.DefaultView;

            initEmpName();
        }

        private bool initEmpName()
        {
            IDataReader idr = null;
            if (!AppConst.db.GetRecordSet("SELECT e.EmployeeCode, e.EmployeeName FROM hospitaldata.dbo.DEmployee e " +
                    "WHERE EXISTS(SELECT 1 FROM DTCOrganizationMembers om WHERE om.EmployeeCode=e.EmployeeCode)", ref idr))
            {
                BLPublic.Dialogs.Error("加载员工信息失败:" + AppConst.db.Error);
                return false;
            }

            if (null == this.dicEmp)
                this.dicEmp = new Dictionary<string, string>(16);
            else
                this.dicEmp.Clear();

            BLPublic.BLDataReader bldr = new BLPublic.BLDataReader(idr);
            while (bldr.next())
            {
                this.dicEmp.Add(bldr.getString("EmployeeCode"), bldr.getString("EmployeeName"));
            }
            bldr.close();
            return true;
        }

        private string getSTDDate(DateTime _dt)
        {
            if (_dt <= DateTime.MinValue)
                return "";

            string stTyp = cbbSTType.Text;
            if ("年".Equals(stTyp))
                return _dt.ToString("yyyy年");

            if ("季度".Equals(stTyp))
            {
                int m = _dt.Month;
                if (1 <= m && m <=3)
                    return _dt.Year.ToString() + "年1季度";
                else if (4 <= m && m <= 6)
                    return _dt.Year.ToString() + "年2季度";
                else if (7 <= m && m <= 9)
                    return _dt.Year.ToString() + "年3季度";
                else
                    return _dt.Year.ToString() + "年4季度";
            }

            if ("月".Equals(stTyp))
                return _dt.ToString("yyyy年M月");

            return _dt.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 根据统计方式，自动选择日期范围
        /// </summary>
        private void setDate()
        {
            string stTyp = cbbSTType.Text;
            if ("年".Equals(stTyp))
            {
                dpStart.SelectedDate = Convert.ToDateTime(dpStart.SelectedDate.Value.ToString("yyyy-01-01"));
                if (dpEnd.SelectedDate.Value.Year >= DateTime.Today.Year)
                    dpEnd.SelectedDate = DateTime.Today;
                else
                    dpEnd.SelectedDate = Convert.ToDateTime(dpEnd.SelectedDate.Value.ToString("yyyy-12-31"));
            }
            else if ("季度".Equals(stTyp))
            {
                int m = dpStart.SelectedDate.Value.Month;
                if (m <= 3)
                    m = 1;
                else if (m <= 6)
                    m = 4;
                else if (m <= 9)
                    m = 7;
                else
                    m = 10;

                dpStart.SelectedDate = Convert.ToDateTime(string.Format("{0}-{1}-01", dpStart.SelectedDate.Value.Year, m));

                m = dpEnd.SelectedDate.Value.Month;
                if (m <= 3)
                    m = 3;
                else if (m <= 6)
                    m = 6;
                else if (m <= 9)
                    m = 9;
                else
                    m = 12;

                DateTime d = Convert.ToDateTime(string.Format("{0}-{1}-01", dpEnd.SelectedDate.Value.Year, m));
                d = d.AddMonths(1);
                dpEnd.SelectedDate = d.AddDays(-1);
            }

            else if ("月".Equals(stTyp))
            {
                dpStart.SelectedDate = Convert.ToDateTime(dpStart.SelectedDate.Value.ToString("yyyy-MM-01"));
                if ((dpEnd.SelectedDate.Value.Year <= DateTime.Today.Year) &&
                    (dpEnd.SelectedDate.Value.Month < DateTime.Today.Month))
                    dpEnd.SelectedDate = Convert.ToDateTime(dpEnd.SelectedDate.Value.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1);
                else
                    dpEnd.SelectedDate = DateTime.Today;
            }
             
        }

        private void btnStat_Click(object sender, RoutedEventArgs e)
        {
            this.tblST.Clear();

            lvStat.Items.Refresh();
            string d1 = dpStart.SelectedDate.Value.ToString("yyyy-MM-dd");
            string d2 = dpEnd.SelectedDate.Value.ToString("yyyy-MM-dd");

            string sql = string.Format(SQL.SEL_CUSTODY_ST, d1, d2);
            sql += " UNION ALL ";
            sql += string.Format(SQL.SEL_INTERVENE_ST, d1, d2);
            sql += " UNION ALL ";
            sql += string.Format(SQL.SEL_PM_NOTE_ST, d1, d2);


            IDataReader idr = null;
            if (!AppConst.db.GetRecordSet(sql, ref idr))
            {
                BLPublic.Dialogs.Error("加载监护失败:" + AppConst.db.Error);
                return;
            }
             
            BLPublic.BLDataReader bldr = new BLPublic.BLDataReader(idr);

            string date = "";
            DataRow nrow = null;
            while (bldr.next())
            {
                date = getSTDDate(bldr.getDateTime("Time"));
                nrow = this.tblST.Rows.Find(new object[] { date, bldr.getString("EmpCode") });
                if (null == nrow)
                {
                    nrow = this.tblST.NewRow();
                    nrow["STDate"] = date;
                    nrow["EmpCode"] = bldr.getString("EmpCode");
                    if (this.dicEmp.ContainsKey(bldr.getString("EmpCode")))
                        nrow["EmpName"] = this.dicEmp[bldr.getString("EmpCode")];
                    else
                        nrow["EmpName"] = bldr.getString("EmpCode");
                     
                    nrow["InterveneNum"] = 0;
                    nrow["CustodyNum"] = 0; 
                    nrow["NoteNum"] = 0;  
                    this.tblST.Rows.Add(nrow);
                }

                if ("intervene".Equals(bldr.getString("Type")))
                    nrow["InterveneNum"] = Convert.ToInt32(nrow["InterveneNum"].ToString()) + 1;
                else if ("custody".Equals(bldr.getString("Type")))
                    nrow["CustodyNum"] = Convert.ToInt32(nrow["CustodyNum"].ToString()) + 1;
                else if ("note".Equals(bldr.getString("Type")))
                    nrow["NoteNum"] = Convert.ToInt32(nrow["NoteNum"].ToString()) + 1; 
            }
            bldr.close();

            this.tblST.DefaultView.Sort = "STDate";
            lvStat.Items.Refresh();
        } 
    }
}
