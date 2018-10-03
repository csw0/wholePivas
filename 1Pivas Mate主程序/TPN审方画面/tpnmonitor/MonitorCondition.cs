using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tpnmonitor
{

    /// <summary>
    /// TPN项目
    /// </summary>
    public class TPNItem
    {
        public TPNItem(int _id, string _type, string _code, string _name, string _value, string _unit, int _seqno)
        {
            this.ID = _id;
            this.Type = _type;
            this.Code = _code;
            this.Name = _name;
            this.Value = _value;
            this.Unit = _unit;
            this.SeqNo = _seqno;
        }
        public TPNItem(int _id, string _noramlvalue, string _value, int _seqno)
        {
            this.ID = _id; 
            this.NormalValue = _noramlvalue;
            this.Value = _value; 
            this.SeqNo = _seqno;
        }

        public int ID { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string NormalValue { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }
        public int SeqNo { get; set; }
    }

    /// <summary>
    /// TPN项目审核结果
    /// </summary>
    public class TPNItemMonitor
    {
        public TPNItemMonitor(int _mntid, int _seqno, string _domain)
        {
            this.MonitorID = _mntid;
            this.SeqNo = _seqno;
            this.Domain = _domain;
            this.DeviateValue = 0;
            this.DeviatePer = 0;
        }

        public int MonitorID { get; set; }
        public int SeqNo { get; set; }

        /// <summary>
        /// 审核判断时使用的值域
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// 审核结果偏离值(小于审核值时为负数)
        /// </summary>
        public double DeviateValue { get; set; }
        /// <summary>
        /// 审核结果偏离值百分比(小于审核值时为负数)
        /// </summary>
        public double DeviatePer { get; set; }
    }

    /// <summary>
    /// 处方制剂信息
    /// </summary>
    public class RecipePrep
    {
        public RecipePrep(int _id, double _dos, double _cap, double _quan)
        {
            this.ID = _id;
            this.Dosage = _dos;
            this.Capacity = _cap;
            this.Quantity = _quan;

            this.TPNValues = new Dictionary<string, double>(32);
        }

        public int ID { get; set; }
        public double Dosage { get; set; }
        public double Capacity { get; set; }
        public double Quantity { get; set; }
        /// <summary>
        /// 制剂TPN项目值
        /// </summary>
        public IDictionary<string, double> TPNValues { get; set; }
    }

    /// <summary>
    /// 审核设置
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 是否审核所有溶媒
        /// </summary>
        public bool CalAllCapacity { get; set; }
        /// <summary>
        /// 可作为容积计算的最小值
        /// </summary>
        public Single CalMinCapacity { get; set; }
    }

    /// <summary>
    /// 值域
    /// </summary>
    public class DataDomain
    {
        public DataDomain(string _domain)
        {
            this.Domain = _domain;
            this.HadMinValue = false;
            this.HadMaxValue = false;
            this.IncMinValue = false;
            this.IncMaxValue = false;
            this.Values = new List<double>();
        }

        public string Domain { set; get; }

        public bool HadMinValue { set; get; }
        public bool HadMaxValue { set; get; }
        public bool IncMinValue { set; get; }   //包含最大值
        public bool IncMaxValue { set; get; }   //包含最小值

        public List<double> Values { set; get; }
    }

    /// <summary>
    /// 审方条件类
    /// </summary>
    class MonitorCondition : DataDomain
    {
        public MonitorCondition(int _mntID, string _type, string _item, string _cdt, bool _isNomral)
            : base(_cdt)
        {
            this.MonitorID = _mntID;
            this.ItemType = _type;
            this.ItemCode = _item;
            this.IsIn = _isNomral;
        }

        public int MonitorID { set; get; }
        public string ItemType { set; get; }
        public string ItemCode { set; get; }
        public bool IsIn { set; get; }

    }

    /// <summary>
    /// 审核条件解析（解析数学公式）
    /// </summary>
    public class ConditionPaser
    {
        private string Error = "";

        public bool compare(DataDomain _cdt, double _val)
        {
            return 0 == compareDomain(_cdt, _val);
        }

        public int compareDomain(DataDomain _cdt, double _val)
        {
            if (null == _cdt)
                return -99;

            if (0 == _cdt.Values.Count)
            {
                this.Error = "没有值域内容";
                return -99;
            }

            if (_cdt.HadMinValue && _cdt.HadMaxValue && (2 != _cdt.Values.Count))
            {
                this.Error = "缺少最大值或最小值";
                return -99;
            }

            //列举值
            if (
                (2 < _cdt.Values.Count) ||
                (!_cdt.HadMinValue && !_cdt.HadMaxValue)
                )
                return _cdt.Values.Contains(_val) ? 0 : -99;

            bool isIn = false;
            if (_cdt.HadMinValue)
            {
                if (_cdt.IncMinValue)
                    isIn = (_val >= _cdt.Values[0]);
                else
                    isIn = (_val > _cdt.Values[0]);

                if (!isIn)
                    return -1;
            }

            if (_cdt.HadMaxValue)
            {
                int vIndex = 1;
                if (!_cdt.HadMinValue)
                    vIndex = 0;

                if (_cdt.IncMaxValue)
                    isIn = (_val <= _cdt.Values[vIndex]);
                else
                    isIn = (_val < _cdt.Values[vIndex]);

                if (!isIn)
                    return 1;
            }

            return isIn ? 0 : -99;
        }

        /// <summary>
        /// 解析表达式，分离表达式值
        /// </summary>
        /// <param name="_cdt"></param>
        public void paser(DataDomain _cdt)
        {
            if (null == _cdt)
                return;

            _cdt.Values.Clear();
            _cdt.HadMinValue = false;
            _cdt.HadMaxValue = false;
            _cdt.IncMinValue = false;
            _cdt.IncMaxValue = false;

            string domain = _cdt.Domain;

            if (string.IsNullOrWhiteSpace(domain))
                return;

            domain = domain.Replace("，", ",");
            domain = domain.Replace("。", ".");

            char[] arrCdt = domain.ToCharArray();
            int i = 0, l = arrCdt.Length - 1;
            bool readVal = true;
            string val = "";
            while (i <= l)
            {
                if (('>' == arrCdt[i]) || ('<' == arrCdt[i]))
                {
                    if (readVal)
                    {
                        addDomainVal(_cdt, val);
                        readVal = false;
                        val = "";
                    }


                    if ('>' == arrCdt[i])
                    {
                        _cdt.HadMinValue = true;
                        i++;
                        if ('=' == arrCdt[i])
                            _cdt.IncMinValue = true;
                        else
                            val = arrCdt[i].ToString();
                    }
                    else
                    {
                        _cdt.HadMaxValue = true;
                        if (i == l)
                            this.Error = "表达式右边没有值";
                        else
                        {
                            i++;
                            if ('=' == arrCdt[i])
                                _cdt.IncMaxValue = true;
                            else
                                val = arrCdt[i].ToString();
                        }
                    }

                    readVal = true;
                }
                else if (('(' == arrCdt[i]) || ('[' == arrCdt[i])) //最小值
                {
                    if (readVal)
                    {
                        addDomainVal(_cdt, val);
                        readVal = false;
                        val = "";
                    }

                    if (i == l) //最后
                        this.Error = "表达式右边没有值";

                    else
                    {
                        _cdt.HadMinValue = true;
                        if ('[' == arrCdt[i])
                            _cdt.IncMinValue = true;

                        readVal = true;
                    }
                }
                else if ((')' == arrCdt[i]) || (']' == arrCdt[i])) //最大值
                {
                    if (readVal)
                    {
                        addDomainVal(_cdt, val);
                        readVal = false;
                        val = "";
                    }

                    _cdt.HadMaxValue = true;

                    if (i == 0)
                        this.Error = "表达式左边没有值";

                    else if (']' == arrCdt[i])
                        _cdt.IncMaxValue = true;

                }
                else if (',' == arrCdt[i])
                {
                    if (readVal)
                        addDomainVal(_cdt, val);

                    readVal = true;
                    val = "";
                }
                else if (readVal)
                    val += arrCdt[i].ToString();

                i++;
            }

            if (readVal)
            {
                addDomainVal(_cdt, val);
                readVal = false;
                val = "";
            }

            if (_cdt.HadMinValue && _cdt.HadMaxValue && (2 == _cdt.Values.Count))
            {
                //交换，[0]为最小值
                if (_cdt.Values[0] > _cdt.Values[1])
                {
                    double tmp = _cdt.Values[0];
                    _cdt.Values[0] = _cdt.Values[1];
                    _cdt.Values[1] = tmp;
                }
            }
        }
        private void addDomainVal(DataDomain _cdt, string _val)
        {
            if (string.IsNullOrWhiteSpace(_val))
                return;

            if ("-∞".Equals(_val))
                _cdt.HadMinValue = false;

            else if ("+∞".Equals(_val))
                _cdt.HadMaxValue = false;

            else
                try
                {
                    _cdt.Values.Add(Convert.ToDouble(_val));
                }
                catch (Exception ex)
                {
                    this.Error = "值(" + _val + ")格式错误.";
                }
        }
    }
}
