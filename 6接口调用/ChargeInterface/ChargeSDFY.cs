using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;
using ChargeInterface.WebReference;
using PIVAsCommon.Helper;
using PIVAsCommon;

namespace ChargeInterface
{
    /// <summary>
    /// 苏大附一计费接口
    /// </summary>
    public class ChargeSDFY : AbstractCharge
    {
        private DB_Help DB = new DB_Help();
        private ServiceSoapClient tw = new ServiceSoapClient();

        public override string Charge(string labelno, string UserID, out string msg)
        {
            Exception exception;
            try
            {
                msg = string.Empty;
                string str = string.Empty;
                string str2 = string.Empty;
                DataSet pIVAsDB = this.DB.GetPIVAsDB(string.Format("select p.DrugType,ivd.drugname,p.usagecode,* from IVRecord i inner join IVRecordDetail ivd on i.IVRecordID =ivd.IVRecordID inner join Prescription p on i.GroupNo =p.GroupNo where i.LabelNo ={0} ", labelno));
                DataSet set2 = this.DB.GetPIVAsDB(string.Format(" select AccountID  from DEmployee where DEmployeeID={0}", UserID));
                string str3 = pIVAsDB.Tables[0].Rows[0]["GroupNo"].ToString().Trim().Split(new char[] { '.' })[0].ToString();
                string s = pIVAsDB.Tables[0].Rows[0]["InfusionDT"].ToString().Trim();
                if (int.Parse(DateTime.Parse(s).ToString("HH")) < 5)
                {
                    s = DateTime.Parse(s).AddDays(1.0).ToString();
                }
                string str5 = set2.Tables[0].Rows[0]["AccountID"].ToString().Trim();
                string str6 = pIVAsDB.Tables[0].Rows[0]["PatCode"].ToString().Trim();
                string str7 = pIVAsDB.Tables[0].Rows[0]["DrugType"].ToString().Trim();
                string str8 = pIVAsDB.Tables[0].Rows[0]["usagecode"].ToString().Trim();
                string str9 = pIVAsDB.Tables[0].Rows[0]["IVStatus"].ToString().Trim();
                int num = pIVAsDB.Tables[0].Rows.Count - 1;
                for (int i = 0; i < pIVAsDB.Tables[0].Rows.Count; i++)
                {
                    if (pIVAsDB.Tables[0].Rows[i]["drugname"].ToString().Trim().Contains("(嘱托)胰岛素注射液"))
                    {
                        num = pIVAsDB.Tables[0].Rows.Count - 2;
                    }
                }
                string str11 = pIVAsDB.Tables[0].Rows[0]["LabelOver"].ToString().Trim();
                str5 = set2.Tables[0].Rows[0]["AccountID"].ToString().Trim();
                if (int.Parse(str11) < 0)
                {
                    msg = "PIVAS:配置取消";
                    return "0";
                }
                //if (int.Parse(str9) >= 9)
                //{
                //    msg = "PIVAS:已核对";
                //    return "1";
                //}
                InternalLogger.Log.Debug("Ivrecord表中IVStatus的值：" + str9);
                string str12 = DateTime.Now.ToString();
                string str13 = string.Empty;
                try
                {
                    DataSet pIVAsHISDBSQL = this.DB.GetPIVAsHISDBSQL(string.Format(string.Concat(new object[] 
                    {
                        "declare @a int,@b varchar(100)  exec pivas_lay_resi_by_carry '", str3, "','", s, "','", num, "','01198', @a out,@b out  select @a  ,@b "
                    }), new object[0]));
                    str2 = pIVAsHISDBSQL.Tables[0].Rows[0][0].ToString().Trim();
                    str = pIVAsHISDBSQL.Tables[0].Rows[0][1].ToString();
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    msg = "pivas:" + exception.Message;
                }
                string str14 = DateTime.Now.ToString();

                InternalLogger.Log.Debug("His计费返回值：" + str2);
                //str2的返回值枚举0:未知，1:计费成功，
                //2:各种原因导致，病人不存在或已经出院/找不到记录/子医嘱数量不匹配/已经作废等
                if (str2 == "1")//1:计费成功
                {
                    str13 = "1";
                    this.DB.GetPIVAsDB("update IVRecord set IVStatus =9  where LabelNo =" + labelno);
                }
                else if (str2 == "2")//这种情况，明确归类为不能配置，所以将LabelOver赋值为-3，计费处取消
                {
                    str13 = "0";
                    this.DB.GetPIVAsDB("update IVRecord set LabelOver =-3,LabelOverTime =GETDATE(),LabelOverID ='" 
                        + str5 + "' where LabelNo =" + labelno);
                }
                else //其他情况暂时只见到str2=0，归类为计费失败
                {
                    str13 = "0";
                }

                string pxml = "";
                string str16 = "";
                if ((str2 == "1") && (((str8 == "1") || (str8 == "TPN")) || (str8 == "111")))
                {
                    if (((pIVAsDB != null) && (pIVAsDB.Tables.Count > 0)) && (pIVAsDB.Tables[0].Rows.Count > 0))
                    {
                        pxml = "<VIEW_FEE_ITEMLIST>";
                        pxml = (((((pxml + "<INP_NO>" + str6 + "</INP_NO>") + "<PATIENT_ID></PATIENT_ID>") + "<NAME>01198</NAME>" + "<ITEM_CODE>301317</ITEM_CODE>") + "<ITEM_NAME></ITEM_NAME>" + "<PRICE>0</PRICE>") + "<QTY>1</QTY>" + "<FEE_DATE></FEE_DATE>") + "<MARK></MARK>" + "</VIEW_FEE_ITEMLIST>";
                    }
                    string str17 = DateTime.Now.ToString();
                    str16 = this.tw.InputFee(pxml);
                    DataSet set4 = new DataSet();
                    StringReader input = new StringReader(str16);
                    XmlTextReader reader = new XmlTextReader(input);
                    set4.ReadXml(reader);
                    DataTable table = set4.Tables[0];
                    DataTable table2 = new DataTable();
                    table2.Columns.Add("res");
                    table2.Columns.Add("err");
                    for (int j = 0; j < table.Rows.Count; j++)
                    {
                        DataRow row = table2.NewRow();
                        row["res"] = table.Rows[j]["ReturnCode"].ToString();
                        row["err"] = table.Rows[j]["ReturnName"].ToString();
                        table2.Rows.Add(row);
                        string str18 = row["res"].ToString();
                        string str19 = row["err"].ToString();
                        string str23 = str18.Trim();
                        if ((str23 != null) && (str23 == "1"))
                        {
                            msg = "配置费成功";
                            str13 = "1";
                            this.DB.GetPIVAsDB("insert into [ChargeRemark] ([LabelNo],[DrugType],[InsertCode],[Status],[Msg]) values ('" + labelno + "','6','" + str5 + "','" + str13 + "','" + str19 + "' )");
                        }
                        else
                        {
                            msg = "配置费失败";
                            str13 = "0";
                            this.DB.GetPIVAsDB("insert into [ChargeRemark] ([LabelNo],[DrugType],[InsertCode],[Status],[Msg]) values ('" + labelno + "','6','" + str5 + "','" + str13 + "','" + str19 + "' )");
                        }
                        string str20 = DateTime.Now.ToString();
                        this.DB.GetPIVAsDB("insert into ToHisChargeLog (begintime,endtime,labelno,msg,ChargeResult,Remark2,[Parameters],HisReturn,Remark1) values ('" + str17 + "','" + str20 + "','" + labelno + "','" + msg + "','" + str13 + "' ,'" + str5 + "','" + pxml + "','" + str18 + "','" + str16 + "' )");
                    }
                }

                msg = (str == "记费成功") ? "计药费成功" : str;
                string str21 = string.Concat(new object[] { "药品数量：", num, " 医嘱组号：", str3, " 用药时间：", s, " 发药人：01198 登录人", str5 });
                this.DB.GetPIVAsDB("insert into ToHisChargeLog (begintime,endtime,labelno,msg,ChargeResult,Remark2,[Parameters],HisReturn) values ('" + str12 + "','" + str14 + "','" + labelno + "','" + msg + "','" + str13 + "' ,'" + str5 + "','" + str21 + "','" + str2 + "' )");
                return str13;
            }
            catch (Exception exception2)
            {
                exception = exception2;
                msg = exception.Message;
                return "0";
            }
        }

