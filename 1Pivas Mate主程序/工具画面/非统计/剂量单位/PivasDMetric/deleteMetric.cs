using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;

namespace DMetricManage
{
    public partial class deleteMetric : Form
    {
        
        DB_Help db = new DB_Help();
        DataTable dt = new DataTable();
   
        private string Eid;

        public deleteMetric()
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


        private void btnDelete_Click(object sender, EventArgs e)//删除数据库内计量单位
        {
            string str = "delete from DMetric where MetricCode = '" + label5.Text + "' and " +
                       "MetricName = '" + label6.Text + "' and UnitID = " + Eid;
            try
            {
                FormMetric form = (FormMetric)this.Owner;
                db.SetPIVAsDB(str);
                form.deleteRow();
                this.DialogResult = DialogResult.OK;
                this.Close(); 
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         
            
        }

        private void deleteMetric_Load(object sender, EventArgs e)
        {            
            try
            {
                label5.Text = rowMetric.oldcode;
                label6.Text = rowMetric.oldname;
                label7.Text = rowMetric.PName;
                Eid = rowMetric.Oldid;
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

        private void pnlExit_Click(object sender, EventArgs e)//关闭
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void Panel_Close_MouseHover(object sender, EventArgs e)//鼠标移入
        {
            Panel_Close.BackColor = Color.FromArgb(86, 160, 255);
        }

        private void Panel_Close_MouseLeave(object sender, EventArgs e)//鼠标移除
        {
            Panel_Close.BackColor = Color.FromArgb(192, 192, 255);
        }

        private void lblDelete_MouseLeave(object sender, EventArgs e)
        {
            lblDelete.BackColor = Color.FromArgb(192, 0, 0);
        }

        private void lblDelete_MouseHover(object sender, EventArgs e)
        {
            lblDelete.BackColor = Color.Red;
        }
     
    }
}
