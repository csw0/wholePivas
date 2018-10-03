using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using PIVAsCommon.Helper;
using PivasLimitDES;

namespace ScanPre
{
    public partial class frmScanPre : Form
    {
        public frmScanPre()
        {
            InitializeComponent();
        }
        public frmScanPre(string EmployeeID)
        {
            InitializeComponent();
            this.EmployeeID = EmployeeID;
        }

        mySQL sql = new mySQL();
       DB_Help DB = new DB_Help();
       DataTable  DSWard = new DataTable ();
       DataTable  DSPre =new DataTable ();   
       private int a;
       public int P = 2;
       private string EmployeeID = string.Empty;
        /// <summary>
        /// 存放病区编号
        /// </summary>
        string code = "";
        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion

        #region 拖动改变窗体大小
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
        #endregion

        private void frmScanPre_Load(object sender, EventArgs e)
        {
            if (GetPivasLimit.Instance.Limit(EmployeeID, "ScanPre"))
            {            
                cbbStatus.SelectedIndex = 1;
                comboBox2.SelectedIndex = 0;
                cb1.Checked = true;
                cb2.Checked = true;
                cb3.Checked = true;

                cbP.Checked = true;
                cbK.Checked = true;
                cbH.Checked = true;
                cbY.Checked = true;
                cbZ.Checked = true;
                newdgvWard(P);
                TraversalDgvWard();
                newdgvPre();
              


                try
                {
                    dtp1.Text = DateTime.Now.AddDays(int.Parse(DB.GetPivasAllSet("医嘱查询-默认开始时间/结束时间-画面显示"))).ToString();
                    dtp2.Text = DateTime.Now.AddDays(int.Parse(DB.GetPivasAllSetValue2("医嘱查询-默认开始时间/结束时间-画面显示"))).ToString();
                }
                catch
                {
                    MessageBox.Show("  综合设置中:<医嘱查询-默认开始时间/结束时间-画面显示> 设置项请设置数字。  ");
                }

                //画面初始大小设置
                if (DB.GetPivasAllSet("医嘱查询-画面初始最大化") == "1")
                {
                    MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.WindowState = FormWindowState.Maximized;
                    this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                    Panel_Max_None.BackgroundImage = global::ScanPre.Properties.Resources.还原;
                }

                //普抗化营筛选显示设置
                if (DB.GetPivasAllSet("医嘱查询-普抗化营筛选-画面显示") == "0")
                {
                    cbP.Visible = false;
                    cbK.Visible = false;
                    cbH.Visible = false;
                    cbY.Visible = false;
                    cbZ.Visible = false;
                }


                //长期临时筛选显示设置
                if (DB.GetPivasAllSet("医嘱查询-长期临时筛选-画面显示") == "0")
                {
                    cb1.Visible = false;
                    cb2.Visible = false;
                }

                //今日生有生成瓶签 筛选显示设置
                string Xianshi = DB.GetPivasAllSet("医嘱查询-今日有瓶签勾选-画面显示");
                string GouXuan = DB.GetPivasAllSetValue2("医嘱查询-今日有瓶签勾选-画面显示");
                if (Xianshi == "0")
                {
                    checkBox1.Visible = false;
                    checkBox1.Checked = false;
                }
                else if (Xianshi == "1" && GouXuan == "1")
                {
                    checkBox1.Visible = true;
                    checkBox1.Checked = true;
                }
                else if (Xianshi == "1" && GouXuan == "0")
                {
                    checkBox1.Visible = true;
                    checkBox1.Checked = false;
                }
                else
                {
                    checkBox1.Visible = false;
                    checkBox1.Checked = false;
                    MessageBox.Show("  综合设置中:<医嘱查询-今日有瓶签勾选-画面显示> 设置项 设置不正确。 ");
                }


                //模糊查询框 显示设置
                if (DB.GetPivasAllSet("医嘱查询-模糊查询框-画面显示") == "0")
                {
                    txtName.Visible = false;
                }

                //病区默认勾选
                if (DB.GetPivasAllSet("医嘱查询-画面初始病区勾选") == "0")
                {
                    P = 0;
                    newdgvWard(P);
                    TraversalDgvWard();
                    newdgvPre();
                }
                else if (DB.GetPivasAllSet("医嘱查询-画面初始病区勾选") == "2")
                {
                    P = 2;
                    newdgvWard(P);
                    TraversalDgvWard();
                    newdgvPre();
                }
                else if (DB.GetPivasAllSet("医嘱查询-画面初始病区勾选") == "1")
                {
                    P = 1;
                    newdgvWard(P);
                    TraversalDgvWard();
                    newdgvPre();
                }
                else
                {
                    newdgvWard(P);
                    TraversalDgvWard();
                    newdgvPre();
                    MessageBox.Show("  综合设置中:<医嘱查询-画面初始病区勾选> 设置项 设置不正确。 ");
                }
            }

            else
            {
                this.Dispose();
            }

        }

