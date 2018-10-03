using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using qqClient;
using PIVAsCommon.Helper;
using PivasLimitDES;

namespace PivasNurse
{
    public partial class NurseWorkStation : Form
    {
        /// <summary>
        /// 记录按下的按钮
        /// </summary>
        string SetStr = string.Empty;
        DB_Help dbHelp = new DB_Help();
        public string TimeNo = "";
        Boolean QiPao;//气泡消息框是否使用变量
        SQL sql = new SQL();
        public DateTime lastTime = DateTime.Now;
        public NurseWorkStation()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            TimeNo = label11.Text;
        }

        public void SetTimeNO()
        {
            label11.Text = TimeNo;
        }

        public NurseWorkStation(string wardcode, string pcode)
        {
            InitializeComponent();
            TimeNo = label11.Text;
            //this.EmployeeID = pcode;
            Txt_ward.Tag = wardcode;
            DataSet dsw = new DataSet();
            dsw = dbHelp.GetPIVAsDB("select * from DWard where WardCode = '" + wardcode + "'  select DEmployeeID,DEmployeeName from DEmployee where AccountID='" + pcode + "'");
            if (dsw != null && dsw.Tables[0].Rows.Count > 0)
            {
                Txt_ward.Text = dsw.Tables[0].Rows[0]["WardName"].ToString();
            }
            if (dsw != null && dsw.Tables[1].Rows.Count > 0)
            {
                this.EmployeeID = dsw.Tables[1].Rows[0]["DEmployeeID"].ToString();

            }
            dsw.Dispose();
        }
        public string EmployeeID;

        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion

