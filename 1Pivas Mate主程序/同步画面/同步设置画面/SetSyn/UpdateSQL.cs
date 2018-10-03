using System.Text;

namespace SetSyn
{
    public  class UpdateSQL
    {
        public string Get_SynSet(string SynCode, string LogDirName, int LogSave, int LogNumDays)
        {
            StringBuilder str=new StringBuilder();
            str.Append("Update SynSet set");
            str.Append(" LogSave={0},");
            str.Append(" LogDirName='{1}',");
            str.Append(" LogDBSave=1,");
            str.Append(" LogNumDays='{2}'");
            str.Append(" Where SynCode='{3}'");
            return string.Format(str.ToString(),LogSave,LogDirName,LogNumDays,SynCode);
        }

        public string Get_SynSet(string SynCode, int LogSave)
        {
            StringBuilder str = new StringBuilder();
            str.Append("Update SynSet set");
            str.Append(" LogSave={0},");
            str.Append(" LogDirName='',");
            str.Append(" LogNumDays=0,");
            str.Append(" LogDBSave={0}");
            str.Append(" Where SynCode='{1}'");
            return string.Format(str.ToString(), LogSave, SynCode);
        }

        public string Get_SynSet(string SynCode, string[] DataSource, string SQL)
        {
            StringBuilder str = new StringBuilder();
            str.Append("Update SynSet set");
            str.Append(string.Format(" SynMode='{0}',", DataSource[0]));
            str.Append(string.Format(" DataSource='{0}',", DataSource[1]));
            str.Append(string.Format(" InitialCatalog='{0}',", DataSource[2]));
            str.Append(string.Format(" UserID='{0}',", DataSource[3]));
            str.Append(string.Format(" Password='{0}',", DataSource[4]));
            str.Append(string.Format(" Sql='{0}'", SQL.Replace("'","''")));
            str.Append(string.Format(" Where SynCode='{0}'", SynCode));
            return str.ToString();
        }
    }
}
