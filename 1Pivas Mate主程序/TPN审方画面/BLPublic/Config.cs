using System; 
using System.IO;
using System.Xml;
using System.Text;

namespace BLPublic
{
/**
 * Laennec通用XML配置文件操作
 */
    public class Config
    {
        private string config_node_name = null;
        protected bool create_if_notexists = false;
        protected string file_name = null;
        protected string xml_error = null;
        protected XmlDocument xdoc = null;
        protected XmlElement root = null, config_node = null;

        public Config()
            : this("config", false)
        {
        }

        public Config(string ConfigName, bool CreateNotExists)
        {
            this.config_node_name = ConfigName;
            this.create_if_notexists = CreateNotExists;
            xdoc = new XmlDocument();
        }

        ~Config()
        {
            xdoc = null;
        }

        public string Error
        {
            get { return this.xml_error; }
        }

        public bool CreateIfNotExists 
        {
            get { return this.create_if_notexists; }
            set { this.create_if_notexists = value; }
        }

        public string ConfigNodeName
        {
            get { return this.config_node_name; }
            set { this.config_node_name = value; }
        }

        public bool LoadXml(string xml)
        {
            if ("" == xml)
                xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><lcf></lcf>";

            try {
                xdoc.LoadXml(xml);
            }
            catch(Exception ex)
            {
                this.xml_error = ex.Message;
                return false;
            }
            root = (XmlElement)xdoc.DocumentElement;
            if (null == root)
            {
                this.xml_error = "没有根节点";
                return false;
            }

            this.config_node = (XmlElement)root.SelectSingleNode(this.config_node_name);
            if (null == this.config_node)
                if (this.create_if_notexists)
                {
                    this.config_node = xdoc.CreateElement(this.config_node_name);
                    root.AppendChild(this.config_node);
                    return true;
                }
                else
                {
                    this.xml_error = "没有指定配置节点(" + this.config_node_name + ")";
                    return false;
                }

            return true;
        }

        public bool LoadXmlFile(string xmlFile)
        {
            this.file_name = xmlFile;
            FileInfo fXML = null;
            try
            { 
                fXML = new FileInfo(xmlFile);
            }
            catch(Exception ex)
            {
                this.xml_error = ex.Message;
                return false;
            }

            if (!fXML.Exists)
            {
                if (this.create_if_notexists)
                    return LoadXml("");
                else
                {
                    this.xml_error = "文件(" + xmlFile + ")不存在.";
                    return false;
                }
            }
            StreamReader s = fXML.OpenText();
            string xml = s.ReadToEnd();
            s.Close();
            return LoadXml(@xml);
        }

        public void Save(string file)
        {
            try
            {
                FileInfo fXML = new FileInfo(file);
                XmlTextWriter xtWriter = null;
                if (!fXML.Exists)
                {
                    FileStream fs = fXML.Create();
                    xtWriter = new XmlTextWriter(new StreamWriter(fs));
                }
                else
                    xtWriter = new XmlTextWriter(fXML.CreateText());
                xdoc.Save(xtWriter);
                xtWriter.Close();
                fXML = null;
            }
            catch
            {
                this.xml_error = "无法保存.";
            }
        }

        public void Save()
        {
            this.Save(this.file_name);
        }

        protected string GetNodeText(string NodeName)
        {
            XmlNode xn = this.config_node.SelectSingleNode(NodeName);
            if (null != xn)
            {
                return xn.InnerText;
            }
            else
            {
                return "";
            }
        }

        protected void SetNodeText(string NodeName, string Value)
        {
            XmlNode xn = this.config_node.SelectSingleNode(NodeName);
            if (null == xn)
            {
                XmlElement xe = xdoc.CreateElement(NodeName);
                this.config_node.AppendChild(xe);
                xn = xdoc.CreateNode(XmlNodeType.Text, NodeName, "");
                xe.AppendChild(xn);
            }
            xn.InnerText = Value;
        }
    }


    //保存数据库连接信息
    public class DBInfoModel
    {
        public DBInfoModel(string _type, string _server, string _instance, string _userid, string _password, string _dbname)
        {
            DBType = _type;
            Server = _server;
            Instance = _instance;
            UserID = _userid;
            Password = _password;
            DataBase = _dbname;
        }
        public string DBType { get; set; }

        public string Server { get; set; }

        public string Instance { get; set; }

        public string UserID { get; set; }

        public string Password { get; set; }

        public string DataBase { get; set; }
    }

/**
 * 读取数据库配置文件
 */
    public class DBConfig : Config
    {
        public const string DBT_MSSQL = "sqlserver";
        public const string DBT_ORACLE = "oracle";
        public const string DBT_DB2 = "db2";
        public const string DBT_WEBSVC = "webservice";
        public const string DBT_HTTP = "http";

        public DBConfig()
            : this(false)
        {
        }

