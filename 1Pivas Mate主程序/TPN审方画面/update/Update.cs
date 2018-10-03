using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;


namespace update
{
    public class Update
    {
        private int[] SVC_PORTS = new int[3]{8080 + 1, 8080 + 449, 8080 + 502};

        private Action<string> onProc = null;
        private string httpCred = "";
        private string svcURL = "";
        private string upFiles = "";


        public Update(Action<string> _onProc)
        {
            this.onProc = _onProc;
        }

        public string ServerURL { get { return this.svcURL; } }
        public string UpdateFiles { get { return this.upFiles; } }

        public string Error { get; set; }

        public bool checkUpdate(string _localPath, string _svcIP, string _sysID)
        { 
            if(!Directory.Exists(_localPath))
            {
                this.Error = "本地程序目录不存在.(" + _localPath + ")";
                return false;
            }

            if (string.IsNullOrWhiteSpace(_svcIP))
            {
                this.Error = "服务器地址为空";
                return false;
            }

            if (string.IsNullOrWhiteSpace(_sysID))
            {
                this.Error = "系统标识为空";
                return false;
            }

            //判断IP地址是否为本机
            string hostName = Dns.GetHostName();
            IPHostEntry localhost = Dns.GetHostEntry(hostName);

            foreach (IPAddress ipa in localhost.AddressList)
            {
                if (ipa.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    if(_svcIP.Equals(ipa.ToString())) //服务器为本机
                        return true;
            }

            //_svcIP = "192.168.1.15";

            this.httpCred = Convert.ToBase64String(new ASCIIEncoding().GetBytes("laennec" + ":" + 
                                BLPublic.BLCrypt.Encrypt("hhycbff", "PASSWORDKEY")));

            showProc("连接服务器");
            int actPort = getSvcActivePort("http://" + _svcIP); 
            if (0 >= actPort)
            {
                this.Error = "服务器未打开";
                return false;
            }

            this.svcURL = string.Format("http://{0}:{1}/", _svcIP, actPort);

            //获取服务器文件 
            showProc("获取服务器文件信息");

            List<SvcFile> files = new List<SvcFile>();

            if (!getSvcFiles(this.svcURL + "systemfileinfo.html?systemID=" + _sysID, ref files))
                return false;

            if (0 == files.Count)
            {
                this.Error = "没有服务器文件记录";
                return false;
            }

            if ('\\' != _localPath[_localPath.Length - 1])
                _localPath += "\\";

            showProc("检查文件更新");
            foreach (SvcFile f in files)
            {
                if (File.Exists(_localPath + f.name))
                {
                    FileInfo fi = new FileInfo(_localPath + f.name);
                    if (fi.LastWriteTime < Convert.ToDateTime(f.time).AddMinutes(-1))
                        upFiles += string.Format("[{0},{1}]", f.name, f.size);
                }
                else if ("1" == f.update)  //必须安装
                    upFiles += string.Format("[{0},{1}]", f.name, f.size);
            }
             
            return true;
        }

        /// <summary>
        /// 获取服务器开启的端口
        /// </summary>
        /// <param name="_url"></param>
        /// <returns></returns>
        private int getSvcActivePort(string _url)
        {
            int rt = 0; 
            int i = 0;
            string chkID = BLPublic.BLCrypt.Encrypt("laennec", "PASSWORDKEY");
            WebRequest wr = null;
            try
            {
                while (i <= 2)
                {
                    wr = WebRequest.Create(_url + ":" + SVC_PORTS[i].ToString() + "/laennecserver.html?ID=" + chkID);
                    wr.Headers.Add("Authorization", "Basic " + this.httpCred);
                    wr.Timeout = 1000;
                    wr.Proxy = null;
                    HttpWebResponse response = (HttpWebResponse)wr.GetResponse(); 
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        rt = SVC_PORTS[i];
                        break;
                    }
                }
            }
            catch(Exception ex)
            {
                this.Error = ex.Message;
            }

            return rt;
        }

        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <param name="_url"></param>
        /// <returns></returns>
        private bool getSvcFiles(string _url, ref List<SvcFile> _files)
        {
            string strFiles = "";

            try
            {
                WebRequest wr = WebRequest.Create(_url);
                wr.Headers.Add("Authorization", "Basic " + this.httpCred);
                wr.Timeout = 2000;
                wr.Proxy = null;
                WebResponse response = wr.GetResponse();

                using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.Default))
                {
                    strFiles = reader.ReadToEnd();
                }
            }
            catch(Exception ex)
            {
                this.Error = ex.Message;
                return false;
            }

            if (string.IsNullOrWhiteSpace(strFiles))
            {
                this.Error = "服务器没有文件信息";
                return false;
            }

            //strFiles = strFiles.Replace("<root>", "<root xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">");// Encoding.UTF8.GetString(Encoding.Default.GetBytes(strFiles));
            try
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(strFiles);
                XmlElement root = (XmlElement)xdoc.DocumentElement;

                if (root.NodeType == XmlNodeType.Text)
                    this.Error = root.Value;
                else 
                {
                    foreach(XmlNode node in root.ChildNodes)
                    {
                        _files.Add(new SvcFile() { 
                            name = node.Attributes["name"].Value,
                            size = Convert.ToInt32(node.Attributes["size"].Value),
                            time = Convert.ToDateTime(node.Attributes["time"].Value.Replace("/", "-")),
                            update = node.Attributes["update"].Value,
                            describe = node.Value
                           });
                    }

                    return true;
                } 
            }
            catch (Exception ex)
            {
                this.Error = "读取服务器文件信息失败:" + ex.Message; 
            } 

            return false;
        }


        /// <summary>
        /// 显示过程信息
        /// </summary>
        /// <param name="_info"></param>
        private void showProc(string _info)
        {
            if (null != this.onProc)
                this.onProc(_info);
        }
    }

    /// <summary>
    /// 服务器返回file对象
    /// </summary>
    class SvcFile
    {
        public string name { get; set; }
        public int size { get; set; }
        public DateTime time { get; set; }
        public string update { get; set; }
        public string describe { get; set; }
    }

}