        /// <summary>
        /// 比较日期大小
        /// </summary>
        private bool compareDate()
        {
            DateTime t1, t2;
            t1 = Convert.ToDateTime(dtp1.Text);
            t2 = Convert.ToDateTime(dtp2.Text);
            if (t1 > t2)
            {
                return false;
              
            }
            return true;
            
        }
        /// <summary>
        /// 遍历病区表
        /// </summary>
        private void TraversalDgvWard()
        {
           
            code = "";
            for (int i = 0; i < dgvWard.Rows.Count; i++)
            {
                if (dgvWard.Rows[i].Cells[0].Value != null && dgvWard.Rows[i].Cells[0].Value.ToString() == "True")
                {
                    code += "'" + dgvWard.Rows[i].Cells[6].Value.ToString() + "',";
                    a = a + 1;
                }                
            }
            if (a == dgvWard.Rows.Count)
            {
                cb3.CheckState = CheckState.Checked;
            }
            else if (a == 0)
            {
                cb3.CheckState = CheckState.Unchecked;
            }
            else
            {
                cb3.CheckState = CheckState.Indeterminate;
            }
              
            code = code.TrimEnd(',');
            a = 0;
        }
        private void dgvWard_Click(object sender, EventArgs e)
        {

            try
            {
                if (dgvWard.Rows.Count > 0)
                {
                    cb1.Checked = true;
                    cb2.Checked = true;
                    //compareDate();
                    if (dgvWard.CurrentCell.ColumnIndex == 0)
                    {
                        if (dgvWard.CurrentRow.Cells[0].Value == null || dgvWard.CurrentRow.Cells[0].Value.ToString() == "False" || dgvWard.CurrentRow.Cells[0].Value.ToString() == "0")
                        {
                            dgvWard.CurrentRow.Cells[0].Value = true;
                        }
                        else
                        {
                            dgvWard.CurrentRow.Cells[0].Value = false;
                        }
                        TraversalDgvWard();

                        if (code.Trim() == "")
                            DSPre.Clear();
                        else
                        {
                            newdgvPre();
                        }
                    }

                    else
                    {
                        code = "'" + dgvWard.CurrentRow.Cells[6].Value.ToString() + "'";
                        newdgvPre();
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 插入病区组
        /// </summary>
        private void cmbadd()
        {
            comboBox1.Items.Clear();
            List<string> lsName = new List<string>();
            DataTable dt = new DataTable();
            dt = DSWard.Copy();        
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                string name = dt.Rows[i][0].ToString();
                if (lsName.Contains(name) || name=="")
                {
                    continue;
                }
                else
                {
                    lsName.Add(name);
                }
            }
            //lsName.Sort();
            comboBox1.Items.Add("全部病区组");
            for (int i = 0; i < lsName.Count; i++)
            {
                comboBox1.Items.Add(lsName[i]);
            }
            comboBox1.SelectedIndex = 0;
           
        }

        private void tbWard_Enter(object sender, EventArgs e)
        {
            if (tbWard.Text == "病区名/简拼")
            {
                tbWard.Text = "";
                tbWard.ForeColor = Color.Black;
            }
        }

        private void txtWard_TextChanged(object sender, EventArgs e)
        {
            cb3.Checked = false;
            if (DSWard.Rows.Count <= 0 || tbWard.Text == "病区名/简拼")
                return;
            if (comboBox1.Text == "全部病区组")
            {
                if (tbWard.Text == "")
                {
                    dgvWard.DataSource = DSWard;
                    return;
                }

                DataTable dt = DSWard.Copy();
                dt.Rows.Clear();
                DataRow[] DR = DSWard.Select(" WardSimName like '%" + tbWard.Text.Trim() + "%' or Spellcode like '%" + tbWard.Text.Trim() + "%' ", "WardSimName ASC");
                foreach (DataRow dr in DR)
                {
                    dt.ImportRow(dr);
                }
                dgvWard.DataSource = dt;
            }
            else
            {
                DataTable dt = DSWard.Copy();
                dt.Rows.Clear();
                DataRow[] DR = DSWard.Select("WardArea ='" + comboBox1.SelectedItem.ToString() + "' and ( WardSimName like '%" + tbWard.Text.Trim() + "%' or Spellcode like '%" + tbWard.Text.Trim() + "%')", "WardSimName ASC");
                foreach (DataRow dr in DR)
                {
                    dt.ImportRow(dr);
                }
                dgvWard.DataSource = dt;

            }
        }
        private void dgvPre_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPre.Rows.Count > 0)
                {
                    string pre = dgvPre.SelectedRows[0].Cells["PreID"].Value.ToString();
                    information1.SetInformation(pre);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Panel_Close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Panel_Close_MouseHover(object sender, EventArgs e)
        {
            Panel_Close.BackgroundImage = global::ScanPre.Properties.Resources._15;
        }

        private void Panel_Close_MouseLeave(object sender, EventArgs e)
        {
            Panel_Close.BackgroundImage = null;
            Panel_Close.BackColor = Color.Transparent;
            Panel_Close.BackgroundImage = global::ScanPre.Properties.Resources._21;
        }

        private void Panel_Max_None_Click(object sender, EventArgs e)
        {
            MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
               
            }
        }

        private void Panel_Max_None_MouseHover(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Normal)
            {
                Panel_Max_None.BackColor = Color.FromArgb(128, 128, 255);
                //Panel_Max_None.BackgroundImage = global::ScanPre.Properties.Resources._16;
            }
            else
            {
                Panel_Max_None.BackColor = Color.FromArgb(128, 128, 255);
                //Panel_Max_None.BackgroundImage = global::ScanPre.Properties.Resources.还原按下时按钮;
            }
        }

        private void Panel_Max_None_MouseLeave(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Normal)
            {
                Panel_Max_None.BackgroundImage = global::ScanPre.Properties.Resources._20;
            }
            else
            {
                Panel_Max_None.BackgroundImage = global::ScanPre.Properties.Resources.还原;
            }
            Panel_Max_None.BackColor = Color.Transparent;
        }

