using System;
using System.Data;
using System.Windows.Forms;
using PivasLabelSelect;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;

namespace PivasNurse
{
    public partial class LabelCheck : Form
    {
        private string LabelNo;
        private string Status;
        DB_Help db = new DB_Help();
        SQL sql = new SQL();
        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion
        public LabelCheck()
        {
            InitializeComponent();

        }
        

        public LabelCheck(string LabelNo,string Status)
        {
            InitializeComponent();
            this.LabelNo = LabelNo;
            this.Status = Status;
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void LabelCheck_Load(object sender, EventArgs e)
        {
            ShowLabelCheck(LabelNo,Status);
        }
      
        private void ShowLabelCheck(string Label, string Status) 
        {
            int i = 0;
            pnlcancel.Controls.Clear();          
            DataSet ds = db.GetPIVAsDB(sql.msg(Label));
            for (int J = 0; J < ds.Tables[1].Rows.Count; J++)
            {
                CancelMessage msg = new CancelMessage();
                msg.setmsg("核对", ds.Tables[1].Rows[J]);
                msg.Top = i * 20;
                pnlcancel.Controls.Add(msg);
                i++;
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                if (int.Parse(ds.Tables[0].Rows[0]["LabelOver"].ToString()) < 0)
                {
                    CancelMessage msg = new CancelMessage();
                    msg.setmsg("配置取消", ds.Tables[0].Rows[0]);
                    msg.Top = i * 20;
                    pnlcancel.Controls.Add(msg);
                    i++;
                }
                if (int.Parse(ds.Tables[0].Rows[0]["WardRetreat"].ToString()) > 0)
                {
                    CancelMessage msg = new CancelMessage();
                    msg.setmsg("退药", ds.Tables[0].Rows[0]);
                    msg.Top = i * 20;
                    pnlcancel.Controls.Add(msg);
                    i++;
                }
            }         
        }

        private void pnlcancel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
    }
}
