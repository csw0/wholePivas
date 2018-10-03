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
    public partial class Form3 : Form
    {
        //属性
        #region


        EDA.WebReference.Service1 ser = new EDA.WebReference.Service1();
        private string DemployeeID = string.Empty;
        private string SerWay = string.Empty;

        #endregion

        //事件
        #region

        public Form3()
        {
            InitializeComponent();
        }

        public Form3(string labelno,string serway)
        {
            InitializeComponent();
            this.SerWay = serway;
            textBox1.Text = labelno;
        }

        public Form3(string serway)
        {
            InitializeComponent();
            //this.DemployeeID = demployeeid;
            this.SerWay = serway;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            ser = new EDA.WebReference.Service1(SerWay);
            lsv_lord();//判断是程序调用还是手动输入
            DWard_lord();//病区加载
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ShowListview();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            button1_Click_1(sender, e);
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ShowListview();
            }
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
                MessageBox.Show("信息不存在");
            }
            return tb;
        }

        /// <summary>
        /// 返回ivstatus
        /// </summary>
        /// <param name="iv"></param>
        /// <returns></returns>
        private string ivstatus(string iv)
        {
            string ivs = string.Empty;
            switch (iv)
            {
                case "3": ivs = "已打印"; break;
                case "4": ivs = "已摆药"; break;
                case "5": ivs = "已排药"; break;
                case "7": ivs = "已进仓"; break;
                case "9": ivs = "已配置"; break;
                case "11": ivs = "已出仓"; break;
                case "13": ivs = "已打包"; break;
                case "15": ivs = "已签收"; break;
                default: ivs = "未知"; break;
            }
            return ivs;
        }

        /// <summary>
        /// 显示ListView
        /// </summary>
        public void ShowListview()
        {
            listView1.Items.Clear();
            string cond = string.Empty;
            if (textBox2.Text != string.Empty && textBox2.Text != "")
            {
                cond += "and iv.PatName ='" + textBox2.Text + "'";
            }
            if (comboBox1.Text != string.Empty && comboBox1.Text != "" && comboBox1.Text != "全部")
            {
                cond = cond + "  and d.WardCode ='" + comboBox1.SelectedValue + "'";
            }
            DataTable dt = JsonToDataTable(ser.LableInfor(textBox1.Text,cond ));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    ListViewItem list = new ListViewItem();

                    list.Text = dt.Rows[i]["LabelNo"].ToString();
                    list.SubItems.Add(ivstatus((dt.Rows[i]["ivstatus"].ToString())));
                    list.SubItems.Add(dt.Rows[i]["WardName"].ToString());
                    list.SubItems.Add(dt.Rows[i]["Batch"].ToString());
                    list.SubItems.Add(dt.Rows[i]["BedNo"].ToString());
                    list.SubItems.Add(dt.Rows[i]["PatName"].ToString());
                    listView1.Items.Add(list);
                }
            }
        }

        /// <summary>
        /// 调用时自动加载数据
        /// </summary>
        private void lsv_lord()
        {
            if (textBox1.Text != string.Empty)
            {
                ShowListview();
            }
        }

        /// <summary>
        /// 加载病区
        /// </summary>
        private void DWard_lord()
        {
            DataTable dt = JsonToDataTable(ser.ShowDward());
            if (dt.Rows.Count > 0)
            {
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "WardName";
                comboBox1.ValueMember = "WardCode";
            }
        }

        #endregion






 
    }
}