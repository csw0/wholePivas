using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorRecord
{
    public class SQL
    {
        public string GetResult(string PreID)// 查审方结果
        {
            StringBuilder str = new StringBuilder();
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

        public string SelPreInfo(string PreID)//医嘱信息
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT DISTINCT P.PrescriptionID,p.DoctorCode,P.CaseID,P.FregCode, ");
            str.Append("DE1.DEmployeeName DocName,DrawerCode,DE2.DEmployeeName DrawerName,PD.EndDT,PD.StartDT,");
            str.Append("iv.WardCode ivWardCode,iv.WardName ivWardName ,iv.BedNo ivBedNo,D.WardName DWardName, ");
            str.Append("Pa.WardCode DWardCode,Pa.BedNo PaBedNo,Pa.PatName,Pa.Sex, Pa.Weight,Pa.Birthday FROM Prescription P  ");
            str.Append("left JOIN PrescriptionDetail PD ON P.PrescriptionID=PD.PrescriptionID ");
            str.Append("left join IVRecord iv on iv.PrescriptionID = P.PrescriptionID ");
            str.Append("left JOIN DEmployee DE1 ON P.DoctorCode=DE1.DEmployeeCode ");
            str.Append("left JOIN DEmployee DE2 ON P.DrawerCode=DE2.DEmployeeCode ");
            str.Append("left JOIN DWard D ON D.WardCode=P.WardCode ");
            str.Append("left JOIN Patient Pa ON Pa.PatCode=P.PatientCode ");
            str.Append("LEFT JOIN CPRecord CD ON CD.PrescriptionID=P.PrescriptionID ");
            str.Append("LEFT JOIN CPResult CT ON CT.CheckRecID=CD.CPRecordID ");
            str.Append("WHERE P.PrescriptionID = " + PreID);

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
            str.Append("  where  a.LabelNo  ='" + LabelNo+"'");
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
        public string Getsf(string PreID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("   SELECT   ");
            str.Append("   a.PrescriptionID as 医嘱号, '审方'as 审方,a.CheckDCode as EID,b.DEmployeeCode AS ID,b.DEmployeeName as 姓名,a.CheckDT as 时间, a .IsPass as TP,''as Location");
            str.Append("   FROM [CPRecord] a");
            str.Append("   left join dbo.DEmployee b on DEmployeeID = CheckDCode  ");
            str.Append("   where a.PrescriptionID ='" + PreID + "'");
            return str.ToString();

        }
    }
    
    
    
}
