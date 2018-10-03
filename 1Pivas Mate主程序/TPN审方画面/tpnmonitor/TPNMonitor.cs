using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace tpnmonitor
{
    public class TPNMonitor
    {
        private BLPublic.DBOperate db = null;
        private TPNCalculator tpnCal = null;
        private List<string> lstError = null;
        private string EmpCode = "";

        private string mntPaitentCode = "";
        private string mntRecipeID = "";
        private Config config = new Config();
        private List<MonitorCondition> lstMntCondition = null;  //审方内容
        private Dictionary<int, string> prepClass = null;       //制剂类型<制剂ID,类型IDs>
        private Dictionary<string, string> pntValue = null;     //患者字符参数(患者属性)
        private Dictionary<string, double> ordersValue = null;  //患者数值参数(检查值等,医嘱), <参数编码,值>
        private Dictionary<int, RecipePrep> drugValue = null;       //药品信息<制剂ID,制剂信息>
        private Dictionary<string, double> tpnValue = null;

        public bool init(BLPublic.DBOperate _db, string _empCode)
        {
            this.db = _db;
            this.EmpCode = _empCode;

            this.lstError = new List<string>();
            this.lstMntCondition = new List<MonitorCondition>();
            this.prepClass = new Dictionary<int, string>();
            this.tpnValue = new Dictionary<string, double>();
            this.pntValue = new Dictionary<string, string>();
            this.ordersValue = new Dictionary<string, double>();
            this.drugValue = new Dictionary<int, RecipePrep>();
            this.tpnCal = new TPNCalculator();
            this.tpnCal.init(_db);

            return loadConfig(); 
        }

        public string getError(string _spt = "\r\n")
        {
            string rt = "";
            foreach (string err in this.lstError)
                rt += err + _spt;

            return rt;
        } 

        /// <summary>
        /// 设置患者信息(加载检查值)
        /// </summary>
        /// <returns></returns>
        private bool initPatient(string _pcode, int _age, string _sex)
        {
            this.clear();

            if (string.IsNullOrWhiteSpace(_pcode))
            {
                this.lstError.Add("患者编码为空");
                return false;
            }
                
            if (_pcode.Equals(this.mntPaitentCode)) //不重复加载
                return true;

            this.mntPaitentCode = _pcode;
             
            this.pntValue.Add("patient.age", _age.ToString());
            this.pntValue.Add("patient.sex", _sex);

            IDataReader dr = null;

            //患者诊断
            if (this.db.GetRecordSet(string.Format(SQL.SEL_PNTDIG, _pcode), ref dr))
            {
                string digCodes = ",";
                while (dr.Read())
                    digCodes += dr["ICD10Code"].ToString() + "," + dr["SubCode1"].ToString() + "," + 
                                dr["SubCode2"].ToString() + ",";

                dr.Close();
                this.pntValue["patient.diagnose"] = digCodes;
            }
            else
                this.lstError.Add("读取患者诊断失败:" + this.db.Error);

            //体格检查
            if (this.db.GetRecordSet(string.Format(SQL.SEL_PNTBODYCHK, _pcode), ref dr))
            {
                while (dr.Read())
                    this.pntValue.Add(dr["Code"].ToString(), dr["ResultValue"].ToString());

                dr.Close();
            }
            else
                this.lstError.Add("读取体格检查失败:" + this.db.Error);


            //实验室检查
            string useLISCodes = this.tpnCal.getUseLISItem();
            if (!string.IsNullOrWhiteSpace(useLISCodes))
                if (this.db.GetRecordSet(string.Format(SQL.SEL_PNTLISCHK, _pcode, useLISCodes), ref dr))
                {
                    while (dr.Read())
                        this.pntValue.Add(dr["Code"].ToString(), dr["ResultValue"].ToString());

                    dr.Close();
                }
                else
                    this.lstError.Add("读取实验室检查失败:" + this.db.Error);

            return true;
        }

        /// <summary>
        /// 获取制剂所属 糖、氨基酸、脂肪乳 哪一类
        /// </summary>
        /// <returns>0都不是，1糖，2氨基酸，4脂肪乳</returns>
        public static int getPrepSAFType(int _prepID)
        {
            if (",1,23,243,245,246,247,248,249,250,251,252,253,254,502,7491,7492,7548,7990,8020,".Contains("," + _prepID.ToString() + ",")) //葡萄糖
                return 0x02;

            else if ((190 <= _prepID && _prepID <= 217) || (449 <= _prepID && _prepID <= 466) ||
                     (227 == _prepID) || (228 == _prepID))  //氨基酸
                return 0x01;

            else if ((405 <= _prepID && _prepID <= 414) || (417 <= _prepID && _prepID <= 432) ||
                     (475 == _prepID) || (476 == _prepID) || (481 <= _prepID && _prepID <= 490) ||
                     (498 <= _prepID && _prepID <= 500) || (4176 == _prepID) || (4177 == _prepID)) //脂肪乳
                return 0x04;
            else
                return 0;
        }

        /// <summary>
        /// 加载医嘱内容。计算TPN项目值
        /// </summary>
        /// <param name="_rcipeID">医嘱号</param>
        /// <returns></returns>
        private bool initOrders(string _recipeID)
        {
            this.mntRecipeID = _recipeID;

            clearDrug();

            DataTable tblRecipe = new DataTable();
            if (!this.db.GetRecordSet(string.Format(SQL.SEL_ORDERSPREP, _recipeID), ref tblRecipe))
            {
                this.lstError.Add("读取医嘱药品制剂失败:" + this.db.Error);
                return false;
            }

            return initRecipeInfo(tblRecipe);
        }

        /// <summary>
        /// 医嘱相关参数
        /// </summary>
        /// <param name="_tblRecipe"></param>
        /// <returns></returns>
        private bool initRecipeInfo(DataTable _tblRecipe)
        {
            double AACapacity = 0;      //氨基酸容量
            double sugarCapacity = 0;   //葡萄糖容量
            double fatCapacity = 0;     //脂肪乳容量
            string drugCodes = "";
            string newPrepIDs = "";
            int SAFType = 0;

            double totalCapacity = ordersCapacity(_tblRecipe, (prepID, quantity, capacity) =>
                            { 
                                if (0 >= prepID)
                                    return;

                                drugCodes += prepID.ToString() + ",";
                                if (!this.prepClass.ContainsKey(prepID))
                                    newPrepIDs += prepID.ToString() + ",";

                                int t = getPrepSAFType(prepID);
                                if (0x01 == t)  //葡萄糖
                                    sugarCapacity += capacity; 

                                else if (0x02 == t)  //氨基酸
                                    AACapacity += capacity;

                                else if (0x04 == t) //脂肪乳
                                    fatCapacity += capacity;


                                SAFType &= t;
                            });


            if (string.IsNullOrWhiteSpace(drugCodes))
            {
                this.lstError.Add("医嘱没有药品内容.");
                return false;
            }

            if (0 >= totalCapacity)
            {
                this.lstError.Add("医嘱没有溶媒.");
                return false;
            }

            drugCodes = "," + drugCodes;
             
            this.pntValue["recipe.drugcode"] = drugCodes;

            setOrdersValue("recipe.tpnsaftype", SAFType);
            setOrdersValue("recipe.capacity", totalCapacity);
            setOrdersValue("recipe.capacity.AA", AACapacity);
            setOrdersValue("recipe.capacity.sugar", sugarCapacity);
            setOrdersValue("recipe.capacity.fat", fatCapacity);


            //获取为初始化的制剂分类
            if (!string.IsNullOrWhiteSpace(newPrepIDs))
            {
                newPrepIDs = newPrepIDs.Substring(0, newPrepIDs.Length - 1);
                IDataReader idr = null;
                if (this.db.GetRecordSet(string.Format(SQL.SEL_PREP_CLASS, newPrepIDs), ref idr))
                {
                    BLPublic.BLDataReader bldr = new BLPublic.BLDataReader(idr);
                    int prepID = 0;
                    while (bldr.next())
                    {
                        prepID = bldr.getInt("UniPreparationID");
                        if (this.prepClass.ContainsKey(prepID))
                            this.prepClass[prepID] += bldr.getString("MedicineClassID") + ",";
                        else
                            this.prepClass.Add(prepID, bldr.getString("MedicineClassID") + ",");
                    }
                    bldr.close();
                }
                else
                    this.lstError.Add("读取药品分类失败:" + this.db.Error);
            }

            //获取药品分类
            string dClass = ",";
            foreach (int pID in this.prepClass.Keys)
                if (drugCodes.Contains("," + pID.ToString() + ","))
                    dClass += this.prepClass[pID] + ",";

            this.pntValue["recipe.drugclass"] = dClass;

            return true;
        }

        /// <summary>
        /// 计算医嘱溶剂
        /// </summary>
        /// <param name="_recipeID"></param>
        /// <param name="_onDrug">计算药品溶剂事件<制剂ID,溶剂></param>
        /// <returns></returns>
        public double ordersCapacity(DataTable _tblDrugs, Action<int, double, double> _onDrug)
        {
            this.drugValue.Clear();

            double capacity = 0;
            double totalCapacity = 0;   //总容量 
            double dosage = 0;
            double quantity = 0;
            string dosageu = "";
            int prepID = 0;

            BLPublic.BLDataReader bldr = new BLPublic.BLDataReader(_tblDrugs.CreateDataReader());
            while (bldr.next())
            {
                prepID = bldr.getInt("UniPreparationID");
                dosage = Convert.ToDouble(bldr.getString("Dosage"));
                dosageu = bldr.getString("DosageUnit").Trim();
                capacity = 0;

                if (dosageu.Equals(bldr.getString("StdDosageUnit").Trim()))
                    quantity = dosage / bldr.getFloat("StdDosage");
                else if (dosageu.Equals(bldr.getString("CapacityUnit").Trim()))
                    quantity = dosage / bldr.getFloat("Capacity");
                else
                    quantity = bldr.getFloat("Quantity");
                 
                if ("ml".Equals(dosageu, StringComparison.CurrentCultureIgnoreCase) || ("毫升".Equals(dosageu)))
                    capacity = dosage;
                else if ("l".Equals(dosageu, StringComparison.CurrentCultureIgnoreCase) || ("升".Equals(dosageu)))
                    capacity = dosage * 1000;
                else if (bldr.getBool("IsMenstruum") || "ml".Equals(bldr.getString("CapacityUnit"))) //溶媒或注射液
                    capacity = bldr.getFloat("Capacity") * quantity;
                   
                if (this.drugValue.ContainsKey(prepID))
                {
                    this.drugValue[prepID].Dosage += dosage;
                    this.drugValue[prepID].Capacity += capacity;
                    this.drugValue[prepID].Quantity += quantity;
                }
                else
                    this.drugValue.Add(prepID, new RecipePrep(prepID, dosage, capacity, quantity));

                //总容积
                if (bldr.getBool("IsMenstruum"))
                    totalCapacity += capacity;
                else if (this.config.CalAllCapacity && (capacity >= this.config.CalMinCapacity)) //是否计算非溶媒注射液 & 大于最小计算量
                    totalCapacity += capacity;
                else
                    capacity = 0;
                 
                if (null != _onDrug)
                    _onDrug(prepID, quantity, capacity);
            }
            bldr.close();

            return totalCapacity;
        }

        /// <summary>
        /// 获取医嘱容积
        /// </summary>
        /// <param name="_recipeID"></param>
        /// <returns></returns>
        public double ordersCapacity(string _recipeID)
        { 
            DataTable tblRecipe = new DataTable();
            if (!this.db.GetRecordSet(string.Format(SQL.SEL_ORDERSPREP, _recipeID), ref tblRecipe))
            {
                this.lstError.Add("读取医嘱药品制剂失败:" + this.db.Error);
                return 0;
            }
            else
                return ordersCapacity(tblRecipe, null);
        }

        /// <summary>
        /// 医嘱TPN审方(单次审方)
        /// </summary>
        /// <param name="_pcode"></param>
        /// <param name="_page"></param>
        /// <param name="_sex"></param>
        /// <param name="_rcipeID"></param>
        /// <returns></returns>
        public bool Monitor(string _pcode, int _page, string _sex, string _recipeID)
        {
            this.clear();

            if (!initPatient(_pcode, _page, _sex))
                return false;

            if (!initOrders(_recipeID))
                return false;

            if (!loadMonitorCondition())
                return false;

            return Monitor();
        }

        /// <summary>
        /// 医嘱TPN审方。
        /// </summary>
        /// <param name="_recipeID">医嘱号(用于保存审方结果)</param>
        /// <returns></returns>
        public bool Monitor(string _recipeID)
        {
            this.clear();
             
            if (!initOrders(_recipeID))
                return false;

            if (!loadMonitorCondition())
                return false;

            return Monitor();
        }

        /// <summary>
        /// 计算医嘱TPN项目值
        /// </summary>
        /// <param name="_recipeID"></param> 
        /// <returns></returns>
        public bool calculatorOrders(string _recipeID)
        {
            this.clear();

            if (!initOrders(_recipeID))
                return false;

            if (!this.tpnCal.Calculate(this.ordersValue, this.drugValue))
            {
                this.lstError.Add("计算TPN值有误，" + this.tpnCal.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 计算医嘱某个TPN项目值
        /// </summary>
        /// <param name="_recipeID"></param>
        /// <param name="_itemCode"></param>
        /// <returns></returns>
        public bool calculatorOrders(string _recipeID, string _itemCode)
        {
            this.clear();

            if (!initOrders(_recipeID))
                return false;

            if (!this.tpnCal.Calculate(this.ordersValue, this.drugValue, _itemCode))
            {
                this.lstError.Add("计算TPN值有误，" + this.tpnCal.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 计算药品TPN参数
        /// </summary>
        /// <param name="_tblDrug">药品记录集
        /// 字段要求: Dosage, DosageUnit, Quantity, StdDosage, StdDosageUnit, 
        ///           Capacity, CapacityUnit, IsMenstruum, UniPreparationID
        /// </param>
        /// <returns></returns>
        public bool calculatorDrugs(DataTable _tblDrug)
        {
            this.clear();

            if (!initRecipeInfo(_tblDrug))
                return false;

            if (!this.tpnCal.Calculate(this.ordersValue, this.drugValue))
            {
                this.lstError.Add("计算TPN值有误，" + this.tpnCal.Error);
                return false;
            }

            return true; 
        }

        /// <summary>
        /// 医嘱TPN审方(已设置患者信息和医嘱信息)。
        /// 需先调用initPatient设置患者信息，initOrders设置药品信息
        /// </summary>
        private bool Monitor()
        {
            if (string.IsNullOrWhiteSpace(this.mntRecipeID))
            {
                this.lstError.Add("未设置医嘱号");
                return false;
            }

            this.db.ExecSQL(string.Format(SQL.DEL_TPNCHKRESULT, this.mntRecipeID));

            if (0 == this.drugValue.Count)
            {
                this.lstError.Add("没有药品信息");
                return false;
            }

            this.tpnValue.Clear();
            if (this.tpnCal.Calculate(this.ordersValue, this.drugValue))
            {
                this.tpnCal.getItemValue(this.tpnValue);
                this.lstError.Add(this.tpnCal.Error);
            }
            else
                this.lstError.Add("计算TPN值有误，" + this.tpnCal.Error);

            if (0 == this.tpnValue.Count)
            {
                this.lstError.Add("没有计算TPN项目值");
                return false;
            }

            bool rt = setItemMonitor();
            rt &= comTPNMonitor();

            return rt;
        }

        /// <summary>
        /// 设置项目审核
        /// </summary>
        /// <returns></returns>
        private bool setItemMonitor()
        {
            ConditionPaser paser = new ConditionPaser();
            List<int> mntIDs = new List<int>(); //已审，存在不符合条件的审方，
            List<int> notChkIDs = new List<int>(); //已审，存在审方通过的ID（下一个条件不用再审）
            string pntICD10 = this.pntValue["patient.diagnose"];
            string drugClass = this.pntValue["recipe.drugclass"];
            string drugCode = this.pntValue["recipe.drugcode"];
            string tempStr = "";
            int SAFType = (int)this.ordersValue["recipe.tpnsaftype"];
            bool isMonitor = false;
             
            //审方
            foreach (MonitorCondition cdt in this.lstMntCondition)
            {
                if (notChkIDs.Contains(cdt.MonitorID))   //此审方已经有一个条件不符合，不用再审其他条件
                    continue;

                isMonitor = false;
                if ("tpn".Equals(cdt.ItemType))
                {
                    if (this.tpnValue.ContainsKey(cdt.ItemCode))
                    {
                        if (0 >= this.tpnValue[cdt.ItemCode])
                            isMonitor = false;

                        else if (paser.compare(cdt, this.tpnValue[cdt.ItemCode])) //值在指定的范围内
                        {
                            if (cdt.IsIn)
                                isMonitor = true;
                        }
                        else if (!cdt.IsIn)
                            isMonitor = true;
                    } 
                }
                else if ("icd".Equals(cdt.ItemType))
                {
                    if (pntICD10.Contains("," + cdt.ItemCode + ","))   //诊断存在
                        isMonitor = cdt.IsIn;
                    
                    else 
                        isMonitor = !cdt.IsIn;
                }
                /* 药品审方
                 * 1. 要求药品是否存在，Domain为空。（IsIn=true，要求存在,反之要求不能存在）
                 * 2. 药品与药品是否能一起使用, ItemCode为药品1，Domain为药品2. （IsIn=true，必须有两个药品，反正两个药品不能一起使用）
                 * 3. 药品与药品分类是否能一起使用. 同2.
                 */
                else if ("drug".Equals(cdt.ItemType) || "class".Equals(cdt.ItemType))  
                {
                    if ("drug".Equals(cdt.ItemType))
                        isMonitor = drugCode.Contains("," + cdt.ItemCode + ",");
                    else
                        isMonitor = drugClass.Contains("," + cdt.ItemCode + ",");

                    if (isMonitor)
                    {
                        isMonitor = false;

                        if (string.IsNullOrWhiteSpace(cdt.Domain)) //没有第二个条件
                            isMonitor = cdt.IsIn;

                        else if (cdt.Domain.Contains("class.")) //第二个条件为药品分类
                        {
                            tempStr = cdt.Domain.Substring(6);
                            if (string.IsNullOrWhiteSpace(tempStr)) 
                                isMonitor = cdt.IsIn;
                            else if (drugClass.Contains("," + tempStr + ","))
                                isMonitor = cdt.IsIn;
                            else
                                isMonitor = !cdt.IsIn; 
                        }
                        else //第二个条件为药品
                        {
                            tempStr = cdt.Domain.Substring(5); //5 = LEN(drug.)
                            if (string.IsNullOrWhiteSpace(tempStr))
                                isMonitor = cdt.IsIn;
                            else if (drugCode.Contains("," + tempStr + ","))
                                isMonitor = cdt.IsIn;
                            else
                                isMonitor = !cdt.IsIn; 
                        }
                    } 
                }
                //糖、氨基酸、脂肪乳审核
                else if ("tpnsaf".Equals(cdt.ItemType))
                {
                    if (0 == string.Compare("sugar", cdt.ItemCode, true)) 
                        isMonitor = cdt.IsIn && (0x01 == (SAFType & 0x01));

                    else if (0 == string.Compare("AA", cdt.ItemCode, true))
                        isMonitor = cdt.IsIn && (0x02 == (SAFType & 0x02));

                    else if (0 == string.Compare("fat", cdt.ItemCode, true))
                        isMonitor = cdt.IsIn && (0x04 == (SAFType & 0x04));
                }

                if (isMonitor)
                {
                    if (!mntIDs.Contains(cdt.MonitorID))
                        mntIDs.Add(cdt.MonitorID);
                }
                else if (!notChkIDs.Contains(cdt.MonitorID))
                    notChkIDs.Add(cdt.MonitorID);
            }
             
            //删除不需要审核的条件
            foreach (int id in notChkIDs)
                if (mntIDs.Contains(id))
                    mntIDs.Remove(id);
             
            if (0 == mntIDs.Count) //没有审方结果
            {
                this.db.ExecSQL(string.Format(SQL.ADD_TPNCHK_RCD, this.mntRecipeID, this.EmpCode, "0"));
                return true;
            }

            string maxLvl = string.Format(SQL.SEL_MAX_LVL, this.mntRecipeID); 
            foreach (int mntID in mntIDs)
            {
                if (!this.db.ExecSQL(string.Format(SQL.ADD_TPNCHKRESULT, mntID, this.mntRecipeID, this.EmpCode)))
                    this.lstError.Add("增加审方记录(" + mntID.ToString() + ")失败:" + this.db.Error); 
            }

            if (!this.db.ExecSQL(string.Format(SQL.ADD_TPNCHK_RCD, this.mntRecipeID, this.EmpCode, maxLvl)))
                this.lstError.Add("记录医嘱审方失败:" + this.db.Error); 

            return true;
        }

        /// <summary>
        /// 审核通用TPN项目
        /// </summary>
        /// <returns></returns>
        private bool comTPNMonitor()
        {
            List<int> lstPrep = new List<int>(16);
            Dictionary<int, double> lstValue = new Dictionary<int, double>(16);
            Dictionary<int, TPNItemMonitor> lstResult = new Dictionary<int, TPNItemMonitor>(8);

            //所含制剂ID
            foreach (int prepID in this.drugValue.Keys)
                lstPrep.Add(prepID);

            //TPN参数值
            foreach (KeyValuePair<string, double> kv in this.tpnValue)
                lstValue.Add(Convert.ToInt32(kv.Key), kv.Value);

            bool rt = checkComTPN(this.db, lstPrep, lstValue, lstResult);
            if (!rt)
            {
                this.lstError.Add("无法审核TPN通用项目");
                return false;
            }

            string sql = ""; 
            foreach (KeyValuePair<int, TPNItemMonitor> kv in lstResult)
            { 
                if (0 != kv.Value.DeviateValue)
                    sql += string.Format(SQL.ADD_COMCHK_RESULT, this.mntRecipeID, kv.Key, kv.Value.Domain, 
                                         kv.Value.DeviateValue, kv.Value.DeviatePer, this.EmpCode,
                                         kv.Value.MonitorID, kv.Value.SeqNo);
                else
                    sql += string.Format(SQL.ADD_COMCHK_RESULT, this.mntRecipeID, kv.Key, kv.Value.Domain,
                                         0, 0, this.EmpCode, kv.Value.MonitorID, kv.Value.SeqNo);
            }

            if (!string.IsNullOrWhiteSpace(sql)) 
                if (!this.db.ExecSQL(sql))
                {
                    this.lstError.Add("保存TPN通用审核项目失败:" + this.db.Error);
                    return false; 
                } 

            return true;
        }

        /// <summary>
        /// 通用TPN项目审核
        /// </summary>
        /// <param name="lstResult">错误项目列表<项目ID, 错误偏差值(小于审核值时为负数)></param> 
        /// <returns></returns>
        public bool checkComTPN(BLPublic.DBOperate _db, List<int> _lstPrep,
            Dictionary<int, double> _tpnValue, Dictionary<int, TPNItemMonitor> lstResult)
        {
            DataTable tblChk = new DataTable();
            //TPN通用审方项目
            if (!_db.GetRecordSet(SQL.SEL_COMTPN_CHK, ref tblChk))
            {
                //this.lstError.Add("加载PIVAS审核项目失败:" + _db.Error);
                return false;
            }
             
            List<DataRow> lstChk = new List<DataRow>(16);
            DataRow[] rows = null;
            int orderType = 0;

            foreach (int prepID in _lstPrep)
            {
                if (0 >= prepID)
                    continue;

                if (0x07 > orderType)
                    orderType |= getPrepSAFType(prepID); 

                rows = tblChk.Select("UniPreparationID=" + prepID.ToString());
                if ((null != rows) && (0 < rows.Length))
                {
                    foreach (DataRow r in rows)
                        lstChk.Add(r);
                }
            }

            if (0 == lstChk.Count)
            { 
                rows = tblChk.Select("(UniPreparationID IS NULL) OR (UniPreparationID=0)");
                foreach (DataRow r in rows)
                {
                    if (!r.IsNull("RequireSugar") && (bool)r["RequireSugar"] && (0x01 != (orderType & 0x01)))
                        continue;
                    else if (!r.IsNull("RequireAA") && (bool)r["RequireAA"] && (0x02 != (orderType & 0x02)))
                        continue;
                    else if (!r.IsNull("RequireFat") && (bool)r["RequireFat"] && (0x04 != (orderType & 0x04)))
                        continue;

                    lstChk.Add(r);
                }
            }

            int itemID = 0;
            int rt = 0;
            double val = 0.0f;
            ConditionPaser paser = new ConditionPaser();
            DataDomain domain = null;
            TPNItemMonitor mntRT = null;

            foreach (DataRow row in lstChk)
            {
                itemID = Convert.ToInt32(row["TPNItemID"].ToString()); //SeqNo
                if (lstResult.ContainsKey(itemID)) //防止重复评价
                    continue;

                mntRT = new TPNItemMonitor((int)row["AlwayCheckID"], Convert.ToInt32(row["SeqNo"].ToString()),
                                           row["NormalValue"].ToString());
                lstResult.Add(itemID, mntRT);  //只要有审核的都有记录

                if (string.IsNullOrWhiteSpace(row["NormalValue"].ToString()))
                    continue;

                if (_tpnValue.ContainsKey(itemID))
                    val = _tpnValue[itemID];
                else
                    val = 0; 

                //if (0 >= _tpnValue[itemID])
                //    continue;
                 
                domain = new tpnmonitor.DataDomain(row["NormalValue"].ToString());
                paser.paser(domain);

                
                rt = paser.compareDomain(domain, val);
                if (-1 == rt)
                {
                    mntRT.DeviateValue = val - domain.Values[0];
                    if (0 != domain.Values[0])
                    {
                        if (0.0 == mntRT.DeviateValue) //没有等号的判断，如val=1, NormalValue为>1
                        {
                            mntRT.DeviateValue = -0.0001;
                            mntRT.DeviatePer = -0.0001; //保留四位小数
                        }  
                        else
                            mntRT.DeviatePer = mntRT.DeviateValue / domain.Values[0];
                    }
                    else
                        mntRT.DeviatePer = 9.9999;
                }
                else if (1 == rt)
                {
                    if (domain.HadMinValue)
                        mntRT.DeviateValue = val - domain.Values[1];
                    else
                        mntRT.DeviateValue = val - domain.Values[0];

                    if (0 != domain.Values[0])
                    {
                        if (0.0 == mntRT.DeviateValue)
                        {
                            mntRT.DeviateValue = 0.0001;
                            mntRT.DeviatePer = 0.0001; 
                        }
                        else
                            mntRT.DeviatePer = mntRT.DeviateValue / domain.Values[0];
                    }
                    else
                        mntRT.DeviatePer = 9.9999;
                } 
            } 

            return true;
        }

        /// <summary>
        /// 清除患者、药品信息
        /// </summary>
        public void clear()
        {
            this.lstError.Clear();
            this.tpnValue.Clear();
            this.pntValue.Clear();
            this.ordersValue.Clear();

            this.mntPaitentCode = "";
            this.mntRecipeID = "";

            this.pntValue.Add("patient.diagnose", "");

            clearDrug();
        }

        /// <summary>
        /// 清除药品信息
        /// </summary>
        public void clearDrug()
        {
            if (null != this.pntValue)
            {
                this.pntValue["recipe.drugclass"] = ""; 
                this.pntValue["recipe.drugcode"] = "";
            }

            if (null != this.drugValue)
                this.drugValue.Clear(); 
        }

        /// <summary>
        /// 保存医嘱TPN计算值
        /// </summary>
        /// <param name="_recipeID">医嘱号</param>
        public bool saveTPNValue()
        {
            this.lstError.Clear();

            if (string.IsNullOrWhiteSpace(this.mntRecipeID))
            {
                this.lstError.Add("未设置医嘱号");
                return false;
            }

            List<TPNItem> result = new List<TPNItem>();
            this.tpnCal.getCalResult(result);

            string sql = string.Format(SQL.DEL_ORDERSTPNVAL, this.mntRecipeID);
            foreach (TPNItem item in result)
                sql += ";" + string.Format(SQL.ADD_ORDERSTPNVAL, this.mntRecipeID, item.ID, item.Value, this.EmpCode);

            bool rt = this.db.ExecSQL(sql);
            if (!rt)
                this.lstError.Add("保存TPN计算结果失败:" + this.db.Error);

            return rt;
        }

        /// <summary>
        /// 获取TPN项目信息
        /// </summary>
        /// <param name="_values"></param>
        public void getTPNItemInfo(string _itemCode, Dictionary<string, string> itemInfo)
        {
            if (null == itemInfo)
                return;

            itemInfo.Clear();
            this.tpnCal.getItemInfo(_itemCode, itemInfo);
        }

        /// <summary>
        /// 获取医嘱属性值
        /// </summary>
        /// <param name="_itemCode"></param>
        /// <returns></returns>
        public RecipePrep getPrepInfo(int _prepID)
        {
            if (this.drugValue.ContainsKey(_prepID))
                return this.drugValue[_prepID];
            else
                return null;
        }

        /// <summary>
        /// 加载系统设置
        /// </summary>
        /// <returns></returns>
        public bool loadConfig()
        {
            bool rt = loadConfig(this.db, ref this.config);

            if (!rt)
                this.lstError.Add("加载系统设置失败:" + this.db.Error);

            return rt;
        }

        public static bool loadConfig(BLPublic.DBOperate _db, ref Config config)
        {
            IDataReader dr = null;
            if (!_db.GetRecordSet(SQL.SEL_CONFIG, ref dr))
                return false;

            while (dr.Read())
            {
                if ("cal_all_capacity".Equals(dr["SettingItemCode"].ToString()))
                {
                    config.CalAllCapacity = !"0".Equals(dr["SettingValue"].ToString());
                }
                else if ("cal_min_capacity".Equals(dr["SettingItemCode"].ToString()))
                {
                    config.CalMinCapacity = Convert.ToSingle(dr["SettingValue"].ToString());
                }
            }

            dr.Close();
            return true;
        }

        /// <summary>
        /// 加载审方条件
        /// </summary>
        /// <returns></returns>
        private bool loadMonitorCondition()
        {
            this.lstMntCondition.Clear();

            IDataReader dr = null;
            if (this.db.GetRecordSet(SQL.SEL_TPNMNT_CDT, ref dr))
            {
                ConditionPaser paser = new ConditionPaser();
                MonitorCondition cdt = null;

                BLPublic.BLDataReader blDr = new BLPublic.BLDataReader(dr);
                while (blDr.next())
                {
                    cdt = new MonitorCondition(blDr.getInt("TPNMonitorID"), blDr.getString("ItemType"), blDr.getString("Code"),
                                               blDr.getString("Condition"), blDr.getBool("IsIn"));

                    if ("tpn".Equals(cdt.ItemType))
                        paser.paser(cdt);

                    this.lstMntCondition.Add(cdt); 
                }
                blDr.close();
                return true;
            }
            else
                this.lstError.Add("加载TPN审方条件失败:" + this.db.Error);

            return false;
        }

        /// <summary>
        /// 设置患者项目(存在更新/累加，不存在增加)
        /// </summary>
        /// <param name="_item"></param>
        /// <param name="_val"></param>
        /// <param name="_overwirte">审方覆盖,不覆盖则累加</param>
        private void setOrdersValue(string _item, double _val, bool _overwirte=true)
        {
            if (this.ordersValue.ContainsKey(_item))
            {
                if (_overwirte)
                    this.ordersValue[_item] = _val;
                else
                    this.ordersValue[_item] += _val;
            }
            else
                this.ordersValue.Add(_item, _val);
        }
    } 
}
