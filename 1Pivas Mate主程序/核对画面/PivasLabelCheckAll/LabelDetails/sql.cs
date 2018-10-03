using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivasLabelCheckAll.LabelDetails
{
    class sql
    {
        public string msg(string LabelNo)
        {
            StringBuilder str = new StringBuilder();
            str.Append("  select LabelNo,Batch,IVStatus,TeamNumber,pa.WardCode,d.WardName, ISNULL( Remark5,'长') as 'KL',InfusionDT ");
            str.Append("  ,LabelOver,LabelOverID,a.DEmployeeName as LabelOverName ,a.DEmployeeCode as LabelOverDEmpCode  ");
            str.Append(" ,LabelOverTime ,WardRetreat,WardRID,b.DEmployeeName as WardRName,WardRtime ,b.DEmployeeCode as WardRetreatDEmpCode from IVRecord  ");
            str.Append(" left join DEmployee a on  IVRecord.LabelOverID=a.DEmployeeID");
            str.Append(" left join DEmployee b on  IVRecord.WardRID=b.DEmployeeID");
            str.Append(" left join Patient pa on pa.PatCode=IVRecord.PatCode ");
            str.Append("   left join DWard d on d.WardCode=pa.WardCode ");
            str.Append("  ");
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
            str.Append("  where  a.LabelNo  ='" + LabelNo + "'");
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
    }
}
