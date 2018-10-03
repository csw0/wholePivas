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

namespace recipemonitorlist
{
    /// <summary>
    /// WinTPNLISChk.xaml 的交互逻辑
    /// </summary>
    public partial class WinTPNLISChk : Window
    {
        private Dictionary<int, string> dicTPNLISChk = null;
        private DataTable tblTPNLISChk = null;
        private BLPublic.DBOperate db = null;
        private string patientCode = null;

        public WinTPNLISChk()
        {
            InitializeComponent();
        }

        public void init(BLPublic.DBOperate _db, string _patientCode)
        {
            this.db = _db;
            this.patientCode = _patientCode;
        }

        /// <summary>
        /// 初始化TPN检查项目
        /// </summary>
        private void initTPNLIS()
        {
            IDataReader dr = null;
            if (!this.db.GetRecordSet(SQL.SEL_PN_CHKITEM, ref dr))
            {
                BLPublic.Dialogs.Error("获取营养检查项目失败:" + this.db.Error);
                return;
            }

            if (null == this.dicTPNLISChk)
                this.dicTPNLISChk = new Dictionary<int, string>(32);

            else
                this.dicTPNLISChk.Clear();

            BLPublic.BLDataReader dldr = new BLPublic.BLDataReader(dr);
            while (dldr.next())
            {
                if (!string.IsNullOrWhiteSpace(dldr.getString("HISCheckItemUnit")))
                    this.dicTPNLISChk.Add(dldr.getInt("KDCheckItemID"), string.Format("{0} ({1})", dldr.getString("HISCheckItemName"), dldr.getString("HISCheckItemUnit")));
                else
                    this.dicTPNLISChk.Add(dldr.getInt("KDCheckItemID"), dldr.getString("HISCheckItemName"));
            }

            dldr.close(); 
        }
        
        /// <summary>
        /// 加载TPN检查项目结果
        /// </summary>
        private void loadTPNLISChk()
        {
            if (null == this.dicTPNLISChk)
                return;

            grdPNLISChk.ItemsSource = null;

            if (null == this.tblTPNLISChk)
            {
                this.tblTPNLISChk = new DataTable();
                this.tblTPNLISChk.Columns.Add("项目", typeof(string));
            }
            else
            {
                this.tblTPNLISChk.Clear();
                for (int i = this.tblTPNLISChk.Columns.Count - 1; i >= 1; i--)
                    this.tblTPNLISChk.Columns.RemoveAt(1);
            }

            DataTable tbl = new DataTable();
            if (!this.db.GetRecordSet(string.Format(SQL.SEL_PN_CHK_RESULT, this.patientCode), ref tbl))
            {
                BLPublic.Dialogs.Error("加载营养检查结果失败:" + this.db.Error);
                return;
            }

            if (0 == tbl.Rows.Count)
                return;

            //获取最小日期
            DateTime minDate = DateTime.Today;
            object o = tbl.Compute("Min(CheckTime)", string.Empty);
            if (null != o)
                minDate = Convert.ToDateTime(o.ToString()).Date;

            //增加日期列
            DateTime colDate = minDate;
            while (colDate <= DateTime.Today)
            {
                this.tblTPNLISChk.Columns.Add(colDate.ToString("M月d日"), typeof(string)); //列名不能有"/"和".",否则无法显示
                colDate = colDate.AddDays(1);
            }

            //this.tblTPNLISChk.Columns.Add(minDate.ToString("今日"), typeof(string));

            //填充数据 
            DataRow[] rows = null;
            DataRow newRow = null;
            int colLen = this.tblTPNLISChk.Columns.Count;
            foreach (KeyValuePair<int, string> kv in this.dicTPNLISChk)
            {
                newRow = this.tblTPNLISChk.NewRow();
                newRow["项目"] = kv.Value;

                for (int i = colLen - 1; i >= 1; i--)
                    newRow[i] = "";

                rows = tbl.Select("KDCheckItemID=" + kv.Key.ToString());
                if ((null != rows) && (0 < rows.Length))
                    foreach (DataRow r in rows)
                    {
                        newRow[Convert.ToDateTime(r["CheckTime"].ToString()).ToString("M月d日")] =
                            BLPublic.Utils.trimZero(r["ResultValue"].ToString());
                    }

                this.tblTPNLISChk.Rows.Add(newRow);
            }

            tbl.Clear();
            grdPNLISChk.ItemsSource = this.tblTPNLISChk.DefaultView;
            grdPNLISChk.Columns[0].Width = 160;
            //grdPNLISChk.Columns[0].CellStyle.Setters["FontWeight"] = "Bold";
            //grdPNLISChk.Columns[0].CellStyle.Setters["BackColor"] = SystemColors.Control;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            initTPNLIS();
            loadTPNLISChk();
        }
    }
}
