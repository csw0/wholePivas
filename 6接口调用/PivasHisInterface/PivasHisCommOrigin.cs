using PIVAsCommon;
using System;
using System.Data;
using System.IO;
using System.Xml;
using WebSynInterface.WebReference;

namespace PivasHisInterface
{
    /// <summary>
    /// pivas与his之间的通信原始类
    /// </summary>
    public class PivasHisCommOrigin : AbstractPivasHisComm
    {
        #region 服务没有源码;根据wsdl生成的代理类
        EsbBusService esbBusService = new EsbBusService();
        #endregion

        /// <summary>
        /// 额外方法1
        /// </summary>
        /// <param name="string1">处方id</param>
        /// <param name="string2">病人id</param>
        /// <param name="string3">备用参数</param>
        /// <param name="string4">备用参数</param>
        /// <param name="string5">备用参数</param>
        /// <param name="string6">备用参数</param>
        public void Extra1(string string1, string string2, string string3, string string4, string string5, string string6)
        {

        }

        /// <summary>
        /// 瓶签查询退药方法
        /// </summary>
        /// <param name="LabelList">瓶签号数组</param>
        /// <param name="EmployeeID">退药人</param>
        /// <returns>0 失败 1成功</returns>
        public int returnCharge(string[] LabelList, string EmployeeID)
        {
            try
            {
                for (int i = 0; i < LabelList.Length; i++)
                {
                    string sql = " update IVRecord set WardRetreat=2 ,WardRID='" + EmployeeID + "',WardRtime=Getdate() ";
                    sql = sql + "where LabelNo='" + LabelList[i] + "' ";
                    dbHelp.SetPIVAsDB(sql);
                }
                return 1;
            }
            catch
            {
                return 0;
            }

        }

        /// <summary>
        /// 同步基础数据
        /// </summary>
        /// <param name="SynCode">SynCode</param>
        /// <returns></returns>
        public DataTable Syn(string SynCode)
        {
            DataTable DT = new DataTable();
            try
            {
                switch (SynCode)
                {
                    case "0":
                        DT = null;
                        break;
                    case "1":
                        DT = SynDrugict();
                        break;
                    case "2":
                        DT = SynWard();
                        break;
                    case "3":
                        DT = SynEmployee();
                        break;
                    case "4":
                        DT = SynDosunit();
                        break;
                    case "5":
                        DT = SynFreq();
                        break;
                    case "6":
                        DT = SynPatient();
                        break;
                    case "7":
                        DT = SynPre();
                        break;
                    case "8":
                        DT = SynDrugList();
                        break;
                    default:
                        DT = null;
                        break;
                };

                return DT;
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error(String.Format("{0}执行WEBSERVICE同步时出错:{1}",SynCode, ex.Message));
            }
            return null;
        }

