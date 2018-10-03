using System.Text;
using System.Data;
using PIVAsCommon.Helper;

namespace DWardManage
{
    class seldb
    {
        DB_Help db=new DB_Help();
        DataSet ds=new DataSet();

        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="kf">是否开房</param>
        /// <param name="fz">病区区域</param>
        /// <param name="gz">排序字段</param>
        /// <returns></returns>
        public DataSet getDWard(string  kf,string fz,string gz) 
        {
            StringBuilder str = new StringBuilder();
            
            str.Append("select * from DWard where 1=1");
            if (kf!="")
            { 
                str.Append(" and IsOpen='"+kf+"'"); 
            }
            if (fz != "") 
            {
                str.Append(" and WardArea='" + fz + "'");
            }
            if (gz != "")
            {
                str.Append(" order by " + gz + " desc");
            }
            else
            { str.Append(" order by WardSeqNo "); }
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }

        /// <summary>
        /// 根据病区编码取得数据
        /// </summary>
        /// <param name="id">病区编码</param>
        /// <returns></returns>
        public DataSet getDWardid(string id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select * from DWard where WardCode='"+id+"'");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }

        /// <summary>
        /// 取得所有已用病区排序号
        /// </summary>
        /// <returns></returns>
        public DataSet getWardSeqNo() 
        {
            StringBuilder str = new StringBuilder();
            str.Append("select distinct WardSeqNo ");
            str.Append("    from DWard");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }
        
        /// <summary>
        /// 取得所有已用区域名
        /// </summary>
        /// <returns></returns>
        public DataSet getWardArea()
        {
            StringBuilder str = new StringBuilder();
            str.Append("select distinct WardArea ");
            str.Append("    from DWard  where  WardArea is not null  and WardArea!=''   ");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="vague"></param>
        /// <returns></returns>
        public DataSet getVagueSelect(string vague, string fz) 
        {
            StringBuilder str = new StringBuilder();
            if (fz == "")
            {
                str.Append("select *  from DWard");
                str.Append(" where WardCode like  '%" + vague + "%' ");
                str.Append(" or WardName like  '%" + vague + "%' ");
                str.Append(" or WardSimName like  '%" + vague + "%' ");
                str.Append(" order by WardSeqNo  ");
            }
            else
            {
                str.Append("select *  from DWard");
                str.Append(" where (WardCode like  '%" + vague + "%' ");
                str.Append(" or WardName like  '%" + vague + "%' ");
                str.Append(" or WardSimName like  '%" + vague + "%' )");
                str.Append("and WardArea='" + fz + "'");
                str.Append(" order by WardSeqNo  ");
            }
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }

    }
}
