using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using PIVAsCommon.Helper;

namespace PivasDDrug
{
    public partial class UpdateDrug : Form
    {
        public UpdateDrug()
        {
            InitializeComponent();
        }
        DB_Help db = new DB_Help();
        Select select = new Select();
        Insert insert = new Insert();
        Update update = new Update();
        DataSet ds = new DataSet();//单位制剂
        DataSet ds1 = new DataSet();//耗材

        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion

        
        DataSet DrugPlusCondition = new DataSet();
        public string[] DrugConcentration = new string[10];
        //溶度指标
        public decimal Major = 0;
        //剂量
        public decimal Dosage;
        //剂量单位
        public string DosageUnit;
        //主药剂量
        public decimal Capacity;
        //主药剂量单位
        public string CapacityUnit;
        //溶度指标单位
        public string MajorUnit = string.Empty;
        //药品制剂
        public string Preparation = string.Empty;

        //药品属性
        public string TheDrugType = string.Empty;

        //public string 
        //药品编码
        public string DrugCode = string.Empty;

        private bool ss = false;

        //濃度設置
        SetDrugTiter Set = new SetDrugTiter();

        //DDrugPlusCondition附加条件表
        public void ShowDrugPlusCondition()
        {
            DrugPlusCondition = db.GetPIVAsDB(select.DDrugPlusCondition(DrugCode));
            clb_DrugPlusCondition.DataSource = DrugPlusCondition.Tables[0];
            clb_DrugPlusCondition.DisplayMember = "Name";
            clb_DrugPlusCondition.ValueMember = "ID";
            for (int i = 0; i < DrugPlusCondition.Tables[0].Rows.Count; i++)
            {
                for (int j = 0; j < DrugPlusCondition.Tables[1].Rows.Count; j++)
                {
                    if (DrugPlusCondition.Tables[0].Rows[i]["ID"].ToString() == DrugPlusCondition.Tables[1].Rows[j]["PlusConditionID"].ToString())
                    {
                        //clb_DrugPlusCondition.SetSelected(i, true);
                        clb_DrugPlusCondition.SetItemChecked(i, true);
                    }

                }

            }
        }

        public void show()
        {
            ds = db.GetPIVAsDB(select.UniPreparation());
            ds1 = db.GetPIVAsDB(select.Consumables());
            TxtAdd();
            ShowDrugPlusCondition();
            UniPreparation("");
            DrugConsumables("");
            DG_UniPreparation.Visible = false;
            dgv_Consumables.Visible = false;
            GetPicture();
        }

