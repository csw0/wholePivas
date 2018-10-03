using PIVAsCommon.Helper;
using System.Text;

namespace PivasFreqRule
{
    class updatedb
    {
        DB_Help db = new DB_Help();
        StringBuilder str;
        /// <summary>
        /// 修改批次
        /// </summary>
        /// <param name="code">频次编号</param>
        /// <param name="pc">批次编号</param>
        /// <param name="tf">0为默认一般规则。修改FreqRule表数据</param>
        /// <param name="tf">1为病区一般规则。修改FreqRuleDetail表数据</param>
        /// <param name="WardCode">病区编码</param>
        public int updateOrderID(string code, string pc,int tf,string WardCode)
        {
            str = new StringBuilder();
            str.Append(" update ");
            if (tf == 0)
            {
                str.Append(" FreqRule ");
            }
            else
            {
                str.Append(" FreqRuleDetail ");
            }
                str.Append(" set    ");
            str.Append(" OrderID='" + pc + "'");
            str.Append(" where FreqSubCode='" + code + "'");
            if (tf != 0)
            {
                str.Append(" and WardCode='"+WardCode+"'");
            }
            return db.SetPIVAsDB(str.ToString());
        }

        public void updateIfFixed(string code, string ifFix)
        {
            str = new StringBuilder();
            str.Append("update FreqRule set");
            str.Append(" IfFixed='" + ifFix + "'");
            str.Append(" where FreqSubCode='" + code + "'");
            db.SetPIVAsDB(str.ToString());
        }

        /// <summary>
        /// 修改单个病区的单个规则
        /// </summary>
        /// <param name="code">规则Code</param>
        /// <param name="ifFix"></param>
        /// <param name="WardCode">病区Code</param>
        public void updateIfFixed(string code, string ifFix,string WardCode)
        {
            str = new StringBuilder();
            if (WardCode.Length > 0)
            {
                str.Append(" update FreqRuleDetail set");
                str.Append(" IfFixed='" + ifFix + "'");
                str.Append(" where FreqSubCode='" + code + "'");
                str.Append(" and WardCode='" + WardCode + "'");
            }
            else
            {
                str.Append("update FreqRule set");
                str.Append(" IfFixed='" + ifFix + "'");
                str.Append(" where FreqSubCode='" + code + "'");
            }
                db.SetPIVAsDB(str.ToString());
        }
        public void updateIfCompound(string code, string ifCompound,string WardCode)
        {
            str = new StringBuilder();
            if (WardCode.Length == 0)
            {
                str.Append("update FreqRule set");
                str.Append(" IfCompound='" + ifCompound + "'");
                str.Append(" where FreqSubCode='" + code + "'");
            }
            else
            {
                str.Append("update FreqRuleDetail set");
                str.Append(" IfCompound='" + ifCompound + "'");
                str.Append(" where FreqSubCode='" + code + "'");
                str.Append(" and WardCode='" + WardCode + "'");
            }
            db.SetPIVAsDB(str.ToString());
        }
        /// <summary>
        /// 跨天
        /// </summary>
        /// <param name="code">频次</param>
        /// <param name="IsCorssDay"></param>
        /// <param name="WardCode"></param>
        public void updateIsCrossDay(string code, string IsCorssDay, string WardCode)
        {
            str = new StringBuilder();
            if (WardCode.Length == 0)
            {
                str.Append("update FreqRule set");
                str.Append(" IsCrossDay='" + IsCorssDay + "'");
                str.Append(" where FreqSubCode='" + code + "'");
            }
            else
            {
                str.Append("update FreqRuleDetail set");
                str.Append(" IsCrossDay='" + IsCorssDay + "'");
                str.Append(" where FreqSubCode='" + code + "'");
                str.Append(" and WardCode='" + WardCode + "'");
            }
            db.SetPIVAsDB(str.ToString());
        }

        /// <summary>
        /// 瓶签上显示用药时间
        /// </summary>
        /// <param name="code"></param>
        /// <param name="UseTime"></param>
        /// <param name="WardCode"></param>
        public void updateUseTime(string code, string UseTime, string WardCode)
        {
            str = new StringBuilder();
            if (WardCode.Length == 0)
            {
                str.Append("update FreqRule set");
                str.Append(" UseTime='" + UseTime + "'");
                str.Append(" where FreqSubCode='" + code + "'");
            }
            else
            {
                str.Append("update FreqRuleDetail set");
                str.Append(" UseTime='" + UseTime + "'");
                str.Append(" where FreqSubCode='" + code + "'");
                str.Append(" and WardCode='" + WardCode + "'");
            }
            db.SetPIVAsDB(str.ToString());
        }




        public void insert(string code, string FreqSubCode, string FreqSubName,string WardCode)
        {
            str = new StringBuilder();
            if (WardCode.Length > 0)
            {
                str.Append("insert into FreqRuleDetail(FreqCode,FreqSubCode,FreqSubName,WardCode)");
            }
            else
            {
                str.Append("insert into FreqRule(FreqCode,FreqSubCode,FreqSubName)");
            }
            str.Append(" values ('" + code + "','" + FreqSubCode + "','" + FreqSubName + "'");
            if(WardCode.Length>0)
            {
                str.Append(",'"+WardCode+"'");
            }
            str.Append(" )");
            db.SetPIVAsDB(str.ToString());
        }

