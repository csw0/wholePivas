using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PIVAsDBhelp;

namespace DrugListConform
{
    public partial class ChangeTime : Form
    {
        DB_Help db = new DB_Help();
        SQL Mysql = new SQL();
        private string groupNo = string.Empty;
        private string recipeID = string.Empty;
        public ChangeTime(string groupno,string recipeid)
        {
            InitializeComponent();
            this.groupNo = groupno;
            this.recipeID = recipeid;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db.SetPIVAsDB(Mysql.UpdateTime(groupNo,recipeID,dateTimePicker2.Value.ToString()));
            this.DialogResult = DialogResult.Yes;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