        #region   画面效果触发
        /// <summary>
        /// 关闭按钮——鼠标移上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel_Close_MouseHover(object sender, EventArgs e)
        {
            Panel_Close.BackColor = Color.FromArgb(84, 199, 253);
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
            Application.Exit();
            //this.Visible = false; //这个也可以
            //this.Hide();
            //this.WindowState = FormWindowState.Minimized;
            //this.notifyIcon1.Visible = true;

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

        private void label2_MouseHover(object sender, EventArgs e)
        {
            label3.Visible = true;
            //label4.Visible = true;
            label5.Visible = true;
        }

        /// <summary>
        /// 审方处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label3_MouseHover(object sender, EventArgs e)
        {
            //label3.BackColor = Color.FromArgb(37, 98, 149);
        }

        /// <summary>
        /// 审方处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label3_MouseLeave(object sender, EventArgs e)
        {
            if (SetStr != "审方处理")
            {
                label3.ForeColor = Color.White;
            }
        }

        ///// <summary>
        ///// 退方查询
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void label4_MouseHover(object sender, EventArgs e)
        //{
        //    label4.ForeColor = Color.FromArgb(37, 98, 149);
        //}

        ///// <summary>
        ///// 退方查询
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void label4_MouseLeave(object sender, EventArgs e)
        //{
        //    if (SetStr != "退方查询")
        //    {
        //        label4.ForeColor = Color.White;
        //    }
        //}

        /// <summary>
        /// 单项退药
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label5_MouseHover(object sender, EventArgs e)
        {
            //label5.ForeColor = Color.FromArgb(37, 98, 149);
        }

        /// <summary>
        /// 单项退药
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label5_MouseLeave(object sender, EventArgs e)
        {
            if (SetStr != "单项退药")
            {
                label5.ForeColor = Color.White;
            }
        }

        /// <summary>
        /// 提前打包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label6_MouseHover(object sender, EventArgs e)
        {
            //label6.ForeColor = Color.FromArgb(37, 98, 149);
        }

        /// <summary>
        /// 提前打包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label6_MouseLeave(object sender, EventArgs e)
        {
            if (SetStr != "提前打包")
            {
                label6.ForeColor = Color.White;
            }
        }

        /// <summary>
        /// 等待签收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label7_MouseHover(object sender, EventArgs e)
        {
            //label7.ForeColor = Color.FromArgb(37, 98, 149);
        }

        /// <summary>
        /// 等待签收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label7_MouseLeave(object sender, EventArgs e)
        {
            if (SetStr != "等待签收")
            {
                label7.ForeColor = Color.White;
            }
        }

        /// <summary>
        /// 医嘱查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label8_MouseHover(object sender, EventArgs e)
        {
            //label8.ForeColor = Color.FromArgb(37, 98, 149);
        }

        /// <summary>
        /// 医嘱查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label8_MouseLeave(object sender, EventArgs e)
        {
            if (SetStr != "医嘱查询")
            {
                label8.ForeColor = Color.White;
            }
        }

        #endregion

        /// <summary>
        /// 博龙公司介绍
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_Click(object sender, EventArgs e)
        {
            //IntroductionForm IForm = new IntroductionForm();
            //IForm.ShowDialog();
        }



        /// <summary>
        /// 审方处理按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label3_Click(object sender, EventArgs e)
        {
            if (GetPivasLimit.Instance.Limit(EmployeeID, "08001"))
            {
                SetTimeNO();
                ButtonToWhite("审方处理");
                SetStr = "审方处理";

                panel3.Controls.Clear();
                ShenFangCL sf = new ShenFangCL(EmployeeID, (string)Txt_ward.Tag);
                sf.Dock = DockStyle.Fill;
                panel3.Controls.Add(sf);
            }
          
        }

        ///// <summary>
        ///// 退方查询按钮
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void label4_Click(object sender, EventArgs e)
        //{
        //    SetTimeNO();
        //    ButtonToWhite("退方查询");
        //    SetStr = "退方查询";
   
        //    panel3.Controls.Clear();
        //    TuiFangCX sf = new TuiFangCX(EmployeeID, (string)Txt_ward.Tag);
        //    sf.Dock = DockStyle.Fill;
        //    panel3.Controls.Add(sf);
        //}

        /// <summary>
        /// 单项退药按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label5_Click(object sender, EventArgs e)
        {
            if (GetPivasLimit.Instance.Limit(EmployeeID, "08002"))
            {
                SetTimeNO();
                ButtonToWhite("单项退药");
                SetStr = "单项退药";
                panel3.Controls.Clear();
                DXTuiYao sf = new DXTuiYao(EmployeeID, (string)Txt_ward.Tag);
                sf.Dock = DockStyle.Fill;
                panel3.Controls.Add(sf);
            }
          
        }

        /// <summary>
        /// 提前打包按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label6_Click(object sender, EventArgs e)
        {
            if (GetPivasLimit.Instance.Limit(EmployeeID, "08003"))
            {
                SetTimeNO();
                ButtonToWhite("提前打包");
                SetStr = "提前打包";
                panel3.Controls.Clear();
                TiQianDB sf = new TiQianDB(EmployeeID, (string)Txt_ward.Tag);
                sf.Dock = DockStyle.Fill;
                panel3.Controls.Add(sf);
            }
         

        }

        /// <summary>
        /// 签收按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label7_Click(object sender, EventArgs e)
        {
            if (GetPivasLimit.Instance.Limit(EmployeeID, "08004"))
            {
                SetTimeNO();
                ButtonToWhite("等待签收");
                SetStr = "等待签收";
                panel3.Controls.Clear();
                QianShou Qs = new QianShou(EmployeeID, (string)Txt_ward.Tag);
                Qs.Dock = DockStyle.Fill;
                panel3.Controls.Add(Qs);
            }
      
        }

        /// <summary>
        /// 医嘱查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label8_Click(object sender, EventArgs e)
        {
            if (GetPivasLimit.Instance.Limit(EmployeeID, "08005"))
            {
                SetTimeNO();
                ButtonToWhite("医嘱查询");
                SetStr = "医嘱查询";
                panel3.Controls.Clear();
                YiZhuCX Qs = new YiZhuCX(EmployeeID, (string)Txt_ward.Tag);
                Qs.Dock = DockStyle.Fill;
                //_7HeDui qs = new _7HeDui();
                //hedui2 qs = new hedui2();
                panel3.Controls.Add(Qs);
            }
          
        }
        /// <summary>
        /// 瓶签查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label4_Click(object sender, EventArgs e)
        {
            if (GetPivasLimit.Instance.Limit(EmployeeID, "08006"))
            {
                SetTimeNO();
                ButtonToWhite("瓶签查询");
                SetStr = "瓶签查询";
                panel3.Controls.Clear();
                LabelNoSearch LS = new LabelNoSearch(EmployeeID, (string)Txt_ward.Tag);
                LS.Dock = DockStyle.Fill;

                panel3.Controls.Add(LS);
            }
        

        }
        private void ButtonToWhite(string But)
        {
           
            if (But != "审方处理")
            {
                //label3.BorderStyle = System.Windows.Forms.BorderStyle.None;
                label3.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
            else
            {

                label3.FlatStyle = System.Windows.Forms.FlatStyle.System;        
                label3.Font = new System.Drawing.Font("宋体", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
            //if (But != "退方查询")
            //{
            //    label4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            //    label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //}
            //else
            //{
            //    label4.ForeColor = Color.White;
            //    label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            //    label4.Font = new System.Drawing.Font("宋体", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //}
            if (But != "单项退药")
            {
                //label5.BorderStyle = System.Windows.Forms.BorderStyle.None;
                label5.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
            else
            {
                //label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
                label5.Font = new System.Drawing.Font("宋体", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            }
            if (But != "提前打包")
            {
                //label6.BorderStyle = System.Windows.Forms.BorderStyle.None;
                label6.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
            else
            {
                //label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                label6.FlatStyle = System.Windows.Forms.FlatStyle.System;
                label6.Font = new System.Drawing.Font("宋体", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
            if (But != "等待签收")
            {
                //label7.BorderStyle = System.Windows.Forms.BorderStyle.None;
                label7.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
            else
            {

                //label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                label7.FlatStyle = System.Windows.Forms.FlatStyle.System;
                label7.Font = new System.Drawing.Font("宋体", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
            if (But != "医嘱查询")
            {
                //label8.BorderStyle = System.Windows.Forms.BorderStyle.None;
                label8.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                label8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
            else
            {
                //label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                label8.FlatStyle = System.Windows.Forms.FlatStyle.System;
                label8.Font = new System.Drawing.Font("宋体", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
            if (But != "瓶签查询")
            {
                //label8.BorderStyle = System.Windows.Forms.BorderStyle.None;
                label4.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
            else
            {
                //label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
                label4.Font = new System.Drawing.Font("宋体", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
        }


        //生成按钮
        private void IsEnble()
        {
            string IsScSql = "select * from LongOrder where WardCode='" + Txt_ward.Tag + "' and DATEDIFF(DD, GETDATE(), OrderDate)=0 ";
            using (DataTable IsScDt = dbHelp.GetPIVAsDB(IsScSql).Tables[0])
            {
                if (IsScDt.Rows.Count > 0)
                {
                    btn1.Enabled = false;
                    btn1.Text = "已生成";
                }
                else
                {
                    btn1.Enabled = true;
                    btn1.Text = "生成";
                }
            }
        }

        ////插入LongOrder表
        //private void button_SC_Click(object sender, EventArgs e)
        //{
        //    SetTimeNO();
        //    string InsertSql = " insert into LongOrder(WardCode,OrderDate,DCode) values ('" + Txt_ward.Tag + "', GETDATE(),'" + EmployeeID + "')";
        //    int a = db.SetPIVAsDB(InsertSql);
        //    IsEnble();
        //}

        private void NurseWorkStation_Load(object sender, EventArgs e)
        {
 
            try
            {

                if (!JugeRight())
                {
                    Application.Exit();
                }
                //label7_Click(null, null);
                IsEnble();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }


            try
            {
                IniRemTime = Convert.ToInt32(dbHelp.IniReadValuePivas("Remind", "RemindTime")) * 60;
                keeptime = Convert.ToInt32(dbHelp.IniReadValuePivas("Remind", "KeepTime"));
                remindtime = IniRemTime;
            }
            catch (Exception ex)
            {
                IniRemTime = 180;
                remindtime = IniRemTime;
                keeptime = 5;

                dbHelp.IniWriteValuePivas("Remind", "RemindTime", "3");
                dbHelp.IniWriteValuePivas("Remind", "KeepTime", "5");
            }
            if (dbHelp.GetPivasAllSet("护士站_气泡消息框") == "0")
            {
                QiPao = false;
            }
            else
            {
                QiPao =true; 
            }
            if (dbHelp.GetPivasAllSetValue2("护士站-按钮一") == "1") 
            {
                btn1.Visible = true;
                btn1.Text = dbHelp.GetPivasAllSet("护士站-按钮一");
                Match m = Regex.Match(dbHelp.GetPivasAllSetValue3("护士站-按钮一"), "^([0-9]{0,3},){2}[0-9]{0,3}$");
                if (m.Success)
                {
                    string[] a = dbHelp.GetPivasAllSetValue3("护士站-按钮一").Split(',');
                    btn1.ForeColor = Color.FromArgb(Convert.ToInt32(a[0]), Convert.ToInt32(a[1]), Convert.ToInt32(a[2]));
                }
            }
            if (dbHelp.GetPivasAllSetValue2("护士站-按钮二") == "1")
            {
                btn2.Visible = true;
                btn2.Text = dbHelp.GetPivasAllSet("护士站-按钮二");
                Match m = Regex.Match(dbHelp.GetPivasAllSetValue3("护士站-按钮二"), "^([0-9]{0,3},){2}[0-9]{0,3}$");
                if (m.Success)
                {
                    string[] a = dbHelp.GetPivasAllSetValue3("护士站-按钮二").Split(',');
                    btn2.ForeColor = Color.FromArgb(Convert.ToInt32(a[0]), Convert.ToInt32(a[1]), Convert.ToInt32(a[2]));
                }
            }
            if (dbHelp.GetPivasAllSetValue2("护士站-按钮三") == "1")
            {
                btn3.Visible = true;
                btn3.Text = dbHelp.GetPivasAllSet("护士站-按钮三");
                Match m = Regex.Match(dbHelp.GetPivasAllSetValue3("护士站-按钮三"), "^([0-9]{0,3},){2}[0-9]{0,3}$");
                if (m.Success)
                {
                    string[] a = dbHelp.GetPivasAllSetValue3("护士站-按钮三").Split(',');
                    btn3.ForeColor = Color.FromArgb(Convert.ToInt32(a[0]), Convert.ToInt32(a[1]), Convert.ToInt32(a[2]));
                }
            }
            if (dbHelp.GetPivasAllSet("护士站-标题显示-1") == "0")
            {
                label3.Visible = false;
            }
            else
            {
                label3.Visible = true;
            }
            if (dbHelp.GetPivasAllSetValue2("护士站-标题显示-1") == "0")
            {
                label5.Visible = false;
            }
            else
            {
                label5.Visible = true;
            }
            if (dbHelp.GetPivasAllSetValue3("护士站-标题显示-1") == "0")
            {
                label6.Visible = false;
            }
            else
            {
                label6.Visible = true;
            }
            if (dbHelp.GetPivasAllSet("护士站-标题显示-2") == "0")
            {
                label7.Visible = false;
            }
            else
            {
                label7.Visible = true; 
            }
            if (dbHelp.GetPivasAllSetValue2("护士站-标题显示-2") == "0")
            {
                label8.Visible = false;
            }
            else
            {
                label8.Visible = true; 
            }
            if (dbHelp.GetPivasAllSetValue3("护士站-标题显示-2") == "0")
            {
                label4.Visible = false;
            }
            else
            {
                label4.Visible = true; 
            }

           
            //是否闪烁
            ShanShuo();
            
          
        }

        private bool JugeRight()
        {
            if (GetPivasLimit.Instance.LimitNoMsg(EmployeeID, "08004"))
            {
                label7_Click(null, null);
                //IsEnble();             
                return true;
            }
            else if (GetPivasLimit.Instance.LimitNoMsg(EmployeeID, "08001"))
            {
                label3_Click(null, null);
                //IsEnble();
                return true;
            }
            else if (GetPivasLimit.Instance.LimitNoMsg(EmployeeID, "08002"))
            {
                label5_Click(null, null);
                //IsEnble();
                return true;
            }
            else if (GetPivasLimit.Instance.LimitNoMsg(EmployeeID, "08003"))
            {
                label6_Click(null, null);
                //IsEnble();
                return true;
            }
            else if (GetPivasLimit.Instance.LimitNoMsg(EmployeeID, "08005"))
            {
                label8_Click(null, null);
                //IsEnble();
                return true;
            }
            else if (GetPivasLimit.Instance.LimitNoMsg(EmployeeID, "08006"))
            {
                label4_Click(null, null);
                //IsEnble();
                return true;
            }
            else
            {
                MessageBox.Show("您没有此操作权限，请于系统管理员联系！！！");
                return false;
            }
            

        }

        private void notifyIcon1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;

            }
          

        }

     
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    SetTimeNO();
        //    int m = 0;
        //    using (DataTable dt = db.GetPIVAsDB(" select WardCode from DWard where IsOpen=1").Tables[0])
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                string InsertSql = " insert into LongOrder(WardCode,OrderDate,DCode) values ('" + dt.Rows[i][0] + "', DATEADD(DD,1,GETDATE()),'" + EmployeeID + "')";
        //                int a = db.SetPIVAsDB(InsertSql);
        //                if (a > 0)
        //                    m = m + 1;
        //            }

        //        }
        //    }
        //    if (m > 0)
        //    {

        //        button1.Enabled = false;
        //        button1.Text = "已经生成";
        //    }
        //}

        /// <summary>
        /// 定时刷新生成按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            IsEnble();
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        SetTimeNO();
        //        ProcessStartInfo P = new ProcessStartInfo("labelprint.exe", Txt_ward.Tag.ToString());
        //        //MessageBox.Show(Txt_ward.Tag.ToString());
        //        Process.Start(P);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        /// <summary>
        /// 张家港检测HIS是否开启。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                //if (Process.GetProcessesByName("WardManager").Length == 0)
                //{
                //    Application.Exit();
                //}
                //if (label11.Text != "0")
                //{ label11.Text = (int.Parse(label11.Text) - 1).ToString(); }
                //else
                //{
                //    this.Dispose();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

   
        private void pic_max_Click(object sender, EventArgs e)
        {

            if (this.WindowState == FormWindowState.Normal)
            {
                this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                this.WindowState = FormWindowState.Maximized;
                pic_max.BackgroundImage = Properties.Resources._22;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                pic_max.BackgroundImage = Properties.Resources._20;
            }
        }

        private void pic_max_MouseHover(object sender, EventArgs e)
        {
            pic_max.BackColor = Color.FromArgb(84, 199, 253);
        }

        private void pic_max_MouseLeave(object sender, EventArgs e)
        {
            pic_max.BackColor = Color.Transparent;
        }
        //右击退出
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {  
            string head = "退出帐号";
            string content = "是否结束程序？";
            Exit exit = new Exit(head, content);
           DialogResult dd= exit.ShowDialog();
           if (dd == DialogResult.Yes && exit.Res == 1)
           {
               this.Dispose();
               Application.Exit();
           }
        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string head = "注销帐号";
            string content = "是否注销帐号，返回登录界面？";
            Exit exit = new Exit(head, content);
            DialogResult dd= exit.ShowDialog();
            if(dd==DialogResult.Yes&&exit.Res==1)
            {
            Login login = (Login)this.Owner;
            login.showhide();
            this.Dispose();
            }
        }

    

        #region 控制提醒窗体变量      
        int remindtime = 10;//倒计时使用
        int IniRemTime = 10;//10秒弹出一次
        Ball pnb;
        int keeptime = 5;//提醒窗口停留时间
        bool IsDelay = false;//是否延迟提醒，默认false不延迟
        int DelayTime = 0;
        int DelayIndex = 2;//提醒窗口combobox 索引
        bool MouseOnfrmRemind = false;//判断鼠标是否停留在提醒窗体上 true 在，false 不在
        DB_Help DB = new DB_Help();
        #endregion
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Screen[] screens = Screen.AllScreens;
            Screen screen = screens[0];
            if (e.Button == MouseButtons.Left)
            {
                if (CheckFormIsOpen("Ball"))
                {
                    pnb.Close();
                }
                pnb = new Ball(Txt_ward.Text.ToString(), Txt_ward.Tag.ToString());
              
                pnb.setMouseOn += new Ball.MouseOn(SetMouseOnRemind);
                pnb.setRemindTime += new Ball.RemindTime(setRemindTime);
                pnb.setDelayTime += new Ball.DelayTime(setDelayTime);

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
               
                if (!pnb.SetRemindForm(index1, index2, DelayIndex))
                {

                    pnb.Dispose();
                    pnb = null;
                    remindtime = IniRemTime;
                    return;
                }
            
            }
            //fnotify.MdiParent = this;
            pnb.Left = screen.WorkingArea.Width - pnb.Width - 10;
            pnb.Top = screen.WorkingArea.Height;
            pnb.Show();
            while (pnb.Top > screen.WorkingArea.Height - pnb.Height)
            {
                Application.DoEvents();
                pnb.Top--;
            }
            remindtime = IniRemTime;   
            }
   
      /// <summary>
        /// 设置提醒窗口相关时间,单位秒
        /// </summary>
        /// <param name="time">弹出间隔时间</param>
        /// <param name="keetime">窗口停留时间</param>
        public void setRemindTime(int time,int keetime)
        {
            try
            {
                timer2.Enabled = false;
                IniRemTime = time;
                remindtime = IniRemTime;
                keeptime = keetime;
                timer2.Enabled = true;
            }
            catch (System.Exception ex)
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
        public void SetMouseOnRemind(bool On)
        {
            MouseOnfrmRemind = On;
        }
        private void timeremind_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!QiPao)//若气泡不自动弹出，直接退出。
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
                    if (pnb==null&& remindtime == 0)
                    {

                        if (CheckFormIsOpen("Ball"))
                        {
                            pnb.Close();
                        }
                        Screen[] screens = Screen.AllScreens;
                        Screen screen = screens[0];
                        pnb= new Ball(Txt_ward.Text.ToString(), Txt_ward.Tag.ToString());
                      

                        pnb.setMouseOn += new Ball.MouseOn(SetMouseOnRemind);
                        pnb.setRemindTime += new Ball.RemindTime(setRemindTime);
                        pnb.setDelayTime += new Ball.DelayTime(setDelayTime);

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
                            pnb.Show();
                            pnb.Left = screen.WorkingArea.Width - pnb.Width - 10;
                            pnb.Top = screen.WorkingArea.Height;

                            if (!pnb.SetRemindForm( index1, index2, DelayIndex))
                            {
                                pnb = null;
                                pnb.Dispose();                             
                                remindtime = IniRemTime;
                                return;
                            }

                          
                        while (pnb.Top > screen.WorkingArea.Height - pnb.Height)
                        {
                            Application.DoEvents();
                           pnb.Top--;
                        }
                        remindtime = IniRemTime;
                    }
                    else
                    {
                        remindtime--;
                        if (remindtime == IniRemTime - keeptime&&pnb!=null )//提醒窗体显示一定时间秒后消失
                        {
                            remindtime = IniRemTime;
                            pnb.Dispose();
                            pnb = null;
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
        #region 窗口拉伸
        const int Guying_HTLEFT = 10;
        const int Guying_HTRIGHT = 11;
        const int Guying_HTTOP = 12;
        const int Guying_HTTOPLEFT = 13;
        const int Guying_HTTOPRIGHT = 14;
        const int Guying_HTBOTTOM = 15;
        const int Guying_HTBOTTOMLEFT = 0x10;
        const int Guying_HTBOTTOMRIGHT = 17;

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
                            m.Result = (IntPtr)Guying_HTTOPLEFT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)Guying_HTBOTTOMLEFT;
                        else m.Result = (IntPtr)Guying_HTLEFT;
                    else if (vPoint.X >= ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)Guying_HTTOPRIGHT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)Guying_HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)Guying_HTRIGHT;
                    else if (vPoint.Y <= 5)
                        m.Result = (IntPtr)Guying_HTTOP;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)Guying_HTBOTTOM;
                    break;
                case 0x0201:                //鼠标左键按下的消息 
                    m.Msg = 0x00A1;         //更改消息为非客户区按下鼠标 
                    m.LParam = IntPtr.Zero; //默认值 
                    m.WParam = new IntPtr(2);//鼠标放在标题栏内 
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        #endregion

        private void btn1_Click(object sender, EventArgs e)
        {
            PivasNurseBall.PivasNurseBall ball = new PivasNurseBall.PivasNurseBall();
            ball.Btn1(Txt_ward.Tag.ToString(), Txt_ward.Text, EmployeeID, "", "", "");
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            PivasNurseBall.PivasNurseBall ball = new PivasNurseBall.PivasNurseBall();
            ball.Btn2(Txt_ward.Tag.ToString(), Txt_ward.Text, EmployeeID, "", "", "");
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            PivasNurseBall.PivasNurseBall ball = new PivasNurseBall.PivasNurseBall();
            ball.Btn3(Txt_ward.Tag.ToString(), Txt_ward.Text, EmployeeID, "", "", "");
        }


        //定时同步签收表
        private void SynTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan DD = DateTime.Now.Subtract(lastTime);
            if (DD.Minutes<30)
            {
                dbHelp.SetPIVAsDB("exec bl_synSignForm 0");
            }
        }

        #region 通讯  张衡2015-1-23  
        DataSet dsQQ = new DataSet(); //通讯记录
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NurseWorkStation));
        private int qq = 0;
        private void QQTimer_Tick(object sender, EventArgs e)
        {
            if (qq ==0)
            {

                this.notifyIcon1.Icon = new Icon("空白.ico");
                qq = 1;          
            }
            else
            {
                this.notifyIcon1.Icon = new Icon("文件名.ico");  
                qq = 0;
            }
           
        }

        private void QQtimer1_Tick(object sender, EventArgs e)
        {
            ShanShuo();
        }
        /// <summary>
        /// 是否闪烁
        /// </summary>
        private void ShanShuo()
        {
            dsQQ = dbHelp.GetPIVAsDB(sql.JudgeNewInfor(EmployeeID, Txt_ward.Tag.ToString()));

            if (dsQQ.Tables.Count>0 && dsQQ.Tables[0].Rows.Count > 0 &&!CheckFormIsOpen("QQMain"))
            {
                QQTimer.Enabled = true;
            }
            else
            {
                QQTimer.Enabled = false;
                this.notifyIcon1.Icon = new Icon("文件名.ico");  
            }
            if (dsQQ.Tables.Count > 0 && dsQQ.Tables[1].Rows.Count > 0)
            {
                NewInfor.Text ="最新公告："+ dsQQ.Tables[1].Rows[0]["Content"].ToString();
            }
        }

       
        private void toolStripMenuItem3_Click_1(object sender, EventArgs e)
        {
            QQTimer.Enabled = false;
            this.notifyIcon1.Icon = new Icon("文件名.ico");  
            if (CheckFormIsOpen("QQMain"))
            {
                return;
            }
            else
            {
                QQMain qm = new QQMain(EmployeeID, Txt_ward.Tag.ToString(), "PivasNurse");
                qm.TopMost = true;
                qm.ShowDialog();
            }
        }

      

        private void LaRoll_Tick(object sender, EventArgs e)
        {
            NewInfor.Left -= 1;//设置label1左边缘与其容器的工作区左边缘之间的距离
            
            if (NewInfor.Right <0)
            {
                NewInfor.Left = this.Size.Width-300;
            }

            //else if (NewInfor.Right < 0 && this.Size.Width > 900)
            //{
            //    NewInfor.Left =1100;//设置label1左边缘与其容器的工作区左边缘之间的距离为该窗体的宽度
            //}
        }
        private void NewInfor_Click(object sender, EventArgs e)
        {
            QQMain qm = new QQMain(EmployeeID, Txt_ward.Tag.ToString(), "PivasNurse");
            qm.TopMost = true;
            qm.ShowDialog();

        }
        #endregion

      

      








    }



}
