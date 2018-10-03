using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using PIVAsCommon.Helper;

namespace PivasFreqRule
{
    /// <summary>
    /// 药品优先规则用户控件
    /// </summary>
    public partial class MedcineRule : UserControl
    {
        DB_Help DB = new DB_Help();
        DataSet DS = new DataSet();
        DataTable DT = new DataTable();
        public static string status;
        public static string SeqNo;
        public static string WardCode;
        

        public MedcineRule()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MedcineRule_Load(object sender, EventArgs e)
        {
            ShowWard();//加载病区
            if (DT.Rows.Count > 0)
            {
                pnlWard.Controls[0].Focus();
                WardCode = DT.Rows[0]["WardCode"].ToString();
                label5.Text = DT.Rows[0]["WardName"].ToString();
                ShowMedcine(WardCode);
            }          

        }

        /// <summary>
        /// 病区加载
        /// </summary>
        private void ShowWard()
        {
            string str = "select WardCode, WardName From DWard where IsOpen = 1";
            try
            {
                DS = DB.GetPIVAsDB(str);
                DT = DS.Tables[0];
                pnlWard.Controls.Clear();
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    DWard Ward = new DWard(DT.Rows[i]["WardCode"].ToString(),pnlWard);
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

        /// <summary>
        /// 加载药品
        /// </summary>
        /// <param name="wardcode"></param>
        public void ShowMedcine(string wardcode)
        {
            //根据部门编号查询该部门下面的药品信息
            string str = "SELECT MPRuleID, IsMedClass, MedCode, MedName,SeqNo "+
                         "FROM OrderMPRule WHERE WardCode = '" + wardcode + "' ORDER BY SeqNo";
            WardCode = wardcode;//部门编号
           try
            {
               //清空显示区域
                pnlMed1.Controls.Clear();//高优先级区域
                pnlMed2.Controls.Clear();//低优先级区域
                DS = DB.GetPIVAsDB(str);
                for (int i = 0; i < DS.Tables[0].Rows.Count;i++ )
                {
                    MedcineRow medcine ;
                    
                    //高优先级区域赋值
                    if (Convert.ToInt32(DS.Tables[0].Rows[i]["SeqNo"].ToString()) < 100000000)
                    {
                        medcine = new MedcineRow(pnlMed1);
                        medcine.SetMedcine(DS.Tables[0].Rows[i]);
                        pnlMed1.Controls.Add(medcine);
                    }

                    //低优先级区域赋值
                    else if (Convert.ToInt32(DS.Tables[0].Rows[i]["SeqNo"].ToString()) > 150000000)
                    {
                        medcine = new MedcineRow(pnlMed2);
                        medcine.SetMedcine(DS.Tables[0].Rows[i]);
                        pnlMed2.Controls.Add(medcine);
                    }
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 高优先级增加向上排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Med1Up_Click(object sender, EventArgs e)
        {
            int I;
            string str,str1,upIndex,lowIndex,upSeqNo,lowSeqNo;
            try
            {
                if (pnlMed1.Controls.Count == 0)
                    return;
                if (MedcineRow.Med == null || Convert.ToInt32(MedcineRow.SeqNo) > 150000000)
                {
                    return;
                }
                I = pnlMed1.Controls.GetChildIndex(MedcineRow.Med);
                if ( I > 0 )
                {
                    lowIndex = ((MedcineRow)pnlMed1.Controls[I]).lblMedID.Text;
                    lowSeqNo = ((MedcineRow)pnlMed1.Controls[I]).lblSeqNo.Text;
                    upIndex = ((MedcineRow)pnlMed1.Controls[I - 1]).lblMedID.Text;
                    upSeqNo = ((MedcineRow)pnlMed1.Controls[I - 1]).lblSeqNo.Text;

                    str = "Update OrderMPRule Set SeqNo = " + upSeqNo + " where MPRuleID = " + lowIndex;
                    str1 = "Update OrderMPRule Set SeqNo = " + lowSeqNo + " where MPRuleID = " + upIndex;
                    ((MedcineRow)pnlMed1.Controls[I]).lblSeqNo.Text = upSeqNo;
                    ((MedcineRow)pnlMed1.Controls[I - 1]).lblSeqNo.Text = lowSeqNo;


                    DB.SetPIVAsDB(str);
                    DB.SetPIVAsDB(str1);

                    pnlMed1.Controls.SetChildIndex(MedcineRow.Med, I - 1);
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("请选择一个药物！");
            }            
        }

        /// <summary>
        /// 高优先级向下排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Med1Down_Click(object sender, EventArgs e)
        {
            int I;
            string str,str1,upIndex,lowIndex,upSeqNo,lowSeqNo;
            try
            {
                if (pnlMed1.Controls.Count == 0)
                    return;
                if (MedcineRow.Med == null || Convert.ToInt32(MedcineRow.SeqNo) > 150000000)
                    return;
                I = pnlMed1.Controls.GetChildIndex(MedcineRow.Med);
                if (I >= 0 && I < pnlMed1.Controls.Count-1)
                {
                    upIndex = ((MedcineRow)pnlMed1.Controls[I]).lblMedID.Text;
                    upSeqNo = ((MedcineRow)pnlMed1.Controls[I]).lblSeqNo.Text;
                    lowIndex = ((MedcineRow)pnlMed1.Controls[I + 1]).lblMedID.Text;
                    lowSeqNo = ((MedcineRow)pnlMed1.Controls[I + 1]).lblSeqNo.Text;

                    str = "Update OrderMPRule Set SeqNo = " + upSeqNo + " where MPRuleID = " + lowIndex;
                    str1 = "Update OrderMPRule Set SeqNo = " + lowSeqNo + " where MPRuleID = " + upIndex;
                    ((MedcineRow)pnlMed1.Controls[I]).lblSeqNo.Text = lowSeqNo;
                    ((MedcineRow)pnlMed1.Controls[I + 1]).lblSeqNo.Text = upSeqNo;

                    DB.SetPIVAsDB(str);
                    DB.SetPIVAsDB(str1);

                    pnlMed1.Controls.SetChildIndex(MedcineRow.Med, I + 1);
                }
                if (I >= pnlMed1.Controls.Count-1) {
                    return;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("请选择一个药物！");
            }  
        }

        /// <summary>
        /// 低优先级向上排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Med2Up_Click(object sender, EventArgs e)
        {
            int I;
            string str,str1,upIndex,lowIndex,upSeqNo,lowSeqNo;
            try
            {
                if (pnlMed2.Controls.Count == 0)
                    return;
                if (MedcineRow.Med == null || Convert.ToInt32(MedcineRow.SeqNo) < 100000000)
                    return;
                I = pnlMed2.Controls.GetChildIndex(MedcineRow.Med);
                if (I > 0)
                {
                    lowIndex = ((MedcineRow)pnlMed2.Controls[I]).lblMedID.Text;
                    lowSeqNo = ((MedcineRow)pnlMed2.Controls[I]).lblSeqNo.Text;
                    upIndex = ((MedcineRow)pnlMed2.Controls[I - 1]).lblMedID.Text;
                    upSeqNo = ((MedcineRow)pnlMed2.Controls[I - 1]).lblSeqNo.Text;

                    str = "Update OrderMPRule Set SeqNo = " + upSeqNo + " where MPRuleID = " + lowIndex;
                    str1 = "Update OrderMPRule Set SeqNo = " + lowSeqNo + " where MPRuleID = " + upIndex;
                    ((MedcineRow)pnlMed2.Controls[I]).lblSeqNo.Text = upSeqNo;
                    ((MedcineRow)pnlMed2.Controls[I - 1]).lblSeqNo.Text = lowSeqNo;

                    DB.SetPIVAsDB(str);
                    DB.SetPIVAsDB(str1);

                    pnlMed2.Controls.SetChildIndex(MedcineRow.Med, I - 1);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("请选择一个药物！");
            }  
        }

        /// <summary>
        /// 低优先级向下排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Med2Down_Click(object sender, EventArgs e)
        {
            int I;
            string str,str1,upIndex,lowIndex,upSeqNo,lowSeqNo;
            try
            {
                if (pnlMed2.Controls.Count == 0)
                    return;
                if (MedcineRow.Med == null || Convert.ToInt32(MedcineRow.SeqNo) < 150000000)
                    return;
                I = pnlMed2.Controls.GetChildIndex(MedcineRow.Med);
                if (I >= 0 & I <pnlMed2.Controls.Count -1)
                {
                    upIndex = ((MedcineRow)pnlMed2.Controls[I]).lblMedID.Text;
                    upSeqNo = ((MedcineRow)pnlMed2.Controls[I]).lblSeqNo.Text;
                    lowIndex = ((MedcineRow)pnlMed2.Controls[I + 1]).lblMedID.Text;
                    lowSeqNo = ((MedcineRow)pnlMed2.Controls[I + 1]).lblSeqNo.Text;

                    str = "Update OrderMPRule Set SeqNo = " + upSeqNo + " where MPRuleID = " + lowIndex;
                    str1 = "Update OrderMPRule Set SeqNo = " + lowSeqNo + " where MPRuleID = " + upIndex;
                    ((MedcineRow)pnlMed2.Controls[I]).lblSeqNo.Text = lowSeqNo;
                    ((MedcineRow)pnlMed2.Controls[I + 1]).lblSeqNo.Text = upSeqNo;

                    DB.SetPIVAsDB(str);
                    DB.SetPIVAsDB(str1);

                    pnlMed2.Controls.SetChildIndex(MedcineRow.Med, I + 1);
                }
                if (I >= pnlMed2.Controls.Count - 1) {
                    return;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("请选择一个药物！");
            }  
        }


        /// <summary>
        /// 每次排序钱需要进行初始值的选择，避免选取上一个panel中的控件
        /// </summary>
        private void InIMedicinePanel(Panel p)
        {
            if (p.Controls.Count > 0)
            {
                foreach (Control c in p.Controls)
                {
                    if (c is MedcineRow)
                    {
                        MedcineRow mr = (MedcineRow)c;
                    }
                }
            }
        }



        /// <summary>
        /// 高优先级添加药品按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddUp_Click(object sender, EventArgs e)
        {
            try
            {
                if(DWard.IsClick)                         
                    WardCode = DWard.DWardCode;
                
                string str = "select max(SeqNo) as SeqNo from OrderMPRule where SeqNo < 100000000 and WardCode = '"+WardCode+"'";
                DS=DB.GetPIVAsDB(str);
                if (DS.Tables[0].Rows[0]["SeqNo"].ToString() == "")
                    SeqNo = "0";
                else
                    SeqNo = DS.Tables[0].Rows[0]["SeqNo"].ToString();
                status = "add";
                MedList List = new MedList();

                
                switch (List.ShowDialog())
                {
                    case DialogResult.OK:
                        if (MedList.MeCode == null)
                            return;
                        MedcineRow r1 = new MedcineRow(pnlMed1);
                        r1.SetMed(MedList.MeID, MedList.MeCode, MedList.MeName, MedList.MeSeqNo, Convert.ToString(MedList.IsClass));
                        if (pnlMed1.Controls.Count > 0)
                        {
                            foreach (Control c in pnlMed1.Controls)
                            {
                                if (c is MedcineRow)
                                {
                                    MedcineRow med = (MedcineRow)c;
                                    if (r1.medCode == med.medCode)//避免重复添加相同的药品
                                    {
                                        //MessageBox.Show("重复！");
                                        break;
                                    }
                                    else
                                    {
                                        pnlMed1.Controls.Add(r1);
                                        ShowMedcine(WardCode);
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            pnlMed1.Controls.Add(r1);
                            ShowMedcine(WardCode);
                            break;
                        }
                	break;

                    case DialogResult.Yes:
                    MedcineRow r2 = new MedcineRow(pnlMed1);
                        r2.SetMed(MedList.MeID, MedList.MeCode, MedList.MeName, MedList.MeSeqNo, Convert.ToString(MedList.IsClass));
                        if (pnlMed1.Controls.Count > 0)
                        {
                            foreach (Control c in pnlMed1.Controls)
                            {
                                if (c is MedcineRow)
                                {
                                    MedcineRow med = (MedcineRow)c;
                                    if (r2.medCode == med.medCode)//避免重复添加相同的药品
                                    {
                                        break; 
                                    }
                                    else
                                    {
                                        pnlMed1.Controls.Add(r2);
                                        ShowMedcine(WardCode);
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            pnlMed1.Controls.Add(r2);
                            ShowMedcine(WardCode);
                            break;
                        }
                    break;
                }

                status = "";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        /// <summary>
        /// 低优先级新增药品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddLow_Click(object sender, EventArgs e)
        {
            try
            {
                if (DWard.IsClick)
                    WardCode = DWard.DWardCode;

                string str = "select max(SeqNo) as SeqNo from OrderMPRule where SeqNo > 150000000 and WardCode = '" + WardCode + "'";
                DS = DB.GetPIVAsDB(str);
                if (DS.Tables[0].Rows[0]["SeqNo"].ToString() == "")
                    SeqNo = "150000000";
                else
                    SeqNo = DS.Tables[0].Rows[0]["SeqNo"].ToString();
                status = "add";
                MedList List = new MedList();

                switch (List.ShowDialog())
                {
                    case DialogResult.OK:
                        MedcineRow r1 = new MedcineRow(pnlMed2);
                        r1.SetMed(MedList.MeID, MedList.MeCode, MedList.MeName, MedList.MeSeqNo, Convert.ToString(MedList.IsClass));
                        pnlMed2.Controls.Add(r1);
                        ShowMedcine(WardCode);
                        break;
                    case DialogResult.Yes:
                        MedcineRow r2 = new MedcineRow(pnlMed2);
                        r2.SetMed(MedList.MeID, MedList.MeCode, MedList.MeName, MedList.MeSeqNo, Convert.ToString(MedList.IsClass));
                        pnlMed2.Controls.Add(r2);
                        ShowMedcine(WardCode);
                        break;
                }

                status = "";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "delete from OrderMPRule where MPRuleID = " + MedcineRow.MedID + "; " +
                    "delete from OrderMPRuleSub where MPRuleID = " + MedcineRow.MedID;
                DB.SetPIVAsDB(str);
                if (Convert.ToInt64(MedcineRow.SeqNo) < 100000000)
                    pnlMed1.Controls.Remove(MedcineRow.Med);
                if (Convert.ToInt64(MedcineRow.SeqNo) > 150000000)
                    pnlMed2.Controls.Remove(MedcineRow.Med);
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
                But_UseAll.Text = "正在应用中...";
                But_UseAll.Enabled = false;
                DataTable dt = DB.GetPIVAsDB("select distinct WardCode From DWard where IsOpen = 1 and WardCode<>'" + WardCode + "'   ").Tables[0];
                if (dt.Rows.Count <= 0)
                    return;
                if (!Check_OnlyInse.Checked)
                {
                    DB.SetPIVAsDB("delete OrderMPRule where WardCode<>'" + WardCode+"'  ");
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DB.SetPIVAsDB("insert into OrderMPRule select IsMedClass,MedCode,MedName,SeqNo,'" + dt.Rows[i][0].ToString() + "' from OrderMPRule where WardCode= '" + WardCode+"'   ");
                }

                But_UseAll.Enabled = true;
                But_UseAll.Text = "应用到所有病区";
            }
            catch { }
          
        }

       


    }
}
