using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ChargeInterface;
using Communication.MOXATcp;
using PIVAsCommon.Extensions;
using PIVAsCommon.Helper;
using Communication;
using PIVAsCommon;
using Communication.DisplayTcp;
using PIVAsCommon.Models;
using Communication.screenTcp;
using Communication.WindowsScreen;

namespace PivasMcc
{
    public partial class frmMcc : Form
    {
        #region 私有成员
        /// <summary>
        /// 记录端口状态，默认-1，打开后保存端口号(多个设备会有重复端口出现)
        /// 同时这个端口比数据库中大4000
        /// </summary>
        int[] arrayPort;
        private MOXAController moxaContrl = null;//moxa控制
        private DisplayController displayController = null;//迪文屏控制
        private ScreenClientController screenClientController = null;//微软屏控制

        List<ListviewMain_Model> list_LvMain_Model = null;//配置台信息
        List<string[]> IsLogin = null; //配置台的登录信息
        private Queue<ControlLightCommand> lightQueue = null;//亮灯指令队列
        List<bool> CloseALLLight = null;//记录红绿灯状态信息
        ICharge charge = null;//计费接口工厂类

        private DataTable dtCheckResult = null;

        private DB_Help db = new DB_Help();
        private Light light;  //亮灯类

        private bool openred = true;
        private bool opengreen = true;
        private string MoxaGroup = "";

        private string screenType = StaticDictionary.ANDROID_SCREEN;//0=安卓屏；1=迪文屏；2：微软屏

        private const int SLEEP_TIME = 300;//灯亮关间隔时间,设置为（plc数*150+50）毫秒
        private string IsCharge = string.Empty;//在舱内核对时，是否进行计费

        private string IsOtherLabel = "0";//是否为第三方瓶签
        private const int LABEL_LEN = 14;//系统中使用的瓶签长度；第三方瓶签将进行转换
        private int LabelLength = LABEL_LEN;//第三方瓶签长度;设置为三方瓶签后会从数据库中拉取
        private string LabelBS = string.Empty;//瓶签默认开头;设置为三方瓶签后会从数据库中拉取;任何标识都包含String.Empty
        private bool bSynLabelData = true;// 是否同步瓶签数据，默认开启

        //S标识记账成功；F标识记账失败；B标识退药
        private int Slong;//长亮时间
        private int Flong;
        private int Blong;

        private int Stwin;//闪烁时间
        private int Ftwin;
        private int Btwin;

        private int STime;//闪烁次数
        private int FTime;
        private int BTime;

        private bool SUseLong;//标识是否启用常亮
        private bool FUseLong;
        private bool BUseLong;
        
        const int Br = 12;
        const int Db = 2;
        const int Sb = 4;
        const int Pt = 32;
        #endregion

        private Thread threadClearAllListView = null;
        private Thread threadGetLightData = null;
        private Thread threadSynLabelData = null;
        public frmMcc()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            #region 初始化参数
            moxaContrl = new MOXAController();
            moxaContrl.Disconnected += MoxaContrl_Disconnected;
            moxaContrl.DataReceived += MoxaContrl_DataReceived;

            displayController = new DisplayController();
            displayController.Connected += DisplayController_Connected;
            displayController.Disconnected += DisplayController_Disconnected;

            screenClientController = new ScreenClientController();
            screenClientController.Connected += ScreenClientController_Connected;
            screenClientController.Disconnected += ScreenClientController_Disconnected;
            screenClientController.LabelNoReceived += ScreenClientController_LabelNoReceived;

            list_LvMain_Model = new List<ListviewMain_Model>();
            IsLogin = new List<string[]>();
            lightQueue = new Queue<ControlLightCommand>();
            CloseALLLight = new List<bool>();
            dtCheckResult = new DataTable();
            #endregion

            (threadClearAllListView = new Thread(ClearAllListView)).Start();
            (threadGetLightData = new Thread(GetLightData)).Start();
            (threadSynLabelData = new Thread(SynLabelData)).Start();
        }        

        #region user32.dll
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion

