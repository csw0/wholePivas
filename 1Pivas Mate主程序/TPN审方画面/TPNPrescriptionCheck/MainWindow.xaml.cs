using PIVAsCommon;
using PIVAsCommon.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace TPNReview
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : UserControl, IMenuManager
    {
        private AutoResetEvent waitAutoResetEvent = new AutoResetEvent(false);//控制是否等待
        private DB_Help dbHelp = new DB_Help();
        private BLPublic.DBOperate db = null;
        private IContentPage activePage = null;
        private IContentPage pageOrders = null;
        private IContentPage pageCheck = null;
        private IContentPage pageNote = null;
        private IContentPage pageAnalysis = null;
        private IContentPage pageExport = null;
        private volatile DataSet dsAllPatient = null;//所有用tpn医嘱的患者
        private volatile DataSet dsRecentPatient = null;//最近一周用tpn医嘱的患者
        private volatile DataSet dsNoReviewTpnOfPatient = null;//未审未停营养医嘱的患者
        private volatile DataSet dsTodayTpnOfPatient = null;//获取今日营养医嘱的患者，含已审过的

        public MainWindow()
        {
            InitializeComponent();
            #region 初始化
            dpStartDate.SelectedDate = dpEndDate.SelectedDate = DateTime.Now;

            //miConfig系统设置
            if ("G".Equals(AppConst.LoginEmpRole))
                miConfig.Visibility = Visibility.Visible;
            else
                miConfig.Visibility = Visibility.Collapsed;

            miStatistics.Visibility = miConfig.Visibility; //各项统计
            miOrdersChk.Visibility = miConfig.Visibility; //医嘱审方
            miTPNMonitor.Visibility = miConfig.Visibility; //审方设置
            #endregion
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) { }

        /// <summary>
        ///  获取所有科室
        /// </summary>
        private void listWard()
        {
            IDataReader dr = null;

            if (!this.db.GetRecordSet(SQL.SEL_WARD, ref dr)) 
                return;

            cbbWard.Items.Clear();
            while (dr.Read())
                cbbWard.Items.Add(new BLPublic.CodeNameItem(dr.GetString(0), dr.GetString(1)));

            dr.Close();
        }

        /// <summary>
        /// 在主窗体加载时，不等待。
        /// </summary>
        /// <param name="bWait">默认等待</param>
        private void loadPatient(bool bWait = true)
        {
            try
            {
                waitAutoResetEvent.Reset();//初始化一下
                StartLoadPatientThread();
                if (bWait)
                    waitAutoResetEvent.WaitOne(60000);//最大等待时间30秒
                
                if (cbTodayTPN.IsChecked == true)
                    loadPatient(dsRecentPatient);
                else
                    loadPatient(dsAllPatient);
            }
            catch (Exception ex)
            {
                ShowMsgHelper.ShowError("加载患者时出错:" + ex.Message);
            }
        }

        /// <summary>
        /// 开启加载患者线程
        /// </summary>
        private void StartLoadPatientThread()
        {
            (new Thread(() =>
            {
                try
                {
                    lock (this)//不允许上次线程未结束，又开启新线程
                    {
                        if (dsAllPatient != null)
                            dsAllPatient.Dispose(); dsAllPatient = null;
                        if (dsRecentPatient != null)
                            dsRecentPatient.Dispose(); dsRecentPatient = null;
                        if (dsNoReviewTpnOfPatient != null)
                            dsNoReviewTpnOfPatient.Dispose(); dsNoReviewTpnOfPatient = null;
                        if (dsTodayTpnOfPatient != null)
                            dsTodayTpnOfPatient.Dispose(); dsTodayTpnOfPatient = null;

                        dsAllPatient = dbHelp.GetPIVAsDB("exec HospitalData.dbo.getAllTpnPatient");
                        dsRecentPatient = dbHelp.GetPIVAsDB("exec HospitalData.dbo.getRecentTpnPatient");
                        dsNoReviewTpnOfPatient = dbHelp.GetPIVAsDB("exec HospitalData.dbo.getNoReviewAndStopTpnOfPatient");
                        dsTodayTpnOfPatient = dbHelp.GetPIVAsDB("exec HospitalData.dbo.getTodayTpnOfPatient");
                        waitAutoResetEvent.Set();//恢复
                    }
                }
                catch (Exception ex)
                {
                    InternalLogger.Log.Error("加载患者线程中出错:" + ex.Message);
                }

            })).Start();//开启线程，里面用的dbHelp对象,需要配置文件IMEQPIVAs.ini
        }

        private void loadPatient(DataSet dsPatient)
        {
            lvPatient.Items.Clear();
            string ageStr = "";
            int ageMonths = 0;
            if (dsPatient != null && dsPatient.Tables.Count > 0 && dsPatient.Tables[0].Rows.Count > 0)
            {
                DateTime birthday = DateTime.Now;
                foreach (DataRow item in dsPatient.Tables[0].Rows)
                {
                    if (item["Birthday"] != DBNull.Value)//生日为空
                        birthday = DateTime.Parse(item["Birthday"].ToString());
                    ageStr = BLPublic.Utils.getAge(birthday,out ageMonths);

                    lvPatient.Items.Add(new PatientModel
                    {
                        PatientCode = item["PatientCode"].ToString().Trim(),
                        WardCode = item["DeptCode"].ToString().Trim(),
                        WardName = item["DeptName"].ToString().Trim(),
                        BedNo = item["BedNo"].ToString().Trim(),
                        HospitalNo = item["HospitalNo"].ToString().Trim(),
                        PatientName = item["PatientName"].ToString().Trim(),
                        Age = ageStr,
                        AgeMonth = ageMonths,
                        Sex = ComClass.getZhSex(item["Sex"].ToString().Trim()),
                        IsHospital = true,//csw修改，赋值为常量true，原是从数据集中获取，修改sql后不再获取
                        HadNotCheckOrders = dsNoReviewTpnOfPatient.Tables[0].Select("PatientCode = " 
                        + item["PatientCode"].ToString().Trim()).Length == 1,
                        HadTodayOrders = dsTodayTpnOfPatient.Tables[0].Select("PatientCode = "
                        + item["PatientCode"].ToString().Trim()).Length == 1,
                    });
                }
            }
            txtPatientNum.Text = "患者数:" + lvPatient.Items.Count.ToString();
        }

        private void clearPatientInfo()
        {
            pnlPatient.DataContext = null;
            txtDiagnose.Text = "";
            pnlOperate.Visibility = Visibility.Collapsed;
            //imgOutPnt.Visibility = Visibility.Collapsed;
            //imgNewChk.Visibility = Visibility.Collapsed;
            //imgNewRcp.Visibility = Visibility.Collapsed;

            if ((null != this.activePage) && (this.pageExport != this.activePage))
                this.activePage.clear(); 
        }

        private void refPatient()
        {
            this.lvPatient.Items.Refresh();
        }

        private void showPatient()
        { 
            if (null == lvPatient.SelectedItem)
                return;
             
            if (null != this.activePage)
                this.activePage.setPatient((PatientModel)lvPatient.SelectedItem);  
        }

        private List<PatientModel> getPatientLst()
        {
            List<PatientModel> rt = new List<PatientModel>(64);
            foreach (object o in lvPatient.Items)
                rt.Add((PatientModel)o);

            return rt;
        }

        /// <summary>
        /// 加载几个页面，sender的不同，操作页面不同
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Click(object sender, RoutedEventArgs e)
        { 
            this.activePage = null;
            btnTPNOrders.Background = this.Background;
            btnCheckRecord.Background = this.Background;
            btnNote.Background = this.Background;
            btnAnalysis.Background = this.Background;
            btnExport.Background = this.Background;

            if (btnTPNOrders == sender)//Tpn医嘱
            {
                if (null == this.pageOrders)
                {
                    this.pageOrders = new PageOrders();
                    this.pageOrders.init(this.refPatient); 
                }

                this.activePage = this.pageOrders;
                btnTPNOrders.Background = new SolidColorBrush(Colors.SkyBlue);
            }
            else if (btnCheckRecord == sender)//检查记录
            {
                if (null == this.pageCheck)
                {
                    this.pageCheck = new PageCheckRecord();
                    this.pageCheck.init(this.refPatient);
                }

                this.activePage = this.pageCheck;
                btnCheckRecord.Background = new SolidColorBrush(Colors.SkyBlue);
            }
            else if (btnNote == sender)
            {
                if (null == this.pageNote)
                {
                    this.pageNote = new PagePharmacistNote();
                    this.pageNote.init(this.refPatient);
                }

                this.activePage = this.pageNote;
                btnNote.Background = new SolidColorBrush(Colors.SkyBlue);
            }
            else if (btnAnalysis == sender)
            {
                if (null == this.pageAnalysis)
                {
                    this.pageAnalysis = new PageAnalysis();
                    this.pageAnalysis.init(this.refPatient);
                }

                this.activePage = this.pageAnalysis;
                btnAnalysis.Background = new SolidColorBrush(Colors.SkyBlue);
            }
            else if (btnExport == sender)
            {
                if (null == this.pageExport)
                {
                    this.pageExport = new PageExport();
                    this.pageExport.init(this.refPatient);
                }

                ((PageExport)this.pageExport).setGetPatientList(getPatientLst);
                this.activePage = this.pageExport;
                btnExport.Background = new SolidColorBrush(Colors.SkyBlue);
            }
            else
                BLPublic.Dialogs.Alert("未实现");
             
            if (null != this.activePage)
                frmBody.Content = this.activePage;

            showPatient();
        }

        private void btnSet_Initialized(object sender, EventArgs e)
        {
            ((Button)sender).ContextMenu = null;
        }

        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            this.cmSetting.PlacementTarget = (Button)sender;
            this.cmSetting.Placement = PlacementMode.Bottom;
            this.cmSetting.IsOpen = true;
        }

        private void miConfig_Click(object sender, RoutedEventArgs e)
        {
            WinConfig win = new WinConfig();
            SetWindowOwner(win);
            win.ShowDialog();
        }

        /// <summary>
        /// 设置窗体的父窗体
        /// </summary>
        private void SetWindowOwner(Window win)
        {
            HwndSource winformWindow = HwndSource.FromDependencyObject(this) as HwndSource;
            if (winformWindow != null)
                new WindowInteropHelper(win) { Owner = winformWindow.Handle };

            if (win.Owner !=null)
                AppConst.winMain = win.Owner;//赋值给保存起来
        }

        private void miTPNItem_Click(object sender, RoutedEventArgs e)
        {
            WinTPNItem win = new WinTPNItem();
            SetWindowOwner(win);
            win.ShowDialog();
        }

        private void miTPNMonitor_Click(object sender, RoutedEventArgs e)
        {
            WinMonitorSet winSet = new WinMonitorSet();
            SetWindowOwner(winSet);
            winSet.ShowDialog();
        }

        private void miTPNAlwayCheck_Click(object sender, RoutedEventArgs e)
        {
            WinMonitorCom win = new WinMonitorCom();
            SetWindowOwner(win);
            win.ShowDialog();
        }

        private void lvPatient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearPatientInfo();

            if (null == lvPatient.SelectedItem)
                return;

            PatientModel pModel = (PatientModel)lvPatient.SelectedItem;
             
            //加载患者诊断
            if ((null == pModel.Diagnose) || pModel.Diagnose.Contains("失败:"))
            {
                string dig = "";
                PageExport.getDiagnose(this.db, pModel.PatientCode, ref dig);
                pModel.Diagnose = dig; 
            }
            txtDiagnose.Text = pModel.Diagnose;

            //记载患者手术
            if ((null == pModel.Operate) || pModel.Operate.Contains("失败:"))
            {
                IDataReader dr = null;
                if (this.db.GetRecordSet(string.Format(SQL.SEL_PNTOP, pModel.PatientCode), ref dr))
                {
                    pModel.Operate = "";
                    while (dr.Read())
                    {
                        if (!string.IsNullOrWhiteSpace(pModel.Operate))
                            pModel.Operate += ",";

                        pModel.Operate += "[" + dr["StartTime"].ToString();
                        pModel.Operate += "-" + dr["EndTime"].ToString() + "]";
                        pModel.Operate += dr["OperateName"].ToString();
                    }

                    dr.Close();
                }
                else
                    pModel.Operate = "加载患者手术失败:" + this.db.Error;
            }

            pnlPatient.DataContext = pModel;

            if (!string.IsNullOrWhiteSpace(pModel.Operate))
                pnlOperate.Visibility = Visibility.Collapsed;

            if ((null != this.activePage) && (this.pageExport != this.activePage))
                this.activePage.setPatient(pModel);  
        }

        private void lvPatient_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (null == lvPatient.SelectedItem)
                return;
             
            if (this.pageExport == this.activePage)
                this.pageExport.setPatient((PatientModel)lvPatient.SelectedItem);
        }

        #region 患者查询
        private void cbQuery_Checked(object sender, RoutedEventArgs e)
        {
            pnlQuery.Visibility = Visibility.Visible; 
        }
        private void cbQuery_Unchecked(object sender, RoutedEventArgs e)
        {
            pnlQuery.Visibility = Visibility.Collapsed;
            loadPatient();
        }
        private void btnQuery_Click(object sender, RoutedEventArgs e)
        {
            string sql = "";
            if (!string.IsNullOrWhiteSpace(txtHspNo.Text))
                sql = string.Format(SQL.SEL_PATIENT_BYHSPNO, txtHspNo.Text.Trim());
            else
            {
                if (null == cbbWard.SelectedItem)
                {
                    BLPublic.Dialogs.Alert("请选择病区");
                    cbbWard.Focus();
                    return;
                }

                sql = string.Format(SQL.QRY_PATIENT, ((BLPublic.CodeNameItem)cbbWard.SelectedItem).Code,
                                    dpStartDate.SelectedDate.Value.ToString("yyyy-MM-dd"), 
                                    dpEndDate.SelectedDate.Value.ToString("yyyy-MM-dd"));
            }
            loadPatient(dbHelp.GetPIVAsDB(sql)); 
        }
        #endregion

        private void cbTodayTPN_Checked(object sender, RoutedEventArgs e)
        {
            if (null != this.db)
                loadPatient();
        }

        private void miStatistics_Click(object sender, RoutedEventArgs e)
        {
            WinStatics win = new WinStatics();
            SetWindowOwner(win);
            win.ShowDialog();
        }

        private void miOrdersChk_Click(object sender, RoutedEventArgs e)
        {
            recipemonitorlist.MainWindow winOrdChk = new recipemonitorlist.MainWindow(); 
            winOrdChk.ShowDialog();
        }
         
        private void miUpdate_Click(object sender, RoutedEventArgs e)
        {
            BLPublic.DBConfig dbcfg = new BLPublic.DBConfig(AppConst.SYSTEM_ID, false);
            if (!dbcfg.LoadXmlFile("bl_server.lcf"))
            {
                BLPublic.Dialogs.Error("获取服务器地址失败:" + dbcfg.Error);
                return;
            }

            string svcIP = dbcfg.Server;
            if (string.IsNullOrWhiteSpace(svcIP))
                return;

            if (svcIP.Contains("\\"))
            {
                int p = svcIP.IndexOf("\\");
                svcIP = svcIP.Substring(0, p);
            }

            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
            info.FileName = @"update.exe";
            info.Arguments = string.Format("-A{0} -S{1} -E{2}", svcIP, "TPN", 
                                System.Reflection.Assembly.GetEntryAssembly().Location);
            info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            System.Diagnostics.Process.Start(info); 
        }

        #region 接口实现
        void IMenuManager.menuBeforeSelect()
        {
            try
            {
                this.db = new BLPublic.DBOperate(AppDomain.CurrentDomain.BaseDirectory + "bl_server.lcf",
                    AppConst.SYSTEM_ID);

                if (!this.db.IsConnected)
                {
                    BLPublic.Dialogs.Error("连接服务器失败:" + this.db.Error);
                    return;
                }
                AppConst.db = this.db;
                ComClass.getAcountNameList(AppConst.db, ComClass.Acounts);

                listWard();//初始化患者查询页面中有个病区的下拉列表，耗时80毫秒
                loadPatient(false);
                Page_Click(btnTPNOrders, null);
            }
            catch (Exception ex)
            {
                ShowMsgHelper.ShowError("TPN画面加载时出错:" + ex.Message);
            }
        }

        void IMenuManager.menuAfterSelect()
        {
        }

        void IDisposable.Dispose()
        {
            //先不实现
        }
        #endregion
    }
}
 