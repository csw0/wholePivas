using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using LabelCheckAuthoritySet.common;
using LabelCheckAuthoritySet.entity;
using LabelCheckAuthoritySet.service;
using LabelCheckAuthoritySet.dao;
using PIVAsCommon.Helper;

namespace LabelCheckAuthoritySet
{
    public partial class frmLabelAuthoritySet : Form
    {
        #region  属性
        private CommonUtil commonUtil = new CommonUtil();//公共方法类
        private AuthoritySetService service = new AuthoritySetService();//获取数据库语句的类
        private AuthoritySetDao dao = new AuthoritySetDao();//数据库层
        private string PageName = "";//当前配置页面的名称
        private string PageStatus = "";//当前配置页面的状态
        private Authority auth = new Authority();
        private Authority updateAuth = new Authority();
        private string AuthOtherName = "";//当前页面的其他配置信息;的name
        /// <summary>
        /// 各种操作的集合
        /// </summary>
        private List<IVstatus> statusList = null;
        private string msg = "";//显示用户配置信息
        private string IVName = "";
        private string InIPath = Application.StartupPath + "\\IMEQPIVAs.ini";
        private DB_Help dbHelper = new DB_Help();
        #endregion 

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="IVstatus">当前页面对应的状态</param>
        public frmLabelAuthoritySet(string IVstatus)
        {
            PageStatus = IVstatus;
            statusList = commonUtil.GetIVStatusList();
            InitializeComponent();
        }

