using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;

namespace PivasFreqRule
{
    public partial class AddDrugK : Form
    {
        DB_Help DB = new DB_Help();
   
        private string wardCode=string.Empty;
        public AddDrugK(string a)
        {
            InitializeComponent();
           this.wardCode=a;
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

        private void AddDrugK_Load(object sender, EventArgs e)
        {
            string sql = "select DrugCode,DrugName,Spec from DDrug where DrugCode not in(select DrugCode from OrderWcodeDrugK where WardCode='"+wardCode+"')";
            DataSet ds = DB.GetPIVAsDB(sql);
            dgv.DataSource = ds.Tables[0];

        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "药品名/药品简称/拼音码/商品名")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "药品名/药品简称/拼音码/商品名";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //lvMedList.Items.Clear();
                if (textBox1.Text != null && textBox1.Text != "" && textBox1.Text != "药品名/药品简称/拼音码/商品名")
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("select DrugCode,DrugName,Spec from DDrug ");
                    str.Append("where (DrugName like '%");
                    str.Append(textBox1.Text);
                    str.Append("%' or DrugNameJC like '%");
                    str.Append(textBox1.Text);
                    str.Append("%' or SpellCode like '%");
                    str.Append(textBox1.Text);
                    str.Append("%' or ProductName like '%");
                    str.Append(textBox1.Text);
                    str.Append("%' ) and DrugCode not in(select DrugCode from OrderWcodeDrugK where WardCode='");
                    str.Append(wardCode);
                    str.Append("') ");

                    DataSet DS = DB.GetPIVAsDB(str.ToString());
                    dgv.DataSource = DS.Tables[0];
                }
                else
                {
                    string sql = "select DrugCode,DrugName,Spec from DDrug where DrugCode not in(select DrugCode from OrderWcodeDrugK where WardCode='" + wardCode + "')";
                    DataSet ds = DB.GetPIVAsDB(sql);
                    dgv.DataSource = ds.Tables[0];
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count > 0)
            {
                string sql = "insert into OrderWcodeDrugK(WardCode,DrugCode) values('" + wardCode + "','" + dgv.CurrentRow.Cells["DrugCode"].Value.ToString() + "')";
                int a = DB.SetPIVAsDB(sql);
                if (a <= 0)
                {
                    MessageBox.Show("添加药品失败！");
                }
                else
                {
                    this.DialogResult = DialogResult.Yes;
                }
            }
        }

        private void dgv_DoubleClick(object sender, EventArgs e)
        {
            if (dgv.Rows.Count > 0)
            {
                string sql = "insert into OrderWcodeDrugK(WardCode,DrugCode) values('" + wardCode + "','" + dgv.CurrentRow.Cells["DrugCode"].Value.ToString() + "')";
                int a = DB.SetPIVAsDB(sql);
                if (a <= 0)
                {
                    MessageBox.Show("添加药品失败！");
                }
                else
                {
                    this.DialogResult = DialogResult.Yes;
                }
            }

        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

      
    }
}
