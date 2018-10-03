using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OfficeOpenXml;


namespace TPNReview
{
    /// <summary>
    /// PageExport.xaml 的交互逻辑
    /// </summary>
    public partial class PageExport : Page, IContentPage
    {
        private List<string> lstPntCodes = null;
        private Action onRefParent = null;
        private Func<List<PatientModel>> getPatientLst = null;

        public PageExport()
        {
            InitializeComponent();

            this.lstPntCodes = new List<string>();
            rdoTPN.IsChecked = true;
            rdoTPN_Click(rdoTPN, null); 
        } 
        
        #region INTERFACE
        public void init(Action _refParent)
        {
            this.onRefParent = _refParent;
        }
        public void setPatient(PatientModel _pnt)
        {
            if (null == _pnt)
                return;

            if (true == rdoTPN.IsChecked)
            {
                if (!lstPntCodes.Contains(_pnt.PatientCode + "=TPN"))
                    if (addRecipe(_pnt))
                        lstPntCodes.Add(_pnt.PatientCode + "=TPN");
            }
            else if (!lstPntCodes.Contains(_pnt.PatientCode + "=INV"))
                if (addIntervene(_pnt))
                    lstPntCodes.Add(_pnt.PatientCode + "=INV"); 
        }

        public void clear()
        {
            string typ = "";
            if (lvExpTPN.Visibility == System.Windows.Visibility.Visible)
            {
                if (null != lvExpTPN.ItemsSource)
                    lvExpTPN.ItemsSource = null;
                else
                    lvExpTPN.Items.Clear();

                typ = "=TPN";
            }

            if (lvExpIntervene.Visibility == System.Windows.Visibility.Visible)
            {
                if (null != lvExpIntervene.ItemsSource)
                    lvExpIntervene.ItemsSource = null;
                else
                    lvExpIntervene.Items.Clear();

                typ = "=INV";
            }

            for (int i = this.lstPntCodes.Count - 1; i >= 0; i--)
                if (this.lstPntCodes[i].Contains(typ))
                    this.lstPntCodes.RemoveAt(i);
        }
        #endregion

        public void setGetPatientList(Func<List<PatientModel>>  _getPatientLst)
        {
            this.getPatientLst = _getPatientLst;
        }

        /// <summary>
        /// 获取病人诊断
        /// </summary>
        /// <param name="_db"></param>
        /// <param name="_pcode"></param>
        /// <param name="diagnose">诊断内容</param>
        /// <returns>获取是否成功</returns>
        public static bool getDiagnose(BLPublic.DBOperate _db, string _pcode, ref string diagnose)
        {
            IDataReader dr = null;
            if (_db.GetRecordSet(string.Format(SQL.SEL_PNTDIG, _pcode), ref dr, true))
            {
                diagnose = "";
                while (dr.Read())
                {
                    if (!string.IsNullOrWhiteSpace(diagnose))
                        diagnose += ",";

                    diagnose += dr["ICD10Name"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("SubName2")) && !string.IsNullOrWhiteSpace(dr["SubName2"].ToString()))
                    {
                        diagnose += "((" + dr["SubName1"].ToString() + "(" + dr["SubName2"].ToString() + "))"; //有SubName2必有SubName1
                    }
                    else if (!dr.IsDBNull(dr.GetOrdinal("SubName1")) && !string.IsNullOrWhiteSpace(dr["SubName1"].ToString()))
                    {
                        diagnose += "(" + dr["SubName1"].ToString() + ")";
                    }

                }

                dr.Close();
                return true;
            }
            else
                diagnose = "加载患者诊断失败:" + _db.Error;

            return false;
        }

        private string getAgeUnit(string _unit)
        {
            if (0 == string.Compare(_unit, "M", true))
                return "月";
            else if (0 == string.Compare(_unit, "D", true))
                return "天";
            else
                return "岁";
        }

