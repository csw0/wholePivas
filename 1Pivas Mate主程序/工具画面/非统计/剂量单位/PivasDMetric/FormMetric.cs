using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;
using PivasLimitDES;

namespace DMetricManage
{
    public partial class FormMetric : Form
    {         
        private DB_Help db = new DB_Help();
        private DataSet ds = new DataSet();
        private string UserID = string.Empty;

        public FormMetric()
        {
            InitializeComponent();
            
        }
        public FormMetric(string ID)
        {
            InitializeComponent();
            UserID = ID;
        }
        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion

        public void FormMetric_Load(object sender, EventArgs e)
        {
            label3.Parent = this.pictureBox1;
            label1.Parent =  this.pictureBox1;
            lblAdd.Parent = this.pictureBox1;
            label2.Parent = this.pictureBox2;
            Pic_Min.Parent = this.pictureBox1;
            Panel_Close.Parent = this.pictureBox1;
         
            try
            {
                if (UserID == null || UserID == "")
                {
                    UserID = "00000000000000000000000000000000000000000000000";
                }

                if (GetPivasLimit.Instance.Limit(UserID, "PivasDMetric"))
                {
                    DataSet nm = new DataSet();
                    label3.Text = "";
                    string str1 = "select DEmployeeName from DEmployee where DEMployeeID = " + UserID + "";
                    nm = db.GetPIVAsDB(str1);
                    label3.Text = nm.Tables[0].Rows[0][0].ToString();
                    
                    StringBuilder str = new StringBuilder();
                    str.Append("select D.MetricCode,D.MetricName,DU.ChineseName,DU.EnglishName,D.UnitID from DMetric D ");
                    str.Append("left join kd0100..DMetrologyUnit DU on D.UnitID=DU.UnitID order by MetricCode");
                    ds = db.GetPIVAsDB(str.ToString());
                    showdata(ds);
                    for (int i = 0; i < flpMetric.Controls.Count; i++)
                    {
                        rowMetric ro = (rowMetric)flpMetric.Controls[i];
                        ro.show();
                    }
                }
                else
                {
                    MessageBox.Show("您没有操作权限，请与管理员联系！");
                    this.Dispose();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }
        /// <summary>
        /// 权限
        /// </summary>
        /// <param name="DEmployeeID"></param>
        /// <param name="LimitName"></param>
        /// <returns></returns>
        //public bool Limit(string DEmployeeID, string LimitName)
        //{
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        string str = "select * from ManageLimit where DEMployeeID = " + DEmployeeID + " and LimitName = '" + LimitName + "'";
        //        ds = db.GetPIVAsDB(str);
        //        if (ds != null && ds.Tables[0].Rows.Count > 0)
        //        {
        //            return true;
        //        }
        //        ds.Dispose();
        //        return false;

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return false;
        //    }
        //}
        private void pnlExit_click(object sender, EventArgs e)//退出
        {
            this.Close();
        }

        private void showdata(DataSet Metric)     //加载用户控件
        {    
            try
            {
                DataTable dt = Metric.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rowMetric row = new rowMetric();
                    row.Name = "rowM" + i.ToString();
                    row.add(dt.Rows[i]);
                    flpMetric.Controls.Add(row);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }      
        
        public void deleteRow() //画面上删除一行计量单位
        {
            flpMetric.Controls.Remove(rowMetric.RName);           
        }

        public void addRow(string str1, string str2, string str3, string str4)//增加一行计量单位
        {
            rowMetric row = new rowMetric();
            row.addstr(str1, str2, str3,str4);
            flpMetric.Controls.Add(row);
        }

        private void btnAdd_Click(object sender, EventArgs e)//增加按钮单击事件
        {
            AddMetric form = new AddMetric();
            form.ShowDialog(this);
        }

        private void pnlMin_Click(object sender, EventArgs e)//最小化窗体
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Panel_Close_MouseHover(object sender, EventArgs e)//鼠标移入关闭按钮
        {
            Panel_Close.BackColor = Color.FromArgb(255, 255, 192);
        }

        private void Panel_Close_MouseLeave(object sender, EventArgs e)//鼠标移除关闭按钮
        {
            Panel_Close.BackColor = Color.Transparent;
       
        }

        private void Pic_Min_MouseHover(object sender, EventArgs e)//鼠标移入最小化按钮
        {
            Pic_Min.BackColor = Color.FromArgb(255, 255, 192);
        }

        private void Pic_Min_MouseLeave(object sender, EventArgs e)//鼠标移除最小化按钮
        {
            Pic_Min.BackColor =  Color.Transparent;
            //Pic_Min.Parent = this.pictureBox1;
      
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)//单击不放可移动窗体
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblAdd_MouseHover(object sender, EventArgs e)
        {
            lblAdd.ForeColor = Color.White;
        }

        private void lblAdd_MouseLeave(object sender, EventArgs e)
        {
            lblAdd.ForeColor = Color.Silver;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
    }
}
