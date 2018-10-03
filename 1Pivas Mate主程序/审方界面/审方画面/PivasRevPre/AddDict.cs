using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PivasRevPre
{
    public partial class AddDict : Form
    {
        public AddDict()
        {
            InitializeComponent();
        }

        string type;
        ListViewItem lvi;
        string id;
        

        public AddDict(string type,ListViewItem lvi,string id)
        {
            InitializeComponent();
            this.type = type;
            this.lvi = lvi;
            this.id = id;

        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("不能为空!");
                return;
            }

            string sql = "";
            if (type == "1")
            {
                if (lvi == null)
                    sql = "insert into CPResultRGDict values('{0}')";
                else
                    sql = "update CPResultRGDict set name='{0}' where [ID]="+id;
            }
            else if (type == "2") 
            {
                if (lvi == null)
                    sql = "insert into CPResultRGDictDetail values('"+id+"','{0}')";
                else
                    sql = "update CPResultRGDictDetail set [Describe]='{0}' where [ID]='" + lvi.SubItems[0].Text.Trim() + "'";
            }
            
            RGDict.db.SetPIVAsDB(string.Format(sql,textBox1.Text.Trim()));
            this.DialogResult = DialogResult.OK;
        }

        private void AddUpdateDict_Load(object sender, EventArgs e)
        {
            if (lvi != null)
                textBox1.Text = lvi.SubItems[1].Text;

            if (type == "1")
            {
                label1.Text = "审方结果：";
            }
            else 
            {
                label1.Text = "说明：";
            }
            textBox1.Focus();
           // textBox1.SelectAll();
        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
    }
}
