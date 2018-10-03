using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tpnmonitor
{
    class SQL
    {
        public const string SEL_CONFIG = "SELECT SettingItemCode, SettingValue FROM SystemSetting ss " +
            "WHERE EXISTS(SELECT 1 FROM SystemSettingItem si WHERE ss.SettingItemCode=si.SettingItemCode " +
            " AND si.SettingItemType='TPN')";
        public const string SEL_TPNTEIM = "SELECT TPNItemID, ItemType, ItemCode, ItemName, Express, Unit, " +
            "NormalValue, SeqNo FROM tpn.dbo.TPNItem WHERE SeqNo>=-1 ";
        //通用审核项目
        public const string SEL_ALWAY_CHK = "SELECT UniPreparationID, TPNItemID, NormalValue, SeqNo " +
            "FROM tpn.dbo.TPNAlwayCheck ";

        //审方条件
        public const string SEL_TPNMNT_CDT = "SELECT TPNMonitorID, 'tpn' ItemType, TPNItemID Code, " +
            "Condition, IsAccord IsIn FROM tpn.dbo.TPNMonitor2TPNItem m2t " +
            "WHERE EXISTS(SELECT 1 FROM tpn.dbo.TPNMonitor tm WHERE m2t.TPNMonitorID=tm.TPNMonitorID AND tm.IsUse=1) " +
            "UNION ALL " +
            "SELECT TPNMonitorID, 'icd' ItemType, DiagnoseCode Code, '' Condition, IsExists IsIn " +
            "FROM tpn.dbo.TPNMonitor2Diagnose m2d " +
            "WHERE EXISTS(SELECT 1 FROM tpn.dbo.TPNMonitor tm WHERE m2d.TPNMonitorID=tm.TPNMonitorID AND tm.IsUse=1) " +
            "UNION ALL " +
            "SELECT TPNMonitorID, CASE WHEN 1=IsType1 THEN 'class' ELSE 'drug' END ItemType, Drug1 Code, " +
            "(CASE WHEN 1=IsType2 THEN 'class.' ELSE 'drug.' END + Drug2) Condition, IsTogether IsIn " +
            "FROM tpn.dbo.TPNMonitor2Drug m2d " +
            "WHERE EXISTS(SELECT 1 FROM tpn.dbo.TPNMonitor tm WHERE m2d.TPNMonitorID=tm.TPNMonitorID AND tm.IsUse=1) ";
        //患者诊断
        public const string SEL_PNTDIG = "SELECT ICD10Code, SubCode1, SubCode2 FROM hospitaldata.dbo.PatientDiagnose (NOLOCK) " +
           "WHERE PatientCode='{0}'";
        //检查项目最新记录
        public const string SEL_PNTBODYCHK = "SELECT ('bodychk.' + bc.BodyCheckItemCode) Code, bc.ResultValue " +
            //", bc.BodyCheckItemName Name, bc.ResultUnit " +
            "FROM LISData.dbo.BodyCheckResult bc (NOLOCK) INNER JOIN LISData.dbo.CheckRecord cr (NOLOCK) " +
            "ON (cr.CheckDate=bc.CheckDate AND cr.CheckRecordNo=bc.CheckRecordNo) " +
            "WHERE cr.PatientCode='{0}' AND cr.CheckType='BODY' " +
            "AND NOT EXISTS(SELECT 1 FROM LISData.dbo.BodyCheckResult bc2 (NOLOCK) " +
            "  INNER JOIN LISData.dbo.CheckRecord cr2 (NOLOCK) " +
            "  ON (cr2.CheckDate=bc2.CheckDate AND cr2.CheckRecordNo=bc2.CheckRecordNo) WHERE cr2.PatientCode='{0}' " +
            "  AND cr2.CheckDate>cr.CheckDate AND bc2.BodyCheckItemCode=bc.BodyCheckItemCode " +
            "  AND cr2.CheckTime>cr.CheckTime) ";
        public const string SEL_PNTLISCHK = "SELECT ('lischk.' + lc.CheckItemCode) Code, lc.ResultValue " +
            //", lc.CheckItemName Name, lc.ResultUnit " +
            "FROM LISData.dbo.LisCheckResult lc (NOLOCK) INNER JOIN LISData.dbo.CheckRecord cr (NOLOCK) " +
            "ON (cr.CheckDate=lc.CheckDate AND cr.CheckRecordNo=lc.CheckRecordNo) " +
            "WHERE cr.PatientCode='{0}' AND cr.CheckType='LIS' " +
            "AND NOT EXISTS(SELECT 1 FROM LISData.dbo.LisCheckResult lc2 (NOLOCK) " +
            "   INNER JOIN LISData.dbo.CheckRecord cr2 (NOLOCK) " +
            "   ON (cr2.CheckDate=lc2.CheckDate AND cr2.CheckRecordNo=lc2.CheckRecordNo) WHERE cr2.PatientCode='{0}' " +
            "   AND cr2.CheckDate>cr.CheckDate AND lc2.CheckItemCode=lc.CheckItemCode AND cr2.CheckTime>cr.CheckTime) " +
            "AND lc.CheckItemCode IN({1})";
        //PIVAS TPN审核项目
        public const string SEL_COMTPN_CHK = "SELECT AlwayCheckID, TPNItemID, NormalValue, UniPreparationID, " +
            "RequireSugar, RequireAA, RequireFat, SeqNo FROM tpn.dbo.TPNAlwayCheck";
        
        //药品
        public const string SEL_DRUG = "SELECT CustomCode, CustomName, SpecDesc, Dosage, DosageUnit, SpellCode, " +
            "Capacity, CapacityUnit, UniPreparationID FROM hospitaldata.dbo.CustomCode"; 
 
        //医嘱药品制剂
        public const string SEL_ORDERSPREP = "SELECT rd.Dosage, rd.DosageUnit, rd.Quantity, c.Dosage StdDosage, " +
            "c.DosageUnit StdDosageUnit, c.Capacity, c.CapacityUnit, c.IsMenstruum, c.UniPreparationID " +
            "FROM hospitaldata.dbo.OrdersDetail rd (NOLOCK) " +
            "INNER JOIN hospitaldata.dbo.CustomCode c ON rd.DrugCode=c.CustomCode " +
            "WHERE rd.RecipeID='{0}' ";
        //制剂类型
        public const string SEL_PREP_CLASS = "SELECT DISTINCT um.UniPreparationID, mm.MedicineClassID " +
            "FROM KD0100.dbo.[MedicineClass-Medicine] mm (NOLOCK) " +
            "INNER JOIN KD0100.dbo.[UniPrep-Medicine] um (NOLOCK) ON um.MedicineID=mm.MedicineID " +
            "WHERE um.UniPreparationID IN({0})";
        //审核结果
        public const string ADD_ORDERSTPNVAL = "INSERT INTO tpn.dbo.OrdersTPNItemValue(RecipeID, TPNItemID, " +
            "ItemValue, CalEmp, CalTime) VALUES('{0}', {1}, '{2}', '{3}', GETDATE())";
        public const string DEL_ORDERSTPNVAL = "DELETE FROM tpn.dbo.OrdersTPNItemValue WHERE RecipeID='{0}'";
        public const string DEL_TPNCHKRESULT = "DELETE FROM tpn.dbo.TPNCheckResult WHERE RecipeID='{0}'; ";
        public const string ADD_TPNCHKRESULT = "INSERT INTO tpn.dbo.TPNCheckResult(RecipeID, ResultType, " +
            "ResultDesc, ReferenceName, ResultLevel, Checker, SourceID, DataDate, CheckDT) " +
            "SELECT '{1}', ResultType, ResultDesc, MonitorRef, ResultLevel, '{2}', {0}, " +
            "CONVERT(VARCHAR(10), GETDATE(), 121), GETDATE() FROM tpn.dbo.TPNMonitor WHERE TPNMonitorID={0}";
        public const string ADD_TPNCHKRESULT_TPNCHK = "INSERT INTO tpn.dbo.TPNCheckResult(RecipeID, ResultType, " +
            "ResultDesc, ReferenceName, ResultLevel, Domain, Checker, SourceID, DeviateValue, DeviatePer, " +
            "DataDate, CheckDT) " +
            "SELECT '{1}', ResultType, ResultDesc, MonitorRef, ResultLevel, Condition, '{2}', {0}, {3}, {4}, " +
            "CONVERT(VARCHAR(10), GETDATE(), 121), GETDATE() FROM tpn.dbo.TPNMonitor WHERE TPNMonitorID={0}";
        public const string ADD_COMCHK_RESULT = "INSERT INTO tpn.dbo.TPNAlwayChkResult(RecipeID, TPNItemID, " +
            "NormalValue, DeviateValue, DeviatePer, Checker, SourceID, SeqNo, DataDate, CheckTime) " +
            "VALUES('{0}', {1}, '{2}', {3}, {4}, '{5}', {6}, {7}, CONVERT(VARCHAR(10), GETDATE(), 121), GETDATE())";
        public const string ADD_TPNCHK_RCD = "UPDATE tpn.dbo.TPNCheckRecord SET IsValid=0 WHERE RecipeID='{0}'; " +
            "INSERT INTO tpn.dbo.TPNCheckRecord(RecipeID, Checker, CheckLevel, DataDate, CheckTime, IsValid) " +
            "SELECT '{0}', '{1}', {2}, CONVERT(VARCHAR(10), GETDATE(), 121), GETDATE(), 1 ";
        public const string SEL_MAX_LVL = "ISNULL((SELECT MAX(ResultLevel) FROM tpn.dbo.TPNCheckResult tcr (NOLOCK) " +
            "WHERE tcr.RecipeID='{0}'), 0)";

        //数据明细 
        public const string SEL_PREP_NAME = "SELECT un.UniversalName, up.StandardSpec FROM kd0100.dbo.UniPreparation up " +
            "INNER JOIN kd0100.dbo.UniversalName un ON up.UniversalID=un.UniversalID " +
            "WHERE up.UniPreparationID={0}";
        public const string SEL_TPN_THING = "SELECT i.ItemName, um.Content, i.Unit FROM KD0100.dbo.[UniPreparation-MedComponent] um " +
            "INNER JOIN tpn.dbo.TPNItem i ON um.MedComponentID=i.OriginID WHERE um.UniPreparationID={0} " +
            "AND i.ItemCode IN({1})";
        public const string SEL_TPN_COMP = "SELECT i.ItemName, uc.Content, i.Unit FROM KD0100.dbo.[UniPreparation-ContentThing] uc " +
            "INNER JOIN tpn.dbo.TPNItem i ON uc.ContentThingID=i.OriginID WHERE uc.UniPreparationID={0} " +
            "AND i.ItemCode IN({1})";
        public const string SEL_UNPREP_THING = "SELECT UniPreparationID, ContentThingID, Content " +
            "FROM KD0100.dbo.[UniPreparation-ContentThing] WHERE ContentThingID IN({0}) AND UniPreparationID IN({1})";
        public const string SEL_UNPREP_COMP = "SELECT UniPreparationID, MedComponentID, Content " +
            "FROM KD0100.dbo.[UniPreparation-MedComponent] WHERE MedComponentID=IN({0}) AND UniPreparationID IN({1})";

        //医嘱内容
        public const string SEL_ORDERSPREP_WDRUG = "SELECT rd.DrugCode, rd.DrugName, rd.DrugSpec, rd.Dosage, " +
            "rd.DosageUnit, rd.Quantity, c.Dosage StdDosage, c.DosageUnit StdDosageUnit, c.Capacity, c.CapacityUnit, " +
            "c.IsMenstruum, c.UniPreparationID FROM hospitaldata.dbo.OrdersDetail rd " +
            "INNER JOIN hospitaldata.dbo.CustomCode c ON rd.DrugCode=c.CustomCode WHERE rd.RecipeID='{0}' ";
    }
}