        private void UniPreparation(string UnName)
        {
            DG_UniPreparation.DataSource = null;

            if (UnName != "")
            {
                string NewUnname = string.Empty;
                //dr = ds.Tables[0].Select(" 通用名 like '% + UnName + "%'");
                DataTable DTable = ds.Tables[0];
                DTable = ds.Tables[0].Copy();
                DTable.Rows.Clear();
                NewUnname = UnName.Replace("%", " ");
                NewUnname = NewUnname.Replace("*", " ");
                NewUnname = NewUnname.Replace("|", " ");
                NewUnname = NewUnname.Replace(":", " ");
                NewUnname = NewUnname.Replace("：", " ");
                //dt2.ImportRow(dt.Rows[0]);//这是加入的是第一行
                //string sql = string.Format(" 通用名 like '%{0}%'", NewUnname);
                string sql = string.Format(" 通用名 like '%{0}%' OR SpellCode like '%{0}%'", NewUnname);    
                try
                {
                    DataRow[] rowsArry = ds.Tables[0].Select(sql);
                    foreach (DataRow row in rowsArry)
                    {
                        DTable.ImportRow(row);
                    }
                    //dt.Rows.Add(dr);
                    if (DTable.Rows.Count > 0)             
                    {
                        DG_UniPreparation.DataSource = DTable;
                    }
                }
                catch
                {
                    DTable = db.GetPIVAsDB(select.UniPreparation(UnName)).Tables[0];
                    if (DTable.Rows.Count > 0)
                    {
                        DG_UniPreparation.DataSource = DTable;
                    }
                }
            }
            else
            {
                DG_UniPreparation.DataSource = ds.Tables[0];
            }

            if (DG_UniPreparation.Rows.Count > 0)
            {
                DG_UniPreparation.Columns[0].Visible = false;
                DG_UniPreparation.Columns[1].Width = 180;
                DG_UniPreparation.Columns[2].Width = 200;
                //DG_UniPreparation.Columns[5].Width = 200*4; 
                DG_UniPreparation.ClearSelection();
            }
        }

    
        private void TxtAdd()
        {
          

            try
            {

                DataSet Dme = db.GetPIVAsDB(select.DMetric());
                DataTable dt = Dme.Tables[0];
                //DataTable dt2 = Dme.Tables[0].Copy();
                Cb_DosageUnit.DataSource = dt;
                Cb_DosageUnit.ValueMember = "MetricCode";
                Cb_DosageUnit.DisplayMember = "Metricname";
                // Cb_CapacityUnit.DataSource = dt2;
                Cb_CapacityUnit.Items.Add("ml");
                Cb_CapacityUnit.ValueMember = "MetricCode";
                Cb_CapacityUnit.DisplayMember = "Metricname";

                DataTable Species = Dme.Tables[1].Copy();
                Cb_Species.Items.Add("");
                for (int i = 0; i < Species.Rows.Count; i++)
                { Cb_Species.Items.Add(Species.Rows[i][0].ToString().Trim()); }
                //Cb_Species.DataSource = Species;
                //Cb_Species.ValueMember = "Species";
                //Cb_Species.DisplayMember = "Species";

                DataTable Positions1 = Dme.Tables[2].Copy();
                Positions1.Rows.InsertAt(Positions1.NewRow(), 0);
                Positions1.Rows[0][0] = string.Empty;
                Cb_Positions1.DataSource = Positions1;
                Cb_Positions1.ValueMember = "进仓号";
                Cb_Positions1.DisplayMember = "进仓号";

                //DataTable Positions2 = Dme.Tables[2].Copy();
                //Cb_Positions2.DataSource = Positions2;
                //Cb_Positions2.ValueMember = "进仓号";
                //Cb_Positions2.DisplayMember = "进仓号";

                DataTable DT = db.GetPIVAsDB(select.DDurg(DrugCode)).Tables[0];
                if (DT != null && DT.Rows.Count > 0)
                {
                    Title.Text = "  " + DT.Rows[0]["药品名称"].ToString();
                    Txt_DrugName.Text = DT.Rows[0]["药品简称"] == null || DT.Rows[0]["药品简称"].ToString().Trim() == "" ? DT.Rows[0]["药品名称"].ToString() : DT.Rows[0]["药品简称"].ToString();
                    Txt_DrugCode.Text = DT.Rows[0]["药品编码"].ToString();
                    Txt_SpellCode.Text = DT.Rows[0]["拼音码"].ToString();
                    Txt_Spsc.Text = DT.Rows[0]["规格"].ToString();
                    Txt_Dosage.Text = DT.Rows[0]["剂量"].ToString();
                    Cb_DosageUnit.Text = DT.Rows[0]["剂量单位"].ToString();
                    //str.Append(" BigUnit as 大包装单位,FormUnit as 小包装单位,Conversion as 小包装数量,");
                    Txt_BitUnit.Text = DT.Rows[0]["大包装单位"] == null ? "" : DT.Rows[0]["大包装单位"].ToString();
                    Txt_FormUnit.Text = DT.Rows[0]["小包装单位"] == null ? "" : DT.Rows[0]["小包装单位"].ToString();
                    Txt_Conversion.Text = DT.Rows[0]["小包装数量"] == null ? "" : DT.Rows[0]["小包装数量"].ToString();
                 
                    if (DT.Rows[0]["溶媒剂量"].ToString().Trim() != "" )
                    { Txt_Capacity.Text = DT.Rows[0]["溶媒剂量"].ToString().Trim(); }
                    else if (DT.Rows[0]["溶媒剂量"].ToString().Trim() == "" && DT.Rows[0]["剂量单位"].ToString() == "ml")
                    { Txt_Capacity.Text = DT.Rows[0]["剂量"].ToString().Trim(); }
                    else
                    { Txt_Capacity.Text = "0"; }
                  
                    if (DT.Rows[0]["溶媒剂量单位"].ToString() != "")
                    { Cb_CapacityUnit.Text = DT.Rows[0]["溶媒剂量单位"].ToString();  }
                    else if (DT.Rows[0]["溶媒剂量单位"].ToString() == "" && DT.Rows[0]["剂量单位"].ToString() == "ml")
                    { Cb_CapacityUnit.Text = "ml";  }
                    else
                    { Cb_CapacityUnit.Text = ""; }
                
                    CB_Withmenstruum.Checked = DT.Rows[0]["自带溶媒"].ToString() == "" ? false : bool.Parse(DT.Rows[0]["自带溶媒"].ToString());
                    checkBox1.Checked = DT.Rows[0]["溶媒"].ToString() == "" ? false : bool.Parse(DT.Rows[0]["溶媒"].ToString());
                    CB_PreConfigure.Checked = DT.Rows[0]["预配药"].ToString() == "" ? false : bool.Parse(DT.Rows[0]["预配药"].ToString());
                    CB_PiShi.Checked = DT.Rows[0]["皮试"].ToString() == "" ? false : bool.Parse(DT.Rows[0]["皮试"].ToString());
                    CB_NotCompound.Checked = DT.Rows[0]["不冲配"].ToString() == "" ? false : bool.Parse(DT.Rows[0]["不冲配"].ToString());
                    CB_Precious.Checked = DT.Rows[0]["贵重药"].ToString() == "" ? false : bool.Parse(DT.Rows[0]["贵重药"].ToString());
                    CB_AsMajorDrug.Checked = DT.Rows[0]["不作主药"].ToString() == "" ? false : bool.Parse(DT.Rows[0]["不作主药"].ToString());
                    Txt_Difficulty.Text = DT.Rows[0]["配液难度系数"].ToString()== "" ? "0" : DT.Rows[0]["配液难度系数"].ToString();
                    Txt_DifficultySF.Text = DT.Rows[0]["配置难度系数"].ToString() == "" ? "0" : DT.Rows[0]["配置难度系数"].ToString();
                    Cob_NoName.Text = DT.Rows[0]["优先级"].ToString();
             
                    Txt_PortNo.Text = DT.Rows[0]["货柜号"].ToString();
                    Cb_Positions1.Text = DT.Rows[0]["仓位1"].ToString();
                   // Cb_Positions2.Text = DT.Rows[0]["仓位2"].ToString();
                    Txt_ProductName.Text = DT.Rows[0]["商品名称"].ToString();
                    

                    if (DT.Rows[0]["浓度监测"].ToString().Trim() == "False" || DT.Rows[0]["浓度监测"].ToString().Trim() == "false" )
                    { checkBox4.Checked = false; }
                    else
                    { checkBox4.Checked = true ; }
               


                    if (DT.Rows[0]["分类"].ToString().Trim() != "")
                    { Cb_Species.Text = DT.Rows[0]["分类"].ToString(); }
                    else
                    { Cb_Species.Text = ""; }

                    Txt_UniPreparation.Text = "";
                    Txt_UniPreparation.Tag = "";
                    if (DT.Rows[0]["单位制剂"].ToString() != null && DT.Rows[0]["单位制剂"].ToString() != "" && DT.Rows[0]["单位制剂"].ToString() != "0")
                    {
                        DataTable DTa = db.GetPIVAsDB(select.UniPreparation(DT.Rows[0]["单位制剂"].ToString())).Tables[0];
                        Txt_UniPreparation.Text = DTa.Rows[0]["NameAndSpec"].ToString();
                        Txt_UniPreparation.Tag = DTa.Rows[0]["ID"].ToString();
                        DTa.Dispose();
                    }


                 
                    Cb_Drugtype.SelectedIndex = Convert.ToInt32(DT.Rows[0]["药品属性"]) - 1;

                 
                    if (DT.Rows[0]["药品编码"].ToString() != null && DT.Rows[0]["药品编码"].ToString() != "" && DT.Rows[0]["药品编码"].ToString() != "0")
                    {
                        DataTable DTb = db.GetPIVAsDB(select.Consumables(DT.Rows[0]["药品编码"].ToString())).Tables[0];
                        if (DTb != null && DTb.Rows.Count > 0)
                        {
                            txt_Consumables.Text = DTb.Rows[0]["ConsumablesName"].ToString();
                            txt_Consumables.Tag = DTb.Rows[0]["ConsumablesCode"].ToString();
                            dgv_Consumables.DataSource = DTb;
                            DTb.Dispose();
                        }
                    }

                    if (Txt_SpellCode.Text.Trim().Length <= 0)
                    {
                        Txt_SpellCode.Text = GetChineseSpell(Title.Text.Trim());
                    }
                }

            }
            catch
            { 
                MessageBox.Show("药品详细数据取得出错。请检查药品字典表字段！！！");
                MessageBox.Show("请检查 表DDrug中 字段Concentration（SMALLINT） 是否存在！！！"); 
            }
        }

