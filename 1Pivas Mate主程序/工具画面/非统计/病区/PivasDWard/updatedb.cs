using PIVAsCommon.Helper;
using System.Text;

namespace DWardManage
{
    class updatedb
    {
        DB_Help dbhelp = new DB_Help();

        /// <summary>
        /// 更行病区排序顺序号
        /// </summary>
        /// <param name="px">排序号</param>
        /// <param name="code">病区/科室CODE</param>
        public void updatpxh( string px,string code) 
        {
            StringBuilder str = new StringBuilder();
            str.Append("update DWard set "); 
            str.Append(" WardSeqNo='" + px + "' ");
            str.Append(" where WardCode='" + code + "'");
            dbhelp.SetPIVAsDB(str.ToString());
        }

        /// <summary>
        /// 更新病区简称
        /// </summary>
        /// <param name="jc">病区简称</param>
        /// <param name="code">病区/科室CODE</param>
        public void updatjc(string jc,string jp, string code)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update DWard set ");
            str.Append("WardSimName='" + jc + "' ");
            str.Append(" ,Spellcode='"+jp+"'");
            str.Append(" where WardCode='" + code + "'");
            dbhelp.SetPIVAsDB(str.ToString());
        }

        /// <summary>
        /// 跟新病区区域
        /// </summary>
        /// <param name="fz">区域名</param>
        /// <param name="code">病区/科室CODE</param>
        public void updatfz(string fz, string code)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update DWard set ");
            str.Append("WardArea='" + fz + "' ");
            str.Append(" where WardCode='" + code + "'");
            dbhelp.SetPIVAsDB(str.ToString());
        }

        /// <summary>
        /// 更新病区开房状态
        /// </summary>
        /// <param name="kf">开放关闭状态</param>
        /// <param name="code">病区/科室CODE</param>
        public void updatkf(bool kf, string code)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update DWard set ");
            str.Append("IsOpen='" + kf + "' ");
            str.Append(" where WardCode='" + code + "'");
            dbhelp.SetPIVAsDB(str.ToString());
        }

        public void updatSC(string sc, string code)
        {
            dbhelp.SetPIVAsDB(string.Format("UPDATE [DWard] SET [Spellcode] = '{0}' WHERE WardCode='{1}'", sc, code));
        }

        public void updateLM(string lm, string wardcode)
        {

            dbhelp.SetPIVAsDB(string.Format("UPDATE [DWard] SET [LimitSTTime] = '{0}' WHERE WardCode='{1}'", lm, wardcode));
        }
    }
}
