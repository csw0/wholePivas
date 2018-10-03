using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlTypes;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Xml;
using System.Web.Security;
using System.IO;
using System.Data.OracleClient;


namespace EDA
{
    public class DBHelp
    {
        #region PIVAs数据库连接配置
        private static string PIVAsPath;
        private static string PIVAsHISPath;
        private static string DatebasePIVAsHIS;
        private static string DatebasePIVAsHISOra;
        private static string DatebasePIVAs;
        
        public DBHelp()
        {
            try
            {
                PIVAsPath = GetCurrentDirectory() + "\\IMEQPIVAs.xml"; 
                //PIVAsHISPath = Application.StartupPath + "//IMEQPIVAsHIS.ini";
                //DatebasePIVAsHIS = DatebasePIVAsHISInfo();
                //DatebasePIVAsHISOra = DatebasePIVAsHISOracle();
                DatebasePIVAs = DatebasePIVAsInfo();
            }
            catch (Exception ex)
            {                
                PreserveLog("配置文件路径异常", ex.Message);
            }
        }

        private static string GetCurrentDirectory()
        {
            try
            {
                string strPath;
                strPath = "";
                strPath = System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName;
                int pos;
                pos = strPath.LastIndexOf("\\");
                strPath = strPath.Substring(0, pos + 1);
                return strPath;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }            
        }

        /// <summary>
        /// 读取xml文件路径
        /// </summary>
        /// <returns></returns>
        public string GetPIVAsPath()
        {
            return PIVAsPath;
        }

        /// <summary>
        /// 获取IMEQPIVAsHIS.ini路径
        /// </summary>
        /// <returns></returns>
        public string GetPIVAsHISPath()
        {
            return PIVAsHISPath;
        }

        /// <summary>
        /// 连接数据库配置
        /// </summary>
        /// <returns></returns>
        public string DatebasePIVAsInfo()
        {
            XmlDataDocument XML=new XmlDataDocument();
            XML.Load(PIVAsPath);

            StringBuilder buf = new StringBuilder();
            buf.Append("data source=" + XML.SelectSingleNode("Set/DataSource").InnerText);
            buf.Append(";initial catalog=" + XML.SelectSingleNode("Set/InitialCatalog").InnerText);
            buf.Append(";user id=" + Decrypt(XML.SelectSingleNode("Set/UserID").InnerText));
            buf.Append(";password=" + Decrypt(XML.SelectSingleNode("Set/Password").InnerText));
            //buf.Append("data source=" + IniReadValue("Database", "Data Source"));
            //buf.Append(";initial catalog=" + IniReadValue("Database", "Initial Catalog"));
            //buf.Append(";user id=" + Decrypt(IniReadValue("Database", "User ID")));
            //buf.Append(";password=" + Decrypt(IniReadValue("Database", "Password")));
            return buf.ToString();
        }

        /// <summary>
        /// 连接HISSQL数据库配置
        /// </summary>
        /// <returns></returns>
        public string DatebasePIVAsHISInfo()
        {
            StringBuilder buf = new StringBuilder();
            buf.Append("data source=" + IniReadValue2("Database", "Data Source"));
            buf.Append(";initial catalog=" + IniReadValue2("Database", "Initial Catalog"));
            buf.Append(";user id=" + Decrypt(IniReadValue2("Database", "User ID")));
            buf.Append(";password=" + Decrypt(IniReadValue2("Database", "Password")));
            return buf.ToString();
        }

        /// <summary>
        /// 连接HISOracle数据库配置
        /// </summary>
        /// <returns></returns>
        public string DatebasePIVAsHISOracle()
        {
            StringBuilder buf = new StringBuilder();
            buf.Append("Provider=" + IniReadValue2("Database", "Provider"));
            buf.Append(";Data source=" + IniReadValue2("Database", "Data Source"));
            //buf.Append(" initial catalog=" + IniReadValue2("Database", "Initial Catalog") + ";");
            buf.Append(";user Id=" + Decrypt(IniReadValue2("Database", "User ID")));
            buf.Append(";Password=" + Decrypt(IniReadValue2("Database", "Password")));
            return buf.ToString();
        }
        #endregion

        #region 读取ini文件中节点值
        /// <summary>
        /// 读取指定ini文件中节点值
        /// </summary>
        /// <param name="BigNode">大节点</param>
        /// <param name="SmallNode">小节点</param>
        /// <param name="path">ini文件路径</param>
        /// <returns></returns>
        public string Readini(string BigNode, string SmallNode, string path)
        {
            StringBuilder temp = new StringBuilder(255);
            try
            {
                int i = GetPrivateProfileString("" + BigNode + "", "" + SmallNode + "", "", temp, 255, GetCurrentDirectory() + "" + path + "");
                return temp.ToString();
            }
            catch (Exception EX)
            {
                PreserveLog("读取指定ini文件中节点值出错", EX.Message);
                return temp.ToString();
            }
        }
        //读取INI文件指定 PIVAsHIS
        /// <summary>
        /// 读取ini
        /// </summary>
        /// <param name="Database">ini节点</param>
        /// <param name="Key">Key值</param>
        /// <returns></returns>
        /// ////声明读写INI文件的API函数 
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string Database, string key, string def, StringBuilder retVal, int size, string filePath);

