using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PIVAsDBhelp;

namespace PivasRevPre
{
    public partial class PreInfo : UserControl
    {
        public PreInfo()
        {
            InitializeComponent();
        }
        DB_Help DB = new DB_Help();
        MesBox Mes = new MesBox();
        SQL SQLStr = new SQL();
        public string Level,PreID;
        public void SetInfo(DataRow R)
        {
            lblFregCode.Text = R["FregCode"].ToString();
            lblEnd.Text = R["EndDT"].ToString();
            lblIncepDT.Text = R["InceptDT"].ToString();

            switch(R["Level"].ToString())
            {
                case "0": 
                    panel3.BackColor = Color.FromArgb(157, 245, 159);
                    panel4.BackColor = Color.FromArgb(157, 245, 159);
                    break;
                case "3": 
                    panel3.BackColor = Color.FromArgb(249, 253, 156);
                    panel4.BackColor = Color.FromArgb(249, 253, 156);
                    break;
                case "5": 
                    panel3.BackColor = Color.FromArgb(253, 212, 218);
                    panel4.BackColor = Color.FromArgb(253, 212, 218);
                    break;
            }
            Level = R["Level"].ToString();
            lblDoctor.Text = R["DEmployeeName"].ToString() + "(" + R["DoctorCode"].ToString() + ")";
            lblStart.Text = R["StartDT"].ToString();
            PreID = R["PrescriptionID"].ToString();
            lblGroupNo.Text = R["GroupNo"].ToString();
            lblp.Text = R["checker"].ToString();
            
            if (R["Attention"].ToString() == "False")
            {
                panel2.Visible = true;
                panel1.Visible = false;
            }
            else
            {
                panel1.Visible = true;
                panel2.Visible = false;
            }

            switch(R["PStatus"].ToString())
            {
                case "1":
                    pnlBack.Visible = false;
                    pnlPass.Visible = false;
                    break;
                case "2":
                    pnlPass.Visible = true;
                    pnlBack.Visible = false;
                    panel3.BackColor = Color.FromArgb(224, 224, 224);
                    panel4.BackColor = Color.FromArgb(224, 224, 224);
                    break;
                case "3":
                    pnlBack.Visible = true;
                    pnlPass.Visible = false;
                    panel3.BackColor = Color.FromArgb(224, 224, 224);
                    panel4.BackColor = Color.FromArgb(224, 224, 224);
                    break;
            }
        }        

