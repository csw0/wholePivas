using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace WorkStatic
{
    internal sealed partial class Login : Form
    {
        private DB_Help db = new DB_Help();
        internal Login()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = checkBox1.Checked ? (char)0 : '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataSet ds = db.GetPIVAsDB(string.Format("SELECT * FROM DEmployee WHERE AccountID = '{0}'", textBox1.Text.Trim())))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Pas"].ToString().Trim() == textBox2.Text.Trim())
                    {
                        using (Form1 form = new Form1(ds.Tables[0].Rows[0]["DEmployeeID"].ToString().Trim()))
                        {
                            this.Hide();
                            form.ShowDialog();
                        }
                        this.Close();
                    }
                    else
                        MessageBox.Show("用户名或密码输入有误");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }
    }
}
