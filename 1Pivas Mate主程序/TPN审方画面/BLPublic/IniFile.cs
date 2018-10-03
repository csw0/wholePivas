using System;
using System.Collections.Generic;
using System.IO;
using System.Text; 
using System.Runtime.InteropServices;

namespace BLPublic
{
    public class IniFile
    {
        #region 读写INI文件相关API
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString", CharSet = CharSet.Ansi)]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString", CharSet = CharSet.Ansi)]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileInt(string lpApplicationName, string lpKeyName, int nDefault, string lpFileName);

        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileSectionNames", CharSet = CharSet.Ansi)]
        private static extern int GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, int nSize, string filePath);

        [DllImport("KERNEL32.DLL ", EntryPoint = "GetPrivateProfileSection", CharSet = CharSet.Ansi)]
        private static extern int GetPrivateProfileSection(string lpAppName, byte[] lpReturnedString, int nSize, string filePath);
        #endregion

        private const int BUFF_SIZE = 10240;

        private string error = null;
        private string iniFile;

        public IniFile(string _iniFile)
        {
            this.iniFile = _iniFile;
        }

        public string Error { get { return this.error; } }
         

        #region 读写操作（字符串）
        /// <summary>
        /// 向INI写入数据
        /// </summary>
        /// <PARAM name="_section">节点名</PARAM>
        /// <PARAM name="Key">键名</PARAM>
        /// <PARAM name="Value">值（字符串）</PARAM>
        public void write(string _section, string _key, string _value)
        {
            WritePrivateProfileString(_section, _key, _value, this.iniFile);
        }
        /// <summary>
        /// 读取INI数据
        /// </summary>
        /// <PARAM name="_section">节点名</PARAM>
        /// <PARAM name="Key">键名</PARAM>
        /// <PARAM name="Path">值名</PARAM>
        /// <returns>值（字符串）</returns>
        public string read(string _section, string _key, string _def="")
        {
            StringBuilder temp = new StringBuilder(BUFF_SIZE);
            int i = GetPrivateProfileString(_section, _key, _def, temp, BUFF_SIZE, this.iniFile);
            return temp.ToString();
        }
        #endregion

        #region 配置节信息
        /// <summary>
        /// 读取一个ini里面所有的节
        /// </summary>
        /// <param name="_sections"></param> 
        /// <returns>-1:没有节信息，0:正常</returns>
        public int getAllSectionNames(out string[] sections)
        {
            int MAX_BUFFER = 32767;
            IntPtr pReturnedString = Marshal.AllocCoTaskMem(MAX_BUFFER);
            int bytesReturned = GetPrivateProfileSectionNames(pReturnedString, MAX_BUFFER, this.iniFile);
            if (bytesReturned == 0)
            {
                sections = null;
                return -1;
            }
            string local = Marshal.PtrToStringAnsi(pReturnedString, (int)bytesReturned).ToString();
            Marshal.FreeCoTaskMem(pReturnedString);
            //use of Substring below removes terminating null for split
            sections = local.Substring(0, local.Length - 1).Split('\0');
            return 0;
        }

        /// <summary>
        /// 返回指定配置文件下的节名称列表
        /// </summary> 
        /// <returns></returns>
        public List<string> getAllSectionNames()
        {
            List<string> sectionList = new List<string>();
            int MAX_BUFFER = 32767;
            IntPtr pReturnedString = Marshal.AllocCoTaskMem(MAX_BUFFER);
            int bytesReturned = GetPrivateProfileSectionNames(pReturnedString, MAX_BUFFER, this.iniFile);
            if (bytesReturned != 0)
            {
                string local = Marshal.PtrToStringAnsi(pReturnedString, (int)bytesReturned).ToString();
                Marshal.FreeCoTaskMem(pReturnedString);
                sectionList.AddRange(local.Substring(0, local.Length - 1).Split('\0'));
            }
            return sectionList;
        }

        /// <summary>
        /// 得到某个节点下面所有的key和value组合
        /// </summary>
        /// <param name="_section">指定的节名称</param>
        /// <param name="keys">Key数组</param>
        /// <param name="values">Value数组</param> 
        /// <returns></returns>
        public int getAllKeyValues(string _section, out string[] keys, out string[] values)
        {
            byte[] b = new byte[65535];//配置节下的所有信息
            GetPrivateProfileSection(_section, b, b.Length, this.iniFile);
            string s = System.Text.Encoding.Default.GetString(b);//配置信息
            string[] tmp = s.Split((char)0);//Key\Value信息
            List<string> result = new List<string>();
            foreach (string r in tmp)
            {
                if (r != string.Empty)
                    result.Add(r);
            }
            keys = new string[result.Count];
            values = new string[result.Count];
            for (int i = 0; i < result.Count; i++)
            {
                string[] item = result[i].Split(new char[] { '=' });//Key=Value格式的配置信息
                //Value字符串中含有=的处理，
                //一、Value加""，先对""处理
                //二、Key后续的都为Value
                if (item.Length > 2)
                {
                    keys[i] = item[0].Trim();
                    values[i] = result[i].Substring(keys[i].Length + 1);
                }
                if (item.Length == 2)//Key=Value
                {
                    keys[i] = item[0].Trim();
                    values[i] = item[1].Trim();
                }
                else if (item.Length == 1)//Key=
                {
                    keys[i] = item[0].Trim();
                    values[i] = "";
                }
                else if (item.Length == 0)
                {
                    keys[i] = "";
                    values[i] = "";
                }
            }
            return 0;
        }

        /// <summary>
        /// 得到某个节点下面所有的key
        /// </summary>
        /// <param name="_section">指定的节名称</param>
        /// <param name="keys">Key数组</param> 
        /// <returns></returns>
        public int getAllKeys(string _section, out string[] keys)
        {
            byte[] b = new byte[65535];

            GetPrivateProfileSection(_section, b, b.Length, this.iniFile);
            string s = System.Text.Encoding.Default.GetString(b);
            string[] tmp = s.Split((char)0);
            List<string> result = new List<string>();
            foreach (string r in tmp)
                if (r != string.Empty)
                    result.Add(r);

            keys = new string[result.Count];
            int p = 0;
            for (int i = 0; i < result.Count; i++)
            {
                p = result[i].IndexOf('=');
                if (0 < p)
                   keys[i] = result[i].Substring(0, p);
                else
                   keys[i] = result[i];
            }
            return 0;
        }

        /// <summary>
        /// 获取指定节下的Key列表
        /// </summary>
        /// <param name="_section">指定的节名称</param> 
        /// <returns>Key列表</returns>
        public List<string> getAllKeys(string _section)
        {
            if (!chkFile())
                return null;

            List<string> keyList = new List<string>();
            byte[] b = new byte[65535];
            GetPrivateProfileSection(_section, b, b.Length, this.iniFile);
            string s = System.Text.Encoding.Default.GetString(b);
            string[] tmp = s.Split((char)0);
            List<string> result = new List<string>();
            foreach (string r in tmp) 
                if (r != string.Empty)
                    result.Add(r); 

            int p = 0;
            for (int i = 0; i < result.Count; i++)
            {
                p = result[i].IndexOf('=');
                if (0 < p) 
                    keyList.Add(result[i].Substring(0, p)); 
                else
                    keyList.Add(result[i]); 
            }
            return keyList;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="_section"></param> 
        /// <returns></returns>
        public List<string> getAllValues(string _section)
        {
            List<string> keyList = new List<string>();
            byte[] b = new byte[65535];
            GetPrivateProfileSection(_section, b, b.Length, this.iniFile);
            string s = System.Text.Encoding.Default.GetString(b);
            string[] tmp = s.Split((char)0);
            List<string> result = new List<string>();
            foreach (string r in tmp)
            {
                if (r != string.Empty)
                    result.Add(r);
            }

            int p = 0;
            for (int i = 0; i < result.Count; i++)
            {
                p = result[i].IndexOf('=');
                if (0 < p) 
                    keyList.Add(result[i].Substring(p+1)); 
                else
                    keyList.Add("");  
            }
            return keyList;
        }

        #endregion

        #region 通过值查找键

        /// <summary>
        /// 第一个键
        /// </summary>
        /// <param name="_section"></param>
        
        /// <param name="value"></param>
        /// <returns></returns>
        public string getFirstKeyByValue(string _section, string _value)
        {
            foreach (string key in getAllKeys(_section)) 
                if (readString(_section, key, "") == _value)
                {
                    return key;
                }
            
            return string.Empty;
        }

        /// <summary>
        /// 所有键
        /// </summary>
        /// <param name="_section"></param> 
        /// <param name="value"></param>
        /// <returns></returns>
        public List<string> getKeysByValue(string _section, string _value)
        {
            List<string> keys = new List<string>();
            foreach (string key in getAllKeys(_section))
                if (readString(_section, key, "") == _value)
                    keys.Add(key);
            
            return keys;
        }
        #endregion


        #region 具体类型的读写

        #region string
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_section"></param>
        /// <param name="_key"></param>
        /// <param name="defaultValue" />
        
        /// <returns></returns>
        public string readString(string _section, string _key, string _def="")
        {
            StringBuilder temp = new StringBuilder(BUFF_SIZE);
            GetPrivateProfileString(_section, _key, _def, temp, BUFF_SIZE, this.iniFile);
            return temp.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_section"></param>
        /// <param name="_key"></param>
        /// <param name="value"></param>

        public void writeString(string _section, string _key, string _value)
        {
            WritePrivateProfileString(_section, _key, _value, this.iniFile);
        }
        #endregion

        #region Int
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_section"></param>
        /// <param name="_key"></param>
        /// <param name="defaultValue"></param>
        
        /// <returns></returns>
        public int readInteger(string _section, string _key, int _def=0)
        {
            return GetPrivateProfileInt(_section, _key, _def, this.iniFile);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_section"></param>
        /// <param name="_key"></param>
        /// <param name="value"></param>

        public void writeInteger(string _section, string _key, int _value)
        { 
            WritePrivateProfileString(_section, _key, _value.ToString(), this.iniFile);
        }
        #endregion

        #region bool
        /// <summary>
        /// 读取布尔值
        /// </summary>
        /// <param name="_section"></param>
        /// <param name="_key"></param>
        /// <param name="defaultValue"></param>
        
        /// <returns></returns>
        public bool readBoolean(string _section, string _key, bool _def=false)
        {
            int temp = _def ? 1 : 0;

            int result = GetPrivateProfileInt(_section, _key, temp, this.iniFile);

            return (result == 0 ? false : true);

        }
        /// <summary>
        /// 写入布尔值
        /// </summary>
        /// <param name="_section"></param>
        /// <param name="_key"></param>
        /// <param name="value"></param>

        public void writeBoolean(string _section, string _key, bool _value)
        {
            string temp = _value ? "1 " : "0 ";
            WritePrivateProfileString(_section, _key, temp, this.iniFile);
        }
        #endregion

        #endregion

        #region 删除操作
        /// <summary>
        /// 删除指定项
        /// </summary>
        /// <param name="_section"></param>
        /// <param name="_key"></param>
        
        public void deleteKey(string _section, string _key)
        {
            WritePrivateProfileString(_section, _key, null, this.iniFile);
        }

        /// <summary>
        /// 删除指定节下的所有项
        /// </summary>
        /// <param name="_section"></param>
        
        public void eraseSection(string _section)
        {
            WritePrivateProfileString(_section, null, null, this.iniFile);
        }
        #endregion

        #region 判断节、键是否存在
        /// <summary>
        /// 指定节知否存在
        /// </summary>
        /// <param name="_section"></param> 
        /// <returns></returns>
        public bool existSection(string _section)
        {
            string[] sections = null;
            getAllSectionNames(out sections);
            if (sections != null) 
                foreach (var s in sections) 
                    if (s == _section)
                    {
                        return true;
                    } 

            return false;
        }
        /// <summary>
        /// 指定节下的键是否存在
        /// </summary>
        /// <param name="_section"></param>
        /// <param name="_key"></param>
        /// <returns></returns>
        public bool existKey(string _section, string _key)
        {
            List<string> lstKeys = getAllKeys(_section);
            if (lstKeys != null) 
                return lstKeys.Contains(_key);

            return false;
        }
        #endregion
         
        private bool chkFile()
        { 
            if (string.IsNullOrEmpty(this.iniFile))
            {
                this.error = "未指定ini文件";
                return false;
            }

            if (!File.Exists(this.iniFile))
            {
                this.error = "文件" + this.iniFile + "不存在";
                return false;
            }

            return true;
        }
    }
}
