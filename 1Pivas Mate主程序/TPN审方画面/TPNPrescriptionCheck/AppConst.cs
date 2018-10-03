using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace TPNReview
{
    /// <summary>
    /// 常量类
    /// </summary>
    class AppConst
    {
        public const string SYSTEM_ID = "CPMATE";
        //public const string DB_CONFIG = "bl_server.lcf";

        public static string LoginEmpCode = "";
        public static string LoginEmpRole = "";
        public static string DEmployeeID = "";//员工表ID，从1自增
        public static BLPublic.DBOperate db = null;

        public static Window winMain = null;
    }

    /// <summary>
    /// 常用方法类
    /// </summary>
    class ComClass
    {
        public static Dictionary<string, string> Acounts = new Dictionary<string, string>();

        public static string getEmpName(string _empCode)
        {
            if ((null == Acounts) || string.IsNullOrWhiteSpace(_empCode) || 
                Acounts.Count < 1 || !Acounts.ContainsKey(_empCode))
                return "";
            else
                return Acounts[_empCode];
        }

        /// <summary>
        /// 获取员工姓名
        /// </summary>
        /// <param name="_db"></param>
        /// <param name="_empCode"></param>
        /// <returns></returns>
        public static string getEmpName(BLPublic.DBOperate _db, string _empCode)
        {
            if ((null == _db) || string.IsNullOrWhiteSpace(_empCode))
                return "";

            string rt = "";
            IDataReader dr = null;
            if (_db.GetRecordSet(string.Format(SQL.SEL_EMPNAME, _empCode), ref dr))
            {
                if (dr.Read())
                    rt = dr.GetString(0);
                dr.Close();
            }

            return rt;
        }

        /// <summary>
        /// 获取<员工账户,姓名>字典集合
        /// </summary>
        public static bool getAcountNameList(BLPublic.DBOperate _db, Dictionary<string, string> lstAcount)
        {
            if ((null == _db) || (null == lstAcount))
                return false;

            lstAcount.Clear();

            bool rt = false;
            IDataReader dr = null;
            if (_db.GetRecordSet(SQL.SEL_ACOUNTNAME, ref dr))
            {
                while (dr.Read())
                    if (!lstAcount.ContainsKey(dr.GetString(0)))
                        lstAcount.Add(dr.GetString(0), dr.GetString(1));
                dr.Close(); 
                rt = true;
            }

            return rt;
        }

        public static string getDateTimeFmt(DateTime _dt)
        {
            if (DateTime.MinValue == _dt)
                return "";
            else
                return _dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string getDateTimeFmtM(DateTime _dt)
        {
            if (DateTime.MinValue == _dt)
                return "";
            else
                return _dt.ToString("yyyy-M-d H:mm");
        }

        /// <summary>
        /// 1=男，2=女
        /// </summary>
        /// <param name="_sex"></param>
        /// <returns></returns>
        public static string getZhSex(string _sex)
        {
            if (_sex.Equals("1"))
                return "男";
            else
                return "女";

        }
    }
}
