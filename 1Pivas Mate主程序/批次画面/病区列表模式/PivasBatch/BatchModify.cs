using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;

namespace PivasBatch
{
    public partial class BatchModify : Form
    {
        DB_Help DB = new DB_Help();
        //批次
        public  string teamNum = string.Empty;
        //#或者空
        public string K_ = string.Empty;
        public string reason = string.Empty;
        public BatchModify()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BatchModify_Load(object sender, EventArgs e)
        {
            GetBatch();
            
        }

        private void GetBatch()
        {
            string sql = "select OrderID from DOrder where IsValid=1 order by OrderID";
            DataTable dt = DB.GetPIVAsDB(sql).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Button b = new Button();
                b.Text = dt.Rows[i]["OrderID"].ToString();
                b.Size = new Size(40, 40);
                //b.FlatStyle = FlatStyle.System;
                b.BackColor = Color.White;
                b.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                b.Click += new EventHandler(Batch_Click);
                this.toolTip1.SetToolTip(b, "选择此项，将改变当前界面所有瓶签的批次");
                //b.MouseHover += new EventHandler(Batch_MouseHover);
                //b.MouseLeave += new EventHandler(Batch_MouseLeave);
                ftp.Controls.Add(b);
            
            }
        }

        private void Batch_Click(object sender, EventArgs e)
        {
         
            foreach (Control c in ftp.Controls)
            {
                c.BackColor = Color.White;
            }
            Button b = (Button)sender;   
            b.BackColor = Color.Red;
            teamNum = b.Text;
        }
      
        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "请输入修改原因(选填)")
            {
                reason = richTextBox1.Text;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                K_ = checkBox1.Text;
            }
           
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                K_ ="-"+ checkBox2.Text;
            }
        }

        private void Batch_MouseHover(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.BackColor == Color.White)
            {
                b.BackColor = Color.Red;
            }
        }

        private void Batch_MouseLeave(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.BackColor == Color.Red)
            {
                b.BackColor = Color.White;
            }
        }

        private void richTextBox1_Enter(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "请输入修改原因(选填)")
            {
                richTextBox1.Text = "";
                richTextBox1.ForeColor = Color.Black;
            }
        }

        private void richTextBox1_Leave(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                richTextBox1.Text = "请输入修改原因(选填)";
                richTextBox1.ForeColor = Color.Gray;
            }
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

        private void ftp_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

      

    }
}
