using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace EDA
{
    public partial class Form6 : Form
    {
        //属性
        #region
        EDA.WebReference.Service1 ser = new EDA.WebReference.Service1();
        private string DemployeeID = string.Empty;
        private string SerWay = string.Empty;
        #endregion 

        //事件
        #region
        public Form6()
        {
            InitializeComponent();
        }

        public Form6(string demployeeid,string serway)
        {
            InitializeComponent();
            this.SerWay = serway;//传输WebService地址
            this.DemployeeID = demployeeid;//记录人ID
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            ser = new EDA.WebReference.Service1(SerWay);//重新实例化WebService，使用传入的WebService地址
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            ShowListView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowListView();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ShowListView();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            listView2.Items.Clear();
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            DataTable dt = JsonToDataTable(ser.SelectStepDetail(listView1.Items[Convert.ToInt32(listView1.SelectedIndices[0].ToString())].Text));
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                ListViewItem list = new ListViewItem();

                list.Text = dt.Rows[i]["Step"].ToString();
                list.SubItems.Add(dt.Rows[i]["DEmployeeName"].ToString());
                string s = dt.Rows[i]["DT"].ToString();

                string d = s==string .Empty?"": s.Substring(0, 4).ToString() + "/" + s.Substring(4, 2).ToString() + "/" + s.Substring(6, 2).ToString() + " " + s.Substring(8, 2).ToString() + ":" + s.Substring(10, 2).ToString() + ":" + s.Substring(12, 2).ToString();
                //MessageBox.Show(d);
                list.SubItems.Add(d);
                list.SubItems.Add(dt.Rows[i]["ivstuts"].ToString());
                list.SubItems.Add(dt.Rows[i]["DEmployeeID"].ToString());
                listView2.Items.Add(list);
            }
            
        }

        private void listView2_ItemActivate(object sender, EventArgs e)
        {
            panel2.Visible = true;
            label2.Text = "瓶签号：" + listView1.Items[Convert.ToInt32(listView1.SelectedIndices[0].ToString())].Text;
            label3.Text = "差错步骤：" + listView2.Items[Convert.ToInt32(listView2.SelectedIndices[0].ToString())].Text;
            label4.Text = "执行人：" + listView2.Items[Convert.ToInt32(listView2.SelectedIndices[0].ToString())].SubItems[1].Text;
            label5.Text = "执行时间：" + listView2.Items[Convert.ToInt32(listView2.SelectedIndices[0].ToString())].SubItems[2].Text;
            string c = string.Empty;
            if (listView2.Items[Convert.ToInt32(listView2.SelectedIndices[0].ToString())].Text == "溶剂" || listView2.Items[Convert.ToInt32(listView2.SelectedIndices[0].ToString())].Text == "溶媒")
            {
                c = "摆药";
            }
            else
            {
                c = listView2.Items[Convert.ToInt32(listView2.SelectedIndices[0].ToString())].Text;
            }
            DataTable dt = JsonToDataTable(ser.ReturnErrorType(c));
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "TypeName";
            comboBox1.ValueMember = "TypeCode";
            if (label4.Text == "执行人：" || label5.Text == "执行时间：")
            {
                button2.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                return;
            }
            string s = ser.AddErrorRecord(listView1.Items[Convert.ToInt32(listView1.SelectedIndices[0].ToString())].Text
                , listView2.Items[Convert.ToInt32(listView2.SelectedIndices[0].ToString())].SubItems[3].Text
                , listView2.Items[Convert.ToInt32(listView2.SelectedIndices[0].ToString())].SubItems[4].Text
                , DemployeeID, listView2.Items[Convert.ToInt32(listView2.SelectedIndices[0].ToString())].SubItems[2].Text
                , comboBox1.SelectedValue.ToString());

            if (s == "true")
            {
                MessageBox.Show("保存成功");
                panel2.Visible = false;
            }
            else
            {
                MessageBox.Show("失败惹，失败信息：" + s);
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion



        //方法
        #region
        /// <summary>
        /// Json转DT
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static DataTable JsonToDataTable(string strJson)
        {
            DataTable tb = new DataTable();
            if (strJson != null && strJson != "")
            {
                //取出表名               
                var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
                string strName = rg.Match(strJson).Value;
                tb = null;
                //去除表名               
                strJson = strJson.Substring(strJson.IndexOf("[") + 1);
                strJson = strJson.Substring(0, strJson.IndexOf("]"));
                //获取数据               
                rg = new Regex(@"(?<={)[^}]+(?=})");
                MatchCollection mc = rg.Matches(strJson);
                for (int i = 0; i < mc.Count; i++)
                {
                    string strRow = mc[i].Value;
                    string[] strRows = strRow.Split(',');
                    //创建表  

                    if (tb == null)
                    {
                        tb = new DataTable();
                        tb.TableName = strName;
                        foreach (string str in strRows)
                        {
                            var dc = new DataColumn();
                            string[] strCell = str.Split(':');
                            string a = strCell[0].TrimStart('"');
                            string b = a.TrimEnd('"');
                            dc.ColumnName = b;
                            tb.Columns.Add(dc);
                        }
                        tb.AcceptChanges();
                    }
                    //增加内容                  
                    DataRow dr = tb.NewRow();
                    for (int r = 0; r < strRows.Length; r++)
                    {
                        dr[r] = strRows[r].Split(':')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                    }
                    tb.Rows.Add(dr);
                    tb.AcceptChanges();

                }
            }
            else
            {
                MessageBox.Show("不存在此遗嘱号/（总）瓶签号!");
            }
            return tb;
        }

        /// <summary>
        /// 加载listview信息
        /// </summary>
        private void ShowListView()
        {
            if (textBox1.Text != string.Empty)
            {
                listView1.Items.Clear();
                DataTable dt = JsonToDataTable(ser.LableInfor(textBox1.Text,""));
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        ListViewItem list = new ListViewItem();

                        list.Text = dt.Rows[i]["LabelNo"].ToString();
                        listView1.Items.Add(list);
                    }
                }
            }
        }

        #endregion








    }
}