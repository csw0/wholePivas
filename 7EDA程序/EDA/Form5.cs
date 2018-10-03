using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EDA
{
    public partial class Form5 : Form
    {
        //属性
        #region
        private string WardName = string.Empty;
        private string Date = string.Empty;
        private string CheckPro = string.Empty;
        private string SerWay = string.Empty;
        EDA.WebReference.Service1 ser = new EDA.WebReference.Service1();

        #endregion

        //事件
        #region
        public Form5()
        {
            InitializeComponent();
        }

        public Form5(string checkpro, string wardname, string date, string serway)
        {
            InitializeComponent();
            this.WardName = wardname;
            this.Date = date;
            this.CheckPro = checkpro;
            this.SerWay = serway;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            ser = new EDA.WebReference.Service1(SerWay);
            DataTable dt = new DataTable();
            dt = JsonToDataTable(ser.InforByDward(CheckPro, Date, WardName));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    ListViewItem list = new ListViewItem();

                    list.Text = dt.Rows[i]["WardName"].ToString();
                    list.SubItems.Add(dt.Rows[i]["Batch"].ToString());
                    list.SubItems.Add(dt.Rows[i]["LabelNo"].ToString());
                    listView1.Items.Add(list);

                }
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            //MessageBox.Show(listView1.Items[Convert.ToInt32(listView1.SelectedIndices[0].ToString())].SubItems[2].Text);
            Form3 f = new Form3(listView1.Items[Convert.ToInt32(listView1.SelectedIndices[0].ToString())].SubItems[2].Text, SerWay);
            f.ShowDialog();


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