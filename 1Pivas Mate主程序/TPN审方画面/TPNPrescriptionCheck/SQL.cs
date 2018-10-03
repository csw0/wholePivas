using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPNReview
{
    public class SQL
    {
        //账户 
        public const string SEL_EMPNAME = "SELECT EmployeeName FROM hospitaldata.dbo.DEmployee " +
            "WHERE EmployeeCode='{0}'";
        public const string SEL_ACOUNTNAME = "SELECT e.EmployeeCode, e.EmployeeName FROM DTCOrganizationMembers m " +
            "LEFT JOIN hospitaldata.dbo.DEmployee e ON m.EmployeeCode=e.EmployeeCode";

        //设置
        public const string SEL_CONFIG = "SELECT SettingItemCode, SettingValue FROM SystemSetting ss " +
            "WHERE EXISTS(SELECT 1 FROM SystemSettingItem si WHERE ss.SettingItemCode=si.SettingItemCode " +
            "AND si.SettingItemType='TPN')";
        public const string SET_CONFIG = "IF EXISTS(SELECT 1 FROM SystemSetting WHERE SettingItemCode='{0}') " +
            "  UPDATE SystemSetting SET SettingValue='{1}', SetTime=GETDATE() WHERE SettingItemCode='{0}' " +
            "ELSE " +
            "  INSERT INTO SystemSetting(SettingItemCode, SettingValue, SetTime) " +
            "  VALUES('{0}', '{1}', GETDATE())";
        public const string SET_CANMOD_ORDERSTYP = "SELECT ss.SettingValue FROM SystemSetting ss " +
            "INNER JOIN SystemSettingItem si ON ss.SettingItemCode=si.SettingItemCode " +
            "WHERE si.SettingItemType='TPN' AND ss.SettingItemCode='modordtyp'";

        
        //TPN项目 
        public const string SEL_TPNTEIM = "SELECT TPNItemID, ItemType, ItemCode, ItemName, Express, Unit, " +
            "NormalValue, SeqNo FROM tpn.dbo.TPNItem WHERE SeqNo>=-1 ";
        public const string SEL_TPNTEIM2 = "SELECT TPNItemID, ItemName, Unit FROM tpn.dbo.TPNItem";
        public const string SEL_TPNTEIM_SEL = "SELECT TPNItemID Code, ItemName + ' ' + ISNULL(Unit, '') [Name], " +
            "'' QryCode FROM tpn.dbo.TPNItem";
        public const string TPNTEIM_ORDER_BY = " ORDER BY CASE WHEN SeqNo IS NULL THEN 999 WHEN SeqNo<0 THEN 999 " +
            "ELSE SeqNo END, ItemName";
        public const string SEL_TPNTEIM_ALL = SEL_TPNTEIM + TPNTEIM_ORDER_BY;
        public const string SEL_TPNTEIM_BYTYP = SEL_TPNTEIM + " WHERE ItemType='{0}'";
        public const string SEL_TPNTEIM_NAME = "SELECT ItemName [Name] FROM tpn.dbo.TPNItem WHERE ItemCode='{0}'";
        public const string SET_TPNTEIM_EXP = "UPDATE tpn.dbo.TPNItem SET Express='{0}' WHERE TPNItemID={1}";
        public const string MOD_TPNTEIM_SEQ = "UPDATE tpn.dbo.TPNItem SET SeqNo={0} WHERE TPNItemID={1}";
        public const string MOD_TPNTEIM_NORVAL = "UPDATE tpn.dbo.TPNItem SET NormalValue='{0}' WHERE TPNItemID={1}";

        //审方项目条件 
        public const string DEL_TPNMNT_CDT = "DELETE FROM tpn.dbo.TPNMonitor2TPNItem WHERE TPNMonitorID={0}; " +
            "DELETE FROM tpn.dbo.TPNMonitor2Drug WHERE TPNMonitorID={0}; " +
            "DELETE FROM tpn.dbo.TPNMonitor2Diagnose WHERE TPNMonitorID={0}; ";
        public const string ADD_TPNMNT_TPN = "INSERT INTO tpn.dbo.TPNMonitor2TPNItem(TPNMonitorID, TPNItemID, " +
            "Condition, IsAccord) VALUES({0}, {1}, '{2}', {3})";
        public const string ADD_TPNMNT_DIG = "INSERT INTO tpn.dbo.TPNMonitor2Diagnose(TPNMonitorID, DiagnoseCode, " +
            "DiagnoseName, IsExists) VALUES({0}, {1}, '{2}', {3})";
        public const string ADD_TPNMNT_DRUG = "INSERT INTO tpn.dbo.TPNMonitor2Drug(TPNMonitorID, Drug1, IsType1, " +
            "Drug2, IsType2, IsTogether) VALUES({0}, '{1}', {2}, '{3}', {4}, {5})";
        //TPN审方
        public const string ADD_TPNMNT = "INSERT INTO tpn.dbo.TPNMonitor(ResultType, ResultLevel, ResultDesc, " +
            "MonitorRef, Inputer, IsUse, InputTime) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', 1, GETDATE())";
        public const string MOD_TPNMNT = "UPDATE tpn.dbo.TPNMonitor SET ResultType='{0}', ResultLevel={1}, " +
            "ResultDesc='{2}', MonitorRef='{3}', InputTime=GETDATE() WHERE TPNMonitorID={4}";
        public const string MOD_TPNMNT_USE = "UPDATE tpn.dbo.TPNMonitor SET IsUse={1} WHERE TPNMonitorID={0}";
        public const string DEL_TPNMNT = "DELETE FROM tpn.dbo.TPNMonitor WHERE TPNMonitorID={0}; " + DEL_TPNMNT_CDT;
        public const string SEL_TPNMNTSET = "SELECT TPNMonitorID, ResultType, ResultDesc, ResultLevel, " +
            "MonitorRef, InputTime, Inputer, IsUse FROM tpn.dbo.TPNMonitor ";
        public const string SEL_TPNMNT_TYPE = "SELECT DISTINCT ResultType FROM tpn.dbo.TPNMonitor";
        public const string SEL_TPNMNT_REF = "SELECT DISTINCT MonitorRef FROM tpn.dbo.TPNMonitor";
        public const string SEL_TPNMNT_ITEM = "SELECT TPNMonitorID, TPNItemID FROM tpn.dbo.TPNMonitor2TPNItem";
        public const string SEL_DIG = "SELECT HisICDCode Code, HisICDName [Name], SpellCode QryCode " +
            "FROM hospitaldata.dbo.HISICD2Standard";
        public const string SEL_PREP = "SELECT up.UniPreparationID Code, un.SpellCode QryCode, " +
            "(un.UniversalName + CASE WHEN 'N/A'=up.StandardSpec THEN '' ELSE ' '+up.StandardSpec END) [Name] " +
            "FROM KD0100.dbo.UniPreparation up " +
            "INNER JOIN KD0100.dbo.UniversalName un ON up.UniversalID=un.UniversalID";
        public const string SEL_MEDCLASS = "SELECT MedicineClassID Code, MedicineClassName [Name], " +
            "SpellCode QryCode FROM KD0100.dbo.MedicineClass";
        public const string SEL_TPNMNT_DIG = "SELECT c.TPNMonitorID, 'tpn' ItemType, i.TPNItemID Code, " +
            "(i.ItemName + '(' + ISNULL(i.Unit, '') + ')') Name, c.Condition, c.IsAccord IsIn " +
            "FROM tpn.dbo.TPNMonitor2TPNItem c INNER JOIN tpn.dbo.TPNItem i ON c.TPNItemID=i.TPNItemID " +
            "UNION ALL " +
            "SELECT TPNMonitorID, 'icd' ItemType, DiagnoseCode Code, DiagnoseName Name, '' Condition, " +
            "IsExists IsIn FROM tpn.dbo.TPNMonitor2Diagnose ";
        //药品审方条件
        public const string SEL_TPNMNT_DRUG = "SELECT md.TPNMonitorID, md.Drug1 Code, md.Drug2 Code2, md.IsType2, " +
            "CASE WHEN 1=md.IsType1 THEN 'class' ELSE 'drug' END ItemType, " + 
            "(CASE WHEN 1=md.IsType1 THEN mc1.MedicineClassName ELSE un1.UniversalName END) Name, " +
            "(CASE WHEN 1=md.IsType2 THEN mc2.MedicineClassName ELSE un2.UniversalName END) Condition, " +
            "md.IsTogether IsIn FROM tpn.dbo.TPNMonitor2Drug md " +
            "LEFT JOIN " +
            "   (KD0100.dbo.UniPreparation up1 INNER JOIN KD0100.dbo.UniversalName un1 ON up1.UniversalID=un1.UniversalID) " +
            " ON (CAST(md.Drug1 AS INT)=up1.UniPreparationID AND md.IsType1=0) " +
            "LEFT JOIN KD0100.dbo.MedicineClass mc1 ON (CAST(md.Drug1 AS INT)=mc1.MedicineClassID AND md.IsType1=1) " +
            "LEFT JOIN " +
            "   (KD0100.dbo.UniPreparation up2 INNER JOIN KD0100.dbo.UniversalName un2 ON up2.UniversalID=un2.UniversalID) " +
            " ON (CAST(md.Drug2 AS INT)=up2.UniPreparationID AND md.IsType2=0) " +
            "LEFT JOIN KD0100.dbo.MedicineClass mc2 ON (CAST(md.Drug2 AS INT)=mc2.MedicineClassID AND md.IsType2=1) ";

        //通用审核设置
        public const string ADD_COMCHECK = "INSERT INTO tpn.dbo.TPNAlwayCheck(TPNItemID, UniPreparationID, " +
            "NormalValue, RequireSugar, RequireAA, RequireFat, SeqNo, Inputer, InputTime) " +
            "VALUES({0}, {1}, '{2}', {3}, {4}, {5}, {6}, '{7}', GETDATE())";
        public const string MOD_COMCHECK = "UPDATE tpn.dbo.TPNAlwayCheck SET TPNItemID={0}, UniPreparationID={1}, " +
            "NormalValue='{2}', RequireSugar={3}, RequireAA={4}, RequireFat={5}, SeqNo={6} WHERE AlwayCheckID={7}";
        public const string DEL_COMCHECK = "DELETE FROM tpn.dbo.TPNAlwayCheck WHERE AlwayCheckID={0}";
        public const string SEL_COMCHECK = "SELECT ac.AlwayCheckID, ac.TPNItemID, ac.UniPreparationID, " +
            "ac.NormalValue, ac.RequireSugar, ac.RequireAA, ac.RequireFat, ac.SeqNo, ac.Inputer, ac.InputTime, " +
            "(i.ItemName + ' ' + i.Unit) ItemName, (un.UniversalName + ' ' + up.StandardSpec) Drug " + 
            "FROM tpn.dbo.TPNAlwayCheck ac INNER JOIN tpn.dbo.TPNItem i ON ac.TPNItemID=i.TPNItemID " +
            "LEFT JOIN " +
            " (KD0100.dbo.UniPreparation up INNER JOIN KD0100.dbo.UniversalName un ON up.UniversalID=un.UniversalID) " +
            "ON ac.UniPreparationID=up.UniPreparationID " +
            "ORDER BY ac.UniPreparationID,ac.SeqNo";
        //通用审核结果
        public const string SEL_COMCHK_BYRCPs = "SELECT cr.RecipeID, i.ItemName, ov.ItemValue, i.Unit, " +
            "cr.NormalValue, cr.DeviateValue, cr.DeviatePer FROM tpn.dbo.TPNAlwayChkResult cr (NOLOCK) " +
            "INNER JOIN tpn.dbo.TPNItem i ON i.TPNItemID=cr.TPNItemID " +
            "LEFT JOIN tpn.dbo.OrdersTPNItemValue ov ON (ov.RecipeID=cr.RecipeID AND ov.TPNItemID=cr.TPNItemID) " + 
            "WHERE cr.RecipeID IN({0}) AND 0<>cr.DeviateValue";
         

        //患者
        public const string SEL_WARD = "SELECT DeptCode, DeptName FROM hospitaldata.dbo.DDept " +
            "WHERE DeptType=1 AND IsValid=1";
        public const string SEL_PATIENT = "SELECT p.PatientCode, p.DeptCode, d.DeptName, p.BedNo, p.HospitalNo, " +
            "p.PatientName, p.Birthday, p.Sex, p.InHospitalDT, p.IsInHospital FROM hospitaldata.dbo.Patient p (NOLOCK) " +
            "LEFT JOIN hospitaldata.dbo.DDept d ON p.DeptCode=d.DeptCode ";
        public const string SEL_TPN_PATIENT = SEL_PATIENT +
            "WHERE EXISTS(SELECT 1 FROM hospitaldata.dbo.Orders r (NOLOCK) WHERE r.PatientCode=p.PatientCode " +
            "AND r.OrdersLabel=4)";

        //获取最近一周内用过tpn的患者
        public const string SEL_RECENTTPN_PATIENT = SEL_PATIENT +
            "WHERE p.IsInHospital=1 AND EXISTS(SELECT 1 FROM hospitaldata.dbo.Orders r (NOLOCK) " +
            " WHERE r.PatientCode=p.PatientCode AND r.OrdersLabel=4) ";
        //获取所有用过TPN的病人（含多次住院就会重复出现）
        public const string SEL_ALL_TPN_PATIENT = SEL_PATIENT +
            "WHERE EXISTS(SELECT 1 FROM hospitaldata.dbo.Orders r (NOLOCK) " +
            " INNER JOIN hospitaldata.dbo.OrdersUseRecord ou (NOLOCK) ON r.GroupNo=ou.RecipeID " +
            " WHERE r.OrdersLabel=4 AND r.PatientCode=p.PatientCode AND r.DeptCode=p.DeptCode) " +
            "ORDER BY d.DeptName, p.BedNo";
        public const string SEL_PATIENT_BYHSPNO = SEL_TPN_PATIENT + " AND p.HospitalNo='{0}'";
        public const string QRY_PATIENT = SEL_TPN_PATIENT + " AND p.DeptCode='{0}' AND p.InHospitalDate<='{2}' " +
            "AND (p.OutHospitalDate IS NULL OR p.OutHospitalDate>'{1}') ORDER BY d.DeptName, p.BedNo";
        public const string SEL_PNTINFO = "SELECT d.DeptName, p.BedNo, p.HospitalNo, p.PatientName, p.Birthday, " +
            "p.Sex FROM hospitaldata.dbo.Patient p (NOLOCK) LEFT JOIN hospitaldata.dbo.DDept d ON p.DeptCode=d.DeptCode " +
            "WHERE p.PatientCode='{0}'";
        public const string SEL_PNTDIG = "SELECT ICD10Name, SubName1, SubName2 FROM hospitaldata.dbo.PatientDiagnose (NOLOCK) " +
            "WHERE PatientCode='{0}'";
        public const string SEL_PNTOP = "SELECT OperateName, StartTime, EndTime FROM hospitaldata.dbo.PatientOperate (NOLOCK) " +
            "WHERE PatientCode='{0}'";

        public const string SEL_PNT_TODAYTPN = "SELECT PatientCode FROM hospitaldata.dbo.Orders r (NOLOCK) " +
            "WHERE StartTime>=CONVERT(VARCHAR(10), GETDATE(), 120) AND r.OrdersLabel=4";
        public const string SEL_PNT_TPNCHK = "SELECT r.RecipeID, cr.CheckLevel FROM hospitaldata.dbo.Orders r (NOLOCK) " +
            "LEFT JOIN tpn.dbo.TPNCheckRecord cr (NOLOCK) ON (cr.RecipeID=r.RecipeID AND cr.IsValid=1) WHERE r.PatientCode='{0}' " +
            "AND r.OrdersLabel=4 ";

        //医嘱
        public const string SEL_ORDERS = "SELECT r.RecipeID, r.NowStatus, r.StartTime, r.StopTime, r.UsageCode, " +
            "r.FreqCode, r.Remark, r.OrdersLabel, r.GroupNo, tcr.CheckLevel TPNLevel, rd.DrugCode, rd.DrugName, " +
            "rd.DrugSpec, rd.Dosage, rd.DosageUnit, rd.Quantity FROM hospitaldata.dbo.Orders r (NOLOCK) " +
            "INNER JOIN hospitaldata.dbo.OrdersDetail rd (NOLOCK) ON r.RecipeID=rd.RecipeID " +
            "LEFT JOIN tpn.dbo.TPNCheckRecord tcr (NOLOCK) ON (tcr.RecipeID=r.RecipeID AND tcr.IsValid=1) " +
            "WHERE r.DeptCode='{0}' AND r.PatientCode='{1}'";
        public const string SEL_ACT_ORDERS = SEL_ORDERS + " AND r.NowStatus>=0 " +
            " AND EXISTS(SELECT 1 FROM hospitaldata.dbo.OrdersUseRecord ou (NOLOCK) WHERE r.GroupNo=ou.RecipeID " +
            "AND ou.UseDate>=CONVERT(VARCHAR(10), GETDATE(), 120)) ORDER BY r.StartTime";
        public const string SEL_ALL_ORDERS = SEL_ORDERS + " ORDER BY r.StartTime";
        public const string SEL_ORDDETAIL = "SELECT RecipeID, DrugCode, DrugName, DrugSpec, Dosage, DosageUnit, " +
            "Quantity FROM hospitaldata.dbo.OrdersDetail (NOLOCK) ";
        //医嘱药品
        public const string SEL_DRUG_NAME = "SELECT (CustomName + ' ' + Spec) [name] FROM hospitaldata.dbo.CustomCode " +
            "WHERE CustomCode='{0}' ";
        //医嘱药品制剂
        public const string SEL_ORDERSDETAII_PREP = "SELECT  rd.RecipeID, rd.DrugCode, c.UniPreparationID " +
            "FROM hospitaldata.dbo.OrdersDetail rd (NOLOCK) " +
            "INNER JOIN hospitaldata.dbo.CustomCode c ON rd.DrugCode=c.CustomCode";
        public const string SEL_ORDERSPREP_BYRCPs = SEL_ORDERSDETAII_PREP + " WHERE rd.RecipeID IN({0}) ";
        //修改医嘱类型
        public const string MOD_ORDERS_TYP = "INSERT INTO OrdersTypeModRecord(RecipeID, OldType, NewType, " +
            "ModifyEmp, ModifyTime, IsValid) VALUES('{0}', '{1}', '{2}', '{3}', GETDATE(), 1) " +
            "UPDATE Orders SET OrdersLabel={2} WHERE RecipeID='{0}'";
        //检查记录
        public const string SEL_CHKRCD = "SELECT CheckRecordNo, CheckType, CheckName, CheckDate, CheckTime, " +
            "CheckerCode, InceptDT FROM LISData.dbo.CheckRecord (NOLOCK) WHERE PatientCode='{0}'";
        public const string SEL_CHKRCD_BYDT = SEL_CHKRCD + " AND CheckDate>='{1}' AND CheckDate<='{2}'";
        public const string SEL_CHKRCD_BYTYPE = SEL_CHKRCD_BYDT + " AND CheckType='{3}'";
        public const string ADD_PNTCHK_1 = "DECLARE @TimeStr VARCHAR(50); " +
            "DECLARE @RecordNo VARCHAR(50); " + 
            "SET @TimeStr=CONVERT(VARCHAR(8), GETDATE(), 112)+REPLACE(CONVERT(VARCHAR(12), GETDATE(), 114), ':', ''); " +
            "SET @RecordNo='{0}.'+ @TimeStr; ";
        public const string ADD_CHK_RCD = "INSERT INTO LISData.dbo.CheckRecord(CheckRecordNo, PatientCode, CheckType, " +
            "CheckName, CheckerCode, CheckTime, CheckDate, InceptDT) VALUES(@RecordNo, '{0}', '{1}', '{2}', '{3}', '{4}', " +
            "CONVERT(VARCHAR(10), '{4}', 120), GETDATE())";
        public const string DEL_CHK_RCD = "DECLARE @ChkType VARCHAR(16); " +
            "SELECT @ChkType=CheckType FROM LISData.dbo.CheckRecord (NOLOCK) WHERE CheckRecordNo='{0}'; " +
            "DELETE FROM LISData.dbo.CheckRecord WHERE CheckRecordNo='{0}'; " +
            "IF 'lischk' = @ChkType DELETE FROM LISData.dbo.LisCheckResult WHERE CheckRecordNo='{0}' " +
            "ELSE DELETE FROM LISData.dbo.BodyCheckResult WHERE CheckRecordNo='{0}' ";
        //体格检查
        public const string ADD_PNTBODYCHK = "INSERT INTO LISData.dbo.BodyCheckResult(CheckRecordNo, BodyCheckResultCode, " +
            "BodyCheckItemCode, BodyCheckItemName, ResultValue, ResultUnit, CheckDate, InceptDT) " +
            "VALUES(@RecordNo, '{0}.'+ @TimeStr, '{0}', '{1}', '{2}', '{3}', CONVERT(VARCHAR(10), '{4}', 120), GETDATE()); ";
        public const string SEL_BODYCHK = "SELECT br.CheckRecordNo, br.BodyCheckResultCode ResultID, br.BodyCheckItemCode Code, " +
            "br.BodyCheckItemName Name, br.ResultValue, br.ResultUnit, br.Domain, br.ValueDrect, cr.CheckTime " +
            "FROM LISData.dbo.BodyCheckResult br (NOLOCK) " +
            "INNER JOIN LISData.dbo.CheckRecord cr (NOLOCK) ON cr.CheckRecordNo=br.CheckRecordNo WHERE cr.CheckDate='{0}' " +
            "AND br.CheckDate='{0}' ";
        public const string SEL_BODYCHK_BYNO = SEL_BODYCHK + " AND br.CheckRecordNo='{1}' ";
        public const string SEL_BODYCHK_BYITEMS = SEL_BODYCHK + " AND cr.PatientCode='{1}' AND BodyCheckItemCode IN({2}) ";
        public const string DEL_BODYCHK = "DELETE FROM LISData.dbo.BodyCheckResult WHERE CheckRecordNo='{0}' " +
            "AND BodyCheckResultCode='{1}'";
        //实验室检查
        public const string ADD_PNTLISCHK = "INSERT INTO LISData.dbo.LisCheckResult(CheckRecordNo, CheckResultCode, " +
            "CheckItemCode, CheckItemName, ResultValue, ResultUnit, CheckDate, InceptDT) " +
            "VALUES(@RecordNo, '{0}.'+ @TimeStr, '{0}', '{1}', '{2}', '{3}', CONVERT(VARCHAR(10), '{4}', 120), GETDATE()); ";
        public const string SEL_LISCHK = "SELECT lc.CheckRecordNo, lc.CheckResultCode ResultID, lc.CheckItemCode Code, " +
            "lc.CheckItemName Name, lc.ResultValue, lc.ResultUnit, lc.Domain, lc.ValueDrect, cr.CheckTime " +
            "FROM LISData.dbo.LisCheckResult lc (NOLOCK) " +
            "INNER JOIN LISData.dbo.CheckRecord cr (NOLOCK) ON cr.CheckRecordNo=lc.CheckRecordNo " +
            "WHERE lc.CheckDate='{0}' AND cr.CheckDate='{0}' ";
        public const string SEL_LISCHK_BYNO = SEL_LISCHK + " AND lc.CheckRecordNo='{1}'";
        public const string SEL_LISCHK_BYITEMS = SEL_LISCHK + " AND cr.PatientCode='{1}' AND lc.CheckItemCode IN({2}) ";
        public const string SEL_LISCHKNAME = "SELECT TOP 1 lc.CheckItemName [Name] FROM LISData.dbo.LisCheckResult lc (NOLOCK) " +
            "INNER JOIN LISData.dbo.CheckRecord cr (NOLOCK) ON cr.CheckRecordNo=lc.CheckRecordNo WHERE cr.PatientCode='{0}' " +
            " AND lc.CheckItemCode='{1}'";
        public const string DEL_LISCHK = "DELETE FROM LISData.dbo.LisCheckResult WHERE CheckRecordNo='{0}' " +
            "AND CheckResultCode='{1}'";

        public const string SEL_VIRUSCHK_BYNO = "SELECT CheckResultCode ResultID, VirusCode Code, VirusName Name, " +
            "ResultValue, ResultUnit, ResistantDrugName, Resistant FROM LISData.dbo.VirusCheckResult vc (NOLOCK) " +
            "WHERE CheckDate='{0}' AND CheckRecordNo='{1}'";

        //审方结果
        public const string DEL_TPNMNT_BYID = "DELETE FROM tpn.dbo.TPNCheckResult WHERE TPNCheckResultID='{0}' ";
        public const string DEL_TPNMNT_BYRCP = "DELETE FROM tpn.dbo.TPNCheckResult WHERE RecipeID='{0}'; " +
            "DELETE FROM tpn.dbo.TPNAlwayChkResult WHERE RecipeID='{0}' ";
        public const string UP_MNTRESULT = "UPDATE tpn.dbo.TPNCheckRecord SET CheckLevel=ISNULL((SELECT MAX(ResultLevel) " +
            " FROM tpn.dbo.TPNCheckResult tcr (NOLOCK) WHERE tcr.RecipeID='{0}'), 0) WHERE RecipeID='{0}' AND IsValid=1";
        public const string SEL_CHKLEVEL = "SELECT CheckLevel FROM tpn.dbo.TPNCheckRecord (NOLOCK) WHERE RecipeID='{0}' " +
            "AND IsValid=1";
        public const string SEL_TPNMNT_RCPs = "SELECT RecipeID, ResultType, ResultDesc, ResultLevel " +
            "FROM tpn.dbo.TPNCheckResult (NOLOCK) WHERE RecipeID IN({0})";
        
        //评价  
        public const string SEL_ANALYSIS_DOCITEM = "SELECT DataItemCode, ViewName, DefaultValue FROM DocumentDataItem " +
            "WHERE DocID=22 OR DocID IS NULL";

        public const string ADD_RCD = "DECLARE @RcdID BIGINT " +
            "INSERT INTO MedicalRecord(PatientCode, DocID, EmployeeCode, RecordTime) VALUES('{0}', 22, '{1}', GETDATE()) " +
            "SELECT @RcdID=SCOPE_IDENTITY();";
        public const string DEL_RCD = "DELETE FROM MedicalRecord WHERE RecordID={0};";
        public const string SEL_RCD = "SELECT PatientCode, DocID, EmployeeCode, RecordTime FROM MedicalRecord (NOLOCK) " +
            "WHERE RecordID={0}";
        public const string ADD_RCD_ITEM = "INSERT INTO MedicalRecordItem(RecordID, DataItemCode, ItemValue) " +
            "VALUES({0}, '{1}', '{2}');";
        public const string DEL_RCD_ITEM_BYRCD = "DELETE FROM MedicalRecordItem WHERE RecordID={0}; ";
        public const string SEL_RCDITEMS = "SELECT DataItemCode, ItemValue FROM MedicalRecordItem (NOLOCK) " +
            "WHERE RecordID={0}";
        public const string SEL_ANALYSIS_RCD = "SELECT mr.RecordID, mr.RecordTime, e.EmployeeName Recorder, mr.LockTime, " +
            "(SELECT TOP 1 ItemValue FROM MedicalRecordItem ri (NOLOCK) WHERE mr.RecordID=ri.RecordID AND DataItemCode='total.score') TotalScore " +
            "FROM MedicalRecord mr (NOLOCK) INNER JOIN hospitaldata.dbo.DEmployee e ON mr.EmployeeCode=e.EmployeeCode " +
            "WHERE mr.PatientCode='{0}' AND mr.DocID=22";

        //自定义审方
        public const string ADD_CUSTOM_CHK = "INSERT INTO CustomCheckResult(PatientCode, RecipeID, ResultDesc, " +
            "ResultLevel,Checker,CheckDT) VALUES('{0}','{1}','{2}',{3},'{4}', GETDATE())";
        public const string MOD_CUSTOM_CHK = "UPDATE CustomCheckResult SET ResultDesc='{1}', ResultLevel={2} " +
            "WHERE CustomCheckResultID={0}";
        public const string DEL_CUSTOM_CHK = "DELETE FROM CustomCheckResult WHERE CustomCheckResultID={0}";
        public const string SEL_CUSTOM_CHK = "SELECT CustomCheckResultID, CheckDT, ResultDesc, ResultLevel, " +
            "Checker FROM CustomCheckResult (NOLOCK) ";
        public const string SEL_CUSTOM_CHK_BYID = SEL_CUSTOM_CHK + " WHERE CustomCheckResultID={0}";
         
        //药师日志
        public const string ADD_PM_NOTE = "INSERT INTO PharmacistNote(PatientCode, NoteContent, Noter, NoteTime, " +
            "NoteDate) VALUES('{0}', '{1}', '{2}', GETDATE(), CONVERT(VARCHAR(10), GETDATE(), 120))";
        public const string MOD_PM_NOTE = "UPDATE PharmacistNote SET NoteContent='{0}' WHERE NoteID='{1}'";
        public const string DEL_PM_NOTE = "DELETE FROM PharmacistNote WHERE NoteID={0}";
        public const string SEL_PM_NOTE = "SELECT NoteID, NoteContent, NoteTime, Noter FROM PharmacistNote (NOLOCK) " +
            "WHERE PatientCode='{0}'";
        public const string SEL_PM_NOTE_WPNT = "SELECT pn.NoteID, pn.NoteContent, pn.NoteTime, pn.Noter, " +
            "d.DeptName, p.BedNo, p.PatientName FROM PharmacistNote pn (NOLOCK) " +
            "LEFT JOIN hospitaldata.dbo.Patient p (NOLOCK) ON pn.PatientCode=p.PatientCode " +
            "LEFT JOIN hospitaldata.dbo.DDept d ON p.DeptCode=d.DeptCode ";
        public const string SEL_PM_NOTE_WPNT_BYPNT = SEL_PM_NOTE_WPNT + "WHERE pn.PatientCode='{0}'";
        public const string SEL_PM_NOTE_WPNT_BYDATE = SEL_PM_NOTE_WPNT + "WHERE pn.NoteDate>='{0}' " +
            "AND pn.NoteDate<'{1}'";
        public const string SEL_PM_NOTE_ST = "SELECT 'note' [Type], Noter EmpCode, NoteTime [Time] " +
            "FROM PharmacistNote (NOLOCK) WHERE NoteDate>='{0}' AND NoteDate<='{1}'";

        //监护 
        public const string ADD_CUSTODY = "INSERT INTO Custody(PatientCode, Custodyer, CustodyDesc, " +
            "CustodyTime, CustodyDate) VALUES('{0}', '{1}', '{2}', GETDATE(), CONVERT(VARCHAR(10), GETDATE(), 120)); ";
        public const string MOD_CUSTODY = "UPDATE Custody SET CustodyDesc='{1}' WHERE CustodyID={0}";
        public const string DEL_CUSTODY_BYID = "DELETE FROM Custody WHERE CustodyID={0} ";
        public const string FINISH_CUSTODY = "UPDATE CustodyObject SET Finisher='{1}', FinishReason='{2}', " +
            "FinishTime=GETDATE() WHERE CustodyID={0}";
        public const string SEL_CUSTODY = "SELECT CustodyID, Custodyer, CustodyTime, CustodyDesc FROM Custody (NOLOCK) ";
        public const string SEL_CUSTODY_BYID = SEL_CUSTODY + " WHERE CustodyID={0} ";
        public const string SEL_CUSTODY_BYPNT = SEL_CUSTODY + " WHERE PatientCode='{0}'";
        public const string SEL_CUSTODY_WPNT = "SELECT c.CustodyID, c.PatientCode, c.Custodyer, c.CustodyTime, " +
            "c.FinishTime, c.FinishReason, c.CustodyDesc, d.DeptName, p.BedNo, p.PatientName FROM Custody c (NOLOCK) " +
            "LEFT JOIN hospitaldata.dbo.Patient p (NOLOCK) ON c.PatientCode=p.PatientCode " +
            "LEFT JOIN hospitaldata.dbo.DDept d ON p.DeptCode=d.DeptCode ";
        public const string SEL_CUSTODY_BYDATE = SEL_CUSTODY_WPNT + " WHERE c.CustodyDate>='{0}' " +
            "AND c.CustodyDate<'{1}'";
        public const string SEL_CUSTODY_BYPNT2 = SEL_CUSTODY_WPNT + " WHERE c.PatientCode='{0}'";
        public const string SEL_CUSTODY_ST = "SELECT 'custody' [Type], Custodyer EmpCode, CustodyTime [Time] " +
            "FROM Custody (NOLOCK) WHERE CustodyDate>='{0}' AND CustodyDate<='{1}'";
        //监护对象
        public const string ADD_CUSTODY_OBJ = "INSERT INTO CustodyObject(CustodyID, ObjectType, ObjectCode, " +
            "ObjectName, ObjectValue, ValueTime) VALUES({0}, '{1}', '{2}', '{3}', '{4}', '{5}'); ";
        public const string DEL_CUSTODY_OBJ = "DELETE FROM CustodyObject WHERE CustodyID={0}";
        public const string SEL_CUSTODY_OBJ = "SELECT ObjectType, ObjectCode, ObjectName, ObjectValue, ValueTime " +
            "FROM CustodyObject (NOLOCK) WHERE CustodyID={0} ";

        //干预
        public const string ADD_INTERVENE = "INSERT INTO Intervene(PatientCode, Intervener, IntervenePlan, " +
            "InterveneTime, InterveneDate) " +
            "VALUES('{0}', '{1}', '{2}', GETDATE(), CONVERT(VARCHAR(10), GETDATE(), 120)); ";
        public const string MOD_INTERVENE = "UPDATE Intervene SET IntervenePlan='{1}' WHERE InterveneID={0} ";
        public const string DEL_INTERVENE = "DELETE FROM Intervene WHERE InterveneID={0}";
        public const string SEL_INTERVENE = "SELECT InterveneID, PatientCode, IntervenePlan, Intervener, InterveneTime " +
            "FROM Intervene (NOLOCK)";
        public const string SEL_INTERVENE_BYID = SEL_INTERVENE + " WHERE InterveneID={0}";
        public const string SEL_INTERVENE_BYPNT = SEL_INTERVENE + " WHERE PatientCode='{0}'";
        public const string SEL_INTERVENE_WPNT = "SELECT i.InterveneID, i.PatientCode, i.Intervener, " +
            "i.InterveneTime, i.IntervenePlan, d.DeptName, p.BedNo, p.PatientName FROM Intervene i (NOLOCK) " + 
            "LEFT JOIN hospitaldata.dbo.Patient p (NOLOCK) ON i.PatientCode=p.PatientCode " +
            "LEFT JOIN hospitaldata.dbo.DDept d ON p.DeptCode=d.DeptCode ";
        public const string SEL_INTERVENE_BYDATE = SEL_INTERVENE_WPNT + " WHERE i.InterveneDate>='{0}' " +
            "AND i.InterveneDate<'{1}'";
        public const string SEL_INTERVENE_BYPNT2 = SEL_INTERVENE_WPNT + "WHERE i.PatientCode='{0}'";
        public const string SEL_INTERVENE_ST = "SELECT 'intervene' [Type], Intervener EmpCode, InterveneTime [Time] " +
            "FROM Intervene (NOLOCK) WHERE InterveneDate>='{0}' AND InterveneDate<='{1}'";
        //干预对象
        public const string ADD_INTERVENE_OBJ = "INSERT INTO InterveneObject(InterveneID, ObjectType, ObjectCode, " +
            "ObjectName, ObjectValue, ValueTime) VALUES({0}, '{1}', '{2}', '{3}', '{4}', '{5}'); ";
        public const string DEL_INTERVENE_OBJ = "DELETE FROM InterveneObject WHERE InterveneID={0}";
        public const string SEL_INTERVENE_OBJ = "SELECT ObjectType, ObjectCode, ObjectName, ObjectValue, " +
            "ValueTime FROM InterveneObject (NOLOCK) WHERE InterveneID={0} ";
 
        //导出
        public const string SEL_PNT_TPN = "SELECT r.RecipeID, d.DeptName, r.BedNo, r.HospitalNo, r.Age, r.AgeUnit, " +
            "p.PatientName, p.Sex, p.InHospitalDT, r.StartTime, r.StopTime FROM hospitaldata.dbo.Orders r (NOLOCK)  " +
            "INNER JOIN hospitaldata.dbo.Patient p (NOLOCK) ON p.PatientCode=r.PatientCode " +
            "LEFT JOIN hospitaldata.dbo.DDept d ON p.DeptCode=d.DeptCode WHERE r.PatientCode='{0}' " +
            "AND r.OrdersLabel=4 " +
            "AND (r.StopTime IS NULL OR r.StopTime>=CONVERT(VARCHAR(10), GETDATE(), 120)) AND r.NowStatus>=0";

        //获取患者当前在用TPN医嘱参数
        public const string SEL_PNT_TPNITEM = "SELECT t.ItemCode, ot.ItemValue, t.Unit " +
            "FROM tpn.dbo.OrdersTPNItemValue ot (NOLOCK) INNER JOIN tpn.dbo.TPNItem t ON ot.TPNItemID=t.TPNItemID " +
            "WHERE EXISTS(SELECT 1 FROM hospitaldata.dbo.Orders r (NOLOCK) " +
            "WHERE r.PatientCode='{0}' AND r.OrdersLabel=4 " +
            "AND (r.StopTime IS NULL OR r.StopTime>=CONVERT(VARCHAR(10), GETDATE(), 120)) AND ot.RecipeID=r.RecipeID)";
    }
}
