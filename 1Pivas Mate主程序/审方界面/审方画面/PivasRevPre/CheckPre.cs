using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ChargeInterface;
using PIVAsCommon.Helper;
using PIVAsCommon;
using PivasLimitDES;

namespace PivasRevPre
{
    public partial class CheckPre : UserControl, IMenuManager
    {
        #region  变量
        DB_Help dbHelp = new DB_Help();
        SQL SQLStr = new SQL();
        MesBox Mes = new MesBox();
        ICharge preCharge = null;//计费接口，使用时先判断是否为null，在窗体构造时创建

        private DataSet dsWard = new DataSet();
        public static DataTable DTable = new DataTable();
        public DataTable NameTable = new DataTable();//保存根据患者编号或床号查询到的处方信息
        public DataTable NameTableLeft = new DataTable();//保存根据患者编号或床号查询到的处方信息
        public DataTable WardsTable = new DataTable();//保存当前选中的病区

        public static string employeeID = "";//员工ID，从1自增
        public string employeeName = "";//员工姓名
        public string employeeCode = "";//员工工号/编号
        
        private string WardArea = "";
        private string Open = "0";
        private string Empty = "0";
        private string Complete = "0";
        public string Mode = "0";
        public string ShowConfirm = "0";
        public string SelectAll = "0";
        public string NameList = "0";
        public static string sys = "0,3,5", per = "1",st = "0";//sys:系统审方  per:人工审方   st:长期临时

        private int SYSFlag = 0;
        public static int RIndex = 0;
        public static int TIndex = 0;
        public static string Care1 = "关注";
        public static string PATCODE = "";
        public static string PREID = "";
        public string wardcode = "";
        private Assembly assembly;
        private Type t;
        private string GroupNO = string.Empty;
        private ContextMenuStrip CMS = new ContextMenuStrip();

        public static string Confirmation = "1";
        public string RightColor = "121, 196, 136";
        public string Level3color = "255, 255, 192";
        public string Level5color = "253, 212, 218";
        public string SelectedColor = "243, 243, 243";
        
        private Color Seled = Color.Blue;
        private Color unSel = Color.Black;
        public int ckFlag = 0;//保存最后一次是checkBox操作还是选择操作 0：ckbox 1:选择
        public int ckID = 0;//保存当前的行号        

        string DaoJiShi = "300";

        string ChangQiLinShi = "0";
        string CheckChangQiLinShi = "0";
        #endregion