        /// <summary>
        /// 同步药单
        /// </summary>
        /// <returns></returns>
        private DataTable SynDrugList()
        {
            try
            {
                DateTime now = DateTime.Now;
                DateTime yesterday = now.AddDays(-1);
                string dateinstart = yesterday.ToString("yyyy-MM-dd");
                string dateinend = DateTime.Now.ToString("yyyy-MM-dd");
                ////定义查询药单入参的开始结束时间。his定义RQ1是开始时间，RQ2是结束时间。查询在
                ////两个时间之间护士提交的药单，是以护士提交的操作时间查询。所以查询昨天和今天两天的就足够了。
                ////即昨天和今天两天护士提交的药单，也就是今天明天两天要使用的药单。

                string x = "<body><head><userid>JPZX</userid><password>123</password><trans_no>C07_01_00_01</trans_no></head>";
                x = x + "<resquest><RQ1>" + dateinstart + "</RQ1><RQ2>" + dateinend + "</RQ2><TXM>-1</TXM><C07_01_00_01></C07_01_00_01></resquest></body>";
                ///定义入参x，手动写包。以下每个方法中都是如此，参数详情请见接口文档。


                string s = esbBusService.Process(x);
                StringReader read = new StringReader(s);
                DataSet DS = new DataSet();
                DS.ReadXml(read);
                DataTable t = DS.Tables[1];
                //将xml读取为datetabel

                DataTable DT = new DataTable();
                DT.Columns.Add("GroupNo");
                DT.Columns.Add("RecipeID");
                DT.Columns.Add("Schedule");
                DT.Columns.Add("DrugListID");
                DT.Columns.Add("DrugFreq");
                DT.Columns.Add("DrugCode");
                DT.Columns.Add("DrugName");
                DT.Columns.Add("OccDT");
                DT.Columns.Add("InsertDT");
                DT.Columns.Add("Quantity");
                DT.Columns.Add("OLDDLGROUP");
                DT.Columns.Add("OLDPGROUP");
                DT.Columns.Add("REMARK1");
                DT.Columns.Add("REMARK2");
                DT.Columns.Add("OLDDRUGLISTID");
                DT.Columns.Add("QuantityUnit");


                DataRow R;
                for (int i = 0; i < t.Rows.Count; i++)
                {
                    R = DT.NewRow();

                    R["GroupNo"] = t.Rows[i]["GROUPCODE"].ToString();
                    R["RecipeID"] = t.Rows[i]["RECIPEID"].ToString();
                    R["Schedule"] = t.Rows[i]["SCHEDULE"].ToString();
                    R["DrugListID"] = t.Rows[i]["LABELNO"].ToString();
                    R["DrugFreq"] = null;
                    R["DrugCode"] = null;
                    R["DrugName"] = null;
                    R["OccDT"] = t.Rows[i]["OCCDT"].ToString();
                    R["Quantity"] = null;
                    R["QuantityUnit"] = null;
                    R["InsertDT"] = DateTime.Now.ToString();
                    R["OLDDLGROUP"] = null;
                    R["OLDPGROUP"] = null;
                    R["REMARK1"] = null;
                    R["REMARK2"] = null;
                    R["OLDDRUGLISTID"] = null;
                    R["QuantityUnit"] = null;

                    DT.Rows.Add(R);
                }

                DS.Dispose();
                t.Dispose();
                //return DS.Tables[0];
                return DT;

            }
            catch (System.Exception ex)
            {
                InternalLogger.Log.Error(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 同步处方
        /// </summary>
        /// <returns></returns>
        private DataTable SynPre()
        {
            try
            {
                string x = "<body><head><userid>JPZX</userid><password>123</password><trans_no>C02_02_00_01</trans_no></head>";
                x = x + "<resquest><C02_02_00_01></C02_02_00_01></resquest></body>";

                string s = esbBusService.Process(x);
                StringReader read = new StringReader(s);
                DataSet DS = new DataSet();
                DS.ReadXml(read);
                DataTable t = DS.Tables[1];

                DataTable DT = new DataTable();
                DT.Columns.Add("GroupNo");
                DT.Columns.Add("RecipeNo");
                DT.Columns.Add("WardCode");
                DT.Columns.Add("DoctorCode");
                DT.Columns.Add("DrawerCode");
                DT.Columns.Add("PatientCode");
                DT.Columns.Add("CaseID");
                DT.Columns.Add("BedNo");
                DT.Columns.Add("UsageCode");
                DT.Columns.Add("UsageName");
                DT.Columns.Add("FreqCode");
                DT.Columns.Add("DrugCode");
                DT.Columns.Add("DrugName");
                DT.Columns.Add("Spec");
                DT.Columns.Add("Dosage");
                DT.Columns.Add("DosageUnit");
                DT.Columns.Add("Quantity");
                DT.Columns.Add("QuantityUnit");
                DT.Columns.Add("InceptDT");
                DT.Columns.Add("InsertDT");
                DT.Columns.Add("StartDT");
                DT.Columns.Add("EndDT");
                DT.Columns.Add("TPN");
                DT.Columns.Add("PDStatus");
                DT.Columns.Add("PPause");
                DT.Columns.Add("Remark1");
                DT.Columns.Add("Remark2");
                DT.Columns.Add("Remark3");
                DT.Columns.Add("Remark4");
                DT.Columns.Add("Remark5");
                DT.Columns.Add("Remark6");
                DT.Columns.Add("Remark7");
                DT.Columns.Add("Remark8");
                DT.Columns.Add("Remark9");
                DT.Columns.Add("Remark10");
                DT.Columns.Add("OldGroup");
                DT.Columns.Add("OldRecipe");
                DT.Columns.Add("OldStop");

                DataRow R;

                for (int i = 0; i < t.Rows.Count; i++)
                {
                    R = DT.NewRow();

                    R["RecipeNo"] = t.Rows[i]["RECIPENO"].ToString();
                    R["GroupNo"] = t.Rows[i]["GROUPCODE"].ToString();
                    R["WardCode"] = t.Rows[i]["WARDCODE"].ToString();
                    R["DoctorCode"] = t.Rows[i]["DOCTORCODE"].ToString();
                    R["DrawerCode"] = t.Rows[i]["DRAWERCODE"].ToString();
                    R["PatientCode"] = t.Rows[i]["PATIENTCODE"].ToString();
                    R["CaseID"] = t.Rows[i]["CASEID"].ToString();
                    R["BedNo"] = t.Rows[i]["BEDNO"].ToString();
                    R["UsageCode"] = t.Rows[i]["FREQCODE"].ToString();
                    R["UsageName"] = t.Rows[i]["FREQNAME"].ToString();
                    R["FreqCode"] = t.Rows[i]["USAGECODE"].ToString();
                    R["DrugCode"] = t.Rows[i]["DRUGCODE"].ToString();
                    R["DrugName"] = t.Rows[i]["DRUGNAME"].ToString();
                    R["Spec"] = t.Rows[i]["SPEC"].ToString();
                    R["Dosage"] = t.Rows[i]["DOSAGE"].ToString();
                    R["DosageUnit"] = t.Rows[i]["DOSAGEUNIT"].ToString();
                    R["Quantity"] = t.Rows[i]["QUANTITY"].ToString();
                    R["QuantityUnit"] = t.Rows[i]["QUANTITYUNIT"].ToString();
                    R["InceptDT"] = DateTime.Now.ToString();
                    R["StartDT"] = t.Rows[i]["STARTDT"].ToString();
                    R["StartDT"] = t.Rows[i]["STARTDT"].ToString();
                    R["EndDT"] = t.Rows[i]["ENDDT"].ToString();
                    R["TPN"] = t.Rows[i]["TPN"].ToString();
                    R["PPause"] = "0";
                    R["PDStatus"] = null;

                    R["Remark1"] = t.Rows[i]["REMARK"].ToString();
                    R["Remark2"] = null;
                    R["Remark3"] = "未记账";
                    R["Remark4"] = null;
                    R["Remark5"] = null;
                    R["Remark6"] = null;
                    R["Remark7"] = null;
                    R["Remark8"] = null;
                    R["Remark9"] = null;
                    R["Remark10"] = null;
                    R["OldGroup"] = null;
                    R["OldRecipe"] = null;
                    R["OldStop"] = null;

                    DT.Rows.Add(R);
                }

                DS.Dispose();
                t.Dispose();
                //return DS.Tables[0];
                return DT;
            }
            catch (System.Exception ex)
            {
                InternalLogger.Log.Error(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 同步病区
        /// </summary>
        /// <returns></returns>
        private DataTable SynWard()
        {
            try
            {
                string x = "<body><head><userid>JPZX</userid><password>123</password><trans_no>D13_00_00_00</trans_no></head>";
                x = x + "<resquest><D13_00_00_00></D13_00_00_00></resquest></body>";

                string s = esbBusService.Process(x);

                StringReader read = new StringReader(s);
                DataSet DS = new DataSet();
                DS.ReadXml(read);
                DataTable t = DS.Tables[1];

                DataTable DT = new DataTable();
                DT.Columns.Add("WardCode");
                DT.Columns.Add("WardName");
                DT.Columns.Add("WardSimName");
                DT.Columns.Add("WardSeqNo");
                DT.Columns.Add("WardArea");
                DT.Columns.Add("IsOpen");
                DT.Columns.Add("Spellcode");

                DataRow R;
                for (int i = 0; i < t.Rows.Count; i++)
                {

                    R = DT.NewRow();
                    R["WardCode"] = t.Rows[i]["WARDCODE"].ToString();
                    R["WardName"] = t.Rows[i]["WARDNAME"].ToString();

                    R["WardSimName"] = null;
                    R["WardSeqNo"] = null;
                    R["WardArea"] = null;
                    R["IsOpen"] = null;
                    R["Spellcode"] = null;
                    DT.Rows.Add(R);

                }

                //DS.Dispose();
                t.Dispose();
                //return DS.Tables[0];
                return DT;
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 同步病人
        /// </summary>
        /// <returns></returns>
        private DataTable SynPatient()
        {
            try
            {
                string x = "<body><head><userid>JPZX</userid><password>123</password><trans_no>C02_06_00_00</trans_no></head>";
                x = x + "<resquest><C02_06_00_00></C02_06_00_00></resquest></body>";

                string s = esbBusService.Process(x);
                StringReader read = new StringReader(s);
                DataSet DS = new DataSet();
                DS.ReadXml(read);
                DataTable t = DS.Tables[1];

                DataTable DT = new DataTable();
                DT.Columns.Add("PatCode");
                DT.Columns.Add("PatName");
                DT.Columns.Add("Sex");
                DT.Columns.Add("Birthday");
                DT.Columns.Add("Height");
                DT.Columns.Add("BedNo");
                DT.Columns.Add("WardCode");
                DT.Columns.Add("Weight");
                DT.Columns.Add("PatStatus");
                DT.Columns.Add("Age");
                DT.Columns.Add("AgeSTR");
                DT.Columns.Add("UPdateStatu");
                DataRow R;

                for (int i = 0; i < t.Rows.Count; i++)
                {
                    R = DT.NewRow();

                    R["PatCode"] = t.Rows[i]["PATCODE"].ToString();
                    R["PatName"] = t.Rows[i]["PATNAME"].ToString();
                    R["SEX"] = t.Rows[i]["SEX"].ToString();
                    R["Birthday"] = t.Rows[i]["BIRTHDAY"].ToString();
                    R["Height"] = t.Rows[i]["HEIGHT"].ToString(); ;
                    R["BedNo"] = t.Rows[i]["BEDNXBO"].ToString();
                    R["WardCode"] = t.Rows[i]["WARDCODE"].ToString();
                    R["Weight"] = t.Rows[i]["WEIGHT"].ToString(); ;
                    R["PatStatus"] = null;
                    R["Age"] = null;
                    R["AgeSTR"] = null;
                    R["UPdateStatu"] = null;

                    DT.Rows.Add(R);
                }

                DS.Dispose();
                t.Dispose();
                //return DS.Tables[0];
                return DT;
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 同步员工
        /// </summary>
        /// <returns></returns>
        private DataTable SynEmployee()
        {
            try
            {
                string x = "<body><head><userid>JPZX</userid><password>123</password><trans_no>D14_00_00_00</trans_no></head>";
                x = x + "<resquest><D14_00_00_00></D14_00_00_00></resquest></body>";
                ///定义入参x，手动写包。以下每个方法中都是如此，参数详情请见接口文档。
                string s = esbBusService.Process(x);

                StringReader read = new StringReader(s);
                DataSet DS = new DataSet();
                DS.ReadXml(read);
                DataTable t = DS.Tables[1];

                DataTable DT = new DataTable();
                DT.Columns.Add("AccountID");
                DT.Columns.Add("Pas");
                DT.Columns.Add("DEmployeeCode");
                DT.Columns.Add("Position");
                DT.Columns.Add("DEmployeeName");
                DT.Columns.Add("IsValid");
                DT.Columns.Add("Type");

                DataRow R;
                for (int i = 0; i < t.Rows.Count; i++)
                {
                    R = DT.NewRow();

                    R["AccountID"] = t.Rows[i]["DEMPLOYEECODE"].ToString();
                    R["Pas"] = "";
                    R["DEmployeeCode"] = t.Rows[i]["DEMPLOYEECODE"].ToString();
                    R["Position"] = "";
                    R["DEmployeeName"] = t.Rows[i]["DEMPLOYEENAME"].ToString();
                    R["IsValid"] = null;
                    R["Type"] = null;
                    DT.Rows.Add(R);
                }


                DS.Dispose();
                t.Dispose();
                //return DS.Tables[0];
                return DT;
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 同步剂量单位
        /// </summary>
        /// <returns></returns>
        private DataTable SynDosunit()
        {
            try
            {
                string x = "<body><head><userid>JPZX</userid><password>123</password><trans_no>D14_00_00_00</trans_no></head>";
                x = x + "<resquest><D14_00_00_00></D14_00_00_00></resquest></body>";

                string s = esbBusService.Process(x);
                StringReader read = new StringReader(s);
                DataSet DS = new DataSet();
                DS.ReadXml(read);
                DataTable t = DS.Tables[0];

                DataTable DT = new DataTable();
                DT.Columns.Add("MetricCode");
                DT.Columns.Add("MetricName");
                DT.Columns.Add("UnitID");

                DataRow R;
                for (int i = 0; i < t.Rows.Count; i++)
                {
                    R = DT.NewRow();
                    R["MetricCode"] = t.Rows[i]["dcode"].ToString();
                    R["MetricName"] = t.Rows[i]["dname"].ToString();
                    R["UnitID"] = t.Rows[i]["dosunit"].ToString();

                    DT.Rows.Add(R);
                }

                DS.Dispose();
                t.Dispose();
                //return DS.Tables[0];
                return DT;
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 同步药品目录
        /// </summary>
        /// <returns></returns>
        private DataTable SynDrugict()
        {
            try
            {
                string x = "<body><head><userid>JPZX</userid><password>123</password><trans_no>D06_00_00_01</trans_no></head>";
                x = x + "<resquest><D06_00_00_01></D06_00_00_01></resquest></body>";

                string s = esbBusService.Process(x);
                StringReader read = new StringReader(s);
                DataSet DS = new DataSet();
                DS.ReadXml(read);
                DataTable t = DS.Tables[0];

                DataTable DT = new DataTable();
                DT.Columns.Add("DrugCode");
                DT.Columns.Add("DrugName");
                DT.Columns.Add("DrugNameJC");
                DT.Columns.Add("Spec");
                DT.Columns.Add("Dosage");
                DT.Columns.Add("DosageUnit");
                DT.Columns.Add("Major");
                DT.Columns.Add("MajorUnit");
                DT.Columns.Add("Capacity");
                DT.Columns.Add("CapacityUnit");
                DT.Columns.Add("Form");

                DT.Columns.Add("FormUnit");
                DT.Columns.Add("Conversion");
                DT.Columns.Add("SpellCode");

                DT.Columns.Add("UniPreparationID");
                DT.Columns.Add("IsMenstruum");
                DT.Columns.Add("withmenstruum");
                DT.Columns.Add("PreConfigure");
                DT.Columns.Add("PiShi");
                DT.Columns.Add("NotCompound");
                DT.Columns.Add("Precious");
                DT.Columns.Add("AsMajorDrug");
                DT.Columns.Add("BigUnit");
                DT.Columns.Add("PortNo");
                DT.Columns.Add("Symbol");
                DT.Columns.Add("IsValid");
                DT.Columns.Add("Difficulty");
                DT.Columns.Add("Species");
                DT.Columns.Add("Positions1");
                DT.Columns.Add("Positions2");
                DT.Columns.Add("NoName");
                DT.Columns.Add("ProductName");

                DataRow R;

                for (int i = 0; i < t.Rows.Count; i++)
                {
                    R = DT.NewRow();
                    R["DrugCode"] = t.Rows[i]["DrugCode"].ToString();
                    R["DrugName"] = t.Rows[i]["DrugName"].ToString();
                    R["DrugNameJC"] = t.Rows[i]["DrugName"].ToString();
                    R["Spec"] = t.Rows[i]["spec"].ToString();
                    R["Dosage"] = t.Rows[i]["Dosage"].ToString();
                    R["DosageUnit"] = t.Rows[i]["DosageUnit"].ToString();
                    R["Major"] = null;
                    R["MajorUnit"] = null;
                    R["Capacity"] = null;
                    R["CapacityUnit"] = null;
                    R["Form"] = null;
                    R["FormUnit"] = t.Rows[i]["formu"].ToString();
                    R["Conversion"] = null;
                    R["SpellCode"] = t.Rows[i]["SpellCode"].ToString();
                    R["IsMenstruum"] = null;
                    R["withmenstruum"] = null;
                    R["PreConfigure"] = null;
                    R["PiShi"] = null;
                    R["NotCompound"] = null;
                    R["Precious"] = null;
                    R["AsMajorDrug"] = null;
                    R["BigUnit"] = null;
                    R["PortNo"] = t.Rows[i]["PortNo"].ToString();
                    R["Symbol"] = null;
                    R["IsValid"] = null;
                    R["Difficulty"] = null;
                    R["Species"] = null;
                    R["Positions1"] = null;
                    R["Positions2"] = null;
                    R["NoName"] = null;
                    R["ProductName"] = null;

                    DT.Rows.Add(R);
                }

                DS.Dispose();
                t.Dispose();
                //return DS.Tables[0];
                return DT;
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 同步频次
        /// </summary>
        /// <returns></returns>
        private DataTable SynFreq()
        {
            try
            {
                string x = "<body><head><userid>JPZX</userid><password>123</password><trans_no>D15_00_00_00</trans_no></head>";
                x = x + "<resquest><D15_00_00_00></D15_00_00_00></resquest></body>";

                string s = esbBusService.Process(x);
                StringReader read = new StringReader(s);
                DataSet DS = new DataSet();
                DS.ReadXml(read);
                DataTable t = DS.Tables[1];

                DataTable DT = new DataTable();
                DT.Columns.Add("FreqCode");
                DT.Columns.Add("FreqName");
                DT.Columns.Add("IntervalDay");
                DT.Columns.Add("TimesOfDay");
                DT.Columns.Add("UseTime");
                DataRow R;

                for (int i = 0; i < t.Rows.Count; i++)
                {
                    R = DT.NewRow();

                    R["FreqCode"] = t.Rows[i]["FREQCODE"].ToString();
                    R["FreqName"] = t.Rows[i]["FREQNAME"].ToString();
                    R["TimesOfDay"] = "1";
                    R["IntervalDay"] = "1";
                    R["UseTime"] = null;

                    DT.Rows.Add(R);
                }

                DS.Dispose();
                t.Dispose();
                //return DS.Tables[0];
                return DT;
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error(ex.Message);
                return null;
            }
        }

        public DataTable GetDataTable(XmlElement doc)
        {
            XmlNodeList xlist = doc.SelectNodes("//table");
            DataTable Dt = new DataTable();
            DataRow Dr;

            for (int i = 0; i < xlist.Count; i++)
            {
                Dr = Dt.NewRow();
                XmlElement xe = (XmlElement)xlist.Item(i);
                for (int j = 0; j < xe.Attributes.Count; j++)
                {
                    if (!Dt.Columns.Contains("@" + xe.Attributes[j].Name))
                        Dt.Columns.Add("@" + xe.Attributes[j].Name);
                    Dr["@" + xe.Attributes[j].Name] = xe.Attributes[j].Value;
                }
                for (int j = 0; j < xe.ChildNodes.Count; j++)
                {
                    if (!Dt.Columns.Contains(xe.ChildNodes.Item(j).Name))
                        Dt.Columns.Add(xe.ChildNodes.Item(j).Name);
                    Dr[xe.ChildNodes.Item(j).Name] = xe.ChildNodes.Item(j).InnerText;
                }
                Dt.Rows.Add(Dr);
            }
            return Dt;
        }
    }
}
