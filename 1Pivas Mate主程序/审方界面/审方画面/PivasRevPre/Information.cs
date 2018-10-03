using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ChargeInterface;
using FormClick;
using PivasHisInterface;
using PIVAsCommon.Helper;

namespace PivasRevPre
{
    public partial class Information : UserControl
    {
        #region 变量
        DB_Help dbHelp = new DB_Help();
        SQL SQLStr = new SQL();
        MesBox Mes = new MesBox();
        public string PrescriptionID="";
        private string CaseID = "";
        private DataTable dtDrugInfo = new DataTable();
        string DEmployeeID = "";

        FormClick.EventClass EC = new EventClass();

        ICharge bPre = null;
        #endregion

        #region  构造方法
        public Information()
        {
            InitializeComponent();
            #region csw修改，需要此exe下配置文件IMEQPIVAs.ini
            bPre = ChargeFactory.GetCharge(dbHelp.IniReadValuePivas("Charge", "HospitalType"));
            #endregion
        }

        public Information(string DEmployeeID)
        {
            InitializeComponent();
            this.DEmployeeID = DEmployeeID;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 设置显示信息的方法
        /// </summary>
        /// <param name="preID">处方ID</param>
        /// <param name="perresult"></param>
        /// <param name="DEmployeeID">员工ID</param>
        public void SetInformation(string preID, string perresult, string DEmployeeID)
        {
            try
            {
                this.DEmployeeID = DEmployeeID;
                string str = SQLStr.INFO(preID, perresult);
                //MessageBox.Show(str);
                DataTable dtinfo = new DataTable();
                DataSet ds = dbHelp.GetPIVAsDB(str);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dtinfo = ds.Tables[0];
                        if (dtinfo.Rows.Count > 0)
                        {
                            setPerson(dtinfo.Rows[0]);
                            if (dtinfo.Rows[0]["PStatus"].ToString() == "3")
                            {
                                btnTuiDan.Enabled = false;
                            }
                            else
                            {
                                btnTuiDan.Enabled = true;
                            }
                            setCurrentDrug(dtinfo);//药品显示
                        }
                    }

                    if (ds.Tables.Count > 1)
                    {
                        DataTable dd = new DataTable();
                        dd = ds.Tables[1];
                        setResult(dd);//审方结果显示
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 重审方法
        /// </summary>
        private void MethodReCheck()
        {
            try
            {
                if (PrescriptionID == "")
                {
                    MessageBox.Show("请选处方！");
                    return;
                }

                if (Mes.ShowDialog("提示", "重审会覆盖之前的结果，确定要重审吗？") == DialogResult.Cancel)
                {
                    return;
                }
                string str = "EXEC [bl_Remonitor] '" + PrescriptionID + "'";
                dbHelp.SetPIVAsDB(str);
                dbHelp.SetPIVAsDB("DELETE FROM BPRecord WHERE PrescriptionID =  '" + PrescriptionID + "' ");
                CheckPre.PREID = PrescriptionID;
                CheckPre cp = (CheckPre)this.Parent.Parent.Parent.Parent;
                cp.GetSelPrescriptions();
                if (cp.dgvResult.Rows.Count > 0)
                {
                    //不需要刷新病区
                }
                else
                {
                    cp.getWards();//刷新病区
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 退单方法
        /// </summary>
        private void MethodTuiDan()
        {
            if (lblPatient.Text == "")
            {
                Mes.ShowDialog("提示", "当前没有处方");
                return;
            }
            if (PrescriptionID == "")
            {
                Mes.ShowDialog("提示", "请选择处方!");
                return;
            }
          
            BPConfirm b = new BPConfirm(btnTuiDan.Text, "不通过", PrescriptionID,DEmployeeID);
            if (b.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string str = "EXEC bl_backPrescription " + PrescriptionID + ",'" + b.eid + "','" + b.DoctorExplain + "'";
                    dbHelp.SetPIVAsDB(str);

                    string s = "SHJJ";
                    bPre.PivasRevPreFalse(lblGroupNo.Text, b.ecode, out s);
                    CheckPre.PREID = "";
                    CheckPre cp = (CheckPre)this.Parent.Parent.Parent.Parent;

                    if (cp.ckFlag == 0)
                    {
                        cp.GetSelPrescriptions();
                        //审方结束后的刷新操作
                        if (cp.dgvResult.Rows.Count > 0)
                        {

                        }
                        else
                        {
                            cp.getWards();
                        }
                    }
                    else
                    {
                        cp.GetSelPrescriptions(cp.ckID);
                        //审方结束后的刷新操作
                        if (cp.dgvResult.Rows.Count > 0)
                        {

                        }
                        else
                        {
                            cp.getWards();
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// 清空方法
        /// </summary>
        public void clear()
        {
            lblBedNo.Text = "";
            lblCaseID.Text = "";
            lblDoctor.Text = "";
            lblEndDT.Text = "";
            lblSex.Text = "";
            lblStartDT.Text = "";
            lblWard.Text = "";

            lblPatient.Text = "";
            PrescriptionID = "";

            lblHeight.Text = "";
            lblWeight.Text = "";
            lblAge.Text = "";

            lblBatch.Text = "";
            lblyongfa.Text = "";
            lblexplain.Text = "";

            lblGroupNo.Text = "";
            lblCPer.Text = "";
            lblCPtime.Text = "";
            tboxDiagnosis.Text = "";
            pnlInfo.Controls.Clear();
            dgvDrugs.Rows.Clear();
        }

        /// <summary>
        /// 设置病人信息
        /// </summary>
        /// <param name="R"></param>
        private void setPerson(DataRow R)//设置信息
        {
            try
            {
                lblBedNo.Text = R["BedNo"].ToString();
                lblCaseID.Text = R["CaseID"].ToString();
                CaseID = R["CaseID"].ToString();//绑定住院号

                //********查询诊断结果*************//
                DataSet ds = GetDiagnosis(CaseID);
                string strDia = "";
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        strDia += ds.Tables[0].Rows[i]["Messages"].ToString() + "，";
                    }
                    strDia = strDia.Substring(0, strDia.Length - 1);
                }
                tboxDiagnosis.Text = strDia;
                lblDoctor.Text = R["DocName"].ToString() + "(" + R["DoctorCode"].ToString() + ")";
                lblEndDT.Text = R["EndDT"].ToString();

                string sex = R["Sex"].ToString().ToLower().Trim();
                if ("2" == sex)
                    lblSex.Text = "女";
                else if ("1" == sex)
                    lblSex.Text = "男";
                lblStartDT.Text = R["StartDT"].ToString();
                lblWard.Text = R["WardName"].ToString();

                lblPatient.Text = R["PatName"].ToString();
                PrescriptionID = R["PrescriptionID"].ToString();
                lblGroupNo.Text = R["GroupNo"].ToString();
                if (R["Height"].ToString() == "0")
                {
                    lblHeight.Text = "-";
                }
                else
                {
                    lblHeight.Text = R["Height"].ToString();
                }
                if (R["Weight"].ToString() == "0.00")
                {
                    lblWeight.Text = "-";
                }
                else
                {
                    lblWeight.Text = R["Weight"].ToString();
                }
                lblAge.Text = R["Birthday"].ToString();

                lblBatch.Text = R["FregCode"].ToString();
                lblyongfa.Text = R["UsageName"].ToString();
                lblexplain.Text = R["Explain"].ToString();

                lblCPer.Text = R["CPer"].ToString();
                lblCPtime.Text = R["CPtime"].ToString()+'/' + R["CPtimeRG"].ToString();
                lblCPtime.Text = lblCPtime.Text == "/" ? "" : lblCPtime.Text;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 设置当前处方所对药品的方法
        /// </summary>
        /// <param name="dt"></param>
        private void setCurrentDrug(DataTable dt)
        {
            //旧的方法(panel中添加用户控件)
            #region
            //try
            //{
            //    pnlInfo.Controls.Clear();
            //    foreach (Control  c in  pnlInfo.Controls)
            //    {
            //        c.Dispose();
            //    }

            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        CurrentDrug Drug = new CurrentDrug();
            //        Drug.setDrug(dt.Rows[i]);
            //        pnlInfo.Controls.Add(Drug);
            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            #endregion


            //新的方法 ADD 罗璨20140710
            dgvDrugs.Rows.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgvDrugs.Rows.Add(1);
                dgvDrugs.Rows[i].Cells["ColDrugName"].Value = dt.Rows[i]["DrugName"].ToString();
                dgvDrugs.Rows[i].Cells["ColDrugSize"].Value = dt.Rows[i]["Spec"].ToString();
                dgvDrugs.Rows[i].Cells["ColYongLiang"].Value = float.Parse(dt.Rows[i]["Dosage"].ToString()).ToString() + dt.Rows[i]["DosageUnit"].ToString();
                dgvDrugs.Rows[i].Cells["Remark7"].Value = dt.Rows[i]["Remark7"].ToString();
                dgvDrugs.Rows[i].Cells["ColRemark8"].Value = dt.Rows[i]["Remark8"].ToString();
                dgvDrugs.Rows[i].Cells["ColRemark9"].Value = dt.Rows[i]["Remark9"].ToString();
                dgvDrugs.Rows[i].Cells["ColRemark10"].Value = dt.Rows[i]["Remark10"].ToString();
                dgvDrugs.Rows[i].Cells["ColUniPreparationID"].Value = dt.Rows[i]["UniPreparationID"].ToString();
                dgvDrugs.Rows[i].Cells["ColCount"].Value = dt.Rows[i]["Quantity"].ToString();

                if (dt.Rows[i]["PiShi"].ToString() == "True")
                {
                    dgvDrugs.Rows[i].Cells["ColPiShi"].Value = "皮试";
                }
                else
                {
                    dgvDrugs.Rows[i].Cells["ColPiShi"].Value = "不需皮试";
                }
            }
            dgvDrugs.Rows[0].Selected = false;
        }

        /// <summary>
        /// 设置审方结果
        /// </summary>
        /// <param name="dt"></param>
        private void setResult(DataTable dt)
        {
            dtDrugInfo = dt;
            pnlInfo.Controls.Clear();
            //Label l = new Label();
            // l.Text = "审方结果：";
            //l.Font = new Font("宋体", 12,);
            // l.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // l.ForeColor = Color.DarkGreen;
            // l.Height = 16;
            //l.Width = 470;
            //pnlInfo.Controls.Add(l);
            //l.Margin = new System.Windows.Forms.Padding(10);
            if (dt != null && dt.Rows.Count >= 0)
            {
                int Count = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    result r = new result();
                    if ((dt.Rows[i]["CensorItem"] != null && dt.Rows[i]["CensorItem"].ToString().Trim() != "")
                       || (dt.Rows[i]["ReferenName"] != null && dt.Rows[i]["ReferenName"].ToString().Trim() != ""))
                    {
                        r.setResult(dt.Rows[i]);
                        r.Width = pnlInfo.Width;
                        Count++;
                        pnlInfo.Controls.Add(r);
                        r.Margin = new Padding(0, 0, 0, 0);
                        r.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                        //r.Dock = System.Windows.Forms.DockStyle.Fill;
                    }


                }
                if (Count <= 0)
                {
                    Label Label_txt = new Label();
                    Label_txt.Text = "通过！";
                    Label_txt.Margin = new System.Windows.Forms.Padding(10);
                    pnlInfo.Controls.Add(Label_txt);
                }
            }
        }

        /// <summary>
        /// 根据住院号查询诊断信息的方法
        /// </summary>
        /// <param name="code">住院号</param>
        private DataSet GetDiagnosis(string code)
        {
            DataSet dsDia = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(" select * from Diagnosis ");
            sql.Append(" where DATEDIFF(DD,InsertDT,(select top(1) InsertDT from Diagnosis  where CaseID = '" + code + "' order by InsertDT desc))=0 ");
            sql.Append(" and CaseID='" + code + "'");
            sql.Append(" order by InsertDT asc;");
            dsDia = dbHelp.GetPIVAsDB(sql.ToString());
            return dsDia;
        }
        #endregion

        #region 事件

        /// <summary>
        /// 重审事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRecheck_Click(object sender, EventArgs e)
        {
            //重审
            MethodReCheck();
        }

        /// <summary>
        /// 退单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTuiDan_Click(object sender, EventArgs e)
        {
            //退单
            MethodTuiDan();
            CheckPre cp = (CheckPre)this.Parent.Parent.Parent.Parent;
            cp.GetCount();//刷新统计数量
        }

        /// <summary>
        /// 药品单元格单击事件（单击药品需要查询药品信息）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDrugs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //去除选中的背景色
           // dgvDrugs.CurrentCell.Selected = false;
            if (e.ColumnIndex == 0&&e.RowIndex >=0)
            {
                string UniPreID = dgvDrugs.Rows[e.RowIndex].Cells["ColUniPreparationID"].Value.ToString();
                try
                {
                    if (string.IsNullOrEmpty(UniPreID))
                    {
                        MessageBox.Show("无匹配药品,请维护", "提示");
                    }
                    else
                    {
                        LibDrugManual.RichFrom mf = new LibDrugManual.RichFrom();
                        mf.UniPreparationID = UniPreID;
                        mf.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            { 
                
            }
        }

        /// <summary>
        /// flowlayoutPanel控件的size改变事件，控制其内部控件的大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlInfo_SizeChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(pnlInfo.Width.ToString());
            //foreach (Control c in pnlInfo.Controls)
            //{
            //    if (c is result)
            //    {
            //        result r = (result)c;
            //        r.Width = pnlInfo.Width;
            //        label22.Text = r.Width.ToString();
            //    }
            //}

            setResult(dtDrugInfo);
        }

        /// <summary>
        /// 拆分器完成分割的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
           // label4.Text = splitContainer1.SplitterDistance.ToString();
            //string fileName = Application.StartupPath + "\\IMEQPIVAs.ini";
            //IniFile iniFile = new IniFile(fileName);
            saveIni();

        }

        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Information_Load(object sender, EventArgs e)
        {

          
            
            //string fileName = Application.StartupPath + "\\IMEQPIVAs.ini";
            //IniFile iniFile = new IniFile(fileName);
            //string str = iniFile.IniReadValue("SplitterDistance", "MIN");
            //if (str != "" && str != null)
            //{
            //    splitContainer1.SplitterDistance = int.Parse(str);
            //}
            
        }
        #endregion

        private void saveIni()
        {
            //string fileName = Application.StartupPath + "\\IMEQPIVAs.ini";
            //IniFile iniFile = new IniFile(fileName);
            //iniFile.IniWriteValue("SplitterDistance", "MIN", splitContainer1.SplitterDistance.ToString());
        }

        private void label21_MouseHover(object sender, EventArgs e)
        {
            btnRecheck.Visible = true;
        }

        private void btnRecheck_MouseLeave(object sender, EventArgs e)
        {
            btnRecheck.Visible = false;
        }
       
        private void linkLabel1_Click(object sender, EventArgs e)
        {

            if (lblPatient.Text == "")
            {
                Mes.ShowDialog("提示", "当前没有处方");
                return;
            }
            if (PrescriptionID == "")
            {
                MessageBox.Show("请选择处方！");
                return;
            }

            string sql = "select *from Prescription  where PStatus<2 and PrescriptionID='" + PrescriptionID + "'";

            DataSet ds = dbHelp.GetPIVAsDB(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (Mes.ShowDialog("提示", "该操作不可逆，是否确定删除处方？") == DialogResult.Cancel)
                {
                    return;
                }

                int a = dbHelp.SetPIVAsDB(SQLStr.DeletePrescription(PrescriptionID));
                if (a > 0)
                {
                    MessageBox.Show("删除完成");

                }

                CheckPre.PREID = "";
                CheckPre cp = (CheckPre)this.Parent.Parent.Parent.Parent;

                cp.GetSelPrescriptions();
                //审方结束后的刷新操作

                cp.getWards();
            }
            else
            {
                MessageBox.Show("该处方正在执行，不能删除");
            }

        }

        private void label23_Click(object sender, EventArgs e)
        {
            try
            {
                if (PrescriptionID == "")
                {
                    MessageBox.Show("请选择处方！");
                    return;
                }
                PivasHisCommOrigin extra = new PivasHisCommOrigin();
                extra.Extra1(PrescriptionID, CaseID, "", "", "", "");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }

        private void lblPatient_Click(object sender, EventArgs e)
        {
            try
            {
                string AccountID = "";
                AccountID = dbHelp.GetPIVAsDB("select AccountID from Demployee where demployeeid='" + DEmployeeID + "'").Tables[0].Rows[0][0].ToString();
                //MessageBox.Show(AccountID);
                EC.NameEvent(AccountID, CaseID);
            }
            catch (Exception ex)
            {
                throw new Exception("方法NameEvent出错，来自FormClick.dll", ex);
            }
        }
       
    }
}
