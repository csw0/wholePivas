using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace TPNReview
{
    /// <summary>
    /// 病人
    /// </summary>
    public class PatientModel
    {
        public string PatientCode { get; set; }
        public string WardCode { get; set; }
        public string WardName { get; set; }
        public string BedNo { get; set; }
        public string HospitalNo { get; set; }
        public string PatientName { get; set; }
        public string Age { get; set; }
        public int AgeMonth { get; set; }
        public string Sex { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string BMI { get; set; }
        public string Albumin { get; set; }
        public string Diagnose { get; set; }
        public string Operate { get; set; }
        public bool IsHospital { get; set; }
        public bool HadNotCheckOrders { get; set; }
        public bool HadTodayOrders { get; set; } 
    }

    public class OrdersModel
    {
        public OrdersModel()
        {
            this.Drugs = new ObservableCollection<OrdersDrugModel>();
        }

        public string RecipeID { get; set; } 
        public string State { get; set; }
        public string GroupNo { get; set; }
        public string OrderTime { get; set; }
        public string StopTime { get; set; }
        public string FreqCode { get; set; }
        public string UseRoute { get; set; } 
        public string Remark { get; set; }
        public string Capacity { get; set; }
        public int CheckLevel { get; set; } 
        public bool IsTPN { get; set; }
        public bool IsStop { get; set; }
        public bool CanMod { get; set; }

        public ObservableCollection<OrdersDrugModel> Drugs { get; set; }
    }

    /// <summary>
    /// 医嘱药品
    /// </summary>
    public class OrdersDrugModel
    {
        public OrdersDrugModel(string _code, string _name, string _spec, string _dos, 
            string _dosunit, string _quan)
        {
            this.Code = _code;
            this.Name = _name;
            this.Spec = _spec;
            this.Dosage = _dos;
            this.DosageUnit = _dosunit;
            this.Quantity = _quan;
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Spec { get; set; }
        public string Dosage { get; set; }
        public string DosageUnit { get; set; }
        public string Quantity { get; set; }
        public override string ToString()
        {
            return this.Name + " " + this.Spec;
        }
    }

    /// <summary>
    /// 检查记录
    /// </summary>
    public class CheckRecord
    {
        public CheckRecord(string _no, string _type, string _typename, string _name, DateTime _time, string _chkemp)
        {
            this.RecordNo = _no;
            this.TypeCode = _type;
            this.CheckType = _typename;
            this.CheckName = _name;
            this.CheckTime = _time.ToString("yyyy-MM-dd HH:mm:ss");
            this.Checker = _chkemp;
        }

        public string RecordNo { get; set; }
        public string TypeCode { get; set; }
        public string CheckType { get; set; }
        public string CheckName { get; set; }
        public string CheckTime { get; set; }
        public string Checker { get; set; }
    }

    /// <summary>
    /// 检查项目
    /// </summary>
    public class CheckItem
    {
        public CheckItem(string _code, string _name, string _unit)
        {
            this.ItemCode = _code;
            this.ItemName = _name; 
            this.Unit = _unit; 
        }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }  
        public string Unit { get; set; }
    }

    /// <summary>
    /// 检查项目记录
    /// </summary>
    public class CheckResult : CheckItem
    {
        public CheckResult(int _seqno, string _resultid, string _code, string _name, string _val, 
            string _unit, string _domain, string _drect)
            : base(_code, _name, _unit)
        {
            this.SeqNo = _seqno;
            this.ResultID = _resultid;
            this.Value = _val; 
            this.Domain = _domain;
            this.ValueDrect = _drect;
        }

        public int SeqNo { get; set; }

        public string ResultID { get; set; }
        public string Value { get; set; } 
        public string Domain { get; set; }
        public string ValueDrect { get; set; }
    }

    public class CheckResult2: CheckResult
    {
        public CheckResult2(string _chkno, string _resultid, string _code, string _name, string _val, 
            string _unit, string _domain, string _drect)
            :base(0, _resultid, _code, _name, _val, _unit, _domain, _drect)
        {
            this.CheckRecordNo = _chkno;
        }

        public string CheckRecordNo { get; set; }
    }

    /// <summary>
    /// 审方记录
    /// </summary>
    class MonitorModel
    {
        public int MonitorID { set; get; }
        public string Condition { set; get; }
        public string ResultType { set; get; }
        public string ResultDesc { set; get; }
        public string Degress { set; get; }
        public string RefName { set; get; }
        public string SetTime { set; get; }
        public string Setter { set; get; }
        public bool IsUse { get; set; }
    }

    /// <summary>
    /// 审方条件
    /// </summary>
    class ConditionModel
    {
        public ConditionModel()
        {
            this.ItemType = "";
            this.ConditionType = ""; 
        }

        public ConditionModel(int _mntID, string _type, string _code, string _name, string _cdtCode, string _cdt, bool _IsNormal)
        {
            this.MonitorID = _mntID;
            this.ItemType = _type;
            this.ItemCode = _code;
            this.ItemName = _name;
            this.ConditionType = "";
            this.ConditionCode = _cdtCode;
            this.Condition = _cdt;
            this.IsIn = _IsNormal;
        }

        public int Index { set; get; }
        public int MonitorID { set; get; }
        public string ItemType { set; get; } 
        public string ItemCode { set; get; }
        public string ItemName { set; get; }
        public string ConditionType { set; get; }
        public string ConditionCode { set; get; }
        public string Condition { set; get; }
        public string LinkName
        { 
            get 
            {
                if ("icd".Equals(ItemType))
                    return "有";
                else if (this.IsDrug)
                    return "⊂";
                else
                    return "∈";
             } 
        }
        public bool IsIn { set; get; }
        public bool IsDrug { get { return "drug".Equals(this.ItemType) || "class".Equals(this.ItemType); } }
        public bool CanInput { get { return !"tpn".Equals(ItemType); } } 
    }

    /// <summary>
    /// 评估记录model
    /// </summary>
    class AnalysisModel
    {
        public int RecordID { get; set; }
        public string TotalScore { get; set; }
        public string RecordTime { get; set; }
        public string Recorder { get; set; } 
    }
}
