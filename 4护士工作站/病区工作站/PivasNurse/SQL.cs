using System;
using System.Collections.Generic;
using System.Text;

namespace PivasNurse
{
    public class SQL
    {
        public string SelCheckResult(string strTime)// 查审方结果,,,暂时不用
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT DISTINCT P.PrescriptionID,P.WardCode,P.CaseID,P.DoctorCode,P.PatientCode,PD.InceptDT,PD.GroupNo,");
            str.Append(" Pa.BedNo,Pa.PatName,D.WardName,P.PStatus,P.Level,P.Attention from Prescription P ");
            str.Append(" LEFT JOIN PrescriptionDetail PD ON P.PrescriptionID=PD.PrescriptionID ");
            str.Append(" LEFT JOIN CPRecord CD ON CD.PrescriptionID=P.PrescriptionID");
            str.Append(" LEFT JOIN CPResult CT ON CT.CheckRecID=CD.CPRecordID");
            str.Append(" LEFT JOIN Patient Pa ON Pa.PatCode=P.PatientCode");
            str.Append(" LEFT JOIN DWard D ON D.WardCode=Pa.WardCode ");
            str.Append(" WHERE D.IsOpen <> 0 AND Pa.PatStatus=1 AND DATEDIFF(DD, P.InceptDT , '" + strTime + "')=0");
            str.Append(" AND P.PStatus IN (1,2,3) ");

            return str.ToString();
        }

        public string SelPreInfo(string PreID)//个人信息
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT DISTINCT P.PrescriptionID,P.DoctorCode,DE1.DEmployeeName DocName,D.WardName,");
            str.Append(" P.CaseID,P.FregCode,CT.CensorItem,CT.Description,CT.ReferenName,");
            str.Append(" Pa.BedNo,Pa.PatName,Pa.Sex, Pa.Weight,Pa.Birthday,PD.EndDT,PD.StartDT FROM Prescription P");
            str.Append(" LEFT JOIN PrescriptionDetail PD ON P.PrescriptionID=PD.PrescriptionID");
            str.Append(" LEFT JOIN DEmployee DE1 ON P.DoctorCode=DE1.DEmployeeCode");
            str.Append(" LEFT JOIN DWard D ON D.WardCode=P.WardCode");
            str.Append(" LEFT JOIN Patient Pa ON Pa.PatCode=P.PatientCode");
            str.Append(" LEFT JOIN CPRecord CD ON CD.PrescriptionID=P.PrescriptionID");
            str.Append(" LEFT JOIN CPResult CT ON CT.CheckRecID=CD.CPRecordID");
            str.Append(" WHERE P.PrescriptionID=" + PreID);

