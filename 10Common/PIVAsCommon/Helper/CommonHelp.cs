using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PIVAsCommon.Helper
{
    public class CommonHelp
    {
        /// <summary>
        /// 获取DataTable前N条数据
        /// </summary>
        /// <param name="TopItem">Top N</param>
        /// <param name="oDT">DataTable</param>
        /// <returns></returns>
        public static DataTable DtSelectTop(int TopItem, DataTable oDT)
        {
            try
            {
                if (oDT.Rows.Count < TopItem) return oDT;

                DataTable NewTable = oDT.Clone();
                DataRow[] rows = oDT.Select("1=1");
                for (int i = 0; i < TopItem; i++)
                {
                    NewTable.ImportRow((DataRow)rows[i]);
                }
                return NewTable;
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("获取DataTable前N条数据出错：" + ex.Message);
            }
            return (new DataTable());
        }
    }
}
