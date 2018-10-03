using PIVAsCommon.Helper;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PivasDDrug
{
    internal partial class DrugLimitForPrint : Form
    {
        private DB_Help db = new DB_Help();
        private DataSet ds = new DataSet();
        private SqlDataAdapter sda = new SqlDataAdapter();
        private string Drugcode = string.Empty; 
        internal DrugLimitForPrint(string drugcode)
        {
            this.Drugcode = drugcode;
            InitializeComponent();
        }

        /// <summary>
        /// 调用药品控量画面
        /// 根据医生或病区设置某药的用药限量，在打印界面进行判断是否达到要求，能否进行打印
       /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrugLimitForPrint_Load(object sender, System.EventArgs e)
        {
            try
            {   //设置各控件的默认值：//药品编码 //医生：限药时间间隔//病区：限药时间间隔//医生：确认生效//病区：确认生效//医生：数量//病区：数量//医生：天数//病区：天数
                label3.Text = Drugcode;  
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                textBox1.Text = "1000";
                textBox2.Text = "10000";
                textBox3.Text = "30";
                textBox4.Text = "30";

                sda = new SqlDataAdapter(string.Format("SELECT * FROM DrugLimitForPrint where DrugCode='{0}'", Drugcode), db.DatebasePIVAsInfo());   //连接数据库、连接表、执行sql语句

                sda.Fill(ds); //执行sql语句得到的结果

                //表中是否读取到数据
                if (ds != null && ds.Tables.Count > 0)
                {
                    ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns[0] };//第一张表的第一列设为主键

                    //第一张表的行没有值时，赋予默认值
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        dr["DrugCode"] = Drugcode;
                        ds.Tables[0].Rows.Add(dr);
                    }
                    else//将输入的值更新到表中
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        int a = 0;

                        checkBox1.Checked = true.Equals(dr["LimitForDoctor"]);
                        int.TryParse(dr["IntervalDateForDoctor"].ToString(), out a);
                        comboBox1.SelectedIndex = a < 3 ? a : 3;//   如果符合a<3，返回值为a，否则返回值为3
                        textBox1.Text = dr["LimitDgNoForDoctor"].ToString();
                        textBox3.Text = dr["IntervalDateForDoctor"].ToString();

                        checkBox2.Checked = true.Equals(dr["LimitForWard"]);
                        int.TryParse(dr["IntervalDateForWard"].ToString(), out a);
                        comboBox2.SelectedIndex = a < 3 ? a : 3;
                        textBox2.Text = dr["LimitDgNoForWard"].ToString();
                        textBox4.Text = dr["IntervalDateForWard"].ToString();
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //取消按钮
        private void button3_Click(object sender, System.EventArgs e)
        {
            this.Dispose(true);
        }

        //设置完成后保存
        private void button1_Click(object sender, System.EventArgs e)
        {
            try
            {   //用药限量的值为空，输入默认值
                if (string.IsNullOrEmpty(textBox1.Text.Trim()))
                {
                    MessageBox.Show("医生：用药限量不能为空");
                    textBox1.Text = "1000";
                    return;
                }
                try//异常，如果发生异常，做出提示（用药限量设置不符合要求）
                {
                    int.Parse(textBox1.Text.Trim());
                }
                catch
                {
                    MessageBox.Show("医生：请输入正确的限量数值！！！");
                    return;
                }
                if (string.IsNullOrEmpty(textBox2.Text.Trim()))
                {
                    MessageBox.Show("病区：用药限量不能为空");
                    textBox2.Text = "10000";
                    return;
                }
                try
                {
                    int.Parse(textBox2.Text.Trim());
                }
                catch
                {
                    MessageBox.Show("病区：请输入正确的限量数值！！！");
                    return;
                }
                //将comboBox1中的值赋给IntervalDateForDoctor
                int IntervalDateForDoctor = comboBox1.SelectedIndex;
                //comboBox1的值为3时（自定间隔日），输入间隔天数
                if (comboBox1.SelectedIndex == 3)
                {
                    int a = 0;
                    int.TryParse(textBox3.Text.Trim(), out a);//将textBox3中的值转为int型，并赋给a
                    //天数小于3，做出提示；大于3，将天数值赋给IntervalDateForDoctor
                    if (a < 3)
                    {
                        MessageBox.Show("医生：请输入正确的间隔天数（必须大于2）");
                        return;
                    }
                    else
                    {
                        IntervalDateForDoctor = a;
                    }
                }

                int IntervalDateForWard = comboBox2.SelectedIndex;
                if (comboBox2.SelectedIndex == 3)
                {
                    int b = 0;
                    int.TryParse(textBox4.Text.Trim(), out b);
                    if (b < 3)
                    {
                        MessageBox.Show("病区：请输入正确的间隔天数（必须大于2）");
                        return;
                    }
                    else
                    {
                        IntervalDateForWard = b;
                    }
                }

                //将输入的值传到数据库的表中
                DataRow dr = ds.Tables[0].Rows[0];

                dr["LimitForDoctor"] = checkBox1.Checked;
                dr["LimitForWard"] = checkBox2.Checked;

                dr["LimitDgNoForDoctor"] = textBox1.Text.Trim();
                dr["LimitDgNoForWard"] = textBox2.Text.Trim();

                dr["IntervalDateForDoctor"] = IntervalDateForDoctor;
                dr["IntervalDateForWard"] = IntervalDateForWard;
                //更新数据
                using (SqlCommandBuilder scb = new SqlCommandBuilder(sda))
                {
                    scb.DataAdapter.Update(ds);
                }
                MessageBox.Show("保存成功！！");
                this.Dispose();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //只有选择为自定间隔日时，textBox3（textBox3）、label5（label5）才会出现
        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            textBox3.Visible = label5.Visible = (comboBox1.SelectedIndex == 3);
        }

        private void comboBox2_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            textBox4.Visible = label2.Visible = (comboBox2.SelectedIndex == 3);
        }
    }
}