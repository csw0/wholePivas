using PIVAsCommon.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace ChargeInterface
{
    /// <summary>
    /// 河南肿瘤的计费接口
    /// </summary>
    public class ChargeHNZL : AbstractCharge
    {
        // Fields
        private OracleHelper orcl = new OracleHelper();

        /// <summary>
        /// 显示屏相关，暂时不用
        /// </summary>
        /// <param name="LabelNo"></param>
        /// <param name="DEmployeeID"></param>
        /// <param name="MoxaIP"></param>
        /// <param name="moxaPort"></param>
        /// <param name="Status"></param>
        public override void ChangeStatus(string LabelNo, string DEmployeeID, string MoxaIP, string moxaPort, int Status)
        {
            //dbHelp.GetPIVAsDB("update ScreenDetail set Result=" + Status + ", Msg='核对成功' where MoxaIP='"
            //    + MoxaIP + "' and port='" + moxaPort + "' and LabelNo = '" + LabelNo + "' ");
        }

        public override string Charge(string labelno, string UserID, out string msg)
        {
            Exception exception;
            string str = "";
            string str2 = "";
            string str3 = "0";
            string str4 = "";
            string str5 = "";
            string str6 = "";
            string str7 = "";
            msg = str2;
            string sqlStr = " select Schedule,DrugCode,Quantity,i.IVStatus from UseDrugList u" 
                + " inner join IVRecord i on u.DrugLGroupNo =i.DrugLGroupNo  where LabelNo ='" + labelno + "'" +
                " select AccountID ,Demployeename  from DEmployee where DEmployeeID='" + UserID + "'";

            try
            {
                DataSet pIVAsDB = this.dbHelp.GetPIVAsDB(sqlStr);
                if (((pIVAsDB != null) && (pIVAsDB.Tables.Count > 0)) && (pIVAsDB.Tables[0].Rows.Count > 0))
                {
                    for (int i = 0; i < pIVAsDB.Tables[0].Rows.Count; i++)
                    {
                        string str9 = pIVAsDB.Tables[1].Rows[0]["AccountID"].ToString().Trim();
                        string str10 = pIVAsDB.Tables[1].Rows[0]["Demployeename"].ToString().Trim();
                        str6 = pIVAsDB.Tables[0].Rows[i]["Schedule"].ToString().Trim();
                        string str11 = pIVAsDB.Tables[0].Rows[i]["DrugCode"].ToString().Trim();
                        string str12 = pIVAsDB.Tables[0].Rows[i]["Quantity"].ToString().Trim();
                        string s = pIVAsDB.Tables[0].Rows[i]["IVStatus"].ToString().Trim();
                        string str14 = "执行单号: " + str6 + " 药品编码: " + str11 + " 药品数量: " + str12;
                        str4 = DateTime.Now.ToString();
                        if (int.Parse(s) >= 9)
                        {
                            msg = str2 = "pivas:已配置";
                            str3 = "1";
                            return "1";
                        }
                        if (pIVAsDB != null)
                        {
                            String strBackDrug = "select  DRUG_CODE,QUANTITY,BACKDT,BACKER from " +
                                "(select DRUG_CODE,QUANTITY,BACKDT,BACKER,EXEC_SQN from zlhis.v_pivas_drug_back a union all " +
                                "select DRUG_CODE,QUANTITY,BACKDT,BACKER,EXEC_SQN from  zlhis.V_PIVAS_DRUG_BACK_sq  b) c where c.EXEC_SQN  ='"
                                + str6 + "'  and c.backdt > sysdate-2";
                            DataSet set2 = dbHelp.GetHisOracleByOLEDB(strBackDrug);

                            if (((set2 != null) && (set2.Tables.Count > 0)) && (set2.Tables[0].Rows.Count > 0))
                            {
                                string str15 = set2.Tables[0].Rows[0]["DRUG_CODE"].ToString().Trim();
                                string str16 = set2.Tables[0].Rows[0]["QUANTITY"].ToString().Trim();
                                string str17 = set2.Tables[0].Rows[0]["BACKDT"].ToString().Trim();
                                string str18 = set2.Tables[0].Rows[0]["BACKER"].ToString().Trim();
                                DataSet set3 = this.dbHelp.GetPIVAsDB(" select DEmployeeID   from DEmployee where DEmployeeCode='" + str18 + "'");
                                if (((set3 != null) && (set3.Tables.Count > 0)) && (set3.Tables[0].Rows.Count > 0))
                                {
                                    str7 = set3.Tables[0].Rows[0]["DEmployeeID"].ToString().Trim();
                                }
                                string str19 = " 执行单号: " + str6 + " 药品编码: " + str15 + " 退药数量: " + str16 + " 退药时间: " + str17 + " 退药人: " + str18;
                                str3 = "0";
                                msg = "退药";
                                str5 = DateTime.Now.ToString();
                                this.dbHelp.GetPIVAsDB("update ivrecord set labelover ='-4' ,wardretreat ='2',wardrid ='" + str7 + "',wardrtime='" + str17 + "',labeloverid='" + UserID + "',labelovertime='" + DateTime.Now.ToString() + "' where labelno='" + labelno + "' ");
                                this.dbHelp.GetPIVAsDB("INSERT INTO ToHisChargeLog (begintime,endtime,LabelNo,[Parameters],ChargeResult,HisReturn,msg)  VALUES ('" + str4 + "','" + str5 + "','" + labelno + "','" + str19 + "','" + str3 + "','" + str + "','" + str19 + "')");
                                return "0";
                            }

                            str14 = "执行存储过程：" + DateTime.Now.ToString() + "执行流水号Schedule：" + str6;
                            try
                            {
                                OleDbParameter[] parameterArray = new OleDbParameter[] { new OleDbParameter("PAR_EXEC_SQN", OleDbType.VarChar) };
                                parameterArray[0].Value = str6;
                                dbHelp.ExecProcHisOracleByOLEDB("zlhis.prc_pyzxxg", new OleDbParameter[] { parameterArray[0] });

                                string str20 = "'call zlhis.prc_pyzxxg(" + str6 + "):" + DateTime.Now.ToString();
                                str5 = DateTime.Now.ToString();
                                this.dbHelp.GetPIVAsDB("INSERT INTO ToHisChargeLog (begintime,endtime,LabelNo,[Parameters],ChargeResult,"+
                                    "HisReturn,msg,remark1)  VALUES ('" + str4 + "','" + str5 + "','" + labelno + "','" + str14 + 
                                    "','1','1','1'," + str20 + "')");

                                msg = "核对成功";
                            }
                            catch (Exception ex1)
                            {
                                exception = ex1;
                                msg = str2 = "pivas:" + exception.Message;
                                str3 = "0";
                                return "0";
                            }
                            str3 = "1";
                        }
                    }
                }
            }
            catch (Exception ex2)
            {
                exception = ex2;
                msg = str2 = "pivas:" + exception.Message;
                str3 = "0";
                return "0";
            }
            return str3;
        }

        public override string PivasRevPreFalse(string GroupNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        public override string PivasRevPreTrue(string GroupNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        public override bool PrintCharge(List<string> LabelNos, string DEmployeeID)
        {
            return true;
        }

        public override string ScanPY5(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        public override string ScanJC7(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        /// <summary>
        /// 与Charge方法完全一样
        /// </summary>
        /// <param name="labelno"></param>
        /// <param name="UserID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override string ScanPZ9(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = String.Empty;
            return string.Empty;
        }

        public override string ScanCC11(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        public override string ScanDB13(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        public override string ScanQS15(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }
    }
}