        #region 方法
        /// <summary>
        /// 加载最大限制条件的下拉列表框
        /// </summary>
        public void loadComboxMain(string pageStatus)
        {
            comBoxMain.Items.Clear();
            cmBoxOther.Items.Clear();
            try
            {
                ComboxItem cmboxItem = null;
                IVstatus status = null;
                for (int i = 0; i < statusList.Count; i++)
                {
                    status = statusList[i];
                    cmboxItem = new ComboxItem(status.IvStatusName, status.IvStatusCode);
                    if (int.Parse(status.IvStatusCode) < int.Parse(pageStatus))
                    {
                        comBoxMain.Items.Add(cmboxItem);
                    }
                }
                comBoxMain.DisplayMember = "Name";
                comBoxMain.ValueMember = "ID";

                auth = service.FindMainAuthorityByArea(pageStatus, "All");
                if (auth.AuthorityName !=null && auth.AuthorityName !="")
                {
                    foreach (IVstatus ivstatus in statusList)
                    {
                        if (ivstatus.IvStatusCode == auth.AUthorityLevel)
                        {
                            int cmbindex = comBoxMain.FindString(ivstatus.IvStatusName);
                            comBoxMain.SelectedItem = comBoxMain.Items[cmbindex];
                            btnSetAll.Text = "修改";
                        }
                        if (int.Parse(ivstatus.IvStatusCode) < int.Parse(auth.AUthorityLevel))
                        { 
                            cmboxItem = new ComboxItem(ivstatus.IvStatusName, ivstatus.IvStatusCode);
                            cmBoxOther.Items.Add(cmboxItem);
                        }
                    }
                    cmBoxOther.DisplayMember = "Name";
                    cmBoxOther.ValueMember = "ID";
                    if (cmBoxOther.Items.Count > 0)
                    {
                        cmBoxOther.SelectedIndex = 0;
                    }
                }
                else
                {
                    comBoxMain.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 加载批次的复选框
        /// </summary>
        public void LoadBatchCheckbox()
        {
            DataSet ds = service.LoadComboxBatch();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CheckBox cbox = new CheckBox();
                    cbox.Text = dt.Rows[i]["OrderID"].ToString();
                    cbox.Size = new Size(30, 30);
                    cbox.Checked = true;
                    cbox.CheckedChanged += new EventHandler(cbox_CheckedChanged);
                    flpBatchs.Controls.Add(cbox);
                }
            }
        }

        /// <summary>
        /// 批次复选框单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cbox_CheckedChanged(object sender, EventArgs e)
        {
            int i = flpBatchs.Controls.Count;
            int j = 0;
            foreach (Control c in flpBatchs.Controls)
            {
                CheckBox cb = (CheckBox)c;
                if (cb.Checked == false)
                {
                    j = j + 1;
                }
            }
            if (i == j)
            {
                CheckBox cb = (CheckBox)sender;
                cb.Checked = true;
            }

            LblOtherMsgShow();
        }


        /// <summary>
        /// 显示其他规则的公共方法
        /// </summary>
        private void LblOtherMsgShow()
        {
            msg = "";
            AuthOtherName = "";
            if (chkBoxLing.Checked == false && chkBoxUnling.Checked == true)
            {
                msg += "非临时瓶签，";
                AuthOtherName += "UL;";
            }
            else if (chkBoxLing.Checked == true && chkBoxUnling.Checked == false)
            {
                msg += "临时瓶签，";
                AuthOtherName += "L;";
            }
            else if (chkBoxLing.Checked == true && chkBoxUnling.Checked == true)
            {
                msg += "非临时和临时瓶签，";
                AuthOtherName += "UL,L;";
            }


            if (chkBoxUnST.Checked == true && chkBoxST.Checked == false)
            {
                msg += "非临时医嘱，";
                AuthOtherName += "UST;";
            }
            else if (chkBoxUnST.Checked == false && chkBoxST.Checked == true)
            {
                msg += "临时医嘱，";
                AuthOtherName += "SL;";
            }
            else if (chkBoxUnST.Checked == true && chkBoxST.Checked == true)
            {
                msg += "非临时和临时医嘱，";
                AuthOtherName += "UST,ST;";
            }

            if (chkBoxK.Checked == true && chkBoxA.Checked == false)
            {
                msg += "K包，";
                AuthOtherName += "K;";
            }
            else if (chkBoxK.Checked == false && chkBoxA.Checked == true)
            {
                msg += "#包，";
                AuthOtherName += "#;";
            }
            else if (chkBoxK.Checked == true && chkBoxA.Checked == true)
            {
                msg += "K和#包，";
                AuthOtherName += "K,#;";
            }

            //AuthOtherName += GetBatchCheckbox() + ";";
           // msg += "第" + cmBoxBatch.Text + "批次 ，";

            msg += "第" + GetBatchCheckbox() + "批次 ，";
            AuthOtherName += GetBatchCheckbox() + ";";

            ComboxItem cmboxItem = (ComboxItem)cmBoxOther.SelectedItem;
            if (cmboxItem != null)
            {
                AuthOtherName += cmboxItem.ID;
            }
            msg += cmBoxOther.Text + "之后 ";
            msg += "允许" + PageName;
            lblOtherMsg.Text = msg;
        }

        /// <summary>
        /// 取得批次条件
        /// </summary>
        /// <returns></returns>
        private string  GetBatchCheckbox()
        {
            string str = "";
            int i = flpBatchs.Controls.Count;
            foreach (Control c in flpBatchs.Controls)
            {
                CheckBox cb = (CheckBox)c;
                if (cb.Checked == true)
                {
                    str += cb.Text + ",";
                }
            }
            if (str != null && str.Length > 0)
            {
                str = str.Substring(0,str.Length-1);
            }
            return str;
        }

        /// <summary>
        /// 加载显示所有的历史规则信息
        /// </summary>
        public void FindAllAuthority()
        {
            flpHistoryShow.Controls.Clear();
            List<Authority> authList = new List<Authority>();
            authList = service.FindAllAuthorityByArea(PageStatus);
            if (authList != null && authList.Count > 0)
            {
                Authority auth = null;
                for (int i = 0; i < authList.Count; i++)
                {
                    auth = authList[i];
                    ucAuthorityInfo authInfo = new ucAuthorityInfo(auth);
                    flpHistoryShow.Controls.Add(authInfo);
                }
            }
        }

        /// <summary>
        /// 修改时调用的显示方法
        /// </summary>
        /// <param name="auth"></param>
        public void ShowOneRule(Authority auth)
        {
            updateAuth = auth;
            string name1 = commonUtil.LabelSetName(auth.AUthorityLevel);
            string name2 = commonUtil.LabelSetName(auth.AuthorityArea);
            if (auth.AuthorityName.Trim() == "All")
            {

            }
            else
            {
                btnSetOther.Text = "修改";
                label7.Text = "修改权限";
                string otherRule = auth.AuthorityName;
                string[] Rules = otherRule.Split(';');
                if (Rules[0].Equals("UL"))
                {
                    chkBoxUnling.Checked = true;
                    chkBoxLing.Checked = false;
                }
                else if (Rules[0].Equals("L"))
                {
                    chkBoxUnling.Checked = false;
                    chkBoxLing.Checked = true;
                }
                else
                {
                    chkBoxUnling.Checked = true;
                    chkBoxLing.Checked = true;
                }

                if (Rules[1].Equals("UST"))
                {
                    chkBoxUnST.Checked = true;
                    chkBoxST.Checked = false;
                }
                else if (Rules[1].Equals("ST"))
                {
                    chkBoxUnST.Checked = false;
                    chkBoxST.Checked = true;
                }
                else
                {
                    chkBoxUnST.Checked = true;
                    chkBoxST.Checked = true;
                }

                if (Rules[2].Equals("K"))
                {
                    chkBoxK.Checked = true;
                    chkBoxA.Checked = false;
                }
                else if (Rules[2].Equals("#"))
                {
                    chkBoxK.Checked = false;
                    chkBoxA.Checked = true;
                }
                else
                {
                    chkBoxK.Checked = true;
                    chkBoxA.Checked = true;
                }

                foreach (IVstatus ivstatus in statusList)
                {
                    if (ivstatus.IvStatusCode == auth.AUthorityLevel)
                    {
                        int cmbindex = cmBoxOther.FindString(ivstatus.IvStatusName);
                        cmBoxOther.SelectedItem = cmBoxOther.Items[cmbindex];
                    }
                }
            }
        }

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="auth"></param>
        public void UpdateRule(Authority auth)
        {
            ShowOneRule(auth);
        }

        /// <summary>
        /// 修改成功之后重新设置新增页面
        /// </summary>
        public void ReturnAddRule()
        {
            label7.Text = "新增权限";
            chkBoxUnling.Checked = true;
            chkBoxLing.Checked = true;
            chkBoxUnST.Checked = true;
            chkBoxST.Checked = true;
            chkBoxK.Checked = true;
            chkBoxA.Checked = true;
            cmBoxOther.SelectedIndex = 0;
            foreach (Control c in flpBatchs.Controls)
            {
                CheckBox cb = (CheckBox)c;
                cb.Checked = true;
            }
            btnSetOther.Text = "新增";
        }

        /// <summary>
        /// 加载ini文件中的是否调用计费接口的信息
        /// </summary>
        public void LoadInIFile()
        {
            string NodeValue = service.ReadInIFile("IsCharge", IVName);
            if (NodeValue == "")
            {
                MessageBox.Show("初始配置文件没有计费接口，默认为不需要调用！！！");
                dbHelper.IniWriteValuePivas("IsCharge",IVName,"0");
                chkBoxJiFei.Checked = false;
            }
            else
            {
                if (NodeValue == "0")
                {
                    chkBoxJiFei.Checked = false;
                }
                else
                {
                    chkBoxJiFei.Checked = true;
                }
            }



            string NodeValue1 = service.ReadInIFile("BatchChecked", IVName);
            if (NodeValue1 == "")
            {
                MessageBox.Show("初始配置文件没有批次是否全选，默认为不需要全选！！！");
                dbHelper.IniWriteValuePivas("BatchChecked", IVName, "0");
                chkBoxBatches.Checked = false;
            }
            else
            {
                if (NodeValue1 == "0")
                {
                    chkBoxBatches.Checked = false;
                }
                else
                {
                    chkBoxBatches.Checked = true;
                }
            }

            string NodeValue2 = service.ReadInIFile("IsUseRule", IVName);
            if (NodeValue2 == "")
            {
                MessageBox.Show("初始配置文件没有规则是否使用，默认为不需要使用！！！");
                dbHelper.IniWriteValuePivas("IsUseRule", IVName, "0");
                chkBoxBatches.Checked = false;
            }
            else
            {
                if (NodeValue2 == "0")
                {
                    checkBox1.Checked = false;
                }
                else
                {
                    checkBox1.Checked = true;
                }
            }

            string NodeValue3 = service.ReadInIFile("IsShowAllDward", IVName);
            if (NodeValue3 == "")
            {
                MessageBox.Show("初始配置文件没有规则是否提示全病区扫描完毕信息，默认为不需要使用！！！");
                dbHelper.IniWriteValuePivas("IsShowAllDward", IVName, "0");
                chkBoxBatches.Checked = false;
            }
            else
            {
                if (NodeValue3 == "0")
                {
                    checkBox2.Checked = false;
                }
                else
                {
                    checkBox2.Checked = true;
                }
            }

            string NodeValue4 = service.ReadInIFile("FontSize", "Size");
            if (NodeValue4 == "")
            {
                MessageBox.Show("初始配置文件没有指定字号，默认为18！！！");
                dbHelper.IniWriteValuePivas("FontSize", "Size", "18");
                comboBox11.Text = "18";
            }
            else
            {
                comboBox11.Text = NodeValue4;
            }

            string NodeValue5 = service.ReadInIFile("FontSize", "Blank");
            if (NodeValue5 == "")
            {
                MessageBox.Show("初始配置文件没有指定间距 ，默认为5！！！");
                dbHelper.IniWriteValuePivas("FontSize", "Blank", "5");
                comboBox11.Text = "5";
            }
            else
            {
                textBox1.Text = NodeValue5;
            }

            string NodeValue6 = service.ReadInIFile("IsBreak", "All");
            if (NodeValue6 == "")
            {
                MessageBox.Show("初始配置文件没有瓶签失败是否暂停，默认为不需要中断！！！");
                dbHelper.IniWriteValuePivas("IsBreak", "All", "0");
                checkBox3.Checked = false;
            }
            else
            {
                if (NodeValue6 == "0")
                {
                    checkBox3.Checked = false;
                }
                else
                {
                    checkBox3.Checked = true;
                }
            }


            string co = string.Empty;
            co = service.ReadInIFile("advanceColor", "adcolorR") + "," + service.ReadInIFile("advanceColor", "adcolorG") + "," + service.ReadInIFile("advanceColor", "adcolorB");
            try
            {
                button1.BackColor = ColorTranslator.FromHtml(co);
                colorDialog1.Color = ColorTranslator.FromHtml(co);
            }
            catch
            {
                MessageBox.Show("初始配置文件没有提前打包配色，默认为白！！！");
                colorDialog1.Color = Color.White;
                button1.BackColor = Color.White;
                dbHelper.IniWriteValuePivas("advanceColor", "adcolorR", colorDialog1.Color.R.ToString());
                dbHelper.IniWriteValuePivas("advanceColor", "adcolorG", colorDialog1.Color.G.ToString());
                dbHelper.IniWriteValuePivas("advanceColor", "adcolorB", colorDialog1.Color.B.ToString());

            }

            string NodeValue7 = service.ReadInIFile("Voice", "Use");
            if (NodeValue7 == "")
            {
                MessageBox.Show("初始配置文件没有选择语音使用，默认为详细语音播报！！！");
                dbHelper.IniWriteValuePivas("Voice", "Use", "0");
                radioButton1.Checked = true;
            }
            else
            {
                if (NodeValue7 == "0")
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
            }

            string NodeValue8 = service.ReadInIFile("IsUselblstatus", "Use");
            if (NodeValue8 == "")
            {
                MessageBox.Show("初始配置文件没有扫描结果是否使用当前状态，默认为使用！！！");
                dbHelper.IniWriteValuePivas("IsUselblstatus", "Use", "1");
                checkBox4.Checked = true;
            }
            else
            {
                if (NodeValue8 == "0")
                {
                    checkBox4.Checked = false;
                }
                else
                {
                    checkBox4.Checked = true;
                }
            }
        }

        public void SetInIFile()
        {
            if (chkBoxJiFei.Checked == true)
            {
                dbHelper.IniWriteValuePivas("IsCharge", IVName, "1");
            }
            else
            {
                dbHelper.IniWriteValuePivas("IsCharge", IVName, "0");
            }

            if (chkBoxBatches.Checked == true)
            {
                dbHelper.IniWriteValuePivas("BatchChecked", IVName, "1");
            }
            else
            {
                dbHelper.IniWriteValuePivas("BatchChecked", IVName, "0");
            }

            if (checkBox1.Checked == true)
            {
                dbHelper.IniWriteValuePivas("IsUseRule", IVName, "1");
            }
            else
            {
                dbHelper.IniWriteValuePivas("IsUseRule", IVName, "0");
            }

            if (checkBox2.Checked == true)
            {
                dbHelper.IniWriteValuePivas("IsShowAllDward", IVName, "1");
            }
            else
            {
                dbHelper.IniWriteValuePivas("IsShowAllDward", IVName, "0");
            }

            if (checkBox3.Checked == true)
            {
                dbHelper.IniWriteValuePivas("IsBreak", "All", "1");
            }
            else
            {
                dbHelper.IniWriteValuePivas("IsBreak", "All", "0");
            }

            if (comboBox11.Text != "")
            {
                dbHelper.IniWriteValuePivas("FontSize", "Size", comboBox11.Text);
            }

            if (textBox1.Text != "")
            {
                dbHelper.IniWriteValuePivas("FontSize", "Blank", textBox1.Text);
            }
            else
            {
                dbHelper.IniWriteValuePivas("FontSize", "Blank", "5");
            }

            if (radioButton1.Checked)
            {
                dbHelper.IniWriteValuePivas("Voice", "Use", "0");
            }
            else
            {
                dbHelper.IniWriteValuePivas("Voice", "Use", "1");
            }

            if (checkBox4.Checked)
            {
                dbHelper.IniWriteValuePivas("IsUselblstatus", "Use", "1");
            }
            else
            {
                dbHelper.IniWriteValuePivas("IsUselblstatus", "Use", "0");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadIVName()
        {
            if (PageStatus == "4")
            {
                IVName = "YP";
            }
            else if (PageStatus == "5")
            {
                IVName = "PY";
            }
            else if (PageStatus == "7")
            {
                IVName = "JC";
            }
            else if (PageStatus == "9")
            {
                IVName = "PZ";
            }
            else if (PageStatus == "11")
            {
                IVName = "CC";
            }
            else if (PageStatus == "13")
            {
                IVName = "DB";
            }
            else if (PageStatus == "30")
            {
                IVName = "TQ";
            }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLabelAuthoritySet_Load(object sender, EventArgs e)
        {
            LoadIVName();
            //commonUtil.createXML();//创建xml文件

            LoadBatchCheckbox();//加载批次复选框

            PageName = commonUtil.LabelSetName(PageStatus);

            LoadSet();//读取配置信息

            if (PageName.Trim() != "")
            {
                lblPageName.Text = PageName;
                lblAllowMsg.Text = "之后允许" + PageName;
            }
            else
            {
                this.Close();
            }

            loadComboxMain(PageStatus);//加载主下拉框
            FindAllAuthority();

            LoadInIFile();
        }

        /// <summary>
        /// 主限制条件下拉框更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comBoxMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMainMsg.Text = comBoxMain.Text + "之后 允许" + PageName;
        }

        /// <summary>
        /// 设定主权限按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetAll_Click(object sender, EventArgs e)
        {
            bool flag = false;
            if (btnSetAll.Text.Trim() == "设定")
            {
                ComboxItem cmboxItem = (ComboxItem)comBoxMain.SelectedItem;
                flag = service.SetMainService("All", PageStatus, cmboxItem.ID);
            }
            else//修改的情况
            {
                ComboxItem cmboxItem = (ComboxItem)comBoxMain.SelectedItem;
                flag = service.UpdateMainService(auth.SeqNo,"All",PageStatus,cmboxItem.ID);
            }

            if (flag)
            {
                MessageBox.Show("操作成功！");
                loadComboxMain(PageStatus);
                FindAllAuthority();//刷新页面
            }
            else
            {
                MessageBox.Show("操作失败！");
            }
        }

        /// <summary>
        /// 选择条件更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void raBtnUnling_CheckedChanged(object sender, EventArgs e)
        {
            LblOtherMsgShow();
        }

        /// <summary>
        /// 设定选择条件更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkBoxUnling_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxLing.Checked == false && chkBoxUnling.Checked == false)
            {
                chkBoxUnling.Checked = true;
            }
            LblOtherMsgShow();
        }

        /// <summary>
        /// 设定选择条件更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkBoxLing_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxLing.Checked == false && chkBoxUnling.Checked == false)
            {
                chkBoxLing.Checked = true;
            }
            LblOtherMsgShow();
        }

        /// <summary>
        /// 设定选择条件更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkBoxUnST_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxUnST.Checked == false && chkBoxST.Checked == false)
            {
                chkBoxUnST.Checked = true;
            }
            LblOtherMsgShow();
        }

