using PIVAsCommon.Helper;
using System;
using System.Windows.Forms;

namespace ConsumablesStatic
{
    public partial class AddConsumables : Form
    {
        DB_Help db = new DB_Help();
        public AddConsumables()
        {
            InitializeComponent();
        }
     
        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                MessageBox.Show("耗材编号不能为空");
                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text.Trim()))
            {
                MessageBox.Show("耗材名称不能为空");
                return;        
            }
            if (string.IsNullOrEmpty(textBox4.Text.Trim()))
            {
                MessageBox.Show("耗材单位不能为空");
                return;    
            }
            string sql = string.Format("if not exists(select 1 from Consumables where ConsumablesCode='{0}' ) "
                + "insert into Consumables([ConsumablesCode],[ConsumablesName],[Spec],[ItemUnit])values('{0}','{1}','{2}','{3}')"
                ,textBox1.Text.Trim(),textBox2.Text.Trim(),textBox3.Text.Trim(),textBox4.Text.Trim());
           int a= db.SetPIVAsDB(sql);
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

        private void Panel_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
