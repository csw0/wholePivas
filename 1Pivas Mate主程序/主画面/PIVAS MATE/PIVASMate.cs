using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PivasSynSet;
using PivasTool;
using PivasRevPre;
using PivasInfor;
using qqClient;
using PIVAsCommon.Helper;
using TPNReview;
using PivasIVRPrint;
using System.Collections.Generic;
using PivasLabelCheck;
using PivasBatch;
using PIVAsCommon;
using PivasLimitDES;

namespace PIVAS_MATE
{
    public partial class PIVASMate : Form
    {
        #region 属性
        Dictionary<Label, string> dicCurrentPage = new Dictionary<Label, string>(1);//保存选中的页面和页面使用的图片名
        Dictionary<PageType, Dictionary<Label, string>> dicAllPage = 
            new Dictionary<PageType, Dictionary<Label, string>>();//保存所有页面和页面使用的图片名
        Dictionary<PageType, UserControl> dicAllUserControlPage = new Dictionary<PageType, UserControl>();//所有画面对象

        Boolean QiPao;//气泡消息框是否使用变量
        Boolean QiPaoShuangJi;//气泡消息框是否使用变量
        //员工表ID，从1自增的
        public string DEmployeeID = string.Empty;
        //员工工号
        public string EmpCode = string.Empty;
        //员工姓名
        public string EmpName = string.Empty;
        DB_Help dbHelp = new DB_Help();
        #endregion

        #region 控制提醒窗体属性
        frmNotify fnotify;  //提醒窗体
        int remindtime = 10;//倒计时使用
        int IniRemTime = 10;//10秒弹出一次

        int keeptime = 5;//提醒窗口停留时间
        bool IsDelay = false;//是否延迟提醒，默认false不延迟
        int DelayTime = 0;
        int DelayIndex = 2;//提醒窗口combobox 索引
        bool MouseOnfrmRemind = false;//判断鼠标是否停留在提醒窗体上 true 在，false 不在
        #endregion

        #region 窗体拖动
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        const int HTLEFT = 10;
        const int HTRIGHT = 11;
        const int HTTOP = 12;
        const int HTTOPLEFT = 13;
        const int HTTOPRIGHT = 14;
        const int HTBOTTOM = 15;
        const int HTBOTTOMLEFT = 0x10;
        const int HTBOTTOMRIGHT = 17;

        private void TopHead_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0084:
                    base.WndProc(ref m);
                    Point vPoint = new Point((int)m.LParam & 0xFFFF,
                        (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPLEFT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMLEFT;
                        else m.Result = (IntPtr)HTLEFT;
                    else if (vPoint.X >= ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPRIGHT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)HTRIGHT;
                    else if (vPoint.Y <= 5)
                        m.Result = (IntPtr)HTTOP;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)HTBOTTOM;
                    break;
                case 0x0201://鼠标左键按下的消息 
                    m.Msg = 0x00A1;//更改消息为非客户区按下鼠标 
                    m.LParam = IntPtr.Zero;//默认值 
                    m.WParam = new IntPtr(2);//鼠标放在标题栏内 
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        #endregion

        public PIVASMate()
        {
            InitializeComponent();            
            InitLabelText();
        }

        #region 内存回收
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]

        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);