        private bool addRecipe(PatientModel _pnt)
        { 
            DataTable tbl = new DataTable();

            if (!AppConst.db.GetRecordSet(string.Format(SQL.SEL_PNT_TPN, _pnt.PatientCode) + " ORDER BY StartTime", ref tbl))
            {
                BLPublic.Dialogs.Error("加载患者医嘱失败:" + AppConst.db.Error);
                return false;
            }
            
            int no = 1;
            foreach (object obj in lvExpTPN.Items)
            {
                if (!string.IsNullOrWhiteSpace(((ExpTPNRecipe)obj).RecipeID) && !string.IsNullOrWhiteSpace(((ExpTPNRecipe)obj).BedNo))
                    no++;
            }

            string rcpIDs = "";
            int startIndx = lvExpTPN.Items.Count;
            BLPublic.BLDataReader dr = new BLPublic.BLDataReader(tbl.CreateDataReader());
            while (dr.next())
            {
                rcpIDs += ",'" + dr.getString("RecipeID") + "'";
                lvExpTPN.Items.Add(new ExpTPNRecipe()
                {
                    No = (no++).ToString(),
                    RecipeID = dr.getString("RecipeID"),
                    PatientCode = _pnt.PatientCode,
                    WardName = dr.getString("DeptName"),
                    BedNo = dr.getString("BedNo"),
                    PatientName = dr.getString("PatientName"),
                    HospitalNo = dr.getString("HospitalNo"),
                    Age = dr.getString("Age") + getAgeUnit(dr.getString("AgeUnit")),
                    Sex = ComClass.getZhSex(dr.getString("Sex")),
                    InHospitalTime = dr.isNull("InHospitalDT") ? "" : dr.getDateTime("InHospitalDT").ToString("yyyy-M-d"),
                    Weight = "-",
                    Height = "-",
                    Diagnose = _pnt.Diagnose,
                    TPNUseTime = dr.getDateTime("StartTime").ToString("yyyy-MM-dd HH:mm:ss") + " ~ " +
                                 (dr.isNull("StopTime") ? "" : dr.getDateTime("StopTime").ToString("yyyy-MM-dd HH:mm:ss")),
                    StartTime = dr.getDateTime("StartTime"),
                    Drugs = "",
                    IsOK = "是"
                }); 
            }
            dr.close();
            tbl.Clear();

            if (string.IsNullOrWhiteSpace(rcpIDs)) 
                return true; 
             
            //加载医嘱内容
            if (!AppConst.db.GetRecordSet(string.Format(SQL.SEL_ORDERSPREP_BYRCPs, rcpIDs.Substring(1)), ref tbl))
            {
                BLPublic.Dialogs.Error("加载医嘱内容失败:" + AppConst.db.Error);
                return false;
            }

            ExpTPNRecipe item = null;
            DataRow[] rows = null;
            int i = 0;
            int orderType = 0;
            string mntRcpIDs = "";
            string tpnChkRcpIDs = "";

            for (i = lvExpTPN.Items.Count - 1; i >= startIndx; i--)
            {
                item = (ExpTPNRecipe)lvExpTPN.Items[i];
                if (string.IsNullOrWhiteSpace(item.RecipeID))
                    continue;


                rows = tbl.Select("RecipeID='" + item.RecipeID + "'");
                foreach (DataRow r in rows)
                    if ("498".Equals(r["UniPreparationID"].ToString())) //卡文
                    {
                        orderType = 498;
                        break;
                    }
                    else if (0x07 > orderType)
                        orderType |= tpnmonitor.TPNMonitor.getPrepSAFType(Convert.ToInt32(r["UniPreparationID"].ToString()));
                    else
                        break;

                if (0 < rows.Length)
                {
                    if (498 == orderType)
                    {
                        item.Drugs = "卡文";
                        tpnChkRcpIDs += ",'" + item.RecipeID + "'";
                    }
                    else if (0x07 == orderType)
                    {
                        item.Drugs = "三合一";
                        mntRcpIDs += ",'" + item.RecipeID + "'";
                    }
                    else if (0 < orderType)
                    {
                        item.Drugs = "二合一";
                        tpnChkRcpIDs += ",'" + item.RecipeID + "'";
                    }
                }
            }
            tbl.Clear();

            if (!string.IsNullOrWhiteSpace(mntRcpIDs))
                monitorResult(mntRcpIDs.Substring(1), startIndx);

            if (!string.IsNullOrWhiteSpace(tpnChkRcpIDs))
                comCheckResult(tpnChkRcpIDs.Substring(1), startIndx);

            return true; 
        }

