using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PivasNurse
{
    public partial class LabelCheck : Form
    {
        private string LabelNo;
        private string Status;
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

        //private void LabelCheck_Load(object sender, EventArgs e)
        //{
        //    ShowLabelCheck(LabelNo,Status);
        //}
        /*
        private void ShowLabelCheck(string Label, string Status)
        {
            DB_Help db=new DB_Help();
            SQL sql = new SQL();
            using (DataTable dt = db.GetPIVAsDB(sql.LabelCheck(Label,Status)).Tables[0]) 
            {
                dgvCheck.DataSource = dt;
                dgvCheck.Columns[0].HeaderText = "扫描记录";
                dgvCheck.Columns[1].HeaderText = "瓶签号";
                dgvCheck.Columns[2].HeaderText = "扫描时间";
                dgvCheck.Columns[3].HeaderText = "扫描人";
                dgvCheck.Columns[0].DataPropertyName = dt.Columns["ScanCount"].ColumnName;
                dgvCheck.Columns[1].DataPropertyName = dt.Columns["IVRecordID"].ColumnName;
                dgvCheck.Columns[2].DataPropertyName = dt.Columns[2].ColumnName;
                dgvCheck.Columns[3].DataPropertyName = dt.Columns["DEmployeeName"].ColumnName;
                dgvCheck.Columns[0].Width =60;
                dgvCheck.Columns[2].Width = 120;
                dgvCheck.Columns[3].Width = 50;
                dgvCheck.Columns[3].Visible = false;
                dgvCheck.Columns[4].Visible = false;
                dgvCheck.Columns[5].Visible = false;
                dgvCheck.Columns[6].Visible = false;
                dgvCheck.Columns[7].Visible = false;
                dgvCheck.Columns[8].Visible = false;
                dgvCheck.Columns[9].Visible = false;
                dgvCheck.Columns[10].Visible = false;
            }
        }
        */
        //private void ShowLabelCheck(string Label, string Status) 
        //{
        //    int i = 0;
        //    pnlcancel.Controls.Clear();
        //    DB_Help db = new DB_Help();
        //    //SQL sql = new SQL();
        //    DataSet ds = db.GetPIVAsDB(sql.msg(Label));
        //    for (int J = 0; J < ds.Tables[1].Rows.Count; J++)
        //    {
        //        CancelMessage msg = new CancelMessage();
        //        msg.setmsg("核对", ds.Tables[1].Rows[J]);
        //        msg.Top = i * 20;
        //        pnlcancel.Controls.Add(msg);
        //        i++;
        //    }
        //    if (int.Parse(ds.Tables[0].Rows[0]["LabelOver"].ToString()) < 0)
        //    {
        //        CancelMessage msg = new CancelMessage();
        //        msg.setmsg("配置取消", ds.Tables[0].Rows[0]);
        //        msg.Top = i * 20;
        //        pnlcancel.Controls.Add(msg);
        //        i++;
        //    }
        //    if (int.Parse(ds.Tables[0].Rows[0]["WardRetreat"].ToString()) > 0)
        //    {
        //        CancelMessage msg = new CancelMessage();
        //        msg.setmsg("退药", ds.Tables[0].Rows[0]);
        //        msg.Top = i * 20;
        //        pnlcancel.Controls.Add(msg);
        //        i++;
        //    }
        
        //}

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
