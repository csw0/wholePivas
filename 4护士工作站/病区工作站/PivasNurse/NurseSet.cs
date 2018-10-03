using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PivasNurse
{
    public partial class NurseSet : Form
    {
        //public string str1;
        public NurseSet()
        {
            InitializeComponent();
            panel1.Visible = false;
            panel2.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;

        }
        DB_Help db = new DB_Help();
        DataTable dt = new DataTable();

        string sql = "";
        public string NurseWardCode;

        private void NurseSet_Load(object sender, EventArgs e)
        {    
            ShowNurseSet();
        }

        public void SetAll()
        {
            //heduikongjian1.ClearForm(str1);
            //heduikongjian2.ClearForm(str1);
            //heduikongjian3.ClearForm(str1);
            //heduikongjian4.ClearForm(str1);
            //heduikongjian5.ClearForm(str1);
        }

        private void Panel_Close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();

        }

        private void Pic_Min_Click(object sender, EventArgs e)
        {
            // this
        }

        private void ChangeWard_Click(object sender, EventArgs e)
        {
            db.IniWriteValuePivas("User", "Ward", "");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" update PivasNurseFormSet set LabelOverFor='");
            str.Append(((comboBox1.SelectedIndex + 1) * 2 + 3));
            str.Append("',PackOverFor='");
            str.Append(((comboBox2.SelectedIndex + 1) * 2 + 3));
            str.Append("',LabelPack='");
            str.Append(((comboBox3.SelectedIndex + 1) * 2 + 3));
            str.Append("',LabelPackAir='");
            str.Append(((comboBox4.SelectedIndex + 1) * 2 + 3));
            str.Append("'  where DateFrom='1' ");
            //str.Append("and WardCode='");
            //str.Append(NurseWardCode);
            //str.Append("' ");
            //str.Append("else ");
            //str.Append("insert into PivasNurseFormSet([DateFrom],[WardCode],[LabelOverFor],[PackOverFor],[LabelPack],[LabelPackAir]) values(1,'");
            //str.Append(NurseWardCode);
            //str.Append("','");
            //str.Append(((comboBox1.SelectedIndex + 1) * 2 + 3));
            //str.Append("','");
            //str.Append(((comboBox2.SelectedIndex + 1) * 2 + 3));
            //str.Append("','");
            //str.Append(((comboBox3.SelectedIndex + 1) * 2 + 3));
            //str.Append("','");
            //str.Append(((comboBox4.SelectedIndex + 1) * 2 + 3));
            //str.Append("')  ");           
            //str.Append("  ");
          db.SetPIVAsDB(str.ToString());
         
        }

        private void ShowNurseSet()
        {
            sql = "select * from PivasNurseFormSet where WardCode='" + NurseWardCode + "'";
            dt = db.GetPIVAsDB(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                show(dt);
            }
            else
            { 
                string sql1 = "Select * from PivasNurseFormSet where DateFrom='1'";
                DataSet ds = db.GetPIVAsDB(sql1);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    StringBuilder str = new StringBuilder();

                    //str.Append("if not  exists(select *from PivasNurseFormSet where WardCode='");
                    //str.Append(NurseWardCode);
                    //str.Append("')");
                    str.Append("insert into PivasNurseFormSet([DateFrom],[WardCode] ,[LabelOverFor],[PackOverFor],[LabelPack] ,[LabelPackAir]) ");
                    str.Append("values('0','");
                    str.Append(NurseWardCode);
                    str.Append("','" + ds.Tables[0].Rows[0]["LabelOverFor"].ToString());
                    str.Append("','" + ds.Tables[0].Rows[0]["PackOverFor"].ToString());
                    str.Append("','" + ds.Tables[0].Rows[0]["LabelPack"].ToString());
                    str.Append("','" + ds.Tables[0].Rows[0]["LabelPackAir"].ToString());
                    str.Append("') ");
                    int b = db.SetPIVAsDB(str.ToString());
                    if (b == 0)
                    {
                        MessageBox.Show("插入病区工作站病区时出错");
                    }
                    else
                    {
                        show(ds.Tables[0]);
                    }
                }
                else
                {
                    MessageBox.Show("未设置初始数据"); 
                }
            }
        }
        private void show(DataTable dt)
        {
            switch (int.Parse(dt.Rows[0]["LabelOverFor"].ToString()))
            {

                case 5: comboBox1.Text = "排药核对";
                    break;
                case 7: comboBox1.Text = "进仓核对";
                    break;
                case 9: comboBox1.Text = "配置核对";
                    break;
                case 11: comboBox1.Text = "出仓核对";
                    break;
                case 13: comboBox1.Text = "打包核对";
                    break;
                case 15: comboBox1.Text = "病区签收";
                    break;
                default:
                    break;
            }

            switch (int.Parse(dt.Rows[0]["PackOverFor"].ToString()))
            {

                case 5: comboBox2.Text = "排药核对";
                    break;
                case 7: comboBox2.Text = "进仓核对";
                    break;
                case 9: comboBox2.Text = "配置核对";
                    break;
                case 11: comboBox2.Text = "出仓核对";
                    break;
                case 13: comboBox2.Text = "打包核对";
                    break;
                case 15: comboBox2.Text = "病区签收";
                    break;
                default:
                    break;
            }

            switch (int.Parse(dt.Rows[0]["LabelPack"].ToString()))
            {

                case 5: comboBox3.Text = "排药核对";
                    break;
                case 7: comboBox3.Text = "进仓核对";
                    break;
                case 9: comboBox3.Text = "配置核对";
                    break;
                case 11: comboBox3.Text = "出仓核对";
                    break;
                case 13: comboBox3.Text = "打包核对";
                    break;
                case 15: comboBox3.Text = "病区签收";
                    break;
                default:
                    break;
            }
            switch (int.Parse(dt.Rows[0]["LabelPackAir"].ToString()))
            {

                case 5: comboBox4.Text = "排药核对";
                    break;
                case 7: comboBox4.Text = "进仓核对";
                    break;
                case 9: comboBox4.Text = "配置核对";
                    break;
                case 11: comboBox4.Text = "出仓核对";
                    break;
                case 13: comboBox4.Text = "打包核对";
                    break;
                case 15: comboBox4.Text = "病区签收";
                    break;
                default:
                    break;
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            int a = 9;
            if (comboBox1.Text == "排药核对")
                a = 5;
            else if (comboBox1.Text == "进仓核对")
                a = 7;
            else if (comboBox1.Text == "配置核对")
                a = 9;
            else if (comboBox1.Text == "出仓核对")
                a = 11;
            else if (comboBox1.Text == "打包核对")
                a = 13;
            else if (comboBox1.Text == "病区签收")
                a = 15;
          int b=  db.SetPIVAsDB("update PivasNurseFormSet set LabelOverFor='" + a + "' where WardCode='" + NurseWardCode + "' ");
          if (b == 0)
          {
              MessageBox.Show("在哪一个环节开始不允许配置取消设置失败"); 
          }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "13816350872")
            {
                panel3.Visible = false;
                panel1.Visible = true;
                panel2.Visible = true;
                panel5.Visible = true;
                panel4.Visible = true;
            }
            else
            {
                MessageBox.Show("密码错误");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (textBox1.Text == "13816350872")
                {
                    panel3.Visible = false;
                    panel1.Visible = true;
                    panel2.Visible = true;
                    panel5.Visible = true;
                    panel4.Visible = true;
                }
                else
                {
                    MessageBox.Show("密码错误");
                }
            }
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            int a = 9;
            if (comboBox2.Text == "排药核对")
                a = 5;
            else if (comboBox2.Text == "进仓核对")
                a = 7;
            else if (comboBox2.Text == "配置核对")
                a = 9;
            else if (comboBox2.Text == "出仓核对")
                a = 11;
            else if (comboBox2.Text == "打包核对")
                a = 13;
            else if (comboBox2.Text == "病区签收")
                a = 15;
          int b=  db.SetPIVAsDB("update PivasNurseFormSet set PackOverFor='" + a + "' where WardCode='" + NurseWardCode + "' ");
          if (b == 0)
          {
              MessageBox.Show("在哪一个环节开始不允许提前打包设置失败");
          }
        }

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            int a = 13;
            if (comboBox3.Text == "排药核对")
                a = 5;
            else if (comboBox3.Text == "进仓核对")
                a = 7;
            else if (comboBox3.Text == "配置核对")
                a = 9;
            else if (comboBox3.Text == "出仓核对")
                a = 11;
            else if (comboBox3.Text == "打包核对")
                a = 13;
            else if (comboBox3.Text == "病区签收")
                a = 15;
          int b = db.SetPIVAsDB("update PivasNurseFormSet set LabelPack='" + a + "' where WardCode='" + NurseWardCode + "'");
          if (b == 0)
          {
           MessageBox.Show("在哪一个环节开始不允许签收设置失败");
          }
        }

        private void comboBox4_TextChanged(object sender, EventArgs e)
        {
            int a = 13;
            if (comboBox4.Text == "排药核对")
                a = 5;
            else if (comboBox4.Text == "进仓核对")
                a = 7;
            else if (comboBox4.Text == "配置核对")
                a = 9;
            else if (comboBox4.Text == "出仓核对")
                a = 11;
            else if (comboBox4.Text == "打包核对")
                a = 13;
            else if (comboBox4.Text == "病区签收")
                a = 15;
          int b=  db.SetPIVAsDB("update PivasNurseFormSet set LabelPackAir='" + a + "' where WardCode='" + NurseWardCode + "'");
          if (b == 0)
          {
              MessageBox.Show("空包在哪一个环节开始不允许签收设置失败"); 
          }
        }

      

       

    
      

    }
}
