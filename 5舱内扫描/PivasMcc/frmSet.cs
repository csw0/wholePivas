using System;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using PIVAsCommon.Helper;

namespace PivasMcc
{
    public partial class frmSet : Form
    {
        public frmSet()
        {
            InitializeComponent();
        }

        string SQL;
        DB_Help DB = new DB_Help();
        MOXA M;      

        /// <summary>
        /// 加载PLC信息
        /// </summary>
        public void LoadPLCDevicePromptDefaultSet()
        {
        }

        /// <summary>
        /// 加载MOXA设置,并显示
        /// </summary>
        public void LoadMCCConfig() 
        {
            M = new MOXA(cbxMoxaGroup.Text);   
            lvServer.Items.Clear();
            lvPort.Items.Clear();
            if (M.DSMOXA == null || M.DSMOXA.Tables.Count == 0 || M.DSMOXA.Tables[0].Rows.Count==0)
            {
                return;
            }
            for(int i=0;i<M.DSMOXA.Tables[0].Rows.Count;i++)
            {
                ListViewItem I = new ListViewItem();
                I.Text = M.DSMOXA.Tables[0].Rows[i]["MOXANo"].ToString();
                I.SubItems.Add(M.DSMOXA.Tables[0].Rows[i]["MOXAModel"].ToString());
                I.SubItems.Add(M.DSMOXA.Tables[0].Rows[i]["MOXAIP"].ToString());
                I.SubItems.Add(M.DSMOXA.Tables[0].Rows[i]["Group"].ToString());

                lvServer.Items.Add(I);
            }

            if (lvServer.Items.Count!=0)
            {
                lvServer.Items[0].Selected = true;
            }
            if (M.DSDesk == null || M.DSDesk.Tables.Count == 0||M.DSDesk.Tables[0].Rows.Count==0)
            {
                return;
            }
            DataRow[] R = M.DSDesk.Tables[0].Select("MOXANo = " + M.DSMOXA.Tables[0].Rows[0]["MOXANo"].ToString()+"AND MOXAPort IS NOT NULL");
            foreach (DataRow r in R)
            {
                ListViewItem I = new ListViewItem();
                I.Text = r["MOXAPort"].ToString();
                I.SubItems.Add(r["DeskNo"].ToString());
                I.SubItems.Add(r["DeskDesc"].ToString());
                I.SubItems.Add(r["PLC"].ToString());
                I.SubItems.Add(r["RedLight"].ToString());
                I.SubItems.Add(r["GreenLight"].ToString());
                I.SubItems.Add(r["ScreenIP"].ToString());

                lvPort.Items.Add(I);
            }
        }

        /// <summary>
        /// 加载PLC设置
        /// </summary>
        /// <returns></returns>
        public void LoadPLCConfig()
        {
                           
        }

        /// <summary>
        /// 加载日志
        /// </summary>
        public void LoadLogConfig()
        {            
              
        }

        /// <summary>
        /// 获取一个新的序列号
        /// </summary>
        /// <returns></returns>
        public int GetNewSerialNo()
        {
            int newMOXANo = 1;
            int q = 0;
            if (lvServer.Items.Count==0)
            {
                return newMOXANo;
            }
            for (int i = 0; i < lvServer.Items.Count;i++ )
            {
                q = Convert.ToInt32(lvServer.Items[i].SubItems[0].Text);
                if (newMOXANo == q)
                {
                    newMOXANo++;
                }
                else
                {
                    break;
                }
            }
            return newMOXANo;
        }

