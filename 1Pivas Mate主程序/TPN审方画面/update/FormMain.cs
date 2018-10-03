using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO; 
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace update
{
    public partial class frmMain : Form
    {
        private string svcURL = "";
        private string sysID = "";
        private string usrPwd = "";
        private string startExe = "";
        private Dictionary<string, int> upFiles = null;
        private bool stop = false;

        public frmMain(string[] args)
        {
            InitializeComponent();

            lblUpFile.Text = "";
            lblUpProc.Text = "";
            lblDownLoadFile.Text = "";
            lblDownLoadProc.Text = "";
            lblInfo.Text = "";
            this.upFiles = new Dictionary<string, int>();

            foreach (string prm in args)
            {
                if (0 == prm.IndexOf("-A"))
                    this.svcURL = prm.Substring(2);
                else if (0 == prm.IndexOf("-S"))
                    this.sysID = prm.Substring(2);
                else if (0 == prm.IndexOf("-E"))
                    this.startExe = prm.Substring(2);
                else if (0 == prm.IndexOf("-F")) 
                    paseFiles(prm.Substring(2)); 
            }

            if (string.IsNullOrWhiteSpace(this.svcURL))
                lblInfo.Text = "启动更新失败，未指定服务器地址.";
            else if (string.IsNullOrWhiteSpace(this.sysID))
                lblInfo.Text = "启动更新失败，未指定系统标识.";
            else if (0 < this.upFiles.Count) 
                lblUpFile.Text = "需更新" + this.upFiles.Count.ToString() + "个文件";
        }

        private void frmMain_Load(object sender, EventArgs e)
        { 
            BLPublic.Utils.setTimeout(500, () => {
                this.Invoke(new MethodInvoker(() => 
                    { 
                        if (0 == this.upFiles.Count)
                            chkUpdate();

                        else
                            doUpdate(); 
                     }));
            }); 
        }

        private void writeLine(Stream _s, string _txt)
        {
            byte[] b = Encoding.Default.GetBytes(_txt);
            _s.Write(b, 0, b.Length);
            _s.WriteByte(10);
            _s.WriteByte(13);
        }

        private void paseFiles(string _files)
        {
            string file = "";
            int i = 0;
            int p = 0;
            while (i < _files.Length)
            {
                if ('[' == _files[i])
                {
                    p = _files.IndexOf(']', i + 1);
                    if (0 <= p)
                    {
                        file = _files.Substring(i + 1, p - i - 1);
                        i = p;

                        p = file.IndexOf(',');
                        if (0 < p)
                            this.upFiles.Add(file.Substring(0, p), Convert.ToInt32(file.Substring(p + 1)));
                    }
                }

                i++;
            }
        }

        private void chkUpdate()
        {
            Update up = new Update((info) => { lblUpFile.Text = info; });
            if (!up.checkUpdate(AppDomain.CurrentDomain.BaseDirectory, this.svcURL, this.sysID))
            {
                lblUpFile.Text = up.Error;
                return;
            }

            paseFiles(up.UpdateFiles);
            if (0 < this.upFiles.Count)
                lblUpFile.Text = "需更新" + this.upFiles.Count.ToString() + "个文件";
            else
                lblUpFile.Text = "没有更新文件";
        }

        private void doUpdate()
        {
            if (0 >= this.upFiles.Count)
            {
                lblInfo.Text = "没有更新文件";
                return;
            }

            this.stop = false;
            btnUpdate.Tag = 1;
            btnUpdate.Text = "取消";
            btnUpdate.Refresh();
            Application.DoEvents();

            if ((this.svcURL.Length - 1) > this.svcURL.LastIndexOf('/'))
                this.svcURL += "/";

            this.usrPwd = Convert.ToBase64String(new ASCIIEncoding().GetBytes("laennec" + ":" +
                                BLPublic.BLCrypt.Encrypt("hhycbff", "PASSWORDKEY")));

            lbFiles.Items.Clear();
            List<string> lstReName = new List<string>();
            string fileName = "";
            string localPath = AppDomain.CurrentDomain.BaseDirectory;
            int i = 1;
            int okNum = 0;
            foreach (KeyValuePair<string, int> f in this.upFiles)
            {
                lblUpProc.Text = string.Format("{0}/{1}", i++, this.upFiles.Count);
                fileName = f.Key;
                if ((0 == string.Compare("BLPublic.dll", fileName)) || (0 == string.Compare("update.exe", fileName)))
                {
                    lstReName.Add(fileName);
                    fileName += ".temp";
                }

                if (downLoadFile(localPath + fileName, f.Key, f.Value))
                    okNum++;

                Application.DoEvents();

                if (this.stop)
                    break;
            }

            btnUpdate.Tag = null;
            btnUpdate.Text = "更新";
            lblUpFile.Text = "更新结束";
            if (this.upFiles.Count <= okNum)
            {
                if (0 < lstReName.Count)
                {
                    FileStream fs = new FileStream(localPath + "\\uprename.bat", FileMode.Create);
                    writeLine(fs, "@echo off");
                    writeLine(fs, "ping -n 2 127.0.0.1"); ////延时2秒,等待UPDATE.exe关闭
                    foreach(string f in lstReName)
                    {
                        writeLine(fs, string.Format("del {0}", f));
                        writeLine(fs, string.Format("rename {0}.temp {0}", f)); 
                    } 
                    fs.Close();
                    
                    this.Close();

                    if (!string.IsNullOrWhiteSpace(this.startExe))
                    { 
                        Application.ExitThread();
                        Thread thtmp = new Thread(new ParameterizedThreadStart(runExe)); 
                        Thread.Sleep(200);
                        thtmp.Start(this.startExe);  
                    }
                    else
                    {
                        System.Diagnostics.Process p = new System.Diagnostics.Process();
                        p.StartInfo.FileName = localPath + "\\uprename.bat";
                        p.StartInfo.Arguments = "";
                        p.StartInfo.UseShellExecute = false;
                        p.StartInfo.RedirectStandardInput = false;
                        p.StartInfo.RedirectStandardOutput = false;
                        p.StartInfo.RedirectStandardError = false;
                        p.StartInfo.CreateNoWindow = true;
                        p.Start();
                        p.WaitForExit();
                    }
                }
                else if (!string.IsNullOrWhiteSpace(this.startExe))
                {
                    this.Close();
                    System.Diagnostics.Process.Start(this.startExe);
                }
            }
            else
                lblUpFile.Text += ",未更新" + (this.upFiles.Count - okNum).ToString() + "个文件"; 
        }

    
        private void runExe(Object obj)
        {
            System.Diagnostics.Process ps = new System.Diagnostics.Process();
            ps.StartInfo.FileName = obj.ToString();
            ps.StartInfo.Arguments = "-Uuprename.bat";
            ps.Start();
        }

        private bool downLoadFile(string _saveFile, string _file, int _size)
        {
            lblInfo.Text = "";
            lblDownLoadFile.Text = "正在下载 " + _file;

            if (0 > _size)
                return false;
             
            prgProc.Maximum = (_size / 1024);
            prgProc.Value = 0;

            FileStream fs = null;
            try
            {
                WebRequest wr = WebRequest.Create(string.Format("{0}updatefile.html?systemID={1}&file={2}", this.svcURL, this.sysID, _file));
                wr.Headers.Add("Authorization", "Basic " + this.usrPwd);
                wr.Timeout = 5000;
                wr.Proxy = null;
                HttpWebResponse rsp = (HttpWebResponse)wr.GetResponse();
                if (HttpStatusCode.OK != rsp.StatusCode)
                {
                    lblInfo.Text = "没有文件下载内容";
                    return false;
                }

                if (File.Exists(_saveFile))
                    File.Delete(_saveFile);

                fs = new FileStream(_saveFile, FileMode.Create);

                Stream ns = rsp.GetResponseStream();

                byte[] nbytes = new byte[1024];
                int totalBuff= 0;
                int totalSize = 0;

                int nReadSize = ns.Read(nbytes, 0, 1024);
                while (nReadSize > 0)
                {
                    fs.Write(nbytes, 0, nReadSize);
                    totalBuff += nReadSize;
                    totalSize = totalBuff / 1024;

                    lblDownLoadProc.Text = string.Format("{0}/{1} kb", totalSize, prgProc.Maximum);
                    if (totalSize < prgProc.Maximum)
                        prgProc.Value = totalSize;
                    else
                        prgProc.Value = prgProc.Maximum;

                    this.Refresh();

                    nReadSize = ns.Read(nbytes, 0, 1024);
                }
                 
                if (totalSize >= prgProc.Maximum)
                {
                    lbFiles.Items.Add("成功 " + _file + string.Format(" {0}/{1} kb", totalBuff, _size));
                    return true;
                }
                else
                {
                    lblInfo.Text = _file + " 下载失败";
                    lbFiles.Items.Add("失败 " + _file);
                }
            }
            catch (Exception ex)
            {
                lbFiles.Items.Add("[" + _file + "] " + ex.Message);
                lblInfo.Text = ex.Message;
            }
            return false;
        }

        private void lbFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (null != lbFiles.SelectedItem)
                MessageBox.Show(lbFiles.SelectedItem.ToString());
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (null == btnUpdate.Tag)
                doUpdate();
            else
                this.stop = true;
        }
    }
}
