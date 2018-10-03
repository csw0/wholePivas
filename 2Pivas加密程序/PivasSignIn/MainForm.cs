using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PivasSignIn
{
    /// <summary>
    /// 生成
    /// </summary>
    internal sealed partial class MainForm : Form
    {
        private string SqlConStr = string.Empty;
        private DB_Help dbHelp = new DB_Help();
        internal MainForm()
        {
            InitializeComponent();
        }

        private string Datebase()
        {
            SqlConnectionStringBuilder buf = new SqlConnectionStringBuilder();
            string SqlSerPort = dbHelp.IniReadValuePivas("Database", "SqlSerPort").Trim();
            buf.DataSource = "." + (string.IsNullOrEmpty(SqlSerPort) ? string.Empty : "," + SqlSerPort);
            buf.InitialCatalog = dbHelp.IniReadValuePivas("Database", "InitialCatalog").Trim();
            buf.UserID = Decrypt(dbHelp.IniReadValuePivas("Database", "UserID")).Trim();
            buf.Password = Decrypt(dbHelp.IniReadValuePivas("Database", "Password")).Trim();

            buf.InitialCatalog = string.IsNullOrEmpty(buf.InitialCatalog) ? "Pivas2014" : buf.InitialCatalog;
            buf.UserID = string.IsNullOrEmpty(buf.UserID) ? "laennec" : buf.UserID;
            buf.Password = string.IsNullOrEmpty(buf.Password) ? "13816350872" : buf.Password;
            return buf.ConnectionString;
        }

        private string Decrypt(string Text)
        {
            if (string.IsNullOrEmpty(Text.Trim()))
            {
                return string.Empty;
            }
            using (System.Security.Cryptography.DES des = System.Security.Cryptography.DES.Create())
            {
                try
                {
                    string sKey = "beijingjarlinfo";
                    int len = Text.Trim().Length / 2;
                    byte[] inputByteArray = new byte[len];
                    for (int x = 0; x < len; x++)
                    {
                        inputByteArray[x] = Convert.ToByte(Convert.ToInt32(Text.Substring(x * 2, 2), 16));
                    }
                    des.Key = Encoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
                    des.IV = Encoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, des.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write))
                        {
                            cs.Write(inputByteArray, 0, inputByteArray.Length);
                            cs.FlushFinalBlock();
                        }
                        return Encoding.Default.GetString(ms.ToArray());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return string.Empty;
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConStr = Datebase();
                using (SqlConnection sc = new SqlConnection(SqlConStr))
                {
                    using (SqlCommand oc = new SqlCommand())
                    {
                        oc.Connection = sc;
                        oc.CommandText = "if not exists (SELECT 1 FROM sys.all_objects where type='U' AND name='PivasDesPassWord') begin Create table [PivasDesPassWord](ID INT identity(1,1) PRIMARY key,MacSSID VARCHAR(250),PivasWord text,Dat datetime) end";
                        sc.Open();
                        oc.ExecuteNonQuery();
                        sc.Close();
                    }
                }
                label2.Text = GetHDsid().Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string GetHDsid()
        {
            string KeyPw = string.Empty;
            try
            {
                using (System.Management.ManagementClass mc = new System.Management.ManagementClass("Win32_DiskDrive"))
                {
                    string strHardDiskID = string.Empty;
                    foreach (System.Management.ManagementObject mo in mc.GetInstances())
                    {
                        if (mo["Index"].ToString().Trim() == "0")
                        {
                            foreach (System.Management.PropertyData pd in mo.Properties)
                            {
                                bool has = false;
                                switch (pd.Name.Trim())
                                {
                                    case "Caption":
                                        {
                                            strHardDiskID += mo["Caption"].ToString().Trim();
                                            break;
                                        }
                                    case "SerialNumber":
                                        {
                                            strHardDiskID += mo["SerialNumber"].ToString().Trim();
                                            has = true;
                                            break;
                                        }
                                    case "Signature":
                                        {
                                            strHardDiskID += mo["Signature"].ToString().Trim();
                                            break;
                                        }
                                    default:
                                        {
                                            break;
                                        }
                                }
                                if (has)
                                {
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    strHardDiskID = string.IsNullOrEmpty(strHardDiskID) ? "13816350872" : strHardDiskID;
                    using (System.Security.Cryptography.MD5 md = System.Security.Cryptography.MD5.Create())
                    {
                        KeyPw = BitConverter.ToString(md.ComputeHash(System.Text.Encoding.UTF8.GetBytes(strHardDiskID))).Replace("-", string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return KeyPw;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetDataObject(label2.Text);
                MessageBox.Show("已复制到剪切板！！！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text.Trim()))
                {
                    MessageBox.Show("密文输入为空！！！");
                }
                else
                {
                    using (DataSet dt = new DataSet())
                    {
                        using (StringReader sr = new StringReader(RunEsOrDs(textBox1.Text.Trim(), label2.Text.Trim(), false)))
                        {
                            dt.ReadXml(sr, XmlReadMode.ReadSchema);
                        }
                        if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
                        {
                            using (SqlCommand oc = new SqlCommand())
                            {
                                oc.Connection = new SqlConnection(SqlConStr);
                                oc.CommandText = string.Format("INSERT INTO [PivasDesPassWord] VALUES('{0}','{1}',GETDATE())", label2.Text.Trim(), textBox1.Text.Trim());
                                oc.Connection.Open();
                                int i = oc.ExecuteNonQuery();
                                oc.Connection.Close();
                                if (i > 0)
                                {
                                    MessageBox.Show("提交成功！！！");
                                }
                                else
                                {
                                    MessageBox.Show("提交失败！！！");
                                }

                            }
                        }
                        else
                        {
                            MessageBox.Show("密文输入有误！！！");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string RunEsOrDs(string ValueString, string Key, bool EsOrDs)
        {
            string RET = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(ValueString) && !string.IsNullOrEmpty(Key))
                {
                    Key = Key + Key.Length;
                    string k = string.Empty;
                    using (System.Security.Cryptography.MD5 md = System.Security.Cryptography.MD5.Create())
                    {
                        k = BitConverter.ToString(md.ComputeHash(Encoding.UTF8.GetBytes(Key))).Replace("-", string.Empty);
                    }
                    byte[] inputByteArray = EsOrDs ? System.Text.Encoding.UTF8.GetBytes(ValueString) : System.Convert.FromBase64String(ValueString);
                    byte[] rgbKey = System.Text.Encoding.UTF8.GetBytes(k.Substring(0, 8));
                    byte[] rgbIV = System.Text.Encoding.UTF8.GetBytes(k.Substring(k.Length - 8, 8));
                    using (System.Security.Cryptography.DES DCSP = System.Security.Cryptography.DES.Create())
                    {
                        using (System.IO.MemoryStream mStream = new System.IO.MemoryStream())
                        {
                            using (System.Security.Cryptography.CryptoStream cStream = new System.Security.Cryptography.CryptoStream(mStream, EsOrDs ? DCSP.CreateEncryptor(rgbKey, rgbIV) : DCSP.CreateDecryptor(rgbKey, rgbIV), System.Security.Cryptography.CryptoStreamMode.Write))
                            {
                                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                                cStream.FlushFinalBlock();
                                RET = EsOrDs ? System.Convert.ToBase64String(mStream.ToArray()) : System.Text.Encoding.UTF8.GetString(mStream.ToArray());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return RET;
        }
    }
}
