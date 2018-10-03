using PIVAsCommon.Helper;
using System;
using System.Windows.Forms;

namespace Damage
{
    public partial class Form4 : Form
    {
        DB_Help DB = new DB_Help();
        public Form4()
        {
            InitializeComponent();
        }

        private static Form4 formlnstance4;

        public static Form4 instance4
        {
            get
            {
                if (formlnstance4 == null)
                {
                    formlnstance4 = new Form4();
                }
                if (formlnstance4.IsDisposed)
                {
                    formlnstance4 = new Form4();
                }
                return formlnstance4;
            }
        }

        public static void ShowChildForm4()
        {
            if (!instance4.Visible)
            {
                instance4.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null || textBox1.Text == "")
            {
                //lb_notice.Text = "药品不能为空";
                //lb_notice.BackColor = Color.Red;
                MessageBox.Show("请输入原因");
                textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            }
            else {
                string reason = textBox1.Text.ToString();
                string str = " insert into DamageReason values ";
                str += "( (select case when  MAX(id) is null then 1 else MAX(id)+1 end from DamageReason) ";
                str += " ,'" + reason + "') ";
                string result = DB.SetPIVAsDB(str) == 1 ? "添加成功" : "添加失败";
                textBox1.Text = "";
            }
            

        }


    }
}