        private void But_Add_Click(object sender, EventArgs e)
        {
            SetDrugPlusCondition add = new SetDrugPlusCondition();
            if (add.ShowDialog() == DialogResult.Cancel)
            {
                ShowDrugPlusCondition();
            }
        }

        private void Label_Close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Label_Set_Click(object sender, EventArgs e)
        {
            Set.DrugCode = DrugCode;
            Set.show(Major, MajorUnit);
            Set.ShowDialog();
        }

       

        private void Txt_UniPreparation_TextChanged(object sender, EventArgs e)
        {
            if (Txt_UniPreparation.Text != "")
            {
                    DG_UniPreparation.Visible = true;
                    UniPreparation(Txt_UniPreparation.Text);
            }
            else
            {
                DG_UniPreparation.Visible = false;
            }
        }

        private void DG_UniPreparation_DoubleClick(object sender, EventArgs e)
        {
            Txt_UniPreparation.Tag = DG_UniPreparation.Rows[DG_UniPreparation.CurrentRow.Index].Cells["ID"].Value.ToString();
            Txt_UniPreparation.Text = DG_UniPreparation.Rows[DG_UniPreparation.CurrentRow.Index].Cells["NameAndSpec"].Value.ToString();
            Preparation = Txt_UniPreparation.Text;
            int i = 0;
            int.TryParse(db.GetPIVAsDB(string.Format("EXEC [dbo].[bl_CrDDrugType] '{0}'", 
                Txt_UniPreparation.Tag)).Tables[0].Rows[0][0].ToString(), out i);
            Cb_Drugtype.SelectedIndex = i > 0 ? i - 1 : 0;
            DG_UniPreparation.Visible = false;
        }

