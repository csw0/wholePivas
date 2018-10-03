using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PIVAsCommon.Helper
{
    /// <summary>
    ///权限管理帮助类
    /// </summary>
    public class PermitHelper
    {
        /// <summary>
        /// 加解密，加密算法详见代码分析
        /// </summary>
        /// <param name="ValueString">明文或密文</param>
        /// <param name="Key">加密种子</param>
        /// <param name="EsOrDs">false=解密，true=加密</param>
        /// <returns></returns>
        public static string EncrypOrDecryp(string ValueString, string Key, bool EsOrDs)
        {
            string RET = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(ValueString) && !string.IsNullOrEmpty(Key))
                {
                    Key = Key + Key.Length;
                    string k = BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(Key))).Replace("-", string.Empty);
                    byte[] inputByteArray = EsOrDs ? Encoding.UTF8.GetBytes(ValueString) : Convert.FromBase64String(ValueString);
                    byte[] rgbKey = Encoding.UTF8.GetBytes(k.Substring(0, 8));
                    byte[] rgbIV = Encoding.UTF8.GetBytes(k.Substring(k.Length - 8, 8));
                    using (DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider())
                    {
                        using (MemoryStream mStream = new MemoryStream())
                        {
                            using (CryptoStream cStream = new CryptoStream(mStream,
                                EsOrDs ? DCSP.CreateEncryptor(rgbKey, rgbIV) : DCSP.CreateDecryptor(rgbKey, rgbIV),
                                CryptoStreamMode.Write))
                            {
                                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                                cStream.FlushFinalBlock();
                                RET = EsOrDs ? Convert.ToBase64String(mStream.ToArray()) : Encoding.UTF8.GetString(mStream.ToArray());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RET = ex.Message;
            }
            return RET;
        }
    }
}
