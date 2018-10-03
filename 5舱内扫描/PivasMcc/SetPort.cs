using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace PivasMcc
{
    public partial class SetPort : Form
    {
        public SetPort()
        {
            InitializeComponent();
        }
        DB_Help DB = new DB_Help();
        public SetPort(string IP,string PortNo,string Group)
        {
            InitializeComponent();            
            txtPort.Text = PortNo;
            this.IP = IP;
            this.Group = Group;
        }

        public SetPort(string IP,string PortNo, string DeskNo, string DeskDes, string PLCAddress, string RedAddress, string GreenAddress,string ScreenIP,string Group)
        {
            InitializeComponent();
            this.IP = IP;

            this.PortNo = PortNo;
            this.DeskNo = DeskNo;
            this.DeskDes = DeskDes;
            this.PLCAddress = PLCAddress;
            this.RedAddress = RedAddress;
            this.GreenAddress = GreenAddress;
            this.ScreenIP = ScreenIP;
            this.Group = Group;

            txtPort.Text = PortNo;            
            txtDeskNo.Text = DeskNo;            
            txtDeskDes.Text = DeskDes;
            PLC.Text = PLCAddress;
            Red.Text = RedAddress;
            Green.Text = GreenAddress;
            Screen.Text = ScreenIP;
        }

        string IP;
        public string PortNo;
        public string DeskNo;
        public string DeskDes;
        public string PLCAddress;
        public string RedAddress;
        public string GreenAddress;
        public string ScreenIP;
        public string Group;

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtPort.Text.Trim() == "")
            {
                MessageBox.Show("请输入端口号");
                return;
            }
            else if (!IsNumeric(txtPort.Text))
            {
                MessageBox.Show("端口号 请输入数字");
                txtPort.Text = "";
                return;
            }

            if (PLC.Text.Trim()=="")
            {
                MessageBox.Show("请输入PLC");
                return;
            }
           

            if (Red.Text.Trim() == "")
            {
                MessageBox.Show("请输入红灯地址");
                return;
            }
            else if (!IsNumeric(Red.Text))
            {
                MessageBox.Show("红灯地址 请输入数字");
                Red.Text = "";
                return;
            }
            if (Green.Text.Trim() == "")
            {
                MessageBox.Show("请输入绿灯地址");
                return;
            }
            else if (!IsNumeric(Green.Text))
            {
                MessageBox.Show("绿灯地址 请输入数字");
                Green.Text = "";
                return;
            }

            if (PortNo == null||PortNo != txtPort.Text)
            {
                DataSet ds = new DataSet();
                string str = "select * from MOXACon where DeskNo = '" + txtDeskNo.Text + "'";
                ds = DB.GetPIVAsDB(str);
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    MessageBox.Show("此配置台号已存在");
                    ds.Dispose();
                    return;
                }

                str = "Select * from MOXACon where MOXAIP = '" + IP + "' and MOXAPort = " + txtPort.Text;
                ds = DB.GetPIVAsDB(str);
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    MessageBox.Show("此磨砂的该端口已存在，请更换端口号");
                    ds.Dispose();
                    return;
                }
                ds.Dispose();
            }
            this.PortNo = txtPort.Text;
            this.DeskNo = txtDeskNo.Text;
            this.DeskDes = txtDeskDes.Text;
            this.PLCAddress = PLC.Text;
            this.RedAddress = Red.Text;
            this.GreenAddress = Green.Text;
            this.ScreenIP = Screen.Text;

            this.DialogResult = DialogResult.OK;
        }

        private void PLC_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Red_TextChanged(object sender, EventArgs e)
        {

        }

        private void Green_TextChanged(object sender, EventArgs e)
        {
            
        }

        static bool IsNumeric(string str)
        {
            if (str == null || str.Length == 0)
                return false;
            foreach (char c in str)
            {
                if (!Char.IsNumber(c))
                {
                    return false;
                }
            }
            return true;
        } 
    }
}
