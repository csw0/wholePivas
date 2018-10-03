using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;

namespace SDFYDataImport
{
    class DataSyn
    {
        //在院患者 
        public const string SEL_ACTTPN_PATIENT = "SELECT p.PatientCode, p.DeptCode, d.DeptName, p.BedNo, " +
            "p.HospitalNo, p.PatientName FROM hospitaldata.dbo.Patient p (NOLOCK) " +
            "LEFT JOIN hospitaldata.dbo.DDept d ON p.DeptCode=d.DeptCode WHERE p.IsInHospital=1 " +
            "AND EXISTS(SELECT 1 FROM hospitaldata.dbo.Orders r (NOLOCK) " +
               " WHERE r.PatientCode=p.PatientCode AND r.OrdersLabel=4" +
               " AND (r.StopTime IS NULL OR r.StopTime>=CONVERT(VARCHAR(10), GETDATE(), 120)) AND r.NowStatus>=0 " +
               ")  ";

        public const string SEL_ACTIVE_TPNPATIENT = "SELECT p.PatientCode, p.HospitalNo, " +
            "(SELECT MAX(cr.CheckDate) FROM LISData.dbo.CheckRecord cr (NOLOCK) WHERE cr.PatientCode=p.PatientCode) PrevDate " +
            "FROM hospitaldata.dbo.Patient p (NOLOCK) WHERE p.IsInHospital=1 " +
            "AND EXISTS(SELECT 1 FROM hospitaldata.dbo.Orders r (NOLOCK) " +
               " WHERE r.PatientCode=p.PatientCode AND r.OrdersLabel=4" +
               " AND (r.StopTime IS NULL OR r.StopTime>=CONVERT(VARCHAR(10), GETDATE(), 120)) AND r.NowStatus>=0 " +
               ")  ";

        private const string SEL_LIS_URL = "SELECT ConfigItemContent FROM InterfaceConfigItem " +
            "WHERE SystemID='LAENNEC' AND ConfigClassCode='interface_server' AND ConfigItemCode='lis_server'";

        //检查记录
        private const string ADD_CHKRECORD = "IF NOT EXISTS(SELECT 1 FROM lisdata.dbo.CheckRecord (NOLOCK)" +
            "WHERE CheckDate=CONVERT(VARCHAR(10), '{6}', 120) AND  PatientCode='{0}' AND CheckRecordNo='{2}') " +
            " INSERT INTO lisdata.dbo.CheckRecord(PatientCode, CheckType, CheckRecordNo, CheckName, " +
            " CheckTime, CheckerCode, CheckDate, RelevantClinicDiag, CheckSpecimen, InceptDT) " +
            " VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', CONVERT(VARCHAR(10), '{6}', 120), '{7}', '{8}', " +
            "  GETDATE()) ";
        private const string ADD_LISCHECK1= "IF NOT EXISTS(SELECT 1 FROM lisdata.dbo.LisCheckResult (NOLOCK)" +
            "WHERE CheckDate=CONVERT(VARCHAR(10), '{6}', 120) AND CheckRecordNo='{0}' AND CheckItemCode='{2}') " +
            " INSERT INTO lisdata.dbo.LisCheckResult(CheckRecordNo, CheckResultCode, CheckItemCode, "+
            "  CheckItemName, ResultValue, ResultUnit, CheckDate, InceptDT) " +
            " VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', CONVERT(VARCHAR(10), '{6}', 120), GETDATE());";
        private const string INST_LISCHECK = "INSERT INTO lisdata.dbo.TempLisCheckResult(CheckRecordNo, "+
            "CheckResultCode, CheckItemCode, CheckItemName, ResultValue, ResultUnit, Domain, ValueDrect, " +
            "CheckDate, InceptDT) ";
        private const string VAL_LISCHECK = "SELECT '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', " +
            "CONVERT(VARCHAR(10), '{8}', 120), GETDATE() ";
        private const string UP_LISCHECK = " INSERT INTO lisdata.dbo.LisCheckResult(CheckRecordNo, CheckResultCode, " +
            "CheckItemCode, CheckItemName, ResultValue, ResultUnit, Domain, ValueDrect, CheckDate, InceptDT) " +
            "SELECT CheckRecordNo, CheckResultCode, CheckItemCode, CheckItemName, ResultValue, ResultUnit," +
            "Domain, ValueDrect, CheckDate, InceptDT FROM lisdata.dbo.TempLisCheckResult tmp " +
            "WHERE NOT EXISTS(SELECT 1 FROM lisdata.dbo.LisCheckResult lc (NOLOCK) WHERE lc.CheckDate=tmp.CheckDate " +
            " AND lc.CheckRecordNo=tmp.CheckRecordNo AND lc.CheckItemCode=tmp.CheckItemCode)";

