using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace qqClient
{
    public partial class AddFriend : Form
    {
        SQL sql = new SQL();
        DB_Help db = new DB_Help();
        private string DEmployId = string.Empty;
        public AddFriend(string demployId)
        {
            InitializeComponent();
            this.DEmployId = demployId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox1.Text == "姓名/编号")
            {
                MessageBox.Show("请输入姓名和编号");
                return;
            }
            DataSet ds = db.GetPIVAsDB(sql.GetEmployee(textBox1.Text));
            if (ds != null)
            {
                dgv.DataSource = ds.Tables[0];
            }


        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "姓名/编号")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "姓名/编号";
                textBox1.ForeColor = Color.Gray;
            }
        }


        private void dgv_DoubleClick(object sender, EventArgs e)
        {
            if (dgv != null)
            {
                DataSet ds = db.GetPIVAsDB(sql.IsFriend(DEmployId, dgv.CurrentRow.Cells["DEmployeeID"].Value.ToString()));
                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    db.SetPIVAsDB(sql.AddFriend(DEmployId, dgv.CurrentRow.Cells["DEmployeeID"].Value.ToString(),0));
                    MessageBox.Show("添加成功！");
                }
                else if (ds.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("已经是你的好友！！", "提示", MessageBoxButtons.OK);
                }
              
            }
        }

        private void AddFriend_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "病区编号/病区名称")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "病区编号/病区名称";
                textBox2.ForeColor = Color.Gray;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox2.Text == "病区编号/病区名称")
            {
                MessageBox.Show("请输入病区名称或病区编号");
                return;
            }
            DataSet ds = db.GetPIVAsDB(sql.GetWard(textBox2.Text));
            if (ds != null)
            {
                dgv1.DataSource = ds.Tables[0];
            }


        }

        private void dgv1_DoubleClick(object sender, EventArgs e)
        {
            if (dgv1 != null)
            {
                DataSet ds = db.GetPIVAsDB(sql.IsFriend(DEmployId, dgv1.CurrentRow.Cells["WardCode"].Value.ToString()));
                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    db.SetPIVAsDB(sql.AddFriend(DEmployId, dgv1.CurrentRow.Cells["WardCode"].Value.ToString(),1));
                    MessageBox.Show("添加成功！");
                }
                else if (ds.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("已经是你的好友！！", "提示", MessageBoxButtons.OK);
                }

            }
        }

        private void AddFriend_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage2);
        }

      
       


    }
}
