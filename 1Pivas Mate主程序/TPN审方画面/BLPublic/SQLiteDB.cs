using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SQLite;
using System.Data;

namespace BLPublic
{
    public class SQLiteDB
    {
        private SQLiteConnection conn;               // 数据库连接
        private SQLiteCommand cmd;                   // 数据库执行的SQL命令
        private SQLiteTransaction transaction;       // 数据库事务

        public string DBType { get; private set; }

        //
        // 初始化conn，并打开数据库连接
        public void Connect(string dbFilePath)
        {
            try
            {
                this.DBType = "sqlite";

                SQLiteConnectionStringBuilder str = new SQLiteConnectionStringBuilder();
                str.DataSource = dbFilePath;

                this.conn = new SQLiteConnection(str.ConnectionString);
                this.conn.Open();
            }
            catch (SQLiteException ex)
            {
                string errorMessage = string.Format("连接数据库失败！\n数据库文件路径：{0}\n", dbFilePath);
                throw new Exception(errorMessage, ex);
            }
        }

        //
        // 将加密的密码解密为明文
        private string Decrypt(string _cipherText, string _key)
        {
            // 去除密文前后的空白
            // 如果密文为空，返回空白字符串""
            string cipherText = _cipherText.Trim();
            if (cipherText.Length <= 0) { return ""; }

            // 将key转换为数组
            // 如果key为空白字符串，默认使用"Think Space"作为key
            char[] keyArray;
            if (_key.Length <= 0)
            {
                keyArray = "Think Space".ToCharArray();
            }
            else
            {
                keyArray = _key.ToCharArray();
            }

            // 循环的全局变量
            int keyPos = 0;         // 当前用到key的第几位
            int lastHex = 0;        // 上一次的16进制值
            StringBuilder result = new StringBuilder();

            for (int cipherPos = 0; cipherPos < cipherText.Length; cipherPos += 2)
            {
                string curCipher = cipherText.Substring(cipherPos, 2);
                int curCipherHex = Convert.ToInt32(curCipher, 16);

                // 密文的头2个16进制数字是加的盐
                // 跳过以下步骤存入lastHash
                // 从第2位开始计算明文
                if (cipherPos >= 2)
                {
                    // 当前1位的key作为整数
                    // 和当前2位16进制的密文，做异或（XOR）

                    // 首先判断如果key遍历完了，从头开始重复使用
                    if (keyPos >= keyArray.Length)
                    {
                        keyPos = 0;
                    }

                    int curResult = curCipherHex ^ ((int)keyArray[keyPos]);
                    keyPos++;

                    // 减去上一位lastHex
                    if (curResult <= lastHex)
                    {
                        // 为了防止负数，小于上一位的情况下，先 +255 再 -lastHex
                        curResult = curResult + 255 - lastHex;
                    }
                    else
                    {
                        curResult = curResult - lastHex;
                    }

                    result.Append(Convert.ToChar(curResult));
                }

                // 保存当前的hex给下一次作lastHex
                lastHex = curCipherHex;
            }

            return result.ToString();
        }

        public void Disconnect()
        {
            try
            {
                if (this.conn != null)
                {
                    this.conn.Close();
                    this.conn.Dispose();
                }
            }
            catch (SQLiteException ex)
            {
                // TODO 只记录日志，什么都不做
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                this.conn = null;
            }
        }

