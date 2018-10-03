using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace EDA
{
    public partial class Menu : Form
    {

        //属性
        #region

        string EmployeeID;
        string ServiceWay = string.Empty;
        EDA.Set set = new Set();

        #endregion

        //事件
        #region
        public Menu()
        {
            InitializeComponent();
        }

        public Menu(string EmployeeID)
        {
            InitializeComponent();
            this.EmployeeID = EmployeeID;
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            //ServiceWay = set.setWay()==string .Empty?"http://192.168.12.88:8081/Service1.asmx":set.setWay();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form1 f = new Form1("IVRecord_YS_ZJG", Serway());
            f.Text = "溶媒核对";
            f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1("IVRecord_YP_ZJG", Serway());
            f.Text = "溶媒核对";
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1("IVRecord_PY", Serway());
            f.Text = "排药核对";
            f.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1("IVRecord_JC", Serway());
            f.Text = "进仓核对";
            f.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1("IVRecord_PZ", Serway());
            f.Text = "配置核对";
            f.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1("IVRecord_QS", Serway());
            f.Text = "病区签收";
            f.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1("IVRecord_DB", Serway());
            f.Text = "打包核对";
            f.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1("", Serway());
            f.Text = "退药查询";
            f.ShowDialog();
        }


        private void button9_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3(EmployeeID, Serway());
            f.Text = "瓶签查询";
            f.ShowDialog();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {

            Set s = new Set();
            s.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4(Serway());
            f.Text = "汇总查询";
            f.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1("Error", Serway());
            f.Text = "差错记录";
            f.ShowDialog();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #endregion

        //方法
        #region

        private string Serway()
        {
            return set.setWay() == string.Empty ? "http://192.168.12.88:8081/Service1.asmx" : set.setWay();
        }

        #endregion





    }
}