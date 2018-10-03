using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using PivasLabelCheckAll.common;
using PivasLabelCheckAll.dao;
using PivasLabelCheckAll.entity;
using LabelCheckAuthoritySet;
using PivasLabelCheckAll.LabelDetails;
using PIVAsCommon.Helper;
using ChargeInterface;
using PivasLimitDES;
using PIVAsCommon;

namespace PivasLabelCheckAll
{
    public partial class UCCommonCheck : UserControl
    {
        #region 属性
        DB_Help dbHelp = new DB_Help();
        private DataSet ShowDS = new DataSet();
        public DataTable LabelTable = new DataTable();//保存符合条件的所有瓶签号;--配置取消用
        public  DataTable nodo = new DataTable();//未扫描的datatale；
        public  DataTable aldo = new DataTable();//已扫描的datatable；
        public  DataTable alldo = new DataTable();//符合条件的全部datatabl
        private DataTable drug = new DataTable();//所有药品code，名称

        public ListView TimeList = new ListView();//各种时间；

        private string CheckKind = string.Empty;
        private string CheckTable = string.Empty;
        private string CheckPro = string.Empty;
        private string CheckDT = string.Empty;
        private string CheckSim = string.Empty;
        private int CheckStatus = 0;

        private string selward = string.Empty;
        private ucLabelsInfo ucLabelsInfo1 = new ucLabelsInfo();
        private ucCountInfomation ucCountInformation1 = new ucCountInfomation();//计数信息？
        private Demployee employee = null;
        private seldb sel = new seldb();//数据库查询方案
        private DataTable WardsTable = new DataTable();//保存当前选中的病区
        public List<Dward> wardSelList = new List<Dward>();//用来存储用户锁定的病区的集合
        public List<Dward> wardUnSelList = new List<Dward>();
        private CommonUtil util = new CommonUtil();//公共方法类
        private ChargeError ce = new ChargeError();
        public int er = 0;

        private Color choseColor = Color.SkyBlue;//选中后的背景色
        private Color unChoseColor = Color.LightBlue;//未选中的背景色
        private Color pnlLeftColor = Color.LightBlue;
        private Color dgvLeftColor = Color.LightBlue;
        private Color pnlRightColor = Color.LightBlue;
        private Color dgvRightColor = Color.White;
        private Color CheckModelColor = Color.SkyBlue;
        private Color FinishColor = Color.SkyBlue;
        private Color CheckColor = Color.White;

        private Color adcolor = Color.White;

        private StringBuilder SelectRule = new StringBuilder();
        private DPatient patient = null;
        private static ShowVoice speak = new ShowVoice();//读中文语音的公共类
        private string date;
        private int flag = 1;//判断当前的页面显示在哪个部分 0：不显示 1：瓶签信息 2：统计
        private int labelFlag = 0;//判断当前页面上的瓶签是不是扫描过的 0 未扫描；1 已扫描 ;2:全部扫描默认显示未扫描

        private string selComboxValue = "";//保存第一个下拉框中的搜索数据
        public int ckFlag = 0;//保存最后一次是checkBox操作还是选择操作 0：ckbox 1:选择
        private string SQL3 = "";
        private List<Panel> pnlList = new List<Panel>();
        private List<Authority> authList = new List<Authority>();//保存权限组信息的集合
        public bool piliangFlag = true;
        public bool jifeiFlag = true;//是否在核对时，调用计费接口
        public bool batchFlag = true;//批次是否选中标志
        public bool IsShowAllDward = false;//是否显示全病区扫描完毕

        public int piliangCount = 0;//保存批量的总数
        public int piliangSucCOunt = 0;//批量扫描成功的数量
        public int piliangCheckNo = 0;//实时扫描数
        public string piliangBarcode = "";//实时扫描瓶签
        public int checkMode = 1;//核对模式，1：普通；2：总瓶签
        public DataTable dtCountSet = null;
        private bool timeFlag = true;
        private bool FreshFlag = false;

        private string LoginTime;//登陆时间
        private bool OtherLabelFlag = false;//是否是第三方瓶签
        public bool _IsBreak = false;//批量扫描-瓶签核对失败是否中断
        private bool UseDetailVoice = true;//默认使用详细语音；使用音频文件
        private bool Uselblstatus = true;//瓶签扫描结果使用状态
        private string  BeforeLblstatus = "0";//保存上个选中的瓶签

        private int ft = 0;//获取刷新的设置时间；
        private int x = 0;

        private delegate void CtrUI(object obj);//控制控件变化的委托
        #endregion

        #region 方法
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="userunqID"></param>
        /// <param name="userID"></param>
        /// <param name="username"></param>
        public UCCommonCheck(string userunqID, string userID, string username,string checkKind)
        {
            InitializeComponent();
            employee = new Demployee();
            employee.DEmployeeID = userunqID;
            employee.AccountID = userID;
            employee.DEmployeeCode = userID;
            employee.DemployeeName = username;

            CheckKind = checkKind;
            LoginTime = DateTime.Now.ToString();
        }

        // 创建结构体用于返回捕获时间 
        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            // 设置结构体块容量 
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;

            // 捕获的时间 
            [MarshalAs(UnmanagedType.U4)]
            public uint dwTime;
        }

        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        // 获取键盘和鼠标没有操作的时间  
        private static long GetLastInputTime()
        {
            LASTINPUTINFO vLastInputInfo = new LASTINPUTINFO();
            vLastInputInfo.cbSize = Marshal.SizeOf(vLastInputInfo);

            // 捕获时间  
            if (!GetLastInputInfo(ref vLastInputInfo))
                return 0;
            else
                return Environment.TickCount - (long)vLastInputInfo.dwTime;
        }

        /// <summary>
        /// 默认第二天设置,设置时间控件的默认值
        /// </summary>
        private void SetDate()
        {
            if (dtCountSet.Rows.Count > 0)
            {
                string dd = dtCountSet.Rows[0]["NextDay"].ToString();
                DateTime ddtime = DateTime.Parse(dd);

                string nowdd = DateTime.Now.ToString("HH:mm");
                DateTime nowddtime = DateTime.Parse(nowdd);

                if (!(dd.Equals("00:00")) && ddtime < nowddtime && timeFlag)
                {
                    date = DateTime.Now.AddDays(1).ToShortDateString();
                    dtpTime.Value = DateTime.Parse(date);
                    timeFlag = false;
                }
            }
        }

        /// <summary>
        /// 是否计费
        /// </summary>
        public void InIJifeiFlag()
        {
            string NodeValue = dbHelp.IniReadValuePivas("IsCharge", CheckSim);
            if (NodeValue == "" || NodeValue == "0")
                jifeiFlag = false;
            else
                jifeiFlag = true;
        }

        /// <summary>
        /// 是否中断
        /// </summary>
        public void InIIsBreak()
        {
            string NodeValue = dbHelp.IniReadValuePivas("IsBreak", "All");
            if (NodeValue == "" || NodeValue == "0")
                _IsBreak = false;
            else
                _IsBreak = true;
        }

        /// <summary>
        /// 批次是否默认选中
        /// </summary>
        public void InIBatches()
        {
            string NodeValue = dbHelp.IniReadValuePivas("BatchChecked", CheckSim);
            if (NodeValue == "" || NodeValue == "0")
                batchFlag = false;
            else
                batchFlag = true;
        }

        public void InIVoice()
        {
            string NodeValue = dbHelp.IniReadValuePivas("Voice", "Use");
            if (NodeValue == "" || NodeValue == "0")
                UseDetailVoice=true;
            else
                UseDetailVoice = false;
        }

        public void InIFontSize()
        {
            string NodeValue = dbHelp.IniReadValuePivas("FontSize","Size");
            string NodeBlank = dbHelp.IniReadValuePivas("FontSize", "Blank");
            if (NodeValue != "")
            {
                foreach (Label l in flowLayoutPanel1.Controls)
                {
                    if (l.Text.Trim() != "")
                    {
                        l.Font = new Font("微软雅黑", Convert.ToInt32(NodeValue));
                    }
                }
                label11.Font=new Font("微软雅黑", Convert.ToInt32(NodeValue)-1);
                lblPageName.Font = new Font("微软雅黑", Convert.ToInt32(NodeValue) - 2);
                ucCountInformation1.dgvCountInformation.Font = new Font("宋体", Convert.ToInt32(NodeValue) - 4); 
            }
            if (NodeBlank != "")
            {
                foreach (Label l in flowLayoutPanel1.Controls)
                {
                    if (l.Text.Trim() == "")
                    {
                        l.Text = "";
                        for (int i = 0; i < Convert.ToInt32(NodeBlank); i++)
                        {
                            l.Text += " ";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 使用瓶签状态或者未完成的上个步骤
        /// </summary>
        private void InIUseLblstatus()
        {
            string NodeValue = dbHelp.IniReadValuePivas("IsUselblstatus","Use");
            if (NodeValue == "" || NodeValue == "0")
                Uselblstatus = false;
            else
                Uselblstatus  = true;
        }

        /// <summary>
        /// 加载上一次选中的瓶签状态
        /// </summary>
        private void loadBeforeStatus()
        {
            string sql = "SELECT CheckID from [PivasCheckFormSet] where IsOpen=1 and CheckID > 1";
            DataSet dsCheckID = dbHelp.GetPIVAsDB(sql);

            if (dsCheckID.Tables.Count > 0)
            {
                for (int i = 0; i < dsCheckID.Tables[0].Rows.Count; i++)
                {
                    if (dsCheckID.Tables[0].Rows[i][0].ToString() == CheckStatus.ToString())
                    {
                        BeforeLblstatus = dsCheckID.Tables[0].Rows[i - 1][0].ToString();
                        break;//跳出for
                    }
                }
            }
        }

        /// <summary>
        /// 加载权限的方法,生成规则的sql语句的方法
        /// </summary>
        public void LoadAuthority()
        {
            InIJifeiFlag();
            InIBatches();
            authList.Clear();
            string id = sel.SelectCheckID(CheckKind);
            DataSet ds = sel.selectAllAuthority(id);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Authority auth = new Authority();
                    auth.SeqNo = int.Parse(ds.Tables[0].Rows[i]["SEQNO"].ToString());
                    auth.AuthorityName = ds.Tables[0].Rows[i]["AuthorityName"].ToString();
                    auth.AuthorityArea = ds.Tables[0].Rows[i]["AuthorityArea"].ToString();
                    auth.AUthorityLevel = ds.Tables[0].Rows[i]["AuthorityLevel"].ToString();
                    authList.Add(auth);
                }
            }

            dtCountSet = sel.CheckMateId(CheckKind).Tables[0];

            //自动刷新设置
            if (dtCountSet.Rows.Count > 0 
                && dtCountSet.Rows[0]["FreshTime"].ToString() != null 
                && dtCountSet.Rows[0]["FreshTime"].ToString() != "" 
                && dtCountSet.Rows[0]["FreshTime"].ToString() != "0")
            {
                x=ft = int.Parse(dtCountSet.Rows[0]["FreshTime"].ToString()) * 60;
                FreshFlag = true;
            }
        }

        /// <summary>
        /// 生成规则的sql语句
        /// </summary>
        /// <returns></returns>
        public string MakeAuthoritySql()
        {
            string sql = "";
                if (authList != null && authList.Count > 0)
                {
                    for (int i = 0; i < authList.Count; i++)
                    {
                        Authority auth = authList[i];
                        if (auth.AuthorityName.Trim().Equals("All")) //主规则
                        {
                            sql += " and (IVR.IVStatus>= '" + auth.AUthorityLevel + "' and IVR.IVStatus<='" + auth.AuthorityArea + "') ";
                        }
                        else //其他规则
                        {
                            string[] aaa = auth.AuthorityName.Split(';');
                            sql += " or ( (IVR.IVStatus >='" + auth.AUthorityLevel + "' and IVR.IVStatus<='" + auth.AuthorityArea + "')";
                            for (int j = 0; j < aaa.Length; j++)
                            {
                                string b = aaa[j];
                                if (j == 0)//瓶签临时还是非临时
                                {
                                    if (b.Contains(','))
                                    {
                                        sql += " ";
                                    }
                                    else if (b.Trim().Equals("UL"))
                                    {
                                        sql += " and  IVR.Batch not like '%L%' ";
                                    }
                                    else
                                    {
                                        sql += " and  IVR.Batch  like '%L%' ";
                                    }
                                }
                                else if (j == 1)
                                {
                                    if (b.Contains(','))
                                    {
                                        sql += " ";
                                    }
                                    else if (b.Trim().Equals("UST"))
                                    {
                                        sql += " and (P.Remark5 IS NULL or Ltrim(Rtrim(P.Remark5))='') ";
                                    }
                                    else
                                    {
                                        sql += " and P.Remark5='ST' ";
                                    }
                                }
                                else if (j == 2)
                                {
                                    if (b.Contains(','))
                                    {
                                        sql += " ";
                                    }
                                    else if (b.Trim().Equals("K"))
                                    {
                                        sql += " and  IVR.Batch  like '%K%' ";
                                    }
                                    else
                                    {
                                        sql += " and  IVR.Batch  like '%#%' ";
                                    }
                                }
                                else if (j == 3)
                                {
                                    string c = "'" + b.Replace(",", "','") + "'";
                                    sql += " and  IVR.TeamNumber  in (" + c + ") ";
                                }
                                else if (j == 4)
                                {
                                    sql += ") ";
                                }
                            }
                        }
                    }
                    //sql += ")";
                }

            return sql;
        }

        /// <summary>
        /// 加载批次的复选框
        /// </summary>
        public void LoadBatchCheckbox()
        {
            //取得所有的自定义批次规则
            DataSet ds = dbHelp.GetPIVAsDB("select distinct OrderID from DOrder where IsValid = 1 order by OrderID");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CheckBox cbox = new CheckBox();
                    cbox.Text = dt.Rows[i]["OrderID"].ToString();
                    cbox.Size = new Size(30, 30);
                    cbox.Checked = batchFlag;
                    cbox.CheckedChanged += new EventHandler(cbox_CheckedChanged);
                    flpBatchs.Controls.Add(cbox);
                }
            }
        }

        /// <summary>
        /// 批次事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cbox_CheckedChanged(object sender, EventArgs e)
        {
            FocusAndSelectTB();
            findAllWards();
            showDgvLabelInfo(flag);
        }

        /// <summary>
        /// 加载病区组筛选的下拉列表框
        /// </summary>
        public void loadWardareaCombox()
        {
            comBoxWardarea.Items.Clear();
            comBoxWardarea.Items.Add("全部病区组");
            comBoxWardarea.Text = "全部病区组";

            DataSet ds = new DataSet();
            ds = sel.getWardareas();

            if (ds.Tables.Count> 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count ; i++)
                {
                    comBoxWardarea.Items.Add(ds.Tables[0].Rows[i]["WardArea"].ToString());
                }
            }
        }