        public string IniReadValue(string Database, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            try
            {
                int i = GetPrivateProfileString(Database, Key, "", temp, 255, PIVAsPath);
                return temp.ToString();
            }
            catch (Exception ex)
            {
                PreserveLog("读取ini出错_" + Database + "_" + Key, ex.Message);
                return temp.ToString();
            }
        }

        //读取INI文件指定 PIVAsHIS
        /// <summary>
        /// 读取ini
        /// </summary>
        /// <param name="Database">ini节点</param>
        /// <param name="Key">Key值</param>
        /// <returns></returns>
        public string IniReadValue2(string Database, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            try
            {
                int a = GetPrivateProfileString(Database, Key, "", temp, 255, PIVAsHISPath);
                return temp.ToString();
            }
            catch (Exception ex)
            {
                PreserveLog("读取ini出错_" + Database + "_" + Key, ex.Message);
                return temp.ToString();
            }
        }
        #endregion


        #region 保存PIVAs系统日志方法
        /// <summary>      
        /// 保存同步药品日志    
        ///【操作数据库：DataLog;】 
        ///【同步处方：RecipeLog;】 
        ///【同步药品：DrugLog;】
        /// <param name="LogType">日志类型</param>        
        /// <param name="log">日志内容</param>
        public void PreserveLog(string LogType, string log)
        {
            try
            {
                string paths = "../Log/" + LogType;
                if (!Directory.Exists(paths))
                {
                    Directory.CreateDirectory(paths);
                }
                StringBuilder xm = new StringBuilder();
                xm.Append("[" + DateTime.Now + "]" + "\r\n");
                xm.Append(log);
                string path = @paths + "/IMEQLog" + DateTime.Now.ToString("yyyy_MM_dd") + ".log";
                File.AppendAllText(path, xm.ToString());
            }
            catch { }
        }
        /// <summary>
        /// 保存错误日志
        /// </summary>
        /// <param name="RandomID">错误编号</param>
        /// <param name="ErrStr">日志内容</param>
        public void InsertLog(string RandomID, string ErrStr)
        {
            SetPIVAsDB("insert into SynErrLog(RandomID,ErrTime,ErrStr) values ('" + RandomID + "',CONVERT(varchar,GETDATE(),121),'" + ErrStr + "')");
        }
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
                SqlCommand command = new SqlCommand(SqlStr, new SqlConnection(DatebasePIVAs));
                command.CommandTimeout = 3600;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(DS);
                da.Dispose();
                return DS;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                PreserveLog("DataLog", "PIVAs库返回DataSet失败" + SqlStr + ex.ToString());
                return DS;
            }
        }
        /// <summary>
        /// PIVAs数据库操作
        /// 数据插入/修改(执行命令，插入或修改数据。)
        /// </summary>
        /// <param name="SqlStr">Sql文</param>
        public int SetPIVAsDB(string SqlStr)
        {
            try
            {
                SqlCommand Command = new SqlCommand(SqlStr, new SqlConnection(DatebasePIVAs));
                Command.CommandTimeout = 3600;
                Command.Connection.Open();
                int a = Command.ExecuteNonQuery();
                Command.Connection.Close();
                return a;
            }
            catch (Exception ex)
            {
                PreserveLog("DataLog", "PIVAs库添加/修改/删除失败" + SqlStr + ex.ToString());
                //MessageBox.Show(ex.ToString());
                return 0;
            }
        }
        #endregion

        #region 操作HIS数据库方法
        /// <summary>
        ///  执行HIS数据库[执行语句，数据库类型 1 SQL；2 Oracle]
        /// </summary>
        /// <param name="sql">执行语句</param>
        /// <param name="DBType">数据库类型</param>
        /// <returns></returns>
        public DataSet GetPIVAsHISDB(string sql, string DBType)
        {
            if (DBType == "1")
            {
                return GetPIVAsHISDBSQL(sql);
            }
            else if (DBType == "2")
            {
                return GetPIVAsHisOracle(sql);
            }
            else
            {
                return new DataSet();
            }
        }
        /// <summary>
        /// 执行HIS数据库[执行语句，数据库类型 1 SQL；2 Oracle]
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="DBType">数据库类型</param>
        /// <returns></returns>
        public int SetPIVAsHISDB(string sql, string DBType)
        {
            if (DBType == "1")
            {
                return SetPIVAsHISDBSQL(sql);
            }
            else if (DBType == "2")
            {
                return SetPIVAsHisOracle(sql);
            }
            else
            {
                return 0;
            }
        }

        #region SQL数据库
        /// <summary>
        /// HIS数据库操作
        /// 数据取得(执行命令，返回DataSet)
        /// </summary>
        /// <param name="SqlStr">Sql文</param>
        /// <returns>DataSet数据集</returns>
        public DataSet GetPIVAsHISDBSQL(string SqlStr)
        {
            DataSet DS = new DataSet();
            try
            {
                SqlCommand command = new SqlCommand(SqlStr, new SqlConnection(DatebasePIVAsHIS));
                command.CommandTimeout = 3600;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(DS);
                da.Dispose();
                return DS;
            }
            catch (Exception ex)
            {
                PreserveLog("DataLog", "HIS【SQL】库返回DataSet失败" + ex.ToString());
                // MessageBox.Show(ex.ToString());
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
                SqlCommand Command = new SqlCommand(SqlStr, new SqlConnection(DatebasePIVAsHIS));
                Command.CommandTimeout = 3600;
                Command.Connection.Open();
                int a = Command.ExecuteNonQuery();
                Command.Connection.Close();
                return a;
            }
            catch (Exception ex)
            {
                PreserveLog("DataLog", "HIS【SQL】库曾/删/改失败" + ex.ToString());
                // MessageBox.Show(ex.ToString());
                return 0;
            }
        }
        #endregion

        #region Oracle数据库
        /// <summary>
        /// HIS数据库操作
        /// 数据取得(执行命令，返回DataSet)
        /// </summary>
        /// <param name="Sql">Sql文</param>
        /// <returns>DataSet数据集</returns>
        public DataSet GetPIVAsHisOracle(string sql)
        {
            DataSet ds = new DataSet();
            try
            {
                OracleCommand oraCon = new OracleCommand(sql, new OracleConnection(DatebasePIVAsHISOra));
                OracleDataAdapter oraDap = new OracleDataAdapter(oraCon);
                oraDap.Fill(ds);
                oraDap.Dispose();
                return ds;
            }
            catch (Exception ex)
            {
                PreserveLog("DataLog", "HIS【Oracle】库返回DataSet失败" + ex.ToString());
                //MessageBox.Show(ex.ToString());
                return ds;
            }

        }
        /// <summary>
        /// 操作HIS Oracle数据库
        /// </summary>
        /// <param name="sql">SQL文</param>
        /// <returns></returns>
        public int SetPIVAsHisOracle(string sql)
        {
            try
            {
                OracleCommand oraCon = new OracleCommand(sql, new OracleConnection(DatebasePIVAsHISOra));
                oraCon.Connection.Open();
                int effnum = oraCon.ExecuteNonQuery();
                oraCon.Connection.Close();
                return effnum;
            }
            catch (Exception ex)
            {
                PreserveLog("DataLog", "HIS【Oracle】库曾/删/改失败" + ex.ToString());
                //MessageBox.Show(ex.ToString());
                return 0;
            }
        }
        #endregion
        #endregion



        #region ========加密========

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string Encrypt(string Text)
        {
            return Encrypt(Text, "beijingjarlinfo");
        }
        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string Encrypt(string Text, string sKey)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.Default.GetBytes(Text);
                des.Key = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
                des.IV = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                StringBuilder ret = new StringBuilder();
                foreach (byte b in ms.ToArray())
                {
                    ret.AppendFormat("{0:X2}", b);
                }
                return ret.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion

        #region ========解密========


        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string Decrypt(string Text)
        {
            try
            {
                return Decrypt(Text, "beijingjarlinfo");
            }
            catch
            {
                return string.Empty;
            }
        }
        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string Decrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[Text.Length / 2];
            int x, i;
            for (x = 0; x < inputByteArray.Length; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        #endregion

        #region 系统警告
        /// <summary>
        /// 系统警告
        /// </summary>
        /// <param name="Msg"></param>
        public static void ShowWarning(String Msg)
        {
            //MessageBox.Show(Msg, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            MessageBox.Show(Msg, "警告");
        }
        /// <summary>
        /// 系统提示
        /// </summary>
        /// <param name="Msg"></param>
        public static void ShowHand(String Msg)
        {
            //MessageBox.Show(Msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            MessageBox.Show(Msg, "提示");
        }
        #endregion

        #region DataTable转换成Xml
        /// <summary>
        /// DataTable转换成Xml
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>Xml</returns>
        public string SerializeDataTableXml(DataTable dt)
        {
            StringBuilder strXml = new StringBuilder();
            try
            {
                strXml.AppendLine("<XmlTable>");
                foreach (DataRow dr in dt.Rows)
                {
                    strXml.AppendLine("<rows>");
                    foreach (DataColumn dc in dt.Columns)
                    {
                        strXml.AppendLine("<" + dc.ColumnName + ">" + dr[dc] + "</" + dc.ColumnName + ">");
                    }
                    strXml.AppendLine("</rows>");
                }
                strXml.AppendLine("</XmlTable>");
                return strXml.ToString();
            }
            catch (Exception ex)
            {
                PreserveLog("DataTable转换成Xml出错", ex.Message);
                return strXml.ToString();
            }
        }
        #endregion
    }
}
