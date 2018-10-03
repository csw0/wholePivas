using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace tpnmonitor
{
    /// <summary>
    /// TPN项目计算
    /// </summary>
    public class TPNCalculator 
    {
        public readonly  int[,] UNITCVT = { //[0]单位转换{单位标志(同于一种类单位一样)，[1]单位ID，[2]转换量}
                                            //* 顺序必须和转换级别一致，从大到小, 比如 g...ug
                                            //克转换
                                            { 1, 1, 1 },        //kg
                                            { 1, 2, 1000 },     //g
                                            { 1, 25, 1000 },    //mg
                                            { 1, 26, 1000 },    //ug
                                            //升转换
                                            { 2, 17, 1 },       //l
                                            { 2, 18, 1000 },    //ml
                                            //摩尔
                                            { 3, 5, 1 },        //mol
                                            { 3, 4, 1000 },     //mmol
                                            { 3, 3, 1000 }      //umol
                                           };

        private const string SEL_TPN_ITEMS = "SELECT TPNItemID, ItemCode, ItemType, ItemName, Express, Unit " +
            "FROM tpn.dbo.TPNItem";
        private const string SEL_PREP_THING = "SELECT UniPreparationID, Content, ContentUnit UnitID, " +
            "('thing.' + CAST(ContentThingID AS VARCHAR(32))) AS Code " +
            "FROM KD0100.dbo.[UniPreparation-ContentThing] uc (NOLOCK) WHERE UniPreparationID IN({0}) " +
            " AND EXISTS(SELECT 1 FROM tpn.dbo.TPNItem i WHERE uc.ContentThingID=i.OriginID AND i.ItemType='thing' ) " +
            "UNION ALL " +
            "SELECT UniPreparationID, Content, ContentUnit UnitID, " +
            "('comp.' + CAST(MedComponentID AS VARCHAR(32))) AS Code " +
            "FROM KD0100.dbo.[UniPreparation-MedComponent] um (NOLOCK) WHERE UniPreparationID IN({0}) " +
            "AND EXISTS(SELECT 1 FROM tpn.dbo.TPNItem i WHERE um.MedComponentID=i.OriginID AND i.ItemType='comp' ) ";

        private BLPublic.DBOperate db = null;
        private DataTable tblTPNItem = null;    //TPN计算项目
        private DataTable tblPrepThing = null;  //制剂所含物  
        private List<string> useLISItem = null;   //使用到的LIS检查项目编码
        private Dictionary<string, int> dicSysUnit = null;
        private string hadInitPrepIDs = "";     //已加载数据的制剂ID
         

        MathParserTK.MathParser parser = null;
        private Dictionary<string, double> ordersVal = null;    //医嘱患者检查值(实验室和体格)、患者年龄、患者性别、医嘱容积等
        private Dictionary<int, RecipePrep> prepInfo = null;    //医嘱制剂数量

        public string Error { get; set; }

        /// <summary>
        /// 计算初始化。加载TPN项目
        /// </summary>
        /// <param name="_db"></param>
        /// <returns></returns>
        public bool init(BLPublic.DBOperate _db)
        {
            this.db = _db;
            this.useLISItem = new List<string>();
            this.dicSysUnit = new Dictionary<string, int>();

            dicSysUnit.Add("千克", 1);
            dicSysUnit.Add("kg", 1);
            dicSysUnit.Add("克", 2);
            dicSysUnit.Add("g", 2);
            dicSysUnit.Add("毫克", 25);
            dicSysUnit.Add("mg", 25);
            dicSysUnit.Add("微克", 26);
            dicSysUnit.Add("μg", 26);
            dicSysUnit.Add("ug", 26);

            dicSysUnit.Add("毫升", 17);
            dicSysUnit.Add("ml", 17);
            dicSysUnit.Add("升", 18);
            dicSysUnit.Add("l", 18);

            dicSysUnit.Add("摩尔", 5);
            dicSysUnit.Add("mol", 5);
            dicSysUnit.Add("毫摩尔", 4);
            dicSysUnit.Add("毫摩", 4);
            dicSysUnit.Add("mmol", 4);
            dicSysUnit.Add("微摩尔", 3);
            dicSysUnit.Add("微摩", 3);
            dicSysUnit.Add("μmol", 3);
            dicSysUnit.Add("umol", 3);

            /*dicSysUnit.Add("焦耳", 8);
            dicSysUnit.Add("j", 8);
            dicSysUnit.Add("千焦", 9);
            dicSysUnit.Add("KJ", 9);*/
              
            return loadTPNItem();
        }

        /// <summary>
        /// 计算TPN医嘱项目值
        /// </summary>
        /// <param name="_patientChk">患者检查值</param>
        /// <param name="_prepQuantity">制剂用量</param>
        /// <returns></returns>
        public bool Calculate(Dictionary<string, double> _ordersVal, Dictionary<int, RecipePrep> _prepQuantity)
        {
            return Calculate(_ordersVal, _prepQuantity, null);
        }
         
        /// <summary>
        /// 计算单个TPN项目值
        /// </summary>
        /// <param name="_ordersVal"></param>
        /// <param name="_prepQuantity"></param>
        /// <param name="_itemCode">要计算的TPN项目编码, null时计算全部</param>
        /// <returns></returns>
        public bool Calculate(Dictionary<string, double> _ordersVal, Dictionary<int, RecipePrep> _prepQuantity,
            string _itemCode)
        {
            if ((null == _ordersVal) || (null == _prepQuantity))
            {
                this.Error = "未指定计算参数";
                return false;
            }

            if ((null == this.tblTPNItem) || (0 == this.tblTPNItem.Rows.Count))
            {
                this.Error = "没有可计算的TPN项目";
                return false;
            }
             
            foreach(DataRow row in this.tblTPNItem.Rows)
            {
                row["Value"] = 0.0;
                row["Result"] = "";
                row["Lock"] = false;
                row["IsCal"] = false;
            }

            this.ordersVal = _ordersVal;
            this.prepInfo = _prepQuantity;
            string newPrepIDs = "";
            //检查制剂是否存在
            if (null != this.tblPrepThing)
            {
                foreach(int prepID in this.prepInfo.Keys)
                {
                    if (!this.hadInitPrepIDs.Contains("," + prepID.ToString() + ","))
                        newPrepIDs += prepID.ToString() + ",";
                }
            }
            else
                foreach(int prepID in this.prepInfo.Keys)  
                    newPrepIDs += prepID.ToString() + ",";

            if (!string.IsNullOrWhiteSpace(newPrepIDs))
                if (!loadPrepThing(newPrepIDs))
                    return false;

            //计算
            if (null == this.parser)
                this.parser = new MathParserTK.MathParser();


            this.Error = "";
            if (null == _itemCode)
            {
                foreach (DataRow row in this.tblTPNItem.Rows)
                    CalculateItem(row);
            }
            else
            {
                DataRow row = this.tblTPNItem.Rows.Find(_itemCode);
                if (null == row)
                {
                    this.Error = "未找到此编码(" + _itemCode + ")对应的项目";
                    return false;
                }
                else
                    CalculateItem(row);
            }
            
            return true;
        }

        /// <summary>
        /// 去除单位里多余的字符(空格，逗号等)
        /// </summary>
        /// <param name="_unit"></param>
        /// <returns></returns>
        private string fmtUnit(string _unit)
        {
            if (string.IsNullOrWhiteSpace(_unit))
                return "";

            return _unit.Replace(" ", "").Replace(",", "");
        }

        /// <summary>
        /// 获取TPN项目值(字符形式)
        /// </summary>
        /// <param name="result"></param>
        public void getCalResult(IList<TPNItem> result, bool _onlyHadVal=true)
        {
            if (null == result)
                return;

            foreach (DataRow row in this.tblTPNItem.Rows)
            {
                if (!_onlyHadVal || (bool)row["IsCal"])
                    result.Add(new TPNItem(Convert.ToInt32(row["TPNItemID"].ToString()), row["ItemType"].ToString(),
                                                           row["ItemCode"].ToString(), row["ItemName"].ToString(), 
                                                           row["Result"].ToString(),
                                                           row.IsNull("Unit") ? "" : fmtUnit(row["Unit"].ToString()), 
                                                           0)); 
            }
        }

        /// <summary>
        /// 获取TPN项目值
        /// </summary>
        /// <param name="result"></param>
        public void getCalResult(IDictionary<string, TPNItem> result, bool _onlyHadVal = true)
        {
            if (null == result)
                return;

            foreach (DataRow row in this.tblTPNItem.Rows)
            {
                if (!_onlyHadVal || (bool)row["IsCal"])
                    result.Add(row["ItemCode"].ToString(),
                               new TPNItem(Convert.ToInt32(row["TPNItemID"].ToString()), row["ItemType"].ToString(),
                                                           row["ItemCode"].ToString(), row["ItemName"].ToString(), 
                                                           row["Result"].ToString(),
                                                           row.IsNull("Unit") ? "" : row["Unit"].ToString(), 0));
            }
        }

        /// <summary>
        /// 获取TPN项目值(数字形式)
        /// </summary>
        /// <param name="result"></param>
        public void getItemValue(Dictionary<string, double> value)
        {
            if (null == value)
                return;

            foreach (DataRow row in this.tblTPNItem.Rows)
            {
                if (0 < Convert.ToDouble(row["Value"].ToString()))
                    value.Add(row["TPNItemID"].ToString(), Convert.ToDouble(row["Value"].ToString()));
            }
        }

        /// <summary>
        /// 获取项目信息
        /// </summary>
        /// <param name="_itemCode"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool getItemInfo(string _itemCode, Dictionary<string, string> itemInfo)
        {
            if (null == itemInfo)
                return false;

            itemInfo.Clear();

            if (string.IsNullOrWhiteSpace(_itemCode))
            {
                this.Error = "未指定项目编码";
                return false;
            }

            DataRow row = this.tblTPNItem.Rows.Find(_itemCode);
            if (null == row)
            {
                this.Error = "未找到此编码(" + _itemCode + ")对应的项目";
                return false;
            }

            itemInfo.Add("TPNItemID", row["TPNItemID"].ToString());
            itemInfo.Add("ItemType", row["ItemType"].ToString());
            itemInfo.Add("ItemCode", row["ItemCode"].ToString());
            itemInfo.Add("ItemName", row["ItemName"].ToString());
            itemInfo.Add("Express", row["Express"].ToString());
            itemInfo.Add("Unit", row["Unit"].ToString());
            itemInfo.Add("Value", row["Value"].ToString());

            return true;
        }

        /// <summary>
        /// 获取TPN项目使用到的LIS检查项目
        /// </summary>
        /// <returns></returns>
        public string getUseLISItem()
        {
            if ((null == this.useLISItem) || (0 == this.useLISItem.Count))
                return "";

            string rt = "";
            foreach(string i in this.useLISItem)
            {
                if (!string.IsNullOrWhiteSpace(rt))
                    rt += ",";

                rt += "'" + i + "'";
            }

            return rt;
        }

        /// <summary>
        /// 获取表达式中的项目
        /// </summary>
        /// <param name="_exp"></param>
        /// <param name="_items"></param>
        public static void getExpressItem(string _exp, List<string> _items)
        {
            if (string.IsNullOrWhiteSpace(_exp) || (null == _items))
                return;

            string expitem = "";
            char[] chr = _exp.ToCharArray();
            int i = 0;
            int valStart = -1;
             
            while (i < chr.Length)
            {
                if ('[' == chr[i])
                {
                    valStart = i;
                }
                else if (']' == chr[i])
                {
                    if (0 <= valStart)
                    {
                        expitem = _exp.Substring(valStart + 1, i - valStart - 1);
                        if (!_items.Contains(expitem))
                            _items.Add(expitem);
                    }

                    valStart = -1;
                }

                i++;
            }
        }

        /// <summary>
        /// 获取单位名称对应的系统单位ID
        /// </summary>
        /// <param name="_unit"></param>
        /// <returns></returns>
        private int getSysUnitID(string _unit)
        {
            if (string.IsNullOrWhiteSpace(_unit))
                return 0;

            _unit = _unit.ToLower();
            if (this.dicSysUnit.ContainsKey(_unit))
                return this.dicSysUnit[_unit];
            else
                return 0;
        }

        private double getUnitZoom(int _fromUnitID, int _toUnitID)
        {
            if ((0 >= _fromUnitID) || (0 >= _toUnitID) || (_fromUnitID == _toUnitID))
                return 1;

            int fUIndx = -1, tUIndx = -1;
            for (int i = UNITCVT.GetLength(1) - 1; i >= 0; i-- )
            {
                if (UNITCVT[i, 1] == _fromUnitID)
                    fUIndx = i;
                else if (UNITCVT[i, 1] == _toUnitID)
                    tUIndx = i;
                else if ((0 <= fUIndx) && (0 <= tUIndx))
                    break;
            }

            if ((-1 == fUIndx) || (-1 == tUIndx) || (fUIndx == tUIndx))
                return 1;
            else if (UNITCVT[fUIndx, 0] != UNITCVT[tUIndx, 0]) //不是同一种单位
                return 1;
            else
            {
                double zoom = 1;
                if (fUIndx < tUIndx)
                {
                    while (fUIndx++ <= tUIndx) 
                        zoom *= UNITCVT[fUIndx, 2]; 
                }
                else
                {
                    while (fUIndx > tUIndx)
                        zoom /= UNITCVT[fUIndx--, 2]; 
                }

                return zoom;
            }
        }

        /// <summary>
        /// 获取医嘱里的项目值（患者参数值、制剂相关值）
        /// </summary>
        /// <param name="_itemCode"></param>
        /// <returns></returns>
        private double getValueFromOrders(string _itemCode, string _unit)
        {
            if (this.ordersVal.ContainsKey(_itemCode))
                return this.ordersVal[_itemCode];


            double totalValue = 0;
            double value = 0;
            double unitZoom = 1.0;
            int uniPrepID = 0;
            int unitID = getSysUnitID(_unit);

            foreach (DataRow rowThing in this.tblPrepThing.Select("Code='" + _itemCode + "'"))
            {
                uniPrepID = Convert.ToInt32(rowThing["PrepID"].ToString());
                if (this.prepInfo.ContainsKey(uniPrepID))
                {
                    if (unitID != Convert.ToInt32(rowThing["UnitID"].ToString()))
                        unitZoom = getUnitZoom(Convert.ToInt32(rowThing["UnitID"].ToString()), unitID);
                    else
                        unitZoom = 1.0;

                    value = Convert.ToDouble(rowThing["Content"].ToString()) * this.prepInfo[uniPrepID].Quantity;
                    totalValue += unitZoom * value;

                    if (this.prepInfo[uniPrepID].TPNValues.ContainsKey(_itemCode))
                        this.prepInfo[uniPrepID].TPNValues[_itemCode] += value;
                    else
                        this.prepInfo[uniPrepID].TPNValues.Add(_itemCode, value);
                }
            }

            return totalValue;
        }

        /// <summary>
        /// 计算TPN项目
        /// </summary>
        /// <param name="_row">TPN项目行</param>
        /// <returns></returns>
        private bool CalculateItem(DataRow row)
        { 
            if (null == row)
                return false;
             
            if ("True".Equals(row["Lock"].ToString())) //避免公式循环使用
                return true;

            //直接累加
            if (row.IsNull("Express") || string.IsNullOrWhiteSpace(row["Express"].ToString()))
            {
                row["Value"] = Math.Round(getValueFromOrders(row["ItemCode"].ToString(), row["Unit"].ToString()), 4);
                row["Result"] = row["Value"];
                row["IsCal"] = true;
                return true;
            }
               

            string express = row["Express"].ToString();
            List<string> lstItem = new List<string>();
            int i = 0;

            getExpressItem(express, lstItem);

            //获取变量值
            if (0 < lstItem.Count)
            {
                row["Lock"] = true;
                Dictionary<string, double> lstVal = new Dictionary<string, double>();
                double value = 0;
                DataRow subRow = null;

                foreach (string item in lstItem)
                {
                    value = 0;

                    if (item.Contains("tpnitem."))
                    { 
                        subRow = this.tblTPNItem.Rows.Find(item);
                        if (null == subRow)
                            continue;

                        if (CalculateItem(subRow))
                            if (Convert.ToBoolean(subRow["IsCal"].ToString()))
                                value = Convert.ToDouble(subRow["Value"].ToString());
                            else
                                this.Error += "TPN项目" + item + "无法计算.\r\n";
                    }
                    else if (this.ordersVal.ContainsKey(item))
                        value = this.ordersVal[item];
                    else
                        value = getValueFromOrders(item, "");

                    if (lstVal.ContainsKey(item))
                        lstVal[item] += value;
                    else
                        lstVal.Add(item, value);
                }

                //替换表达式变量值
                foreach (string val in lstVal.Keys)
                    express = express.Replace('[' + val + ']', lstVal[val].ToString());

                row["Lock"] = false;
            }

            if (express.Contains(":"))
            {
                string[] rates = express.Split(':');
                for (i = 0; i < rates.Length; i++)
                {
                    if (!rates[i].Contains(":")) //不能有二重比值
                    try
                    {
                        rates[i] = Math.Round(this.parser.Parse(rates[i], false), 2).ToString(); 
                    }
                    catch(Exception ex)
                    {
                        //
                    } 
                }

                express = rates[0];
                for (i = 1; i < rates.Length; i++)
                    express += ":" + rates[i];

                row["Value"] = "0.0";
                row["Result"] = express;

                if ((2 == rates.Length) && (0 < Convert.ToSingle(rates[1])))
                try
                {
                    double v = Math.Round(this.parser.Parse(rates[0] + "/" + rates[1], false), 2);
                    row["Value"] = v;
                    row["Result"] = Math.Round(v, 2);
                    /*if (1 < v)
                        row["Result"] = v.ToString() + " : 1";
                    else if (0 < v)
                        row["Result"] = "1 : " + Math.Round(1 / v, 2);
                    else
                        row["Result"] = "0";*/
                }
                catch (Exception ex)
                { } 
            }
            else
            {
                try
                {  
                    row["Value"] = Math.Round(this.parser.Parse(express, false), 2); //计算表达式 
                }
                catch(Exception ex)
                {
                    this.Error += "计算TPN项目" + row["ItemName"].ToString() + "失败" + ex.Message + ".\r\n";
                }
                row["Result"] = row["Value"];
            }

            row["IsCal"] = true;

            return true;
        }


        /// <summary>
        /// 加载TPN项目
        /// </summary>
        /// <returns></returns>
        private bool loadTPNItem()
        {
            this.tblTPNItem = new DataTable();

            if (!this.db.GetRecordSet(SEL_TPN_ITEMS, ref this.tblTPNItem))
            {
                this.Error = "加载TPN项目失败:" + this.db.Error;
                return false;
            }

            foreach (DataRow row in this.tblTPNItem.Rows)
                if ("lischk".Equals(row["ItemType"].ToString()))
                {
                    if (!row.IsNull("Express"))
                        getExpressItem(row["Express"].ToString(), this.useLISItem); 
                }

            this.tblTPNItem.PrimaryKey = new DataColumn[] { this.tblTPNItem.Columns["ItemCode"] };
            this.tblTPNItem.Columns.Add("Value", typeof(double));
            this.tblTPNItem.Columns.Add("Result", typeof(string));  //结果字符
            this.tblTPNItem.Columns.Add("Lock", typeof(bool));  //锁定计算
            this.tblTPNItem.Columns.Add("IsCal", typeof(bool));  //是否已计算
             
            return true;
        }
 

        /// <summary>
        /// 加载制剂所含物
        /// </summary>
        /// <returns></returns>
        private bool loadPrepThing(string _prepIDs)
        {
            if (string.IsNullOrWhiteSpace(_prepIDs))
            {
                this.Error = "未指定加载内容的制剂";
                return false;
            }

            if (',' == _prepIDs[_prepIDs.Length - 1])
                _prepIDs = _prepIDs.Substring(0, _prepIDs.Length - 1);

            IDataReader dr = null;
            if (!this.db.GetRecordSet(string.Format(SEL_PREP_THING, _prepIDs), ref dr))
            {
                this.Error = "加载制剂内容失败:" + this.db.Error;
                return false;
            }

            if (null == this.tblPrepThing)
            {
                this.tblPrepThing = new DataTable();
                this.tblPrepThing.Columns.Add("PrepID", typeof(long));
                this.tblPrepThing.Columns.Add("Code", typeof(string));
                this.tblPrepThing.Columns.Add("Content", typeof(double));
                this.tblPrepThing.Columns.Add("UnitID", typeof(int));
            }

            BLPublic.BLDataReader bldr = new BLPublic.BLDataReader(dr);
            DataRow rowThing = null;
            while (bldr.next())
            {
                rowThing = this.tblPrepThing.NewRow();
                rowThing["PrepID"] = bldr.getInt("UniPreparationID");
                rowThing["Code"] = bldr.getString("Code");
                rowThing["Content"] = bldr.getFloat("Content");
                rowThing["UnitID"] = bldr.getInt("UnitID");

                this.tblPrepThing.Rows.Add(rowThing);
                if (!this.hadInitPrepIDs.Contains("," + rowThing["PrepID"] + ","))
                    this.hadInitPrepIDs += "," + rowThing["PrepID"] + ",";
            }
            bldr.close(); 

            return true;
        }
    } 
}
