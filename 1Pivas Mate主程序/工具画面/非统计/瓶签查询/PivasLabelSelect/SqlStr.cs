using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivasLabelSelect
{
    class SqlStr
    {

        public string INFO(string PreID,string labelno)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("SELECT DISTINCT P.PrescriptionID,P.DoctorCode,P.CaseID,P.FregCode,P.Level,P.PStatus,p.UsageName, ");
            str.AppendLine("DE1.DEmployeeName DocName,DrawerCode,DE2.DEmployeeName DrawerName,D.WardName,PD.EndDT,PD.StartDT, ");
            str.AppendLine("Pa.BedNo,Pa.PatName,Pa.Sex, Pa.Weight,Pa.Height,Pa.Birthday,Pa.Age,Pa.AgeSTR,pd.PrescriptionDetailID  FROM Prescription P ");
            str.AppendLine("left JOIN PrescriptionDetail PD ON P.PrescriptionID=PD.PrescriptionID ");
            str.AppendLine("left JOIN DEmployee DE1 ON P.DoctorCode=DE1.DEmployeeCode ");
            str.AppendLine("left JOIN DEmployee DE2 ON P.DrawerCode=DE2.DEmployeeCode ");
            str.AppendLine("left JOIN DWard D ON D.WardCode=P.WardCode ");
            str.AppendLine("left JOIN Patient Pa ON Pa.PatCode=P.PatientCode ");
            str.AppendLine("WHERE P.PrescriptionID ={0}  ");

            str.AppendLine("select  dd.DrugCode,dd.DrugName,DD.Spec,dd.NoName,PiShi,  DD.UniPreparationID,ivd.Dosage,ivd.DosageUnit,ivd.dgno,ivd.IVRecodedDetaiID,1 as iscancel ");
            str.AppendLine(" from IVRecord iv ");
            str.AppendLine("left join IVRecordDetail ivd on ivd.IVRecordID=iv.IVRecordID ");
            str.AppendLine("left join DDrug dd on dd.DrugCode=ivd.DrugCode ");
            str.AppendLine("where LabelNo='{1}' and ivd.DrugCode is not null ");
            str.AppendLine("   union all  ");
            str.AppendLine("select  dd.DrugCode,dd.DrugName,DD.Spec,dd.NoName,PiShi,  DD.UniPreparationID,ivd.Dosage");
            str.AppendLine(",ivd.DosageUnit,ivd.dgno,ivd.IVRecodedDetaiID, 0 as iscancel from IVRecord iv ");
            str.AppendLine(" left join IVRecord_DrugDeleteLog ivd on ivd.LabelNo=iv.LabelNo  ");
            str.AppendLine("  left join DDrug dd on dd.DrugCode=ivd.DrugCode  ");
            str.AppendLine("  where  iv.LabelNo='{1}' and ivd.DrugCode is not null ");
            str.AppendLine("order by iscancel desc, dd.NoName ");      
            return string.Format( str.ToString(),PreID,labelno);
        }


        public string msg(string LabelNo) 
        {
            StringBuilder str = new StringBuilder();
            str.Append(" select LabelNo,LabelOver,LabelOverID,a.DEmployeeName as LabelOverName ,a.DEmployeeCode as LabelOverDEmpCode ,LabelOverTime ");
            str.Append("        ,WardRetreat,WardRID,b.DEmployeeName as WardRName,WardRtime,b.DEmployeeCode as WardRetreatDEmpCode");
            str.Append(" ,PackAdvance,PackID,PackTime,c.DEmployeeName as PackName,c.DEmployeeCode as PackDEmpCode");
            str.Append(" from V_IVRecord  ");
            str.Append(" left join DEmployee a on  V_IVRecord.LabelOverID=a.DEmployeeID");
            str.Append(" left join DEmployee b on  V_IVRecord.WardRID=b.DEmployeeID");
            str.Append(" left join DEmployee c on  V_IVRecord.PackID=c.DEmployeeID");
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
            str.Append("SELECT '审方' as 核对 ,CD.CheckDCode as EID,b.DEmployeeCode AS ID ,");
            str.Append("b.DEmployeeName as  姓名,CD.CheckDT as  时间,'1' as TP,'' as Location ");
            str.Append("FROM V_IVRecord IV ");
            str.Append("left join Prescription P on IV.GroupNo = P.GroupNo ");
            str.Append("left join CPRecord CD ON CD.PrescriptionID = P.PrescriptionID ");
            str.Append("left join dbo.DEmployee b on DEmployeeID = CD.CheckDCode  ");
            str.Append("where  IV.LabelNo = '" + LabelNo + "'");

            str.Append("union all   ");
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

        public string DeleteLabelDrug(string employID,string labelNo, string IVRecodedDetaiID)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("insert into IVRecord_DrugDeleteLog(IVRecodedDetaiID, [LabelNo],[DrugCode],[DemoloyID],[Insertdt],[Dosage] ");
            str.AppendLine(",[DosageUnit],[DgNo],[ReturnFromHis],[Remark7],[Remark8],[Remark9],[Remark10],[RecipeNo]) ");
            str.AppendLine("  select IVRecodedDetaiID, '{0}' ,[DrugCode],'{1}',GETDATE() ,[Dosage],[DosageUnit],[DgNo],[ReturnFromHis] ");
            str.AppendLine(",[Remark7],[Remark8],[Remark9],[Remark10],[RecipeNo] from ivrecorddetail where IVRecodedDetaiID='{2}' ");
           
            str.AppendLine(" delete from IVRecordDetail where IVRecodedDetaiID='{2}' ");

            return string.Format(str.ToString(),labelNo,employID,IVRecodedDetaiID);
        }
      
    }
}
