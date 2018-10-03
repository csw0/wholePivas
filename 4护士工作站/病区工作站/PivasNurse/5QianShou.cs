using System;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Media;
using PIVAsCommon.Helper;

namespace PivasNurse
{
    public partial class QianShou : UserControl
    {
        //private string time = "";
        private int time = 0;//定时刷新界面
        private int time1 =60; //同步签收表
        public DateTime lastTime = DateTime.Now;//记录最后一次使用
        private static DB_Help db = new DB_Help();
        SQL sql = new SQL();
        private string WardCode, PrescriptionCode, EmployeeID;
        private string labelType = "0";
        public QianShou()
        {
            InitializeComponent();
        }
        public QianShou(string Employee, string WardCode)
        {
            InitializeComponent();
            this.WardCode = WardCode;
            this.EmployeeID = Employee;
        }
     
        //private string currentRowLabelNo = "";
        private void QianShou_Load(object sender, EventArgs e)
        {  
            //同步签收表
            db.SetPIVAsDB("exec bl_synSignForm 0");
            labelType= db.GetPivasAllSet("第三方瓶签");
            time = int.Parse(label12.Text);
            LoadBatch();
            LoadPrescription();
            tLabelCount();
         
        }
        /// <summary>
        /// 添加动态批次数字
        /// </summary>
        private void LoadBatch()
        {
            string str = sql.AllBatch();
            DataTable dt_Batch = db.GetPIVAsDB(str).Tables[0];

            for (int i = 0; i < dt_Batch.Rows.Count; i++)
            {
                CheckBox cb = new CheckBox();
                cb.Text = dt_Batch.Rows[i][0].ToString();
                cb.Size = new Size(30,20);
                cb.AutoSize = true;
                cb.Location = new Point();
                cb.Checked = true;
                cb.CheckedChanged += new System.EventHandler(cbx);
                ftp.Controls.Add(cb);
                

            }

        }
       