        private void frmMcc_Load(object sender, EventArgs e)
        {
            LoadINI();
            bool showScreen = screenType == StaticDictionary.ANDROID_SCREEN ? false : true;//0是安卓屏，不需界面操作
            舱内屏操作ToolStripMenuItem.Visible = showScreen;
            打开屏ToolStripMenuItem.Visible = showScreen;
            关闭屏连接ToolStripMenuItem.Visible = showScreen;

            try
            {
                loadalready();
                IsOtherLabel = db.GetPivasAllSet("第三方瓶签");//获取第三方瓶签的设置

                if (IsOtherLabel == "1")
                {
                    LabelLength = int.Parse(db.GetPivasAllSetValue2("第三方瓶签"));//获取瓶签长度
                    LabelBS = db.GetPivasAllSetValue3("第三方瓶签"); //获取瓶签标识
                    InternalLogger.Log.Debug("LabelLength" + LabelLength.ToString() + "LabelBS" + LabelBS.ToString());
                }
                moxaContrl.SetThridLabelLen(LabelLength, LabelBS);//临时方式，建议在硬件层面解决

                //读取计费配置值并获取接口对象
                IsCharge = db.IniReadValuePivas("Charge", "PZ");
                charge = ChargeFactory.GetCharge(db.IniReadValuePivas("Charge", "HospitalType"));

                //串口号，采用的是默认设置。只支持一个com
                DataSet ds =db.GetPIVAsDB("Select top 1 serialname,rate,PlcType from lightmodel where DeskNo='默认设置'");
                if (ds == null || ds.Tables.Count <= 0)
                    MessageBox.Show("找不到PLC设置信息,PLC配置失败");
                else
                    light = new Light(ds.Tables[0].Rows[0]["serialname"].ToString(),
                        Convert.ToInt32(ds.Tables[0].Rows[0]["rate"].ToString()),
                        ds.Tables[0].Rows[0]["PlcType"].ToString());//连接com口，设置波特率

                GetLightRule();
                GetMoxaList(); //获取配置台信息
                ShowMoxaList();//主画面显示配置台列表

                for (int i = 0; i < listViewMain.Items.Count; i++)
                {
                    IsLogin.Add(new string[2] { "未登录", "未登录" });
                    CloseALLLight.Add(false);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("初始化发生错误，请检查配置"+ex.Message);
            }
        }

        #region 读取Moxa数据和处理
        /// <summary>
        /// 判断是否药师登录
        /// 77776 35990 96680 48437 50 长度22位，是根据药师ID（9999）生成的二维码信息
        /// </summary>
        bool IsLogionData(string moxaData,out String labelData)
        {
            labelData = String.Empty;
            try
            {
                if (moxaData.Trim().Length == 22  && moxaData.Substring(0, 4).Equals("7777"))
                {
                    labelData = moxaData;
                    return true;
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("判断是否药师登录信息时出错;" + ex.Message);
            }
            return false;
        }

        /// <summary>
        /// 判断瓶签是否合法,返回获取到的瓶签数据(这里采用标准格式，长度为14；第三方时转为标准)
        /// 第三方瓶签，需要查询IVRecord_Scan5表，从三方瓶签转为标准瓶签
        /// </summary>
        /// <param name="moxaData"></param>
        /// <returns></returns>
        bool IsLabelNo(string moxaData,out String labelData)
        {
            labelData = String.Empty;
            try
            {
                if (moxaData.Contains(LabelBS) && moxaData.Length >= LabelLength)//这里对瓶签的长度和标识校验
                {
                    if (IsOtherLabel == "1")//将moxa接收数据（第三方瓶签），转为pivas系统标准数据
                    {
                        DataSet dsIVRecord_Scan5 = db.GetPIVAsDB("select LabelNo from IVRecord_Scan5 where ThirdLabelNo like '" + moxaData + "%'");
                        if (dsIVRecord_Scan5 != null && dsIVRecord_Scan5.Tables.Count > 0 && dsIVRecord_Scan5.Tables[0].Rows.Count > 0)
                            moxaData = dsIVRecord_Scan5.Tables[0].Rows[0][0].ToString();
                        else
                        {
                            InternalLogger.Log.Warn(String.Format(
                                "将接收到的第三方瓶签{0}转为标准瓶签时，在IVRecord_Scan5表中未找到第三方瓶签，不处理", moxaData));
                            return false;
                        }
                    }

                    //对标准数据校验是否为今明两日瓶签
                    string today = DateTime.Now.ToString("yyyyMMdd");
                    string tomorrow = DateTime.Now.AddDays(1).ToString("yyyyMMdd");
                    if (moxaData.Contains(today) && moxaData.Substring(moxaData.LastIndexOf(today)).Length >= LABEL_LEN)
                    {
                        labelData = moxaData.Substring(moxaData.LastIndexOf(today), LABEL_LEN).ToString();
                        return true;
                    }
                    else if (moxaData.Contains(tomorrow) && moxaData.Substring(moxaData.LastIndexOf(tomorrow)).Length >= LABEL_LEN)
                    {
                        labelData = moxaData.Substring(moxaData.LastIndexOf(tomorrow), LABEL_LEN).ToString();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("判断是否是合法瓶签时出错;" + ex.Message);
            }
            return false;
        }

        /// <summary>
        /// 登录登出处理
        /// </summary>
        /// <param name="obj">object[0]:配置台号;object[1]:员工登录号</param>
        private void CheckInOut(int index, string labelData)
        {
            try
            {
                DataTable dt = db.GetPIVAsDB(" select * from QRcodeLog a inner join DEmployee b on a.DEmployeeID=b.DEmployeeID  where QRcode='" 
                    + labelData + "' and DelDT is null and b.IsValid=1 ").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    byte loginStatus = (byte)StaticDictionary.DOCTOR_STATUS_FALSE;//0未登录，1登陆成功
                    string employeeID = string.Empty;
                    string employeeName = string.Empty;

                    if (IsLogin[index][0] == "未登录") //登录操作
                    {
                        employeeName = IsLogin[index][0] = dt.Rows[0]["DEmployeeName"].ToString();
                        employeeID = IsLogin[index][1] = dt.Rows[0]["DEmployeeID"].ToString();
                        CloseALLLight[index] = true;//登陆成功，第一次扫描时需要先关灯

                        listViewMain.SafeAction(() =>
                        {
                            UpdateListViewMainOfLogin(new object[3]
                            { index, dt.Rows[0]["DEmployeeName"].ToString(), dt.Rows[0]["DEmployeeID"].ToString() });
                        });

                        if (opengreen)//微软屏不用红绿灯
                            EnqueueLightQueue(new ControlLightCommand(list_LvMain_Model[index].MoxaIP, list_LvMain_Model[index].MoxaPort, 2, 1,0));//开绿灯
                        loginStatus = (byte)StaticDictionary.DOCTOR_STATUS_TRUE;
                    }
                    else  //登出操作
                    {
                        IsLogin[index][0] = "未登录";
                        IsLogin[index][1] = "未登录";
                        listViewMain.SafeAction(() =>
                        {
                            UpdateListViewMainOfLogOut(index);
                        });

                        if (CloseALLLight[index] == true && opengreen)//关绿灯
                            EnqueueLightQueue(new ControlLightCommand(list_LvMain_Model[index].MoxaIP, list_LvMain_Model[index].MoxaPort, 2, 0,0));

                        if (arrayPort[index] >= 0 && openred)//登出后，判断端口打开，亮红灯，指示端口打开，但未登录,//开红灯
                            EnqueueLightQueue(new ControlLightCommand(list_LvMain_Model[index].MoxaIP, list_LvMain_Model[index].MoxaPort, 1, 1,0));

                        CloseALLLight[index] = false;
                        loginStatus = (byte)StaticDictionary.DOCTOR_STATUS_FALSE;
                    }

                    if (screenType == StaticDictionary.DIWEN_SCREEN)
                    {
                        //将登陆状态发给屏，迪文屏
                        ScreenInfoHandler.Instance.SendLoginStatus(displayController, list_LvMain_Model[index].ScreenIP,
                            list_LvMain_Model[index].ScreenPort, loginStatus);
                    }
                    else if(screenType == StaticDictionary.WINDOWS_SCREEN)
                    {
                        ScreenClientHandler.Instance.SendLoginStatus(screenClientController, list_LvMain_Model[index].ScreenIP,
                            list_LvMain_Model[index].ScreenPort, loginStatus);
                    }
                    else//默认安卓屏
                    {
                        SaveLoginStatus(list_LvMain_Model[index].MoxaIP, list_LvMain_Model[index].MoxaPort, employeeName, employeeID);
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("药师登录登出处理出错:" + ex.Message);
            }
        }

        /// <summary>
        /// 处理单个瓶签
        /// 瓶签处理的主要目的是计费；计费成功就OK；
        /// 计费失败需将失败原因获取到；ChargeResult
        /// </summary>
        /// <param name="index">listmain的序号</param>
        /// <param name="labelData">瓶签数据</param>
        void CheckLabelNo(int index, string labelData)
        {
            string chargeMsg = String.Empty;//调用计费接口，计费错误时返回的错误原因；
            string chargeRtnValue = "0";//调用计费接口的返回值，默认0为计费失败；9未知，其他认为计费成功；
            ControlLightType controlLightType= ControlLightType.ChargeFail;//控制灯的类型,默认为计费失败
            try
            {
                if (IsLogin[index][0] != "未登录" && IsLogin[index][1] != "未登录")
                {
                    if (IsChecked(labelData))//脱机表中存在，表示已扫描且计费成功
                    {
                        charge.ChangeStatus(labelData, IsLogin[index][0].ToString(), list_LvMain_Model[index].MoxaIP, list_LvMain_Model[index].MoxaPort, 1);
                        chargeMsg = ChargeResult_Const.RESCAN_CHARGE_SUCCESS;//重复扫描，已成功计过费
                        controlLightType = ControlLightType.ChargeSuccess;//重复计费按照计费成功处理
                        chargeRtnValue = "1";//重复扫描，认为计费成功，可配置
                    }
                    else
                    {
                        if (IsCharge == "1")//根据配置判断是否计费
                        {
                            InternalLogger.Log.Debug("开始调用计费接口，IsCharge=" + IsCharge);
                            chargeRtnValue = charge.Charge(labelData, IsLogin[index][1], out chargeMsg);
                        }
                        else
                        {
                            //配置文件中设设置，舱内核对时，不计费；认为成功，但追加日志记录
                            InternalLogger.Log.Warn("配置文件中设置，在舱内核对时，不计费；系统将此情况认为计费成功");
                            chargeMsg = ChargeResult_Const.NOTCONFIG_SUCCESS;
                            chargeRtnValue = "1";//将结果设置为成功，UI操作在下面继续处理
                        }

                        InternalLogger.Log.Debug("计费接口的返回值：" + chargeRtnValue);
                        if (chargeRtnValue.Trim().Equals("0"))
                        {
                            //chargeResult使用默认值"0"(计费失败),chargeMsg为接口中返回的值
                            controlLightType = ControlLightType.ChargeFail;//HIS因各种原因造成计费失败，统一归类为计费失败 
                        }
                        else 
                        {
                            //数据库更新卡，原来更新为多线程，会造成结果不准确；重新更新为单线程。
                            UpdateIVStatusAndInsertPZ(IsLogin[index][1], labelData, list_LvMain_Model[index].DeskNO);

                            //chargeMsg为接口中返回的值
                            chargeRtnValue = "1";//将不等于0的情况，归类为等于1（计费成功）
                            controlLightType = ControlLightType.ChargeSuccess;//计费成功 
                            AddTodt(labelData);//标记是否已扫码且计费成功过
                        }

                        if (screenType == StaticDictionary.ANDROID_SCREEN)//安卓屏，最后根据计费结果，更新数据库表
                        {
                            string strSQL = String.Format("update ScreenDetail set DemployeeID='{0}',DemployeeName='{1}',LabelNo='{2}',Result='{3}',Msg='{4}' "
                                +" where MoxaIP='{5}' and port='{6}'", IsLogin[index][1], IsLogin[index][0], labelData,
                                chargeRtnValue, chargeMsg, list_LvMain_Model[index].MoxaIP, list_LvMain_Model[index].MoxaPort);
                            db.SetPIVAsDB(strSQL);
                        }
                    }
                }
                else
                {
                    controlLightType = ControlLightType.ChargeFail;//计费失败
                    chargeMsg = ChargeResult_Const.PHARMACIST_LOGOUT;
                }
                //根据计费结果，控制灯响应
                InternalLogger.Log.Info(String.Format("瓶签号：{0},计费结果：{1}", labelData, chargeMsg));//这句日志是重要日志，可以据此查询计费结果
                if(!screenType.Equals(StaticDictionary.WINDOWS_SCREEN))//不是微软屏,才去控制红绿灯
                    LightController(list_LvMain_Model[index].MoxaIP, list_LvMain_Model[index].MoxaPort, controlLightType);
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("处理Moxa发送的单个瓶签时出错:" + ex.Message);
                //只更新UI，不控制灯
                chargeMsg = ChargeResult_Const.LABELHANDLE_EXCEPTION;
            }
            finally
            {
                //根据计费结果，更新UI
                this.SafeAction(() =>
                {
                    UpdatelistViewMainOfResult(index, labelData, chargeMsg);
                    UpdateListViewLabelScan(index, labelData, chargeMsg, chargeRtnValue.Equals("0") ? false : true);
                });

                //UpdatelistViewMainOfResult会更新配置数量，直接用界面上listViewMain.Items[index].SubItems[7]的值
                string count = listViewMain.Items[index].SubItems[7].Text.Trim();
                if (screenType == StaticDictionary.DIWEN_SCREEN) //迪文屏
                {
                    ScreenInfoHandler.Instance.SendInfoToScreen(displayController, list_LvMain_Model[index].ScreenIP,
                        list_LvMain_Model[index].ScreenPort, labelData, chargeRtnValue, chargeMsg, this.IsLogin[index][0].Trim(), count);
                }
                else if(screenType == StaticDictionary.WINDOWS_SCREEN)//微软屏
                {
                    ScreenClientHandler.Instance.SendInfoToScreen(screenClientController, list_LvMain_Model[index].ScreenIP,
                        list_LvMain_Model[index].ScreenPort, labelData, short.Parse(chargeRtnValue), chargeMsg, this.IsLogin[index][0].Trim(), count);
                } 
                else//默认安卓屏
                {
                    SaveLabelInfo(index, chargeRtnValue, chargeMsg, labelData, count);
                }
            }
        }

        /// <summary>
        /// 调用这个函数有个条件，配置计费成功
        /// 更新IVStatus=9，并插入IVRecord_PZ表
        /// </summary>
        void UpdateIVStatusAndInsertPZ(string DEmployeeID, string LabelNo,string location)
        {
            try
            {
                string strUpdate1 = "update IVRecord set IVStatus =9  where LabelNo =" + LabelNo + " and IVStatus<9  ";
                db.SetPIVAsDB(strUpdate1);

               string strInsert = string.Format(" insert into IVRecord_PZ(IVrecordID, PZDT, ScanCount, pcode, Location,[Type]) "
                     + "select {0},getdate(),0,{1},'{2}',1 where not exists(select * from IVRecord_PZ where IVRecordID = {0})  ",
                LabelNo, DEmployeeID, location);
                db.SetPIVAsDB(strInsert);

                string strUpdate2 = "  update IVRecord_Scan5 set Confirm=2 where LabelNo = '" + LabelNo + "'";
                db.SetPIVAsDB(strUpdate2);
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("更新IVStatus=9，并插入IVRecord_PZ表error:" + ex.Message);
            }
        }

        /// <summary>
        /// 搜索数据库前，判断是否已配置
        /// </summary>
        /// <param name="labelno"></param>
        /// <returns></returns>
        private bool IsChecked(string labelno)
        {
            try
            {
                lock (dtCheckResult.Rows.SyncRoot)
                {
                    if (dtCheckResult.Select("labelno ='" + labelno + "'").Length > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("IsChecked error:" + ex.Message);
                return false;
            }
        }
        private void AddTodt(string labelno)
        {
            try
            {
                lock (dtCheckResult.Rows.SyncRoot)
                {
                    DataRow dr = dtCheckResult.NewRow();
                    dr[0] = labelno;

                    dtCheckResult.Rows.Add(dr);
                    dtCheckResult.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("AddTodt" + ex.Message);
            }
        }
        #endregion

        #region 实现moxaContrl事件  断线重连和数据接收
        private void MoxaContrl_DataReceived(object sender, PivasEventArgs<TCPMessage> e)
        {
            try
            {
                for (int index = 0; index < listViewMain.Items.Count; index++)
                {
                    if (list_LvMain_Model[index].MoxaIP.Trim().Equals(e.Value.ServerIp.Trim())
                        && list_LvMain_Model[index].MoxaPort.ToString().Equals(e.Value.ServerPort.ToString()))//匹配到某项
                    {
                        string moxaData = e.Value.TcpData;
                        InternalLogger.Log.Info(String.Format("从Moxa[{0}:{1}]读取到数据{2}",
                            list_LvMain_Model[index].MoxaIP, list_LvMain_Model[index].MoxaPort, moxaData));

                        if (CloseALLLight[index] == true)
                        {
                            //这里主要是因登录成功后，绿灯亮了。扫瓶签时，会先关闭，这里若首次扫瓶签是红灯，会造成红灯一致亮
                            EnqueueLightQueue(new ControlLightCommand(list_LvMain_Model[index].MoxaIP, list_LvMain_Model[index].MoxaPort, 2, 0, 0));//关绿灯
                            if (openred)
                                EnqueueLightQueue(new ControlLightCommand(list_LvMain_Model[index].MoxaIP, list_LvMain_Model[index].MoxaPort, 1, 0, 0));//关红灯
                            CloseALLLight[index] = false;
                        }

                        String labelData = String.Empty;
                        if (IsLogionData(moxaData, out labelData)) //员工登录
                        {
                            InternalLogger.Log.Debug(moxaData + "被判断为登录信息,因7777开头且长度为22");
                            CheckInOut(index, labelData);//登录处理
                        }
                        else if (IsLabelNo(moxaData, out labelData))//只对今天和明天的瓶签计费；并校验瓶签长度和瓶签标识
                        {
                            InternalLogger.Log.Debug(moxaData + "被判断瓶签数据");
                            CheckLabelNo(index, labelData);
                        }
                        else
                        {
                            InternalLogger.Log.Warn(moxaData + "被判断为非法Moxa数据，不做处理也不显示");
                            return; //跳出数据接收处理方法
                        }

                        //合法moxa数据时，更新UI
                        this.SafeAction(() =>
                        {
                            UpdateListViewDeskInfo(listViewMain.Items[index].SubItems[0].Text,
                                listViewMain.Items[index].SubItems[2].Text, labelData);
                            listViewMain.Items[index].SubItems[5].Text = labelData;
                        });
                        break;//跳出for
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("接收到Moxa数据，处理出错：" + ex.Message);
            }
        }

        private void MoxaContrl_Disconnected(object sender, PivasEventArgs<MOXATcpClient> e)
        {
            try
            {                
                for (int i = 0; i < list_LvMain_Model.Count; i++)
                {
                    int port = e.Value.ServerPort - moxaContrl.GetMOXA_BASEPORT;
                    if (list_LvMain_Model[i].MoxaIP.Trim().Equals(e.Value.ServerIp)
                      && list_LvMain_Model[i].MoxaPort.Trim().Equals(port.ToString())
                      && arrayPort[i] >= 0)
                    {
                        //属于界面上显示打开了端口，但触发断线的情况；需做断线重连
                        e.Value.Stop(); //需要先关闭，再重连；关闭结果不重要
                        if (!e.Value.Start())
                        {
                            InternalLogger.Log.Warn(String.Format("Moxa {0}:{1}重连失败。会再次等待重连",
                                e.Value.ServerIp, port));
                        }
                        else
                        {
                            InternalLogger.Log.Warn(String.Format("Moxa {0}:{1}重连成功",
                                  e.Value.ServerIp, port));
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("MOXA断线后处理出错："+ex.Message);
            }
        }
        #endregion

        #region 实现DisplayController事件，连接成功与失败，更新UI
        private void DisplayController_Disconnected(object sender, PivasEventArgs<TCPMessageEx> e)
        {
            try
            {
                UpdateListViewPort(string.Format("屏 IP:{0}；端口:{1}；因网络或设备原因，造成连接断开",
                    e.Value.ServerIp, e.Value.ServerPort));
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("屏连接断开后，更新UI出错：" + ex.Message);
            }
        }

        private void DisplayController_Connected(object sender, PivasEventArgs<TCPMessageEx> e)
        {
            try
            {
                UpdateListViewPort(string.Format("屏 IP:{0}；端口:{1}；连接成功",
                    e.Value.ServerIp, e.Value.ServerPort));
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("屏连接成功后，更新UI出错：" + ex.Message);
            }
        }
        #endregion

        #region UI线程安全更新操作
        private void UpdateListViewDeskInfo(string B, string C, string a)
        {
            ListViewItem j = new ListViewItem();
            j.Text = listViewDeskInfo.Items.Count.ToString();
            j.SubItems.Add(B);
            j.SubItems.Add(C);
            j.SubItems.Add(a);
            j.SubItems.Add(DateTime.Now.ToString());
            listViewDeskInfo.Items.Add(j);
        }

        private void UpdateListViewSendPLCInfo(string moxaIp, string moxaPort, int lightColor, int lightStatus)
        {
            ListViewItem item = new ListViewItem();
            item.Text = listViewSendPLCInfo.Items.Count.ToString();
            item.SubItems.Add(moxaIp);
            item.SubItems.Add(moxaPort);
            if (lightColor == 1)//红灯
            {
                item.SubItems.Add("红");
            }
            else if (lightColor == 2)//绿灯
            {
                item.SubItems.Add("绿");
            }

            if (lightStatus == 1)//开
            {
                item.SubItems.Add("开");
            }
            else if (lightStatus == 2)//关
            {
                item.SubItems.Add("关");
            }
            else
            {
                item.SubItems.Add("关");
            }

            item.SubItems.Add(DateTime.Now.ToString());
            listViewSendPLCInfo.Items.Add(item);
            //在窗体显示的同时，将信息记录到日志
            InternalLogger.Log.Debug(String.Format("PLC发送信息-ID：{0}，IP：{1}，端口/条码枪：{2}，灯：{3}，指令：{4}，处理时间：{5}",
                item.SubItems[0].Text, item.SubItems[1].Text, item.SubItems[2].Text,
                item.SubItems[3].Text, item.SubItems[4].Text, DateTime.Now));
        }
        /// <summary>
        /// 各个端口的员工登录（界面操作）
        /// </summary>
        /// <param name="obj">端口号</param>
        private void UpdateListViewMainOfLogin(object obj)
        {
            object[] newobj = obj as object[];
            int i = (int)newobj[0];
            listViewMain.Items[i].SubItems[2].Text = newobj[1].ToString();
            listViewMain.Items[i].SubItems[8].Text = newobj[2].ToString();
            listViewMain.Items[i].SubItems[3].Text = DateTime.Now.ToString();
            listViewMain.Items[i].BackColor = Color.PaleGreen;
        }

        /// <summary>
        /// 各个端口的员工登出（界面操作）
        /// </summary>
        /// <param name="obj">端口号</param>
        private void UpdateListViewMainOfLogOut(object obj)
        {
            int i = (int)obj;
            listViewMain.Items[i].SubItems[2].Text = "未登录";
            listViewMain.Items[i].SubItems[7].Text = "0";
            listViewMain.Items[i].SubItems[8].Text = "";
            listViewMain.Items[i].SubItems[3].Text = "未登录";
            listViewMain.Items[i].SubItems[4].Text = "0";
            if (arrayPort[i] >= 0)
            {
                listViewMain.Items[i].BackColor = Color.White;
            }
            else
            {
                listViewMain.Items[i].BackColor = Color.Pink;
            }
        }

        /// <summary>
        /// 根据瓶签结果更新
        /// </summary>
        private void UpdatelistViewMainOfResult(int index, string labenno, string chargeMsg)
        {
            try
            {
                //无论成功与否都进行累加
                //if (chargeMsg == ChargeResult_Const.CHARGE_SUCCESS)
                //{
                    listViewMain.Items[index].SubItems[4].Text = (int.Parse(listViewMain.Items[index].SubItems[4].Text) + 1).ToString();//这个数字统计不对
                //}
                listViewMain.Items[index].SubItems[5].Text = labenno;
                listViewMain.Items[index].SubItems[6].Text = chargeMsg;
                if (chargeMsg != ChargeResult_Const.PHARMACIST_LOGOUT)
                {
                    string strPZDT = String.Empty;//药师登录时间
                    if (listViewMain.Items[index].SubItems[3].Text.Trim().Equals("未登录"))
                        strPZDT = DateTime.Now.ToString();
                    else
                        strPZDT = listViewMain.Items[index].SubItems[3].Text;

                    //统计药师自登录时间到目前已配置的数量。
                    string str = " select COUNT (IVRecordID) as 配置数量 from IVRecord_PZ where PCode ='"
                        + listViewMain.Items[index].SubItems[8].Text + "' and PZDT>='"
                        + strPZDT + "' and ScanCount='0'";

                    if (db.GetPIVAsDB(str).Tables.Count > 0 && db.GetPIVAsDB(str).Tables[0].Rows.Count > 0)
                    {
                        listViewMain.Items[index].SubItems[7].Text = db.GetPIVAsDB(str).Tables[0].Rows[0]["配置数量"].ToString();
                    }
                    else
                    {
                        listViewMain.Items[index].SubItems[7].Text = "0";
                    }
                }
            }
            catch (Exception e)
            {
                InternalLogger.Log.Error("将瓶签结果更新到listViewMain出错：" + e.Message);
            }
        }

        /// <summary>
        /// 更新瓶签扫描信息列表
        /// </summary>
        /// <param name="index"></param>
        /// <param name="labenno"></param>
        /// <param name="chareMsg"></param>
        /// <param name="chargeRtnValue"></param>
        private void UpdateListViewLabelScan(int index, string labenno, string chareMsg,bool chargeSuccess)
        {
            try
            {
                ListViewItem lv = new ListViewItem();
                lv.Text = labenno;
                lv.SubItems.Add(DateTime.Now.ToString());
                lv.SubItems.Add(chareMsg);

                lv.BackColor = chargeSuccess ? Color.PaleGreen : Color.Pink;
                listViewLabelScan.Items.Insert(0, lv);
                //在窗体显示的同时，将信息记录到日志
                InternalLogger.Log.Debug(String.Format("瓶签扫描信息-瓶签号：{0}，处理时间：{1}，计费结果：{2}", labenno, DateTime.Now, chareMsg));
            }
            catch (Exception e)
            {
                InternalLogger.Log.Error("UpdateListViewLabelScan function error：" + e.Message);
            }
        }

        /// <summary>
        /// 更新操作信息表
        /// </summary>
        /// <param name="info">更新的信息</param>
        private void UpdateListViewPort(string info)
        {
            try
            {
                ListViewItem j = new ListViewItem();
                j.Text = (listViewPort.Items.Count + 1).ToString();
                j.SubItems.Add(DateTime.Now.ToString());
                j.SubItems.Add(info);
                listViewPort.SafeAction(() =>
                {
                    listViewPort.Items.Insert(0,j);//add->insert
                });
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("更新操作信息表出错：" + ex.Message);
            }
        }
        #endregion

        #region 界面用户操作
        private void Set_Click(object sender, EventArgs e)
        {
            try
            {
                ClosePort();
                frmSet s = new frmSet();
                if (s.ShowDialog() == DialogResult.Cancel)
                {
                    frmMcc_Load(null, null);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("MCC设置 窗体加载错误" + ex.Message);
            }
        }
        /// <summary>
        /// 打开所有端口，线程调用，加安全保护
        /// </summary>
        private void OpenPort()
        {
            string ip = "";
            try
            {
                for (int i = 0; i < listViewMain.Items.Count; i++)
                {
                    int num = int.Parse(list_LvMain_Model[i].MoxaPort);
                    ip = list_LvMain_Model[i].MoxaIP;
                    if (arrayPort[i] < 0)//磨砂端口未打开
                    {
                        arrayPort[i] = moxaContrl.MoxaOpen(ip, num);
                        if (arrayPort[i] < 0)
                        {
                            UpdateListViewPort("MOXA IP:" + ip + "；" + "端口:" + num + "；无法打开");
                            listViewMain.SafeAction(() =>
                            {
                                listViewMain.Items[i].BackColor = Color.Pink;//打开端口失败，背景色粉
                            });
                        }
                        else
                        {
                            listViewMain.SafeAction(() =>
                            {
                                listViewMain.Items[i].SubItems[1].Text = "开启";
                                listViewMain.Items[i].SubItems[2].Text = "未登录";
                                listViewMain.Items[i].SubItems[3].Text = "未登录";
                                listViewMain.Items[i].BackColor = Color.White;//打开端口成功，未登录，背景色白
                            });

                            UpdateListViewPort("MOXA IP:" + ip + "；" + "端口:" + num + "；打开成功");
                            if (openred)//开红灯
                                EnqueueLightQueue(new ControlLightCommand(list_LvMain_Model[i].MoxaIP, list_LvMain_Model[i].MoxaPort, 1, 1, 0));
                        }
                    }
                    else//磨砂端口已打开
                    {
                        UpdateListViewPort("MOXA IP:" + ip + "；" + "端口:" + num + "；已打开");
                    }
                }
            }
            catch
            {
                InternalLogger.Log.Error("打开MOXA所有端口时出错，磨砂ip:" + ip);
            }
        }

        /// <summary>
        /// 打开单个端口，ui线程调用
        /// </summary>
        private void OpenSinglePort()
        {
            try
            {
                int i = listViewMain.SelectedItems[0].Index;
                int num = int.Parse(list_LvMain_Model[i].MoxaPort);
                string ip = list_LvMain_Model[i].MoxaIP;
                if (arrayPort[i] < 0)//磨砂端口未打开
                {
                    arrayPort[i] = moxaContrl.MoxaOpen(ip, num);
                    if (arrayPort[i] < 0)
                    {
                        UpdateListViewPort("MOXA IP:" + ip + "；" + "端口:" + num + "；无法打开");
                        listViewMain.Items[i].BackColor = Color.Pink;//打开端口失败，背景色粉
                    }
                    else
                    {
                        listViewMain.Items[i].SubItems[1].Text = "开启";
                        listViewMain.Items[i].SubItems[2].Text = "未登录";
                        listViewMain.Items[i].SubItems[3].Text = "未登录";
                        listViewMain.Items[i].BackColor = Color.White;//打开端口成功，未登录，背景色白

                        UpdateListViewPort("MOXA IP:" + ip + "；" + "端口:" + num + "；打开成功");

                        if (openred)//开红灯
                            EnqueueLightQueue(new ControlLightCommand(list_LvMain_Model[i].MoxaIP, list_LvMain_Model[i].MoxaPort, 1, 1, 0));
                    }
                }
                else//磨砂端口已打开
                {
                    UpdateListViewPort("MOXA IP:" + ip + "；" + "端口:" + num + "；已打开");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开MOXA端口时出错:" + ex.Message);
            }
        }

        /// <summary>
        /// 关端口,UI和线程都可能调用，加安全保护
        /// </summary>
        private void ClosePort()
        {
            for (int i = 0; i < listViewMain.Items.Count; i++)
            {
                string ip = list_LvMain_Model[i].MoxaIP;

                if (arrayPort[i] >= 0)//磨砂端口打开中
                {
                    int a = moxaContrl.MoxaClose(ip, arrayPort[i]);
                    if (a != 0)
                    {
                        UpdateListViewPort("MOXA IP:" + ip + "；" + "端口:" + arrayPort[i] + "；关闭失败");
                    }
                    else
                    {
                        arrayPort[i] = -1;
                        UpdateListViewPort("MOXA IP:" + ip + "；" + "端口:" + arrayPort[i] + "；关闭成功");

                        listViewMain.SafeAction(() =>
                        {
                            listViewMain.Items[i].SubItems[1].Text = "关闭";
                            listViewMain.Items[i].BackColor = Color.Pink;//打开端口失败，背景色粉
                        });

                        //关绿灯，来自登陆成功
                        EnqueueLightQueue(new ControlLightCommand(list_LvMain_Model[i].MoxaIP, list_LvMain_Model[i].MoxaPort, 2, 0, 0));
                        if (openred)
                            //关红灯，来自端口打开成功
                            EnqueueLightQueue(new ControlLightCommand(list_LvMain_Model[i].MoxaIP, list_LvMain_Model[i].MoxaPort, 1, 0, 0));
                    }
                }
                else//已关闭
                {
                    UpdateListViewPort("MOXA IP:" + ip + "；" + "端口:" + arrayPort[i] + "；已关闭");
                }
            }
        }

        /// <summary>
        /// 关闭单个端口，ui线程调用
        /// </summary>
        private void CloseSinglePort()
        {
            int i = listViewMain.SelectedItems[0].Index;
            string ip = list_LvMain_Model[i].MoxaIP;
            if (arrayPort[i] >= 0)//磨砂端口打开中
            {
                int a = moxaContrl.MoxaClose(ip, arrayPort[i]);
                if (a != 0)
                {
                    UpdateListViewPort("MOXA IP:" + ip + "；" + "端口:" + arrayPort[i] + "；关闭失败");
                }
                else
                {
                    arrayPort[i] = -1;
                    UpdateListViewPort("MOXA IP:" + ip + "；" + "端口:" + arrayPort[i] + "；关闭成功");

                    listViewMain.Items[i].SubItems[1].Text = "关闭";
                    listViewMain.Items[i].BackColor = Color.Pink;//打开端口失败，背景色粉

                    //关绿灯，来自登陆成功
                    EnqueueLightQueue(new ControlLightCommand(list_LvMain_Model[i].MoxaIP, list_LvMain_Model[i].MoxaPort, 2, 0, 0));

                    if (openred)
                        //关红灯，来自端口打开成功
                        EnqueueLightQueue(new ControlLightCommand(list_LvMain_Model[i].MoxaIP, list_LvMain_Model[i].MoxaPort, 1, 0, 0));
                }
            }
            else//已关闭
            {
                UpdateListViewPort("MOXA IP:" + ip + "；" + "端口:" + arrayPort[i] + "；已关闭");
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenSinglePort();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确认关闭端口?", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    listViewPort.Items.Clear();
                    CloseSinglePort();
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("关闭端口出错" + ex.Message);
            }
        }

        /// <summary>
        /// 窗体加载时，从数据库获取配置台信息，保持不变
        /// </summary>
        private void GetMoxaList()
        {
            list_LvMain_Model.Clear();
            DataTable dt = new DataTable();
            try
            {
                string sql = "select * from MOXACon where MOXAPort is not null and [Group]='" + MoxaGroup + "'";
                dt = db.GetPIVAsDB(sql).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ListviewMain_Model lvMain_Model = new ListviewMain_Model();
                        lvMain_Model.DeskNO = dt.Rows[i]["DeskNo"].ToString();
                        lvMain_Model.DeskDesc = dt.Rows[i]["DeskDesc"].ToString();
                        lvMain_Model.MoxaIP = dt.Rows[i]["MOXAIP"].ToString();
                        lvMain_Model.MoxaPort = dt.Rows[i]["MOXAPort"].ToString();
                        lvMain_Model.PLC = dt.Rows[i]["PLC"].ToString();
                        lvMain_Model.RedLight = dt.Rows[i]["RedLight"].ToString();
                        lvMain_Model.GreenLight = dt.Rows[i]["GreenLight"].ToString();
                        lvMain_Model.GreenLight = dt.Rows[i]["GreenLight"].ToString();

                        if (screenType == StaticDictionary.DIWEN_SCREEN || screenType == StaticDictionary.WINDOWS_SCREEN)
                        {
                            if (string.IsNullOrEmpty(dt.Rows[i]["ScreenIP"].ToString()))
                            {
                                string str = string.Format("控制台 {0} 没有配置屏IP，确认不用屏吗？\r\n若用屏请点击 否 后设置屏IP",
                                    dt.Rows[i]["DeskNo"].ToString());
                                if (MessageBox.Show(str, "确认", MessageBoxButtons.YesNo) == DialogResult.No)
                                    return;
                            }
                            lvMain_Model.ScreenIP = dt.Rows[i]["ScreenIP"].ToString();
                            int port = Int32.Parse(db.IniReadValuePivas("SCREEN", "ServerPort").Trim());
                            lvMain_Model.ScreenPort = port;//新屏的端口配置死；迪文屏必须是8080端口
                        }

                        list_LvMain_Model.Add(lvMain_Model);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取配置台信息失败！"+ ex.Message);
            }
        }

        //主画面显示配置台列表
        private void ShowMoxaList()
        {
            try
            {
                listViewMain.Items.Clear();
                for (int j = 0; j < list_LvMain_Model.Count; j++)
                {
                    ListViewItem i = new ListViewItem();
                    i.Text = list_LvMain_Model[j].DeskNO;
                    i.SubItems.Add("关闭");
                    i.SubItems.Add(("未登录"));
                    i.SubItems.Add(("未登录"));
                    i.SubItems.Add(("0"));
                    i.SubItems.Add((""));
                    i.SubItems.Add((""));
                    i.SubItems.Add(("0"));
                    i.SubItems.Add((""));
                    listViewMain.Items.Add(i);
                }
                arrayPort = new int[list_LvMain_Model.Count];//端口状态初始化
                for (int i = 0; i < list_LvMain_Model.Count; i++)
                {
                    arrayPort[i] = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("显示配置台列表错误" + ex.Message);
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < arrayPort.Length; i++)
            {
                if (arrayPort[i] >= 0)
                {
                    MessageBox.Show("请关闭端口");
                    return ;
                }
            }
            try
            {
                ClearThread();
                this.notifyIcon1.Dispose();
                //Application.Exit();
                Environment.Exit(0);
            }
            catch { }
        }

        private void notifyIcon1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void CMSexit_Click(object sender, EventArgs e)
        {
            try
            {
                ClearThread();
                this.notifyIcon1.Dispose();
                //Application.Exit();
                Environment.Exit(0);
            }
            catch { }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("label1_MouseDown错误" + ex.Message);
            }
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(32, 167, 218);
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(19, 179, 253);
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel2_MouseHover(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(32, 167, 218);
        }

        private void panel2_MouseLeave(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(19, 179, 253);
        }

        //UI双击登陆登出
        private void listViewMain_DoubleClick(object sender, EventArgs e)
        {
            LoginInOrOut();
        }

        /// <summary>
        /// 登陆登出操作
        /// </summary>
        private void LoginInOrOut()
        {
            try
            {
                if (listViewMain.SelectedItems[0].SubItems[2].Text == "未登录")
                {
                    LoginUser loginUser = new LoginUser(arrayPort[listViewMain.SelectedItems[0].Index]);
                    LoginUser.loginUser -= new LoginUser.mydelegate(ShowLogin);
                    LoginUser.loginUser += new LoginUser.mydelegate(ShowLogin);
                    loginUser.ShowDialog();
                }
                else
                {
                    IsLogin[listViewMain.SelectedIndices[0]][0] = "未登录";
                    IsLogin[listViewMain.SelectedIndices[0]][1] = "未登录";
                    listViewMain.SelectedItems[0].SubItems[2].Text = "未登录";
                    listViewMain.SelectedItems[0].SubItems[3].Text = "未登录";
                    listViewMain.SelectedItems[0].SubItems[4].Text = "0";
                    listViewMain.SelectedItems[0].SubItems[5].Text = "";
                    listViewMain.SelectedItems[0].SubItems[6].Text = "";
                    listViewMain.SelectedItems[0].SubItems[8].Text = "";

                    int i = listViewMain.SelectedItems[0].Index;
                    if (CloseALLLight[i] == true)//关绿灯
                        EnqueueLightQueue(new ControlLightCommand(list_LvMain_Model[i].MoxaIP, list_LvMain_Model[i].MoxaPort, 2, 0, 0));

                    if (arrayPort[i] >= 0) //登出后，判断端口打开，亮红灯，指示端口打开，但未登录
                    {
                        if (openred)//开红灯
                            EnqueueLightQueue(new ControlLightCommand(list_LvMain_Model[i].MoxaIP, list_LvMain_Model[i].MoxaPort, 1, 1, 0));
                        listViewMain.Items[i].BackColor = Color.White;
                    }
                    else
                        listViewMain.Items[i].BackColor = Color.Pink;

                    if (screenType == StaticDictionary.DIWEN_SCREEN)
                    {
                        ScreenInfoHandler.Instance.SendLoginStatus(displayController, list_LvMain_Model[i].ScreenIP,
                            list_LvMain_Model[i].ScreenPort, (ushort)StaticDictionary.DOCTOR_STATUS_FALSE);
                    }
                    else if (screenType == StaticDictionary.DIWEN_SCREEN)
                    {
                        ScreenClientHandler.Instance.SendLoginStatus(screenClientController, list_LvMain_Model[i].ScreenIP,
                            list_LvMain_Model[i].ScreenPort, StaticDictionary.DOCTOR_STATUS_FALSE);
                    }
                    else
                        SaveLoginStatus(list_LvMain_Model[i].MoxaIP, list_LvMain_Model[i].MoxaPort, string.Empty, string.Empty);
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("界面上登录登出操作出错:" + ex.Message);
            }
        }

        private void ShowLogin(string name, string id)
        {
            IsLogin[listViewMain.SelectedIndices[0]][0] = name;
            IsLogin[listViewMain.SelectedIndices[0]][1] = id;
            listViewMain.SafeAction(() =>
            {
                listViewMain.SelectedItems[0].SubItems[2].Text = name;
                listViewMain.SelectedItems[0].SubItems[3].Text = DateTime.Now.ToString();
                listViewMain.SelectedItems[0].SubItems[4].Text = "0";
                listViewMain.SelectedItems[0].SubItems[5].Text = "";
                listViewMain.SelectedItems[0].SubItems[6].Text = "";
                listViewMain.SelectedItems[0].SubItems[8].Text = id;
                listViewMain.SelectedItems[0].BackColor = Color.PaleGreen;
            });
           
            int i = listViewMain.SelectedItems[0].Index;
            if (opengreen) //开绿灯
                EnqueueLightQueue(new ControlLightCommand(list_LvMain_Model[i].MoxaIP, list_LvMain_Model[i].MoxaPort, 2, 1, 0));
            CloseALLLight[i] = true;//登陆成功，第一次扫描时需要先关灯

            if (screenType == StaticDictionary.DIWEN_SCREEN)
            {
                ScreenInfoHandler.Instance.SendLoginStatus(displayController, list_LvMain_Model[i].ScreenIP,
                    list_LvMain_Model[i].ScreenPort, (ushort)StaticDictionary.DOCTOR_STATUS_TRUE);
            }
            else if (screenType == StaticDictionary.WINDOWS_SCREEN)
            {
                ScreenClientHandler.Instance.SendLoginStatus(screenClientController, list_LvMain_Model[i].ScreenIP,
                            list_LvMain_Model[i].ScreenPort, StaticDictionary.DOCTOR_STATUS_TRUE);
            }
            else//默认安卓屏
                SaveLoginStatus(list_LvMain_Model[i].MoxaIP, list_LvMain_Model[i].MoxaPort, name, id);

        }

        private void Panel_Max_None_MouseHover(object sender, EventArgs e)
        {
            Panel_Max_None.BackColor = Color.FromArgb(32, 167, 218);
        }

        private void Panel_Max_None_MouseLeave(object sender, EventArgs e)
        {
            Panel_Max_None.BackColor = Color.FromArgb(19, 179, 253);
        }

        private void Panel_Max_None_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Normal)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                Panel_Max_None.BackgroundImage = global::PivasMcc.Properties.Resources.还原;

            }
            else
            {
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;
                Panel_Max_None.BackgroundImage = global::PivasMcc.Properties.Resources._20;
            }
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
           if (this.WindowState == System.Windows.Forms.FormWindowState.Normal)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                Panel_Max_None.BackgroundImage = global::PivasMcc.Properties.Resources.还原;

            }
            else
            {
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;
                Panel_Max_None.BackgroundImage = global::PivasMcc.Properties.Resources._20;
            }
        }

        private void MenuItemOpenAllPort_Click(object sender, EventArgs e)
        {
            try
            {
                listViewPort.Items.Clear();
                ThreadStart threadstart = new ThreadStart(OpenPort);
                Thread thread = new Thread(threadstart);
                thread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开所有端口错误" + ex.Message);
            }
        }

        private void MenuItemCloseAllPort_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确认关闭所有Moxa连接?", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    listViewPort.Items.Clear();
                    ClosePort();
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("关闭所有端口出错：" + ex.Message);
            }
        }

        private void 打开屏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = listViewMain.SelectedItems[0].Index;
            OpenOneScreen(i);
        }

        private void 关闭屏连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = listViewMain.SelectedItems[0].Index;
            if (MessageBox.Show("确认关闭当前屏连接?", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                CloseOneScreen(i);
            }
        }

        private void MenuItemOpenAllScreen_Click(object sender, EventArgs e)
        {
            try
            {
                ThreadStart threadstart = new ThreadStart(() =>
                {
                    for (int i = 0; i < listViewMain.Items.Count; i++)
                        OpenOneScreen(i);
                });
                Thread thread = new Thread(threadstart);
                thread.Start();
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("打开屏连接错误：" + ex.Message);
            }
        }

        private void MenuItemCloseAllScreen_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确认关闭所有屏连接?", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ThreadStart threadstart = new ThreadStart(() =>
                    {
                        for (int i = 0; i < listViewMain.Items.Count; i++)
                        {
                            CloseOneScreen(i);
                        }
                    });
                    Thread thread = new Thread(threadstart);
                    thread.Start();
                    displayController.ReConnnect = false;//用户在界面上关闭了连接（不管结果），就关闭重连
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("关闭屏连接错误：" + ex.Message);
            }
        }

        private void OpenOneScreen(int index)
        {
            try
            {
                if (!string.IsNullOrEmpty(list_LvMain_Model[index].ScreenIP))
                {
                    if (displayController.DisplayOpen(list_LvMain_Model[index].ScreenIP, list_LvMain_Model[index].ScreenPort))
                    {
                        displayController.ReConnnect = true;//用户在界面上开启了连接（不管结果），就开启重连
                    }
                    else
                    {
                        UpdateListViewPort("屏IP:" + list_LvMain_Model[index].ScreenIP + "；" + "端口:" + list_LvMain_Model[index].ScreenPort + "；无法打开");
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("打开一个屏连接错误：" + ex.Message);
            }
        }

        /// <summary>
        /// ReConnnect属性只有全部关闭，才能赋值为false
        /// </summary>
        /// <param name="index"></param>
        private void CloseOneScreen(int index)
        {
            try
            {
                if (!string.IsNullOrEmpty(list_LvMain_Model[index].ScreenIP))
                {
                    displayController.DisplayClose(list_LvMain_Model[index].ScreenIP, list_LvMain_Model[index].ScreenPort);
                    UpdateListViewPort(string.Format("屏 IP:{0}；端口:{1}；断开连接",
                        list_LvMain_Model[index].ScreenIP, list_LvMain_Model[index].ScreenPort));
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("关闭一个屏连接错误：" + ex.Message);
            }
        }

        private void GetLightRule()
        {
            string SQL = "SELECT * from LightModel WHere DeskNo = '默认设置'";
                DataSet ds = db.GetPIVAsDB(SQL);

                Slong = Convert.ToInt32( ds.Tables[0].Rows[0]["SLong"].ToString());
                Flong = Convert.ToInt32( ds.Tables[0].Rows[0]["FLong"].ToString());
                Blong = Convert.ToInt32( ds.Tables[0].Rows[0]["BLong"].ToString());

                Stwin = Convert.ToInt32( ds.Tables[0].Rows[0]["STwinkle"].ToString());
                Ftwin = Convert.ToInt32( ds.Tables[0].Rows[0]["FTwinkle"].ToString());
                Btwin = Convert.ToInt32( ds.Tables[0].Rows[0]["BTwinkle"].ToString());

                STime = Convert.ToInt32( ds.Tables[0].Rows[0]["STimes"].ToString());
                FTime = Convert.ToInt32( ds.Tables[0].Rows[0]["FTimes"].ToString());
                BTime = Convert.ToInt32( ds.Tables[0].Rows[0]["BTimes"].ToString());

                if (ds.Tables[0].Rows[0]["SRemind"].ToString() == "True")
                    SUseLong = false;
                else
                    SUseLong = true ;

                if (ds.Tables[0].Rows[0]["FRemind"].ToString() == "True")
                    FUseLong = false;
                else
                    FUseLong= true;

                if (ds.Tables[0].Rows[0]["BRemind"].ToString() == "True")
                    BUseLong = false;
                else
                    BUseLong = true;
        }

        public void LoadINI()
        {
            string RedValue = db.IniReadValuePivas("doublelight", "openred");

            if (RedValue == "1")
            {
               openred = true;
            }
            else if (RedValue == "0")
            {
                openred = false;
            }
            else
            {
                MessageBox.Show("初始红灯未设置，默认使用");
                db.IniWriteValuePivas("doublelight", "openred", "1");
                openred = true;
            }

            string GreenValue = db.IniReadValuePivas("doublelight", "opengreen");

            if (GreenValue == "1")
            {
                opengreen = true;
            }
            else if (GreenValue == "0")
            {
                opengreen = false;
            }
            else
            {
                MessageBox.Show("初始绿灯未设置，默认使用");
                db.IniWriteValuePivas("doublelight", "opengreen", "1");
                opengreen = true;
            }

            screenType = db.IniReadValuePivas("SCREEN", "type");
            if (screenType != StaticDictionary.ANDROID_SCREEN 
                && screenType != StaticDictionary.DIWEN_SCREEN 
                && screenType != StaticDictionary.WINDOWS_SCREEN)
            {
                MessageBox.Show("屏类型未设置，默认使用旧版");
                db.IniWriteValuePivas("SCREEN", "type", "0");
            }

            MoxaGroup = db.IniReadValuePivas("MOXA", "Group");
            if (string.IsNullOrEmpty(MoxaGroup))
            {
                MessageBox.Show("Moxa组号，默认使用1");
                db.IniWriteValuePivas("MOXA", "Group", "1");
                MoxaGroup = "1";
            }
        }

        private void 同步ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (同步ToolStripMenuItem.Text == "关同步")
            {
                if (MessageBox.Show("危险，不要关闭同步，\r\n选择\"是\"会造成PivasMCC无法正常工作!", "危险警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bSynLabelData = false;
                    同步ToolStripMenuItem.Text = "开同步";
                }
            }
            else
            {
                bSynLabelData = true;
                同步ToolStripMenuItem.Text = "关同步";
            }
        }

        /// <summary>
        /// 载入已配置瓶签，已Confirm=2来标记
        /// 红灯的一般不会出仓扫描
        /// </summary>
        private void loadalready()
        {
            try
            {
                string sql = "select labelno from IVRecord_Scan5 where Confirm=2 and DATEDIFF(DAY,GETDATE (),InfusionDT) between 0 and 1 ";

                DataSet ds = db.GetPIVAsDB(sql);
                lock(dtCheckResult.Rows.SyncRoot)
                {
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        dtCheckResult = ds.Tables[0];
                    }
                    else
                    {
                        dtCheckResult = new DataTable();
                        DataColumn dc = new DataColumn("labelno");
                        dtCheckResult.Columns.Add(dc);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("loadalready错误"+ex.Message);
            }
        }

        private void ToolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            (new FrmAbout()).ShowDialog();
        }
        #endregion

        #region 显示屏打开关闭
        /// <summary>
        /// 保存药师登录信息到数据库，用于安卓屏的显示
        /// </summary>
        /// <param name="moxaIp"></param>
        /// <param name="port"></param>
        /// <param name="demployId">清空时，empty</param>
        /// <param name="demployName">清空时，empty</param>
        private void SaveLoginStatus(string moxaIp, string port, string demployName, string demployId)
        {
            try
            {
                string str = string.Format(" update ScreenDetail set DemployeeID='{0}',DemployeeName='{1}',LabelNo='',Count=0,Result=0," +
                    "Msg='' where MoxaIP='{2}' and Port='{3}'", demployId, demployName, moxaIp, port);
                db.GetPIVAsDB(str);
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("保存药师登录信息到数据库，用于安卓屏的显示出错：" + ex.Message);
            }
        }

        /// <summary>
        /// 保存瓶签信息到数据库，用于安卓屏的显示
        /// </summary>
        /// <param name="index"></param>
        /// <param name="chargeRtnValue"></param>
        /// <param name="chargeMsg"></param>
        /// <param name="labelData"></param>
        private void SaveLabelInfo(int index,string chargeRtnValue,string chargeMsg,string labelData,string count)
        {
            try
            {
                string strUpdateScreenDetail = string.Format(
                    "update ScreenDetail set DemployeeID='{0}',DemployeeName='{1}', LabelNo='{2}',Count={3},Result='{4}'," +
                    "Msg='{5}' where MoxaIP='{6}' and port='{7}'", IsLogin[index][1], IsLogin[index][0], labelData,
                    count, chargeRtnValue, chargeMsg,list_LvMain_Model[index].MoxaIP, list_LvMain_Model[index].MoxaPort);

                db.SetPIVAsDB(strUpdateScreenDetail);
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("保存瓶签信息到数据库，用于安卓屏的显示出错：" + ex.Message);
            }
        }
        #endregion

        private const int HOUR = 2;//夜里两点，保证一天一清理

        /// <summary>
        /// 每天定时清理四个listview显示的日志信息
        /// </summary>
        void ClearAllListView()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(1000 * 60 * 60);//1h
                    if (DateTime.Now.Hour == HOUR)
                    {
                        this.SafeAction(() =>
                        {
                            listViewLabelScan.Items.Clear();
                            listViewDeskInfo.Items.Clear();
                            listViewSendPLCInfo.Items.Clear();
                        });
                    }
                }
                catch (Exception ex)
                {
                    InternalLogger.Log.Error("ClearAllListView出错"+ ex.Message);
                }
            }
        }

        /// <summary>
        /// 同步两表瓶签数据，同时保持瓶签状态IVStatus一致
        /// bl_synPZCheck：从IVRecord表中拉取今明两天的瓶签记录，插入到IVRecord_Scan5。同时保证IVStatus一致
        /// </summary>
        void SynLabelData()
        {
            while (true)
            {
                try
                {
                    if (bSynLabelData)
                        db.GetPIVAsDB("EXEC [dbo].[bl_synPZCheck] @Type = 0");
                    Thread.Sleep(300000);//300秒
                }
                catch 
                {
                    InternalLogger.Log.Error("MCC同步两表瓶签数据出错");
                }
            }
        }

        #region 读取控制灯光指令和发送到PLC
        /// <summary>
        /// 此线程没有时间间隔，不停循环
        /// 从控制灯队列中获取指令，然后发送给PLC
        /// </summary>
        void GetLightData()
        {
            while (true)
            {
                try
                {
                    ControlLightCommand lightModel = null;

                    if (lightQueue.Count > 0)
                    {
                        lock (lightQueue)
                        {
                            lightModel = lightQueue.Dequeue();
                        }
                    }

                    if (lightModel != null)//取出指令进行UI更新
                    {
                        listViewSendPLCInfo.SafeAction(() =>
                        {
                            UpdateListViewSendPLCInfo(lightModel.MoxaIP, lightModel.MoxaPort, lightModel.LightColor, lightModel.LightStatus);
                        });
                                                
                        light.Lighting(lightModel);
                    }
                }
                catch (Exception ex)
                {
                    InternalLogger.Log.Error("从控制灯指令队列中读取数据出错:" + ex.Message);
                }
            }
        }

        /// <summary>
        ///  将控制灯指令保存到队列
        /// </summary>
        /// <param name="lightmodel">对象ControlLightCommand.LightTime=0时，代表单个动作(开或关)</param>
        public void EnqueueLightQueue(ControlLightCommand lightmodel)
        {
            try
            {
                if (screenType != StaticDictionary.WINDOWS_SCREEN)//微软屏不用红绿灯
                {
                    lock (lightQueue)
                    {
                        lightQueue.Enqueue(lightmodel);
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("控制灯指令入队出错:" + ex.Message);
            }
        }

        /// <summary>
        /// 根据不同模式和配置，向PLC发送不同指令，控制灯的状态
        /// </summary>
        /// <param name="moxaIp"></param>
        /// <param name="moxaport"></param>
        /// <param name="controlModel">ControlModel</param>
        void LightController(string moxaIp,string moxaport, ControlLightType controlModel)
        {
            try
            {
                bool bControlLight = true;
                int twinkleCount = 0;//灯闪烁次数
                int lightTime = 0;//灯亮持续时间
                int lightColor = -1; //1=红；2=绿
                switch (controlModel)
                {
                    case ControlLightType.ChargeSuccess:
                        lightColor = 2;
                        if (SUseLong)
                        {
                            lightTime = Slong;
                            twinkleCount = 1;
                        }
                        else
                        {
                            lightTime = Stwin;
                            twinkleCount = STime;
                        }
                        break;
                    case ControlLightType.BackDrug:
                        lightColor = 1;
                        if (BUseLong)
                        {
                            lightTime = Blong;
                            twinkleCount = 1;
                        }
                        else
                        {
                            lightTime = Btwin;
                            twinkleCount = BTime;
                        }
                        break;
                    case ControlLightType.ChargeFail:
                        lightColor = 1;
                        if (FUseLong)
                        {
                            lightTime = Flong;
                            twinkleCount = 1;
                        }
                        else
                        {
                            lightTime = Ftwin;
                            twinkleCount = FTime;
                        }
                        break;
                    default:
                        bControlLight = false;
                        break;
                }
                if (bControlLight)
                {
                    if (lightTime < SLEEP_TIME)
                        lightTime = SLEEP_TIME;//时间间隔最小为SLEEP_TIME毫秒；不然灯指令会覆盖

                    //控制灯亮闪烁。且指定次数和闪烁间隔
                    for (int i = 0; i < twinkleCount; i++)
                    {
                        EnqueueLightQueue(new ControlLightCommand(moxaIp, moxaport, lightColor, 1, lightTime));//开
                        EnqueueLightQueue(new ControlLightCommand(moxaIp, moxaport, lightColor, 0, lightTime));//关
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("根据不同模式和配置，将控制灯指令入队出错：" + ex.Message);
            }
        }
        #endregion

        #region 微软屏事件
        private void ScreenClientController_LabelNoReceived(object sender, PivasEventArgs<TCPMessage> e)
        {
            try
            {
                for (int index = 0; index < listViewMain.Items.Count; index++)
                {
                    if (list_LvMain_Model[index].ScreenIP.Trim().Equals(e.Value.ServerIp.Trim())
                        && list_LvMain_Model[index].ScreenPort.ToString().Equals(e.Value.ServerPort.ToString()))//匹配到某项
                    {
                        string screenData = e.Value.TcpData;
                        InternalLogger.Log.Info(String.Format("从[{0}:{1}]读取到数据{2}",
                            list_LvMain_Model[index].ScreenIP, list_LvMain_Model[index].ScreenPort, screenData));

                        String labelData = String.Empty;
                        if (IsLogionData(screenData, out labelData)) //员工登录
                        {
                            InternalLogger.Log.Debug(screenData + "被判断为登录信息,因7777开头且长度为22");
                            CheckInOut(index, labelData);//登录处理
                        }
                        else if (IsLabelNo(screenData, out labelData))//只对今天和明天的瓶签计费；并校验瓶签长度和瓶签标识
                        {
                            InternalLogger.Log.Debug(screenData + "被判断瓶签数据");
                            CheckLabelNo(index, labelData);
                        }
                        else
                        {
                            InternalLogger.Log.Warn(screenData + "被判断为非法Screen数据，不做处理也不显示");
                            return; //跳出数据接收处理方法
                        }

                        //合法moxa数据时，更新UI
                        this.SafeAction(() =>
                        {
                            UpdateListViewDeskInfo(listViewMain.Items[index].SubItems[0].Text,
                                listViewMain.Items[index].SubItems[2].Text, labelData);
                            listViewMain.Items[index].SubItems[5].Text = labelData;
                        });
                        break;//跳出for
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("接收到LabelNo数据，处理出错：" + ex.Message);
            }
        }

        private void ScreenClientController_Disconnected(object sender, PivasEventArgs<TCPMessage> e)
        {
            UpdateListViewPort(string.Format("屏 IP:{0}；端口:{1}；因网络或设备原因，造成连接断开",
                   e.Value.ServerIp, e.Value.ServerPort));
        }

        private void ScreenClientController_Connected(object sender, PivasEventArgs<TCPMessage> e)
        {
            UpdateListViewPort(string.Format("屏 IP:{0}；端口:{1}；连接成功",
                   e.Value.ServerIp, e.Value.ServerPort));
        }
        #endregion 微软屏事件

        private void ClearThread()
        {
            if (light != null)
                light.Dispose();
            if (moxaContrl != null)
                moxaContrl.Dispose();
            if (threadClearAllListView != null)
            {
                try
                {
                    threadClearAllListView.Abort();
                    threadClearAllListView = null;
                }
                catch {}
                try
                {
                    threadGetLightData.Abort();
                    threadGetLightData = null;
                }
                catch { }
                try
                {
                    threadSynLabelData.Abort();
                    threadSynLabelData = null;
                }
                catch { }
            }
        }       
    }
}