        /// <summary>
        /// 加载审方结果
        /// </summary>
        /// <param name="_rcpIDs"></param>
        private bool monitorResult(string _rcpIDs, int _startIndx)
        {
            DataTable tbl = new DataTable();
            //加载审方
            if (!AppConst.db.GetRecordSet(string.Format(SQL.SEL_TPNMNT_RCPs, _rcpIDs), ref tbl))
            {
                BLPublic.Dialogs.Error("加载审方结果失败:" + AppConst.db.Error);
                return false;
            }

            int indx = 0;
            ExpTPNRecipe item = null;
            DataRow[] rows = null;

            for (int i = lvExpTPN.Items.Count - 1; i >= _startIndx; i--)
            {
                item = (ExpTPNRecipe)lvExpTPN.Items[i];
                if (string.IsNullOrWhiteSpace(item.RecipeID) || string.IsNullOrWhiteSpace(item.BedNo))
                    continue;

                indx = 0;
                rows = tbl.Select("RecipeID='" + item.RecipeID + "'");
                foreach (DataRow r in rows)
                {
                    if (0 == indx) 
                        item.IsOK = "否";
                    
                    else
                    {
                        item = new ExpTPNRecipe()
                        {
                            PatientCode = item.PatientCode,
                            RecipeID = item.RecipeID,
                            StartTime = item.StartTime
                        }; 
                        lvExpTPN.Items.Insert(i + indx, item);
                    }
                    item.NoOKReason = r["ResultDesc"].ToString();

                    indx++;
                }
            }
            tbl.Clear();

            return true;
        }

        /// <summary>
        /// 加载通用审核结果
        /// </summary>
        /// <param name="_rcpIDs"></param>
        private bool comCheckResult(string _rcpIDs, int _startIndx)
        {
            DataTable tbl = new DataTable();
            //加载审方
            if (!AppConst.db.GetRecordSet(string.Format(SQL.SEL_COMCHK_BYRCPs, _rcpIDs), ref tbl))
            {
                BLPublic.Dialogs.Error("加载审方结果失败:" + AppConst.db.Error);
                return false;
            }

            int indx = 0;
            ExpTPNRecipe item = null;
            DataRow[] rows = null;

            for (int i = lvExpTPN.Items.Count - 1; i >= _startIndx; i--)
            {
                item = (ExpTPNRecipe)lvExpTPN.Items[i];
                if (string.IsNullOrWhiteSpace(item.RecipeID) || string.IsNullOrWhiteSpace(item.BedNo))
                    continue;

                indx = 0;
                rows = tbl.Select("RecipeID='" + item.RecipeID + "'");
                foreach (DataRow r in rows)
                {
                    if (0 == indx)
                        item.IsOK = "否";

                    else
                    {
                        item = new ExpTPNRecipe()
                        {
                            PatientCode = item.PatientCode,
                            RecipeID = item.RecipeID,
                            StartTime = item.StartTime
                        }; 
                        lvExpTPN.Items.Insert(i + indx, item);
                    }

                    item.NoOKReason = string.Format("{0}({1} {2})超出正常值({3})范围{4}({5}%)",
                            r["ItemName"].ToString(), r["ItemValue"].ToString(), r["Unit"].ToString(),
                            r["NormalValue"].ToString(), r["DeviateValue"].ToString(), 
                            Math.Round(((double)r["DeviatePer"]) * 100, 2));

                    indx++;
                }
            }
            tbl.Clear();

            return true;
        }