            return str.ToString();
        }

        public string SelDrug(string s)//病人所有药
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT PD.GroupNo,P.FregCode Batch,D.DrugName,D.Dosage,D.DosageUnit from DDrug D ");
            str.Append("INNER JOIN PrescriptionDetail PD ON PD.DrugCode = D.DrugCode ");
            str.Append("INNER JOIN Prescription P on P.PrescriptionID =PD.PrescriptionID ");
            str.Append("WHERE P.PatientCode = '" + s + "' Order BY PD.GroupNo");

            return str.ToString();
        }

        public string SeaTCheckResult(string WardCode, string strSea)//按条件查审方结果
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT DISTINCT P.PrescriptionID,P.WardCode,P.PatientCode,PD.InceptDT,PD.GroupNo,");
            str.Append(" Pa.BedNo,Pa.PatName,D.WardName,CD.IsPass ,P.PStatus,P.Level,P.Attention from Prescription P ");
            str.Append(" LEFT JOIN PrescriptionDetail PD ON P.PrescriptionID=PD.PrescriptionID ");
            str.Append(" LEFT JOIN CPRecord CD ON CD.PrescriptionID=P.PrescriptionID");
            str.Append(" LEFT JOIN CPResult CT ON CT.CheckRecID=CD.CPRecordID");
            str.Append(" LEFT JOIN Patient Pa ON Pa.PatCode=P.PatientCode");
            str.Append(" LEFT JOIN DWard D ON D.WardCode=P.WardCode ");
            str.Append(" WHERE  (BedNo like '%" + strSea + "%' ");
            str.Append(" OR PatName like '%" + strSea + "%' ");
            str.Append(" OR PD.GroupNo like '%" + strSea + "%')");
            str.Append(" AND D.IsOpen <> 0 AND Pa.PatStatus = 1 ");
            str.Append(" AND Pa.WardCode = '" + WardCode + "'");

            return str.ToString();
        }

        public string SeaDCheckResult(string WardCode, string strSea)//按条件查审方结果
        {
            StringBuilder str = new StringBuilder();

            str.Append("DECLARE @OrderLabelCountByPatient TABLE ( ");
            str.Append("PatCode varchar(16), ");
            str.Append("PatName varchar(50),");
            str.Append("UnCheckCount int,");
            str.Append("TotalCount int,back bit) ");
            str.Append("BEGIN ");
            str.Append("INSERT @OrderLabelCountByPatient ");
            str.Append("SELECT  ");
            str.Append("Pa.PatCode, ");
            str.Append("Pa.PatName, ");
            str.Append("SUM(case when P.PStatus = 1 then 1 else 0 end) AS UnCheckCount, ");
            str.Append("COUNT(DISTINCT P.PrescriptionID) AS TotalCount, ");
            str.Append("Sum(case when P.Pstatus=3 then 1 else 0 end) as back ");
            str.Append("FROM Patient Pa ");
            str.Append("INNER JOIN Prescription P (NOLOCK) ON Pa.PatCode=P.PatientCode ");
            str.Append("WHERE ");
            str.Append("P.PStatus in(1,2,3) ");
            str.Append("AND Pa.WardCode ='" + WardCode + "' ");
            str.Append("AND (Pa.PatName LIKE '%" + strSea + "%' OR Pa.BedNo LIKE '%" + strSea + "%') ");
            str.Append("GROUP BY Pa.PatCode, Pa.PatName ");
            str.Append("INSERT @OrderLabelCountByPatient ");
            str.Append("SELECT PatCode, PatName, 0 ,0 ,0 ");
            str.Append("FROM Patient ");
            str.Append("WHERE PatCode NOT IN (SELECT PatCode FROM @OrderLabelCountByPatient) ");
            str.Append("AND WardCode='" + WardCode + "' AND (PatName LIKE '%" + strSea + "%' OR BedNo LIKE '%" + strSea + "%') ");
            str.Append("END ");
            str.Append("SELECT O.* FROM @OrderLabelCountByPatient O ");
            str.Append("LEFT OUTER JOIN Patient P ON O.PatCode = P.PatCode ");

            return str.ToString();
        }

        public string SeaPerCheckResult(string sysstatus, string perstatus, string WardCode, string time)//查人工审方
        {
            StringBuilder str = new StringBuilder();

            str.Append("SELECT DISTINCT P.PrescriptionID,P.WardCode,P.PatientCode,PD.InceptDT,PD.GroupNo,");
            str.Append(" Pa.BedNo,Pa.PatName,D.WardName,P.PStatus ,P.Level,P.Attention from Prescription P ");
            str.Append(" LEFT JOIN PrescriptionDetail PD ON P.PrescriptionID=PD.PrescriptionID ");
            str.Append(" LEFT JOIN CPRecord CD ON CD.PrescriptionID=P.PrescriptionID");
            str.Append(" LEFT JOIN CPResult CT ON CT.CheckRecID=CD.CPRecordID");
            str.Append(" LEFT JOIN Patient Pa ON Pa.PatCode=P.PatientCode");
            str.Append(" LEFT JOIN DWard D ON D.WardCode=P.WardCode ");
            str.Append(" WHERE D.IsOpen <> 0 AND Pa.PatStatus = 1 AND Pa.WardCode = '" + WardCode + "'");
            str.Append(" AND P.Level in (" + sysstatus + ")");
            str.Append(" AND P.PStatus in (" + perstatus + ") AND DATEDIFF(DD, P.InceptDT , '" + time + "')=0");

            return str.ToString();
        }

        public string SeaSysCheckResult(string sysstatus, string perstatus, string WardCode, string time)//系统审方
        {
            StringBuilder str = new StringBuilder();

            str.Append("SELECT DISTINCT P.PrescriptionID,P.WardCode,P.PatientCode,PD.InceptDT,PD.GroupNo,");
            str.Append(" Pa.BedNo,Pa.PatName,D.WardName,P.PStatus,P.Level,P.Attention from Prescription P ");
            str.Append(" LEFT JOIN PrescriptionDetail PD ON P.PrescriptionID=PD.PrescriptionID ");
            str.Append(" LEFT JOIN CPRecord CD ON CD.PrescriptionID=P.PrescriptionID");
            str.Append(" LEFT JOIN CPResult CT ON CT.CheckRecID=CD.CPRecordID");
            str.Append(" LEFT JOIN Patient Pa ON Pa.PatCode=P.PatientCode");
            str.Append(" LEFT JOIN DWard D ON D.WardCode=P.WardCode ");
            str.Append(" WHERE D.IsOpen <> 0 AND Pa.PatStatus = 1 AND Pa.WardCode = '" + WardCode + "'");
            str.Append(" AND P.Level in (" + sysstatus + ")");
            str.Append(" AND PSTatus in (" + perstatus + ")");
            str.Append(" AND DATEDIFF(DD, P.InceptDT , '" + time + "')=0");
            str.Append(" ORDER BY D.WardName");

            return str.ToString();
        }

        public string SelCurrentDrug(string PreID)//当前处方的药品
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT D.DrugName,D.Spec,PiShi,P.FregCode from DDrug D ");
            str.Append("INNER JOIN PrescriptionDetail PD ON D.DrugCode=PD.DrugCode ");
            str.Append("INNER JOIN Prescription P on P.PrescriptionID=PD.PrescriptionID ");
            str.Append("where P.PrescriptionID = " + PreID);
            return str.ToString();
        }

        public string SelRes(string PreID)//当前处方的审方结果
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT DISTINCT CT.CensorItem,CT.Description,CT.ReferenName,P.Level from CPResult CT ");
            str.Append("INNER JOIN CPRecord CD on CD.CPRecordID=CT.CheckRecID ");
            str.Append("INNER JOIN Prescription P on P.PrescriptionID=CD.PrescriptionID ");
            str.Append("WHERE P.PrescriptionID = " + PreID);

            return str.ToString();
        }

        public string SelWardRes(string code, string strTime)//按病区和时间查审方结果
        {
            StringBuilder str = new StringBuilder();
            str.Append(" SELECT DISTINCT P.PrescriptionID,P.WardCode,P.PatientCode,P.InceptDT,P.GroupNo,");
            str.Append(" Pa.BedNo,Pa.PatName,D.WardName,P.PStatus,P.Level,P.Attention from Prescription P ");
            str.Append(" LEFT JOIN CPRecord CD ON CD.PrescriptionID=P.PrescriptionID");
            str.Append(" LEFT JOIN CPResult CT ON CT.CheckRecID=CD.CPRecordID");
            str.Append(" LEFT JOIN DWard D ON D.WardCode=P.WardCode ");
            str.Append(" LEFT JOIN Patient Pa ON Pa.PatCode=P.PatientCode");
            str.Append(" WHERE D.IsOpen <> 0 AND Pa.PatStatus=1 AND DATEDIFF(DD, P.InceptDT , '" + strTime + "')=0");
            str.Append(" AND Pa.WardCode = '" + code + "' AND P.PStatus = 1");
            return str.ToString();
        }

        public string INFO(string PreID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT DISTINCT P.PrescriptionID,P.DoctorCode,PiShi,P.CaseID,P.FregCode,P.Level,P.PStatus, ");
            str.Append("DE1.DEmployeeName DocName,DrawerCode,DE2.DEmployeeName DrawerName,D.WardName,PD.EndDT,PD.StartDT ,DD.DrugName,DD.Spec,DD.UniPreparationID,DD.NoName, ");
            str.Append("PD.Quantity+PD.QuantityUnit as Quantity,");
            str.Append("CT.CensorItem,CT.Description,CT.ReferenName, ");
            str.Append("Pa.BedNo,Pa.PatName,Pa.Sex, Pa.Weight,Pa.Birthday,P.BedNo as BedNo1 FROM Prescription P ");
            str.Append("left JOIN PrescriptionDetail PD ON P.PrescriptionID=PD.PrescriptionID ");
            str.Append("left JOIN DEmployee DE1 ON P.DoctorCode=DE1.DEmployeeCode ");
            str.Append("left JOIN DEmployee DE2 ON P.DrawerCode=DE2.DEmployeeCode ");
            str.Append("left JOIN DWard D ON D.WardCode=P.WardCode ");
            str.Append("left JOIN Patient Pa ON Pa.PatCode=P.PatientCode ");
            str.Append("LEFT JOIN CPRecord CD ON CD.PrescriptionID=P.PrescriptionID ");
            str.Append("LEFT JOIN CPResult CT ON CT.CheckRecID=CD.CPRecordID ");
            str.Append("left JOIN DDrug DD ON DD.DrugCode=PD.DrugCode ");
            str.Append("WHERE P.PrescriptionID = " + PreID);
            str.Append(" order by DD.NoName");

            str.Append(" SELECT distinct CT.CensorItem,CT.DrugACode,du.DrugName as 药品名称1, du.DrugNameJC as 药品简称1,");
            str.Append(" CT.DrugBCode,du2.DrugName as 药品名称2, du2.DrugNameJC as 药品简称2,");
            str.Append(" CT.ReferenName,CT.Description,P.PrescriptionID,P.Level, ");
            str.Append(" CT.Level from CPRecord CD INNER JOIN CPResult CT ON CT.CheckRecID=CD.CPRecordID ");
            str.Append(" INNER JOIN Prescription P ON P.PrescriptionID=CD.PrescriptionID ");
            str.Append(" left JOIN DDrug du on du.DrugCode=ct.DrugACode");
            str.Append(" left JOIN DDrug du2 on du2.DrugCode=ct.DrugBCode");
            str.Append(" WHERE P.PrescriptionID = " + PreID);
           
            return str.ToString();
        }
        public string INFO1(string PreID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT DISTINCT P.PrescriptionID,P.DoctorCode,PiShi,P.CaseID,P.FregCode,P.Level,P.PStatus, ");
            str.Append("DE1.DEmployeeName DocName,DrawerCode,DE2.DEmployeeName DrawerName,D.WardName,PD.EndDT,PD.StartDT ,DD.DrugName,DD.Spec,DD.UniPreparationID,DD.NoName, ");
            str.Append("PD.Quantity+PD.QuantityUnit as Quantity, ");
            str.Append("Pa.BedNo,Pa.PatName,Pa.Sex, Pa.Weight,Pa.Birthday,P.BedNo as BedNo1 FROM Prescription P ");
            str.Append("left JOIN PrescriptionDetail PD ON P.PrescriptionID=PD.PrescriptionID ");
            str.Append("left JOIN DEmployee DE1 ON P.DoctorCode=DE1.DEmployeeCode ");
            str.Append("left JOIN DEmployee DE2 ON P.DrawerCode=DE2.DEmployeeCode ");
            str.Append("left JOIN DWard D ON D.WardCode=P.WardCode ");
            str.Append("left JOIN Patient Pa ON Pa.PatCode=P.PatientCode ");
            str.Append("left JOIN DDrug DD ON DD.DrugCode=PD.DrugCode ");
            str.Append("WHERE P.PrescriptionID = " + PreID);
            str.Append(" order by DD.NoName");

            str.Append(" select BPRecord.BPExplain,BPRecord.BPCode,de.DEmployeeName from BPRecord  ");
            str.Append(" left join DEmployee de on de.DEmployeeID=BPRecord.BPCode  ");
            str.Append(" left join Prescription p on p.PrescriptionID=BPRecord.PrescriptionID ");
            str.Append(" where p.PrescriptionID=" + PreID);
            
            return str.ToString();
        }
        public string INFO2(string PreID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT DISTINCT P.PrescriptionID,P.DoctorCode,PiShi,P.CaseID,P.FregCode,P.Level,P.PStatus, ");
            str.Append("DE1.DEmployeeName DocName,DrawerCode,DE2.DEmployeeName DrawerName,D.WardName,PD.EndDT,PD.StartDT ,DD.DrugName,DD.Spec,DD.UniPreparationID, DD.NoName, ");
           // str.Append("CT.CensorItem,CT.Description,CT.ReferenName, ");
            str.Append("iv.WardCode ivWardCode,iv.WardName ivWardName ,iv.BedNo ivBedNo,D.WardName DWardName,Pa.WardCode DWardCode,");
            str.Append("PD.Quantity+PD.QuantityUnit as Quantity,");
            str.Append("Pa.BedNo PaBedNo,Pa.PatName,Pa.Sex, Pa.Weight,Pa.Birthday FROM Prescription P ");
            str.Append("left JOIN PrescriptionDetail PD ON P.PrescriptionID=PD.PrescriptionID ");
            str.Append("left join IVRecord_Scan15 iv on iv.PrescriptionID = P.PrescriptionID ");
            str.Append("left JOIN DEmployee DE1 ON P.DoctorCode=DE1.DEmployeeCode ");
            str.Append("left JOIN DEmployee DE2 ON P.DrawerCode=DE2.DEmployeeCode ");
            str.Append("left JOIN Patient Pa ON Pa.PatCode=P.PatientCode ");
            str.Append("left JOIN DWard D ON D.WardCode=Pa.WardCode ");
        
           // str.Append("LEFT JOIN CPRecord CD ON CD.PrescriptionID=P.PrescriptionID ");
            //str.Append("LEFT JOIN CPResult CT ON CT.CheckRecID=CD.CPRecordID ");
            str.Append("left JOIN DDrug DD ON DD.DrugCode=PD.DrugCode ");
            str.Append("WHERE P.PrescriptionID = " + PreID);
            str.Append("  order by DD.NoName ");

            str.Append(" SELECT distinct CT.CensorItem,CT.DrugACode,du.DrugName as 药品名称1, du.DrugNameJC as 药品简称1,");
            str.Append(" CT.DrugBCode,du2.DrugName as 药品名称2, du2.DrugNameJC as 药品简称2,");
            str.Append(" CT.ReferenName,CT.Description,P.PrescriptionID,P.Level, ");
            str.Append(" CT.Level from CPRecord CD INNER JOIN CPResult CT ON CT.CheckRecID=CD.CPRecordID ");
            str.Append(" INNER JOIN Prescription P ON P.PrescriptionID=CD.PrescriptionID ");
            str.Append(" left JOIN DDrug du on du.DrugCode=ct.DrugACode");
            str.Append(" left JOIN DDrug du2 on du2.DrugCode=ct.DrugBCode");
            str.Append(" WHERE P.PrescriptionID = " + PreID);

            return str.ToString();
        }

        public string SelDetail(string sys, string per, string PatCode, string time)//按时间选个人未审医嘱
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT DISTINCT P.PrescriptionID, P.Level,P.InceptDT,P.FregCode,P.GroupNo,P.CaseID,P.PStatus, ");
            str.Append("DE.DEmployeeName,P.DoctorCode,D.WardCode,D.WardName, DD.PiShi,DD.Spec,P.Attention, ");
            str.Append("Pa.PatCode,Pa.PatName,Pa.Birthday,Pa.BedNo,Pa.Sex,Pa.Weight,");
            str.Append("PD.StartDT,PD.EndDT,PD.DrugName,PD.Dosage,PD.DosageUnit,PD.Quantity,");
            str.Append("DE1.DEmployeeName checker ");
            str.Append("FROM Prescription P ");
            str.Append("LEFT JOIN Patient Pa ON Pa.PatCode=P.PatientCode ");
            str.Append("LEFT JOIN DWard D ON D.WardCode=Pa.WardCode ");
            str.Append("LEFT JOIN PrescriptionDetail PD ON P.PrescriptionID=PD.PrescriptionID ");
            str.Append("LEFT JOIN CPRecord CD ON CD.PrescriptionID=P.PrescriptionID ");
            // str.Append("LEFT JOIN CPResult CT ON CT.CheckRecID=CD.CPRecordID ");
            str.Append("LEFT JOIN DEmployee DE ON DE.DEmployeeCode=P.DoctorCode ");
            str.Append("LEFT JOIN DDrug DD ON DD.DrugCode=PD.DrugCode ");
            str.Append("LEFT JOIN DEmployee DE1 ON DE1.DEmployeeID=CD.CheckDCode ");
            str.Append("WHERE PatCode='" + PatCode + "' AND DATEDIFF(DD,P.InceptDT,'" + time + "')=0 ");
            str.Append("AND P.Level IN (" + sys + ") AND P.PStatus IN (" + per + ") ");
            str.Append("ORDER BY P.PrescriptionID ");

            return str.ToString();
        }

        public string SelAllDetail(string PatCode, string per, string time)//按时间选个人所有医嘱
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT DISTINCT P.PrescriptionID, P.Level,P.InceptDT,P.FregCode,P.GroupNo,P.CaseID,P.PStatus, ");
            str.Append("DE.DEmployeeName,P.DoctorCode,D.WardCode,D.WardName, DD.PiShi,DD.Spec,P.Attention, ");
            str.Append("Pa.PatCode,Pa.PatName,Pa.Birthday,Pa.BedNo,Pa.Sex,Pa.Weight,");
            str.Append("PD.StartDT,PD.EndDT,PD.DrugName,PD.Dosage,PD.DosageUnit,PD.Quantity,");
            str.Append("DE1.DEmployeeName checker ");
            str.Append("FROM Prescription P ");
            str.Append("LEFT JOIN Patient Pa ON Pa.PatCode=P.PatientCode ");
            str.Append("LEFT JOIN DWard D ON D.WardCode=Pa.WardCode ");
            str.Append("LEFT JOIN PrescriptionDetail PD ON P.PrescriptionID=PD.PrescriptionID ");
            str.Append("LEFT JOIN CPRecord CD ON CD.PrescriptionID=P.PrescriptionID ");
            //str.Append("LEFT JOIN CPResult CT ON CT.CheckRecID=CD.CPRecordID ");
            str.Append("LEFT JOIN DEmployee DE ON DE.DEmployeeID=P.DoctorCode ");
            str.Append("LEFT JOIN DDrug DD ON DD.DrugCode=PD.DrugCode ");
            str.Append("LEFT JOIN DEmployee DE1 ON DE1.DEmployeeID=CD.CheckDCode ");
            str.Append("WHERE PatCode='" + PatCode + "' AND DATEDIFF(DD,P.InceptDT,'" + time + "')=0 ");
            str.Append("AND P.PStatus NOT IN (" + per + ")");
            str.Append("ORDER BY P.PrescriptionID ");
            return str.ToString();
        }

        public string SelDetailSys(string sys, string per, string wardcode, string time)
        {
            StringBuilder str = new StringBuilder();

            str.Append("DECLARE @OrderLabelCountByPatient TABLE ( ");
            str.Append("PatCode varchar(16), ");
            str.Append("PatName varchar(50),");
            str.Append("UnCheckCount int,");
            str.Append("TotalCount int,back bit) ");
            str.Append("BEGIN ");
            str.Append("INSERT @OrderLabelCountByPatient ");
            str.Append("SELECT  ");
            str.Append("Pa.PatCode, ");
            str.Append("Pa.PatName, ");
            str.Append("SUM(case when P.PStatus in(" + per + ") AND P.Level in(" + sys + ") then 1 else 0 end) AS UnCheckCount, ");
            str.Append("COUNT(DISTINCT P.PrescriptionID) AS TotalCount, ");
            str.Append("Sum(case when P.Pstatus=3 then 1 else 0 end) as back ");
            str.Append("FROM Patient Pa ");
            str.Append("INNER JOIN Prescription P (NOLOCK) ON Pa.PatCode=P.PatientCode ");
            str.Append("WHERE ");
            str.Append("P.PStatus in(1,2,3) ");
            str.Append("AND DATEDIFF(DD, P.InceptDT, '" + time + "') =0 ");
            str.Append("AND Pa.WardCode ='" + wardcode + "'");
            str.Append("GROUP BY Pa.PatCode, Pa.PatName ");
            str.Append("INSERT @OrderLabelCountByPatient ");
            str.Append("SELECT PatCode, PatName, 0 ,0 ,0 ");
            str.Append("FROM Patient ");
            str.Append("WHERE PatCode NOT IN (SELECT PatCode FROM @OrderLabelCountByPatient) ");
            str.Append("AND WardCode='" + wardcode + "' ");
            str.Append("END ");
            str.Append("SELECT O.* FROM @OrderLabelCountByPatient O ");
            str.Append("LEFT OUTER JOIN Patient P ON O.PatCode = P.PatCode ");
            return str.ToString();
        }

        /// <summary>
        /// 护士站处方处理 加载处方
        /// </summary>
        /// <param name="Ward"></param>
        /// <param name="operate"></param>
        /// <returns></returns>
        public string NurseLoadPre1(string Ward, string operate)//
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT P.BedNo, PatientCode, PatName, P.DoctorCode, P.GroupNo, P.PrescriptionID, CPRecordID, ");
            str.Append("  CD.CheckDCode AS CDoctorCode, DE.DEmployeeName, CD.DoctorExplain  ");
            str.Append("  FROM CPRecord CD ");
            str.Append("  INNER JOIN Prescription P ON CD.PrescriptionID = P.PrescriptionID");
            str.Append("  INNER JOIN Patient PT ON PT.PatCode=P.PatientCode");
            str.Append("  LEFT OUTER JOIN DEmployee DE ON CD.CheckDCode = DE.DEmployeeID ");
            str.Append("  WHERE CD.IsPass = 0 AND P.PStatus = 1 AND DATEDIFF(dd, P.InceptDT, GETDATE()) <= 1 ");
            str.Append("  AND PT.WardCode = '" + Ward + "' AND DoctorOperate = " + operate);

            return str.ToString();
        }

        /// <summary>
        /// 护士站处方处理 加载处方
        /// </summary>
        /// <param name="Ward"></param>
        /// <returns></returns>
        public string NurseLoadPre(string Ward,string operate)
        {
            StringBuilder str = new StringBuilder();

            str.Append("SELECT PT.BedNo,PatName, DE.DEmployeeName,P.GroupNo, CD.DoctorExplain,P.PStatus,P.Level, DoctorOperate,  ");
                str.Append("  CD.CheckDCode AS CDoctorCode,PatientCode,  P.DoctorCode,  P.PrescriptionID, CPRecordID  ");
                str.Append("  FROM CPRecord CD ");
                str.Append("  INNER JOIN Prescription P ON CD.PrescriptionID = P.PrescriptionID");
                str.Append("  INNER JOIN Patient PT ON PT.PatCode=P.PatientCode");
                str.Append("  LEFT OUTER JOIN DEmployee DE ON CD.CheckDCode = DE.DEmployeeID  ");
                str.Append("  WHERE DATEDIFF(dd, P.InceptDT, GETDATE()) <= 1 ");
                str.Append("  AND PT.WardCode = '" + Ward + "'");
                if (operate.Trim() == "0")
                {
 
                }
                else if (operate.Trim() == "1")
                {
                    str.Append(" AND P.PStatus = 1  and P.Level=0 ");
                }
                else if (operate.Trim() == "2")
                {
                    str.Append(" AND P.PStatus = 1  and P.Level !=0 AND (DoctorOperate =0 or DoctorOperate is null) ");
                }
                else if (operate.Trim() == "3")
                {
                    str.Append(" AND P.PStatus = 1  and P.Level !=0  AND DoctorOperate =1");
                }
                else if (operate.Trim() == "4")
                {
                    str.Append(" AND P.PStatus = 1 and P.Level !=0  AND DoctorOperate =2");
                }
                else if (operate.Trim() == "5")
                {
                    str.Append(" and P.PStatusDoctor=3 ");
                }
                   
                   
               
           
            return str.ToString();
        }

        public string BPRecore(string Ward, string Date, int BPisread, int Pstatus)//护士站退方查询
        {
            StringBuilder str = new StringBuilder();
            str.Append("select distinct BPIsRead,BPDT,Pa.BedNo,PatName,de.DEmployeeName,p.GroupNo,BPExplain,p.WardCode,");
            str.Append(" (case p.PStatus when 0 then '新处方' when 1 then '系统已审方' when 2 then '人工通过' when 3  then '人工退方' when 4 then'处方已停' End) as 'PStatus',");
            str.Append(" P.PrescriptionID, BPRecordID,case c.DoctorOperate when 1 then '强制执行' End  as 'doperate' from Prescription p");
            str.Append(" left join BPRecord BP on p.PrescriptionID= BP.PrescriptionID");
            str.Append(" left join PrescriptionDetail pd on pd.PrescriptionID=p.PrescriptionID");
            str.Append(" LEFT JOIN CPRecord C ON BP.PrescriptionID = C.PrescriptionID");
            str.Append(" LEFT JOIN Patient Pa ON Pa.PatCode=P.PatientCode");
            str.Append("  LEFT OUTER JOIN DEmployee DE ON BP.BPCODE = DE.DEmployeeID  ");
            str.Append(" WHERE   Pa.WardCode = '" + Ward + "'  and DATEDIFF(DD,p.InceptDT,'");
            str.Append(Date);
            str.Append("')=0 ");
           
            if (Pstatus == 1)
            {
                str.Append("  AND p.PStatusDoctor=3 ");
                if (BPisread != 3)
                {
                    str.Append(" and (BPIsRead='");
                    str.Append(BPisread);
                    str.Append("' or BPIsRead is null) ");
                }
            
            }
            else if (Pstatus == 2)
            {
                str.Append("  and p.PStatus=2");
            }
          
            return str.ToString();
        }
        public string ReturnPre(string Ward) //退药处方加载
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT distinct  p.PrescriptionID, PT.BedNo, PatientCode, PT.PatName,D.WardName,P.GroupNo,P.FregCode,");
            str.Append(" PD.StartDT ,PD.EndDT,dd.DrugName,dd.Spec ");
            str.Append(" from Prescription p");
            str.Append(" inner join Patient PT ON PT.PatCode=P.PatientCode");
            str.Append(" left join PrescriptionDetail PD on p.PrescriptionID=PD.PrescriptionID");
            //str.Append(" INNER JOIN DWard D ON D.WardCode=P.WardCode ");
            str.Append(" INNER JOIN DWard D ON D.WardCode=PT.WardCode ");//--张建双20130317---//
            //str.Append("  WHERE  P.PStatus <> 3 and P.PStatus<> 4 ");//2014/01/15 顾甡停医嘱处方仍然显示，可以退药。
            str.Append("inner join IVRecord iv on iv.PrescriptionID = p.PrescriptionID ");
            str.Append("inner join DDrug dd on dd.DrugCode = iv.MarjorDrug   ");
            //str.Append(" WHERE P.PStatus < 3  and ");//2014/10/28 陆卓春 停医嘱处方仍然显示，可以退药。
            str.Append(" where");
            str.Append("  PT.WardCode = '" + Ward + "' ");
            str.Append(" and (DATEDIFF(dd, iv.InfusionDT, GETDATE())=0 or  DATEDIFF(dd, iv.InfusionDT, GETDATE()) = -1)");
            //--张建双20130317---//
            //str.Append(" and P.PrescriptionID in (select PrescriptionID  from IVRecord ");
            //str.Append(" where  DATEDIFF(dd, InfusionDT, GETDATE())=0 or  DATEDIFF(dd, InfusionDT, GETDATE()) = -1)");
            //str.Append("  order by p.PrescriptionID ");
            str.Append(" order by BedNo");
            return str.ToString();
        }
        public string ReturnRecord(string id, int istoday)//退药瓶签加载 
        {
            StringBuilder str = new StringBuilder();
            str.Append(" select distinct LabelNo,batch ,IVStatus,WardRetreat,PackTime,LabelOver,dd.DrugName,ivr.WardAct,ivr.CenterAct from IVRecord iv");
            str.Append(" left join DDrug dd on iv.MarjorDrug=dd.DrugCode ");
            str.Append(" left join IVRecordUpdateWait ivr on ivr.WardLabelNo=iv.LabelNo ");
            str.Append(" where PrescriptionID='" + id + "'");
            if (istoday == 0)
            {
                str.Append(" and DATEDIFF(dd, InfusionDT, GETDATE())=0");
            }
            else
            {
                str.Append(" and DATEDIFF(dd, InfusionDT, GETDATE()) = -1");
            }
            return str.ToString();
        }

        public string CancelDrug(string id, string Rid, DateTime Rtime)//退药 
        {
            StringBuilder str = new StringBuilder();
            str.Append(" update IVRecord set ");
            str.Append("  LabelOverID='" + Rid + "' , LabelOverTime='" + Rtime + "'");
            str.Append(" , LabelOver='-1'");
            str.Append(" where LabelNo='" + id + "'");
            str.Append(" and  LabelOver>='0'");
            return str.ToString();
        }
        public string CancelInsert(string id, string Rid, DateTime Rtime,int a)
        {
            StringBuilder str = new StringBuilder();
            str.Append("if not exists (select *from IVRecordUpdateWait where WardLabelNo='");
            str.Append(id);
            str.Append("'and CenterAct='0')");          
            str.Append("insert into IVRecordUpdateWait (WardEmployeeID,WardInsertDT,WardLabelNo,WardAct,WardRemark1,CenterAct) ");
            str.Append("  values(' ");
            str.Append(Rid);
            str.Append("','");
            str.Append(Rtime);
            str.Append("','");
            str.Append(id);
            str.Append("','");
            str.Append(a);
            str.Append("','adaa','0')");          
            return str.ToString();
        }
        public string EarlyDB(string id, string Rid, DateTime Rtime)//提前打包 
        {
            StringBuilder str = new StringBuilder();
            str.Append(" update IVRecord set PackAdvance=1");
            str.Append(" , PackID='" + Rid + "' , PackTime='" + Rtime + "'");
            str.Append(" where LabelNo='" + id + "'");
            return str.ToString();
        }
        public string RowQianShow(string time, string batch, string type, string WardCode)
        {
         
            //if (type == "空")
            //    type = "K";
            //else if (type == "非空")
            //    type = "#";
            //else
            //    type = "";//----------张建双-----------//
            StringBuilder str = new StringBuilder();
            str.Append(" select LabelNo,PT.BedNo,Batch,PT.PatName,GroupNo,PrescriptionID,FreqCode,IVStatus,(select COUNT(1) from IVRecord_QS where IVRecordID=LabelNo) mm,WardRetreat,LabelOver");
            str.Append(" from IVRecord IV");
            str.Append(" inner join Patient PT ON PT.PatCode=IV.PatCode");
            str.Append(" where 1=1 and PT.WardCode='" + WardCode + "' and IVStatus>=3");
            if (time.ToString() != "")
                str.Append(" and DateDiff(dd,InfusionDT,'" + time + "')=0 ");
            if (batch.ToString() != "" && batch.ToString() != "<全部>")
                str.Append(" and Batch like '" + batch + "%'");
            if (type.ToString() != "" && type.ToString() != "<全部>")
                str.Append(" and Batch like '%" + type + "%'");
            str.Append(" order by BedNo");
            return str.ToString();
        }
        public string QianShou(string LabelNo)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" update IVRecord set IVStatus='15'");
            return str.ToString();
        }
        public string IsQianShou(string LabelNo) 
        {
            StringBuilder str = new StringBuilder();
            str.Append(" select * from IVRecord_QS where IVRecordID='" + LabelNo + "' ");
            return str.ToString();
        }
     

        public string AllBatch() //所有的批次
        {
            StringBuilder str = new StringBuilder();
            str.Append(" select distinct OrderID ");
            str.Append(" from DOrder order by OrderId ");
            return str.ToString();
        }

        public string AllPre(string status, string search, string WardCode,int a) //执行医嘱清单
        {
            StringBuilder str = new StringBuilder();
            str.Append("select distinct P.PatientCode,PT.BedNo,PT.PatName,PT.Sex");
            str.Append(" ,DATEDIFF(YEAR,PT.Birthday,GETDATE())as Age");
            str.Append(" from Prescription P inner join Patient PT");
            str.Append(" on P.PatientCode=PT.PatCode");
            str.Append("  left join IVRecord iv on iv.PrescriptionID=p.PrescriptionID ");
            //str.Append(" inner join DDrug DD on DD.DrugCode=P.FreqCode");
            str.Append(" where PT.WardCode='"+WardCode+"'");         
            if (status.ToString() == "执行中")
                str.Append(" and P.PStatus <3 and p.PPause=0 ");
            else if (status == "已停止")
                str.Append(" and P.PStatus='4' ");
            else if (status == "已暂停")
                str.Append(" and P.PPause='1' ");
            if (search != "" && search != "姓名/床号")
            {
                str.Append(" and( PT.PatName like '%" +search+ "%'");
                str.Append("or PT.BedNo like '%" + search + "%')");
            }
           
            if (a == 1)
            {
                str.Append(" and DATEDIFF(DD,iv.InfusionDT,GETDATE())=0 ");
            }
            str.Append(" order by PT.BedNo ");
            return str.ToString();
        }

        public string DDrug(string PreID,string status,int a) //所有药品
        {
            StringBuilder str = new StringBuilder();
            str.Append(" select PD.PrescriptionID");
            str.Append(",DF.FreqCode,DF.FreqName,PD.DrugName,P.PStatus,P.PPause,PD.Spec,convert(varchar,PD.Dosage)+PD.Dosageunit as Dosage ,UniPreparationID,DDrug.NoName ");
            str.Append(" from PrescriptionDetail PD");
            str.Append(" left join Prescription P on PD.PrescriptionID=P.PrescriptionID ");
            str.Append(" left join DDrug on DDrug.DrugCode=PD.DrugCode");
            str.Append(" left join DFreq DF on P.FregCode=DF.FreqCode");
            str.Append("  left join IVRecord iv on iv.PrescriptionID=P.PrescriptionID ");
            str.Append(" where P.PatientCode='" + PreID + "'");
            
            if (status == "执行中")
                str.Append(" and P.PStatus<>'4' ");
            else if(status=="已停止")
                str.Append(" and P.PStatus='4' ");
            else if(status=="已暂停")
                str.Append(" and P.PPause='1' ");
            if (a == 1)
            {
                str.Append(" and DATEDIFF(DD,iv.InfusionDT,GETDATE())=0 ");
            }
            str.Append(" order by PD.PrescriptionID,DDrug.NoName");
            return str.ToString();
        }


        public string LabelCheck(string LabelNo, string LastStatus) 
        {
            StringBuilder str = new StringBuilder();
            str.Append(" select a.* ,b.DEmployeeName from ");
            if(LastStatus=="已排药")
                str.Append(" IVRecord_PY a");
            else if (LastStatus == "已进仓")
                str.Append(" IVRecord_JC a");
            else if (LastStatus == "已配置")
                str.Append(" IVRecord_PZ a ");
            else if (LastStatus == "已打包")
                str.Append(" IVRecord_DB a");
            else if (LastStatus == "已出仓")
                str.Append(" IVRecord_CC a");
            else if (LastStatus == "已签收")
                str.Append(" IVRecord_QS a");
            str.Append(" left join DEmployee b on  a.PCode=b.DEmployeeID");
            str.Append(" where IVRecordID='" + LabelNo + "'");
            return str.ToString();
        }

        public string PreForLabel(string PreID, string Date) 
        {
            StringBuilder str = new StringBuilder();
            str.Append(" select * from IVRecord");
            str.Append(" where PrescriptionID='" + PreID + "'");
            str.Append("and DateDiff(dd,InfusionDT,'" + Date + "')=0");
            return str.ToString();
        }

      

        public string msg(string LabelNo)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" select LabelNo,LabelOver,LabelOverID,a.DEmployeeName as LabelOverName ,a.DEmployeeCode as LabelOverDEmpCode ,LabelOverTime ");
            str.Append("        ,WardRetreat,WardRID,b.DEmployeeName as WardRName,WardRtime,b.DEmployeeCode as WardRetreatDEmpCode ");
            str.Append(" from IVRecord  ");
            str.Append(" left join DEmployee a on  IVRecord.LabelOverID=a.DEmployeeID");
            str.Append(" left join DEmployee b on  IVRecord.WardRID=b.DEmployeeID");
            str.Append(" where LabelNo='" + LabelNo + "'    ");
            str.Append(GetHeDui(LabelNo).ToString());
            return str.ToString();
        }
        /// <summary>
        /// 取得瓶签左右核对信息
        /// </summary>
        /// <param name="LabelNo"></param>
        /// <returns></returns>
        public string GetHeDui(string LabelNo)
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT    ");
            str.Append("     '打印' as 核对 ,a.PrintCode as EID,b.DEmployeeCode AS ID ,b.DEmployeeName as  姓名 ,a.PrintDT as  时间 ,'9' as TP ,'' as Location ");
            str.Append("   FROM [IVRecord_Print] a ");
            str.Append("   left join dbo.DEmployee b on DEmployeeID = PrintCode  ");
            str.Append("   where  a.LabelNo  ='" + LabelNo + "'");
            str.Append("      AND  PrintCount='0'  ");

            str.Append("union all   ");
            str.Append("SELECT    ");
            str.Append("     '拿药' as 核对 ,a.PCode as EID,b.DEmployeeCode AS ID ,b.DEmployeeName as  姓名 ,a.YPDT as  时间 ,a.type as TP,a.Location");
            str.Append("   FROM [IVRecord_YP_ZJG] a ");
            str.Append("   left join dbo.DEmployee b on DEmployeeID = PCode  ");
            str.Append("   where  a.IVRecordID  ='" + LabelNo + "'");
            str.Append("      AND  ScanCount='0'  ");

            str.Append("union all   ");
            str.Append("SELECT    ");
            str.Append("     '贴签' as 核对 ,a.PCode as EID,b.DEmployeeCode AS ID ,b.DEmployeeName as  姓名 ,a.YSDT as  时间 ,a.type as TP,a.Location");
            str.Append("   FROM [IVRecord_YS_ZJG] a ");
            str.Append("   left join dbo.DEmployee b on DEmployeeID = PCode  ");
            str.Append("   where  a.IVRecordID  ='" + LabelNo + "'");
            str.Append("      AND  ScanCount='0'  ");

            str.Append("union all   ");
            str.Append("SELECT    ");
            str.Append("     '排药' as 核对 ,a.PCode as EID,b.DEmployeeCode AS ID ,b.DEmployeeName as  姓名 ,a.PYDT as  时间 ,a.type as TP,a.Location");
            str.Append("   FROM [IVRecord_PY] a ");
            str.Append("   left join dbo.DEmployee b on DEmployeeID = PCode  ");
            str.Append("   where  a.IVRecordID  ='" + LabelNo + "'");
            str.Append("      AND  ScanCount='0'  ");


            str.Append("union all   ");
            str.Append("SELECT    ");
            str.Append("     '进仓' as 核对 ,a.PCode as EID,b.DEmployeeCode AS ID ,b.DEmployeeName as  姓名,a.JCDT as  时间 ,a.type as TP,a.Location");
            str.Append("   FROM [IVRecord_JC] a ");
            str.Append("   left join dbo.DEmployee b on DEmployeeID = PCode  ");
            str.Append("   where  a.IVRecordID  ='" + LabelNo + "'");
            str.Append("      AND  ScanCount='0'   ");

            str.Append("union all   ");
            str.Append("SELECT    ");
            str.Append("     '配置' as 核对 ,a.PCode as EID,b.DEmployeeCode AS ID ,b.DEmployeeName as  姓名,a.PZDT as  时间 ,a.type as TP,a.Location");
            str.Append("   FROM [IVRecord_PZ] a ");
            str.Append("   left join dbo.DEmployee b on DEmployeeID = PCode  ");
            str.Append("   where  a.IVRecordID  ='" + LabelNo + "'");
            str.Append("      AND  ScanCount='0'   ");

            str.Append("union all   ");
            str.Append("SELECT    ");
            str.Append("     '出仓' as 核对 ,a.PCode as EID,b.DEmployeeCode AS ID ,b.DEmployeeName as  姓名,a.CCDT as  时间 ,a.type as TP,a.Location");
            str.Append("   FROM [IVRecord_CC] a ");
            str.Append("   left join dbo.DEmployee b on DEmployeeID = PCode  ");
            str.Append("   where  a.IVRecordID  ='" + LabelNo + "'");
            str.Append("      AND  ScanCount='0'   ");

            str.Append(" union all ");
            str.Append(" SELECT '提前打包' as 核对,a.PackID as EID,b.DEmployeeCode AS ID ,b.DEmployeeName as  姓名 ");
            str.Append(" ,a.PackTime as  时间 ,0 as TP,'' as 'Location' ");
            str.Append(" FROM [IVRecord] a   ");
            str.Append("  left join dbo.DEmployee b on DEmployeeID = a.PackID   ");
            str.Append("  where  a.LabelNo  ='" + LabelNo+"'");
            str.Append("  AND  PackAdvance=1 ");
      
            str.Append("union all   ");
            str.Append("SELECT    ");
            str.Append("     '打包' as 核对 ,a.PCode as EID,b.DEmployeeCode AS ID ,b.DEmployeeName as  姓名,a.DBDT as  时间 ,a.type as TP,a.Location");
            str.Append("   FROM [IVRecord_DB] a ");
            str.Append("   left join dbo.DEmployee b on DEmployeeID = PCode  ");
            str.Append("   where  a.IVRecordID  ='" + LabelNo + "'");
            str.Append("      AND  ScanCount='0'   ");

            str.Append("union all   ");
            str.Append("SELECT    ");
            str.Append("     '病区签收' as 核对 ,a.PCode as EID,b.DEmployeeCode AS ID ,b.DEmployeeName as  姓名,a.QSDT as  时间,a.type as TP,a.Location ");
            str.Append("   FROM [IVRecord_QS] a ");
            str.Append("   left join dbo.DEmployee b on DEmployeeID = PCode  ");
            str.Append("   where  a.IVRecordID  ='" + LabelNo + "'");
            str.Append("      AND  ScanCount='0'   ");
            return str.ToString();
        }



        public string JudgeNewInfor(string demployId,string wardCode)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" select* from QQLog   where  not exists(select  ReadId from ReadLog where RdemployId='");
            str.Append(demployId);
            str.Append("' and qqlog.id=ReadLog.ReadId ) ");
            str.Append("  and (ToDEmployid='AllWard'or (ToDEmployid='PivasMate' and SWardCode='");
            str.Append(wardCode);
            str.Append("') or ToDEmployid='");
            str.Append(wardCode);
            str.Append("') and DEmployeeID!='");
            str.Append(demployId);
            str.Append("'");

            str.Append("  select top 1 Content from QQLog where DATEDIFF(DD,InsertTime,GETDATE())<=3 and SType=1  order by InsertTime desc");
            return str.ToString();

        }
    }
    
    
    
}
