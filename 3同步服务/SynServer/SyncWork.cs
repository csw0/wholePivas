using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using CommonUI.Froms;
using System.Text;
using PIVAsCommon;
using System.Management;
using PIVAsCommon.Helper;
using PIVAsCommon.Extensions;

namespace SynServer
{
    internal partial class SyncWork : Form
    {
        #region 私有变量
        private CommonFunc db = new CommonFunc();
        private DB_Help dbHelp = new DB_Help();

        private static bool bCanRun = false;//用于保证同一数据库只有一个同步程序
        private static bool bDBConnectStatus = false;//保存数据库连接状态
        private string synServerIP = string.Empty;//保存数据库中同步程序的最新IP

        private string HDssid = string.Empty;//加密后的硬盘序列号，加载时获取一次
        //保存网卡mac地址和ip
        private string[] arrayMacIP;

        //保存同步类别和时刻
        private static Dictionary<int, DateTime> dicSynCodeTime =  new Dictionary<int, DateTime>();

        private Thread threadUpdateMsgAndDB= null;
        private const int UPDATEMSGDB_INTERNAL = 15 * 1000;//毫秒,15秒，此间隔要尽量小
        private Thread threadUpdatePWClearLog = null;
        private const int UPDATEPWCLEARLOG_INTERNAL = 3600 * 1000;//毫秒,1小时
        private Thread threadSyncBaseDataByHIS = null;
        private const int SYNCBASEDATA_INTERNAL = 10 * 1000;//毫秒,10秒
        private Thread threadInsertSynLogByTimeCode = null;
        private const int INSERTLOGBYTIME_INTERNAL = 5 * 1000;//毫秒,5秒
        #endregion

        #region 主窗体的拖动效果
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MOVE = 0xF010;
        private const int HTCAPTION = 0x0002;

        private void SyncWork_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        #endregion

        public SyncWork()
        {
            InitializeComponent();
        }

        internal SyncWork(string args)
        {
            try
            {
                this.arrayMacIP = args.Split('|');
                InitializeComponent();
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("SyncWork构造过程出错："+ex.Message);
            }
        }

        private void SyncWork_Load(object sender, EventArgs e)
        {
            try
            {
                HDssid = GetHDsid();
                lblSyncMacIPTime.Text = string.Empty;

                #region 开启线程
                if (threadUpdateMsgAndDB == null)
                {
                    threadUpdateMsgAndDB = new Thread(CheckDBStatusAndCanRun);
                    threadUpdateMsgAndDB.IsBackground = true;
                    threadUpdateMsgAndDB.Start();
                }
                if (threadUpdatePWClearLog == null)
                {
                    threadUpdatePWClearLog = new Thread(UpdatePassWDClearLog);
                    threadUpdatePWClearLog.IsBackground = true;
                    threadUpdatePWClearLog.Start();
                }
                if (threadSyncBaseDataByHIS == null)
                {
                    threadSyncBaseDataByHIS = new Thread(SyncBaseDataByHIS);
                    threadSyncBaseDataByHIS.IsBackground = true;
                    threadSyncBaseDataByHIS.Start();
                }
                if (threadInsertSynLogByTimeCode == null)
                {
                    threadInsertSynLogByTimeCode = new Thread(InsertSynLogByTimeCode);
                    threadInsertSynLogByTimeCode.IsBackground = true;
                    threadInsertSynLogByTimeCode.Start();
                }
                #endregion

                #region csw设置开机自启动,测试时先注释
                //SetAutoRun(true);
                #endregion
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("同步程序加载出错："+ex.Message);
            }
        }

        /// <summary>
        /// 当同步程序是唯一时更新UI
        /// </summary>
        private void UpdateUIByRunning()
        {
            this.SafeAction(() =>
            {
                pictureBoxSynRuning.Visible = true;
                pictureBoxSynWait.Visible = false;
                labelShowMsg.Text = "同步服务运行中";
                labelShowMsg.ForeColor = Color.White;
            });
        }