        /// <summary>
        /// 释放内存
        /// </summary>
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
        }
        #endregion

        /// <summary>
        /// 初始化画面使用图片名
        /// </summary>
        private void InitLabelText()
        {
            try
            {
                dicAllPage.Clear();

                Dictionary<Label, string> dicSyn = new Dictionary<Label, string>();
                dicSyn.Add(Label_Syn, "同步");
                dicAllPage.Add(PageType.SynNo, dicSyn);

                Dictionary<Label, string> dicReview = new Dictionary<Label, string>();
                dicReview.Add(Label_Checking, "审方");
                dicAllPage.Add(PageType.Review, dicReview);

                Dictionary<Label, string> dicBatch = new Dictionary<Label, string>();
                dicBatch.Add(Label_Batch, "批次");
                dicAllPage.Add(PageType.Batch, dicBatch);

                Dictionary<Label, string> dicCheck = new Dictionary<Label, string>();
                dicCheck.Add(Label_Check, "核对");
                dicAllPage.Add(PageType.Check, dicCheck);

                Dictionary<Label, string> dicPrint = new Dictionary<Label, string>();
                dicPrint.Add(Label_print, "打印");
                dicAllPage.Add(PageType.Print, dicPrint);

                Dictionary<Label, string> dicTool = new Dictionary<Label, string>();
                dicTool.Add(Label_Tool, "工具");
                dicAllPage.Add(PageType.Tool, dicTool);

                Dictionary<Label, string> dicTpnReview = new Dictionary<Label, string>();
                dicTpnReview.Add(Label_TPNReview, "TPN审方");
                dicAllPage.Add(PageType.TpnReview, dicTpnReview);
            }
            catch (Exception ex)
            {
                ShowMsgHelper.ShowError("初始化画面使用图片名出错：" + ex.Message);
            }
        }

        private void PIVASMate_Load(object sender, EventArgs e)
        {
            try
            {
                this.ControlBox = false;
                this.Text = this.Text + " (" + EmpName + ")";
                this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                this.WindowState = FormWindowState.Maximized;

                NowDate.Text = "PIVAS MATE 3.0 " + DateTime.Now.ToString();
                FormPath(DEmployeeID);
                labelLoginer.Text = EmpName + "(" + EmpCode + ")";

                //*****************张建双2014-07-16**********************//
                try
                {
                    IniRemTime = Convert.ToInt32(dbHelp.IniReadValuePivas("Remind", "RemindTime")) * 60;
                    keeptime = Convert.ToInt32(dbHelp.IniReadValuePivas("Remind", "KeepTime"));
                    remindtime = IniRemTime;
                }
                catch
                {
                    IniRemTime = 180;
                    remindtime = IniRemTime;
                    keeptime = 5;
                    dbHelp.IniWriteValuePivas("Remind", "RemindTime", "3");
                    dbHelp.IniWriteValuePivas("Remind", "KeepTime", "5");
                }

                //判断气泡消息框是否可用
                if (dbHelp.GetPivasAllSet("主画面_气泡消息框") == "0")
                {
                    QiPao = true;//执行不可用
                }
                else
                {
                    QiPao = false;
                }

                //判断气泡消息框是否可用
                if (dbHelp.GetPivasAllSetValue2("主画面_气泡消息框") == "0")
                {
                    QiPaoShuangJi = true;//执行不可用
                }
                else
                {
                    QiPaoShuangJi = false;
                }

                ShanShuo();
            }
            catch (Exception ex)
            {
                ShowMsgHelper.ShowError("主窗体加载出错：" + ex.Message);
            }
        }

        /// <summary>
        /// 二次登陆系统判断上一次停留程序路径
        /// </summary>
        /// <param name="DEmpID">登陆账号ID</param>
        protected void FormPath(string DEmpID)
        {
            try
            {
                Control_welcome we = new Control_welcome();
                Control_welcome.DEmployeeID = Convert.ToInt32(DEmployeeID);
                Control_welcome.Logname = EmpName;

                DataSet ds = dbHelp.GetPIVAsDB("select * from PivasMateFormSet where DEmployeeID='" + DEmpID + "'");
                bool noShowWelcome = true;//默认显示
                PageType pageNo = PageType.Check;//默认是页面
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    noShowWelcome = bool.Parse(ds.Tables[0].Rows[0]["Welcome"].ToString());
                    int iStartForm = Convert.ToInt32(ds.Tables[0].Rows[0]["StartForm"].ToString().Trim());
                    if (iStartForm >0 && iStartForm <8)//在PageType枚举范围内
                        pageNo = (PageType)iStartForm;
                }

                if (!noShowWelcome)//若显示，则添加
                {
                    panelContent.Controls.Add(we);
                    we.Size = panelContent.Size;
                }
                switchPage(pageNo);
            }
            catch (Exception ex)
            {
                ShowMsgHelper.ShowError("登录后设置欢迎页面和上次页面出错:" + ex.Message);
            }
        }

        /// <summary>
        /// 鼠标悬停或离开页面的效果显示
        /// </summary>
        /// <param name="labelPage">画面的label控件</param>
        /// <param name="imageName">画面使用的图片名</param>
        /// <param name="PageLabelEffect">效果</param>
        public void Hover_Leave(Label labelPage, string imageName, PageLabelEffect effect= PageLabelEffect.MouseMove)
        {
            switch (effect)
            {
                case PageLabelEffect.MouseLeave:
                    if (!dicCurrentPage.ContainsKey(labelPage))//离开的不是已选中的画面
                        labelPage.Image = (Image)PIVAS_MATE.Properties.Resources.ResourceManager.GetObject(imageName);
                    break;
                case PageLabelEffect.MouseMove:
                    labelPage.Image = (Image)PIVAS_MATE.Properties.Resources.ResourceManager.GetObject(imageName + "按下效果");
                    break;
                case PageLabelEffect.MouseDown:
                    foreach (var item in dicCurrentPage)
                    {
                        item.Key.Image = (Image)PIVAS_MATE.Properties.Resources.ResourceManager.GetObject(item.Value);
                        break;
                    }
                    labelPage.Image = (Image)PIVAS_MATE.Properties.Resources.ResourceManager.GetObject(imageName + "按下效果");
                    break;
                default:
                    break;
            }
        }

        #region 画面切换
        private void switchPage(PageType pageType)
        {
            try
            {
                Dictionary<Label, string> dicOnePage = null;
                Label showPageLabel = null;
                string imgName = string.Empty;

                dicAllPage.TryGetValue(pageType,out dicOnePage);
                foreach (var item in dicOnePage)
                {
                    showPageLabel = item.Key;
                    imgName = item.Value;
                    break;//只有一个
                }

                Hover_Leave(showPageLabel, imgName, PageLabelEffect.MouseDown);

                #region 一定要在画面效果显示后才能保存选中的画面信息(先清理)
                dicCurrentPage.Clear();
                dicCurrentPage.Add(showPageLabel, imgName);
                #endregion

                //"d"十进制整数
                Control_welcome.SetForm(int.Parse(Enum.Format(pageType.GetType(), pageType,"d")));
                showOnePage(pageType);
            }
            catch (Exception ex)
            {
                ShowMsgHelper.ShowError("画面切换出错:" + ex.Message);
            }
        }

        /// <summary>
        /// 显示一个画面，不存在创建，存在直接用
        /// </summary>
        private void showOnePage(PageType pageType)
        {
            try
            { 
                foreach (UserControl item in panelContent.Controls)
                    item.Visible = false;

                UserControl page = null;//需要显示的页面
                if (dicAllUserControlPage.ContainsKey(pageType))//包含
                {
                    dicAllUserControlPage.TryGetValue(pageType, out page);
                    page.Visible = true;
                    ((IMenuManager)page).menuBeforeSelect();
                }
                else//不存在,创建并保存到字典中
                {
                    switch (pageType)
                    {
                        case PageType.SynNo:
                            page = new Sync(DEmployeeID, EmpCode, EmpName);
                            dicAllUserControlPage.Add(PageType.SynNo, page);
                            break;
                        case PageType.Review:
                            page = new CheckPre(DEmployeeID, EmpCode, EmpName);
                            dicAllUserControlPage.Add(PageType.Review, page);
                            break;
                        case PageType.Batch:
                            page = new UserControlBatch(DEmployeeID, EmpCode, EmpName);
                            dicAllUserControlPage.Add(PageType.Batch, page);
                            break;
                        case PageType.Check:
                            page = new UserControlCheck(DEmployeeID, EmpCode, EmpName);
                            dicAllUserControlPage.Add(PageType.Check, page);
                            break;
                        case PageType.Print:
                            page = new UserControlPrint(DEmployeeID, EmpCode, EmpName);
                            dicAllUserControlPage.Add(PageType.Print, page);
                            break;
                        case PageType.Tool:
                            page = new UMaxControl(DEmployeeID, EmpCode, EmpName);
                            dicAllUserControlPage.Add(PageType.Tool, page);
                            break;
                        case PageType.TpnReview:
                            string empRole = EmpCode.Trim() == "9999" ? "G" : "E";//csw 只有9999才有管理员权限，tpn审方的设置界面
                            page = new UCTPNReview(EmpCode, empRole, DEmployeeID);
                            dicAllUserControlPage.Add(PageType.TpnReview, page);
                            break;
                        default:
                            break;
                    }
                    if (page != null)
                    {
                        page.Size = panelContent.Size;//先改变大小，减少加载闪烁
                        page.Dock = DockStyle.Fill;
                        panelContent.Controls.Add(page);
                        ((IMenuManager)page).menuBeforeSelect();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMsgHelper.ShowError("显示单个页面时出错:"+ex.Message);
            }
        }

        /// <summary>
        /// 同步画面
        /// </summary>
        private void Label_Syn_Click(object sender, EventArgs e)
        {
            if (GetPivasLimit.Instance.Limit(DEmployeeID, "01000") == false)
                return;
            switchPage(PageType.SynNo);
        }
        private void Label_Syn_MouseMove(object sender, MouseEventArgs e)
        {
            Hover_Leave(Label_Syn, "同步");
        }
        private void Label_Syn_MouseLeave(object sender, EventArgs e)
        {
            Hover_Leave(Label_Syn, "同步", PageLabelEffect.MouseLeave);
        }

        /// <summary>
        /// 工具画面
        /// </summary>
        private void Label_Tool_Click(object sender, EventArgs e)
        {
            if (GetPivasLimit.Instance.Limit(DEmployeeID, "06000") == false)
                return;

            switchPage(PageType.Tool);
        }
        private void Label_Tool_MouseMove(object sender, EventArgs e)
        {
            Hover_Leave(Label_Tool, "工具");
        }
        private void Label_Tool_MouseLeave(object sender, EventArgs e)
        {
            Hover_Leave(Label_Tool, "工具", PageLabelEffect.MouseLeave);
        }

        /// <summary>
        /// 审方画面
        /// </summary>
        private void Label_Checking_Click(object sender, EventArgs e)
        {
            if (GetPivasLimit.Instance.Limit(DEmployeeID, "02000") == false)
                return;

            switchPage(PageType.Review);
        }
        private void Label_Checking_MouseMove(object sender, MouseEventArgs e)
        {
            Hover_Leave(Label_Checking, "审方");
        }
        private void Label_Checking_MouseLeave(object sender, EventArgs e)
        {
            Hover_Leave(Label_Checking, "审方", PageLabelEffect.MouseLeave);
        }

        /// <summary>
        /// 统计画面
        /// </summary>
        private void Label_Count_Click(object sender, EventArgs e)
        {
            if (GetPivasLimit.Instance.Limit(DEmployeeID, "07000") == false)
                return;

            //Down = true;
            //Hover_Leave(Label_Count, "统计");
        }
        private void Label_Count_MouseMove(object sender, EventArgs e)
        {
            //Hover_Leave(Label_Count, "统计");
        }
        private void Label_Count_MouseLeave(object sender, EventArgs e)
        {
            //Hover_Leave(Label_Count, "统计", PageLabelEffect.MouseLeave);
        }

        /// <summary>
        /// 批次画面
        /// </summary>
        private void Label_Batch_Click(object sender, EventArgs e)
        {
            if (GetPivasLimit.Instance.Limit(DEmployeeID, "03000") == false)
                return;
            switchPage(PageType.Batch);
        }
        private void Label_Batch_MouseMove(object sender, MouseEventArgs e)
        {
            Hover_Leave(Label_Batch, "批次");
        }
        private void Label_Batch_MouseLeave(object sender, EventArgs e)
        {
            Hover_Leave(Label_Batch, "批次", PageLabelEffect.MouseLeave);
        }

        /// <summary>
        /// 核对画面
        /// </summary>
        private void Label_Check_Click(object sender, EventArgs e)
        {
            if (GetPivasLimit.Instance.Limit(DEmployeeID, "05000") == false)
                return;

            switchPage(PageType.Check);
        }
        private void Label_Check_MouseMove(object sender, EventArgs e)
        {
            Hover_Leave(Label_Check, "核对");
        }
        private void Label_Check_MouseLeave(object sender, EventArgs e)
        {
            Hover_Leave(Label_Check, "核对", PageLabelEffect.MouseLeave);
        }

        /// <summary>
        /// 打印画面
        /// </summary>
        private void Label_print_Click(object sender, EventArgs e)
        {
            if (GetPivasLimit.Instance.Limit(DEmployeeID, "04000") == false)
                return;
            switchPage(PageType.Print);
        }
        private void Label_print_MouseMove(object sender, EventArgs e)
        {
            Hover_Leave(Label_print, "打印");
        }
        private void Label_print_MouseLeave(object sender, EventArgs e)
        {
            Hover_Leave(Label_print, "打印", PageLabelEffect.MouseLeave);
        }

        /// <summary>
        /// TPN审方画面
        /// </summary>
        private void Label_TPNReview_Click(object sender, EventArgs e)
        {
            switchPage(PageType.TpnReview);
        }
        private void Label_TPNReview_MouseLeave(object sender, EventArgs e)
        {
            Hover_Leave(Label_TPNReview, "TPN审方", PageLabelEffect.MouseLeave);
        }
        private void Label_TPNReview_MouseMove(object sender, MouseEventArgs e)
        {
            Hover_Leave(Label_TPNReview, "TPN审方");
        }
        #endregion

        #region 右下角托盘右键菜单
        /// <summary>
        /// 右下角图标双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notify_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (QiPaoShuangJi)//若配置双击不可用，直接退出。
                { return; }

                DataSet ds = new DataSet();
                ds = dbHelp.GetPIVAsDB(GetSql());
                if (CheckFormIsOpen("frmNotify"))
                    fnotify.Close();
                Screen[] screens = Screen.AllScreens;
                Screen screen = screens[0];
                fnotify = new frmNotify(this.DEmployeeID);

                fnotify.setMouseOn += new frmNotify.MouseOn(SetMouseOnRemind);
                fnotify.setRemindTime += new frmNotify.RemindTime(setRemindTime);
                fnotify.setDelayTime += new frmNotify.DelayTime(setDelayTime);
                if (ds != null && ds.Tables.Count > 0)
                {
                    int index1 = 0;
                    int index2 = 0;
                    switch (IniRemTime)
                    {
                        case 60: index1 = 0; break;
                        case 180: index1 = 1; break;
                        case 300: index1 = 2; break;
                        case 600: index1 = 3; break;
                    }
                    switch (keeptime)
                    {
                        case 5: index2 = 0; break;
                        case 10: index2 = 1; break;
                        case 15: index2 = 2; break;
                        case 60: index2 = 3; break;
                    }
                    if (!fnotify.SetRemindForm(ds, index1, index2, DelayIndex))
                    {
                        fnotify.Dispose();
                        fnotify = null;
                        remindtime = IniRemTime;
                        return;
                    }
                }

                fnotify.Show();
                fnotify.Left = screen.WorkingArea.Width - fnotify.Width - 10;
                fnotify.Top = screen.WorkingArea.Height;

                while (fnotify.Top > screen.WorkingArea.Height - fnotify.Height)
                {
                    Application.DoEvents();
                    fnotify.Top--;
                }
                remindtime = IniRemTime;
            }
            catch (Exception ex)
            {
                ShowMsgHelper.ShowError("右下角图标双击出错：" + ex.Message);
            }
        }

        public void SetMouseOnRemind(bool On)
        {
            MouseOnfrmRemind = On;
        }

        /// <summary>
        /// 设置提醒窗口相关时间,单位秒
        /// </summary>
        /// <param name="time">弹出间隔时间</param>
        /// <param name="keetime">窗口停留时间</param>
        public void setRemindTime(int time, int keetime)
        {
            try
            {
                timeremind.Enabled = false;
                IniRemTime = time;
                remindtime = IniRemTime;
                keeptime = keetime;
                timeremind.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void setDelayTime(int time, int delayIndex)
        {
            this.DelayIndex = delayIndex;
            IsDelay = true;
            DelayTime = time;
        }

        private string GetSql()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append("select ISNULL(SUM(Case Level when 0 then 1 else 0 end),0 )num1, ISNULL(SUM(Case Level when 0 then 0 else 1 end),0) num2 from Prescription where PStatus = 1  ");
            Sql.Append("union all ");
            Sql.Append("select SUM(case when DATEDIFF(DD,InfusionDT,getdate())=0 then 1 else 0 end),SUM(case when DATEDIFF(DD,InfusionDT,getdate())=-1 then 1 else 0 end) from IVRecord  ");
            Sql.Append("union all ");
            Sql.Append("select COUNT(IVRecordID),0 from IVRecord where IVStatus<9 and DATEDIFF(DD,InfusionDT,getdate())=0 ");
            Sql.Append("union all ");
            Sql.Append("select ISNULL(SUM(case when DATEDIFF(DD,InfusionDT,getdate())=0 then 1 else 0 end),0),ISNULL(SUM(case when DATEDIFF(DD,InfusionDT,getdate()+1)=0 then 1 else 0 end),0) from IVRecord where IVStatus=15  ");
            Sql.Append("union all ");
            Sql.Append("select ISNULL(SUM(case when DATEDIFF(DD,InceptDT,getdate())=0 then 1 else 0 end),0),ISNULL(SUM(case when DATEDIFF(DD,InceptDT,getdate()+1)=0 then 1 else 0 end),0) from Prescription where PStatus = 3   ");
            Sql.Append(" union all ");
            Sql.Append("select count(0),0 from IVRecordUpdateWait where DATEDIFF(DD,WardInsertDT,getdate()-1)<=0  and WardAct =0 and CenterAct=0    ");//提前打包
            Sql.Append(" union all ");
            Sql.Append(" select count(0),0 from IVRecordUpdateWait where DATEDIFF(DD,WardInsertDT,getdate()-1)<=0 and WardAct =1 and CenterAct=0    ");//配置取消
            Sql.Append("");

            return Sql.ToString();
        }

        //判断窗口是否已经打开
        private bool CheckFormIsOpen(string Forms)
        {
            bool bResult = false;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == Forms)
                {
                    bResult = true;
                    break;
                }
            }
            return bResult;
        }

        /// <summary>
        /// 弹出局域网沟通工具，类似qq；建议废弃
        /// 菜单属性已设置为不可见
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmenuItemQQ_Click(object sender, EventArgs e)
        {
            this.QQTime1.Enabled = false;
            this.notify.Icon = ((System.Drawing.Icon)(resources.GetObject("notify.Icon")));
            if (CheckFormIsOpen("QQMain"))
                return;
            else
            {
                QQMain qm = new QQMain(DEmployeeID, "", "PivasMate");
                qm.TopMost = true;
                qm.ShowDialog();
            }
        }

        /// <summary>
        /// 瓶签查询，菜单项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerarchLabel_Click(object sender, EventArgs e)
        {
            ProcessHelper.StartProcess(EmpCode, "PivasLabelSelect.exe");
        }

        /// <summary>
        /// 医嘱查询，菜单项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchPre_Click(object sender, EventArgs e)
        {
            ProcessHelper.StartProcess(EmpCode, "ScanPre.exe");
        }

        /// <summary>
        /// 基础数据维护-批次规则
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuBatch_Click(object sender, EventArgs e)
        {
            ProcessHelper.StartProcess(EmpCode, "PivasFreqRule.exe");
        }

        /// <summary>
        /// 基础数据维护-药品目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuDrug_Click(object sender, EventArgs e)
        {
            ProcessHelper.StartProcess(EmpCode, "PivasDDrug.exe");
        }

        /// <summary>
        /// 基础数据维护-病区维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuWard_Click(object sender, EventArgs e)
        {
            ProcessHelper.StartProcess(EmpCode, "PivasDWard.exe");
        }

        /// <summary>
        /// 基础数据维护-剂量维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuDose_Click(object sender, EventArgs e)
        {
            ProcessHelper.StartProcess(EmpCode, "PivasDMetric.exe");
        }

        /// <summary>
        /// 基础数据维护-医院员工维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuEmployee_Click(object sender, EventArgs e)
        {
            ProcessHelper.StartProcess(EmpCode, "PivasDEmployee.exe");
        }

        /// <summary>
        /// 瓶签操作-提前打包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 提前打包ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessHelper.StartProcess(EmpCode + " 0", "PivasLabelNoCZ.exe");
        }

        /// <summary>
        /// 瓶签操作-配置取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 配置取消ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessHelper.StartProcess(EmpCode + " 1", "PivasLabelNoCZ.exe");
        }
        #endregion

        #region  标题菜单最大化，最小化，关闭；帮助
        private void panel_Help_MouseHover(object sender, EventArgs e)
        {
            this.panel_Help.BackgroundImage = global::PIVAS_MATE.Properties.Resources._34;
        }

        private void panel_Help_MouseLeave(object sender, EventArgs e)
        {
            this.panel_Help.BackgroundImage = global::PIVAS_MATE.Properties.Resources._37;
        }

        private void panel_Help_Click(object sender, EventArgs e)
        {
            foreach (var item in dicCurrentPage)
            {
                frmPivasInfor frmInfor = new frmPivasInfor(item.Key.Name);//根据Label名字创建窗体，不要随便修改名字
                frmInfor.ShowDialog();
                break;//只有一个
            }
        }
        private void Panel_Close_MouseLeave(object sender, EventArgs e)
        {
            Panel_Close.BackgroundImage = (Image)PIVAS_MATE.Properties.Resources.ResourceManager.GetObject("关闭");
        }

        private void Panel_Close_MouseMove(object sender, MouseEventArgs e)
        {
            Panel_Close.BackgroundImage = (Image)PIVAS_MATE.Properties.Resources.ResourceManager.GetObject("关闭按下时按钮");
        }

        private void Panel_Close_Click(object sender, EventArgs e)
        {
            try
            {
                Environment.Exit(0);
            }
            catch
            {
            }
        }

        private void Panel_Max_None_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                Panel_Max_None.BackgroundImage = (Image)PIVAS_MATE.Properties.Resources.ResourceManager.GetObject("最大化");
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                Panel_Max_None.BackgroundImage = (Image)PIVAS_MATE.Properties.Resources.ResourceManager.GetObject("还原");
            }
        }
        private void Panel_Max_None_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                Panel_Max_None.BackgroundImage = (Image)PIVAS_MATE.Properties.Resources.ResourceManager.GetObject("还原按下时按钮");
            }
            else
            {
                Panel_Max_None.BackgroundImage = (Image)PIVAS_MATE.Properties.Resources.ResourceManager.GetObject("最大化按下时按钮");
            }
        }

        private void Panel_Max_None_MouseLeave(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                Panel_Max_None.BackgroundImage = (Image)PIVAS_MATE.Properties.Resources.ResourceManager.GetObject("还原");
            }
            else
            {
                Panel_Max_None.BackgroundImage = (Image)PIVAS_MATE.Properties.Resources.ResourceManager.GetObject("最大化");
            }
        }
        private void Panel_Min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void Panel_Min_MouseHover(object sender, EventArgs e)
        {
            Panel_Min.BackgroundImage = (Image)PIVAS_MATE.Properties.Resources.ResourceManager.GetObject("最小化按下时按钮");
        }
        private void Panel_Min_MouseLeave(object sender, EventArgs e)
        {
            Panel_Min.BackgroundImage = (Image)PIVAS_MATE.Properties.Resources.ResourceManager.GetObject("最小化");
        }
        #endregion

        #region 定时器
        private void timeremind_Tick(object sender, EventArgs e)
        {
            try
            {
                if (QiPao)//若气泡不自动弹出，直接退出。
                { return; }

                timeremind.Enabled = false;
                if (IsDelay && DelayTime > 0)
                {
                    DelayTime--;
                    return;
                }
                else
                {
                    IsDelay = false;
                    DelayIndex = 2;
                }
                if (!MouseOnfrmRemind)
                {
                    if (fnotify == null && remindtime == 0)
                    {
                        if (CheckFormIsOpen("frmNotify"))
                        {
                            fnotify.Close();
                        }
                        DataSet ds = new DataSet();

                        ds = dbHelp.GetPIVAsDB(GetSql());

                        Screen[] screens = Screen.AllScreens;
                        Screen screen = screens[0];
                        fnotify = new frmNotify(this.DEmployeeID);
                        fnotify.setMouseOn += new frmNotify.MouseOn(SetMouseOnRemind);
                        fnotify.setRemindTime += new frmNotify.RemindTime(setRemindTime);
                        fnotify.setDelayTime += new frmNotify.DelayTime(setDelayTime);
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            int index1 = 0;
                            int index2 = 0;
                            switch (IniRemTime)
                            {
                                case 60: index1 = 0; break;
                                case 180: index1 = 1; break;
                                case 300: index1 = 2; break;
                                case 600: index1 = 3; break;
                            }
                            switch (keeptime)
                            {
                                case 5: index2 = 0; break;
                                case 10: index2 = 1; break;
                                case 15: index2 = 2; break;
                                case 60: index2 = 3; break;
                            }

                            if (!fnotify.SetRemindForm(ds, index1, index2, DelayIndex))
                            {
                                fnotify.Dispose();
                                fnotify = null;
                                remindtime = IniRemTime;
                                return;
                            }
                        }
                        else
                        {
                            fnotify.Dispose();
                            fnotify = null;
                            remindtime = IniRemTime;
                            return;
                        }
                        fnotify.Show();
                        fnotify.Left = screen.WorkingArea.Width - fnotify.Width - 10;
                        fnotify.Top = screen.WorkingArea.Height;

                        while (fnotify.Top > screen.WorkingArea.Height - fnotify.Height)
                        {
                            Application.DoEvents();
                            fnotify.Top--;
                        }
                        remindtime = IniRemTime;
                    }
                    else
                    {
                        remindtime--;
                        if (remindtime == IniRemTime - keeptime && fnotify != null)//提醒窗体显示一定时间秒后消失
                        {
                            remindtime = IniRemTime;
                            fnotify.Dispose();
                            fnotify = null;
                        }
                    }
                }

                timeremind.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //底部的时间
        private void TimeManage_Tick(object sender, EventArgs e)
        {
            NowDate.Text = "PIVAS MATE 3.0 " + DateTime.Now.ToString();
            ClearMemory();//回收内存
        }

        #region 通讯记录 
        DataSet dsQQ = new DataSet(); //通讯记录  
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PIVASMate));
        private int qq = 0;
        private void QQTimer1_Tick(object sender, EventArgs e)
        {
            if (qq == 0)
            {
                this.notify.Icon = new Icon("空白.ico");
                qq = 1;
            }
            else
            {
                this.notify.Icon = ((System.Drawing.Icon)(resources.GetObject("notify.Icon")));
                qq = 0;
            }
        }

        /// <summary>
        /// 是否闪烁
        /// </summary>
        private void ShanShuo()
        {
            StringBuilder str = new StringBuilder();
            str.Append(" select* from QQLog   where  not exists(select  ReadId from ReadLog where RdemployId='");
            str.Append(DEmployeeID);
            str.Append("' and qqlog.id=ReadLog.ReadId )   and SWardCode!=ToDEmployid and DEmployeeID!='");
            str.Append(DEmployeeID);
            str.Append("' ");
            dsQQ = dbHelp.GetPIVAsDB(str.ToString());
            if (dsQQ != null && dsQQ.Tables[0].Rows.Count > 0 && !CheckFormIsOpen("QQMain"))
            {
                QQTime1.Enabled = true;
            }
            else
            {
                QQTime1.Enabled = false;
                this.notify.Icon = ((System.Drawing.Icon)(resources.GetObject("notify.Icon")));
            }
        }
        private void QQTimer2_Tick(object sender, EventArgs e)
        {
            ShanShuo();
        }
        #endregion
        #endregion
    }
}
