using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text.RegularExpressions;
using ChargeInterface;
using System.IO;
using PivasHisInterface;
using PIVAsCommon.Helper;
using PivasLimitDES;

namespace PivasLabelSelect
{
    public partial class PivasLabel : Form
    {
        public string Time = "300";
        string ischargePZ = "2"; //0是未计费,1是已计费，2是全部
        string chargeType = "";  //存放计费类型
        public int re6 = 0;
        private int serbtn1 = 1;
        private int serbtn2 = 1;
        private int serbtn3 = 1;
        private string UserID;
        private static DB_Help dbHelp = new DB_Help();
        private static SqlStr SQLStr = new SqlStr();
        private DataTable dt = new DataTable();  //瓶签号集合
        private DataTable DTWard = new DataTable();
        private string EmployeeID = "";
        FilterLabel fk;
        FilterLabel PCFilterLabel;
        private int PackOverFor = 0, ReTreatOverFor = 0;
        private string WardCode = "";
        int ChoseCount = 4;
        private string Useful1 = string.Empty;
        private string Useful2 = string.Empty;
        private string Useful3 = string.Empty;
        private string Useless1 = string.Empty;
        private string Useless2 = string.Empty;
        private string Useless3 = string.Empty;
        private string LaseSelectLabel = string.Empty;

        public PivasLabel()
        {
           
            InitializeComponent();
            Init();
            SetClassLong(Handle, GCL_STYLE, GetClassLong(Handle, GCL_STYLE) | CS_DropSHADOW);
            clear();
            this.EmployeeID = "1";
        }

        public PivasLabel(string UserID,string EmployeeID)
        {           
            InitializeComponent();
            Init();
            this.UserID = UserID;
            label18.Text = UserID;
            this.EmployeeID = EmployeeID;
            SetClassLong(Handle, GCL_STYLE, GetClassLong(Handle, GCL_STYLE) | CS_DropSHADOW);
           
        }
        
        #region    移动窗体用到的变量
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MOVE = 0xF010;
        private const int HTCAPTION = 0x0002;
        private const int CS_DropSHADOW = 0x20000;
        private const int GCL_STYLE = (-26);
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        [DllImport("user32.dll")]
        private static extern int SetClassLong(IntPtr hwnd, int nlndex, int dwNewLong);
        [DllImport("user32.dll")]
        private static extern int GetClassLong(IntPtr hwnd, int nlndex);

        #endregion

        #region   画面效果触发
        /// <summary>
        /// 关闭按钮——鼠标移上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel_Close_MouseHover(object sender, EventArgs e)
        {
            //Panel_Close.BackColor = Color.FromArgb(84, 199, 253);
                  Panel_Close.BackColor = Color.FromArgb(255, 192, 192);
       
        }

        /// <summary>
        /// 关闭按钮——鼠标离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel_Close_MouseLeave(object sender, EventArgs e)
        {
            Panel_Close.BackColor = Color.Transparent;
        }

        /// <summary>
        /// 关闭按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel_Close_Click(object sender, EventArgs e)
        {

            this.Dispose();

        }

        /// <summary>
        /// 最小化按钮——鼠标移上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pic_Min_MouseHover(object sender, EventArgs e)
        {
            Pic_Min.BackColor = Color.FromArgb(84, 199, 253);
        }

        /// <summary>
        /// 最小化按钮——鼠标离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pic_Min_MouseLeave(object sender, EventArgs e)
        {
            Pic_Min.BackColor = Color.Transparent;
        }
        private void Pic_Max_MouseHover(object sender, EventArgs e)
        {
            Pic_Max.BackColor = Color.FromArgb(84, 199, 253);
        }

        private void Pic_Max_MouseLeave(object sender, EventArgs e)
        {
            Pic_Max.BackColor = Color.Transparent;
        }


        /// <summary>
        /// 最小化按钮——按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pic_Min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// 画面移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        #endregion

