using PIVAsCommon;
using PIVAsCommon.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ChargeInterface.ChargeGDHQService
{
    /// <summary>
    /// 广东华侨医院计费接口
    /// </summary>
    public class ChargeGDHQ : AbstractCharge
    {
        OrderManagerService hisChargeService = new OrderManagerService();

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
            //配置费
            ChargeItemInfo ItemInfo_pzf = new ChargeItemInfo();
            ItemInfo_pzf.Quantity = 1;
            //注射器
            ChargeItemInfo ItemInfo_zsq = new ChargeItemInfo();
            ItemInfo_zsq.Quantity = 1;

            //三升袋
            ChargeItemInfo ItemInfo_ssd = new ChargeItemInfo();
            ItemInfo_zsq.Quantity = 1;

            msg = string.Empty;
            string inpatid = "", execOrgId = "33020B", operatorId = "";
            string begintime = DateTime.Now.ToString();
            string endtime = "";
            string Parameters = "";
            string ret = string.Empty;
            string hisRet = string.Empty;
            string LabelOver = "", DrugType = "", DrugCount = "";

            try
            {
                string sql = "select  p.CaseID,iv.LabelOver,p.DrugType ,(select count(DrugCode) from PrescriptionDetail "+
                    "where PrescriptionDetail.GroupNo=p.GroupNo) as DrugCount from IVRecord iv inner join Prescription p "+
                    "on iv.PrescriptionID=p.PrescriptionID  where LabelNo='{0}'  ";
                sql = sql + " select top 1 hiscode from DEmployee  where DEmployeeID ='{1}'";
                sql = string.Format(sql, labelno, UserID);
                DataSet Lds = dbHelp.GetPIVAsDB(sql);

                #region 调用HIS计费接口前，判断是否需要调用
                if (Lds == null || Lds.Tables.Count <= 0 || Lds.Tables[0].Rows.Count <= 0 || Lds.Tables[1].Rows.Count <= 0)
                {
                    msg = "无计费信息";
                    return "0";
                }

                inpatid = Lds.Tables[0].Rows[0]["CaseID"].ToString().Trim();
                LabelOver = Lds.Tables[0].Rows[0]["LabelOver"].ToString().Trim();
                DrugType = Lds.Tables[0].Rows[0]["DrugType"].ToString().Trim();
                DrugCount = Lds.Tables[0].Rows[0]["DrugCount"].ToString().Trim();
                operatorId = Lds.Tables[1].Rows[0]["hiscode"].ToString().Trim();

                //瓶签配置已取消
                if (int.Parse(LabelOver) < 0)
                {
                    msg = ChargeResult_Const.MANUAL_CANCEL;
                    return "0";
                }
                //单药不收费，不调用his计费接口
                if (int.Parse(DrugCount) <= 1)
                {
                    msg = "单药不收费";
                    return "1";
                }
                //这里添加对IVRecord表中Remark3值的判断，若等于15，则已计费成功，直接返回计费成功，不再调用计费接口
                #endregion

                #region 调用his计费接口
                ResponseContainerOfBoolean hisret_pzf = null;
                ResponseContainerOfBoolean hisret_zsq = null;
                ResponseContainerOfBoolean hisret_ssd = null;

                /*
                    120400013-2b	其他药物集中配置
                    120400013-2a	抗肿瘤化学药物集中配置
                    120400013-2c	全静脉营养液集中配置
                    */

                //转换配置收费类型
                if (DrugType == "3")//化疗药
                {
                    ItemInfo_pzf.ItemId = "120400013-2a";
                    ItemInfo_zsq.ItemId = "950030044";

                    //住院号 inpatid，项目信息ItemInfo，执行科室编码execOrgId，操作员编码operatorId
                    hisret_pzf = hisChargeService.ChargeUpItem(inpatid, ItemInfo_pzf, execOrgId, operatorId);
                    hisret_zsq = hisChargeService.ChargeUpItem(inpatid, ItemInfo_zsq, execOrgId, operatorId);

                    if (hisret_pzf.ResponseContent)
                    {
                        Parameters = Parameters + "|配置费：" + inpatid + "," + ItemInfo_pzf.ItemId + "," + execOrgId + "," 
                            + operatorId;
                    }
                    else
                    {
                        Parameters = Parameters + "|配置费（失败）：" + inpatid + "," + ItemInfo_pzf.ItemId + "," 
                            + execOrgId + "," + operatorId;
                    }

                    if (hisret_zsq.ResponseContent)
                    {
                        Parameters = Parameters + "|耗材：" + inpatid + "," + ItemInfo_zsq.ItemId + "," + execOrgId + "," 
                            + operatorId;
                    }
                    else
                    {
                        Parameters = Parameters + "|耗材（失败）：" + inpatid + "," + ItemInfo_zsq.ItemId + "," 
                            + execOrgId + "," + operatorId;
                    }
                }
                else if (DrugType == "4")//营养处方
                {
                    /*
                        * 按照每组药的单位是否为ML判断使用哪种规格注射器
                    0.1ML	1ML	    1ML注射器	02010100087   950030034
                    1.1ML	2.4ML	2ML注射器	02010100088   950030035
                    2.5ML	5ML	    5ML注射器	02010100090   950030037
                    5.1ML	10ML	10ML注射器	02010100091   950030038
                    10.1ML	15ML	10ML注射器	02010100091   950030038
                    15.1ML	20ML	20ML注射器	950030040
                    20.1ML	30ML	30ML注射器	950030042
                    30.1ML	以上	50ML注射器	950030044
                    粉针剂	普通20ml抗30ml化50ml
                        * 
                        * 
                        * 后面确定 1ml：950030034       2ml：950030035      5ml：950030037        10ml：950030038

                        */

                    ItemInfo_pzf.ItemId = "120400013-2c";
                    //住院号 inpatid，项目信息ItemInfo，执行科室编码execOrgId，操作员编码operatorId
                    hisret_pzf = hisChargeService.ChargeUpItem(inpatid, ItemInfo_pzf, execOrgId, operatorId);

                    if (hisret_pzf.ResponseContent)
                    {
                        Parameters = Parameters + "|营养液配置费：" + inpatid + "," + ItemInfo_pzf.ItemId + ","
                            + execOrgId + "," + operatorId;
                    }
                    else
                    {
                        Parameters = Parameters + "|营养液配置费（失败）：" + inpatid + "," + ItemInfo_pzf.ItemId + "," 
                            + execOrgId + "," + operatorId;
                    }
                    string sql_hly = "select pr.Dosage, pr.DosageUnit,p.UsageCode,pr.DrugCode,dr.TheDrugType from IVRecord iv "+
                        "inner join Prescription p on iv.PrescriptionID=p.PrescriptionID inner join PrescriptionDetail pr on "+
                        "iv.GroupNo=pr.GroupNo left join DDrug dr on pr.DrugCode=dr.DrugCode where LabelNo='" + labelno + "'";

                    DataSet dsSQL_hly = dbHelp.GetPIVAsDB(sql_hly);

                    if (dsSQL_hly.Tables.Count > 0)
                    {
                        DataTable DosageDT = dsSQL_hly.Tables[0];
                        for (int i = 0; i < DosageDT.Rows.Count; i++)
                        {
                            //如果单位是ML的按医生开的剂量绑定耗材
                            if (DosageDT.Rows[i]["DosageUnit"].ToString().Equals("ml"))
                            {
                                if (float.Parse(DosageDT.Rows[i]["Dosage"].ToString()) >= 0.1 && float.Parse(DosageDT.Rows[i]["Dosage"].ToString()) <= 1)
                                {
                                    ItemInfo_zsq.ItemId = "950030034";
                                }
                                else if (float.Parse(DosageDT.Rows[i]["Dosage"].ToString()) > 1 && float.Parse(DosageDT.Rows[i]["Dosage"].ToString()) <= 2.5)
                                {
                                    ItemInfo_zsq.ItemId = "950030035";
                                }
                                else if (float.Parse(DosageDT.Rows[i]["Dosage"].ToString()) > 2.5 && float.Parse(DosageDT.Rows[i]["Dosage"].ToString()) <= 5)
                                {
                                    ItemInfo_zsq.ItemId = "950030037";
                                }
                                else if (float.Parse(DosageDT.Rows[i]["Dosage"].ToString()) > 5 && float.Parse(DosageDT.Rows[i]["Dosage"].ToString()) <= 15)
                                {
                                    ItemInfo_zsq.ItemId = "950030038";
                                }
                                else if (float.Parse(DosageDT.Rows[i]["Dosage"].ToString()) > 15 && float.Parse(DosageDT.Rows[i]["Dosage"].ToString()) <= 20)
                                {
                                    ItemInfo_zsq.ItemId = "950030040";
                                }
                                else if (float.Parse(DosageDT.Rows[i]["Dosage"].ToString()) > 20 && float.Parse(DosageDT.Rows[i]["Dosage"].ToString()) <= 30)
                                {
                                    ItemInfo_zsq.ItemId = "950030042";
                                }
                                else
                                {
                                    ItemInfo_zsq.ItemId = "950030044";
                                }
                            }
                            else //非ML单位的查询药品的属性普、抗、化
                            {
                                if (DosageDT.Rows[i]["TheDrugType"].ToString().Equals("1"))
                                {
                                    ItemInfo_zsq.ItemId = "950030040";
                                }
                                else if (DosageDT.Rows[i]["TheDrugType"].ToString().Equals("2"))
                                {
                                    ItemInfo_zsq.ItemId = "950030042";
                                }
                                else if (DosageDT.Rows[i]["TheDrugType"].ToString().Equals("3"))
                                {
                                    ItemInfo_zsq.ItemId = "950030044";
                                }
                                else
                                {
                                    ItemInfo_zsq.ItemId = "950030040";
                                }
                            }
                            hisret_zsq = hisChargeService.ChargeUpItem(inpatid, ItemInfo_zsq, execOrgId, operatorId);
                            if (hisret_zsq.ResponseContent)
                            {
                                Parameters = Parameters + "|营养液耗材：" + ItemInfo_zsq.ItemId;
                            }
                            else
                            {
                                Parameters = Parameters + "|营养液耗材（失败）：" + ItemInfo_zsq.ItemId;
                            }
                        }

                        /*
                        一次性肠外营养输液袋970174003 按照组收费，医嘱用法为IVYY静脉营养才收费
                            * */

                        if (DosageDT.Rows[0]["UsageCode"].ToString().Contains("YY"))
                        {
                            ItemInfo_ssd.ItemId = "970174003";
                            hisret_ssd = hisChargeService.ChargeUpItem(inpatid, ItemInfo_ssd, execOrgId, operatorId);
                            if (hisret_ssd.ResponseContent)
                            {
                                Parameters = Parameters + "|营养液三升袋：" + ItemInfo_ssd.ItemId;
                            }
                            else
                            {
                                Parameters = Parameters + "|营养液三升袋（失败）：" + ItemInfo_ssd.ItemId;
                            }
                        }
                    }
                }
                else
                {
                    ItemInfo_pzf.ItemId = "120400013-2b";
                    if (DrugType == "2")
                    {
                        ItemInfo_zsq.ItemId = "950030042";
                    }
                    else
                    {
                        ItemInfo_zsq.ItemId = "950030040";
                    }

                    //住院号 inpatid，项目信息ItemInfo，执行科室编码execOrgId，操作员编码operatorId
                    hisret_pzf = hisChargeService.ChargeUpItem(inpatid, ItemInfo_pzf, execOrgId, operatorId);

                    hisret_zsq = hisChargeService.ChargeUpItem(inpatid, ItemInfo_zsq, execOrgId, operatorId);

                    if (hisret_pzf.ResponseContent)
                    {
                        Parameters = Parameters + "|配置费：" + inpatid + "," + ItemInfo_pzf.ItemId + "," + execOrgId + "," + operatorId;
                    }
                    else
                    {
                        Parameters = Parameters + "|配置费（失败）：" + inpatid + "," + ItemInfo_pzf.ItemId + "," + execOrgId + "," + operatorId;
                    }

                    if (hisret_zsq.ResponseContent)
                    {
                        Parameters = Parameters + "|耗材：" + inpatid + "," + ItemInfo_zsq.ItemId + "," + execOrgId + "," + operatorId;
                    }
                    else
                    {
                        Parameters = Parameters + "|耗材（失败）：" + inpatid + "," + ItemInfo_zsq.ItemId + "," + execOrgId + "," + operatorId;
                    }

                }
                #endregion

                #region 根据HIS计费接口返回值，更新pivas数据库表，更新内容如下
                //1、HIS确定都计费成功的，将IVRecord表中Remark3更新为15
                //2、HIS确定都计费失败的，将IVRecord表中LabelOver更新为-3
                //3、HIS因各种原因造成计费结果模糊的，什么也不处理，ret值设置为"0"
                if (hisret_pzf.ResponseContent && hisret_zsq.ResponseContent)
                {
                    ret = "1";
                    msg = "配置费：成功" + "|注射器：成功";
                    hisRet += "配置费：" + hisret_pzf.ResponseContent.ToString() + "|注射器：" + hisret_zsq.ResponseContent.ToString();
                }
                else if (!hisret_pzf.ResponseContent && hisret_zsq.ResponseContent)
                {
                    ret = "0";
                    msg = "配置费： " + hisret_pzf.ExceptionMessage + "|注射器：成功";
                    hisRet += "配置费：" + hisret_pzf.ResponseContent.ToString() + "|注射器：" + hisret_zsq.ResponseContent.ToString();
                }
                else if (hisret_pzf.ResponseContent && !hisret_zsq.ResponseContent)
                {
                    ret = "0";
                    msg = "配置费：成功" + "|注射器：" + hisret_zsq.ExceptionMessage;
                    hisRet += "配置费：" + hisret_pzf.ResponseContent.ToString() + "|注射器：" + hisret_zsq.ResponseContent.ToString();
                }
                else
                {
                    ret = "0";
                    msg = "配置费：" + hisret_pzf.ExceptionMessage + "|注射器：" + hisret_zsq.ExceptionMessage;
                    hisRet += "配置费：" + hisret_pzf.ResponseContent.ToString() + "|注射器：" + hisret_zsq.ResponseContent.ToString();
                }
                #endregion
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("调用HIS计费接口出错：" + ex.Message);
                ret = "0";
                msg = "pivas:" + ex.Message;
            }

            endtime = DateTime.Now.ToString();
            string sql_toHis = "insert into ToHisChargeLog (begintime,endtime,labelno,[Parameters],ChargeResult,"+
                "HisReturn,msg,Remark1) values ('" + begintime + "','" + endtime + "','" + labelno + "','" + Parameters 
                + "','" + ret + "' ,'','" + msg + "','" + hisRet + "' )";
            dbHelp.GetPIVAsDB(sql_toHis);

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
