using System;
using System.Text;
using System.Data;
using PIVAsCommon.Helper;

namespace DrugCollectionForPrint.dao
{
    class sqldb
    {
        DB_Help DB = new DB_Help();
        DataSet ds;

        /// <summary>
        /// 药品统计
        /// </summary>
        /// <param name="date1">日期1</param>
        /// <param name="date2">日期2</param>
        /// <param name="ivstatus1">状态1</param>
        /// <param name="ivstatus2">状态2</param>
        /// <param name="labelover">配置取消</param>
        /// <returns></returns>
        public DataSet DrugSelect(string date1, string date2, string ivstatus1, string ivstatus2, bool labelover,bool K)
        {
            ds = new DataSet();
            StringBuilder str = new StringBuilder();
            StringBuilder con = new StringBuilder();
            try
            {
                if (ivstatus1 == ivstatus2)
                {
                    con.Append("   IVStatus = " + ivstatus1 + "");
                }
                else
                {
                    con.Append("   IVStatus between " + ivstatus1 + " and " + ivstatus2 + "");
                }

                if (!labelover)
                {
                    con.Append("   and LabelOver >=0 ");
                }

                if (!K)
                {
                    con.Append("    and batch not like '%K%'");
                }
                if (date1 == date2)
                {
                    con.Append("   and  datediff(DD, InfusionDT,'"+date1+"')=0 ");

                }
                else
                {
                    con.Append("   and  InfusionDT  between '" + date1 + "' and '" + date2 + "'");
                }

                str.Append("   select distinct d.PortNo,D.DrugName,D.DrugNameJC,D.Spec,SUM(DgNo) num  ");
                str.Append("   FROM IVRecord iv inner join IVRecordDetail ivd on iv.IVRecordID=ivd.IVRecordID");
                str.Append("  inner join DDrug d on ivd.DrugCode=d.DrugCode ");
                str.Append("   where " + con.ToString() + " ");
                str.Append(" GROUP BY D.DrugCode,D.DrugName,D.DrugNameJC,D.Spec,PortNo,d.FormUnit ");
                str.Append("   order by PortNo ");

                ds = DB.GetPIVAsDB(str.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("方法DrugSelect:", ex);
            }

            return ds;
        }

        /// <summary>
        /// 载入IVstatus
        /// </summary>
        /// <returns></returns>
        public DataSet IVStatusSelect()
        {
            ds = new DataSet();
            try
            {
                string sql = "select CheckID,CheckName from PivasCheckFormSet where CheckID between 0 and 15";

                ds = DB.GetPIVAsDB(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("方法IVStatusSelect:", ex);
            }

            return ds;
        }


    }
}
