using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace PivasDDrug
{
    public partial class SetDrugPlusCondition : Form
    {
        public SetDrugPlusCondition()
        {
            InitializeComponent();
        }

        Select select = new Select();
        Update update = new Update();
        Insert insert = new Insert();
        DB_Help db = new DB_Help();
        Delete delet = new Delete();
        private void AddDrugPlusCondition_Load(object sender, EventArgs e)
        {
            Datashow();
            Btn_Cancel.Enabled = false;
        }

        private void Datashow()
        {
            DataSet ds = db.GetPIVAsDB(select.DDrugPlusCondition(""));
            listBox1.DataSource = ds.Tables[0];
            listBox1.DisplayMember = "Name";
            listBox1.ValueMember = "Id";
            listBox1.SelectedItem = null;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //    try
            //    {
            //        Txt_DrugPlus.Text = listBox1.Text;
            //        Txt_DrugPlus.Tag = listBox1.SelectedValue.ToString();
            //        MessageBox.Show(Txt_DrugPlus.Tag.ToString());

            //    }
            //    catch { }
        }

        private void Btn_Add_Click(object sender, EventArgs e)
        {
            if (Txt_DrugPlus.Text != "")
            {
                if (Btn_Add.Text == "添加")
                {
                    if (sele())
                    {
                        //添加
                        int f = db.SetPIVAsDB(insert.PlusCondition(Txt_DrugPlus.Text));
                        if (f > 0)
                        {
                            MessageBox.Show("添加成功");
                            Datashow();
                        }
                    }
                    else
                    {
                        MessageBox.Show("您想添加的附加条件已存在！");
                    }
                }
                else
                {
                    if (sele())
                    {
                        //修改
                        int f = db.SetPIVAsDB(update.DDrugPlusCondition(Txt_DrugPlus.Text, Txt_DrugPlus.Tag.ToString()));
                        if (f > 0)
                        {
                            MessageBox.Show("修改成功");
                            Datashow();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("添加的附加条件为空！");
                Txt_DrugPlus.Focus();
            }
        }

        private bool sele()
        {
            bool tag = false;
            foreach (DataRowView c in listBox1.Items)
            {
                if (c["Name"].ToString().Trim() == Txt_DrugPlus.Text.Trim())
                {
                    tag = false;
                    break;
                }
                else
                {
                    tag = true;
                }
            }
            return tag;
        }

        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            //删除
            try
            {
                int f = db.SetPIVAsDB(delet.DDrugPlusCondition(listBox1.SelectedValue.ToString()));
                if (f <= 0)
                {
                    MessageBox.Show("删除失败！");
                }
                Datashow();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("没有要删除的附加条件");
            }
           
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Btn_Add.Text = "添加";
            Btn_Cancel.Enabled = false;
            Txt_DrugPlus.Text = "";
            Txt_DrugPlus.Tag = "";
        }

        private void Btn_Update_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                Btn_Add.Text = "保存";
                Btn_Cancel.Enabled = true;
                Txt_DrugPlus.Text = listBox1.Text;
                Txt_DrugPlus.Tag = listBox1.SelectedValue.ToString();
            }
        }
    }
}
