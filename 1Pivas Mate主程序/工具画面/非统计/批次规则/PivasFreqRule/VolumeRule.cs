using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using PIVAsCommon.Helper;

namespace PivasFreqRule
{
    public partial class VolumeRule : UserControl
    {
        DB_Help DB = new DB_Help();
        DataSet DS = new DataSet();
        DataTable DT = new DataTable();
        public static string code;
        
        public VolumeRule()
        {
            InitializeComponent();
        }

        private void VolumeRule_Load(object sender, EventArgs e)
        {
            ShowWard();
            if (DT.Rows.Count > 0)
            {
                pnlWard.Controls[0].Focus();
                code = DT.Rows[0]["WardCode"].ToString();
                label2.Text = DT.Rows[0]["WardName"].ToString();
                ShowLimit(code);
            }
        }


        /// <summary>
        /// 加载所有病区的方法
        /// </summary>
        private void ShowWard()
        {
            string str = "select WardCode, WardName From DWard where IsOpen = 1";
            try
            {
                DS = DB.GetPIVAsDB(str);
                DT = DS.Tables[0];
                //string aaa = DT.Rows[0]["WardName"].ToString();
                pnlWard.Controls.Clear();
                for (int i = 0; i < DT.Rows.Count;i++ )
                {
                    DWard Ward = new DWard(DT.Rows[i]["WardCode"].ToString(), pnlWard);
                    Ward.Show(DT.Rows[i]);
                    Ward.Top = i * 30;
                    //默认选中第一行数据
                    if (i == 0)
                    {
                        Ward.BackColor = Color.FromArgb(140, 140, 255);
                    }
                    pnlWard.Controls.Add(Ward);
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }


        public void ShowLimit(string WardCode)
        {
            code = WardCode;//部门编号
            string str = "SELECT MPRule2ID,OrderID,LowMetric,HighMetric from OrderMCPRule2 WHERE WardCode ='" + WardCode + "' Order BY OrderID";
            try
            {
                pnlLimit.Controls.Clear();
                DS = DB.GetPIVAsDB(str);
                for (int i = 0; i < DS.Tables[0].Rows.Count;i++ )
                {
                    VolumeLimit limit = new VolumeLimit();
                    limit.Show(DS.Tables[0].Rows[i]);
                    pnlLimit.Controls.Add(limit);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtOrder1 = new DataTable();
                DataTable dtOrder2 = new DataTable();
                
                string s,str1,strcode;
                int a;
                string str = "select OrderID from DOrder ORDER BY OrderID";
                dtOrder1 = DB.GetPIVAsDB(str).Tables[0];


                pBar.Maximum = DT.Rows.Count;
                pBar.Visible = true;
                for (int j = 0; j < DT.Rows.Count; j++)
                {
                    strcode = DT.Rows[j]["WardCode"].ToString();
                    str1 = "select OrderID from OrderMCPRule2 where WardCode ='" + strcode + "' ORDER BY OrderID";
                    dtOrder2 = DB.GetPIVAsDB(str1).Tables[0];

                    for (int i = 0; i < dtOrder1.Rows.Count;i++ )
                    {
                        a = Convert.ToInt32(dtOrder1.Rows[i]["OrderID"].ToString());
                        DataRow[] R = dtOrder2.Select("OrderID = " + dtOrder1.Rows[i]["OrderID"].ToString()+"");
                        if(R.Length==0)
                        {
                            s = "insert into OrderMCPRule2 values('" + Convert.ToString(a) + "',500,1000,'" + Convert.ToString(a + 1) + "','" + strcode + "')";
                            DB.SetPIVAsDB(s);
                        }
                    }
                    for (int i = 0; i < dtOrder2.Rows.Count;i++ )
                    {
                        DataRow[] R = dtOrder1.Select("OrderID = " + dtOrder2.Rows[i]["OrderID"].ToString()+"");
                        if (R.Length==0)
                        {
                            s = "delete from OrderMCPRule2 where WardCode = '" + strcode + "' and OrderID = " + dtOrder2.Rows[i]["OrderID"].ToString();
                            DB.SetPIVAsDB(s);
                        }
                    }  
                    pBar.Value=j;                                       
                }
                ShowLimit(code);
                pBar.Visible = false;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void But_UseAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确认应用到全部病区？", "确认", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    return;
                DataTable dt = DB.GetPIVAsDB("select distinct WardCode From DWard where IsOpen = 1 and WardCode<>'" + code + "'   ").Tables[0];
                if (dt.Rows.Count <= 0)
                    return;
                if (!Check_OnlyInse.Checked)
                {
                    DB.SetPIVAsDB("delete OrderMCPRule2 where WardCode<>'" + code + "'  ");
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DB.SetPIVAsDB("insert into OrderMCPRule2 select OrderID,LowMetric,HighMetric,ToOrderID,'" + dt.Rows[i][0].ToString() + "' from OrderMCPRule2 where WardCode= '" + code+"'  ");
                }
            }
            catch { }
        }

 


    }
}
