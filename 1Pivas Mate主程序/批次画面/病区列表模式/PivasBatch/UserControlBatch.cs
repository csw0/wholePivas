using System;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PivasBatchDiff;
using System.Text.RegularExpressions;
using System.IO;
using PivasBatchMX;
using PivasBatchCommon;
using PIVAsCommon.Helper;
using PIVAsCommon;
using PivasLimitDES;

namespace PivasBatch
{
    public partial class UserControlBatch : UserControl, IMenuManager
    {
        //单个病区编码
        public string ward = string.Empty;
        //病区数据集
        DataSet WardDS = new DataSet();
        //药品数据集
        DataSet Drugds = new DataSet();
        DB_Help DB = new DB_Help();
        SelectSql select = new SelectSql();
        InsertSql insert = new InsertSql();
        //病人数量，加载时候会用到
        int PatCount = 0;
        //病区数量
        int WardCount = 0;
        //病人编码
        public string Petcode = string.Empty;
        //所有病人的批次

        UpdateSql update = new UpdateSql();

        //首次进入下拉框
        bool ChangeChoes = false;
        bool ClickDgvWard = false;

        DataTable dtPatient = new DataTable();
        Patient patient;
        bool Initing;

        //设置表里的NextDay
        string time;
        
        //记录最后同步的synID
        string synID=string.Empty;
        //监控同步时间
        int SeeSynID=0;

        /// <summary>
        /// 判断最后一次是勾选还是单击  0 勾选 ；1 单击；
        /// </summary>
        int ClickOrSelect = 0;

        public string S1 = string.Empty; //瓶签为#时remark3的值
        public string SK = string.Empty;//瓶签为K时remark3的值
        public string LS1 = string.Empty; //瓶签为L#时remark3的值
        public string LSK = string.Empty; //瓶签为LK时remark3的值

        private bool CS = true; //取消发送按钮在AllSet中是否显示
        private bool BM = true; //批量修改按钮在AllSet中是否显示

        public UserControlBatch()
        {
            InitializeComponent();
            this.Panel_BatchRule.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Panel_BatchRule_OnMouseWheel);
        }