        private string getTPNValue(DataTable _tbl, string _itemCode)
        {
            DataRow[] rows = _tbl.Select("ItemCode='" + _itemCode + "'");
            if (null != rows && 1 <= rows.Length)
                return rows[0]["ItemValue"].ToString() + " " + rows[0]["Unit"].ToString();
            else
                return "";
        }

        /// <summary>
        /// 增加患者干预
        /// </summary>
        /// <param name="_pcode"></param>
        /// <returns></returns>
        private bool addIntervene(PatientModel _pnt)
        {
            string diagnose = "";
            PageExport.getDiagnose(AppConst.db, _pnt.PatientCode, ref diagnose);

            DataTable tbl = new DataTable();

            if (!AppConst.db.GetRecordSet(string.Format(SQL.SEL_PNT_TPNITEM, _pnt.PatientCode), ref tbl))
            {
                BLPublic.Dialogs.Error("加载患者信息失败:" + AppConst.db.Error);
                return false;
            }

            lvExpIntervene.Items.Add(new ExpIntervene()
            {
                PatientCode = _pnt.PatientCode,
                IDate = DateTime.Now.ToString("yyyy-M-d"),
                HospitalNo = _pnt.HospitalNo,
                SYZ = _pnt.Diagnose,
                TND = getTPNValue(tbl, "tpnitem.7"),    //葡萄糖浓度
                AJSND = getTPNValue(tbl, "tpnitem.8"),  //氨基酸浓度
                DJZ = getTPNValue(tbl, "tpnitem.4"),    //电解质
                RDB = getTPNValue(tbl, "tpnitem.9"),    //热氮比
                TZB = getTPNValue(tbl, "tpnitem.10"),   //糖脂比
                YDS = getTPNValue(tbl, "comp.7431"),    //胰岛素
                Drugs = "",
                Dosage = ""
            }); 
            tbl.Clear();

            return true;
        }

