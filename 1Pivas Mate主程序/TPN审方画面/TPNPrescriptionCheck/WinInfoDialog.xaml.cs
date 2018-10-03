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
using LiveCharts;
using LiveCharts.Wpf;

namespace TPNReview
{
    /// <summary>
    /// DialogWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WinInfoDialog : Window
    {
        private Action<int, string, string> onNewCustody = null;  //有新监护,刷新父界面
        private DateTime chkTime;

        public WinInfoDialog()
        {
            InitializeComponent();
        }

        public string PatientCode { get; set; }

        /// <summary>
        /// 加载检查结果
        /// </summary>
        public void showCheckResult(string _rcdNo, string _type, string _dt,
            Action<int, string, string> _onCustody=null)
        {
            this.Title = "患者检查记录";
            this.onNewCustody = _onCustody;
            this.chkTime = Convert.ToDateTime(_dt);

            lbChkResult.Visibility = Visibility.Collapsed;
            chtLine.Visibility = Visibility.Collapsed;
            miCustody.Visibility = Visibility.Visible;
            lvChkResult.Visibility = Visibility.Visible;

            string sql = "";

            if (WinAddCheck.CT_LISCHK.Equals(_type))
                sql = SQL.SEL_LISCHK_BYNO;

            else if (WinAddCheck.CT_BODYCHK.Equals(_type))
                sql = SQL.SEL_BODYCHK_BYNO;

            else if (WinAddCheck.CT_VIRUSCHK.Equals(_type))
                sql = SQL.SEL_VIRUSCHK_BYNO;

            else
                return;

            sql = string.Format(sql, _dt.Substring(0, 10), _rcdNo);

            IDataReader dr = null;
            int i = 1;

            if (AppConst.db.GetRecordSet(sql, ref dr))
            {
                while (dr.Read())
                    lvChkResult.Items.Add(new CheckResult(i++, dr["ResultID"].ToString(), dr["Code"].ToString(),
                        dr["Name"].ToString(), BLPublic.Utils.trimZero(dr["ResultValue"].ToString()), 
                        dr["ResultUnit"].ToString(), dr["Domain"].ToString(), dr["ValueDrect"].ToString()));

                dr.Close();
            }
            else
                BLPublic.Dialogs.Error("加载检查内容失败:" + AppConst.db.Error);
        }
        
            /// <summary>
        /// 加载TPN审方结果
        /// </summary>
        /// <param name="_recipeID"></param>
        public void showTPNChkResult(string _recipeID)
        {
            this.Title = "TPN审方记录";

            lvChkResult.Visibility = Visibility.Collapsed;
            chtLine.Visibility = Visibility.Collapsed;
            lbChkResult.Visibility = Visibility.Visible;

            recipemonitorlist.RecipeTPNCheck.loadTPNCheck(lbChkResult, AppConst.db, _recipeID);
        }

        public void showChartLine(string _paitentCode, int _kdItemID, string _name)
        {
            this.Title = _name + " 趋势图";
            lvChkResult.Visibility = Visibility.Collapsed;
            lbChkResult.Visibility = Visibility.Collapsed;
            chtLine.Visibility =  Visibility.Visible;

            DataTable tbl = new DataTable();
            if (!AppConst.db.GetRecordSet(string.Format(recipemonitorlist.SQL.SEL_PN_CHK_RESULT_BYKDID,
                                          _paitentCode, _kdItemID), ref tbl))
            {
                BLPublic.Dialogs.Error("加载营养检查结果失败:" + AppConst.db.Error);
                return;
            }

            if (0 == tbl.Rows.Count)
                return;

            TPNLISResultModel model = new TPNLISResultModel();
            model.Labels = new string[tbl.Rows.Count]; 
            
            ChartValues<double> val = new ChartValues<double>();
            int i = 0;
            double d = 0;

            tbl.DefaultView.Sort = "CheckTime";
            //填充数据  
            foreach (DataRow row in tbl.DefaultView.ToTable().Rows)
            {
                model.Labels[i++] = Convert.ToDateTime(row["CheckTime"].ToString()).ToString("M月d日");
                if (double.TryParse(row["ResultValue"].ToString(), out d))
                    val.Add(d); 
                else 
                    val.Add(0); 
            }

            tbl.Clear();  

            model.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = _name,
                    Values = val,
                    DataLabels = true
                }
            };
             
            chtLine.DataContext = model;
        }

        private void miCustody_Click(object sender, RoutedEventArgs e)
        { 
            if (0 == lvChkResult.SelectedItems.Count)
            {
                BLPublic.Dialogs.Alert("请选择要监护的项目");
                lvChkResult.Focus();
                return;
            }

            if (!WinCustodyEdit.chkWin(false))
                return;

            CheckResult cr = null;
            if (null == WinCustodyEdit.OnSetObject)
            {
                if (!WinCustodyEdit.TPNCustodyIntervene(this.PatientCode, false,
                                                    (id, objStr, desc) =>
                                                    {
                                                        if (null != this.onNewCustody)
                                                            this.onNewCustody(id, objStr, desc);
                                                    }))
                    return;
            }
             
            foreach (object o in lvChkResult.SelectedItems)
            {
                cr = (CheckResult)o;
                WinCustodyEdit.OnSetObject(WinCustodyEdit.OBJTYP_LIS, cr.ItemCode, cr.ItemName, cr.Value + cr.Unit,
                    this.chkTime);
            } 
        }
    }


    /// <summary>
    /// TPN检查值记录
    /// </summary>
    class TPNLISResultModel
    {
        public SeriesCollection Series { get; set; } 
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get { return value => value.ToString(); } }
    }
}