        private string makesql1()
        {
            StringBuilder str = new StringBuilder();
            str.Append(" select distinct LabelNo,PT.BedNo,Batch,PT.PatName,iv.GroupNo,iv.PrescriptionID,FreqCode,IVStatus,WardRetreat,LabelOver,[ThirdLabelNo]");
            str.Append("  from IVRecord_Scan15 IV");
            str.Append(" inner join Patient PT ON PT.PatCode=IV.PatCode");
            str.Append(" inner join Prescription p on p.PrescriptionID=IV.PrescriptionID ");

            str.Append("  left join PivasNurseFormSet PN on pn.WardCode=iv.WardCode  ");
            str.Append(" where 1=1 and PT.WardCode='" + WardCode + "' and LabelOver=0  ");
            str.Append(" and DateDiff(dd,InfusionDT,'" + dtp.Text + "')=0 ");
            if (cb1.Checked && cb2.Checked)
            {

            }
            else if (cb1.Checked)
            {
                str.Append(" and (Ltrim(Rtrim(P.Remark5))='' or p.Remark5 is null or p.Remark5=''or p.Remark5='长期')  ");
            }
            else if (cb2.Checked)
            {
                str.Append(" and p.Remark5='ST'  ");
            }
            else if (!cb1.Checked && !cb2.Checked)
            {

                str.Append("and p.Remark5='m' ");
                //MessageBox.Show("未选中长期临时");

            }

            if (cb3.Checked && cb4.Checked)
            {
                str.Append(" and(( Batch like '%" + cb3.Text + "' and IVStatus>=LabelPack ) or");
                str.Append(" (Batch like '%" + cb4.Text + "'and IVStatus>=LabelPackAir ))");
            }
            else if (cb3.Checked == true)
            {
                str.Append(" and Batch like '%" + cb3.Text + "' and IVStatus>=LabelPack ");
            }
            else if (cb4.Checked == true)
            {
                str.Append(" and Batch like '%" + cb4.Text + "'and IVStatus>=LabelPackAir ");
            }

            else if (!cb3.Checked && !cb4.Checked)
            {
                str.Append("and Batch='aaa'");
            }
            str.Append(" and TeamNumber in (-1");
            foreach (CheckBox o in ftp.Controls)
            {
                if (o.Checked == true)
                {
                    str.Append("," + (o.Text));
                }
            }
            str.Append(") ");
            if (cb5.Checked && cb6.Checked)
            {

            }
            else if (cb5.Checked)
            {
                str.Append("  and IVStatus=15 ");
            }
            else if (cb6.Checked)
            {
                str.Append(" and IVStatus!=15 ");
            }
            else if (!cb5.Checked && !cb6.Checked)
            {
                str.Append(" and IVStatus =150 ");
            }
            str.Append(" order by BedNo");
            return str.ToString();
        }
        private void LoadPrescription() //绑定dvg
        {
            int a = 0;
            int b = 0;
            dgvPre.Rows.Clear();
            string str = makesql1(); ;
            using (DataSet ds = db.GetPIVAsDB(str))
            {

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    //添加瓶签信息
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvPre.Rows.Add(1);
                        if (labelType == "0")
                        {
                            dgvPre.Rows[i].Cells["LabelNo"].Value = ds.Tables[0].Rows[i]["LabelNo"].ToString();
                        }
                        else
                        {
                            dgvPre.Rows[i].Cells["LabelNo"].Value = ds.Tables[0].Rows[i]["ThirdLabelNo"].ToString();
                        }
                        dgvPre.Rows[i].Cells["MyLabel"].Value = ds.Tables[0].Rows[i]["LabelNo"].ToString();
                        dgvPre.Rows[i].Cells["BedNo"].Value = ds.Tables[0].Rows[i]["BedNo"].ToString();
                        dgvPre.Rows[i].Cells["PatName"].Value = ds.Tables[0].Rows[i]["PatName"].ToString();
                        dgvPre.Rows[i].Cells["IVStatus"].Value = CheckReturn(int.Parse(ds.Tables[0].Rows[i]["IVStatus"].ToString()), ds.Tables[0].Rows[i]["WardRetreat"].ToString(), int.Parse(ds.Tables[0].Rows[i]["LabelOver"].ToString()));
                        dgvPre.Rows[i].Cells["Batch"].Value = ds.Tables[0].Rows[i]["Batch"].ToString().Trim();
                        dgvPre.Rows[i].Cells["FreqCode"].Value = ds.Tables[0].Rows[i]["FreqCode"].ToString().Trim();
                        dgvPre.Rows[i].Cells["PreCode"].Value = ds.Tables[0].Rows[i]["PrescriptionID"].ToString();
                        dgvPre.Rows[i].Cells["TrueStatus"].Value = CheckReturn(int.Parse(ds.Tables[0].Rows[i]["IVStatus"].ToString()), "False", 0);
                        if (ds.Tables[0].Rows[i]["IVStatus"].ToString() == "15")
                        {
                            dgvPre.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                            a++;
                        }
                        else
                        {
                            b++;
                        }

                        if (ds.Tables[0].Rows[i]["WardRetreat"].ToString() == "1" || ds.Tables[0].Rows[i]["WardRetreat"].ToString() == "2" || int.Parse(ds.Tables[0].Rows[i]["LabelOver"].ToString()) < 0)

                            dgvPre.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                    dgvPre.Rows[0].Selected = false;
                   

                }
                label_WQS.Text = b.ToString();
                label_YQS.Text = a.ToString();
                Label_XQS.Text = ds.Tables[0].Rows.Count.ToString();
            }
            textBox1.Focus();
            textBox1.SelectAll();
        }
      

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        
            time = 120;

