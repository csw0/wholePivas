using PIVAsCommon;
using PIVAsCommon.Helper;
using PivasHisInterface;
using System;
using System.Data;

namespace SynServer
{
    /// <summary>
    /// 同步程序通用类
    /// </summary>
    internal sealed class CommonFunc
    {
        private DB_Help dbHelp = new DB_Help();

        /// <summary>
        /// 根据类别从Pivas数据库中查询同步his数据的脚本
        /// 这里需优化，减少访问数据库的次数（可以首次加载）
        /// </summary>
        /// <param name="SynCode"></param>
        /// <returns></returns>
        private DataSet GetSynHISDataScript(string SynCode)
        {
            if (!string.IsNullOrEmpty(SynCode))
            {
                return dbHelp.GetPIVAsDB("SELECT ss.* ,CONVERT(varchar(20), getdate(), 114) as Times FROM SynSet ss WHERE SynCode='" + SynCode + "'");
            }
            else
            {
                return new DataSet();
            }
        }

        /// <summary>
        /// 执行同步HIS的脚本；从HIS返回最新基础信息
        /// </summary>
        /// <param name="SynCode">同步ID</param>
        /// <returns></returns>
        internal DataSet GetBaseInfoByHIS(string SynCode)
        {
            DataSet ds = new DataSet();
            try
            {
                if (!string.IsNullOrEmpty(SynCode))
                {
                    using (DataSet info = GetSynHISDataScript(SynCode))
                    {
                        if (info != null && info.Tables.Count > 0 && info.Tables[0].Rows.Count > 0)
                        {
                            string strSQl = info.Tables[0].Rows[0]["Sql"].ToString().Trim();
                            if (String.IsNullOrEmpty(strSQl))
                                return ds;
                            
                            //判断采用何种方式与HIS数据同步
                            switch (info.Tables[0].Rows[0]["SynMode"].ToString().Trim().ToUpper())
                            {
                                case "SQLSERVER"://HIS数据库是SQlServer
                                    ds = dbHelp.GetPIVAsHISDBSQL(strSQl);
                                    break;
                                case "ORACLE":
                                    ds = dbHelp.GetHisOracleByOLEDB(strSQl);
                                    break;
                                case "WEBSERVICE":
                                    DataTable tempDS = new PivasHisCommOrigin().Syn(SynCode);
                                    if (tempDS != null && tempDS.Rows.Count > 0)//服务返回的不是空表
                                        ds.Tables.Add(tempDS);
                                    break;
                                default: break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("执行同步HIS的脚本,从HIS返回最新基础信息出错：" + ex.Message);
            }
            return ds;
        }

        /// <summary>
        /// 插入SynLog表，开始同步(同步开始日志)
        /// </summary>
        /// <param name="SynCode">同步类别</param>
        /// <param name="RandomID">随机值</param>
        internal void InsertStartSyLog(string SynCode, string RandomID)
        {
            if (!string.IsNullOrEmpty(SynCode))
            {
                dbHelp.SetPIVAsDB("insert into SynLog (SynCode,RandomID,StartTime,SynAct,[Schedule],[ScheduleTxt])"+
                    " values ('" + SynCode + "','" + RandomID + "',getdate(),'-888888','0','等待同步')");
            }
        }
    }
}
