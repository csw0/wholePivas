using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DrugFlowStatic
{
    public partial class Form1 : Form
    {
        DataBase db = new DataBase();

        public Form1()
        {
            InitializeComponent();
            if (this.textBox1.Text == "药品名称/药品编码/拼音缩写")
            {
                this.textBox1.ForeColor = Color.Gray;
            }
            showCheck();
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (0 != e.Index)
                return;
            for (int i = 1; i < this.checkedListBox1.Items.Count; i++)
            {
                this.checkedListBox1.SetItemChecked(i, (CheckState.Checked != e.CurrentValue));
            }
        }

        private void checkedListBox2_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (0 != e.Index)
                return;
            for (int i = 1; i < this.checkedListBox2.Items.Count; i++)
            {
                this.checkedListBox2.SetItemChecked(i, (CheckState.Checked != e.CurrentValue));
            }
        }

        private void showCheck()
        {
            this.checkedListBox1.Items.Clear();
            this.checkedListBox2.Items.Clear();
            DataSet ds1 = null;
            this.checkedListBox1.Items.Add("全选/反选");
            this.checkedListBox1.SetItemChecked(0, true);
            ds1 = db.getWard();
            if (ds1 != null || ds1.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    this.checkedListBox1.Items.Add(ds1.Tables[0].Rows[i]["WardName"].ToString());
                    this.checkedListBox1.SetItemChecked(i + 1, true);
                }
            }
            this.checkedListBox2.Items.Add("全选/反选");
            this.checkedListBox2.SetItemChecked(0, true);
            DataSet ds2 = null;
            ds2 = db.getDrug();
            if (ds2 != null || ds2.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                {
                    this.checkedListBox2.Items.Add(ds2.Tables[0].Rows[i]["DrugName"].ToString());
                    this.checkedListBox2.SetItemChecked(i + 1, true);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text == "药品名称/药品编码/拼音缩写")
            {
                this.textBox1.Text = "";
            }
        }
    }
}