            LoadPrescription();
            tLabelCount();
          
        }
      
        private string QianShouSql1()
        {
            StringBuilder str = new StringBuilder();
            //需签收
            str.Append(" select top 10000 count(distinct iv.LabelNo) as 'm' from IVRecord_Scan15 IV ");
            str.Append(" inner join PivasNurseFormSet PN on pn.WardCode=iv.WardCode  ");
            str.Append(" inner join Patient PT ON PT.PatCode=IV.PatCode");
            str.Append("  where PT.WardCode='");
            str.Append(WardCode);
            str.Append("' and DateDiff(dd,InfusionDT,'");
            str.Append(dtp.Text);
            str.Append("')=0 and LabelOver=0 and WardRetreat=0 ");
            str.Append(" and(( Batch like '%" + cb3.Text + "' and IVStatus>=LabelPack ) or");
            str.Append(" (Batch like '%" + cb4.Text + "'and IVStatus>=LabelPackAir))");
            //已签收
            str.Append(" union all ");
            str.Append("  select top 10000 count(distinct ivq.IVRecordID) as 'm' from IVRecord_Scan15 ivq");
            str.Append(" left join IVRecord on IVRecord.LabelNo=ivq.IVRecordID");
            str.Append(" left join Patient on IVRecord.PatCode=Patient.PatCode");
            str.Append(" where Patient.WardCode='");
            str.Append(WardCode);
            str.Append("'");
            str.Append(" and DateDiff(dd,InfusionDT,'" + dtp.Text + "')=0 ");
            //未签收
            str.Append(" union all");
            str.Append(" select top 10000 count(distinct iv.LabelNo) as 'm' from IVRecord_Scan15 IV");
            str.Append(" inner join Patient PT ON PT.PatCode=IV.PatCode");
            str.Append(" inner join Prescription p on p.PrescriptionID=IV.PrescriptionID ");
            str.Append(" inner join PivasNurseFormSet PN on pn.WardCode=iv.WardCode  ");
            str.Append(" where IVStatus!=15");
            str.Append(" and PT.WardCode='" + WardCode + "'");
            str.Append(" and(( Batch like '%" + cb3.Text + "' and IVStatus>=LabelPack ) or");
            str.Append(" (Batch like '%" + cb4.Text + "'and IVStatus>=LabelPackAir ))");
            str.Append(" and DateDiff(dd,InfusionDT,'" + dtp.Text + "')=0 ");
            str.Append(" and LabelOver=0 and WardRetreat=0");
            return str.ToString();
        }


        private void tLabelCount()
        {
            StringBuilder str = new StringBuilder();
            str.Append(" select top 10000 count(distinct iv.LabelNo) as 'm' from IVRecord_Scan15 IV ");
            str.Append(" inner join PivasNurseFormSet PN on pn.WardCode=iv.WardCode  ");
            str.Append(" inner join Patient PT ON PT.PatCode=IV.PatCode");
            str.Append("  where PT.WardCode='");
            str.Append(WardCode);
            str.Append("' and DateDiff(dd,InfusionDT,'");
            str.Append(dtp.Text);
            str.Append("')=0 and LabelOver=0 and WardRetreat=0 ");
            str.Append(" and(( Batch like '%" + cb3.Text + "' and IVStatus>=LabelPack ) or");
            str.Append(" (Batch like '%" + cb4.Text + "'and IVStatus>=LabelPackAir))");
            DataSet ds = new DataSet();
            ds = db.GetPIVAsDB(str.ToString());
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                label_ZYQ.Text = ds.Tables[0].Rows[0][0].ToString();
            }
        }
    
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            label_QS();
        }


        private void label_QS()
        {
            TimeSpan DD = DateTime.Now.Subtract(lastTime);
            if (DD.Minutes > 19)
            {
                db.SetPIVAsDB("exec bl_synSignForm 0");
            }

            ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();
           

            //if (textBox1.Text.Length < 14)
            //{
            //    return;
            //}
            if ((textBox1.Text.Substring(0, 4) == "8888" || textBox1.Text.Substring(0, 4) == "9999") && textBox1.Text.Length >= 22)
            {
                #region 总瓶签签收
                string text = textBox1.Text.Substring(0, 22);
                int a = db.SetPIVAsDB("exec bl_LabelAll_QS '" + EmployeeID + "', '" + text + "','" + WardCode + "','1'");
                if (a <= 0)
                {
                    PlaySound("未找到总瓶签.wav");
                    MessageBox.Show("未找到总瓶签");
                }
                else
                {
                    PlaySound("正常.wav");
                    LoadPrescription();
                }
                #endregion
            }
            else
            {
                string labelNo = string.Empty;             
                if (labelType == "0")
                {
                    labelNo = textBox1.Text.Split('@')[0];
                    DLabel(labelNo);
                }
                else
                {
                    string text1 = textBox1.Text.Split('@')[0];
                    string sql = "select LabelNo from IVRecord_Scan15 where ThirdLabelNo='" + text1 + "'";
                    DataSet ds = db.GetPIVAsDB(sql);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        labelNo = ds.Tables[0].Rows[0][0].ToString();
                        DLabel(labelNo);
                    }
                    else
                    {
                        PlaySound("未知瓶签.wav");
                        MessageBox.Show("未找到瓶签");
                    }
                }

            }
            lastTime = DateTime.Now;
            textBox1.SelectAll();
        
        }
        /// <summary>
        /// 判断单个瓶签能否签收
        /// </summary>
        /// <param name="text"></param>
        private void DLabel(string text)
        {


            string str = "select IVStatus,WardRetreat,LabelOver,pt.WardCode,DWard.WardName  from IVRecord_Scan15 iv inner join patient pt on iv.PatCode=pt.PatCode  left join DWard on DWard.WardCode=pt.WardCode  where DATEDIFF(DAY,'" + dtp.Value.ToString("yyyy-MM-dd") + "',InfusionDT)=0 and LabelNo='" + text + "'";
            using (DataTable DtStatus = db.GetPIVAsDB(str).Tables[0])
            {
                if (DtStatus.Rows.Count > 0)
                {
                    if (DtStatus.Rows[0]["WardCode"].ToString() != this.WardCode)
                    {
                        PlaySound("非本病区.wav");
                        MessageBox.Show("非本病区瓶签,这是" + DtStatus.Rows[0]["WardName"].ToString() + " 的瓶签");
                    }
                    else if (DtStatus.Rows[0]["WardRetreat"].ToString() == "2" || DtStatus.Rows[0]["WardRetreat"].ToString() == "1")
                    {
                        PlaySound("已退药.wav");
                        MessageBox.Show("该瓶签已退药");
                    }
                    else if (int.Parse(DtStatus.Rows[0]["LabelOver"].ToString()) < 0)
                    {
                        PlaySound("配置取消.wav");
                        MessageBox.Show("该瓶签已配置取消");
                    }
                    else
                    {
                        if (IsInDvgPre())
                        {
                            int a = db.SetPIVAsDB("exec bl_Label_QS '" + dtp.Value.ToString("yyyy-MM-dd") + "','" + EmployeeID + "', '" + text + "','" + WardCode + "','0'");
                            if (a <= 0)
                            {
                                MessageBox.Show("签收不成功");
                            }
                            else
                            {
                                PlaySound("正常.wav");
                                LoadPrescription();
                                SelectRow(text);
                            }
                        }
                        else
                        {
                            MessageBox.Show("瓶签不在当前条件下的列表中");
                        }
                    }
                }
                else
                {
                    PlaySound("未知瓶签.wav");
                    MessageBox.Show("未找到瓶签");
                }
            }
         
        }
        //播放声音
        private void PlaySound(string name)
        {
            try
            {
                string location = Application.StartupPath + "\\Sound\\" + name;
                SoundPlayer media = new System.Media.SoundPlayer(Application.StartupPath + "\\Sound" + "\\" + name);
                media.Play();
            }
            catch
            {
                MessageBox.Show("找不到声音文件:" + name);
            }
        }

        private void SelectRow(string text)//焦点在扫描的瓶签
        {
            dgvPre.ClearSelection();
            for (int i = 0; i < dgvPre.Rows.Count; i++)
            {
                if (dgvPre.Rows[i].Cells["LabelNo"].Value.ToString() == text)
                {
                    dgvPre.Rows[i].Selected = true;                  
                    dgvPre.CurrentCell = dgvPre.Rows[i].Cells[0];
                    LabelDetail();
                    textBox1.Focus();
                    return;
                }
            }
        }

        private Boolean IsInDvgPre()//瓶签是否在dvg中
        {
            for (int i = 0; i < dgvPre.Rows.Count; i++)
            {
                if (dgvPre.Rows[i].Cells["LabelNo"].Value.ToString() ==textBox1.Text)
                {
                    return true;
                }
             

            }
            return false;
        }
        private void LabelDetail() //瓶签详细信息
        {
            if (dgvPre.Rows.Count <= 0)
            {
                return;
            }
            PrescriptionCode = dgvPre.CurrentRow.Cells["PreCode"].Value.ToString();
            informationQS1.SetInformation(PrescriptionCode);
        }

        private void dgvPre_Click(object sender, EventArgs e)
        {
            //((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();
            if (dgvPre.Rows.Count <= 0)
                return;
            time = 120;
            textBox1.Text = dgvPre.CurrentRow.Cells["LabelNo"].Value.ToString();

            LabelDetail();
        }

        private string CheckReturn(int a, string WardRetreat, int LabelOver)
        {


            if (WardRetreat == "2" || WardRetreat == "1")
                return "已退药";
            else if (LabelOver < 0)
                return "配置取消";
            else
            {
                switch (a)
                {
                    case 3: return "已打印";
                    case 5: return "已排药";
                    case 7: return "已进仓";
                    case 9: return "已配置";
                    case 11: return "已出仓";
                    case 13: return "已打包";
                    case 15: return "已签收";
                    default:
                        break;
                }
            }
            return "";
        }

        private void dgvPre_DoubleClick(object sender, EventArgs e)
        {
            time = 120;
            if (dgvPre.Rows.Count <= 0)
                return;
         
            string status = dgvPre.CurrentRow.Cells["IVStatus"].Value.ToString();

            LabelCheck lb = new LabelCheck(dgvPre.CurrentRow.Cells["MyLabel"].Value.ToString(), status);
            lb.ShowDialog(); 
        }
        public DataSet IVRecordPrint(string Code, string Select)
        {
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append(" select distinct IVRecord_Print.LabelNo from IVRecord_Print");
            str.Append(" inner join IVRecord_Scan15 on IVRecord_Print.LabelNo=IVRecord.LabelNo");
            str.Append("  where (DrugQRCode='" + Code + "'or OrderQRCode='" + Code + "')");
            str.Append("  and IVRecord_Scan15.WardCode ='" + WardCode + "' ");
            str.Append(" " + Select + " ");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            TimeSpan DD = DateTime.Now.Subtract(lastTime);
            if (DD.Minutes > 19)
            {
                db.SetPIVAsDB("exec bl_synSignForm 0");
            }
                if (dgvPre.Rows.Count > 0)
                {
                    
                    //MessageBox.Show("是否确认全部签收？");
                    for (int i = 0; i < dgvPre.Rows.Count; i++)
                    {
                        if (dgvPre.Rows[i].Cells["Column1"].Value!=null&& dgvPre.Rows[i].Cells["Column1"].Value.ToString()=="True")
                        {
                            string text = dgvPre.Rows[i].Cells["LabelNo"].Value.ToString();
                            string str = "select IVStatus,WardRetreat,LabelOver,pt.WardCode,DWard.WardName  from  IVRecord_Scan15 iv inner join patient pt on iv.PatCode=pt.PatCode  left join DWard on DWard.WardCode=pt.WardCode  where DATEDIFF(DAY,'" + dtp.Value.ToString("yyyy-MM-dd") + "',InfusionDT)=0 and LabelNo='" + text + "'";
                            using (DataTable DtStatus = db.GetPIVAsDB(str).Tables[0])
                            {
                                if (DtStatus.Rows[0]["WardRetreat"].ToString() == "2" || DtStatus.Rows[0]["WardRetreat"].ToString() == "1")
                                {
                                    PlaySound("已退药.wav");
                                    MessageBox.Show("该瓶签已退药");
                                }
                                else if (int.Parse(DtStatus.Rows[0]["LabelOver"].ToString()) < 0)
                                {
                                    PlaySound("配置取消.wav");
                                    MessageBox.Show("该瓶签已配置取消");
                                }
                                else
                                {
                                    int a = db.SetPIVAsDB("exec bl_Label_QS '" + dtp.Value.ToString("yyyy-MM-dd") + "','" + EmployeeID + "', '" + text + "','" + WardCode + "','0'");
                                }

                            }
                        }
                    }
                    PlaySound("正常.wav");
                    LoadPrescription();
                  
                }
           
        }
        private void button2_Click(object sender, EventArgs e)
        {
            label_QS();
        }


        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();
            time = 120;
            textBox1.SelectAll();
        }

        private void cb1_CheckedChanged(object sender, EventArgs e)
        {
            ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();
            time = 120;
            LoadPrescription();
        }

        private void cb2_CheckedChanged(object sender, EventArgs e)
        {
            ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();
            time = 120;
            LoadPrescription();
        }

        private void cb3_CheckedChanged(object sender, EventArgs e)
        {
            ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();
            time = 120;
            LoadPrescription();
        }

        private void cb4_CheckedChanged(object sender, EventArgs e)
        {
            ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();
            LoadPrescription();
        }

        private void cbx(object sender, EventArgs e)
        {
            ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();

            LoadPrescription();
        }

        private void cb5_CheckedChanged(object sender, EventArgs e)
        {
            ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();
            if (cb5.Checked == false && cb6.Checked == false)
            {
                cb6.Checked = true;
            }
            LoadPrescription();
        }

        private void cb6_CheckedChanged(object sender, EventArgs e)
        {
            ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();
            if (cb6.Checked == false && cb5.Checked == false)
            {
                cb5.Checked = true;
            }
            LoadPrescription();
        }

        /// <summary>
        /// 时钟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (time > 0)
            {
                time--;
                label12.Text = time.ToString();
            }
            else
            {
                LoadPrescription();
            }
        }

        private void dgvPre_KeyUp(object sender, KeyEventArgs e)
        {
            textBox1.Text = dgvPre.CurrentRow.Cells["LabelNo"].Value.ToString();
            LabelDetail();
        }

        private void label10_MouseHover(object sender, EventArgs e)
        {
            label10.ForeColor = Color.Black;
        }

        private void label10_MouseLeave(object sender, EventArgs e)
        {
            label10.ForeColor = Color.Gray;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (time1 == -1)
            {
                return; 
            }
            else if (time1 > 0)
            {
                time1--;
                label8.Text = time1.ToString();
                label1.Visible = false;
            }
            else if (time1 == 0)
            {
                label8.Text = time1.ToString();
                time1 = -1;
                label1.Visible = true;
            }
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
       
                label1.ForeColor = Color.Black;
           
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
           
                label1.ForeColor = Color.DarkGray;          
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
            label8.Text = time1.ToString();
            db.SetPIVAsDB(" exec bl_synSignForm 1");

            ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();
            time = 120;
            LoadPrescription();
            time1 = 60;
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked && dgvPre != null && dgvPre.Rows.Count > 0)
            {
                for (int i = 0; i < dgvPre.Rows.Count; i++)
                {
                    dgvPre.Rows[i].Cells["Column1"].Value = true;
                }
            }
            else if (!checkBox1.Checked && dgvPre != null && dgvPre.Rows.Count > 0)
            {
                for (int i = 0; i < dgvPre.Rows.Count; i++)
                {
                    dgvPre.Rows[i].Cells["Column1"].Value =false;
                }
            }
        }

        /// <summary>
        /// 定时签收临时表同步
        /// 超过30分钟未使用扫描，停止同步，下一次扫描时判断，若大于19分钟未扫描，则先同步后扫描。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SynTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan DD = DateTime.Now.Subtract(lastTime);
            if (DD.Minutes < 30)
            {
                db.SetPIVAsDB("exec bl_synSignForm 0");
            }
        }
    }
}
