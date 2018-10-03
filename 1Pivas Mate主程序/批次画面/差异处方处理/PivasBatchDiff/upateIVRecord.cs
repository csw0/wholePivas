using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;

namespace PivasBatchDiff
{
    public partial class upateIVRecord : Form
    {
        
        DataTable  dtWard = new DataTable (); 
        //DataSet dw = new DataSet();
        private string UserID = string.Empty;
        public upateIVRecord()
        {
            InitializeComponent();
        }

        DB_Help db = new DB_Help();

        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion

        public upateIVRecord(string date,string ID)
        {
            InitializeComponent();
            this.lbDate.Text = date;
            UserID = ID;
        }
        const int HTLEFT = 10;
        const int HTRIGHT = 11;
        const int HTTOP = 12;
        const int HTTOPLEFT = 13;
        const int HTTOPRIGHT = 14;
        const int HTBOTTOM = 15;
        const int HTBOTTOMLEFT = 0x10;
        const int HTBOTTOMRIGHT = 17;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0084:
                    base.WndProc(ref m);
                    Point vPoint = new Point((int)m.LParam & 0xFFFF,
                        (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPLEFT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMLEFT;
                        else m.Result = (IntPtr)HTLEFT;
                    else if (vPoint.X >= ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPRIGHT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)HTRIGHT;
                    else if (vPoint.Y <= 5)
                        m.Result = (IntPtr)HTTOP;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)HTBOTTOM;
                    break;
                case 0x0201://鼠标左键按下的消息 
                    m.Msg = 0x00A1;//更改消息为非客户区按下鼠标 
                    m.LParam = IntPtr.Zero;//默认值 
                    m.WParam = new IntPtr(2);//鼠标放在标题栏内 
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }


        private void updateIVRecord_Load(object sender, EventArgs e)
        {

            if (Limit(UserID, "PivasDWard"))
            {
                DataSet nm = new DataSet();
                label9.Text = "";
                string str1 = "select DEmployeeName from DEmployee where DEMployeeID = " + UserID + "";
                nm = db.GetPIVAsDB(str1);
                label9.Text = nm.Tables[0].Rows[0][0].ToString();
                //DataSet dtWard = new DataSet();
     
                if (lbDate.Text == "")
                {
                    MessageBox.Show("请选择日期");
                    return;
                }
                else
                {
                    int l=getDwardSelect (lbDate.Text.ToString()).Tables[0].Rows.Count;
                    //dtWard = getDwardSelect(lbDate.Text.ToString()).Tables[0];
                    if (l <= 0)
                    {

                        dgvWard.DataSource = null;
                        Dgv_Info.DataSource = null;
                        DgvDrug.DataSource = null;
                        MessageBox.Show("当日已没有不匹配瓶签药");
                        this.Dispose();
                    }
                    else
                    {
                        load();
                    }
                }
           



            //StringBuilder sql = new StringBuilder();
            //sql.Append(" select  iv.WardCode,dw.WardSimName as WardName, COUNT(LabelNo) as Count from ivrecord iv ");
            //sql.Append(" inner join DWard dw on iv.WardCode=dw.WardCode");
            //sql.Append(" where Remark2='-1' and DATEDIFF(DD,InfusionDT,'"+lbDate.Text+"')=15");
            //sql.Append(" group by iv.WardCode,dw.WardSimName,dw.WardSeqNo order by dw.WardSeqNo");
            //dtWard = db.GetPIVAsDB(sql.ToString());

            //int all = 0;
            //dtWard = getDwardSelect(lbDate.Text.ToString()).Tables[0];
            //if (dtWard.Rows.Count <= 0)
            //    return;

            //for (int i = 0; i < dtWard.Rows.Count; i++)
            //    all = all + int.Parse(dtWard.Rows[i]["Count"].ToString());
            //DataRow dr = dtWard.NewRow();
            //dr["WardName"] = "<全部>";
            //dr["WardCode"] = "<全部>";
            //dr["Count"] = all;
            //dtWard.Rows.InsertAt(dr,0);
            //dgvWard.DataSource = dtWard;


            //dgvWard.Rows[0].Selected = true;

            //dgvWard_Click(null, null);
            }
            else
            {
                MessageBox.Show("您没有操作权限，请与管理员联系！");
                this.Dispose();
            }

        }
        /// <summary>
        /// 页面载入
        /// </summary>
        public void load()
        {
            int all = 0;
            dtWard = getDwardSelect(lbDate.Text.ToString()).Tables[0];             
                //dgvWard.DataSource = null;
                //Dgv_Info.DataSource = null;
                //DgvDrug.DataSource = null;
                for (int i = 0; i < dtWard.Rows.Count; i++)
                    all = all + int.Parse(dtWard.Rows[i]["Count"].ToString());
                DataRow dr = dtWard.NewRow();
                dr["WardName"] = "<全部>";
                dr["WardCode"] = "<全部>";
                dr["Count"] = all;
                dtWard.Rows.InsertAt(dr, 0);
                dgvWard.DataSource = dtWard;


                dgvWard.Rows[0].Selected = true;

                dgvWard_Click(null, null);           

        }
        /// <summary>
        /// 获取初始化数据
        /// </summary>
        /// <param name="lbdate"></param>
        /// <returns></returns>
        public DataSet getDwardSelect(string lbdate)
        {
            DataSet ds = new DataSet();
            StringBuilder sql = new StringBuilder();
            sql.Append(" select  iv.WardCode,dw.WardSimName as WardName, COUNT(LabelNo) as Count from ivrecord iv ");
            sql.Append(" inner join DWard dw on iv.WardCode=dw.WardCode");
            sql.Append(" where Remark2='-1' and DATEDIFF(DD,InfusionDT,'" + lbdate + "')=0");
            sql.Append(" group by iv.WardCode,dw.WardSimName,dw.WardSeqNo order by dw.WardSeqNo");
            ds = db.GetPIVAsDB(sql.ToString());
            return ds;

        }
        /// <summary>
        /// 权限
        /// </summary>
        /// <param name="DEmployeeID"></param>
        /// <param name="LimitName"></param>
        /// <returns></returns>
        public bool Limit(string DEmployeeID, string LimitName)
        {
            try
            {
                DataSet ds = new DataSet();
                string str = "select * from ManageLimit where DEMployeeID = " + DEmployeeID + " and LimitName = '" + LimitName + "'";
                //MessageBox.Show(str);
                ds = db.GetPIVAsDB(str);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                ds.Dispose();
                return false;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        /// <summary>
        /// 显示病区数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvWard_Click(object sender, EventArgs e)      
        {
            
            if (dgvWard.Rows.Count == 0)
                return;

            int i = dgvWard.CurrentCell.RowIndex;//获取选中行的行号


            if (dgvWard.Rows[i].Cells[0].EditedFormattedValue.ToString().Equals("False"))
                dgvWard.Rows[i].Cells[0].Value = true;
            else
                dgvWard.Rows[i].Cells[0].Value = false;


            //全选的情况
            if (dgvWard.Rows[i].Cells[1].Value.ToString() == "<全部>")
            {
                for (int j = 1; j < dgvWard.Rows.Count; j++)
                {
                    if (dgvWard.Rows[i].Cells[0].EditedFormattedValue.ToString().Equals("False"))
                    {
                        dgvWard.Rows[j].Cells[0].Value = false;
                    }
                    else
                    {
                        dgvWard.Rows[j].Cells[0].Value = true;
                    }
                }
            }
            bdDgvInfo(getWardCode());
        }


        /// <summary>
        /// 获取病区数据
        /// </summary>
        /// <returns></returns>
        private string  getWardCode() 
        {
            string WardCode="";
            foreach (DataGridViewRow dr in dgvWard.Rows) 
            {
                if (dr.Cells[0].EditedFormattedValue.ToString().Equals("True"))
                    WardCode = WardCode == "" ? dr.Cells["WardCode"].Value.ToString() : WardCode+"','" + dr.Cells["WardCode"].Value.ToString() ;
            }
            return WardCode;
        }


        /// <summary>
        /// 显示病人信息
        /// </summary>
        /// <param name="wardcode"></param>
        private void bdDgvInfo(string wardcode) 
        {
            Dgv_Info.DataSource = null;
            if (wardcode == "")
                return;
            StringBuilder str = new StringBuilder();
            DataTable dtInfo = new DataTable();
            str.Append("  select distinct i.BedNo 床号,i.PatName 姓名,Batch 批次,FreqName 频序,(i.WardName)+'('+i.WardCode+')' 病区,");
            str.Append("  InfusionDT 用药时间,i.GroupNo 组号, LabelNo 瓶签号,PatCode 病人编码");
            str.Append("  from dbo.IVRecord i ");
            str.Append("   where remark2='-1'and datediff(dd,InfusionDT,'"+lbDate.Text.Trim()+"')=0 and i.WardCode in('"+wardcode+"') ");
            str.Append("  order by i.BedNo,PatCode ");
            dtInfo = db.GetPIVAsDB(str.ToString()).Tables[0];
            
            Dgv_Info.DataSource = dtInfo;
            allCheck();//全选

            if (Dgv_Info.Rows.Count > 0) 
            {
                Dgv_Info.Rows[0].Selected = true; 
            }
            Dgv_Info_Click(null, null);
        }

        /// <summary>
        /// 显示瓶签药与处方药信息
        /// </summary>
        /// <param name="labelno"></param>
        private void bdDgvDrug(string labelno ) 
        {
            DgvDrug.DataSource = null;
            StringBuilder str = new StringBuilder();
            DataTable dtdrug = new DataTable();
            str.Append(" select a.DrugName as 瓶签药,b.DrugName as 处方药,PrescriptionDetailID from ");
            str.Append(" (select distinct iv.DrugCode,iv.DrugName from IVRecord i join IVRecordDetail iv on i.IVRecordID=iv.IVRecordID ");
            str.Append(" where LabelNo='" + labelno  + "') a");
            str.Append(" full join ");
            str.Append(" (select  distinct PrescriptionDetailID,DrugCode,DrugName from PrescriptionDetail p join IVRecord iv on p.PrescriptionID=iv.PrescriptionID where iv.LabelNo = '" + labelno + "') b");
            str.Append(" on a.DrugCode=b.DrugCode where a.DrugName is not null or b.DrugName is not null");
            dtdrug = db.GetPIVAsDB(str.ToString()).Tables[0];
            if (dtdrug.Rows.Count <= 0)
                return;
            DgvDrug.DataSource = dtdrug;
            DgvDrug.Columns[0].AutoSizeMode =DgvDrug.Columns[1].AutoSizeMode =DataGridViewAutoSizeColumnMode.Fill;
            DgvDrug.Columns[0].FillWeight = DgvDrug.Columns[1].FillWeight = 50;
            DgvDrug.Columns[2].Visible = false;
            
        }

        private void Dgv_Info_Click(object sender, EventArgs e)
        {
            if (Dgv_Info.Rows.Count <= 0)
            {
                DgvDrug.DataSource = null;
                return;
            }
            else
            {

                bdDgvDrug(Dgv_Info.CurrentRow.Cells["瓶签号"].Value.ToString());
            }
        }

        private void Panel_Close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n=0;
            for (int i = 0; i < Dgv_Info.Rows.Count; i++)
            {
                if (Dgv_Info.Rows[i].Cells[0].Value.ToString() == "True")
                    n++;
            }
            if (n == 0)
            {
                MessageBox.Show("没有勾选项");
            }
            else
            {
                Confirm confirm = new Confirm("1");
                confirm.check += new Confirm.NewDelegate(Confirm);
                confirm.ShowDialog();
            }
        }
        /// <summary>
        /// 通过界面
        /// </summary>
        /// <param name="code"></param>
        /// <param name="txt"></param>
        private void Confirm(string code,string txt,string rmk2) 
        {
            try
            {
                string labelNo = "";
                foreach (DataGridViewRow dr in Dgv_Info.Rows)
                {
                    if (dr.Cells[0].Value.ToString() == "True")
                        labelNo = labelNo == "" ? dr.Cells["瓶签号"].Value.ToString() : labelNo + "','" + dr.Cells["瓶签号"].Value.ToString();
                }
                if (labelNo == "")
                    return;
                string sql1 = " update IVRecord set Remark2='"+ rmk2 +"' where LabelNo in ('{0}')";
                string sql2 = " insert into IVRecordUpdateLog select '" + code + "',GETDATE(), labelno,1,'" + txt.Trim() + "',null,null from IVRecord where  LabelNo in ('{0}')";
                db.SetPIVAsDB(string.Format((sql1 + sql2), labelNo));
                if (dtWard.Rows.Count <= 0)
                {

                    dgvWard.DataSource = null;
                    Dgv_Info.DataSource = null;
                    DgvDrug.DataSource = null;
                    MessageBox.Show("当日已没有不匹配瓶签药");
                    return;
                }
                load();
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }

            bdDgvInfo(getWardCode());
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                for (int i = 0; i < Dgv_Info.Rows.Count; i++)
                {
                    Dgv_Info.Rows[i].Cells[0].Value = true;
                }
            }
            else 
            {
                for (int i = 0; i < Dgv_Info.Rows.Count; i++)
                {
                    Dgv_Info.Rows[i].Cells[0].Value = false;
                }
            }
        }