        /// <summary>
        /// 增加MOXA设备
        /// </summary>
        public void InsertMCCConfig(string SerialNo,string Model, string IP,string Group )
        {
            try
            {
                SQL = "INSERT INTO MOXACon VALUES('" + IP + "','" + Model + "',null,null,null,null,null,null," + SerialNo + ",null," + Group + ")";
                DB.SetPIVAsDB(SQL);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        /// <summary>
        /// 修改MOXA设备信息
        /// </summary>
        /// <param name="SerialNo"></param>
        /// <param name="Model"></param>
        /// <param name="MAC"></param>
        /// <param name="IP"></param>
        public void ModifyMCCConfig(string SerialNo,string Model,string IP,string Group)
        {
            try
            {
                SQL = "UPDATE MOXACon SET MOXAModel = '" + Model + "',MOXAIP = '" + IP + "',[Group]='" + Group + "' WHERE MOXANo = " + SerialNo;
                DB.SetPIVAsDB(SQL);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 获取新端口
        /// </summary>
        /// <returns></returns>
        public int GetNewPortNo()
        {
            int newport = 1;
            int q = 0;
            if (lvPort.Items.Count == 0)
            {
                return newport;
            }
            
            for (int i = 0; i < lvPort.Items.Count; i++)
            {
                q = Convert.ToInt32(lvPort.Items[i].SubItems[0].Text);
                if (newport == q)
                {
                    newport++;
                }
                else
                {
                    break;
                }
            }
            return newport;
        }
        /// <summary>
        /// 添加新端口
        /// </summary>
        /// <param name="eqSection"></param>
        /// <param name="PortNo"></param>
        /// <param name="DeskNo"></param>
        /// <param name="DeskDesc"></param>
        public void InsertMCCPortConfig(string IP,string Model,string MOXANo,int PortNo,string  DeskNo,string  DeskDesc,string PLCAddress,string RedAddress,string GreedAddress,string ScreenIP,string Group)
        {
            try
            {
                SQL = "INSERT INTO MOXACon VALUES('" + IP + "','" + Model + "'," + PortNo + ",'" + DeskNo + "','" + DeskDesc + "','" +
                    PLCAddress + "'," + RedAddress + "," + GreedAddress + "," + MOXANo + ",'" + ScreenIP + "','" + Group + "')";
                SQL += " INSERT INTO ScreenDetail  (IP,MoxaIP,Port,DeskNo) VALUES('" + ScreenIP + "','" + IP + "','" + PortNo + "','" + DeskNo + "')";
                DB.SetPIVAsDB(SQL);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 修改端口
        /// </summary>
        /// <param name="eqSection"></param>
        /// <param name="PortNo"></param>
        /// <param name="DeskNo"></param>
        /// <param name="DeskDesc"></param>
        public void ModifyMCCPortConfig(string Moxaip,string MOXANo, int PortNo, string DeskNo, string DeskDesc, string PLCAddress, string RedAddress, string GreenAddress, string ScreenIP)
        {
            try
            {
                SQL = "UPDATE MOXACon SET DeskNo = '" + DeskNo + "',DeskDesc = '" + DeskDesc + "'" +
                    ",PLC ='" + PLCAddress + "',RedLight = " + RedAddress + ",GreenLight = " + GreenAddress + ",ScreenIP ='" + ScreenIP +
                    "' WHERE MOXANo = " + MOXANo + " AND MOXAPort = " + PortNo;
                SQL += " UPDATE ScreenDetail SET IP='" + ScreenIP + "'WHERE Port='" + PortNo + "'and MoxaIP='" + Moxaip + "' ";
                DB.SetPIVAsDB(SQL);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 验证数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static bool IsNumeric(string str)
        {
            if (str == null || str.Length == 0)
                return false;
            foreach (char c in str)
            {
                if (!Char.IsNumber(c))
                {
                    return false;
                }
            }
            return true;
        } 

        public void frmSet_Load(object sender, EventArgs e)
        {
            try
            {
                LoadPLCDevicePromptDefaultSet();
                LoadMOXAGroup();
                LoadMCCConfig();
                LoadPLCConfig();
                LoadLogConfig();
                LoadINI();
                #region remove 无意义tab页面
                tcSet.TabPages.Remove(tcSet.TabPages["tabPage9"]);
                tcSet.TabPages.Remove(tcSet.TabPages["tabPage8"]);
                tcSet.TabPages.Remove(tcSet.TabPages["tabPage7"]);
                tcSet.TabPages.Remove(tcSet.TabPages["tabPage6"]);
                #endregion
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lvServer_Click(object sender, EventArgs e)
        {

            if (lvServer.SelectedItems.Count==0)
            {
                return;
            }

            lvPort.Items.Clear();
            DataRow[] R = M.DSDesk.Tables[0].Select("MOXANo = " + lvServer.SelectedItems[0].SubItems[0].Text + "AND MOXAPort IS NOT NULL");
            foreach (DataRow r in R)
            {
                ListViewItem I = new ListViewItem();
                I.Text = r["MOXAPort"].ToString();
                I.SubItems.Add(r["DeskNo"].ToString());
                I.SubItems.Add(r["DeskDesc"].ToString());
                I.SubItems.Add(r["PLC"].ToString());
                I.SubItems.Add(r["RedLight"].ToString());
                I.SubItems.Add(r["GreenLight"].ToString());
                I.SubItems.Add(r["ScreenIP"].ToString());
                lvPort.Items.Add(I);
            }
        }

        private void btnAddMOXA_Click(object sender, EventArgs e)
        {
            int serialno = GetNewSerialNo();
           
            SetMOXA s = new SetMOXA(serialno.ToString());
            if (s.ShowDialog()==DialogResult.OK)
            {
                InsertMCCConfig(serialno.ToString(), s.Model, s.IP,s.Group);
                LoadMOXAGroup();
                LoadMCCConfig();                
            }
        }

        private void btnChangeMOXA_Click(object sender, EventArgs e)
        {
            if(lvServer.SelectedItems.Count==0)
            {
                MessageBox.Show("选项为空");
                return;
            }

            int serialno =Convert.ToInt32(lvServer.SelectedItems[0].SubItems[0].Text);
            string Model = lvServer.SelectedItems[0].SubItems[1].Text;
            string IP = lvServer.SelectedItems[0].SubItems[2].Text;
            string Group = lvServer.SelectedItems[0].SubItems[3].Text;

            SetMOXA s = new SetMOXA(serialno.ToString(), Model, IP,Group);
            if (s.ShowDialog() == DialogResult.OK)
            {
                ModifyMCCConfig(serialno.ToString(), s.Model, s.IP,s.Group);
                LoadMOXAGroup();
                LoadMCCConfig();
            }
        }

        private void btnDelMOXA_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvServer.SelectedItems.Count==0)
                {
                    MessageBox.Show("选项为空");
                    return;
                }

                SQL = "DELETE FROM MOXACon WHERE MOXANo = " + lvServer.SelectedItems[0].SubItems[0].Text;
                DB.SetPIVAsDB(SQL);

                MessageBox.Show("已删除");
                LoadMCCConfig();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddPort_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvServer.SelectedItems.Count == 0)
                {
                    MessageBox.Show("选择则一个摩莎");
                    return;
                }

                int port = GetNewPortNo();
                string MOXANo = lvServer.SelectedItems[0].SubItems[0].Text;
                string IP = lvServer.SelectedItems[0].SubItems[2].Text;
                string Model = lvServer.SelectedItems[0].SubItems[1].Text;

                SetPort s = new SetPort(IP, port.ToString(),cbxMoxaGroup.Text);
                if (s.ShowDialog() == DialogResult.OK)
                {
                    port = Convert.ToInt32(s.PortNo);
                    InsertMCCPortConfig(IP, Model, MOXANo, port, s.DeskNo, s.DeskDes, s.PLCAddress, s.RedAddress, s.GreenAddress,s.ScreenIP,s.Group);

                    string SQL = "INSERT INTO LightModel ([DeskNo],[SRemind],[SLong],[STwinkle],[STimes],[FRemind],[FLong],[Ftwinkle],[FTimes],[BRemind],[BLong]" +
                        ",[BTwinkle],[BTimes],[Lremind],[LLong],[LTwinkle],[LTimes],[SerialName],[Rate])" +
                        "SELECT '" + s.DeskNo + "',[SRemind],[SLong],[STwinkle],[STimes],[FRemind],[FLong],[Ftwinkle],[FTimes],[BRemind]      ,[BLong]" +
                        ",[BTwinkle],[BTimes],[Lremind],[LLong],[LTwinkle],[LTimes],[SerialName],[Rate] FROM LightModel where DeskNo = '默认设置'         ";

                    DB.SetPIVAsDB(SQL);
                    LoadMCCConfig();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btnChangePort_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvPort.SelectedItems.Count==0)
                {
                    MessageBox.Show("选项为空");
                    return;
                }

                string IP = lvServer.SelectedItems[0].SubItems[2].Text;
                int port = Convert.ToInt32(lvPort.SelectedItems[0].SubItems[0].Text);
                string MOXANo = lvServer.SelectedItems[0].SubItems[0].Text;
                string DeskNo = lvPort.SelectedItems[0].SubItems[1].Text;
                string DeskDesc = lvPort.SelectedItems[0].SubItems[2].Text;
                string PLC = lvPort.SelectedItems[0].SubItems[3].Text;
                string Red = lvPort.SelectedItems[0].SubItems[4].Text;
                string Green = lvPort.SelectedItems[0].SubItems[5].Text;
                string ScreenIP=lvPort.SelectedItems[0].SubItems[6].Text ;
                string MoxaIP = lvServer.SelectedItems[0].SubItems[2].Text;

                SetPort s = new SetPort(IP, port.ToString(), DeskNo, DeskDesc, PLC, Red, Green,ScreenIP,cbxMoxaGroup.Text);
                if (s.ShowDialog()==DialogResult.OK)
                {
                    ModifyMCCPortConfig(MoxaIP,MOXANo, port, s.DeskNo, s.DeskDes,s.PLCAddress,s.RedAddress,s.GreenAddress,s.ScreenIP); ;
                    LoadMCCConfig();
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnDelPort_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvPort.SelectedItems.Count == 0)
                {
                    MessageBox.Show("选项为空");
                    return;
                }

                SQL = "DELETE FROM MOXACon WHERE MOXANo = " + lvServer.SelectedItems[0].SubItems[0].Text + " AND MOXAPort = " +
                    lvPort.SelectedItems[0].SubItems[0].Text;
                DB.SetPIVAsDB(SQL);

                MessageBox.Show("已删除");
                LoadMCCConfig();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetDefault_Click(object sender, EventArgs e)
        {
            //S标识记账成功；F标识记账失败；B标识退药
            string slong, flong, blong;//长亮时间
            string stwinkle, ftwinkle, btwinkle;//闪烁时间
            string stime, ftime, btime;//闪烁次数
            string s = "", f = "", b = "";//标识是否启用常亮机制0=常亮；1=闪烁

            try
            {
                bool bNum = CheckTextBox(sdtSLong) &&
                    CheckTextBox(sdtFLong) &&
                    CheckTextBox(sdtBLong) &&
                    CheckTextBox(sdtSAlternation) &&
                    CheckTextBox(sdtFAlternation) &&
                    CheckTextBox(sdtBAlternation) &&
                    CheckTextBox(sdtSTime) &&
                    CheckTextBox(sdtFTime) &&
                    CheckTextBox(sdtBTime);

                if (!bNum)//有值不满足
                    return;

                slong = sdtSLong.Text.ToString();
                flong = sdtFLong.Text.ToString();
                blong = sdtBLong.Text.ToString();

                stwinkle = sdtSAlternation.Text.ToString();
                ftwinkle = sdtFAlternation.Text.ToString();
                btwinkle = sdtBAlternation.Text.ToString();

                btime = sdtBTime.Text.ToString();
                ftime = sdtFTime.Text.ToString();
                stime = sdtSTime.Text.ToString();

                if (rbtSLong.Checked)
                    s = "0";
                else if (rbtSAlternation.Checked)
                    s = "1";

                if (rbtFLong.Checked)
                    f = "0";
                else if (rbtFAlternation.Checked)
                    f = "1";

                if (rbtBLong.Checked)
                    b = "0";
                else if (rbtBAlternation.Checked)
                    b = "1";

                SQL = "UPDATE LightModel SET [SRemind]=" + s + ",[SLong]=" + slong + ",[STwinkle]=" + stwinkle + ",[STimes]=" + stime +
                    ",[FRemind]=" + f + ",[FLong]=" + flong + ",[Ftwinkle]=" + ftwinkle + ",[FTimes]=" + ftime + ",[BRemind]=" + b + " ,[BLong]= " + blong +
                        ",[BTwinkle]=" + btwinkle + ",[BTimes]=" + btime + " where DeskNo = '默认设置'";

                DB.SetPIVAsDB(SQL);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLoadDefault_Click(object sender, EventArgs e)
        {
            try
            {
                SQL = "SELECT * from LightModel WHere DeskNo = '默认设置'";
                DataSet ds = DB.GetPIVAsDB(SQL);

                sdtSLong.Text = ds.Tables[0].Rows[0]["SLong"].ToString();
                sdtFLong.Text = ds.Tables[0].Rows[0]["FLong"].ToString();
                sdtBLong.Text = ds.Tables[0].Rows[0]["BLong"].ToString();

                sdtSAlternation.Text = ds.Tables[0].Rows[0]["STwinkle"].ToString();
                sdtFAlternation.Text = ds.Tables[0].Rows[0]["FTwinkle"].ToString();
                sdtBAlternation.Text = ds.Tables[0].Rows[0]["BTwinkle"].ToString();

                sdtBTime.Text = ds.Tables[0].Rows[0]["STimes"].ToString();
                sdtFTime.Text = ds.Tables[0].Rows[0]["FTimes"].ToString();
                sdtSTime.Text = ds.Tables[0].Rows[0]["BTimes"].ToString();

                if (ds.Tables[0].Rows[0]["SRemind"].ToString() == "True")
                    rbtSAlternation.Checked = true;
                else
                    rbtSLong.Checked = true;

                if (ds.Tables[0].Rows[0]["FRemind"].ToString() == "True")
                    rbtFAlternation.Checked = true;
                else
                    rbtFLong.Checked = true;

                if (ds.Tables[0].Rows[0]["BRemind"].ToString() == "True")
                    rbtBAlternation.Checked = true;
                else
                    rbtBLong.Checked = true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tcSet_Selected(object sender, TabControlEventArgs e)
        {
            try
            {
                DataSet ds = DB.GetPIVAsDB("Select TOP 1 SerialName,Rate,PlcType from lightModel");
                string com = ds.Tables[0].Rows[0]["SerialName"].ToString();
                edtDevicePort.Text = com.Substring(3, com.Length - 3);
                textBox1.Text = ds.Tables[0].Rows[0]["Rate"].ToString();
                cbCPUType.Text = ds.Tables[0].Rows[0]["PlcType"].ToString();

                PLCsetLoad();//plc亮灯获取默认设置
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void edtDevicePort_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (IsNumeric(edtDevicePort.Text))
                    {
                        SQL = "UPDATE LightModel SET SerialName = 'COM" + edtDevicePort.Text + "'";
                        DB.SetPIVAsDB(SQL);
                    }
                    else
                    {
                        MessageBox.Show("请输入数字");
                        edtDevicePort.Text = "";
                        return;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rbtSLong_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbtSLong.Checked)
                {
                    SQL = "UPDATE LightModel SET SRemind = 0 WHERE DeskNo = '默认设置'";
                }
                else
                    SQL = "UPDATE LightModel SET SRemind = 1 WHERE DeskNo = '默认设置'";
                   
                DB.SetPIVAsDB(SQL);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rbtBLong_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbtBLong.Checked)
                {
                    SQL = "UPDATE LightModel SET BRemind = 0 WHERE DeskNo = '默认设置 '";
                }
                else
                    SQL = "UPDATE LightModel SET BRemind = 1 WHERE DeskNo = '默认设置'";

                DB.SetPIVAsDB(SQL);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void rbtFLong_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbtFLong.Checked)
                {
                    SQL = "UPDATE LightModel SET FRemind = 0 WHERE DeskNo = '默认设置'";
                }
                else
                    SQL = "UPDATE LightModel SET FRemind = 1 WHERE DeskNo = '默认设置'";

                DB.SetPIVAsDB(SQL);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// 校验TextBox的值类型
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns>true=继续；false=停止</returns>
        private bool CheckTextBox(TextBox ctrl)
        {
            try
            {
                if (!IsNumeric(ctrl.Text))
                {
                    MessageBox.Show("请输入数字");
                    ctrl.Text = "0";
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (IsNumeric(textBox1.Text))
                    {
                        SQL = "UPDATE LightModel SET Rate ="+ textBox1.Text;
                        DB.SetPIVAsDB(SQL);
                    }
                    else
                    {
                        MessageBox.Show("请输入数字");
                        edtDevicePort.Text = "";
                        return;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbCPUType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SQL = "UPDATE LightModel SET PlcType = '" + cbCPUType.Text + "'";
                DB.SetPIVAsDB(SQL);
            }
            catch
            {
                MessageBox.Show("更行PLC类型出错，请查表LightModel中PlcType字段是否正确！！！");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = DB.GetPIVAsDB("Select top 1 serialname,rate,PlcType from lightmodel").Tables[0];
            if (dt.Rows.Count <= 0) 
            {
                MessageBox.Show("无plc设置信息1");
                return;
            }
            Light lt = new Light(dt.Rows[0]["serialname"].ToString(), Convert.ToInt32(dt.Rows[0]["rate"].ToString()), dt.Rows[0]["PlcType"].ToString());//连接com口，设置波特率
          
            dt = DB.GetPIVAsDB(" SELECT * FROM MOXACon m INNER JOIN LightModel l on l.DeskNo=m.DeskNo ").Tables[0];
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("无plc设置信息2");
                return;
            }
            new Thread(() =>
            {
                foreach (DataRow dr in dt.Rows)
                {
                    lt.Lighting(new ControlLightCommand(dr["MOXAIP"].ToString(), dr["MOXAPort"].ToString(), 1, 1, 0));
                    lt.Lighting(new ControlLightCommand(dr["MOXAIP"].ToString(), dr["MOXAPort"].ToString(), 2, 1, 0));
                }
            }).Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = DB.GetPIVAsDB("Select top 1 serialname,rate,PlcType from lightmodel").Tables[0];
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("无plc设置信息1");
                return;
            }
            Light lt = new Light(dt.Rows[0]["serialname"].ToString(), Convert.ToInt32(dt.Rows[0]["rate"].ToString()), dt.Rows[0]["PlcType"].ToString());//连接com口，设置波特率
            dt = DB.GetPIVAsDB(" SELECT * FROM MOXACon m INNER JOIN LightModel l on l.DeskNo=m.DeskNo ").Tables[0];
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("无plc设置信息2");
                return;
            }

            new Thread(() =>
            {
                foreach (DataRow dr in dt.Rows)
                {
                    lt.Lighting(new ControlLightCommand(dr["MOXAIP"].ToString(), dr["MOXAPort"].ToString(), 1, 0, 0));
                    lt.Lighting(new ControlLightCommand(dr["MOXAIP"].ToString(), dr["MOXAPort"].ToString(), 2, 0, 0));
                }
            }).Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = DB.GetPIVAsDB("Select top 1 serialname,rate,PlcType from lightmodel").Tables[0];
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("无plc设置信息1");
                return;
            }
            Light lt = new Light(dt.Rows[0]["serialname"].ToString(), Convert.ToInt32(dt.Rows[0]["rate"].ToString()), dt.Rows[0]["PlcType"].ToString());//连接com口，设置波特率
            dt = DB.GetPIVAsDB(" SELECT * FROM MOXACon m INNER JOIN LightModel l on l.DeskNo=m.DeskNo ").Tables[0];
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("无plc设置信息2");
                return;
            }

            new Thread(() =>
            {
                foreach (DataRow dr in dt.Rows)
                {
                    lt.Lighting(new ControlLightCommand(dr["MOXAIP"].ToString(), dr["MOXAPort"].ToString(), 1, 1, 0));
                    lt.Lighting(new ControlLightCommand(dr["MOXAIP"].ToString(), dr["MOXAPort"].ToString(), 2, 1, 0));
                }
                Thread.Sleep(800);
                foreach (DataRow dr in dt.Rows)
                {
                    lt.Lighting(new ControlLightCommand(dr["MOXAIP"].ToString(), dr["MOXAPort"].ToString(), 1, 0, 0));
                    lt.Lighting(new ControlLightCommand(dr["MOXAIP"].ToString(), dr["MOXAPort"].ToString(), 2, 0, 0));
                }
            }).Start();
        }

        private void PLCsetLoad()
        {
            try
            {
                SQL = "Select * from LightModel where DeskNo='默认设置'";
                DataSet ds = DB.GetPIVAsDB(SQL);

                if(ds!=null&&ds.Tables[0].Rows.Count >0)
                {
                    //ds.Tables[0].Rows[0][0].ToString();
                    sdtSLong.Text = ds.Tables[0].Rows[0]["SLong"].ToString();
                    sdtSAlternation.Text = ds.Tables[0].Rows[0]["STwinkle"].ToString();
                    sdtSTime.Text = ds.Tables[0].Rows[0]["STimes"].ToString();
                    sdtBLong.Text = ds.Tables[0].Rows[0]["BLong"].ToString();
                    sdtBAlternation.Text = ds.Tables[0].Rows[0]["BTwinkle"].ToString();
                    sdtBTime.Text = ds.Tables[0].Rows[0]["BTimes"].ToString();
                    sdtFLong.Text = ds.Tables[0].Rows[0]["FLong"].ToString();
                    sdtFAlternation.Text = ds.Tables[0].Rows[0]["FTwinkle"].ToString();
                    sdtFTime.Text = ds.Tables[0].Rows[0]["FTimes"].ToString();

                    if (ds.Tables[0].Rows[0]["SRemind"].ToString() == "True")
                        rbtSAlternation.Checked = true;
                    else
                        rbtSLong.Checked = true;

                    if (ds.Tables[0].Rows[0]["FRemind"].ToString() == "True")
                        rbtFAlternation.Checked = true;
                    else
                        rbtFLong.Checked = true;

                    if (ds.Tables[0].Rows[0]["BRemind"].ToString() == "True")
                        rbtBAlternation.Checked = true;
                    else
                        rbtBLong.Checked = true;
                } 
                else
                {
                    SQL = "INSERT INTO LightModel ([DeskNo],[SRemind],[SLong],[STwinkle],[STimes],[FRemind],[FLong],[Ftwinkle],[FTimes],[BRemind],[BLong]" +
                        ",[BTwinkle],[BTimes],[Lremind],[LLong],[LTwinkle],[LTimes],[SerialName],[Rate])" +
                        "SELECT '默认设置','1','1000','3000','3','1','0','300','2','1'      ,'0'" +
                        ",'300','1','0','2000','500','1','COM3','9600'  ";

                    DB.SetPIVAsDB(SQL);
                    ds = DB.GetPIVAsDB("Select * from LightModel where DeskNo='默认设置'");
                    sdtSLong.Text = ds.Tables[0].Rows[0]["SLong"].ToString();
                    sdtSAlternation.Text = ds.Tables[0].Rows[0]["STwinkle"].ToString();
                    sdtSTime.Text = ds.Tables[0].Rows[0]["STimes"].ToString();
                    sdtBLong.Text = ds.Tables[0].Rows[0]["BLong"].ToString();
                    sdtBAlternation.Text = ds.Tables[0].Rows[0]["BTwinkle"].ToString();
                    sdtBTime.Text = ds.Tables[0].Rows[0]["BTimes"].ToString();
                    sdtFLong.Text = ds.Tables[0].Rows[0]["FLong"].ToString();
                    sdtFAlternation.Text = ds.Tables[0].Rows[0]["FTwinkle"].ToString();
                    sdtFTime.Text = ds.Tables[0].Rows[0]["FTimes"].ToString();

                    if (ds.Tables[0].Rows[0]["SRemind"].ToString() == "True")
                        rbtSAlternation.Checked = true;
                    else
                        rbtSLong.Checked = true;

                    if (ds.Tables[0].Rows[0]["FRemind"].ToString() == "True")
                        rbtFAlternation.Checked = true;
                    else
                        rbtFLong.Checked = true;

                    if (ds.Tables[0].Rows[0]["BRemind"].ToString() == "True")
                        rbtBAlternation.Checked = true;
                    else
                        rbtBLong.Checked = true;

                    return;
                }

                //SQL = "select * from MOXACon WHere DeskNo = '默认设置'";

                //ds = DB.GetPIVAsDB(SQL);
                //if (ds != null && ds.Tables[0].Rows.Count != 0)
                //{
                //    edtSDeviceName.Text = ds.Tables[0].Rows[0]["PLC"].ToString() + ds.Tables[0].Rows[0]["GreenLight"].ToString();
                //    edtBDeviceName.Text = ds.Tables[0].Rows[0]["PLC"].ToString() + ds.Tables[0].Rows[0]["RedLight"].ToString();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadINI()
        {
            string Moxa = DB.IniReadValuePivas("SCREEN", "type");
            switch (Moxa)
            {
                case "0":
                    cbxScreenType.SelectedIndex = 0;
                    break;
                case "1": 
                    cbxScreenType.SelectedIndex = 1;
                    break;
                case "2":
                    cbxScreenType.SelectedIndex = 2;
                    break;
                default:
                    MessageBox.Show("屏版本未设置，默认使用旧版");
                    cbxScreenType.SelectedIndex = 0;
                    DB.IniWriteValuePivas("SCREEN", "type", "0");
                    break;
            }

            string RedValue = DB.IniReadValuePivas("doublelight", "openred");

            if (RedValue == "1")
            {
                checkBoxUseRed.Checked = true;
            }
            else if (RedValue == "0")
            {
                checkBoxUseRed.Checked = false;
            }
            else
            {
                MessageBox.Show("初始红灯未设置，默认使用");
                DB.IniWriteValuePivas("doublelight", "openred", "1");
                checkBoxUseRed.Checked = true;
            }

            string GreenValue = DB.IniReadValuePivas("doublelight", "opengreen");

            if (GreenValue == "1")
            {
                checkBoxUseGreen.Checked = true;
            }
            else if (GreenValue == "0")
            {
                checkBoxUseGreen.Checked = false;
            }
            else
            {
                MessageBox.Show("初始绿灯未设置，默认使用");
                DB.IniWriteValuePivas("doublelight", "opengreen", "1");
                checkBoxUseGreen.Checked = true;
            }

            if (cbxMoxaGroup.Text != "")
            {
                string MoxaGroup = DB.IniReadValuePivas("MOXA", "Group");
                if (string.IsNullOrEmpty(MoxaGroup))
                {
                    MessageBox.Show("Moxa组号，默认使用1");
                    DB.IniWriteValuePivas("MOXA", "Group", "1");
                }
                else
                {
                    cbxMoxaGroup.Text = MoxaGroup;
                }
            }
        }

        private void LoadMOXAGroup()
        {
            DataSet ds = new DataSet();
            string sql = "select distinct [Group] from MOXACon";
            try
            {
                ds = DB.GetPIVAsDB(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        cbxMoxaGroup.Items.Add(ds.Tables[0].Rows[i]["Group"].ToString().Trim());
                    }
                    cbxMoxaGroup.SelectedIndex  = 0;
                }
                else
                {
                    cbxMoxaGroup.Text = string.Empty;
                }
               
                //cbxMoxaGroup.DisplayMember = "Group";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMCCConfig();
        }

        private void checkBoxUseRed_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string value = checkBoxUseRed.Checked ? "1" : "0";
                DB.IniWriteValuePivas("doublelight", "openred", value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBoxUseGreen_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string value = checkBoxUseGreen.Checked ? "1" : "0";
                DB.IniWriteValuePivas("doublelight", "opengreen", value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbxScreenType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DB.IniWriteValuePivas("SCREEN", "type", cbxScreenType.SelectedIndex.ToString());
        }
    }
}
