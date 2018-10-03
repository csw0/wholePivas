using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using PIVAsCommon.Helper;

namespace ConsumablesStatic
{
    public partial class AddPeiZhi : Form
    {
        DB_Help db = new DB_Help();
       
        public AddPeiZhi()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 得到耗材种类
        /// </summary>
        private void GetConSu()
        {
            string sql = " select ConsumablesCode,ConsumablesName+'('+Spec+')'  CONBN from Consumables ";
            DataSet ds = db.GetPIVAsDB(sql);
            if (ds != null&&ds.Tables[0].Rows.Count>0)
            {
                comboBox2.DataSource = ds.Tables[0];              
                comboBox2.DisplayMember = "CONBN";
                comboBox2.ValueMember = "ConsumablesCode";
                comboBox2.SelectedIndex = 0;

                comboBox3.DataSource = ds.Tables[0];
                comboBox3.DisplayMember = "CONBN";
                comboBox3.ValueMember = "ConsumablesCode";
                comboBox3.SelectedIndex = 0;
            }

        }
        /// <summary>
        /// 添加耗材配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                MessageBox.Show("耗材数量不能为空！");
                return;
            }
           
            string sql =string.Format( "if not exists (select 1 from ConsumablesRule where ConsumablesCode='{0}' and DrugType={1})"
                + "insert into ConsumablesRule(ConsumablesCode,DrugType,ConsumablesQuantity,QuantityUnit)values('{0}',{1},'{2}','{3}')"
                , comboBox2.SelectedValue.ToString(),(comboBox1.SelectedIndex + 1).ToString(), textBox1.Text.Trim(), label4.Text.Trim());
            int a = db.SetPIVAsDB(sql);
            if (a == 1)
            {
                MessageBox.Show("添加成功！");
            }
            else
            {
                MessageBox.Show("添加失败！提示：可能是数据库该数据已存在或数据格式不对");
            }
            this.DialogResult = DialogResult.OK;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text.Trim()))
            {
                MessageBox.Show("药品名称不能为空！");
                return;
            }
            if (string.IsNullOrEmpty(textBox5.Text.Trim()))
            {
                MessageBox.Show("耗材数量不能为空！");
                return;
            }
            if (listBox1.Visible == true)
            {
                MessageBox.Show("不能添加配置");
                return;

            }
            string sql = string.Format("if not exists(select 1 from ConsumablesRule_Spec where ConsumablesCode='{0}' and DrugCode='{1}')"
                 + " insert into ConsumablesRule_Spec([ConsumablesCode],[DrugCode],[SpecQuantity],[SpecQuantityUnit])values('{0}','{1}','{2}','{3}')"
                 , comboBox3.SelectedValue.ToString(),textBox3.Tag.ToString().Trim(), textBox5.Text.Trim(),label5.Text.Trim());

            int a = db.SetPIVAsDB(sql);
            if (a == 1)
            {
                MessageBox.Show("添加成功！");
            }
            else
            {
                MessageBox.Show("添加失败！提示：可能是数据库该数据已存在或数据格式不对");
            }
            this.DialogResult = DialogResult.OK;

        }
        private void AddPeiZhi_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            GetConSu();
            GetUnit();
        }

       /// <summary>
       /// 得到耗材的单位
       /// </summary>
        private void GetUnit()
        {
            string sql = string.Format("select ItemUnit from Consumables where ConsumablesCode='{0}'", comboBox2.SelectedValue.ToString());
            DataSet ds = db.GetPIVAsDB(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                label4.Text = ds.Tables[0].Rows[0][0].ToString();
                label5.Text = ds.Tables[0].Rows[0][0].ToString();
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            string sql = string.Format("select top 10 DrugCode,DrugName+'('+Spec+')' DrugName from DDrug where DrugCode like '%{0}%' or DrugName like '%{0}%'", textBox3.Text);
            DataSet ds = db.GetPIVAsDB(sql);
            if (ds != null)
            {
                listBox1.Visible = true;
                listBox1.DataSource = ds.Tables[0];
                listBox1.DisplayMember = "DrugName";
                listBox1.ValueMember = "DrugCode";
            
            }


        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "药品名/药品编码")
            {
                pictureBox1.Visible = false;
                textBox3.Text = "";              
                textBox3.ForeColor = Color.Black;
            }
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            GetUnit();
        }

        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {

            GetUnit();
        }

      

        private void listBox1_Click(object sender, EventArgs e)
        {
            textBox3.Tag = listBox1.SelectedValue.ToString();
            textBox3.Text = listBox1.Text.ToString();
            listBox1.Visible = false;
            pictureBox1.Visible = true;
        }

       

     
    }
}
