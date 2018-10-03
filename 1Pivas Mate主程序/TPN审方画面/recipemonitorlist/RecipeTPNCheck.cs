using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace recipemonitorlist
{ 
    public class RecipeTPNCheck
    {
        private static DataTable tblMntSrc = null;

        public static void loadTPNCheck(ListBox _lbChkResult, BLPublic.DBOperate _db, string _recipeID, 
            TextBlock _txtResult=null)
        {
            if ((null == _lbChkResult) || string.IsNullOrWhiteSpace(_recipeID))
                return;

            _lbChkResult.Items.Clear();
            if (null != _txtResult)
                _txtResult.Text = "";

            if (null == tblMntSrc)
                tblMntSrc = new DataTable();
            else
                tblMntSrc.Clear();

            if (!_db.GetRecordSet(string.Format(SQL.SEL_ORD_TPNMNT, _recipeID), ref tblMntSrc))
                if (null != _txtResult) 
                    _txtResult.Text = "加载错误," + _db.Error; 

            IDataReader dr = null;
            //显示TPN审方结果
            if (_db.GetRecordSet(string.Format(SQL.SEL_TPNMNT, _recipeID), ref dr))
            {
                BLPublic.BLDataReader bldr = new BLPublic.BLDataReader(dr);
                while (bldr.next())
                {
                    _lbChkResult.Items.Add(new MonitorResult
                    {
                        ResultID = bldr.getInt("TPNCheckResultID"),
                        Title = bldr.getString("ResultType"),
                        DrugName = getMonitorItems(bldr.getInt("SourceID")),
                        ResultDesc = bldr.getString("ResultDesc"),
                        CheckLevel = bldr.getInt("ResultLevel"),
                        ReferenName = bldr.getString("ReferenceName"),
                        CheckTime = bldr.getDateTime("CheckDT").ToString("yyyy-M-d h:mm:ss")
                    });
                }

                bldr.close();
            }
            else
            {
                if (null != _txtResult)
                    _txtResult.Text += "加载TPN审方失败:" + _db.Error;
                else
                    BLPublic.Dialogs.Error("加载TPN审方失败:" + _db.Error);
            } 


            if (_db.GetRecordSet(string.Format(SQL.SEL_CUSTOM_CHK_BYRCP, _recipeID), ref dr))
            {
                BLPublic.BLDataReader bldr = new BLPublic.BLDataReader(dr);
                while (bldr.next())
                {
                    _lbChkResult.Items.Add(new MonitorResult
                    {
                        ResultID = bldr.getInt("CustomCheckResultID"),
                        IsCustom = true,
                        Title = "自定义",
                        DrugName = "",
                        ResultDesc = bldr.getString("ResultDesc"),
                        CheckLevel = bldr.getInt("ResultLevel"),
                        ReferenName = "",
                        CheckTime = bldr.getDateTime("CheckDT").ToString("yyyy-M-d h:mm:ss")
                    });
                }
                dr.Close();
            }
            else
            {
                if (null != _txtResult)
                    _txtResult.Text += "加载自定义审方失败:" + _db.Error;
                else
                    BLPublic.Dialogs.Error("加载自定义审方失败:" + _db.Error);
            }

            if (null != _txtResult)
                if (string.IsNullOrWhiteSpace(_txtResult.Text))
                {
                    if (0 == _lbChkResult.Items.Count)
                    {
                        _txtResult.Text = "通过";
                        _txtResult.Foreground = new SolidColorBrush(Colors.Green);
                    }
                }
                else 
                    _txtResult.Foreground = new SolidColorBrush(Colors.Red);
        }

         
        /// <summary>
        /// 获取审方结果关联TPN项目值
        /// </summary>
        /// <param name="_srcID"></param>
        /// <returns></returns>
        private static string getMonitorItems(int _srcID)
        {
            string rt = "";
            DataRow[] rows = tblMntSrc.Select("TPNMonitorID=" + _srcID.ToString());
            if (null != rows) 
                foreach (DataRow r in rows)
                {
                    if (!string.IsNullOrWhiteSpace(rt))
                        rt += "，";

                    rt += r["ItemName"].ToString() + "=" + r["ItemValue"].ToString() + r["Unit"].ToString();

                }  

            return rt;
        }
    }

    /// <summary>
    /// 患者信息
    /// </summary>
    public class PatientInfo
    {
        public string PatientCode { get; set; }
        public string WardName { get; set; }
        public string BedNo { get; set; }
        public string HospitalNo { get; set; }
        public string PatientName { get; set; }
        public string Diagnose { get; set; }
        public DateTime Birthday { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
    }

    /// <summary>
    /// 审方结果
    /// </summary>
    public class MonitorResult
    {
        public int ResultID { set; get; }
        public bool IsMonitorTitle { set; get; }
        public bool IsCustom { set; get; }
        public string Title { set; get; }
        public string DrugName { set; get; }
        public string ResultDesc { set; get; }
        public string ReferenName { set; get; }
        public string CheckTime { set; get; }
        public int CheckLevel { set; get; } 
    }
}
