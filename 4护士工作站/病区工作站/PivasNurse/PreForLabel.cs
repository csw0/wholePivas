using System;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;

namespace PivasNurse
{
    public partial class PreForLabel : Form
    {
        private string PrescriptionID;
        //private string Status;
        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion
        
        
        public PreForLabel()
        {
            InitializeComponent();

        }
        
        public PreForLabel(string PrescriptionID)
        {
            InitializeComponent();
            this.PrescriptionID = PrescriptionID;
            //this.Status = Status;
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void ShowLabelCheck(string Label, string date)
        {
            DB_Help db = new DB_Help();
            SQL sql = new SQL();
            dgvPre.Rows.Clear();
            using (DataTable dt = db.GetPIVAsDB(sql.PreForLabel(Label, date)).Tables[0])
            {
                if (dt.Rows.Count> 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dgvPre.Rows.Add(1);
                        dgvPre.Rows[i].Cells["LabelNo"].Value = dt.Rows[i]["LabelNo"].ToString();
                        dgvPre.Rows[i].Cells["BedNo"].Value = dt.Rows[i]["BedNo"].ToString();
                        dgvPre.Rows[i].Cells["PatName"].Value = dt.Rows[i]["PatName"].ToString();
                        dgvPre.Rows[i].Cells["IVStatus"].Value = CheckReturn(int.Parse(dt.Rows[i]["IVStatus"].ToString()), dt.Rows[i]["WardRetreat"].ToString(), int.Parse(dt.Rows[i]["LabelOver"].ToString()));
                        dgvPre.Rows[i].Cells["Batch"].Value = dt.Rows[i]["Batch"].ToString().Trim();
                        dgvPre.Rows[i].Cells["FreqCode"].Value = dt.Rows[i]["FreqCode"].ToString().Trim();
                        dgvPre.Rows[i].Cells["PreCode"].Value = dt.Rows[i]["PrescriptionID"].ToString();

                       
                    }
                }
            }
        }
        private string CheckReturn(int a, string WardRetreat, int LabelOver)
        {
            if (WardRetreat == "True")
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
                    case 3: return "已打印";
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            ShowLabelCheck(PrescriptionID, dateTimePicker1.Text);
        }

        private void PreForLabel_Load(object sender, EventArgs e)
        {
            ShowLabelCheck(PrescriptionID, dateTimePicker1.Text);
        }

        private void dgvPre_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
    
    }
}