        /// <summary>
        /// 把FreqRule的数据根据病区复制到FreqRuleDetail中
        /// </summary>
        /// <param name="WardCode">病区Code</param>
        /// <param name="TF">是否只插增量数据  0是否，1是是</param>
        public void insert(string WardCode, int TF)
        {
            str = new StringBuilder();
            str.Append(" insert into FreqRuleDetail(wardCode,freqcode,freqsubCode,freqsubname,medicinetime,orderid,ifcompound,iffixed,UseTime,IsCrossDay)");
            str.Append(" select '" + WardCode + "',freqcode,freqsubCode,freqsubname,medicinetime,orderid,ifcompound,iffixed,UseTime,IsCrossDay");
            str.Append(" from FreqRule Fr ");
            if (TF == 1)
            {
                str.Append(" where fr.FreqSubCode not in (select FreqSubCode from FreqRuleDetail ");
                str.Append(" where WardCode='" + WardCode + "'");
                str.Append(" )");
            }
            db.SetPIVAsDB(str.ToString());
        }

        /// <summary>
        /// 把FreqRuleDetail中某个病区的数据复制到FreqRulel中
        /// </summary>
        /// <param name="WardCode">病区Code</param>
        /// <param name="TF">是否只插增量数据  0是否，1是是</param>
        public void insert(string WardCode)
        {

            str = new StringBuilder();
            str.Append(" Delete from FreqRule where freqsubCode in(select FreqSubCode from FreqRuleDetail where WardCode='" + WardCode + "') ");
            str.Append(" insert into FreqRule(freqcode,freqsubCode,freqsubname,medicinetime,orderid,ifcompound,iffixed,UseTime,IsCrossDay)");
            str.Append(" select freqcode,freqsubCode,freqsubname,medicinetime,orderid,ifcompound,iffixed,UseTime,IsCrossDay");
            str.Append(" from FreqRuleDetail Fr ");
            //str.Append(" where fr.FreqSubCode not in (select FreqSubCode from FreqRuleDetail ");
            str.Append(" where WardCode='" + WardCode + "'");
            db.SetPIVAsDB(str.ToString());
        }

        public void Start_Time(string code, string BeginTime, bool NextDay)
        {
            str = new StringBuilder();
            str.Append("update DOrder set");
            str.Append(" BeginTime='" + BeginTime + "',NextDay='" + NextDay + "'");
            str.Append(" where OrderID='" + code + "'");
            db.SetPIVAsDB(str.ToString());
        }

        public void End_Time(string code, string EndTime, bool NextDay)
        {
            str = new StringBuilder();
            str.Append("update DOrder set");
            str.Append(" EndTime='" + EndTime + "',NextDay='" + NextDay + "'");
            str.Append(" where OrderID='" + code + "'");
            db.SetPIVAsDB(str.ToString());
        }

        public void IsValid(string code, bool IsValid)
        {
            str = new StringBuilder();
            str.Append("update DOrder set");
            str.Append(" IsValid='" + IsValid + "'");
            str.Append(" where OrderID='" + code + "'");
            db.SetPIVAsDB(str.ToString());
        }
        public void insertTimeRule(string id, string start_time, string end_time, bool nextday)
        {
            str = new StringBuilder();
            str.Append("insert into DOrder(OrderID,NextOrderID,BeginTime,Endtime,NextDay)");
            str.Append(" values ('" + id + "',CONVERT(int," + id + ")+1");
            str.Append("   ,'" + start_time + "','" + end_time + "','" + nextday + "')");
            db.SetPIVAsDB(str.ToString());
        }

        public void UpdateTimeRule(string code, string BeginTime, string EndTime, bool NextDay)
        {
            str = new StringBuilder();
            str.Append("update DOrder set");
            str.Append(" EndTime='" + EndTime + "'");
            str.Append(" ,BeginTime='" + BeginTime + "',NextDay='" + NextDay + "'");
            str.Append(" where OrderID='" + code + "'");
            db.SetPIVAsDB(str.ToString());
        }

        public void deleteTimeRule(string id)
        {
            str = new StringBuilder();
            str.Append("delete DOrder");
            str.Append(" where OrderID='" + id + "'");
            db.SetPIVAsDB(str.ToString());
        }
        /// <summary>
        /// 更新批次规则
        /// </summary>
        /// <param name="time">每批时间</param>
        /// <param name="order">批次</param>
        /// <param name="code">用法编码</param>
        /// <param name="codeid">用法编码分组</param>
        public void updateFreqRule(string time, string order, string code, string codeid)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update FreqRule set ");
            str.Append(" MedicineTime='" + time + "' ");
            str.Append(" ,OrderID='" + order + "'");
            str.Append(" where FreqCode='" + code + "'and FreqSubCode='" + codeid + "' ");
            db.SetPIVAsDB(str.ToString());
        }

        /// <summary>
        /// 插入批次规则
        /// </summary>
        /// <param name="code"></param>
        /// <param name="codeid"></param>
        public void intsertFreqRule(string code, string codeid)
        {
            StringBuilder str = new StringBuilder();
            str.Append("insert into FreqRule ");
            str.Append(" values('" + code + "','" + codeid + "','','','','','')");
            db.SetPIVAsDB(str.ToString());
        }
        /// <summary>
        /// 删除批次规则
        /// </summary>
        /// <param name="i"></param>
        public void deleteFreqRule(string i)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete FreqRule");
            str.Append(" where FreqSubCode='" + i + "' ");
            db.SetPIVAsDB(str.ToString());
        }

        /// <summary>
        /// 删除一般规则Detail
        /// </summary>
        public void deleteFreqRuleDetail()
        {
            StringBuilder str = new StringBuilder();
            str.Append("truncate table FreqRuleDetail");
            db.SetPIVAsDB(str.ToString());
        }
    }
}