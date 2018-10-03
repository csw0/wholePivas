using System.Text;
using System.Data;
using PIVAsCommon.Helper;

namespace MistakeCollection.Dao
{
    class sql
    {
        DB_Help DB = new DB_Help();
        DataSet DS = null;

        /// <summary>
        /// 差错细节
        /// </summary>
        /// <param name="labelno1"></param>
        /// <param name="labelno2"></param>
        /// <returns></returns>
        public DataSet MistakeDetails(string labelno1, string labelno2,string Date ,string ErrorID,string FindID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("    SELECT  [PreLabCode] as 瓶签号 ,[Describe] as 描述,ers.StatusName as 差错节点,fds.StatusName 记录节点,isnull(de.DEmployeeName,'无') as 差错人,isnull(de1.DEmployeeName,'无') as 记录人,[ErrorTime] as 差错时间,[FindTime] as 记录时间,ers.TypeName as 错误类型");
            str.Append("    FROM [ErrorRecord] er");
            str.Append("    left join ErrorRule ers on er.ErrorStatus=ers.StatusCode and er.ErrorType=ers.TypeCode");
            str.Append("    left join ErrorRule fds on er.FindStatus=fds.StatusCode ");
            str.Append("    left join DEmployee de on er.ErrorDEmployeeID=de.DEmployeeID ");
            str.Append("    left join DEmployee de1 on er.FindDEmployeeID=de1.DEmployeeID");
            str.Append("    where PreLabCode between '" + labelno1 + "' and '" + labelno2 + "' ");

            if (Date != "")
            {
                str.Append("    and PreLabCode like '" + Date + "%'");
            }

            if (ErrorID == "无")
            {
                str.Append("    and er.ErrorDEmployeeID=''");
            }
            else if (ErrorID != "")
            {
                str.Append("    and de.DEmployeeName='" + ErrorID + "'");
            }
            

            if (FindID == "无")
            {
                str.Append("    and er.FindDEmployeeID=''");
            }
            else if (FindID != "")
            {
                str.Append("    and de1.DEmployeeName='" + FindID + "'");
            }


            DS = new DataSet();
            DS = DB.GetPIVAsDB(str.ToString());

            return DS;

        }

        /// <summary>
        /// 按日期统计
        /// </summary>
        /// <param name="labelno1"></param>
        /// <param name="labelno2"></param>
        /// <returns></returns>
        public DataSet CountByDate(string labelno1, string labelno2)
        {
            StringBuilder str = new StringBuilder();
            str.Append("    Select  SUBSTRING(prelabcode,1,8)as 日期,COUNT(* ) as 数量 from ErrorRecord");
            str.Append("    where PreLabCode  between '" + labelno1 + "' and '" + labelno2 + "'");
            str.Append("    group by SUBSTRING(prelabcode,1,8) ");

            DS = new DataSet();
            DS = DB.GetPIVAsDB(str.ToString());

            return DS;
        }

        /// <summary>
        /// 按差错人统计
        /// </summary>
        /// <param name="labelno1"></param>
        /// <param name="labelno2"></param>
        /// <returns></returns>
        public DataSet CountByErrorID(string labelno1, string labelno2)
        {
            StringBuilder str = new StringBuilder();
            str.Append("    Select  isnull(de.DEmployeeName,'无')as 差错人,COUNT(* ) as 数量 from ErrorRecord er");
            str.Append("    left join DEmployee de on er.ErrorDEmployeeID=de.DEmployeeID");
            str.Append("    where PreLabCode  between '" + labelno1 + "' and '" + labelno2 + "'");
            str.Append("    group by de.DEmployeeName ");

            DS = new DataSet();
            DS = DB.GetPIVAsDB(str.ToString());

            return DS;
        }

        /// <summary>
        /// 按记录人统计
        /// </summary>
        /// <param name="labelno1"></param>
        /// <param name="labelno2"></param>
        /// <returns></returns>
        public DataSet CountByFindID(string labelno1, string labelno2)
        {
            StringBuilder str = new StringBuilder();
            str.Append("    Select  isnull(de.DEmployeeName,'无')as 记录人,COUNT(* ) as 数量 from ErrorRecord er");
            str.Append("    left join DEmployee de on er.FindDEmployeeID=de.DEmployeeID");
            str.Append("    where PreLabCode  between '" + labelno1 + "' and '" + labelno2 + "'");
            str.Append("    group by de.DEmployeeName ");

            DS = new DataSet();
            DS = DB.GetPIVAsDB(str.ToString());

            return DS;
        }
    }
}
