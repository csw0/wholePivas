using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SQLite;
using System.Data;

namespace BLPublic
{
    public class SQLite_DBOperator
    {
        private SQLiteDB conn;

        public string DBType { get; private set; }

        public SQLite_DBOperator(string dbFilePath)
        {
            this.conn = new SQLiteDB();
            this.conn.Connect(dbFilePath);
            this.DBType = this.conn.DBType;
        }

        public void Disconnect()
        {
            this.conn.Disconnect();
        }

        public bool IsConnected
        {
            get { return this.conn.IsConnected; }
        }

        //
        // 将OleDbDataReader中的数据转换为List
        // 向更高层的程序屏蔽底层的OleDb类，并且使用后关闭reader释放资源
        public List<Dictionary<string, object>> GetRecord(string select)
        {
            // 首先使用reader从数据库获取数据
            SQLiteDataReader reader = this.conn.GetRecord(select);

            try
            {
                // 定义结果集
                var result = new List<Dictionary<string, object>>();

                while (reader.Read())
                {
                    // reader的当前记录保存在Dictionary<string, object>中
                    var record = new Dictionary<string, object>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string name = reader.GetName(i);
                        object value;

                        if (reader.IsDBNull(i))
                        {
                            // 对于数据库空值的情况，用DBValueNull()专门给一个默认值
                            value = DefaultValue4DBNull.DefaultValue(reader.GetFieldType(i));
                        }
                        else
                        {
                            value = reader[i];
                        }

                        record.Add(name, value);
                    }

                    // 当前记录全部保存完毕，添加到结果集中
                    result.Add(record);
                }

                // 全部记录保存完毕，返回结果集
                return result;
            }
            catch (SQLiteException ex)
            {
                string errorMessage = "从数据库SELECT数据集发生异常\n"
                                    + "发生错误的SQL语句：" + select;
                throw new Exception(errorMessage, ex);
            }
            finally
            {
                // 最后关闭reader
                reader.Close();
                reader.Dispose();
            }
        }

        /// <summary>
        /// 返回SELECT语句的查询结果集合，其中DBNull值表示为null
        /// </summary>
        /// <param name="select">SELECT语句</param>
        /// <returns>查询结果集，包含null值</returns>
        public List<Dictionary<string, object>> GetRecord_WithNull(string select)
        {
            // 首先使用reader从数据库获取数据
            SQLiteDataReader reader = this.conn.GetRecord(select);

            try
            {
                // 定义结果集
                var result = new List<Dictionary<string, object>>();

                while (reader.Read())
                {
                    // reader的当前记录保存在Dictionary<string, object>中
                    var record = new Dictionary<string, object>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string name = reader.GetName(i);
                        object value;

                        if (reader.IsDBNull(i))
                        {
                            // 对于数据库空值用null表示
                            value = null;
                        }
                        else
                        {
                            value = reader[i];
                        }

                        record.Add(name, value);
                    }

                    // 当前记录全部保存完毕，添加到结果集中
                    result.Add(record);
                }

                // 全部记录保存完毕，返回结果集
                return result;
            }
            catch (SQLiteException ex)
            {
                string errorMessage = "从数据库SELECT数据集发生异常\n"
                                    + "发生错误的SQL语句：" + select;
                throw new Exception(errorMessage, ex);
            }
            finally
            {
                // 最后关闭reader
                reader.Close();
                reader.Dispose();
            }
        }

        /// <summary>
        /// 返回SELECT语句的查询结果的第一条记录
        /// ，其中DBNull取一个默认值（见DefaultValue4DBNull类）
        /// </summary>
        /// <param name="select">SELECT语句</param>
        /// <returns>查询结果的第一条记录</returns>
        public Dictionary<string, object> GetFirstRecord(string select)
        {
            var list = this.GetRecord(select);

            Dictionary<string, object> result;
            if (list.Count >= 1)
            {
                // 取第一条记录
                result = list[0];
            }
            else
            {
                // 如果查询结果为空，返回空记录Dictionary
                result = new Dictionary<string, object>();
            }

            return result;
        }

        /// <summary>
        /// 返回SELECT语句的查询结果的第一条记录，其中DBNull值表示为null
        /// </summary>
        /// <param name="select">SELECT语句</param>
        /// <returns>查询结果的第一条记录，包含null值</returns>
        public Dictionary<string, object> GetFirstRecord_WithNull(string select)
        {
            var list = this.GetRecord_WithNull(select);

            Dictionary<string, object> result;
            if (list.Count >= 1)
            {
                // 取第一条记录
                result = list[0];
            }
            else
            {
                // 如果查询结果为空，返回空记录Dictionary
                result = new Dictionary<string, object>();
            }

            return result;
        }

        //
        // 返回内容均为string的List
        public List<Dictionary<string, string>> GetStringRecord(string select)
        {
            var list = this.GetRecord(select);

            // 将List<Dictionary<string, object>>转换为List<Dictionary<string, string>>
            var result = new List<Dictionary<string, string>>();
            foreach (var record in list)
            {
                var stringRecord = new Dictionary<string, string>();
                foreach (var kv in record)
                {
                    string key = kv.Key;
                    string value = kv.Value.ToString();
                    stringRecord.Add(key, value);
                }
                result.Add(stringRecord);
            }

            return result;
        }

        //
        // 只取GetStringRecord的第一条记录，返回Dictionary
        public Dictionary<string, string> GetFirstStringRecord(string select)
        {
            var list = this.GetStringRecord(select);

            Dictionary<string, string> result;
            if (list.Count >= 1)
            {
                result = list[0];
            }
            else
            {
                result = new Dictionary<string, string>();
            }

            return result;
        }

        /// <summary>
        /// 使用SELECT语句查询表中的字段，返回查询字段的列名和.NET数据类型数据类型
        /// </summary>
        /// <param name="select">SELECT语句，用于指定表和查询的字段</param>
        /// <returns>字段名，.NET数据类型的字典集合</returns>
        public Dictionary<string, Type> GetTableSchema(string select)
        {
            SQLiteDataReader reader = this.conn.GetRecord(select);
            
            try
            {
                var result = new Dictionary<string, Type>();

                DataTable schemaTable = reader.GetSchemaTable();
                foreach (DataRow row in schemaTable.Rows)
                {
                    string colName = row["ColumnName"] as string;
                    Type colType = row["DataType"] as Type;
                    result.Add(colName, colType);
                }

                return result;
            }
            catch (SQLiteException ex)
            {
                string errorMessage = "从数据库SELECT数据集发生异常\n"
                                    + "发生错误的SQL语句：" + select;
                throw new Exception(errorMessage, ex);
            }
            finally
            {
                reader.Close();
                reader.Dispose();
            }
        }

        #region 直接委托给conn处理的方法
        
        public int ExecuteNonQuery(string sql)
        {
            return this.conn.ExecuteNonQuery(sql);
        }

        public int ExecuteNonQuery_WithoutTransaction(string sql)
        {
            return this.conn.ExecuteNonQuery_WithoutTransaction(sql);
        }

        // TODO，插入有ID的列
        //public long ExecuteInsertReturnID(string insert)
        //{
        //    return this.conn.ExecuteInsertReturnID(insert);
        //}
        #endregion

        /// <summary>
        /// 执行参数化SQL语句
        /// </summary>
        /// <param name="sql">参数化SQL语句</param>
        /// <param name="parmValuesList">使用的参数列表</param>
        /// <returns>影响的行数</returns>
        public int ExecuteParmSQL(string sql, List<Dictionary<string, object>> parmValuesList)
        {
            return this.conn.ExecuteParmSQL(sql, parmValuesList);
        }



    }
}
