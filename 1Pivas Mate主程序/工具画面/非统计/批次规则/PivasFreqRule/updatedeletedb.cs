using System.Text;
using PIVAsCommon.Helper;

namespace PivasFreqRule
{
    class updatedeletedb
    {
        DB_Help dbhelp = new DB_Help();
        /// <summary>
        /// 更新间隔时间
        /// </summary>
        /// <param name="jg">间隔时间</param>
        /// <param name="i">用法名称</param>
        public void updatDFreq( string jg,string i)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update DFreq set ");
            str.Append(" IntervalDay='" + jg + "' ");
            str.Append(" where FreqCode='"+i+"' ");
            dbhelp.SetPIVAsDB(str.ToString());
        }
        /// <summary>
        /// 更新每日次数
        /// </summary>
        /// <param name="cs">每日次数</param>
        /// <param name="i">用法名称</param>
        public void updatDFreq2(string cs, string i)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update DFreq set ");
            str.Append(" TimesOfDay='" + cs + "' ");
            str.Append(" where FreqCode='" + i + "' ");
            dbhelp.SetPIVAsDB(str.ToString());
        }
        /// <summary>
        /// 更新使用时间
        /// </summary>
        /// <param name="time">使用时间</param>
        /// <param name="i">用法名称</param>
        public void updateTime(string time, string i) 
        {
            StringBuilder str = new StringBuilder();
            str.Append("update DFreq set ");
            str.Append(" UseTime='" + time + "' ");
            str.Append(" where FreqCode='" + i + "' ");
            dbhelp.SetPIVAsDB(str.ToString());
        }
        /// <summary>
        /// 删除用法
        /// </summary>
        /// <param name="i">用法名称</param>
        public void deleteDFreq(string i)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" delete FreqRuleDetail where  FreqCode='"+i+"'");
            str.Append(" delete FreqRule");
            str.Append(" where FreqCode='" + i + "' ");
            str.Append(" delete DFreq ");
            str.Append(" where FreqCode='" + i + "' ");
            dbhelp.SetPIVAsDB(str.ToString());
        }

       
        /// <summary>
        /// 添加新用法
        /// </summary>
        /// <param name="bm">编码</param>
        /// <param name="mc">名称</param>
        /// <param name="jg">间隔时间</param>
        /// <param name="cs">每日次数</param>
        /// <param name="time">用法时间</param>
        public void insertDFreq(string bm,string mc,string jg,string cs,string time) 
        {
            StringBuilder str = new StringBuilder();
            str.Append("insert into DFreq(FreqCode,FreqName,IntervalDay,TimesOfDay,UseTime)");
            str.Append(" values('"+bm+"','"+mc+"','"+jg+"','"+cs+"','"+time+"')");
            dbhelp.SetPIVAsDB(str.ToString());
        }
        /// <summary>
        /// 插入批次规则
        /// </summary>
        /// <param name="code"></param>
        /// <param name="codeid"></param>
        public void intsertFreqRule(string code, string codeid) 
        {
            StringBuilder str = new StringBuilder();
            str.Append(" insert into FreqRule(FreqCode,FreqSubCode,FreqSubName) ");
            str.Append(" values('" + code + "','" + codeid + "','"+codeid+"')");
            dbhelp.SetPIVAsDB(str.ToString());
        }
        /// <summary>
        /// 更新批次规则
        /// </summary>
        /// <param name="time">每批时间</param>
        /// <param name="order">批次</param>
        /// <param name="code">用法编码</param>
        /// <param name="codeid">用法编码分组</param>
        public void updateFreqRule(string time, string order,string code,string codeid) 
        {
            StringBuilder str = new StringBuilder();
            str.Append("update FreqRule set ");
            str.Append(" MedicineTime='" + time + "' ");
            str.Append(" ,OrderID='" + order + "'");
            str.Append(" ,UseTime='" + time + "'");
            str.Append(" where FreqCode='" + code + "'and FreqSubCode='"+codeid+"' ");
            dbhelp.SetPIVAsDB(str.ToString());
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
            dbhelp.SetPIVAsDB(str.ToString());
        }
    }
}
