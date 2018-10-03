using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;


namespace EDA
{
    public partial class Set : Form
    {
        //属性
        #region
        string con = string.Empty;
        XmlDocument xml = new XmlDocument();

        #endregion 

        //事件
        #region

        public Set()
        {
            InitializeComponent();
        }

        public  void Set_Load(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = setWay();
                
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                xml.RemoveAll();

                XmlNode node = xml.CreateElement("Database");
                xml.AppendChild(node);

                XmlElement el1 = xml.CreateElement("DataSource");
                el1.InnerText = textBox1.Text;
                node.AppendChild(el1);

                xml.Save("IMEQPIVAs.xml");
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != string.Empty && textBox1.Text != "")
                {
                    EDA.WebReference.Service1 ser = new EDA.WebReference.Service1(textBox1.Text);
                    DataTable dt = JsonToDataTable(ser.Login("9999", ""));
                    if (dt.Rows[0][0].ToString() == "2")
                    { label2.Text = "测试成功"; label2.ForeColor = Color.Green; }
                    else
                    { label2.Text = "测试失败"; label2.ForeColor = Color.Red; }
                }
                else
                {
                    label2.Text = "别闹了，空的你也测试..."; label2.ForeColor = Color.Pink;
                }
            }
            catch (Exception ex)
            {
                label2.Text = "太坏了，无效的URL！←_←"; label2.ForeColor = Color.Yellow;
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        #endregion

        //方法
        #region

        public static DataTable JsonToDataTable(string strJson)
        {
            //取出表名               
            var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
            string strName = rg.Match(strJson).Value;
            DataTable tb = null;
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
            return tb;
        }

        public string setWay()
        {
            string con = string.Empty;
            try
            {
   
                xml.Load("IMEQPIVAs.xml");
                XmlNode node = xml.SelectSingleNode("Database");
                con = node.SelectSingleNode("DataSource").InnerText;
                return con;
            }
            catch (Exception ex)
            {
                return con;
            }
        }

        #endregion

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label2.Text = "";
        }


    }
}