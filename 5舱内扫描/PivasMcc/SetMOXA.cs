using System;
using System.Windows.Forms;

namespace PivasMcc
{
    public partial class SetMOXA : Form
    {
        public SetMOXA()
        {
            InitializeComponent();
        }

        public string SerialNo;
        public string Model;
        public string IP;
        public string Group;

        public SetMOXA(string SerialNo)
        {
            InitializeComponent();
            txtSerial.Text = SerialNo;
        }

        public SetMOXA(string SerialNo,string Model,string IP,string Group)
        {
            InitializeComponent();
            txtSerial.Text = SerialNo;
            this.Model = Model;
            txtModel.Text = Model;                       
            this.IP = IP;
            txtIP.Text = IP;
            this.Group = Group;
            txtGp.Text = Group;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtSerial.Text.Trim() == "")
            {
                MessageBox.Show("请输入序号");
                return;
            }
            else if (!IsNumeric(txtSerial.Text))
            {
                MessageBox.Show("序号 请输入数字");
                txtSerial.Text = "";
                return;
            }

            if (txtModel.Text.Trim()=="")
            {
                MessageBox.Show("请填写型号");
                txtModel.Focus();
                return;
            }
            else if(txtIP.Text.Trim()=="")
            {
                MessageBox.Show("请填写IP地址");
                txtIP.Focus();
                return;
            }
            else if (txtGp.Text.Trim() == "")
            {
                MessageBox.Show("请填写Group号");
                txtGp.Focus();
                return;
            }
            else
            {
                SerialNo = txtSerial.Text;
                Model = txtModel.Text;                
                IP = txtIP.Text;
                Group = txtGp.Text;
            }

            this.DialogResult = DialogResult.OK;
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