        private void miDel_Click(object sender, RoutedEventArgs e)
        {
            ListView lv = null;
            if (Visibility.Visible == lvExpIntervene.Visibility)
                lv = lvExpIntervene;
            else
                lv = lvExpTPN;

            if (null == lv.SelectedItem)
            {
                BLPublic.Dialogs.Alert("请选择要删除的医嘱");
                return;
            }

            if (lvExpTPN == lv)
            {
                ExpTPNRecipe item = (ExpTPNRecipe)lvExpTPN.SelectedItem;
                lstPntCodes.Remove(item.PatientCode);

                string rcpID = item.RecipeID;
                int no = 0;

                //先删除后面的行
                int i = lvExpTPN.SelectedIndex + 1;
                while (i < lvExpTPN.Items.Count)
                    if (((ExpTPNRecipe)lvExpTPN.Items[i]).RecipeID.Equals(rcpID))
                        lvExpTPN.Items.RemoveAt(i);
                    else
                        break;

                //删除空白行
                if (i < lvExpTPN.Items.Count && string.IsNullOrWhiteSpace(((ExpTPNRecipe)lvExpTPN.Items[i]).RecipeID))
                    lvExpTPN.Items.RemoveAt(i);

                //删除前面行
                i = lvExpTPN.SelectedIndex;
                while (i >= 0)
                {
                    item = (ExpTPNRecipe)lvExpTPN.Items[i];
                    if (item.RecipeID.Equals(rcpID))
                    {
                        if (!string.IsNullOrWhiteSpace(item.No))
                            no = Convert.ToInt32(item.No);

                        lvExpTPN.Items.RemoveAt(i);
                    }
                    else
                        break;

                    i--;
                }

                //重新生成序号 
                no = 1;
                for (i= 0; i < lvExpTPN.Items.Count; i++)
                {
                    item = (ExpTPNRecipe)lvExpTPN.Items[i];
                    if (!string.IsNullOrWhiteSpace(item.RecipeID) && !string.IsNullOrWhiteSpace(item.BedNo))
                        item.No = (no++).ToString();
                }

                lvExpTPN.Items.Refresh();
            }
            else
                lvExpIntervene.Items.RemoveAt(lvExpIntervene.SelectedIndex);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            this.clear();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            ListView lv = null;
            string title = "";
            if (true == rdoTPN.IsChecked)
            {
                title += rdoTPN.Content;
                lv = lvExpTPN;
            }
            else
            {
                title += rdoIntervene.Content;
                lv = lvExpIntervene;
            }

            if (0 == lv.Items.Count)
            {
                BLPublic.Dialogs.Alert("没有可导出的内容");
                return;
            }
             
            
            title += "(" + DateTime.Now.ToString("yyyy.M.d") + ")";

            string fileName = "";
            Microsoft.Win32.SaveFileDialog frm = new Microsoft.Win32.SaveFileDialog();
            frm.Filter = "Excel文件(*.xls,xlsx)|*.xls;*.xlsx"; 
            frm.FileName = title + ".xls";
            if (true == frm.ShowDialog())
                fileName = frm.FileName;
            else
                return;

            try
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);
            }
            catch(Exception ex)
            {
                BLPublic.Dialogs.Error("创建导出文件失败:" + ex.Message);
                return;
            }

            bool ok = false;
            using (ExcelPackage excelPgk = new ExcelPackage(new FileInfo(fileName)))
            {
                ExcelWorkbook workBook = excelPgk.Workbook;
                if (null == workBook) 
                {
                    BLPublic.Dialogs.Error("无法创建Excel文件");
                    return;
                }
                    
                ExcelWorksheet sheet = null;
                try
                {
                    if (0 >= workBook.Worksheets.Count)
                        sheet = workBook.Worksheets.Add("导出");

                    else
                        sheet = workBook.Worksheets[0];
                }
                catch (Exception ex)
                {
                    BLPublic.Dialogs.Error("无法创建Excel文件:" + ex.Message);
                    return;
                }
                 
                if (true == rdoTPN.IsChecked)
                    ok = exportTPN2Excel(sheet);
                else
                    ok = exportIntervene2Excel(sheet);

                if (ok)
                    excelPgk.Save(); 
            }

            if (ok)
                BLPublic.Dialogs.Info("导出成功"); 
        }

         
        private bool exportTPN2Excel(ExcelWorksheet _xslSht)
        {
            GridView gv = (GridView)lvExpTPN.View;

            //标题行
            _xslSht.Cells[1, 1].Style.Font.Size = 20;
            _xslSht.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            _xslSht.Cells[1, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            _xslSht.Cells[1, 1].Value = rdoTPN.Content;
            _xslSht.Row(1).Height = 40;
            //标题行合并
            _xslSht.Cells[1, 1, 1, gv.Columns.Count].Merge = true;

            //列头
            for (int i = 0; i < gv.Columns.Count; i++)
            {
                _xslSht.Cells[2, i + 1].Value = gv.Columns[i].Header;
                _xslSht.Cells[2, i + 1].Style.Font.Bold = true;
                _xslSht.Cells[2, i + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                _xslSht.Cells[2, i + 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                _xslSht.Column(i + 1).Width = gv.Columns[i].Width * 0.15f;
            }
            _xslSht.Column(16).Width = gv.Columns[15].Width * 0.3f;

            //内容
            ExpTPNRecipe item = null;
            string prvRcpID = "";
            int R = 2, num = 0;
            for (int i = 0; i < lvExpTPN.Items.Count; i++)
            {
                item = (ExpTPNRecipe)lvExpTPN.Items[i];
                if (!prvRcpID.Equals(item.RecipeID))
                {
                    if ((2 <= num) && (3 <= R))
                        _xslSht.Row(R).Height = 18 * num;

                    prvRcpID = item.RecipeID;
                    R++;
                    num = 1;
                    _xslSht.Row(R).Height = 20;
                    _xslSht.Row(R).CustomHeight = true;
                    _xslSht.Cells[R, 1].Value = item.No;
                    _xslSht.Cells[R, 2].Value = item.WardName;
                    _xslSht.Cells[R, 3].Value = item.BedNo;
                    _xslSht.Cells[R, 4].Value = item.PatientName;
                    _xslSht.Cells[R, 5].Value = item.HospitalNo;
                    _xslSht.Cells[R, 6].Value = item.Sex;
                    _xslSht.Cells[R, 7].Value = item.Age;
                    _xslSht.Cells[R, 8].Value = item.Weight;
                    _xslSht.Cells[R, 9].Value = item.Height;
                    _xslSht.Cells[R, 10].Value = item.BMI;
                    _xslSht.Cells[R, 11].Value = item.InHospitalTime;
                    _xslSht.Cells[R, 12].Value = item.Diagnose;
                    _xslSht.Cells[R, 13].Value = item.TPNUseTime;
                    _xslSht.Cells[R, 14].Value = item.Drugs;
                    _xslSht.Cells[R, 15].Value = item.IsOK;
                    _xslSht.Cells[R, 16].Value = item.NoOKReason;

                    _xslSht.Cells[R, 14].Style.WrapText = true;
                    _xslSht.Cells[R, 16].Style.WrapText = true;
                }
                else
                {
                    num++;
                    //if (!string.IsNullOrWhiteSpace(item.Drug))
                    //    _xslSht.Cells[R, 14].Value += "\r\n" + item.Drug + "  " + item.Dosage;

                    if (!string.IsNullOrWhiteSpace(item.NoOKReason))
                        _xslSht.Cells[R, 16].Value += "\r\n" + item.NoOKReason;
                }
            }

            if (3 <= R)
            {
                if (2 <= num)
                    _xslSht.Row(R).Height = 18 * num;

                //边框线
                _xslSht.Cells[2, 1, R, 16].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                _xslSht.Cells[3, 1, R, 16].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                _xslSht.Cells[3, 15, R, 15].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }

            return true;
        }

        private bool exportIntervene2Excel(ExcelWorksheet _xslSht)
        {
            GridView gv = (GridView)lvExpIntervene.View;
             
            //标题行
            _xslSht.Cells[1, 1].Style.Font.Size = 20;
            _xslSht.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            _xslSht.Cells[1, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            _xslSht.Cells[1, 1].Value = rdoIntervene.Content;
            _xslSht.Row(1).Height = 40;
            //标题行合并
            _xslSht.Cells[1, 1, 1, gv.Columns.Count].Merge = true;


            //列头
            _xslSht.Cells[2, 1].Value = gv.Columns[0].Header;
            _xslSht.Cells[2, 2].Value = gv.Columns[1].Header;
            _xslSht.Cells[2, 3].Value = "干预内容";
            _xslSht.Cells[2, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            _xslSht.Cells[2, 3].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

            for (int i = 2; i < gv.Columns.Count; i++) 
                _xslSht.Cells[3, i + 1].Value = gv.Columns[i].Header;
            
            for (int i = 0; i < gv.Columns.Count; i++) 
            {
                _xslSht.Cells[3, i + 1].Style.Font.Bold = true;
                _xslSht.Cells[3, i + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                _xslSht.Cells[3, i + 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                _xslSht.Column(i + 1).Width = gv.Columns[i].Width * 0.15f;
            }

            _xslSht.Cells[2, 1, 3, 1].Merge = true;
            _xslSht.Cells[2, 2, 3, 2].Merge = true;
            _xslSht.Cells[2, 3, 2, gv.Columns.Count].Merge = true;

            //内容
            ExpIntervene item = null;
            int R = 4;
            for (int i = 0; i < lvExpIntervene.Items.Count; i++)
            {
                item = (ExpIntervene)lvExpIntervene.Items[i];
                _xslSht.Row(R).Height = 20;
                _xslSht.Row(R).CustomHeight = true;
                _xslSht.Cells[R, 1].Value = item.IDate;
                _xslSht.Cells[R, 2].Value = item.HospitalNo;
                _xslSht.Cells[R, 3].Value = item.SYZ;
                _xslSht.Cells[R, 4].Value = item.TND;
                _xslSht.Cells[R, 5].Value = item.AJSND;
                _xslSht.Cells[R, 6].Value = item.DJZ;
                _xslSht.Cells[R, 7].Value = item.RDB;
                _xslSht.Cells[R, 8].Value = item.TZB;
                _xslSht.Cells[R, 9].Value = item.YDS;
                _xslSht.Cells[R, 10].Value = item.Drugs;
                _xslSht.Cells[R, 11].Value = item.Dosage;
                _xslSht.Cells[R, 2].Style.Numberformat.Format = "@";

                R++;
            }

            if (5 <= R)
            {
                //边框线
                _xslSht.Cells[2, 1, R - 1, 11].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                _xslSht.Cells[3, 1, R - 1, 11].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
            }

            return true;
        }

        private void rdoTPN_Click(object sender, RoutedEventArgs e)
        {
            lvExpTPN.Visibility = System.Windows.Visibility.Collapsed;
            lvExpIntervene.Visibility = System.Windows.Visibility.Collapsed;

            if (true == rdoTPN.IsChecked)
                lvExpTPN.Visibility = System.Windows.Visibility.Visible;
            else
                lvExpIntervene.Visibility = System.Windows.Visibility.Visible; 
        }

        private void btnAllPatient_Click(object sender, RoutedEventArgs e)
        {
            if (null == this.getPatientLst)
            {
                BLPublic.Dialogs.Error("未实现导出全部患者");
                return;
            }

            Button btn = (Button)sender;
            btn.IsEnabled = false;
            btn.Content = "正在添加患者...";
            UIHelper.DoEvents();

            this.clear();

            List<PatientModel> lstPnt = this.getPatientLst();
            string dig = "";
            int len = lstPnt.Count;
            for (int i = 0; i < len; i++)
            { 
                btn.Content = string.Format("正在添加患者 {0}/{1}", i + 1, len);
                UIHelper.DoEvents();

                if (string.IsNullOrWhiteSpace(lstPnt[i].Diagnose))
                    if (getDiagnose(AppConst.db, lstPnt[i].PatientCode, ref dig))
                        lstPnt[i].Diagnose = dig;

                setPatient(lstPnt[i]);
            }

            btn.Content = "添加全部患者";
            btn.IsEnabled = true;
        }
    }

    class ExpTPNRecipe
    { 
        public string RecipeID { get; set; }
        public string No { get; set; }
        public string WardName { get; set; }
        public string BedNo { get; set; }
        public string PatientCode { get; set; }
        public string PatientName { get; set; }
        public string HospitalNo { get; set; }
        public string Sex { get; set; }
        public string Age { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string BMI { get; set; }
        public string InHospitalTime { get; set; }
        public string Diagnose { get; set; }
        public string TPNUseTime { get; set; }
        public string Drugs { get; set; }
        public string Dosage { get; set; }
        public string IsOK { get; set; }
        public string NoOKReason { get; set; }
        public DateTime StartTime { get; set; }
    }

    class ExpIntervene
    {
        public string PatientCode { get; set; }
        public string IDate { get; set; }
        public string HospitalNo { get; set; }
        public string SYZ { get; set; }
        public string TND { get; set; }
        public string AJSND { get; set; }
        public string DJZ { get; set; }
        public string RDB { get; set; }
        public string TZB { get; set; }
        public string YDS { get; set; }
        public string Drugs { get; set; }
        public string Dosage { get; set; }
    }
}
