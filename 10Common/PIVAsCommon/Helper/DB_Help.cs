using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Web.Security;

namespace PIVAsCommon.Helper
{
    /// <summary>
    /// 新版Pivas数据库依赖DLL
    /// </summary>
    public class DB_Help
    {
        #region PIVAs数据库连接配置
        private string PIVAsPath = Application.StartupPath +"\\IMEQPIVAs.ini";
        private string PIVAsHISPath =  Application.StartupPath +"\\IMEQPIVAsHIS.ini";
        private string PIVAsInfo;//pivas数据库（Sql）连接字符串，因pivas目前只采用sql数据库
        private string PIVAsHISInfo;//HIS数据库（Sql）连接字符串
        private string PIVAsHISOracle_Client;//HIS数据库（Oracle）连接字符串，采用System.Data.OracleClient.dll不建议使用
        private string PIVAsHISOracle_OLEDB; //HIS数据库（Oracle）连接字符串，采用微软提供的OLEDB驱动
        public DB_Help()
        {
            DatebasePIVAsInfo();
            DatebasePIVAsHISInfo();
            DatebasePIVAsHISOracle();
        }
        /// <summary>
        /// 连接数据库配置
        /// </summary>
        /// <returns></returns>
        public string DatebasePIVAsInfo()
        {
            try
            {
                SqlConnectionStringBuilder buf = new SqlConnectionStringBuilder();
                buf.DataSource = IniReadValuePivas("Database", "Data Source");
                buf.InitialCatalog = IniReadValuePivas("Database", "Initial Catalog");
                buf.UserID = Decrypt(IniReadValuePivas("Database", "User ID"));
                buf.Password = Decrypt(IniReadValuePivas("Database", "Password"));
                PIVAsInfo = buf.ConnectionString;
           
                return buf.ConnectionString;
            }
            catch(Exception ex)
            {
                InternalLogger.Log.Error("获取pivas数据库连接参数出错"+ ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// 连接HISSQL数据库配置
        /// </summary>
        /// <returns></returns>
        public string DatebasePIVAsHISInfo()
        {
            try
            {
                SqlConnectionStringBuilder buf = new SqlConnectionStringBuilder();
                buf.DataSource = IniReadValueHIS("Database", "Data Source");
                buf.InitialCatalog = IniReadValueHIS("Database", "Initial Catalog");
                buf.UserID = Decrypt(IniReadValueHIS("Database", "User ID"));
                buf.Password = Decrypt(IniReadValueHIS("Database", "Password"));
                PIVAsHISInfo = buf.ConnectionString;
                return buf.ConnectionString;
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("获取HIS SqlServer数据库连接参数出错" + ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// 连接HISOracle数据库配置
        /// </summary>
        /// <returns></returns>
        public void DatebasePIVAsHISOracle()
        {
            try
            {
                OleDbConnectionStringBuilder bufOLE = new OleDbConnectionStringBuilder();
                bufOLE.Provider = IniReadValueHIS("Database", "Provider");
                OracleConnectionStringBuilder buf = new OracleConnectionStringBuilder();

                bufOLE.DataSource = buf.DataSource = IniReadValueHIS("Database", "Data Source");
                buf.UserID = Decrypt(IniReadValueHIS("Database", "User ID"));
                bufOLE.Add("User ID", buf.UserID);
                buf.Password = Decrypt(IniReadValueHIS("Database", "Password"));
                bufOLE.Add("Password", buf.Password);

                PIVAsHISOracle_Client = buf.ConnectionString;
                string persistSecurityInfo = IniReadValueHIS("Database", "Persist Security Info");
                if (String.IsNullOrEmpty(persistSecurityInfo) || persistSecurityInfo == "0")
                {
                    bufOLE.PersistSecurityInfo = false;
                }
                else
                {
                    bufOLE.PersistSecurityInfo = true;
                }
                PIVAsHISOracle_OLEDB = bufOLE.ConnectionString;
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("获取HIS Oracle数据库连接参数出错" + ex.Message);
            }
        }
        #endregion PIVAs数据库连接配置

        #region 读写ini文件
        public string IniReadValuePivas(string Database, string Key)
        {
            return IniFileHelper.INIGetStringValue(PIVAsPath, Database, Key, string.Empty);
        }

        public bool IniWriteValuePivas(string Database, string Key, string vlue)
        {
            return IniFileHelper.INIWriteValue(PIVAsPath, Database, Key, vlue.Trim());
        }

        public string IniReadValueHIS(string Database, string Key)
        {
            return IniFileHelper.INIGetStringValue(PIVAsHISPath, Database, Key, string.Empty);
        }

        public bool IniWriteValueHIS(string Database, string Key, string vlue)
        {
            return IniFileHelper.INIWriteValue(PIVAsHISPath, Database, Key, vlue.Trim());
        }
        #endregion

        #region 读写XML文件
        /// <summary>
        /// 读取XML文件内容
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ReadXML()
        {
            StringBuilder buf = new StringBuilder();
            try
            {
                using (FileStream file = new FileStream(@"login/UserInfo.xml", FileMode.Open, FileAccess.Read))
                {
                    StreamReader sr = new StreamReader(file, Encoding.Default);
                    sr.BaseStream.Seek(0, SeekOrigin.Begin);

                    string str = sr.ReadLine();
                    while (str != null)
                    {
                        buf.Append(str);
                        str = sr.ReadLine();
                    }
                    sr.Close();
                }
                return Decrypt(buf.ToString());
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("读取XML文件出错" + ex.Message);
                return ex.ToString();
            }
        }

        /// <summary>
        /// 保存XML文件
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        public void SaveXML(string username, string pwd)
        {
            StringBuilder xml = new StringBuilder();
            try
            {
                DataSet ds = XmlSerializeHelper.XmlToDataTable(ReadXML());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow[] drws = ds.Tables[0].Select("name=" + username + "");
                    if (drws.Length <= 0)
                    {
                        DataRow Row = ds.Tables[0].NewRow();
                        Row["name"] = username;
                        Row["pwd"] = pwd;
                        Row["date"] = DateTime.Now;
                        ds.Tables[0].Rows.InsertAt(Row, 0);
                    }
                }
                string LogDirName = @"login";
                try
                {
                    Directory.Delete(@"login", true);
                }
                catch { }
                if (!Directory.Exists(LogDirName))
                {
                    Directory.CreateDirectory(LogDirName);
                }
                string paths = LogDirName + "/";
                DirectoryInfo dir = Directory.CreateDirectory(paths);
                string path = @paths + "UserInfo" + ".xml";
                StreamWriter sw = File.AppendText(path);//向日志文件写XML信息      

                sw.WriteLine(Encrypt(ds.GetXml()));
                sw.Flush();
                sw.Close();
            }
            catch
            {
                try
                {
                    Directory.Delete(@"login", true);
                }
                catch { }
                xml.Append("<NewDataSet>" + "\r\n");
                xml.Append("<userinfo>" + "\r\n");
                xml.Append("<name>" + username + "</name>" + "\r\n");
                xml.Append("<pwd>" + pwd + "</pwd>" + "\r\n");
                xml.Append("<date>" + DateTime.Now + "</date>" + "\r\n");
                xml.Append("</userinfo>" + "\r\n");
                xml.Append("</NewDataSet>");
                string LogDirName = @"login";
                if (!Directory.Exists(LogDirName))
                {
                    Directory.CreateDirectory(LogDirName);
                }
                string paths = LogDirName + "/";
                DirectoryInfo dir = Directory.CreateDirectory(paths);
                string path = @paths + "UserInfo" + ".xml";
                StreamWriter sw = File.AppendText(path);//向日志文件写XML信息               
                sw.WriteLine(Encrypt(xml.ToString()));
                sw.Flush();
                sw.Close();
            }
        }
        #endregion

        #region 保存PIVAs系统日志方法
        /// <summary>日志改为用Log4Net</summary>   
        /// <param name="LogType">日志类型</param>        
        /// <param name="log">日志内容</param>
        //public void PreserveLog(string LogType, string log)
        //{
        //    try
        //    {
        //        string paths = "./Log/" + LogType;
        //        if (!Directory.Exists(paths))
        //        {
        //            Directory.CreateDirectory(paths);
        //        }
        //        StringBuilder xm = new StringBuilder();
        //        xm.AppendLine("[" + DateTime.Now + "]" + "\r\n");
        //        xm.AppendLine(log);
        //        string path = @paths + "/IMEQLog" + DateTime.Now.ToString("yyyy_MM_dd") + ".log";
        //        File.AppendAllText(path, xm.ToString());
        //    }
        //    catch { }
        //}
        #endregion

        #region 操作PIVAs数据库方法
        /// <summary>
        /// PIVAs数据库操作
        /// 数据取得(执行命令，返回DataSet)
        /// </summary>
        /// <param name="SqlStr">Sql文</param>
        /// <returns>DataSet数据集</returns>
        public DataSet GetPIVAsDB(string SqlStr)
        {
            DataSet DS = new DataSet();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(string.IsNullOrEmpty(PIVAsInfo) ? DatebasePIVAsInfo() : PIVAsInfo))
                {
                    using (SqlCommand command = new SqlCommand(SqlStr, sqlConn))
                    {
                        command.CommandTimeout = 180;//s
                        using (SqlDataAdapter da = new SqlDataAdapter(command))
                        {
                            da.Fill(DS);
                        }
                    }
                }
                return DS;
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("读取数据库出错,请检测一下SQL是否正确:" + SqlStr + "." + ex.Message);
                return DS;
            }
        }

        /// <summary>
        /// PIVAs数据库操作数据插入/修改(执行命令，插入或修改数据。)
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <returns>默认为-1</returns>
        public int SetPIVAsDB(string SqlStr)
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(string.IsNullOrEmpty(PIVAsInfo) ? DatebasePIVAsInfo() : PIVAsInfo))
                {
                    using (SqlCommand sqlComm = new SqlCommand(SqlStr, sqlConn))
                    {
                        sqlComm.CommandTimeout = 180;//s
                        sqlComm.Connection.Open();
                        //对于 UPDATE、INSERT 和 DELETE 语句，返回值为该命令所影响的行数。
                        //对于所有其他类型的语句，返回值为 -1。如果发生回滚，返回值也为 -1 。
                        int a = sqlComm.ExecuteNonQuery();
                        sqlComm.Connection.Close();
                        return a < 0 ? 0 : a;//执行成功保证返回0
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("写入数据库出错,请检测一下SQL是否正确:" + SqlStr + "." + ex.Message);
            }
            return -1;
        }

        /// <summary>
        /// 测试IVAs数据库连接状态
        /// </summary>
        /// <returns></returns>
        public bool TestDB()
        {
            string Str = "select top 1 AccountID  from DEmployee";
            DataSet DS = new DataSet();
            try
            {
                PIVAsInfo = String.Empty;//为了重新从文件读取
                using (SqlConnection sqlConn = new SqlConnection(string.IsNullOrEmpty(PIVAsInfo) ? DatebasePIVAsInfo() : PIVAsInfo))
                {
                    using (SqlCommand command = new SqlCommand(Str, sqlConn))
                    {
                        command.CommandTimeout = 3;//秒
                        using (SqlDataAdapter da = new SqlDataAdapter(command))
                        {
                            da.Fill(DS);
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("测试PIVAs数据库连接状态出错"+ ex.Message);
            }
            return false;
        }

        /// <summary>
        /// 将DataTable保存到数据库中表TabName
        /// 目前只用于pivas sql类型数据库
        /// 先清空临时表，然后将his返回的最新信息存入临时表
        /// </summary>
        /// <param name="dtHis"></param>
        /// <param name="TabName"></param>
        public void CopyDataTableToDB(DataTable dtHis, string TabName)
        {
            try
            {
                if (dtHis != null && dtHis.Rows.Count > 0 && !string.IsNullOrEmpty(TabName))
                {
                    if (SetPIVAsDB(string.Format("truncate table {0}", TabName)) >= 0)//执行清空表且成功
                    {
                        using (SqlConnection sqlConn = new SqlConnection((string.IsNullOrEmpty(PIVAsInfo) ? DatebasePIVAsInfo() : PIVAsInfo)))
                        {
                            using (SqlBulkCopy sbCopy = new SqlBulkCopy(sqlConn))
                            {
                                using (DataSet ds = GetPIVAsDB(string.Format("SELECT * FROM {0}", TabName)))
                                {
                                    //查询得到是空白，但有表结构；使用原表结构保存到临时表
                                    if (ds != null && ds.Tables.Count > 0)
                                    {
                                        sbCopy.DestinationTableName = TabName;
                                        foreach (DataColumn dc in ds.Tables[0].Columns)
                                        {
                                            if (dc.ColumnName.ToUpper() != "ID" && dc.ColumnName.Trim() != "UseDrugID")
                                                sbCopy.ColumnMappings.Add(dc.ColumnName.ToUpper(), dc.ColumnName);
                                        }
                                        sbCopy.BulkCopyTimeout = 400;
                                        sqlConn.Open();
                                        sbCopy.WriteToServer(dtHis);
                                    }
                                }
                            }
                            sqlConn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("将DataTable保存到数据库中表" + TabName + "出错：" + ex.Message);
            }
        }

        /// <summary>
        /// PIVAs数据库操作
        /// 数据获取object类型数据(如图片)
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <returns></returns>
        [Obsolete("未经测试，建议不用",false)]
        public object GetPIVAsDBforObject(string SqlStr)
        {
            try
            {
                using (SqlCommand Command = new SqlCommand(SqlStr, new SqlConnection(string.IsNullOrEmpty(PIVAsInfo) ? DatebasePIVAsInfo() : PIVAsInfo)))
                {
                    Command.CommandTimeout = 3600;
                    Command.Connection.Open();
                    object a = Command.ExecuteScalar();
                    Command.Connection.Close();
                    return a;
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("数据库读取错误"+ "SqlStr:" + SqlStr + ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// PIVAs数据库操作
        /// 数据插入/修改图片(执行命令，插入或修改数据。) 带二进制参数
        /// </summary>
        /// <param name="SqlStr">Sql文</param>
        [Obsolete("未经测试，建议不用", false)]
        public int SetPIVAsDB(string SqlStr, byte[] photo, string ParametersName)
        {
            try
            {
                using (SqlCommand Command = new SqlCommand(SqlStr, new SqlConnection(string.IsNullOrEmpty(PIVAsInfo) ? DatebasePIVAsInfo() : PIVAsInfo)))
                {
                    Command.CommandTimeout = 3600;

                    Command.Parameters.Add(ParametersName, SqlDbType.Image).Value = photo;
                    Command.Connection.Open();
                    int a = Command.ExecuteNonQuery();
                    Command.Connection.Close();
                    return a;
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("写入数据库错误"+ "SqlStr: " + SqlStr +  ex.Message);
                return -1;
            }
        }
        #endregion

        #region 操作HIS数据库方法
        #region SQL数据库
        /// <summary>
        /// HIS数据库操作
        /// 数据取得(执行命令和存储过程，返回DataSet)
        /// </summary>
        /// <param name="SqlStr">Sql文</param>
        /// <returns>DataSet数据集</returns>
        public DataSet GetPIVAsHISDBSQL(string SqlStr)
        {
            DataSet DS = new DataSet();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(string.IsNullOrEmpty(PIVAsHISInfo) ? DatebasePIVAsHISInfo() : PIVAsHISInfo))
                {
                    using (SqlCommand command = new SqlCommand(SqlStr, sqlConn))
                    {
                        command.CommandTimeout = 180;//s
                        using (SqlDataAdapter da = new SqlDataAdapter(command))
                        {
                            da.Fill(DS);
                        }
                    }
                }
                return DS;
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("读取HIS数据库出错" +  ex.Message);
                return DS;
            }
        }

        /// <summary>
        /// HIS数据库操作
        /// 数据插入/修改(执行命令，插入或修改数据。)
        /// </summary>
        /// <param name="SqlStr">Sql文</param>
        public int SetPIVAsHISDBSQL(string SqlStr)
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(string.IsNullOrEmpty(PIVAsHISInfo) ? DatebasePIVAsHISInfo() : PIVAsHISInfo))
                {
                    using (SqlCommand command = new SqlCommand(SqlStr, sqlConn))
                    {
                        command.CommandTimeout = 180;//s
                        command.Connection.Open();
                        int a = command.ExecuteNonQuery();
                        command.Connection.Close();
                        return a;
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("写入HIS数据库失败" +  ex.Message);
                return -1;
            }
        }
        #endregion

        #region Oracle数据库

        #region system.Data.OracleClient方式
        [Obsolete("System.Data.OracleClient已丢弃，建议不用", false)]
        public DataSet GetPIVAsHisOracle(string SqlStr)
        {
            DataSet ds = new DataSet();
            try
            {
                if (string.IsNullOrEmpty(PIVAsHISOracle_Client))
                {
                    DatebasePIVAsHISOracle();
                }
                using (OracleConnection oraConn = new OracleConnection(PIVAsHISOracle_Client))
                {
                    using (OracleCommand command = new OracleCommand(SqlStr, oraConn))
                    {
                        command.CommandTimeout = 180;//s
                        using (OracleDataAdapter oraData = new OracleDataAdapter(command))
                        {
                            oraData.Fill(ds);
                        }
                    }
                }
                return ds;
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("读取HIS【Oracle】库失败" + ex.Message);
                return ds;
            }
        }
        [Obsolete("System.Data.OracleClient已丢弃，建议不用", false)]
        public int SetPIVAsHisOracle(string sql)
        {
            try
            {
                if (string.IsNullOrEmpty(PIVAsHISOracle_Client))
                {
                    DatebasePIVAsHISOracle();
                }
                using (OracleConnection oraConn = new OracleConnection(PIVAsHISOracle_Client))
                {
                    using (OracleCommand command = new OracleCommand(sql, oraConn))
                    {
                        command.CommandTimeout = 180;//s
                        command.Connection.Open();
                        int a = command.ExecuteNonQuery();
                        command.Connection.Close();
                        return a;
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("写入HIS【Oracle】数据库失败" + ex.Message);
                return -1;
            }
        }
        #endregion

        #region ODP.NET方式访问
        //public DataSet GetPIVAsHisOracleEx(string SqlStr)
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        string strConn = String.IsNullOrEmpty(PIVAsHISOracle) ? DatebasePIVAsHISOracle() : PIVAsHISOracle;
        //        return OracleHelper.Query(strConn, SqlStr, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        InternalLogger.Log.Error("读取HIS【Oracle】库失败" + ex.Message);
        //    }
        //    return ds;
        //}
        //public int SetPIVAsHisOracleEx(string sql)
        //{
        //    try
        //    {
        //        string strConn = String.IsNullOrEmpty(PIVAsHISOracle) ? DatebasePIVAsHISOracle() : PIVAsHISOracle;
        //        return OracleHelper.ExecuteNonQuery(strConn, sql);
        //    }
        //    catch (Exception ex)
        //    {
        //        InternalLogger.Log.Error("写入HIS【Oracle】数据库失败" + ex.Message);
        //    }
        //    return -1;
        //}
        #endregion

        #region 微软提供OLEDB驱动方式访问
        /// <summary>
        /// HIS数据库操作
        /// 数据插入/修改(执行命令，插入或修改数据。)
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int SetHisOracleByOLEDB(String sql)
        {
            try
            {
                if (string.IsNullOrEmpty(PIVAsHISOracle_OLEDB))
                {
                    DatebasePIVAsHISOracle();
                }
                using (OleDbConnection oleConn = new OleDbConnection(PIVAsHISOracle_OLEDB))
                {
                    using (OleDbCommand command = new OleDbCommand(sql, oleConn))
                    {
                        command.CommandTimeout = 180;//s
                        command.Connection.Open();
                        int a = command.ExecuteNonQuery();
                        command.Connection.Close();
                        return a;
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("通过MSOLEDB写入HIS【Oracle】库失败" + ex.Message);
            }
            return -1;
        }

        /// <summary>
        /// HIS数据库操作
        /// 数据取得(执行命令和存储过程，返回DataSet)
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetHisOracleByOLEDB(String sql)
        {
            DataSet ds = new DataSet();
            try
            {
                if (string.IsNullOrEmpty(PIVAsHISOracle_OLEDB))
                {
                    DatebasePIVAsHISOracle();
                }
                using (OleDbConnection oleConn = new OleDbConnection(PIVAsHISOracle_OLEDB))
                {
                    using (OleDbCommand command = new OleDbCommand(sql, oleConn))
                    {
                        using (OleDbDataAdapter oraDap = new OleDbDataAdapter(command))
                        {
                            oraDap.Fill(ds);
                        }
                        command.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("通过MSOLEDB读取HIS【Oracle】库失败" + ex.Message);
            }
            return ds;
        }

        /// <summary>
        /// 执行带参数的存储过程，无参数或参数简单时请用GetHisOracleByOLEDB
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="cmdType"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int ExecProcHisOracleByOLEDB(string strSql, params OleDbParameter[] param)
        {
            int rtn = 0;
            try
            {
                if (string.IsNullOrEmpty(PIVAsHISOracle_OLEDB))
                {
                    DatebasePIVAsHISOracle();
                }
                using (OleDbConnection oleConn = new OleDbConnection(PIVAsHISOracle_OLEDB))
                {
                    using (OleDbCommand command = new OleDbCommand(strSql, oleConn))
                    {
                        command.CommandText = strSql;
                        command.CommandType = CommandType.StoredProcedure;
                        foreach (var item in param)
                        {
                            if (item.Value == null)
                                item.Value = DBNull.Value;
                            command.Parameters.Add(item);
                        }

                        rtn = command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    oleConn.Close();
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("执行带参数的存储过程出错：" + ex.Message);
            }

            return rtn;
        }

        #endregion
        #endregion
        #endregion

        #region ========加密========
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public string Encrypt(string Text)
        {
            return Encrypt(Text, "beijingjarlinfo");
        }
        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public string Encrypt(string Text, string sKey)
        {
            try
            {
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    byte[] inputByteArray = Encoding.Default.GetBytes(Text);
                    des.Key = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
                    des.IV = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(inputByteArray, 0, inputByteArray.Length);
                            cs.FlushFinalBlock();
                            StringBuilder ret = new StringBuilder();
                            foreach (byte b in ms.ToArray())
                            {
                                ret.AppendFormat("{0:X2}", b);
                            }
                            return ret.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("加密时出错:"+ ex.Message);
            }
            return string.Empty;
        }
        #endregion

        #region ========解密========
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public string Decrypt(string Text)
        {
            if (string.IsNullOrEmpty(Text))
                return string.Empty;
            else
                return Decrypt(Text, "beijingjarlinfo");
        }

        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public string Decrypt(string Text, string sKey)
        {
            try
            {
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    byte[] inputByteArray = new byte[Text.Length / 2];
                    int x, i;
                    for (x = 0; x < inputByteArray.Length; x++)
                    {
                        i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                        inputByteArray[x] = (byte)i;
                    }
                    des.Key = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
                    des.IV = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(inputByteArray, 0, inputByteArray.Length);
                            cs.FlushFinalBlock();
                            return Encoding.Default.GetString(ms.ToArray());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("解密时出错:" + ex.Message);
            }
            return string.Empty;
        }
        #endregion

        #region Pivas综合设置使用方法
        /// <summary>
        /// PIVAS综合设置结果取得
        /// </summary>
        /// <param name="SetName">综合设置名</param>
        /// <returns>设置结果，若取不到返回0，一般默认设置都是0.</returns>
        public string GetPivasAllSet(string SetName)
        {
            try
            {
                DataSet ds = new DataSet();
                string str = "select Value from PivasAllSet where Pro = '" + SetName.Trim()  + "'  ";
                ds = GetPIVAsDB(str);
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Value"].ToString().Trim() != "----")
                    {
                        return ds.Tables[0].Rows[0]["Value"].ToString().Trim();
                    }
                    else
                    { return "0"; }
                }
                else
                {
                    InPivasAllSet(SetName.Trim());
                    return "0";
                }
            }
            catch
            { 
                return "0"; 
            }
        }

        /// <summary>
        /// PIVAS综合设置结果取得
        /// </summary>
        /// <param name="SetName">综合设置名</param>
        /// <returns>设置结果，若取不到返回0，一般默认设置都是0.</returns>
        public DataTable GetPivasAllSet(string SetName,string AllValue)
        {
            try
            {
                DataSet ds = new DataSet();
                string str = "select * from PivasAllSet where Pro = '" + SetName.Trim() + "'  ";
                ds = GetPIVAsDB(str);
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                        return ds.Tables[0];
                }
                else
                {
                    InPivasAllSet(SetName.Trim());
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// PIVAS综合设置结果取得
        /// </summary>
        /// <param name="SetName">综合设置名</param>
        /// <returns>设置结果，若取不到返回0，一般默认设置都是0.</returns>
        public string GetPivasAllSetValue2(string SetName)
        {
            try
            {
                DataSet ds = new DataSet();
                string str = "select Value2 from PivasAllSet where Pro = '" + SetName + "'  ";
                ds = GetPIVAsDB(str);
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Value2"].ToString().Trim() != "----")
                    {
                        return ds.Tables[0].Rows[0]["Value2"].ToString().Trim();
                    }
                    else
                    { return "0"; }
                }
                else
                {
                    InPivasAllSet(SetName);
                    return "0";
                }
            }
            catch
            {
                return "0";
            }
        }

        /// <summary>
        /// PIVAS综合设置结果取得
        /// </summary>
        /// <param name="SetName">综合设置名</param>
        /// <returns>设置结果，若取不到返回0，一般默认设置都是0.</returns>
        public string GetPivasAllSetValue3(string SetName)
        {
            try
            {
                DataSet ds = new DataSet();
                string str = "select Value3 from PivasAllSet where Pro = '" + SetName + "'  ";
                ds = GetPIVAsDB(str);
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0 )
                {
                    if (ds.Tables[0].Rows[0]["Value3"].ToString().Trim() != "----")
                    {
                        return ds.Tables[0].Rows[0]["Value3"].ToString().Trim();
                    }
                    else
                    { return "0"; }
                }
                else
                {
                    InPivasAllSet(SetName);
                    return "0";
                }
            }
            catch
            {
                return "0";
            }
        }

        /// <summary>
        /// 将数据库中未找到的设置项初始化到数据库
        /// </summary>
        /// <param name="SetName">设置项名称</param>
        private void InPivasAllSet(string SetName)
        {
            StringBuilder IntStr = new StringBuilder ();
            IntStr.Length =0;
            if (SetName == "排批次-#")
            {
                IntStr.Append(" INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])  ");
                IntStr.Append(" VALUES('排批次-#','1','----','----','0:未记账,1:可打印,2:已记账','瓶签生成，#的瓶签，状态设置。（不包含临时）','1')  ");
            }
            else if (SetName == "排批次-L#")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("  VALUES('排批次-L#','1','----','----','0:未记账,1:可打印,2:已记账','瓶签生成，#的临时瓶签，状态设置。','2')   ");
            }
            else if (SetName == "排批次-K")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])  ");
                IntStr.Append("  VALUES('排批次-K','1','----','----','0:未记账,1:可打印,2:已记账','瓶签生成，K的瓶签，状态设置。（不包含临时）','3')   ");
            }
            else if (SetName == "排批次-LK")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("   VALUES('排批次-LK','1','----','----','0:未记账,1:可打印,2:已记账','瓶签生成，K的临时瓶签，状态设置。','4')   ");
            }
            else if (SetName == "同步-药品目录-画面显示")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("   VALUES('同步-药品目录-画面显示','1','----','----','0:不显示,1:显示','1','5')   ");
            }
            else if (SetName == "同步-病区-画面显示")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("   VALUES('同步-病区-画面显示','1','----','----','0:不显示,1:显示','2','6')   ");
            }
            else if (SetName == "同步-员工-画面显示")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("   VALUES('同步-员工-画面显示','1','----','----','0:不显示,1:显示','3','7')   ");
            }
            else if (SetName == "同步-剂量单位-画面显示")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("   VALUES('同步-剂量单位-画面显示','1','----','----','0:不显示,1:显示','4','8')   ");
            }
            else if (SetName == "同步-频次-画面显示")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("   VALUES('同步-频次-画面显示','1','----','----','0:不显示,1:显示','5','9')   ");
            }
            else if (SetName == "同步-患者-画面显示")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("   VALUES('同步-患者-画面显示','1','----','----','0:不显示,1:显示','6','10')   ");
            }
            else if (SetName == "同步-医嘱-画面显示")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("   VALUES('同步-医嘱-画面显示','1','----','----','0:不显示,1:显示','7','11')   ");
            }
            else if (SetName == "同步-药单-画面显示")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("   VALUES('同步-药单-画面显示','1','----','----','0:不显示,1:显示','8','12')   ");
            }

            else if (SetName == "同步-患者身高体重-画面显示")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("   VALUES('同步-患者身高体重-画面显示','1','----','----','0:不显示,1:显示','9','13')   ");
            }
            else if (SetName == "同步-临床诊断-画面显示")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("   VALUES('同步-临床诊断-画面显示','1','----','----','0:不显示,1:显示','10','14')   ");
            }
            else if (SetName == "同步-统药单-画面显示")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("   VALUES('同步-统药单-画面显示','1','----','----','0:不显示,1:显示','11','15')   ");
            }

            else if (SetName == "SaveLabelDay")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("    VALUES('SaveLabelDay','90','----','----','0~365(天数)','瓶签表中保留瓶签天数,超过天数瓶签将转移到历史表中,统计按照视图数据统计.','301')   ");
            }
            else if (SetName == "第三方瓶签") 
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("    VALUES('第三方瓶签','0','14','2014','(value)0:Pivas瓶签,1：第三方瓶签(value2)瓶签长度(value3)瓶签标识','所有瓶签相关的操作是否使用第三方的瓶签号，以及第三方瓶签的格式','302')   ");
            }
            else if (SetName == "主画面_调用外部画面1")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("   VALUES('主画面_调用外部画面1','0','按钮1','程序exe名','(Value)0:隐藏,1:显示(Value2)控件名称(Value3)调用exe的名称。','瓶签表中保留瓶签天数,超过天数瓶签将转移到历史表中,统计按照视图数据统计.','1101')   ");
            }
            else if (SetName == "主画面_调用外部画面2")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('主画面_调用外部画面2','0','按钮2','程序exe名','(Value)0:隐藏,1:显示(Value2)控件名称(Value3)调用exe的名称。','瓶签表中保留瓶签天数,超过天数瓶签将转移到历史表中,统计按照视图数据统计.','1102')   ");
            }
            else if (SetName == "主画面_气泡消息框")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('主画面_气泡消息框','0','0','----','(Value自动弹出 0:不可用,1:可用）(Value2 双击弹出 0:不可用,1:可用）','主画面气泡弹出框的设置','1102')   ");
            }
            else if (SetName == "审方-列表模式处方-标题-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('审方-列表模式处方-标题-画面显示','1','9','20','(Value)0:隐藏,1:显示(Value2 标题字体大小):1~20 建议只用9或10  (Value3 标题高度设置):20~30 建议只用20到30','审方画面，列表模式下，中间处方，标题设置。','2001')   ");
            }
            else if (SetName == "审方-列表模式处方-字体大小-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('审方-列表模式处方-字体大小-画面显示','9','25','0','(Value 数据字体大小):1~20 建议只用9或10 (Value2 数据行高):20~50 建议只用25或35之间的数字 (Value3 字体设置):0普通1粗体2斜体3下划线','审方画面，列表模式下，中间处方，数据字体大小，行高度设置。','2002')   ");
            }
            else if (SetName == "审方-Remark1-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("  VALUES('审方-Remark1-画面显示','0','字段1','----','0:不显示, 1~300：显示宽度(Value2:列名)','处方表中Remark1，在审方画面中的显示设置。与瓶签表（Remark1）相同,用于备注显示。','2301')  ");
            }
            else if (SetName == "审方-Remark2-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('审方-Remark2-画面显示','0','字段2','----','0:不显示, 1~300：显示宽度(Value2:列名)','处方表中Remark2，在审方画面中的显示设置。处方表可单独使用（瓶签表此字段作为判断）','2302')   ");
            }
            else if (SetName == "审方-Remark3-画面显示")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("   VALUES('审方-Remark3-画面显示','0','字段3','----','0:不显示, 1~300：显示宽度(Value2:列名)','处方表中Remark3，在审方画面中的显示设置。可用于处方备注显示（此字段不传入瓶签表，瓶签表另做他用）','2303')   ");
            }
            else if (SetName == "审方-Remark4-画面显示")
            {
                IntStr.Append("    INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("   VALUES('审方-Remark4-画面显示','0','字段4','----','0:不显示, 1~300：显示宽度(Value2:列名)','处方表中Remark4，在审方画面中的显示设置。处方表现在没有作用（瓶签表留做发药机使用）','2304')  ");
            }
            else if (SetName == "审方-Remark5-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("  VALUES('审方-Remark5-画面显示','0','字段5','----','0:不显示, 1~300：显示宽度(Value2:列名)','处方表中Remark5，在审方画面中的显示设置。处方临时非临时（传入瓶签表）','2305')  ");
            }
            else if (SetName == "审方-Remark6-画面显示")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("   VALUES('审方-Remark6-画面显示','0','字段6','----','0:不显示, 1~300：显示宽度(Value2:列名)','处方表中Remark6，在审方画面中的显示设置。可用于处方备注显示（此字段不传入瓶签表，瓶签表另做他用）','2306')  ");
            }
            else if (SetName == "审方-Remark7-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('审方-Remark7-画面显示','0','----','----','0:不显示, 1~300：显示宽度','处方表中Remark7，在审方画面中的显示设置(明细)。  与瓶签表中Remark7相同（可用于显示传递）','2307')  ");
            }
            else if (SetName == "审方-Remark8-画面显示")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("   VALUES('审方-Remark8-画面显示','0','----','----','0:不显示, 1~300：显示宽度','处方表中Remark8，在审方画面中的显示设置(明细)。药品数量（HIS传过来的药品数量）','2308')  ");
            }
            else if (SetName == "审方-Remark9-画面显示")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("   VALUES('审方-Remark9-画面显示','0','----','----','0:不显示, 1~300：显示宽度','处方表中Remark9，在审方画面中的显示设置(明细)。与瓶签表中Remark9相同（可用于显示传递）','2309')  ");
            }
            else if (SetName == "审方-Remark10-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])     ");
                IntStr.Append("  VALUES('审方-Remark10-画面显示','0','----','----','0:不显示, 1~300：显示宽度','处方表中Remark10，在审方画面中的显示设置(明细)。与瓶签表中Remark10相同（可用于显示传递）','2310')   ");
            }
            else if (SetName == "审方-药品明细标题-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('审方-药品明细标题-画面显示','0','9','20','(Value)0:隐藏,1:显示(Value2 标题字体大小):1~20 建议只用9或10  (Value3 标题高度设置):20~30 建议只用20到30','审方画面，处方明细，字体大小设置设置。','2311')   ");
            }
            else if (SetName == "审方-药品明细-字体大小-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('审方-药品明细-字体大小-画面显示','9','25','0','(Value 字体大小):1~20 建议只用9或10  (Value2 行高设置):20~40 建议只用25到35  (Value3 字体设置):0普通1粗体2斜体3下划线','审方画面，处方明细药品，数据显示大小行高设置。','2312')   ");
            }
            else if (SetName == "审方-药品名称-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('审方-药品名称-画面显示','200','----','----','(Value)0:不显示, 1~300：显示宽度(Value2)(Value3)','审方画面，处方明细药品，药品名称显示设置。','2313')   ");
            }
            else if (SetName == "审方-药品规格-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('审方-药品规格-画面显示','120','----','----','(Value)0:不显示, 1~300：显示宽度(Value2)(Value3)','审方画面，处方明细药品，药品规格显示设置。','2314')   ");
            }
            else if (SetName == "审方-药品用量-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('审方-药品用量-画面显示','100','----','----','(Value)0:不显示, 1~300：显示宽度(Value2)(Value3)','审方画面，处方明细药品，药品用量显示设置。','2315')   ");
            }
            else if (SetName == "审方-药品数量-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('审方-药品数量-画面显示','0','----','----','(Value)0:不显示, 1~300：显示宽度(Value2)(Value3)','审方画面，处方明细药品，药品数量显示设置。','2316')   ");
            }
            else if (SetName == "审方-皮试信息-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('审方-皮试信息-画面显示','0','----','----','(Value)0:不显示, 1~300：显示宽度(Value2)(Value3)','审方画面，处方明细药品，皮试信息显示设置。','2317')   ");
            }
            else if (SetName == "审方-(长期/临时)筛选")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])     ");
                IntStr.Append("  VALUES('审方-(长期/临时)筛选','0','0','----','(Value 0:隐藏,1:显示)(Value2 0:长期临时都可同时选中 1:只可选中一个，默认长期 2:只可选中一个，默认临时 3:可同时选中,默认长期 4:可同时选中,默认临时)','审方画面上面的，长期/临时医嘱筛选是否显示配置。','2501')  ");
            }
            else if (SetName == "审方-界面设置")
            {
                IntStr.Append("    INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("    VALUES('审方-界面设置','0','----','----','0:隐藏,1:显示','审方画面上，设置按钮是否可见。','2502')   ");
            }
            else if (SetName == "审方-医嘱同步")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("   VALUES('审方-医嘱同步','0','----','----','0:隐藏,1:显示','审方画面上，医嘱同步按钮是否可见。','2503')  ");
            }
            else if (SetName == "审方-历史医嘱")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("   VALUES('审方-历史医嘱','0','----','----','0:隐藏,1:显示','审方画面上，历史医嘱按钮是否可见。','2504')   ");
            }
            else if (SetName == "审方-人工审方结果筛选")
            {
                IntStr.Append("    INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("      VALUES('审方-人工审方结果筛选','0','----','----','0:隐藏,1:显示','审方画面上，人工审方结果筛选是否可见。','2505')   ");
            }
            else if (SetName == "审方-系统审方结果筛选")
            {
                IntStr.Append("    INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("      VALUES('审方-系统审方结果筛选','0','----','----','0:隐藏,1:显示','审方画面上，系统审方结果筛选是否可见。','2506')   ");
            }
            else if (SetName == "审方-普抗化营筛选")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("   VALUES('审方-普抗化营筛选','0','----','----','0:隐藏,1:显示','审方画面上，普抗化营，筛选是否显示。','2507')  ");
            }
            else if (SetName == "审方-全选-勾选框")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("   VALUES('审方-全选-勾选框','0','----','----','0:隐藏,1:显示','审方画面上，全选所有处方勾选框，是否显示。','2508')  ");
            }
            else if (SetName == "审方-处方模糊查询-输入框")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("   VALUES('审方-处方模糊查询-输入框','0','----','----','0:隐藏,1:显示','审方画面上，审方处方模糊查询输入框，是否显示。','2509')  ");
            }
            else if (SetName == "审方-条件选择-显示模式")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("   VALUES('审方-条件选择-显示模式','0','----','----','0:勾选模式,1:下拉框模式','审方画面上，批次等条件的显示模式','2510')  ");
            }
            else if (SetName == "审方-退单按钮-设置")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("   VALUES('审方-退单按钮-设置','0','退单','----','[Value]0:按钮显示,1:按钮隐藏   [Value2]：按钮显示字设置（默认退单）','审方画面上，退单按钮属性设置','2511')  ");
            }
            else if (SetName == "审方-通过按钮-设置")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("   VALUES('审方-通过按钮-设置','0','----','----','[Value]0:不调用接口,1:调用接口   [Value2]：','审方画面上，通过按钮属性设置','2512')  ");
            }
            else if (SetName == "审方-删除处方")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("   VALUES('审方-删除处方','0','----','----','0:不显示,1:显示','审方画面上，是否显示删除处方按钮的设置','2513')  ");
            }
            else if (SetName == "批次-病区选中设置-列表")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("    VALUES('批次-病区选中设置-列表','1','----','----','0:全部不选中,1:选中第一个病区 2:全部选中','批次画面打开时，列表栏选中状态设置。','3001')  ");
            }
            else if (SetName == "批次-药单同步按钮")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("    VALUES('批次-药单同步按钮','0','----','----','0:隐藏,1:显示','批次画面上，药单同步按钮，是否可见设置。','3101')  ");
            }
            else if (SetName == "批次-瓶签生成按钮")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("   VALUES('批次-瓶签生成按钮','0','----','----','0:隐藏,1:显示','批次画面上，瓶签生成按钮，是否可见设置。','3102')   ");
            }
            else if (SetName == "批次-差异瓶签处理按钮")
            {
                IntStr.Append("    INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("    VALUES('批次-差异瓶签处理按钮','0','----','----','0:隐藏,1:显示','批次画面上，差异瓶签处理按钮，是否可见设置。','3103')   ");
            }
            else if (SetName == "批次-批次重排按钮")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("   VALUES('批次-批次重排按钮','0','----','----','0:隐藏,1:显示','批次画面上，批次重排按钮，是否可见设置。','3104')   ");
            }
            else if (SetName == "批次-取消发送-按钮")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("   VALUES('批次-取消发送-按钮','0','----','----','0:隐藏,1:显示','批次画面上，取消发送按钮，是否可见设置。','3107')   ");
            }
            else if (SetName == "批次-批量修改批次-按钮")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("   VALUES('批次-批量修改批次-按钮','0','----','----','0:隐藏,1:显示','批次画面上，批量修改批次按钮，是否可见设置。','3108')   ");
            }
          
            else if (SetName == "批次-界面设置")
            {
                IntStr.Append("    INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("    VALUES('批次-界面设置','0','----','----','0:隐藏,1:显示','批次画面上，界面设置按钮，是否可见设置。','3105')  ");
            }
            else if (SetName == "批次-快速修改批次")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("       VALUES('批次-快速修改批次','0','----','----','0:隐藏,1:显示','批次画面上，批次-快速修改批次，是否可见设置。','3201')    ");
            }
            else if (SetName == "批次-普抗化营中")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("       VALUES('批次-普抗化营','0','11111','----','[Value]：控件是否显示设置   [Value2]：默认选中设置 默认为11111五个全部选中，若要某个默认不选中则改为0（列10111）','批次列表模式下普抗化营设置 ','3201')    ");
            }
            else if (SetName == "批次-显示模式切换")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("    VALUES('批次-显示模式切换','0','----','----','0:隐藏,1:显示','批次画面上，显示模式切换，是否可见设置。','3202')     ");
            }
            else if (SetName == "批次-修改-已打印")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("    VALUES('批次-修改-已打印','0','----','----','0:不可修改,1:可修改','在瓶签 已打印 的状态下，批次是否可以再修改配置。','3301')     ");
            }
            else if (SetName == "批次-修改-已发送")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("    VALUES('批次-修改-已发送','0','----','----','0:不可修改,1:可修改','在瓶签 已发送 的状态下，批次是否可以再修改配置。','3302')     ");
            }
            else if (SetName == "批次-修改-打印画面")
            {
                IntStr.Append("    INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("    VALUES('批次-修改-打印画面','0','0','0',");
                IntStr.Append("    '(Value 打印画面所有瓶签  0:不可修改,1:可修改)(Value2 打印画面已发送状态瓶签 0:不可修改,1:可修改)(Value3 打印画面已打印状态瓶签  0:不可修改,1:可修改)','打印画面修改批次配置。在瓶签 已发送或已打印 的状态下，批次是否可以再修改配置。','3303')       ");
            }
            else if (SetName == "批次-列表模式瓶签-标题-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('批次-列表模式瓶签-标题-画面显示','1','9','20','(Value)0:隐藏,1:显示(Value2 标题字体大小):1~20 建议只用9或10  (Value3 标题高度设置):20~30 建议只用20到30','批次画面，列表模式下，中间瓶签，标题设置。','3501')   ");
            }
            else if (SetName == "批次-列表模式瓶签-字体大小-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('批次-列表模式瓶签-字体大小-画面显示','9','25','0','(Value):1~20 建议只用9或10    (Value2 行高设置):20~40 建议只用25到35  (Value3 字体设置):0普通1粗体2斜体3下划线  ','批次画面，列表模式下，中间瓶签，字体大小设置。','3502')   ");
            }
            else if (SetName == "批次-病人明细模式-未发送-已发送已打印瓶签")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('批次-病人明细模式-未发送-已发送已打印瓶签','0','0','0','Value(已发送瓶签) ：0不显示 1显示  Value2(已打印瓶签) ：0不显示 1显示','病人明细模式下，选择查看未发送瓶签时，是否显示已发送与已打印的瓶签的配置。','3503')   ");
            }
            else if (SetName == "批次-病人明细模式-液体总量计算")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('批次-病人明细模式-液体总量计算','50','0','0','Value：各批次容量，小于多少毫升的瓶签不计算容量，默认50ml。','病人明细模式下，各批次容量计算配置。','3504')   ");
            }
            else if (SetName == "批次-瓶签明细-标题-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('批次-瓶签明细-标题-画面显示','0','9','20','(Value)0:隐藏,1:显示  (Value2 标题字体大小):1~20 建议只用9或10   (Value3 标题高度设置):20~30 建议只用20到30','批次画面，列表模式下，右边被选中瓶签明细，标题设置。','3651')   ");
            }
            else if (SetName == "批次-瓶签明细-字体大小-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('批次-瓶签明细-字体大小-画面显示','9','25','0','(Value):1~20 建议只用9或10     (Value2 行高设置):20~40 建议只用25到35  (Value3 字体设置):0普通1粗体2斜体3下划线  ','批次画面，列表模式下，右边被选中瓶签明细，字体大小设置。','3652')   ");
            }
            else if (SetName == "批次-瓶签明细-药品名-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('批次-瓶签明细-药品名-画面显示','150','----','----','(Value)0:不显示, 1~300：显示宽度','批次画面，列表模式下，右边被选中瓶签明细，药品名称列设置。','3653')   ");
            }
            else if (SetName == "批次-瓶签明细-规格-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('批次-瓶签明细-规格-画面显示','85','----','----','(Value)0:不显示, 1~300：显示宽度','批次画面，列表模式下，右边被选中瓶签明细，规格列设置。','3654')   ");
            }
            else if (SetName == "批次-瓶签明细-剂量-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('批次-瓶签明细-剂量-画面显示','50','----','----','(Value)0:不显示, 1~300：显示宽度','批次画面，列表模式下，右边被选中瓶签明细，剂量列设置。','3655')   ");
            }
            else if (SetName == "批次-瓶签明细-单位-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('批次-瓶签明细-单位-画面显示','35','----','----','(Value)0:不显示, 1~300：显示宽度','批次画面，列表模式下，右边被选中瓶签明细，单位列设置。','3656')   ");
            }
            else if (SetName == "批次-瓶签明细-数量-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('批次-瓶签明细-数量-画面显示','35','----','----','(Value)0:不显示, 1~300：显示宽度','批次画面，列表模式下，右边被选中瓶签明细，数量列设置。','3657')   ");
            }
            else if (SetName == "批次-瓶签明细-皮试-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('批次-瓶签明细-皮试-画面显示','50','----','----','(Value)0:不显示, 1~300：显示宽度','批次画面，列表模式下，右边被选中瓶签明细，皮试列设置。','3658')   ");
            }
            else if (SetName == "批次-瓶签明细-备注-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('批次-瓶签明细-备注-画面显示','50','----','----','(Value)0:不显示, 1~300：显示宽度','批次画面，列表模式下，右边被选中瓶签明细，备注列设置。','3659')   ");
            }
            else if (SetName == "批次-当日所有药品-标题-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('批次-当日所有药品-标题-画面显示','0','9','20','(Value)0:隐藏,1:显示  (Value2 标题字体大小):1~20 建议只用9或10   (Value3 标题高度设置):20~30 建议只用20到30 ','批次画面，右下角，显示病人当天所有瓶签，标题设置。','3701')   ");
            }
            else if (SetName == "批次-当日所有药品-字体大小-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('批次-当日所有药品-字体大小-画面显示','9','25','0','(Value):1~20 建议只用9或10     (Value2 行高设置):20~40 建议只用25到35  (Value3 字体设置):0普通1粗体2斜体3下划线   ','批次画面，右下角，显示病人当天所有瓶签，字体大小设置。','3701')   ");
            }
            else if (SetName == "批次-当日所有药品-状态-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('批次-当日所有药品-状态-画面显示','55','----','----','(Value)0:不显示, 1~300：显示宽度','批次画面，右下角，显示病人当天所有瓶签，标题设置。','3702')   ");
            }
            else if (SetName == "批次-当日所有药品-药品名-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('批次-当日所有药品-药品名-画面显示','170','----','----','(Value)0:不显示, 1~300：显示宽度','批次画面，右下角，显示病人当天所有瓶签，药品名列的设置。','3703')   ");
            }
            else if (SetName == "批次-当日所有药品-频次-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('批次-当日所有药品-频次-画面显示','0','----','----','(Value)0:不显示, 1~300：显示宽度(Value2)(Value3)','批次画面，右下角，显示病人当天所有瓶签，用法列的设置。','3704')   ");
            }
            else if (SetName == "批次-当日所有药品-用量-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('批次-当日所有药品-用量-画面显示','70','----','----','(Value)0:不显示, 1~300：显示宽度(Value2)(Value3)','批次画面，右下角，显示病人当天所有瓶签，用量列的设置。','3705')   ");
            }
            else if (SetName == "批次-当日所有药品-批次-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('批次-当日所有药品-批次-画面显示','70','1','----','(Value)0:不显示, 1~300：显示宽度 (Value2) 0:不合并相同批次显示 1：合并相同批次显示  (Value3)','批次画面，右下角，显示病人当天所有瓶签，批次列的设置。','3706')   ");
            }
            else if (SetName == "批次-当日所有药品-组号-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('批次-当日所有药品-组号-画面显示','0','----','----','(Value)0:不显示, 1~300：显示宽度(Value2)(Value3)','批次画面，右下角，显示病人当天所有瓶签，批次列的设置。','3707')   ");
            }
            else if (SetName == "批次-当日所有药品-溶媒-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('批次-当日所有药品-溶媒-画面显示','0','----','----','(Value)0:不显示, 1~300：显示宽度(Value2)(Value3)','批次画面，右下角，显示病人当天所有瓶签，批次列的设置。','3708')   ");
            }
            else if (SetName == "批次-当日所有药品-病区-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('批次-当日所有药品-病区-画面显示','0','----','----','(Value)0:不显示, 1~300：显示宽度(Value2)(Value3)','批次画面，右下角，显示病人当天所有瓶签，批次列的设置。','3709')   ");
            }
            else if (SetName == "批次-当日所有药品-床号-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('批次-当日所有药品-床号-画面显示','0','----','----','(Value)0:不显示, 1~300：显示宽度(Value2)(Value3)','批次画面，右下角，显示病人当天所有瓶签，批次列的设置。','3710')   ");
            }
            else if (SetName == "批次-当日所有药品-患者名-画面显示")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('批次-当日所有药品-患者名-画面显示','70','----','----','(Value)0:不显示, 1~300：显示宽度(Value2)(Value3)','批次画面，右下角，显示病人当天所有瓶签，患者名列的设置。','3711')   ");
            }
            else if (SetName == "打印-界面设置")
            {
                IntStr.Append("    INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('打印-界面设置','0','----','----','0:隐藏,1:显示','打印画面，设置按钮，是否显示设置。','4101')   ");
            }
            else if (SetName == "打印-统药单按钮")
            {
                IntStr.Append("   INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("   VALUES('打印-统药单按钮','0','----','----','0:隐藏,1:显示','打印画面，统药单按钮，是否显示设置。','4102')   ");
            }
            else if (SetName == "打印-计费按钮")
            {
                IntStr.Append("    INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('打印-计费按钮','0','----','----','0:隐藏,1:显示','打印画面，计费按钮，是否显示设置。','4103')   ");
            }
            else if (SetName == "打印-发药机按钮")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('打印-发药机按钮','0','----','----','0:隐藏,1:显示','打印画面，发药机按钮，是否显示设置。','4104')   ");
            }
            else if (SetName == "打印-重排仓位按钮")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('打印-重排仓位按钮','0','----','----','0:不显示,1:显示','打印画面，重排仓位按钮，是否显示设置','4105')   ");
            }
            else if (SetName == "打印-打印按钮-预览模式下")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('打印-打印按钮-预览模式下','0','----','----','0:不可用,1:可用','打印画面，打印-打印按钮-预览模式下，是否可用。','4205')   ");
            }
            else if (SetName == "打印-页面自选-画面显示")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('打印-页面自选-画面显示','0','----','----','0:隐藏,1:显示','打印画面，选择第几张瓶签到第几张瓶签勾选，是否可用。','4206')   ");
            }
            else if (SetName == "打印-瓶签模糊查询-画面显示")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('打印-瓶签模糊查询-画面显示','0','----','----','0:隐藏,1:显示','打印画面，模糊查询输入框，是否可用。','4207')   ");
            }
            else if (SetName == "打印-姓名选择-瓶签显示")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('打印-姓名选择-瓶签显示','0','----','----','0:隐藏,1:显示','打印画面，瓶签上显示的排药，配置，打包人选择。','4208')   ");
            }
            else if (SetName == "打印-打印确认-统药单设置")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('打印-打印确认-统药单设置','0','----','----','0:不显示,1:显示(不勾选),2:显示(勾选)','打印画面，打印确认的弹窗中,设置统药单的勾选框的初始显示','4209')   ");
            }
            else if (SetName == "打印-打印确认-打印完成后调用画面")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('打印-打印确认-打印完成后调用画面','0','确认画面显示信息','----','[Value] 0:不显示,1:摆药核对,2:排药核对,3:进仓核对,4:配置核对,5:出仓核对,6:打包核对（[Value2]弹出框提示信息）','打印画面，打印完成后，是否弹出后续调用画面确认。','4210')   ");
            }
            else if (SetName == "打印-瓶签打印状态下拉框-内容显示")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('打印-瓶签打印状态下拉框-内容显示','0','----','----','0:不显示,1:显示','打印画面，按瓶签打印状态筛选下拉框中，设置是否有全部这个选项','4211')   ");
            }
            else if (SetName == "打印-打印计费-弹窗设置")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('打印-打印计费-弹窗设置','20','----','----','20~ 单位秒','打印画面，打印计费时，设置弹窗出现的时间','4212')   ");
            }
            else if (SetName == "打印-多药数量设置")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('打印-多药数量设置','3','----','----','3~  填写3 以上数字 ','筛选药品数量中，有一个多药，数字代表大于等于这个数字药品的瓶签属于多药。','4801')   ");
            }
            else if (SetName == "打印-瓶签查询-瓶签状态显示配置-不可打印")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('打印-瓶签查询-瓶签状态显示配置-不可打印','其他','耗材已收','欠费','remark3=1为[Value] / remark3=3为[Value2] / remark3=9为[Value3] ','打印画面与瓶签查询画面，显示瓶签状态灵活配置。','4802')   ");
            }
            else if (SetName == "打印-瓶签查询-瓶签状态显示配置-可打印")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('打印-瓶签查询-瓶签状态显示配置-可打印','配置1','配置2','配置3','remark3=22为[Value] / remark3=23为[Value2] / remark3=23为[Value3] ','打印画面与瓶签查询画面，显示瓶签状态灵活配置。','4803')   ");
            }
            else if (SetName == "核对-摆药核对-画面颜色")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('核对-摆药核对-画面颜色','0','---','---','0~  填写0-5 之间数字 ','摆药核对中，设置3块不同区域的颜色风格。','5101')   ");
            }
            else if (SetName == "核对-排药核对-画面颜色")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('核对-排药核对-画面颜色','0','---','---','0~  填写0-5 之间数字 ','排药核对中，设置3块不同区域的颜色风格。','5201')   ");
            }
            else if (SetName == "核对-进仓核对-画面颜色")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('核对-进仓核对-画面颜色','0','---','---','0~  填写0-5 之间数字 ','进仓核对中，设置3块不同区域的颜色风格。','5301')   ");
            }
            else if (SetName == "核对-配置核对-画面颜色")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('核对-配置核对-画面颜色','0','---','---','0~  填写0-5 之间数字 ','配置核对中，设置3块不同区域的颜色风格。','5401')   ");
            }
            else if (SetName == "核对-出仓核对-画面颜色")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('核对-出仓核对-画面颜色','0','---','---','0~  填写0-5 之间数字 ','出仓核对中，设置3块不同区域的颜色风格。','5501')   ");
            }
            else if (SetName == "核对-打包核对-画面颜色")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('核对-打包核对-画面颜色','0','---','---','0~  填写0-5 之间数字 ','打包核对中，设置3块不同区域的颜色风格。','5601')   ");
            }

            else if (SetName == "核对-摆药核对-按钮显示")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('核对-摆药核对-按钮显示','1','---','---','0:不显示,1:显示','核对界面中是否显示摆药核对按钮。','5102')   ");
            }
            else if (SetName == "核对-排药核对-按钮显示")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('核对-排药核对-按钮显示','1','---','---','0:不显示,1:显示','核对界面中是否显示排药核对按钮。','5202')   ");
            }
            else if (SetName == "核对-进仓核对-按钮显示")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('核对-进仓核对-按钮显示','1','---','---','0:不显示,1:显示','核对界面中是否显示进仓核对按钮。','5303')   ");
            }
            else if (SetName == "核对-配置核对-按钮显示")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('核对-配置核对-按钮显示','1','---','---','0:不显示,1:显示','核对界面中是否显示配置核对按钮。','5404')   ");
            }
            else if (SetName == "核对-出仓核对-按钮显示")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('核对-出仓核对-按钮显示','1','---','---','0:不显示,1:显示','核对界面中是否显示出仓核对按钮。','5505')   ");
            }
            else if (SetName == "核对-打包核对-按钮显示")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('核对-打包核对-按钮显示','1','---','---','0:不显示,1:显示','核对界面中是否显示打包核对按钮。','5606')   ");
            }
            else if (SetName == "核对-贴签核对-按钮显示")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('核对-贴签核对-按钮显示','0','---','---','0:不显示,1:显示','核对界面中是否显示贴签核对按钮。','5607')   ");
            }

            else if (SetName == "瓶签查询-画面初始最大化")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('瓶签查询-画面初始最大化','0','----','----','0:原始大小,1:最大化','瓶签查询画面，画面启动时，全屏显示，或者初始化大小显示设置。','8001')   ");
            }
            else if (SetName == "瓶签查询-按钮一")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('瓶签查询-按钮一','按钮1','0','0,0,0','(value1：按钮名称)(value2：0不显示；1显示)(value3：按钮背景三原色,如255,255,255)','瓶签查询画面，画面启动时，第一个按钮的显示设置。','8002')   ");
            }
            else if (SetName == "瓶签查询-按钮二")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('瓶签查询-按钮二','按钮2','0','0,0,0','(value1：按钮名称)(value2：0不显示；1显示)(value3：按钮背景三原色，如255,255,255)','瓶签查询画面，画面启动时，第二个按钮的显示设置。','8003')   ");
            }
            else if (SetName == "瓶签查询-按钮三")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('瓶签查询-按钮三','按钮3','0','0,0,0','(value1：按钮名称)(value2：0不显示；1显示)(value3：按钮背景三原色，如255,255,255)','瓶签查询画面，画面启动时，第三个按钮的显示设置。','8004')   ");
            }
            else if (SetName == "瓶签查询-按钮一-调用")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('瓶签查询-按钮一-调用','1','----','----','1~ 1:瓶签号为空不能调用 0:瓶签号为空可调用','瓶签查询画面，点击第一个按钮时的调用方式设置。','8005')   ");
            }
            else if (SetName == "瓶签查询-按钮二-调用")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('瓶签查询-按钮二-调用','1','----','----','1~ 1:瓶签号为空不能调用 0:瓶签号为空可调用','瓶签查询画面，点击第一个按钮时的调用方式设置。','8006')   ");
            }
            else if (SetName == "瓶签查询-按钮三-调用")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('瓶签查询-按钮三-调用','1','----','----','1~ 1:瓶签号为空不能调用 0:瓶签号为空可调用','瓶签查询画面，点击第一个按钮时的调用方式设置。','8007')   ");
            }

            else if (SetName == "瓶签修改确认-打包核对/配置取消确认")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('瓶签修改确认-打包核对/配置取消确认','0','----','----','Value: 0_不可切换打包核对与配置取消状态 1_可切换','确认打包核对与配置取消界面，工作状态切换是否可用配置。','8071')   ");
            }
            else if (SetName == "医嘱查询-画面初始最大化")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('医嘱查询-画面初始最大化','0','----','----','0:原始大小,1:最大化','医嘱查询画面，画面启动时，全屏显示，或者初始化大小显示设置。','8101')   ");
            }
            else if (SetName == "医嘱查询-画面初始病区勾选")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('医嘱查询-画面初始病区勾选','1','----','----','（Value）0 全部不勾选 1 勾选第一个病区 2 全选全部病区  ','医嘱查询画面，画面启动时，左侧病区列表选择配置。','8102')   ");
            }
            else if (SetName == "医嘱查询-普抗化营筛选-画面显示")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('医嘱查询-普抗化营筛选-画面显示','0','----','----','0:隐藏,1:显示','医嘱查询画面，普抗化营筛选，是否显示。','8302')   ");
            }
            else if (SetName == "医嘱查询-长期临时筛选-画面显示")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('医嘱查询-长期临时筛选-画面显示','0','----','----','0:隐藏,1:显示','医嘱查询画面，长期临时筛选，是否显示。','8303')   ");
            }
            else if (SetName == "医嘱查询-模糊查询框-画面显示")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('医嘱查询-模糊查询框-画面显示','0','----','----','0:隐藏,1:显示','医嘱查询画面，模糊查询框，是否显示。','8304')   ");
            }
            else if (SetName == "医嘱查询-今日有瓶签勾选-画面显示")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('医嘱查询-今日有瓶签勾选-画面显示','0','0','----','（Value:0:隐藏,1:显示）（Value2:0:不勾选,1:勾选）','医嘱查询画面，今日有瓶签勾选，是否显示，是否默认勾选','8305')   ");
            }
            else if (SetName == "医嘱查询-默认开始时间/结束时间-画面显示")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('医嘱查询-默认开始时间/结束时间-画面显示','-90','0','----','Value:开始时间,Value2:结束时间','0为今日，多少天前，就写负数数字。','8306')   ");
            }
            else if (SetName == "护士站_气泡消息框")
            {
                IntStr.Append("  INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])    ");
                IntStr.Append("  VALUES('护士站_气泡消息框','0','0','----','(Value自动弹出 0:不可用,1:可用）(Value2 双击弹出 0:不可用,1:可用）','护士站气泡弹出框的设置','9001')   ");
            }
            else if (SetName == "护士站-按钮一")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('护士站-按钮一','按钮1','0','0,0,0','(value1：按钮名称)(value2：0不显示；1显示)(value3：按钮背景三原色,如255,255,255)','护士站画面，画面启动时，第一个按钮的显示设置。','9002')   ");
            }
            else if (SetName == "护士站-按钮二")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('护士站-按钮二','按钮2','0','0,0,0','(value1：按钮名称)(value2：0不显示；1显示)(value3：按钮背景三原色,如255,255,255)','护士站画面，画面启动时，第二个按钮的显示设置。','9003')   ");
            }
            else if (SetName == "护士站-按钮三")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('护士站-按钮三','按钮3','0','0,0,0','(value1：按钮名称)(value2：0不显示；1显示)(value3：按钮背景三原色,如255,255,255)','护士站画面，画面启动时，第三个按钮的显示设置。','9004')   ");
            }
            else if (SetName == "护士站-系统审方-强制执行")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('护士站-系统审方-强制执行','0','---','----','0~ 0不改变 1 改变','护士站，系统审方画面，强制执行处方是否改变处方状态','9100')   ");
            }
            else if (SetName == "护士站-单项退药-确认")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('护士站-单项退药-确认','1','---','----','1~ 0不需要确认 1 需要确认','护士站，单项退药画面，瓶签配置取消，是否需要确认','9301')   ");
            }
            else if (SetName == "护士站-提前打包-确认")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('护士站-提前打包-确认','1','---','----','1~ 0不需要确认 1 需要确认','护士站，提前打包，是否需要确认','9301')   ");
            }
            else if (SetName == "护士站-标题显示-1")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('护士站-标题显示-1','1','1','1','0:不显示1:显示  [Value]-系统审方处理,[Value2]-单项退药,[Value3]-提前打包 ','护士站，大标题显示设置','9302')   ");
            }
            else if (SetName == "护士站-标题显示-2")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('护士站-标题显示-2','1','1','1','0:不显示1:显示  [Value]-等待签收,[Value2]-医嘱查询,[Value3]-瓶签查询 ','护士站，大标题显示设置','9303')   ");
            }
            else if (SetName == "护士站-系统审方处理")
            {
                IntStr.Append("     INSERT INTO [PivasAllSet]([Pro],[Value],[Value2],[Value3],[Remark],[Caption],[OrdBy])   ");
                IntStr.Append("     VALUES('护士站-系统审方处理','1','--','--','0:不显示1:显示  [Value]-药师审方结果查询(按钮是否显示设置),[Value2]-,[Value3]- ','护士站，系统审方处理画面设置','9304')   ");
            }
            else
            { IntStr.Length = 0; }

            try
            {
                if (IntStr.Length  != 0)
                {
                    SetPIVAsDB(IntStr.ToString());//将设置项插入数据库。
                }
                else
                { MessageBox.Show("未找到 : " + SetName + "  所对应的配置项。"); }
            }
            catch
            { MessageBox.Show("配置项 :  "+SetName+"   ——插入时出错。"); }

        }
        #endregion

        #region HIS需回滚事务的操作
        // Fields
        private string connString;
        private SqlDataAdapter sqlAdapter;
        private SqlCommand sqlCmd;
        private SqlConnection sqlConn;
        private DataSet sqlData = null;
        private SqlTransaction sqlTran;

        public int addAndUpdate(string sql)
        {
            this.getConnection();
            this.sqlCmd = new SqlCommand(sql, this.sqlConn);
            this.sqlCmd.Transaction = this.sqlTran;
            try
            {
                int num = this.sqlCmd.ExecuteNonQuery();
                this.sqlTran.Commit();
                this.closeConnection();
                return num;
            }
            catch (Exception exception)
            {
                this.Rollback(exception);
            }
            return 0;
        }

        private void closeConnection()
        {
            if (this.sqlData != null)
            {
                this.sqlData.Dispose();
            }
            if (this.sqlCmd != null)
            {
                this.sqlCmd.Dispose();
            }
            if (this.sqlAdapter != null)
            {
                this.sqlAdapter.Dispose();
            }
            if (this.sqlConn != null)
            {
                this.sqlConn.Close();
                this.sqlConn.Dispose();
            }
        }

        private void getConnection()
        {
            try
            {
                string connectionString = string.IsNullOrEmpty(PIVAsInfo) ? DatebasePIVAsInfo() : PIVAsInfo;
                this.SqlConn = new SqlConnection(connectionString);
            }
            catch
            {
                this.SqlConn = new SqlConnection(this.connString);
            }
            this.SqlConn.Open();
            this.SqlTran = this.SqlConn.BeginTransaction();
        }

        public List<Dictionary<string, object>> query(string sql, string tableName)
        {
            this.queryConnection();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            try
            {
                this.sqlAdapter = new SqlDataAdapter(sql, this.sqlConn);
                this.sqlData = new DataSet();
                this.sqlAdapter.Fill(this.sqlData, tableName);
                DataTable table = this.sqlData.Tables[tableName];
                foreach (DataRow row in table.Rows)
                {
                    Dictionary<string, object> item = new Dictionary<string, object>();
                    foreach (DataColumn column in table.Columns)
                    {
                        item.Add(column.ColumnName, row[column]);
                    }
                    list.Add(item);
                }
            }
            catch (Exception exception)
            {
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                dictionary2.Add("error", exception.ToString());
                list.Clear();
                list.Add(dictionary2);
            }
            return list;
        }

        private void queryConnection()
        {
            try
            {
                string connectionString = string.IsNullOrEmpty(PIVAsInfo) ? DatebasePIVAsInfo() : PIVAsInfo;
                this.SqlConn = new SqlConnection(connectionString);
            }
            catch
            {
                this.SqlConn = new SqlConnection(this.connString);
            }
            this.SqlConn.Open();
        }

        private void Rollback(Exception ex)
        {
            try
            {
                this.sqlTran.Rollback();
            }
            catch (Exception exception)
            {
                throw new Exception("Fail to Rollback:" + exception.Message);
            }
            throw new Exception("Success to Rollback:" + ex.Message);
        }

        // Properties
        public string ConnString
        {
            get
            {
                return this.connString;
            }
            set
            {
                this.connString = value;
            }
        }

        public SqlDataAdapter SqlAdapter
        {
            get
            {
                return this.sqlAdapter;
            }
            set
            {
                this.sqlAdapter = value;
            }
        }

        public SqlCommand SqlCmd
        {
            get
            {
                return this.sqlCmd;
            }
            set
            {
                this.sqlCmd = value;
            }
        }

        public SqlConnection SqlConn
        {
            get
            {
                return this.sqlConn;
            }
            set
            {
                this.sqlConn = value;
            }
        }

        public DataSet SqlData
        {
            get
            {
                return this.sqlData;
            }
            set
            {
                this.sqlData = value;
            }
        }

        public SqlTransaction SqlTran
        {
            get
            {
                return this.sqlTran;
            }
            set
            {
                this.sqlTran = value;
            }
        }
        #endregion
    }
}