        public DBConfig(bool create)
            : this("db_config", create)
        {
        }

        public DBConfig(string config_name, bool create)
            : base(config_name, create)
        {
        }

        public string DBType
        {
            get { return GetNodeText("db_type"); }
            set { SetNodeText("db_type", value); }
        }

        public string Server
        {
			get { return GetNodeText("server_name"); }
			set { SetNodeText("server_name", value); }
        }

        public string Instance
        {
            get { return GetNodeText("instance"); }
            set { SetNodeText("instance", value); }
        }

        public string UserID
        {
			get { return GetNodeText("user_name"); }
			set { SetNodeText("user_name", value); }
        }

        public string Password
        {
            get { return decodePwd(GetNodeText("password")); }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    SetNodeText("password", "");

                else 
                    SetNodeText("password", BLCrypt.Encrypt(value, "PASSWORDKEY")); 
            }
        }

        public string DataBase
        {
            get { return GetNodeText("database"); }
            set { SetNodeText("database", value); }
        }

        public DBInfoModel DBInfo
        {
            get { return new DBInfoModel(DBType, Server, Instance, UserID, Password, DataBase); }
            set {
                    this.DBType = value.DBType;
                    this.Server = value.Server;
                    this.Instance = value.Instance;
                    this.UserID = value.UserID;
                    this.Password = value.Password;
                    this.DataBase = value.DataBase;
                }
        }

        public string ConnectString()
        {
            return ConnectString(this.DBType, this.Server, this.Instance, this.UserID, this.Password, this.DataBase);
        }

        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <param name="_dbInfo"></param>
        /// <returns></returns>
        public static string ConnectString(DBInfoModel _dbInfo)
        {
            return ConnectString(_dbInfo.DBType, _dbInfo.Server, _dbInfo.Instance, _dbInfo.UserID, _dbInfo.Password, _dbInfo.DataBase);
        }

        /// <summary>
        /// 获取数据库连接字符串，不支持非数据库内容
        /// </summary>
        /// <param name="_dbtype">服务器类型</param>
        /// <param name="_svcAddr"></param>
        /// <param name="_instance"></param>
        /// <param name="_usr"></param>
        /// <param name="_pwd"></param>
        /// <param name="_db"></param>
        /// <returns></returns>
        public static string ConnectString(string _dbtype, string _svcAddr, string _instance, 
            string _usr, string _pwd, string _db)
        {
            string connstr = "";

            if (DBT_MSSQL.Equals(_dbtype, StringComparison.OrdinalIgnoreCase))
            {
                //connstr = "Provider=SQLOLEDB.1;Persist Security Info=true;";
                connstr += "Data Source=" + _svcAddr;
                if (!string.IsNullOrWhiteSpace(_instance))
                    connstr += "\\" + _instance;
                connstr += ";User ID=" + _usr;
                connstr += ";Password=" + _pwd;
                connstr += ";Initial Catalog=" + _db;
            }
            else if (DBT_ORACLE.Equals(_dbtype, StringComparison.OrdinalIgnoreCase))
            {
                if (4 == IntPtr.Size) //x86
                {
                    connstr = "Provider=MSDAORA.1;Persist Security Info=True";
                    connstr += ";Data Source=" + _svcAddr;
                }
                else
                {
                    connstr = "Provider=OraOLEDB.Oracle;Persist Security Info=True"; //MSDAORA不支持64位 
                    connstr += ";Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + _svcAddr +
                               ")(PORT = 1521))) (CONNECT_DATA=(SERVICE_NAME=" + _instance + ")))";
                }
                connstr += ";User ID=" + _usr;
                connstr += ";Password=" + _pwd;
            }
            else if (DBT_DB2.Equals(_dbtype, StringComparison.OrdinalIgnoreCase))
            {
                connstr = "Provider=IBMDADB2";
                connstr += ";Data Source=" + _svcAddr;
                connstr += ";User ID=" + _usr;
                connstr += ";Password=" + _pwd;
            }
            else
            {
                connstr = "Type=" + _dbtype;
                connstr += ";Server=" + _svcAddr;
                connstr += ";Instance=" + _instance;
                connstr += ";User=" + _usr;
                connstr += ";Password=" + _pwd;
                connstr += ";DataBase=" + _db;
            }

            return connstr;
        }

        private string decodePwd(string _pwd)
        {
            if (string.IsNullOrWhiteSpace(_pwd))
                return "";
            else
                return BLCrypt.Dencrypt(_pwd, "PASSWORDKEY"); 
        }
    }

    //服务器配置文件
    public class ServerConfig : Config
    {
        public ServerConfig()
            : this(false)
        {
        }

        public ServerConfig(bool create)
            : base("server", create)
        {
        }

        public string Address
        {
            get { return GetNodeText("address"); }
            set { SetNodeText("address", value); }
        }
    }
}
