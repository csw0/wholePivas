using System;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;

namespace PivasNurse
{
    public partial class DoctorOpinion : Form
    {
        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion
        public DoctorOpinion()
        {
            InitializeComponent();
        }

        public DoctorOpinion(string Account,string RecID,string PreID)
        {
            InitializeComponent();
            AccountID = Account;
            CPRecordID = RecID;
            this.PreID = PreID;
        }
        string AccountID, CPRecordID,Operate;
        DB_Help DB = new DB_Help();
        string PreID;


        private void button1_Click(object sender, EventArgs e)
        {
            if (txtDoc.Text.Trim()=="")
            {
                MessageBox.Show("请输入账户名");
                txtDoc.Focus();
                return;
            }
            //else if (txtPas.Text.Trim()=="")            
            //{
            //    MessageBox.Show("请输入密码");
            //    txtPas.Focus();
            //    return;
            //}

            if (comboBox1.Text.Trim()=="" && rBRun.Checked)
            {
                MessageBox.Show("请填写说明");
                comboBox1.Focus();
                return;
            }
            string str = "SELECT * FROM DEmployee WHERE AccountID='" + txtDoc.Text + "' AND Pas ='" + txtPas.Text + "'";            
            if (DB.GetPIVAsDB(str).Tables[0].Rows.Count==0)
            {
                MessageBox.Show("账户或密码错误");
                return;
            }

           if (rBAccept.Checked)
           {
               Operate = "2";
               //str = "UPDATE CPRecord SET DoctorExplain = '接受退单',CheckDCode = '" + AccountID + "', DoctorOperate = " + Operate
               // + "WHERE CPRecordID = " + CPRecordID;
               str = "IF EXISTS (SELECT * FROM CPRecord WHERE CPRecordID = "+CPRecordID+" ) " +
                    "UPDATE CPRecord SET DoctorExplain = '接受退单',CheckDCode = '" + AccountID + "', DoctorOperate = 2  " +
                    "WHERE CPRecordID = " + CPRecordID +" ELSE " +
                    "INSERT INTO CPRecord VALUES ('" + PreID + "',0,GETDATE(),'接受退单','" + AccountID + "',GETDATE(),1,2)";
          
           }
           else
           {
               Operate = "1";
               //str = "UPDATE CPRecord SET DoctorExplain = '" + comboBox1.Text + "',CheckDCode = '" + AccountID + "', DoctorOperate = " + Operate
               // + "WHERE CPRecordID = " + CPRecordID;
               str = "IF EXISTS (SELECT * FROM CPRecord WHERE CPRecordID = "+CPRecordID+" ) "+
                    "UPDATE CPRecord SET DoctorExplain = '" + comboBox1.Text + "',CheckDCode = '" + AccountID + "', DoctorOperate = 1  " +
                    "WHERE CPRecordID = "+CPRecordID+" "+
                    "ELSE "+
                    "INSERT INTO CPRecord VALUES ('"+PreID+"',0,GETDATE(),'" + comboBox1.Text + "','" + AccountID + "',GETDATE(),1,1)";

               if (DB.GetPivasAllSet("护士站-系统审方-强制执行") == "1")
               {
                   string sql = "update Prescription set PStatus=1 where PrescriptionID='" + PreID + "'";
                   DB.SetPIVAsDB(sql);
               }
           }
            
            DB.SetPIVAsDB(str);

            this.DialogResult = DialogResult.OK;
        }

        private void rBAccept_Click(object sender, EventArgs e)
        {
            if (rBAccept.Checked)
            {
                comboBox1.Enabled = false;
            }
            else
                comboBox1.Enabled = true;
        }

        private void rBRun_CheckedChanged(object sender, EventArgs e)
        {
            if (rBRun.Checked)
            {
                comboBox1.Enabled = true;
            }
            else
                comboBox1.Enabled = false;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void DoctorOpinion_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
    }
}
