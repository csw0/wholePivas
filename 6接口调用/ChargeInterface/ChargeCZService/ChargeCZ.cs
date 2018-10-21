using PIVAsCommon;
using PIVAsCommon.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace ChargeInterface.ChargeCZService
{
    /// <summary>
    /// CZ医院计费接口
    /// </summary>
    public class ChargeCZ : AbstractCharge
    {
        /// <summary>
        /// 计费接口（舱内扫描程序调用）
        /// 0 计费不成功
        /// 1 计费成功
        /// msg 失败原因
        /// </summary>
        /// <param name="labelno"></param>
        /// <param name="UserID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override string Charge(string labelno, string UserID, out string msg)
        {
            string ret = "0";
            msg = "未知原因";
            try
            {
                string selsql = "select PatCode,p.WardCode,p.DrugType ,i.labelover ,i.wardretreat,i.remark3,i.packadvance,i.batch "+
                    "from  IVRecord  i left join Prescription p on p.GroupNo = i.GroupNo where LabelNo  ='" + labelno + "'";
                selsql += "select DEmployeeCode from DEmployee where DEmployeeID ='" + UserID + "'";

                DataSet Lds = dbHelp.GetPIVAsDB(selsql);
                if (Lds.Tables[0].Rows.Count <= 0)
                {
                    msg = "未找到医嘱信息";
                    return "0";
                }
                if (Lds.Tables[1].Rows.Count <= 0)
                {
                    msg = "未找到员工信息";
                    return "0";
                }
                if (Convert.ToInt32(Lds.Tables[0].Rows[0]["labelover"].ToString().Trim()) < 0)
                {
                    msg = "配置取消";
                    return "0";
                }
                if (Convert.ToInt32(Lds.Tables[0].Rows[0]["wardretreat"].ToString().Trim()) == 2)
                {
                    msg = "已退药";
                    return "0";
                }
                if (Convert.ToInt32(Lds.Tables[0].Rows[0]["remark3"].ToString().Trim()) == 15)
                {
                    msg = "已计费";
                    return "1";
                }
                if (Convert.ToInt32(Lds.Tables[0].Rows[0]["packadvance"].ToString().Trim()) == 1)
                {
                    msg = "提前打包";
                    return "0";
                }
                String batch = Lds.Tables[0].Rows[0]["batch"].ToString();
                if (batch.ToUpper().Contains("K"))
                {
                    msg = "空包";
                    return "1";
                }

                msg = "计费成功";
                return "1";
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("调用HIS计费接口出错：" + ex.Message);
                ret = "0";
                msg = "pivas:" + ex.Message;
            }
            return ret;
        }

        /// <summary>
        /// 审方通过调用
        /// 0 不成功
        /// 1 成功
        /// 不管返回成功 不成功，程序都暂不做任何处理。只会将MSG报出来。
        /// </summary>
        /// <param name="GroupNo">处方组号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override string PivasRevPreTrue(string GroupNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        /// <summary>
        /// 审方不通过调用
        /// 0 不成功
        /// 1 成功
        /// 不管返回成功 不成功，程序都暂不做任何处理。只会将MSG报出来。
        /// </summary>
        /// <param name="GroupNo">处方组号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override string PivasRevPreFalse(string GroupNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        /// <summary>
        /// 打印计费按钮调用
        /// </summary>
        /// <param name="LabelNos"></param>
        /// <param name="DEmployeeID">计费人ID</param>
        /// <returns></returns>
        public override bool PrintCharge(List<string> LabelNos, string DEmployeeID)
        {
            return true;
        }

        /// <summary>
        /// 排药核对
        /// 0 不成功
        /// 1 成功
        /// 若不成功，程序将终止后续操作
        /// </summary>
        /// <param name="LabelNo">瓶签号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override string ScanPY5(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        /// <summary>
        /// 进仓核对
        /// 0 不成功
        /// 1 成功
        /// 若不成功，程序将终止后续操作
        /// </summary>
        /// <param name="LabelNo">瓶签号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override string ScanJC7(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        /// <summary>
        /// 配置核对（可通过此方法调用舱内扫描计费接口）
        /// 0 不成功
        /// 1 成功
        /// 若不成功，程序将终止后续操作
        /// </summary>
        /// <param name="LabelNo">瓶签号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override string ScanPZ9(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        /// <summary>
        /// 出仓核对
        /// 0 不成功
        /// 1 成功
        /// 若不成功，程序将终止后续操作
        /// </summary>
        /// <param name="LabelNo">瓶签号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override string ScanCC11(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        /// <summary>
        /// 打包核对
        /// 0 不成功
        /// 1 成功
        /// 若不成功，程序将终止后续操作
        /// </summary>
        /// <param name="LabelNo">瓶签号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override string ScanDB13(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        /// <summary>
        /// 病区签收
        /// 0 不成功
        /// 1 成功
        /// 若不成功，程序将终止后续操作
        /// </summary>
        /// <param name="LabelNo">瓶签号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override string ScanQS15(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        /// <summary>
        /// 更改ScreenDeatils表的状态
        /// </summary>
        /// <param name="LabelNo">瓶签号</param>
        /// <param name="DEmployeeID">员工号</param>
        /// <param name="Status">状态 0：未知；1：核对已成功</param>
        public override void ChangeStatus(string LabelNo, string DEmployeeID, string MoxaIP, string port, int Status)
        {
            //db.GetPIVAsDB("update ScreenDetail set Result='1',Msg='核对成功' where MoxaIP='" + LvS[i].MoxaIP + "' and port='" + LvS[i].MoxaPort + "'");
        }
    }
}
