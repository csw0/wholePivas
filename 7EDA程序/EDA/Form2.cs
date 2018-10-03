using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace EDA
{
    public partial class Form2 : Form
    {
        //属性
        #region

        EDA.WebReference.Service1 ser = new EDA.WebReference.Service1();
        private string CheckKind = string.Empty;
        private string DemployeeID = string.Empty;
        private string resault = string.Empty;
        private string SerWay = string.Empty;

        #endregion

        // 事件
        #region

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(string demployeeid,string checkname,string serway)
        {
            InitializeComponent();
            this.DemployeeID = demployeeid;
            this.CheckKind = checkname;
            this.SerWay = serway;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
           ser = new EDA.WebReference.Service1(SerWay);//实例化新服务器地址的WebService接口
        }

        private void button1_Click(object sender, EventArgs e)//Go
        {
            check();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            formClear();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Dispose();
               // this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            check();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                check();
            }
        }

        #endregion 


        //方法
        #region

        private void formload()
        {
            panel3.Visible = true;
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            dt = JsonToDataTable(ser.LableInfor(textBox1.Text,""));
            dt1 = JsonToDataTable(ser.DrugInfor(textBox1.Text));
            labDwardBed.Text = dt.Rows[0]["WardName"].ToString() + "/" + dt.Rows[0]["BedNo"].ToString();
            labName.Text = dt.Rows[0]["PatName"].ToString();
            labBatch.Text = dt.Rows[0]["Batch"].ToString();
            labAge.Text = dt.Rows[0]["Age"].ToString() + dt.Rows[0]["AgeSTR"].ToString();
            labLL.Text = dt.Rows[0]["Batch"].ToString().Contains("L") ? "临" : "长";
            labUse.Text = dt.Rows[0]["UsageName"].ToString();


            int m = 0;
            listView1.Items.Clear();
            if (dt1!=null&&dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {

                    ListViewItem list = new ListViewItem();

                    list.Text = dt1.Rows[i][1].ToString();
                    list.SubItems.Add(dt1.Rows[i][2].ToString());
                    list.SubItems.Add(dt1.Rows[i][3].ToString() + dt1.Rows[i][4].ToString());
                    listView1.Items.Add(list);
                    m = m + Convert.ToInt32(dt1.Rows[i][5].ToString());
                }
            }
            textBox2.Text = m.ToString();


        }

        public static DataTable JsonToDataTable(string strJson)//Json装DT
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
                        if (strRows[r].ToString().Contains("##$$")) 
                        {
                            strRows[r] = strRows[r].ToString().Replace("##$$", "[");
                        }
                        if (strRows[r].ToString().Contains("$$##"))
                        {
                            strRows[r] = strRows[r].ToString().Replace("$$##", "]");
                        }
                        dr[r] = strRows[r].Split(':')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                    }
                    tb.Rows.Add(dr);
                    tb.AcceptChanges();

                }
            }
            return tb;
        }

        private void formClear()//清空
        {
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            panel3.Visible = false;
            listView1.Items.Clear();
            label5.Text = "请扫描";
        }

        private void check()
        {
            DataTable dt = new DataTable();

            switch (CheckKind)
            {
                case "IVRecord_YS_ZJG": resault = ser.Rongji(DemployeeID, textBox1.Text); this.Text = "核对";
                    break;
                case "IVRecord_YP_ZJG": resault = ser.RongMei(DemployeeID, textBox1.Text); this.Text = "核对";
                    break;
                case "IVRecord_PY": resault = ser.PaiYao(DemployeeID, textBox1.Text); this.Text = "核对";
                    break;
                case "IVRecord_JC": resault = ser.JinCang(DemployeeID, textBox1.Text); this.Text = "核对";
                    break;
                case "IVRecord_PZ": resault = ser.PeiZhi(DemployeeID, textBox1.Text); this.Text = "核对";
                    break;
                case "IVRecord_CC": resault = ser.ChuCang(DemployeeID, textBox1.Text); this.Text = "核对";
                    break;
                case "IVRecord_DB": resault = ser.DaBao(DemployeeID, textBox1.Text); this.Text = "核对";
                    break;
                case "IVRecord_QS":  resault =ser.Qianshou (DemployeeID, textBox1.Text); this.Text = "核对";
                    break;
                case "": resault = ser.PeiZhi(DemployeeID, textBox1.Text); this.Text = "退药查询";
                    break;
    
            }
            if (resault == "非当天瓶签或瓶签不存在！")//配置核对：当天提示
            {
                MessageBox.Show(resault);
                return;
            }
            label5.Text = resault;
            if (resault != "不可核对" && resault != "瓶签不能为空")
            {
                dt = JsonToDataTable(ser.AllDrugNum(DemployeeID, CheckKind));
                int c = 0;
                textBox3.Text = dt.Rows.Count.ToString();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    c = c + Convert.ToInt32(dt.Rows[i][1].ToString());
                }
                textBox4.Text = c.ToString();


                formload();
            }
            textBox1.SelectAll();
        }

        #endregion 













    }
}