        private void Pic_Min_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        private void Pic_Min_MouseHover(object sender, EventArgs e)
        {
            Pic_Min.BackColor = Color.FromArgb(128, 128, 255);
            //Pic_Min.BackgroundImage = global::ScanPre.Properties.Resources.最小化按下时按钮;
        }

        private void Pic_Min_MouseLeave(object sender, EventArgs e)
        {
            Pic_Min.BackgroundImage = null;
            Pic_Min.BackColor = Color.Transparent;
            Pic_Min.BackgroundImage = global::ScanPre.Properties.Resources.最小化;
        }

        private void label5_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        /// <summary>
        /// 取颜色
        /// </summary>
        private void changeColor()
        {
            string sql = "  select top 1[Level3Color],[Level5Color],[RightColor],[SelectedColor] from RevPreFormSet order by DEmployeeID ";
            DataTable dt = DB.GetPIVAsDB(sql).Tables[0];
            string[] numbers = dt.Rows[0][2].ToString().Trim().Split(',');
            string[] numbers1 = dt.Rows[0][0].ToString().Trim().Split(',');
            string[] numbers2 = dt.Rows[0][1].ToString().Trim().Split(',');
            string[] numbers3 = dt.Rows[0][3].ToString().Trim().Split(',');
            if (dt.Rows.Count == 0)
            {
                for (int i = 0; i < dgvPre.Rows.Count; i++)
                {
                    switch (dgvPre.Rows[i].Cells["Level"].Value.ToString().Trim())
                    {
                        case "0":
                            dgvPre.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(157, 245, 159);
                            dgvPre.Rows[i].DefaultCellStyle.SelectionBackColor = Color.FromArgb(205, 250, 205);
                            break;
                        case "3":
                            dgvPre.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(249, 253, 156);
                            dgvPre.Rows[i].DefaultCellStyle.SelectionBackColor = Color.FromArgb(252, 254, 182);
                            break;
                        case "5":
                            dgvPre.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(253, 212, 218);
                            dgvPre.Rows[i].DefaultCellStyle.SelectionBackColor = Color.FromArgb(254, 231, 235);
                            break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < dgvPre.Rows.Count; i++)
                {
                    switch (dgvPre.Rows[i].Cells["Level"].Value.ToString().Trim())
                    {
                        case "0":
                            dgvPre.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(Convert.ToInt32(numbers[0]), Convert.ToInt32(numbers[1]), Convert.ToInt32(numbers[2]));
                            dgvPre.Rows[i].DefaultCellStyle.SelectionBackColor = Color.FromArgb(Convert.ToInt32(numbers3[0]), Convert.ToInt32(numbers3[1]), Convert.ToInt32(numbers3[2]));
                            break;
                        case "3":
                            dgvPre.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(Convert.ToInt32(numbers1[0]), Convert.ToInt32(numbers1[1]), Convert.ToInt32(numbers1[2]));
                            dgvPre.Rows[i].DefaultCellStyle.SelectionBackColor = Color.FromArgb(Convert.ToInt32(numbers3[0]), Convert.ToInt32(numbers3[1]), Convert.ToInt32(numbers3[2]));
                            break;
                        case "5":
                            dgvPre.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(Convert.ToInt32(numbers2[0]), Convert.ToInt32(numbers2[1]), Convert.ToInt32(numbers2[2]));
                            dgvPre.Rows[i].DefaultCellStyle.SelectionBackColor = Color.FromArgb(Convert.ToInt32(numbers3[0]), Convert.ToInt32(numbers3[1]), Convert.ToInt32(numbers3[2]));
                            break;

                    }
                }
            }
          

        }
      
        /// <summary>
        /// 刷新dgvWard表
        /// </summary>
        private void newdgvWard(int p)
        {
            StringBuilder str = new StringBuilder();
            //compareDate();
            str.Append(" select distinct DWard.WardArea,DWard.Spellcode,DWard.WardSimName,COUNT(distinct p.PrescriptionID)as 'TotalCount' ");
            str.Append(", p.WardCode from Prescription p  ");
            str.Append("  inner join PrescriptionDetail pd on p.PrescriptionID=pd.PrescriptionID  ");
            str.Append(" inner join DWard on DWard.WardCode=p.WardCode ");
            if (checkBox1.Checked)
            {
                str.Append("  inner join IVRecord  iv on p.PrescriptionID=iv.PrescriptionID  ");
            }
            str.Append(" where  DATEDIFF(dd,pd.StartDT, ' ");
            str.Append(dtp1.Text);
            str.Append("')<=0 and DATEDIFF(dd,pd.StartDT,'  ");
            str.Append(dtp2.Text);
            str.Append("  ')>=0  ");
            if (checkBox1.Checked)
            {
                str.Append("and DATEDIFF(DD,iv.InfusionDT,'" + DateTime.Now.Date.ToString() + "')=0 ");
            }
            str.Append("and DrugType in (0");
            if (cbP.Checked)
            {
                str.Append(",1");
            }
            if (cbK.Checked)
            {
                str.Append(",2");
            }
            if (cbH.Checked)
            {
                str.Append(",3");
            }
            if (cbY.Checked)
            {
                str.Append(",4");
            }
            if (cbZ.Checked)
            {
                str.Append(",5");
            }
            str.Append(")");

            if (cb1.Checked && cb2.Checked)
            {

            }
            else if (cb1.Checked)
            {
                str.Append(" and (Ltrim(Rtrim(P.Remark5))='' or p.Remark5 is null or p.Remark5=''or p.Remark5='长期')  ");
            }
            else if (cb2.Checked)
            {
                str.Append(" and p.Remark5='ST'  ");
            }
            else if (!cb1.Checked && !cb2.Checked)
            {
                MessageBox.Show("未选中长期临时");

            }

            if (cbbStatus.SelectedIndex == 0)
            {
            }
            else if (cbbStatus.SelectedIndex == 1)
            {
                str.Append(" and p.PPause=0 and p.PStatus!=4 and p.PStatus!=3 ");
            }
            else if (cbbStatus.SelectedIndex == 2)
            {
                str.Append(" and p.PPause=1  ");
            }
            else if (cbbStatus.SelectedIndex == 3)
            {
                str.Append(" and p.PStatus=4  ");
            }
            else if (cbbStatus.SelectedIndex == 4)
            {
                str.Append(" and p.PStatusDoctor=3  ");
            }

            if (comboBox2.SelectedIndex == 0)
            {
            }
            else if(comboBox2.SelectedIndex==1)
            {
                str.Append(" and p.Level=0 ");
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                str.Append(" and p.Level=3 ");
            }
            else
            {
                str.Append(" and p.Level=5 ");
            }

            str.Append(" group by p.WardCode,DWard.WardArea,DWard.Spellcode,DWard.WardSimName ");  
            dgvWard.DefaultCellStyle.BackColor = Color.White;
            DSWard = DB.GetPIVAsDB(str.ToString()).Tables[0];
            dgvWard.DataSource = DSWard;
            dgvWard.Columns["WardArea"].Visible = true ;
            dgvWard.Columns["Spellcode"].Visible = false;
            dgvWard.Columns["WardCode"].Visible = false;
            cmbadd();
            if (DSWard.Rows.Count > 0)
            {
                if (p == 2)
                {
                    for (int i = 0; i < dgvWard.Rows.Count; i++)
                    {
                        dgvWard.Rows[i].Cells[0].Value = true;
                    }
                }
                else if (p == 1)
                {
                    dgvWard.Rows[0].Cells[0].Value = true;
                    for (int i = 1; i < dgvWard.Rows.Count; i++)
                    {
                        dgvWard.Rows[i].Cells[0].Value = false;
                    }
                }
                else if (p == 0)
                {
                    for (int i = 0; i < dgvWard.Rows.Count; i++)
                    {
                        dgvWard.Rows[i].Cells[0].Value = false;
                    }
                }
            }
        }

        /// <summary>
        /// 刷新dgvPre表
        /// </summary>
        private void newdgvPre()
        {
            string serch = "";
            //compareDate();
             StringBuilder str= new StringBuilder();         
            if (txtName.Text.Trim() != "姓名/病人编码/住院号/组号/床号/病区/药品")
            {
                serch = txtName.Text;
            }
            
            if (string.IsNullOrEmpty(code))
            {
                DSPre.Clear();
                information1.clear();
            }
            else
            {
                
                str.Append("select distinct case PStatus when 4 then '已停止' when 3 then '已退方' else case PPause when 1 then '已暂停' else '执行中' end end as status ");
                str.Append(",p.GroupNo,d.WardName ,p.BedNo, p.FregCode ");
                str.Append("  ,case p.DrugType when 1 then '普' when 2 then '抗'  when 3 then '化'  when 4 then '营' when 5 then '中' end as  DrugType  ");
                str.Append("  ,p.InceptDT,MAX( pd.EndDT) as EndDT,MIN( pd.StartDT) as StartDT,p.Level,pa.PatName  ,p.PrescriptionID,p.CaseID,p.PatientCode,p.PStatusDoctor ,p.PStatus   ");
                str.Append("  from Prescription p   ");
                str.Append("  inner join PrescriptionDetail pd on p.PrescriptionID=pd.PrescriptionID  ");
                str.Append("  inner join DWard d on d.WardCode=p.WardCode ");
                str.Append("  inner join Patient pa on p.PatientCode=pa.PatCode  ");
                if (checkBox1.Checked)
                {
                    str.Append("  inner join IVRecord  iv on p.PrescriptionID=iv.PrescriptionID  ");
                }
                str.Append("where p.WardCode in (  ");
                str.Append(code);
                str.Append(") ");
                str.Append("and (pa.PatName like '%");
                str.Append(serch);
                str.Append("%' or p.CaseID like '%");
                str.Append(serch);
                str.Append("%' or d.WardName like '%");
                str.Append(serch);
                str.Append("%' or p.PatientCode like '%");
                str.Append(serch);
                str.Append("%' or p.GroupNo like '%");
                str.Append(serch);
                str.Append("%' or p.BedNo like '%");
                str.Append(serch);
                str.Append("%'or pd.DrugName like '%");
                str.Append(serch);
                str.Append("%')");
                str.Append("and DATEDIFF(dd,pd.StartDT, '");
                str.Append(dtp1.Text);
                str.Append("')<=0 and DATEDIFF(dd,pd.StartDT, '");
                str.Append(dtp2.Text);
                str.Append("')>=0 ");

                if (checkBox1.Checked)
                {
                    str.Append("and DATEDIFF(DD,iv.InfusionDT,'" + DateTime.Now.Date.ToString() + "')=0");
                }
                
                str.Append("and DrugType in (0");
                if (cbP.Checked)
                {
                    str.Append(",1");
                }
                if (cbK.Checked)
                {
                    str.Append(",2");
                }
                if (cbH.Checked)
                {
                    str.Append(",3");
                }
                if (cbY.Checked)
                {
                    str.Append(",4");
                }
                if (cbZ.Checked)
                {
                    str.Append(",5");
                }
                str.Append(")");

                if (cb1.Checked && cb2.Checked)
                {
                 
                }
                else if (cb1.Checked)
                {                
                    str.Append(" and (Ltrim(Rtrim(P.Remark5))='' or p.Remark5 is null or p.Remark5=''or p.Remark5='长期')  ");
                }
                else if (cb2.Checked)
                {               
                    str.Append(" and p.Remark5='ST'  ");
                }
                else if (!cb1.Checked && !cb2.Checked)
                {
                    MessageBox.Show("未选中长期临时");
                
                }
                str.Append(" group by p.PrescriptionID, p.CaseID,p.PatientCode,p.PPause,PStatus,p.InceptDT,p.BedNo,p.GroupNo,d.WardName,pa.PatName,p.Level,p.DrugType,p.FregCode ,p.PStatusDoctor  ");
              
              DataTable dt = DB.GetPIVAsDB(str.ToString()).Tables[0];           
                         
               
                DataTable dt1 =SelectLevel (dt);
                DataRow[] dt2 = dt1.Select(" PStatus<=1 ");
                label4.Text = dt2.Length.ToString(); 

                DSPre = SelectPstaus(dt1);
                DataTable dt3 = new DataTable();
                System.Data.DataColumn column = new DataColumn();
                column.ColumnName = "add";
                column.AutoIncrement = true;
                column.AutoIncrementSeed = 1;
                column.AutoIncrementStep = 1;
                dt3.Columns.Add(column);
                dt3.Merge(DSPre);
                DSPre=dt3;
                dgvPre.DataSource = DSPre;
                changeColor();
                if (DSPre.Rows.Count > 0)
                {
                    string pre = dgvPre.SelectedRows[0].Cells["PreID"].Value.ToString();
                    information1.SetInformation(pre);
                }
                else
                {
                    information1.clear();
                }
               
            }       
        }
        /// <summary>
        /// 根据医嘱级别筛选医嘱
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable SelectLevel(DataTable dt)
        {
            DataTable dt2 =dt.Clone();
            dt2.Rows.Clear();
            DataTable dt3 = new DataTable();
            if (comboBox2.SelectedIndex == 0)
            {
                dt3.Merge(dt);
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                foreach (DataRow dr in dt.Select("Level='0'"))
                {
                    dt2.Rows.Add(dr.ItemArray);
                }
                dt3.Merge(dt2);
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                foreach (DataRow dr in dt.Select("Level='3'"))
                {
                    dt2.Rows.Add(dr.ItemArray);
                }
                dt3.Merge(dt2);
            }
            else
            {
                foreach (DataRow dr in dt.Select("Level='5'"))
                {
                    dt2.Rows.Add(dr.ItemArray);
                }
                dt3.Merge(dt2);
            }
         
          
            return dt3;
        }
        /// <summary>
        /// 筛选医嘱的执行状态
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable SelectPstaus(DataTable dt)
        {
            DataTable dt1 = new DataTable();
            dt1 = dt.Clone();
            dt1.Rows.Clear();
            switch (cbbStatus.SelectedIndex)
            {
                case 0:
                    foreach (DataRow dr in dt.Select(""))
                    {
                        dt1.Rows.Add(dr.ItemArray);
                    }
                    break;
                case 4:
                    foreach (DataRow dr in dt.Select("PStatusDoctor='3'"))
                    {
                        dt1.Rows.Add(dr.ItemArray);
                    }
                    break;
                default:
                    foreach (DataRow dr in dt.Select("status ='" + cbbStatus.SelectedItem.ToString() + "'"))
                    {
                        dt1.Rows.Add(dr.ItemArray);
                    }
                    break;
            }
          
            return dt1;
        
        }
        /// <summary>
        /// 改变处方执行状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbStatus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            newdgvWard(P);
            TraversalDgvWard();
            newdgvPre();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label7.Text = DateTime.Now.ToString();
        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==(char)Keys.Enter)
            {
                newdgvWard(P);
                TraversalDgvWard();              
                newdgvPre();                            
            }
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            if (txtName.Text.Trim()=="")
            {
                txtName.Text = "姓名/病人编码/住院号/组号/床号/病区/药品";
                txtName.ForeColor = Color.Silver;
            }
        }

