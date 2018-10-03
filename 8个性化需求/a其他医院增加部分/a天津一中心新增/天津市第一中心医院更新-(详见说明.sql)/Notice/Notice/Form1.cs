using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PIVAsDBhelp;

namespace Notice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            ShowDrugInfo();
        }

        DB_Help db = new DB_Help();
        string str = "";

        private void textBox1_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
        }

        private void ShowDrugInfo()
        {
            this.dataGridView1.DataSource = getCount(str).Tables[0];
        }

        private void setColumns()
        {
            db.GetPIVAsDB("if not exists(select * from syscolumns where id=object_id('DDrug') and name='isStop') " + "\r\n" +
                                  "ALTER TABLE DDrug ADD isStop bit DEFAULT 0 " + "\r\n" +
                                  "GO " + "\r\n" +
                                  "UPDATE DDrug SET isStop = 0 WHERE isStop IS NULL ");
        }

        private void creatTables()
        {
            db.GetPIVAsDB("IF NOT EXISTS(SELECT name FROM sysobjects WHERE name = 'Notice' AND type = 'U') " + "\r\n" +
                          "   CREATE TABLE Notice (DrugCode varchar(50), DrugName varchar(100), Discription varchar(500)) " + "\r\n" +
                          "GO");
        }

        private DataSet getCount(string code)
        {
            DataSet ds = null;
            try 
            {
                if (code == "")
                    ds = db.GetPIVAsDB("SELECT DrugCode AS [药品编码], DrugName AS [药品名称], Spec AS [规格], firm_id AS [厂家], isStop AS [是否停止] FROM DDrug ");
                else
                    ds = db.GetPIVAsDB("SELECT DrugCode AS [药品编码], DrugName AS [药品名称], Spec AS [规格], firm_id AS [厂家], isStop AS [是否停止] FROM DDrug WHERE DrugCode LIKE '%" + code + "%' OR DrugName LIKE '%" + code + "%' OR SpellCode LIKE '%" + code + "%'");
            }
            catch (Exception e)
            { }

            return ds;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.textBox1.ForeColor = Color.Black;
            str = this.textBox1.Text;
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Refresh();
            this.dataGridView1.DataSource = getCount(str).Tables[0];
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string i = dataGridView1[4, e.RowIndex].Value.ToString();
            if ("药品名称/药品编码/拼音码" == this.textBox1.Text)
                str = "";
            else
                str = this.textBox1.Text;
            string code = dataGridView1[0, e.RowIndex].Value.ToString();
            if ("True" == i)
                db.GetPIVAsDB("UPDATE DDrug SET isStop = 0 WHERE DrugCode = '" + code + "'");
            else
                db.GetPIVAsDB("UPDATE DDrug SET isStop = 1 WHERE DrugCode = '" + code + "'");
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Refresh();
            this.dataGridView1.DataSource = getCount(str).Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrugChange drugChange = new DrugChange();
            drugChange.Show();
        }
    }
}