        public string ChargeForPeiZhi(string LabelNo, string DEmployeeID, out string msg)
        {
            Exception exception;
            try
            {
                msg = string.Empty;
                string str = string.Empty;
                string str2 = string.Empty;
                DataSet pIVAsDB = this.DB.GetPIVAsDB(string.Format("select p.DrugType,ivd.drugcode,p.usagecode,* from IVRecord i inner join IVRecordDetail ivd on i.IVRecordID =ivd.IVRecordID inner join Prescription p on i.GroupNo =p.GroupNo where i.LabelNo ={0} ", LabelNo));
                DataSet set2 = this.DB.GetPIVAsDB(string.Format(" select AccountID  from DEmployee where DEmployeeID={0}", DEmployeeID));
                string str3 = pIVAsDB.Tables[0].Rows[0]["GroupNo"].ToString().Trim().Split(new char[] { '.' })[0].ToString();
                string str4 = pIVAsDB.Tables[0].Rows[0]["InfusionDT"].ToString().Trim();
                string str5 = set2.Tables[0].Rows[0]["AccountID"].ToString().Trim();
                string str6 = pIVAsDB.Tables[0].Rows[0]["PatCode"].ToString().Trim();
                string str7 = pIVAsDB.Tables[0].Rows[0]["DrugType"].ToString().Trim();
                string str8 = pIVAsDB.Tables[0].Rows[0]["usagecode"].ToString().Trim();
                string s = pIVAsDB.Tables[0].Rows[0]["IVStatus"].ToString().Trim();
                int num = pIVAsDB.Tables[0].Rows.Count - 1;
                for (int i = 0; i < pIVAsDB.Tables[0].Rows.Count; i++)
                {
                    switch (pIVAsDB.Tables[0].Rows[i]["drugcode"].ToString().Trim())
                    {
                        case "00053":
                        case "01112":
                            num = pIVAsDB.Tables[0].Rows.Count - 2;
                            break;
                    }
                }
                if (int.Parse(pIVAsDB.Tables[0].Rows[0]["LabelOver"].ToString().Trim()) < 0)
                {
                    msg = "PIVAS:配置取消";
                    return "0";
                }
                if (int.Parse(s) >= 9)
                {
                    msg = "PIVAS:已核对";
                    return "1";
                }
                string str12 = DateTime.Now.ToString();
                string str13 = string.Empty;
                try
                {
                    DataSet pIVAsHISDBSQL = this.DB.GetPIVAsHISDBSQL(string.Format(string.Concat(new object[] { "declare @a int,@b varchar(100)  exec pivas_lay_resi_by_carry '", str3, "','", str4, "','", num, "','", str5, "', @a out,@b out  select @a  ,@b " }), new object[0]));
                    str2 = pIVAsHISDBSQL.Tables[0].Rows[0][0].ToString();
                    str = pIVAsHISDBSQL.Tables[0].Rows[0][1].ToString();
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    msg = "pivas:" + exception.Message;
                }
                string str14 = DateTime.Now.ToString();
                if (str2 == "1")
                {
                    str13 = "1";
                }
                else if (str == "病人医嘱已停！")
                {
                    str13 = "0";
                    this.DB.GetPIVAsDB("update IVRecord set LabelOver =-3,LabelOverTime =GETDATE (),LabelOverID ='" + str5 + "' where LabelNo =" + LabelNo);
                }
                else
                {
                    str13 = "0";
                }
                string pxml = "";
                string str16 = "";
                if ((str2 == "1") && (((str8 == "1") || (str8 == "TPN")) || (str8 == "111")))
                {
                    if (((pIVAsDB != null) && (pIVAsDB.Tables.Count > 0)) && (pIVAsDB.Tables[0].Rows.Count > 0))
                    {
                        pxml = "<VIEW_FEE_ITEMLIST>";
                        pxml = ((((((pxml + "<INP_NO>" + str6 + "</INP_NO>") + "<PATIENT_ID></PATIENT_ID>") + "<NAME>" + str5 + "</NAME>") + "<ITEM_CODE>301317</ITEM_CODE>") + "<ITEM_NAME></ITEM_NAME>" + "<PRICE>0</PRICE>") + "<QTY>1</QTY>" + "<FEE_DATE></FEE_DATE>") + "<MARK></MARK>" + "</VIEW_FEE_ITEMLIST>";
                    }
                    string str17 = DateTime.Now.ToString();
                    str16 = this.tw.InputFee(pxml);
                    DataSet set4 = new DataSet();
                    StringReader input = new StringReader(str16);
                    XmlTextReader reader = new XmlTextReader(input);
                    set4.ReadXml(reader);
                    DataTable table = set4.Tables[0];
                    DataTable table2 = new DataTable();
                    table2.Columns.Add("res");
                    table2.Columns.Add("err");
                    for (int j = 0; j < table.Rows.Count; j++)
                    {
                        DataRow row = table2.NewRow();
                        row["res"] = table.Rows[j]["ReturnCode"].ToString();
                        row["err"] = table.Rows[j]["ReturnName"].ToString();
                        table2.Rows.Add(row);
                        string str18 = row["res"].ToString();
                        string str19 = row["err"].ToString();
                        string str23 = str18.Trim();
                        if ((str23 != null) && (str23 == "1"))
                        {
                            msg = "配置费成功";
                            str13 = "1";
                            this.DB.GetPIVAsDB("insert into [ChargeRemark] ([LabelNo],[DrugType],[InsertCode],[Status],[Msg]) values ('" + LabelNo + "','6','" + str5 + "','" + str18 + "','" + msg + "' )");
                        }
                        else
                        {
                            msg = "配置费失败";
                            str13 = "0";
                            this.DB.GetPIVAsDB("insert into [ChargeRemark] ([LabelNo],[DrugType],[InsertCode],[Status],[Msg]) values ('" + LabelNo + "','6','" + str5 + "','" + str18 + "','" + msg + "' )");
                        }
                        string str20 = DateTime.Now.ToString();
                        this.DB.GetPIVAsDB("insert into ToHisChargeLog (begintime,endtime,labelno,msg,ChargeResult,Remark2,[Parameters],HisReturn,Remark1) values ('" + str17 + "','" + str20 + "','" + LabelNo + "','" + msg + "','" + str13 + "' ,'" + str5 + "','" + pxml + "','" + str18 + "','" + str16 + "' )");
                    }
                }
                msg = (str == "记费成功") ? "计药费成功" : str;
                string str21 = string.Concat(new object[] { "药品数量：", num, " 医嘱组号：", str3, " 用药时间：", str4, " 员工号：", str5 });
                this.DB.GetPIVAsDB("insert into ToHisChargeLog (begintime,endtime,labelno,msg,ChargeResult,Remark2,[Parameters],HisReturn) values ('" + str12 + "','" + str14 + "','" + LabelNo + "','" + msg + "','" + str13 + "' ,'" + str5 + "','" + str21 + "','" + str2 + "' )");
                return str13;
            }
            catch (Exception exception2)
            {
                exception = exception2;
                msg = exception.Message;
                return "0";
            }
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

        public string ReturnsForCharge(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
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

        public override string ScanPZ9(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = string.Empty;
            string str = string.Empty;
            string str2 = string.Empty;
            DataSet pIVAsDB = this.DB.GetPIVAsDB(string.Format("select p.DrugType,ivd.drugname,p.usagecode ,* from IVRecord i inner join IVRecordDetail ivd on i.IVRecordID =ivd.IVRecordID inner join Prescription p on i.GroupNo =p.GroupNo where i.LabelNo ={0} ", LabelNo));
            DataSet set2 = this.DB.GetPIVAsDB(string.Format(" select AccountID  from DEmployee where DEmployeeID={0}", DEmployeeID));
            string str3 = pIVAsDB.Tables[0].Rows[0]["GroupNo"].ToString().Trim().Split(new char[] { '.' })[0].ToString();
            string s = pIVAsDB.Tables[0].Rows[0]["InfusionDT"].ToString().Trim();
            if (int.Parse(DateTime.Parse(s).ToString("HH")) < 5)
            {
                s = DateTime.Parse(s).AddDays(1.0).ToString();
            }
            string str5 = set2.Tables[0].Rows[0]["AccountID"].ToString().Trim();
            string str6 = pIVAsDB.Tables[0].Rows[0]["PatCode"].ToString().Trim();
            string str7 = pIVAsDB.Tables[0].Rows[0]["DrugType"].ToString().Trim();
            string str8 = pIVAsDB.Tables[0].Rows[0]["usagecode"].ToString().Trim();
            string str9 = pIVAsDB.Tables[0].Rows[0]["IVStatus"].ToString().Trim();
            int num = pIVAsDB.Tables[0].Rows.Count - 1;
            for (int i = 0; i < pIVAsDB.Tables[0].Rows.Count; i++)
            {
                if (pIVAsDB.Tables[0].Rows[i]["drugname"].ToString().Trim().Contains("(嘱托)胰岛素注射液"))
                {
                    num = pIVAsDB.Tables[0].Rows.Count - 2;
                }
            }
            if (int.Parse(pIVAsDB.Tables[0].Rows[0]["LabelOver"].ToString().Trim()) < 0)
            {
                msg = "PIVAS:配置取消";
                return "0";
            }
            if (int.Parse(str9) >= 9)
            {
                msg = "PIVAS:已核对";
                return "1";
            }
            string str12 = DateTime.Now.ToString();
            string str13 = string.Empty;
            try
            {
                DataSet pIVAsHISDBSQL = this.DB.GetPIVAsHISDBSQL(string.Format(string.Concat(new object[] { "declare @a int,@b varchar(100)  exec pivas_lay_resi_by_carry '", str3, "','", s, "','", num, "','01198', @a out,@b out  select @a  ,@b " }), new object[0]));
                str2 = pIVAsHISDBSQL.Tables[0].Rows[0][0].ToString();
                str = pIVAsHISDBSQL.Tables[0].Rows[0][1].ToString();
            }
            catch (Exception exception)
            {
                msg = "pivas:" + exception.Message;
            }
            string str14 = DateTime.Now.ToString();
            if (str2 == "1")
            {
                str13 = "1";
                this.DB.GetPIVAsDB("update IVRecord set IVStatus =9  where LabelNo =" + LabelNo);
            }
            else if (str == "病人医嘱已停！")
            {
                str13 = "0";
                this.DB.GetPIVAsDB("update IVRecord set LabelOver =-3,LabelOverTime =GETDATE(),LabelOverID ='" + str5 + "' where LabelNo =" + LabelNo);
            }
            else
            {
                str13 = "0";
                this.DB.GetPIVAsDB("update IVRecord set LabelOver =-3,LabelOverTime =GETDATE(),LabelOverID ='" + str5 + "' where LabelNo =" + LabelNo);
            }
            string pxml = "";
            string str16 = "";
            if ((str2 == "1") && (((str8 == "1") || (str8 == "TPN")) || (str8 == "111")))
            {
                if (((pIVAsDB != null) && (pIVAsDB.Tables.Count > 0)) && (pIVAsDB.Tables[0].Rows.Count > 0))
                {
                    pxml = "<VIEW_FEE_ITEMLIST>";
                    pxml = (((((pxml + "<INP_NO>" + str6 + "</INP_NO>") + "<PATIENT_ID></PATIENT_ID>") + "<NAME>01198</NAME>" + "<ITEM_CODE>301317</ITEM_CODE>") + "<ITEM_NAME></ITEM_NAME>" + "<PRICE>0</PRICE>") + "<QTY>1</QTY>" + "<FEE_DATE></FEE_DATE>") + "<MARK></MARK>" + "</VIEW_FEE_ITEMLIST>";
                }
                string str17 = DateTime.Now.ToString();
                str16 = this.tw.InputFee(pxml);
                DataSet set4 = new DataSet();
                StringReader input = new StringReader(str16);
                XmlTextReader reader = new XmlTextReader(input);
                set4.ReadXml(reader);
                DataTable table = set4.Tables[0];
                DataTable table2 = new DataTable();
                table2.Columns.Add("res");
                table2.Columns.Add("err");
                for (int j = 0; j < table.Rows.Count; j++)
                {
                    DataRow row = table2.NewRow();
                    row["res"] = table.Rows[j]["ReturnCode"].ToString();
                    row["err"] = table.Rows[j]["ReturnName"].ToString();
                    table2.Rows.Add(row);
                    string str18 = row["res"].ToString();
                    string str19 = row["err"].ToString();
                    string str23 = str18.Trim();
                    if ((str23 != null) && (str23 == "1"))
                    {
                        msg = "配置费成功";
                        str13 = "1";
                        this.DB.GetPIVAsDB("insert into [ChargeRemark] ([LabelNo],[DrugType],[InsertCode],[Status],[Msg]) values ('" + LabelNo + "','6','" + str5 + "','" + str13 + "','" + msg + "' )");
                    }
                    else
                    {
                        msg = "配置费失败";
                        str13 = "0";
                        this.DB.GetPIVAsDB("insert into [ChargeRemark] ([LabelNo],[DrugType],[InsertCode],[Status],[Msg]) values ('" + LabelNo + "','6','" + str5 + "','" + str13 + "','" + msg + "' )");
                    }
                    string str20 = DateTime.Now.ToString();
                    this.DB.GetPIVAsDB("insert into ToHisChargeLog (begintime,endtime,labelno,msg,ChargeResult,Remark2,[Parameters],HisReturn,Remark1) values ('" + str17 + "','" + str20 + "','" + LabelNo + "','" + msg + "','" + str13 + "' ,'" + str5 + "','" + pxml + "','" + str18 + "','" + str16 + "' )");
                }
            }
            msg = (str == "记费成功") ? "计药费成功" : str;
            string str21 = string.Concat(new object[] { "药品数量：", num, " 医嘱组号：", str3, " 用药时间：", s, " 发药人：01198登录人", str5 });
            this.DB.GetPIVAsDB("insert into ToHisChargeLog (begintime,endtime,labelno,msg,ChargeResult,Remark2,[Parameters],HisReturn) values ('" + str12 + "','" + str14 + "','" + LabelNo + "','" + msg + "','" + str13 + "' ,'" + str5 + "','" + str21 + "','" + str2 + "' )");
            return str13;
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
