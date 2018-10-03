using System;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;
using PivasLimitDES;

namespace ErrorRecord
{
    public partial class MainError : Form
    {
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MOVE = 0xF010;
        private const int HTCAPTION = 0x0002;
        private const int CS_DropSHADOW = 0x20000;
        private const int GCL_STYLE = (-26);
        DB_Help DB = new DB_Help();
        string LabelPreID = string.Empty;
        string UserID = string.Empty;
  
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        [DllImport("user32.dll")]
        private static extern int SetClassLong(IntPtr hwnd, int nlndex, int dwNewLong);
        [DllImport("user32.dll")]
        private static extern int GetClassLong(IntPtr hwnd, int nlndex);
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString2(string Database, string key, string val, string filePath);
        public MainError()
        {
            InitializeComponent();
            UserID = "";
        }
 
        public MainError(string ID)
        {
            InitializeComponent();
            this.UserID=ID;
            string str = "select DEmployeeName from DEmployee where DEmployeeID= '" + UserID + "'";
            DataSet ds = DB.GetPIVAsDB(str);
            labelUser.Text = ds.Tables[0].Rows[0][0].ToString();
            
        }
        public MainError(string PreID,string ID)
        {
            InitializeComponent();
            LabelPreID = PreID;
            this.UserID = ID;
            string str = "select DEmployeeName from DEmployee where DEmployeeID= '" + UserID + "'";
            DataSet ds = DB.GetPIVAsDB(str);
            if (ds.Tables[0].Rows.Count != 0)
            {
                labelUser.Text = ds.Tables[0].Rows[0][0].ToString();
            }

        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
           // MessageBox.Show(UserID);
            if (UserID == null || UserID == "")
            {
                MessageBox.Show("  请从正常途径登录本画面！ ");
                this.Dispose();
            }
            else if (GetPivasLimit.Instance.Limit(UserID, "ErrorRecord"))
            {
                //MessageBox.Show(UserID+"123456");
                radioButton2.Checked = true;
                panel1.Controls.Add(new ErrorAddOthers(LabelPreID, UserID));
                //radioButton1.Checked = true;
                //panel1.Controls.Add(new ErrorAdd(UserID));
                panel1.Controls[0].Dock = DockStyle.Fill;
                labelnowtime.Text = DateTime.Now.Date.ToShortDateString();
                this.labelnowtime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                //this.BackColor = Color.Green;
                //this.TransparencyKey = Color.Green;
                //this.Opacity = 0.95;
            }
            else
            {
                this.Dispose();
            }
        }

        private void panel6_MouseEnter(object sender, EventArgs e)
        {
            //panel6.BackgroundImage = Properties.Resources.MH;
            panel6.BackColor = System.Drawing.Color.OrangeRed;
        }
        private void panel6_MouseLeave(object sender, EventArgs e)
        {
            //panel6.BackgroundImage = Properties.Resources._3;
            panel6.BackColor = System.Drawing.Color.Transparent;
        }
        private void panel4_MouseEnter(object sender, EventArgs e)
        {
           // panel4.BackgroundImage = Properties.Resources._8;
            panel4.BackColor = System.Drawing.Color.DeepSkyBlue;
        }
        private void panel4_MouseLeave(object sender, EventArgs e)
        {
            //panel4.BackgroundImage = Properties.Resources._9;
            panel4.BackColor = System.Drawing.Color.Transparent;
        }
        private void panel6_MouseClick(object sender, MouseEventArgs e)
        {
            Dispose();
        }
        private void panel5_MouseClick(object sender, MouseEventArgs e)
        {
            MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        }
        private void panel4_MouseClick(object sender, MouseEventArgs e)
        {
            MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Minimized;

        }
        private void panel5_MouseEnter(object sender, EventArgs e)
        {
            panel5.BackgroundImage = WindowState == FormWindowState.Maximized ? Properties.Resources._6 : Properties.Resources._4;
        }
        private void panel5_MouseLeave(object sender, EventArgs e)
        {
            panel5.BackgroundImage = WindowState == FormWindowState.Maximized ? Properties.Resources._7 : Properties.Resources._5;
        }
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //if (linkLabel1.Text == "规则维护")
            //{
            //    linkLabel1.Text = "规则维护 >>>";
            //    linkLabel1.Location = new System.Drawing.Point(759, 72);
                new Rule().ShowDialog();
            //}
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                radioButton2.Checked = false;
                panel1.Controls.Clear();
                panel1.Controls.Add(new ErrorAdd());
            }
            else
            {
                radioButton1.Checked = false;
                panel1.Controls.Clear();
                panel1.Controls.Add( new ErrorAddOthers());     
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelnowtime.Text = DateTime.Now.ToString();
        }
      
    }
}
