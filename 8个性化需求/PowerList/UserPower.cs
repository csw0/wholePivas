using System.Text;
using System.Data;
using PIVAsCommon.Helper;

namespace PowerList
{
    /// <summary>
    /// 反编译新增
    /// </summary>
    public class UserPower
    {
        private DB_Help db = new DB_Help();

        #region 陈松伟反编译新增
        public bool PowerInfo(string userid, string modid, string modsmallid)
        {
            DataSet pIVAsDB = this.db.GetPIVAsDB(this.selectpower(userid, modid, modsmallid));
            if (pIVAsDB != null)
            {
                if (!(modsmallid != ""))
                {
                    return false;
                }
                if (pIVAsDB.Tables[0].Rows.Count > 0)
                {
                    return (pIVAsDB.Tables[0].Rows[0]["Isopen"].ToString() == "True");
                }
                return true;
            }
            return false;
        }

        public string selectpower(string userid, string Modid, string Modinsmallid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select distinct sm.Modid, md.Modinsmallname,pw.UserID,pw.Isopen,pw.CheckName,md.ControlName ");
            builder.Append(" from Synsmallmodule md left join SynModule sm on md.Modid=sm.Modid ");
            builder.Append(" left join SynPower pw on md.Modinsmallid=pw.Modinsmallid where pw.UserID='" + userid + "'  ");
            builder.Append("and sm.Modid='" + Modid + "' ");
            if (Modinsmallid != "")
            {
                builder.Append(" and md.Modinsmallid='" + Modinsmallid + "'");
            }
            return builder.ToString();
        }
        #endregion
    }
}