        private void btnPass_Click(object sender, EventArgs e)
        {
            try
            {
                string ecode="",str;
                if(this.pnlPass.Visible==true)
                {
                    Mes.ShowDialog("提示","该处方已经确认！");                 
                    return;                
                }
                if(this.pnlBack.Visible==true)
                {
                    if (Mes.ShowDialog("提示","你选择了已退单的处方，确定通过？")==DialogResult.Cancel)
                    {
                        return;
                    }
                }
                string Explain="";
                if(((CheckPre)this.Parent.Parent.Parent.Parent).ShowConfirm=="1"||Level!="0")
                {
                    CPConfirm CP = new CPConfirm();
                    if (CP.ShowDialog() == DialogResult.Cancel)
                        return;
                    else
                    {
                        ecode = CP.ecode;
                        Explain = CP.DoctorExplain;
                    }
                }

                str = "EXEC bl_checkconfirm '" + PreID + "','" + ecode + "','" + Explain + "'";
                DB.SetPIVAsDB(str);
                str = "SELECT DEmployeeName FROM DEmployee WHERE DEmployeeID = " + CheckPre.EMployeeID;
                lblp.Text = DB.GetPIVAsDB(str).Tables[0].Rows[0]["DEmployeeName"].ToString();


                if (((CheckPre)this.Parent.Parent.Parent.Parent).CBAll.Checked == false)
                {
                    switch(CheckPre.per)
                    {
                        case "1":
                            Ward.W.lblUNum.Text = Convert.ToString(Convert.ToInt32(Ward.W.lblUNum.Text) - 1);
                            CheckPre.Wd.lblUNum.Text = Convert.ToString(Convert.ToInt32(CheckPre.Wd.lblUNum.Text) - 1);
                            str = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value.ToString();
                            ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value = Convert.ToString(Convert.ToInt32(str) - 1);                    
                            break;
                        case "2,3":
                            break;
                        case "3":
                            str = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value.ToString();
                            ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value = Convert.ToString(Convert.ToInt32(str) - 1);                    
                            break;
                    }                    
                    

                    if (((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value.ToString() == "0")
                    {
                        if (Ward.W.lblUNum.Text == "0")
                        {
                            ((CheckPre)this.Parent.Parent.Parent.Parent).NextWard();
                        }
                        else
                        {
                            int a = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Index;
                            ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows.RemoveAt(CheckPre.RIndex);
                            if (a != ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows.Count)
                            {
                                ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[a].Selected = true;
                                string code = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[a].Cells["PCode"].Value.ToString();
                                string time = ((CheckPre)this.Parent.Parent.Parent.Parent).DT.Text;
                                str = SQLStr.SelDetail(CheckPre.sys, CheckPre.per, code, time);
                                CheckPre.DTable = DB.GetPIVAsDB(str).Tables[0];
                                if (CheckPre.DTable.Rows.Count <= 0)
                                {
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).personInfo1.Clear();
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).p.Controls.Clear();
                                    return;
                                }
                                ((CheckPre)this.Parent.Parent.Parent.Parent).personInfo1.SetDetailInfo(CheckPre.DTable);
                                ((CheckPre)this.Parent.Parent.Parent.Parent).ShowPerDetail(CheckPre.DTable);
                            }
                            else
                            {
                                CheckPre.RIndex = a - 1;
                                ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Selected = true;
                                string code = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["PCode"].Value.ToString();
                                string time = ((CheckPre)this.Parent.Parent.Parent.Parent).DT.Text;
                                str = SQLStr.SelDetail(CheckPre.sys, CheckPre.per, code, time);
                                CheckPre.DTable = DB.GetPIVAsDB(str).Tables[0];
                                if (CheckPre.DTable.Rows.Count <= 0)
                                {
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).personInfo1.Clear();
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).p.Controls.Clear();
                                    return;
                                }
                                ((CheckPre)this.Parent.Parent.Parent.Parent).personInfo1.SetDetailInfo(CheckPre.DTable);
                                ((CheckPre)this.Parent.Parent.Parent.Parent).ShowPerDetail(CheckPre.DTable);
                            }
                        }
                    }
                    else
                    {
                        if (CheckPre.per == "1" || CheckPre.per == "3")
                            this.Dispose();
                        else
                        {
                            this.btnPass.Enabled = false;
                            this.pnlPass.Visible = true;
                            this.pnlBack.Visible = false;
                            this.btnBack.Enabled = true;
                            panel3.BackColor = Color.FromArgb(224, 224, 224);
                            panel4.BackColor = Color.FromArgb(224, 224, 224);

                        }
                    }
                }

