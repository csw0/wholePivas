using System.Text;
using System.Data;
using PIVAsCommon.Helper;

namespace PivasFreqRule
{
    class seldb
    {
        DB_Help db=new DB_Help();
        StringBuilder str;
        DataSet ds;
        public DataSet getDFreq() 
        {
            ds=new DataSet();
            str = new StringBuilder();
            str.Append("select FreqCode,FreqName,TimesOfDay from DFreq");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }


        public DataSet getOrder()
        {
            ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append(" select OrderID from DOrder");
            str.Append(" where IsValid=1");
            str.Append(" order by orderID");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }
  
        public DataSet getDFreq2(string code)
        {
            ds = new DataSet();
            str = new StringBuilder();
            str.Append(" select FreqCode,TimesOfDay from");
            str.Append(" DFreq");
            str.Append(" where FreqCode='" + code + "'");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }

        public DataSet getFreqRule(string code) 
        {
            ds = new DataSet();
            str = new StringBuilder();
            str.Append("select  ");
            str.Append("  fr.FreqSubCode,fr.FreqSubName,fr.MedicineTime,fr.OrderID,fr.IfCompound,fr.IfFixed,df.FreqCode,df.FreqName,fr.UseTime,fr.IsCrossDay  ");
            str.Append("from FreqRule fr  ");
            str.Append("left join DFreq df on df.FreqCode = fr.FreqCode");
            str.Append(" where fr.FreqCode='" + code + "'  order by FreqSubCode ");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }


        public DataSet getFreqRule(string WardCode, string FreqCode)
        {
            ds = new DataSet();
            str = new StringBuilder();
            str.Append("select  ");
            str.Append("  fr.FreqSubCode,fr.FreqSubName,fr.MedicineTime,fr.OrderID,fr.IfCompound,fr.IfFixed,df.FreqCode,df.FreqName ,fr.UseTime,fr.IsCrossDay ");
            str.Append("from FreqRuleDetail fr  ");
            str.Append("left join DFreq df on df.FreqCode = fr.FreqCode");
            str.Append(" where fr.FreqCode='" + FreqCode + "' and WardCode='"+WardCode+"'  order by FreqSubCode ");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }

        public DataSet getTimeRule()
        {
            ds = new DataSet();
            str = new StringBuilder();
            str.Append("select * from DOrder ");
            str.Append(" order by CONVERT(int,OrderID)  ");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }
        public DataSet getHeadRule(string id) 
        {
            ds = new DataSet();
            str = new StringBuilder();
            str.Append("select * from DOrder");
            str.Append(" where NextOrderID='"+id+"'");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }
        public DataSet getLaterOrder() 
        {
            ds = new DataSet();
            str = new StringBuilder();
            str.Append("select * from DOrder");
            str.Append(" where OrderID in (select max(CONVERT(int,OrderID))");
            str.Append(" from DOrder )");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }

        /// <summary>
        /// 用法表
        /// </summary>
        /// <returns></returns>
        public DataSet getDFreg()
        {
            ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append("select * from DFreq");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }

        /// <summary>
        /// 时间规则表
        /// </summary>
        /// <param name="time">用药时间</param>
        /// <returns>批次</returns>
        public DataSet getOrderID(string time)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select OrderID from DOrder ");
            str.Append(" where BeginTime<='" + time + "' and EndTime>='" + time + "'");
            str.Append(" or (BeginTime<='"+time+"' and NextDay='1' or EndTime>='"+time+"'and NextDay='1' )");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }


        /// <summary>
        /// 用法表
        /// </summary>
        /// <returns></returns>
        public DataSet getDFregID()
        {
            StringBuilder str = new StringBuilder();
            str.Append("select distinct FreqCode from DFreq ");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }

        /// <summary>
        /// 用法表
        /// </summary>
        /// <param name="id">用法名称</param>
        /// <returns></returns>
        public DataSet getDFregid(string id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select * from DFreq where FreqCode='" + id + "'");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }

        /// <summary>
        /// 每个用法的数量
        /// </summary>
        /// <param name="SubCode"></param>
        /// <returns></returns>
        public DataSet getCount(string SubCode)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select count(*) from FreqRule ");
            str.Append(" where FreqCode='" + SubCode + "'");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }

        /// <summary>
        /// 查询开放的病区
        /// </summary>
        /// <returns></returns>
        public DataSet getWard()
        {
            ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append(" select WardCode,WardSimName from DWard");
            str.Append(" where Isopen=1");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }

        //查找设置的特殊字符
        public string GetSpecialChar()
        {
            StringBuilder str = new StringBuilder();
            str.Append("select Id, [StrS],(case[OrderCheck] when 0 then '否' when 1 then CAST(OrderID as varchar(3)) end) as 'BindBatch' ");
            str.Append(" from OrderDrugKandORD ");
            str.Append("order by Id ");
            str.Append(" ");
            str.Append(" ");
            str.Append(" ");
            str.Append(" ");
            return str.ToString();
        }

        //插入特殊字符
        public string InsertSpecialChar(string specialChar,string check,string batch)
        {
            StringBuilder str = new StringBuilder();
            str.Append("if not exists (select 1 from OrderDrugKandORD where StrS='");
            str.Append(specialChar);
            str.Append("' )");
            //str.Append(batch);
            //str.Append("' and OrderCheck='");
            //str.Append(check);
            //str.Append("') ");
            //str.Append(" ");
            str.Append("insert into OrderDrugKandORD([StrS],[OrderCheck],[OrderID]) values('");
            str.Append(specialChar);
            str.Append("','");
            str.Append(check);
            str.Append("','");
            str.Append(batch);
            str.Append("') ");
            return str.ToString();
        }

    }
}
