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
    public partial class Form4 : Form
    {
        //属性
        #region

        EDA.WebReference.Service1 ser = new EDA.WebReference.Service1();
        private string demployeeid = string.Empty;
        private string SerWay = string.Empty;
        private string checkpro = string.Empty;
        #endregion

        //事件
        #region

        public Form4()
        {
            InitializeComponent();
        }

        public Form4(string serway)
        {
            InitializeComponent();
            this.SerWay = serway;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            ser = new EDA.WebReference.Service1(SerWay);
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            DataTable dt = new DataTable();
            
            int[] c=new int [3]{0,0,0};

            if (comboBox1.Text == string.Empty) 
            { MessageBox.Show("请选择核对类型"); return; }
            switch (comboBox1.SelectedItem.ToString())
            {
                case "溶剂汇总": checkpro = "IVRecord_YS_ZJG"; break;
                case "溶媒汇总": checkpro = "IVRecord_YP_ZJG"; break;
                case "排药汇总": checkpro = "IVRecord_PY"; break;
                case "进仓汇总": checkpro = "IVRecord_JC"; break;
                case "配置汇总": checkpro = "IVRecord_PZ"; break;
                case "出仓汇总": checkpro = "IVRecord_CC"; break;
                case "打包汇总": checkpro = "IVRecord_DB"; break;
                
            }
            dt = JsonToDataTable(ser.DwardInfor("9118", checkpro, dateTimePicker1.Text));
            if (dt.Rows.Count > 0) 
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    ListViewItem list = new ListViewItem();

                    list.Text = dt.Rows[i]["病区"].ToString();
                    list.SubItems.Add(dt.Rows[i]["总数"].ToString());
                    list.SubItems.Add(dt.Rows[i]["已核对"].ToString());
                    list.SubItems.Add(dt.Rows[i]["未核对"].ToString());
                    listView1.Items.Add(list);

                    c[0] = c[0] + Convert.ToInt32(dt.Rows[i]["总数"].ToString());
                    c[1] = c[1] + Convert.ToInt32(dt.Rows[i]["已核对"].ToString());
                    c[2] = c[2] + Convert.ToInt32(dt.Rows[i]["未核对"].ToString());
                }
            }
            ListViewItem Count = new ListViewItem();

            Count.Text = "总计";
            Count.SubItems.Add(c[0].ToString());
            Count.SubItems.Add(c[1].ToString());
            Count.SubItems.Add(c[2].ToString());
            listView1.Items.Add(Count);

           
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            if (checkpro != string.Empty)
            {
                Form5 f = new Form5(checkpro, listView1.Items[Convert.ToInt32(listView1.SelectedIndices[0].ToString())].Text, dateTimePicker1.Text,SerWay);
                f.Text = "病区";
                f.ShowDialog();
            }

        }

        #endregion

        //方法
        #region


        /// <summary>
        /// Json转换DT
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
            return tb;

        }

        #endregion


    }
}