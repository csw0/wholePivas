using System.Text;

namespace SetSyn
{
    public class SelectSQL
    {
        public string Get_SynSet(string Syncode)
        {
            StringBuilder str = new StringBuilder("Select * from SynSet where SynCode='{0}'");
            return string.Format(str.ToString(),Syncode);
        }

        /// <summary>
        /// 查询数据库配置
        /// </summary>
        /// <param name="IP">查询项</param>
        /// <param name="tag">0，DataSource   1，InitialCatalog</param>
        /// <returns></returns>
        public string Get_SynSets(string type)
        {
            string ret = string.Empty;
            switch(type)
            {
                case "DataSource":
                    {
                        ret = "Select distinct DataSource from SynSet";
                        break;
                    }
                case "InitialCatalog":
                    {
                        ret = "Select distinct InitialCatalog from SynSet where DataSource='{0}'";
                        break;
                    }
                case "UserID":
                    {
                        ret = "Select distinct UserID from SynSet where DataSource='{0}' and InitialCatalog='{1}'";
                        break;
                    }
                case "Password":
                    {
                        ret = "Select top 1 Password from SynSet where DataSource='{0}' and InitialCatalog='{1}' and UserID='{2}'";
                        break;
                    }
            }
            return ret;
        }
    }
}
