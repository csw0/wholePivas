using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using PIVAsCommon.Helper;

namespace PivasRevPre
{
    public partial class Med : UserControl
    {
        public Med()
        {
            InitializeComponent();
        }

        SQL SQLStr = new SQL();
        DB_Help DB = new DB_Help();

        private void pnlGroup_Click(object sender, EventArgs e)
        {
            pnlGroup.Focus();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        public void clear()
        {
            //pnlGroup.Controls.Clear();
            dgvDrugsInfo.Rows.Clear();
        }

        /// <summary>
        /// 设置背景色选择
        /// </summary>
        /// <param name="colorNow"></param>
        /// <returns></returns>
        private Color changeColor(Color colorNow)
        {
            Color color1 = Color.FromArgb(220, 244, 255);
            Color bgColor = color1;
            Color color2 = Color.FromArgb(220, 220, 255);
            if (colorNow == color1)
            {
                return color2;
            }
            else
            {
                return color1;
            }
        }

        /// <summary>
        /// 设置背景色
        /// </summary>
        /// <param name="i"></param>
        private void SetBgColor(int i,Color bgColor)
        {
            dgvDrugsInfo.Rows[i].Cells["colGroupNo"].Style.BackColor = bgColor;
            dgvDrugsInfo.Rows[i].Cells["colBatch"].Style.BackColor = bgColor;
            dgvDrugsInfo.Rows[i].Cells["colCPresult"].Style.BackColor = bgColor;
            dgvDrugsInfo.Rows[i].Cells["colDrugName"].Style.BackColor = bgColor;
            dgvDrugsInfo.Rows[i].Cells["colDosage"].Style.BackColor = bgColor;
        }


        private string T(string temp)
        {
            while (true)
            {
                if (temp == "")
                    break;
                if (temp[temp.Length - 1] == '0')
                    temp = temp.Remove(temp.Length - 1);
                else
                    break;
            }

            if (temp != "" && temp[temp.Length - 1] == '.')
            {
                temp = temp.Remove(temp.Length - 1);
            }
            return temp;
        }

        public void AddMed(string Pcode)//显示药品
        {
            try
            {
                string str = SQLStr.SelDrug(Pcode);
                DataSet ds = DB.GetPIVAsDB(str);
                dgvDrugsInfo.Rows.Clear();
                string groupNo1 = "";
                string groupNo2 = "";
                Color color1 = Color.FromArgb(220, 244, 255);
                Color bgColor = color1;
                Color color2 = Color.FromArgb(220, 220, 255);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    groupNo1 = dt.Rows[0]["GroupNo"].ToString();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dgvDrugsInfo.Rows.Add(1);
                        
                        groupNo2= dt.Rows[i]["GroupNo"].ToString();
                        if (i == 0)//第一行的情况，直接添加
                        {
                            dgvDrugsInfo.Rows[i].Cells["colGroupNo"].Value = dt.Rows[i]["GroupNo"].ToString();
                            dgvDrugsInfo.Rows[i].Cells["colBatch"].Value = dt.Rows[i]["Batch"].ToString();

                            switch (dt.Rows[i]["PStatus"].ToString())
                            {
                                case "2": dgvDrugsInfo.Rows[i].Cells["colCPresult"].Value = "审核通过"; break;
                                case "3": dgvDrugsInfo.Rows[i].Cells["colCPresult"].Value = "审核未过"; break;
                                default: dgvDrugsInfo.Rows[i].Cells["colCPresult"].Value = "未审"; break;
                            }

                            dgvDrugsInfo.Rows[i].Cells["colDrugName"].Value = dt.Rows[i]["DrugName"].ToString();
                            dgvDrugsInfo.Rows[i].Cells["colDosage"].Value = T(dt.Rows[i]["Dosage"].ToString()).ToString() + dt.Rows[i]["DosageUnit"].ToString();

                            SetBgColor(i,bgColor);
                        }
                        else//从第二行开始判断是否和之前的组号一样，一样的不显示组号和频次
                        {
                            if (groupNo1 == groupNo2)
                            {
                                dgvDrugsInfo.Rows[i].Cells["colGroupNo"].Value = "";
                                dgvDrugsInfo.Rows[i].Cells["colBatch"].Value = "";
                                dgvDrugsInfo.Rows[i].Cells["colDrugName"].Value = dt.Rows[i]["DrugName"].ToString();
                                dgvDrugsInfo.Rows[i].Cells["colDosage"].Value = T(dt.Rows[i]["Dosage"].ToString()).ToString() + dt.Rows[i]["DosageUnit"].ToString();

                                SetBgColor(i, bgColor);
                            }
                            else
                            {
                                bgColor = changeColor(bgColor);
                                dgvDrugsInfo.Rows[i].Cells["colGroupNo"].Value = dt.Rows[i]["GroupNo"].ToString();
                                dgvDrugsInfo.Rows[i].Cells["colBatch"].Value = dt.Rows[i]["Batch"].ToString();

                                switch (dt.Rows[i]["PStatus"].ToString())
                                {
                                    case "2": dgvDrugsInfo.Rows[i].Cells["colCPresult"].Value = "审核通过"; break;
                                    case "3": dgvDrugsInfo.Rows[i].Cells["colCPresult"].Value = "审核未过"; break;
                                    default: dgvDrugsInfo.Rows[i].Cells["colCPresult"].Value = "未审"; break;
                                }

                                dgvDrugsInfo.Rows[i].Cells["colDrugName"].Value = dt.Rows[i]["DrugName"].ToString();
                                dgvDrugsInfo.Rows[i].Cells["colDosage"].Value = T(dt.Rows[i]["Dosage"].ToString()).ToString() + dt.Rows[i]["DosageUnit"].ToString();

                                SetBgColor(i, bgColor);
                            }
                        }

                        groupNo1 = groupNo2;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            #region 旧的方法
            //try
            //{
            //    string str = SQLStr.SelDrug(Pcode);
            //    DataTable dt = new DataTable();
            //    DataSet ds = DB.GetPIVAsDB(str);
            //    Color color = new Color();
            //    Group G = null;
            //    AllDrug D = null;
            //    pnlGroup.Controls.Clear();

            //    if (ds != null && ds.Tables[0].Rows.Count > 0)
            //    {
            //        dt = ds.Tables[0];
            //        bool s = true;
            //        color = Color.FromArgb(220, 244, 255);
            //        string gro = dt.Rows[0]["GroupNo"].ToString();
            //        G = new Group();
            //        G.SetGroup(dt.Rows[0]);
            //        G.Top = 0;
            //        G.Left = 0;
            //        G.BackColor = color;
            //        pnlGroup.Controls.Add(G);
            //        D = new AllDrug();
            //        D.SetName(dt.Rows[0]);
            //        D.Top = 0;
            //        D.Left = 120;
            //        D.BackColor = color;
            //        pnlGroup.Controls.Add(D);
            //        D.Anchor = AnchorStyles.Right;
            //        for (int i = 1; i < dt.Rows.Count; i++)
            //        {
            //            if (gro == dt.Rows[i]["GroupNo"].ToString())
            //            {
            //                G = new Group();
            //                G.clear();
            //                G.Top = 15 * i;
            //                G.Left = 0;
            //                G.BackColor = color;
            //                pnlGroup.Controls.Add(G);
            //                D = new AllDrug();
            //                D.SetName(dt.Rows[i]);
            //                D.Top = 15 * i;
            //                D.Left = 120;
            //                D.BackColor = color;
            //                pnlGroup.Controls.Add(D);
            //            }
            //            else
            //            {
            //                s = !s;
            //                if (s)
            //                    color = Color.FromArgb(220, 244, 255);
            //                else
            //                    color = Color.FromArgb(220, 220, 255);
            //                G = new Group();
            //                gro = dt.Rows[i]["GroupNo"].ToString();
            //                G.SetGroup(dt.Rows[i]);
            //                G.Top = 15 * i;
            //                G.Left = 0;
            //                G.BackColor = color;
            //                pnlGroup.Controls.Add(G);
            //                D = new AllDrug();
            //                D.SetName(dt.Rows[i]);
            //                D.Top = 15 * i;
            //                D.Left = 120;
            //                D.BackColor = color;
            //                pnlGroup.Controls.Add(D);
            //            }
            //        }
            //        GC.Collect();//垃圾回收
            //    }
            //    else
            //    {

            //    }

            //}
            //catch (System.Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            #endregion
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }

    
}
