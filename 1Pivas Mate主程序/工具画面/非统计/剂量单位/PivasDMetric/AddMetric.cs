using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;

namespace DMetricManage
{
    public partial class AddMetric : Form
    {
        
        DB_Help db = new DB_Help();
        DataTable dt = new DataTable();
        private int a;
        private string UId;

        public AddMetric()
        {
            InitializeComponent();
            
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


        private void btnAdd_Click(object sender, EventArgs e)//数据库内增加计量单位
        {
            string str;
            if (UId==null)
            {
                str = "insert into DMetric values('" + txtCode.Text + "','" + txtName.Text + "', 0)";
            }
            else
            {
                str = "insert into DMetric values('" + txtCode.Text + "','" + txtName.Text + "'," + UId + ")";
            }
            
            FormMetric form1 = (FormMetric)this.Owner;
            db.SetPIVAsDB(str);            
            form1.addRow(txtCode.Text, txtName.Text, cbbPName.Text,UId);
            
            this.Close();                        
        }

        private void AddMetric_Load(object sender, EventArgs e)//新增计量单位时加载Pivas选框内容
        {
            DataSet ds = new DataSet();       
            string str="select * from KD0100..DMetrologyUnit";
            string str1;
            try
            {
                ds=db.GetPIVAsDB(str.ToString());
                dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str1 = dt.Rows[i]["ChineseName"].ToString() + "|" + dt.Rows[i]["EnglishName"].ToString();
                    cbbPName.Items.Add(str1);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("错误：" + ex.Message);
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void pnlExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbbPName_SelectedValueChanged(object sender, EventArgs e)
        {
            a = cbbPName.SelectedIndex;
            UId=dt.Rows[a]["UnitID"].ToString();
        }

        private void Key(object sender, KeyPressEventArgs e)//各编辑框响应的键盘按键
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string str;
                if (UId == null)
                {
                    str = "insert into DMetric values('" + txtCode.Text + "','" + txtName.Text + "', 0)";
                }
                else
                {
                    str = "insert into DMetric values('" + txtCode.Text + "','" + txtName.Text + "'," + UId + ")";
                }

                FormMetric form1 = (FormMetric)this.Owner;
                db.SetPIVAsDB(str);
                form1.addRow(txtCode.Text, txtName.Text, cbbPName.Text, UId);
                this.Close();
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void Panel_Close_MouseHover(object sender, EventArgs e)
        {
            Panel_Close.BackColor = Color.FromArgb(86, 160, 255);
        }

        private void Panel_Close_MouseLeave(object sender, EventArgs e)
        {
            Panel_Close.BackColor = Color.FromArgb(128, 128, 255);
        }

        private void lblAdd_MouseLeave(object sender, EventArgs e)
        {
            lblAdd.BackColor = Color.FromArgb(19, 179, 253);
        }

        private void lblAdd_MouseHover(object sender, EventArgs e)
        {
            lblAdd.BackColor = Color.FromArgb(17, 161, 227);
        }
    }
}