                else
                {
                    switch (CheckPre.per)
                    {
                        case "1":
                            if (pnlBack.Visible == false)
                            {
                                Ward.W.lblUNum.Text = Convert.ToString(Convert.ToInt32(Ward.W.lblUNum.Text) - 1);
                                CheckPre.Wd.lblUNum.Text = Convert.ToString(Convert.ToInt32(CheckPre.Wd.lblUNum.Text) - 1);
                                str = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value.ToString();
                                ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value = Convert.ToString(Convert.ToInt32(str) - 1);
                            }
                            break;
                        case "2,3":
                            if (pnlBack.Visible == false&&pnlPass.Visible==false)
                            {
                                Ward.W.lblUNum.Text = Convert.ToString(Convert.ToInt32(Ward.W.lblUNum.Text) - 1);
                                CheckPre.Wd.lblUNum.Text = Convert.ToString(Convert.ToInt32(CheckPre.Wd.lblUNum.Text) - 1);
                                str = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value.ToString();
                                ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value = Convert.ToString(Convert.ToInt32(str) + 1);
                            }
                            break;
                        case "3":
                            if (pnlBack.Visible == false && pnlPass.Visible == false)
                            {
                                Ward.W.lblUNum.Text = Convert.ToString(Convert.ToInt32(Ward.W.lblUNum.Text) - 1);
                                CheckPre.Wd.lblUNum.Text = Convert.ToString(Convert.ToInt32(CheckPre.Wd.lblUNum.Text) - 1);                               
                            }
                            else
                            {
                                str = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value.ToString();
                                ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value = Convert.ToString(Convert.ToInt32(str) + 1);
                            }
                            break;
                    }                    

                    if (((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value.ToString() == "0")
                    {
                        ((CheckPre)this.Parent.Parent.Parent.Parent).CBAll.Checked = false;
                        if (Ward.W.lblUNum.Text == "0")
                        {
                            ((CheckPre)this.Parent.Parent.Parent.Parent).NextWard();
                        }
                        else
                        {
                            int a = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Index;
                            ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows.RemoveAt(CheckPre.RIndex);
                            if (a != ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows.Count)
                            {
                                ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[a].Selected = true;
                                string code = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[a].Cells["PCode"].Value.ToString();
                                string time = ((CheckPre)this.Parent.Parent.Parent.Parent).DT.Text;
                                str = SQLStr.SelDetail(CheckPre.sys, CheckPre.per, code, time);
                                CheckPre.DTable = DB.GetPIVAsDB(str).Tables[0];
                                if (CheckPre.DTable.Rows.Count <= 0)
                                {
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).personInfo1.Clear();
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).p.Controls.Clear();
                                    return;
                                }
                                ((CheckPre)this.Parent.Parent.Parent.Parent).personInfo1.SetDetailInfo(CheckPre.DTable);
                                ((CheckPre)this.Parent.Parent.Parent.Parent).ShowPerDetail(CheckPre.DTable);
                            }
                            else
                            {
                                CheckPre.RIndex = a - 1;
                                ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Selected = true;
                                string code = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["PCode"].Value.ToString();
                                string time = ((CheckPre)this.Parent.Parent.Parent.Parent).DT.Text;
                                str = SQLStr.SelDetail(CheckPre.sys, CheckPre.per, code, time);
                                CheckPre.DTable = DB.GetPIVAsDB(str).Tables[0];
                                if (CheckPre.DTable.Rows.Count <= 0)
                                {
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).personInfo1.Clear();
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).p.Controls.Clear();
                                    return;
                                }
                                ((CheckPre)this.Parent.Parent.Parent.Parent).personInfo1.SetDetailInfo(CheckPre.DTable);
                                ((CheckPre)this.Parent.Parent.Parent.Parent).ShowPerDetail(CheckPre.DTable);
                            }
                        }
                    }
                    else
                    {                       
                        this.btnPass.Enabled = false;
                        this.pnlPass.Visible = true;
                        this.pnlBack.Visible = false;
                        this.btnBack.Enabled = true;
                        panel3.BackColor = Color.FromArgb(224, 224, 224);
                        panel4.BackColor = Color.FromArgb(224, 224, 224);
                    }
                }                                         
            }
            catch(System.Exception ex)
            {
               // Mes.ShowDialog("提示",ex.Message);
            }
        }

        private void btnReCheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (Mes.ShowDialog("提示", "重审会覆盖之前的结果，确定要重审吗？") == DialogResult.Cancel)
                {
                    return;
                }

                string str = "EXEC [bl_Remonitor] '" + PreID + "'";

                DB.SetPIVAsDB(str);                
                btnBack.Enabled = true;
                btnPass.Enabled = true;

                this.pnlInfo.Controls.Clear();
                DataTable dt = new DataTable();
                str = "SELECT DISTINCT " +
                        "P.Level, PD.DrugName,PD.Quantity,DD.PiShi,P.FregCode,PD.Dosage,PD.DosageUnit,PD.Spec " +
                        "FROM  Prescription P  " +
                        "INNER JOIN PrescriptionDetail PD ON PD.PrescriptionID=P.PrescriptionID " +
                        "INNER JOIN DDrug DD ON PD.DrugCode=DD.DrugCode " +
                        "WHERE P.PrescriptionID ='" + PreID + "'";
                dt = DB.GetPIVAsDB(str).Tables[0];
                switch (dt.Rows[0]["Level"].ToString())
                {
                    case "0":
                        panel3.BackColor = Color.FromArgb(157, 245, 159);
                        panel4.BackColor = Color.FromArgb(157, 245, 159);
                        break;
                    case "3":
                        panel3.BackColor = Color.FromArgb(249, 253, 156);
                        panel4.BackColor = Color.FromArgb(249, 253, 156);
                        break;
                    case "5":
                        panel3.BackColor = Color.FromArgb(253, 212, 218);
                        panel4.BackColor = Color.FromArgb(253, 212, 218);
                        break;
                }
                Level = dt.Rows[0]["Level"].ToString();
                lblp.Text = "";

                int d = 0, b = 0; 
                int h = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SetDrug(dt.Rows[i], b * 20 + 5);
                    b++;
                }

                SetPanel(b * 20 + 10);

                Settitle(b * 20 + 20);

                h = 20 * b + 37;
               DataSet dddd = DB.GetPIVAsDB(SQLStr.SelRes(PreID));
               if (dddd.Tables[0].Rows.Count > 0)
               {
                   dt = dddd.Tables[0];
                   for (int j = 0; j < dt.Rows.Count; j++)
                   {
                       SetResult(dt.Rows[j], Convert.ToString(d + 1), ref h);
                       //SetResult(dt.Rows[j], Convert.ToString(d + 1), 40 * d + 20 * b + 37);
                       d++;
                   }

               }
               else
               {
                   //SetResult(Convert.ToString(d + 1), 40 * d + 20 * b + 37);
                   SetResult(Convert.ToString(d + 1), ref h);
                   d++;
               }
                this.Height = 39 * d + 17 * b + 82;
                if (CheckPre.per != "1")
                {                    
                    if (((CheckPre)this.Parent.Parent.Parent.Parent).CBAll.Checked == false)
                    {                        
                        Ward.W.lblUNum.Text = Convert.ToString(Convert.ToInt32(Ward.W.lblUNum.Text) + 1);
                        CheckPre.Wd.lblUNum.Text = Convert.ToString(Convert.ToInt32(CheckPre.Wd.lblUNum.Text) + 1);
                        str = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value.ToString();
                        ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value = Convert.ToString(Convert.ToInt32(str) - 1);

                        if (((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value.ToString() == "0")
                        {
                            ((CheckPre)this.Parent.Parent.Parent.Parent).CBAll.Checked = false;
                            if (Ward.W.lblUNum.Text == "0")
                            {
                                ((CheckPre)this.Parent.Parent.Parent.Parent).NextWard();
                            }
                            else
                            {
                                int a = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Index;
                                ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows.RemoveAt(CheckPre.RIndex);
                                if (a != ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows.Count)
                                {
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[a].Selected = true;

                                    string code = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[a].Cells["PCode"].Value.ToString();
                                    string time = ((CheckPre)this.Parent.Parent.Parent.Parent).DT.Text;
                                    str = SQLStr.SelDetail(CheckPre.sys, CheckPre.per, code, time);
                                    CheckPre.DTable = DB.GetPIVAsDB(str).Tables[0];
                                    if (CheckPre.DTable.Rows.Count <= 0)
                                    {
                                        ((CheckPre)this.Parent.Parent.Parent.Parent).personInfo1.Clear();
                                        ((CheckPre)this.Parent.Parent.Parent.Parent).p.Controls.Clear();
                                        return;
                                    }
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).personInfo1.SetDetailInfo(CheckPre.DTable);
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).ShowPerDetail(CheckPre.DTable);
                                }
                                else
                                {
                                    CheckPre.RIndex = a - 1;
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Selected = true;
                                    string code = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["PCode"].Value.ToString();
                                    string time = ((CheckPre)this.Parent.Parent.Parent.Parent).DT.Text;
                                    str = SQLStr.SelDetail(CheckPre.sys, CheckPre.per, code, time);
                                    CheckPre.DTable = DB.GetPIVAsDB(str).Tables[0];
                                    if (CheckPre.DTable.Rows.Count <= 0)
                                    {
                                        ((CheckPre)this.Parent.Parent.Parent.Parent).personInfo1.Clear();
                                        ((CheckPre)this.Parent.Parent.Parent.Parent).p.Controls.Clear();
                                        return;
                                    }
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).personInfo1.SetDetailInfo(CheckPre.DTable);
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).ShowPerDetail(CheckPre.DTable);
                                }
                            }
                        }
                        else
                            this.Dispose();
                    }
                    else
                    {

                        if(pnlBack.Visible)
                        {
                            Ward.W.lblUNum.Text = Convert.ToString(Convert.ToInt32(Ward.W.lblUNum.Text) + 1);
                            CheckPre.Wd.lblUNum.Text = Convert.ToString(Convert.ToInt32(CheckPre.Wd.lblUNum.Text) + 1);
                            str = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value.ToString();
                            ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value = Convert.ToString(Convert.ToInt32(str) - 1);
                        }
                        else if (pnlPass.Visible)
                        {
                            Ward.W.lblUNum.Text = Convert.ToString(Convert.ToInt32(Ward.W.lblUNum.Text) + 1);
                            CheckPre.Wd.lblUNum.Text = Convert.ToString(Convert.ToInt32(CheckPre.Wd.lblUNum.Text) + 1);                            
                        }
                        else
                        {

                        }

                        if (((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value.ToString() == "0")
                        {
                            ((CheckPre)this.Parent.Parent.Parent.Parent).CBAll.Checked = false;
                            if (Ward.W.lblUNum.Text == "0")
                            {
                                ((CheckPre)this.Parent.Parent.Parent.Parent).NextWard();
                            }
                            else
                            {
                                int a = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Index;
                                ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows.RemoveAt(CheckPre.RIndex);
                                if (a != ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows.Count)
                                {
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[a].Selected = true;

                                    string code = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[a].Cells["PCode"].Value.ToString();
                                    string time = ((CheckPre)this.Parent.Parent.Parent.Parent).DT.Text;
                                    str = SQLStr.SelDetail(CheckPre.sys, CheckPre.per, code, time);
                                    CheckPre.DTable = DB.GetPIVAsDB(str).Tables[0];
                                    if (CheckPre.DTable.Rows.Count <= 0)
                                    {
                                        ((CheckPre)this.Parent.Parent.Parent.Parent).personInfo1.Clear();
                                        ((CheckPre)this.Parent.Parent.Parent.Parent).p.Controls.Clear();
                                        return;
                                    }
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).personInfo1.SetDetailInfo(CheckPre.DTable);
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).ShowPerDetail(CheckPre.DTable);
                                }
                                else
                                {
                                    CheckPre.RIndex = a - 1;
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Selected = true;
                                    string code = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["PCode"].Value.ToString();
                                    string time = ((CheckPre)this.Parent.Parent.Parent.Parent).DT.Text;
                                    str = SQLStr.SelDetail(CheckPre.sys, CheckPre.per, code, time);
                                    CheckPre.DTable = DB.GetPIVAsDB(str).Tables[0];
                                    if (CheckPre.DTable.Rows.Count <= 0)
                                    {
                                        ((CheckPre)this.Parent.Parent.Parent.Parent).personInfo1.Clear();
                                        ((CheckPre)this.Parent.Parent.Parent.Parent).p.Controls.Clear();
                                        return;
                                    }
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).personInfo1.SetDetailInfo(CheckPre.DTable);
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).ShowPerDetail(CheckPre.DTable);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (((CheckPre)this.Parent.Parent.Parent.Parent).CBAll.Checked)
                    {
                        if (this.pnlBack.Visible || this.pnlPass.Visible)
                        {
                            Ward.W.lblUNum.Text = Convert.ToString(Convert.ToInt32(Ward.W.lblUNum.Text) + 1);
                            CheckPre.Wd.lblUNum.Text = Convert.ToString(Convert.ToInt32(CheckPre.Wd.lblUNum.Text) + 1);
                            str = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value.ToString();
                            ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value = Convert.ToString(Convert.ToInt32(str) + 1);
                        }
                    }
                }
                this.pnlBack.Visible = false;
                this.pnlPass.Visible = false;
            }
            catch (System.Exception ex)
            {
               // Mes.ShowDialog("提示", ex.Message);
            }

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {         
                BackPre b = new BackPre();
                if (b.ShowDialog() == DialogResult.OK)
                {
                    string str = "EXEC bl_backPrescription " + PreID + ",'" + CheckPre.EMployeeID + "','" + b.strExplain + "'";
                    DB.SetPIVAsDB(str);
                    str = "SELECT DEmployeeName FROM DEmployee WHERE DEmployeeID = " + CheckPre.EMployeeID;
                    lblp.Text = DB.GetPIVAsDB(str).Tables[0].Rows[0]["DEmployeeName"].ToString();


                    if (((CheckPre)this.Parent.Parent.Parent.Parent).CBAll.Checked == false)
                    {

                        switch (CheckPre.per)
                        {
                            case "1":
                                Ward.W.lblUNum.Text = Convert.ToString(Convert.ToInt32(Ward.W.lblUNum.Text) - 1);
                                CheckPre.Wd.lblUNum.Text = Convert.ToString(Convert.ToInt32(CheckPre.Wd.lblUNum.Text) - 1);
                                str = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value.ToString();
                                ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value = Convert.ToString(Convert.ToInt32(str) - 1);
                                break;
                            case "2,3":                                
                                break;
                            case "3":                                
                                break;
                        }

                        if (((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value.ToString() == "0")
                        {
                            ((CheckPre)this.Parent.Parent.Parent.Parent).CBAll.Checked = false;
                            if (Ward.W.lblUNum.Text == "0")
                            {
                                ((CheckPre)this.Parent.Parent.Parent.Parent).NextWard();
                            }
                            else
                            {
                                int a = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Index;
                                ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows.RemoveAt(CheckPre.RIndex);
                                if (a != ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows.Count)
                                {
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[a].Selected = true;
                                    string code = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[a].Cells["PCode"].Value.ToString();
                                    string time = ((CheckPre)this.Parent.Parent.Parent.Parent).DT.Text;
                                    str = SQLStr.SelDetail(CheckPre.sys, CheckPre.per, code, time);
                                    CheckPre.DTable = DB.GetPIVAsDB(str).Tables[0];
                                    if (CheckPre.DTable.Rows.Count <= 0)
                                    {
                                        ((CheckPre)this.Parent.Parent.Parent.Parent).personInfo1.Clear();
                                        ((CheckPre)this.Parent.Parent.Parent.Parent).p.Controls.Clear();
                                        return;
                                    }
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).personInfo1.SetDetailInfo(CheckPre.DTable);
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).ShowPerDetail(CheckPre.DTable);
                                }
                                else
                                {
                                    CheckPre.RIndex = a - 1;
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Selected = true;
                                    string code = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["PCode"].Value.ToString();
                                    string time = ((CheckPre)this.Parent.Parent.Parent.Parent).DT.Text;
                                    str = SQLStr.SelDetail(CheckPre.sys, CheckPre.per, code, time);
                                    CheckPre.DTable = DB.GetPIVAsDB(str).Tables[0];
                                    if (CheckPre.DTable.Rows.Count <= 0)
                                    {
                                        ((CheckPre)this.Parent.Parent.Parent.Parent).personInfo1.Clear();
                                        ((CheckPre)this.Parent.Parent.Parent.Parent).p.Controls.Clear();
                                        return;
                                    }
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).personInfo1.SetDetailInfo(CheckPre.DTable);
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).ShowPerDetail(CheckPre.DTable);
                                }
                            }
                        }

                        else
                        {
                            if (CheckPre.per == "1")
                                this.Dispose();
                            else
                            {
                                this.pnlBack.Visible = true;
                                this.pnlPass.Visible = false;
                                cbbSelect.Visible = false;
                                btnBack.Enabled = false;
                                btnPass.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        switch (CheckPre.per)
                        {
                            case "1":
                                if (pnlPass.Visible == false && pnlBack.Visible == false)
                                {
                                    Ward.W.lblUNum.Text = Convert.ToString(Convert.ToInt32(Ward.W.lblUNum.Text) - 1);
                                    CheckPre.Wd.lblUNum.Text = Convert.ToString(Convert.ToInt32(CheckPre.Wd.lblUNum.Text) - 1);
                                    str = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value.ToString();
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value = Convert.ToString(Convert.ToInt32(str) - 1);
                                }                               
                                break;
                            case "2,3":
                                if (this.pnlPass.Visible == false && this.pnlBack.Visible == false)
                                {
                                    Ward.W.lblUNum.Text = Convert.ToString(Convert.ToInt32(Ward.W.lblUNum.Text) - 1);
                                    CheckPre.Wd.lblUNum.Text = Convert.ToString(Convert.ToInt32(CheckPre.Wd.lblUNum.Text) - 1);
                                    str = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value.ToString();
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value = Convert.ToString(Convert.ToInt32(str) + 1);
                                }
                                break;
                            case "3":
                                if (this.pnlPass.Visible)
                                {
                                    str = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value.ToString();
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value = Convert.ToString(Convert.ToInt32(str) + 1);
                                }
                                else
                                {
                                    Ward.W.lblUNum.Text = Convert.ToString(Convert.ToInt32(Ward.W.lblUNum.Text) - 1);
                                    CheckPre.Wd.lblUNum.Text = Convert.ToString(Convert.ToInt32(CheckPre.Wd.lblUNum.Text) - 1);
                                    str = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value.ToString();
                                    ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Cells["U"].Value = Convert.ToString(Convert.ToInt32(str) + 1);
                                }
                                break;
                        }
                        panel3.BackColor = Color.FromArgb(224, 224, 224);
                        panel4.BackColor = Color.FromArgb(224, 224, 224);
                        this.cbbSelect.Visible = false;
                        if (((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.CurrentRow.Cells["U"].Value.ToString() == "0")
                        {
                            ((CheckPre)this.Parent.Parent.Parent.Parent).CBAll.Checked = false;
                            int a = ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[CheckPre.RIndex].Index;
                            ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows.RemoveAt(a);
                            ((CheckPre)this.Parent.Parent.Parent.Parent).dgvPat.Rows[a].Selected = true;
                        }
                        this.btnBack.Enabled = false;
                        this.pnlBack.Visible = true;
                        this.pnlPass.Visible = false;
                        btnPass.Enabled = true;
                        if (Ward.W.lblUNum.Text == "0")
                        {
                            ((CheckPre)this.Parent.Parent.Parent.Parent).NextWard();
                        }
                    }                    
                }
            }
            catch (System.Exception ex)
            {
                //Mes.ShowDialog("提示", ex.Message); 
            }
        }    


        /// <summary>
        /// 审方不通过
        /// </summary>
        /// <param name="R"></param>
        /// <param name="top"></param>
        public void SetDrug(DataRow R,int top)
        {
            DetailDrug Drug = new DetailDrug();
            Drug.setDrug(R);
            Drug.Top = top;
            Drug.Width = pnlInfo.Width - 20;
            pnlInfo.Controls.Add(Drug);

        }

        /// <summary>
        /// 审方通过
        /// </summary>
        /// <param name="a"></param>
        /// <param name="top"></param>
        public void SetResult(string a,ref int top)
        {
            Label Label_txt = new Label();
            Label_txt.Text = " 通过 ！ ";
            Label_txt.ForeColor = Color.Green;
            Label_txt.Font = new System.Drawing.Font("宋体", 11, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            Label_txt.Top = top;
            Label_txt.Left = 90;
            //Label_txt.AutoSize = false;
            Label_txt.Width = pnlInfo.Width-20;
           // Label_txt.Height = 50;
          //  Label_txt.Margin = new System.Windows.Forms.Padding(10);
            Control[] label=pnlInfo.Controls.Find("LResult", false);
            if (label.Length > 0)
            {
                Label Result = (Label)label[0];
                Label_txt.Location = new Point(Label_txt.Location.X,Result.Location.Y+Result.Height);
            }
            top = Label_txt.Top + Label_txt.Height + 20;
            
            pnlInfo.Controls.Add(Label_txt);
            
            //pnlInfo.Controls.Add(r);
        }

        public void SetResult(DataRow R, string a,ref int top)
        {
            result r = new result();
            if (R["CensorItem"].ToString() != "" && R["ReferenName"].ToString() != "")
            {
                r.setResult(R);
               // r.lblDesc.MaximumSize = new Size(r.Width - lblDesc.Location.Y, 0);
            }
            else
            {
                r.setResult();
            }
           // r.lblNum.Text = a;
            r.Top = top;
            r.Left = 33;
            r.Width = pnlInfo.Width - 20;
            top=r.Top+r.Height+2;
           // r.lblDesc.MaximumSize = new Size(r.Width - r.lblDesc.Location.Y, 0);
            pnlInfo.Controls.Add(r);
        }        
                         
        public void Settitle(int top)
        {
            Label l = new Label();
            l.Text = "审方结果:";
            l.Font = new Font("宋体", 11);
            l.ForeColor = Color.Black;
            l.Name = "LResult";
            l.Height = 17;
            l.Width = 320;
            l.Top = top;
            l.Left = 33;
            pnlInfo.Controls.Add(l);
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            DB.SetPIVAsDB("Update Prescription Set Attention = 1 Where PrescriptionID = " + PreID);
            panel1.Visible = true;
            panel2.Visible = false;
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            DB.SetPIVAsDB("Update Prescription Set Attention = 0 Where PrescriptionID = " + PreID);
            panel1.Visible = false;
            panel2.Visible = true;
        }

        private void pnlInfo_Click(object sender, EventArgs e)
        {
            this.Focus();
            ((Panel)this.Parent).HorizontalScroll.Value = 0;
        }

        private void pnlInfo_SizeChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < pnlInfo.Controls.Count; i++)
            {
                pnlInfo.Controls[i].Width = pnlInfo.Width - 10;
            }
        }

        public void SetPanel(int top)
        {
            Panel p = new Panel();
            p.BackColor = Color.Gray;
            p.Height = 1;
            p.Width = pnlInfo.Width - 10;
            p.Left = 5;
            p.Top = top;
            pnlInfo.Controls.Add(p);
        }

        public void Dispose()
        {
            base.Dispose();
        }
      
    }
}