        /// <summary>
        /// 全选
        /// </summary>
        private void allCheck() 
        {
            if (checkBox1.Checked == true)
            {
                for (int i = 0; i < Dgv_Info.Rows.Count; i++)
                {
                    Dgv_Info.Rows[i].Cells[0].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < Dgv_Info.Rows.Count; i++)
                {
                    Dgv_Info.Rows[i].Cells[0].Value = false;
                }
            }
        }

      

       

        private void Dgv_Info_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0)
                return;

            if (Dgv_Info.CurrentRow.Cells[0].Value.ToString() == "True")
                Dgv_Info.CurrentRow.Cells[0].Value = false;
            else
                Dgv_Info.CurrentRow.Cells[0].Value = true;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void Panel_Close_MouseHover(object sender, EventArgs e)
        {
            Panel_Close.BackColor = Color.Red;
        }

        private void Panel_Close_MouseLeave(object sender, EventArgs e)
        {
            Panel_Close.BackColor = Color.Transparent;

        }

        private void dgvWard_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        /// <summary>
        /// 补足
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
           // string labelno = string.Empty;
           List<string> b=new List<string>();
            int n = 0;
            if (dtWard.Rows.Count > 1)
            {
                for (int i = 0; i < Dgv_Info.Rows.Count; i++)            //遍历打钩项
                {
                    if (Dgv_Info.Rows[i].Cells[0].Value.ToString() == "True")
                    {
                        //labelno = "'" + Dgv_Info.Rows[i].Cells["瓶签号"].Value.ToString() + "'," + labelno;
                        b.Add(Dgv_Info.Rows[i].Cells["瓶签号"].Value.ToString());
                        n++;
                        bdDgvDrug(Dgv_Info.Rows[i].Cells["瓶签号"].Value.ToString());
                        if (DgvDrug.RowCount != 0)
                        {
                            for (int a = 0; a < DgvDrug.RowCount; a++)
                            {
                                if (DgvDrug.Rows[a].Cells[0].Value.ToString() != DgvDrug.Rows[a].Cells[1].Value.ToString())
                                {
                                    dtWard = new DataTable();

                                    StringBuilder str = new StringBuilder();
                                    str.Append("insert into IVRecordDetail");
                                    str.Append(" Select distinct a.IVRecordID, b.InceptDT,b.DrugCode,b.DrugName,b.Spec,b.Dosage,");
                                    str.Append("b.DosageUnit,b.Remark8 as DgNo,-1 as ReturnFromHis,b.Remark7,b.Remark8,b.Remark9,b.Remark10,b.RecipeNo");
                                    str.Append(" from");
                                    str.Append(" (select * from PrescriptionDetail where PrescriptionDetailID='" + DgvDrug.Rows[a].Cells[2].Value.ToString() + "') b join");
                                    str.Append(" (select ivrecordid,PrescriptionID from IVRecord where PrescriptionID= ");
                                    str.Append(" (select PrescriptionID from IVRecord where LabelNo = '" + Dgv_Info.Rows[i].Cells["瓶签号"].Value.ToString() + "'))a");
                                    str.Append("  on a.PrescriptionID=b.PrescriptionID ");
                                    db.SetPIVAsDB(str.ToString());
                                }

                            }
                            //string str1 = "update  IVRecord set Remark2='1' where LabelNo ='" + Dgv_Info.CurrentRow.Cells["瓶签号"].Value.ToString() + "'";
                            //db.SetPIVAsDB(str1.ToString());

                            //dgvWard.Rows[0].Selected = true;

                            //dgvWard_Click(null, null);
                        }
                    }
                }
                if (n == 0)
                {
                    MessageBox.Show("没有勾选项");
                }
                else
                {
                MessageBox.Show("补足完成");
                }
            }
            else
            {

                //dgvWard.DataSource = null;
                //Dgv_Info.DataSource = null;
                //DgvDrug.DataSource = null;
                MessageBox.Show("当日已没有不匹配瓶签药");
                return;
               
            }
            load();
            //foreach (DataGridViewRow dr in Dgv_Info.Rows)
            //{
            //    if (labelno.Contains(dr.Cells["瓶签号"].Value.ToString()))
            //        dr.Cells[0].Value = true;
            //}
            for (int j = 0; j < Dgv_Info.Rows.Count; j++)
            {
                for (int i = 0; i < b.Count; i++)
                {
                    if (Dgv_Info.Rows[j].Cells["瓶签号"].Value.ToString() == b[i])
                    {
                        Dgv_Info.Rows[j].Cells[0].Value = true;
                        break;
                    }
                    else
                    {
                        Dgv_Info.Rows[j].Cells[0].Value = false;
                    }
                }
            }
        }            
        /// <summary>
        /// 废弃
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            int n = 0;
            for (int i = 0; i < Dgv_Info.Rows.Count; i++)
            {
                if (Dgv_Info.Rows[i].Cells[0].Value.ToString() == "True")
                    n++;
            }
            if (n == 0)
            {
                MessageBox.Show("没有勾选项");
            }
            else
            {
                Confirm confirm = new Confirm("-2");
                confirm.check += new Confirm.NewDelegate(Confirm);
                confirm.ShowDialog();
            }   

            //if (DgvDrug.RowCount != 0)
            //{
            //    StringBuilder str = new StringBuilder();
            //    //str.Append("delete from IVRecordDetail where IVRecordID=(select IVRecordID  from IVRecord where LabelNo ='" + Dgv_Info.CurrentRow.Cells["瓶签号"].Value.ToString() + "')");
            //    str.AppendLine("update  IVRecord set Remark2='-2' where LabelNo ='" + Dgv_Info.CurrentRow.Cells["瓶签号"].Value.ToString() + "'");
            //    db.SetPIVAsDB(str.ToString());
            //    MessageBox.Show("瓶签已废弃");
               
            //}
            //if (dtWard.Rows.Count <= 1)
            //{

            //    //dgvWard.DataSource = null;
            //    //Dgv_Info.DataSource = null;
            //    //DgvDrug.DataSource = null;
            //    MessageBox.Show("当日已没有不匹配瓶签药");
            //    return;
            //}
            //load();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //dgvWard.DataSource = null;
            //Dgv_Info.DataSource = null;
            //DgvDrug.DataSource = null;
            load();
        }
    }

}