        /// <summary>
        /// 当同步程序停止运行时更新UI
        /// </summary>
        private void UpdateUIByStop()
        {
            this.SafeAction(() =>
            {
                pictureBoxSynRuning.Visible = false;
                pictureBoxSynWait.Visible = true;
                labelShowMsg.Text = "停止状态，点击启动，将停止其他同步程序";
                labelShowMsg.ForeColor = Color.Red;
                lblSyncMacIPTime.Text = string.Empty;
            });
        }

        #region 线程事件
        /// <summary>
        /// 根据配置的同步周期，插入到日志表
        /// 原来是30秒
        /// </summary>
        void InsertSynLogByTimeCode()
        {
            while (true)
            {
                try
                {
                    if (bDBConnectStatus && bCanRun)//数据库连接成功状态且同步程序是唯一的
                    {
                        //where (DATEDIFF(MI,EndTime,GETDATE())>=SyncSpaceTime or StartTime is null
                        string sql = "SELECT [SynTimeCode],[SynStarTime],[SynEndTime],[SyncSpaceTime],[SynID],"+
                            "[SynCode],[StartTime],[StartUPTime],[EndUPTime],[EndTime],[Success],[Schedule],"+
                            "GETDATE() dat FROM [dbo].[V_Sync]";
                        using (DataSet ds = dbHelp.GetPIVAsDB(sql))
                        {
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                InsertSynLog(ds);
                            }                            
                            ListBoxMsgAddItem("检测自动同步   " + DateTime.Now.ToString("HH:mm:ss"));
                        }
                    }
                }
                catch (Exception ex)
                {
                    InternalLogger.Log.Error("插入到日志表SynLog时出错" + ex.Message);
                }
                Thread.Sleep(INSERTLOGBYTIME_INTERNAL);
            }
        }