        private void txtName_Enter(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "姓名/病人编码/住院号/组号/床号/病区/药品")
            {
                txtName.Text = "";
                txtName.ForeColor = Color.Black;
            }
        }
        /// <summary>
        /// 病区组选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cb3.Checked = false;
            DataRow[] DR;
            DataTable dt = DSWard.Copy();
            dt.Rows.Clear();
            if (comboBox1.SelectedItem.ToString() == "全部病区组")
            {
                DR = DSWard.Select("", "WardSimName ASC");
            }
            else
            {
                DR = DSWard.Select(" WardArea='" + comboBox1.SelectedItem.ToString() + "'", "WardSimName ASC");
            }
            foreach (DataRow dr in DR)
            {
                dt.ImportRow(dr);
            }
            dgvWard.DataSource = dt;


        }
        /// <summary>
        /// 长期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb1_Click(object sender, EventArgs e)
        {
            newdgvWard(P);       
                TraversalDgvWard();
                newdgvPre();      
        }
        /// <summary>
        /// 临时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb2_Click(object sender, EventArgs e)
        {
               newdgvWard(P);   
                TraversalDgvWard();
                newdgvPre();      
        }
       
       
        /// <summary>
        /// 搜病区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbWard_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (DSWard.Rows.Count <= 0 || tbWard.Text == "病区名/简拼")
                    return;
                if (comboBox1.Text == "全部病区组")
                {
                    if (tbWard.Text == "")
                    {
                        dgvWard.DataSource = DSWard;
                        return;
                    }

                    DataTable dt = DSWard.Copy();
                    dt.Rows.Clear();
                    DataRow[] DR = DSWard.Select("WardSimName like '%" + tbWard.Text.Trim() + "%' or Spellcode like '%" + tbWard.Text.Trim() + "%' ", "WardSimName ASC");
                    foreach (DataRow dr in DR)
                    {
                        dt.ImportRow(dr);
                    }
                    dgvWard.DataSource = dt;
                }
                else
                {
                    DataTable dt = DSWard.Copy();
                    dt.Rows.Clear();
                    DataRow[] DR = DSWard.Select("WardArea ='" + comboBox1.SelectedItem.ToString() + "' and ( WardSimName like '%" + tbWard.Text.Trim() + "%' or Spellcode like '%" + tbWard.Text.Trim() + "%')", "WardSimName ASC");
                    foreach (DataRow dr in DR)
                    {
                        dt.ImportRow(dr);
                    }
                    dgvWard.DataSource = dt;

                }
            }

        }

        private void tbWard_Leave(object sender, EventArgs e)
        {
            if (tbWard.Text == "")
            {
                tbWard.Text = "病区名/简拼";
              tbWard.ForeColor = Color.Silver;
            }

        }

        private void dgvPre_Sorted(object sender, EventArgs e)
        {
            changeColor();
        }
        /// <summary>
        /// 选中全部病区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb3_Click(object sender, EventArgs e)
        {
            StringBuilder str=new StringBuilder();
            //compareDate();
            if (cb3.CheckState==CheckState.Checked)
            {
                if (dgvWard.Rows.Count <= 0)
                {
                    for (int i = 0; i < dgvWard.Rows.Count; i++)
                    {
                        dgvWard.Rows[i].Cells[0].Value = true;
                    }
                    str.Append("select distinct  '执行中' as status,  ");
                    str.Append("p.PrescriptionID,p.CaseID,p.PatientCode,p.InceptDT,pd.EndDT,pd.StartDT,d.WardName   ");
                    str.Append(" ,p.BedNo,p.Level,p.GroupNo,pa.PatName from Prescription p  ");
                    str.Append(" inner join PrescriptionDetail pd on p.PrescriptionID=pd.PrescriptionID ");
                    str.Append("inner join DWard d on d.WardCode=p.WardCode ");
                    str.Append("inner join Patient pa on p.PatientCode=pa.PatCode  ");
                    str.Append("where PStatus <3 and PPause != 1 and DATEDIFF(dd,pd.StartDT, ' ");
                    str.Append(dtp1.Text);
                    str.Append("')<=0 and DATEDIFF(dd,pd.StartDT, '");
                    str.Append(dtp2.Text);
                    str.Append(" ')>=0 ");
                    str.Append("  ");
                    str.Append("  ");
                    DSPre = DB.GetPIVAsDB(str.ToString()).Tables[0];
                    if (DSPre.Rows.Count > 0)
                    {
                        DataTable dt3 = new DataTable();
                        System.Data.DataColumn column = new DataColumn();
                        column.ColumnName = "add";
                        column.AutoIncrement = true;
                        column.AutoIncrementSeed = 1;
                        column.AutoIncrementStep = 1;
                        dt3.Columns.Add(column);
                        dt3.Merge(DSPre);
                        DSPre = dt3;
                        dgvPre.DataSource = DSPre;
                        changeColor();
                        string pre = dgvPre.SelectedRows[0].Cells["PreID"].Value.ToString();
                        information1.SetInformation(pre);
                    }
                }
                else
                {
                    for (int i = 0; i < dgvWard.Rows.Count; i++)
                    {
                        dgvWard.Rows[i].Cells[0].Value = true;
                    }
                    TraversalDgvWard();
                    newdgvPre();
                }

            }
            else
            {
                cb3.CheckState =CheckState.Unchecked;
                for (int i = 0; i < dgvWard.Rows.Count; i++)
                {
                    dgvWard.Rows[i].Cells[0].Value = 0;
                }
                TraversalDgvWard();
                newdgvPre();
                information1.clear();
            }

        }
        /// <summary>
        /// 普
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbP_Click(object sender, EventArgs e)
        {
            newdgvWard(P);
            TraversalDgvWard();
            newdgvPre();
        }
        /// <summary>
        /// 抗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbK_Click(object sender, EventArgs e)
        {
            newdgvWard(P);
            TraversalDgvWard();
            newdgvPre();
        }
        /// <summary>
        /// 化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbH_Click(object sender, EventArgs e)
        {
            newdgvWard(P);
            TraversalDgvWard();
            newdgvPre();
        }
        /// <summary>
        /// 营
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbY_Click(object sender, EventArgs e)
        {
            newdgvWard(P);
            TraversalDgvWard();
            newdgvPre();
        }
        /// <summary>
        /// 查看医嘱产生的瓶签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvPre_DoubleClick(object sender, EventArgs e)
        {
            if (dgvPre.RowCount > 0)
            {
                labelNo ln = new labelNo(dgvPre.CurrentRow.Cells["Column8"].Value.ToString());
                ln.LabelNo = dgvPre.CurrentRow.Cells["Column8"].Value.ToString();
                ln.ShowDialog();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            newdgvWard(P);
            TraversalDgvWard();
            newdgvPre();       
        }
        /// <summary>
        /// 日期改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtp1_CloseUp(object sender, EventArgs e)
        {

            cb1.Checked = true;
            cb2.Checked = true;
            cb3.Checked = true;
            if (compareDate())
            {
                newdgvWard(P);
                TraversalDgvWard();
                newdgvPre();
            }
            else
            {
                MessageBox.Show("开始时间大于结束时间");
                dtp1.Value = dtp2.Value;

                newdgvWard(P);
                TraversalDgvWard();
                newdgvPre();

            }
           
        }
        /// <summary>
        /// 日期改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtp2_CloseUp(object sender, EventArgs e)
        {
            cb1.Checked = true;
            cb2.Checked = true;
            cb3.Checked = true;
            if (compareDate())
            {
                newdgvWard(P);
                TraversalDgvWard();
                newdgvPre();
            }
            else
            {
                MessageBox.Show("开始时间大于结束时间");
                dtp2.Value = dtp1.Value;

                newdgvWard(P);
                TraversalDgvWard();
                newdgvPre();
            }

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
                    for (int i = 0; i < dgv.ColumnCount; i++)
                    {
                        if (i > 0)
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
                        for (int k = 0; k < dgv.Columns.Count; k++)
                        {
                            if (k > 0)
                            {
                                columnValue += "\t";
                            }
                            if (dgv.Rows[j].Cells[k].Value == null)
                                columnValue += "";
                            else
                                columnValue += " " + dgv.Rows[j].Cells[k].Value.ToString();
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

        private void button1_Click(object sender, EventArgs e)
        {
            DataToExcel(this.dgvPre);
        }

        /// <summary>
        /// 系统审方的区分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            newdgvWard(P);
            TraversalDgvWard();
            newdgvPre();
        }

        private void cbZ_Click(object sender, EventArgs e)
        {
            newdgvWard(P);
            TraversalDgvWard();
            newdgvPre();
        }


      
   
    }
}
