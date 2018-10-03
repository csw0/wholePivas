using System;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;

namespace PivasLabelCheckAll.LabelDetails
{
    public partial class LabelDetail : Form
    {
        private string LabelNo;
        DB_Help db = new DB_Help();
       sql sql = new sql();

           string batch=string.Empty; //批次，包括#K
            string K=string.Empty; //K，#
            string L=string.Empty; //长期，临时
            string wardCode =string.Empty;
            string CheckKind = string.Empty;
       int IVStatus=0;

        public LabelDetail(string LabelNo, string bat, string k1, string L1, string wardcode, string checkKind)
        {
            InitializeComponent();
            this.LabelNo = LabelNo;
            this.batch = bat;
            this.K = k1;
            this.L = L1;
            this.wardCode = wardcode;
            this.CheckKind = checkKind;
         
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

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
           
        }

        private void LabelDetail_Load(object sender, EventArgs e)
        {
            state();
            this.Text = LabelNo;
            ShowLabelCheck(LabelNo);
        }

        private void ShowLabelCheck(string Label)
        {
            int i = 0;
            pnlcancel.Controls.Clear();
          
            DataSet ds = db.GetPIVAsDB(sql.msg(Label));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                JugeResons(ds.Tables[0]);
                if (string.IsNullOrEmpty(richTextBox1.Text))
                {
                    richTextBox1.Text = "该瓶签可以扫描"; 
                }
                for (int J = 0; J < ds.Tables[1].Rows.Count; J++)
                {
                    CancelMessage msg = new CancelMessage();
                    msg.setmsg("核对", ds.Tables[1].Rows[J]);
                    msg.Top = i * 20;
                    pnlcancel.Controls.Add(msg);
                    i++;
                }
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
            else
            {
                richTextBox1.Text = "不可扫描：当前瓶签号不存在或者不是瓶签号!";
            }

        }
        private void JugeResons(DataTable dt)
        {
            //批次
            if (batch.Contains(dt.Rows[0]["TeamNumber"].ToString()))
            {
            }
            else
            {
                richTextBox1.Text += "当前瓶签的批次未选中!" +"当前瓶签批次为" + dt.Rows[0]["Batch"].ToString()+ "\r\n";               

            }
            //K,#
            if(string.IsNullOrEmpty(K))
            {
                //label3.Text += "当前未选中K和#!" + "\r\n";
            }
            else if(K == "#K")
            { 
            }
            else if (dt.Rows[0]["Batch"].ToString().Contains(K))
            {
            }
            else
            {
                richTextBox1.Text += "当前选中批次类型（K或#）不正确!" + "\r\n"; 
            }

            //长期， 临时
            if (L.Contains(dt.Rows[0]["KL"].ToString()))
            {
            }
            else
            {
                richTextBox1.Text += "当前瓶签长期临时选中不正确！" + "\r\n";
            }
            //病区
            if (wardCode.Contains(dt.Rows[0]["WardCode"].ToString()))
            {
            }
            else
            {
                richTextBox1.Text += "不可扫描：当前瓶签不在选中的病区中！该瓶签病区为" + dt.Rows[0]["WardName"].ToString() + "\r\n";
            }
            //瓶签状态
            if (int.Parse(dt.Rows[0]["IVStatus"].ToString()) >= IVStatus || int.Parse(dt.Rows[0]["IVStatus"].ToString())<=3)
            {
                richTextBox1.Text += "不可扫描：" + NowState(dt.Rows[0]["IVStatus"].ToString()) + "\r\n";
            }
        }


        private void state()
        {
            switch (CheckKind)
            {
                case "排药核对": IVStatus=5 ;
                    break;
                case "进仓扫描": IVStatus=7;
                    break;
                case "配置核对": IVStatus=9;
                    break;
                case "出仓扫描": IVStatus=11;
                    break;
                case "打包核对": IVStatus=13;
                    break;
                default: break;
            }

        }
        private string NowState(string state1)
        {
            string state = string.Empty;
            switch (state1)
            {
                case "0":
                    state = "这是新生成的瓶签！";
                    break;
                case "1":
                    state = "这是批次新生成的瓶签！";
                    break;
                case "3":
                    state = "这是已打印的瓶签,未摆药！";
                    break;
                case "4":
                    state = "这是已摆药的瓶签！";
                    break;
                case "5":
                    state = "这是已排药的瓶签！";
                    break;
                case "7":
                    state = "这是已进仓的瓶签！";
                    break;
                case "9":
                    state = "这是已配置的瓶签！";
                    break;
                case "11":
                    state = "这是已出仓的瓶签！";
                    break;
                case "13":
                    state = "这是已打包的瓶签！";
                    break;
                case "15":
                    state = "这是已签收的瓶签！";
                    break;

            }
            return state;
        }
    }
}
