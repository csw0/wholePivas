using System;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;
using System.Security.Cryptography;
using PIVAsCommon.Helper;

namespace PivasNurse
{
    public class conntion
    {
        #region 保存PIVAs系统日志方法

        DB_Help db = new DB_Help();

        /// <summary>
        /// 将用户信息保存本地
        /// </summary>
        /// <param name="username">登录</param>
        /// <param name="pwd">密码</param>
        public void PreserveLog(string username, string pwd)
        {
            StringBuilder xml = new StringBuilder();
            try
            {
                DataSet ds = XmlToDataTable(RedStringTxT());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow[] drws = ds.Tables[0].Select("name=" + username + "");
                    if (drws.Length <= 0)
                    {
                        DataRow Row = ds.Tables[0].NewRow();
                        Row["name"] = username;
                        Row["pwd"] = pwd;
                        Row["date"] = DateTime.Now;
                        //ds.Tables[0].Rows.Add(new object[] { username, pwd, DateTime.Now });
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
                string path = @paths + "NurseUserInfo" + ".xml";
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
                string path = @paths + "NurseUserInfo" + ".xml";
                StreamWriter sw = File.AppendText(path);//向日志文件写XML信息               
                sw.WriteLine(Encrypt(xml.ToString()));
                sw.Flush();
                sw.Close();
            }
        }

        #endregion

        /// <summary>
        /// XML转换成DataSet
        /// </summary>
        /// <param name="outXml">XMl</param>
        /// <returns></returns>
        /// 
        public DataSet XmlToDataTable(string outXml)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(outXml);
            XmlNodeReader reader = new XmlNodeReader(xml);
            DataSet ds = new DataSet();
            ds.ReadXml(reader);
            return ds;
        }

        /// <summary>
        /// 获取DataTable前N条数据
        /// </summary>
        /// <param name="TopItem">Top N</param>
        /// <param name="oDT">DataTable</param>
        /// <returns></returns>
        public DataTable DtSelectTop(int TopItem, DataTable oDT)
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

        /// <summary>
        /// 读取txt文件内容
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string RedStringTxT()
        {
            StringBuilder buf = new StringBuilder();
            try
            {
                using (FileStream file = new FileStream(@"login/NurseUserInfo.xml", FileMode.Open, FileAccess.Read))
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
                return ex.ToString();
            }

        }

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
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
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
            catch (Exception ex)
            {
                return "";

             
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
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        #endregion
    }
}
