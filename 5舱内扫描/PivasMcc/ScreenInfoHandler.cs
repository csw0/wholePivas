using Amib.Threading;
using Communication;
using Communication.DisplayTcp;
using PIVAsCommon;
using PIVAsCommon.Extensions;
using PIVAsCommon.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace PivasMcc
{
    /// <summary>
    /// 采用SmartThreadTool，根据瓶签获取到所有的屏显信息
    /// </summary>
    public class ScreenInfoHandler
    {
        #region 单例
        private static ScreenInfoHandler instance;
        public static ScreenInfoHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScreenInfoHandler();
                }
                return instance;
            }
        }

        private static SmartThreadPool smartThreadPool = null;
        static ScreenInfoHandler()
        {
            smartThreadPool = new SmartThreadPool();
            smartThreadPool.Start();
        }
        #endregion

        /// <summary>
        /// 根据瓶签号，生成所有信息，发送到屏进行显示
        /// </summary>
        /// <param name="labelNo">扫描到的瓶签号，第三方瓶签是不是还要逆过去</param>
        /// <param name="chargeRtnValue">计费返回值0=计费失败，不可配置；非0=计费成功，可配置</param>
        /// <param name="chargeMsg">计费补充消息</param>
        public void SendInfoToScreen(DisplayController displayController,string screenIp,int screenPort,
            string labelNo, string chargeRtnValue,string chargeMsg,string employeeName,string MixCount)
        {
            //清空药品信息行\配置方法行\计费结果
            ClearDrugInfo(displayController, screenIp, screenPort);
            ClearChareRes(displayController, screenIp, screenPort);
            ClearMethodRow(displayController, screenIp, screenPort);

            //扔进线程池
            smartThreadPool.QueueWorkItem(() =>
            {
                try
                {
                    SendEmployeeRow(displayController, screenIp, screenPort, employeeName, MixCount);//发送药师信息

                    string mixMethods = string.Empty;

                    #region csw模拟测试
                    //labelNo = "20171209100165";
                    #endregion
                    DataSet dsLableInfo = dbHelp.GetPIVAsDB(GET_LABELINFO + labelNo);
                    if (dsLableInfo.Tables.Count > 0 && dsLableInfo.Tables[0].Rows.Count > 0)
                    {
                        SendPatientRow(displayController, screenIp, screenPort, labelNo, dsLableInfo);//发送患者信息

                        List<MsgDrugRow> lstDrugRow = GetDrugRowAndMixMethod(dsLableInfo, out mixMethods);//获取药品信息和配置方法

                        SendChareRes(displayController, screenIp, screenPort, chargeRtnValue, chargeMsg);//发送计费结果

                        foreach (MsgDrugRow item in lstDrugRow)
                        {
                            PackSendData(displayController, screenIp, screenPort, item);//循环发送药品信息
                        }

                        #region 发送配置方法
                        if (!string.IsNullOrEmpty(mixMethods))
                        {
                            MsgDrugMixMethod drugMixMethod = new MsgDrugMixMethod();
                            drugMixMethod.RowAddress = BitConverter.GetBytes(GetScreenAddress(ScreenMsgType.DrugMixMethod));
                            Array.Reverse(drugMixMethod.RowAddress);//大小端转换

                            GeneralMethodRow(ref drugMixMethod, mixMethods);
                            PackSendData(displayController, screenIp, screenPort, drugMixMethod);
                        }
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    InternalLogger.Log.Error("根据瓶签号，生成信息并发送到屏进行显示出错：" + ex.Message);
                }
            });
        }

        /// <summary>
        /// 发送药师登录状态到屏
        /// </summary>
        public void SendLoginStatus(DisplayController displayController, string screenIp, int screenPort, ushort status)
        {
            smartThreadPool.QueueWorkItem(() =>
            {
                try
                {
                    MsgLoginStatus loginStatus = new MsgLoginStatus();

                    loginStatus.RowAddress = BitConverter.GetBytes(GetScreenAddress(ScreenMsgType.LoginStatus));
                    Array.Reverse(loginStatus.RowAddress);//大小端转换

                    loginStatus.Status = BitConverter.GetBytes(status);
                    Array.Reverse(loginStatus.Status);//大小端转换

                    PackSendData(displayController, screenIp, screenPort, loginStatus);//发送药师信息到屏
                }
                catch (Exception ex)
                {
                    InternalLogger.Log.Error("发送药师登录状态到屏出错：" + ex.Message);
                }
            });

            if (status == 0)//0未登录状态，清空所有
                ClearAllScreen(displayController, screenIp, screenPort);
        }

        /// <summary>
        /// 发送药师信息行到屏
        /// </summary>
        private void SendEmployeeRow(DisplayController displayController, string screenIp, int screenPort, string employeeName, string MixCount)
        {
            try
            {
                MsgDEmployeeRow employeeRow = new MsgDEmployeeRow();
               
                employeeRow.RowAddress = BitConverter.GetBytes(GetScreenAddress(ScreenMsgType.EmployeeRow));
                Array.Reverse(employeeRow.RowAddress);//大小端转换

                employeeRow.EmployeeNameMark = GetStandandArray(employeeRow, "EmployeeNameMark", "药师:");
                employeeRow.MixCountMark = GetStandandArray(employeeRow, "MixCountMark", "配置量:");
                employeeRow.EmployeeName = GetStandandArray(employeeRow, "EmployeeName", GetGBK2312String(employeeName));
                employeeRow.MixCount = GetStandandArray(employeeRow, "MixCount", MixCount,false);
                employeeRow.ShowTime = GetStandandTime(employeeRow, "ShowTime");
                
                PackSendData(displayController, screenIp, screenPort, employeeRow);//发送药师信息到屏
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("发送药师信息行到屏出错：" + ex.Message);
            }
        }

        /// <summary>
        /// 将gbk2312不支持的汉字，转为*
        /// </summary>
        /// <param name="oldString"></param>
        /// <returns></returns>
        private string GetGBK2312String(string oldString)
        {
            string newString = oldString;
            try
            {
                byte[] byteOld = Encoding.GetEncoding(20936).GetBytes(oldString);
                newString = Encoding.GetEncoding(20936).GetString(byteOld).Replace('?', '*');
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("将gbk2312不支持的汉字转为*出错：" + ex.Message);
            }
            return newString;
        }


        /// <summary>
        /// 获取屏需要的标准时间
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private byte[] GetStandandTime<T>(T t,string keyField)
        {
            int len = GetFieldSizeConst(t, keyField);
            byte[] rtnArray = new byte[len];

            try
            {
                if (len >= 3)
                {
                    rtnArray[0] = Convert.ToByte(DateTime.Now.Hour.ToString(), 16);
                    rtnArray[1] = Convert.ToByte(DateTime.Now.Minute.ToString(), 16);
                    rtnArray[2] = Convert.ToByte(DateTime.Now.Second.ToString(), 16);
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("获取屏需要的标准时间出错：" + ex.Message);
            }
            return rtnArray;
        }

        /// <summary>
        /// 发送患者信息行到屏
        /// </summary>
        private void SendPatientRow(DisplayController displayController, string screenIp, int screenPort, string labelNo, DataSet dsLableInfo)
        {
            try
            {
                MsgPatientRow patientRow = new MsgPatientRow();
                                                               
                patientRow.RowAddress = BitConverter.GetBytes(GetScreenAddress(ScreenMsgType.PatientRow));
                Array.Reverse(patientRow.RowAddress);

                patientRow.WardName = GetStandandArray(patientRow, "WardName", dsLableInfo.Tables[0].Rows[0]["WardName"].ToString());
                patientRow.PatientName = GetStandandArray(patientRow, "PatientName", 
                    GetGBK2312String(dsLableInfo.Tables[0].Rows[0]["PatName"].ToString()));
                patientRow.LabelNoMark = GetStandandArray(patientRow, "LabelNoMark", "瓶签:");
                patientRow.LabelNo = GetStandandArray(patientRow, "LabelNo", labelNo);

                PackSendData(displayController, screenIp, screenPort, patientRow);//发送
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("发送患者信息行到屏出错：" + ex.Message);
            }
        }

        /// <summary>
        /// 发送计费结果到屏
        /// </summary>
        private void SendChareRes(DisplayController displayController, string screenIp, int screenPort, string chargeRtnValue, string chargeMsg)
        {
            try
            {
                MsgChareResRow chareResRow = new MsgChareResRow();
                chareResRow.RowAddress = BitConverter.GetBytes(GetScreenAddress(ScreenMsgType.ChareResRow));
                Array.Reverse(chareResRow.RowAddress);//大小端转换

                chareResRow.ChargeMessage = GetStandandArray(chareResRow, "ChargeMessage", chargeMsg);

                PackSendData(displayController, screenIp, screenPort, chareResRow);//发送计费结果数据到屏
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("发送计费结果到屏出错：" + ex.Message);
            }
        }

        /// <summary>
        /// 生成四行药品配置方法
        /// </summary>
        /// <param name="index"></param>
        private void GeneralMethodRow(ref MsgDrugMixMethod method, string mixMethods)
        {
            try
            {
                string[] arrayDrugMixMethod = SplitByLen(mixMethods, 25);//每行25个汉字

                for (int index = 0; index < 4; index++)//固定4行
                {
                    if (index >= arrayDrugMixMethod.Length)
                        break;//跳出for

                    switch (index)
                    {
                        case 0:
                            method.MixMethodRow1 = GetStandandArray(method, "MixMethodRow1",arrayDrugMixMethod[index]);
                            break;
                        case 1:
                            method.MixMethodRow2 = GetStandandArray(method, "MixMethodRow2", arrayDrugMixMethod[index]);
                            break;
                        case 2:
                            method.MixMethodRow3 = GetStandandArray(method, "MixMethodRow3", arrayDrugMixMethod[index]);
                            break;
                        case 3:
                            method.MixMethodRow4 = GetStandandArray(method, "MixMethodRow4", arrayDrugMixMethod[index]);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("生成四行药品配置方法出错：" + ex.Message);
            }
        }

        /// <summary>
        /// 根据长度，将string拆分为string[]
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separatorCharNum"></param>
        /// <returns></returns>
        private string[] SplitByLen(string str, int separatorCharNum)
        {
            List<string> strList = new List<string>();
            try
            {
                if (str.Length <= separatorCharNum)
                {
                    return new string[] { str };
                }
                string tempStr = str;                
                int iMax = Convert.ToInt32(Math.Ceiling(str.Length / (separatorCharNum * 1.0)));//获取循环次数  
                for (int i = 1; i <= iMax; i++)
                {
                    string currMsg = tempStr.Substring(0, tempStr.Length > separatorCharNum ? separatorCharNum : tempStr.Length);
                    strList.Add(currMsg);
                    if (tempStr.Length > separatorCharNum)
                    {
                        tempStr = tempStr.Substring(separatorCharNum, tempStr.Length - separatorCharNum);
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("根据长度，将string拆分为string数组出错：" + ex.Message);
            }
            return strList.ToArray();
        }

        /// <summary>
        /// 获取药品行信息和配置方法
        /// </summary>
        /// <param name="labelNo"></param>
        /// <param name="drugCode"></param>
        private List<MsgDrugRow> GetDrugRowAndMixMethod(DataSet dsLableInfo,out string mixMethods)
        {
            List<MsgDrugRow> lstScreenInfo = new List<MsgDrugRow>();//lstScreenInfo是所有药品行信息;
            mixMethods = string.Empty; //mixMethods是所有的配置方法，需分包发送；

            try
            {
                for (int i = 0; i < dsLableInfo.Tables[0].Rows.Count; i++)
                {
                    string uniPreparationID = dsLableInfo.Tables[0].Rows[i]["UniPreparationID"].ToString();
                    InternalLogger.Log.Debug("uniPreparationID:" + uniPreparationID);
                    mixMethods += GetMixMethodByDrugCode(uniPreparationID);

                    MsgDrugRow drugRow = new MsgDrugRow();

                    drugRow.RowAddress = BitConverter.GetBytes(GetScreenAddressOfDrug(i));
                    Array.Reverse(drugRow.RowAddress);

                    drugRow.DrugIndex = GetStandandArray(drugRow, "DrugIndex", (i + 1).ToString(),false);//从1开始
                    drugRow.DrugName = GetStandandArray(drugRow, "DrugName", dsLableInfo.Tables[0].Rows[i]["DrugName"].ToString());
                    drugRow.DrugSpec = GetStandandArray(drugRow, "DrugSpec", dsLableInfo.Tables[0].Rows[i]["Spec"].ToString());
                    drugRow.DrugDose = GetStandandArray(drugRow, "DrugDose", dsLableInfo.Tables[0].Rows[i]["Dosage"].ToString());
                    drugRow.DrugCount = GetStandandArray(drugRow, "DrugCount", dsLableInfo.Tables[0].Rows[i]["DgNo"].ToString(), false);

                    lstScreenInfo.Add(drugRow);
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("获取药品行信息和配置方法出错：" + ex.Message);
            }
            return lstScreenInfo;
        }

        /// <summary>
        /// 新建数组，并初始化为0，具体初始值以屏的要求为准；
        /// 然后将字段数据按照不同编码，填充到数组中
        /// 20936是gbk2312,936是gbk;目前系统默认都已是gbk；但屏硬件仅支持gbk2312
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="keyField"></param>
        /// <param name="dataInfo">字段数据</param>
        /// <param name="dataType">数据占用字节，GBK2312=2个字节；AscII=1个字节</param>
        /// <returns></returns>
        private byte[] GetStandandArray<T>(T t,string keyField,string dataInfo,bool bGBK2312 = true)
        {
            int len = GetFieldSizeConst(t, keyField);
            byte[] rtnArray = new byte[len];

            try
            {
                for (int i = 0; i < rtnArray.Length; i++)
                    rtnArray[i] = 0;
                byte[] arrayData = null;
                if (bGBK2312)
                    arrayData = Encoding.GetEncoding(20936).GetBytes(dataInfo.Trim());
                else
                    arrayData = Encoding.ASCII.GetBytes(dataInfo.Trim());

                Array.Copy(arrayData, rtnArray, Math.Min(arrayData.Length, len));
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("新建数组并初始化默认值出错："+ ex.Message);
            }
            return rtnArray;
        }

        /// <summary>
        /// 获取药品的配置方法
        /// </summary>
        /// <param name="drugCode"></param>
        private string GetMixMethodByDrugCode(string drugCode)
        {
            string rtn = string.Empty;
            try
            {
                #region csw模拟测试
                //drugCode = "247";
                #endregion
                
                DataSet dsMixMethod = dbHelp.GetPIVAsDB(string.Format(GET_DRUGMIXMETHOD_PHARMACOLOGY, drugCode));
                if (dsMixMethod.Tables.Count > 0 && dsMixMethod.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsMixMethod.Tables[0].Rows.Count; i++)
                    {
                        rtn += dsMixMethod.Tables[0].Rows[i]["PreparationDesc"].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("获取药品的配置方法出错:" + ex.Message);
            }
          
            return rtn;
        }

        /// <summary>
        /// 将数据打包发送
        /// </summary>
        /// <param name="displayController"></param>
        /// <param name="screenIp"></param>
        /// <param name="screenPort"></param>
        /// <param name="rowData"></param>
        private void PackSendData(DisplayController displayController, string screenIp, int screenPort,object rowData)
        {
            try
            {
                MsgGeneral general = new MsgGeneral();
                general.FrameHead = BitConverter.GetBytes((ushort)0x5AA5);
                Array.Reverse(general.FrameHead);//大小端
                general.FrameLen = (byte)(Marshal.SizeOf(rowData) + 1);//因含0x82一个字节
                general.FrameConst = 0x82;

                byte[] arraySendData = general.StructToBytes().MergeBytes(rowData.StructToBytes());
                TCPMessageEx tcpMessage = new TCPMessageEx(screenIp, screenPort, arraySendData);

                displayController.DisplayDataEnqueue(new PivasEventArgs<TCPMessageEx>(tcpMessage));
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("打包发送；需将data分成指定格式出错：" + ex.Message);
            }
        }

        /// <summary>
        /// 获取屏地址，屏地址与屏设备约定，写死
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        private ushort GetScreenAddress(ScreenMsgType msgType)
        {
            try
            {
                switch (msgType)
                {
                    case ScreenMsgType.LoginStatus:
                        return 0x126E;
                    case ScreenMsgType.PatientRow:
                        return 0x1000;
                    case ScreenMsgType.DrugRow:
                        return 0x1020;//首行
                    case ScreenMsgType.ChareResRow:
                        return 0x1334;
                    case ScreenMsgType.EmployeeRow:
                        return 0x1270;
                    case ScreenMsgType.DrugMixMethod:
                        return 0x1285;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("根据屏行号获取行的屏地址出错:" + ex.Message);
            }

            InternalLogger.Log.Warn("没有获取到屏地址，返回屏初始地址0x1000");
            return 0x1000;//屏起始地址
        }

        /// <summary>
        /// 获取药品行的屏地址
        /// </summary>
        /// <param name="index">行号,从0开始</param>
        /// <returns></returns>
        private ushort GetScreenAddressOfDrug(int index)
        {
            return (ushort)(GetScreenAddress(ScreenMsgType.DrugRow) + DRUGROW_LEN * index);
        }

        /// <summary>
        /// 获取结构体中公共字段的长度
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msgType"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        private int GetFieldSizeConst<T>(T msgType,string fieldName)
        {
            try
            {
                FieldInfo field = typeof(T).GetField(fieldName);
                if (field != null)
                {
                    object[] attrs = field.GetCustomAttributes(false);
                    foreach (object attr in attrs)
                    {
                        MarshalAsAttribute authAttr = attr as MarshalAsAttribute;
                        if (authAttr != null)
                            return authAttr.SizeConst;
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("出错：" + ex.Message);
            }
            return 0;
        }

        #region 清屏指令
        /// <summary>
        /// 清空屏的所有显示,放进线程池
        /// </summary>
        private void ClearAllScreen(DisplayController displayController, string screenIp, int screenPort)
        {
            ClearPatientRow(displayController, screenIp, screenPort);
            ClearChareRes(displayController, screenIp, screenPort);
            ClearEmployeeRow(displayController, screenIp, screenPort);
            ClearMethodRow(displayController, screenIp, screenPort);
            ClearDrugInfo(displayController, screenIp, screenPort);
        }

        /// <summary>
        /// 清空患者行
        /// </summary>
        private void ClearPatientRow(DisplayController displayController, string screenIp, int screenPort)
        {
            try
            {
                MsgPatientRow patientRow = new MsgPatientRow();
                patientRow.RowAddress = BitConverter.GetBytes(GetScreenAddress(ScreenMsgType.PatientRow));
                Array.Reverse(patientRow.RowAddress);

                patientRow.WardName = GetStandandArray(patientRow, "WardName",string.Empty);
                patientRow.PatientName = GetStandandArray(patientRow, "PatientName", string.Empty);
                patientRow.LabelNoMark = GetStandandArray(patientRow, "LabelNoMark", string.Empty);
                patientRow.LabelNo = GetStandandArray(patientRow, "LabelNo", string.Empty);

                PackSendData(displayController, screenIp, screenPort, patientRow);//发送
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("清空患者行出错：" + ex.Message);
            }
        }

        /// <summary>
        /// 清空计费结果
        /// </summary>
        /// <param name="displayController"></param>
        /// <param name="screenIp"></param>
        /// <param name="screenPort"></param>
        /// <param name="chargeRtnValue"></param>
        /// <param name="chargeMsg"></param>
        private void ClearChareRes(DisplayController displayController, string screenIp, int screenPort)
        {
            try
            {
                MsgChareResRow chareResRow = new MsgChareResRow();

                chareResRow.RowAddress = BitConverter.GetBytes(GetScreenAddress(ScreenMsgType.ChareResRow));
                Array.Reverse(chareResRow.RowAddress);//大小端转换
                chareResRow.ChargeMessage = GetStandandArray(chareResRow, "ChargeMessage", string.Empty);

                PackSendData(displayController, screenIp, screenPort, chareResRow);//发送计费结果数据到屏
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("清理屏出错：" + ex.Message);
            }
        }

        /// <summary>
        /// 清空药师信息
        /// </summary>
        /// <param name="displayController"></param>
        /// <param name="screenIp"></param>
        /// <param name="screenPort"></param>
        /// <param name="employeeName"></param>
        /// <param name="MixCount"></param>
        private void ClearEmployeeRow(DisplayController displayController, string screenIp, int screenPort)
        {
            try
            {
                MsgDEmployeeRow employeeRow = new MsgDEmployeeRow();

                employeeRow.RowAddress = BitConverter.GetBytes(GetScreenAddress(ScreenMsgType.EmployeeRow));
                Array.Reverse(employeeRow.RowAddress);//大小端转换
                employeeRow.EmployeeNameMark = GetStandandArray(employeeRow, "EmployeeNameMark", string.Empty);
                employeeRow.MixCountMark = GetStandandArray(employeeRow, "MixCountMark", string.Empty);
                employeeRow.EmployeeName = GetStandandArray(employeeRow, "EmployeeName", string.Empty);
                employeeRow.MixCount = GetStandandArray(employeeRow, "MixCount", string.Empty);
                employeeRow.ShowTime = GetStandandTime(employeeRow, "ShowTime");

                PackSendData(displayController, screenIp, screenPort, employeeRow);//发送药师信息到屏
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("清空药师信息出错：" + ex.Message);
            }
        }

        /// <summary>
        /// 清空配置方法行
        /// </summary>
        /// <param name="displayController"></param>
        /// <param name="screenIp"></param>
        /// <param name="screenPort"></param>
        private void ClearMethodRow(DisplayController displayController, string screenIp, int screenPort)
        {
            try
            {
                MsgDrugMixMethod drugMixMethod = new MsgDrugMixMethod();
                drugMixMethod.RowAddress = BitConverter.GetBytes(GetScreenAddress(ScreenMsgType.DrugMixMethod));
                Array.Reverse(drugMixMethod.RowAddress);//大小端转换

                drugMixMethod.MixMethodRow1 = GetStandandArray(drugMixMethod, "MixMethodRow1", string.Empty);
                drugMixMethod.MixMethodRow2 = GetStandandArray(drugMixMethod, "MixMethodRow2", string.Empty);
                drugMixMethod.MixMethodRow3 = GetStandandArray(drugMixMethod, "MixMethodRow3", string.Empty);
                drugMixMethod.MixMethodRow4 = GetStandandArray(drugMixMethod, "MixMethodRow4", string.Empty);

                PackSendData(displayController, screenIp, screenPort, drugMixMethod);
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("清空配置方法行出错：" + ex.Message);
            }
        }

        /// <summary>
        /// 清空药品信息行
        /// </summary>
        /// <param name="displayController"></param>
        /// <param name="screenIp"></param>
        /// <param name="screenPort"></param>
        private void ClearDrugInfo(DisplayController displayController, string screenIp, int screenPort)
        {
            for (int i = 0; i < 10; i++)//药品行固定10行，循环清空
            {
                MsgDrugRow drugRow = new MsgDrugRow();

                drugRow.RowAddress = BitConverter.GetBytes(GetScreenAddressOfDrug(i));
                Array.Reverse(drugRow.RowAddress);

                drugRow.DrugIndex = GetStandandArray(drugRow, "DrugIndex", string.Empty);
                drugRow.DrugName = GetStandandArray(drugRow, "DrugName", string.Empty);
                drugRow.DrugSpec = GetStandandArray(drugRow, "DrugSpec", string.Empty);
                drugRow.DrugDose = GetStandandArray(drugRow, "DrugDose", string.Empty);
                drugRow.DrugCount = GetStandandArray(drugRow, "DrugCount", string.Empty);

                PackSendData(displayController, screenIp, screenPort, drugRow);
            }
        }
        #endregion

        //根据瓶签号获取到所有药品信息
        private const string GET_LABELINFO = "select distinct dw.WardName,dd.UniPreparationID," +
            "pa.PatName,ivd.DrugName,ivd.Spec,CONVERT(float, ivd.Dosage) as Dosage,ivd.DgNo " +
            "from IVRecord iv left join IVRecordDetail ivd on ivd.IVRecordID=iv.IVRecordID " +
            "left join DDrug dd on dd.DrugCode= ivd.DrugCode " +
            "left join Patient pa on pa.PatCode= iv.PatCode " +
            "left join DWard dw on dw.WardCode= pa.WardCode where iv.LabelNo = ";

        //含溶媒的sql，获取到药品配置方法
        private const string GET_DRUGMIXMETHOD_MENSTRUUM = "SELECT pm.PreparationDesc FROM KD0102.dbo.PreparationMethods pm " +
            "WHERE EXISTS(SELECT 1 FROM KD0102.dbo.PreparationMethod2Uniprep pmu WHERE " +
            "pm.PreparationMethodsID= pmu.PreparationMethodsID AND pmu.UniPreparationID = {0})";

        //只关联药理成分的sql，获取到药品配置方法
        private const string GET_DRUGMIXMETHOD_PHARMACOLOGY = "SELECT pm.PreparationDesc FROM KD0102.dbo.PreparationMethods pm " +
            "WHERE EXISTS(SELECT 1 FROM KD0102.dbo.PreparationMethod2Uniprep pmu WHERE " +
            "pm.PreparationMethodsID= pmu.PreparationMethodsID AND pmu.UniPreparationID = {0})";

        private DB_Help dbHelp = new DB_Help();
        private const int DRUGROW_LEN = 59;//药品行每行长度59个屏地址，59*2=118个字节
    }
}
