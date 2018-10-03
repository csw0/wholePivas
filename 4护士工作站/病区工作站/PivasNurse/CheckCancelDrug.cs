using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace PivasNurse
{
    public partial class CheckCancelDrug : Form
    {
        public CheckCancelDrug()
        {
            InitializeComponent();
        }
        public delegate void NewDelegate(string doctor,DateTime time);
        
        public static event NewDelegate CancelDrug;
        DB_Help DB = new DB_Help();
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtDoc.Text.Trim() == "")
            {
                MessageBox.Show("请输入账户名");
                txtDoc.Focus();
                return;
            }
            string str = "SELECT * FROM DEmployee WHERE AccountID='" + txtDoc.Text + "' AND Pas ='"+ txtPas.Text + "'";
            DataSet ds=new DataSet();
            ds = DB.GetPIVAsDB(str);
            if (ds==null||ds.Tables.Count==0||ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("账户不存在");
                ds.Dispose();
                return;
            }
            CancelDrug(ds.Tables[0].Rows[0]["DEmployeeID"].ToString(), DateTime.Now);
         
            ds.Dispose();
            this.DialogResult = DialogResult.OK;
        }
    }
}