        /// <summary>
        /// 显示病区列表
        /// </summary>
        /// <param name="Date">时间</param>
        /// <param name="Status">瓶签状态</param>
        /// <param name="Batch">批次 包括长期，临时，1批等</param>
        /// <param name="Sx">K，#</param>
        /// <param name="DrugType">普抗化营</param>
        public void BDdvgDWard(string Date,string Status, string Batch, string Sx,string DrugType)
        {
            try
            {
                int all = 0;
                StringBuilder SqlDward = new StringBuilder();
                SqlDward.Append("select distinct a.WardSimName WardName,a.Spellcode ,a.WardCode,isnull(b.DCount,0)as DCount,WardSeqNo,a.WardArea  from DWard a ");
                SqlDward.Append(" left join (select  COUNT(IVRecordID)as DCount,iv.WardCode   from V_IVRecord iv");             
                SqlDward.Append(" left join Prescription p on iv.PrescriptionID=p.PrescriptionID");
                if (ischargePZ != "2" )
                {
                    SqlDward.Append("  left join ChargeRemark cr on cr.LabelNo=iv.LabelNo ");
                }
                SqlDward.Append(" where 1=1");
             

                if (Date != "")
                {
                    if (cb_hour1.Text != "" || cb_hour2.Text != "")
                    {
                        string hour1 = cb_hour1.Text == "" ? "00" : cb_hour1.Text;
                        string hour2 = cb_hour2.Text == "" ? "24" : cb_hour2.Text;
                        hour1 = int.Parse(hour1) > int.Parse(hour2) ? hour2 : hour1;
                        hour2 = int.Parse(hour1) > int.Parse(hour2) ? hour1 : hour2;
                        hour1 = Date + " " + (hour1 + ":00").Replace("24:00", "23:59");
                        hour2 = Date + " " + (hour2 + ":59").Replace("24:59", "23:59");
                        SqlDward.Append(" and DATEDIFF(HH,InfusionDT,'" + hour1 + "')<=0 ");
                        SqlDward.Append(" and DATEDIFF(HH,InfusionDT,'" + hour2 + "')>=0 ");
                    }
                    else
                    {
                        SqlDward.Append(" and DATEDIFF(DD,InfusionDT,'" + Date + "')=0 ");
                    }
                }
                
                if (Sx != "" && Sx != "<全部>")
                {
                    string NewSX = Sx.Replace("筛选:", "").Trim();
                    SqlDward.Append(" and Batch like '%" + NewSX + "%'");
                }
                if (Batch != "" && Batch != "<全部>")
                    SqlDward.Append(SqlCob1(Batch));
                if (Status != "<全部>" && Status != "" && Status != "状态:<全部>")
                    SqlDward.Append(SqlCob3(Status));
                else
                    SqlDward.Append(" and (PackAdvance != 1 or Packadvance is null)");

                if (DrugType != "")
                {
                    DrugType= DrugType.Replace("普", "1").Replace("抗", "2").Replace("化", "3").Replace("营", "4").Replace("中","5");
                    SqlDward.Append(" and p.DrugType in (" + DrugType + ")");
                }
                if (ischargePZ != "2")
                {
                    if (ischargePZ == "1")
                    { SqlDward.Append(" and cr.DrugType in (0" + chargeType + ") and cr.Status='1'"); }
                    else if (ischargePZ == "0" && checkBox10.Checked)
                    {
                        SqlDward.Append(" and ( cr.Status is null or cr.Status !='1') and (cr.DrugType in (0" + chargeType + ") or cr.DrugType is null ) ");
                    }
                    else
                    {
                        SqlDward.Append(" and ( cr.Status is null or cr.Status='0' ) and cr.DrugType in (0" + chargeType + ")  ");
                    }
                }
               
                
                //SqlDward.Append("  group by WardCode,WardName)b on a.WardCode=b.WardCode");
                SqlDward.Append("  group by iv.WardCode)b on a.WardCode=b.WardCode");
                SqlDward.Append(" where IsOpen='1'");

                if (checkBox3.Checked == true)//仅显示有数据病区
                {SqlDward.Append("  and b.DCount >0 "); }

                SqlDward.Append(" order by WardSeqNo");
                #region
                    DataSet ds = new DataSet();
                    ds = dbHelp.GetPIVAsDB(SqlDward.ToString());
                    if (ds == null || ds.Tables.Count == 0)
                    {
                        return;
                    }
                    DataTable dtDward = ds.Tables[0];
                    ds.Dispose();
                    for (int i = 0; i < dtDward.Rows.Count; i++)
                    {
                        all = all + int.Parse(dtDward.Rows[i]["DCount"].ToString());
                    }
                    DataRow row = dtDward.NewRow();
                    row["WardName"] = "<全部>";
                    row["WardCode"] = "<全部>";
                    row["DCount"] = all;
                    dtDward.Rows.InsertAt(row, 0);
                    dgvDWard.DataSource = dtDward;
                    DTWard = dtDward;
                #endregion
                if (dgvDWard.Rows.Count > 0)
                {
                    dgvDWard.Columns["WardCode"].Visible = dgvDWard.Columns["Spellcode"].Visible = dgvDWard.Columns["WardArea"].Visible = false;
                    dgvDWard.Columns["DCount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                 
                    foreach (DataGridViewRow dr in dgvDWard.Rows) 
                    {
                        if (WardCode.Contains(dr.Cells["WardCode"].Value.ToString()))
                            dr.Cells[0].Value = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (GetPivasLimit.Instance.Limit(EmployeeID, "PivasLabelSelect"))
                {
                    BangdingCob1();
                    updatelist();
                    cmb4Add();
                    cbbList.SelectedIndex = 0;
                    GetPrintOrder();

                    if (dbHelp.GetPivasAllSet("瓶签查询-画面初始最大化") == "1")//设置画面初始化大小
                    {
                        MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
                        Pic_Max.BackgroundImage = Properties.Resources._3;
                        this.FormBorderStyle = FormBorderStyle.None;
                        this.WindowState = FormWindowState.Maximized;
                        for (int i = 0; i < flowLayoutPanel2.Controls.Count; i++)
                        {
                            flowLayoutPanel2.Controls[i].Width = Screen.PrimaryScreen.WorkingArea.Width - dgvDWard.Width;
                        }
                        this.flowLayoutPanel3.Width = this.Width - dgvDWard.Width - 18;
                        this.flowLayoutPanel4.Width = flowLayoutPanel3.Width;
                        this.flowLayoutPanel5.Width = flowLayoutPanel3.Width;
                        //this.flowLayoutPanel6.Width = flowLayoutPanel3.Width;
                        this.flowLayoutPanel7.Width = flowLayoutPanel3.Width;
                        Line1.Width = flowLayoutPanel3.Width;
                        Line2.Width = flowLayoutPanel3.Width;
                        Line3.Width = flowLayoutPanel3.Width;
                        Line4.Width = flowLayoutPanel3.Width;
                    }
                    if (dbHelp.GetPivasAllSetValue2("瓶签查询-按钮一") == "1")
                    {
                        button1.Visible = true;
                        button1.Text = dbHelp.GetPivasAllSet("瓶签查询-按钮一");
                        Match m = Regex.Match(dbHelp.GetPivasAllSetValue3("瓶签查询-按钮一"), "^([0-9]{0,3},){2}[0-9]{0,3}$");
                        if (m.Success)
                        {
                            //if (db.GetPivasAllSetValue3("瓶签查询-按钮一") like [[0-9]{1,3},]{2},[0-9]{1,3})
                            //string c = db.GetPivasAllSetValue3("瓶签查询-按钮一");
                            string[] a = dbHelp.GetPivasAllSetValue3("瓶签查询-按钮一").Split(',');
                            button1.ForeColor = System.Drawing.Color.FromArgb(Convert.ToInt32(a[0]), Convert.ToInt32(a[1]), Convert.ToInt32(a[2]));
                        }
                        else
                        {
                            //MessageBox.Show("瓶签查询-按钮一的颜色配置格式错误！");
                            button1.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
                        }
                    }
                    if (dbHelp.GetPivasAllSetValue2("瓶签查询-按钮二") == "1")
                    {
                        button2.Visible = true;
                        button2.Text = dbHelp.GetPivasAllSet("瓶签查询-按钮二");
                        Match m = Regex.Match(dbHelp.GetPivasAllSetValue3("瓶签查询-按钮二"), "^([0-9]{0,3},){2}[0-9]{0,3}$");
                        if (m.Success)
                        {
                            string[] a = dbHelp.GetPivasAllSetValue3("瓶签查询-按钮二").Split(',');
                            button2.ForeColor = System.Drawing.Color.FromArgb(Convert.ToInt32(a[0]), Convert.ToInt32(a[1]), Convert.ToInt32(a[2]));
                        }
                        else
                        {
                            // MessageBox.Show("瓶签查询-按钮二的颜色配置格式错误！");
                            button2.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
                        }

                    }
                    if (dbHelp.GetPivasAllSetValue2("瓶签查询-按钮三") == "1")
                    {
                        button3.Visible = true;
                        button3.Text = dbHelp.GetPivasAllSet("瓶签查询-按钮三");
                        Match m = Regex.Match(dbHelp.GetPivasAllSetValue3("瓶签查询-按钮三"), "^([0-9]{0,3},){2}[0-9]{0,3}$");
                        if (m.Success)
                        {
                            string[] a = dbHelp.GetPivasAllSetValue3("瓶签查询-按钮三").Split(',');
                            button3.ForeColor = System.Drawing.Color.FromArgb(Convert.ToInt32(a[0]), Convert.ToInt32(a[1]), Convert.ToInt32(a[2]));
                        }
                        else
                        {
                            // MessageBox.Show("瓶签查询-按钮三的颜色配置格式错误！");
                            button3.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
                        }
                    }
                    if (dbHelp.GetPivasAllSet("第三方瓶签") == "1")
                    {
                        re6 = 1;
                        dgvPre.Columns["remark6"].Visible = true;
                    }
                    else
                    {
                        re6 = 0;
                    }

                    serbtn1 = Convert.ToInt32(dbHelp.GetPivasAllSet("瓶签查询-按钮一-调用"));
                    serbtn2 = Convert.ToInt32(dbHelp.GetPivasAllSet("瓶签查询-按钮二-调用"));
                    serbtn3 = Convert.ToInt32(dbHelp.GetPivasAllSet("瓶签查询-按钮三-调用"));

                    Useful1 = dbHelp.GetPivasAllSet("打印-瓶签查询-瓶签状态显示配置-可打印").ToString() == "" ? "未配置" : dbHelp.GetPivasAllSet("打印-瓶签查询-瓶签状态显示配置-可打印").ToString();
                    Useful2 = dbHelp.GetPivasAllSetValue2("打印-瓶签查询-瓶签状态显示配置-可打印").ToString() == "" ? "未配置" : dbHelp.GetPivasAllSetValue2("打印-瓶签查询-瓶签状态显示配置-可打印").ToString();
                    Useful3 = dbHelp.GetPivasAllSetValue3("打印-瓶签查询-瓶签状态显示配置-可打印").ToString() == "" ? "未配置" : dbHelp.GetPivasAllSetValue3("打印-瓶签查询-瓶签状态显示配置-可打印").ToString();
                    Useless1 = dbHelp.GetPivasAllSet("打印-瓶签查询-瓶签状态显示配置-不可打印").ToString() == "" ? "未配置" : dbHelp.GetPivasAllSet("打印-瓶签查询-瓶签状态显示配置-不可打印").ToString();
                    Useless2 = dbHelp.GetPivasAllSetValue2("打印-瓶签查询-瓶签状态显示配置-不可打印").ToString() == "" ? "未配置" : dbHelp.GetPivasAllSetValue2("打印-瓶签查询-瓶签状态显示配置-不可打印").ToString();
                    Useless3 = dbHelp.GetPivasAllSetValue3("打印-瓶签查询-瓶签状态显示配置-不可打印").ToString() == "" ? "未配置" : dbHelp.GetPivasAllSetValue3("打印-瓶签查询-瓶签状态显示配置-不可打印").ToString();
                }

                else
                {
                    this.Dispose();
                }


                addChargeType();
                

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 加载计费类型
        /// </summary>
        private void addChargeType()
        {
            try
            {
                panel10.Controls.Clear();             
                string sql = " select distinct DrugType.Drugtypeid,DrugType.DrugtypeName from DrugType inner join ChargeRemark cr on cr.DrugType=DrugType.Drugtypeid";
                DataSet ds = dbHelp.GetPIVAsDB(sql);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CheckBox cb = new CheckBox();
                        cb.Text = ds.Tables[0].Rows[i]["DrugtypeName"].ToString();
                        cb.Tag = ds.Tables[0].Rows[i]["Drugtypeid"].ToString();
                        cb.Size = new Size(45, 18);
                        cb.Location = new Point(0+45*i, 3);
                        cb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(180)))), ((int)(((byte)(43)))));
                        cb.Checked = true;
                        cb.CheckedChanged += new EventHandler(ChargeTypeCheckedChange);
                        panel10.Controls.Add(cb);
                    }                
                }
             
            }
            catch 
            { 
            
            }
        
        }
        private void ChargeTypeCheckedChange(object sender, EventArgs e)
        {
            ChargeTypeStr();
            BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text, comboBox5.Text);
            BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text, string.Empty, comboBox5.Text, cbbPrinted.Text);
        }

        private void ChargeTypeStr()
        {
            chargeType = "";
            foreach (CheckBox cb in panel10.Controls)
            {
                if (cb.Checked)
                { chargeType +=","+ cb.Tag.ToString(); }
            
            }
        
        }
        /// <summary>
        /// 刷新中间列表
        /// </summary>
        /// <param name="Date">瓶签日期</param>
        /// <param name="DWard">病区</param>
        /// <param name="status">瓶签状态</param>
        /// <param name="Batch">批次</param>
        /// <param name="Sx"></param>
        /// <param name="LabelQuery"></param>
        /// <param name="DrugType"></param>
        /// <param name="IsPrint"></param>
        private void BDDvg(string Date, string DWard, string status, string Batch, string Sx, string LabelQuery,string DrugType,string IsPrint)
        {
            try
            {
                StringBuilder SqlLabel = new StringBuilder();
                SqlLabel.Append("select iv.IVStatus, iv.LabelNo,iv.remark6,iv.WardName ,iv.BedNo,iv.PatName ,iv.Batch,iv.GroupNo,iv.PrescriptionID,");
                SqlLabel.Append("    iv.LabelOver,iv.WardRetreat,iv.Remark3,DD.DrugName,DD2.DrugName as MenstruumName  ");
                SqlLabel.Append("    ,case P.DrugType when 1 then '普'  when 2 then '抗' when 3 then '化' when 4 then '营' when 5 then '中' end AS Class  ");
                SqlLabel.Append(" ,chargetype=(select distinct dt.DrugtypeName from ChargeRemark cr left join DrugType dt on dt.Drugtypeid=cr.DrugType where cr.LabelNo=iv.LabelNo  )  ");
                SqlLabel.Append("   from V_IVRecord iv  ");
                SqlLabel.Append(" left join DDrug DD on iv.MarjorDrug=DD.DrugCode ");
                SqlLabel.Append(" left join DDrug DD2 on iv.Menstruum=DD2.DrugCode ");
              
                SqlLabel.Append(" left join Prescription P on P.GroupNo= iv.GroupNo  ");
                if (ischargePZ != "2")
                {
                    SqlLabel.Append("  left join ChargeRemark cr on cr.LabelNo=iv.LabelNo  ");
                }
                SqlLabel.Append(" where 1=1");
           
                if (DWard != "" && DWard != "<全部>")
                    SqlLabel.Append(" and iv.WardCode in ('" + DWard + "') ");
                if (Date != "")
                {
                   
                    if (cb_hour1.Text != "" || cb_hour2.Text != "")
                    {
                        string hour1 =cb_hour1.Text == "" ? "00" : cb_hour1.Text;
                        string hour2 =cb_hour2.Text == "" ? "24" : cb_hour2.Text;
                        hour1 = int.Parse(hour1) > int.Parse(hour2) ? hour2 : hour1;
                        hour2 = int.Parse(hour1) > int.Parse(hour2) ? hour1 : hour2;
                        hour1 = Date + " " + (hour1+":00").Replace("24:00","23:59");
                        hour2 = Date + " " + (hour2 + ":59").Replace("24:59", "23:59");
                        SqlLabel.Append(" and DATEDIFF(HH,InfusionDT,'" + hour1+ "')<=0 ");
                        SqlLabel.Append(" and DATEDIFF(HH,InfusionDT,'" + hour2 + "')>=0 ");
                    }
                    else
                    {
                        SqlLabel.Append(" and DATEDIFF(DD,InfusionDT,'" + Date + "')=0 ");
                    }
                }
                #region 配置费
                if (ischargePZ != "2")
                {
                    if (ischargePZ == "1")
                    { SqlLabel.Append(" and cr.DrugType in (0" + chargeType + ") and cr.Status='1'"); }
                    else if (ischargePZ == "0" && checkBox10.Checked)
                    {
                        SqlLabel.Append(" and ( cr.Status is null or cr.Status !='1') and (cr.DrugType in (0" + chargeType + ") or cr.DrugType is null ) ");
                    }
                    else
                    {
                        SqlLabel.Append(" and ( cr.Status is null or cr.Status='0' ) and cr.DrugType in (0" + chargeType + ")");
                    }
                }
             
                #endregion
                if (Sx != "" && Sx != "<全部>")
                {
                    string NewSX = Sx.Replace("筛选:", "").Trim();
                    SqlLabel.Append(" and Batch like '%" + NewSX + "%'");
                }
                if (Batch != "")
                    SqlLabel.Append(SqlCob1(Batch));
                if (status != "<全部>" && status != "" && status != "状态:<全部>")
                    SqlLabel.Append(SqlCob3(status));
                if (LabelQuery != string.Empty)//输入框模糊查询
                {
                    SqlLabel.Append(" AND (");
                    SqlLabel.Append(" iv.LabelNo like '%" + LabelQuery + "%' ");//瓶签号
                    SqlLabel.Append(" OR ");
                    if (re6 == 1)
                    {
                        SqlLabel.Append(" iv.remark6 like '%" + LabelQuery + "%' ");//第三方瓶签
                        SqlLabel.Append(" OR ");
                    }
                    SqlLabel.Append(" iv.BedNo like '%" + LabelQuery + "%' ");//床号
                    SqlLabel.Append(" OR ");
                    SqlLabel.Append(" iv.PatName like '%" + LabelQuery + "%' ");//患者姓名
                    SqlLabel.Append(" OR ");
                    SqlLabel.Append(" DD.DrugName like '%" + LabelQuery + "%' ");//主药
                    SqlLabel.Append(" OR ");
                    SqlLabel.Append(" DD2.DrugName like '%" + LabelQuery + "%' ");//溶媒
                    SqlLabel.Append(" OR ");
                    SqlLabel.Append(" iv.GroupNo like '%" + LabelQuery + "%' ");//医嘱号
                    SqlLabel.Append(" OR ");
                    SqlLabel.Append(" iv.PatCode like '%" + LabelQuery + "%' ");//医嘱号
                    SqlLabel.Append(" OR ");
                    SqlLabel.Append(" p.CaseID like '%" + LabelQuery + "%' ");//医嘱号
                    SqlLabel.Append(" ) "); 
                }
                if (DrugType != "")
                {
                   DrugType= DrugType.Replace("普", "1").Replace("抗", "2").Replace("化", "3").Replace("营", "4").Replace("中","5");
                   SqlLabel.Append(" and p.DrugType in (" + DrugType + ")");
                }

                SqlLabel.Append(" order by  iv.LabelNo");


                DataSet ds = new DataSet();
                ds = dbHelp.GetPIVAsDB(SqlLabel.ToString());
                if (ds == null || ds.Tables.Count == 0)
                {
                    dgvPre.Rows.Clear();
                    return;
                }
                dt = ds.Tables[0];
                ds.Dispose();
                dgvPre.Rows.Clear();
                //dgvPre.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                 
                    if (comboBox3.Text == "提前打包")
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dgvPre.Rows.Add(dt.Rows[i]);
                            dgvPre.Rows[i].Cells["LabelNo"].Value = dt.Rows[i]["LabelNo"].ToString().Trim();
                            dgvPre.Rows[i].Cells["remark6"].Value = dt.Rows[i]["remark6"].ToString().Trim();
                            dgvPre.Rows[i].Cells["BedNo"].Value = dt.Rows[i]["BedNo"].ToString().Trim();
                            dgvPre.Rows[i].Cells["PatName"].Value = dt.Rows[i]["PatName"].ToString().Trim();
                            dgvPre.Rows[i].Cells["DWard"].Value = dt.Rows[i]["WardName"].ToString().Trim();
                            dgvPre.Rows[i].Cells["GroupNo"].Value = dt.Rows[i]["GroupNo"].ToString().Trim();
                            dgvPre.Rows[i].Cells["Batch"].Value = dt.Rows[i]["Batch"].ToString().Trim();
                            dgvPre.Rows[i].Cells["IVStatus"].Value = "提前打包";
                            dgvPre.Rows[i].Cells["MainDrug"].Value = dt.Rows[i]["DrugName"].ToString().Trim();
                            dgvPre.Rows[i].Cells["MenstruumName"].Value = dt.Rows[i]["MenstruumName"].ToString().Trim();
                            dgvPre.Rows[i].Cells["PrescriptionID"].Value = dt.Rows[i]["PrescriptionID"].ToString().Trim();
                            dgvPre.Rows[i].Cells["Remark"].Value =CheckRemark(dt.Rows[i]["Remark3"].ToString().Trim());
                            dgvPre.Rows[i].Cells["Select"].Value = false;
                            dgvPre.Rows[i].Cells["Class"].Value = dt.Rows[i]["Class"].ToString().Trim();
                            dgvPre.Rows[i].Cells["chrgetype"].Value = dt.Rows[i]["chargetype"].ToString().Trim();
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dgvPre.Rows.Add(dt.Rows[i]);
                            dgvPre.Rows[i].Cells["LabelNo"].Value = dt.Rows[i]["LabelNo"].ToString().Trim();
                            dgvPre.Rows[i].Cells["remark6"].Value = dt.Rows[i]["remark6"].ToString().Trim();
                            dgvPre.Rows[i].Cells["BedNo"].Value = dt.Rows[i]["BedNo"].ToString().Trim();
                            dgvPre.Rows[i].Cells["PatName"].Value = dt.Rows[i]["PatName"].ToString().Trim();
                            dgvPre.Rows[i].Cells["DWard"].Value = dt.Rows[i]["WardName"].ToString().Trim();
                            dgvPre.Rows[i].Cells["GroupNo"].Value = dt.Rows[i]["GroupNo"].ToString().Trim();
                            dgvPre.Rows[i].Cells["Batch"].Value = dt.Rows[i]["Batch"].ToString().Trim();
                            dgvPre.Rows[i].Cells["IVStatus"].Value = CheckReturn(int.Parse(dt.Rows[i]["IVStatus"].ToString()), dt.Rows[i]["WardRetreat"].ToString(), int.Parse(dt.Rows[i]["LabelOver"].ToString()));
                            dgvPre.Rows[i].Cells["MainDrug"].Value = dt.Rows[i]["DrugName"].ToString().Trim();
                            dgvPre.Rows[i].Cells["MenstruumName"].Value = dt.Rows[i]["MenstruumName"].ToString().Trim();
                            dgvPre.Rows[i].Cells["PrescriptionID"].Value = dt.Rows[i]["PrescriptionID"].ToString().Trim();
                            dgvPre.Rows[i].Cells["Remark"].Value =CheckRemark(dt.Rows[i]["Remark3"].ToString().Trim());
                            dgvPre.Rows[i].Cells["Select"].Value = false;
                            dgvPre.Rows[i].Cells["Class"].Value = dt.Rows[i]["Class"].ToString().Trim();
                            dgvPre.Rows[i].Cells["chrgetype"].Value = dt.Rows[i]["chargetype"].ToString().Trim();   
                        }
                    }
                }
                checkBox2.Checked = false;
                GetPrintOrder();
            }
            catch 
            {
               // MessageBox.Show(ex.Message);
            }

        }


        private string CheckRemark(string index) 
        {
            string ret="";
            switch (index) 
            {
                case "0": ret = "未记账"; break;
                case "1": ret = Useless1; break;
                case "2": ret = "耗材未收"; break;
                case "3": ret = Useless2; break;
                case "4": ret = "配置费失败"; break;
                case "5": ret = "配置费成功"; break;
                case "6": ret = "扣库存成功"; break;
                case "7": ret = "扣库存失败"; break;
                case "8": ret = "记账失败"; break;
                case "9": ret = Useless3; break;
                case "10": ret = "可打印"; break;
                case "12": ret = "记账失败"; break;
                case "13": ret = "耗材未收"; break;
                case "14": ret = "耗材已收"; break;
                case "15": ret = "记账成功"; break;
                case "16": ret = "已记账"; break;
                case "17": ret = "扣库存成功"; break;
                case "18": ret = "扣库存失败"; break;
                case "19": ret = "配置费成功"; break;
                case "20": ret = "其他"; break;
                case "21": ret = "配置费失败"; break;
                case "22": ret = Useful1; break;
                case "23": ret = Useful2; break;
                case "24": ret = Useful3; break;
                default: break;

            }
            return ret;
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="WardRetreat"></param>
        /// <param name="LabelOver"></param>
        /// <returns></returns>
        private string CheckReturn(int a, string WardRetreat, int LabelOver)
        {
            if (WardRetreat == "1")
                return "费用已退";
            else if (WardRetreat == "2")
                return "已退药";
            else if (LabelOver < 0)
                return "配置取消";
            else
            {
                switch (a)
                {
                    case 0: return "未打印";
                    case 1: return "未打印";
                    case 2: return "未打印";
                    case 3: if (comboBox3.Text == "已摆药") 
                            {
                                if (cb_yp.Checked)
                                    return "摆溶剂";
                                else if (cb_ys.Checked)
                                    return "摆溶媒";
                                else return "已打印";
                            }
                            else return "已打印";
                    case 4: return "已摆药";
                    case 5: return "已排药";
                    case 7: return "已进仓";
                    case 9: return "已配置";
                    case 11: return "已出仓";
                    case 13: return "已打包";
                    case 15: return "已签收";
                    default:
                        break;
                }
            }
            return "";
        }

        private void BangdingCob1()
        {
            try
            {
                //comboBox1.Items.Clear();
                string SqlCob1 = "select distinct OrderID from DOrder  where IsValid='1'";
                DataTable DtCob1 = dbHelp.GetPIVAsDB(SqlCob1).Tables[0];
                if (DtCob1.Rows.Count > 0)
                {
                    for (int i = 0; i < DtCob1.Rows.Count; i++)
                    {
                        comboBox1.Items.Add("长期:" + DtCob1.Rows[i]["OrderID"] + "#");
                        //comboBox5.Items.Add(DtCob1.Rows[i]["OrderID"]);
                        PivasLabel la = new PivasLabel();
                        LinkLabel lab = la.linkLabel22;
                        lab.Text = "" + DtCob1.Rows[i]["OrderID"]+"批";
                        
                        flowLayoutPanel9.Controls.Add(lab);

                        lab.Visible = true;

                        lab.Click += new EventHandler(AddBatchTemp);
                        

                        la.Dispose();
                        int r= flowLayoutPanel4.Controls.Count;
                    }
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvDWard_Click(object sender, EventArgs e)
        {
            label17.Text = Time;
            if (dgvDWard.Rows.Count == 0)
                return;
            
            int i = dgvDWard.CurrentCell.RowIndex;//获取选中行的行号
            int columnIndex = dgvDWard.CurrentCell.ColumnIndex;//获取选中列

            if (columnIndex == 0)//勾选
            {


                if (Equals(dgvDWard.Rows[i].Cells[0].Value,true))
                    dgvDWard.Rows[i].Cells[0].Value = false;
                else
                    dgvDWard.Rows[i].Cells[0].Value = true;


                //全选的情况
                if (dgvDWard.Rows[i].Cells[1].Value.ToString() == "<全部>")
                {
                    for (int j = 1; j < dgvDWard.Rows.Count; j++)
                    {
                        if (Equals(dgvDWard.Rows[i].Cells[0].Value, true))
                        {
                            dgvDWard.Rows[j].Cells[0].Value = true;
                        }
                        else
                        {
                            dgvDWard.Rows[j].Cells[0].Value = false;
                        }
                    }
                }

                WardCode = "";
                foreach (DataGridViewRow dr in dgvDWard.Rows)
                {
                    if (dr.Cells[0].EditedFormattedValue.ToString().Equals("True"))
                        WardCode = WardCode + dr.Cells["WardCode"].Value.ToString() + "','";
                    // MessageBox.Show("fdfd");
                }
                WardCode = WardCode.TrimEnd(',') == "" ? dgvDWard.Rows[i].Cells["WardCode"].Value.ToString() : WardCode.TrimEnd(',');
            }
            //点击
            else 
            {

                WardCode = dgvDWard.Rows[i].Cells["WardCode"].Value.ToString();
            }


            
            try
            {
                
                BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text, string.Empty, comboBox5.Text,cbbPrinted.Text);
                // BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text);
            }
            catch { }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void cbo3()
        {
            switch (comboBox3.Text)
            {
                case"<全部>":
                   // checkBox1.Enabled = false;
                    //checkBox1.Checked = true;
                    label2.Visible = false;
                    checkBox9.Checked =true;
                    checkBox9.Visible = false;
                  //  cbbPrinted.Visible = false;
                  //  label19.Visible = false;
                    label5.Visible = true;
                    label5.Text = "退药";
                    checkBox8.Visible = false;
                    panel8.Visible = false;
                   
                    break;
                case "未打印":
                    //checkBox9.Checked = false;
                    checkBox9.Visible = true;
                   // checkBox1.Enabled = false;
                   // checkBox1.Checked = true;
                    lbCount.Visible = false;
                    label2.Visible = false;                    
                  //  cbbPrinted.Visible = false;
                   // label19.Visible = false;
                    label5.Visible = 0 <= ReTreatOverFor ? true :  false;
                    label5.Text = "退药";
                    checkBox8.Visible = false;
                    panel8.Visible = false;
                    break;
                case "已打印":
                    //checkBox9.Checked = false;
                    checkBox9.Visible = true;
                    //checkBox1.Enabled = true;
                   // checkBox1.Checked = true;
                    lbCount.Visible = false;
                    label2.Visible = false;                    
                    //cbbPrinted.Visible = false;
                   // label19.Visible = false;
                    label5.Visible = 3 < ReTreatOverFor ? true : false;
                    label5.Text = "退药";
                    checkBox8.Visible = false;
                    panel8.Visible = false;
                    break;
                case "已摆药":
                    //checkBox9.Checked = false;
                    checkBox9.Visible = true;
                    //checkBox1.Enabled = true;
                    // checkBox1.Checked = true;
                    lbCount.Visible = false;
                    label2.Visible = false;
                    //cbbPrinted.Visible = false;
                    // label19.Visible = false;
                    label5.Visible = 3 < ReTreatOverFor ? true : false;
                    label5.Text = "退药";
                    checkBox8.Visible = false;
                    panel8.Visible = true;
                    break;
                case "已排药":
                    //checkBox9.Checked = false;
                    checkBox9.Visible = true;
                   // checkBox1.Enabled = true;
                   // checkBox1.Checked = true;
                    lbCount.Visible = false;
                    label2.Visible = false;                    
                    //label19.Visible = false;
                    //cbbPrinted.Visible = false;
                    label5.Visible = 5 < ReTreatOverFor ? true : false;
                    label5.Text = "退药";
                    checkBox8.Visible = false;
                    panel8.Visible = false;
                    break;
                case "已进仓":
                    //checkBox9.Checked = false;
                    checkBox9.Visible = true;
                   // checkBox1.Enabled = true;
                   // checkBox1.Checked = true;
                    lbCount.Visible = false;
                    label2.Visible = false;                    
                    //cbbPrinted.Visible = false;
                   // label19.Visible = false;
                    label5.Visible = 7 < ReTreatOverFor ? true : false;
                    label5.Text = "退药";
                    checkBox8.Visible = false;
                    panel8.Visible = false;
                    break;
                case "已配置":
                    //checkBox9.Checked = false;
                    checkBox9.Visible = true;
                  //  checkBox1.Enabled = true;
                   // checkBox1.Checked = true;
                    lbCount.Visible = true;
                    label2.Visible = false;
                   // cbbPrinted.Visible = false;
                   // label19.Visible = false;
                    label5.Visible = 9 < ReTreatOverFor ? true : false;
                    label5.Text = "退药";
                    checkBox8.Visible = false;
                    panel8.Visible = false;
                    break;
                case "已出仓":
                    //checkBox9.Checked = false;
                    checkBox9.Visible = true;
                  //  checkBox1.Enabled = true;
                   // checkBox1.Checked = true;
                    lbCount.Visible = false;
                    label2.Visible = false;
                    //cbbPrinted.Visible = false;
                    //label19.Visible = false;
                    label5.Visible = 11 < ReTreatOverFor ? true : false;
                    label5.Text = "退药";
                    checkBox8.Visible = false;
                    panel8.Visible = false;
                    break;
                case "已打包":
                    //checkBox9.Checked = false;
                    checkBox9.Visible = true;
                  //  checkBox1.Enabled = true;
                    lbCount.Visible = true;
                    //checkBox1.Checked = true;
                    label5.Visible = 13 < ReTreatOverFor ? true : false;
                    label5.Text = "退药";
                    label2.Visible = false;
                    //cbbPrinted.Visible = false;
                    //label19.Visible = false;
                    checkBox8.Visible = false;
                    panel8.Visible = false;
                    break;
                case "已签收":
                    //checkBox9.Checked = false;
                    checkBox9.Visible = true;
                   // checkBox1.Enabled = false;
                   // checkBox1.Checked = true;
                    lbCount.Visible = false;
                    label2.Visible = false;
                   // cbbPrinted.Visible = false;
                   // label19.Visible = false;
                    label5.Visible = 15 <= ReTreatOverFor ? true : false;
                    label5.Text = "退药";
                    checkBox8.Visible = false;
                    panel8.Visible = false;
                    break;
                case "提前打包":
                    checkBox9.Checked = false;
                    checkBox9.Visible =false;
                    //checkBox1.Enabled = false;
                  //  checkBox1.Checked = true;
                    lbCount.Visible = true;
                    label2.Visible = false;
                    //cbbPrinted.Visible = true;
                   // cbbPrinted.SelectedIndex = 1;
                   // label19.Visible = true;
                    label5.Visible =  false;
                    label5.Text = "退药";
                    checkBox8.Visible = false;
                    panel8.Visible = false;
                    break;
                case "配置取消":
                    checkBox9.Checked = true;
                    checkBox9.Visible = false;
                    //checkBox1.Enabled = false;
                    //checkBox1.Checked = true;
                    label2.Visible = true;
                    label5.Visible = true;
                    lbCount.Visible = false;
                    checkBox8.Visible = true;
                  //  cbbPrinted.Visible = false;
                   // label19.Visible = false;
                    label5.Text = "审核";
                    panel8.Visible = false;
                    break;
                case "已退药":
                  
                    checkBox9.Visible = false;
                   // checkBox1.Enabled = false;
                    lbCount.Visible = false;
                    //checkBox1.Checked = true;
                    label2.Visible = false;
                    label5.Visible = false;
                   // cbbPrinted.Visible = true;
                    //cbbPrinted.SelectedIndex = 1;
                    //label19.Visible = true;
                    checkBox8.Visible = false;
                    panel8.Visible = false;
                    break;
            
                default:
                   
                    checkBox9.Visible = false;
                  //  checkBox1.Enabled = false;
                    lbCount.Visible = false;
                    //checkBox1.Checked = true;
                    label2.Visible = false;
                    label5.Visible = true;
                  //  cbbPrinted.Visible = false;
                    label5.Text = "退药";
                   // label19.Visible = false;
                    checkBox8.Visible = false;
                    panel8.Visible = false;
                    break;
            }

            

        }


        private void Combox5_TextChanged(object sender, EventArgs e)
        {
            label17.Text = Time;
            BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text,comboBox5.Text);
            BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text, string.Empty, comboBox5.Text,cbbPrinted.Text);
        }

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            label17.Text = Time;
            cbo3();
            /// <param name="Date">时间</param>
            /// <param name="Status">瓶签状态</param>
            /// <param name="Batch">批次</param>
            /// <param name="Sx">筛选：已打印,未打印</param>
            BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text, comboBox5.Text);
            BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text, string.Empty, comboBox5.Text,cbbPrinted.Text);
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            label17.Text = Time;
            BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text,comboBox5.Text);
            BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text,string.Empty,comboBox5.Text,cbbPrinted.Text );

        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            label17.Text = Time;
            BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text,comboBox5.Text);
            BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text,string.Empty,comboBox5.Text,cbbPrinted.Text );

        }
        private string SqlCob3(string Cob3Txt) 
        {
            Cob3Txt = Cob3Txt.Replace("状态:","").Trim();
            StringBuilder SqlCob3 = new StringBuilder();
            //
            if (checkBox1.Checked&&!checkBox9.Checked)
            {
                switch (Cob3Txt)
                {
                    case "未打印": SqlCob3.Append(" and IVStatus < 3 and LabelOver=0 ");
                	break;
                        case "已打印": SqlCob3.Append(" and IVStatus = 3 and (PackAdvance != 1 or Packadvance is null)  and LabelOver=0 ");
                    break;
                        case "已摆药": 
                            if(cb_yp.Checked&&cb_ys.Checked)
                                SqlCob3.Append(" and IVStatus = 4 and LabelOver=0 ");
                            else if (cb_yp.Checked)
                                SqlCob3.Append(" and IVStatus=3 and iv.labelno in (select distinct labelno from dbo.IVRecod_YP_Check where SolventCheck='1') and LabelOver=0 ");
                            else if (cb_ys.Checked)
                                SqlCob3.Append(" and IVStatus=3 and iv.labelno in (select distinct labelno from dbo.IVRecod_YP_Check where MenstruumCheck='1') and LabelOver=0 ");
                            else
                                SqlCob3.Append(" and IVStatus = 4 and LabelOver=0 ");
                            SqlCob3.Append(" and (PackAdvance != 1 or Packadvance is null)");
                	break;
                        case "已排药": SqlCob3.Append(" and IVStatus = 5 and (PackAdvance != 1 or Packadvance is null) and LabelOver=0 ");
                	break;
                        case "已进仓": SqlCob3.Append(" and IVStatus = 7 and (PackAdvance != 1 or Packadvance is null) and LabelOver=0 ");    
                	break;
                        case "已配置": SqlCob3.Append(" and IVStatus = 9 and (PackAdvance != 1 or Packadvance is null) and LabelOver=0 ");    
                	break;
                        case "已出仓": SqlCob3.Append(" and IVStatus = 11 and (PackAdvance != 1 or Packadvance is null) and LabelOver=0 ");    
                	break;
                        case "已打包": SqlCob3.Append(" and IVStatus = 13 and (PackAdvance != 1 or Packadvance is null) and LabelOver=0 ");    
                	break;
                        case "已签收": SqlCob3.Append(" and IVStatus = 15 and (PackAdvance != 1 or Packadvance is null) and LabelOver=0 ");
                	break;
                        case "已退药":
                            switch(cbbPrinted.SelectedIndex)
                            {
                                case 0: SqlCob3.Append(" and WardRetreat='2' ");
                                    break;
                                case 1: SqlCob3.Append(" and WardRetreat='2'  and IVStatus >=3 ");
                                    break;
                                case 2: SqlCob3.Append(" and WardRetreat='2'  and IVStatus < 3 ");
                                    break;
                                default: SqlCob3.Append(" and WardRetreat='2'  ");
                                    break;
                            }
                            
                	break;
                        case "配置取消":
                            switch(cbbPrinted.SelectedIndex)
                            {case 0: SqlCob3.Append(" and LabelOver<0 and WardRetreat!='2'  ");
                                    break;
                            case 1: SqlCob3.Append(" and LabelOver<0 and WardRetreat!='2' and IVStatus >=3 ");
                                    break;
                            case 2: SqlCob3.Append(" and LabelOver<0 and WardRetreat!='2'  and IVStatus < 3 ");
                                    break;
                            default: SqlCob3.Append(" and LabelOver<0 and WardRetreat!='2'");
                                    break;
                              }
                         if(checkBox8.Checked==true)
                             SqlCob3.Append(" and ISNUMERIC(iv.remark3)>0 and  iv.remark3>=10");
                                         //SqlCob3.Append(" and (PackAdvance != 1 or Packadvance is null) ");
                        //case "配置取消": SqlCob3.Append(" and LabelOver<0 and WardRetreat!='2' and IVStatus>=3 and PackAdvance != 1 ");
                	break;
                        case "提前打包": 
                    switch (cbbPrinted.SelectedIndex)
                    {
                        case 0: SqlCob3.Append(" and PackAdvance = 1 and (LabelOver>=0 and WardRetreat=0 )");
                            break;
                        case 1: SqlCob3.Append(" and PackAdvance = 1  and IVStatus >=3 and (LabelOver>=0 and WardRetreat=0 )");
                            break;
                        case 2: SqlCob3.Append(" and PackAdvance = 1 and IVStatus < 3 and (LabelOver>=0 and WardRetreat=0 )");
                            break;
                        default: SqlCob3.Append(" and PackAdvance = 1 and (LabelOver>=0 and WardRetreat=0 )");
                            break;
                    }
                	break;
                }                
            }
            else if (!checkBox9.Checked && !checkBox1.Checked)
            {
                switch (Cob3Txt)
                {
                    case "未打印": SqlCob3.Append(" and IVStatus < 3 and LabelOver=0 ");
                        break;
                    case "已打印": SqlCob3.Append(" and IVStatus >= 3 and (PackAdvance != 1 or Packadvance is null) and LabelOver=0 ");
                        break;
                    case "已摆药":
                        if (cb_yp.Checked && cb_ys.Checked)
                            SqlCob3.Append(" and IVStatus >=4 and LabelOver=0 ");
                        else if (cb_yp.Checked)
                            SqlCob3.Append(" and IVStatus >=3 and iv.labelno in (select distinct labelno from dbo.IVRecod_YP_Check where SolventCheck='1') and LabelOver=0 ");
                        else if (cb_ys.Checked)
                            SqlCob3.Append(" and IVStatus >=3 and iv.labelno in (select distinct labelno from dbo.IVRecod_YP_Check where MenstruumCheck='1') and LabelOver=0 ");
                        else
                            SqlCob3.Append(" and IVStatus >=4 and LabelOver=0 ");
                        SqlCob3.Append(" and (PackAdvance != 1 or Packadvance is null)");
                        break;
                    case "已排药": SqlCob3.Append(" and IVStatus >= 5 and (PackAdvance != 1 or Packadvance is null) and LabelOver=0 ");
                        break;
                    case "已进仓": SqlCob3.Append(" and IVStatus >= 7 and (PackAdvance != 1 or Packadvance is null) and LabelOver=0  ");
                        break;
                    case "已配置": SqlCob3.Append(" and IVStatus >= 9 and (PackAdvance != 1 or Packadvance is null) and LabelOver=0 ");
                        break;
                    case "已出仓": SqlCob3.Append(" and IVStatus >= 11 and (PackAdvance != 1 or Packadvance is null) and LabelOver=0 ");
                        break;
                    case "已打包": SqlCob3.Append(" and IVStatus >= 13 and (PackAdvance != 1 or Packadvance is null) and LabelOver=0 ");
                        break;
                    case "已签收": SqlCob3.Append(" and IVStatus = 15 and (PackAdvance != 1 or Packadvance is null) and LabelOver=0 ");
                        break;
                    case "已退药":
                        switch (cbbPrinted.SelectedIndex)
                        {
                            case 0: SqlCob3.Append(" and WardRetreat='2' ");
                                break;
                            case 1: SqlCob3.Append(" and WardRetreat='2'  and IVStatus >=3 ");
                                break;
                            case 2: SqlCob3.Append(" and WardRetreat='2'  and IVStatus < 3 ");
                                break;
                            default: SqlCob3.Append(" and WardRetreat='2'  ");
                                break;
                        }
                        break;
                    case "配置取消":
                        switch (cbbPrinted.SelectedIndex)
                        {
                            case 0: SqlCob3.Append(" and LabelOver<0 and WardRetreat!='2'  ");
                                break;
                            case 1: SqlCob3.Append(" and LabelOver<0 and WardRetreat!='2' and IVStatus >=3 ");
                                break;
                            case 2: SqlCob3.Append(" and LabelOver<0 and WardRetreat!='2'  and IVStatus < 3 ");
                                break;
                            default: SqlCob3.Append(" and LabelOver<0 and WardRetreat!='2'");
                                break;
                        }
                        if (checkBox8.Checked == true)
                            SqlCob3.Append(" and ISNUMERIC(iv.remark3)>0 and  iv.remark3>=10");
                                     //SqlCob3.Append("and (PackAdvance != 1 or Packadvance is null) ");
                        //case "配置取消": SqlCob3.Append(" and LabelOver<0 and WardRetreat!='2' and IVStatus =3 and (PackAdvance != 1 or Packadvance is null) ");
                        break;
                    case "提前打包": 
                        switch (cbbPrinted.SelectedIndex)
                        {
                            case 0: SqlCob3.Append(" and PackAdvance = 1 and (LabelOver>=0 and WardRetreat=0 )");
                                break;
                            case 1: SqlCob3.Append(" and PackAdvance = 1  and IVStatus >=3 and (LabelOver>=0 and WardRetreat=0 )");
                                break;
                            case 2: SqlCob3.Append(" and PackAdvance = 1 and IVStatus < 3 and (LabelOver>=0 and WardRetreat=0 )");
                                break;
                            default: SqlCob3.Append(" and PackAdvance = 1 and (LabelOver>=0 and WardRetreat=0 )");
                                break;
                        }
                        break;
                }                
            }
            else if (checkBox9.Checked && checkBox1.Checked)
            {
                switch (Cob3Txt)
                {
                    case "未打印": SqlCob3.Append(" and IVStatus < 3 ");
                        break;
                    case "已打印": SqlCob3.Append(" and IVStatus = 3 and (PackAdvance != 1 or Packadvance is null) ");
                        break;
                    case "已摆药":
                        if (cb_yp.Checked && cb_ys.Checked)
                            SqlCob3.Append(" and IVStatus = 4  ");
                        else if (cb_yp.Checked)
                            SqlCob3.Append(" and IVStatus=3 and iv.labelno in (select distinct labelno from dbo.IVRecod_YP_Check where SolventCheck='1') ");
                        else if (cb_ys.Checked)
                            SqlCob3.Append(" and IVStatus=3 and iv.labelno in (select distinct labelno from dbo.IVRecod_YP_Check where MenstruumCheck='1') ");
                        else
                            SqlCob3.Append(" and IVStatus = 4  ");
                        SqlCob3.Append(" and (PackAdvance != 1 or Packadvance is null)");
                        break;
                    case "已排药": SqlCob3.Append(" and IVStatus = 5 and (PackAdvance != 1 or Packadvance is null) ");
                        break;
                    case "已进仓": SqlCob3.Append(" and IVStatus = 7 and (PackAdvance != 1 or Packadvance is null) ");
                        break;
                    case "已配置": SqlCob3.Append(" and IVStatus = 9 and (PackAdvance != 1 or Packadvance is null) ");
                        break;
                    case "已出仓": SqlCob3.Append(" and IVStatus = 11 and (PackAdvance != 1 or Packadvance is null) ");
                        break;
                    case "已打包": SqlCob3.Append(" and IVStatus = 13 and (PackAdvance != 1 or Packadvance is null) ");
                        break;
                    case "已签收": SqlCob3.Append(" and IVStatus = 15 and (PackAdvance != 1 or Packadvance is null) ");
                        break;
                    case "已退药":
                        switch (cbbPrinted.SelectedIndex)
                        {
                            case 0: SqlCob3.Append(" and WardRetreat='2' ");
                                break;
                            case 1: SqlCob3.Append(" and WardRetreat='2'  and IVStatus >=3 ");
                                break;
                            case 2: SqlCob3.Append(" and WardRetreat='2'  and IVStatus < 3 ");
                                break;
                            default: SqlCob3.Append(" and WardRetreat='2'  ");
                                break;
                        }

                        break;
                    case "配置取消":
                        switch (cbbPrinted.SelectedIndex)
                        {
                            case 0: SqlCob3.Append(" and LabelOver<0 and WardRetreat!='2'  ");
                                break;
                            case 1: SqlCob3.Append(" and LabelOver<0 and WardRetreat!='2' and IVStatus >=3 ");
                                break;
                            case 2: SqlCob3.Append(" and LabelOver<0 and WardRetreat!='2'  and IVStatus < 3 ");
                                break;
                            default: SqlCob3.Append(" and LabelOver<0 and WardRetreat!='2'");
                                break;
                        }
                        if (checkBox8.Checked == true)
                            SqlCob3.Append(" and ISNUMERIC(iv.remark3)>0 and  iv.remark3>=10");
                        //SqlCob3.Append(" and (PackAdvance != 1 or Packadvance is null) ");
                        //case "配置取消": SqlCob3.Append(" and LabelOver<0 and WardRetreat!='2' and IVStatus>=3 and PackAdvance != 1 ");
                        break;
                    case "提前打包":
                        switch (cbbPrinted.SelectedIndex)
                        {
                            case 0: SqlCob3.Append(" and PackAdvance = 1 and (LabelOver>=0 and WardRetreat=0 )");
                                break;
                            case 1: SqlCob3.Append(" and PackAdvance = 1  and IVStatus >=3 and (LabelOver>=0 and WardRetreat=0 )");
                                break;
                            case 2: SqlCob3.Append(" and PackAdvance = 1 and IVStatus < 3 and (LabelOver>=0 and WardRetreat=0 )");
                                break;
                            default: SqlCob3.Append(" and PackAdvance = 1 and (LabelOver>=0 and WardRetreat=0 )");
                                break;
                        }
                        break;
                }    
            
            }
            else if (checkBox9.Checked && !checkBox1.Checked)
            {

                switch (Cob3Txt)
                {
                    case "未打印": SqlCob3.Append(" and IVStatus < 3 ");
                        break;
                    case "已打印": SqlCob3.Append(" and IVStatus = 3 and (PackAdvance != 1 or Packadvance is null) ");
                        break;
                    case "已摆药":
                        if (cb_yp.Checked && cb_ys.Checked)
                            SqlCob3.Append(" and IVStatus = 4  ");
                        else if (cb_yp.Checked)
                            SqlCob3.Append(" and IVStatus=3 and iv.labelno in (select distinct labelno from dbo.IVRecod_YP_Check where SolventCheck='1') ");
                        else if (cb_ys.Checked)
                            SqlCob3.Append(" and IVStatus=3 and iv.labelno in (select distinct labelno from dbo.IVRecod_YP_Check where MenstruumCheck='1') ");
                        else
                            SqlCob3.Append(" and IVStatus = 4  ");
                        SqlCob3.Append(" and (PackAdvance != 1 or Packadvance is null)");
                        break;
                    case "已排药": SqlCob3.Append(" and IVStatus = 5 and (PackAdvance != 1 or Packadvance is null) ");
                        break;
                    case "已进仓": SqlCob3.Append(" and IVStatus = 7 and (PackAdvance != 1 or Packadvance is null) ");
                        break;
                    case "已配置": SqlCob3.Append(" and IVStatus = 9 and (PackAdvance != 1 or Packadvance is null) ");
                        break;
                    case "已出仓": SqlCob3.Append(" and IVStatus = 11 and (PackAdvance != 1 or Packadvance is null) ");
                        break;
                    case "已打包": SqlCob3.Append(" and IVStatus = 13 and (PackAdvance != 1 or Packadvance is null) ");
                        break;
                    case "已签收": SqlCob3.Append(" and IVStatus = 15 and (PackAdvance != 1 or Packadvance is null) ");
                        break;
                    case "已退药":
                        switch (cbbPrinted.SelectedIndex)
                        {
                            case 0: SqlCob3.Append(" and WardRetreat='2' ");
                                break;
                            case 1: SqlCob3.Append(" and WardRetreat='2'  and IVStatus >=3 ");
                                break;
                            case 2: SqlCob3.Append(" and WardRetreat='2'  and IVStatus < 3 ");
                                break;
                            default: SqlCob3.Append(" and WardRetreat='2'  ");
                                break;
                        }

                        break;
                    case "配置取消":
                        switch (cbbPrinted.SelectedIndex)
                        {
                            case 0: SqlCob3.Append(" and LabelOver<0 and WardRetreat!='2'  ");
                                break;
                            case 1: SqlCob3.Append(" and LabelOver<0 and WardRetreat!='2' and IVStatus >=3 ");
                                break;
                            case 2: SqlCob3.Append(" and LabelOver<0 and WardRetreat!='2'  and IVStatus < 3 ");
                                break;
                            default: SqlCob3.Append(" and LabelOver<0 and WardRetreat!='2'");
                                break;
                        }
                        if (checkBox8.Checked == true)
                            SqlCob3.Append(" and ISNUMERIC(iv.remark3)>0 and  iv.remark3>=10");
                        //SqlCob3.Append(" and (PackAdvance != 1 or Packadvance is null) ");
                        //case "配置取消": SqlCob3.Append(" and LabelOver<0 and WardRetreat!='2' and IVStatus>=3 and PackAdvance != 1 ");
                        break;
                    case "提前打包":
                        switch (cbbPrinted.SelectedIndex)
                        {
                            case 0: SqlCob3.Append(" and PackAdvance = 1 and (LabelOver>=0 and WardRetreat=0 )");
                                break;
                            case 1: SqlCob3.Append(" and PackAdvance = 1  and IVStatus >=3 and (LabelOver>=0 and WardRetreat=0 )");
                                break;
                            case 2: SqlCob3.Append(" and PackAdvance = 1 and IVStatus < 3 and (LabelOver>=0 and WardRetreat=0 )");
                                break;
                            default: SqlCob3.Append(" and PackAdvance = 1 and (LabelOver>=0 and WardRetreat=0 )");
                                break;
                        }
                        break;
                }    
            }
            string s = "";
            
            if (cbbList.Text=="<全部>"||cbbList.Text=="")
            {
                return SqlCob3.ToString();
            }
            else if (cbbList.Text=="已打印")
            {
                s = "=1";
            }
            else 
            {
                s = "!=1";
            }


            switch (Cob3Txt)
            {
                case "未打印": SqlCob3.Append(" and iv.labelno in (select distinct LabelNo from PrintList where  unprintList"+s+")");
                    break;
                case "已打印": SqlCob3.Append(" and iv.labelno in (select distinct LabelNo from PrintList where  printedList" + s + ")");
                    break;
                case "已摆药": SqlCob3.Append(" and iv.labelno in (select distinct LabelNo from PrintList where  BYList" + s + ")");
                    break;
                case "已排药": SqlCob3.Append(" and iv.labelno in (select distinct LabelNo from PrintList where  PYList" + s + ")");
                    break;
                case "已进仓": SqlCob3.Append(" and iv.labelno in (select distinct LabelNo from PrintList where  JCList" + s + ")");
                    break;
                case "已配置": SqlCob3.Append(" and iv.labelno in (select distinct LabelNo from PrintList where  PZList" + s + ")");
                    break;
                case "已出仓": SqlCob3.Append(" and iv.labelno in (select distinct LabelNo from PrintList where  CCList" + s + ")");
                    break;
                case "已打包": SqlCob3.Append(" and iv.labelno in (select distinct LabelNo from PrintList where  DBList" + s + ")");
                    break;
                case "已签收": SqlCob3.Append(" and iv.labelno in (select distinct LabelNo from PrintList where  QSList" + s + ")");
                    break;
                case "已退药": SqlCob3.Append(" and iv.labelno in (select distinct LabelNo from PrintList where  TYList" + s + ")");              
                    break;
                case "配置取消": SqlCob3.Append(" and iv.labelno in (select distinct LabelNo from PrintList where  PZQXList" + s + ")");
                    break;
                case "提前打包": SqlCob3.Append(" and iv.labelno in (select distinct LabelNo from PrintList where  TQDBList" + s + ")");
                    break;
            }                

            return SqlCob3.ToString();

            
            
        }

        private string SqlCob1(string Cob1Txt) 
        {
            StringBuilder SqlCob1 = new StringBuilder();
            string[] a = Cob1Txt.Split(',');
            if (a.Length <=0)
                return "";
            if (a[0].Contains("<临时>"))
                SqlCob1.Append(" and Batch like '%L%'");
            else if (a[0].Contains("<长期>"))
                SqlCob1.Append(" and Batch not like '%L%'");

            if (a.Length<=1)
                return SqlCob1.ToString();
            SqlCob1.Append(" and (1=2 ");
            for (int i = 1; i < a.Length; i++)
            {
                string Now = a[i].Replace("批", "").Trim(); ;
                SqlCob1.Append(" or Batch like '%" + Now + "%'");
            }
            SqlCob1.Append(" ) ");
                //if (Cob1Txt.Contains("<临时>"))
                //{
                //    Cob1Txt = Cob1Txt.Replace("<临时>", "");
                //    SqlCob1.Append(" and Batch like '%" + Cob1Txt + "%' and Batch like '%L%'");
                //}
                //else if (Cob1Txt.Contains("<长期>"))
                //{

                //    Cob1Txt = Cob1Txt.Replace("<长期>", "");
                //    Cob1Txt = Cob1Txt.Replace("#", "");
                //    SqlCob1.Append(" and Batch like '%" + Cob1Txt + "%' and Batch like '%#%'");
                //}
            return SqlCob1.ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            label17.Text = Time;
            ChoseLabel cl = new ChoseLabel(re6);
            ChoseLabel.Choselb += new ChoseLabel.NewDelegate(dagou);
            cl.ShowDialog();
        }

        public bool dagou(string LabelNo) //打钩
        {
            label17.Text = Time;
            for (int i = 0; i < dgvPre.Rows.Count; i++) 
            {
                if (LabelNo == dgvPre.Rows[i].Cells["LabelNo"].Value.ToString().Trim()) 
                {
                    dgvPre.Rows[i].Cells["Select"].Value = true;
                    //lb = LabelNo+ ","+lb;
                    //lb = lb.TrimEnd(',');
                    dgvPre.CurrentCell = dgvPre.Rows[i].Cells["Select"];
                    return true;
                }
                else if (re6 == 1 && LabelNo == dgvPre.Rows[i].Cells["remark6"].Value.ToString().Trim())
                {
                    dgvPre.Rows[i].Cells["Select"].Value = true;
                    //lb = LabelNo+ ","+lb;
                    //lb = lb.TrimEnd(',');
                    dgvPre.CurrentCell = dgvPre.Rows[i].Cells["Select"];
                    return true;
                }
            }
            return false;
            //MessageBox.Show(dgvPre.Rows[0].Cells["LabelNo"].ToString().Trim());
        }

        private void label5_Click(object sender, EventArgs e)//退药
        {
            label17.Text = Time;
            string s = CheckSelectLabel();
            if (s.Length > 0)
            {
                if (MessageBox.Show("确认退药！", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {                    
                    string[] a = s.Split(',');
                    if (label5.Text == "审核")
                    {
                        ////string str = "exec ToBackHisRemLabel '" + lb.Remove(0, 1) + "','" + UserID + "'";
                        ////db.SetPIVAsDB(str);
                        //for (int i = 0; i < a.Length; i++)
                        //{
                        //    string sql = " update IVRecord set WardRetreat=2 ,WardRID='" + EmployeeID + "',WardRtime=Getdate() ";
                        //    sql = sql + "where LabelNo='" + a[i] + "' ";
                        //    //sql = sql + "and IVRecordID not  in (select distinct V_IVRecordDetail.IVRecordID from V_IVRecordDetail inner join V_IVRecord ";
                        //    //sql = sql + "on V_IVRecord.IVRecordID = V_IVRecordDetail.IVRecordID where ReturnFromHis=3 and V_IVRecord.LabelNo ='" + a[i] + "'  )";
                        //    db.SetPIVAsDB(sql);
                        //}
                        PivasHisCommOrigin Interface = new PivasHisCommOrigin();
                        Interface.returnCharge(a, EmployeeID);
                    }
                    else if (label5.Text == "退药")
                        PeiZhiCancel(a);
                    
                    BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text,comboBox5.Text);
                    BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text,string.Empty,comboBox5.Text ,cbbPrinted.Text);
                    //BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text);
                }
            }
            else
            {
                MessageBox.Show("请勾选需要操作的瓶签");
                return;
            }
        }

        private void PeiZhiCancel(string[] a) //配置取消
        {
            for (int i = 0; i < a.Length; i++)
            {
                string sql = " update IVRecord set LabelOver=-4,LabelOverID='" + EmployeeID + "',LabelOverTime=Getdate() where labelno='" + a[i] + "' ";
                dbHelp.SetPIVAsDB(sql);
            }
        }

        public void SetInformation(string preID,string LabelNo)//设置显示信息
        {
            try
            {
                string str = SQLStr.INFO(preID, LabelNo);
                DataSet dtinfo = new DataSet();
                dtinfo = dbHelp.GetPIVAsDB(str);
                clear();
                if (dtinfo==null||dtinfo.Tables.Count==0)
                {
                    return;
                }
                setPerson(dtinfo.Tables[0].Rows[0]);
                setCurrentDrug(dtinfo.Tables[1], LabelNo);
               // setResult(dtinfo);

                //取得扫描记录与配置取消退药记录
                using (DataSet msg = dbHelp.GetPIVAsDB(SQLStr.msg(LabelNo))) 
                {
                    if (msg.Tables[0].Rows.Count == 0 && msg.Tables[1].Rows.Count == 0)
                    {
                        return;
                    }
                    SetCancelMessage(msg);
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void clear()//清空信息
        {
            txtBedNo.Text = "";
            txtCaseID.Text = "";
            txtDoctor.Text = "";
            txtUname.Text = "";
            textEndDT.Text = "";
            txtSex.Text = "";
            textStartDT.Text = "";
            textWard.Text = "";
            textPatient.Text = "";
            textHeight.Text = "";
            //ID = "";

            textWeight.Text = "";
            textAge.Text = "";
            textDrawerName.Text = "";
            txtBatch.Text = "";
            pnlInfo.Controls.Clear();
            pnlcancel.Controls.Clear();
        }

        private void setPerson(DataRow R)//设置信息
        {
            try
            {
                txtBedNo.Text = R["BedNo"].ToString();
                txtCaseID.Text = R["CaseID"].ToString();
                txtDoctor.Text = R["DocName"].ToString() + "(" + R["DoctorCode"].ToString() + ")";
                textDrawerName.Text = R["DrawerName"].ToString() + "(" + R["DrawerCode"].ToString() + ")";
                textEndDT.Text = R["EndDT"].ToString();
                txtUname.Text = R["UsageName"].ToString();
                string sex = R["Sex"].ToString().ToLower().Trim();
                if ("2" == sex)
                    txtSex.Text = "女";
                else if ("1" == sex)
                    txtSex.Text = "男";
                textStartDT.Text = R["StartDT"].ToString();
                textWard.Text = R["WardName"].ToString();

                textPatient.Text = R["PatName"].ToString();
                //ID = R["PrescriptionID"].ToString();

                if (R["Weight"].ToString() == "0.00")
                { textWeight.Text = "  --"; }
                else
                { textWeight.Text = R["Weight"].ToString(); }

                if (R["Height"].ToString() == "0")
                { textHeight.Text = "  --"; }
                else
                { textHeight.Text = R["Height"].ToString(); }

              
                string BirDay = R["Birthday"].ToString();
                if (BirDay.Length > 10)
                {
                   DateTime Newdt =Convert.ToDateTime(BirDay);
                   BirDay = Newdt.Year + "/" + Newdt.Month + "/" + Newdt.Day;
                }

                int sub=0;
                if (R["AgeSTR"].ToString().Trim()=="天")//婴儿计算现在距出生有多少天。
                {
                    DateTime dt1 = Convert.ToDateTime(BirDay);
                    DateTime dt2 = Convert.ToDateTime(DateTime.Now.ToString());
                    TimeSpan ts = dt2 - dt1;
                    sub = ts.Days;
                }
 
               textAge.Text = R["Age"].ToString() + R["AgeSTR"].ToString() + "(" + BirDay.Trim() + ")";
               if (sub != 0)
               { textAge.Text = textAge.Text + " 出生" + sub.ToString() + "天"; }
          
                txtBatch.Text = R["FregCode"].ToString();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 插入瓶签状态信息
        /// </summary>
        /// <param name="dt"></param>
        private void SetCancelMessage(DataSet ds)
        {
            int i = 0;
            pnlcancel.Controls.Clear();
            for (int J = 0; J < ds.Tables[1].Rows.Count; J++)
            {
                CancelMessage msg = new CancelMessage();
                msg.setmsg("核对", ds.Tables[1].Rows[J]);
                msg.Top = i * 20;
                pnlcancel.Controls.Add(msg);
                i++;
            }
            if (int.Parse(ds.Tables [0].Rows[0]["LabelOver"].ToString()) < 0) 
            {
                CancelMessage msg=new CancelMessage();
                msg.setmsg("配置取消", ds.Tables[0].Rows[0]);
                msg.Top = i * 20;
                pnlcancel.Controls.Add(msg);
                i++;
            }
            if (int.Parse(ds.Tables[0].Rows[0]["WardRetreat"].ToString())>0)
            {
                CancelMessage msg = new CancelMessage();
                msg.setmsg("退药", ds.Tables[0].Rows[0]);
                msg.Top = i * 20;
                pnlcancel.Controls.Add(msg);
                i++;
            }
            if (int.Parse(ds.Tables[0].Rows[0]["PackAdvance"].ToString()) == 1) 
            {
                CancelMessage msg = new CancelMessage();
                msg.setmsg("提前打包", ds.Tables[0].Rows[0]);
                msg.Top = i * 20;
                pnlcancel.Controls.Add(msg);
                i++;
            }

            
        }

        private void setCurrentDrug(DataTable dt,string labelno)//设置当前处方所对药品
        {
            pnlInfo.Controls.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CurrentDrug Drug = new CurrentDrug(labelno, dt.Rows[i]);           
                pnlInfo.Controls.Add(Drug);
            }
        }

        private void dgvPre_DoubleClick(object sender, EventArgs e)
        {
            
            if (panel2.Visible == true)
            {
                panel2.Visible = false;
                dgvPre.Height = dgvPre.Height + panel2.Height;
                panel5.Top = dgvPre.Top + dgvPre.Height;
            }
            else
            {
                panel2.Visible = true;
                dgvPre.Height = dgvPre.Height - panel2.Height;
                panel5.Top = dgvPre.Top + dgvPre.Height;
            }
            
        }

        private void dgvPre_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            label17.Text = Time;
            if (e.ColumnIndex == 0)
            {

                if (dgvPre.CurrentRow.Cells["Select"].Value.ToString() == "True")
                {
                    dgvPre.CurrentRow.Cells["Select"].Value = false;
                    //lb = lb.Replace("," + dgvPre.CurrentRow.Cells["LabelNo"].Value.ToString().Trim(), "");
                    // MessageBox.Show(lb);
                }
                else
                {
                    dgvPre.CurrentRow.Cells["Select"].Value = true;
                    //lb = lb + "," + dgvPre.CurrentRow.Cells["LabelNo"].Value;
                    // MessageBox.Show(lb);
                }
            }
            //SetInformation(dgvPre.CurrentRow.Cells["PrescriptionID"].Value.ToString());
        }

        private void dgvPre_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label17.Text = Time;
           
            if (e.RowIndex >= 0)
            {
                SetInformation(dgvPre.CurrentRow.Cells["PrescriptionID"].Value.ToString(), dgvPre.CurrentRow.Cells["LabelNo"].Value.ToString());
               
            }
            else
            {
                lieMing = dgvPre.Columns[e.ColumnIndex].HeaderText;
            }
        }
     

        private void Pic_Max_Click(object sender, EventArgs e)
        {
            MaximizedBounds = Screen.PrimaryScreen.WorkingArea;

            if (this.WindowState == FormWindowState.Normal)
            {
                Pic_Max.BackgroundImage = Properties.Resources._3;
                this.FormBorderStyle = FormBorderStyle.None;
               // this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                this.WindowState = FormWindowState.Maximized;

                for (int i = 0; i < flowLayoutPanel2.Controls.Count;i++ )
                {
                    flowLayoutPanel2.Controls[i].Width = Screen.PrimaryScreen.WorkingArea.Width - dgvDWard.Width;
                }
            }
            else
            {
                Pic_Max.BackgroundImage = Properties.Resources._2;
                this.WindowState = FormWindowState.Normal;
                for (int i = 0; i < flowLayoutPanel2.Controls.Count; i++)
                {
                    flowLayoutPanel2.Controls[i].Width = Screen.PrimaryScreen.WorkingArea.Width - dgvDWard.Width;
                }
            }

            this.flowLayoutPanel3.Width = this.Width - dgvDWard.Width - 18;

            this.flowLayoutPanel4.Width = flowLayoutPanel3.Width;
            this.flowLayoutPanel5.Width = flowLayoutPanel3.Width;
            //this.flowLayoutPanel6.Width = flowLayoutPanel3.Width;
            this.flowLayoutPanel7.Width = flowLayoutPanel3.Width;
            Line1.Width = flowLayoutPanel3.Width;
            Line2.Width = flowLayoutPanel3.Width;
            Line3.Width = flowLayoutPanel3.Width;
            Line4.Width = flowLayoutPanel3.Width;
        }



        private void lbPrint_Click(object sender, EventArgs e)
        {
            try
            {

                string s = "";
                s = getLabelstr();
                if (dgvPre.Rows.Count==0)
                {
                    return;
                }
                if (s=="")
                {
                    MessageBox.Show("请选择要打印的瓶签");
                    return;
                }
                label17.Text = Time;
                StringBuilder SqlLabel = new StringBuilder();
                StringBuilder CountSql = new StringBuilder();
                string count="";
                SqlLabel.Append("select D.WardSimName WardName,iv.BedNo,iv.PatName ,iv.Batch, iv.LabelNo,iv.WardCode,b.Spec,b.[Dosage],b.[DosageUnit],");
                SqlLabel.Append("b.DrugName,DD.DrugNameJC,DD.ProductName,b.DgNo,DD.DrugName as MainDrug ,DD.NoName,D.WardArea  from V_IVRecord iv");
                SqlLabel.Append(" left join V_IVRecordDetail b on iv.IVRecordID=b.IVRecordID");
                SqlLabel.Append(" left join DDrug DD on iv.MarjorDrug=DD.DrugCode ");
                SqlLabel.Append(" left join DWard D ON D.WardCode = iv.WardCode ");
                SqlLabel.Append("inner join PrintList pl on pl.LabelNo=iv.LabelNo ");
                SqlLabel.Append(" where 1=1 and iv.LabelNo in ("+s+") ");
                SelectRule(SqlLabel);
                SqlLabel.Append(GetPrintOrder());
                //SqlLabel.Append(" order by iv.WardCode,iv.LabelNo");
                CountSql.Append( "select count(distinct iv.LabelNo) from V_IVRecord iv left join V_IVRecordDetail b on iv.IVRecordID=b.IVRecordID ");
                CountSql.Append( "inner join printlist pl on pl.labelno = iv.LabelNo ");
                CountSql.Append(" where 1=1 and iv.LabelNo in (" + s + ") ");
                SelectRule(CountSql);
                count = dbHelp.GetPIVAsDB(CountSql.ToString()).Tables[0].Rows[0][0].ToString();

                DataSet ds = new DataSet();
                ds = dbHelp.GetPIVAsDB(SqlLabel.ToString());
                if (ds!=null&&ds.Tables[0].Rows.Count>0)
                {
                  //  PrintPreview1 preview = new PrintPreview1(ds.Tables[0], comboBox3.Text, count,s);
                    PrintPreview.PrintPreview preview = new PrintPreview.PrintPreview(ds.Tables[0], comboBox3.Text, count, s);
                    preview.ShowDialog();
                    BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text,comboBox5.Text);
                    BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text,string.Empty,comboBox5.Text,cbbPrinted.Text);
                }                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void SelectRule(StringBuilder SqlLabel) 
        {
            if (WardCode != "" && WardCode != "<全部>")
                SqlLabel.Append(" and iv.WardCode in ('" + WardCode + "') ");
            if (dateTimePicker1.Text != "")
                SqlLabel.Append(" and DATEDIFF(DD,InfusionDT,'" + dateTimePicker1.Text + "')=0 ");
            if (comboBox2.Text != "")
                SqlLabel.Append(" and Batch like '%" + comboBox2.Text + "%'");
            if (comboBox1.Text != "")
                SqlLabel.Append(SqlCob1(comboBox1.Text));
            if (comboBox3.Text != "<全部>")
                SqlLabel.Append(SqlCob3(comboBox3.Text));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
             
                if (label17.Text != "0")
                {label17.Text = (int.Parse(label17.Text) - 1).ToString();}
                else
                { 
                    comboBox3_TextChanged(sender, e);
                    label17.Text = Time;
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TQDB_Click(object sender, EventArgs e)
        {
            try
            {
                string s = dgvPre.CurrentRow.Cells["LabelNo"].Value.ToString();
                dbHelp.SetPIVAsDB("UPDATE IVRecord Set PackAdvance = 1,PackTime = GETDATE(),PackID = '" + EmployeeID + "'  WHERE LabelNo = '" + s + "'");
                BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text,comboBox5.Text);
                BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text,string.Empty,comboBox5.Text,cbbPrinted.Text);
                //BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            try
            {
                string s = dgvPre.CurrentRow.Cells["LabelNo"].Value.ToString();
                string sql = "SELECT * FROM V_IVRecord WHERE LabelNo = '" + s + "'";
                DataSet ds = new DataSet();
                ds = dbHelp.GetPIVAsDB(sql);
                int a = Convert.ToInt32(ds.Tables[0].Rows[0]["IVStatus"].ToString());
                int b = Convert.ToInt32(ds.Tables[0].Rows[0]["LabelOver"].ToString());
                int c = Convert.ToInt32(ds.Tables[0].Rows[0]["PackAdvance"].ToString());
                if (a>=PackOverFor||b<0||c==1)
                {
                    TQDB.Enabled = false;
                }
                else
                {
                    TQDB.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void dgvPre_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {                    
                    dgvPre.ClearSelection();
                    dgvPre.Rows[e.RowIndex].Selected = true;
                    label17.Text = Time;
                    SetInformation(dgvPre.Rows[e.RowIndex].Cells["PrescriptionID"].Value.ToString(), dgvPre.Rows[e.RowIndex].Cells["LabelNo"].Value.ToString());
                    dgvPre.CurrentCell = dgvPre.Rows[e.RowIndex].Cells[e.ColumnIndex];
                                       
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            try
            {
                BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text, comboBox5.Text);
                BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text,string.Empty,comboBox5.Text ,cbbPrinted.Text);
                //BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Init()
        {
            try
            {
                DataSet ds = new DataSet();
                string s = "SELECT TOP 1 * FROM PivasLabelSelectFormSet";
                ds = dbHelp.GetPIVAsDB(s);
                PackOverFor = Convert.ToInt32(ds.Tables[0].Rows[0]["PackOverFor"].ToString());
                ReTreatOverFor = Convert.ToInt32(ds.Tables[0].Rows[0]["ReTreatOverFor"].ToString());
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



       

        private void checkBox2_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvPre.Rows.Count;i++ )
                {
                    dgvPre.Rows[i].Cells[0].Value = checkBox2.Checked;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

     

        private void updatelist()
        {
            try
            {
                string s = "insert into PrintList " +
                        "select LabelNo,0,0,0,0,0,0,0,0,0,0,0,0 from V_IVRecord where DATEDIFF(DD,InfusionDT,'" + dateTimePicker1.Text + "')=0 " +
                        "and LabelNo not in (select LabelNo FROM PrintList) ";

                dbHelp.SetPIVAsDB(s);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 得到选中的瓶签号
        /// </summary>
        /// <returns></returns>
        private string getLabelstr()
        {
            try
            {
                string s="";
                for (int i = 0; i < dgvPre.Rows.Count;i++ )
                {
                    if (dgvPre.Rows[i].Cells[0].Value.ToString()=="True")
                    {
                        s= dgvPre.Rows[i].Cells["LabelNo"].Value.ToString()+","+s;
                    }
                }

                return s.TrimEnd(',');
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

      

        private string CheckSelectLabel()
        {
            try
            {
                string s= "";
                for (int i = 0; i < dgvPre.Rows.Count;i++ )
                {
                    if (dgvPre.Rows[i].Cells[0].Value.ToString()=="True")
                    {
                        s=dgvPre.Rows[i].Cells["LabelNo"].Value.ToString()+','+s;
                    }
                }

                return s.TrimEnd(',');
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        private void linkLabel32_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkLabel32.Tag.ToString().Trim().Equals("0"))
            {
                linkLabel32.Tag = 1;
                linkLabel32.Text = "▲";
                flowLayoutPanel4.Height = flowLayoutPanel4.Height * 2;
              
            }
            else
            {
                linkLabel32.Tag = 0;
                linkLabel32.Text = "▼";
                flowLayoutPanel4.Height = flowLayoutPanel4.Height/2;
            } 
        }

    

        /// <summary>
        /// 添加筛选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Choseindex">选择的是哪个大项</param>
        /// <param name="Chosestr">大项的名字</param>
        /// <param name="Change">改变哪个下拉框的值</param>
        /// <param name="Line">选择项下的虚线</param>
        private void AddChose(object sender,int Choseindex,string Chosestr,ComboBox Change)
        {
            FlowLayoutPanel F = new FlowLayoutPanel();

            FilterLabel fl = new FilterLabel();//自定义控件
         
            LinkLabel lab = (LinkLabel)sender;

            if (fk == null)
            {
                //flowLayoutPanel8.Controls.Add(fl);
                fk = fl;
            }


            

            if (Choseindex == 1)
            {
                if (PCFilterLabel == null)
                {
                    flowLayoutPanel8.Controls.Add(fl);
                    PCFilterLabel = fl;
                    PCFilterLabel.ChangeTextVal += new FilterLabel.DelegateChangeTextS(Chose);
                }
                if (lab.Text.Equals("<长期>") || lab.Text.Equals("<临时>")||lab.Text.Equals("<全部>") )
                {
                 
                    //  (((LinkLabel)(sender)).Parent).Visible = true;
                    linkLabel33.Visible = linkLabel21.Visible = linkLabel23.Visible = true;
                    lab.Visible = false;
                }
                else
                {
                    (((LinkLabel)(sender)).Parent.Parent).Visible =false;
                }
                PCFilterLabel.ShowData(Chosestr + ((LinkLabel)(sender)).Text, checkBox1.Checked, Choseindex);
              
            }
            else
            {
                flowLayoutPanel8.Controls.Add(fl);
                (((LinkLabel)(sender)).Parent).Visible = false;
                fl.ShowData(Chosestr + ((LinkLabel)(sender)).Text, checkBox1.Checked, Choseindex);
                fl.ChangeTextVal += new FilterLabel.DelegateChangeTextS(Chose);
            }

            if (Choseindex == 1)
            {
                Change.Text = PCFilterLabel.label1.Text;
            }
            else if ((Choseindex != 1 && Choseindex != 2) || !((LinkLabel)(sender)).Text.Trim().Equals("<全部>"))
            {
                Change.Text = (fl.label1.Text.Substring(fl.label1.Text.IndexOf(":") + 1, fl.label1.Text.Length - fl.label1.Text.IndexOf(":") - 1));
            }
            else
            {
                Change.Text = "";
            }
           // int Count = flowLayoutPanel8.Controls.Count - 2;//计算容器中，添加的自定义控件的数量

            if (Choseindex == 3)
            {
                fl.Name = "cbbPrinted";
            }
            if (Choseindex == 0 && (Change.Text.Contains("提前打包") || Change.Text.Contains("已退药")))
            {
                ChoseCount = 4;
                //flowLayoutPanel6.Visible = true;
            }
            else if (Choseindex == 1)
            {
                ChoseCount = 4;
            }
            else
            {
                ChoseCount = 3;
                //flowLayoutPanel6.Visible = false;
            }


            int Count = 0;
            foreach (Control c in flowLayoutPanel2.Controls)
            {
                if (c.Visible)
                {
                    Count = Count + 1;
                }
            }

            Line1.Visible = false;
            Line2.Visible = false;
            Line3.Visible = false;
            Line4.Visible = false;


            for (int i = 1; i < Count - 1; i++)
            {
                ((Label)(this.Controls.Find("Line" + i, false)[0])).Visible = true;
            }

            dgvPre.Top = flowLayoutPanel2.Height + panel1.Height+5;
            dgvPre.Height = panel5.Location.Y - dgvPre.Location.Y;
     

        }

        public void Chose(int Chose, FilterLabel ff, bool checkd)
        {
            if (checkd)
            {
                string str = ff.label1.Text;
                flowLayoutPanel8.Controls.Remove(ff);
                int s = flowLayoutPanel8.Controls.Count - 2;
                switch (Chose)
                {
                    case 0: flowLayoutPanel3.Visible = true; //flowLayoutPanel6.Visible = false;
                        str=str.Replace("状态:","");
                        if (str.Trim().Equals("提前打包") || str.Trim().Equals("已退药"))
                        {
                            FilterLabel a;
                            try
                            {
                                a = (FilterLabel)flowLayoutPanel8.Controls.Find("cbbPrinted", false)[0];
                                flowLayoutPanel8.Controls.Remove(a);
                            }
                            catch { }
                        }
                        comboBox3.Text = ""; 
                        break;
                    case 1: flowLayoutPanel4.Visible = true; comboBox1.SelectedIndex = 0;
                        linkLabel33.Visible = linkLabel21.Visible = linkLabel23.Visible = true;
                        flowLayoutPanel9.Visible = false;
                        flowLayoutPanel10.Visible = true;
                        PCFilterLabel  = null;
                        label31.Text = "长期/临时:";
                        comboBox1.Text = ""; 
                        break;
                    case 2: flowLayoutPanel5.Visible = true; comboBox2.Text = ""; break;
                    case 3:
                        if (comboBox3.Text.Trim().Equals("提前打包") || comboBox3.Text.Trim().Equals("已退药"))
                        {
                            ChoseCount = 4;
                            //flowLayoutPanel6.Visible = true;
                            cbbPrinted.SelectedIndex = 0;
                            
                        }
                        else
                        {
                            ChoseCount = 5;
                           // flowLayoutPanel6.Visible = false;
                        }
                        break;
                    case 4: flowLayoutPanel7.Visible = true; cbbList.SelectedIndex = 0; break;
                }

                int Count = 0;
                foreach (Control c in flowLayoutPanel2.Controls)
                {
                    if (c.Visible)
                    {
                        Count = Count + 1;
                    }
                }

                Line1.Visible = false;
                Line2.Visible = false;
                Line3.Visible = false;
                Line4.Visible = false;


                for (int i = 1; i < Count - 1; i++)
                {
                    ((Label)(this.Controls.Find("Line" + i, false)[0])).Visible = true;
                }
                //int hei=flowLayoutPanel2.Height + panel1.Height;
                
            }
            else
            {
                checkBox1.Checked = ff.checkBox1.Checked;

            }
            dgvPre.Top = flowLayoutPanel2.Height + panel1.Height +5;
            dgvPre.Height = panel5.Location.Y - dgvPre.Location.Y;
        }

        /// <summary>
        /// 状态点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_Click(object sender, EventArgs e)
        {
            //AddChose(sender, 0, "状态:", comboBox3);
            LinkChose(sender, comboBox3);
        }


        //普通筛选
        private void LinkChose(object sender, ComboBox cb) 
        {
            if (((LinkLabel)sender).Image!=null)
            {
                ((LinkLabel)sender).Image = null;
                if (cb.Name == "cbbPrinted")
                {
                    cb.Text = "<全部>";
                }
                else
                {
                    cb.Text = "";
                }
                
                return;
            }
            CheckNull(((LinkLabel)sender).Parent);//清空父容器勾选

            ((LinkLabel)sender).Image = Properties.Resources.tick;
         
            LaseSelectLabel = ((LinkLabel)sender).Text;

            cb.Text = ((LinkLabel)sender).Text;
           // MessageBox.Show(cb.Text);
        }

        //批次全部、临时、长期筛选
        private void LinkChoseBatch(object sender, ComboBox cb) 
        {
            if (((LinkLabel)sender).Image != null)
            {
                ((LinkLabel)sender).Image = null;

                CheckNull(flowLayoutPanel9);//清空flowLayoutPanel9所有
                return;
            }

            CheckNull(((LinkLabel)sender).Parent);//清空父容器勾选
            ((LinkLabel)sender).Image = Properties.Resources.tick;
//            flowLayoutPanel9.Visible = true;

            string BatchTemp = string.Empty;
            BatchTemp = AddRule(BatchTemp, flowLayoutPanel10);
            BatchTemp = AddRule(BatchTemp, flowLayoutPanel9);
            cb.Text = BatchTemp;
        }

        //药品筛选
        private void LinkChoseDrug(object sender, ComboBox cb) 
        {
            if (((LinkLabel)sender).Image != null)
            {
                ((LinkLabel)sender).Image = null;
            }
            else
            {
                ((LinkLabel)sender).Image = Properties.Resources.tick;
            }
            string DrugTemp = string.Empty;
            DrugTemp = AddRule(DrugTemp,panel7);
            cb.Text = DrugTemp;
        }

        //批次筛选
        private void AddBatchTemp(object sender, EventArgs e) 
        {
            if (((LinkLabel)sender).Image != null)
                ((LinkLabel)sender).Image = null;
            else
                ((LinkLabel)sender).Image = Properties.Resources.tick;

            string BatchTemp = string.Empty;
            BatchTemp = AddRule(BatchTemp, flowLayoutPanel10);
            BatchTemp = BatchTemp == "" ? "<全部>" : BatchTemp;
            BatchTemp = AddRule(BatchTemp, flowLayoutPanel9);
            comboBox1.Text = BatchTemp;
        }


        //清空容器内的勾选
        private void CheckNull(object sender) 
        {
            foreach (Control col in ((Control)sender).Controls)
            {
                if (col is LinkLabel)
                    ((LinkLabel)col).Image = null;
            }   
        }

        //拼接容器中打钩的内容
        private string AddRule(string temp, object sender)
        {
            foreach (Control col in ((Control)sender).Controls)
            {
                if (col is LinkLabel)
                {
                    if (((LinkLabel)col).Image != null)
                        temp = temp == "" ? col.Text : temp + "," + col.Text;
                }
            }
            return temp;
        }

        private void linkLabel21_Click(object sender, EventArgs e)
        {
            //批次
            //label31.Text = "批次:";
            //(((LinkLabel)(sender)).Parent).Visible = false;
            //flowLayoutPanel9.Visible = true;
            //AddChose(sender, 1, "批次:", comboBox1);
            LinkChoseBatch(sender, comboBox1);
        }

       

        private void linkLabel31_Click(object sender, EventArgs e)
        {
            //筛选
            //AddChose(sender, 2, "筛选:", comboBox2);
            LinkChose(sender, comboBox2);
        }



        private void linkLabel13_Click(object sender, EventArgs e)
        {
            //瓶签
            //AddChose(sender, 3, "瓶签:", cbbPrinted);
            LinkChose(sender, cbbPrinted);
        }

        

      

        private void linkLabel24_Click(object sender, EventArgs e)
        {
            //药品类型
            LinkChoseDrug(sender, comboBox5);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            label17.Text = Time;
            cbo3();
            BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text,comboBox5.Text);
            BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text, string.Empty,comboBox5.Text,cbbPrinted.Text);
        }

        private void PivasLabel_SizeChanged(object sender, EventArgs e)
        {
            this.flowLayoutPanel3.Width = this.Width - dgvDWard.Width - 18;
            this.flowLayoutPanel4.Width = flowLayoutPanel3.Width;
            this.flowLayoutPanel5.Width = flowLayoutPanel3.Width;
           //this.flowLayoutPanel6.Width = flowLayoutPanel3.Width;
            this.flowLayoutPanel7.Width = flowLayoutPanel3.Width;
            Line1.Width = flowLayoutPanel3.Width;
            Line2.Width = flowLayoutPanel3.Width;
            Line3.Width = flowLayoutPanel3.Width;
            Line4.Width = flowLayoutPanel3.Width;
            dgvPre.Top = flowLayoutPanel2.Height + panel1.Height + 5;
            dgvPre.Height = panel5.Location.Y - dgvPre.Location.Y;
        }

        private void txtWard_TextChanged(object sender, EventArgs e)
        {
            if (DTWard.Rows.Count <= 0 || txtWard.Text == "病区名/简拼")
                return;           
            if (comboBox4.Text == "全部病区组")
            {               
                if (txtWard.Text == "")
                {
                    dgvDWard.DataSource = DTWard;
                    return;
                }

                DataTable dt = DTWard.Copy();
                dt.Rows.Clear();
                DataRow[] DR = DTWard.Select(" WardName like '%" + txtWard.Text.Trim() + "%' or Spellcode like '%" + txtWard.Text.Trim() + "%' ", "WardSeqNo ASC");
                foreach (DataRow dr in DR)
                {
                    dt.ImportRow(dr);
                }
                dgvDWard.DataSource = dt;
            }
            else
            {
                DataTable dt = DTWard.Copy();
                dt.Rows.Clear();
                DataRow[] DR = DTWard.Select("WardArea ='"+comboBox4.SelectedItem.ToString()+"' and ( WardName like '%" + txtWard.Text.Trim() + "%' or Spellcode like '%" + txtWard.Text.Trim() + "%')" , "WardSeqNo ASC");
                foreach (DataRow dr in DR)
                {
                    dt.ImportRow(dr);
                }
                dgvDWard.DataSource = dt;

            }
        }

        private void txtWard_Enter(object sender, EventArgs e)
        {
            if (txtWard.Text == "病区名/简拼") 
            {
                txtWard.Text = "";
                txtWard.ForeColor = Color.Black;
            }
        }

        private void txtWard_Leave(object sender, EventArgs e)
        {
            if (txtWard.Text == "")
            {
                txtWard.Text = "病区名/简拼";
                txtWard.ForeColor = Color.Gray;
            }
        }
        /// <summary>
        /// 添加病区组
        /// </summary>
        private void cmb4Add()
        {
            string sql = " select distinct WardArea from DWard";
            DataTable dt = dbHelp.GetPIVAsDB(sql).Tables[0];
            comboBox4.Items.Add("全部病区组");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0] != null&& dt.Rows[i][0].ToString() != "")
                    {
                        comboBox4.Items.Add(dt.Rows[i][0]);
                    }
                }
            }
            comboBox4.SelectedIndex = 0;
        }
        /// <summary>
        /// 病区组改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataTable dt = DTWard.Copy();
            dt.Rows.Clear();
            DataRow[] DR;

            if (comboBox4.SelectedItem.ToString() != "全部病区组")
            {DR = DTWard.Select(" WardArea = '" + comboBox4.SelectedItem.ToString() + "'", "WardSeqNo ASC"); }
            else
            { DR = DTWard.Select("", "WardSeqNo ASC"); }

            foreach (DataRow dr in DR)
            {
                dt.ImportRow(dr);
            }
            dgvDWard.DataSource = dt;

        }

     

        /// <summary>
        /// 查询框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == "瓶签号/患者姓名/患者ID/床号/主药/溶媒/医嘱号")
            {
                textBox1.Text = string.Empty;
                textBox1.ForeColor = Color.Black;
              
            }
        }

        /// <summary>
        ///  查询框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            //if (textBox1.Text.Trim()=="")
            //{
            //    textBox1.Text = "瓶签号/姓名/床号";
            //    textBox1.ForeColor = Color.Silver;
            //    textBox1.SelectAll();
            //}
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                textBox1.Text = "瓶签号/患者姓名/患者ID/床号/主药/溶媒/医嘱号";
                textBox1.ForeColor = Color.Silver;
                //textBox1.SelectAll();
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            label17.Text = Time;
            BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text, comboBox5.Text);
            BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text, string.Empty, comboBox5.Text,cbbPrinted.Text);
        }

      

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.Trim() != "" && textBox1.Text.Trim() != "瓶签号/患者姓名/患者ID/床号/主药/溶媒/医嘱号")
                {
                    BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text, textBox1.Text.Trim(), comboBox5.Text,cbbPrinted.Text);
                }
                else
                {
                    BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text, string.Empty, comboBox5.Text,cbbPrinted.Text);
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textPatient_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_Enter(object sender, EventArgs e)
        {

            comboBox4.ForeColor = Color.Black;
        }

        private void comboBox4_Leave(object sender, EventArgs e)
        {
            comboBox4.ForeColor = Color.Gray;
        }


        const int HTLEFT = 10;
        const int HTRIGHT = 11;
        const int HTTOP = 12;
        const int HTTOPLEFT = 13;
        const int HTTOPRIGHT = 14;
        const int HTBOTTOM = 15;
        const int HTBOTTOMLEFT = 0x10;
        const int HTBOTTOMRIGHT = 17;

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

        private void cbbList_TextChanged(object sender, EventArgs e)
        {
            try
            {

                BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text, comboBox5.Text);
                BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text, string.Empty, comboBox5.Text,cbbPrinted.Text);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void linkLabel16_Click(object sender, EventArgs e)
        {
            //清单
            // AddChose(sender, 4, "清单:", cbbList);
            LinkChose(sender, cbbList);
        }

        private void lbCount_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox3.Text == "已配置")
                {
                    Process.Start("ConfectCollect.exe");
                }
                else if (comboBox3.Text == "已打包")
                {
                    Process.Start("PackCollect.exe");
                }
                else if (comboBox3.Text == "提前打包")
                {
                    Process.Start("PackCollect.exe");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// 服务部使用按钮1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            label17.Text = Time;
            string s = CheckSelectLabel().Trim();
            if (s.Length > 0||serbtn1==0)
            {
                if (MessageBox.Show("确认"+button1.Text.Trim()+"!", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string msg=string.Empty;
                    #region csw修改，需要此exe下配置文件IMEQPIVAs.ini
                    ICharge charge = ChargeFactory.GetCharge(dbHelp.IniReadValuePivas("Charge", "HospitalType"));
                    #endregion

                    charge.SPARE1(s, EmployeeID, out msg);
                    if (serbtn1==1){
                        MessageBox.Show(msg);
                    }
                    BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text, comboBox5.Text);
                    BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text, string.Empty, comboBox5.Text,cbbPrinted.Text);
                }
            }
            else
            {
                MessageBox.Show("请勾选需要操作的瓶签");
                return;
            }
        }
        /// <summary>
        /// 服务部使用按钮2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            label17.Text = Time;
            string s = CheckSelectLabel().Trim();
            if (s.Length > 0||serbtn2==0)
            {

                if (MessageBox.Show("确认" + button2.Text.Trim() + "!", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string msg = string.Empty;
                    #region csw修改，需要此exe下配置文件IMEQPIVAs.ini
                    ICharge charge = ChargeFactory.GetCharge(dbHelp.IniReadValuePivas("Charge", "HospitalType"));
                    #endregion

                    charge.SPARE2(s, EmployeeID, out msg);
                    if (serbtn2 == 1)
                        MessageBox.Show(msg);
                    BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text, comboBox5.Text);
                    BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text, string.Empty, comboBox5.Text,cbbPrinted.Text);
                    //BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text);
                }
            }
            else
            {
                MessageBox.Show("请勾选需要操作的瓶签");
                return;
            }
        }
        /// <summary>
        /// 服务部使用按钮3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            label17.Text = Time;
            string s = CheckSelectLabel().Trim();
            if (s.Length > 0||serbtn3==0)
            {

                if (MessageBox.Show("确认" + button3.Text.Trim() + "!", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string msg = string.Empty;
                    #region csw修改，需要此exe下配置文件IMEQPIVAs.ini
                    ICharge charge = ChargeFactory.GetCharge(dbHelp.IniReadValuePivas("Charge", "HospitalType"));
                    #endregion
                    charge.SPARE3(s, EmployeeID, out msg);
                    if (serbtn3 == 1)
                        MessageBox.Show(msg);
                    BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text, comboBox5.Text);
                    BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text, string.Empty, comboBox5.Text,cbbPrinted.Text);
                }
            }
            else
            {
                MessageBox.Show("请勾选需要操作的瓶签");
                return;
            }
        }

        private void cbbPrinted_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text, comboBox5.Text);
                BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text, string.Empty, comboBox5.Text, cbbPrinted.Text);
                // BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text, comboBox5.Text);
                BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text, string.Empty, comboBox5.Text, cbbPrinted.Text);
                // BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cb_yp_CheckedChanged(object sender, EventArgs e)
        {
            label17.Text = Time;
            BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text, comboBox5.Text);
            BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text, string.Empty, comboBox5.Text, cbbPrinted.Text);
        }

        private void cb_ys_CheckedChanged(object sender, EventArgs e)
        {

            label17.Text = Time;
            BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text, comboBox5.Text);
            BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text, string.Empty, comboBox5.Text, cbbPrinted.Text);
        }

       
        

        private void cb_hour1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            label17.Text = Time;
            BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text, comboBox5.Text);
            BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text, string.Empty, comboBox5.Text, cbbPrinted.Text);

        }

        private void cb_hour2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            label17.Text = Time;
            BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text, comboBox5.Text);
            BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text, string.Empty, comboBox5.Text, cbbPrinted.Text);
        }

        private void checkBox9_Click(object sender, EventArgs e)
        {
            //string Str = string.Empty;
            //if (LaseSelectLabel != "配置取消")
            //{ Str = "配置取消"; }
            //else
            //{Str="已打包";}

            //comboBox3.Text = "";
            //comboBox3.Text = LaseSelectLabel;
        }



        #region 排序--张衡2015-01-17改

        public static bool IsNumeric(string str)
        {
            if (str.Length > 10)
                return false;
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            byte[] bytestr = ascii.GetBytes(str);
            if (bytestr[0] == 43)
                return true;
            foreach (byte c in bytestr)
            {
                if ((c < 48 || c > 57))
                {
                    return false;
                }
            }
            return true;
        }
        public class RowComparer : IComparer<DataRow>
        {
            public Dictionary<int, System.Windows.Forms.SortOrder> SortColumns { get; set; }

            private static int sortOrderModifier = 1;

            public RowComparer(System.Windows.Forms.SortOrder sortOrder)
            {
                if (sortOrder ==System.Windows.Forms.SortOrder.Descending)
                {
                    sortOrderModifier = -1;
                }
                else if (sortOrder == System.Windows.Forms.SortOrder.Ascending)
                {
                    sortOrderModifier = 1;
                }
            }

            public int Compare(System.Data.DataRow x, System.Data.DataRow y)
            {
                int compareResult = 0;

                foreach (int key in SortColumns.Keys)
                {
                    string value1 = string.Empty;
                    string value2 = string.Empty;
                    //int compareResult;
                    // Check for nulls
                    if (x.ItemArray[key] == DBNull.Value)
                        value1 = "0";
                    else
                    {
                        value1 = x.ItemArray[key].ToString().TrimEnd('+');
                        //if (value1.Contains('监'))
                        //{
                        //    value1 = value1.TrimStart('监'); 
                        //}
                        if (value1.Contains('.'))
                        {
                            value1 = value1.TrimStart('.');
                        }
                        if (value1.Contains('床'))
                        {
                            //value1.Replace("床", "");
                            value1 = value1.TrimEnd('床');
                        }
                    }
                    if (y.ItemArray[key] == DBNull.Value)
                        value2 = "0";
                    else
                    {
                        value2 = y.ItemArray[key].ToString().TrimEnd('+');
                       
                        if (value2.Contains('.'))
                        {
                            value2 = value2.TrimStart('.');
                        }
                        if (value2.Contains('床'))
                        {
                            //value2.Replace("床", "");
                            value2 = value2.TrimEnd('床');
                        }
                    }

                    if (IsNumeric(value1) && IsNumeric(value2))
                    {
                        if (int.Parse(value1) > int.Parse(value2))
                        {
                            compareResult = 1;
                        }
                        else if (int.Parse(value1) < int.Parse(value2))
                        {
                            compareResult = -1;
                        }
                        else
                        {
                            compareResult = 0;
                        }
                    }
                    else if (IsNumeric(value1))
                    {
                        compareResult = -1;

                    }
                    else if (IsNumeric(value2))
                    {
                        compareResult = 1;
                    }
                    else
                    {

                        compareResult = System.String.Compare(value1, value2);
                    }

                }
                return compareResult * sortOrderModifier;
            }

        }

        public DataTable dvtodt(DataGridView dv)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            for (int i = 0; i < dv.Columns.Count; i++)
            {
                dc = new DataColumn();
                dc.ColumnName = dv.Columns[i].Name.ToString();
                dt.Columns.Add(dc);
            }
            for (int j = 0; j < dv.Rows.Count; j++)
            {
                DataRow dr = dt.NewRow();
                for (int x = 0; x < dv.Columns.Count; x++)
                {
                    dr[x] = dv.Rows[j].Cells[x].Value;
                }
                dt.Rows.Add(dr);
            }
            dt.Columns.Remove("Select");
            return dt;
        }
        int m = 0;
        string lieMing;


        private void labelPX(DataTable dt)
        {
            dgvPre.Rows.Clear();
            if (dt.Rows.Count > 0)
            {           
                if (comboBox3.Text == "提前打包")
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dgvPre.Rows.Add(dt.Rows[i]);
                        dgvPre.Rows[i].Cells["LabelNo"].Value = dt.Rows[i]["LabelNo"].ToString().Trim();
                        dgvPre.Rows[i].Cells["remark6"].Value = dt.Rows[i]["remark6"].ToString().Trim();
                        dgvPre.Rows[i].Cells["BedNo"].Value = dt.Rows[i]["BedNo"].ToString().Trim();
                        dgvPre.Rows[i].Cells["PatName"].Value = dt.Rows[i]["PatName"].ToString().Trim();
                        dgvPre.Rows[i].Cells["DWard"].Value = dt.Rows[i]["DWard"].ToString().Trim();
                        dgvPre.Rows[i].Cells["GroupNo"].Value = dt.Rows[i]["GroupNo"].ToString().Trim();
                        dgvPre.Rows[i].Cells["Batch"].Value = dt.Rows[i]["Batch"].ToString().Trim();
                        dgvPre.Rows[i].Cells["IVStatus"].Value = "提前打包";
                        dgvPre.Rows[i].Cells["MainDrug"].Value = dt.Rows[i]["MainDrug"].ToString().Trim();
                        dgvPre.Rows[i].Cells["MenstruumName"].Value = dt.Rows[i]["MenstruumName"].ToString().Trim();
                        dgvPre.Rows[i].Cells["PrescriptionID"].Value = dt.Rows[i]["PrescriptionID"].ToString().Trim();
                        dgvPre.Rows[i].Cells["Remark"].Value = dt.Rows[i]["Remark"].ToString().Trim();
                        dgvPre.Rows[i].Cells["Select"].Value = false;
                        dgvPre.Rows[i].Cells["Class"].Value = dt.Rows[i]["Class"].ToString().Trim();
                        dgvPre.Rows[i].Cells["chrgetype"].Value = dt.Rows[i]["chrgetype"].ToString().Trim();
                    }
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dgvPre.Rows.Add(dt.Rows[i]);
                        dgvPre.Rows[i].Cells["LabelNo"].Value = dt.Rows[i]["LabelNo"].ToString().Trim();
                        dgvPre.Rows[i].Cells["remark6"].Value = dt.Rows[i]["remark6"].ToString().Trim();
                        dgvPre.Rows[i].Cells["BedNo"].Value = dt.Rows[i]["BedNo"].ToString().Trim();
                        dgvPre.Rows[i].Cells["PatName"].Value = dt.Rows[i]["PatName"].ToString().Trim();
                        dgvPre.Rows[i].Cells["DWard"].Value = dt.Rows[i]["DWard"].ToString().Trim();
                        dgvPre.Rows[i].Cells["GroupNo"].Value = dt.Rows[i]["GroupNo"].ToString().Trim();
                        dgvPre.Rows[i].Cells["Batch"].Value = dt.Rows[i]["Batch"].ToString().Trim();
                        dgvPre.Rows[i].Cells["IVStatus"].Value = dt.Rows[i]["IVStatus"].ToString();
                        dgvPre.Rows[i].Cells["MainDrug"].Value = dt.Rows[i]["MainDrug"].ToString().Trim();
                        dgvPre.Rows[i].Cells["MenstruumName"].Value = dt.Rows[i]["MenstruumName"].ToString().Trim();
                        dgvPre.Rows[i].Cells["PrescriptionID"].Value = dt.Rows[i]["PrescriptionID"].ToString().Trim();
                        dgvPre.Rows[i].Cells["Remark"].Value =dt.Rows[i]["Remark"].ToString().Trim();
                        dgvPre.Rows[i].Cells["Select"].Value = false;
                        dgvPre.Rows[i].Cells["Class"].Value = dt.Rows[i]["Class"].ToString().Trim();
                        dgvPre.Rows[i].Cells["chrgetype"].Value =  dt.Rows[i]["chrgetype"].ToString().Trim();
                    }
                }
            }
            checkBox2.Checked = false;
        }
        #endregion

       

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            label17.Text = Time;
            cbo3();
            BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text, comboBox5.Text);
            BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text, string.Empty, comboBox5.Text, cbbPrinted.Text);
        }


        /// <summary>
        /// 导出EXCEL
        /// </summary>
        /// <param name="m_DataView"></param>
        public void DataToExcel(DataGridView dgv)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Execl files (*.xls)|*.xls";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.CreatePrompt = true;
            dlg.Title = "保存为Excel文件";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Stream myStream;
                myStream = dlg.OpenFile();
                StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));
               
                string columnTitle = "";
                try
                {
                    //写入列标题
                    for (int i = 1; i < dgv.ColumnCount; i++)
                    {
                        if (i > 1)
                        {
                            columnTitle += "\t";
                        }
                        columnTitle += dgv.Columns[i].HeaderText;
                    }
                    sw.WriteLine(columnTitle);
                    //写入列内容
                    for (int j = 0; j < dgv.Rows.Count; j++)
                    {
                        string columnValue = "";
                        for (int k =1; k < dgv.Columns.Count; k++)
                        {
                            if (k > 1)
                            {
                                columnValue += "\t";
                            }

                            if (dgv.Rows[j].Cells[k].Value == null)
                                columnValue += " ";
                            else
                            {  
                                columnValue += " " + dgv.Rows[j].Cells[k].Value.ToString();
                            }
                        }
                        sw.WriteLine(columnValue);
                    }
                    sw.Close();
                    myStream.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                finally
                {
                    sw.Close();
                    myStream.Close();
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataToExcel(this.dgvPre);
        }


        private string GetPrintOrder()
        {
            string l="Order by ";
            string order = "病区组,批次,主药,病区";
            string sx = comboBox2.Text == "" || comboBox2.Text == "<全部>" ? "All" : comboBox2.Text;
            string status = comboBox3.Text == "" ? "<全部>" : comboBox3.Text;
            if (dbHelp.IniReadValuePivas(status, sx) == "" || dbHelp.IniReadValuePivas(status, sx) == string.Empty)
            {
                dbHelp.IniWriteValuePivas(status, sx, order);
            }
            else
            {
                order = dbHelp.IniReadValuePivas(status, sx);
            }
            string[] s = order.Split(',');
            listBox1.Items.Clear();
            foreach (string a in s)
            {
                listBox1.Items.Add(a);
                l = l + Getname(a) + ",";
            }
            label42.Text = "条件:" + status +" && "+ sx;
            l=l.Substring(0, l.Length - 1);
            return l;
         
            
        }



        private void button7_Click(object sender, EventArgs e)
        {
            panel9.Visible = !panel9.Visible;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if (index == -1) return;
            if (index == 0) return;
            string[] strs = listBox1.Items[index].ToString().Split('\t');
            string[] strs2 = listBox1.Items[index - 1].ToString().Split('\t');
            string temp = string.Empty;
            temp = strs[0];
            strs[0] = strs2[0];
            strs2[0] = temp;
            listBox1.Items[index] = strs[0] + "\t";
            listBox1.Items[index - 1] = strs2[0] + "\t";
            listBox1.SelectedIndex = index - 1;
            StoreOrder();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if (index == -1) return;
            if (index == listBox1.Items.Count - 1) return;
            string[] strs = listBox1.Items[index].ToString().Split('\t');
            string[] strs2 = listBox1.Items[index + 1].ToString().Split('\t');
            string temp = string.Empty;
            temp = strs[0];
            strs[0] = strs2[0];
            strs2[0] = temp;
            listBox1.Items[index] = strs[0] + "\t";
            listBox1.Items[index + 1] = strs2[0] + "\t";
            listBox1.SelectedIndex = index + 1;
            StoreOrder();
        }

        private void StoreOrder()
        {
            string order="";
            foreach (string a in listBox1.Items)
            {
                order += order == "" ? a.Trim() : "," + a.Trim();
            }

            string sx = comboBox2.Text == "" || comboBox2.Text == "<全部>" ? "All" : comboBox2.Text;
            string status = comboBox3.Text == "" ? "<全部>" : comboBox3.Text;
            dbHelp.IniWriteValuePivas(status, sx, order);
        }

        private string Getname(string a)
        {
            switch (a)
            {
                case "病区组": return "D.WardArea";   
                case "病区": return "iv.WardCode";
                case "批次": return "iv.Batch";
                case "主药": return "MainDrug";
                default: return "";
            }
        }

        private void dgvPre_MouseDown(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Right)
            {
                HISJF his = new HISJF(this.dgvPre.CurrentRow.Cells["LabelNo"].Value.ToString());
                his.ShowDialog();
            }
        }
    
        private void linkLabel29_Click(object sender, EventArgs e)
        {
            panel10.Visible = false;               
            ischargePZ = "2";
            checkBox10.Visible = false;
            ReflashPanel10();
            ChargeTypeStr();

            if (((LinkLabel)sender).Image != null)
            {
                ((LinkLabel)sender).Image = null;             
            }
            else
            {
                CheckNull(((LinkLabel)sender).Parent);//清空父容器勾选
                ((LinkLabel)sender).Image = Properties.Resources.tick;
            }
           
            BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text, comboBox5.Text);
            BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text, string.Empty, comboBox5.Text, cbbPrinted.Text);
      

        }

        private void linkLabel30_Click(object sender, EventArgs e)
        {
            panel10.Visible = true;
            ischargePZ = "1";
            checkBox10.Visible = false;
            ReflashPanel10();
               ChargeTypeStr();

            if (((LinkLabel)sender).Image != null)
            {
                ((LinkLabel)sender).Image = null;
                ischargePZ = "2";            
            }
            else
            {
                CheckNull(((LinkLabel)sender).Parent);//清空父容器勾选
                ((LinkLabel)sender).Image = Properties.Resources.tick;
            }
                    
            BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text, comboBox5.Text);
            BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text, string.Empty, comboBox5.Text, cbbPrinted.Text);
        }

        private void linkLabel34_Click(object sender, EventArgs e)
        {
            panel10.Visible = true;
            checkBox10.Visible = true;
            ischargePZ = "0";
            ReflashPanel10();
            ChargeTypeStr();
            if (((LinkLabel)sender).Image != null)
            {
                ((LinkLabel)sender).Image = null;
                ischargePZ = "2";
            }
            else
            {
                CheckNull(((LinkLabel)sender).Parent);//清空父容器勾选
                ((LinkLabel)sender).Image = Properties.Resources.tick;
            }    
            BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text, comboBox5.Text);
            BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text, string.Empty, comboBox5.Text, cbbPrinted.Text);
        }
        /// <summary>
        /// 重置panel10
        /// </summary>
        private void ReflashPanel10()
        {
            foreach (CheckBox cb in panel10.Controls)
            { cb.Checked = true; }
        }

        private void linkLabel35_Click(object sender, EventArgs e)
        {
            //药品类型
            LinkChoseDrug(sender, comboBox5);
        }

        private void checkBox10_Click(object sender, EventArgs e)
        {
            BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text, comboBox5.Text);
            BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text, string.Empty, comboBox5.Text, cbbPrinted.Text);
        }

        
        private void btnPrint_Click(object sender, EventArgs e)
        {
            string labels=getLabelstr();
            if (!string.IsNullOrEmpty(labels))
            {
                PrintPreviews pp = new PrintPreviews(labels, dateTimePicker1.Value.ToString());
                pp.ShowDialog();
            }
            else
            {
                MessageBox.Show("未勾选任何瓶签！");
            }
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {

            label17.Text = Time;
            updatelist();
            cb_hour1.Text = cb_hour2.Text = "";
            BDdvgDWard(dateTimePicker1.Text, comboBox3.Text, comboBox1.Text, comboBox2.Text, comboBox5.Text);
            BDDvg(dateTimePicker1.Text, WardCode, comboBox3.Text, comboBox1.Text, comboBox2.Text, string.Empty, comboBox5.Text, cbbPrinted.Text);
        }

     


     
        
    }

    
}
