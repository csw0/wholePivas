using System;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PivasFreqRule
{
    public partial class TimeRule : UserControl
    {
        public TimeRule()
        {
            InitializeComponent();
        }

        DataTable dt=new DataTable();
        DataTable dt1=new DataTable();
        seldb sel=new seldb();
        updatedb update = new updatedb();
        int i = 0;
        string codeid;
        private void TimeRule_Load(object sender, EventArgs e)
        {
            
            panel1.Controls.Clear();
            dt = sel.getTimeRule().Tables[0];
            for ( i = 0; i < dt.Rows.Count;i++ )
            {

                TimeRow time = new TimeRow();
                time.show(dt.Rows[i]);
                time.Name = i.ToString();
                time.Top = 33 * i;
                time.Parent =this;
                panel1.Controls.Add(time); 
                
            }
         
            
        }

        /// <summary>
        /// 增加事件规则
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_add_Click(object sender, EventArgs e)
        {
            CheckTimeRule();
            panel1.VerticalScroll.Value = panel1.VerticalScroll.Minimum;
            if (i < 24)
            {
                DataTable dt1 = new DataTable();
                dt1 = sel.getLaterOrder().Tables[0];
                string OrderID="1";
                string start_time="00:00";
                string end_time = "23:59";
                if (dt1.Rows.Count > 0)
                {
                    OrderID = dt1.Rows[0]["NextOrderID"].ToString();
                    start_time = dt1.Rows[0]["EndTime"].ToString();
                }
                if(DateTime.Compare(DateTime.Parse(end_time),DateTime.Parse(start_time))<0)
                {
                    update.insertTimeRule(OrderID, start_time,end_time,true); 
                }
                else
                {
                    update.insertTimeRule(OrderID, start_time,end_time,false);
                }
                dt = sel.getTimeRule().Tables[0];
                TimeRow time = new TimeRow();
                time.show(dt.Rows[i]);
                time.Name = (i).ToString();
                time.Top = 33 * (i);
                panel1.Controls.Add(time);
                i++;
            }
            else 
            {
                MessageBox.Show("数量超出范围");
            }
        }

  


        /// <summary>
        /// 核对FreqSubRule并更新
        /// </summary>
        /// <param name="i"></param>
        private void checkSubRule(int i)
        {
            dt1 = sel.getCount(dt.Rows[i]["FreqCode"].ToString()).Tables[0];
            int k = Convert.ToInt32(dt.Rows[i]["TimesOfDay"]) - Convert.ToInt32(dt1.Rows[0][0]);
            if (k > 0)
            {
                for (int j = 1; j <= Math.Abs(k); j++)
                {
                    codeid = dt.Rows[i][0].ToString() + (Convert.ToInt32(dt1.Rows[0][0]) + j);
                    update.intsertFreqRule(dt.Rows[i][0].ToString(), codeid);
                }
            }
            else if (k < 0)
            {
                for (int j = 0; j < Math.Abs(k); j++)
                {
                    codeid = dt.Rows[i][0].ToString() + (Convert.ToInt32(dt1.Rows[0][0]) - j);
                    update.deleteFreqRule(codeid);
                }
            }
        }

        private void label_delete_Click(object sender, EventArgs e)
        {
            
            if (i>=1) 
            {
                if (MessageBox.Show(" 确定删除最后一个批次 ？", "删除", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (panel1.Controls[i - 1].Name == (i - 1).ToString())
                    {
                        update.deleteTimeRule(i.ToString());
                        panel1.Controls.Remove(panel1.Controls[i - 1]);
                        i--;
                        
                    }
                }
            }
        }

        private void label_freg_MouseHover(object sender, EventArgs e)
        {
            label_freg.BackColor = Color.FromArgb(57, 125, 243);
        }

        private void label_freg_MouseLeave(object sender, EventArgs e)
        {
            label_freg.BackColor = Color.FromArgb(16, 107, 225);
        }

        private void label_add_MouseHover(object sender, EventArgs e)
        {
            label_add.BackColor = Color.FromArgb(57, 125, 243);
        }

        private void label_add_MouseLeave(object sender, EventArgs e)
        {
            label_add.BackColor = Color.FromArgb(16, 107, 225);
        }

        private void label_delete_MouseHover(object sender, EventArgs e)
        {
            label_delete.BackColor = Color.FromArgb(57, 125, 243);
        }

        private void label_delete_MouseLeave(object sender, EventArgs e)
        {
            label_delete.BackColor = Color.FromArgb(16, 107, 225);
        }


        /// <summary>
        /// 更新批次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_freg_Click(object sender, EventArgs e)
        {
            string ret = CheckTimeRule();
            if (ret != "")
            {
                MessageBox.Show(ret);
                return;
            }
            else 
            {
                foreach (Control c in panel1.Controls)
                {
                    if (c is TimeRow)
                    {
                        TimeRow tr = (TimeRow)c;
                        update.UpdateTimeRule(tr.label1.Text,tr.Start_Time.Text, tr.End_Time.Text, false);
                    }
                }
            }
           
            StringBuilder mrg = new StringBuilder();
            dt = sel.getDFreg().Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                checkSubRule(i);
                string[] time = dt.Rows[i][4].ToString().Split('_');
                if (time.Length > 0)
                {
                    for (int j = 0; j < time.Length; j++)
                    {
                        if (sel.getOrderID(time[j]).Tables[0].Rows.Count > 0)
                        {
                            string order = sel.getOrderID(time[j]).Tables[0].Rows[0][0].ToString();
                            codeid = dt.Rows[i][0].ToString() + (j + 1);
                            update.updateFreqRule(time[j], order, dt.Rows[i][0].ToString(), codeid);
                        }
                        else
                        {
                            mrg.Append("找不到" + time[j] + "的批次" + "\n");
                        }
                    }
                }
            }
            MessageBox.Show("更新成功");
            if (mrg.ToString() != "")
            {
                MessageBox.Show(mrg.ToString(), "请到时间规则维护");
            }

            
        }

        /// <summary>
        /// 判断上一列结束时间是否满足
        /// </summary>
        /// <param name="name">上一列名称</param>
        /// <param name="id">这一列名称</param>
        public void  SelectLastPanel(int name,string id ) 
        {
            bool isbx=false ;
            foreach (Control c in panel1.Controls) 
            {
                if (c.Name == name.ToString())
                {
                    string text = ((TimeRow)c).End_Time.Text;
                    if (DateTime.Compare(DateTime.Parse(text), DateTime.Parse(id))!=0)
                    {
                        ((TimeRow)c).panel2.BackColor = Color.Yellow;
                        isbx = true;
                    }
                    else 
                    {
                        ((TimeRow)c).panel2.BackColor = Color.Transparent;
                        isbx = false;
                    }
                }
                if (c.Name == (name + 1).ToString()) 
                {
                    if (isbx) 
                    {
                        ((TimeRow)c).panel1.BackColor = Color.Yellow;
                        
                    }
                    else
                    {
                        ((TimeRow)c).panel1.BackColor = Color.Transparent;
                        
                    }
                }
            }
            
        }
        /// <summary>
        /// 判断下一列起始时间是否满足
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        public void SelectNextPanel(int name, string id) 
        {
            bool isbx = false;
            foreach (Control c in panel1.Controls)
            {
                if (c.Name == name.ToString())
                {
                    string text = ((TimeRow)c).Start_Time.Text;
                    if (DateTime.Compare(DateTime.Parse(text), DateTime.Parse(id))!=0)
                    {
                        ((TimeRow)c).panel1.BackColor = Color.Yellow;
                        isbx = true;
                    }
                    else
                    {
                        ((TimeRow)c).panel1.BackColor = Color.Transparent;
                        isbx = false;
                    }
                }

            }
        }


        

        public string  CheckTimeRule() 
        {
            string ret=string.Empty;
            string count=panel1.Controls.Count.ToString();
            string LastEndTime = "";
            
            foreach (Control c in panel1.Controls) 
            {
                if (c is TimeRow) 
                {
                   string code = ((TimeRow)c).label1.Text;
                   string  end_time = ((TimeRow)c).End_Time.Text;
                   string  begin_time = ((TimeRow)c).Start_Time.Text;
                   if (DateTime.Compare(DateTime.Parse(begin_time), DateTime.Parse(end_time)) >= 0 )
                   {
                       ((TimeRow)c).panel1.BackColor = Color.Yellow;
                       ret+="第"+code+"批："+"结束时间必须大于开始时间\n";
                   }
                   else if((code!="1"&&DateTime.Compare(DateTime.Parse(begin_time), DateTime.Parse(LastEndTime)) != 0))
                   {
                       ((TimeRow)c).panel1.BackColor = Color.Yellow;
                       ret += "第"+code+"批："+"开始时间必须等于上一批次结束时间\n";
                   }
                   else
                       ((TimeRow)c).panel1.BackColor = Color.Transparent;

                   if (code == "1")
                   {
                       if (((TimeRow)c).Start_Time.Text != "00:00")
                       {
                           ((TimeRow)c).panel1.BackColor = Color.Yellow;
                           ret += "第" + code + "批：" + "第一批起始时间必须为00:00\n";
                       }
                       else
                           ((TimeRow)c).panel1.BackColor = Color.Transparent;
                   }
                   if (code == count )
                   {
                       if (((TimeRow)c).End_Time.Text != "23:59")
                       {
                           ((TimeRow)c).panel2.BackColor = Color.Yellow;
                           ret += "第" + code + "批：" + "最后一批结束时间必须为23:59";
                       }
                       else
                           ((TimeRow)c).panel2.BackColor = Color.Transparent;
                   }
                   LastEndTime = end_time;
                }
            }
            return ret;
        }

        private void label_freg_Click_1(object sender, EventArgs e)
        {
            CheckTimeRule();
        }
    }
}