        /// <summary>
        /// 设定选择条件更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkBoxST_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxUnST.Checked == false && chkBoxST.Checked == false)
            {
                chkBoxST.Checked = true;
            }
            LblOtherMsgShow();
        }

        /// <summary>
        /// 设定选择条件更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkBoxK_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxK.Checked == false && chkBoxA.Checked == false)
            {
                chkBoxK.Checked = true;
            }
            LblOtherMsgShow();
        }

        /// <summary>
        /// 设定选择条件更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkBoxA_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxK.Checked == false && chkBoxA.Checked == false)
            {
                chkBoxA.Checked = true;
            }
            LblOtherMsgShow();
        }

        /// <summary>
        /// 保存其他配置信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetOther_Click(object sender, EventArgs e)
        {
            if (btnSetOther.Text.Trim() == "新增")
            {
                ComboxItem cmboxItem = (ComboxItem)cmBoxOther.SelectedItem;
                if (cmboxItem != null)
                {
                    Authority authOther = new Authority();
                    authOther.AuthorityName = AuthOtherName;
                    authOther.AuthorityArea = PageStatus;
                    authOther.AUthorityLevel = cmboxItem.ID;
                    bool flag = service.SetOtherService(authOther);
                    if (flag)
                    {
                        MessageBox.Show("操作成功！");
                        loadComboxMain(PageStatus);
                    }
                    else
                    {
                        MessageBox.Show("操作失败！");
                    }
                }
                else
                {
                    MessageBox.Show("请先配置主权限！");
                }
            }
            else //修改权限
            { 
                ComboxItem cmboxItem = (ComboxItem)cmBoxOther.SelectedItem;
                if (cmboxItem != null)
                {
                    Authority authOther = new Authority();
                    authOther.SeqNo = updateAuth.SeqNo;
                    authOther.AuthorityName = AuthOtherName;
                    authOther.AuthorityArea = PageStatus;
                    authOther.AUthorityLevel = cmboxItem.ID;

                    bool flag = service.UpdateOtherService(authOther);

                    if (flag)
                    {
                        MessageBox.Show("操作成功！");
                        loadComboxMain(PageStatus);
                        ReturnAddRule();
                    }
                    else
                    {
                        MessageBox.Show("操作失败！");
                    }
                }
            }

            FindAllAuthority();//刷新权限显示
        }


        /// <summary>
        /// 判断是否调用计费接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkBoxJiFei_CheckedChanged(object sender, EventArgs e)
        {
            //SetInIFile();
        }
        #endregion

        #region  基本行列配置

        //seldb sel = new seldb();
        DataTable dt = new DataTable();
        DataTable dt_cm = new DataTable();
        public delegate void childclose();
        public event childclose closefather;
        string label_color;


        /// <summary>
        /// 调出颜色框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void color_click(object sender, EventArgs e)
        {
            Button Label_Color = (Button)sender;
            label_color = Label_Color.Name;
            if (panel6.Visible == true)
            {
                panel6.Visible = false;
            }
            else
            {
                panel6.Visible = true;
            }
        }

        /// <summary>
        /// 颜色的选取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_Color_Click(object sender, EventArgs e)
        {
            Label Label_UColor = (Label)sender;
            //BColor = Label_UColor.BackColor.R + "," + Label_UColor.BackColor.G + "," + Label_UColor.BackColor.B;
            //Label_Batch2.BackColor = System.Drawing.ColorTranslator.FromHtml(BColor);
            //Panel_Total2.BackColor = System.Drawing.ColorTranslator.FromHtml(BColor);   
            if (label_color == "Color1")
            {
                Color1.BackColor = Label_UColor.BackColor;
            }
            else if (label_color == "Color2")
            {
                Color2.BackColor = Label_UColor.BackColor;
            }
            else if (label_color == "Color3")
            {
                Color3.BackColor = Label_UColor.BackColor;
            }
            else if (label_color == "Color4")
            {
                Color4.BackColor = Label_UColor.BackColor;
            }
            else if (label_color == "Color5")
            {
                Color5.BackColor = Label_UColor.BackColor;
            }
            else if (label_color == "Color6")
            {
                Color6.BackColor = Label_UColor.BackColor;
            }
            panel5.Visible = false;
        }





        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            string color1 = Color1.BackColor.R + "," + Color1.BackColor.G + "," + Color1.BackColor.B;
            string color2 = Color2.BackColor.R + "," + Color2.BackColor.G + "," + Color2.BackColor.B;
            string color3 = Color3.BackColor.R + "," + Color3.BackColor.G + "," + Color3.BackColor.B;
            string color4 = Color4.BackColor.R + "," + Color4.BackColor.G + "," + Color4.BackColor.B;
            string color5 = Color5.BackColor.R + "," + Color5.BackColor.G + "," + Color5.BackColor.B;
            string color6 = Color6.BackColor.R + "," + Color6.BackColor.G + "," + Color6.BackColor.B;
            string NextDay = comboBox7.Text + ":" + comboBox8.Text;
            string WaitTime = comboBox9.Text;
            string FreshTime = comboBox10.Text;
            try
            {
                SetInIFile();
                bool flag = dao.updateCombox(PageName, comboBox1.Text, comboBox2.Text, comboBox3.Text, comboBox4.Text, comboBox5.Text, comboBox6.Text, color1, color2, color3, color4, color5, color6, NextDay, WaitTime, FreshTime);
               if (flag == true)
               {
                   MessageBox.Show("设置成功！");
               }else
               {
                   MessageBox.Show("设置失败！");
               }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 页面打开时读取数据库的预先配置信息
        /// </summary>
        private void LoadSet()
        {
            DataSet ds = dao.getCheckFormSet(PageName);
            
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                dt_cm = ds.Tables[0];
                comboBox2.Text = dt_cm.Rows[0]["Content2"].ToString();
                comboBox3.Text = dt_cm.Rows[0]["Content3"].ToString();
                comboBox4.Text = dt_cm.Rows[0]["Content4"].ToString();
                comboBox5.Text = dt_cm.Rows[0]["Content5"].ToString();
                comboBox6.Text = dt_cm.Rows[0]["Content6"].ToString();
                Color1.BackColor = ColorTranslator.FromHtml(dt_cm.Rows[0]["Color1"].ToString());
                Color2.BackColor = ColorTranslator.FromHtml(dt_cm.Rows[0]["Color2"].ToString());
                Color3.BackColor = ColorTranslator.FromHtml(dt_cm.Rows[0]["Color3"].ToString());
                Color4.BackColor = ColorTranslator.FromHtml(dt_cm.Rows[0]["Color4"].ToString());
                Color5.BackColor = ColorTranslator.FromHtml(dt_cm.Rows[0]["Color5"].ToString());
                Color6.BackColor = ColorTranslator.FromHtml(dt_cm.Rows[0]["Color6"].ToString());
                if (dt_cm.Rows[0]["NextDay"].ToString().Length >= 5)
                {
                    comboBox7.Text = dt_cm.Rows[0]["NextDay"].ToString().Substring(0, 2);
                    comboBox8.Text = dt_cm.Rows[0]["NextDay"].ToString().Substring(3, 2);
                }
                else
                {
                    comboBox7.Text = "";
                    comboBox8.Text = "";
                }
                comboBox9.Text = dt_cm.Rows[0]["WaitTime"].ToString();
                comboBox10.Text = dt_cm.Rows[0]["FreshTime"].ToString();
            }
        }
        #endregion

        private void chkBoxJiFei_Click(object sender, EventArgs e)
        {
            SetInIFile();
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            SetInIFile();
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            SetInIFile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button1.BackColor = colorDialog1.Color;
            }
            dbHelper.IniWriteValuePivas("advanceColor", "adcolorR", colorDialog1.Color.R.ToString());
            dbHelper.IniWriteValuePivas("advanceColor", "adcolorG", colorDialog1.Color.G.ToString());
            dbHelper.IniWriteValuePivas("advanceColor", "adcolorB", colorDialog1.Color.B.ToString());
            string co = string.Empty;
            co = service.ReadInIFile("advanceColor", "adcolorR") + "," + service.ReadInIFile("advanceColor", "adcolorG") + "," + service.ReadInIFile("advanceColor", "adcolorB");
            button1.BackColor = ColorTranslator.FromHtml(co);
        }

        





















    }
}
