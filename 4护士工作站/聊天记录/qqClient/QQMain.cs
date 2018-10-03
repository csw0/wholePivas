using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace qqClient
{
    public partial class QQMain : Form
    {
        SQL sql = new SQL();
        private string Demployeeid = string.Empty; //员工号
        private string NodeName = string.Empty; //选择的节点
        private string WardCode = string.Empty; //病区名
        private string loginType = string.Empty; //登陆类别
        private string LatestNotice = string.Empty; //最新公告 
       
        DB_Help db = new DB_Help(); 
        public QQMain(string DEmployeeId,string wardCode,string LoginType)
        {
            InitializeComponent();
            this.Demployeeid = DEmployeeId;
            this.WardCode = wardCode;
            this.loginType = LoginType;
        }

        private void Main_Load(object sender, EventArgs e)
        {
         
            GetUserInfor();
            //this.Location = new Point(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - this.Width-100, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - this.Height-100);
            GetFriend();
            treeView1.ExpandAll();
            AddNewInfor();
          
        }

        private void GetUserInfor()
        {
            DataSet ds = db.GetPIVAsDB(sql.GetInfor(Demployeeid, WardCode));
            label2.Text = ds.Tables[0].Rows[0][0].ToString();
           if (loginType == "PivasNurse")
           {
               //button3.Visible = false;
               label4.Text = ds.Tables[1].Rows[0][0].ToString();
               label4.Tag = WardCode;
           }
           else if (loginType == "PivasMate")
           {
               label4.Text = "配置中心";
               label4.Tag = "PivasMate";
               //button3.Visible = true;
           }
        }
        private void GetFriend()
        {
            treeView1.Nodes.Clear();
            
            TreeNode tn2 = new TreeNode("分组");
            tn2.Name = "WardS";
            treeView1.Nodes.Add(tn2);
           

            //TreeNode tn1 = new TreeNode("我的好友");
            //tn1.Name = "Friends";
            //treeView1.Nodes.Add(tn1);

            TreeNode tn3 = new TreeNode("公告");
            tn3.Name = "AllWard";
            tn3.Tag = "AllWard";
            tn2.Nodes.Add(tn3);
            if (loginType == "PivasNurse")
            {
                TreeNode tn5 = new TreeNode();
                tn5.Text = "配置中心";
                tn5.Name = "PivasMate";
                tn5.Tag = "PivasMate";
                tn2.Nodes.Add(tn5);

                TreeNode tn4 = new TreeNode();
                tn4.Text = label4.Text;
                tn4.Name = WardCode;
                tn4.Tag = WardCode;
                tn2.Nodes.Add(tn4);

            
            }
            else if(loginType=="PivasMate")
            {
                DataSet ds1 = db.GetPIVAsDB("select WardCode,WardName from DWard where IsOpen=1 order by wardcode");
             
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    TreeNode tn = new TreeNode();
                    tn.Text = ds1.Tables[0].Rows[i]["WardName"].ToString();
                    tn.Name = ds1.Tables[0].Rows[i]["WardCode"].ToString();
                    tn.Tag = ds1.Tables[0].Rows[i]["WardCode"].ToString();
                    tn2.Nodes.Add(tn);
                }
            }

            //DataSet ds = db.GetPIVAsDB(sql.GetFriend(Demployeeid));
            //if (ds != null)
            //{
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        if (ds.Tables[0].Rows[i]["GroupNo"].ToString() == "0")
            //        {
            //            TreeNode tn = new TreeNode();
            //            tn.Text = ds.Tables[0].Rows[i]["Name"].ToString();
            //            tn.Tag = ds.Tables[0].Rows[i]["FriendId"].ToString();
            //            tn1.Nodes.Add(tn);
            //        }
          
            //    }
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddFriend af = new AddFriend(Demployeeid);
            af.ShowDialog();
            if (af.DialogResult == DialogResult.OK)
            {
                GetFriend();
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            flp.Controls.Clear();
            richTextBox1.Text = "";
            if (e.Node.Tag != null)
            {
                button4.Visible = true;
             
                NodeName = e.Node.Tag.ToString();
                e.Node.ImageIndex = 0;


                if (e.Node.Tag.ToString() == "AllWard")
                {
                    label3.Text = "公告";
                    label3.Tag = e.Node.Tag;

                    if (loginType == "PivasMate")
                    {
                        button3.Visible = true;
                    }
                    Talk1(1);
                }
                else
                {
                    label3.Text = "和 " + e.Node.Text + " 聊天中";
                    label3.Tag = e.Node.Tag;
                    button3.Visible = false;
                    Talk1(0);
                }
               
            }
            else
            {
                NodeName = string.Empty;
                label3.Text = "";
                button4.Visible = false;
            }
        }
     
        /// <summary>
        /// 获得群聊天记录
        /// </summary>
        private void Talk1(int type)
        {
            flp.Controls.Clear();
            DataSet ds=new DataSet ();
            if (type == 1)
            {
                ds = db.GetPIVAsDB(sql.GetWardTalk(WardCode, "0", false,DateTime.Now,DateTime.Now));              
            }
            else
            {
                //配置中心
                if (loginType == "PivasMate")
                {
                    ds = db.GetPIVAsDB(sql.GetWardTalk(label3.Tag.ToString(),"1", false,DateTime.Now,DateTime.Now));
                }
                 //护士工作站内
                else if (loginType == "PivasNurse"&&label3.Tag.ToString()==WardCode)
                {
                    ds = db.GetPIVAsDB(sql.GetWardTalk(label3.Tag.ToString(), "2", false, DateTime.Now, DateTime.Now));
                }
                else if (loginType == "PivasNurse" && label3.Tag.ToString() == "PivasMate")
                {
                    ds = db.GetPIVAsDB(sql.GetWardTalk(label4.Tag.ToString(), "1", false, DateTime.Now, DateTime.Now));
                }


            }
            ShowNewNotice();
            ShowTalkLog(ds);
            InsertReadLog(ds);
        }

        private void InsertReadLog(DataSet ds)
        {
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                { 
                    db.SetPIVAsDB(sql.UpdateQQLog(Demployeeid,ds.Tables[0].Rows[i]["id"].ToString()));
                }
            }

        }

        private void ShowTalkLog(DataSet ds)
        {
            if (ds.Tables.Count>0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string de = ds.Tables[0].Rows[i]["DEmployeeName"].ToString();
                    string ins = ds.Tables[0].Rows[i]["InsertTime"].ToString();
                    string cont = ds.Tables[0].Rows[i]["Content"].ToString();
                    string type = ds.Tables[0].Rows[i]["stype"].ToString();
                    string wardName = ds.Tables[0].Rows[i]["wardName"].ToString();
                    JudgeShow(de, ins, cont, type,wardName);

                    if (type == "1")
                    {
                        LatestNotice = ds.Tables[0].Rows[i]["Content"].ToString();
                    }
                }
                flp.ScrollControlIntoView(flp.Controls[ds.Tables[0].Rows.Count - 1]);              
            }
            else
            {
                flp.Controls.Clear();
            }
        }
        /// <summary>
        /// 显示内容
        /// </summary>
        /// <param name="de">发送人</param>
        /// <param name="ins">发送时间</param>
        /// <param name="cont">发送内容</param>
        /// <param name="type">登陆人类别</param>
        /// <param name="wardName">发送人病区</param>
        private void JudgeShow(string de,string ins,string cont,string type,string wardName)
        {
            if (type == "1")
            {
                Notice n = new Notice(de, ins, cont);
                flp.Controls.Add(n);

            }
            else
            {
                MyTalkLog mtl = new MyTalkLog(de, ins, cont, wardName, label2.Text, label3.Tag.ToString(),loginType);
                flp.Controls.Add(mtl);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label3.Text == "")
            {
                MessageBox.Show("请选择接收人");
                return;
            }
            if (richTextBox1.Text.ToString().Trim() == "")
            {
                return; 
            }

            db.SetPIVAsDB(sql.InsertTalkLog(Demployeeid, richTextBox1.Text, label3.Tag.ToString(), label4.Tag.ToString(),"0"));
            MyTalkLog myl = new MyTalkLog(label2.Text, DateTime.Now.ToString(), richTextBox1.Text, label4.Text, label2.Text,label3.Tag.ToString(), loginType);
            flp.Controls.Add(myl);
            flp.ScrollControlIntoView(flp.Controls[flp.Controls.Count - 1]);
         
            richTextBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认发送公告？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                db.SetPIVAsDB(sql.InsertTalkLog(Demployeeid, richTextBox1.Text, "AllWard", "PivasMate", "1"));
                 
                Notice n = new Notice(label2.Text, DateTime.Now.ToString(), richTextBox1.Text);
                    flp.Controls.Add(n);
                    flp.ScrollControlIntoView(flp.Controls[flp.Controls.Count - 1]);
                    LatestNotice = richTextBox1.Text;
                    ShowNewNotice();
                richTextBox1.Text = "";
            }

        }

        private void flp_Click(object sender, EventArgs e)
        {
            this.flp.Focus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            AddNewInfor();           
        }
        /// <summary>
        /// 显示 最新公告
        /// </summary>
        private void ShowNewNotice()
        {
            if (!string.IsNullOrEmpty(LatestNotice) && !label5.Text.ToString().Contains(LatestNotice))
            {
                LatestNotice = LatestNotice.Replace("\n", "       ");
                LatestNotice = LatestNotice.Replace("\n",""); 
                label5.Text = "最新公告：" + LatestNotice;
             
                    label5.Left = 572;
              
            }
        
        }

        /// <summary>
        /// 添加来信息标志
        /// </summary>
        private void AddNewInfor()
        {

            //flp.Controls.Clear();
            DataSet ds = new DataSet();
          
                ds = db.GetPIVAsDB(sql.GetAllInfor(Demployeeid,loginType,WardCode));
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["ToDEmployid"].ToString() == "AllWard")
                {
                    JudgeGonggao(ds.Tables[0].Rows[i]);
                }
                //配置中心登陆 
                else if (loginType == "PivasMate")
                {
                    JudgeMate(ds.Tables[0].Rows[i]);
                }
                //护士工作站登陆
                else
                {
                    JudgeNurse(ds.Tables[0].Rows[i]);
                }
            }

            ShowNewNotice();
        }
        /// <summary>
        /// 判断公告界面添加提醒或者信息
        /// </summary>
        /// <param name="dr"></param>
        private void JudgeGonggao(DataRow dr)
        {
            if (NodeName == "AllWard")
            {
                showdaterow(dr);
                db.SetPIVAsDB(sql.UpdateQQLog(Demployeeid, dr["id"].ToString()));
                flp.ScrollControlIntoView(flp.Controls[flp.Controls.Count - 1]);
            }
            else 
            {
                treeView1.Nodes["WardS"].Nodes["AllWard"].ImageIndex = 1;
            }     
        }
        /// <summary>
        /// 配置中心登陆
        /// </summary>
        /// <param name="dr"></param>
        private void JudgeMate(DataRow dr)
        {
            foreach (TreeNode tn in treeView1.Nodes["WardS"].Nodes)
            {
                if (tn.Name == dr["SWardCode"].ToString() && dr["ToDEmployid"].ToString() == "PivasMate")
                {
                    if (tn.Name ==NodeName)
                    {
                        showdaterow(dr);
                        db.SetPIVAsDB(sql.UpdateQQLog(Demployeeid, dr["id"].ToString()));
                        flp.ScrollControlIntoView(flp.Controls[flp.Controls.Count - 1]);
                    }
                    else
                    {
                        tn.ImageIndex = 1;

                    }
                }

            }

        }
        /// <summary>
        /// 护士工作站登陆
        /// </summary>
        /// <param name="dr"></param>
        private void JudgeNurse(DataRow dr)
        {
            DateTime dt1 = Convert.ToDateTime(dr["InsertTime"].ToString());
            //病区本身通讯
            if (dr["ToDEmployid"].ToString() == WardCode && dr["SWardCode"].ToString() == WardCode)
            {
                if (NodeName== WardCode)
                {
                    showdaterow(dr);
                    flp.ScrollControlIntoView(flp.Controls[flp.Controls.Count - 1]);
                    db.SetPIVAsDB(sql.UpdateQQLog(Demployeeid, dr["id"].ToString()));
                }
                else 
                {
                    treeView1.Nodes[0].Nodes[WardCode].ImageIndex = 1;
                }
            }
            //病区与配置中心通讯
            else 
            {
                if (NodeName== "PivasMate" )
                {
                    showdaterow(dr);
                    flp.ScrollControlIntoView(flp.Controls[flp.Controls.Count - 1]);
                    db.SetPIVAsDB(sql.UpdateQQLog(Demployeeid, dr["id"].ToString()));
                }
                else 
                {
                    treeView1.Nodes[0].Nodes["PivasMate"].ImageIndex = 1;
                }
            }
            //else if (dr["SWardCode"].ToString() == WardCode && dr["ToDEmployid"].ToString() == "PivasMate")
            //{

            //    if (NodeName== "PivasMate" && dr["DEmployeeID"].ToString() != Demployeeid)
            //    {               
            //            showdaterow(dr);
            //            flp.ScrollControlIntoView(flp.Controls[flp.Controls.Count - 1]);

            //    }
            //    else if (dr["DEmployeeID"].ToString() != Demployeeid)
            //    {
            //        treeView1.Nodes[0].Nodes["PivasMate"].ImageIndex = 1;
            //    }
            //}
        }
        /// <summary>
        /// 显示新的信息
        /// </summary>
        /// <param name="dr"></param>
        private void showdaterow(DataRow dr)
        {
            string de = dr["DEmployeeName"].ToString();
            string ins =dr["InsertTime"].ToString();
            string cont =dr["Content"].ToString();
            string type = dr["stype"].ToString();
            string wardName =dr["wardName"].ToString();
            JudgeShow(de, ins, cont, type, wardName);
            if (type == "1")
            {
                LatestNotice = cont;
            }
        }
       
        /// <summary>
        /// 释放所有资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QQMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }
        /// <summary>
        /// 聊天历史记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (label3.Text != "")
            {
                TalkHistory th = new TalkHistory(WardCode, Demployeeid, label2.Text, loginType, label3.Tag.ToString());
                th.TopMost = true;
                th.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选择通讯对象！");
            }
        }

        private void ScrollTimer_Tick(object sender, EventArgs e)
        {
            label5.Left -=1;//设置label1左边缘与其容器的工作区左边缘之间的距离
            if (label5.Right < 65)
            {
                label5.Left=572;//设置label1左边缘与其容器的工作区左边缘之间的距离为该窗体的宽度
            }
        }

     
    }
}
