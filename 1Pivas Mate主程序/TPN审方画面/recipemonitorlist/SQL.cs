using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace recipemonitorlist
{
    public class SQL
    {
        public const string SEL_HSPCODE = "SELECT HospitalCode FROM hospitaldata.dbo.HospitalInfo";
        public const string SEL_TPNTEIM = "SELECT TPNItemID, ItemType, ItemCode, ItemName, Express, Unit, " +
            "NormalValue, SeqNo FROM tpn.dbo.TPNItem WHERE SeqNo>=-1 ";

        //患者信息
        public const string SEL_PNTINFO = "SELECT d.DeptName, p.BedNo, p.HospitalNo, p.PatientName, p.Birthday, " +
            "p.Sex FROM hospitaldata.dbo.Patient p (NOLOCK) LEFT JOIN hospitaldata.dbo.DDept d ON p.DeptCode=d.DeptCode " +
            "WHERE p.PatientCode='{0}'";
        public const string SEL_ORDDETAIL = "SELECT RecipeID, DrugCode, DrugName, DrugSpec, Dosage, " +
            "DosageUnit, Quantity FROM hospitaldata.dbo.OrdersDetail (NOLOCK) ";
        public const string SEL_ORDERSDETAIL = SEL_ORDDETAIL + " WHERE RecipeID='{0}' ";

        //通用审核TPN项目
        public const string SEL_ALWAY_CHK = "SELECT UniPreparationID, TPNItemID, NormalValue, SeqNo " +
            "FROM tpn.dbo.TPNAlwayCheck ";
        public const string SEL_COMCHK_RT = "SELECT TPNItemID, NormalValue, DeviateValue, DeviatePer, SeqNo " +
            "FROM tpn.dbo.TPNAlwayChkResult WHERE RecipeID='{0}'";

        //医嘱药品制剂
        public const string SEL_ORDERSPREP = "SELECT rd.DrugCode, ISNULL(c.UniPreparationID, 0) UniPreparationID " +
            "FROM hospitaldata.dbo.OrdersDetail rd (NOLOCK) " +
            "INNER JOIN hospitaldata.dbo.CustomCode c ON rd.DrugCode=c.CustomCode WHERE rd.RecipeID='{0}' ";

        public const string DEL_TPNMNT_BYRCP = "DELETE FROM tpn.dbo.TPNCheckResult WHERE RecipeID='{0}'; " +
            "DELETE FROM tpn.dbo.TPNAlwayChkResult WHERE RecipeID='{0}' ";

        //加载审方医嘱
        public const string SEL_TPNMNT = "SELECT TPNCheckResultID, ResultType, ResultDesc, ResultLevel, " +
            "ReferenceName, CheckDT, SourceID FROM tpn.dbo.TPNCheckResult (NOLOCK) WHERE RecipeID='{0}' "; 
        public const string SEL_TPNMNT_ITEM = "SELECT mi.TPNMonitorID, i.TPNItemID, i.ItemCode, i.ItemName, " +
            "i.Unit FROM tpn.dbo.TPNMonitor2TPNItem mi INNER JOIN tpn.dbo.TPNItem i ON mi.TPNItemID=i.TPNItemID";
        public const string CMF_TPNMNT = "UPDATE tpn.dbo.TPNCheckRecord SET Confirmer='{1}', IsConfirm=1, " +
            "ConfirmTime=GETDATE() WHERE TPNCheckResultID='{0}'"; 
        public const string SEL_CUSTOM_CHK_BYRCP = "SELECT CustomCheckResultID, CheckDT, ResultDesc, " +
            "ResultLevel, Checker FROM CustomCheckResult (NOLOCK) WHERE RecipeID='{0}'";

        //医嘱明细
        public const string SEL_ORD_TPNMNT = "SELECT i.ItemName, i.Unit, oi.ItemValue, mi.TPNMonitorID " +
            "FROM tpn.dbo.TPNItem i INNER JOIN tpn.dbo.OrdersTPNItemValue oi (NOLOCK) ON i.TPNItemID=oi.TPNItemID " +
            "INNER JOIN tpn.dbo.TPNMonitor2TPNItem mi ON mi.TPNItemID=i.TPNItemID " +
            "WHERE oi.RecipeID='{0}' AND EXISTS" +
            "(SELECT 1 FROM tpn.dbo.TPNCheckResult cr (NOLOCK) WHERE mi.TPNMonitorID=cr.SourceID AND cr.RecipeID='{0}')";
        public const string SEL_ORDERSTPNVAL = "SELECT TPNItemID, ItemValue FROM tpn.dbo.OrdersTPNItemValue (NOLOCK) " +
            "WHERE RecipeID='{0}'";
        public const string SEL_TPN_ORDERS = "SELECT p.PatientCode, d.DeptName, p.BedNo, p.PatientName, " +
            "r.RecipeID, r.GroupNo, r.NowStatus, r.StartTime, r.StopTime, r.UsageCode, r.FreqCode, r.Remark, " +
            "CASE ISNULL((SELECT MAX(ABS(acr.DeviateValue)) FROM tpn.dbo.TPNAlwayChkResult acr (NOLOCK) " +
                       " WHERE acr.RecipeID=r.RecipeID), -1) " +
            "WHEN 0.0 THEN 0 WHEN -1 THEN -1 ELSE 5 END TPNLevel FROM hospitaldata.dbo.Orders r (NOLOCK) " +
            "INNER JOIN hospitaldata.dbo.Patient p (NOLOCK) ON (r.PatientCode=p.PatientCode AND r.DeptCode=p.DeptCode) " + 
            "LEFT JOIN hospitaldata.dbo.DDept d ON (p.DeptCode=d.DeptCode AND d.IsValid=1) " +
            "WHERE r.OrdersLabel=4  AND r.NowStatus>=1 " +
            "AND (r.StopTime IS NULL OR r.StopTime>=CONVERT(VARCHAR(10), GETDATE(), 120)) ";
        public const string SEL_NOCHK_TPN_ORDERS = SEL_TPN_ORDERS + " AND (r.NowStatus=1 OR " +
            "EXISTS(SELECT 1 FROM hospitaldata.dbo.OrdersUseRecord ou (NOLOCK) " +
            " WHERE UseDate>=CONVERT(VARCHAR(10), GETDATE(), 120) AND ou.RecipeID=r.GroupNo))";

        //营养关联检查项目
        public const string SEL_PN_CHKITEM = "SELECT ci.HISCheckItemCode, ci.HISCheckItemName, ci.HISCheckItemUnit, " +
            "ci.KDCheckItemID FROM hospitaldata.dbo.HISCheckItem2Standard ci (NOLOCK) INNER JOIN tpn.dbo.PNCheckItem pn " +
            "ON pn.LISCheckItemCode=ci.HISCheckItemCode WHERE pn.IsMaster=1 ORDER BY pn.SeqNo";
        public const string SEL_PN_CHK_RESULT = "SELECT lc.CheckItemCode, lc.CheckItemName, lc.ResultValue, lc.ResultUnit, " +
            "lc.Domain, lc.ValueDrect, cr.CheckTime, ci.KDCheckItemID FROM LISData.dbo.LisCheckResult lc (NOLOCK) " +
            "INNER JOIN LISData.dbo.CheckRecord cr (NOLOCK) ON cr.CheckRecordNo=lc.CheckRecordNo " +
            "LEFT JOIN hospitaldata.dbo.HISCheckItem2Standard ci (NOLOCK) ON ci.HISCheckItemCode=lc.CheckItemCode " +
            "WHERE cr.PatientCode='{0}' AND EXISTS(SELECT 1 FROM tpn.dbo.PNCheckItem pn WHERE pn.LISCheckItemCode=lc.CheckItemCode) ";

        public const string SEL_PN_CHK_RESULT_BYKDID = SEL_PN_CHK_RESULT + " AND ci.KDCheckItemID={1}";
    }
}