        /// <summary>
        /// 有参构造方法
        /// </summary>
        /// <param name="EMployeeID">员工编号</param>
        /// <param name="AccountID">员工登录号</param>
        /// <param name="EMployeeName">员工姓名</param>
        public CheckPre(string EMployeeID, string AccountID, string EMployeeName)
        {
            InitializeComponent();
            CheckPre.employeeID = EMployeeID;
            this.employeeCode = AccountID;
            this.employeeName = EMployeeName;
            #region csw修改，需要此exe下配置文件IMEQPIVAs.ini
            preCharge = ChargeFactory.GetCharge(dbHelp.IniReadValuePivas("Charge", "HospitalType"));
            #endregion
        }
        #region 方法
        /// <summary>
        /// 加载病区组的下拉框
        /// </summary>
        private void LoadComboxWardArea()
        {
            cmBoxWardArea.Items.Add("全部病区组");
            cmBoxWardArea.Text = "全部病区组";

            string sql = "select distinct WardArea  from Dward where WardArea <>'' and WardArea is Not Null;";
            DataSet ds = new DataSet();
            ds = dbHelp.GetPIVAsDB(sql.ToString());

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    cmBoxWardArea.Items.Add(ds.Tables[0].Rows[i]["WardArea"].ToString());
                }
            }
        }

        /// <summary>
        /// 加载用户保存的配置信息
        /// </summary>
        private void SetUserPeizhi()
        {
            string str = "";
            try
            {
                DataTable dtUserPeizhi;
                //str = "SELECT DEmployeeID,WardOpen,WardIdle,RevPreOver,PreviewMode,Confirm,SelectAll,NameList,RightColor,Level3Color,Level5Color ,SelectedColor  " +
                //"FROM RevPreFormSet WHERE DEmployeeID = " + EMployeeID;
                str = "SELECT top 1 DEmployeeID,WardOpen,WardIdle,RevPreOver,Confirmation,PreviewMode,Confirm,SelectAll,NameList,RightColor,Level3Color,Level5Color  ,SelectedColor  " +
               "FROM RevPreFormSet ";

                DataSet ds = dbHelp.GetPIVAsDB(str);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dtUserPeizhi = ds.Tables[0];
                }
                else
                {
                    dtUserPeizhi = null;
                }
                if (dtUserPeizhi == null || dtUserPeizhi.Rows.Count == 0)
                {
                    str = "INSERT INTO RevPreFormSet Values(" + employeeID + "," + Empty + "," + Open + "," + Complete + "," + Mode + "," + ShowConfirm + "," + SelectAll + "," + NameList + ",1 )";
                    dbHelp.SetPIVAsDB(str);
                }
                else
                {
                    Open = dtUserPeizhi.Rows[0]["WardOpen"].ToString();
                    Empty = dtUserPeizhi.Rows[0]["WardIdle"].ToString();
                    Complete = dtUserPeizhi.Rows[0]["RevPreOver"].ToString();
                    Mode = dtUserPeizhi.Rows[0]["PreviewMode"].ToString();
                    ShowConfirm = dtUserPeizhi.Rows[0]["Confirm"].ToString();
                    SelectAll = dtUserPeizhi.Rows[0]["SelectAll"].ToString();
                    NameList = dtUserPeizhi.Rows[0]["NameList"].ToString();
                    Confirmation = dtUserPeizhi.Rows[0]["Confirmation"].ToString();

                    if (dtUserPeizhi.Rows[0]["RightColor"].ToString().Trim() != "")
                    {
                        RightColor = dtUserPeizhi.Rows[0]["RightColor"].ToString();
                        Level3color = dtUserPeizhi.Rows[0]["Level3Color"].ToString();
                        Level5color = dtUserPeizhi.Rows[0]["Level5Color"].ToString();
                        SelectedColor = dtUserPeizhi.Rows[0]["SelectedColor"].ToString();
                    }
                }
                if (Mode == "0")
                {
                    pnlHongqi.Visible = true;
                    pnlbaiqi.Visible = false;
                    pnlDetail.Visible = false;
                    pnlTable.Visible = true;
                    txtSearch.Text = "患者姓名/住院号/床号";
                }
                else
                {
                    pnlHongqi.Visible = false;
                    pnlbaiqi.Visible = false;
                    pnlTable.Visible = false;
                    pnlDetail.Visible = true;
                    txtSearch.Text = "患者姓名/住院号/床号";
                    btnPass.Visible = false;
                    checkBox1.Visible = false;
                }
                //陆卓春20140617加

                SetRemarkWidth();

                if (NameList == "1")
                    label2.Visible = true;
                else
                    label2.Visible = false;
                DT.Value = DateTime.Now;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("请检查表RevPreFormSet中字段是否缺少：" + str);
                Mes.ShowDialog("提示", ex.Message);
            }
        }

        /// <summary>
        /// 获取病区的table(包含加载病区显示的方法)
        /// </summary>
        public DataTable getWards()
        {
            string str = "";
            DataTable dtWard = null;
            str = "EXEC [bl_getCPcountbyarea] '" + DT.Text + "','" + per + "'," + Empty + "," + Open + ",'" + WardArea + "'";
            DataSet ds = dbHelp.GetPIVAsDB(str);
            if (ds != null && ds.Tables[0].Rows.Count >1)
            {
                dtWard = ds.Tables[0];
                lblUnchecks.Text = dtWard.Rows[0]["UnCheckCount"].ToString();
                lblAllChecks.Text = dtWard.Rows[0]["TotalCount"].ToString();

                dtWard.Rows.RemoveAt(0);//去除全部那一行的显示
                DataTable dtt = dtWard.Copy();

                if (Complete == "0")//仅显示审方未完成病区
                {
                    dtt.Rows.Clear();

                    DataRow[] R = dtWard.Select("UnCheckCount > " + Complete + " ", "WardSeqNo");
                    foreach (DataRow dr in R)
                    {
                        dtt.ImportRow(dr);
                    }
                }
                WardsTable = dtt;
                showWards(WardsTable);
            }
            else
            {
                MessageBox.Show("没有符合要求的病区！");
            }
            return dtWard;
        }        

        /// <summary>
        /// 加载病区显示
        /// </summary>
        /// <param name="dt">病区的table</param>
        private void showWards(DataTable dt)
        {
            dgvWards.Rows.Clear();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgvWards.Rows.Add(1);
                    dgvWards.Rows[i].Cells["cbBoxSel"].Value = "False";//=true 默认全选
                    dgvWards.Rows[i].Cells["colWardCode"].Value = dt.Rows[i]["WardCode"].ToString();
                    dgvWards.Rows[i].Cells["colWardName"].Value = dt.Rows[i]["WardName"].ToString();
                    dgvWards.Rows[i].Cells["colUnchecks"].Value = dt.Rows[i]["UnCheckCount"].ToString();
                    dgvWards.Rows[i].Cells["colAllChecks"].Value = dt.Rows[i]["TotalCount"].ToString();
                }
                
                GetSelPrescriptions(0);
                BDNameList(0);
            }
            else
            { 
                
            }
        }

        /// <summary>
        /// 根据搜索条件获取处方信息
        /// </summary>
        public void GetSelPrescriptions()
        {
            try
            {
                string str = "";
                str = SQLStr.SeaPerCheckResult(sys, per, GetWardsStr(), DT.Text, st, GetDrugTypeStr());
                
                DataSet ds = dbHelp.GetPIVAsDB(str);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    DTable = ds.Tables[0];
                }
                NameTable = DTable;
                AddResult(DTable);
                DTable = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查询指定病区的处方
        /// </summary>
        /// <param name="RowIndex">行号</param>
        public void GetSelPrescriptions(int RowIndex)
        {
            try
            {
                string str = "";
                str = SQLStr.SeaPerCheckResult(sys, per, GetWardsStr(RowIndex), DT.Text, st, GetDrugTypeStr());
                DataSet ds = dbHelp.GetPIVAsDB(str);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    DTable = ds.Tables[0];
                }
                NameTable = DTable;
                AddResult(DTable);
                DTable = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 加载审方结果的方法
        /// </summary>
        /// <param name="dtResult"></param>
        public void AddResult(DataTable dtResult)
        {
            try
            {
                dgvResult.Rows.Clear();
                
                if (dtResult == null || dtResult.Rows.Count <= 0)
                {
                    information1.clear();
                    med1.clear();
                    return;
                }
                //dgvResult.DataSource = dtResult;
                dgvResult.DefaultCellStyle.SelectionBackColor = System.Drawing.ColorTranslator.FromHtml(SelectedColor);//设置选中的背景色
                //dgvResult.SelectionChanged += new EventHandler(kong);
                Application.DoEvents();
                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    //Application.DoEvents();
                    dgvResult.Rows.Add(1);
                    dgvResult.Rows[i].Cells["Check"].Value = "False";
                    dgvResult.Rows[i].Cells["InceptDT"].Value = dtResult.Rows[i]["InceptDT"].ToString();
                    dgvResult.Rows[i].Cells["WardName"].Value = dtResult.Rows[i]["WardName"].ToString();
                    dgvResult.Rows[i].Cells["BedNo"].Value = dtResult.Rows[i]["BedNo"].ToString();
                    dgvResult.Rows[i].Cells["PatName"].Value = dtResult.Rows[i]["PatName"].ToString();
                    dgvResult.Rows[i].Cells["GroupNo"].Value = dtResult.Rows[i]["GroupNo"].ToString().Trim();
                    dgvResult.Rows[i].Cells["DoctorExplain"].Value = dtResult.Rows[i]["DoctorExplain"].ToString().Trim();
                    dgvResult.Rows[i].Cells["Level"].Value = dtResult.Rows[i]["Level"].ToString();
                    dgvResult.Rows[i].Cells["PatientCode"].Value = dtResult.Rows[i]["PatientCode"].ToString();
                    dgvResult.Rows[i].Cells["PrescriptionID"].Value = dtResult.Rows[i]["PrescriptionID"].ToString();
                    dgvResult.Rows[i].Cells["StartDT"].Value = dtResult.Rows[i]["StartDT"].ToString();

                    dgvResult.Rows[i].Cells["Remark1"].Value = dtResult.Rows[i]["Remark1"].ToString();
                    dgvResult.Rows[i].Cells["Remark2"].Value = dtResult.Rows[i]["Remark2"].ToString();
                    dgvResult.Rows[i].Cells["Remark3"].Value = dtResult.Rows[i]["Remark3"].ToString();
                    dgvResult.Rows[i].Cells["Remark4"].Value = dtResult.Rows[i]["Remark4"].ToString();
                    dgvResult.Rows[i].Cells["Remark5"].Value = dtResult.Rows[i]["Remark5"].ToString();
                    dgvResult.Rows[i].Cells["Remark6"].Value = dtResult.Rows[i]["Remark6"].ToString();


                    if (dtResult.Rows[i]["Attention"].ToString() == "False")
                    {
                        dgvResult.Rows[i].Cells["Care"].Tag = 0;
                        dgvResult.Rows[i].Cells["Care"].Value = "";

                    }
                    else
                    {
                        dgvResult.Rows[i].Cells["Care"].Tag = 1;
                        dgvResult.Rows[i].Cells["Care"].Value = Care1.ToString();
                        //dgvResult.Rows[i].Cells["Care"].Value = global::PivasRevPre.Properties.Resources._1;
                    }
                    switch (dtResult.Rows[i]["PStatus"].ToString())//判断处方是否审过
                    {
                        case "2":
                            //dgvResult.Rows[i].Cells["Rev2"].Value = global::PivasRevPre.Properties.Resources._3;//蓝色小人，通过
                            //dgvResult.Rows[i].Cells["Rev1"].Value = global::PivasRevPre.Properties.Resources._3;
                            dgvResult.Rows[i].Cells["Rev1"].Tag = 2;
                            break;
                        case "3":
                            //dgvResult.Rows[i].Cells["Rev2"].Value = global::PivasRevPre.Properties.Resources._4;//红色小人，退单
                            //dgvResult.Rows[i].Cells["Rev1"].Value = global::PivasRevPre.Properties.Resources._4;
                            dgvResult.Rows[i].Cells["Rev1"].Tag = 3;
                            break;
                        default:
                            dgvResult.Rows[i].Cells["Rev2"].Value = null;//未审过，没小人
                            dgvResult.Rows[i].Cells["Rev1"].Value = null;
                            dgvResult.Rows[i].Cells["Rev1"].Tag = 1;
                            break;
                    }

                    if (dtResult.Rows[i]["PStatus"].ToString() == "1")
                    {
                        dgvResult.Rows[i].Cells["IsPass"].Value = "未审";
                    }
                    else if (dtResult.Rows[i]["PStatus"].ToString() == "3")
                    {
                        dgvResult.Rows[i].Cells["IsPass"].Value = "不通过";
                        dgvResult.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                        dgvResult.Rows[i].DefaultCellStyle.BackColor = Color.DarkGray;

                        //dgvResult.Rows[i].DefaultCellStyle.SelectionBackColor = Color.Silver;
                    }
                    else if (dtResult.Rows[i]["PStatus"].ToString() == "2")
                    {
                        dgvResult.Rows[i].Cells["IsPass"].Value = "已审通过";
                    }

                    switch (dtResult.Rows[i]["Level"].ToString())
                    {
                        case "0":
                            //dgvResult.Rows[i].Cells["IsPass"].Value = "通过";
                            dgvResult.Rows[i].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(RightColor);
                            dgvResult.Rows[i].HeaderCell.Style.BackColor = System.Drawing.ColorTranslator.FromHtml(RightColor);
                            //dgvResult.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(121, 196, 136);
                            //dgvResult.Rows[i].HeaderCell.Style.BackColor = Color.FromArgb(121, 196, 136);
                            //dgvResult.Rows[i].DefaultCellStyle.SelectionBackColor = Color.FromArgb(205, 250, 205);
                            dgvResult.Rows[i].DefaultCellStyle.SelectionForeColor = Color.Black;


                            switch (dtResult.Rows[i]["DoctorOperate"].ToString())
                            {
                                case "0": dgvResult.Rows[i].Cells["dgvSysResult"].Value = "系统通过";
                                    break;
                                case "1": dgvResult.Rows[i].Cells["dgvSysResult"].Value = "强制执行";
                                    break;
                                case "2": dgvResult.Rows[i].Cells["dgvSysResult"].Value = "接受退单";
                                    break;
                                default: dgvResult.Rows[i].Cells["dgvSysResult"].Value = "系统通过";
                                    break;
                            }
                            break;

                        case "3":
                            //dgvResult.Rows[i].Cells["IsPass"].Value = "不通过";

                            //dgvResult.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(242, 201, 74);
                            //dgvResult.Rows[i].HeaderCell.Style.BackColor = Color.FromArgb(242, 201, 74);
                            dgvResult.Rows[i].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Level3color);
                            dgvResult.Rows[i].HeaderCell.Style.BackColor = System.Drawing.ColorTranslator.FromHtml(Level3color);
                            //dgvResult.Rows[i].DefaultCellStyle.SelectionBackColor = Color.FromArgb(252, 254, 182);
                            dgvResult.Rows[i].DefaultCellStyle.SelectionForeColor = Color.Black;

                            //dgvResult.Rows[i].Cells["dgvSysResult"].Value = "3级未过";
                            switch (dtResult.Rows[i]["DoctorOperate"].ToString())
                            {
                                case "0": dgvResult.Rows[i].Cells["dgvSysResult"].Value = "3级未过";
                                    break;
                                case "1": dgvResult.Rows[i].Cells["dgvSysResult"].Value = "强制执行";
                                    break;
                                case "2": dgvResult.Rows[i].Cells["dgvSysResult"].Value = "接受退单";
                                    break;
                                default: dgvResult.Rows[i].Cells["dgvSysResult"].Value = "3级未过";
                                    break;
                            }
                            break;

                        case "5":
                            //dgvResult.Rows[i].Cells["IsPass"].Value = "不通过";
                            dgvResult.Rows[i].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Level5color);
                            dgvResult.Rows[i].HeaderCell.Style.BackColor = System.Drawing.ColorTranslator.FromHtml(Level5color);
                            //dgvResult.Rows[i].DefaultCellStyle.SelectionBackColor = Color.FromArgb(254, 231, 235);
                            dgvResult.Rows[i].DefaultCellStyle.SelectionForeColor = Color.Black;
                            //dgvResult.Rows[i].Cells["dgvSysResult"].Value = "5级未过";
                            switch (dtResult.Rows[i]["DoctorOperate"].ToString())
                            {
                                case "0": dgvResult.Rows[i].Cells["dgvSysResult"].Value = "5级未过";
                                    break;
                                case "1": dgvResult.Rows[i].Cells["dgvSysResult"].Value = "强制执行";
                                    break;
                                case "2": dgvResult.Rows[i].Cells["dgvSysResult"].Value = "接受退单";
                                    break;
                                default: dgvResult.Rows[i].Cells["dgvSysResult"].Value = "5级未过";
                                    break;
                            }
                            break;
                    }
                }
                dgvResult.Columns["RowNum"].HeaderText = Convert.ToString(dgvResult.Rows.Count);

                dtResult.Dispose();

   //             MessageBox.Show(dgvResult.Rows[0].Cells["IsPass"].Value.ToString());

                if (PREID != "")
                {
                    for (int i = 0; i < dgvResult.Rows.Count; i++)
                    {
                        if (dgvResult.Rows[i].Cells["PrescriptionID"].Value.ToString() == PREID)
                        {
                            dgvResult.Rows[i].Selected = true;

                            information1.SetInformation(PREID, dgvResult.Rows[i].Cells["IsPass"].Value.ToString(), employeeID);
                            string str = dgvResult.Rows[i].Cells["PatientCode"].Value.ToString();
                            med1.AddMed(str);

                            break;
                        }
                    }
                }
                else
                {
                    if (dgvResult.Rows.Count > 0)
                    {
                        dgvResult.Rows[0].Selected = true;
                        PREID = dgvResult.Rows[0].Cells["PrescriptionID"].Value.ToString();
                        information1.SetInformation(PREID, dgvResult.Rows[0].Cells["IsPass"].Value.ToString(), employeeID);
                        string str = dgvResult.Rows[0].Cells["PatientCode"].Value.ToString();
                        med1.AddMed(str);
                    }
                }
                              

                TableSelAll();
                //dgvResult.SelectionChanged += new EventHandler(dgvResult_SelectionChang);
                //dgvResult.Select();
            }
            catch (System.Exception ex)
            {
                Mes.ShowDialog("提示", ex.Message);
            }
        }

        /// <summary>
        /// 实时统计长期临时的数量
        /// </summary>
        public void GetCount()
        {
            try
            {
                string sqlLong = SQLStr.SelLongCount(DT.Text);
                DataSet dsLong = dbHelp.GetPIVAsDB(sqlLong);

                string sqlLing = SQLStr.SelLingCount(DT.Text);
                DataSet dsLing = dbHelp.GetPIVAsDB(sqlLing);

                if (dsLong != null && dsLong.Tables[0].Rows.Count > 0)
                {
                    lblLongCount.Text = dsLong.Tables[0].Rows[0]["longCount"].ToString();
                }

                if (dsLing != null && dsLing.Tables[0].Rows.Count > 0)
                {
                    lblLingCount.Text = dsLing.Tables[0].Rows[0]["lingCount"].ToString();
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
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
        /// 绑定病人姓名列表
        /// </summary>
        /// <returns>true:选中病区有病人 false:选中病区没有病人</returns>
        public  bool BDNameList() 
        {
            string str = "";
            DataTable dt = new DataTable();
            //"0,3,5", per, GetWardsStr(), str1, st, GetDrugTypeStr()
            str = SQLStr.SelPatName(GetWardsStr(), DT.Text, per);
            dt = dbHelp.GetPIVAsDB(str).Tables[0];
            if (dt.Rows.Count <= 0)
            {
                NameTableLeft = null;
                dataGridView1.DataSource = null;
                return false;
            }
            NameTableLeft = dt;
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[0].Width = 90;
            return true;
        }
        /// <summary>
        /// 通过datatable绑定病人姓名
        /// </summary>
        /// <param name="dt"></param>
       private void BDNameList(DataTable dt)
        {
            if (dt != null)
            {
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[0].Width = 90;
            }
            else
            {
                dataGridView1.DataSource = null; 
            }
        }
        /// <summary>
        /// 显示病姓名列表
        /// </summary>
       private void GetPatName()
       {
           if (NameTableLeft == null)
           {
               return;
           }
           else
           {
               if (txtName.Text.Trim() != "")
               {
                   DataTable dt = NameTableLeft.Copy();
                   dt.Rows.Clear();
                   DataRow[] DR = NameTableLeft.Select(" PatName like '%" + txtName.Text.Trim() + "%' or PatCode like '%" + txtName.Text.Trim() +"%' or BedNo like '%" + txtName.Text.Trim() +"%' or CaseID like '%" + txtName.Text.Trim() +"%'");
                   foreach (DataRow dr in DR)
                   {
                       dt.ImportRow(dr);
                   }
                  
                       BDNameList(dt);
                 
               }
               else
               {
                   if (NameTableLeft.Rows.Count > 0)
                   {
                       BDNameList(NameTableLeft);
                   }
               }
           }
       }
        /// <summary>
        /// 绑定指定病区病人姓名列表
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public bool BDNameList(int rowIndex)
        {
            string str = "";
            DataTable dt = new DataTable();
            //"0,3,5", per, GetWardsStr(), str1, st, GetDrugTypeStr()
            str = SQLStr.SelPatName(GetWardsStr(rowIndex), DT.Text, per);
            dt = dbHelp.GetPIVAsDB(str).Tables[0];
            if (dt.Rows.Count <= 0)
            {
                NameTableLeft = null;
                dataGridView1.DataSource = null;
                return false;
            }
            NameTableLeft = dt;
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[0].Width = 90;
            return true;
        }

        /// <summary>
        /// 根据病人编号查询处方信息
        /// </summary>
        private void SelPreByPatCode()
        {
            DataTable dt = new DataTable();
            if (dataGridView1.Rows.Count <= 0)
                return;
            string code = dataGridView1.CurrentRow.Cells["PatCode"].Value.ToString();
            string sql = SQLStr.SelPatWardRes(wardcode, DT.Text, per, code);
            dt = dbHelp.GetPIVAsDB(sql).Tables[0];

            //搜索完毕加载处方信息
            AddResult(dt);
        }

        /// <summary>
        /// 列表模式通过处方(审方通过的方法)
        /// </summary>
        private void TablePass()
        {
            string str, check;
            //int a = 0;
            bool sel = false, perback = false, sysback = false;
            for (int i = 0; i < dgvResult.Rows.Count; i++)
            {
                if (dgvResult.Rows[i].Cells["Check"].Value.ToString() == "True")
                {
                    sel = true;
                    if (dgvResult.Rows[i].Cells["Level"].ToString() != "0")
                    {
                        sysback = true;
                    }
                    if (Convert.ToInt32(dgvResult.Rows[i].Cells["Rev1"].Tag) == 3)
                    {
                        perback = true;
                        break;
                    }
                    if (sysback && perback)
                    {
                        break;
                    }
                }
            }
            if (!sel)
            {
                Mes.ShowDialog("提示", "未选中任何处方！");
                return;
            }
            if (perback)
            {
                if (Mes.ShowDialog("提示", "选中了人工退单的处方,确定要通过？") == DialogResult.Cancel)
                    return;
            }

            string spass = "SHTG";
            //if (sysback)
            //{
            if (ShowConfirm == "0" && perback == false)
            {
                for (int i = 0; i < dgvResult.Rows.Count; i++)
                {
                    check = dgvResult.Rows[i].Cells["Check"].Value.ToString();
                    if (check == "False")
                        continue;
                    if (Convert.ToInt32(dgvResult.Rows[i].Cells["Rev1"].Tag) == 2)
                    {
                        dgvResult.Rows[i].Cells["Check"].Value = false;
                        continue;
                    }
                    else if (Convert.ToInt32(dgvResult.Rows[i].Cells["Rev1"].Tag) == 3)
                    {
                        str = "EXEC bl_checkconfirm '" + dgvResult.Rows[i].Cells["PrescriptionID"].Value.ToString()
                        + "','" + employeeID + "',''";
                        dbHelp.SetPIVAsDB(str);
                        preCharge.PivasRevPreTrue(dgvResult.Rows[i].Cells["GroupNo"].Value.ToString(), employeeID, out spass);
                        continue;
                    }
                    str = "EXEC bl_checkconfirm '" + dgvResult.Rows[i].Cells["PrescriptionID"].Value.ToString()
                        + "','" + employeeID + "',''";
                    dbHelp.SetPIVAsDB(str);
                    try
                    {
                        string  set = dbHelp.GetPivasAllSet("审方-通过按钮-设置").Trim();
                        if (set == "1")
                        {
                        preCharge.PivasRevPreTrue(dgvResult.Rows[i].Cells["GroupNo"].Value.ToString(), employeeID, out spass);
                        }
                        else if (set == "0")
                        {
                        }
                        else
                        {
                            MessageBox.Show("PivasAllSet 审方-通过按钮-设置 值不正确 ！！！");
                        }
                    }
                    catch
                    { }
                }
                PREID = "";
                if (ckFlag == 0)//勾选操作
                {
                    GetSelPrescriptions();
                    //审方结束后的刷新操作
                    if (dgvResult.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        getWards();
                    }                   
                }
                else
                {
                    GetSelPrescriptions(ckID);
                    //审方结束后的刷新操作
                    if (dgvResult.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        getWards();
                    }
                }
                
                return;
            }
            CPConfirm CP = new CPConfirm("通过");
            if (CP.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < dgvResult.Rows.Count; i++)
                {
                    check = dgvResult.Rows[i].Cells["Check"].Value.ToString();
                    if (check == "False")
                        continue;
                    if (Convert.ToInt32(dgvResult.Rows[i].Cells["Rev1"].Tag) == 2)
                    {
                        dgvResult.Rows[i].Cells["Check"].Value = false;
                        continue;
                    }
                    else if (Convert.ToInt32(dgvResult.Rows[i].Cells["Rev1"].Tag) == 3)
                    {
                        str = "EXEC bl_checkconfirm '" + dgvResult.Rows[i].Cells["PrescriptionID"].Value.ToString()
                        + "','" + CP.eid + "','" + CP.DoctorExplain + "'";
                        dbHelp.SetPIVAsDB(str);

                        preCharge.PivasRevPreTrue(dgvResult.Rows[i].Cells["GroupNo"].Value.ToString(), CP.eid, out spass);
                        continue;
                    }

                    str = "EXEC bl_checkconfirm '" + dgvResult.Rows[i].Cells["PrescriptionID"].Value.ToString()
                        + "','" + CP.eid + "','" + CP.DoctorExplain + "'";
                    dbHelp.SetPIVAsDB(str);
                    preCharge.PivasRevPreTrue(dgvResult.Rows[i].Cells["GroupNo"].Value.ToString(), CP.eid, out spass);
                }
                PREID = "";

                if (ckFlag == 0)//勾选操作
                {
                    GetSelPrescriptions();
                    //审方结束后的刷新操作
                    if (dgvResult.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        getWards();
                    }

                }
                else
                {
                    GetSelPrescriptions(ckID);
                    //审方结束后的刷新操作
                    if (dgvResult.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        getWards();
                    }
                }

                //审方结束后的刷新操作
                //if (dgvResult.Rows.Count > 0)
                //{
                //    GetSelPrescriptions();
                //}
                //else
                //{
                //    getWards();
                //    GetSelPrescriptions();
                //}
            }
        }

        /// <summary>
        /// 设置系统审方的公共方法
        /// </summary>
        private void SYScheck(Label lbl)
        {
            foreach (Control c in pnlSYS.Controls)
            {
                Label lblSysInfo = (Label)c;
                if (lbl.Name != lblSysInfo.Name)//未勾选
                {
                    lblSysInfo.ForeColor = Color.Black;
                    lblSysInfo.Font = new Font("宋体", 9, FontStyle.Regular);
                }
                else//勾选
                {
                    lbl.ForeColor = Seled;
                    lbl.Font = new Font("宋体", 9, FontStyle.Underline);
                }
            }

            if (lbl.Name == "lblSysAll")//系统全部
            {
                sys = "0,3,5";
            }
            else if (lbl.Name == "lblSysPass")//系统通过
            {
                sys = "0";
            }
            else if (lbl.Name == "lblSysUnPass")//系统未过
            {
                sys = "3,5";
            }
            else
            {
                sys = "0,3,5";
            }
        }

        /// <summary>
        /// 初始化系统审核部分样式(人工审方控制)
        /// </summary>
        /// <param name="flag">1,有效  0 ,无效</param>
        private void IniSysPnl(int flag)
        {
            sys = "0,3,5";


            foreach (Control c in pnlSYS.Controls)
            {
                Label lblSysInfo = (Label)c;
                if (lblSysInfo.Name == "lblSysAll")//系统全部
                {
                    lblSysInfo.ForeColor = Seled;
                    lblSysInfo.Font = new Font("宋体", 9, FontStyle.Underline);
                    if (flag == 0)
                    {
                        lblSysInfo.Click -= new EventHandler(this.lblSysAll_Click);
                    }
                    else
                    {
                        lblSysInfo.Click += new EventHandler(this.lblSysAll_Click);
                    }

                }
                else if (lblSysInfo.Name == "lblSysPass")//系统通过
                {
                    lblSysInfo.Font = new Font("宋体", 9, FontStyle.Regular);

                    if (flag == 0)
                    {
                        lblSysInfo.ForeColor = Color.Gray;
                        lblSysInfo.Click -= new EventHandler(this.lblSysAll_Click);
                    }
                    else
                    {
                        lblSysInfo.ForeColor = unSel;
                        lblSysInfo.Click += new EventHandler(this.lblSysAll_Click);
                    }
                }
                else if (lblSysInfo.Name == "lblSysUnPass")//系统未过
                {
                    lblSysInfo.Font = new Font("宋体", 9, FontStyle.Regular);
                    if (flag == 0)
                    {
                        lblSysInfo.ForeColor = Color.Gray;
                        lblSysInfo.Click -= new EventHandler(this.lblSysAll_Click);
                    }
                    else
                    {
                        lblSysInfo.ForeColor = unSel;
                        lblSysInfo.Click += new EventHandler(this.lblSysAll_Click);
                    }
                }
            }

        }

        /// <summary>
        /// 设置人工审方的公共方法
        /// </summary>
        /// <param name="lbl"></param>
        private void PERcheck(Label lbl,int sflag)
        {
            foreach (Control c in pnlPER.Controls)
            {
                Label lblPerInfo = (Label)c;
                if (lbl.Name != lblPerInfo.Name)//未勾选
                {
                    lblPerInfo.ForeColor = unSel;
                    lblPerInfo.Font = new Font("宋体", 9, FontStyle.Regular);
                }
                else//勾选
                {
                    lbl.ForeColor = Seled;
                    lbl.Font = new Font("宋体", 9, FontStyle.Underline);
                }
            }

            if (lbl.Name == "lblPerWeiAndTui")//人工未审/退单
            {
                per = "1,3";
                toolTip1.SetToolTip(lblUnchecks, "当前日期以前所有的人工未审/退单的处方数（含当天）"); 
                if (sflag == 1)
                {
                    IniSysPnl(1);
                  
                    SYSFlag = 0;
                }
            }
            else if (lbl.Name == "lblPerWei")//人工未审
            {
                per = "1";
                toolTip1.SetToolTip(lblUnchecks, "当前日期以前所有的人工未审的处方数（含当天）"); 
                if (sflag == 1)
                {                  
                    IniSysPnl(1);
                    SYSFlag = 0;
                }
            }
            else if (lbl.Name == "lblPerYi")//人工已审
            {
                SYSFlag = 1;
                toolTip1.SetToolTip(lblUnchecks, "当前日期人工已审的处方数"); 
                per = "2,3";
                IniSysPnl(0);
            }
            else if (lbl.Name == "lblPerTui")//人工退单
            {
                SYSFlag = 1;
                toolTip1.SetToolTip(lblUnchecks, "当前日期人工退单的处方数"); 
                per = "3";
                IniSysPnl(0);
            }
        }
        /// <summary>
        /// 长期临时条件设置公共方法（罗璨0701）
        /// </summary>
        private void setST()
        {
            if ((chkBoxStLong.Checked == false && chkBoxSTShort.Checked == true)||PreType.SelectedIndex==2)//临时
            {
                st = "2";
            }
            else if ((chkBoxStLong.Checked == true && chkBoxSTShort.Checked == false)||PreType.SelectedIndex==1)//长期
            {
                st = "1";
            }
            else if ((chkBoxStLong.Checked == true && chkBoxSTShort.Checked == true)||PreType.SelectedIndex==0)//全部
            {
                st = "0";
            }
            else
            {
                st = "3";
            }

            label1.Text = DaoJiShi;

            if (ckFlag == 0)
            {
                GetSelPrescriptions();
            }
            else
            {
                GetSelPrescriptions(ckID);
            }

        }

        /// <summary>
        /// 获取选中病区的查询sql 
        /// </summary>
        /// <returns></returns>
        private string GetWardsStr()
        {
            string SelWardsStr = "";
            for (int j = 0; j < dgvWards.Rows.Count; j++)
            {
                if (dgvWards.Rows[j].Cells[0].EditedFormattedValue.ToString().Equals("True"))
                {
                    SelWardsStr += " '" + dgvWards.Rows[j].Cells[1].Value.ToString() + "',";
                }
            }

            if (SelWardsStr.Length > 0)
            {
                SelWardsStr = SelWardsStr.Substring(0, SelWardsStr.Length - 1);
            }

            
            return SelWardsStr;

        }

        /// <summary>
        /// 获取单个病区的查询sql 
        /// </summary>
        /// <param name="RowIndex">选中行的行号</param>
        /// <returns></returns>
        private string GetWardsStr(int RowIndex)
        {
            string SelOneWardStr = "";
            SelOneWardStr = " '" + dgvWards.Rows[RowIndex].Cells[1].Value.ToString() + "'";
            return SelOneWardStr;
        }
        /// <summary>
        /// 获取普抗化营药品的查询条件sql
        /// </summary>
        /// <returns></returns>
        private string GetDrugTypeStr()
        {
            string str = "0,";
            if (ckBoxPu.Checked == true)
            {
                str += "'1',";
            }
            if (ckBoxKang.Checked == true)
            {
                str += "'2',";
            }
            if (ckBoxHua.Checked == true)
            {
                str += "'3',";
            }
            if (ckBoxYin.Checked == true)
            {
                str += "'4',";
            }
            if (ckBoxZhong.Checked == true)
            {
                str += "'5',";
            }
            if (str.Length > 0)
            {
                str = str.Substring(0, str.Length - 1);
            }
            return str;
        }

        /// <summary>
        /// 选中全部处方的方法
        /// </summary>
        public void TableSelAll()
        {
            if (SelectAll == "1" && (per == "1,3" || per == "1"))
            {
                checkBox1.Checked = true;
                for (int i = 0; i < dgvResult.Rows.Count; i++)
                {
                    dgvResult.Rows[i].Cells["Check"].Value = checkBox1.Checked;
                }
            }
            else
                checkBox1.Checked = false;
        }

        /// <summary>
        /// 根据病人姓名床号住院号查询处方信息
        /// </summary>
        private void SelPreByNameOrBedOrCaseID()
        {
            try
            {
                dgvResult.DataSource = null;
                if (NameTable == null)
                {
                    return;
                }
                DataTable dt = NameTable.Copy();
                dt.Rows.Clear();
                DataRow[] DR = NameTable.Select(" PatName like '%" + txtSearch.Text.Trim() + "%' or BedNo like '%" + txtSearch.Text.Trim() + "%' or CaseID like '%" + txtSearch.Text.Trim() + "%' ");
                foreach (DataRow dr in DR)
                {
                    dt.ImportRow(dr);
                }
                AddResult(dt);
                //dgvResult.DataSource = dt;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        #endregion

        #region 功能性事件
        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckPre_Load(object sender, EventArgs e) { }

        /// <summary>
        /// 全选按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckBoxSelAll_Click(object sender, EventArgs e)
        {
            string aaa = ckBoxSelAll.CheckState.ToString();
            if (ckBoxSelAll.CheckState == CheckState.Unchecked)//已经全不选，改成全选
            {
                for (int j = 0; j < dgvWards.Rows.Count; j++)
                {
                    if (dgvWards.Rows[j].Cells[0].EditedFormattedValue.ToString().Equals("True"))
                    {
                        dgvWards.Rows[j].Cells[0].Value = "False";
                    }
                }
                //ckBoxSelAll.CheckState = CheckState.Checked;
            }
            else if(ckBoxSelAll.CheckState == CheckState.Indeterminate)
            {
                for (int j = 0; j < dgvWards.Rows.Count; j++)
                {
                    if (dgvWards.Rows[j].Cells[0].EditedFormattedValue.ToString().Equals("True"))
                    {
                        dgvWards.Rows[j].Cells[0].Value = "False";
                    }
                }
                ckBoxSelAll.CheckState = CheckState.Unchecked;
            }
            else if (ckBoxSelAll.CheckState == CheckState.Checked)
            {
                for (int j = 0; j < dgvWards.Rows.Count; j++)
                {
                    if (dgvWards.Rows[j].Cells[0].EditedFormattedValue.ToString().Equals("False"))
                    {
                        dgvWards.Rows[j].Cells[0].Value = "True";
                    }
                }
                //ckBoxSelAll.CheckState = CheckState.Unchecked;
            }

            //全选完毕刷新处方
            GetSelPrescriptions();
            //绑定病人姓名列表
            BDNameList();
        }

        /// <summary>
        /// 病区组下拉框选取之后关闭事件（根据病区组筛选病区）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmBoxWardArea_SelectionChangeCommitted(object sender, EventArgs e)
        {
            WardArea = cmBoxWardArea.Text;
            if (WardArea == "全部病区组")
            {
                WardArea = "";
            }
            getWards();
        }

        /// <summary>
        /// 病区查询框查询条件改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWard_TextChanged(object sender, EventArgs e)
        {
            if (txtWard.Text != "病区名/简拼" && txtWard.Text.Trim()!="")
            {
                SelWardsByNameOrCode();
            }

        }

        /// <summary>
        /// 病区搜索框按回车事件（查询病区）
        /// </summary> 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWard_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13 && txtWard.Text == "")
                {
                    getWards();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 病人列表单击事件（搜索指定病人的处方信息）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            SelPreByPatCode();
        }

        /// <summary>
        /// 系统审方结果下拉框选择项下拉框关闭事件（按条件筛选系统审方结果）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbSys_SelectionChangeCommitted(object sender, EventArgs e)//按条件筛选系统审方结果
        {
            label1.Text = DaoJiShi;//重置记数
            try
            {
                switch (cbbSys.SelectedIndex)
                {
                    case 0:
                        sys = "0,3,5";
                        break;
                    case 1:
                        sys = "0";
                        break;
                    case 2:
                        sys = "3,5";
                        break;
                }

                //string str = "";
                PREID = "";
                if (Mode == "0")
                {
                    TIndex = 0;
                    GetSelPrescriptions();
                }
                else
                {
                    //RIndex = 0;
                    //str = SQLStr.SelDetailSys(sys, per, GetWardsStr(), DT.Text, st);
                    //DataSet ds = DB.GetPIVAsDB(str);
                    //if (ds != null && ds.Tables[0].Rows.Count > 0)
                    //{
                    //    DTable = ds.Tables[0];
                    //}

                    //AddPatient(DTable);
                    //DTable = null;
                }
            }
            catch (System.Exception ex)
            {
                Mes.ShowDialog("提示", ex.Message);
            }
        }

        /// <summary>
        /// 系统是否通过label单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblSysAll_Click(object sender, EventArgs e)
        {
            if (sender is Label)
            {
                Label lbl = (Label)sender;
                SYScheck(lbl);
            }
            if (ckFlag == 0)
            {
                GetSelPrescriptions();
            }
            else
            {
                GetSelPrescriptions(ckID);
            }
        }

        /// <summary>
        /// 人工审方结果下拉框选择项下拉框关闭事件（按条件筛选人工审方结果）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbPer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            label1.Text = DaoJiShi;
            cbbSys.SelectedIndex = 0;
            try
            {
                //string str;
                switch (cbbPer.SelectedIndex)
                {
                    case 0: //人工未审，退单
                        per = "1,3";
                        RIndex = 0;
                        cbbSys.Enabled = true;
                        break;
                    case 1://未审
                        per = "1";
                        RIndex = 0;
                        cbbSys.Enabled = true;
                        break;
                    case 2://已审
                        per = "2,3";
                        RIndex = 0;
                        cbbSys.Enabled = false;
                        break;
                    case 3://退单
                        per = "3";
                        RIndex = 0;
                        cbbSys.Enabled = false;
                        break;
                }
                PREID = "";
                if (Mode == "0")
                {
                    TIndex = 0;
                    GC.Collect();
                    sys = "0,3,5";
                    getWards();
                    GetSelPrescriptions();
                }
                else
                {

                }

            }
            catch (System.Exception ex)
            {
                Mes.ShowDialog("提示", ex.Message);
            }
        }

        /// <summary>
        /// 人工审核label单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblPerWei_Click(object sender, EventArgs e)
        {
            if (sender is Label)
            {
                Label lbl = (Label)sender;
                PERcheck(lbl,SYSFlag);

                getWards();
                GetSelPrescriptions();
            }
        }

        /// <summary>
        /// 审方确认通过按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPass_Click(object sender, EventArgs e)
        {
            label1.Text = DaoJiShi;
            try
            {
                TablePass();
                GetCount();//刷新数量
                getPrescriptionProperty();
                checkBox1.Checked = false;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
        /// <summary>
        /// 筛选是否关注
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel2_Click(object sender, EventArgs e)
        {
            label1.Text = DaoJiShi;
            for (int i = 0; i < dgvResult.Rows.Count; i++)
            {
                if (Convert.ToInt32(dgvResult.Rows[i].Cells["Care"].Tag) == 0)
                    dgvResult.Rows[i].Visible = false;
            }

            pnlHongqi.Visible = false;
            pnlbaiqi.Visible = true;
        }

        /// <summary>
        /// 设置按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSet_Click(object sender, EventArgs e)
        {
            label1.Text = DaoJiShi;
            try
            {
                if (GetPivasLimit.Instance.Limit(employeeID, "02004"))
                {
                    frmSet f = new frmSet(employeeID);
                    f.Set(Empty, Open, Complete, Mode, ShowConfirm, SelectAll, NameList, RightColor, Level3color, Level5color, SelectedColor, Confirmation);

                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        Empty = f.E;
                        Open = f.O;
                        Complete = f.C;
                        Mode = f.M;
                        ShowConfirm = f.s;
                        SelectAll = f.all;
                        NameList = f.Name;
                        Confirmation = f.Confirmation;

                        //重新加载用户配置
                        SetUserPeizhi();

                        //设置完毕返回刷新病区
                        getWards();

                        //刷新处方
                        GetSelPrescriptions();

                        if (Mode == "0")
                        {
                            pnlHongqi.Visible = true;
                            pnlDetail.Visible = false;
                            pnlTable.Visible = true;
                            txtSearch.Text = "患者姓名/住院号/床号";
                            btnPass.Visible = true;
                            checkBox1.Visible = true;
                        }
                        else if (Mode == "1")
                        {
                            pnlHongqi.Visible = false;
                            pnlTable.Visible = false;
                            pnlDetail.Visible = true;
                            txtSearch.Text = "患者姓名/住院号/床号";
                            btnPass.Visible = false;
                            checkBox1.Visible = false;
                        }
                        if (NameList == "1")
                        {
                            label2.Visible = true;
                        }
                        else
                        {
                            label2.Visible = false;

                        }

                    }
                }
                else
                {
                    MessageBox.Show("您没有设置此画面权限，请与管理员联系 ！！！");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        ///  列表模式下  (处方列表单元格单击事件)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label1.Text = DaoJiShi;
            if (e.RowIndex > -1)
            {
                TIndex = e.RowIndex;
                try
                {
                    if (dgvResult.RowCount > 0)
                    {
                        if (dgvResult.CurrentCell.ColumnIndex == 1)
                        {
                            if (Convert.ToInt32(dgvResult.CurrentCell.Tag) == 1)
                            {
                                dgvResult.CurrentRow.Cells["Care"].Tag = 0;
                                dgvResult.CurrentRow.Cells["Care"].Value = "";
                                dbHelp.SetPIVAsDB("Update Prescription Set Attention = 0 Where PrescriptionID = " + dgvResult.CurrentRow.Cells["PrescriptionID"].Value.ToString());
                            }
                            else if (Convert.ToInt32(dgvResult.CurrentCell.Tag) == 0)
                            {
                                dgvResult.CurrentRow.Cells["Care"].Tag = 1;
                                dgvResult.CurrentRow.Cells["Care"].Value = Care1.ToString();
                                dbHelp.SetPIVAsDB("Update Prescription Set Attention = 1 Where PrescriptionID = " + dgvResult.CurrentRow.Cells["PrescriptionID"].Value.ToString());
                            }
                        }

                        PREID = dgvResult.Rows[dgvResult.CurrentRow.Index].Cells["PrescriptionID"].Value.ToString();
                        information1.SetInformation(PREID, dgvResult.Rows[dgvResult.CurrentRow.Index].Cells["IsPass"].Value.ToString(), employeeID);

                        string str = dgvResult.Rows[dgvResult.CurrentRow.Index].Cells["PatientCode"].Value.ToString();
                        med1.AddMed(str);

                        GC.Collect();//垃圾回收
                    }
                }
                catch (System.Exception ex)
                {
                    Mes.ShowDialog("提示", ex.Message);
                }
            }
        }

        /// <summary>
        /// 长期条件复选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkBoxStLong_CheckedChanged(object sender, EventArgs e)
        {
            //if (CheckChangQiLinShi=="0")
            //{
            //    setST();
            //}
            //else
            //{
            //    if (LongShortChanging)
            //    {
            //        return;
            //    }
            //    if (chkBoxStLong.Checked)
            //    {
            //        LongShortChanging = true;
            //        chkBoxSTShort.Checked = false;
            //        setST();
            //        LongShortChanging = false;
            //    }                
            //}
            
        }
        /// <summary>
        /// 临时条件复选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkBoxSTShort_CheckedChanged(object sender, EventArgs e)
        {
            //if (CheckChangQiLinShi=="0")
            //{
            //    setST();
            //}
            //else
            //{
            //    if (LongShortChanging)
            //    {
            //        return;
            //    }
            //    if (chkBoxSTShort.Checked)
            //    {
            //        LongShortChanging = true;
            //        chkBoxStLong.Checked = false;
            //        setST();
            //        LongShortChanging = false;
            //    }                
            //}
        }

        /// <summary>
        /// 病区单元格单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvWards_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label1.Text = DaoJiShi;
            PREID = "";
            int i = dgvWards.CurrentCell.RowIndex;//获取选中单元格的行号

            int colNum = dgvWards.CurrentCell.ColumnIndex;//获取选中单元格的列号

            if (colNum == 0)//只有点击每一行的checkbox的时候才会勾选或者不勾选
            {
                //去除背景色
                //dgvWards.CurrentRow.Selected = false;
                if (dgvWards.Rows[i].Cells[0].EditedFormattedValue.ToString().Equals("False"))
                {
                    dgvWards.Rows[i].Cells[0].Value = true;
                }
                else
                {
                    dgvWards.Rows[i].Cells[0].Value = false;
                }
                ckFlag = 0;
                int k = 0;
                for (int j = 0; j < dgvWards.Rows.Count; j++)
                {
                    if (dgvWards.Rows[j].Cells[0].EditedFormattedValue.ToString().Equals("True"))
                    {
                        k = k + 1;
                    }
                }
                if (k < dgvWards.Rows.Count && k > 0)
                {
                    ckBoxSelAll.CheckState = CheckState.Indeterminate;
                }
                else if (k == dgvWards.Rows.Count)
                {
                    ckBoxSelAll.CheckState = CheckState.Checked;
                }
                else
                {
                    ckBoxSelAll.CheckState = CheckState.Unchecked;
                }

                //查询处方信息
                GetSelPrescriptions();
                //绑定病人姓名列表
                BDNameList();
            }
            else//单击了非checkbox区域，需要单独查询单个病区的内容
            {
                ckFlag = 1;
                ckID = i;
                GetSelPrescriptions(i);
                BDNameList(i);
            }
        }

        /// <summary>
        /// 普抗化疗选择是否选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckBoxPu_CheckedChanged(object sender, EventArgs e)
        {
            setST();
        }
        /// <summary>
        /// 修改时间后触发的事件(刷新病区处方)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DT_CloseUp(object sender, EventArgs e)
        {
            label1.Text = DaoJiShi;

            getWards();
            GetSelPrescriptions();
            GetCount();
        }

        /// <summary>
        /// 时间控件定时刷新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = (Convert.ToInt32(label1.Text) - 1).ToString();
            if (label1.Text.Trim() == "0")
            {
                if (Mode == "0")
                {
                    getWards();
                    //GetSelPrescriptions();
                }
                else
                {

                }
                label1.Text = DaoJiShi;
            }
        }

        /// <summary>
        /// 历史医嘱查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnhistory_Click(object sender, EventArgs e)
        {
            try
            {
                if (GetPivasLimit.Instance.Limit(employeeID, "02005"))
                {
                    ProcessStartInfo start = new ProcessStartInfo("ScanPre.exe", employeeCode + " " + employeeID);
                    Process.Start(start);
                }
                else
                {
                    MessageBox.Show("您没有权限，请与管理员联系 ！");
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        /// <summary>
        /// 处方区域按键移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvResult_KeyUp(object sender, KeyEventArgs e)
        {
            label1.Text = DaoJiShi;
            TIndex = dgvResult.CurrentRow.Index;
            try
            {
                if (e.KeyValue == 38 || e.KeyValue == 40)
                {

                    if (dgvResult.Rows.Count <= 0)
                        return;

                    PREID = dgvResult.Rows[dgvResult.CurrentRow.Index].Cells["PrescriptionID"].Value.ToString();
                    information1.SetInformation(PREID, dgvResult.Rows[dgvResult.CurrentRow.Index].Cells["IsPass"].Value.ToString(), employeeID);

                    string str = dgvResult.Rows[dgvResult.CurrentRow.Index].Cells["PatientCode"].Value.ToString();
                    med1.AddMed(str);
                }
            }
            catch (System.Exception ex)
            {
                Mes.ShowDialog("提示", ex.Message);
            }
        }

        /// <summary>
        /// 白旗单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlbaiqi_Click(object sender, EventArgs e)
        {
            label1.Text = DaoJiShi;
            for (int i = 0; i < dgvResult.Rows.Count; i++)
            {
                dgvResult.Rows[i].Visible = true;
            }

            pnlHongqi.Visible = true;
            pnlbaiqi.Visible = false;
        }

        /// <summary>
        /// 刷新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblFlush_Click(object sender, EventArgs e)
        {
            getWards();
            //GetSelPrescriptions();
            MessageBox.Show("刷新成功!");
        }


        /// <summary>
        /// 搜索框按照住院号查询病人处方信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 查询按钮回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSearch.Text.Trim() != "" && txtSearch.Text.Trim() != "患者姓名/住院号/床号")
                {
                    SelPreByNameOrBedOrCaseID();
                }
                else
                {
                    if (ckFlag == 0)
                    {
                        GetSelPrescriptions();
                    }
                    else
                    {
                        GetSelPrescriptions(ckID);
                    }
                }
            }
        }
        #endregion 

        #region 样式事件
        /// <summary>
        /// 病区搜索框鼠标进入事件(修改样式)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 病区搜索框鼠标离开事件（修改样式）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWard_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtWard.Text.Trim() == "")
                {
                    txtWard.Text = "病区名/简拼";
                    txtWard.ForeColor = Color.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 病区搜索框鼠标双击事件（全选 修改样式）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWard_DoubleClick(object sender, EventArgs e)
        {
            txtWard.SelectAll();
        }

        /// <summary>
        /// 设置病人列表显示或者不显示的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void label2_Click(object sender, EventArgs e)
        {
            if (label2.Text == ">")
            {
                BDNameList();
                if (dataGridView1.Rows.Count<=0)
                {
                    MessageBox.Show("没有选中病区或者该病区无病人");
                    return;
                }
                label2.Text = "<";
                panel2.Location = new Point(panel2.Location.X + 126, panel2.Location.Y);
                panel2.Visible = true;
            }
            else
            {

                label2.Text = ">";
                panel2.Location = new Point(panel2.Location.X - 126, panel2.Location.Y);
                panel2.Visible = false;
            }

        }

        /// <summary>
        /// 处方全选按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_Click(object sender, EventArgs e)
        {
            label1.Text = DaoJiShi;
            if (Mode == "0")
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < dgvResult.Rows.Count; i++)
                    {
                        if (dgvResult.Rows[i].Visible == true)
                        {
                            dgvResult.Rows[i].Cells["Check"].Value = true;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dgvResult.Rows.Count; i++)
                    {
                        dgvResult.Rows[i].Cells["Check"].Value = false;
                    }
                }
            }
            else
            {
            }
        }

        /// <summary>
        /// //药品隐藏按钮事件(隐藏药品显示)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlUp_Click(object sender, EventArgs e)
        {
            med1.Visible = true;
        }

        /// <summary>
        /// 处方表格行重绘事件（去除处方表格单元格的虚框）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvResult_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts = DataGridViewPaintParts.All ^ DataGridViewPaintParts.Focus;
            dgvResult.Rows[e.RowIndex].Cells["RowNum"].Value = e.RowIndex + 1;
        }


        /// <summary>
        /// 处方表格大小属性值改变触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvResult_SizeChanged(object sender, EventArgs e)
        {
            if (dgvResult.Width > 350)
            {
                dgvResult.Columns["Rev1"].Visible = false;
                //dgvResult.Columns["GroupNo"].Width = dgvResult.Width - 580;
            }
            else
                dgvResult.Columns["Rev1"].Visible = true;
        }

        /// <summary>
        /// 病区表格行重绘事件（去除处方表格单元格的虚框）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvWards_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts = DataGridViewPaintParts.All ^ DataGridViewPaintParts.Focus;
        }

        /// <summary>
        /// 住院号行号txtbox进入事件（修改样式）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            label1.Text = DaoJiShi;
            if (txtSearch.Text == "患者姓名/住院号/床号")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// 处方查询进入事件（修改样式）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (txtName.Text == "姓名/ID/床")
            {
                txtName.Text = "";
                txtName.ForeColor = Color.Black;
            }
        }
        /// <summary>
        /// 住院号行号txtbox离开事件（修改样式）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim() == "")
            {
                txtSearch.Text = "患者姓名/住院号/床号";
                txtSearch.ForeColor = Color.Gray;
                //GetSelPrescriptions();
            }
        }

        /// <summary>
        /// 药品隐藏按钮事件（隐藏不显示药品  修改样式）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void med1_VisibleChanged(object sender, EventArgs e)
        {
            pnlUp.Visible = !med1.Visible;
        }

        /// <summary>
        /// 查询指定病人处方双击事件（修改样式）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_DoubleClick(object sender, EventArgs e)
        {
            txtSearch.SelectAll();
        }

        /// <summary>
        /// 显示病人的区域离开事件（搜索框重置）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel2_Leave(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                txtName.Text = "姓名/ID/床";
                txtName.ForeColor = Color.Gray;
            }
        }

        /// <summary>
        /// 左边病人查询释放按键之后，右边处方显示区域获取焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)(Keys.Up) || e.KeyValue == (int)(Keys.Down))
            {
                dataGridView1.Focus();
            }
        }
        #endregion

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int ShowScrollBar(IntPtr hWnd, int bar, int show);

        private class SubWindow : NativeWindow
        {
            private int m_Horz = 0;
            private int m_Show = 0;


            public SubWindow(int p_Horz, int p_Show)
            {
                m_Horz = p_Horz;
                m_Show = p_Show;
            }
            protected override void WndProc(ref Message m_Msg)
            {
                ShowScrollBar(m_Msg.HWnd, m_Horz, m_Show);
                base.WndProc(ref m_Msg);
            }
        }

        public static void SetScrollBar(IntPtr p_ControlHandle, int p_Horz, int p_Show)
        {
            SubWindow _SubWindow = new SubWindow(p_Horz, p_Show);
            _SubWindow.AssignHandle(p_ControlHandle);
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            button2.Visible = true;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.Visible = false;
        }

        /// <summary>
        /// 医嘱同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (GetPivasLimit.Instance.Limit(employeeID, "02006"))
                {
                    StringBuilder SB = new StringBuilder();
                    SB.Append(" select * from SynLog where ");
                    SB.Append(" SynCode =(select top 1  SynCode from  [dbo].[SynSet] where SYnName='医嘱')   ");
                    SB.Append(" and  EndTime  is  null   ");
                    DataSet ds = dbHelp.GetPIVAsDB(SB.ToString());
                    if (ds == null)
                    {
                        MessageBox.Show("未取得同步数据信息 ！！！");  
                    }
                    else if (ds.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show("【医嘱】正在同步，若要停止之前操作，请到同步画面！");
                    }
                    else
                    {
                        MessageBox.Show("开始同步 ！ 您可以到同步画面去查看同步进度。");
                        dbHelp.GetPIVAsDB(insertNewSynLog());
                    }
                }
                else
                {
                    MessageBox.Show("您没有权限，请与管理员联系 ！");
                }
            }
            catch(Exception ex)
            {
                ShowMsgHelper.ShowError("医嘱同步出错"+ ex.Message);
            }
        }
        private string insertNewSynLog()
        {
            try
            {
                DataSet ds2 = dbHelp.GetPIVAsDB(" select top 1 SynCode from SynSet where SYnName='医嘱'  ");
                string SynCode = ds2.Tables[0].Rows[0]["SynCode"].ToString();
                return string.Format("exec bl_InsertNewSynLog '{0}','{1}'", SynCode, employeeID).ToString();
            }
            catch 
            {
                MessageBox.Show("同步信息插入出错！");
                return "";
            }
        }

        private void SetRemarkWidth()
        {
            try
            {
                //医嘱是长期临时筛选是否显示
                DataTable dt = new DataTable();
                dt = dbHelp.GetPivasAllSet("审方-(长期/临时)筛选", "");
                if (dt == null || dt.Rows.Count == 0 || dt.Rows[0]["Value"].ToString() == "" || !IsNum(dt.Rows[0]["Value"].ToString()) || dt.Rows[0]["Value"].ToString().Trim() == "0")
                {
                    ChangQiLinShi = "0";
                }
                else
                {
                    ChangQiLinShi = dt.Rows[0]["Value"].ToString();
                }
                //ChangQiLinShi = DB.GetPivasAllSet("审方-(长期/临时)筛选");
                if (ChangQiLinShi == "1")
                {
                    if (dbHelp.GetPivasAllSet("审方-条件选择-显示模式") == "1")
                    {
                        panel8.Visible = true;
                        panel10.Visible = false;
                    }
                    else
                    {
                        panel8.Visible = false;
                        panel10.Visible = true;
                        CheckChangQiLinShi = dt.Rows[0]["Value2"].ToString();
                        switch (CheckChangQiLinShi)
                        {
                            case "0":
                                chkBoxStLong.Checked = true;
                                chkBoxSTShort.Checked = true;
                                st = "0";
                                break;
                            case "1":
                                chkBoxStLong.Checked = true;
                                chkBoxSTShort.Checked = false;
                                st = "1";
                                break;
                            case "2":
                                chkBoxStLong.Checked = false;
                                chkBoxSTShort.Checked = true;
                                st = "2";
                                break;
                            case "3":
                                chkBoxStLong.Checked = true;
                                chkBoxSTShort.Checked = false;
                                st = "1";
                                break;
                            case "4":
                                chkBoxStLong.Checked = false;
                                chkBoxSTShort.Checked = true;
                                st = "2";
                                break;
                            default: chkBoxStLong.Checked = true;
                                chkBoxSTShort.Checked = false;
                                st = "1";
                                break;

                        }
                    }
                }
              
                else
                {
                    CheckChangQiLinShi = "0";
                    panel8.Visible = false; 
                    chkBoxStLong.Visible = false;
                    chkBoxSTShort.Visible = false;
                    lblLongCount.Visible = false;
                    lblLingCount.Visible = false;
                }

                //人工审方结果筛选（是否显示）
                if (dbHelp.GetPivasAllSet("审方-人工审方结果筛选") == "1")
                {
                    if (dbHelp.GetPivasAllSet("审方-条件选择-显示模式") == "1")
                    {
                        panel7.Visible = true;
                        pnlPER.Visible = false;
                    }
                    else
                    {
                        panel7.Visible = false;
                        pnlPER.Visible = true;
                    }

                }
                else
                {
                    pnlPER.Visible = false;
                    label4.Visible = false;
                    CPType.Visible = false;

                }

                //系统审方结果筛选（是否显示）
                if (dbHelp.GetPivasAllSet("审方-系统审方结果筛选") == "1")
                {

                    if (dbHelp.GetPivasAllSet("审方-条件选择-显示模式") == "1")
                    {
                        panel9.Visible = true;
                        pnlSYS.Visible = false;
                    }
                    else
                    {
                        panel9.Visible = false;
                        pnlSYS.Visible = true;
                    }

                }
                else
                {
                    pnlSYS.Visible = false;
                    label3.Visible = false;
                    SysType.Visible = false;

                }

                //医嘱同步（是否显示）
                if (dbHelp.GetPivasAllSet("审方-医嘱同步") == "1")
                {
                    button3.Visible = true;
                }
                else
                {
                    button3.Visible = false;
                }


                //历史医嘱（是否显示）
                if (dbHelp.GetPivasAllSet("审方-历史医嘱") == "1")
                {
                    btnhistory.Visible = true;
                }
                else
                {
                    btnhistory.Visible = false;
                }

                //界面设置（是否显示）
                if (dbHelp.GetPivasAllSet("审方-界面设置") == "1")
                {
                    button1.Visible = true;
                }
                else
                {
                    button1.Visible = false;
                }

                //  普抗化营  筛选（是否显示）
                if (dbHelp.GetPivasAllSet("审方-普抗化营筛选") == "1")
                {
                    ckBoxPu.Visible = true;
                    ckBoxKang.Visible = true;
                    ckBoxHua.Visible = true;
                    ckBoxYin.Visible = true;
                }
                else
                {
                    ckBoxPu.Visible = false;
                    ckBoxKang.Visible = false;
                    ckBoxHua.Visible = false;
                    ckBoxYin.Visible = false;
                }

                //界面设置（是否显示）
                if (dbHelp.GetPivasAllSet("审方-全选-勾选框") == "1")
                {
                    checkBox1.Visible = true;
                }
                else
                {
                    checkBox1.Visible = false;
                }

                //界面设置（是否显示）
                if (dbHelp.GetPivasAllSet("审方-处方模糊查询-输入框") == "1")
                {
                    txtSearch.Visible = true;
                }
                else
                {
                    txtSearch.Visible = false;
                }



                if (dbHelp.GetPivasAllSet("审方-删除处方") == "0")
                {
                    information1.linkLabel1.Visible = false;
                }
                else
                {
                    information1.linkLabel1.Visible = true;
                }

                if (dbHelp.GetPivasAllSet("审方-退单按钮-设置") == "0")
                {
                    information1.btnTuiDan.Visible = true;
                }
                else
                {
                    information1.btnTuiDan.Visible = false;
                }

                if (dbHelp.GetPivasAllSetValue2("审方-退单按钮-设置")!= "退单")
                {
                    information1.btnTuiDan.Text = dbHelp.GetPivasAllSetValue2("审方-退单按钮-设置");
                }

                string svalue = "";
                svalue = dbHelp.GetPivasAllSet("审方-药品名称-画面显示");
                if (svalue == "" || !IsNum(svalue) || svalue.Trim() == "0")
                {
                    information1.dgvDrugs.Columns["ColDrugName"].Visible = false;
                }
                else
                {
                    information1.dgvDrugs.Columns["ColDrugName"].Width = Convert.ToInt32(svalue);
                }

                svalue = dbHelp.GetPivasAllSet("审方-药品规格-画面显示");
                if (svalue == "" || !IsNum(svalue) || svalue.Trim() == "0")
                {
                    information1.dgvDrugs.Columns["ColDrugSize"].Visible = false;
                }
                else
                {
                    information1.dgvDrugs.Columns["ColDrugSize"].Width = Convert.ToInt32(svalue);
                }

                svalue = dbHelp.GetPivasAllSet("审方-药品用量-画面显示");
                if (svalue == "" || !IsNum(svalue) || svalue.Trim() == "0")
                {
                    information1.dgvDrugs.Columns["ColYongLiang"].Visible = false;
                }
                else
                {
                    information1.dgvDrugs.Columns["ColYongLiang"].Width = Convert.ToInt32(svalue);
                }

                svalue = dbHelp.GetPivasAllSet("审方-皮试信息-画面显示");
                if (svalue == "" || !IsNum(svalue) || svalue.Trim() == "0")
                {
                    information1.dgvDrugs.Columns["ColPiShi"].Visible = false;
                }
                else
                {
                    information1.dgvDrugs.Columns["ColPiShi"].Width = Convert.ToInt32(svalue);
                }

                svalue = dbHelp.GetPivasAllSet("审方-药品数量-画面显示");
                if (svalue == "" || !IsNum(svalue) || svalue.Trim() == "0")
                {
                    information1.dgvDrugs.Columns["ColCount"].Visible = false;
                }
                else
                {
                    information1.dgvDrugs.Columns["ColCount"].Width = Convert.ToInt32(svalue);
                }


                dt = dbHelp.GetPivasAllSet("审方-Remark1-画面显示", "");
                if (dt == null || dt.Rows.Count == 0 || dt.Rows[0]["Value"].ToString() == "" || !IsNum(dt.Rows[0]["Value"].ToString()) || dt.Rows[0]["Value"].ToString().Trim() == "0")
                {
                    dgvResult.Columns["Remark1"].Visible = false;
                }
                else
                {
                    dgvResult.Columns["Remark1"].Width = Convert.ToInt32(dt.Rows[0]["Value"].ToString());
                    dgvResult.Columns["Remark1"].HeaderText = dt.Rows[0]["Value2"].ToString();
                }

                dt = dbHelp.GetPivasAllSet("审方-Remark2-画面显示", "");
                if (dt == null || dt.Rows.Count == 0 || dt.Rows[0]["Value"].ToString() == "" || !IsNum(dt.Rows[0]["Value"].ToString()) || dt.Rows[0]["Value"].ToString().Trim() == "0")
                {
                    dgvResult.Columns["Remark2"].Visible = false;
                }
                else
                {
                    dgvResult.Columns["Remark2"].Width = Convert.ToInt32(dt.Rows[0]["Value"].ToString());
                    dgvResult.Columns["Remark2"].HeaderText = dt.Rows[0]["Value2"].ToString();
                }


                dt = dbHelp.GetPivasAllSet("审方-Remark3-画面显示", "");
                if (dt == null || dt.Rows.Count == 0 || dt.Rows[0]["Value"].ToString() == "" || !IsNum(dt.Rows[0]["Value"].ToString()) || dt.Rows[0]["Value"].ToString().Trim() == "0")
                {
                    dgvResult.Columns["Remark3"].Visible = false;
                }
                else
                {
                    dgvResult.Columns["Remark3"].Width = Convert.ToInt32(dt.Rows[0]["Value"].ToString());
                    dgvResult.Columns["Remark3"].HeaderText = dt.Rows[0]["Value2"].ToString();
                }

                dt = dbHelp.GetPivasAllSet("审方-Remark4-画面显示", "");
                if (dt == null || dt.Rows.Count == 0 || dt.Rows[0]["Value"].ToString() == "" || !IsNum(dt.Rows[0]["Value"].ToString()) || dt.Rows[0]["Value"].ToString().Trim() == "0")
                {
                    dgvResult.Columns["Remark4"].Visible = false;
                }
                else
                {
                    dgvResult.Columns["Remark4"].Width = Convert.ToInt32(dt.Rows[0]["Value"].ToString());
                    dgvResult.Columns["Remark4"].HeaderText = dt.Rows[0]["Value2"].ToString();
                }

                dt = dbHelp.GetPivasAllSet("审方-Remark5-画面显示", "");
                if (dt == null || dt.Rows.Count == 0 || dt.Rows[0]["Value"].ToString() == "" || !IsNum(dt.Rows[0]["Value"].ToString()) || dt.Rows[0]["Value"].ToString().Trim() == "0")
                {
                    dgvResult.Columns["Remark5"].Visible = false;
                }
                else
                {
                    dgvResult.Columns["Remark5"].Width = Convert.ToInt32(dt.Rows[0]["Value"].ToString());
                    dgvResult.Columns["Remark5"].HeaderText = dt.Rows[0]["Value2"].ToString();
                }

                dt = dbHelp.GetPivasAllSet("审方-Remark6-画面显示", "");
                if (dt == null || dt.Rows.Count == 0 || dt.Rows[0]["Value"].ToString() == "" || !IsNum(dt.Rows[0]["Value"].ToString()) || dt.Rows[0]["Value"].ToString().Trim() == "0")
                {
                    dgvResult.Columns["Remark6"].Visible = false;
                }
                else
                {
                    dgvResult.Columns["Remark6"].Width = Convert.ToInt32(dt.Rows[0]["Value"].ToString());
                    dgvResult.Columns["Remark6"].HeaderText = dt.Rows[0]["Value2"].ToString();
                }


                dt = dbHelp.GetPivasAllSet("审方-Remark7-画面显示", "");
                if (dt == null || dt.Rows.Count == 0 || dt.Rows[0]["Value"].ToString() == "" || !IsNum(dt.Rows[0]["Value"].ToString()) || dt.Rows[0]["Value"].ToString().Trim() == "0")
                {
                    information1.dgvDrugs.Columns["Remark7"].Visible = false;
                }
                else
                {
                    information1.dgvDrugs.Columns["Remark7"].Width = Convert.ToInt32(dt.Rows[0]["Value"].ToString());
                    information1.dgvDrugs.Columns["Remark7"].HeaderText = dt.Rows[0]["Value2"].ToString();
                }

                dt = dbHelp.GetPivasAllSet("审方-Remark8-画面显示", "");
                if (dt == null || dt.Rows.Count == 0 || dt.Rows[0]["Value"].ToString() == "" || !IsNum(dt.Rows[0]["Value"].ToString()) || dt.Rows[0]["Value"].ToString().Trim() == "0")
                {
                    information1.dgvDrugs.Columns["ColRemark8"].Visible = false;
                }
                else
                {
                    information1.dgvDrugs.Columns["ColRemark8"].Width = Convert.ToInt32(dt.Rows[0]["Value"].ToString());
                    information1.dgvDrugs.Columns["ColRemark8"].HeaderText = dt.Rows[0]["Value2"].ToString();
                }


                dt = dbHelp.GetPivasAllSet("审方-Remark9-画面显示", "");
                if (dt == null || dt.Rows.Count == 0 || dt.Rows[0]["Value"].ToString() == "" || !IsNum(dt.Rows[0]["Value"].ToString()) || dt.Rows[0]["Value"].ToString().Trim() == "0")
                {
                    information1.dgvDrugs.Columns["ColRemark9"].Visible = false;
                }
                else
                {
                    information1.dgvDrugs.Columns["ColRemark9"].Width = Convert.ToInt32(dt.Rows[0]["Value"].ToString());
                    information1.dgvDrugs.Columns["ColRemark9"].HeaderText = dt.Rows[0]["Value2"].ToString();
                }


                dt = dbHelp.GetPivasAllSet("审方-Remark10-画面显示", "");
                if (dt == null || dt.Rows.Count == 0 || dt.Rows[0]["Value"].ToString() == "" || !IsNum(dt.Rows[0]["Value"].ToString()) || dt.Rows[0]["Value"].ToString().Trim() == "0")
                {
                    information1.dgvDrugs.Columns["ColRemark10"].Visible = false;
                }
                else
                {
                    information1.dgvDrugs.Columns["ColRemark10"].Width = Convert.ToInt32(dt.Rows[0]["Value"].ToString());
                    information1.dgvDrugs.Columns["ColRemark10"].HeaderText = dt.Rows[0]["Value2"].ToString();
                }


                svalue = dbHelp.GetPivasAllSet("审方-药品明细标题-画面显示");
                if (svalue == "" || !IsNum(svalue) || svalue.Trim() == "0")
                {
                    information1.dgvDrugs.ColumnHeadersVisible = false;
                }
                else
                {
                    information1.dgvDrugs.ColumnHeadersVisible = true;
                    //标题字体大小设置
                    svalue = dbHelp.GetPivasAllSetValue2("审方-药品明细标题-画面显示");
                    if (!IsNum(svalue) || svalue == "0")
                    {
                        information1.dgvDrugs.RowTemplate.DefaultCellStyle.Font = new Font("宋体", 9);
                    }
                    else
                    {
                        information1.dgvDrugs.Font = new System.Drawing.Font("宋体", Convert.ToInt32(svalue));
                    }
                    //标题高度设置
                    svalue = dbHelp.GetPivasAllSetValue3("审方-药品明细标题-画面显示");
                    if (!IsNum(svalue) || svalue == "0")
                    {
                        information1.dgvDrugs.ColumnHeadersHeight = 18;
                    }
                    else
                    {
                        information1.dgvDrugs.ColumnHeadersHeight = Convert.ToInt32(svalue);
                    }
                }
                //字体大小设置
                svalue = dbHelp.GetPivasAllSet("审方-药品明细-字体大小-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    information1.dgvDrugs.RowTemplate.DefaultCellStyle.Font = new Font("宋体", 9);
                }
                else
                {
                    string i = dbHelp.GetPivasAllSetValue3("审方-药品明细-字体大小-画面显示").ToString().Trim();//是否设置成粗体判断
                    if (i == "1")
                    {
                        information1.dgvDrugs.RowTemplate.DefaultCellStyle.Font = new Font("宋体", Convert.ToInt32(svalue), FontStyle.Bold);
                    }
                    else if (i == "2")
                    {
                        information1.dgvDrugs.RowTemplate.DefaultCellStyle.Font = new Font("宋体", Convert.ToInt32(svalue), FontStyle.Italic);
                    }
                    else if (i == "3")
                    {
                        information1.dgvDrugs.RowTemplate.DefaultCellStyle.Font = new Font("宋体", Convert.ToInt32(svalue), FontStyle.Underline);
                    }
                    else
                    {
                        information1.dgvDrugs.RowTemplate.DefaultCellStyle.Font = new Font("宋体", Convert.ToInt32(svalue), FontStyle.Regular);
                    }
                }
                //行高设置
                svalue = dbHelp.GetPivasAllSetValue2("审方-药品明细-字体大小-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    information1.dgvDrugs.RowTemplate.Height = 23;
                }
                else
                {
                    information1.dgvDrugs.RowTemplate.Height = Convert.ToInt32(svalue);
                }

                svalue = dbHelp.GetPivasAllSet("审方-列表模式处方-标题-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    dgvResult.ColumnHeadersVisible = false;
                }
                else
                {
                    dgvResult.ColumnHeadersVisible = true;
                    //标题字体大小设置
                    svalue = dbHelp.GetPivasAllSetValue2("审方-列表模式处方-标题-画面显示");
                    if (!IsNum(svalue) || svalue == "0")
                    {
                        dgvResult.RowTemplate.DefaultCellStyle.Font = new Font("宋体", 9);
                    }
                    else
                    {
                        dgvResult.Font = new System.Drawing.Font("宋体", Convert.ToInt32(svalue));
                    }
                    //标题高度设置
                    svalue = dbHelp.GetPivasAllSetValue3("审方-列表模式处方-标题-画面显示");
                    if (!IsNum(svalue) || svalue == "0")
                    {
                        dgvResult.ColumnHeadersHeight = 18;
                    }
                    else
                    {
                        dgvResult.ColumnHeadersHeight = Convert.ToInt32(svalue);
                    }
                }

                svalue = dbHelp.GetPivasAllSet("审方-列表模式处方-字体大小-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    dgvResult.RowTemplate.DefaultCellStyle.Font = new Font("宋体", 9);
                }
                else
                {
                    string i = dbHelp.GetPivasAllSetValue3("审方-列表模式处方-字体大小-画面显示").ToString().Trim();//是否设置成粗体判断
                    if (i == "1")
                    {
                        dgvResult.RowTemplate.DefaultCellStyle.Font = new Font("宋体", Convert.ToInt32(svalue), FontStyle.Bold);
                    }
                    else if (i == "2")
                    {
                        dgvResult.RowTemplate.DefaultCellStyle.Font = new Font("宋体", Convert.ToInt32(svalue), FontStyle.Italic);
                    }
                    else if (i == "3")
                    {
                        dgvResult.RowTemplate.DefaultCellStyle.Font = new Font("宋体", Convert.ToInt32(svalue), FontStyle.Underline);
                    }
                    else
                    {
                        dgvResult.RowTemplate.DefaultCellStyle.Font = new Font("宋体", Convert.ToInt32(svalue), FontStyle.Regular);
                    }
                }

                svalue = dbHelp.GetPivasAllSetValue2("审方-列表模式处方-字体大小-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    dgvResult.RowTemplate.Height = 23;
                }
                else
                {
                    dgvResult.RowTemplate.Height = Convert.ToInt32(svalue);
                }



            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void information1_SizeChanged(object sender, EventArgs e)
        {
            Application.DoEvents();
        }

        private void dgvResult_Scroll(object sender, ScrollEventArgs e)
        {
            //Application.DoEvents();
        }
 
        public bool IsNum(string s)
        {
            string pattern = "^[0-9]*$";
            Regex rx = new Regex(pattern);
            return rx.IsMatch(s);
        }

        private void chkBoxStLong_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show(chkBoxStLong.Checked.ToString());
                if (CheckChangQiLinShi == "0")
                {
                    setST();
                }
                else
                {
                    if (CheckChangQiLinShi == "1" || CheckChangQiLinShi == "2")
                    {
                        if (chkBoxStLong.Checked)
                        {

                            chkBoxSTShort.Checked = false;

                        }
                    }
                    setST();
                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkBoxSTShort_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show(chkBoxStLong.Checked.ToString());
                if (CheckChangQiLinShi == "0")
                {
                    setST();
                }
                else
                {
                    if (CheckChangQiLinShi == "1" || CheckChangQiLinShi == "2")
                    {
                        if (chkBoxSTShort.Checked)
                        {

                            chkBoxStLong.Checked = false;

                        }
                    }
                    setST();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13 && txtWard.Text == "")
                {
                    GetPatName();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 姓名筛选事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            GetPatName();

        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
            {
                txtName.Text = "姓名/ID/床";
                txtName.ForeColor = Color.Gray;
            }
        }

        private void txtName_DoubleClick(object sender, EventArgs e)
        {
            txtName.SelectAll();
        }

        private void PreType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (PreType.SelectedIndex==2)//临时
            {
                st = "2";
            }
            else if (PreType.SelectedIndex==1)//长期
            {
                st = "1";
            }
            else if (PreType.SelectedIndex==0)//全部
            {
                st = "0";
            }
         
            label1.Text = DaoJiShi;

            if (ckFlag == 0)
            {
                GetSelPrescriptions();
            }
            else
            {
                GetSelPrescriptions(ckID);
            }
        }

        private void SysType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (SysType.SelectedIndex==0)//系统全部
            {
                sys = "0,3,5";
            }
            else if (SysType.SelectedIndex==1)//系统通过
            {
                sys = "0";
            }
            else if (SysType.SelectedIndex==2)//系统未过
            {
                sys = "3,5";
            }
            else
            {
                sys = "0,3,5";
            }
            label1.Text = DaoJiShi;

            if (ckFlag == 0)
            {
                GetSelPrescriptions();
            }
            else
            {
                GetSelPrescriptions(ckID);
            }
        }

        private void dgvResult_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvResult.RowCount > 0 && e.RowIndex > -1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    dgvResult.CurrentCell = dgvResult.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    this.GroupNO = dgvResult.Rows[e.RowIndex].Cells["GroupNo"].Value.ToString();
                    CMS.Show(MousePosition);
                    dgvResult_CellClick(sender, new DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex));
                }
            }
        }

        private void CPType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (CPType.SelectedIndex==0)//人工未审/退单
            {
                per = "1,3";
                toolTip1.SetToolTip(lblUnchecks, "当前日期以前所有的人工未审/退单的处方数（含当天）");
                if (SYSFlag== 1)
                {
                    IniSysPnl(1);

                    SYSFlag = 0;
                }
            }
            else if (CPType.SelectedIndex==1)//人工未审
            {
                per = "1";              
                if (SYSFlag == 1)
                {
                    IniSysPnl(1);
                    SYSFlag = 0;
                }
            }
            else if (CPType.SelectedIndex==2)//人工已审
            {
                SYSFlag = 1;  
                per = "2,3";
                IniSysPnl(0);
            }
            else if (CPType.SelectedIndex==3)//人工退单
            {
                SYSFlag = 1;      
                per = "3";
                IniSysPnl(0);
            }


            getWards();
            GetSelPrescriptions();
        }

        private void getPrescriptionProperty()
        {
            try
            {
                string s = "EXEC bl_getPrescriptionProperty ";
                dbHelp.SetPIVAsDB(s);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region 接口实现
        void IMenuManager.menuBeforeSelect()
        {
            //初始化所有配置
            label1.Text = DaoJiShi;//时间计数重置
            sys = "0,3,5";
            per = "1";
            st = "0";
            PREID = "";
            ckID = 0;
            SetScrollBar(dgvResult.Handle, 0, 0);
            dgvResult.ClearSelection();

            cbbPer.SelectedIndex = 1;
            cbbSys.SelectedIndex = 0;
            chkBoxStLong.Checked = true;
            chkBoxSTShort.Checked = true;

            //下拉框模式张衡1106
            PreType.SelectedIndex = 0;
            SysType.SelectedIndex = 0;
            CPType.SelectedIndex = 1;

            //加载病区组下拉框
            LoadComboxWardArea();

            //获取用户配置信息(新登陆的用户需要加载默认)
            SetUserPeizhi();

            //根据条件获取所有病区
            getWards();

            //分别统计长期和临时的数量
            GetCount();
            //根据条件获取所有处方
            //GetSelPrescriptions();
            try
            {
                string CMSS = dbHelp.IniReadValuePivas("PrePlugIn", "PreAddin");
                string DllFile = ".\\PrePlugIn.dll";
                if (File.Exists(DllFile))
                {
                    if (!string.IsNullOrEmpty(CMSS))
                    {
                        assembly = Assembly.LoadFrom(DllFile);
                        t = assembly.GetType("PrePlugIn.PreAddin", true, true);
                        CMS.ShowImageMargin = false;
                        foreach (string s in CMSS.Split(','))
                        {
                            ToolStripMenuItem tsmi = new ToolStripMenuItem(s.Split('-')[0]);
                            tsmi.Click += (object send, EventArgs es) =>
                            {
                                try
                                {
                                    object obj = Activator.CreateInstance(t);
                                    MethodInfo mi = t.GetMethod(s.Split('-')[1], new Type[] { typeof(string) });
                                    mi.Invoke(obj, new[] { GroupNO });
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            };
                            CMS.Items.Add(tsmi);
                        }
                    }
                    else
                    {
                        dbHelp.IniWriteValuePivas("PrePlugIn", "PreAddin", "");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void IMenuManager.menuAfterSelect()
        {
        }
        #endregion
    }
}
