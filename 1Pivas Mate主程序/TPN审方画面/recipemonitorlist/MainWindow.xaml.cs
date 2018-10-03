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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace recipemonitorlist
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private BLPublic.DBOperate db = null;
        private TPNItemView tpnView = null;
        private DataTable tblData = null; 

        public static string LoginEmpCode = "";

        public MainWindow()
        {
            InitializeComponent();

            cbbStatus.Items.Add(new BLPublic.CodeNameItem("ALL", "<全部>"));
            cbbStatus.Items.Add(new BLPublic.CodeNameItem("passed", "合格"));
            cbbStatus.Items.Add(new BLPublic.CodeNameItem("notpass", "不合格"));

            cbbStatus.SelectedIndex = 0;
        }


        /// <summary>
        /// 加载医嘱
        /// </summary>
        private void loadOrders()
        {
            this.clear();
            grdOrders.ItemsSource = null;
            txtNum.Text = "";

            if (null == this.tblData)
                this.tblData = new DataTable();
            else
                this.tblData.Clear();

            if (!this.db.GetRecordSet(SQL.SEL_NOCHK_TPN_ORDERS, ref this.tblData))
            {
                BLPublic.Dialogs.Error("加载医嘱失败:" + this.db.Error);
                return;
            }

            if (!this.tblData.Columns.Contains("IsChecked"))
            {
                DataColumn dc = new DataColumn("IsChecked", typeof(bool));
                dc.DefaultValue = false;
                this.tblData.Columns.Add(dc);
            }

            listOrders();
        }
        
        private void listOrders()
        {
            txtNum.Text = "";
            if (null == this.tblData)
                return;

            string st = "";
            if (null != cbbStatus.SelectedItem)
                st = ((BLPublic.CodeNameItem)cbbStatus.SelectedItem).Code;

            if ("notpass".Equals(st))
                this.tblData.DefaultView.RowFilter = "TPNLevel>0";
            else if ("passed".Equals(st))
                this.tblData.DefaultView.RowFilter = "TPNLevel=0";
            else
                this.tblData.DefaultView.RowFilter = "";

            this.tblData.DefaultView.Sort = "StartTime";
            grdOrders.ItemsSource = this.tblData.DefaultView;
             
            txtNum.Text = string.Format("不合格 {0} 条", this.tblData.Select("TPNLevel>0").Length);

            int noChk = this.tblData.Select("TPNLevel<0").Length;
            if (0 < noChk)
                txtNum.Text += ", 未审 " + noChk.ToString() + " 条";

            txtNum.Text += string.Format(" 共 {0} 条", grdOrders.Items.Count);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.db = new BLPublic.DBOperate(AppDomain.CurrentDomain.BaseDirectory + "bl_server.lcf", "CPMATE");
            if (!this.db.IsConnected)
            {
                BLPublic.Dialogs.Error("连接服务器失败:" + this.db.Error);
                return;
            }

            this.tpnView = new TPNItemView(this.db, lvTPNItems);
            this.loadOrders();
        }

        private void clear()
        {
            pnlPatient.DataContext = null;
            pnlOrders.DataContext = null;
            lvOrdersDrug.ItemsSource = null;
            lvTPNItems.Items.Clear();

            this.tpnView.clearView();
        }

        private void grdOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.clear();

            if (null == grdOrders.SelectedItem)
                return;

            DataRowView drv = (DataRowView)grdOrders.SelectedItem;

            string rcpID = drv.Row["RecipeID"].ToString();
            string pCode = drv.Row["PatientCode"].ToString();

            IDataReader dr = null;
            if (!this.db.GetRecordSet(string.Format(SQL.SEL_PNTINFO, pCode), ref dr))
            {
                BLPublic.Dialogs.Error("加载患者信息失败:" + this.db.Error);
                return;
            }

            PatientInfo patient = null;
            BLPublic.BLDataReader bldr = new BLPublic.BLDataReader(dr);
            if (bldr.next())
            {
                patient = new PatientInfo
                {
                    PatientCode = pCode,
                    WardName = bldr.getString("DeptName"),
                    BedNo = bldr.getString("BedNo"),
                    HospitalNo = bldr.getString("HospitalNo"),
                    PatientName = bldr.getString("PatientName"),
                    Age = BLPublic.Utils.getAge(bldr.getDateTime("Birthday")),
                    Sex = "f".Equals(bldr.getString("Sex")) ? "女" : "男"
                };
            }
            bldr.close();

            pnlPatient.DataContext = patient;
            pnlOrders.DataContext = drv;
             
            DataTable tblDrugs = new DataTable();
            if (!this.db.GetRecordSet(string.Format(SQL.SEL_ORDERSDETAIL, rcpID), ref tblDrugs))
            {
                BLPublic.Dialogs.Error("加载医嘱内容信息失败:" + this.db.Error);
                return;
            }

            lvOrdersDrug.ItemsSource = tblDrugs.DefaultView;

            //RecipeTPNCheck.loadTPNCheck(lbChkResult, this.db, rcpID, txtChkResult);
            this.tpnView.showRecipeTPN(rcpID); 
        }

        /// <summary>
        /// 单个医嘱勾选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbOrdCheck_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (null == cb.Tag)
                return;

            DataRow[] rows = this.tblData.Select("RecipeID=" + cb.Tag.ToString());
            if ((null != rows) && (0 < rows.Length))
            {
                rows[0]["IsChecked"] = true == cb.IsChecked;
            }

            grdOrders.Items.Refresh();
        }

        private void SelAll_Click(object sender, RoutedEventArgs e)
        {
            bool sel = (true == ((CheckBox)sender).IsChecked);

            foreach (object dr in grdOrders.Items)
            {
                ((DataRowView)dr).Row["IsChecked"] = sel;
            }
            grdOrders.Items.Refresh();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.loadOrders();
        }

        private void btnPass_Click(object sender, RoutedEventArgs e)
        {
            if (0 == grdOrders.Items.Count)
            {
                BLPublic.Dialogs.Alert("没有医嘱.");
                grdOrders.Focus();
                return;
            }

            DataRowView drv = null;
            string rcpIDs = "";
            foreach (object dr in grdOrders.Items)
            {
                drv = (DataRowView)dr;

                if ((bool)drv.Row["IsChecked"])
                    rcpIDs += BLPublic.DBOperate.ACS(drv.Row["RecipeID"].ToString()) + ",";
            }

            if (string.IsNullOrWhiteSpace(rcpIDs))
            {
                BLPublic.Dialogs.Alert("请选择要确认的医嘱.");
                grdOrders.Focus();
                return; 
            }

            if (this.db.ExecSQL(string.Format(SQL.CMF_TPNMNT, rcpIDs.Substring(0, rcpIDs.Length-1), 
                MainWindow.LoginEmpCode)))
            {
                loadOrders();
                BLPublic.Dialogs.Info("确认成功");
            }
            else
                BLPublic.Dialogs.Error("确认失败:" + this.db.Error);
        }

        private void lvTPNItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (null == lvTPNItems.SelectedItem)
                return;

            TPNItemModel item = (TPNItemModel)lvTPNItems.SelectedItem;
            if ("_EXPAND_".Equals(item.Code))
                this.tpnView.showTPN(true);

            else if ("_COLLAPSED_".Equals(item.Code))
                this.tpnView.showTPN(false);

            else if (!string.IsNullOrWhiteSpace(item.Code) && (0 < item.ID))
            {
                if (null == grdOrders.SelectedItem)
                    return;

                DataRowView drv = (DataRowView)grdOrders.SelectedItem; 
                string rcpID = drv.Row["RecipeID"].ToString();

                tpnmonitor.frmCalDetail frmDetail = new tpnmonitor.frmCalDetail();
                frmDetail.init(this.db, rcpID, item.Code);
                frmDetail.ShowDialog();
            }
        } 

        private void btnUpQR_Click(object sender, RoutedEventArgs e)
        {
            if (null == lvOrdersDrug.ItemsSource)
            {
                BLPublic.Dialogs.Alert("请选择医嘱");
                grdOrders.Focus();
                return;
            }

            IDataReader dr = null;
            if (!this.db.GetRecordSet(SQL.SEL_HSPCODE, ref dr))
            {
                BLPublic.Dialogs.Error("读取医院信息失败:" + this.db.Error);
                return;
            }

            string hspCode = "";
            if (dr.Read())
                hspCode = dr.GetString(0);

            dr.Close();

            if (string.IsNullOrWhiteSpace(hspCode))
            {
                BLPublic.Dialogs.Error("未设置医院信息");
                return;
            }

            DataView dv= (DataView)lvOrdersDrug.ItemsSource;

            StringBuilder sb = new StringBuilder(200);
            sb.Append("http://pn.iphar.cn/u/");
            sb.Append(QREncode.encode("r" + hspCode));
            sb.Append("|");
            foreach(DataRowView r in dv)
            {
                sb.Append(r["DrugCode"].ToString());
                sb.Append("|");
                sb.Append(BLPublic.Utils.trimZero(r["Dosage"].ToString()));
                sb.Append("|"); 
            }
             
            qrcode.frmQR.showQR("医嘱信息", sb.ToString(), "微信扫描，上传医嘱信息给临床药师"); 
        }

        private void lbChkResult_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
            eventArg.RoutedEvent = UIElement.MouseWheelEvent;
            eventArg.Source = sender; 
            ((ListBox)sender).RaiseEvent(eventArg);
        }

        private void cbbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null != cbbStatus.SelectedItem)
                listOrders();
        }

        private void btnTPNLISChk_Click(object sender, RoutedEventArgs e)
        {
            if (null == grdOrders.SelectedItem)
            {
                BLPublic.Dialogs.Alert("请选择医嘱");
                grdOrders.Focus();
                return;
            } 

            DataRowView drv = (DataRowView)grdOrders.SelectedItem;
             
            WinTPNLISChk win = new WinTPNLISChk();
            win.Owner = this;
            win.init(this.db, drv.Row["PatientCode"].ToString());
            win.ShowDialog();
            win = null;
        }

        private void ReMonitor_Click(object sender, RoutedEventArgs e)
        {
            if (null == grdOrders.SelectedItem)
            {
                BLPublic.Dialogs.Alert("请选择要重审的医嘱.");
                grdOrders.Focus();
                return;
            }
             
            if (BLPublic.Dialogs.Yes != BLPublic.Dialogs.Ask("此操作将会删除现在的TPN审方结果，是否继续?"))
            {
                grdOrders.Focus();
                return;
            }

            DataRowView drv = (DataRowView)grdOrders.SelectedItem;
            string recipeID = drv.Row["RecipeID"].ToString();
            if (!this.db.ExecSQL(string.Format(SQL.DEL_TPNMNT_BYRCP, recipeID)))
            {
                BLPublic.Dialogs.Alert("删除TPN审方失败:" + this.db.Error);
                return;
            } 
             

            ((Button)sender).IsEnabled = false;

            tpnmonitor.TPNMonitor tpnMnt = new tpnmonitor.TPNMonitor();
            tpnMnt.init(this.db, LoginEmpCode);
            tpnMnt.loadConfig();
            bool rt = tpnMnt.Monitor(drv.Row["PatientCode"].ToString(), 0, "", recipeID);

            ((Button)sender).IsEnabled = true;

            if (!rt)
            {
                BLPublic.Dialogs.Error("重审失败:" + tpnMnt.getError());
                return;
            }

            tpnMnt.saveTPNValue(); 
            loadOrders();
  
            foreach(object o in grdOrders.Items)
                if (((DataRowView)o).Row["RecipeID"].ToString().Equals(recipeID))
                {
                    grdOrders.SelectedItem = o;
                    break;
                }

            BLPublic.Dialogs.Info("重审成功");
        } 
    } 

    class QREncode
    {
        private static readonly string DATACODE = "LK6JHG1FD5SAZ4XC0VB2N7MPO3IUY9TRE8WQ";

        public static string encode(string _txt)
        {
            if (string.IsNullOrWhiteSpace(_txt))
                return "";

            _txt = _txt.ToUpper();
            Random rand = new Random();
            int len = DATACODE.Length;
            int r = rand.Next(len);
            string rt = "";
            for (int i = 0; i < _txt.Length; i++)
                rt += DATACODE[(DATACODE.IndexOf(_txt[i]) + r) % len];

            rt += DATACODE[r];

            return rt;
        }
    }
}