        private void Txt_UniPreparation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                DG_UniPreparation.Select();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                DG_UniPreparation.Visible = false;
            }
            else if (e.KeyCode == Keys.Enter&&DG_UniPreparation.Visible == true)
            {
                DG_UniPreparation.Visible = false;
            }
            else if (e.KeyCode == Keys.Enter && DG_UniPreparation.Visible == false )
            {
                DG_UniPreparation.Visible = true;
            }
        }

        private void DG_UniPreparation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Txt_UniPreparation.Tag = DG_UniPreparation.Rows[DG_UniPreparation.CurrentRow.Index].Cells["ID"].Value.ToString();
                Txt_UniPreparation.Text = DG_UniPreparation.Rows[DG_UniPreparation.CurrentRow.Index].Cells["NameAndSpec"].Value.ToString();
                Preparation = Txt_UniPreparation.Text;
                DG_UniPreparation.Visible = false;
            }
        }

        private void Label_Save_Click(object sender, EventArgs e)
        {
            Add();
        }

        public void Add()
        {
            string nameJC = Txt_DrugName.Text;
            string Spellcode = Txt_SpellCode.Text;
            string Spec = Txt_Spsc.Text;
            Dosage = Txt_Dosage.Text.ToString() == "" ? 0 : decimal.Parse(Txt_Dosage.Text.ToString());
            DosageUnit = Cb_DosageUnit.Text;
            if ( Txt_Capacity.Text.ToString().Trim() == "" )
            { Txt_Capacity.Text = "0"; }
            Capacity =  decimal.Parse(Txt_Capacity.Text.ToString().Trim());
            CapacityUnit = Cb_CapacityUnit.Text;

            //int dr = 0;
            //if (Cb_Drugtype.Text =="抗生素")
            //{
            //    dr = 2;
            //}
            //else if (Cb_Drugtype.Text == "化疗药")
            //{
            //    dr = 3;
            //}
            //else if (Cb_Drugtype.Text== "营养药")
            //{
            //    dr = 4;
            //}
            //else
            //{
            //    dr = 1;
            //
            int TheDrugType = Cb_Drugtype.SelectedIndex + 1;
            this.TheDrugType = Cb_Drugtype.SelectedItem.ToString();


           // string UniPreparationID = Txt_UniPreparation.Text  == "" ? null : Txt_UniPreparation.Tag.ToString();
            string UniPreparationID = string.Empty;
            if (Txt_UniPreparation.Text.Trim() == "")
            {
                MessageBox.Show("单位制剂匹配不能为空！！！");
                Txt_UniPreparation.Focus();
                return;
            }
            else
            {
                UniPreparationID = Txt_UniPreparation.Text == "" ? null : Txt_UniPreparation.Tag.ToString();
            }

            string FormUnit = Txt_FormUnit.Text;
            Int16 Conversion = Txt_Conversion.Text == "" ? (Int16)0 : Int16.Parse(Txt_Conversion.Text);
            string BitUnit = Txt_BitUnit.Text;
            string PortNo = Txt_PortNo.Text;
            bool ismenstruum = checkBox1.Checked;//是否溶媒
            bool Withmenstruum = CB_Withmenstruum.Checked;
            bool PreConfigure = CB_PreConfigure.Checked;
            bool PiShi = CB_PiShi.Checked;
            bool NotCompound = CB_NotCompound.Checked;
            bool Precious = CB_Precious.Checked;
            bool AsMajorDrug = CB_AsMajorDrug.Checked;
            decimal Difficulty = Txt_Difficulty.Text.ToString() == "" ? 0 : decimal.Parse(Txt_Difficulty.Text.ToString());
            string DifficultySF = Txt_DifficultySF.Text.ToString();
            string Species=Cb_Species.Text;
            string Positions1=Cb_Positions1.Text;
         //   string Positions2=Cb_Positions2.Text;
            int NoName = int.Parse(Cob_NoName.Text);
            string ProductName=Txt_ProductName.Text;

            bool Concentration = false ;//浓度监测
            if (checkBox4.Checked == true)
            { Concentration = true ; }
            else
            { Concentration = false ; }


            object[] ob = new object[30];//数组定义
            ob[0] = Spellcode;
            ob[1] = Spec;
            ob[2] = Dosage;
            ob[3] = DosageUnit;
            ob[4] = Capacity;
            ob[5] = CapacityUnit;
            ob[6] = UniPreparationID;
            ob[7] = FormUnit;
            ob[8] = Conversion;
            ob[9] = BitUnit;
            ob[10] = PortNo;
            ob[11] = Withmenstruum;
            ob[12] = PreConfigure;
            ob[13] = PiShi;
            ob[14] = NotCompound;
            ob[15] = Precious;
            ob[16] = AsMajorDrug;
            ob[17] = Major;
            ob[18] = MajorUnit;
            ob[19] = Difficulty;
            ob[20] = nameJC;
            ob[21] = Species;
            ob[22] = Positions1;
            ob[23] = string.Empty;
            ob[24] = NoName;
            ob[25] = ismenstruum;
            ob[26] = ProductName;
            ob[27] = Concentration;//浓度监测
            ob[28] = DifficultySF;
            ob[29] = TheDrugType;

            string checkedText = string.Empty;
            for (int i = 0; i < clb_DrugPlusCondition.Items.Count; i++)
            {
                if (clb_DrugPlusCondition.GetItemChecked(i))
                {
                    clb_DrugPlusCondition.SetSelected(i, true);
                    checkedText += (String.IsNullOrEmpty(checkedText) ? "" : ",") + clb_DrugPlusCondition.SelectedValue.ToString();
                }
            }
            //MessageBox.Show(update.DDurg(ob, DrugCode));
            int Tf = db.SetPIVAsDB(update.DDurg(ob, DrugCode));

            Preparation = Txt_UniPreparation.Text;

            if (Tf > 0)
            {
                if (!checkedText.Trim().Equals(""))
                {
                    string[] MedDrugPlusCondition = checkedText.Split(',');
                    int Tags = db.SetPIVAsDB(insert.MedDrugPlusCondition(DrugCode, MedDrugPlusCondition));

                    if (Tags > 0)
                    {
                        this.DialogResult = DialogResult.Yes;
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("修改附加条件失败,请重试！");
                    }
                }
                else
                {
                    int Tags = db.SetPIVAsDB("  DELETE FROM MedDrugPlusCondition WHERE DrugID='" + DrugCode + "' ");
                }
                if (!Set.update(DrugCode))
                {
                    MessageBox.Show("浓度指标设置失败！");
                }
                else
                {
                    this.DialogResult = DialogResult.Yes;
                    this.Hide();
                }

            }
            else
            {
                MessageBox.Show("修改失败");
            }
            if (txt_Consumables.Text!= null && txt_Consumables.Text.ToString() != "")
            {
                int a = db.SetPIVAsDB(update.DrugConsumables(txt_Consumables.Tag.ToString(), DrugCode));
                if (a == 0)
                {
                    MessageBox.Show("耗材修改错误");
                }
            }
            else
            {
               
                int a = db.SetPIVAsDB(update.DrugConsumables(DrugCode));
                if (a == 0)
                {
                    MessageBox.Show("耗材修改错误");
                }
            }
        }

        #region ********************************自动获取药品拼音码***************************************
        /// <summary>
        /// 获取首字母
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public string GetChineseSpell(string strText)
        {
            int len = strText.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            { myStr += getSpell(strText.Substring(i, 1)); }
            return myStr;
        }

        public string getSpell(string cnChar)
        {
            byte[] arrCN = System.Text.Encoding.Default.GetBytes(cnChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25)
                        max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return System.Text.Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                } return "*";
            }
            else return cnChar;
        }
        #endregion  ********************************自动获取药品拼音码***************************************

        private void label15_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        public bool Validate(string Str, string RegexStr)
        {
            Regex regex = new Regex(RegexStr);
            Match match = regex.Match(Str);
            if (match.Success)
                return true;
            else
                return false;

        }

        private void Txt_Dosage_KeyPress(object sender, KeyPressEventArgs e)
        {
            //TextBox tx = (TextBox)sender;
            //if (tx.Text.Trim() != "")
            //{
            //    if (!Validate(tx.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
            //    {
            //        e.Handled = true;
            //    }
            //    else
            //    {
            //        e.Handled = false;
            //    }
            //}
        }

        private void Txt_UniPreparation_Enter(object sender, EventArgs e)
        {
            DG_UniPreparation.Visible = true;
        }

        private void Txt_UniPreparation_Leave(object sender, EventArgs e)
        {
            if (!DG_UniPreparation.Focused)
            {
                 DG_UniPreparation.Visible = false;
            }
        }

        private void DG_UniPreparation_Leave(object sender, EventArgs e)
        {
            //if (!Txt_UniPreparation.Focused)
            //{
                DG_UniPreparation.Visible = false;
            //}
        }

        private void Txt_Difficulty_TextChanged(object sender, EventArgs e)
        {
            Regex rex = new Regex("^([0-9]|[0-9]{2}|100)$");
            TextBox T = (TextBox)sender;
            if(T.Text!=""&&!rex.IsMatch(T.Text))
            {
                MessageBox.Show("请输入0-100的数字");
                T.Text = "0";
                T.SelectAll();
            }

        }
        #region 张衡2014-10-15添加耗材
        private void txt_Consumables_Enter(object sender, EventArgs e)
        {
            dgv_Consumables.Visible = true;
        }

        private void txt_Consumables_Leave(object sender, EventArgs e)
        {
            if (!dgv_Consumables.Focused)
            {
                dgv_Consumables.Visible = false;
            }
        }

        private void txt_Consumables_TextChanged(object sender, EventArgs e)
        {
            if (dgv_Consumables.Visible == false)
            {
                dgv_Consumables.Visible = true;
            }
            if (txt_Consumables.Text != null)
            {               
                DrugConsumables(txt_Consumables.Text.ToString().Trim());
            }
            else
            {
                DrugConsumables(""); 
            }
        }

        private void dgv_Consumables_DoubleClick(object sender, EventArgs e)
        {
            txt_Consumables.Text = dgv_Consumables.CurrentRow.Cells["耗材名称"].Value.ToString();
            txt_Consumables.Tag = dgv_Consumables.CurrentRow.Cells["耗材编码"].Value.ToString();
            dgv_Consumables.Visible = false;
        }

        private void dgv_Consumables_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txt_Consumables.Text = dgv_Consumables.CurrentRow.Cells["耗材名称"].Value.ToString();
                txt_Consumables.Tag = dgv_Consumables.CurrentRow.Cells["耗材编码"].Value.ToString();
                dgv_Consumables.Visible = false;
            }
        }

        private void txt_Consumables_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgv_Consumables.Select();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                dgv_Consumables.Visible = false;
            }
            else if (e.KeyCode == Keys.Enter && dgv_Consumables.Visible == true)
            {
                dgv_Consumables.Visible = false;
            }
            else if (e.KeyCode == Keys.Enter && dgv_Consumables.Visible == false)
            {
                dgv_Consumables.Visible = true;
            }
        }

        private void DrugConsumables(string Consumables)
        {
            if (Consumables != "")
            {
                DataTable DTable = ds1.Tables[0];
                DTable = ds1.Tables[0].Copy();
                DTable.Rows.Clear();
                string sql = string.Format(" 耗材名称 like '%{0}%' OR 耗材编码 like '%{0}%'", Consumables);
                try
                {
                    DataRow[] rowsArry = ds1.Tables[0].Select(sql);
                    foreach (DataRow row in rowsArry)
                    {
                        DTable.ImportRow(row);
                    }
                    //dt.Rows.Add(dr);
                    if (DTable.Rows.Count > 0)
                    {
                        dgv_Consumables.DataSource = DTable;
                    }
                }
                catch
                {
                    DTable = db.GetPIVAsDB(select.Consumables(Consumables)).Tables[0];
                    if (DTable.Rows.Count > 0)
                    {
                        dgv_Consumables.DataSource = DTable;
                    }
                }
            }
            else
            {
                dgv_Consumables.DataSource = ds1.Tables[0];               
            }
           
                dgv_Consumables.Columns[1].Visible = false;
                dgv_Consumables.Columns[0].Width = 800;
            
        }
        #endregion 

        private void button1_Click(object sender, EventArgs e)
        {
            DrugLimitForPrint DLFP = new DrugLimitForPrint(DrugCode);
            DLFP.ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            int ismm = checkBox1.Checked ? 1 : 0;
            int notcd = CB_NotCompound.Checked ? 1 : 0;
            DataTable Positions1 = db.GetPIVAsDB(string.Format(" select DeskNo as '进仓号' from  IVRecordDeskSet where DeskNo is not null and DeskNo!='' and isopen='1' and 0={0} and 0={1}", ismm, notcd)).Tables[0];
            Positions1.Rows.InsertAt(Positions1.NewRow(), 0);
            Positions1.Rows[0][0] = string.Empty;
            Cb_Positions1.DataSource = Positions1;
            Cb_Positions1.ValueMember = "进仓号";
            Cb_Positions1.DisplayMember = "进仓号";
        }


        #region 添加药品图片

        private void GetPicture()
        {
            if (DrugCode != "" && DrugCode != null)
            {


                if (!Convert.IsDBNull (db.GetPIVAsDBforObject("select DrugPicture from DDrug where DrugCode='" + DrugCode + "'")))
                {
                    byte[] b = (byte[])db.GetPIVAsDBforObject("select DrugPicture from DDrug where DrugCode='" + DrugCode + "'");

                    MemoryStream stream = new MemoryStream(b, true);
                    stream.Write(b, 0, b.Length);
                    pictureBox1.Image = new Bitmap(stream);
                    stream.Close();
                }
                else
                {

                    pictureBox1.Image = global::PivasDDrug.Properties.Resources.NoPicture;
                    
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            //saveFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            FileStream fs = File.OpenRead(openFileDialog1.FileName);
            byte[] imageb = new byte[fs.Length];
            fs.Read(imageb, 0, imageb.Length);
            fs.Close();
            db.SetPIVAsDB("update DDrug set DrugPicture=@images where DrugCode='" + DrugCode + "'", imageb, "@images");
            //db.SetPIVAsDB("update DDrug set DrugPicture="+imageb+" where DrugCode='" + DrugCode + "'");
            GetPicture();
            //pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            //pictureBox1.Image = new Bitmap(saveFileDialog1.OpenFile());
        }

        #endregion

        private void Cb_Drugtype_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            if (openFileDialog2.OpenFile().Length > 31460000)
            {
                MessageBox.Show("视频大小限制30M");
                return;
            }
            try
            {
                string name = db.DatebasePIVAsInfo().Split(';')[0].Split('=')[1];
                name = Dns.GetHostEntry(name).HostName;
                //MessageBox.Show(name);
                string source = openFileDialog2.FileName;
                string dest = @"\\" + name + "\\Media\\" + DrugCode + ".mp4";
                if (File.Exists(dest))
                {
                    DialogResult dr = MessageBox.Show("文件" + dest + "已存在，是否覆盖？", "提示", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.OK)
                    {
                        File.Delete(dest);
                    }
                    else
                    {
                        return;
                    }
                }

                File.Copy(source, dest);// Dialog2.FileName, "\\ADMIN-PC\\zhangmuyun");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



    }
}
