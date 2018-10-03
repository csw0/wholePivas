using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MistakeCollection
{
    public partial class Form1 : Form
    {
        private Dao.sql sel = new MistakeCollection.Dao.sql();
        string labelno1="";
        string labelno2="";

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        #region 方法
        /// <summary>
        /// 统计方法
        /// </summary>
        private int CollectMode()
        {
            int m=0;
            if (按瓶签时间ToolStripMenuItem.Checked)
            {
                m = 1;
            }
            else if (按差错人ToolStripMenuItem.Checked)
            {
                m = 2;
            }
            else if (按记录人ToolStripMenuItem.Checked)
            {
                m = 3;
            }
            else
            {
                m = 0;
            }
            return m;  
        }

        private void getLabelnos()
        {
            labelno1 = dateTimePicker1.Value.Year.ToString();
            if (dateTimePicker1.Value.Month < 10)
            {
                labelno1 += "0";
            }
            labelno1 += dateTimePicker1.Value.Month.ToString();
            if (dateTimePicker1.Value.Day < 10)
            {
                labelno1 += "0";

            }
            labelno1 += dateTimePicker1.Value.Day.ToString() + "100001";

            labelno2 = dateTimePicker2.Value.Year.ToString();
            if (dateTimePicker2.Value.Month < 10)
            {
                labelno2 += "0";
            }
            labelno2 += dateTimePicker2.Value.Month.ToString();
            if (dateTimePicker2.Value.Day < 10)
            {
                labelno2 += "0";

            }
            labelno2 += dateTimePicker2.Value.AddDays(1).Day.ToString() + "100001";

        }
        #endregion



        #region 事件
        
        private void 按瓶签时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            按记录人ToolStripMenuItem.Checked = 按差错人ToolStripMenuItem.Checked = false;
        }
     
        private void 按记录人ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            按瓶签时间ToolStripMenuItem.Checked = 按差错人ToolStripMenuItem.Checked = false;
        }

        private void 按差错人ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            按瓶签时间ToolStripMenuItem.Checked = 按记录人ToolStripMenuItem.Checked = false;
        }

    
        /// <summary>
        /// 统计按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            DataSet DS = new DataSet();
            listView1.Clear();
            dataGridView1.DataSource = null;
            try
            {
                getLabelnos();
                switch (CollectMode())
                {

                    case 1: DS = sel.CountByDate(labelno1, labelno2); break;
                    case 2: DS = sel.CountByErrorID(labelno1, labelno2); break;
                    case 3: DS = sel.CountByFindID(labelno1, labelno2); break;
                    //default: DS = sel.MistakeDetails(labelno1, labelno2); break;
                    default: DS = null; break ;
                }
                if (DS == null) return;
                listView1.Columns.Add(DS.Tables[0].Columns[0].ColumnName,100,HorizontalAlignment.Left);
                listView1.Columns.Add(DS.Tables[0].Columns[1].ColumnName,90,HorizontalAlignment.Right);

                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    ListViewItem l = new ListViewItem();
                    l.Text = DS.Tables[0].Rows[i][0].ToString();
                    l.SubItems.Add(DS.Tables[0].Rows[i][1].ToString());
                    listView1.Items.Add(l);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("加载结果出错：", ex);
            }

            

        }

        private void listView1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            DataSet ds=new DataSet ();
            if (按瓶签时间ToolStripMenuItem.Checked)
                ds = sel.MistakeDetails(labelno1, labelno2,listView1.SelectedItems[0].Text,"","");
            else if (按差错人ToolStripMenuItem.Checked)
                ds = sel.MistakeDetails(labelno1, labelno2,"",listView1.SelectedItems[0].Text,"");
            else if (按记录人ToolStripMenuItem.Checked)
                ds = sel.MistakeDetails(labelno1, labelno2,"","",listView1.SelectedItems[0].Text);
            else
                ds = sel.MistakeDetails(labelno1, labelno2,"","","");

            dataGridView1.DataSource = ds.Tables[0];
        }

        #endregion






    }
}
