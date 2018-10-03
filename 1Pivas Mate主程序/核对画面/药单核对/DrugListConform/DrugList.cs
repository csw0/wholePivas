using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsDBhelp;

namespace DrugListConform
{
    public partial class DrugList : Form
    {
        DB_Help db = new DB_Help();
        SQL Mysql = new SQL();
        DataSet dsWard = new DataSet();
        DataSet dsPatient = new DataSet();
        private string userId = string.Empty;
        private string datetime = string.Empty;
        public DrugList(string userID,string date)
        {
            InitializeComponent();
            this.userId = userID;
            this.lbDate.Text =date; 
            
        }
        #region    移动窗体用到的变量
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
        private void DrugList_Load(object sender, EventArgs e)
        {


            if (Limit(userId, "DrugListConform"))
            {
                DataSet nm = new DataSet();
                string str1 = "select DEmployeeName from DEmployee where DEMployeeID = " + userId + "";
                nm = db.GetPIVAsDB(str1);
                label9.Text = nm.Tables[0].Rows[0][0].ToString();

                if (lbDate.Text == "")
                {
                    MessageBox.Show("请选择日期");
                    return;
                }
                else
                {
                    int l = getDwardSelect(lbDate.Text.ToString()).Tables[0].Rows.Count;

                    if (l <= 0)
                    {

                        dgvWard.DataSource = null;

                        MessageBox.Show("当日已没有不匹配瓶签药");
                        this.Dispose();
                    }
                    else
                    {
                        load();
                    }
                }
                
             
            }
            else
            {
                MessageBox.Show("您没有操作权限，请与管理员联系！");
                this.Dispose();
            }
        }

        /// <summary>
        /// 权限
        /// </summary>
        /// <param name="DEmployeeID"></param>
        /// <param name="LimitName"></param>
        /// <returns></returns>
        public bool Limit(string DEmployeeID, string LimitName)
        {
            try
            {
                DataSet ds = new DataSet();
                string str = "select * from ManageLimit where DEMployeeID = " + DEmployeeID + " and LimitName = '" + LimitName + "'";
                //MessageBox.Show(str);
                ds = db.GetPIVAsDB(str);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                ds.Dispose();
                return false;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 获取初始化数据
        /// </summary>
        /// <param name="lbdate"></param>
        /// <returns></returns>
        public DataSet getDwardSelect(string lbdate)
        {
            DataSet ds = new DataSet();          
            ds = db.GetPIVAsDB(Mysql.GetWard(lbdate));
            return ds;

        }

        /// <summary>
        /// 页面载入
        /// </summary>
        public void load()
        {
            int all = 0;
            dsWard = getDwardSelect(lbDate.Text.ToString());         
            if (dsWard != null)
            {
                for (int i = 0; i < dsWard.Tables[0].Rows.Count; i++)
                {
                    all = all + int.Parse(dsWard.Tables[0].Rows[i]["Count"].ToString());
                }
                    DataRow dr = dsWard.Tables[0].NewRow();
                    dr["check"] = "False";
                    dr["WardName"] = "<全部>";
                    dr["WardCode"] = "<全部>";
                    dr["Count"] = all;

                    dsWard.Tables[0].Rows.InsertAt(dr, 0);
               
                dgvWard.DataSource = dsWard.Tables[0];


                dgvWard.Rows[0].Selected = true;

               
            }

        }

        private void label3_MouseHover(object sender, EventArgs e)
        {
            label3.BackColor = Color.Red;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(210)))), ((int)(((byte)(251)))));
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 获取病区编码数据
        /// </summary>
        /// <returns></returns>
        private string getWardCode()
        {
            string WardCode = "";
            foreach (DataGridViewRow dr in dgvWard.Rows)
            {
                if (dr.Cells[1].Value.ToString()!="<全部>"&&dr.Cells[0].Value.ToString().Equals("True"))
                    WardCode = WardCode == "" ? dr.Cells["WardCode"].Value.ToString() : WardCode + "','" + dr.Cells["WardCode"].Value.ToString();
            }
            return WardCode;
        }

        /// <summary>
        /// 显示病人信息
        /// </summary>
        /// <param name="wardcode"></param>
        private void bdDgvInfo(string wardcode)
        {

            dsPatient = db.GetPIVAsDB(Mysql.GetPatient(wardcode, lbDate.Text));
             dgvPatient.DataSource = dsPatient.Tables[0];
           for (int i = 0; i < dgvPatient.Rows.Count; i++)
           {
               if (dgvPatient.Rows[i].Cells["Type"].Value.ToString() == "1")
               {
                   dgvPatient.Rows[i].DefaultCellStyle.BackColor = Color.Red;
               }
           }
        }

        /// <summary>
        /// 显示药单信息
        /// </summary>
        private void ShowdrugList()
        {
            panel2.Controls.Clear();
            CompareTime ct = new CompareTime(dgvPatient.CurrentRow.Cells["PatientCode"].Value.ToString());
            ct.Dock = DockStyle.Fill;
            panel2.Controls.Add(ct); 
        }

        private void dgvWard_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvWard.Rows.Count == 0)
                return;

            int i = dgvWard.CurrentCell.RowIndex;//获取选中行的行号


            if (dgvWard.Rows[i].Cells[0].EditedFormattedValue.ToString().Equals("True"))
                dgvWard.Rows[i].Cells[0].Value = true;
            else
                dgvWard.Rows[i].Cells[0].Value = false;


            //全选的情况
            if (dgvWard.Rows[i].Cells[1].Value.ToString() == "<全部>")
            {
                for (int j = 1; j < dgvWard.Rows.Count; j++)
                {
                    if (dgvWard.Rows[i].Cells[0].EditedFormattedValue.ToString().Equals("False"))
                    {
                        dgvWard.Rows[j].Cells[0].Value = false;
                    }
                    else
                    {
                        dgvWard.Rows[j].Cells[0].Value = true;
                    }
                }
            }
            bdDgvInfo(getWardCode());
        }

     

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void dgvPatient_Click(object sender, EventArgs e)
        {
            ShowdrugList();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "姓名")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "姓名";
                textBox1.ForeColor = Color.Silver;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (dsPatient == null)
            {
                return;
            }
            else
            {
                if ( textBox1.Text.Trim()!=""&&textBox1.Text!="姓名")
                {
                    DataTable dt = dsPatient.Tables[0].Copy();
                    dt.Rows.Clear();
                    DataRow[] DR = dsPatient.Tables[0].Select(" PatName like '%" + textBox1.Text.Trim() + "%'");
                    foreach (DataRow dr in DR)
                    {
                        dt.ImportRow(dr);
                    }
                  
                        dgvPatient.DataSource=dt;
                  
                }
                else
                {
                    if (dsPatient.Tables[0].Rows.Count > 0)
                    {
                        dgvPatient.DataSource = dsPatient.Tables[0];
                    }
                }
            }
           
        }

       


    }
}
