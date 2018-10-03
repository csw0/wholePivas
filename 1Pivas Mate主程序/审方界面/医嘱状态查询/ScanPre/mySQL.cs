using System;
using System.Collections.Generic;
using System.Text;

namespace ScanPre
{
    public class mySQL
    {

        public string INFO(string PreID)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" SELECT DISTINCT P.PrescriptionID,P.DoctorCode,PiShi,P.CaseID,P.FregCode,P.Level,P.PStatus, ");
            str.Append(" DE1.DEmployeeName DocName,D.WardName,PD.Dosage,PD.DosageUnit,PD.EndDT,PD.StartDT ,dd.UniPreparationID,DD.DrugName,DD.Spec, ");
            str.Append(" Pa.BedNo,Pa.PatName,Pa.Sex,Pa.Height, Pa.Weight,(Convert(varchar,Pa.Age,120)+Pa.AgeSTR) as Birthday,");
            str.Append(" NoName FROM Prescription P ");
            str.Append(" INNER JOIN PrescriptionDetail PD ON P.PrescriptionID=PD.PrescriptionID ");
            str.Append(" LEFT JOIN DEmployee DE1 ON P.DoctorCode=DE1.DEmployeeCode ");
            str.Append(" INNER JOIN DWard D ON D.WardCode=P.WardCode ");
            str.Append(" INNER JOIN Patient Pa ON Pa.PatCode=P.PatientCode ");
            //=======================================
            str.Append(" left JOIN DDrug DD ON DD.DrugCode=PD.DrugCode and DD.Spec=PD.Spec ");
            //=======================================
            str.Append(" WHERE P.PrescriptionID = " + PreID);
            str.Append(" order by NoName");
            return str.ToString();
        }

        public string SelDetail(string sys, string per, string PatCode, string time)//按时间选个人未审医嘱
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT DISTINCT P.PrescriptionID, P.Level,P.InceptDT,P.FregCode,P.GroupNo,P.CaseID,P.PStatus, ");
            str.Append("DE.DEmployeeName,P.DoctorCode,D.WardCode,D.WardName, DD.PiShi,DD.Spec,P.Attention, ");
            str.Append("Pa.PatCode,Pa.PatName,(Convert(varchar,Pa.Age,120)+Pa.AgeSTR) as Birthday,Pa.BedNo,Pa.Sex,Pa.Weight,");
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
            str.Append("WHERE PatCode='" + PatCode + "' AND DATEDIFF(DD,P.InceptDT,'" + time + "')>=0 ");
            str.Append("AND P.Level IN (" + sys + ") AND P.PStatus IN (" + per + ") ");
            str.Append("ORDER BY P.PrescriptionID ");

            return str.ToString();
        }

        public string SelAllDetail(string PatCode, string per, string time)//按时间选个人所有医嘱
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT DISTINCT P.PrescriptionID, P.Level,P.InceptDT,P.FregCode,P.GroupNo,P.CaseID,P.PStatus, ");
            str.Append("DE.DEmployeeName,P.DoctorCode,D.WardCode,D.WardName, DD.PiShi,DD.Spec,P.Attention, ");
            str.Append("Pa.PatCode,Pa.PatName,(Convert(varchar,Pa.Age,120)+Pa.AgeSTR) as Birthday,Pa.BedNo,Pa.Sex,Pa.Weight,");
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
            str.Append("WHERE PatCode='" + PatCode + "' AND DATEDIFF(DD,P.InceptDT,'" + time + "')>=0 ");
            str.Append("AND P.PStatus NOT IN (" + per + ")");
            str.Append("ORDER BY P.PrescriptionID ");

            return str.ToString();
        }

        public string msg(string LabelNo)
        {
            StringBuilder str = new StringBuilder();
            //str.Append(" select ");
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
            str.Append("   a.LabelNo as 瓶签号 ,'打印' as 核对 ,a.PrintCode as EID,b.DEmployeeCode AS ID ,b.DEmployeeName as  姓名 ,a.PrintDT as  时间 ,'9' as TP ,'' as Location ");
            str.Append("   FROM [IVRecord_Print] a ");
            str.Append("   left join dbo.DEmployee b on DEmployeeID = PrintCode  ");
            str.Append("   where  a.LabelNo  ='" + LabelNo + "'");
            str.Append("      AND  PrintCount='0'  ");

            str.Append("union all   ");
            str.Append("SELECT    ");
            str.Append("   a.IVRecordID as 瓶签号, '拿药' as 核对 ,a.PCode as EID,b.DEmployeeCode AS ID ,b.DEmployeeName as  姓名 ,a.YPDT as  时间 ,a.type as TP,a.Location");
            str.Append("   FROM [IVRecord_YP_ZJG] a ");
            str.Append("   left join dbo.DEmployee b on DEmployeeID = PCode  ");
            str.Append("   where  a.IVRecordID  ='" + LabelNo + "'");
            str.Append("      AND  ScanCount='0'  ");

            str.Append("union all   ");
            str.Append("SELECT    ");
            str.Append("    a.IVRecordID as 瓶签号,  '贴签' as 核对 ,a.PCode as EID,b.DEmployeeCode AS ID ,b.DEmployeeName as  姓名 ,a.YSDT as  时间 ,a.type as TP,a.Location");
            str.Append("   FROM [IVRecord_YS_ZJG] a ");
            str.Append("   left join dbo.DEmployee b on DEmployeeID = PCode  ");
            str.Append("   where  a.IVRecordID  ='" + LabelNo + "'");
            str.Append("      AND  ScanCount='0'  ");

            str.Append("union all   ");
            str.Append("SELECT    ");
            str.Append("    a.IVRecordID as 瓶签号, '排药' as 核对 ,a.PCode as EID,b.DEmployeeCode AS ID ,b.DEmployeeName as  姓名 ,a.PYDT as  时间 ,a.type as TP,a.Location");
            str.Append("   FROM [IVRecord_PY] a ");
            str.Append("   left join dbo.DEmployee b on DEmployeeID = PCode  ");
            str.Append("   where  a.IVRecordID  ='" + LabelNo + "'");
            str.Append("      AND  ScanCount='0'  ");


            str.Append("union all   ");
            str.Append("SELECT    ");
            str.Append("    a.IVRecordID as 瓶签号, '进仓' as 核对 ,a.PCode as EID,b.DEmployeeCode AS ID ,b.DEmployeeName as  姓名,a.JCDT as  时间 ,a.type as TP,a.Location");
            str.Append("   FROM [IVRecord_JC] a ");
            str.Append("   left join dbo.DEmployee b on DEmployeeID = PCode  ");
            str.Append("   where  a.IVRecordID  ='" + LabelNo + "'");
            str.Append("      AND  ScanCount='0'   ");

            str.Append("union all   ");
            str.Append("SELECT    ");
            str.Append("    a.IVRecordID as 瓶签号, '配置' as 核对 ,a.PCode as EID,b.DEmployeeCode AS ID ,b.DEmployeeName as  姓名,a.PZDT as  时间 ,a.type as TP,a.Location");
            str.Append("   FROM [IVRecord_PZ] a ");
            str.Append("   left join dbo.DEmployee b on DEmployeeID = PCode  ");
            str.Append("   where  a.IVRecordID  ='" + LabelNo + "'");
            str.Append("      AND  ScanCount='0'   ");

            str.Append("union all   ");
            str.Append("SELECT    ");
            str.Append("    a.IVRecordID as 瓶签号, '出仓' as 核对 ,a.PCode as EID,b.DEmployeeCode AS ID ,b.DEmployeeName as  姓名,a.CCDT as  时间 ,a.type as TP,a.Location");
            str.Append("   FROM [IVRecord_CC] a ");
            str.Append("   left join dbo.DEmployee b on DEmployeeID = PCode  ");
            str.Append("   where  a.IVRecordID  ='" + LabelNo + "'");
            str.Append("      AND  ScanCount='0'   ");

            str.Append(" union all ");
            str.Append(" SELECT a.LabelNo as 瓶签号,'提前打包' as 核对,a.PackID as EID,b.DEmployeeCode AS ID ,b.DEmployeeName as  姓名 ");
            str.Append(" ,a.PackTime as  时间 ,0 as TP,'' as 'Location' ");
            str.Append(" FROM [IVRecord] a   ");
            str.Append("  left join dbo.DEmployee b on DEmployeeID = a.PackID   ");
            str.Append("  where  a.LabelNo  ='" + LabelNo + "'");
            str.Append("  AND  PackAdvance=1 ");

            str.Append("union all   ");
            str.Append("SELECT    ");
            str.Append("   a.IVRecordID as 瓶签号,  '打包' as 核对 ,a.PCode as EID,b.DEmployeeCode AS ID ,b.DEmployeeName as  姓名,a.DBDT as  时间 ,a.type as TP,a.Location");
            str.Append("   FROM [IVRecord_DB] a ");
            str.Append("   left join dbo.DEmployee b on DEmployeeID = PCode  ");
            str.Append("   where  a.IVRecordID  ='" + LabelNo + "'");
            str.Append("      AND  ScanCount='0'   ");

            str.Append("union all   ");
            str.Append("SELECT    ");
            str.Append("   a.IVRecordID as 瓶签号,  '病区签收' as 核对 ,a.PCode as EID,b.DEmployeeCode AS ID ,b.DEmployeeName as  姓名,a.QSDT as  时间,a.type as TP,a.Location ");
            str.Append("   FROM [IVRecord_QS] a ");
            str.Append("   left join dbo.DEmployee b on DEmployeeID = PCode  ");
            str.Append("   where  a.IVRecordID  ='" + LabelNo + "'");
            str.Append("      AND  ScanCount='0'   ");


            str.Append("   SELECT   ");
            str.Append("   c.labelno as 瓶签号, '审方'as 审方,a.CheckDCode as EID,b.DEmployeeCode AS ID,b.DEmployeeName as 姓名,a.CheckDT as 时间, a .IsPass as TP,''as Location");
            str.Append("   FROM [CPRecord] a");
            str.Append("   left join dbo.DEmployee b on DEmployeeID = CheckDCode  ");
            str.Append("   left join IVRecord c on a.PrescriptionID =c.PrescriptionID");
            str.Append("   where c.labelno='" + LabelNo + "'");
            return str.ToString();
        }

        

    }
}
