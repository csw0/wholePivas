using PIVAsCommon.Helper;
using System;
using System.Data;

namespace ItemDamageCount.Dao
{
    class sql
    {
        DB_Help DB = new DB_Help();

        /// <summary>
        /// 插入记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string InsertRemark(ItemDamageCount.Class.Item item)
        {
            string result;
            string sql;
            try
            {
                sql = "Insert into ItemDamage values";
                sql += "('" + item.Drugcode + "'";
                sql += ",'" + item.Drugname + "'";
                sql += ",'" + item.Spec + "'";
                sql += ",'" + item.Count + "'";
                sql += ",'" + item.Money + "'";
                sql += ",'" + item.Reason + "'";
                sql += ",'" + item.Responsibilityid + "'";
                sql += ",'" + item.Responsibilityer + "'";
                sql += ",'" + item.Reportid + "'";
                sql += ",'" + item.Reporter + "'";
                sql += ",'" + item.Damagetime + "'";
                sql += ",'" + item.Date + "')";

                result = DB.SetPIVAsDB(sql) != 0 ? "插入成功" : "插入失败";
            }
            catch (Exception ex)
            {
                result = "插入出错";
                throw ex;
            }
         
            return result;

        }

        /// <summary>
        /// 用日期搜索日志
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public DataSet SelectRemarkByDate(string date1 ,string date2)
        {
            string sql;
            DataSet ds=null;
            try
            {
                sql = "select * from ItemDamage where ";
                if (date1 == date2)
                {
                    sql += " datediff(DD,date,'" + date1 + "')=0";
                }
                else
                {
                    sql += "date Between '" + date1 + "' and '" + date2 + "'";
                }
                sql += " order by ID ";

                ds = DB.GetPIVAsDB(sql);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return ds;
        }


        /// <summary>
        /// 查询药品
        /// </summary>
        /// <returns></returns>
        public DataTable SelectDrug()
        {
            string sql;
            DataTable dt = null;

            try
            {
                sql = "select DrugCode,DrugName from DDrug";
                dt = DB.GetPIVAsDB(sql).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        /// <summary>
        /// 查询员工
        /// </summary>
        /// <returns></returns>
        public DataTable SelectEmployee()
        {
            string sql;
            DataTable dt = null;

            try
            {
                sql = "select Demployeeid,DemployeeCode,DemployeeName from Demployee";
                dt = DB.GetPIVAsDB(sql).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        /// <summary>
        /// 查询规格
        /// </summary>
        /// <returns></returns>
        public DataTable SelectSpec()
        {
            string sql;
            DataTable dt = null;
            try
            {
                sql = "select Distinct Spec from DDrug";
                dt = DB.GetPIVAsDB(sql).Tables[0];

            }
            catch(Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public void DeleteItem(ItemDamageCount.Class.Item item)
        {
            string sql;
            try
            {
                sql = "Delete from ItemDamage where id='" + item.Id + "' ";
                DB.SetPIVAsDB(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