        /// <summary>
        /// 查询病区的datatable
        /// </summary>
        public void findAllWards()
        {
            string sql1 = getSelectConditions();
            string sql2 = getSelectAll();
            string batch1 = getBatch1();
            string drugtype1 = getDrugtype();
            string sql3 = MakeAuthoritySql();
            SQL3 = UseSQL3(sql3);
            date = dtpTime.Value.Date.ToString();
            if (checkMode == 1)//病区模式
            {
                if (chkBoxSWardsHave.Checked == true)
                {
                    DataSet ds = sel.getDwardHaveData(date, sql1, SQL3, batch1, drugtype1);
                    WardsTable = ds.Tables[0];
                    showWards(WardsTable);
                }
                else
                {
                    DataSet ds = sel.getAllDward(date, sql1, SQL3, batch1, drugtype1,comBoxWardarea.Text);
                    WardsTable = ds.Tables[0];
                    showWards(WardsTable);
                }
            }
            else//总瓶签模式
            {
                DataSet ds = sel.selectAllLabelGroups(date, sql1, sql2, SQL3, batch1, drugtype1);
                WardsTable = ds.Tables[0];
                showWards(WardsTable);
            }
        }

        /// <summary>
        /// 根据病区组查询病区
        /// </summary>
        /// <param name="WardArea"></param>
        public void SelAllWardsByWardArea(string WardArea)
        {
            string sql1 = getSelectConditions();
            string sql2 = getSelectAll();
            string batch1 = getBatch1();
            string drugtype1 = getDrugtype();
            string sql3 = MakeAuthoritySql();
            SQL3 = UseSQL3(sql3);
            DataSet ds = sel.getAllDward(date, sql1, SQL3, batch1,drugtype1, comBoxWardarea.Text);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                WardsTable = ds.Tables[0];
                showWards(WardsTable);
            }
        }

        /// <summary>
        /// 加载显示的病区
        /// </summary>
        /// <param name="dt"></param>
        private void showWards(DataTable dt)
        {
            wardSelList.Clear();
            dgvWards.Rows.Clear();
            dgvPrintCode.Rows.Clear();
            if (dt != null && dt.Rows.Count > 0)
            {
                if (checkMode == 1)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dgvWards.Rows.Add(1);
                        //dgvWards.Rows[i].Cells["colBoxSel"].Value = true;//=true 默认全选
                        dgvWards.Rows[i].Cells["colWardCode"].Value = dt.Rows[i]["WardCode"].ToString();
                        dgvWards.Rows[i].Cells["colWardName"].Value = dt.Rows[i]["WardName"].ToString();
                        dgvWards.Rows[i].Cells["colWardSimName"].Value = dt.Rows[i]["WardSimName"].ToString();
                        dgvWards.Rows[i].Cells["colWardArea"].Value = dt.Rows[i]["WardArea"].ToString();
                        dgvWards.Rows[i].Cells["colSpellcode"].Value = dt.Rows[i]["Spellcode"].ToString();
                        dgvWards.Rows[i].Cells["colLabelWardCount"].Value = int.Parse(dt.Rows[i]["LabelWardCount"].ToString());
                        //dgvWards.Rows[i].Cells["colPackAdvanceCount"].Value = int.Parse(dt.Rows[i]["PackAdvanceCount"].ToString());//病区列表显示提前打包统计
                        if (selward != string.Empty)
                        {
                            string[] a = selward.Split(',');
                            foreach (string n in a)
                            {
                                if (dgvWards.Rows[i].Cells["colWardCode"].Value.ToString() == n)
                                {
                                    dgvWards.Rows[i].Cells["colBoxSel"].Value = true;

                                    Dward ward = new Dward();
                                    ward.WardCode = dt.Rows[i]["WardCode"].ToString();
                                    ward.WardName = dt.Rows[i]["WardName"].ToString();
                                    ward.WardSimName = dt.Rows[i]["WardSimName"].ToString();
                                    ward.WardArea = dt.Rows[i]["WardArea"].ToString();
                                    ward.WardSpellCode = dt.Rows[i]["Spellcode"].ToString();
                                    wardSelList.Add(ward);
                                }
                            }
                        }
                    }
                }
                else//总瓶签模式
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dgvPrintCode.Rows.Add(1);
                        //dgvPrintCode.Rows[i].Cells["colCheckBoxSel"].Value = true;//=true 默认全选
                        dgvPrintCode.Rows[i].Cells["colDrugQRCode"].Value = dt.Rows[i]["DrugQRCode"].ToString();
                        dgvPrintCode.Rows[i].Cells["colOrderQRCode"].Value = dt.Rows[i]["OrderQRCode"].ToString();
                        dgvPrintCode.Rows[i].Cells["colPrintDT"].Value = dt.Rows[i]["PrintDT"].ToString();
                        dgvPrintCode.Rows[i].Cells["colPrintCode"].Value = dt.Rows[i]["PrintCode"].ToString();
                        dgvPrintCode.Rows[i].Cells["colPrintName"].Value = dt.Rows[i]["PrintName"].ToString();
                        if (selward != string.Empty)
                        {
                            string[] a = selward.Split(',');
                            foreach (string n in a)
                            {
                                if (dgvPrintCode.Rows[i].Cells["colDrugQRCode"].Value.ToString() == n)
                                {
                                    dgvPrintCode.Rows[i].Cells["colCheckBoxSel"].Value = true;
                                    Dward ward = new Dward();
                                    ward.WardCode = dt.Rows[i]["DrugQRCode"].ToString();
                                    wardSelList.Add(ward);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 瓶签核对查询方法
        /// </summary>
        /// <param name="labelCode"></param>
        public void labelCheck(string labelCode)
        {
            date = dtpTime.Value.Date.ToString();
            if (labelCode != "" && labelCode.Length >= 4)//瓶签标准符合要求的情况，正确的瓶签号
            {
                
                if ((labelCode.Substring(0, 4) == "8888" || labelCode.Substring(0, 4) == "9999") && labelCode.Length >= 22)//扫描的是总瓶签
                {
                    if (checkMode == 2)
                    {
                        if (wardSelList.Count <= 0)
                        {
                            MessageBox.Show("未选择病区或总瓶签");
                            return;
                        }
                        string barCode = labelCode.Substring(0, 22);
                        WaitForm f = new WaitForm(this);            //实例化
                        string sql1 = getSelectConditions();        //拼
                        string sql2 = getSelectAll();               //个
                        string batch1 = getBatch1();                //条    
                        string sql3 = MakeAuthoritySql();           //件
                        f.ds = sel.LabelNoCollection(barCode, date, sql1, sql3, batch1, 1, this.CheckTable); //获得要用的瓶签号
                        f.ShowDialog(this);                                                                 
                    }
                    else
                    {
                        MessageBox.Show("请切换总瓶签扫描模式！");
                    }
                }
                else if (labelCode.Substring(0, 4) == "7777" && labelCode.Length >= 22)//扫描的是员工编号
                {
                    string barCode = labelCode;
                    //需要重新登陆,重新绑定用户信息
                    Demployee employeeNew = new Demployee();
                    employeeNew = util.login(barCode, "");
                    if (employeeNew != null)
                    {
                        if (employeeNew.DEmployeeCode != employee.DEmployeeCode)
                        {
                            employee = employeeNew;
                            lblMsg.Text = "欢迎您:     " + employee.DemployeeName;
                        }
                        else
                        {
                            //同样的人再次扫描，登出
                            Application.Exit();
                        }
                    }
                    else
                    {
                        MessageBox.Show("此用户编号不存在！");
                    }
                }
                else if (labelCode != "" && labelCode.Length >= 14)//扫描的是单个瓶签
                {
                    if (OtherLabelFlag)
                    {
                        string labelno = sel.CheckIsHisLabel(labelCode);//第三方瓶签 位数大于等于14的情况
                        if (labelno == "")
                        {
                            CheckOneLabel(labelCode);
                        }
                        else
                        {
                            CheckOneLabel(labelno);
                        }
                    }
                    else
                    {
                        CheckOneLabel(labelCode);
                    }
                }
                else
                {
                    if (OtherLabelFlag)
                    {
                        string labelno = sel.CheckIsHisLabel(labelCode);//第三方瓶签 位数小于14的情况
                        if (labelno == "")
                        {
                            PlaySound("不正常.wav");
                            lblStatus.Text = "不是瓶签！";
                            lblStatus.ForeColor = Color.Black;
                        }
                        else
                        {
                            CheckOneLabel(labelno);
                        }
                    }
                    else
                    {
                        PlaySound("不正常.wav");
                        lblStatus.Text = "不是瓶签！";
                        lblStatus.ForeColor = Color.Black;
                    }
                }
            }
            else
            {
                PlaySound("不正常.wav");
                lblStatus.Text = "不是瓶签！";
                lblStatus.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// 扫描单个瓶签的方法
        /// </summary>
        /// <param name="labelCode"></param>
        private void CheckOneLabel(string labelCode)
        {
            //-------------------showtime--------------------//开始单个瓶签
            string sql1 = getSelectConditions();
            string sql2 = getSelectAll();
            string batch1 = getBatch1();
            string sql3 = MakeAuthoritySql();
            SQL3 = sql3;
            string drugtype1 = getDrugtype();
            date = dtpTime.Value.Date.ToString();
            string barCode = labelCode.Substring(0, 14);
            DataSet ds = sel.IVRecord(CheckTable, CheckDT, getSelectDwardsInfo(wardSelList), date, sql1, batch1, "", "",checkMode,barCode,drugtype1);
            //-------------------showtime--------------------//开始匹配条件
            DataTable dt = null;
            if (ds != null&&ds.Tables.Count!=0 && ds.Tables[0].Rows.Count > 0)//找到瓶签
            {
                dt = ds.Tables[0];
                IVRecord IVR = CheckInLabelDt(dt, barCode);

                if (IVR != null)//瓶签是有效的
                {
                    DataSet patientDs = new DataSet();
                    ds = sel.FindDwardAndBedOLD(labelCode);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        patient = new DPatient();
                        patient.PCode = ds.Tables[0].Rows[0]["PCode"].ToString();
                        patient.PWardOld = ds.Tables[0].Rows[0]["PWardOld"].ToString();
                        patient.PWardNew = ds.Tables[0].Rows[0]["pWardNew"].ToString();
                        patient.PBedNew = ds.Tables[0].Rows[0]["PBedNew"].ToString();
                        patient.PBedOld = ds.Tables[0].Rows[0]["PbedOld"].ToString();
                        patient.PatientName = ds.Tables[0].Rows[0]["PatName"].ToString();
                        patient.PWardNameOld = ds.Tables[0].Rows[0]["PWardNameOld"].ToString();
                        patient.PWardNameNew = ds.Tables[0].Rows[0]["PWardNameNew"].ToString();


                        if (patient.PWardNew == patient.PWardOld)//病区未改
                        {
                            //lblWard.Text = patient.PWardNameNew;
                            label10.Text = patient.PWardNameNew;
                        }
                        else//病区改动
                        {
                            lblWard.Text = patient.PWardNameOld;
                            label10.Text = "转";
                            label9.Text = patient.PWardNameNew;
                            lblWard.ForeColor = Color.Red;
                            label10.ForeColor = Color.Red;
                            label9.ForeColor = Color.Red;
                            try
                            {
                                if (UseDetailVoice)
                                {
                                    Thread spkThread = new Thread(new ParameterizedThreadStart(talkInfo));
                                    spkThread.Start(lblWard.Text + label10.Text + label9.Text);
                                }
                                else
                                {
                                    PlaySound("转科.wav");
                                    Thread.Sleep(1500);
                                }
                            }
                            catch
                            {
                            }
                        }

                        if (patient.PBedOld == patient.PBedNew)//床位未改
                        {
                            lblBedNo.Text = patient.PBedNew + "  床";
                        }
                        else
                        {
                            lblBedNo.Text = patient.PBedOld + "床  转 " + patient.PBedNew + "床";
                            lblBedNo.ForeColor = Color.Red;
                            try
                            {
                                if (UseDetailVoice)
                                {
                                    Thread spkThread = new Thread(new ParameterizedThreadStart(talkInfo));
                                    spkThread.Start(lblBedNo.Text);
                                }
                                else
                                {
                                    PlaySound("转床.wav");
                                    Thread.Sleep(1500);
                                }
                            }
                            catch
                            {
                            }
                        }
                    }

                    //-------------------showtime--------------------//条件匹配结束
                    DataSet dss = sel.CheckLabelNumber(getSelectDwardsInfo(wardSelList), date, sql1,sql3, batch1, 0,barCode,checkMode,CheckTable,CheckDT);
                    if (dss != null && dss.Tables[0].Rows.Count > 0)//可以核对的情况
                    {
                        //执行核对操作
                        DABAO(barCode, employee.DEmployeeID, getSelectDward(wardSelList));
                    }
                    else//不能核对的情况
                    {
                        //-------------------showtime--------------------//条件匹配结束
                        if (Uselblstatus)
                        {
                            if (int.Parse(IVR.IvStatus) == 3)
                            {
                                lblStatus.Text = "已打印";
                                PlaySound("不正常.wav");
                            }
                            else if (int.Parse(IVR.IvStatus) == 4)
                            {
                                lblStatus.Text = "已摆药";
                                PlaySound("不正常.wav");
                            }
                            else if (int.Parse(IVR.IvStatus) == 5)
                            {
                                lblStatus.Text = "已排药";
                                if (CheckKind == "排药核对")
                                { PlaySound("已经核对.wav"); }
                                else
                                { PlaySound("不正常.wav"); }

                            }
                            else if (int.Parse(IVR.IvStatus) == 7)
                            {
                                lblStatus.Text = "已进仓";
                                if (CheckKind == "进仓扫描")
                                { PlaySound("已经核对.wav"); }
                                else
                                { PlaySound("不正常.wav"); }
                            }
                            else if (int.Parse(IVR.IvStatus) == 9)
                            {
                                lblStatus.Text = "已配置";
                                if (CheckKind == "配置核对")
                                { PlaySound("已经核对.wav"); }
                                else
                                { PlaySound("不正常.wav"); }
                            }
                            else if (int.Parse(IVR.IvStatus) == 11)
                            {
                                lblStatus.Text = "已出仓";
                                if (CheckKind == "出仓扫描")
                                { PlaySound("已经核对.wav"); }
                                else
                                { PlaySound("不正常.wav"); }
                            }
                            else if (int.Parse(IVR.IvStatus) == 13)
                            {
                                lblStatus.Text = "已打包";
                                if (CheckKind == "打包核对")
                                { PlaySound("已经核对.wav"); }
                                else
                                { PlaySound("不正常.wav"); }
                            }

                            else if (int.Parse(IVR.IvStatus) >= 15)
                            {
                                PlaySound("不正常.wav");
                                lblStatus.Text = "已签收";
                            }
                            else if (int.Parse(IVR.WardRetreat) == 1 || int.Parse(IVR.WardRetreat) == 2 || int.Parse(IVR.LabelOver) < 0)
                            {
                                PlaySound("不正常.wav");
                                lblStatus.Text = " 已退药";

                            }
                            else
                            {
                                PlaySound("不正常.wav");
                                lblStatus.Text = " 条件不符！";

                            }
                        }
                        else
                        {
                            if (int.Parse(IVR.IvStatus) < CheckStatus)
                            {
                                switch (BeforeLblstatus)
                                {
                                    case "3": lblStatus.Text = "未打印"; break;
                                    case "4": lblStatus.Text = "摆药核对未扫"; break;
                                    case "5": lblStatus.Text = "排药核对为扫"; break;
                                    case "7": lblStatus.Text = "进仓核对未扫"; break;
                                    case "9": lblStatus.Text = "配置核对未扫"; break;
                                    case "11": lblStatus.Text = "出仓核对未扫"; break;
                                    case "13": lblStatus.Text = "打包核对未扫"; break;
                                }
                                PlaySound("不正常.wav");
                            }
                            else if (int.Parse(IVR.IvStatus) >= CheckStatus)
                            {
                                lblStatus.Text = "已" + CheckKind.Substring(0, 2);
                                PlaySound("已经核对.wav");
                            }
                            else if (int.Parse(IVR.WardRetreat) == 1 || int.Parse(IVR.WardRetreat) == 2 || int.Parse(IVR.LabelOver) < 0)
                            {
                                PlaySound("不正常.wav");
                                lblStatus.Text = " 已退药";
                            }
                            else
                            {
                                PlaySound("不正常.wav");
                                lblStatus.Text = " 条件不符！";
                            }
                        }
                        lblStatus.ForeColor = Color.Red;
                    }
                }
                else
                {
                    PlaySound("不正常.wav");
                    lblStatus.Text = "找不到瓶签！";
                    lblStatus.ForeColor = Color.Black;
                }
            }
            else
            {
                PlaySound("不正常.wav");
                lblStatus.Text = "不符合要求";
                lblStatus.ForeColor = Color.Black;
                DataRow[] dr = LabelTable.Select("瓶签号 like '" + labelCode + "'");
                if (dr.Length > 0)
                {
                    label2.ForeColor = Color.Red;
                    if (Convert.ToInt32(dr[0]["LabelOver"].ToString()) == -1)
                    {
                        label2.Text = "护士工作站配置取消！！";
                    }
                    else if (Convert.ToInt32(dr[0]["LabelOver"].ToString()) == -2)
                    {
                        label2.Text = "系统配置取消！！";
                    }
                    else if (Convert.ToInt32(dr[0]["LabelOver"].ToString()) == -3)
                    {
                        label2.Text = "收费处配置取消！！";
                    }
                    else if (Convert.ToInt32(dr[0]["LabelOver"].ToString()) == -4)
                    {
                        label2.Text = "瓶签查询配置取消！！";
                    }
                    else if(Convert.ToInt32(dr[0]["LabelOver"].ToString()) == 0)
                    {
                        label2.Text="";
                    }
                }
            }
        }

        //播放声音
        private void PlaySound(string name)
        {
            string location=string.Empty;
            try
            {
                location = Application.StartupPath + "\\Sound\\" + name;
                SoundPlayer media = new System.Media.SoundPlayer(Application.StartupPath + "\\Sound" + "\\" + name);
                media.Play();
            }
            catch 
            {
                MessageBox.Show("未找到文件：" + location);
            }
        }

        /// <summary>
        /// 核对公共方法
        /// </summary>
        public void DABAO(string barCode, string employeeID, string wards)
        {
            string returnMsg=string.Empty;
            string msg = "";
            if (jifeiFlag == true)//需要计费
            {
                string Msg = Charge(CheckSim, barCode, employeeID,out msg);
                if (Msg == "1"||Msg=="-1")
                {
                    MainMethod(barCode, employeeID, wards);
                    label2.Text = "计费成功";
                    label2.ForeColor = Color.Green;
                }
                else
                {
                    label2.Text = "计费失败 " + msg;
                    label2.ForeColor = Color.Red;
                    if (UseDetailVoice)
                    {
                        Thread spkThread = new Thread(new ParameterizedThreadStart(talkInfo));
                        spkThread.Start(label2.Text);
                    }
                    else
                    {
                        PlaySound("计费失败.wav");
                        Thread.Sleep(1000);
                    }
                }
            }
            else//不需要计费
            {
                MainMethod(barCode, employeeID, wards);                    
            }
        }

        /// <summary>
        /// 调用存储过程
        /// </summary>
        /// <param name="barCode"></param>
        /// <param name="employeeID"></param>
        /// <param name="wards"></param>
        private void MainMethod(string barCode, string employeeID, string wards)
        {
            string str = "exec " + CheckPro + " '" + employeeID + "','" + barCode + "','" + wards + "','" + null + "','" 
                + '1' + "','" + '1' + "','" + checkMode + "'";
            int a = dbHelp.SetPIVAsDB(str);

            if (a > 0)
            {
                lblStatus.Text = "核对成功！";
                lblStatus.ForeColor = Color.Green;

                PlaySound("正常.wav");

                //核对成功时候需要回数据库判断是否扫描完毕
                if (IsShowAllDward == true)
                {
                    DataSet wardSet = new DataSet();
                    wardSet = sel.selectWardCodeByLabel(barCode);
                    if (wardSet != null && wardSet.Tables[0].Rows.Count > 0)
                    {
                        string wCode = wardSet.Tables[0].Rows[0]["WardCode"].ToString();
                        string wName = wardSet.Tables[0].Rows[0]["WardName"].ToString();
                        string wTeamNumber = wardSet.Tables[0].Rows[0]["TeamNumber"].ToString();
                        DataSet dsUncheck = new DataSet();
                        dsUncheck = sel.CountWardsLabels(date, wCode, wTeamNumber,CheckSim);
                        if (dsUncheck != null && dsUncheck.Tables[0].Rows.Count == 0)//扫描完毕
                        {
                            
                            string strSPK = wName;
                            string strbatch = "第" + wTeamNumber + "批次";
                            object[] ob = new object[] { strSPK, strbatch };
                            string strMsg = strSPK +strbatch+ "全部扫描完毕";

                            try
                            {
                                //语音播报
                                Thread spkThread = new Thread(new ParameterizedThreadStart(talkInfo));
                                spkThread.Start(strMsg);

                                //加载提示页面
                                Thread msgThread = new Thread(new ParameterizedThreadStart(showFrmMsg));
                                msgThread.Start(ob);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                    }
                }
            }
            else
            {
                PlaySound("不正常.wav");
            }
        }

        /// <summary>
        /// 无参数的方法，用来让进程调用
        /// </summary>
        private void talkInfo(Object obj)
        {
            string i = string.Empty;
            string str = obj as string;
            if (!string.IsNullOrEmpty(str))
            {
                i = speak.Speak(str);//读取语音
            }
            CtrUI ctrui = new CtrUI((object objText) =>
            {
                label11.Text = (string)objText;
            });
            this.Invoke(ctrui, i);
        }

        private void showFrmMsg(Object ob)
        {
            object[] obj = ob as object[];
            string wname = obj[0] as string;
            string batch = obj[1] as string;
            if (!string.IsNullOrEmpty(wname))
            {
                frmMessage msg = new frmMessage(wname, batch);
                msg.ShowDialog();
            }
        }

        /// <summary>
        /// 获取当前瓶签状态
        /// </summary>
        /// <param name="IVstatus"></param>
        /// <returns></returns>
        public string LabelSetName(string IVstatus)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string path = "ConfigFile\\LabelAuthorityXml.xml";
            string name = "";
            xmlDoc.Load(path);
            XmlElement root = xmlDoc.DocumentElement;//获取根节点
            XmlNodeList nodes = root.GetElementsByTagName("Operation");

            foreach (XmlNode node in nodes)
            {
                string value = ((XmlElement)node).GetAttribute("value");
                if (value == IVstatus)
                {
                    XmlNodeList subNameNodes = ((XmlElement)node).GetElementsByTagName("name");  //获取age子XmlElement集合
                    if (subNameNodes.Count == 1)
                    {
                        name = subNameNodes[0].InnerText;
                    }
                }
            }
            return name;
        }

        /// <summary>
        /// 检查单个瓶签号是否存在并且符合核对要求
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public IVRecord CheckInLabelDt(DataTable dt, string text)
        {
            IVRecord ivr = null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (text == dt.Rows[i]["LabelNo"].ToString())//瓶签存在
                {
                    ivr = new IVRecord();
                    ivr.Batch = dt.Rows[i]["Batch"].ToString();
                    ivr.IvStatus = dt.Rows[i]["IVStatus"].ToString();
                    ivr.WardRetreat = dt.Rows[i]["WardRetreat"].ToString();
                    ivr.LabelOver = dt.Rows[i]["LabelOver"].ToString();
                    return ivr;
                }
            }
            return ivr;
        }

        /// <summary>
        /// 查询条件中的病区组的拼写sql
        /// </summary>
        /// <param name="wList"></param>
        /// <returns></returns>
        public string getSelectDwardsInfo(List<Dward> wList)
        {
            if (wList.Count > 0)
            {
                string strDward = "(";
                for (int i = 0; i < wList.Count; i++)
                {
                    Dward ward = wList[i];
                    strDward += " '" + ward.WardCode + "', ";
                }
                strDward = strDward.Substring(0, strDward.Length - 2);

                strDward += ")";
                return strDward;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 存储过程的病区组传入
        /// </summary>
        /// <param name="wList">当前选中病区集合</param>
        /// <returns>病区查询字符串</returns>
        public string getSelectDward(List<Dward> wList)
        {
            string strDward = null;
            //if (wList.Count == 0)
            //{
            //    MessageBox.Show("未选择病区或总瓶签");
            //    return "there is no ward";
            //}

            for (int i = 0; i < wList.Count; i++)
            {
                Dward ward = wList[i];
                strDward += ward.WardCode + ";";
            }

            return strDward.TrimEnd(';');

        }

        /// <summary>
        /// 获取病区的集合
        /// </summary>
        public void GetWardList()
        {
            selward = string.Empty;
            wardSelList.Clear();
            if (checkMode == 1)
            {
                for (int i = 0; i < dgvWards.Rows.Count; i++)
                {
                    if (Equals(dgvWards.Rows[i].Cells[0].Value, "true") || Equals(dgvWards.Rows[i].Cells[0].Value, true))
                    {
                        Dward ward = new Dward();
                        ward.WardCode = dgvWards.Rows[i].Cells["colWardCode"].Value.ToString();
                        ward.WardName = dgvWards.Rows[i].Cells["colWardName"].Value.ToString();
                        ward.WardSimName = dgvWards.Rows[i].Cells["colWardSimName"].Value.ToString();
                        ward.WardArea = dgvWards.Rows[i].Cells["colWardArea"].Value.ToString();
                        ward.WardSpellCode = dgvWards.Rows[i].Cells["colSpellcode"].Value.ToString();
                        wardSelList.Add(ward);
                        
                        selward = selward + ',' + dgvWards.Rows[i].Cells["colWardCode"].Value;
                    }
                }
            }

            else
            {
                for (int i = 0; i < dgvPrintCode.Rows.Count; i++)
                {
                    if (Equals(dgvPrintCode.Rows[i].Cells[0].Value, "true") || Equals(dgvPrintCode.Rows[i].Cells[0].Value, true))
                    {
                        Dward ward = new Dward();
                        ward.WardCode = dgvPrintCode.Rows[i].Cells["colDrugQRCode"].Value.ToString();
                        wardSelList.Add(ward);
                        selward = selward + ',' + dgvPrintCode.Rows[i].Cells["colDrugQRCode"].Value;
                    }
                }
            }
            

        }

        /// <summary>
        /// 加载dataGridView的数据集的公共方法
        /// </summary>
        public void showDGVInfo(DataTable dt, int flag)
        {
            if (flag == 1)//瓶签信息
            {
               
                ucLabelsInfo1.CheckTable = this.CheckTable;
                ucLabelsInfo1.CheckDt = this.CheckDT;
                ucCountInformation1.Visible = false;
                ucLabelsInfo1.Visible = true;
                pnlMenuCount.Controls.Add(ucLabelsInfo1);
                ucLabelsInfo1.dgvDT(dt);
                //ucLabelsInfo1.dgvLabelsInfo.DataSource = dt;
                ucLabelsInfo1.Dock = DockStyle.Fill;
                ucLabelsInfo1.dgvLabelsInfo.Columns[0].Width = 110;
                ucLabelsInfo1.dgvLabelsInfo.Columns[1].Width = 60;
                ucLabelsInfo1.dgvLabelsInfo.Columns[2].Width = 60;
                ucLabelsInfo1.dgvLabelsInfo.Columns[3].Width = 160;
                ucLabelsInfo1.dgvLabelsInfo.Columns[4].Width = 60;
                ucLabelsInfo1.dgvLabelsInfo.Columns[7].Width = 80;

                ucLabelsInfo1.Adcolor = adcolor;
                for (int i = 0; i < ucLabelsInfo1.dgvLabelsInfo.Rows.Count; i++)
                {
                    if (dt.Rows[i]["提前打包"].ToString() == "提前打包")
                    { ucLabelsInfo1.dgvLabelsInfo.Rows[i].DefaultCellStyle.BackColor = adcolor; }

                }

            }
            else//统计
            {
               
                ucCountInformation1.Visible = true;
                ucLabelsInfo1.Visible = false;
                pnlMenuCount.Controls.Add(ucCountInformation1);
                //if (ds != null && ds.Tables[0].Rows.Count > 0)
                //{
                    ucCountInformation1.dgvCountInformation.DataSource = dt;
                    ucCountInformation1.dgvCountInformation.Columns[0].Width = 220;
                //}
                ucCountInformation1.changeColor(dtCountSet);
                ucCountInformation1.Dock = DockStyle.Fill;
               
            }
        }
        /// <summary>
        /// 取得显示的数据集
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="batchConditions">批次L#K搜索条件</param>
        /// <param name="flag">1:瓶签信息 2：统计</param>
        /// <returns></returns>
        public DataSet getDataSet(string date, string sql1, string sql2,string sql3, string batch1,string drugtype1, int flag, int LabelKindFlag)
        {
            DataSet ds = new DataSet();
            if (flag == 1)//瓶签信息
            {
                //ds = sel.CountLabel(getSelectDwardsInfo(wardSelList), date, sql1, sql3, batch1, LabelKindFlag, checkMode,CheckTable,CheckDT);
                ShowDS=ds = sel.AllLabel(getSelectDwardsInfo(wardSelList), date, sql1, sql3, batch1,drugtype1, LabelKindFlag, checkMode, CheckTable, CheckDT);
                LabelTable= sel.AllLabel("", date, sql1, "", batch1,drugtype1, LabelKindFlag, checkMode, CheckTable, CheckDT).Tables[0];
                return ds;
            }
            else if (flag == 2)//统计
            {
                ds = sel.CountAll(getSelectDwardsInfo(wardSelList), dtpTime.Value.Date.ToString(), sql2, sql3, batch1, drugtype1, CheckTable, dtCountSet, checkMode);
                return ds;
            }
            else
            {
                return ds = sel.CountLabel(getSelectDwardsInfo(wardSelList), date, sql1, sql3, batch1,drugtype1, LabelKindFlag, checkMode,CheckTable, CheckDT);
            }

        }

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="FLAG">1：瓶签信息  2：统计</param>
        public void showDgvLabelInfo(int FLAG)
        {
            LoadAuthority();
            string sql1 = getSelectConditions();
            string sql2 = getSelectAll();
            string batch1 = getBatch1();
            string drugtype1 = getDrugtype();
            string sql3 = MakeAuthoritySql();
            SQL3 = UseSQL3(sql3);
            date = dtpTime.Value.Date.ToString();

            DataSet ds = getDataSet(date, sql1, sql2,SQL3, batch1,drugtype1, FLAG, labelFlag);
           
            if (FLAG == 1)//瓶签查询
            {
                DataTable showdt = NodoOrAldo(ds.Tables[0]);//拆分各类表
                showDGVInfo(showdt, FLAG);
            }
            else
            {
                showDGVInfo(ds.Tables[0], FLAG);
            }
            //统计按钮显示数量
            CountLabelsShow();
            x = ft;
        }

        /// <summary>
        /// 显示具体的统计信息的方法
        /// </summary>
        public void CountLabelsShow()
        {
            //string sql1 = getSelectConditions();
            //string sql2 = getSelectAll();
            //string batch1 = getBatch1();
            //string sql3 = MakeAuthoritySql();
            //string SQL3=UseSQL3(sql3);
            //DataSet ds = sel.CountShowAll(getSelectDwardsInfo(wardSelList), date, sql1, SQL3, batch1, checkMode,CheckTable);
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    DataTable dt = ds.Tables[0];
            //    lblAllCount.Text = dt.Rows[0]["总数"].ToString();
            //    lblPackagedCount.Text = dt.Rows[0]["已核对"].ToString();
            //    lblUnPackageCount.Text = dt.Rows[0]["未核对"].ToString();
            //}
            lblAllCount.Text = alldo.Rows.Count.ToString();
            lblPackagedCount.Text = aldo.Rows.Count.ToString();
            lblUnPackageCount.Text = nodo.Rows.Count.ToString();
            ChangeCountNum();
            //CountLabelCheckLogin();
        }

        /// <summary>
        /// 全部扫描完毕之后修改
        /// </summary>
        public void ChangeCountNum()
        {
            if (lblAllCount.Text == lblPackagedCount.Text)
            {
                pnlMenuCount.BackColor = FinishColor;
            }
            else
            {
                pnlMenuCount.BackColor = CheckColor;
            }
        }

        /// <summary>
        /// 取得批次条件的方法
        /// </summary>
        /// <returns></returns>
        public string getBatch1()
        {
            String batch1 = "";
            foreach (Control c in flpBatchs.Controls)
            {
                CheckBox chkBox = (CheckBox)c;
                if (chkBox.Checked == true)
                {
                    batch1 += "'" + chkBox.Text + "',";
                }
            }
            if (batch1.Length > 0)
            {
                batch1 = batch1.Substring(0, batch1.Length - 1);
                return batch1;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 统计获取查询条件
        /// </summary>
        /// <returns></returns>
        public string getSelectAll()
        {

            string selConditions = "";//拼出来的批次查询条件
            selComboxValue = GetCheckBoxValue();
            if (selComboxValue == "")//选择全部的情况
            {
                selConditions = "";
            }
            else if (selComboxValue == "Ling")//临时包的情况
            {
                if (checkBoxK.Checked == false && checkBoxA.Checked == false)
                {
                    selConditions = "";
                }
                else if (checkBoxK.Checked == true && checkBoxA.Checked == false)//所有临时包的空包
                {
                    selConditions = "LingK";
                }
                else if (checkBoxK.Checked == false && checkBoxA.Checked == true)//所有临时包的充沛包
                {
                    selConditions = "LingA";
                }
                else//所有的临时包
                {
                    selConditions = "LingKA";
                }
            }
            else if (selComboxValue == "Long")//长期包的情况
            {
                if (checkBoxK.Checked == false && checkBoxA.Checked == false)
                {
                    selConditions = "";
                }
                else if (checkBoxK.Checked == true && checkBoxA.Checked == false)//所有长期包的空包
                {
                    selConditions = "LongK";
                }
                else if (checkBoxK.Checked == false && checkBoxA.Checked == true)//所有长期包的充沛包
                {
                    selConditions = "LongA";
                }
                else//所有的临时包
                {
                    selConditions = "LongKA";
                }
            }
            else if (selComboxValue == "LL")//长临都选
            {
                if (checkBoxK.Checked == false && checkBoxA.Checked == false)
                {
                    selConditions = "";
                }
                else if (checkBoxK.Checked == true && checkBoxA.Checked == false)//所有的空包
                {
                    selConditions = "LLK";
                }
                else if (checkBoxK.Checked == false && checkBoxA.Checked == true)//所有的充沛包
                {
                    selConditions = "LLA";
                }
                else//所有包
                {
                    selConditions = "LLKA";
                }
            }
            return selConditions;
        }

        /// 单元格单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgvLabels_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            FocusAndSelectTB();
        }

        /// <summary>
        /// 输入框获取焦点并且全选
        /// </summary>
        public void FocusAndSelectTB()
        {
            tbBarCode.Focus();
            tbBarCode.SelectAll();
        }

        /// <summary>
        /// 语音播报的方法
        /// </summary>
        /// <param name="date"></param>
        /// <param name="DwardList"></param>
        private void SpeakInfo(string date, List<Dward> DwardList)
        {
            string wardSQL = getSelectDwardsInfo(DwardList);
            DataSet ds = new DataSet();
        }


        /// <summary>
        /// 获取检索条件的方法
        /// </summary>
        public string getSelectConditions()
        {
            string selConditions = "";//拼出来的批次查询条件
            selComboxValue = GetCheckBoxValue();

            if (selComboxValue == "Long")//长期包的情况
            {
                if (checkBoxK.Checked == true && checkBoxA.Checked == false)//长期的空包
                {
                    selConditions = "and IVR.Batch not  like '%L%' and IVR.Batch like '%K%' ";
                }
                else if (checkBoxK.Checked == false && checkBoxA.Checked == true)
                {
                    selConditions = "and IVR.Batch not  like '%L%' and IVR.Batch like '%#%' ";
                }
                else if (checkBoxK.Checked == true && checkBoxA.Checked == true)
                {
                    selConditions = "and IVR.Batch not  like '%L%' ";
                }
                else
                {
                    selConditions = " and IVR.Batch not  like '%L%' and IVR.Batch not like '%#%' and IVR.Batch not like '%K%' ";
                }
            }
            else if (selComboxValue == "Ling")
            {
                if (checkBoxK.Checked == true && checkBoxA.Checked == false)//长期的空包
                {
                    selConditions = "and IVR.Batch  like '%L%' and IVR.Batch like '%K%' ";
                }
                else if (checkBoxK.Checked == false && checkBoxA.Checked == true)
                {
                    selConditions = "and IVR.Batch  like '%L%' and IVR.Batch like '%#%' ";
                }
                else if (checkBoxK.Checked == true && checkBoxA.Checked == true)
                {
                    selConditions = "and IVR.Batch  like '%L%' ";
                }
                else
                {
                    selConditions = "and IVR.Batch not  like '%L%' and IVR.Batch not like '%#%' and IVR.Batch not like '%K%' ";
                }
            }
            else if (selComboxValue == "LL")
            {
                if (checkBoxK.Checked == true && checkBoxA.Checked == false)//长期的空包
                {
                    selConditions = " and IVR.Batch like '%K%' ";
                }
                else if (checkBoxK.Checked == false && checkBoxA.Checked == true)
                {
                    selConditions = " and IVR.Batch like '%#%' ";
                }
                else if (checkBoxK.Checked == true && checkBoxA.Checked == true)
                {
                    selConditions = "";
                }
                else
                {
                    selConditions = "and IVR.Batch not  like '%L%' and IVR.Batch not like '%#%' and IVR.Batch not like '%K%' ";
                }
            }
            else
            {
                selConditions = "and IVR.Batch not  like '%L%' and IVR.Batch not like '%#%' and IVR.Batch not like '%K%' ";
            }

            return selConditions;
        }

        /// <summary>
        /// 获取药品类型
        /// </summary>
        /// <returns></returns>
        private string getDrugtype()
        {
            string drugtype = "";

            if (checkBox1.Checked)
            {
                drugtype = drugtype == "" ? "'1'" : drugtype + ",'1'";
            }

            if (checkBox2.Checked)
            {
                drugtype = drugtype == "" ? "'2'" : drugtype + ",'2'";
            }


            if (checkBox3.Checked)
            {
                drugtype = drugtype == "" ? "'3'" : drugtype + ",'3'";

            }

            if (checkBox4.Checked)
            {
                drugtype = drugtype == "" ? "'4'" : drugtype + ",'4'";
            }
            if(checkBox5.Checked)
            {
                drugtype = drugtype == "" ? "'5'" : drugtype + ",'5'";
            }

            if (drugtype == "")
            {
                drugtype = "'0'";
            }
            drugtype = "(" + drugtype + ")";

            return drugtype;
        }

        /// <summary>
        /// 获取长期临时包的条件
        /// </summary>
        /// <returns></returns>
        private string GetCheckBoxValue()
        {
            if (checkBoxLong.Checked == true && checkBoxLing.Checked == false)
            {
                return "Long";
            }
            else if (checkBoxLong.Checked == false && checkBoxLing.Checked == true)
            {
                return "Ling";
            }
            else if (checkBoxLong.Checked == true && checkBoxLing.Checked == true)
            {
                return "LL";
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 取得单选框的条件
        /// </summary>
        /// <returns></returns>
        private int getLabelFlag()
        {
            if (rbUnchecked.Checked == true)
            {
                return 0;
            }
            else if (rbChecked.Checked == true)
            {
                return 1;
            }
            else
            {
                return 2;
            }

        }

        /// <summary>
        /// 单选框公共查询方法
        /// </summary>
        private void radioBoxSelected()
        {
            labelFlag = getLabelFlag();

            //setFlag();
            flag = 1;//设置显示瓶签信息区域

            //pnlShow.Controls.Clear();//清空显示区域的所有控件
            //背景色变换
            pnlWardLeft.BackColor = unChoseColor;
            //pnlMenuLableInfo.BackColor = choseColor;
            //pnlMenuCount.BackColor = unChoseColor;
            //showDgvLabelInfo(flag);//显示详细信息
            switch (labelFlag)
            {
                case 0: showDGVInfo(nodo, flag);//未扫描
                    break;
                case 1:showDGVInfo(aldo,flag);//已扫描
                    break;
                case 2: showDGVInfo(alldo, flag);//全部扫描
                    break;

            }
            
            btnCount.Text = "统计";
            label1.Text = "瓶签信息：";
            FocusAndSelectTB();
        }

        /// <summary>
        /// 按照简称或者首字母筛选病区
        /// </summary>
        private void SelWardsByNameOrCode()
        {
            try
            {
                if (WardsTable == null)
                {
                    return;
                }
                else
                {
                    if (txtWard.Text.Trim() != "")
                    {
                        DataTable dt = WardsTable.Copy();
                        dt.Rows.Clear();
                        DataRow[] DR = WardsTable.Select(" WardSimName like '%" + txtWard.Text.Trim() + "%' or SpellCode like '%" + txtWard.Text.Trim() + "%' ");
                        foreach (DataRow dr in DR)
                        {
                            dt.ImportRow(dr);
                        }
                        if (dt.Rows.Count > 0)
                        {
                            showWards(dt);
                        }
                    }
                    else
                    {
                        if (WardsTable.Rows.Count > 0)
                        {
                            showWards(WardsTable);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 初始化将需要变色的panel放入集合中便于修改颜色
        /// </summary>
        private void IniPanel()
        {
            pnlList.Add(pnlStartCheck);
            pnlList.Add(pnlBaobiao);
        }

        /// <summary>
        /// 改变选中功能的背景颜色
        /// </summary>
        /// <param name="p"></param>
        private void ChangeSelectedColor(Panel p)
        {
            p.BackColor = choseColor;
            if (pnlList != null && pnlList.Count > 0)
            {
                for (int i = 0; i < pnlList.Count; i++)
                {
                    if (p.Name != pnlList[i].Name)
                    {
                        pnlList[i].BackColor = unChoseColor;
                    }
                }
            }
        }

        /// <summary>
        /// 仅显示有数据的病区
        /// </summary>
        public DataTable ShowDateWards()
        {
            DataTable dt = null;
            string sql1 = getSelectConditions();
            string sql2 = getSelectAll();
            string batch1 = getBatch1();
            string drugtype1 = getDrugtype();
            string sql3 = MakeAuthoritySql();
            SQL3 = UseSQL3(sql3);
            date = dtpTime.Value.Date.ToString();

            DataSet ds = sel.getDwardHaveData(date, sql1, SQL3, batch1, drugtype1);
            if(ds !=null && ds.Tables[0].Rows.Count>0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        #endregion

        #region 事件
        private void UCCommonCheck_Load(object sender, EventArgs e)
        {
            try
            {
                ucCountInformation1 = new ucCountInfomation(this);
                switch (CheckKind)
                {
                    case "贴签核对":
                        CheckTable = "IVRecord_TQ"; CheckPro = "bl_Label_TQ"; CheckDT = "TQDT"; CheckSim = "TQ"; CheckStatus = 0;
                        break;
                    case "排药核对":
                        CheckTable = "IVRecord_PY"; CheckPro = "bl_Label_PY"; CheckDT = "PYDT"; CheckSim = "PY"; CheckStatus = 5;
                        break;
                    case "进仓扫描":
                        CheckTable = "IVRecord_JC"; CheckPro = "bl_Label_JC"; CheckDT = "JCDT"; CheckSim = "JC"; CheckStatus = 7;
                        break;
                    case "配置核对":
                        CheckTable = "IVRecord_PZ"; CheckPro = "bl_Label_PZ"; CheckDT = "PZDT"; CheckSim = "PZ"; CheckStatus = 9;
                        break;
                    case "出仓扫描":
                        CheckTable = "IVRecord_CC"; CheckPro = "bl_Label_CC"; CheckDT = "CCDT"; CheckSim = "CC"; CheckStatus = 11;
                        break;
                    case "打包核对":
                        CheckTable = "IVRecord_DB"; CheckPro = "bl_Label_DB"; CheckDT = "DBDT"; CheckSim = "DB"; CheckStatus = 13;
                        break;
                    default: break;
                }

                loadDrug();
                loadDatable();
                LoadAuthority();//加载所有的规则
                getPackColor();
                InIVoice();
                InIFontSize();
                SetDate();

                InIIsBreak();
                InIUseLblstatus();
                loadBeforeStatus();

                date = dtpTime.Value.Date.ToString();

                SetPageColorStyle();
                InIColor();

                InIIsShowAllDward();//加载配置，全病区扫描完毕是否显示提示框
                IniPanel();//初始化panl便于修改颜色
                loadWardareaCombox();//加载病区组
                LoadBatchCheckbox();//加载批次
                findAllWards();

                SetIsHisLableNo();
                dtCountSet = sel.CheckMateId(CheckKind).Tables[0];
                lblPageName.Text = CheckKind;

                lblMsg.Text = "欢迎您:     " + employee.DemployeeName;
                cmBoxCheckMode.SelectedIndex = 0;//核对模式默认为病区模式，1；还有一个是总瓶签模式为2。

                //背景色变换
                pnlStartCheck.BackColor = unChoseColor;
                pnlWardLeft.BackColor = unChoseColor;
                pnlBaobiao.BackColor = unChoseColor;
                flag = 1;
                showDgvLabelInfo(flag);//显示详细信息

                FocusAndSelectTB();

                label11.Text = "";
                label2.Text = "";
                lblReturnMsg.Text = "";
                lblBedNo.Text = "";
                lblWard.Text = "";
                lblStatus.Text = "";
                label10.Text = "";
                label9.Text = "";
            }
            catch (Exception ex)
            {
                ShowMsgHelper.ShowError("核对通用窗体加载出错:" + ex.Message);
            }
        }

        /// <summary>
        /// 调出开始扫描的区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlStartCheck_Click(object sender, EventArgs e)
        {
            pnlCheckArea.Visible = true;
            pnlCheckArea.BringToFront();
            ChangeSelectedColor(pnlStartCheck);
            FocusAndSelectTB();
        }

        /// <summary>
        /// 统计单击事件(统计扫描的瓶签信息)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlMenuCount_Click(object sender, EventArgs e)
        {
            if (btnCount.Text == "统计")
            {

                // pnlCheckArea.Visible = false;
                flag = 2;
                btnCount.Text = "瓶签";
                label1.Text = "统计信息：";
            }
            else
            {
                flag = 1;
                btnCount.Text = "统计";
                label1.Text = "瓶签信息：";
            }
            //背景色变换
            //ChangeSelectedColor(pnlMenuCount);
            //显示统计信息
            showDgvLabelInfo(flag);
            FocusAndSelectTB();
            tbBarCode.Focus();

        }
        /// <summary>
        /// 瓶签号区域enter，扫描枪扫描到信息的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            string labelno = "";
            if (e.KeyCode == Keys.Enter)
            {
                if (OtherLabelFlag)
                    labelno = sel.CheckIsHisLabel(tbBarCode.Text);//第三方瓶签

                DataRow[] dr = LabelTable.Select("瓶签号 like '" + labelno + "'");
                if (dr.Length > 0)
                {
                    if (dr[0]["提前打包"].ToString() == "提前打包")
                    {
                        lblReturnMsg.Text = "注意！提前打包瓶签！！！";
                        PlaySound("提前打包.wav");
                        Thread.Sleep(1000);
                    }
                }
                label11.Text = "";
                lblWard.Text = "";
                label10.Text = "";
                label9.Text = "";
                lblBedNo.Text = "";
                labelCheck(tbBarCode.Text.ToString());
                tbBarCode.SelectAll();//选中所有

                DataRow[] dr1 = ShowDS.Tables[0].Select("瓶签号 like '" +  labelno + "'");//在脱机表里扫描成功的
                if (dr1.Length > 0&&lblStatus.ForeColor==Color.Green)//存在的情况
                {
                    dr1[0]["Ivstatus"] = CheckStatus.ToString() == "0" ? dr1[0]["Ivstatus"] : CheckStatus.ToString();
                    dr1[0]["记录时间"] = DateTime.Now.ToString();
                    //dgv信息刷新
                    if (flag == 1)//瓶签查询
                    {
                        DataTable showdt = NodoOrAldo(ShowDS.Tables[0]);//拆分各类表
                        showDGVInfo(showdt, flag);
                    }
                    else//统计
                    {
                        showDgvLabelInfo(flag);
                        //showDGVInfo(ds.Tables[0], flag);
                    }
                }
                else if (lblStatus.ForeColor != Color.Green)//核对不成功，什么都不做
                {
                }
                else//不存在的情况：刷新showds，重新载入
                {
                    showDgvLabelInfo(flag);
                }
                //统计按钮显示数量
                CountLabelsShow();
            }
        }


        /// <summary>
        /// 用户完成选择日期后发生的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpTime_CloseUp(object sender, EventArgs e)
        {
            FocusAndSelectTB();
            findAllWards();
            showDgvLabelInfo(flag);
            timeFlag = true;
            tbBarCode.Focus();
        }
        /// <summary>
        /// 下拉框更改之后的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboxBatch_SelectedValueChanged(object sender, EventArgs e)
        {
            FocusAndSelectTB();
            findAllWards();
            showDgvLabelInfo(flag);
            tbBarCode.Focus();
        }

        /// <summary>
        /// 瓶签输入框输入改变的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbBarCode_TextChanged(object sender, EventArgs e)
        {
            lblReturnMsg.Text = "";
            lblBedNo.Text = "";
            lblBedNo.ForeColor = Color.White;
            lblWard.Text = "";
            lblWard.ForeColor = Color.Yellow;
            label10.Text = "";
            label10.ForeColor = Color.Yellow;
            label9.Text = "";
            label9.ForeColor = Color.Yellow;
            lblStatus.Text = "";
            label2.Text = "";
            label11.Text = "";
        }

        /// <summary>
        /// 单选框属性更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            radioBoxSelected();
        }

        private void rbUnchecked_Click(object sender, EventArgs e)
        {
            radioBoxSelected();
        }

        private void rbChecked_Click(object sender, EventArgs e)
        {
            radioBoxSelected();
        }

        private void rbAll_Click(object sender, EventArgs e)
        {
            radioBoxSelected();
        }

        /// <summary>
        /// 全选按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkBoxSelAll_Click(object sender, EventArgs e)
        {
            string aaa = chkBoxSelAll.CheckState.ToString();
            if (checkMode == 1)
            {
                if (chkBoxSelAll.CheckState == CheckState.Unchecked)//已经全不选，改成全选
                {
                    for (int j = 0; j < dgvWards.Rows.Count; j++)
                    {
                        dgvWards.Rows[j].Cells[0].Value = false;
                    }
                    //ckBoxSelAll.CheckState = CheckState.Checked;
                }
                else if (chkBoxSelAll.CheckState == CheckState.Indeterminate)
                {
                    for (int j = 0; j < dgvWards.Rows.Count; j++)
                    {
                        dgvWards.Rows[j].Cells[0].Value = false;
                    }
                    chkBoxSelAll.CheckState = CheckState.Unchecked;
                }
                else if (chkBoxSelAll.CheckState == CheckState.Checked)
                {
                    for (int j = 0; j < dgvWards.Rows.Count; j++)
                    {
                        dgvWards.Rows[j].Cells[0].Value = true;
                    }
                    //ckBoxSelAll.CheckState = CheckState.Unchecked;
                }
            }
            else
            {
                if (chkBoxSelAll.CheckState == CheckState.Unchecked)//已经全不选，改成全选
                {
                    for (int j = 0; j < dgvPrintCode.Rows.Count; j++)
                    {
                        dgvPrintCode.Rows[j].Cells[0].Value = false;
                    }
                    //ckBoxSelAll.CheckState = CheckState.Checked;
                }
                else if (chkBoxSelAll.CheckState == CheckState.Indeterminate)
                {
                    for (int j = 0; j < dgvPrintCode.Rows.Count; j++)
                    {
                        dgvPrintCode.Rows[j].Cells[0].Value = false;
                    }
                    chkBoxSelAll.CheckState = CheckState.Unchecked;
                }
                else if (chkBoxSelAll.CheckState == CheckState.Checked)
                {
                    for (int j = 0; j < dgvPrintCode.Rows.Count; j++)
                    {
                        dgvPrintCode.Rows[j].Cells[0].Value = true;
                    }
                    //ckBoxSelAll.CheckState = CheckState.Unchecked;
                }
            }

            GetWardList();//重新获取病区的选择对象
            showDgvLabelInfo(flag);//重新查询
            tbBarCode.Focus();
        }

        /// <summary>
        /// 病区组筛选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comBoxWardarea_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string WardArea = comBoxWardarea.Text;
            if (WardArea == "全部病区组")
            {
                WardArea = "";
                findAllWards();//查询所有病区
            }

            SelAllWardsByWardArea(comBoxWardarea.Text);
        }

        /// <summary>
        /// 病区查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWard_TextChanged(object sender, EventArgs e)
        {
            if (txtWard.Text != "病区名/简拼" && txtWard.Text.Trim() != "")
            {
                SelWardsByNameOrCode();
            }
        }

        private void txtWard_Enter(object sender, EventArgs e)
        {
            try
            {
                if (txtWard.Text == "病区名/简拼")
                {
                    txtWard.Text = "";
                    txtWard.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtWard_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtWard.Text.Trim() == "")
                {
                    txtWard.Text = "病区名/简拼";
                    txtWard.ForeColor = Color.Gray;
                    findAllWards();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 病区单元格单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvWards_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;//获取选中单元格的行号
            
            int colNum = e.ColumnIndex;//获取选中单元格的列号

            if (colNum == 0&&i>=0)//只有点击每一行的checkbox的时候才会勾选或者不勾选
            {
                ckFlag = 0;
                //去除背景色
                //dgvWards.CurrentRow.Selected = false;
                if (!Equals(dgvWards.Rows[i].Cells[0].Value, true))
                {
                    dgvWards.Rows[i].Cells[0].Value = true;
                }
                else
                {
                    dgvWards.Rows[i].Cells[0].Value = false;
                }
                int k = 0;
                for (int j = 0; j < dgvWards.Rows.Count; j++)
                {
                    if (Equals(dgvWards.Rows[j].Cells[0].Value, true))
                    {
                        k = k + 1;
                    }
                }
                if (k < dgvWards.Rows.Count && k > 0)
                {
                    chkBoxSelAll.CheckState = CheckState.Indeterminate;
                }
                else if (k == dgvWards.Rows.Count)
                {
                    chkBoxSelAll.CheckState = CheckState.Checked;
                }
                else
                {
                    chkBoxSelAll.CheckState = CheckState.Unchecked;
                }
                GetWardList();//重新获取病区的选择对象
                showDgvLabelInfo(flag);//重新查询
            }
            else//单击了非checkbox区域，需要单独查询单个病区的内容
            {
                ckFlag = 1;

            }
            tbBarCode.Focus();
        }


        /// <summary>
        /// 设置按钮调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSet_Click(object sender, EventArgs e)
        {
            if (GetPivasLimit.Instance.Limit(employee.DEmployeeID, "CheckSet"))
            {
                string ID = sel.SelectCheckID(CheckKind);
                LabelCheckAuthoritySet.frmLabelAuthoritySet authoritySet = new frmLabelAuthoritySet(ID);
                if (authoritySet.ShowDialog() == DialogResult.Cancel)
                {
                    InIFontSize();
                    InIVoice();
                    getPackColor();
                    InIIsBreak();
                    //pnlShow.Controls.Clear();//清空显示区域的所有控件
                    findAllWards();//刷新病区
                    LoadAuthority();//加载所有的规则
                    showDgvLabelInfo(flag);//显示详细信息
                }
            }
            timeFlag = true;
        }

        /// <summary>
        /// 报表单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlBaobiao_Click(object sender, EventArgs e)
        {
            ChangeSelectedColor(pnlBaobiao);

            try
            {
                ChangeSelectedColor(pnlBaobiao);
                switch (CheckSim)
                {
                    //case "TQ": 
                    //    break;
                    //case "PY": returnMsg = charge.ScanPY5(barcode, employeeid, out msg);
                    //    break;
                    //case "JC": returnMsg = charge.ScanJC7(barcode, employeeid, out msg);
                    //    break;
                    case "PZ": System.Diagnostics.Process.Start(Application.StartupPath + "\\ConfectCollect.exe", "");
                        break;
                    //case "CC": returnMsg = charge.ScanCC11(barcode, employeeid, out msg);
                    //    break;
                    case "DB": System.Diagnostics.Process.Start(Application.StartupPath + "\\PackCollect.exe", "");
                        break;
                    default: MessageBox.Show("木有报表！");
                        break;
                }
                //System.Diagnostics.Process.Start(Application.StartupPath + "\\PackCollect.exe", "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //MessageBox.Show("该功能正在完善！");
        }
        

        /// <summary>
        /// 设置操作区域的panel的边框颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlCheckArea_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                                 pnlCheckArea.ClientRectangle,
                                Color.DimGray,
                                3,
                                ButtonBorderStyle.Solid,
                                Color.DimGray,
                                3,
                                ButtonBorderStyle.Solid,
                                Color.DimGray,
                                3,
                                ButtonBorderStyle.Solid,
                                Color.DimGray,
                                3,
                                ButtonBorderStyle.Solid);
        }

        /// <summary>
        /// 关闭扫描按钮事件需要刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            pnlCheckArea.Visible = false;
            showDgvLabelInfo(flag);//显示详细信息
        }

        /// <summary>
        /// 仅显示有数据的病区筛选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkBoxSWardsHave_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxSWardsHave.Checked == true)
            {
                chkBoxSelAll.Checked = true;
                showWards(ShowDateWards());
            }
            else
            {
                chkBoxSelAll.Checked = true;
                findAllWards();
            }
            tbBarCode.Focus();
        }
        #endregion

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            DialogResult dr= MessageBox.Show("确认批量扫描？","确认框", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                if (rbUnchecked.Checked == false)
                {
                    MessageBox.Show("请在右上角选择未扫描的瓶签！");
                }
                else
                {
                    WaitForm f = new WaitForm(this);
                    string sql1 = getSelectConditions();
                    string sql2 = getSelectAll();
                    string batch1 = getBatch1();
                    string sql3 = MakeAuthoritySql();
                    string date = dtpTime.Value.Date.ToString();
                    f.dt = NodoForRule(ShowDS.Tables[0]);
                    f.ShowDialog(this);
                    er = ce.dataGridView1.Rows.Count ;
                    label4.Text = "扫描收集信息：" + er ;
                    if (er != 0) { label4.BackColor = Color.Red; ce.ShowDialog(); } else { label4.BackColor = Color.Transparent; }
                    findAllWards();
                    showDgvLabelInfo(flag);//显示详细信息
                }
            }
            tbBarCode.Focus();
        }

        /// <summary>
        /// 核对方法
        /// </summary>
        /// <returns></returns>
        public int LabelsCheck(string barcode)
        {
            piliangSucCOunt = 0;
            LoadAuthority();
            if (piliangFlag == false)
            {
                piliangFlag = true;
                return 0;
            }
            piliangBarcode = barcode;
            return CheckMethod(barcode, employee.DEmployeeID, getSelectDward(wardSelList));
        }

        /// <summary>
        /// 批量扫描方法
        /// </summary>
        /// <param name="barCode"></param>
        /// <param name="employeeID"></param>
        /// <param name="wards"></param>
        public int CheckMethod(string barCode, string employeeID, string wards)
        {
            string msg = string.Empty;
            int i = 0;
            if (jifeiFlag == true)//需要调用dll
            {
                string  Msg = Charge(CheckSim,barCode ,employeeID,out msg);
                if (Msg == "1"||Msg=="-1")
                {
                    string str = "exec " + CheckPro + " '" + employeeID + "','" + barCode + "','" + wards + "','" + null + "','" + '1' + "','" + '1' + "','" + checkMode + "'";
                    int a = dbHelp.SetPIVAsDB(str);

                    if (a > 0)
                    {
                        i = a;
                    }
                    else
                    {
                        i = -1;
                    }
                    if (wardChange(barCode) != "" || bedchange(barCode) != "")//存在转床或者转病区
                    {
                        ce.labelno = barCode;
                        ce.errorInfor = wardChange(barCode) + " " + bedchange(barCode);
                        ce.demployeeid = employee.DemployeeName;
                        ce.drugQRCode = sel.SelectErrorChargeInfor(barCode);
                        ce.errordt = DateTime.Now.ToString();
                        ce.noInfor = false;
                        ce.Showdgv();
                    }
                }
                else
                {
                    ce.labelno = barCode;
                    ce.errorInfor = msg + " " + wardChange(barCode) + " " + bedchange(barCode);
                    ce.demployeeid = employee.DemployeeName;
                    ce.drugQRCode = sel.SelectErrorChargeInfor(barCode);
                    ce.errordt = DateTime.Now.ToString();
                    ce.noInfor = false;
                    ce.Showdgv();
                }
            }
            else//不需要调用dll
            {
                string str = "exec " + CheckPro + " '" + employeeID + "','" + barCode + "','" + wards + "','" + null + "','" + '1' + "','" + '1' + "','" + checkMode + "'";
                int a = dbHelp.SetPIVAsDB(str);

                if (a > 0)
                {
                    i = a;
                }
                else
                {
                    i = -1;
                }
                if (wardChange(barCode) != "" || bedchange(barCode) != "")//存在转床或者转病区
                {
                    ce.labelno = barCode;
                    ce.errorInfor = wardChange(barCode) + " " + bedchange(barCode);
                    ce.demployeeid = employee.DemployeeName;
                    ce.drugQRCode = sel.SelectErrorChargeInfor(barCode);
                    ce.errordt = DateTime.Now.ToString();
                    ce.noInfor = false;
                    ce.Showdgv();
                }
            }
            return i;
        }

        private void cmBoxCheckMode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chkBoxSelAll.Checked = false;
            selward = string.Empty;
            if (cmBoxCheckMode.Text == "病区模式")
            {
                dgvPrintCode.Visible = false;
                checkMode = 1;
                comBoxWardarea.Visible = true;
                chkBoxSWardsHave.Visible = true;
                txtWard.Visible = true;
            }
            else //总瓶签模式
            {
                dgvPrintCode.Visible = true;
                checkMode = 2;
                comBoxWardarea.Visible = false;
                chkBoxSWardsHave.Visible = false;
                txtWard.Visible = false;
            }
            findAllWards();
            FocusAndSelectTB();
            showDgvLabelInfo(flag);
            tbBarCode.Focus();
        }

        //-----------------------------------------------------页面配色——————————————————————————//
        private void SetPageColorStyle()
        {
            int mode = 0;
            string smode = dbHelp.GetPivasAllSet("核对-出仓核对-画面颜色");
            if (smode != null && !(smode.Equals("")))
            {
                mode = int.Parse(smode);
            }
            else
            {
                mode = 0;
            }
            if (mode == 0)
            {
                choseColor = Color.SkyBlue;//选中后的背景色
                unChoseColor = Color.LightBlue;//未选中的背景色
                pnlLeftColor = Color.LightBlue;
                dgvLeftColor = Color.LightBlue;
                pnlRightColor = Color.LightBlue;
                dgvRightColor = Color.White;
                CheckModelColor = Color.SkyBlue;
            }
            else if (mode == 1)
            {
                choseColor = Color.SkyBlue;//选中后的背景色
                unChoseColor = Color.LightBlue;//未选中的背景色
                pnlLeftColor = Color.LightBlue;
                dgvLeftColor = Color.LightBlue;
                pnlRightColor = Color.LightBlue;
                dgvRightColor = Color.White;
                CheckModelColor = Color.SkyBlue;
            }
            else if (mode == 2)
            {
                choseColor = Color.SkyBlue;//选中后的背景色
                unChoseColor = Color.LightBlue;//未选中的背景色
                pnlLeftColor = Color.LightBlue;
                dgvLeftColor = Color.LightBlue;
                pnlRightColor = Color.LightBlue;
                dgvRightColor = Color.White;
                CheckModelColor = Color.SkyBlue;
            }
            else if (mode == 3)
            {
                choseColor = Color.SkyBlue;//选中后的背景色
                unChoseColor = Color.LightBlue;//未选中的背景色
                pnlLeftColor = Color.LightBlue;
                dgvLeftColor = Color.LightBlue;
                pnlRightColor = Color.LightBlue;
                dgvRightColor = Color.White;
                CheckModelColor = Color.SkyBlue;
            }
            else if (mode == 4)
            {
                choseColor = Color.SkyBlue;//选中后的背景色
                unChoseColor = Color.LightBlue;//未选中的背景色
                pnlLeftColor = Color.LightBlue;
                dgvLeftColor = Color.LightBlue;
                pnlRightColor = Color.LightBlue;
                dgvRightColor = Color.White;
                CheckModelColor = Color.SkyBlue;
            }
            else if (mode == 5)
            {
                choseColor = Color.SkyBlue;//选中后的背景色
                unChoseColor = Color.LightBlue;//未选中的背景色
                pnlLeftColor = Color.LightBlue;
                dgvLeftColor = Color.LightBlue;
                pnlRightColor = Color.LightBlue;
                dgvRightColor = Color.White;
                CheckModelColor = Color.SkyBlue;
            }
        }

        private void InIColor()
        {
            pnlLeft.BackColor = pnlLeftColor;
            flpBatchs.BackColor = pnlLeftColor;
            pnlWardLeft.BackColor = pnlLeftColor;
            dgvPrintCode.BackgroundColor = pnlLeftColor;
            dgvWards.BackgroundColor = pnlLeftColor;
            panel1.BackColor = pnlLeftColor;
            pnlStartCheck.BackColor = pnlLeftColor;
            pnlBaobiao.BackColor = pnlLeftColor;
            pnlRight.BackColor = pnlLeftColor;
            pnlMenuCount.BackColor = pnlLeftColor;
            pnlMenuCount.BackColor = pnlLeftColor;
            pnlCheckArea.BackColor = CheckModelColor;
        }

        private void SetIsHisLableNo()
        {
            if (dbHelp.GetPivasAllSet("第三方瓶签") == "1")
                OtherLabelFlag = true;
        }

        private void InIIsShowAllDward()
        {
            if (dbHelp.IniReadValuePivas("IsShowAllDward", CheckSim) == "1")
                IsShowAllDward = true;
            else
                IsShowAllDward = false;
        }

        private void timeFocus_Tick_1(object sender, EventArgs e)
        {
            try
            {
                string tm = DateTime.Now.ToString();
                lblSysTime.Text = tm;
                dtCountSet = sel.CheckMateId(CheckKind).Tables[0];

                long aaa = GetLastInputTime();
                if (dtCountSet.Rows.Count > 0
                    && dtCountSet.Rows[0]["WaitTime"].ToString() != null 
                    && dtCountSet.Rows[0]["WaitTime"].ToString() != "" 
                    && dtCountSet.Rows[0]["WaitTime"].ToString() != "0")
                {
                    if (aaa >= long.Parse(dtCountSet.Rows[0]["WaitTime"].ToString()) * 1000 * 60)
                    {
                        SetDate();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMsgHelper.ShowError("timeFocus_Tick_1定时器执行出错" + ex.Message);
            }
        }

        private void timerFresh_Tick(object sender, EventArgs e)
        {
            if (flag == 1 && FreshFlag == true)
            {
                if (x != 0)
                {
                    x = x - 1;
                }
                else//x=0
                {
                    flag = 1;
                    showDgvLabelInfo(flag);//显示详细信息
                    FocusAndSelectTB();
                    x=ft;
                }
            }
        }

        private void dgvPrintCode_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;//获取选中单元格的行号

            int colNum = e.ColumnIndex;//获取选中单元格的列号

            if (colNum == 0 && i>=0)//只有点击每一行的checkbox的时候才会勾选或者不勾选
            {
                ckFlag = 0;
                if (!Equals(dgvPrintCode.Rows[i].Cells[0].Value, true))
                {
                    dgvPrintCode.Rows[i].Cells[0].Value = true;
                }
                else
                {
                    dgvPrintCode.Rows[i].Cells[0].Value = false;
                }
                int k = 0;
                for (int j = 0; j < dgvPrintCode.Rows.Count; j++)
                {
                    if (Equals(dgvPrintCode.Rows[j].Cells[0].Value, true))
                    {
                        k = k + 1;
                    }
                }
                if (k < dgvPrintCode.Rows.Count && k > 0)
                {
                    chkBoxSelAll.CheckState = CheckState.Indeterminate;
                }
                else if (k == dgvPrintCode.Rows.Count)
                {
                    chkBoxSelAll.CheckState = CheckState.Checked;
                }
                else
                {
                    chkBoxSelAll.CheckState = CheckState.Unchecked;
                }
                GetWardList();//重新获取病区的选择对象
                showDgvLabelInfo(flag);//重新查询
            }
            else//单击了非checkbox区域，需要单独查询单个病区的内容
            {
                ckFlag = 1;
            }

            GetWardList();//重新获取病区的选择对象
            showDgvLabelInfo(flag);//重新查询
            tbBarCode.Focus();
        }

        private string Charge(string checksim,string barcode,string employeeid,out string  msg )
        {
            string returnMsg=string .Empty;
            #region csw修改，需要此exe下配置文件IMEQPIVAs.ini
            ICharge charge = ChargeFactory.GetCharge(dbHelp.IniReadValuePivas("Charge", "HospitalType"));
            #endregion
            switch (CheckSim)
            {
                case "TQ": returnMsg = "1"; msg = "";
                    break;
                case "PY": returnMsg = charge.ScanPY5(barcode, employeeid, out msg);
                    break;
                case "JC": returnMsg = charge.ScanJC7(barcode, employeeid, out msg);
                    break;
                case "PZ": returnMsg = charge.ScanPZ9(barcode, employeeid, out msg);
                    break;
                case "CC":
                    returnMsg = charge.ScanCC11(barcode, employeeid, out msg);
                    break;
                case "DB":
                    returnMsg = charge.ScanDB13(barcode, employeeid, out msg);
                    break;
                default: returnMsg = "1"; msg = ""; break;
            }

            return returnMsg;  
        }

        private string UseSQL3(string sql)
        {
            string SQL3 = string.Empty;
            string NodeValue = dbHelp.IniReadValuePivas("IsUseRule", CheckSim);//是否使用规则
            if (NodeValue == "1")
            {
                SQL3 = sql;
            }
            else
            {
                SQL3 = "";
            }
            return SQL3;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            ce.ShowDialog();
            er = ce.dataGridView1.Rows.Count ;
            label4.Text = "计费错误信息：" + er;
            if (er != 0) { label4.BackColor = Color.Red; } else { label4.BackColor = Color.Transparent; }
        }

        /// <summary>
        /// 获取提前打包的背景色
        /// </summary>
        private void getPackColor()
        {
            try
            {
                string co = dbHelp.IniReadValuePivas("advanceColor", "adcolorR") + "," 
                    + dbHelp.IniReadValuePivas("advanceColor", "adcolorG") + "," 
                    + dbHelp.IniReadValuePivas("advanceColor", "adcolorB");
                adcolor = ColorTranslator.FromHtml(co);
            }
            catch
            {
                adcolor = Color.OrangeRed;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbBarCode.Text))
            {
                return;
            }
            string bat = getBatch1(); //批次，包括#K
            string K = string.Empty; //K，#
            string L = string.Empty; //长期，临时
            string wardCode = string.Empty;
            if (dgvWards != null && dgvWards.Rows.Count > 0)
            {
                wardCode = getSelectDward(wardSelList);
            }
            else
            {
                wardCode = "";
            }
            if (checkBoxA.Checked)
            {
                K += "#";
            }
            if (checkBoxK.Checked)
            {
                K += "K";
            }

            if (checkBoxLong.Checked)
            {
                L += "长";
            }
            if (checkBoxLing.Checked)
            {
                L += "ST";
            }
            LabelDetail ld = new LabelDetail(tbBarCode.Text, bat, K, L, wardCode, CheckKind);
            ld.ShowDialog();
        }

        /// <summary>
        /// 将所有的瓶签分到已扫描表和未扫描表
        /// </summary>
        /// <param name="alltable"></param>
        private DataTable NodoOrAldo(DataTable alltable)
        {
            try
            {
                alldo.Rows.Clear();
                nodo.Rows.Clear();
                aldo.Rows.Clear();

                if (alltable.Rows.Count > 0)
                {
                    for (int i = 0; i < alltable.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(alltable.Rows[i]["LabelOver"].ToString()) >= 0 && Convert.ToInt32(alltable.Rows[i]["WardRetreat"].ToString()) == 0)//保证瓶签labelover>=0 and WardRetreat=0
                        {
                            //用code找药名
                            string MarjorDrug = drug.Select("drugcode ='" + alltable.Rows[i]["主药"].ToString() + "'")[0][1].ToString() == "" ? alltable.Rows[i]["主药"].ToString() : drug.Select("drugcode ='" + alltable.Rows[i]["主药"].ToString() + "'")[0][1].ToString();
                            string Menstruum = drug.Select("drugcode ='" + alltable.Rows[i]["溶媒"].ToString() + "'")[0][1].ToString() == "" ? alltable.Rows[i]["溶媒"].ToString() : drug.Select("drugcode ='" + alltable.Rows[i]["溶媒"].ToString() + "'")[0][1].ToString();

                            alldo.Rows.Add(new object[] { alltable.Rows[i]["瓶签号"].ToString(), alltable.Rows[i]["批次"].ToString(), alltable.Rows[i]["病人"].ToString(), alltable.Rows[i]["病区"].ToString(), alltable.Rows[i]["床号"].ToString(), MarjorDrug,alltable.Rows[i]["主药颜色"].ToString (), Menstruum,alltable.Rows[i]["溶媒颜色"].ToString (), alltable.Rows[i]["提前打包"].ToString() });
                            if (CheckStatus!=0&& Convert.ToInt32(alltable.Rows[i]["IVStatus"].ToString()) < CheckStatus)//未扫描
                            {
                                nodo.Rows.Add(new object[] { alltable.Rows[i]["瓶签号"].ToString(), alltable.Rows[i]["批次"].ToString(), alltable.Rows[i]["病人"].ToString(), alltable.Rows[i]["病区"].ToString(), alltable.Rows[i]["床号"].ToString(), MarjorDrug, alltable.Rows[i]["主药颜色"].ToString(), Menstruum, alltable.Rows[i]["溶媒颜色"].ToString(), alltable.Rows[i]["提前打包"].ToString() });
                            }
                            else if (CheckStatus == 0 && alltable.Rows[i]["记录时间"].ToString() == "")//贴签核对
                            {
                                nodo.Rows.Add(new object[] { alltable.Rows[i]["瓶签号"].ToString(), alltable.Rows[i]["批次"].ToString(), alltable.Rows[i]["病人"].ToString(), alltable.Rows[i]["病区"].ToString(), alltable.Rows[i]["床号"].ToString(), MarjorDrug, alltable.Rows[i]["主药颜色"].ToString(), Menstruum, alltable.Rows[i]["溶媒颜色"].ToString(), alltable.Rows[i]["提前打包"].ToString() });
                            }
                            else //已扫描
                            {
                                aldo.Rows.Add(new object[] { alltable.Rows[i]["瓶签号"].ToString(), alltable.Rows[i]["批次"].ToString(), alltable.Rows[i]["病人"].ToString(), alltable.Rows[i]["病区"].ToString(), alltable.Rows[i]["床号"].ToString(), MarjorDrug, alltable.Rows[i]["主药颜色"].ToString(), Menstruum, alltable.Rows[i]["溶媒颜色"].ToString(), alltable.Rows[i]["提前打包"].ToString() });
                            }

                            //drug.Select("drugcode ='" + alltable.Rows[i]["主药"].ToString() + "'")[0][1].ToString();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message );
            }

                if (labelFlag == 0)//未扫描
                {
                    return nodo;
                }
                else if (labelFlag == 1)//已扫描
                {
                    return aldo;
                }
                else //全部
                {
                    return alldo;
                }
        }

        /// <summary>
        /// 为datatable创建列
        /// </summary>
        private void loadDatable()
        {
            alldo.Columns.Add("瓶签号", typeof(string));
            alldo.Columns.Add("批次", typeof(string));
            alldo.Columns.Add("病人", typeof(string));
            alldo.Columns.Add("病区", typeof(string));
            alldo.Columns.Add("床号", typeof(string));
            alldo.Columns.Add("主药", typeof(string));
            alldo.Columns.Add("主药颜色", typeof(string));
            alldo.Columns.Add("溶媒", typeof(string));
            alldo.Columns.Add("溶媒颜色", typeof(string));
            alldo.Columns.Add("提前打包", typeof(string));

            nodo.Columns.Add("瓶签号", typeof(string));
            nodo.Columns.Add("批次", typeof(string));
            nodo.Columns.Add("病人", typeof(string));
            nodo.Columns.Add("病区", typeof(string));
            nodo.Columns.Add("床号", typeof(string));
            nodo.Columns.Add("主药", typeof(string));
            nodo.Columns.Add("主药颜色", typeof(string));
            nodo.Columns.Add("溶媒", typeof(string));
            nodo.Columns.Add("溶媒颜色", typeof(string));
            nodo.Columns.Add("提前打包", typeof(string));

            aldo.Columns.Add("瓶签号", typeof(string));
            aldo.Columns.Add("批次", typeof(string));
            aldo.Columns.Add("病人", typeof(string));
            aldo.Columns.Add("病区", typeof(string));
            aldo.Columns.Add("床号", typeof(string));
            aldo.Columns.Add("主药", typeof(string));
            aldo.Columns.Add("主药颜色", typeof(string));
            aldo.Columns.Add("溶媒", typeof(string));
            aldo.Columns.Add("溶媒颜色", typeof(string));
            aldo.Columns.Add("提前打包", typeof(string));
        }

        /// <summary>
        /// 预先加载所有药品，一般600多种
        /// </summary>
        private void loadDrug()
        {
            DataSet dsDrug = dbHelp.GetPIVAsDB("select distinct drugcode,drugname from DDrug");
            if (dsDrug.Tables.Count > 0)
                drug = dsDrug.Tables[0];
        }

        private string wardChange(string labelno)//病区更改
        {
            DataRow[] dr = ShowDS.Tables[0].Select("瓶签号 like '" + labelno + "'");
            if (dr[0]["病区"].ToString() != dr[0]["新病区"].ToString())
            {
                return "转病区:"+dr[0]["病区"].ToString() + "转" + dr[0]["新病区"].ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 床号更改
        /// </summary>
        /// <param name="labelno"></param>
        /// <returns></returns>
        private string bedchange(string labelno)
        {
            DataRow[] dr = ShowDS.Tables[0].Select("瓶签号 like '" + labelno + "'");
            if (dr[0]["床号"].ToString() != dr[0]["新床号"].ToString())
            {
                return "转床:"+dr[0]["床号"].ToString() + "床转" + dr[0]["新床号"].ToString()+"床"; 
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 重新选择病区
        /// </summary>
        /// <param name="WardName"></param>
        public void Rechoice(string WardName)
        {
            for (int i = 0; i < dgvWards.Rows.Count; i++)
            {
                if (dgvWards.Rows[i].Cells["colWardName"].Value.ToString() == WardName)
                {
                    dgvWards.Rows[i].Cells[0].Value = true;
                }
                else
                {
                    dgvWards.Rows[i].Cells[0].Value = false;
                }
            }
            GetWardList();
            showDgvLabelInfo(1);//重新查询
        }

        /// <summary>
        /// 将dt的记录转存到DT,并返回
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable NodoForRule(DataTable dt)
        {
            DataTable DT = new DataTable();
            DT.Columns.Add("瓶签号", typeof(string));
            DT.Columns.Add("批次", typeof(string));
            DT.Columns.Add("病人", typeof(string));
            DT.Columns.Add("病区", typeof(string));
            DT.Columns.Add("床号", typeof(string));
            DT.Columns.Add("主药", typeof(string));
            DT.Columns.Add("主药颜色", typeof(string));
            DT.Columns.Add("溶媒", typeof(string));
            DT.Columns.Add("溶媒颜色", typeof(string));
            DT.Columns.Add("提前打包", typeof(string));
            try
            {
                int a = 0;
                for (int i = 0; i < authList.Count; i++)
                {
                    if (Convert.ToInt32(authList[i].AUthorityLevel) > a)
                    { a = Convert.ToInt32(authList[i].AUthorityLevel); }
                }

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (Convert.ToInt32(dt.Rows[j]["IVstatus"].ToString()) >= a 
                        && Convert.ToInt32(dt.Rows[j]["IVstatus"].ToString()) < CheckStatus
                        && Convert.ToInt32(dt.Rows[j]["LabelOver"].ToString()) >= 0 
                        && Convert.ToInt32(dt.Rows[j]["WardRetreat"].ToString()) == 0)
                    {
                        DT.Rows.Add(new object[] { dt.Rows[j]["瓶签号"].ToString(), dt.Rows[j]["批次"].ToString(),
                            dt.Rows[j]["病人"].ToString(), dt.Rows[j]["病区"].ToString(), dt.Rows[j]["床号"].ToString(),
                            dt.Rows[j]["主药"].ToString(), dt.Rows[j]["主药颜色"].ToString(), dt.Rows[j]["溶媒"].ToString (),
                            dt.Rows[j]["溶媒颜色"].ToString(), dt.Rows[j]["提前打包"].ToString() });
                    }
                }
                DT.AcceptChanges();
            }
            catch(Exception ex)
            {
                throw new Exception("筛选未扫描瓶签出错", ex);
            }
            return DT;
        }       
    }
}
