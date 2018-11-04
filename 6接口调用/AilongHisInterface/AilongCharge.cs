using AilongHisInterface.Message;
using ChargeInterface;
using PIVAsCommon;
using PIVAsCommon.Extensions;
using PIVAsCommon.Helper;
using PIVAsCommon.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AilongHisInterface
{
    /// <summary>
    /// 实现艾隆对his接口的计费调用
    /// </summary>
    public class AilongCharge
    {
        #region 单例
        private static AilongCharge instance;
        public static AilongCharge Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AilongCharge();
                }
                return instance;
            }
        }

        static AilongCharge()
        {
            InitConfig();
        }
        #endregion

        private static DB_Help dbHelp = new DB_Help();

        private static string IsOtherLabel = "0";//是否为第三方瓶签
        private const int LABEL_LEN = 14;//系统中使用的瓶签长度；第三方瓶签将进行转换
        private static int LabelLength = LABEL_LEN;//第三方瓶签长度;设置为三方瓶签后会从数据库中拉取
        private static string LabelBS = string.Empty;//瓶签默认开头;设置为三方瓶签后会从数据库中拉取;任何标识都包含String.Empty

        private static bool IsCharge = true;//在舱内核对时，是否进行计费，默认true ;1=true 0=false
        private static ICharge charge = null;//计费接口工厂类
        private Dictionary<string,bool> dicEmployeeStatus = new Dictionary<string, bool>();//标记药师登录状态;不同药师<药师ID，登录状态>
        private static List<string> listCheckResult = new List<string>();//保存已扫描过的瓶签,长时间运行会耗尽内存
        private string currentEmployeeID = string.Empty;//记录当前操作药师Id；登录时保存，退出时置空
        private string currentEmployeeName = string.Empty;//记录当前操作药师名字；登录时保存，退出时置空
        private const string HOSPITAL_TYPE = "SDFY";

        //根据瓶签号获取到所有药品信息
        private const string GET_LABELINFO = "select distinct dw.WardName,dd.UniPreparationID," +
            "pa.PatName,ivd.DrugName,ivd.Spec,CONVERT(float, ivd.Dosage) as Dosage,ivd.DgNo " +
            "from IVRecord iv left join IVRecordDetail ivd on ivd.IVRecordID=iv.IVRecordID " +
            "left join DDrug dd on dd.DrugCode= ivd.DrugCode " +
            "left join Patient pa on pa.PatCode= iv.PatCode " +
            "left join DWard dw on dw.WardCode= pa.WardCode where iv.LabelNo = ";

        /// <summary>
        /// 收到瓶签收据
        /// </summary>
        /// <returns></returns>
        public bool GetResultByLabelNo(string labelNo,out string result)
        {
            result = string.Empty;
            try
            {
                InternalLogger.Log.Info("收到瓶签号:"+labelNo);
                bool rtn = false;
                String labelData = String.Empty;
                if (IsLogionData(labelNo, out labelData)) //员工登录
                {
                    InternalLogger.Log.Debug(labelNo + "被判断为登录信息,因7777开头且长度为22");
                    MsgLoginResult msgLoginResult = null;
                    rtn = HandleInOut(labelData,out msgLoginResult);//登录处理
                    if (rtn && msgLoginResult != null)
                        result = msgLoginResult.ToJson();
                }
                else if (IsLabelNo(labelNo, out labelData))//只对今天和明天的瓶签计费；并校验瓶签长度和瓶签标识
                {
                    InternalLogger.Log.Debug(labelNo + "被判断瓶签数据");
                    MsgLabelResult msgLabelResult = null;
                    rtn = HandleLabelNo(labelData,out msgLabelResult);
                    if (rtn && msgLabelResult != null)
                        result = msgLabelResult.ToJson();
                }
                else
                {
                    InternalLogger.Log.Warn(labelNo + "被判断为非法Moxa数据，不做处理也不显示");
                    return false; //跳出数据接收处理方法
                }
                return rtn;//登录成功与计费成功都返回rtn
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("接收到Moxa数据，处理出错：" + ex.Message);
            }
            return false;
        }

        #region 收到数据后的处理
        /// <summary>
        /// 判断是否药师登录
        /// 77776 35990 96680 48437 50 长度22位，是根据药师ID（9999）生成的二维码信息
        /// </summary>
        private bool IsLogionData(string moxaData, out String labelData)
        {
            labelData = String.Empty;
            try
            {
                if (moxaData.Trim().Length == 22 && moxaData.Substring(0, 4).Equals("7777"))
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
        private bool IsLabelNo(string moxaData, out String labelData)
        {
            labelData = String.Empty;
            try
            {
                if (moxaData.Contains(LabelBS) && moxaData.Length >= LabelLength)//这里对瓶签的长度和标识校验
                {
                    if (IsOtherLabel == "1")//将moxa接收数据（第三方瓶签），转为pivas系统标准数据
                    {
                        DataSet dsIVRecord_Scan5 = dbHelp.GetPIVAsDB("select LabelNo from IVRecord_Scan5 where ThirdLabelNo like '" + moxaData + "%'");
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
        private bool HandleInOut(string labelData,out MsgLoginResult loginResult)
        {
            loginResult = new MsgLoginResult();
            try
            {
                DataTable dt = dbHelp.GetPIVAsDB(" select * from QRcodeLog a inner join DEmployee b on a.DEmployeeID=b.DEmployeeID  where QRcode='"
                    + labelData + "' and DelDT is null and b.IsValid=1 ").Tables[0];

                short loginStatus = StaticDictionary.DOCTOR_STATUS_FALSE;//0未登录，1登陆成功;
                if (dt.Rows.Count > 0)//验证手牌合法，进入判断登录状态并保存
                {
                    string employeeID = dt.Rows[0]["DEmployeeID"].ToString();
                    string employeeName = dt.Rows[0]["DEmployeeName"].ToString();
                    if (dicEmployeeStatus.ContainsKey(employeeID))
                    {
                        if (dicEmployeeStatus[employeeID])//上一次状态时登录状态
                        {
                            //登出成功，返回药师信息
                            loginStatus = StaticDictionary.DOCTOR_STATUS_FALSE;
                            UpdateCurrentEmployee(string.Empty, string.Empty);
                        }
                        else
                        {
                            //登录成功，返回药师信息
                            loginStatus = StaticDictionary.DOCTOR_STATUS_TRUE;
                            UpdateCurrentEmployee(employeeID, employeeName);
                        }
                    }
                    else
                    {
                        //不存在，添加保存，返回登录成功，返回药师信息
                        loginStatus = StaticDictionary.DOCTOR_STATUS_TRUE;
                        UpdateCurrentEmployee(employeeID, employeeName);
                        dicEmployeeStatus.Add(employeeID, true);
                    }
                    loginResult.Status = loginStatus;
                    loginResult.EmployeeName = employeeName;
                    return true;//手牌合法，根据内容判断是登录还是登出
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("药师登录登出处理出错:" + ex.Message);
            }
            //验证手牌出错或不合法，都将置空
            UpdateCurrentEmployee(string.Empty, string.Empty);
            return false;
        }

        /// <summary>
        /// 处理单个瓶签
        /// 瓶签处理的主要目的是计费；计费成功就OK；
        /// 计费失败需将失败原因获取到；ChargeResult
        /// </summary>
        /// <param name="labelData">瓶签数据</param>
        private bool HandleLabelNo(string labelData,out MsgLabelResult msgLabelResult)
        {
            bool bGetLabelResult = false;
            msgLabelResult = new MsgLabelResult();
            string chargeMsg = String.Empty;//调用计费接口，计费错误时返回的错误原因；
            string chargeRtnValue = "0";//调用计费接口的返回值，默认0为计费失败；9未知，其他认为计费成功；

            try
            {
                if (currentEmployeeID != string.Empty)//非空就是由药师处于登录状态
                {
                    if (IsChecked(labelData))//脱机表中存在，表示已扫描且计费成功
                    {
                        chargeMsg = ChargeResult_Const.RESCAN_CHARGE_SUCCESS;//重复扫描，已成功计过费
                        chargeRtnValue = "1";//重复扫描，认为计费成功，可配置
                    }
                    else
                    {
                        if (IsCharge)//根据配置判断是否计费
                        {
                            InternalLogger.Log.Debug("开始调用计费接口");
                            chargeRtnValue = charge.Charge(labelData, currentEmployeeID, out chargeMsg);
                            #region 测试
                            //chargeRtnValue = "1";
                            //chargeMsg = "计费成功";
                            #endregion
                        }
                        else
                        {
                            //配置文件中设设置，舱内核对时，不计费；认为成功，但追加日志记录
                            InternalLogger.Log.Warn("配置文件中设置，在舱内核对时，不计费；系统将此情况认为计费成功");
                            chargeMsg = ChargeResult_Const.NOTCONFIG_SUCCESS;
                            chargeRtnValue = "1";//将结果设置为成功，UI操作在下面继续处理
                        }

                        InternalLogger.Log.Debug("计费接口的返回值：" + chargeRtnValue);
                        if (!chargeRtnValue.Trim().Equals("0"))//计费成功，更新值
                        {
                            UpdateIVStatusAndInsertPZ(currentEmployeeID, labelData);

                            //chargeMsg为接口中返回的值
                            chargeRtnValue = "1";//将不等于0的情况，归类为等于1（计费成功）
                            AddTodt(labelData);//标记是否已扫码且计费成功过
                        }
                    }
                }
                else
                    chargeMsg = ChargeResult_Const.PHARMACIST_LOGOUT;//计费失败
                InternalLogger.Log.Info(String.Format("瓶签号：{0},计费结果：{1}", labelData, chargeMsg));//这句日志是重要日志，可以据此查询计费结果
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("处理发送的单个瓶签时出错:" + ex.Message);
                chargeRtnValue = "0";//统一归类为失败
                chargeMsg = ChargeResult_Const.LABELHANDLE_EXCEPTION;
            }
            finally
            {
                bGetLabelResult = GetLabelResult(labelData, short.Parse(chargeRtnValue), chargeMsg, currentEmployeeName,out msgLabelResult);
            }
            return ((chargeRtnValue == "1") && bGetLabelResult);//计费成功且返回的信息也成功
        }

        /// <summary>
        /// 调用这个函数有个条件，配置计费成功
        /// 更新IVStatus=9，并插入IVRecord_PZ表
        /// </summary>
        private void UpdateIVStatusAndInsertPZ(string DEmployeeID, string LabelNo)
        {
            try
            {
                string location = "艾隆自动冲配机";
                string strUpdate1 = "update IVRecord set IVStatus =9  where LabelNo =" + LabelNo + " and IVStatus<9  ";
                dbHelp.SetPIVAsDB(strUpdate1);

                string strInsert = string.Format(" insert into IVRecord_PZ(IVrecordID, PZDT, ScanCount, pcode, Location,[Type]) "
                      + "select {0},getdate(),0,{1},'{2}',1 where not exists(select * from IVRecord_PZ where IVRecordID = {0})  ",
                 LabelNo, DEmployeeID, location);
                dbHelp.SetPIVAsDB(strInsert);

                string strUpdate2 = "  update IVRecord_Scan5 set Confirm=2 where LabelNo = '" + LabelNo + "'";
                dbHelp.SetPIVAsDB(strUpdate2);
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("更新IVStatus=9，并插入IVRecord_PZ表error:" + ex.Message);
            }
        }
        #endregion

        #region 预防重复配置
        /// <summary>
        /// 搜索数据库前，判断是否已配置
        /// </summary>
        /// <param name="labelno"></param>
        /// <returns></returns>
        private bool IsChecked(string labelno)
        {
            try
            {
                lock (listCheckResult)
                {
                    if (listCheckResult.Contains(labelno))
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
        /// <summary>
        /// 配置过的瓶签进行保存
        /// </summary>
        /// <param name="labelno"></param>
        private void AddTodt(string labelno)
        {
            try
            {
                lock (listCheckResult)
                {
                    listCheckResult.Clear();//全部清空
                    listCheckResult.Add(labelno);
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("AddTodt" + ex.Message);
            }
        }
        /// <summary>
        /// 载入已配置瓶签，已Confirm=2来标记
        /// </summary>
        private static void loadalready()
        {
            try
            {
                string sql = "select labelno from IVRecord_Scan5 where Confirm=2 and DATEDIFF(DAY,GETDATE (),InfusionDT) between 0 and 1 ";

                DataSet ds = dbHelp.GetPIVAsDB(sql);
                lock (listCheckResult)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        listCheckResult = (from row in ds.Tables[0].AsEnumerable()
                                           select row.Field<string>("labelno")).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("载入已配置瓶签出错" + ex.Message);
            }
        }
        #endregion 预防重复配置

        /// <summary>
        /// 调用接口前需初始化参数；
        /// 从数据库或配置文件中加载配置
        /// </summary>
        /// <returns></returns>
        private static bool InitConfig()
        {
            try
            {
                loadalready();
                IsOtherLabel = dbHelp.GetPivasAllSet("第三方瓶签");//获取第三方瓶签的设置

                if (IsOtherLabel == "1")
                {
                    LabelLength = int.Parse(dbHelp.GetPivasAllSetValue2("第三方瓶签"));//获取瓶签长度
                    LabelBS = dbHelp.GetPivasAllSetValue3("第三方瓶签"); //获取瓶签标识
                    InternalLogger.Log.Debug("LabelLength" + LabelLength.ToString() + "LabelBS" + LabelBS.ToString());
                }

                //读取计费配置值并获取接口对象
                IsCharge = dbHelp.IniReadValuePivas("Charge", "PZ") == "1";
                #region 测试
                charge = ChargeFactory.GetCharge(HOSPITAL_TYPE);
                #endregion

                return true;
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("初始化发生错误，请检查配置;" + ex.Message);
            }
            return false;
        }

        /// <summary>
        /// 更新当前登录的药师，只保留一个
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="employeeName"></param>
        private void UpdateCurrentEmployee(string employeeID, string employeeName)
        {
            currentEmployeeID = employeeID;
            currentEmployeeName = employeeName;
        }

        #region 获取药品信息
        /// <summary>
        /// 根据瓶签号，生成所有信息
        /// </summary>
        /// <param name="labelNo">扫描到的瓶签号</param>
        /// <param name="chargeRtnValue">计费返回值0=计费失败；1=计费成功</param>
        /// <param name="chargeMsg">计费补充消息</param>
        private bool GetLabelResult(string labelNo, short chargeRtnValue, string chargeMsg, string employeeName, out MsgLabelResult msgLabelResult)
        {
            msgLabelResult = new MsgLabelResult();
            try
            {
                msgLabelResult.EmployeeName = employeeName;

                DataSet dsLableInfo = dbHelp.GetPIVAsDB(GET_LABELINFO + labelNo);
                if (dsLableInfo.Tables[0].Rows.Count > 0)
                {
                    msgLabelResult.WardName = dsLableInfo.Tables[0].Rows[0]["WardName"].ToString().Trim();
                    msgLabelResult.PatientName = dsLableInfo.Tables[0].Rows[0]["PatName"].ToString().Trim();
                    msgLabelResult.LabelNo = labelNo;
                    msgLabelResult.ChargeResult = chargeRtnValue;
                    msgLabelResult.ChargeMessage = chargeMsg;

                    msgLabelResult.Drugs = GetDrugRowAndMixMethod(dsLableInfo);
                }
                return true;
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("根据瓶签号生成结果出错：" + ex.Message);
            }
            return false;
        }

        /// <summary>
        /// 获取药品行信息
        /// </summary>
        /// <param name="labelNo"></param>
        /// <param name="drugCode"></param>
        private List<MsgDrugRowInfo> GetDrugRowAndMixMethod(DataSet dsLableInfo)
        {
            List<MsgDrugRowInfo> lstScreenInfo = new List<MsgDrugRowInfo>();//lstScreenInfo是所有药品行信息;
            try
            {
                for (int i = 0; i < dsLableInfo.Tables[0].Rows.Count; i++)
                {
                    MsgDrugRowInfo drugRow = new MsgDrugRowInfo();
                    drugRow.DrugIndex = (i + 1).ToString();//从1开始
                    drugRow.DrugName = dsLableInfo.Tables[0].Rows[i]["DrugName"].ToString().Trim();
                    drugRow.DrugSpec = dsLableInfo.Tables[0].Rows[i]["Spec"].ToString().Trim();
                    drugRow.DrugDose = dsLableInfo.Tables[0].Rows[i]["Dosage"].ToString().Trim();
                    drugRow.DrugCount = dsLableInfo.Tables[0].Rows[i]["DgNo"].ToString().Trim();
                    lstScreenInfo.Add(drugRow);
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("获取药品行信息出错：" + ex.Message);
            }
            return lstScreenInfo;
        }
        #endregion
    }
}
