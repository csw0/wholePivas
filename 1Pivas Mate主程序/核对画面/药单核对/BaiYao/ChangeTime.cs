using PIVAsCommon.Helper;
using System;
using System.Windows.Forms;

namespace BaiYaoCheck
{
    public partial class ChangeTime : Form
    {
        DB_Help db = new DB_Help();
        SQL Mysql = new SQL();
        private string groupNo = string.Empty;
        private string recipeID = string.Empty;
        public ChangeTime(string groupno, string recipeid,string date)
        {
            InitializeComponent();
            this.groupNo = groupno;
            this.recipeID = recipeid;
            this.label3.Text = date;
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db.SetPIVAsDB(Mysql.UpdateTime(groupNo, recipeID, dateTimePicker2.Value.ToString("yyyy-MM-dd ")+comboBox1.SelectedItem.ToString()));
            this.DialogResult = DialogResult.Yes;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