        private string LisURL = "http://10.1.1.71/webapp/Service.asmx";

        private string appPath = "";
        private string logPath = "";
        private bool isStop = false;
        private bool isDebug = true;
        private BLPublic.DBOperate db = null;
        private BLPublic.LogOperate logOp = null;

        public DataSyn(string _appPath)
        {
            this.appPath = _appPath;
            this.logPath = (new DirectoryInfo(this.appPath)).Parent.FullName + @"\InterfaceDataLog\检查\";
            this.logOp = new BLPublic.LogOperate(this.appPath + @"\log\", "SDFYSyn");

            this.db = new BLPublic.DBOperate(Path.Combine(this.appPath, "bl_server.lcf"));
            if (!this.db.IsConnected)
                this.logOp.log("连接服务器失败:" + this.db.Error); 
        }
         
        /// <summary>
        /// 同步进程事件
        /// </summary>
        public Action<int, int> OnProcess { get; set; }
        public Action<string> OnMessage { get; set; }

        /// <summary>
        /// 取消同步
        /// </summary>
        public void cancel()
        {
            this.isStop = true;
        }

        public void close()
        {
            if (null != this.db)
                this.db.Close();
        }

        /// <summary>
        /// 同步TPN患者实验室检查
        /// </summary>
        public void synTPNPatientLab(bool _synByTime=true)
        {    
            this.logOp.log("同步实验室检查开始.");
             
            IDataReader dr = null;
            if (!this.db.GetRecordSet(SEL_LIS_URL, ref dr))
            {
                this.logOp.log("LIS服务器设置失败:" + this.db.Error);
                return;
            }

            string set = "";
            if (!dr.Read() || dr.IsDBNull(0))
            {
                this.logOp.log("没有LIS服务器设置");
                dr.Close();
                return;
            }
            set = dr.GetString(0);
            dr.Close();
             
            string[] info = set.Split('\n');
            foreach(string item in info)
            {
                if (item.Contains("server_address="))
                {
                    this.LisURL = item.Substring(15);
                    break;
                }
            }

            DataTable tblPatient = null;
            if (!this.db.GetRecordSet(SEL_ACTIVE_TPNPATIENT, ref tblPatient))
            {
                this.logOp.log("加载TPN患者失败:" + this.db.Error);
                return;
            }
              
            int num = 0, total = 0;
            DateTime prevDate;

            if (!Directory.Exists(this.logPath))
                Directory.CreateDirectory(this.logPath);
             
            this.isStop = false;
            total = tblPatient.Rows.Count;
            foreach (DataRow row in tblPatient.Rows)
            {
                num++;
                if (null != this.OnProcess)
                    this.OnProcess(num, total);
                  
                //getLabOld(row["HospitalNo"].ToString());

                if (!_synByTime) 
                    prevDate = DateTime.MinValue; //不按时间同步

                else if (row.IsNull("PrevDate"))
                    prevDate = DateTime.MinValue;

                else 
                    prevDate = Convert.ToDateTime(row["PrevDate"].ToString()).Date;

                try
                {
                    getLabReport(row["HospitalNo"].ToString(), prevDate);
                }
                catch (Exception ex)
                {
                    this.logOp.log("获取患者(" + row["HospitalNo"].ToString() + " - " + prevDate + ")检查单失败:" + ex.Message);
                } 

                if (this.isStop)
                    break; 
            }

            
            this.logOp.log("同步实验室检查结束.");
        }

        /// <summary>
        /// 同步病人检查
        /// </summary>
        /// <param name="_hspNo"></param>
        /// <param name="_startDate"></param>
        public void synPatient(string _hspNo, DateTime _startDate)
        {
            try
            {
                getLabReport(_hspNo, _startDate);
            }
            catch (Exception ex)
            {
                if (null != this.OnMessage)
                    this.OnMessage("获取患者检查单失败:" + ex.Message);
            } 
        }

        /// <summary>
        /// 老检查系统接口
        /// </summary>
        /// <param name="_hspNo"></param>
        private void getLabOld(string _hspNo)
        {  
            string param = "";
            object result = "";
            MemoryStream ms = null; //1mb
            byte[] buff = null;

            param = string.Format("<Request><Area>1</Area><PatientType>2</PatientType><PatientId>{0}</PatientId></Request>", _hspNo);
            result = WebServiceHelper.InvokeWebService(this.LisURL, "GetLabResult", new object[] { param }).ToString();
            if (null == result)
                this.logOp.log("加载患者(" + _hspNo + ")实验室检查失败.");

            else
            {
                ms = new MemoryStream(1024 * 1000);

                buff = Encoding.UTF8.GetBytes("<?xml version=\"1.0\" encoding=\"utf-8\" ?><!DOCTYPE ROWSET [<!ENTITY div \"&#47;\">]>");
                ms.Write(buff, 0, buff.Length);

                buff = Encoding.UTF8.GetBytes(result.ToString());
                ms.Write(buff, 0, buff.Length);

                ms.Position = 0;
                FileStream fs = new FileStream(this.logPath + "\\LAB." + _hspNo + ".xml", FileMode.OpenOrCreate);
                fs.Position = 0;
                BinaryWriter w = new BinaryWriter(fs);
                w.Write(ms.ToArray());
                fs.Close();

                try
                {
                    this.db.ExecSQL("TRUNCATE TABLE TempLisCheckResult");
                    dealLabXmlOld(ms, _hspNo);

                    if (!this.db.ExecSQL(UP_LISCHECK))
                        this.logOp.log("更新(" + _hspNo + ")实验室数据失败:" + this.db.Error);
                }
                catch (Exception ex)
                {
                    this.logOp.log("解析(" + _hspNo + ")实验室数据失败:" + ex.Message);
                }

                ms.Close();
            }
        }

        /// <summary>
        /// 获取值列表里的值
        /// </summary>
        /// <param name="_dic"></param>
        /// <param name="_key"></param>
        /// <returns></returns>
        private string getDicValue(IDictionary<string, string> _dic, string _key)
        {
            if (_dic.ContainsKey(_key))
                return _dic[_key];
            else
                return "";
        }
         
        /// <summary>
        /// 获取患者检查报告单
        /// </summary>
        /// <param name="_hspNo"></param>
        /// <param name="_prevDate">上次报告日期(yyyyMMdd)</param> 
        /// <returns></returns>
        private void getLabReport(string _hspNo, DateTime _prevDate)
        {
            //返回报告单列表<报告单号>
            List<string> lstTestNo = new List<string>(32); 

            object result = WebServiceHelper.InvokeWebService(this.LisURL, "GetLabMaster", new object[] { _hspNo });

            if (null == result)
                this.logOp.log("加载患者(" + _hspNo + ")检查单失败.");

            else if (result is string[]) 
                try
                {
                    dealReport((string[])result, _hspNo, _prevDate, ref lstTestNo); 
                }
                catch (Exception ex)
                {
                    this.logOp.log("解析患者(" + _hspNo + ")检查单失败:" + ex.Message);
                }
             
            this.db.ExecSQL("TRUNCATE TABLE lisdata.dbo.TempLisCheckResult");
            foreach (string testNo in lstTestNo)
            {
                result = WebServiceHelper.InvokeWebService(this.LisURL, "GetLabResult",
                            new object[] { testNo });

                if (null == result)
                    this.logOp.log("加载检查单(" + testNo + ")内容失败.");

                else if (result is string[])  
                    try
                    {
                        dealReportDetail((string[])result, _hspNo, testNo);
                    }
                    catch (Exception ex)
                    {
                        this.logOp.log("解析检查单(" + testNo + ")内容失败:" + ex.Message);
                    }  
            }

            if (!this.db.ExecSQL(UP_LISCHECK))
                this.logOp.log("更新(" + _hspNo + ")检查单内容失败:" + this.db.Error);
        } 

        /// <summary>
        /// 获取检查单
        /// </summary>
        /// <param name="_xml"></param>
        /// <param name="_hspNo"></param>
        /// <param name="_prevDate"></param>
        /// <param name="dicNos">检验单号</param>
        private void dealReport(string[] _arrXml, string _hspNo, DateTime _prevDate, ref List<string> lstTestNo)
        {  
            string tagName = ""; 
            string rcdTime = "";
            string rcdDate = "";
            string testNo = ""; 
            bool isRead = false;
            DateTime iNDate;
            Dictionary<string, string> values = new Dictionary<string, string>(32);
            MemoryStream ms = null;

            FileStream fs = new FileStream(Path.Combine(this.logPath, DateTime.Today.ToString("yyyyMMdd") + _hspNo + ".xml"), 
                                           FileMode.OpenOrCreate);
            fs.Position = 0;
            BinaryWriter w = new BinaryWriter(fs);

            foreach (string xml in _arrXml)
            {
                ms = new MemoryStream(1024 * 1000); 
                byte[] buff = Encoding.UTF8.GetBytes(xml);
                ms.Write(buff, 0, buff.Length);
                 
                ms.Position = 0;
                w.Write(ms.ToArray());

                values.Clear();
                isRead = false;
                ms.Position = 0;
                XmlTextReader xmlReader = new XmlTextReader(ms);
                xmlReader.WhitespaceHandling = WhitespaceHandling.All;
                while (xmlReader.Read())
                {
                    if (XmlNodeType.Element == xmlReader.NodeType)
                    {
                        tagName = xmlReader.Name;
                        if ("GetPatientLabTestMaster".Equals(tagName)) 
                            isRead = true; 
                    } 
                    else if (XmlNodeType.Text == xmlReader.NodeType)
                    { 
                        if (isRead)
                            values.Add(tagName, xmlReader.Value);
                    }
                }

                ms.Close();
                xmlReader.Close();

                if (isRead)
                {
                    testNo = getDicValue(values, "TestNo");
                    if (string.IsNullOrWhiteSpace(testNo))
                        continue;

                    rcdTime = getDicValue(values, "ResultsRptDateTime");
                    rcdTime = rcdTime.Replace('/', '-');
                    rcdDate = rcdTime.Substring(0, rcdTime.IndexOf(' '));

                    if (!DateTime.TryParse(rcdDate, out iNDate))
                    {
                        this.logOp.log("保存患者(" + _hspNo + ")实验室单(" + testNo + ")失败:日期格式不正确," + rcdTime);
                        
                    }
                    else if (_prevDate <= iNDate) //未接收日期 
                    {
                        if (2 > testNo.Length)
                            continue;

                        if ("20".Equals(testNo.Substring(0, 2))) //以20XX年份开头的
                            testNo = "1" + testNo.Substring(2);  //1为住院病人 
                         
                        lstTestNo.Add(testNo);

                        if (!this.db.ExecSQL(string.Format(ADD_CHKRECORD, getDicValue(values, "PatientID"), "lischk",
                                                 testNo, getDicValue(values, "TestCause"),
                                                 rcdTime, getDicValue(values, "transcriptionist"), rcdDate,
                                                 getDicValue(values, "RelevantClinicDiag"), getDicValue(values, "Specimen"))))
                        {
                            this.logOp.log("保存患者(" + _hspNo + ")实验室单(" + testNo + ")失败:" + this.db.Error);
                            if (lstTestNo.Contains(testNo))
                                lstTestNo.Remove(testNo);
                        }
                    }
                }
            }

            fs.Close();
        }

        /// <summary>
        /// 获取检查单内容
        /// </summary>
        /// <param name="_xml"></param>
        private void dealReportDetail(string[] _arrXml, string _hspNo, string _testNo)
        {  
            string tagName = "";
            string rcdDate = "";
            string sql = "";
            string temp = ""; 

            bool isRead = false;
            Dictionary<string, string> values = new Dictionary<string, string>(32);
            MemoryStream ms = null;

            FileStream fs = new FileStream(Path.Combine(this.logPath, DateTime.Today.ToString("yyyyMMdd") + _hspNo + _testNo + ".xml"),
                                           FileMode.OpenOrCreate);
            fs.Position = 0;
            BinaryWriter w = new BinaryWriter(fs);

            foreach (string xml in _arrXml)
            {
                ms = new MemoryStream(1024 * 1000);
                byte[] buff = Encoding.UTF8.GetBytes(xml);
                ms.Write(buff, 0, buff.Length);

                ms.Position = 0;
                w.Write(ms.ToArray());

                values.Clear();
                isRead = false;
                ms.Position = 0;
                XmlTextReader xmlReader = new XmlTextReader(ms);
                xmlReader.WhitespaceHandling = WhitespaceHandling.All;
                while (xmlReader.Read())
                {
                    if (XmlNodeType.Element == xmlReader.NodeType)
                    {
                        tagName = xmlReader.Name;
                        if ("GetPatientLabTestMaster".Equals(tagName))
                            isRead = true;
                    } 
                    else if (XmlNodeType.Text == xmlReader.NodeType)
                    { 
                        if (isRead)
                            values.Add(tagName, xmlReader.Value);
                    }
                }

                ms.Close();
                xmlReader.Close();

                if (!string.IsNullOrWhiteSpace(sql))
                    sql += " UNION ALL ";

                temp = getDicValue(values, "AbnormalIndicator");
                if ("L".Equals(temp))
                    temp = "↓";
                else if ("H".Equals(temp))
                    temp = "↑";
                else
                    temp = "";

                rcdDate = getDicValue(values, "ResultDateTime");
                if (string.IsNullOrWhiteSpace(rcdDate))
                {
                    rcdDate = getDicValue(values, "NDate"); 
                    rcdDate = rcdDate.Insert(6, "-");
                    rcdDate = rcdDate.Insert(4, "-");
                }
                else
                {
                    if (0 < rcdDate.IndexOf(' '))
                        rcdDate = rcdDate.Substring(0, rcdDate.IndexOf(' '));

                    rcdDate = rcdDate.Replace('/', '-');
                }

                sql += string.Format(VAL_LISCHECK, _testNo, "", 
                        getDicValue(values, "ReportItemName"), getDicValue(values, "ReportItemName"), 
                        getDicValue(values, "Result"), getDicValue(values, "Units"), 
                        getDicValue(values, "ReferenceResult"), temp, rcdDate);

            }
            fs.Close();

            if (!string.IsNullOrWhiteSpace(sql))
                if (!this.db.ExecSQL(INST_LISCHECK + sql))
                {
                    this.logOp.log("保存检查单(" + _testNo + ")数据失败:" + this.db.Error);
                    if (this.isDebug)
                        this.logOp.log(sql); 
                }
        }

        /// <summary>
        /// 老接口解析
        /// </summary>
        /// <param name="_xml"></param>
        /// <param name="_hspNo"></param>
        private void dealLabXmlOld(Stream _xml, string _hspNo)
        {
            if ((null == _xml) || (0 == _xml.Length))
                return;

            bool isFail = false;
            bool isSave = true;
            bool isFirstItem = true;
            string tagName = "";
            string rcdNo = "";
            string rcdTime = "";
            string rcdDate = ""; //rcdTime里时间格式可能不正确，这里只有日期部分
            string sql = "";
            string temp = "";
            Dictionary<string, string> values = new Dictionary<string, string>(32);
            IDataReader dr = null;

            _xml.Position = 0;
            XmlTextReader xmlReader = new XmlTextReader(_xml);
            xmlReader.WhitespaceHandling = WhitespaceHandling.All;
            while (xmlReader.Read())
            {
                if (XmlNodeType.Element == xmlReader.NodeType)
                {
                    tagName = xmlReader.Name;
                    if ("LabList".Equals(tagName))
                    { 
                        values.Clear();
                        isFirstItem = true;
                        isSave = true;
                        sql = "";
                    }
                    else if ("Items".Equals(tagName))
                    { 
                        if (isFirstItem)
                        {
                            rcdNo = getDicValue(values, "TestNo");
                            rcdTime = getDicValue(values, "ResultsRptDateTime");
                            rcdDate = rcdTime.Substring(0, rcdTime.IndexOf(' '));
                            
                            if (this.db.GetRecordSet(string.Format(ADD_CHKRECORD, getDicValue(values, "PatientId"), "lischk", rcdNo,
                                                     getDicValue(values, "TestCause"), rcdTime, getDicValue(values, "transcriptionist"),
                                                     rcdDate, getDicValue(values, "RelevantClinicDiag"), getDicValue(values, "Specimen")), ref dr))
                            {
                                if (dr.Read())
                                    isSave = (1 == dr.GetInt32(0));
                                else
                                    isSave = false;

                                dr.Close();
                            }
                            else
                            {
                                this.logOp.log("保存患者(" + _hspNo + ")实验室单(" + rcdNo + ")失败:" + this.db.Error);
                                isSave = false;
                            }
                        }

                        isFirstItem = false;
                        values.Clear();
                    }
                }
                else if (XmlNodeType.EndElement == xmlReader.NodeType)
                {
                    tagName = xmlReader.Name;
                    if ("ResultCode".Equals(tagName))
                    {
                        if (!"1".Equals(values[tagName]))
                            isFail = true;
                    }
                    else if ("ResultContent".Equals(tagName))
                    {
                        if (isFail)
                        {
                            this.logOp.log("患者(" + _hspNo + ")实验室检查数据错误:" + values[tagName]);
                            break;
                        }
                    }
                    else if ("Items".Equals(tagName))
                    {
                        if (isSave)
                        {
                            if (!string.IsNullOrWhiteSpace(sql))
                                sql += "UNION ALL ";

                            temp = getDicValue(values, "AbnormalIndicator");
                            if ("L".Equals(temp))
                                temp = "↓";
                            else if ("H".Equals(temp))
                                temp = "↑";
                            else
                                temp = "";

                            sql += string.Format(VAL_LISCHECK, rcdNo, "", getDicValue(values, "ReportItemName"),
                                    getDicValue(values, "ReportItemName"), getDicValue(values, "Result"),
                                    getDicValue(values,"Units"), getDicValue(values,"ReferenceResult"),
                                    temp, rcdDate);
                        }

                        values.Clear();
                    }
                    else if ("LabList".Equals(tagName))
                    {
                        if (isSave && !string.IsNullOrWhiteSpace(sql))
                            if (!this.db.ExecSQL(INST_LISCHECK + sql)) 
                            {
                                this.logOp.log("保存患者(" + _hspNo + ")实验室(" + rcdNo + ")数据失败:" + this.db.Error);
                                if (this.isDebug)
                                    this.logOp.log(sql);
                                isSave = false;
                            }

                        sql = "";
                    } 
                    tagName = "";
                }
                else if (XmlNodeType.Text == xmlReader.NodeType)
                {
                    if (isSave) 
                        values.Add(tagName, xmlReader.Value); 
                }
            }

            xmlReader.Close();
        }
    }
}