        /// <summary>
        /// 构造函数2
        /// </summary>
        /// <param name="UID">员工唯一识别码</param>
        /// <param name="AccountID">员工账号</param>
        /// <param name="DEmployeeName">员工姓名</param>
        public UserControlBatch(string uid, string Accountid, string dEmployeeName)
        {
            InitializeComponent();
            this.Panel_BatchRule.MouseWheel += new MouseEventHandler(this.Panel_BatchRule_OnMouseWheel);
            pvb.DEmployeeID = uid;
            pvb.AccountID = Accountid;
            pvb.DEmployeeName = dEmployeeName;
        }


        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="count">数量为0时 是刷新所有数据</param>
        /// <param name="IsSame">是否选择了是否改动的下拉框,false为未选择，true为选择</param>
        public void ShowWard(int count, bool isSame)
        {
            try
            {
                bool tags = true;
                if (count == 0)
                {
                    Panel_BatchRule.Controls.Clear();
                    med1.clear();
                    GC.Collect();
                    ward = "";
                    //选择未发或者已发或者全部
                    pvb.IvBatchSaved = Check_BatchSaved.SelectedIndex;
                    //选择有改动或者未改动+
                    int IVstatus = 0;
                    string strWard = "EXEC [bl_getIVRecordbyarea] '" + pvb.datetime.ToString("yyyyMMdd") + "%'," + pvb.WardIdle + "," + pvb.WardOpen + "," + IVstatus + "," + pvb.IvBatchSaved;
                    WardDS = DB.GetPIVAsDB(strWard);//获取病区数据集
                    WardCount = 0;//病区数量值为0
                    ward = WardDS.Tables[0].Rows[0][0].ToString();
                    //如果未选择是否改动的下拉框
                    tags = NewMethod(isSame, tags);
                }
                else
                {
                    int wardtal = WardCount + 1;
                    for (int i = WardCount; i < wardtal; i++)
                    {
                        Ward w = new Ward();
                        if (ward == string.Empty || WardDS.Tables[0].Rows[i]["WardCode"].ToString().Trim().Length != 0)
                        {
                            //把数据集中的病区放在病区字段里，不刷新整个界面
                            ward = WardDS.Tables[0].Rows[i]["WardCode"].ToString();
                            pvb.ward = "'" + ward + "'";
                        }
                        w.SetWard(WardDS.Tables[0].Rows[i], pvb.IvBatchSaved, this);
                        w.Name = WardDS.Tables[0].Rows[i]["WardCode"].ToString();
                        WardCount++;
                    }
                }
            }
            catch (System.Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10004:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
            finally
            {
                WardDS.Dispose();
            }
        }

        /// <summary>
        /// 显示病区数据和药品
        /// </summary>
        /// <param name="isSame">是否有改动</param>
        /// <param name="tags"></param>
        /// <returns></returns>
        private bool NewMethod(bool isSame, bool tags)
        {
            tags = ShowWordS(isSame, tags);
            //显示病人 ward病区编号，tags是否为首次进入
            return tags;
        }

        /// <summary>
        /// 搜索显示病区数据
        /// </summary>
        /// <param name="isSame"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        private bool ShowWordS(bool isSame, bool tags)
        {
            if (!isSame)
            {
                DataRow[] dr = new DataRow[200];
                if (Txt_Serch.Text.ToString().Trim() != "病区名/简拼" && Txt_Serch.Text.ToString().Trim() != "")//是否有病区查询
                {
                    string s = "";
                    if (cbbWardArea.SelectedIndex == 0)
                    {
                        s = string.Format(" WardName like '%{0}%' or SpellCode like '%{0}%' ", Txt_Serch.Text.ToString().Trim());
                    }
                    else
                    {
                        s = string.Format(" (WardName like '%{0}%'  or SpellCode like '%{0}%') and WardArea = '{1}' ", Txt_Serch.Text.ToString().Trim(), cbbWardArea.Text);
                    }
                    dr = WardDS.Tables[0].Select(s);
                    if (dr.Length == 0)
                    {
                        lblTotal.Text = "0";
                        lblUnSend.Text = "0";
                        while (dgvWard.Rows.Count > 0)
                        {
                            dgvWard.Rows.RemoveAt(0);
                        }
                        dgv_Info.Dgv_Info.DataSource = null;
                        med1.clear();
                        return tags;
                    }

                    DataTable dt = new DataTable();
                    dt = WardDS.Tables[0].Clone();
                    for (int i = 0; i < dr.Length; i++)
                    {
                        dt.ImportRow((DataRow)dr[i]);
                    }
                    dt.Columns.Add("select");
                    dgvWard.DataSource = dt;
                }
                else if (Txt_Serch.Text.ToString().Trim() == "病区名/简拼" || Txt_Serch.Text.ToString().Trim() == "")
                {
                    string s = "";
                    if (cbbWardArea.SelectedIndex == 0)
                    {
                    }
                    else
                    {
                        s = string.Format(" WardArea = '{0}' ", cbbWardArea.Text);
                    }
                    if (WardDS.Tables[0].Rows.Count > 0)
                    {
                        lblTotal.Text = WardDS.Tables[0].Rows[0]["TotalCount"].ToString();
                        lblUnSend.Text = WardDS.Tables[0].Rows[0]["UnCheckCount"].ToString();
                        WardDS.Tables[0].Rows.RemoveAt(0);
                    }
                    dr = WardDS.Tables[0].Select(s);
                    if (dr.Length == 0)
                    {
                        while (dgvWard.Rows.Count > 0)
                        {
                            dgvWard.Rows.RemoveAt(0);
                        }

                        dgv_Info.Dgv_Info.DataSource = null;
                        med1.clear();
                        return tags;
                    }

                    DataTable dt = new DataTable();
                    dt = WardDS.Tables[0].Clone();
                    for (int i = 0; i < dr.Length; i++)
                    {
                        dt.ImportRow((DataRow)dr[i]);
                    }
                    dt.Columns.Add("select");
                    dgvWard.DataSource = dt;
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt = WardDS.Tables[0];
                    dt.Columns.Add("select");
                    dt.Rows.RemoveAt(0);
                    if (dt.Rows.Count == 0)
                    {
                        lblTotal.Text = "0";
                        lblUnSend.Text = "0";
                        while (dgvWard.Rows.Count > 0)
                        {
                            dgvWard.Rows.RemoveAt(0);
                        }

                        dgv_Info.Dgv_Info.DataSource = null;
                        med1.clear();
                        return tags;
                    }
                    dgvWard.DataSource = dt;
                }
            }

            return tags;
        }

        /// <summary>
        /// 调用存储过程
        /// </summary>
        /// <param name="count"></param>
        public void ShowWard(int count)
        {
            int IVstatus = 0;//Check_Ivstatus.Checked == true ? 1 : 0;
            string strWard = "EXEC [bl_getIVRecordbyarea] '" + pvb.datetime.ToString("yyyyMMdd") + "%'," + pvb.WardIdle + "," + pvb.WardOpen + "," + IVstatus + "," + pvb.IvBatchSaved;
            WardDS = DB.GetPIVAsDB(strWard);

            //找到pvb里保存的Wardcode的控件
            //如果找到。修改单个病区的数量
            //如果没有找到，选中第一个病区

            ShowWard(0, false);
            string[] w = new string[] { };
            if (pvb.ward.Length > 0 && pvb.ward != "''")
            {
                try
                {
                    w = pvb.ward.Replace("'", "").Split(',');
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            for (int i = 0; i < w.Length; i++)
            {
                for (int j = 0; j < dgvWard.Rows.Count; j++)
                {
                    if (dgvWard.Rows[j].Cells["WardCode"].Value.ToString() == w[i])
                    {
                        dgvWard.Rows[j].Cells["dgvselect"].Value = true;
                    }
                }
            }
            ShowSelectWard();
        }

        /// <summary>
        /// 显示病人或者瓶签信息
        /// </summary>
        /// <param name="Tward">病区</param>
        /// <param name="tags">是否为首次进入，true首次进入，false</param>
        public void ShowDrug(string Tward, bool tags)
        {
            int IVstatus = 0;// Check_Ivstatus.Checked == true ? 1 : 0;
            pvb.ward = Tward;
            try
            {
                //首次进入清空总病人数量
                int PatCounts = 0;
                if (tags)
                {
                    //清空病人Code
                    Petcode = string.Empty;
                    //清空病人数量
                    PatCount = 0;                   
                   
                    //首次进入
                    PatCounts = 5;
                    //病人编码，时间，瓶签状态：默认未打印，查询
                    Drugds = DB.GetPIVAsDB(select.IVRecord(Tward, pvb.datetime.ToString("yyyy-MM-dd"), pvb.IvBatchSaved, Text_SelectText.Text));
                }
                else
                {
                    PatCounts = PatCount + 1;
                }

                //明细模式
                if (pvb.PreviewMode == "0")
                {
                    panel3.Visible = true;
                    txtPatient.Visible = true;
                    //详细数据置顶置顶
                    Panel_BatchRule.BringToFront();
                    //Label_Get.Visible = false;
                    //列表模式，显示病人栏
                    Panel_Patient.Visible = true;
                    Panel_BatchRule.Visible = true;
                    Sp_Info.Visible = false;
                    Panel_BatchRule.Location = new Point(panel3.Location.X + panel3.Width, Panel_BatchRule.Location.Y);
                    Panel_BatchRule.Width = this.Size.Width - dgvWard.Width - panel3.Width +30;
                    Panel_Patient.Visible = true;
                    //加载病人控件
                    if (Panel_Patient.Controls.Count <= 0)
                    {
                        patient = new Patient(this);
                        Panel_Patient.Controls.Add(patient);
                    }
                    else
                    {
                        patient = (Patient)(Panel_Patient.Controls[0]);
                    }
                    //医生Code,有改动的病人颜色根据登陆者的工号查询OrderFormSet中的ChangeColor
                    patient.ECode = pvb.DEmployeeID;
                    int IsSame = 0;
                    if (Check_IsSame.SelectedIndex >= 0)
                    {
                        IsSame = Check_IsSame.SelectedIndex;
                    }
                    string s = "";

                    s = Tward.Replace("'", "");

                    string strWard = "EXEC [bl_getIVRecordbypatient] '" + pvb.datetime.ToString("yyyyMMdd") + "%','" + s + "'," + pvb.WardOpen + "," + IVstatus + "," + pvb.IvBatchSaved + "," + IsSame;
                    DataSet ds = DB.GetPIVAsDB(strWard);
                    if (ds.Tables.Count == 0)
                    {
                        while (patient.Dgv_Patient.Rows.Count>0)
                        {
                            patient.Dgv_Patient.Rows.RemoveAt(0);
                        }
                        return;
                    }
                    dtPatient = ds.Tables[0];
                    patient.Dgv_Patient.Rows.Clear();
                    //不能用捆绑式的，原因是颜色无法改变。
                    for (int i = 0; i < dtPatient.Rows.Count; i++)
                    {
                        patient.Dgv_Patient.Rows.Add(1);
                        patient.Dgv_Patient.Rows[i].Cells["dgv_PatName"].Value = dtPatient.Rows[i]["PatName"].ToString();
                        patient.Dgv_Patient.Rows[i].Cells["dgv_BedNo"].Value = dtPatient.Rows[i]["BedNo"].ToString();
                        patient.Dgv_Patient.Rows[i].Cells["dgv_PatCode"].Value = dtPatient.Rows[i]["PatCode"].ToString();
                        patient.Dgv_Patient.Rows[i].Cells["dgv_UnCheckCount"].Value = dtPatient.Rows[i]["UnCheckCount"].ToString();
                        patient.Dgv_Patient.Rows[i].Cells["dgv_TotalCount"].Value = dtPatient.Rows[i]["TotalCount"].ToString();
                        patient.Dgv_Patient.Rows[i].Cells["dgv_BatchSaved"].Value = dtPatient.Rows[i]["BatchSaved"].ToString();
                        patient.Dgv_Patient.Rows[i].Cells["dgv_IsSame"].Value = dtPatient.Rows[i]["IsSame"].ToString();
                        patient.Dgv_Patient.Rows[i].Cells["IsOpen"].Value = dtPatient.Rows[i]["IsOpen"].ToString();
                        patient.Dgv_Patient.Rows[i].Cells["WardCode"].Value = dtPatient.Rows[i]["WardCode"].ToString();

                    }              
               
                    patient.Size = Panel_Patient.Size;
                    patient.ShowData();
                    
                }
                else if (pvb.PreviewMode == "3")
                {
                    panel3.Visible = false;
                    txtPatient.Visible = false;
                    Panel_Patient.Visible = false;
                    Sp_Info.Visible = true;
                    //详细数据
                    Panel_BatchRule.Location = new Point(dgvWard.Location.X + dgvWard.Width - 10, Panel_BatchRule.Location.Y);
                    Panel_BatchRule.Width = this.Size.Width - dgvWard.Width + 35;
                    
                    OnePatInfo(Tward.Trim().Equals("''") ? pvb.ward : Tward);
                }
                else if (pvb.PreviewMode == "1")
                {
                    //病人栏隐藏
                    Panel_Patient.Visible = false;
                    //详细数据
                    Panel_BatchRule.Visible = true;
                    dgv_Info.Visible = false;
                    Panel_BatchRule.Location = new Point(dgvWard.Location.X + dgvWard.Width - 10, Panel_BatchRule.Location.Y);
                    Panel_BatchRule.Width = this.Size.Width - dgvWard.Width + 35;
                    int petcount = PatCount;

                    for (int i = petcount; i < PatCounts; i++)
                    {
                        if (Petcode != Drugds.Tables[0].Rows[i]["Patcode"].ToString())
                        {
                            string patcode = Drugds.Tables[0].Rows[i]["Patcode"].ToString();
                            //单个病人显示
                            OnePatient(patcode, false);
                        }
                        PatCount++;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 显示单个病人
        /// </summary>
        /// <param name="patcode"></param>
        public void OnePatient(string patcode, bool ClearS)
        {
            try
            {
                if (ClearS)
                {
                    foreach (Control c in Panel_BatchRule.Controls)
                    {
                        c.Dispose();
                    }
                    GC.Collect();                   
                }
                if (pvb.PreviewMode == "0" || pvb.PreviewMode == "1")
                {
                    PatientInfo main;

                    if (this.Panel_BatchRule.Controls.Count > 0 && pvb.PreviewMode == "0")
                    {
                        main = (PatientInfo)Panel_BatchRule.Controls[0];
                    }
                    else
                    {
                        main = new PatientInfo();
                    }
                    if (pvb.PreviewMode == "0")
                    {
                        main.Width = this.Size.Width - dgvWard.Width - panel3.Width + 20;
                    }
                    else
                    {
                        main.Width = this.Size.Width - dgvWard.Width;
                    }
                    main.premode = pvb.PreviewMode;
                    //单个病人数据 ward病区，patcode病人编码，时间，发送状态，是否打印状态
                   
                    main.SetInfo(pvb.ward, patcode, pvb.datetime, (Check_BatchSaved.SelectedIndex), false, ClearS, S1, SK, LS1, LSK);
                    Panel_BatchRule.Controls.Add(main);
                    Petcode = patcode;
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10035:" + ex.ToString() + "   " + DateTime.Now.ToString() + "\r\n");
            }
        }

        /// <summary>
        /// 单个病区显示，列表模式下
        /// </summary>
        /// <param name="WardCode"></param>
        public void OnePatInfo(string WardCode)
        {
            try
            {
                dgv_Info.Dgv_Info.DataSource = null;
                med1.clear();
                Sp_Info.Visible = true;
                //隐藏栏 
                Sp_Info.Location = new Point(Panel_BatchRule.Location.X + 10, Panel_BatchRule.Location.Y);
                Panel_BatchRule.Visible = false;
                //显示瓶签信息
                if (WardCode.Trim() != "''" || Text_SelectText.Text.Trim() != "姓名(编码)/床号/主药/溶媒/频序/组号")
                {
                    int IsSame = 0;
                    //有改动未改动
                    if (Check_IsSame.SelectedIndex >= 0)
                    {
                        IsSame = Check_IsSame.SelectedIndex;
                    }
              
                    if (Text_SelectText.Text.Trim() != "姓名(编码)/床号/主药/溶媒/频序/组号")
                    {
                        dgv_Info.ShowInfo(WardCode, pvb.datetime.ToString("yyyy-MM-dd"), pvb.
                            IvBatchSaved, IsSame, Text_SelectText.Text.Trim(), LongOrtemp(),GetDrugType());
                    }
                    else
                    {
                        dgv_Info.ShowInfo(WardCode, pvb.datetime.ToString("yyyy-MM-dd"), pvb.IvBatchSaved, IsSame, "", LongOrtemp(),GetDrugType());
                    }
                }
                else
                {
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 显示病人，用于病人控件
        /// </summary>
        public void OnePatient(bool tags)
        {
            string patcode = ((Patient)(Panel_Patient.Controls[0])).Dgv_Patient.SelectedRows[0].Cells["Patcode"].Value.ToString();
            PatientInfo main = new PatientInfo();

            if (pvb.PreviewMode != "2" && pvb.PreviewMode != "3")
            {
                main.premode = pvb.PreviewMode;
                //单个病人数据 ward病区，patcode病人编码，时间，发送状态，是否打印状态
                //main.SetInfo(pvb.ward, patcode, pvb.datetime, int.Parse(Check_BatchSaved.Tag.ToString()), Check_Ivstatus.Checked, true);
                main.SetInfo(pvb.ward, patcode, pvb.datetime, int.Parse(Check_BatchSaved.Tag.ToString()), false, true,S1,SK,LS1,LSK);
                Panel_BatchRule.Controls.Add(main);
                Petcode = patcode;
                ((Patient)(Panel_Patient.Controls[0])).Dgv_Patient.Enabled = tags;
            }
        }

        /// <summary>
        /// 显示时间
        /// </summary>
        private void ShowDateTime()
        {
            //tt为单个登录者在设置里设置的时间
            string[] tt = new string[2];
            if (time == null || time.Trim().Length <= 0) //时间点，在这个时间点后显示第二天数据
            {
                time = "18,00";
            }

            tt = time.Split(',');
            if (DateTime.Now.Hour >= int.Parse(tt[0]))
            {
                if (DateTime.Now.Hour == int.Parse(tt[0]))
                {
                    //是否当前时间的分钟大于或者等于设置时间中的分钟
                    if (DateTime.Now.Minute > int.Parse(tt[1]))
                    {
                        //当前时间加一天
                        DateP_Date.Text = DateTime.Now.AddDays(1).ToShortDateString();
                    }
                    else
                    {
                        DateP_Date.Text = DateTime.Now.ToShortDateString();
                    }
                }
                else
                {
                    DateP_Date.Text = DateTime.Now.AddDays(1).ToShortDateString();
                }
            }
            else
            {
                DateP_Date.Text = DateTime.Now.ToShortDateString();
            }
            pvb.datetime = DateTime.Parse(DateP_Date.Text);
        }

        /// <summary>
        /// 获取设置的值
        /// </summary>
        private void TGetType()
        {
            //根据每个登录号来判断显示方式
            DataSet ds = DB.GetPIVAsDB(select.IVRecordSetUp(pvb.DEmployeeID));
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["PreviewMode"] != null && ds.Tables[0].Rows[0]["PreviewMode"].ToString().Trim().Length != 0)
                {
                    //显示模式
                    pvb.PreviewMode = ds.Tables[0].Rows[0]["PreviewMode"].ToString();
                }
                else
                {
                    pvb.PreviewMode = "0";
                }
                if (ds.Tables[0].Rows[0]["WardIdle"] != null && ds.Tables[0].Rows[0]["WardIdle"].ToString().Trim().Length != 0)
                {
                    //有数据病区
                    pvb.WardIdle = int.Parse(ds.Tables[0].Rows[0]["WardIdle"].ToString());
                }
                else
                {
                    pvb.WardIdle = 1;
                }
                if (ds.Tables[0].Rows[0]["WardOpen"] != null && ds.Tables[0].Rows[0]["WardOpen"].ToString().Trim().Length != 0)
                {
                    //开放病区
                    pvb.WardOpen = int.Parse(ds.Tables[0].Rows[0]["WardOpen"].ToString());
                }
                else
                {
                    pvb.WardOpen = 1;
                }
                if (ds.Tables[0].Rows[0]["AutoGetOrder"] != null && ds.Tables[0].Rows[0]["AutoGetOrder"].ToString().Trim().Length != 0)
                {
                    //进入批次时生成瓶签
                    pvb.AutoGetOrder = int.Parse(ds.Tables[0].Rows[0]["AutoGetOrder"].ToString());
                }
                else
                {
                    pvb.AutoGetOrder = 1;
                }
                if (ds.Tables[0].Rows[0]["LabelOrderBy"] != null && ds.Tables[0].Rows[0]["LabelOrderBy"].ToString().Trim().Length != 0)
                {
                    //显示排序条件
                    pvb.LabelOrderBy = int.Parse(ds.Tables[0].Rows[0]["LabelOrderBy"].ToString());

                }
                else
                {
                    pvb.LabelOrderBy = 0;
                }
                if (ds.Tables[0].Rows[0]["NextDay"] != null && ds.Tables[0].Rows[0]["NextDay"].ToString().Trim().Length != 0)
                {
                    //时间点，在这个时间点后显示第二天数据
                    time = ds.Tables[0].Rows[0]["NextDay"].ToString();

                }
                if (ds.Tables[0].Rows[0]["TimeCount"] != null && ds.Tables[0].Rows[0]["TimeCount"].ToString().Trim().Length != 0)
                {
                    //停留在此画面不操作
                    pvb.timeCount = int.Parse(ds.Tables[0].Rows[0]["TimeCount"].ToString()) * 60;
                    Label_down.Text = pvb.timeCount.ToString();
                    pvb.operate = false;
                }

                if (ds.Tables[0].Rows[0]["IsPack"] != null && ds.Tables[0].Rows[0]["IsPack"].ToString().Trim().Length != 0)
                {
                    //不计算空包
                    pvb.IsPack = int.Parse(ds.Tables[0].Rows[0]["IsPack"].ToString());
                }
                pvb.ChangeColords = DB.GetPIVAsDB(select.IVRecordSetUp(pvb.DEmployeeID));

                ds = DB.GetPIVAsDB("select OrderID from DOrder where IsValid = 1");
                if (ds != null && ds.Tables.Count > 0)
                {
                    //pvb.OrderID
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        pvb.OrderID[i] = Convert.ToInt32(ds.Tables[0].Rows[i]["OrderID"].ToString());
                    }
                }
                                
            }
            else
            {
                //如果没有。 添加此登陆者的默认设置
                DB.SetPIVAsDB(insert.OrderFormSet(pvb.DEmployeeID));
            }
        }

        private void Pivasbatch_Load(object sender, EventArgs e) { }
        /// <summary>
        /// 画面大小改变
        /// </summary>
        /// <param name="cn"></param>
        private void Foreach(Control cn)
        {
            if (this.Width != 0 && this.Height != 0)
            {
                if (pvb.PreviewMode == "0" || pvb.PreviewMode == "2")
                {
                    foreach (Control j in cn.Controls)
                    {
                        j.Width = this.Size.Width - dgvWard.Width - panel3.Width + 20;
                    }
                }
                else
                {
                    foreach (Control j in cn.Controls)
                    {
                        j.Width = this.Size.Width - dgvWard.Width;
                    }
                }
            }
        }

        /// <summary>
        /// 详细数据多时滚动加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel_BatchRule_OnMouseWheel(object sender, MouseEventArgs e)
        {
            if (pvb.PreviewMode == "1")
            {
                if (Drugds != null)
                {
                    if (Drugds.Tables[0].Rows.Count > PatCount && e.Delta < 0)
                    {
                        ShowDrug(pvb.ward, false);
                    }
                }
            }
        }

        /// <summary>
        /// 病区数据多时滚动加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlWard_OnMouseWheel(object sender, MouseEventArgs e)
        {

            if (WardDS != null)
            {
                if (WardDS.Tables[0].Rows.Count > WardCount && e.Delta < 0)
                {
                    ShowWard(WardCount, false);
                }
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Text_SelectText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                ShowDrug(pvb.ward, true);
                pvb.operate = true;
            }
        }

        /// <summary>
        /// 发送病区的所有瓶签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_Get_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("确定发送当前列表的所有瓶签吗？", "确定发送当前列表的所有瓶签吗？ ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    string s = "";
                    for (int i = 0; i < dgv_Info.Dgv_Info.Rows.Count; i++)
                    {
                        s = s + "'" + dgv_Info.Dgv_Info.Rows[i].Cells["瓶签号"].Value.ToString() + "',";
                    }
                    if (s.Length > 0)
                    {
                        s = s.Remove(s.Length - 1, 1);
                    }
                    DB.SetPIVAsDB(update.IVRecordBatchSaved(s,S1,SK,LS1,LSK));
                    ShowWard(0, false);
                    if (dgvWard.Rows.Count > 0)
                    {
                        switch (pvb.WardDefaultSelectMode)
                        {
                            case "0": break;

                            case "1": dgvWard.Rows[0].Cells["dgvselect"].Value = true; break;

                            case "2":
                                for (int i = 0; i < dgvWard.Rows.Count; i++)
                                {
                                    dgvWard.Rows[i].Cells["dgvselect"].Value = true;
                                }
                                break;

                            default: dgvWard.Rows[0].Cells["dgvselect"].Value = true; break;
                        }
                    }
                    ShowSelectWard();
                }
                pvb.operate = true;
            }
            catch (System.Exception ex)
            {

            }
        }

        /// <summary>
        /// 选中第几个病区
        /// </summary>
        /// <param name="Cou"></param>
        /// <param name="WardCode"></param>
        public void ClickWard(int Cou, string WardCode)
        {
            ShowDrug(pvb.ward, false);
        }

        /// <summary>
        /// 大小改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pivasbatch_SizeChanged(object sender, EventArgs e)
        {
            this.SuspendLayout();
            if (pvb.PreviewMode == "0" || pvb.PreviewMode == "2")
            {
                Panel_BatchRule.Width = this.Size.Width - dgvWard.Width - panel3.Width + 30;
            }
            else
            {
                Panel_BatchRule.Width = this.Size.Width - dgvWard.Width + 15;
            }

            if (Panel_Patient.Visible == true)
            {
                Panel_Patient.Controls[0].Height = Panel_Patient.Height;
            }
            Foreach(Panel_BatchRule);
            if (this.Width != 0)
            {
                Sp_Info.Panel2Collapsed = false;
                Sp_Info.Location = new Point(Panel_BatchRule.Location.X + 10, Panel_BatchRule.Location.Y);
            }
            this.ResumeLayout(false);

        }

        /// <summary>
        /// 设置画面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_SetUp_Click(object sender, EventArgs e)
        {
            SetUp newset = new SetUp(pvb.DEmployeeID, pvb.DEmployeeName);
            DialogResult diag = newset.ShowDialog();
            if (diag == DialogResult.OK || diag == DialogResult.Cancel)
            {
                TGetType();
                ShowWard(0, false);

                string[] w = new string[] { };
                if (pvb.ward.Length > 0 && pvb.ward != "''")
                {
                    try
                    {
                        w = pvb.ward.Replace("'", "").Split(',');
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("timer1 " + ex.Message);
                    }
                }

                for (int i = 0; i < w.Length; i++)
                {
                    for (int j = 0; j < dgvWard.Rows.Count; j++)
                    {
                        if (dgvWard.Rows[j].Cells["WardCode"].Value.ToString() == w[i])
                        {
                            dgvWard.Rows[j].Cells["dgvselect"].Value = true;
                        }
                    }
                }
                ShowSelectWard();
            }
            pvb.operate = true;
        }

        /// <summary>
        /// 隐藏或者显示右边信息栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_Show_Click(object sender, EventArgs e)
        {
            pvb.operate = true;
            if (Sp_Info.Panel2Collapsed == false)
            {
                Sp_Info.Panel2Collapsed = true;
            }
            else
            {
                Sp_Info.Panel2Collapsed = false;
            }
        }
        /// <summary>
        /// 选择时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MC_Date2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ChangeChoes)
                {
                    pvb.operate = true;
                    pvb.datetime = DateTime.Parse(DateP_Date.Value.ToString());
                    ward = "";
                    pvb.ward = "''";
                    //pnlWard.Controls.Clear();
                    // pnlWard.Controls.Clear();
                    // dgv_Info.Dgv_Info.DataSource = null;
                    ShowWard(0, false);
                    //ClickWard(1, "''");
                    bool select = false;
                    // ClickWard(1, pvb.ward);
                    for (int i = 0; i < dgvWard.Rows.Count; i++)
                    {
                        if (dgvWard.Rows[i].Cells["dgvselect"].Value.ToString() == "True")
                        {
                            select = true;
                            break;
                        }
                    }
                    if (dgvWard.Rows.Count > 0 && !select)
                    {
                        switch (pvb.WardDefaultSelectMode)
                        {
                            case "0": break;

                            case "1": dgvWard.Rows[0].Cells["dgvselect"].Value = true; break;

                            case "2":
                                for (int i = 0; i < dgvWard.Rows.Count; i++)
                                {
                                    dgvWard.Rows[i].Cells["dgvselect"].Value = true;
                                }
                                break;

                            default: dgvWard.Rows[0].Cells["dgvselect"].Value = true; break;
                        }
                    }
                    ShowSelectWard();
                }
            }
            catch (System.Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10013:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }

        /// <summary>
        /// 选择已发送，未发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Check_BatchSaved_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ChangeChoes)
                {
                    try
                    {
                        if (Check_BatchSaved.SelectedIndex==0&&BM)
                        {
                            btnBatchModify.Visible = true;
                                                     
                        }
                        else
                        {
                            btnBatchModify.Visible = false;                                      
                        }

                        if (Check_BatchSaved.SelectedIndex == 1 && CS)
                        {
                            LCancelSend.Visible = true;
                        }
                        else
                        {
                            LCancelSend.Visible = false;
                        }
                        pvb.operate = true;
                        ShowWard(0, false);
                        bool select = false;
                        for (int i = 0; i < dgvWard.Rows.Count; i++)
                        {
                            if (dgvWard.Rows[i].Cells["dgvselect"].Value.ToString() == "True")
                            {
                                select = true;
                                break;
                            }
                        }
                        if (dgvWard.Rows.Count > 0 && !select)
                        {
                            switch (pvb.WardDefaultSelectMode)
                            {
                                case "0": break;

                                case "1": dgvWard.Rows[0].Cells["dgvselect"].Value = true; break;

                                case "2":
                                    for (int i = 0; i < dgvWard.Rows.Count; i++)
                                    {
                                        dgvWard.Rows[i].Cells["dgvselect"].Value = true;
                                    }
                                    break;

                                default: dgvWard.Rows[0].Cells["dgvselect"].Value = true; break;
                            }
                        }
                        ShowSelectWard();
                    }
                    catch (System.Exception ex)
                    {
                        File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10014:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
                    }
                    if (Check_BatchSaved.SelectedIndex == 0)
                    {
                        btnSend.Visible = true;
                    }
                    else
                    {
                        btnSend.Visible = false;
                    }
                }
            }
            catch (System.Exception ex)
            {

            }
        }

        /// <summary>
        /// 选择有改动，未改动，全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Check_IsSame_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ChangeChoes)
                {
                    pvb.operate = true;
                    ShowWard(0, true);
                    bool select = false;
                    for (int i = 0; i < dgvWard.Rows.Count; i++)
                    {
                        if (dgvWard.Rows[i].Cells["dgvselect"].Value.ToString() == "True")
                        {
                            select = true;
                            break;
                        }
                    }
                    if (dgvWard.Rows.Count > 0 && !select)
                    {
                        switch (pvb.WardDefaultSelectMode)
                        {
                            case "0": break;

                            case "1": dgvWard.Rows[0].Cells["dgvselect"].Value = true; break;

                            case "2":
                                for (int i = 0; i < dgvWard.Rows.Count; i++)
                                {
                                    dgvWard.Rows[i].Cells["dgvselect"].Value = true;
                                }
                                break;

                            default: dgvWard.Rows[0].Cells["dgvselect"].Value = true; break;
                        }
                    }
                    ShowDrug(pvb.ward, true);
                }
            }
            catch (System.Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10015:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }
        bool myMutex = false;
        /// <summary>
        /// 不操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!myMutex)
                {
                    myMutex = true;
                    int timecount = int.Parse(Label_down.Text.ToString());
                    if (!pvb.operate)
                    {
                        if (timecount > 0)
                        {
                            Label_down.Text = (timecount - 1).ToString();
                        }
                        else
                        {
                            ShowWard(0, false);
                            string[] w = new string[] { };
                            if (pvb.ward.Length > 0 && pvb.ward != "''")
                            {
                                try
                                {
                                    w = pvb.ward.Replace("'", "").Split(',');
                                }
                                catch (System.Exception ex)
                                {
                                    MessageBox.Show("timer1 " + ex.Message);
                                }
                            }

                            for (int i = 0; i < w.Length; i++)
                            {
                                for (int j = 0; j < dgvWard.Rows.Count; j++)
                                {
                                    if (dgvWard.Rows[j].Cells["WardCode"].Value.ToString() == w[i])
                                    {
                                        dgvWard.Rows[j].Cells["dgvselect"].Value = true;
                                    }
                                }
                            }

                            ShowSelectWard();
                            Label_down.Text = pvb.timeCount.ToString();
                            pvb.operate = false;
                        }
                    }
                    else
                    {
                        Label_down.Text = pvb.timeCount.ToString();
                        pvb.operate = false;
                    }
                    myMutex = false;
                }
            }
            catch (System.Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10017:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }

        /// <summary>
        /// 进入画面后是否自动生成瓶签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (pvb.AutoGetOrder == 1)
                {
                    timer2.Enabled = false;
                    Wait wait = new Wait(pvb.datetime, pvb.DEmployeeID, "", "", 0);
                    DialogResult result = wait.ShowDialog();
                    if (result == DialogResult.Cancel)
                    {
                        ShowWard(0, false);
                        if (dgvWard.Rows.Count > 0)
                        {
                            switch (pvb.WardDefaultSelectMode)
                            {
                                case "0": break;

                                case "1": dgvWard.Rows[0].Cells["dgvselect"].Value = true; break;

                                case "2":
                                    for (int i = 0; i < dgvWard.Rows.Count; i++)
                                    {
                                        dgvWard.Rows[i].Cells["dgvselect"].Value = true;
                                    }
                                    break;

                                default: dgvWard.Rows[0].Cells["dgvselect"].Value = true; break;
                            }
                        }
                        ShowSelectWard();
                    }
                }
            }
            catch (System.Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10018:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }
        /// <summary>
        /// 病区搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel_Serch_Click(object sender, EventArgs e)
        {
            try
            {
                pvb.operate = true;
                ShowWard(0, false);
                if (Txt_Serch.Text.ToString().Trim() != "病区名/简拼" && Txt_Serch.Text.ToString().Trim() != "")
                {
                    bool select = false;
                    for (int i = 0; i < dgvWard.Rows.Count; i++)
                    {
                        if (dgvWard.Rows[i].Cells["dgvselect"].Value.ToString() == "True")
                        {
                            select = true;
                            break;
                        }
                    }
                    if (dgvWard.Rows.Count > 0 && !select)
                    {
                        switch (pvb.WardDefaultSelectMode)
                        {
                            case "0": break;

                            case "1": dgvWard.Rows[0].Cells["dgvselect"].Value = true; break;

                            case "2":
                                for (int i = 0; i < dgvWard.Rows.Count; i++)
                                {
                                    dgvWard.Rows[i].Cells["dgvselect"].Value = true;
                                }
                                break;

                            default: dgvWard.Rows[0].Cells["dgvselect"].Value = true; break;
                        }
                    }
                    ShowSelectWard();
                }
            }
            catch (Exception ex)
            {

                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10019:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }

        private void Check_L_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (System.Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10031:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }

        private void Txt_Serch_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    pvb.operate = true;
                    ShowWard(0, false);
                    if (Txt_Serch.Text.ToString().Trim() != "病区名/简拼")
                    {

                        if (dgvWard.Rows.Count > 0)
                        {
                            switch (pvb.WardDefaultSelectMode)
                            {
                                case "0": break;

                                case "1": dgvWard.Rows[0].Cells["dgvselect"].Value = true; break;

                                case "2":
                                    for (int i = 0; i < dgvWard.Rows.Count; i++)
                                    {
                                        dgvWard.Rows[i].Cells["dgvselect"].Value = true;
                                    }
                                    break;

                                default: dgvWard.Rows[0].Cells["dgvselect"].Value = true; break;
                            }
                        }
                        ShowSelectWard();
                    }
                }
            }
            catch (System.Exception ex)
            {

                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10020:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }

        private void pnlWard_Scroll(object sender, ScrollEventArgs e)
        {
            if (WardDS != null)
            {
                if (WardDS.Tables[0].Rows.Count > WardCount)
                {
                    ShowWard(WardCount, false);
                }
            }
        }

        private void Txt_Serch_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (dataGridView2.Visible == true)
            {
                dataGridView2.Visible = false;
            }
            else
            {
                int Same = 0;
                if (Check_IsSame.SelectedIndex >= 0)
                {
                    Same = Check_IsSame.SelectedIndex;
                }

                DataSet ds1 = new DataSet();
                if (Text_SelectText.Text.Trim() != "姓名(编码)/床号/主药/溶媒/频序/组号")
                {
                    ds1 = DB.GetPIVAsDB(select.BatchNum(pvb.ward, pvb.datetime.ToString("yyyy-MM-dd"), pvb.IvBatchSaved, Text_SelectText.Text, Same, LongOrtemp()));
                }
                else
                {
                    ds1 = DB.GetPIVAsDB(select.BatchNum(pvb.ward, pvb.datetime.ToString("yyyy-MM-dd"), pvb.IvBatchSaved, "", Same, LongOrtemp()));
                }

                dataGridView2.DataSource = ds1.Tables[0];
                dataGridView2.Visible = true;
            }
        }

        /// <summary>
        /// 加载病区组选项
        /// </summary>
        private void AddWardArea()
        {
            try
            {
                DataSet ds = DB.GetPIVAsDB("select  distinct WardArea from  DWard where WardArea is not null and WardArea <> ''");
                cbbWardArea.Items.Clear();
                cbbWardArea.Items.Add("所有病区组");
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        cbbWardArea.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                    }
                }
            }
            catch (System.Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10021:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
            finally
            {
            }
        }

        private void dgvWard_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 6)
                {
                    {                       
                        ClickOrSelect = 0;
                        ClickDgvWard = true;
                        if (dgvWard.CurrentRow.Cells["dgvselect"].Value.ToString() == "True")
                        {
                            dgvWard.CurrentRow.Cells["dgvselect"].Value = false;
                        }
                        else
                        {
                            dgvWard.CurrentRow.Cells["dgvselect"].Value = true;
                        }
                    }
                    Application.DoEvents();
                    ShowSelectWard();
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10022:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }

        /// <summary>
        /// 显示勾选的病区的数据
        /// </summary>
        public void ShowSelectWard()
        {
            try
            {
                string sward = "";
                int SelCount = 0;
                
                    for (int i = 0; i < dgvWard.Rows.Count; i++)
                    {
                        if (dgvWard.Rows[i].Cells["dgvselect"].Value.ToString() == "True")
                        {
                            sward = sward + "'" + dgvWard.Rows[i].Cells["wardcode"].Value.ToString() + "',";
                            SelCount++;
                        }
                    }
                    ClickDgvWard = true;
                    if (SelCount == 0)
                    {
                        checkBox1.Checked = false;
                    }
                    else if (SelCount == dgvWard.Rows.Count)
                    {
                        checkBox1.CheckState = CheckState.Checked;
                    }
                    else
                    {
                        checkBox1.CheckState = CheckState.Indeterminate;
                    }


                    if (sward.Length > 0)
                    {
                        sward = sward.Remove(sward.Length - 1, 1);
                    }
                    else
                    {
                        sward = "''";
                    }

                    pvb.ward = sward;
                    ShowDrug(sward, true);           
                ClickDgvWard = false;
            }
            catch (Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10023:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }

        private void Txt_Serch_Enter(object sender, EventArgs e)
        {
            if (Txt_Serch.Text.Trim() == "病区名/简拼")
            {
                Txt_Serch.Text = "";
                Txt_Serch.ForeColor = Color.Black;
            }
        }

        private void Txt_Serch_Leave(object sender, EventArgs e)
        {
            if (Txt_Serch.Text.Trim() == "")
            {
                Txt_Serch.Text = "病区名/简拼";
                Txt_Serch.ForeColor = Color.Gray;
            }
        }

        private void cbbWardArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pvb.operate = true;

                ShowWard(0, false);

                if (dgvWard.Rows.Count > 0)
                {
                    switch (pvb.WardDefaultSelectMode)
                    {
                        case "0": break;

                        case "1": dgvWard.Rows[0].Cells["dgvselect"].Value = true;break;

                        case "2":
                            for (int i = 0; i < dgvWard.Rows.Count;i++ )
                            {
                                dgvWard.Rows[i].Cells["dgvselect"].Value = true;
                            }
                            break;

                        default: dgvWard.Rows[0].Cells["dgvselect"].Value = true; break;
                    }
                    
                }

                ShowSelectWard();

            }
            catch (System.Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10024:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }

        private void Text_SelectText_Enter(object sender, EventArgs e)
        {
            try
            {
                if (Text_SelectText.Text.Trim() == "姓名(编码)/床号/主药/溶媒/频序/组号")
                {
                    Text_SelectText.Text = "";
                    Text_SelectText.ForeColor = Color.Black;
                }
            }
            catch (System.Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10025:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }

        private void Text_SelectText_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Text_SelectText.Text.Trim() == "")
                {
                    Text_SelectText.Text = "姓名(编码)/床号/主药/溶媒/频序/组号";
                    Text_SelectText.ForeColor = Color.Gray;
                }

            }
            catch (System.Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10026:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }

        /// <summary>
        /// 返回查选条件，长期还是临时 
        /// </summary>
        /// <returns>1、长期，2、临时，3、长期和临时</returns>
        private int LongOrtemp()
        {
            try
            {
                if (cblong.Checked == cbtemp.Checked)
                {
                    return 3;
                }
                else if (cblong.Checked)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            catch (System.Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10027:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
                return 3;
            }
        }

        private void cbtemp_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ShowDrug(pvb.ward, true);
                pvb.operate = true;
            }
            catch (System.Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10028:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
        }

        private void dataGridView2_Leave(object sender, EventArgs e)
        {
        }

        private void SelectBatch()
        {
            try
            {
                pvb.operate = true;

                pvb.Choose2 = " and (1=0 ";
                if (Check_K.Checked)
                {
                    pvb.Choose2 += " or Batch like '%K%'  ";
                }
                if (Check_ChongPei.Checked)
                {
                    pvb.Choose2 += " or  Batch like '%#%' ";
                }
                pvb.Choose2 += " )";
                pvb.Choose2 += " and ( 1=0  ";

                if (Check_L.Checked)
                {
                    pvb.Choose2 += "  or Batch like '%L%'";
                }
                if (Check_Long.Checked)
                {
                    pvb.Choose2 += "  or Batch not like '%L%'";
                }
                pvb.Choose2 += " )";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Check_ChongPei_CheckedChanged(object sender, EventArgs e)
        {
            SelectBatch();

            ShowWard(0, true);
            ShowDrug(pvb.ward, true);
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (!ClickDgvWard)
            {
                if (checkBox1.CheckState != CheckState.Indeterminate)
                {
                    for (int i = 0; i < dgvWard.Rows.Count; i++)
                    {
                        dgvWard.Rows[i].Cells["dgvselect"].Value = checkBox1.Checked;
                    }
                }
                ShowSelectWard();
                ClickDgvWard = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void dgvWard_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                pvb.operate = true;
                ClickOrSelect = 1;
                if (dgvWard.CurrentCell.ColumnIndex != 6)
                {
                    dgvWard.CurrentRow.DefaultCellStyle.SelectionBackColor = Color.FromArgb(184, 204, 214);

                   ward= "'" + dgvWard.CurrentRow.Cells["WardCode"].Value.ToString() + "'";
                    ShowDrug(ward, false);
                }
            }
            catch (System.Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10033:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }      
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                StringBuilder SB = new StringBuilder();
                SB.Append(" select * from SynLog where ");
                SB.Append(" SynCode =(select top 1  SynCode from  [dbo].[SynSet] where SYnName='药单')   ");
                SB.Append(" and  EndTime  is  null   ");
                ds = DB.GetPIVAsDB(SB.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //检测是否有正在实行的药单同步，若有，则需要先等待要当同步结束。
                     MessageBox.Show("【药单】正在同步，若要停止之前进程，请到同步画面操作 ！");
                    SeeSynIDv();
                }
                else
                {
                    timer1.Enabled = false;
                    Wait wait = new Wait(pvb.datetime, pvb.DEmployeeID, "", "", 0);
                    DialogResult result = wait.ShowDialog();
                    if (result == DialogResult.Cancel || result == DialogResult.OK)
                    {
                        ShowWard(0, false);

                        if (dgvWard.Rows.Count>0)
                        {
                            switch (pvb.WardDefaultSelectMode)
                            {
                                case "0": break;

                                case "1": dgvWard.Rows[0].Cells["dgvselect"].Value = true; break;

                                case "2":
                                    for (int i = 0; i < dgvWard.Rows.Count; i++)
                                    {
                                        dgvWard.Rows[i].Cells["dgvselect"].Value = true;
                                    }
                                    break;

                                default: dgvWard.Rows[0].Cells["dgvselect"].Value = true; break;
                            }
                        }

                        ShowSelectWard();
                        timer1.Enabled = true;
                    }
                    pvb.operate = true;
                }
            }
            catch (System.Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10034:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            try
            {
                SetUp newset = new SetUp(pvb.DEmployeeID, pvb.DEmployeeName);
                DialogResult diag = newset.ShowDialog();
                if (diag == DialogResult.OK || diag == DialogResult.Cancel)
                {
                    TGetType();

                    Initing = true;
                    if (pvb.PreviewMode == "0")//病区列表模式模式
                    {
                        cbbMode.SelectedIndex = 0;
                        flowLayoutPanel3.Visible = false;
                        checkBox2.Visible = false;
                    }
                    else//病人明细模式
                    {
                        cbbMode.SelectedIndex = 1;
                        flowLayoutPanel3.Visible =true ;
                        checkBox2.Visible = true;
                    }
                    Initing = false;

                    ShowWard(0, false);

                    string[] w = new string[] { };
                    if (pvb.ward.Length > 0 && pvb.ward != "''")
                    {
                        try
                        {
                            w = pvb.ward.Replace("'", "").Split(',');
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show("timer1 " + ex.Message);
                        }
                    }

                    for (int i = 0; i < w.Length; i++)
                    {
                        for (int j = 0; j < dgvWard.Rows.Count; j++)
                        {
                            if (dgvWard.Rows[j].Cells["WardCode"].Value.ToString() == w[i])
                            {
                                dgvWard.Rows[j].Cells["dgvselect"].Value = true;
                            }
                        }
                    }

                    ShowSelectWard();
                }
            }
            catch (System.Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10036:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }
      
        private void btnGennerationBatch_Click(object sender, EventArgs e)
        {
            try
            { 
                if (DB.GetPIVAsDB(select.PBatchTemp()) == null || DB.GetPIVAsDB(select.PBatchTemp()).Tables[0].Rows.Count <= 0)
                {
                    DialogResult result = MessageBox.Show("确定重排当前病区的所有批次吗？", "确定重排当前病区的所有批次吗？ ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        timer1.Enabled = false;

                        int num = pvb.ward.IndexOf(",");
                        if (num > 0)
                        {
                            MessageBox.Show("重排只能勾选一个病区");
                            return;
                        }
                        string wardcode = pvb.ward.TrimStart('\'').TrimEnd('\'');
                        
                        //等待
                        Wait wait = new Wait(pvb.datetime, pvb.DEmployeeID, wardcode, "", 1);
                        DialogResult rr = wait.ShowDialog();
                        if (rr == DialogResult.Cancel)
                        {
                            ShowDrug(pvb.ward, true);
                            timer1.Enabled = true;
                        }
                        pvb.operate = true;
                    }

                }
                else
                {
                    MessageBox.Show("正在排批次，请稍候！");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                pvb.operate = true;
                if (pvb.PreviewMode == "3")
                {

                    DialogResult result = MessageBox.Show("确定发送当前列表的所有瓶签吗？", "确定发送当前列表的所有瓶签吗？ ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        string s = "";
                        for (int i = 0; i < dgv_Info.Dgv_Info.Rows.Count; i++)
                        {
                            s = s + "'" + dgv_Info.Dgv_Info.Rows[i].Cells["瓶签号"].Value.ToString() + "',";
                        }
                        if (s.Length > 0)
                        {
                            s = s.Remove(s.Length - 1, 1);
                        }
                        if (s != null && s != "")
                        {
                            DB.SetPIVAsDB(update.IVRecordBatchSaved(s,S1,SK,LS1,LSK));
                        }
                        else
                        {
                            return;
                        }
                        ShowWard(0, false);
                        if (dgvWard.Rows.Count > 0)
                        {
                            switch (pvb.WardDefaultSelectMode)
                            {
                                case "0": break;

                                case "1": dgvWard.Rows[0].Cells["dgvselect"].Value = true; break;

                                case "2":
                                    for (int i = 0; i < dgvWard.Rows.Count; i++)
                                    {
                                        dgvWard.Rows[i].Cells["dgvselect"].Value = true;
                                    }
                                    break;

                                default: dgvWard.Rows[0].Cells["dgvselect"].Value = true; break;
                            }
                        }
                        ShowSelectWard();
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("确定发送当前列表的所有患者的瓶签吗？", "确定发送当前列表的所有患者的瓶签吗？ ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        string s = "";

                        for (int i = 0; i < patient.Dgv_Patient.Rows.Count; i++)
                        {
                            s = s + "'" + patient.Dgv_Patient.Rows[i].Cells["dgv_PatCode"].Value.ToString() + "',";
                        }
                        if (s.Length > 0)
                        {
                            s = s.Remove(s.Length - 1, 1);
                        }
                        DB.SetPIVAsDB(update.IVRecordBatchSaved(s,DateP_Date.Value.ToString("yyyyMMdd"),S1,SK,LS1,LSK));
                        ShowWard(0, false);
                        if (dgvWard.Rows.Count > 0)
                        {
                            switch (pvb.WardDefaultSelectMode)
                            {
                                case "0": break;

                                case "1": dgvWard.Rows[0].Cells["dgvselect"].Value = true; break;

                                case "2":
                                    for (int i = 0; i < dgvWard.Rows.Count; i++)
                                    {
                                        dgvWard.Rows[i].Cells["dgvselect"].Value = true;
                                    }
                                    break;

                                default: dgvWard.Rows[0].Cells["dgvselect"].Value = true; break;
                            }
                        }
                        ShowSelectWard();
                    }
                }
            }
            catch (System.Exception ex)
            {

            }
        }

        private void txtPatient_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (txtPatient.Text != "患者姓名/ID/床号" && e.KeyChar == (char)13)
                {
                    DataRow[] R=new DataRow[]{};
                    DataTable dt = new DataTable();
                    R = dtPatient.Select(String.Format("PatCode like '%{0}%' or PatName like '%{0}%' or BedNo like '%{0}%'",txtPatient.Text));
                    if (R.Length==0)
                    {
                        while (patient.Dgv_Patient.Rows.Count>0)
                        {
                            patient.Dgv_Patient.Rows.RemoveAt(0);
                        }
                        return;
                    }
                    dt = dtPatient.Clone();
                    for (int i = 0; i < R.Length;i++ )
                    {
                        dt.ImportRow(R[i]);
                    }
                    patient.Dgv_Patient.DataSource = dt;
                    patient.ShowData();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPatient_Enter(object sender, EventArgs e)
        {
            
        }

        private void txtPatient_Click(object sender, EventArgs e)
        {
            txtPatient.SelectAll();
        }

        private void cbbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //previewmode为0时是明细模式。为3时 是列表模式
                if (Initing == false)
                {
                    if (cbbMode.SelectedIndex == 0)
                    {
                        pvb.PreviewMode = "0";
                        DB.SetPIVAsDB("update OrderFormSet set PreviewMode = 0 where DEmployeeID= '" + pvb.DEmployeeID + "'");
                        flowLayoutPanel3.Visible = false;
                        checkBox2.Visible = false;
                        flowLayoutPanel4.Visible = false;
                    }
                    else if (cbbMode.SelectedIndex == 1)
                    {
                        pvb.PreviewMode = "3";
                        DB.SetPIVAsDB("update OrderFormSet set PreviewMode = 3 where DEmployeeID= '" + pvb.DEmployeeID + "'");
                        flowLayoutPanel3.Visible = true;
                        checkBox2.Visible = true;
                        flowLayoutPanel4.Visible = true;
                    }
                    TGetType();
                    ShowWard(0, false);

                    string[] w = new string[] { };
                    if (pvb.ward.Length > 0 && pvb.ward != "''")
                    {
                        try
                        {
                            w = pvb.ward.Replace("'", "").Split(',');
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show("timer1 " + ex.Message);
                        }
                    }

                    for (int i = 0; i < w.Length; i++)
                    {
                        for (int j = 0; j < dgvWard.Rows.Count; j++)
                        {
                            if (dgvWard.Rows[j].Cells["WardCode"].Value.ToString() == w[i])
                            {
                                dgvWard.Rows[j].Cells["dgvselect"].Value = true;
                            }
                        }
                    }

                    ShowSelectWard();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                upateIVRecord u = new upateIVRecord(DateP_Date.Text, pvb.DEmployeeID);
                u.ShowDialog();
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            pvb.fastChange = checkBox2.Checked;
        }

        /// <summary>
        /// 快速同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSynDrugList_Click(object sender, EventArgs e)
        {
            try
            {
                if (GetPivasLimit.Instance.Limit(pvb.DEmployeeID, "03004"))
                {
                    DataSet ds = new DataSet();
                    StringBuilder SB = new StringBuilder();
                    SB.Append(" select * from SynLog where ");
                    SB.Append(" SynCode =(select top 1  SynCode from  [dbo].[SynSet] where SYnName='药单')   ");
                    SB.Append(" and  EndTime  is  null   ");
                    ds = DB.GetPIVAsDB(SB.ToString());
                    if (ds == null)
                    {
                        MessageBox.Show("未取得同步数据信息 ！！！");
                    }
                    else if (ds.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show("【药单】正在同步，若要停止之前进程，请到同步画面操作 ！");
                        SeeSynIDv();
                    }
                    else
                    {
                        MessageBox.Show("开始同步 ！ 您可以到同步画面去查看同步进度。");
                        insertNewSynLog();
                    }
                }
                else
                {
                    MessageBox.Show("您没有权限，请与管理员联系 ！");
                }
            }
            catch
            {
                MessageBox.Show("药单同步出错！！！");
            }
        }

        private string insertNewSynLog()
        {
            try
            {
                DataSet ds2 = new DataSet();
                StringBuilder SB = new StringBuilder();
                SB.Append(" select top 1  SynCode from  SynSet  where SYnName='药单'   ");
                ds2 = DB.GetPIVAsDB(SB.ToString());
                string SynCode = ds2.Tables[0].Rows[0]["SynCode"].ToString();
                string rt = string.Format("exec bl_InsertNewSynLog '{0}','{1}'", SynCode, pvb.DEmployeeID).ToString();
                DB.GetPIVAsDB(rt);

                SeeSynIDv();//打开监控
             
                return "1" ;
            }
            catch
            {
                synID = string.Empty;
                SeeSynID = 0;
                timer3.Enabled = false;
                MessageBox.Show("同步信息插入出错！");
                return "0";
            }
        }

        /// <summary>
        /// 监控同步程序状态
        /// </summary>
        /// <param name="SynCode"></param>
        private void SeeSynIDv()
        {
            //插入同步命令后，开始监控此命令。
            DataSet ds2 = new DataSet();
            StringBuilder SB2 = new StringBuilder();
            SB2.Append(" select max(SynID) as  SynID  from  SynLog      ");
            SB2.Append("  where SynCode=(select top 1  SynCode from  SynSet  where SYnName='药单')    ");
            // MessageBox.Show(SB2.ToString());
            ds2 = DB.GetPIVAsDB(SB2.ToString());
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                synID = ds2.Tables[0].Rows[0]["SynID"].ToString();
                SeeSynID = 180;//设定监控3分钟，过了三分钟，提醒。
                timer3.Enabled = true;//打开监控
            }
            else
            {
                MessageBox.Show(" 没有找到同步监控数据  ！！！ ");
            }
        }

        /// <summary>
        /// 监控同步是否完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer3_Tick(object sender, EventArgs e)
        {
            if (SeeSynID != 0 && synID != string.Empty)
            {
                SeeSynID = SeeSynID - 1;
            }
            else
            {
                synID = string.Empty;
                timer3.Enabled = false;
                return;
            }
            DataSet ds = new DataSet();
            StringBuilder SB2 = new StringBuilder();
            SB2.Append(" select  *  from  SynLog  where SynID='");
            SB2.Append(synID);
            SB2.Append("'   ");
            ds = DB.GetPIVAsDB(SB2.ToString());
            string StaTime =ds.Tables[0].Rows[0]["StartTime"].ToString();
            string EndTime = ds.Tables[0].Rows[0]["EndTime"].ToString();
            string synID2 = ds.Tables[0].Rows[0]["synID"].ToString();
            ds.Dispose();
            if(SeeSynID==0)
            {
                synID = string.Empty;
                timer3.Enabled = false;
                MessageBox.Show("同步时间过长，请到同步画面确认！");
            }
            else if (EndTime != string.Empty)
            {
                synID = string.Empty;
                timer3.Enabled = false;
                MessageBox.Show("同步完成");
            }

        }

        /// <summary>
        /// 判断字符串是否是数字
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool IsNum(string s)
        {
            if (s.Trim() == "")
            {
                return false;
            }
            string pattern = "^[0-9]*$";
            Regex rx = new Regex(pattern);
            return rx.IsMatch(s);
        }

        /// <summary>
        /// 加载批次修改设置，已发送和以打印是否允许修改
        /// </summary>
        public void SetChangeBatch()
        {
            try
            {
                //MessageBox.Show(med1.Dgv_Info.Columns.Count.ToString());

                if (DB.GetPivasAllSet("批次-药单同步按钮") == "1")
                { btnSynDrugList.Visible = true; }
                else
                { btnSynDrugList.Visible = false; }

                if (DB.GetPivasAllSet("批次-瓶签生成按钮") == "1")
                { btnGenerate.Visible = true; }
                else
                { btnGenerate.Visible = false; }

                if (DB.GetPivasAllSet("批次-差异瓶签处理按钮") == "1")
                { button2.Visible = true; }
                else
                { button2.Visible = false; }

                if (DB.GetPivasAllSet("批次-批次重排按钮") == "1")
                { btnGennerationBatch.Visible = true; }
                else
                { btnGennerationBatch.Visible = false; }

                if (DB.GetPivasAllSet("批次-取消发送-按钮") == "1")
                {
                        //LCancelSend.Visible = true;
                    CS = true;
                }
                else
                { 
                    //LCancelSend.Visible = false;
                    CS = false;

                }

                if (DB.GetPivasAllSet("批次-批量修改批次-按钮") == "1")
                {
                    btnBatchModify.Visible= true;
                     BM = true;
                }
                else
                {
                    BM = false;
                    btnBatchModify.Visible = false; }

                if (DB.GetPivasAllSet("批次-界面设置") == "1")
                { btnSet.Visible = true; }
                else
                { btnSet.Visible = false; }

                if (DB.GetPivasAllSet("批次-快速修改批次") == "1" && pvb.PreviewMode !="0")
                { checkBox2.Visible = true; }
                else
                { checkBox2.Visible = false; }

                if (DB.GetPivasAllSet("批次-显示模式切换") == "1")
                { cbbMode.Visible = true; }
                else
                { cbbMode.Visible = false; }

                string svalue="";
                svalue = DB.GetPivasAllSet("批次-修改-已打印");
                if (svalue=="0")
                {
                    pvb.ChangePrintBatch = "0";
                }
                else
                {
                    pvb.ChangePrintBatch = "1";
                }

                svalue = DB.GetPivasAllSet("批次-修改-已发送");
                if (svalue == "0")
                {
                    pvb.ChangeSendBatch = "0";
                }
                else
                {
                    pvb.ChangeSendBatch = "1";
                }
                svalue = DB.GetPivasAllSet("批次-病区选中设置-列表");
                switch (svalue)
                {
                    case "0": pvb.WardDefaultSelectMode = "0"; break;
                    case "1": pvb.WardDefaultSelectMode = "1"; break;
                    case "2": pvb.WardDefaultSelectMode = "2"; break;
                    default: pvb.WardDefaultSelectMode = "1"; break;
                }
                
                
                svalue = DB.GetPivasAllSet("批次-当日所有药品-标题-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    med1.Dgv_Info.ColumnHeadersVisible = false;
                }
                else
                {
                    med1.Dgv_Info.ColumnHeadersVisible = true;
                    //标题字体大小设置
                    svalue = DB.GetPivasAllSetValue2("批次-当日所有药品-标题-画面显示");
                    if (!IsNum(svalue) || svalue == "0")
                    {
                        med1.Dgv_Info.RowTemplate.DefaultCellStyle.Font = new Font("宋体", 9);
                    }
                    else
                    {
                        med1.Dgv_Info.Font = new System.Drawing.Font("宋体", Convert.ToInt32(svalue));
                    }
                    //标题高度设置
                    svalue = DB.GetPivasAllSetValue3("批次-当日所有药品-标题-画面显示");
                    if (!IsNum(svalue) || svalue == "0")
                    {
                        med1.Dgv_Info.ColumnHeadersHeight = 18;
                    }
                    else
                    {
                        med1.Dgv_Info.ColumnHeadersHeight = Convert.ToInt32(svalue);
                    }
                }

                svalue = DB.GetPivasAllSet("批次-当日所有药品-状态-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    med1.Dgv_Info.Columns["colStatus"].Visible = false;                        
                }
                else
                {
                    med1.Dgv_Info.Columns["colStatus"].Width = Convert.ToInt32(svalue);
                }

                svalue = DB.GetPivasAllSet("批次-当日所有药品-药品名-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    med1.Dgv_Info.Columns["colDrug"].Visible = false;
                }
                else
                {
                    med1.Dgv_Info.Columns["colDrug"].Width = Convert.ToInt32(svalue);
                }

                svalue = DB.GetPivasAllSet("批次-当日所有药品-用量-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    med1.Dgv_Info.Columns["colYongliang"].Visible = false;
                }
                else
                {
                    med1.Dgv_Info.Columns["colYongliang"].Width = Convert.ToInt32(svalue);
                }
                svalue = DB.GetPivasAllSet("批次-当日所有药品-批次-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    med1.Dgv_Info.Columns["colBatch1"].Visible = false;
                }
                else
                {
                    med1.Dgv_Info.Columns["colBatch1"].Width = Convert.ToInt32(svalue);
                }
               svalue = DB.GetPivasAllSetValue2("批次-当日所有药品-批次-画面显示");
               if (!IsNum(svalue) || svalue == "0")
               {
                  pvb.combineBatch = "0";
               }
               else
               {
                  pvb.combineBatch = "1";
               }
                svalue = DB.GetPivasAllSet("批次-当日所有药品-患者名-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    med1.Dgv_Info.Columns["colPatient"].Visible = false;
                }
                else
                {
                    med1.Dgv_Info.Columns["colPatient"].Width = Convert.ToInt32(svalue);
                }

                svalue = DB.GetPivasAllSet("批次-当日所有药品-病区-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    med1.Dgv_Info.Columns["colWardName"].Visible = false;
                }
                else
                {
                    med1.Dgv_Info.Columns["colWardName"].Width = Convert.ToInt32(svalue);
                }

                svalue = DB.GetPivasAllSet("批次-当日所有药品-床号-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    med1.Dgv_Info.Columns["colBedNo"].Visible = false;
                }
                else
                {
                    med1.Dgv_Info.Columns["colBedNo"].Width = Convert.ToInt32(svalue);
                }

                svalue = DB.GetPivasAllSet("批次-当日所有药品-组号-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    med1.Dgv_Info.Columns["colGroupNo"].Visible = false;
                }
                else
                {
                    med1.Dgv_Info.Columns["colGroupNo"].Width = Convert.ToInt32(svalue);
                }

                svalue = DB.GetPivasAllSet("批次-当日所有药品-频次-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    med1.Dgv_Info.Columns["colFreq"].Visible = false;
                }
                else
                {
                    med1.Dgv_Info.Columns["colFreq"].Width = Convert.ToInt32(svalue);
                }

                //svalue = DB.GetPivasAllSet("批次-当日所有药品-溶媒-画面显示");
                //if (!IsNum(svalue) || svalue == "0")
                //{
                //    med1.Dgv_Info.Columns["colMenstruum"].Visible = false;
                //}
                //else
                //{
                //    med1.Dgv_Info.Columns["colMenstruum"].Width = Convert.ToInt32(svalue);
                //}

                svalue = DB.GetPivasAllSet("批次-瓶签明细-标题-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    med1.dgvCurrentDrug.ColumnHeadersVisible = false;
                }
                else
                {
                    med1.dgvCurrentDrug.ColumnHeadersVisible =true;

                    //标题字体大小设置
                    svalue = DB.GetPivasAllSetValue2("批次-瓶签明细-标题-画面显示");
                    if (!IsNum(svalue) || svalue == "0")
                    {
                        med1.dgvCurrentDrug.Font = new Font("宋体", 9);
                    }
                    else
                    {
                        med1.dgvCurrentDrug.Font = new System.Drawing.Font("宋体", Convert.ToInt32(svalue));
                    }
                    //标题高度设置
                    svalue = DB.GetPivasAllSetValue3("批次-瓶签明细-标题-画面显示");
                    if (!IsNum(svalue) || svalue == "0")
                    {
                        med1.dgvCurrentDrug.ColumnHeadersHeight = 18;
                    }
                    else
                    {
                        med1.dgvCurrentDrug.ColumnHeadersHeight = Convert.ToInt32(svalue);
                    }
                }
                svalue = DB.GetPivasAllSet("批次-瓶签明细-药品名-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    med1.dgvCurrentDrug.Columns["colDrugName"].Visible = false;
                }
                else
                {
                    med1.dgvCurrentDrug.Columns["colDrugName"].Width = Convert.ToInt32(svalue);
                }
                svalue = DB.GetPivasAllSet("批次-瓶签明细-规格-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    med1.dgvCurrentDrug.Columns["colSpec"].Visible = false;
                }
                else
                {
                    med1.dgvCurrentDrug.Columns["colSpec"].Width = Convert.ToInt32(svalue);
                }
                svalue = DB.GetPivasAllSet("批次-瓶签明细-剂量-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    med1.dgvCurrentDrug.Columns["coldDosage"].Visible = false;
                }
                else
                {
                    med1.dgvCurrentDrug.Columns["coldDosage"].Width = Convert.ToInt32(svalue);
                }
                //svalue = DB.GetPivasAllSet("批次-瓶签明细-单位-画面显示");
                //if (!IsNum(svalue) || svalue == "0")
                //{
                //    med1.dgvCurrentDrug.Columns["colUnit"].Visible = false;
                //}
                //else
                //{
                //    med1.dgvCurrentDrug.Columns["colUnit"].Width = Convert.ToInt32(svalue);
                //}
                svalue = DB.GetPivasAllSet("批次-瓶签明细-数量-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    med1.dgvCurrentDrug.Columns["colDgNo"].Visible = false;
                }
                else
                {
                    med1.dgvCurrentDrug.Columns["colDgNo"].Width = Convert.ToInt32(svalue);
                }

                svalue = DB.GetPivasAllSet("批次-瓶签明细-皮试-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    med1.dgvCurrentDrug.Columns["colPishi"].Visible = false;
                }
                else
                {
                    med1.dgvCurrentDrug.Columns["colPishi"].Width = Convert.ToInt32(svalue);
                }

                svalue = DB.GetPivasAllSet("批次-瓶签明细-备注-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    med1.dgvCurrentDrug.Columns["Remark7"].Visible = false;
                }
                else
                {
                    med1.dgvCurrentDrug.Columns["Remark7"].Width = Convert.ToInt32(svalue);
                }

                svalue = DB.GetPivasAllSet("批次-瓶签明细-字体大小-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    med1.dgvCurrentDrug.DefaultCellStyle.Font = new Font("宋体", 9);
                }
                else
                {
                    //med1.dgvCurrentDrug.DefaultCellStyle.Font = new Font("宋体", Convert.ToInt32(svalue));
                    string i = DB.GetPivasAllSetValue3("批次-瓶签明细-字体大小-画面显示").ToString().Trim();//是否设置成粗体判断
                    if (i == "1")
                    {
                        med1.dgvCurrentDrug.RowTemplate.DefaultCellStyle.Font = new Font("宋体", Convert.ToInt32(svalue), FontStyle.Bold);
                    }
                    else if (i == "2")
                    {
                        med1.dgvCurrentDrug.RowTemplate.DefaultCellStyle.Font = new Font("宋体", Convert.ToInt32(svalue), FontStyle.Italic);
                    }
                    else if (i == "3")
                    {
                        med1.dgvCurrentDrug.RowTemplate.DefaultCellStyle.Font = new Font("宋体", Convert.ToInt32(svalue), FontStyle.Underline);
                    }
                    else
                    {
                        med1.dgvCurrentDrug.RowTemplate.DefaultCellStyle.Font = new Font("宋体", Convert.ToInt32(svalue), FontStyle.Regular);
                    }
                }

                //行高设置
                svalue = DB.GetPivasAllSetValue2("批次-瓶签明细-字体大小-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    med1.dgvCurrentDrug.RowTemplate.Height = 23;
                }
                else
                {
                    med1.dgvCurrentDrug.RowTemplate.Height = Convert.ToInt32(svalue);
                }

                svalue = DB.GetPivasAllSet("批次-当日所有药品-字体大小-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    med1.Dgv_Info.RowTemplate.DefaultCellStyle.Font = new Font("宋体", 9);
                }
                else
                {
                    string i = DB.GetPivasAllSetValue3("批次-当日所有药品-字体大小-画面显示").ToString().Trim();//是否设置成粗体判断
                    if (i == "1")
                    {
                        med1.Dgv_Info.RowTemplate.DefaultCellStyle.Font = new Font("宋体", Convert.ToInt32(svalue), FontStyle.Bold);
                    }
                    else if (i == "2")
                    {
                        med1.Dgv_Info.RowTemplate.DefaultCellStyle.Font = new Font("宋体", Convert.ToInt32(svalue), FontStyle.Italic);
                    }
                    else if (i == "3")
                    {
                        med1.Dgv_Info.RowTemplate.DefaultCellStyle.Font = new Font("宋体", Convert.ToInt32(svalue), FontStyle.Underline);
                    }
                    else
                    {
                        med1.Dgv_Info.RowTemplate.DefaultCellStyle.Font = new Font("宋体", Convert.ToInt32(svalue), FontStyle.Regular);
                    }
                }

                //行高设置
                svalue = DB.GetPivasAllSetValue2("批次-当日所有药品-字体大小-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    med1.Dgv_Info.RowTemplate.Height = 23;
                }
                else
                {
                    med1.Dgv_Info.RowTemplate.Height = Convert.ToInt32(svalue);
                }

                svalue = DB.GetPivasAllSet("批次-列表模式瓶签-标题-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    dgv_Info.Dgv_Info.ColumnHeadersVisible = false;
                }
                else
                {
                    dgv_Info.Dgv_Info.ColumnHeadersVisible = true;
                    //标题字体大小设置
                    svalue = DB.GetPivasAllSetValue2("批次-列表模式瓶签-标题-画面显示");
                    if (!IsNum(svalue) || svalue == "0")
                    {
                        dgv_Info.Dgv_Info.RowTemplate.DefaultCellStyle.Font = new Font("宋体", 9);
                    }
                    else
                    {
                        dgv_Info.Dgv_Info.Font = new System.Drawing.Font("宋体", Convert.ToInt32(svalue));
                    }
                    //标题高度设置
                    svalue = DB.GetPivasAllSetValue3("批次-列表模式瓶签-标题-画面显示");
                    if (!IsNum(svalue) || svalue == "0")
                    {
                        dgv_Info.Dgv_Info.ColumnHeadersHeight = 18;
                    }
                    else
                    {
                        dgv_Info.Dgv_Info.ColumnHeadersHeight = Convert.ToInt32(svalue);
                    }
                }

                svalue = DB.GetPivasAllSet("批次-列表模式瓶签-字体大小-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    dgv_Info.Dgv_Info.RowTemplate.DefaultCellStyle.Font = new Font("宋体", 9);
                }
                else
                {
                    string i = DB.GetPivasAllSetValue3("批次-列表模式瓶签-字体大小-画面显示").ToString().Trim();//是否设置成粗体判断
                    if (i == "1")
                    {
                        dgv_Info.Dgv_Info.RowTemplate.DefaultCellStyle.Font = new Font("宋体", Convert.ToInt32(svalue), FontStyle.Bold);
                    }
                    else if (i == "2")
                    {
                        dgv_Info.Dgv_Info.RowTemplate.DefaultCellStyle.Font = new Font("宋体", Convert.ToInt32(svalue), FontStyle.Italic);
                    }
                    else if (i == "3")
                    {
                        dgv_Info.Dgv_Info.RowTemplate.DefaultCellStyle.Font = new Font("宋体", Convert.ToInt32(svalue), FontStyle.Underline);
                    }
                    else
                    {
                        dgv_Info.Dgv_Info.RowTemplate.DefaultCellStyle.Font = new Font("宋体", Convert.ToInt32(svalue), FontStyle.Regular);
                    }
                }
                //行高设置
                svalue = DB.GetPivasAllSetValue2("批次-列表模式瓶签-字体大小-画面显示");
                if (!IsNum(svalue) || svalue == "0")
                {
                    dgv_Info.Dgv_Info.RowTemplate.Height = 23;
                }
                else
                {
                    dgv_Info.Dgv_Info.RowTemplate.Height = Convert.ToInt32(svalue);
                }

                if (DB.GetPivasAllSet("批次-普抗化营") == "1" && pvb.PreviewMode != "0")
                {
                    flowLayoutPanel4.Visible = true;
                    string p=DB.GetPivasAllSetValue2("批次-普抗化营");
                    if (p.Length == 5)
                    {
                        checkBox3.Checked = p.Substring(0, 1) == "0" ? false : true;
                        checkBox4.Checked = p.Substring(1, 1) == "0" ? false : true;
                        checkBox5.Checked = p.Substring(2, 1) == "0" ? false : true;
                        checkBox6.Checked = p.Substring(3, 1) == "0" ? false : true;
                        checkBox7.Checked = p.Substring(4, 1) == "0" ? false : true;

                    }
                    else //不是4位 默认“1111”
                    {
                        checkBox3.Checked = true;
                        checkBox4.Checked = true;
                        checkBox5.Checked = true;
                        checkBox6.Checked = true;
                        checkBox7.Checked = true;
                    }
                }
                else
                {
                    flowLayoutPanel4.Visible = false;
                }

                svalue = DB.GetPivasAllSet("排批次-#");
                if (svalue == "0")
                {
                    S1 = "0";
                }
                else if (svalue == "1")
                {
                    S1 = "10";
                }
                else
                {
                    S1 = "16";
                }
                svalue = DB.GetPivasAllSet("排批次-K");
                if (svalue == "0")
                {
                    SK = "0";
                }
                else if (svalue == "1")
                {
                    SK = "10";
                }
                else
                {
                    SK = "16";
                }
                svalue = DB.GetPivasAllSet("排批次-L#");
                if (svalue == "0")
                {
                    LS1 = "0";
                }
                else if (svalue == "1")
                {
                    LS1 = "10";
                }
                else
                {
                    LS1 = "16";
                }
                svalue = DB.GetPivasAllSet("排批次-LK");
                if (svalue == "0")
                {
                    LSK = "0";
                }
                else if (svalue == "1")
                {
                    LSK  = "10";
                }
                else
                {
                   LSK = "16";
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private string GetDrugType()
        {
            string str = "";
            if (checkBox3.Checked == true)
            {
                str += "'1',";
            }
            if (checkBox4.Checked == true)
            {
                str += "'2',";
            }
            if (checkBox5.Checked == true)
            {
                str += "'3',";
            }
            if (checkBox6.Checked == true)
            {
                str += "'4',";
            }
            if (checkBox7.Checked)
            {
                str += "'5',";
            }
            if (str.Length > 0)
            {
                str = str.Substring(0, str.Length - 1);
            }
            return str;
        }

        private void btnBatchModify_Click(object sender, EventArgs e)
        {
            if (dgv_Info.Dgv_Info.Rows.Count > 0)
            {
              
                BatchModify bm = new BatchModify();
                bm.ShowDialog();
                if (bm.DialogResult == DialogResult.OK)
                {
                    for (int i = 0; i < dgv_Info.Dgv_Info.Rows.Count; i++)
                    {
                        string oldbatch = dgv_Info.Dgv_Info.Rows[i].Cells["批次"].Value.ToString();
                        string oldTeamNum = dgv_Info.Dgv_Info.Rows[i].Cells["TeamNumber"].Value.ToString();    
                        if (!string.IsNullOrEmpty(bm.teamNum) && !string.IsNullOrEmpty(bm.K_))
                        {
                            string batchRule = string.Format("批量修改--修改人:{0},由{1}修改为{2},修改时间为:{3}", pvb.DEmployeeName, oldbatch, oldTeamNum, DateTime.Now.ToString());
                            ChangeBatchModify(dgv_Info.Dgv_Info.Rows[i].Cells["瓶签号"].Value.ToString(), oldbatch, "'" + bm.teamNum + bm.K_ + "'", bm.teamNum, batchRule, bm.teamNum + bm.K_,bm.reason);
                            dgv_Info.Dgv_Info.Rows[i].Cells["批次"].Value = bm.teamNum + bm.K_;
                            dgv_Info.Dgv_Info.Rows[i].Cells["TeamNumber"].Value = bm.teamNum;
                        }
                        else if (!string.IsNullOrEmpty(bm.teamNum) && string.IsNullOrEmpty(bm.K_))
                        {
                            string batchRule = string.Format("批量修改--修改人:{0},由{1}批修改为{2}批,修改时间为:{3}",pvb.DEmployeeName, oldTeamNum, bm.teamNum, DateTime.Now.ToString());
                            ChangeBatchModify(dgv_Info.Dgv_Info.Rows[i].Cells["瓶签号"].Value.ToString(), oldbatch, "REPLACE(Batch, TeamNumber,'" + bm.teamNum + "')", bm.teamNum,batchRule,oldbatch.Replace(oldTeamNum,bm.teamNum),bm.reason);
                            dgv_Info.Dgv_Info.Rows[i].Cells["批次"].Value =oldbatch.Replace(oldTeamNum,bm.teamNum);
                            dgv_Info.Dgv_Info.Rows[i].Cells["TeamNumber"].Value = bm.teamNum;
                        }
                        else if (string.IsNullOrEmpty(bm.teamNum) && !string.IsNullOrEmpty(bm.K_))
                        {
                            string batchRule = string.Format("批量修改--修改人:{0},由{1}修改为{2},修改时间为:{3}",pvb.DEmployeeName, oldbatch,oldTeamNum+ bm.K_, DateTime.Now.ToString());
                            ChangeBatchModify(dgv_Info.Dgv_Info.Rows[i].Cells["瓶签号"].Value.ToString(), oldbatch, "'" + oldTeamNum + bm.K_ + "'",oldTeamNum,batchRule,oldTeamNum+bm.K_,bm.reason);
                            dgv_Info.Dgv_Info.Rows[i].Cells["批次"].Value = oldTeamNum + bm.K_;
                        }

                    }

                  
                }
            }
            else
            {
                MessageBox.Show("当前界面没有可修改的瓶签！","提示", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
        }

        private void LCancelSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (Check_BatchSaved.SelectedIndex == 1&&cbbMode.SelectedIndex==1)
                {
                    DialogResult result = MessageBox.Show("确定取消当前界面所发送的瓶签吗？", "确定取消当前界面所发送的瓶签吗？", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        for (int i = 0; i < dgv_Info.Dgv_Info.Rows.Count; i++)
                        {
                            ChangeBatchSend(dgv_Info.Dgv_Info.Rows[i].Cells["瓶签号"].Value.ToString());
                        }                     
                        ShowWard(0,false);
                    }
                }
                else
                {
                    MessageBox.Show("请选择\"已发送\"状态！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10038:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            
            }
        }
        /// <summary>
        /// 取消当前瓶签的发送状态
        /// </summary>
        /// <param name="labelno"></param>
        private void ChangeBatchSend(string labelno)
        {
            try
            {
                string sql = string.Format(" update IVRecord set BatchSaved=0,BatchSavedDT=null where LabelNo='{0}'", labelno);
                string sql1 = string.Format("insert into IVRecord_CancelSend(DemployId,LabelNo,CaneCelTime) values('{0}','{1}',GETDATE())"
                    , pvb.DEmployeeID, labelno);
                int b = DB.SetPIVAsDB(sql1);

                int a = DB.SetPIVAsDB(sql);
              
            }
            catch (Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10037:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        
        }

        private void ChangeBatchModify(string labeno, string oldBatch, string newbatch, string newteamNum, string batchRule, string newBatch1,string reason)
        {
            try
            {
               
                  string sql = string.Format("update IVRecord set Batch={0},BatchRule='{1}',TeamNumber={2} where LabelNo='{3}'",newbatch, batchRule,newteamNum, labeno);
                  DB.SetPIVAsDB(sql);
                  string sql1 = string.Format("insert into OrderChangeLog([LabelNo],[ChangeDT],[DEmployeeID],[DEmployeeName],[old],[new],[IVStatus],[Reason],ReasonDetail)" +
                              "values('{0}',GETDATE(),'{1}','{2}','{3}','{4}','{5}','批量修改','{6}')", labeno, pvb.DEmployeeID, pvb.DEmployeeName, oldBatch,
                               newBatch1, '0',reason);
                  DB.SetPIVAsDB(sql1);
            }
            catch (Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10038:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }

        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            SelectBatch();

            ShowWard(0, true);
            ShowDrug(pvb.ward, true);
        }

        #region 接口实现
        void IMenuManager.menuBeforeSelect()
        {
            //显示模式
            TGetType();
            //显示时间
            ShowDateTime();
            SelectBatch();

            //判断是否进入批次画面自动生成瓶签
            Check_BatchSaved.Text = "未发送";
            Check_IsSame.Text = "有/未改动";
            pvb.Choose = "#/K/L";

            AddWardArea();

            cbbWardArea.SelectedIndex = 0;//默认全部病区组

            Initing = true;
            if (pvb.PreviewMode == "0")
            {
                cbbMode.SelectedIndex = 0;
                flowLayoutPanel3.Visible = false;
                flowLayoutPanel4.Visible = false;
                checkBox2.Visible = false;
            }
            else
            {
                cbbMode.SelectedIndex = 1;
                flowLayoutPanel3.Visible = true;
                flowLayoutPanel4.Visible = true;
                checkBox2.Visible = true;
            }
            Initing = false;

            //详细数据置顶置顶
            ChangeChoes = true;
            timer1.Enabled = true;
            timer2.Enabled = true;

            SetChangeBatch();
        }

        void IMenuManager.menuAfterSelect()
        {
        }
        #endregion
    }
}