        /// <summary>
        /// 根据同步设置模式，插入日志后“等待同步”
        /// 4=手动模式时，什么也不做，空转
        /// </summary>
        /// <param name="ds"></param>
        void InsertSynLog(DataSet ds)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                try
                {
                    string SynCode = dr["SynCode"].ToString();
                    DateTime dateTime = Convert.ToDateTime(dr["dat"].ToString());
                    string SynStarTime = dr["SynStarTime"].ToString();
                    string SynEndTime = dr["SynEndTime"].ToString();
                    string StartTime = dr["StartTime"].ToString();
                    string EndTime = dr["EndTime"].ToString();
                    int SyncSpaceTime = Convert.ToInt32(dr["SyncSpaceTime"].ToString());

                    //同步周期编号1：一次 2：时段 3：全天 4：手动
                    switch (dr["SynTimeCode"].ToString())
                    {
                        case "1":
                            if (string.IsNullOrEmpty(StartTime) || (Convert.ToDateTime(StartTime).AddHours(23) <= dateTime 
                                && Convert.ToDateTime((SynStarTime.Contains(",") ? SynStarTime.Split(',')[0] : 
                                SynStarTime) + (SynStarTime.Contains(":") ? string.Empty : ":00")).Hour == dateTime.Hour))
                            {
                                db.InsertStartSyLog(SynCode, Guid.NewGuid().ToString());
                            }
                            break;
                        case "2":
                            string[] sst = SynStarTime.Split(',');
                            string[] set = SynEndTime.Split(',');
                            bool TimIn = false;
                            for (int i = 0; i < sst.Length; i++)
                            {
                                if (Convert.ToDateTime(sst[i].Contains(":") ? sst[i] : (sst[i] + ":00")) <= dateTime && 
                                    dateTime <= Convert.ToDateTime(set[i].Contains(":") ? set[i] : (set[i] + ":00").Replace("24", "23")))
                                {
                                    TimIn = true;
                                    break;
                                }
                            }
                            if (string.IsNullOrEmpty(StartTime) || (TimIn && !string.IsNullOrEmpty(EndTime) && 
                                Convert.ToDateTime(EndTime).AddMinutes(SyncSpaceTime) < dateTime))
                            {
                                db.InsertStartSyLog(SynCode, Guid.NewGuid().ToString());
                            }
                            break;
                        case "3":
                            if (string.IsNullOrEmpty(StartTime) || (!string.IsNullOrEmpty(EndTime) && 
                                Convert.ToDateTime(EndTime).AddMinutes(SyncSpaceTime) < dateTime))
                            {
                                db.InsertStartSyLog(SynCode, Guid.NewGuid().ToString());
                            }
                            break;
                        default:break;
                    }
                }
                catch (Exception ex)
                {
                    InternalLogger.Log.Error("循环查询自动同步时出错" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 从HIS循环拉取基础数据到临时表，然后复制更新
        /// 原来是5秒定时器
        /// </summary>
        void SyncBaseDataByHIS()
        {
            while (true)
            {
                try
                {
                    if (bDBConnectStatus && bCanRun)//数据库连接成功状态且(唯一同步程序，其实就是手动点击后)
                    {
                        SyncBaseData();
                        ListBoxMsgAddItem("检测是否需要同步   " + DateTime.Now.ToString("HH:mm:ss"));
                    }
                }
                catch (Exception ex)
                {
                    InternalLogger.Log.Error("从HIS循环拉取基础数据出错" + ex.Message);
                }
                Thread.Sleep(SYNCBASEDATA_INTERNAL);
            }
        }

        /// <summary>
        /// ListBoxMsg添加一项
        /// </summary>
        void ListBoxMsgAddItem(string itemValue)
        {
            try
            {
                this.SafeAction(() =>
                {
                    if (listBoxMsg.Items.Count > 20)
                        listBoxMsg.Items.RemoveAt(0);

                    listBoxMsg.Items.Add(itemValue);
                });
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("ListBoxMsg添加" + itemValue + ex.Message);
            }
        }

        /// <summary>
        /// 同步His数据，通过V_Sync的参数确定是否有需要同步的数据
        /// V_Sync的信息是根据SynLog表更新
        /// </summary>
        void SyncBaseData()
        {
            using (DataSet ds = dbHelp.GetPIVAsDB("SELECT SynID,SynCode FROM [dbo].[V_Sync] "+
                "where EndTime is null and SynID is not null "))
            {
                if (ds == null || ds.Tables.Count <= 0)
                    return;//没有需要同步的数据

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    int SynCode = 0;
                    try
                    {
                        int.TryParse(dr["SynCode"].ToString(), out SynCode);
                        string SynID = dr["SynID"].ToString();

                        if (!dicSynCodeTime.ContainsKey(SynCode))
                        {
                            dicSynCodeTime.Add(SynCode, DateTime.Now);
                            Thread ths = new Thread(() =>
                            {
                                //开启同步线程
                                new ExecProcedures().SyncRun(SynCode.ToString(), SynID);
                                //存储过程执行完成后，移除
                                if (dicSynCodeTime.ContainsKey(SynCode))
                                {
                                    dicSynCodeTime.Remove(SynCode);
                                }
                            });
                            ths.IsBackground = true;
                            ths.Start();
                        }
                        else
                        {
                            //如果执行存储过程已超十分钟，(可能没执行完成可能执行完成)；移除
                            if (dicSynCodeTime[SynCode].AddMinutes(10) < DateTime.Now)
                            {
                                dicSynCodeTime.Remove(SynCode);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        InternalLogger.Log.Error("与HIS同步SynCode=" + SynCode.ToString() + "时出错" + ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 更新加解密秘钥和清理日志表
        /// 原来是60秒定时器
        /// </summary>
        void UpdatePassWDClearLog()
        {
            while (true)
            {
                try
                {
                    if (bDBConnectStatus && bCanRun)//数据库连接成功
                    {
                        //每两天在夜里一点清空SynLog表;数据库状态是成"功的
                        if ((DateTime.Now.Day % 2) == 0 && DateTime.Now.Hour == 1)
                            dbHelp.SetPIVAsDB("truncate table dbo.SynLog");

                        RunDesTab();
                    }
                }
                catch (Exception ex)
                {
                    InternalLogger.Log.Error("更新加解密秘钥和清理日志表出错：" + ex.Message);
                }
                Thread.Sleep(UPDATEPWCLEARLOG_INTERNAL);//小时级别
            }
        }
        
        bool CheckCanRun()
        {
            try
            {
                using (DataSet ds = dbHelp.GetPIVAsDB("SELECT top 1 isnull([SyncMAC],'nul') AS [SyncMAC] FROM [SynSet]"))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        //不是首次点击时，将最新的ip保存下来进行显示
                        if (lblSyncMacIPTime.Text != String.Empty)
                            synServerIP = ds.Tables[0].Rows[0][0].ToString().Split('|')[1];

                        // 根据数据库中最新的值，判断是否有其他程序开启
                        return Equals(lblSyncMacIPTime.Text, ds.Tables[0].Rows[0][0].ToString()); ;
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("判断同步程序是否唯一出错：" + ex.Message);
            }
            return false;
        }

        /// <summary>
        /// 根据配置文件中数据库参数，判断数据库连接状态
        /// 同步程序是否唯一；原来是5秒定时器
        /// </summary>
        void CheckDBStatusAndCanRun()
        {
            while (true)
            {
                try
                {
                    //访问数据库，判断连接性
                    if (dbHelp.TestDB())
                    {
                        StartSyncWork();//因数据库连接成功，在调用TestCanRun前实现手动点击功能
                        UpdateUIByDBSuccess(CheckCanRun());
                    }
                    else
                    {
                        bDBConnectStatus = false;
                        bCanRun = false;
                        synServerIP = String.Empty;//因数据库断开，造成停止，已有其他日志追踪，这里恢复默认值
                        UpdateUIByDBFail();
                    }
                }
                catch (Exception ex)
                {
                    InternalLogger.Log.Error("清理显示消息或判断数据库连接状态出错:" + ex.Message);
                }
                Thread.Sleep(UPDATEMSGDB_INTERNAL);//此间隔要尽量小
            }
        }

        /// <summary>
        /// 数据库断开连接时更新UI
        /// </summary>
        void UpdateUIByDBFail()
        {
            UpdatePBVisible(false);

            //程序不可运行显示
            UpdateUIByStop();

            ListBoxMsgAddItem("连接数据库失败，请检查IMEQPIVAs.ini文件和网络");
        }

        /// <summary>
        /// 更新数据库状态对应的pictureBox
        /// </summary>
        /// <param name="dbStatus">数据库状态</param>
        void UpdatePBVisible(bool dbStatus)
        {
            try
            {
                this.SafeAction(() =>
                {
                    if (dbStatus != pictureBoxSuccess.Visible)//当状态已更新后，不再重复更新
                    {
                        pictureBoxSuccess.Visible = dbStatus;
                        pictureBoxFail.Visible = !dbStatus;
                    }
                });
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("更新数据库状态对应的pictureBox出错：" + ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// 修改注册表，使看护程序开机重启
        /// </summary>
        /// <param name="isAutoRun"></param>
        /// <returns></returns>
        private bool SetAutoRun(bool isAutoRun)
        {
            RegistryKey reg = null;
            try
            {
                string fileName = Application.StartupPath + "\\SynServerWacther.exe";
                if (File.Exists(fileName))
                {
                    reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    if (reg == null)
                    {
                        reg = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                    }
                    else
                    {
                        if (isAutoRun)
                        {
                            reg.SetValue("SynServer", fileName);
                            reg.Flush();
                        }
                        else
                        {
                            reg.DeleteValue("SynServer", true);
                        }
                    }
                    return true;
                }
                else
                {
                    MessageBox.Show("执行文件不存在!");
                    return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                if (reg != null)
                    reg.Close();
            }
        }

        #region 硬盘和时间的加密处理
        /// <summary>
        /// 获取加密后的硬盘序列号
        /// </summary>
        /// <returns></returns>
        private string GetHDsid()
        {
            string KeyPw = string.Empty;
            try
            {
                using (ManagementClass mc = new ManagementClass("Win32_DiskDrive"))
                {
                    string strHardDiskID = string.Empty;
                    foreach (ManagementObject mo in mc.GetInstances())
                    {
                        if (mo["Index"].ToString().Trim() == "0")//硬盘序号，插入U盘或手机时，会出错
                        {
                            foreach (PropertyData pd in mo.Properties)
                            {
                                if (pd.Name.Trim() == "SerialNumber")//获取硬盘出厂编号
                                {
                                    strHardDiskID += mo["SerialNumber"].ToString().Trim();
                                    break;
                                }
                                else
                                {
                                    switch (pd.Name.Trim())
                                    {
                                        case "Caption"://获取硬盘名称 
                                            strHardDiskID += mo["Caption"].ToString().Trim();
                                            break;
                                        case "Signature"://获取硬盘出厂编号,此方式需管理员权限
                                            strHardDiskID += mo["Signature"].ToString().Trim();
                                            break;
                                        default: break;
                                    }
                                }
                            }
                            break;
                        }
                    }

                    strHardDiskID = string.IsNullOrEmpty(strHardDiskID) ? "13816350872" : strHardDiskID;
                    using (MD5 md = MD5.Create())
                    {
                        KeyPw = BitConverter.ToString(md.ComputeHash(Encoding.UTF8.GetBytes(strHardDiskID))).Replace("-", string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("获取加密后的硬盘序列号出错"+ ex.Message);
            }
            return KeyPw;
        }

        /// <summary>
        /// 更新PivasDesPassWord表ID最大的记录synMac为本次运行的硬盘序列号加密后的值
        /// 插入PivasDesSoftTAB表一条记录，记录时间加密后的值
        /// </summary>
        private void RunDesTab()
        {
            try
            {
                string dat = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string PW = PermitHelper.EncrypOrDecryp(dat, dat, true);
                StringBuilder sb = new StringBuilder(4096);
                sb.Append("IF OBJECT_ID('PivasDesPassWord') IS NULL ");
                sb.Append(" begin ");
                sb.Append(" Create table [PivasDesPassWord](ID INT identity(1,1) PRIMARY key,MacSSID VARCHAR(250),PivasWord text,Dat datetime) ");
                sb.Append(" end ");
                sb.Append(" else ");
                sb.Append(" begin ");
                sb.Append(string.Format(" UPDATE [PivasDesPassWord] SET [MacSSID] = '{0}' WHERE ID=(SELECT MAX(ID) FROM [PivasDesPassWord]) ", HDssid));
                sb.Append(" end ");
                sb.Append("IF OBJECT_ID('PivasDesSoftTAB') IS NULL ");
                sb.Append(" begin ");
                sb.Append(" Create table [PivasDesSoftTAB](ID INT identity(1,1) PRIMARY key,SoftPW VARCHAR(512),Dat datetime) ");
                sb.Append(string.Format(" insert into [PivasDesSoftTAB] values('{0}','{1}') ", PW, dat));
                sb.Append(" end ");
                sb.Append(" else ");
                sb.Append(" begin ");
                sb.Append(" truncate table [PivasDesSoftTAB] ");
                sb.Append(string.Format(" insert into [PivasDesSoftTAB] values('{0}','{1}') ", PW, dat));
                sb.Append(" end ");
                dbHelp.SetPIVAsDB(sb.ToString());
            }
            catch(Exception ex)
            {
                InternalLogger.Log.Error("更新加密信息出错"+ ex.Message);
            }
        }
        #endregion

        #region 数据库设置
        private void pictureBoxFail_Click(object sender, EventArgs e)
        {
            //数据库自动连接失败，将显示设置窗体；连接成功将不显示。
            ShowDBSet();
        }

        private void ShowDBSet()
        {
            DBSet form = new DBSet(DatabaseType.PivasDB);
            form.ConnectDBResult += DBSet_ConnectDBResult;
            form.ShowDialog();
        }

        private void DBSet_ConnectDBResult(object sender, EventArgs e)
        {
            try
            {
                bool DBConnStatus = (bool)sender;
                if (DBConnStatus)
                    UpdateUIByDBSuccess(CheckCanRun());
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("处理数据连接结果出错：" + ex.Message);
            }
        }

        /// <summary>
        ///  数据库连接成功时，更新参数和UI
        /// </summary>
        /// <param name="canRun">同步程序是否唯一</param>
        void UpdateUIByDBSuccess(bool canRun)
        {
            bDBConnectStatus = true;

            UpdatePBVisible(true);

            if (canRun)
            {
                bCanRun = true;
                UpdateUIByRunning();
            }
            else
            {
                bCanRun = false;
                UpdateUIByStop();
                if (!String.IsNullOrEmpty(synServerIP))//synServerIP有获取到的情况，标记停止的原因
                {
                    ListBoxMsgAddItem(String.Format("另一程序在{0}处打开，此程序停止", synServerIP));
                }
            }
        }
        #endregion

        private void pictureBoxSynWait_Click(object sender, EventArgs e)
        {
            StartSyncWork();
        }

        /// <summary>
        /// 开启同步工作
        /// </summary>
        private void StartSyncWork()
        {
            try
            {
                //lblSyncMacIPTime为空且数据库连接状态为成功
                if (string.IsNullOrEmpty(lblSyncMacIPTime.Text) && bDBConnectStatus)
                {
                    using (DataSet ds = dbHelp.GetPIVAsDB("select getdate()"))//获取数据库当前时间
                    {
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            string strMacIp = string.Empty;
                            for (int i = 0; (arrayMacIP != null && i < arrayMacIP.Length); i++)
                                strMacIp += arrayMacIP[i] + "|";

                            lblSyncMacIPTime.Text = strMacIp + Convert.ToDateTime(ds.Tables[0].Rows[0][0]).ToString("HH:mm:ss.fff");

                            //同步程序开启后，将标记值保存到SynSet表中SyncMAC字段
                            if (dbHelp.SetPIVAsDB(string.Format("UPDATE [SynSet] SET [SyncMAC] ='{0}'", lblSyncMacIPTime.Text)) > 0)
                                UpdateUIByRunning();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("开启同步工作出错" + ex.Message);
            }
        }

        #region 最小化面板效果
        private void PanelMin_MouseHover(object sender, EventArgs e)
        {
            PanelMin.BackColor = Color.FromArgb(86, 160, 255);
        }

        private void PanelMin_MouseLeave(object sender, EventArgs e)
        {
            PanelMin.BackColor = Color.FromArgb(16, 107, 225);
        }

        private void PanelMin_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            this.Hide();
        }
        #endregion

        #region 状态栏图标
        /// <summary>
        /// 窗体关闭前，释放notifyIcon1
        /// </summary>
        private void SyncWork_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.threadUpdateMsgAndDB = null;
            this.threadUpdatePWClearLog = null;
            this.threadSyncBaseDataByHIS = null; 
            this.threadInsertSynLogByTimeCode = null; 
            this.notifyIcon1.Dispose();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            notifyIcon1.Visible = false;
            this.Show();
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                notifyIcon1.Visible = false;
                notifyIcon1.Dispose();
                this.Dispose();
                this.Close();
            }
            catch 
            {
            }
        }
        private void SyncWork_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Environment.Exit(0);
            }
            catch
            {
            }
        }
        #endregion
    }
}
