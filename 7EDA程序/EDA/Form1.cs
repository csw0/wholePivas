using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using EDA.WebReference;
using System.Text.RegularExpressions;





namespace EDA
{
    public partial class Form1 : Form
    {
        //属性
        #region
 
        EDA.WebReference.Service1 ser = new EDA.WebReference.Service1();
        private string CheckPro = string.Empty;
        private string SerWay = string.Empty;

        #endregion


        //事件
        #region

        public Form1()
        {
            InitializeComponent();
        }

        public Form1(string checkpro,string serway)
        {
            InitializeComponent();
            this.CheckPro = checkpro;
            this.SerWay = serway;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ser = new Service1(SerWay);
            textBox1.Focus();    
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            this.Dispose();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)13)
            {
                menuItem1_Click(sender, e);
            }


        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Trim() == "")
                {
                    MessageBox.Show("请输入用户名");
                    return;
                }
                string rel =ser.Login(textBox1.Text, textBox2.Text);
                DataTable dt = JsonToDataTable(rel);
                switch (dt.Rows[0][0].ToString())
                {
                    case "0": label3.Text = "请输入用户名"; label3.ForeColor = Color.Red; textBox1.Text = textBox2.Text = string.Empty;
                        break;
                    case "1": label3.Text = "登录失败，输入账号或密码错误"; label3.ForeColor = Color.Red; textBox1.Text = textBox2.Text = string.Empty;
                        break;
                    case "2":
                        {
                            if (CheckPro == "Error")
                            {
                                Form6 f = new Form6(dt.Rows[0][1].ToString(), SerWay);
                                f.ShowDialog();
                            }
                            else
                            {
                                Form2 f = new Form2(dt.Rows[0][1].ToString(), CheckPro, SerWay);
                                f.Text = this.Text;
                                f.ShowDialog();
                            }
                            this.Close();
                        }
                        break;
                    case "3": label3.Text = "连接失败"; label3.ForeColor = Color.Red; textBox1.Text = textBox2.Text = string.Empty;
                        break;
                    case "4": label3.Text = "二维码已删除"; label3.ForeColor = Color.Red; textBox1.Text = textBox2.Text = string.Empty;
                        break;
                }
            }
            catch (Exception ex)
            { //MessageBox.Show(ex.Message);
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion



        //方法
        #region

        /// <summary>  根据Json返回DateTable,JSON数据格式如:           
        /// {table:[{column1:1,column2:2,column3:3},{column1:1,column2:2,column3:3}]}           
        /// </summary>           
        /// <param name="strJson">Json字符串</param>           
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