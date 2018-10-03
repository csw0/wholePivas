using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace BLPublic
{
    public class DBOperate
	{ 
        private bool selfCreateConn = false;
        private IDbConnection conn = null;
        private IDbCommand cmd = null;
        private IDbTransaction trans = null;

        private string dbErr = null;

        public DBOperate(IDbConnection _conn)
        {
            this.conn = _conn;
        }

		public DBOperate(string _connstr)
        {
            this.selfCreateConn = true;

            if (0 > _connstr.ToLower().IndexOf("provider="))
            {
                DBConnectByConfig ConnCfg = new DBConnectByConfig();
                if (!ConnCfg.InitConn(_connstr, "", ref this.conn)) 
                    this.dbErr = ConnCfg.Error; 
            }
            else
            {
                if (null != this.conn)
                    this.conn.Close();

                if (IsOracle(_connstr))
			        this.conn = new OleDbConnection();
                else
                    this.conn = new SqlConnection();

                if (!DBConnectByConfig.ConnectDB(_connstr, ref this.conn)) 
                    this.dbErr = "无法连接数据库"; 
            }
        }

        public DBOperate(string _configfile, string _db_config)
        {
            this.selfCreateConn = true;

            DBConnectByConfig ConnCfg = new DBConnectByConfig();
            if (!ConnCfg.InitConn(_configfile, _db_config, ref this.conn)) 
                this.dbErr = ConnCfg.Error;
            
            ConnCfg = null;
        }

        public DBOperate()
        {
            
        }

        ~DBOperate()
        {
            this.Close();
            this.cmd = null; 
            this.conn = null;
        }

        public IDbConnection Connection
        {
			get { return conn; }
            set { this.conn = value; }
		}

        public IDbTransaction Transaction
        {
            get { return this.trans; } 
        }

        public bool IsConnected
        {
            get { return CheckConn(); }
        }

        public void Close()
        {
            if (null != this.conn)
            {
                if (this.selfCreateConn)
                    try
                    {
                        this.conn.Close();
                        this.conn.Dispose();
                    }
                    catch (Exception ex)
                    {
                        this.dbErr = ex.Message;
                    }
            }
        }

        public bool BeginTrans()
        {
            try
            {
                if (null == this.conn)
                {
                    this.dbErr = "数据库未连接";
                    return false;
                }
                initCommand();
                this.trans = this.conn.BeginTransaction(); 
                this.cmd.Transaction = this.trans;
                return true;
            }
            catch (Exception ex)
            {
                this.dbErr = ex.Message;
                return false;
            }
        }

        public void Commit()
        {
            try
            {
                if (null != this.trans)
                    this.trans.Commit();
                 
                if (null != this.cmd)
                    this.cmd.Transaction = null;
                this.trans.Dispose();
                this.trans = null;
            }
            catch (Exception ex)
            {
                this.dbErr = ex.Message;
            }
        }

        public void RollBack()
        {
            try
            {
                if (null != this.trans)
                    this.trans.Rollback();

                if (null != this.cmd)
                    this.cmd.Transaction = null;

                this.trans.Dispose();
                this.trans = null;
            }
            catch (Exception ex)
            {
                this.dbErr = ex.Message;
            }
        }
    
		public bool ExecSQL(string _sql)
		{
			if (! CheckConn()){
				return false;
			}
            else if (0 == _sql.Length)
            {
                this.dbErr = "没有执行SQL语句.";
                return false;
            }
			else
            {
               if (this.IsOracle())
               {
                   for (int i = _sql.Length - 1; i >= 0;  i--)
                       if (' ' != _sql[i])
                       {
                           if (';' == _sql[i])
                               _sql = "begin " + _sql + " end;";
                           break;
                       }
               }

               initCommand();
               this.cmd.CommandText = _sql;
               try
               { 
                   this.cmd.ExecuteNonQuery();
                   return true;
               }
               catch (Exception ex)
               {
                   this.dbErr = "执行错误," + ex.Message;
                   return false;
               }
            }
		}

        public bool InsertAndGetId(string insert_sql, out int id)
        {
            id = 0;
            if (!CheckConn()) return false;

            IDataReader dr = null;
            try
            {
                initCommand();

                if (this.IsOracle())
                {
                    string tblName = "", usrName = "";
                    this.GetInsertSQLTable(insert_sql, out tblName, out usrName);
                    if (0 == tblName.Length)
                    {
                        this.dbErr = "未指定插入表名.";
                        return false;
                    }

                    this.cmd.CommandText = insert_sql;
                    this.cmd.ExecuteNonQuery();

                    if (1 <= usrName.Length)
                        usrName += '.';

                    this.cmd.CommandText = string.Format("SELECT {0}SCOPE_IDENTITY('{1}') FROM DUAL", usrName, tblName);
                    dr = this.cmd.ExecuteReader(); 
                }
                else
                {
                    this.cmd.CommandText = insert_sql;
                    this.cmd.ExecuteNonQuery();

                    this.cmd.CommandText = "SELECT SCOPE_IDENTITY() AS ID"; //IDENT_CURRENT("+tblName+")
                    dr = this.cmd.ExecuteReader(); 
                }

                if ((null != dr) && dr.Read())
                { 
                    id = Convert.ToInt32(dr.GetValue(0).ToString());
                    dr.Close();
                    return true;
                }
                else
                {
                    this.dbErr = "无法增加记录.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.dbErr = "插入错误," + ex.Message;
                return false;
            }
        }

        public bool GetRecordSet(string sql, ref DataTable dt)
        {
            if (!CheckConn())
                return false;

            try
            {
                if (null == dt)
                    dt = new DataTable();
                IDbDataAdapter da = null;
                if (this.conn is SqlConnection)
                {
                    da = new SqlDataAdapter(sql, (SqlConnection)this.conn);
                    ((SqlDataAdapter)da).Fill(dt);
                }
                else
                {
                    da = new OleDbDataAdapter(sql, (OleDbConnection)this.conn);
                    ((OleDbDataAdapter)da).Fill(dt);
                }
                da = null;
                return true;
            }
            catch (Exception ex)
            {
                this.dbErr = "查询错误," + ex.Message;
                return false;
            } 
        }

        public bool GetRecordSet(string sql, ref IDataReader dr)
        {
            if (!CheckConn())
                return false;

            try
            {
                initCommand();
                this.cmd.CommandText = sql;
                dr = this.cmd.ExecuteReader();
                return true;
            }
            catch (Exception ex)
            {
                this.dbErr = "查询错误," + ex.Message;
                return false;
            }  
        }

        public bool GetRecordSet(string sql, ref IDataReader dr, bool _useTbl)
        {
            if (_useTbl)
            {
                DataTable tbl = new DataTable();
                if (!GetRecordSet(sql, ref tbl))
                    return false;

                dr = tbl.CreateDataReader();

                return true;
            }
            else
                return GetRecordSet(sql, ref dr);
        }
         
        public bool GetRecordSet(string sql, ref DataTableReader dr)
        {
            DataTable dt = new DataTable();
            if (GetRecordSet(sql, ref dt))
            {
                dr = dt.CreateDataReader();
                dt = null;
                return true;
            }
            dt = null;
            return false;
        }

        private IDbCommand createCommand()
        {
            if (null == this.conn)
                return null;

            if (this.conn is SqlConnection)
                return new SqlCommand();
            else
                return new OleDbCommand(); 
        }

        private void initCommand()
        {
            if (null != this.cmd)
                return;
             
            this.cmd = createCommand();
            if (null != this.cmd)
                this.cmd.Connection = this.conn;
        }

        private bool CheckConn()
        {
            if (null == this.conn)
            {
                if (string.IsNullOrWhiteSpace(this.dbErr))
                    this.dbErr = "未创建服务器连接.";
                return false;
            }
            else if (ConnectionState.Closed == this.conn.State)
            {
                try
                { 
                    this.conn.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    this.dbErr = "连接服务器错误," + ex.Message;
                    return false;
                }
            }
            else
                return true;
        }

        
        public string Error
        {
            get { return dbErr; }
        }

        public static string ACS(string _txt)
        {
            if (string.IsNullOrWhiteSpace(_txt))
                return "";
            return _txt.Replace("'", "''");
        }

        public static string ACSLike(string str)
        {
            if (null == str)
                return "";
            else
                return string.Format("%{0}%", ACS(str));
        }

        public static string fmtDT(DateTime _dt)
        {
            return _dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string fmtDate(DateTime _dt)
        {
            return _dt.ToString("yyyy-MM-dd");
        }

        private bool IsOracle()
        {
            return IsOracle(this.conn.ConnectionString);
        }

        private static bool IsOracle(string _connStr)
        {
            _connStr = _connStr.ToUpper();
            int p = _connStr.IndexOf("PROVIDER");
            if (0 > p)
                return false;

            int s = p + 8;

            for (int i = p + 8; i < _connStr.Length; i++)
            {
                if ('=' == _connStr[i])
                    s = i;
                else if (';' == _connStr[i])
                {
                    _connStr = _connStr.Substring(s + 1, i - s - 1);
                    break;
                }
            }

            return ((0 <= _connStr.IndexOf("MSDAORA")) || (0 <= _connStr.IndexOf(".ORACLE")));
        }

        private void GetInsertSQLTable(string _sql, out string tblName, out string usrName)
        {
            tblName = "";
            usrName = "";
            if ((null == _sql) || (0 == _sql.Length))
                return;

            int s = 0, i = 0;
            string SQL = _sql.ToUpper();

            s = SQL.IndexOf("INSERT INTO");
            if (0 > s)
            {
                s = SQL.IndexOf("INSERT");
                if (0 == s)
                    return;
                else
                    s += 6;
            }
            else
                s += 12;

            //去掉表名前空格
            for (i = s; i < SQL.Length; i++)
                if (' ' == SQL[i])
                    s++;
                else
                    break;


            for (i = s; i < SQL.Length; i++)
                if (('(' == SQL[i]) || (' ' == SQL[i]))
                {
                    string tbl = _sql.Substring(s, i - s);

                    s = tbl.IndexOf('.');
                    if (0 <= s)
                    {
                        usrName = tbl.Substring(0, s);
                        tblName = tbl.Substring(s + 1, tbl.Length - s - 1);
                    }
                    else
                        tblName = tbl;

                    break;
                }
        }

    }


/**
 * 通过配置文件，初始化数据库连接
 */
    public class DBConnectByConfig
    {
        private string error = null;

        public string Error
        {
            get { return this.error; }
        }

        public static bool ConnectDB(string _connstr, ref IDbConnection conn)
        {
            if (null == conn)
                return false;

            if (conn is SqlConnection) 
                _connstr = ftmSQLConnectStr(_connstr); 

            try
            {
                conn.Close();
                conn.ConnectionString = _connstr;
                conn.Open();
            }
            catch (Exception ex)
            { 
                return false;
            }
            return true;
        }

        public bool InitConn(string _confg_file, string _config_type, ref IDbConnection conn)
        {
            if (string.IsNullOrWhiteSpace(_config_type))
                _config_type = "db_config";

            DBConfig dbcfg = new DBConfig(_config_type, false);
            if (!dbcfg.LoadXmlFile(_confg_file))
            {
                this.error = dbcfg.Error;
                return false;
            }

            if (string.IsNullOrWhiteSpace(dbcfg.Server))
            {
                this.error = "未设置服务器地址";
                return false;
            }

            if (string.IsNullOrWhiteSpace(dbcfg.UserID))
            {
                this.error = "未设置访问用户";
                return false;
            }
             

            if (null == conn)
            {
                if (dbcfg.DBType.ToLower().Contains("sqlserver"))
                    conn = new SqlConnection();
                else
                    conn = new OleDbConnection();
            }
            else
                conn.Close();
             
            try
            {
                conn.Close();
                conn.ConnectionString = dbcfg.ConnectString();
                conn.Open();
            }
            catch (Exception ex)
            {
                this.error = ex.Message;
                return false;
            }
            return true; 
        }

        public bool InitConn(string _confg_file, ref OleDbConnection conn)
        {
            IDbConnection Iconn = conn;
            return InitConn(_confg_file, "db_config", ref Iconn);
        }

        public bool InitConn(string _confg_file, ref SqlConnection conn)
        {
            IDbConnection Iconn = conn;
            return InitConn(_confg_file, "db_config", ref Iconn);
        }

        //SQL去掉privder
        private static string ftmSQLConnectStr(string _connstr)
        {
            string temp = "";
            int p = _connstr.ToUpper().IndexOf("PROVIDER");
            if (0 <= p)
            {
                int end = -1;

                for (int i = p + 8; i < _connstr.Length; i++)
                    if (';' == _connstr[i])
                    {
                        end = i;
                        break;
                    }

                if (0 <= end)
                {
                    if (0 < p)
                        temp = _connstr.Substring(0, p);

                    temp += _connstr.Substring(end + 1);
                }
                else
                    temp = _connstr.Substring(0, p - 1);

                _connstr = temp;
            }

            return _connstr;
        }
    } 
}