        public bool IsConnected
        {
            get
            {
                if (this.conn == null) { return false; }

                try
                {
                    return !this.conn.State.Equals(ConnectionState.Closed);
                }
                catch (SQLiteException ex)
                {
                    // TODO 只记录日志，然后返回false
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
        }

        //
        // 初始化事务
        private void BeginTransaction()
        {
            if (!this.IsConnected)
            {
                string errorMessage = "程序尝试操作数据库，但还未连接到数据库";
                throw new Exception(errorMessage);
            }

            try
            {
                this.transaction = this.conn.BeginTransaction();
            }
            catch (SQLiteException ex)
            {
                string errorMessage = "尝试创建数据库事务（SQLiteTransaction）失败";
                throw new Exception(errorMessage, ex);
            }
        }

        //
        // 执行DML语句成功后，提交事务
        // 然后关闭事务并置为null
        private void CommitTransaction()
        {
            try
            {
                this.transaction.Commit();
            }
            finally
            {
                this.transaction.Dispose();
                this.transaction = null;
            }
        }

        //
        // 当执行DML语句发生错误后，回滚事务，再关闭事务
        //
        // 接收异常作为参数，是为了在回滚也失败的情况下
        // 在错误信息中告知用户之前发生了某个异常，导致的回滚，然后回滚也异常了
        // 如果输入参数为null，表示不是执行SQL异常引起的回滚
        private void RollbackTransaction(Exception exWhyRollback)
        {
            try
            {
                this.transaction.Rollback();
            }
            catch (SQLiteException ex)
            {
                string errorMessage = "执行SQL语句失败，并且回滚失败！\n";
                if (this.cmd != null)
                {
                    errorMessage += "发生错误的SQL语句：" + this.cmd.CommandText + "\n";
                }
                if (exWhyRollback != null)
                {
                    errorMessage += "执行SQL语句失败的错误信息（该错误导致回滚）：\n"
                                  + exWhyRollback.ToString();
                }
                throw new Exception(errorMessage, ex);
            }
            finally
            {
                this.transaction.Dispose();
                this.transaction = null;
            }
        }

        //
        // 初始化cmd
        private void BeginCommand()
        {
            if (!this.IsConnected)
            {
                string errorMessage = "程序尝试操作数据库，但还未连接到数据库";
                throw new Exception(errorMessage);
            }

            try
            {
                this.cmd = this.conn.CreateCommand();

                // 如果已经BeginTransaction，并且还未Commit或Rollback
                // 设置cmd为当前事务
                if (this.transaction != null)
                {
                    this.cmd.Transaction = this.transaction;
                }
            }
            catch (SQLiteException ex)
            {
                string errorMessage = "尝试创建数据库命令（SQLiteCommand）失败";
                throw new Exception(errorMessage, ex);
            }
        }

        //
        // 关闭数据库命令cmd
        // 确保命令cmd和事务transaction置为null
        // PS.transaction的关闭放在Commit()和Rollback()中，这里只保证置为null
        private void CloseCommand()
        {
            try
            {
                if (this.cmd != null)
                {
                    this.cmd.Dispose();
                }
            }
            catch (SQLiteException ex)
            {
                // TODO 只记录日志，什么都不做
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                this.cmd = null;
                this.transaction = null;
            }
        }

        public SQLiteDataReader GetRecord(string sql)
        {
            try
            {
                this.BeginCommand();
                this.cmd.CommandText = sql;
                SQLiteDataReader reader = this.cmd.ExecuteReader();
                return reader;
            }
            catch (SQLiteException ex)
            {
                string errorMessage = "执行SQL查询语句失败\n"
                                    + "发生错误的SQL语句：" + sql;
                throw new Exception(errorMessage, ex);
            }
            finally
            {
                this.CloseCommand();
            }
        }

        //
        // 执行DML语句改变（增删改）数据库，返回受影响的行数
        public int ExecuteNonQuery(string sql)
        {
            try
            {
                this.BeginTransaction();

                this.BeginCommand();
                this.cmd.CommandText = sql;
                int rowCount = this.cmd.ExecuteNonQuery();

                this.CommitTransaction();
                return rowCount;
            }
            catch (SQLiteException ex)
            {
                this.RollbackTransaction(ex);
                string errorMessage = "执行SQL语句失败，已经成功回滚\n"
                                    + "发生错误的SQL语句：" + sql;
                throw new Exception(errorMessage, ex);
            }
            finally
            {
                this.CloseCommand();
            }
        }

        /// <summary>
        /// 不使用事务，执行SQL语句
        /// </summary>
        /// <param name="sql">执行的SQL语句</param>
        /// <returns>影响的行数</returns>
        public int ExecuteNonQuery_WithoutTransaction(string sql)
        {
            try
            {
                this.BeginCommand();
                this.cmd.CommandText = sql;
                int rowCount = this.cmd.ExecuteNonQuery();

                return rowCount;
            }
            catch (SQLiteException ex)
            {
                string errorMessage = "执行SQL语句失败\n"
                                    + "发生错误的SQL语句：" + sql;
                throw new Exception(errorMessage, ex);
            }
            finally
            {
                this.CloseCommand();
            }
        }

        /// <summary>
        /// 执行参数化SQL语句
        /// </summary>
        /// <param name="sql">参数化SQL语句</param>
        /// <param name="parmValuesList">使用的参数列表</param>
        /// <returns>影响的行数</returns>
        public int ExecuteParmSQL(string sql, List<Dictionary<string, object>> parmValuesList)
        {
            int rowCount = 0;

            try
            {
                this.BeginTransaction();

                foreach (var parmValues in parmValuesList)
                {
                    this.BeginCommand();
                    this.cmd.CommandText = sql;

                    foreach (var kv in parmValues)
                    {
                        string parmName = kv.Key;
                        object parmValue = kv.Value;

                        this.cmd.Parameters.AddWithValue(parmName, parmValue);
                    }

                    rowCount += this.cmd.ExecuteNonQuery();
                }

                this.CommitTransaction();
                return rowCount;
            }
            catch (SQLiteException ex)
            {
                this.RollbackTransaction(ex);
                string errorMessage = "执行SQL语句失败，已经成功回滚\n"
                                    + "发生错误的SQL语句：" + sql;
                throw new Exception(errorMessage, ex);
            }
            finally
            {
                this.CloseCommand();
            }
        }

        //
        // TODO，插入数据到有自增ID的表，并返回自增ID
        //public long ExecuteInsertReturnID(string insert)
        //{
        //    this.SetupSQLVars();

        //    try
        //    {
        //        // 插入和获得自增长ID位于同一个事物中
        //        this.BeginTransaction();

        //        // 首先执行插入
        //        this.cmd.CommandText = insert;
        //        this.cmd.ExecuteNonQuery();


        //        // 然后获得自增长ID
        //        // 设置获得自增长ID的SQL语句
        //        switch (this.DBType)
        //        {
        //            case "oracle":
        //                {
        //                    // 执行Oracle函数SCOPE_IDENTITY()获得自增长ID
        //                    // 函数所需的当前表名，通过解析INSERT语句获得
        //                    string dbUserName = this.getDBUserNameFromInsert(insert);
        //                    string tableName = this.getTableNameFromInsert(insert);
        //                    this.cmd.CommandText =
        //                        string.Format("SELECT {0}.SCOPE_IDENTITY('{1}') \"ID\" FROM DUAL"
        //                                      , dbUserName, tableName);
        //                    break;
        //                }
        //            case "sqlserver":
        //                {
        //                    // 执行SQL Server系统函数，获取刚才插入的自增长ID
        //                    this.cmd.CommandText = "SELECT SCOPE_IDENTITY() \"ID\"";
        //                    break;
        //                }
        //        }

        //        using (SQLiteDataReader reader = this.cmd.ExecuteReader())
        //        {
        //            if (!reader.Read())
        //            {
        //                // 如果无法获得自增长ID，则回滚并抛出异常
        //                this.RollbackTransaction(null);
        //                string errorMessage = "获得INSERT记录的自增长ID出错，已经成功回滚\n"
        //                                    + "发生错误的SELECT自增长ID语句：" + this.cmd.CommandText + "\n"
        //                                    + "INSERT语句：" + insert;
        //                throw new Exception(errorMessage);
        //            }

        //            long id;
        //            if (reader.IsDBNull(0))
        //            {
        //                // 如果发生向没有自增长列的表插入数据
        //                // 会返回DBNull，这里作为0处理
        //                id = 0L;
        //            }
        //            else
        //            {
        //                // 一切正常，读取long id
        //                id = Convert.ToInt64(reader["ID"]);
        //            }

        //            // 提交事务并返回id
        //            this.CommitTransaction();
        //            return id;
        //        }
        //    }
        //    catch (SQLiteException ex)
        //    {
        //        this.RollbackTransaction(ex);
        //        string errorMessage = "执行SQL插入语句失败，已经成功回滚\n"
        //                            + "发生错误的SQL语句：" + insert;
        //        throw new Exception(errorMessage, ex);
        //    }
        //    catch (IndexOutOfRangeException ex)
        //    {
        //        // 如果从reader读不到ID列
        //        this.RollbackTransaction(ex);
        //        string errorMessage = "获得INSERT记录的自增长ID出错，已经成功回滚\n"
        //                            + "发生错误的SELECT语句：" + this.cmd.CommandText + "\n"
        //                            + "INSERT语句：" + insert;
        //        throw new Exception(errorMessage, ex);
        //    }
        //    finally
        //    {
        //        this.TeardownSQLVars();
        //    }
        //}

        ////
        //// 表名 = 完整表名的点号后半部分
        //private string getDBUserNameFromInsert(string insert)
        //{
        //    string fullTableName = this.getFullTableNameFromInsert(insert);
        //    string tableName = Regex.Replace(fullTableName, "\\..*", "");
        //    return tableName;
        //}

        ////
        //// DB用户名 = 完整表名的点号前半部分
        //private string getTableNameFromInsert(string insert)
        //{
        //    string fullTableName = this.getFullTableNameFromInsert(insert);
        //    string dbUserName = Regex.Replace(fullTableName, ".*\\.", "");
        //    return dbUserName;
        //}

        ////
        //// 从INSERT语句中取得完整表明
        //private string getFullTableNameFromInsert(string insert)
        //{
        //    // 先全部转换为大写
        //    string upperInsert = insert.ToUpper();

        //    // 用正则表达式，去掉表名前后的字符串
        //    string upperFullTableName = upperInsert;
        //    upperFullTableName = upperFullTableName.Replace("INSERT INTO", "");
        //    upperFullTableName = Regex.Replace(upperFullTableName, "\\(.*", "");
        //    upperFullTableName = upperFullTableName.Trim();

        //    // 获得表名在INSERT字符串中的begin、end
        //    int begin = upperInsert.IndexOf(upperFullTableName);

        //    // 根据begin、end获得tableName
        //    string fullTableName = insert.Substring(begin, upperFullTableName.Length);
        //    return fullTableName;
        //}

    }
}
