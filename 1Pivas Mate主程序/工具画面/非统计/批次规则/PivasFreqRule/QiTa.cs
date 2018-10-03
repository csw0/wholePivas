using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace PivasFreqRule
{
    public partial class QiTa : UserControl
    {
        DB_Help DB = new DB_Help();
        public QiTa()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 空包排入固定批次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                comboBox1.Enabled = true;
                checkBox4.Checked = true;
                checkBox4.Enabled = false;
            }
            else
            {
                comboBox1.Enabled = false;
                checkBox4.Checked = false;
                checkBox4.Enabled = true;
            }
            string str = "update orderrule2 set K ='" + checkBox1.Checked +"'";
            DB.SetPIVAsDB(str);     
        }

        /// <summary>
        /// 当输液无法接续时，剩余的一日多次的输液往上靠
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            string str = "update orderrule2 set ContinuousTransfusion ='" + checkBox1.Checked + "'";
            DB.SetPIVAsDB(str);     
        }                           


        /// <summary>
        /// 空包不计算容积
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            string str = "update orderrule2 set WithOutK ='" + checkBox4.Checked + "'";
            DB.SetPIVAsDB(str);     
        }

        /// <summary>
        /// 优先考虑一日多次医嘱首次用药
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            string str = "update orderrule2 set FirstOfMultiPrio ='" + checkBox4.Checked + "'";
            DB.SetPIVAsDB(str);     
        }

        public void SetBatch()
        {


            DataSet d1 = new DataSet();
            string str = "Select * from DOrder where IsValid = 1 Order BY OrderID";
            d1 = DB.GetPIVAsDB(str);
            if (d1 == null || d1.Tables[0].Rows.Count == 0)
            {
            }
            else
            {
                comboBox1.DataSource = d1.Tables[0];
                comboBox1.DisplayMember = "OrderID";
                comboBox1.ValueMember = "OrderID";
            }

            str = "Select top 1 * from OrderRule2 ";
            DataSet d = new DataSet();
            d = DB.GetPIVAsDB(str);
            if (d == null || d.Tables[0].Rows.Count == 0)
            {


                checkBox1.Checked = true;
                checkBox2.Checked = true;
                checkBox3.Checked = true;
                checkBox4.Checked = true;
                comboBox1.Text = "1";
                str = "INSERT INTO OrderRule2 Values('"+Convert.ToInt32(checkBox1.Checked) + "," + comboBox1.Text + "," +
                    Convert.ToInt32(checkBox2.Checked) + "," + Convert.ToInt32(checkBox3.Checked) +"," + Convert.ToInt32(checkBox3.Checked) +")";
                DB.SetPIVAsDB(str);

            }
            else
            {
                checkBox1.Checked = Convert.ToBoolean(d.Tables[0].Rows[0]["K"].ToString());
                checkBox2.Checked = Convert.ToBoolean(d.Tables[0].Rows[0]["ContinuousTransfusion"].ToString());
                checkBox3.Checked = Convert.ToBoolean(d.Tables[0].Rows[0]["FirstOfMultiPrio"].ToString());
                checkBox4.Checked = Convert.ToBoolean(d.Tables[0].Rows[0]["WithOutK"].ToString());
                comboBox1.Text = d.Tables[0].Rows[0]["KIN"].ToString();
            }
        }

        private void QiTa_Load(object sender, EventArgs e)
        {
            SetBatch();
        }

       

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string str = "update orderrule2 set KIN =" + comboBox1.SelectedValue.ToString();// +" where wardcode= '" + WardCode + "'";
            DB.SetPIVAsDB(str);  
        }

    
    }
}
