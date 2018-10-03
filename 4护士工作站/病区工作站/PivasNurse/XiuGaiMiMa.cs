using PIVAsCommon.Helper;
using System;
using System.Text;
using System.Windows.Forms;

namespace PivasNurse
{
    public partial class XiuGaiMiMa : Form
    {
        DB_Help db = new DB_Help();
        public string AccountID;
        public XiuGaiMiMa()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != textBox2.Text.Trim())
            { MessageBox.Show("两次输入新密码不一致！"); }
            else
            {
                StringBuilder Sb = new StringBuilder();
                Sb.Append("UPDATE DEmployee ");
                Sb.Append("SET Pas = '");
                Sb.Append(textBox1.Text.Trim());
                Sb.Append("' ");
                Sb.Append(" WHERE  ");
                Sb.Append("[DEmployeeID] =  '");
                Sb.Append(AccountID.Trim());
                Sb.Append("' ");
                db.SetPIVAsDB(Sb.ToString());
                this.Dispose();
            }
        }
    }
}
