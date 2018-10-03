using PIVAsCommon.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TPNReview
{
    /// <summary>
    /// PageOrders.xaml 的交互逻辑
    /// </summary>
    public partial class PageOrders : Page, IContentPage
    {
        private PatientModel patient = null;
        private Action onRefParent = null;

        private tpnmonitor.TPNMonitor tpnMnt = null;
        private recipemonitorlist.TPNItemView tpnView = null;
        private DataTable tblOrders = null;
        private DataTable tblMntSrcItem = null;
        private DataTable tblOpRecord = null;
        private DataTable tblTPNLISChk = null; 
        private string useLISItems = ""; 
        private bool canModType = false;
         

        public PageOrders()
        {
            InitializeComponent();

            btnTPNLISChk.Background = new SolidColorBrush(Colors.SkyBlue);

            this.tpnView = new recipemonitorlist.TPNItemView(AppConst.db, lvTPNItems);

            loadMonitorSource();
            initTPNLIS();

            IDataReader dr = null;
            if (AppConst.db.GetRecordSet(SQL.SET_CANMOD_ORDERSTYP, ref dr))
            {
                if (dr.Read())
                    this.canModType = "1".Equals(dr.GetString(0)) || (0 == string.Compare("true", dr.GetString(0)));

                dr.Close();
            }
        }
         

        public void init(Action _refParent)
        {
            this.onRefParent = _refParent;
        }


        public void setPatient(PatientModel _pnt)
        {
            this.patient = _pnt;

            grdPNLISChk.ItemsSource = null;
            lvLISChkRecord.Items.Clear();
            lbOrdChkResult.Items.Clear();

            if (null != this.tblOpRecord)
            {
                this.tblOpRecord.Clear();
                lbOpRecord.Items.Refresh();
            }

            if (null != this.patient)
            {
                loadOrders(this.patient.WardCode, this.patient.PatientCode);

                loadCheckResult();

                if (null == tblOpRecord)
                {
                    this.tblOpRecord = new DataTable();
                    this.tblOpRecord.Columns.Add("OpType", typeof(string));
                    this.tblOpRecord.Columns.Add("OpID", typeof(int));
                    this.tblOpRecord.Columns.Add("OpTime", typeof(DateTime));
                    this.tblOpRecord.Columns.Add("OpContent", typeof(string));
                    this.tblOpRecord.Columns.Add("Operater", typeof(string));

                    this.tblOpRecord.PrimaryKey = new DataColumn[] { this.tblOpRecord.Columns["OpType"], 
                                                                 this.tblOpRecord.Columns["OpID"] };
                }
                else
                {
                    this.tblOpRecord.Clear();
                    lbOpRecord.Items.Refresh();
                }

                loadAnalysis();
                loadPMNote();
                loadCustody();
                loadIntervene();

                this.tblOpRecord.DefaultView.Sort = "OpTime DESC";
                lbOpRecord.ItemsSource = this.tblOpRecord.DefaultView; 
            }

            if (0 < lbOrders.Items.Count)
                lbOrders.SelectedIndex = 0;
        }

        public void clear()
        {
            lbOrders.Items.Clear(); 
            grdPNLISChk.ItemsSource = null;
            lvLISChkRecord.Items.Clear();

            clearOrders();
        }

        public void clearOrders()
        {   
            lvTPNItems.Items.Clear();
            lbOrdChkResult.Items.Clear();
            
            this.tpnView.clearView();
        }

        /// <summary>
        /// 初始化TPN检查项目
        /// </summary>
        private void initTPNLIS()
        { 
            IDataReader dr = null;
            if (!AppConst.db.GetRecordSet(recipemonitorlist.SQL.SEL_PN_CHKITEM, ref dr))
            {
                BLPublic.Dialogs.Error("获取营养检查项目失败:" + AppConst.db.Error);
                return;
            }

            if (null == this.tblTPNLISChk)
            {
                this.tblTPNLISChk = new DataTable();
                this.tblTPNLISChk.Columns.Add("ID", typeof(int));
                this.tblTPNLISChk.Columns.Add("ItemName", typeof(string));
                this.tblTPNLISChk.Columns.Add("LastTime", typeof(string));
                this.tblTPNLISChk.Columns.Add("LastValue", typeof(string));
                this.tblTPNLISChk.Columns.Add("ValueDiret", typeof(string));
                this.tblTPNLISChk.Columns.Add("NormalValue", typeof(string));
                this.tblTPNLISChk.Columns.Add("Diret", typeof(int));
            }
            else
                this.tblTPNLISChk.Clear(); 

            DataRow newRow = null;
            BLPublic.BLDataReader dldr = new BLPublic.BLDataReader(dr);
            while (dldr.next())
            {
                newRow = this.tblTPNLISChk.NewRow();
                newRow["ID"] = dldr.getInt("KDCheckItemID");

                if (!string.IsNullOrWhiteSpace(dldr.getString("HISCheckItemUnit")))
                    newRow["ItemName"] = string.Format("{0} ({1})",dldr.getString("HISCheckItemName"), dldr.getString("HISCheckItemUnit"));
                else
                    newRow["ItemName"] = dldr.getString("HISCheckItemName");

                this.tblTPNLISChk.Rows.Add(newRow);
            }

            dldr.close(); 
        }

        /// <summary>
        /// 加载使用营养药医嘱
        /// </summary>
        private void loadOrders(string _deptCode, string _patientCode)
        {
            lbOrders.Items.Clear();

            if (null == this.tpnMnt)
            {
                this.tpnMnt = new tpnmonitor.TPNMonitor();
                this.tpnMnt.init(AppConst.db, AppConst.LoginEmpCode);
            }

            if (null == this.tblOrders)
            {
                this.tblOrders = new DataTable();

                DataColumn col = new DataColumn("Capacity", typeof(double));
                col.DefaultValue = -1;
                this.tblOrders.Columns.Add(col); 
            }
            else
                this.tblOrders.Clear();

            string sql = "";
            if (true == cbOnlyUse.IsChecked)
                sql = string.Format(SQL.SEL_ACT_ORDERS, _deptCode, _patientCode);
            else
                sql = string.Format(SQL.SEL_ALL_ORDERS, _deptCode, _patientCode);

            if (AppConst.db.GetRecordSet(sql, ref this.tblOrders))
                listOrders();
            else
                BLPublic.Dialogs.Error("加载医嘱失败:" + AppConst.db.Error);
        }

        /// <summary>
        /// 加载 LIS/医嘱审核结果
        /// </summary>
        private void loadCheckResult()
        { 
            if (Visibility.Visible == grdPNLISChk.Visibility)
                loadTPNLISChk();
            else if (Visibility.Visible == lvLISChkRecord.Visibility)
                loadChkRecord();
            else
                loadOrderChkResut();
        }


        /// <summary>
        /// 加载TPN检查项目结果
        /// </summary>
        private void loadTPNLISChk()
        {
            if (null == this.tblTPNLISChk)
                return;

            grdPNLISChk.ItemsSource = null;
            foreach (DataRow chkRow in this.tblTPNLISChk.Rows)
            {
                chkRow["LastTime"] = "";
                chkRow["LastValue"] = "";
                chkRow["ValueDiret"] = "";
                chkRow["NormalValue"] = "";
                chkRow["Diret"] = 0;
            } 
              
            DataTable tbl = new DataTable();
            if (!AppConst.db.GetRecordSet(string.Format(recipemonitorlist.SQL.SEL_PN_CHK_RESULT, 
                        patient.PatientCode), ref tbl))
            {
                BLPublic.Dialogs.Error("加载营养检查结果失败:" + AppConst.db.Error);
                return;
            }

            if (0 == tbl.Rows.Count)
                return;
            
            //填充数据 
            DataRow[] rows = null; 
            int colLen = this.tblTPNLISChk.Columns.Count;
            foreach (DataRow chkRow in this.tblTPNLISChk.Rows)
            {
                rows = tbl.Select("KDCheckItemID=" + chkRow["ID"].ToString(), "CheckTime DESC");
                if ((null != rows) && (0 < rows.Length))  
                    {
                        chkRow["LastTime"] =  Convert.ToDateTime(rows[0]["CheckTime"].ToString()).ToString("yyyy-M-d H:mm");
                        chkRow["LastValue"] = BLPublic.Utils.trimZero(rows[0]["ResultValue"].ToString());
                        chkRow["ValueDiret"] = rows[0]["ValueDrect"].ToString();
                        chkRow["NormalValue"] = rows[0]["Domain"].ToString();
                        if ("↑".Equals(chkRow["ValueDiret"].ToString()))
                            chkRow["Diret"] = 1;
                        
                        else if ("↓".Equals(chkRow["ValueDiret"].ToString()))
                            chkRow["Diret"] = -1; 
                    } 
            }

            tbl.Clear(); 
            grdPNLISChk.ItemsSource = this.tblTPNLISChk.DefaultView; 
        }

        /// <summary>
        /// 加载患者检查记录
        /// </summary>
        /// <param name="_recipeID"></param>
        private void loadChkRecord()
        {
            DataTable tbl = new DataTable();

            if (AppConst.db.GetRecordSet(string.Format(SQL.SEL_CHKRCD, this.patient.PatientCode), ref tbl))
            {
                tbl.Columns.Add("SortTime", typeof(DateTime));
                foreach (DataRow row in tbl.Rows)
                {
                    try
                    {
                        row["SortTime"] = Convert.ToDateTime(row["CheckTime"].ToString());
                    }
                    catch
                    {
                        row["SortTime"] = (DateTime)row["InceptDT"];
                    }
                }

                tbl.DefaultView.Sort = "SortTime DESC";
                foreach(DataRow row in tbl.DefaultView.ToTable().Rows)
                {
                    lvLISChkRecord.Items.Add(new CheckRecord(row["CheckRecordNo"].ToString(), row["CheckType"].ToString(),
                                            PageCheckRecord.getChkTypeName(row["CheckType"].ToString()), row["CheckName"].ToString(),
                                            Convert.ToDateTime(row["CheckTime"].ToString()), row["CheckerCode"].ToString()));
                }
                tbl.Clear();
            }
            else
                BLPublic.Dialogs.Error("加载检查记录失败:" + AppConst.db.Error);
        }

        /// <summary>
        /// 加载评估结果
        /// </summary>
        private void loadAnalysis()
        { 
            IDataReader dr = null;
            if (AppConst.db.GetRecordSet(string.Format(SQL.SEL_ANALYSIS_RCD, this.patient.PatientCode), ref dr))
            {
                BLPublic.BLDataReader bldr = new BLPublic.BLDataReader(dr);
                while (bldr.next())
                {
                    this.tblOpRecord.Rows.Add(new object[] { "analysis", bldr.getInt("RecordID"),
                        bldr.getDateTime("RecordTime"), "总分: " + bldr.getString("TotalScore"), 
                        bldr.getString("Recorder") });
                }
                dr.Close();
            }
            else
                BLPublic.Dialogs.Error("加载评估记录失败:" + AppConst.db.Error);
        }

        /// <summary>
        /// 加载药师日记
        /// </summary>
        private void loadPMNote()
        {
            IDataReader dr = null;
            if (AppConst.db.GetRecordSet(string.Format(SQL.SEL_PM_NOTE, this.patient.PatientCode), ref dr))
            {
                BLPublic.BLDataReader bldr = new BLPublic.BLDataReader(dr);
                while (bldr.next())
                {
                    this.tblOpRecord.Rows.Add(new object[] { "note", bldr.getInt("NoteID"),
                        bldr.getDateTime("NoteTime"), bldr.getString("NoteContent"), 
                        ComClass.getEmpName(bldr.getString("Noter")) });
                }
                dr.Close(); 
            }
            else
                BLPublic.Dialogs.Error("加载笔记记录失败:" + AppConst.db.Error);
        }

        /// <summary>
        /// 新增笔记时刷新
        /// </summary>
        /// <param name="_content"></param>
        public void addNote(int _id, string _content)
        {
            this.tblOpRecord.Rows.Add(new object[] { "note", _id, DateTime.Now, _content, 
                        ComClass.getEmpName(AppConst.LoginEmpCode) });
            lbOpRecord.Items.Refresh();
        }

        /// <summary>
        /// 加载监护记录
        /// </summary>
        private void loadCustody()
        {
            DataTable tbl = null;
            if (AppConst.db.GetRecordSet(string.Format(SQL.SEL_CUSTODY_BYPNT, this.patient.PatientCode), ref tbl))
            {
                BLPublic.BLDataReader bldr = new BLPublic.BLDataReader(tbl.CreateDataReader());
                while (bldr.next())
                {
                    addCustody(bldr.getInt("CustodyID"), WinCustodyEdit.getObjectStr(bldr.getInt("CustodyID"), "\r\n"), 
                               bldr.getString("CustodyDesc"), bldr.getDateTime("CustodyTime"), bldr.getString("Custodyer"));
                }

                bldr.close(); 
                tbl.Clear(); 
            }
            else
                BLPublic.Dialogs.Error("加载监护记录失败:" + AppConst.db.Error);
        }

        /// <summary>
        /// 增加监护显示
        /// </summary>
        /// <param name="_objName"></param>
        /// <param name="_desc"></param>
        public void addCustody(int _id, string _objName, string _desc, DateTime _time, string _empCode=null)
        { 
            if (null == _empCode)
                _empCode = AppConst.LoginEmpCode;

            this.tblOpRecord.Rows.Add(new object[] { "custody", _id, _time, _objName + "\r\n" + _desc, 
                        ComClass.getEmpName(_empCode) });
        }
        public void addCustody(int _id, string _objName, string _desc)
        {
            addCustody(_id, _objName, _desc, DateTime.Now); 
        }

        /// <summary>
        /// 增加干预显示
        /// </summary>
        /// <param name="_objName"></param>
        /// <param name="_desc"></param>
        public void addIntervene(int _id, string _objName, string _desc, DateTime _time, string _empCode = null)
        {
            if (null == _empCode)
                _empCode = AppConst.LoginEmpCode;

            this.tblOpRecord.Rows.Add(new object[] { "intervene", _id, _time, _objName + "\r\n" + _desc, 
                        ComClass.getEmpName(_empCode) }); 
        }
        public void addIntervene(int _id, string _objName, string _desc)
        {
            addIntervene(_id, _objName, _desc, DateTime.Now);
        }

        /// <summary>
        /// 加载干预记录
        /// </summary>
        private void loadIntervene()
        {
            DataTable tbl = null; 
            if (AppConst.db.GetRecordSet(string.Format(SQL.SEL_INTERVENE_BYPNT, this.patient.PatientCode), ref tbl))
            {
                BLPublic.BLDataReader bldr = new BLPublic.BLDataReader(tbl.CreateDataReader());
                while (bldr.next())
                {
                    addIntervene(bldr.getInt("InterveneID"), WinInterveneEdit.getObjectStr(bldr.getInt("InterveneID")), 
                                 bldr.getString("IntervenePlan"), bldr.getDateTime("InterveneTime"), bldr.getString("Intervener"));
                }

                bldr.close();
                tbl.Clear(); 
            }
            else
                BLPublic.Dialogs.Error("加载干预记录失败:" + AppConst.db.Error);
        }

        /// <summary>
        /// 加载TPN审方条件关联的TPN项目
        /// </summary>
        private void loadMonitorSource()
        {
            if (null == this.tblMntSrcItem)
                this.tblMntSrcItem = new DataTable();
            else
                this.tblMntSrcItem.Clear();

            AppConst.db.GetRecordSet(SQL.SEL_TPNMNT_ITEM, ref this.tblMntSrcItem);
        }


        /// <summary>
        /// 显示医嘱
        /// </summary>
        private void listOrders()
        {
            lbOrders.Items.Clear();
            lvTPNItems.Items.Clear();

            if (null == this.tblOrders)
                return;

            this.tpnMnt.loadConfig();

            string rowFilter = "";
            if (true == cbOnlyTPN.IsChecked) 
                rowFilter = "OrdersLabel=4"; 

            this.tblOrders.DefaultView.RowFilter = rowFilter;

            int tpnLvl = -1;
            bool isTPN = false;
            bool hadNotChk = false;
            string prveRecipeID = "";
            OrdersModel odrModel = null;

            foreach (DataRow row in this.tblOrders.DefaultView.ToTable().Rows)
            {
                if (!prveRecipeID.Equals(row["RecipeID"].ToString()))
                {
                    prveRecipeID = row["RecipeID"].ToString();
                    isTPN = "4".Equals(row["OrdersLabel"].ToString()); 
                    if (row.IsNull("TPNLevel"))
                    {
                        tpnLvl = -1;
                        hadNotChk = true;
                    }
                    else
                        tpnLvl = Convert.ToInt32(row["TPNLevel"].ToString());

                    if (0 > (double)row["Capacity"])
                        row["Capacity"] = this.tpnMnt.ordersCapacity(row["RecipeID"].ToString());

                    odrModel = new OrdersModel
                    { 
                        RecipeID = prveRecipeID,

                        OrderTime = ComClass.getDateTimeFmt(Convert.ToDateTime(row["StartTime"].ToString())),
                        StopTime = row.IsNull("StopTime") ? "--" : ComClass.getDateTimeFmt(Convert.ToDateTime(row["StopTime"].ToString())),
                        UseRoute = row["UsageCode"].ToString(),
                        FreqCode = row["FreqCode"].ToString(),
                        Remark = row["Remark"].ToString(),
                        Capacity = row["Capacity"].ToString(),
                        GroupNo = row["GroupNo"].ToString(), 
                        CheckLevel = tpnLvl, 
                        IsTPN = isTPN,
                        IsStop = false,
                        CanMod = this.canModType
                    };

                    if (isTPN) 
                    {
                        if (-1 == tpnLvl)
                            odrModel.State = "未审方";
                        else if (0 == tpnLvl)
                            odrModel.State = "通过"; 
                        else
                            odrModel.State = "未通过";
                    }
                    else
                        odrModel.State = "--";

                    if (!row.IsNull("StopTime")) 
                    { 
                        odrModel.IsStop = true;
                    }
                    else if ("-1" == row["NowStatus"].ToString())
                    {
                        odrModel.StopTime = "已退方";
                        odrModel.IsStop = true;
                    }

                    lbOrders.Items.Add(odrModel);
                }

                odrModel.Drugs.Add(new OrdersDrugModel(row["DrugCode"].ToString(), row["DrugName"].ToString(),
                                                       row["DrugSpec"].ToString(), 
                                                       BLPublic.Utils.trimZero(row["Dosage"].ToString()),
                                                       row["DosageUnit"].ToString(),
                                                       row["Quantity"].ToString()));
            }

            if (hadNotChk && (null != this.patient))
            {
                this.patient.HadNotCheckOrders = true;
                if (null != this.onRefParent)
                    this.onRefParent();
            } 
        }

        /// <summary>
        /// 获取TPN使用到的LIS检查项目编码
        /// </summary>
        private void initUseLISCheckIitem()
        {
            IDataReader dr = null;
            if (!AppConst.db.GetRecordSet(string.Format(SQL.SEL_TPNTEIM_BYTYP, "lischk"), ref dr))
            {
                BLPublic.Dialogs.Error("加载检查TPN项目失败:" + AppConst.db.Error);
                return;
            }

            List<string> lstItems = new List<string>();
            int i = dr.GetOrdinal("Express");
            while (dr.Read())
            {
                if (!dr.IsDBNull(i))
                    tpnmonitor.TPNCalculator.getExpressItem(dr.GetString(i), lstItems);
            }

            dr.Close();

            this.useLISItems = "";
            foreach (string code in lstItems)
            {
                if (!string.IsNullOrWhiteSpace(this.useLISItems))
                    this.useLISItems += ",";

                this.useLISItems += "'" + code + "'";
            }

        }
           
        /// <summary>
        /// 获取审方结果关联TPN项目值
        /// </summary>
        /// <param name="_srcID"></param>
        /// <returns></returns>
        private string getMonitorItems(int _srcID, ref List<int> _tpnItems)
        {
            if ((null == this.tblMntSrcItem) || (0 == this.tblMntSrcItem.Rows.Count))
                return "";

            DataRow[] rows = this.tblMntSrcItem.Select("TPNMonitorID=" + _srcID.ToString());
            string rt = "";
            DataRow row = null;
            foreach(DataRow r in rows)
            {
                _tpnItems.Add(Convert.ToInt32(r["TPNItemID"].ToString()));
                row = this.tpnView.getTPNItem(Convert.ToInt32(r["TPNItemID"].ToString()));
                if (null != row)
                {
                    if (!string.IsNullOrWhiteSpace(rt))
                        rt += "，";
                    rt += row["ItemName"].ToString() + "=" + row["ItemValue"].ToString() + row["Unit"].ToString();
                }
            } 
                            
            return rt;
        }
         
          
        /// <summary>
        /// 更新患者审方
        /// </summary>
        /// <param name="_pModel"></param>
        private void refPatientTPNMnt(PatientModel _pModel)
        {
            if (null == _pModel)
                return;

            IDataReader dr = null;
            if (!AppConst.db.GetRecordSet(string.Format(SQL.SEL_PNT_TPNCHK, _pModel.PatientCode), ref dr))
            {
                BLPublic.Dialogs.Error("更新审方结果失败:" + AppConst.db.Error);
                return;
            }

            bool hadNoChk = false;
            Dictionary<string, int> lstTPNMnt = new Dictionary<string, int>();
            while (dr.Read())
            {
                if (dr.IsDBNull(1))
                {
                    hadNoChk = true;
                    lstTPNMnt.Add(dr["RecipeID"].ToString(), -1);
                }
                else
                    lstTPNMnt.Add(dr["RecipeID"].ToString(), dr.GetInt32(1));
            }

            dr.Close();

            _pModel.HadNotCheckOrders = hadNoChk;
            if (null != this.onRefParent)
                this.onRefParent();

            foreach (OrdersModel odr in lbOrders.Items)
                if (odr.IsTPN && lstTPNMnt.ContainsKey(odr.RecipeID) && 
                    !string.IsNullOrWhiteSpace(odr.State) //医嘱行
                   ) 
                    odr.CheckLevel = lstTPNMnt[odr.RecipeID];

            lbOrders.Items.Refresh();
        }

        private void updateChkResult(OrdersModel _recipe)
        {
            if (!AppConst.db.ExecSQL(string.Format(SQL.UP_MNTRESULT, _recipe.RecipeID)))
            {
                BLPublic.Dialogs.Error("更新审方结果等级失败:" + AppConst.db.Error);
                return;
            }

            IDataReader dr = null;
            if (!AppConst.db.GetRecordSet(string.Format(SQL.SEL_CHKLEVEL, _recipe.RecipeID), ref dr))
            {
                BLPublic.Dialogs.Error("获取审方结果等级失败:" + AppConst.db.Error);
                return;
            }

            if (dr.Read())
            {
                _recipe.CheckLevel = dr.GetInt32(0);
                lbOrders.Items.Refresh();
            }

            dr.Close();
        }
         

        /// <summary>
        /// 加载审方结果
        /// </summary>
        private void loadOrderChkResut()
        {
            lbOrdChkResult.Items.Clear();
            if (null == lbOrders.SelectedItem)
                return;
            recipemonitorlist.RecipeTPNCheck.loadTPNCheck(lbOrdChkResult, AppConst.db, 
                        ((OrdersModel)lbOrders.SelectedItem).RecipeID); 
        }


        #region 控件事件

        private void lbOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null == lbOrders.SelectedItem)
                return;

            OrdersModel ord = (OrdersModel)lbOrders.SelectedItem;

            this.clearOrders();

            if (string.IsNullOrWhiteSpace(ord.RecipeID))
                return;
             
            if (ord.IsTPN)
            {
                this.tpnView.showRecipeTPN(ord.RecipeID);
                loadOrderChkResut();
            } 
        }

        private void cbbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listOrders();
        }

        private void btnAddCustomChk_Click(object sender, RoutedEventArgs e)
        {
            if (null == lbOrders.SelectedItem)
            {
                BLPublic.Dialogs.Alert("请先选择医嘱.");
                return;
            }

            OrdersModel ord = (OrdersModel)lbOrders.SelectedItem;

            WinCustomCheck winChk = new WinCustomCheck();
            winChk.init(this.patient.PatientCode, ord.RecipeID);
            if (true == winChk.ShowDialog())
            {
                loadOrderChkResut();
            } 
        }

        private void ReMonitor_Click(object sender, RoutedEventArgs e)
        {
            if (null == lbOrders.SelectedItem)
            {
                BLPublic.Dialogs.Alert("请选择要重审的医嘱.");
                lbOrders.Focus();
                return;
            }

            OrdersModel orders = (OrdersModel)lbOrders.SelectedItem;
            if (string.IsNullOrWhiteSpace(orders.RecipeID))
            {
                BLPublic.Dialogs.Alert("请选择要重审的医嘱内容.");
                lbOrders.Focus();
                return;
            }

            if (BLPublic.Dialogs.Yes != BLPublic.Dialogs.Ask("此操作将会删除现在的TPN审方结果，是否继续?"))
            {
                lbOrders.Focus();
                return;
            }
             
            if (!AppConst.db.ExecSQL(string.Format(SQL.DEL_TPNMNT_BYRCP, orders.RecipeID)))
            {
                BLPublic.Dialogs.Alert("删除TPN审方失败:" + AppConst.db.Error);
                return;
            }

            ((Button)sender).IsEnabled = false;

            this.tpnMnt.loadConfig();
            bool rt = this.tpnMnt.Monitor(this.patient.PatientCode, this.patient.AgeMonth, 
                                          this.patient.Sex, orders.RecipeID);

            ((Button)sender).IsEnabled = true;

            if (rt)
            {
                this.tpnMnt.saveTPNValue(); 
                refPatientTPNMnt(this.patient);

                this.clearOrders();

                this.tpnView.showRecipeTPN(orders.RecipeID);
                loadOrderChkResut();

                BLPublic.Dialogs.Info("重审成功");
            }
            else
                BLPublic.Dialogs.Error("重审失败:" + this.tpnMnt.getError());
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (null != this.patient)
                {
                    this.clearOrders();
                    loadOrders(this.patient.WardCode, this.patient.PatientCode);
                }
            }
            catch (Exception ex)
            {
                ShowMsgHelper.ShowError("点击 仅显示在用医嘱 复选框出错:" + ex.Message);
            }
        }

        private void TPNItemColumn_Click(object sender, RoutedEventArgs e)
        {
            ListViewClick.ColumnSort_Click(sender, e);
        }

        private void UnSelected_LostFocus(object sender, RoutedEventArgs e)
        {
            ((ListBox)sender).SelectedIndex = -1;
        } 

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (null == ((Button)sender).Tag)
            {
                BLPublic.Dialogs.Alert("无法编辑，未关联审方结果记录");
                return;
            }

            recipemonitorlist.MonitorResult mntRt = (recipemonitorlist.MonitorResult)((Button)sender).Tag;
            WinCustomCheck winChk = new WinCustomCheck();
            winChk.EditID = mntRt.ResultID;
            if (true == winChk.ShowDialog())
            {
                mntRt.ResultDesc = winChk.txtDesc.Text.Trim();
                mntRt.CheckLevel = true == winChk.rdo5Degress.IsChecked ? 5 : 3;
            } 
        }

        private void lvTPNItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((null == lbOrders.SelectedItem) || (null == lvTPNItems.SelectedItem))
                return;

            recipemonitorlist.TPNItemModel item = (recipemonitorlist.TPNItemModel)lvTPNItems.SelectedItem;
            if ("_EXPAND_".Equals(item.Code))
                this.tpnView.showTPN(true);

            else if ("_COLLAPSED_".Equals(item.Code))
                this.tpnView.showTPN(false);

            else if (!string.IsNullOrWhiteSpace(item.Code) && (0 < item.ID))
            {
                OrdersModel ord = (OrdersModel)lbOrders.SelectedItem;

                tpnmonitor.frmCalDetail frmDetail = new tpnmonitor.frmCalDetail();
                frmDetail.init(AppConst.db, ord.RecipeID, item.Code);
                frmDetail.ShowDialog();
            }
        }

        private void modOrdersType(string _recipeID, string _oldTyp, string _newTyp)
        {
            if (AppConst.db.ExecSQL(string.Format(SQL.MOD_ORDERS_TYP, _recipeID, _oldTyp, _newTyp, AppConst.LoginEmpCode)))
            {
                loadOrders(this.patient.WardCode, this.patient.PatientCode);
                BLPublic.Dialogs.Info("修改成功");
            }
            else
                BLPublic.Dialogs.Error("修改医嘱类型失败:" + AppConst.db.Error);
        }

        private void btnModType_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (null == btn.Tag)
                return;

            if (BLPublic.Dialogs.Yes != BLPublic.Dialogs.Ask("是否确定" + btn.ToolTip.ToString() + "?"))
                return;

            string recipeID = btn.Tag.ToString();
            string oldTyp = "1";
            DataRow[] rows = this.tblOrders.Select("RecipeID='" + recipeID + "'");
            if (null != rows && 0 < rows.Length)
                oldTyp = rows[0]["OrdersLabel"].ToString();
             
            if (btn.ToolTip.ToString().Contains("营养"))
                modOrdersType(recipeID, oldTyp, "4");

            else
            {
                List<BLPublic.CodeNameItem> list = new List<BLPublic.CodeNameItem>();
                list.Add(new BLPublic.CodeNameItem("1", "普通"));
                list.Add(new BLPublic.CodeNameItem("2", "抗生素"));
                list.Add(new BLPublic.CodeNameItem("3", "化疗药"));

                WinList winLst = new WinList();
                winLst.Owner = AppConst.winMain;
                winLst.init(list, (obj) =>
                {
                    modOrdersType(recipeID, oldTyp, ((BLPublic.CodeNameItem)obj).Code); 
                });
                winLst.Show();
            } 
        }

        private void cbOnlyTPN_Checked(object sender, RoutedEventArgs e)
        {
            if (null != this.tblOrders) 
                listOrders();
        } 

        private void lvChkRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (null == lvLISChkRecord.SelectedItem)
                return;

            CheckRecord chkRcd = (CheckRecord)lvLISChkRecord.SelectedItem;
             
            WinInfoDialog win = new WinInfoDialog();
            win.Owner = AppConst.winMain;
            win.Title = chkRcd.CheckType;
            win.PatientCode = this.patient.PatientCode;
            
            win.showCheckResult(chkRcd.RecordNo, chkRcd.TypeCode, chkRcd.CheckTime, this.addCustody);
            win.ShowDialog();
            win = null;
            lbOpRecord.Items.Refresh();
        }

        private void btnMntResult_Click(object sender, RoutedEventArgs e)
        {
            if (null == lbOrders.SelectedItem)
            {
                BLPublic.Dialogs.Alert("请选择医嘱.");
                lbOrders.Focus();
                return;
            }

            WinInfoDialog win = new WinInfoDialog();
            win.Owner = AppConst.winMain;
            win.Title = "审方结果";
            win.PatientCode = this.patient.PatientCode;
            win.showTPNChkResult(((OrdersModel)lbOrders.SelectedItem).RecipeID);
            win.ShowDialog();
            win = null;
        }

        /// <summary>
        /// TPN参数监护和敢于
        /// </summary>
        private void TPNCustodyIntervene(bool _isIntervene)
        {
            if (0 == lvTPNItems.SelectedItems.Count)
            {
                BLPublic.Dialogs.Alert("请选择要监护的项目");
                lvTPNItems.Focus();
                return;
            }

            if (!WinCustodyEdit.chkWin(_isIntervene))
                return;

            if ((1 == lvTPNItems.SelectedItems.Count) && (0 >= ((recipemonitorlist.TPNItemModel)lvTPNItems.SelectedItems[0]).ID))
            {
                BLPublic.Dialogs.Alert("请选择TPN项目");
                lvTPNItems.Focus();
                return;
            }

            recipemonitorlist.TPNItemModel tpn = null;
            if (null == WinCustodyEdit.OnSetObject)
            {
                if (!WinCustodyEdit.TPNCustodyIntervene(this.patient.PatientCode, _isIntervene,
                                                    (id, objStr, desc) =>
                                                    {
                                                        if (_isIntervene)
                                                            this.addIntervene(id, objStr, desc);
                                                        else
                                                            this.addCustody(id, objStr, desc);

                                                        lbOpRecord.Items.Refresh();
                                                    }))
                    return;
            }
              
            foreach (object o in lvTPNItems.SelectedItems)
            {
                tpn = (recipemonitorlist.TPNItemModel)o;
                if (0 < tpn.ID)
                    WinCustodyEdit.OnSetObject(WinCustodyEdit.OBJTYP_TPN, tpn.Code, tpn.Name, tpn.Value + tpn.Unit,
                            Convert.ToDateTime(((OrdersModel)lbOrders.SelectedItem).OrderTime));
            }
        }


        private void btnTPNCustody_Click(object sender, RoutedEventArgs e)
        {
            TPNCustodyIntervene(false);
        }

        private void Detail_Click(object sender, RoutedEventArgs e)
        {
            btnTPNLISChk.Background = SystemColors.ControlBrush; 
            btnChkRecord.Background = SystemColors.ControlBrush; 
            btnMntResult.Background = SystemColors.ControlBrush;

            ((Button)sender).Background = new SolidColorBrush(Colors.SkyBlue);
             
            grdPNLISChk.Visibility = Converter.bool2Visibe(btnTPNLISChk.Equals(sender));
            lbOrdChkResult.Visibility = Converter.bool2Visibe(btnMntResult.Equals(sender));
            lvLISChkRecord.Visibility = Converter.bool2Visibe(btnChkRecord.Equals(sender));

            btnAddCustomChk.Visibility = lbOrdChkResult.Visibility;

            loadCheckResult();
        }


        private void btnDelChk_Click(object sender, RoutedEventArgs e)
        {   
            if (null == ((Button)sender).Tag)
            {
                BLPublic.Dialogs.Alert("无法取消，未关联审方结果记录");
                return;
            }

            if (BLPublic.Dialogs.Yes != BLPublic.Dialogs.Ask("确定删除此审方结果."))
                return;

            recipemonitorlist.MonitorResult mntRt = (recipemonitorlist.MonitorResult)((Button)sender).Tag;
            string sql = "";
            if (mntRt.IsCustom)
                sql = string.Format(SQL.DEL_CUSTOM_CHK, mntRt.ResultID);
            else
                sql = string.Format(SQL.DEL_TPNMNT_BYID, mntRt.ResultID);

            if (AppConst.db.ExecSQL(sql))
            {
                for (int i = lbOrdChkResult.Items.Count - 1; i >= 0; i--)
                    if (mntRt.ResultID == ((recipemonitorlist.MonitorResult)lbOrdChkResult.Items[i]).ResultID)
                    {
                        lbOrdChkResult.Items.RemoveAt(i);
                        break;
                    }

                updateChkResult((OrdersModel)lbOrders.SelectedItem);
            }
            else
                BLPublic.Dialogs.Error("删除审核结果失败:" + AppConst.db.Error);
        }

        private void btnMock_Click(object sender, RoutedEventArgs e)
        {
            if (null == lbOrders.SelectedItem)
            {
                BLPublic.Dialogs.Alert("请选择医嘱.");
                lbOrders.Focus();
                return;
            }

            OrdersModel ord = (OrdersModel)lbOrders.SelectedItem;

            tpnmonitor.frmCalDetail frmDetail = new tpnmonitor.frmCalDetail();
            frmDetail.initMockRecipe(AppConst.db, ord.RecipeID);
            frmDetail.ShowDialog();
        }

        private void miDrugIntervene_Click(object sender, RoutedEventArgs e)
        {
            if (null == lbOrders.SelectedItem)
            {
                BLPublic.Dialogs.Alert("请选择医嘱.");
                lbOrders.Focus();
                return;
            }

            //选择药品
            List<object> list = new List<object>();
            foreach (OrdersDrugModel drug in ((OrdersModel)lbOrders.SelectedItem).Drugs) 
                list.Add(drug);

            OrdersDrugModel selDrug = null;
            WinList winLst = new WinList();
            winLst.Owner = AppConst.winMain;
            winLst.Width = 300;
            winLst.init(list);
            if (true == winLst.ShowDialog())
                selDrug = (OrdersDrugModel)winLst.SelObject;

            if (null == selDrug)
                return;

            if (null == WinCustodyEdit.OnSetObject)
                if (!WinCustodyEdit.TPNCustodyIntervene(this.patient.PatientCode, true,
                                                    (id, objStr, desc) =>
                                                    {
                                                        this.addIntervene(id, objStr, desc);
                                                        lbOpRecord.Items.Refresh();
                                                    }))
                    return;


            WinCustodyEdit.OnSetObject(WinInterveneEdit.OBJTYP_DRUG, selDrug.Code, selDrug.Name,
                                       selDrug.Dosage + selDrug.DosageUnit,
                                       Convert.ToDateTime(((OrdersModel)lbOrders.SelectedItem).OrderTime));
        }

        private void btnTPNIntervene_Click(object sender, RoutedEventArgs e)
        {
            TPNCustodyIntervene(true);
        }

        private void miModOp_Click(object sender, RoutedEventArgs e)
        { 
            if (null == lbOpRecord.SelectedItem)
            {
                BLPublic.Dialogs.Alert("请选择要修改的记录.");
                lbOpRecord.Focus();
                return;
            }

            DataRowView dv = (DataRowView)lbOpRecord.SelectedItem;
            string typeCode = dv["OpType"].ToString();

            if ("note".Equals(typeCode))
            {
                WinInput input = new WinInput();
                input.Owner = AppConst.winMain;
                input.inputLong("笔记", "修改笔记", (_txt) =>
                {
                    if (AppConst.db.ExecSQL(string.Format(SQL.MOD_PM_NOTE, BLPublic.DBOperate.ACS(_txt),
                                            dv["OpID"].ToString())))
                    {
                        dv["OpContent"] = _txt;
                        lbOpRecord.Items.Refresh();
                        return true;
                    }
                    else
                        BLPublic.Dialogs.Error("保存笔记失败:" + AppConst.db.Error);
                    return false;
                },
                    dv["OpContent"].ToString());

                input.ShowDialog();
                input = null;
            }
            else if ("custody".Equals(typeCode))
            {
                WinCustodyEdit.TPNCustodyIntervene(Convert.ToInt32(dv["OpID"].ToString()), false,
                                                    (id, objStr, desc) =>
                                                    {
                                                        dv["OpContent"] = objStr + "\r\n" + desc;
                                                        lbOpRecord.Items.Refresh();
                                                    }); 
            }
            else if ("intervene".Equals(typeCode))
            {
                WinCustodyEdit.TPNCustodyIntervene(Convert.ToInt32(dv["OpID"].ToString()), true,
                                                    (id, objStr, desc) =>
                                                    {
                                                        dv["OpContent"] = objStr + "\r\n" + desc;
                                                        lbOpRecord.Items.Refresh();
                                                    }); 
            }
        }

        private void miDelOp_Click(object sender, RoutedEventArgs e)
        {
            if (null == lbOpRecord.SelectedItem)
            {
                BLPublic.Dialogs.Alert("请选择要删除的记录.");
                lbOpRecord.Focus();
                return;
            }

            if (BLPublic.Dialogs.Yes != BLPublic.Dialogs.Ask("是否确定删除所选记录?"))
                return;

            DataRowView dv = (DataRowView)lbOpRecord.SelectedItem;
            string typeCode = dv["OpType"].ToString();
            string sql = "";
            if ("note".Equals(typeCode))
                sql = string.Format(SQL.DEL_PM_NOTE, dv["OpID"].ToString());
            else if ("custody".Equals(typeCode))
            {
                sql = string.Format(SQL.DEL_CUSTODY_BYID, dv["OpID"].ToString());
                sql += "; ";
                sql += string.Format(SQL.DEL_CUSTODY_OBJ, dv["OpID"].ToString());
            }
            else if ("intervene".Equals(typeCode))
            {
                sql = string.Format(SQL.DEL_INTERVENE, dv["OpID"].ToString());
                sql += "; ";
                sql += string.Format(SQL.DEL_INTERVENE_OBJ, dv["OpID"].ToString());
            }
            else
                return;

            if (AppConst.db.ExecSQL(sql))
            {
                DataRow r = this.tblOpRecord.Rows.Find(new object[] { typeCode, dv["OpID"] });
                if (null != r)
                    this.tblOpRecord.Rows.Remove(r);
                lbOpRecord.Items.Refresh();

                BLPublic.Dialogs.Info("删除成功");
            }
            else
                BLPublic.Dialogs.Error("删除失败:" + AppConst.db.Error);
        }


        private void Note_Click(object sender, RoutedEventArgs e)
        {
            if (null == patient)
            {
                BLPublic.Dialogs.Alert("请先选择病人."); 
                return;
            }

            WinInput input = new WinInput();
            input.Owner = AppConst.winMain;
            input.inputLong("记录", "输入笔记", (_txt) =>
            {
                int noteID = 0;
                if (AppConst.db.InsertAndGetId(string.Format(SQL.ADD_PM_NOTE, patient.PatientCode,
                                                BLPublic.DBOperate.ACS(_txt), AppConst.LoginEmpCode), out noteID))
                {
                    addNote(noteID, _txt);
                    return true;
                }
                else
                    BLPublic.Dialogs.Error("保存笔记失败:" + AppConst.db.Error);
                return false;
            });
            input.ShowDialog();
            input = null;
        }

        private void Custody_Click(object sender, RoutedEventArgs e)
        {
            if (null == patient)
            {
                BLPublic.Dialogs.Alert("请先选择病人."); 
                return;
            }

            WinCustodyEdit win = new WinCustodyEdit();
            win.Owner = AppConst.winMain;
            win.init(patient.PatientCode,
                    (isOK) =>
                    {
                        if (isOK)
                            addCustody(win.EditID, win.getObjectStr(), win.getDesc());
                    });

            win.ShowDialog();
        }

        private void grdPNLISChk_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (null == grdPNLISChk.SelectedItem)
                return;

            DataRowView drv = (DataRowView)grdPNLISChk.SelectedItem;
             

            WinInfoDialog win = new WinInfoDialog();
            win.Owner = AppConst.winMain; 
            win.showChartLine(this.patient.PatientCode, (int)drv.Row["ID"], drv.Row["ItemName"].ToString());
            win.ShowDialog();
            win = null;
        }

        #endregion
    }
}
