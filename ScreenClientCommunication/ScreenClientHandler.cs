using Amib.Threading;
using Communication.DisplayTcp;
using Communication.WindowsScreen;
using PIVAsCommon;
using PIVAsCommon.Extensions;
using PIVAsCommon.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Communication.screenTcp
{
    /// <summary>
    /// 采用SmartThreadTool，根据瓶签获取到所有的屏显信息
    /// </summary>
    public class ScreenClientHandler
    {
        #region 单例
        private static ScreenClientHandler instance;
        public static ScreenClientHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScreenClientHandler();
                }
                return instance;
            }
        }

        private static SmartThreadPool smartThreadPool = null;
        static ScreenClientHandler()
        {
            smartThreadPool = new SmartThreadPool();
            smartThreadPool.Start();

            int start = 65, end = 90;
            while(start <= end)
            {
                listIndex.Add(((char)start).ToString());
                start++;
            }
        }
        #endregion

        private static List<string> listIndex = new List<string>();

        /// <summary>
        /// 根据瓶签号，生成所有信息，发送到屏进行显示
        /// </summary>
        /// <param name="labelNo">扫描到的瓶签号</param>
        /// <param name="chargeRtnValue">计费返回值0=计费失败，不可配置；非0=计费成功，可配置</param>
        /// <param name="chargeMsg">计费补充消息</param>
        public void SendInfoToScreen(ScreenClientController screenClientController, string screenIp,int screenPort,
            string labelNo, short chargeRtnValue,string chargeMsg,string employeeName,string mixCount)
        {
            //扔进线程池
            smartThreadPool.QueueWorkItem(() =>
            {
                try
                {
                    string mixMethods = string.Empty;
                    MsgScreenInfo msgScreenInfo = new MsgScreenInfo();

                    DataSet dsLableInfo = dbHelp.GetPIVAsDB(GET_LABELINFO + labelNo);
                    if (dsLableInfo.Tables.Count > 0 && dsLableInfo.Tables[0].Rows.Count > 0)
                    {
                        msgScreenInfo.MsgType = (short)MsgType.ScrennInfo;
                        msgScreenInfo.ChargeMessage = chargeMsg;
                        msgScreenInfo.ChargeResult = chargeRtnValue;
                        msgScreenInfo.EmployeeName = employeeName;
                        msgScreenInfo.LabelNo = labelNo;
                        msgScreenInfo.MixCount = mixCount;
                        msgScreenInfo.PatientName = dsLableInfo.Tables[0].Rows[0]["PatName"].ToString();
                        msgScreenInfo.ShowTime = DateTime.Now.ToString("HH:mm:ss MM/dd");
                        msgScreenInfo.WardName = dsLableInfo.Tables[0].Rows[0]["WardName"].ToString();
                        msgScreenInfo.Drugs = new List<MsgDrugRowInfo>();
                        msgScreenInfo.Methods = new List<string>();

                        //遍历获取药品行信息
                        for (int i = 0; i < dsLableInfo.Tables[0].Rows.Count; i++)
                        {
                            string uniPreparationID = dsLableInfo.Tables[0].Rows[i]["UniPreparationID"].ToString();
                            InternalLogger.Log.Debug("uniPreparationID:" + uniPreparationID);
                            mixMethods += GetMixMethodByDrugCode(uniPreparationID);

                            MsgDrugRowInfo msgDrugRowInfo = new MsgDrugRowInfo();
                            msgDrugRowInfo.DrugIndex = listIndex[i + 1];//从1开始
                            msgDrugRowInfo.DrugName = dsLableInfo.Tables[0].Rows[i]["DrugName"].ToString();
                            msgDrugRowInfo.DrugSpec = dsLableInfo.Tables[0].Rows[i]["Spec"].ToString();
                            msgDrugRowInfo.DrugDose = dsLableInfo.Tables[0].Rows[i]["Dosage"].ToString();
                            msgDrugRowInfo.DrugCount = dsLableInfo.Tables[0].Rows[i]["DgNo"].ToString();
                            msgScreenInfo.Drugs.Add(msgDrugRowInfo);
                        }

                        //药品配置方法行
                        string[] s = SplitByLen(mixMethods,50);
                        foreach (var item in s)
                        {
                            msgScreenInfo.Methods.Add(item);
                        }
                    }
                    //发送到屏
                    screenClientController.sendDataEnqueue(new PivasEventArgs<TCPMessage>(new TCPMessage(screenIp, screenPort, msgScreenInfo.ToJson())));
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
        public void SendLoginStatus(ScreenClientController screenClientController, string screenIp, int screenPort, short status)
        {
            smartThreadPool.QueueWorkItem(() =>
            {
                try
                {
                    MsgLoginResult msgLoginResult = new MsgLoginResult();
                    msgLoginResult.MsgType = (short)MsgType.LoginStatus;
                    msgLoginResult.Status = status;
                    //将登陆状态发给屏，微软屏
                    screenClientController.sendDataEnqueue(new PivasEventArgs<TCPMessage>(new TCPMessage(screenIp,
                        screenPort, msgLoginResult.ToJson())));
                }
                catch (Exception ex)
                {
                    InternalLogger.Log.Error("发送药师登录状态到屏出错：" + ex.Message);
                }
            });
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
        /// 获取药品的配置方法
        /// </summary>
        /// <param name="drugCode"></param>
        private string GetMixMethodByDrugCode(string drugCode)
        {
            string rtn = string.Empty;
            try
            {
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
    }
}
