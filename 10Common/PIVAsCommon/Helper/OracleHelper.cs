﻿namespace PIVAsCommon.Helper
{
    public class OracleHelper
    {

    }
    //public class OracleHelper
    //{
    //    //Create a hashtable for the parameter cached
    //    private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

    //    /// <summary>
    //    /// Execute a database query which does not include a select
    //    /// </summary>
    //    /// <param name="connString">Connection string to database</param>
    //    /// <param name="cmdType">Command type either stored procedure or SQL</param>
    //    /// <param name="cmdText">Acutall SQL Command</param>
    //    /// <param name="commandParameters">Parameters to bind to the command</param>
    //    /// <returns></returns>
    //    public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
    //    {
    //        // Create a new Oracle command
    //        OracleCommand cmd = new OracleCommand();

    //        //Create a connection
    //        using (OracleConnection connection = new OracleConnection(connectionString))
    //        {
    //            //Prepare the command
    //            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);

    //            //Execute the command
    //            int val = cmd.ExecuteNonQuery();
    //            connection.Close();
    //            cmd.Parameters.Clear();
    //            return val;
    //        }
    //    }
    //    /// <summary>
    //    /// 执行查询语句，返回DataSet
    //    /// </summary>
    //    /// <param name="SQLString">查询语句</param>
    //    /// <returns>DataSet</returns>
    //    public static DataSet Query(string connectionString, string SQLString, params OracleParameter[] cmdParms)
    //    {
    //        using (OracleConnection connection = new OracleConnection(connectionString))
    //        {
    //            OracleCommand cmd = new OracleCommand();
    //            PrepareCommand(cmd, connection, null, SQLString, cmdParms);
    //            using (OracleDataAdapter da = new OracleDataAdapter(cmd))
    //            {
    //                DataSet ds = new DataSet();
    //                try
    //                {
    //                    da.Fill(ds, "ds");
    //                    cmd.Parameters.Clear();
    //                }
    //                catch (System.Data.SqlClient.SqlException ex)
    //                {
    //                    throw new Exception(ex.Message);
    //                }
    //                return ds;
    //            }
    //        }
    //    }

    //    private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, string cmdText, OracleParameter[] cmdParms)
    //    {
    //        if (conn.State != ConnectionState.Open)
    //            conn.Open();
    //        cmd.Connection = conn;
    //        cmd.CommandText = cmdText;
    //        if (trans != null)
    //            cmd.Transaction = trans;
    //        cmd.CommandType = CommandType.Text;//cmdType;
    //        if (cmdParms != null)
    //        {
    //            foreach (OracleParameter parameter in cmdParms)
    //            {
    //                if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
    //                    (parameter.Value == null))
    //                {
    //                    parameter.Value = DBNull.Value;
    //                }
    //                cmd.Parameters.Add(parameter);
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 执行一条计算查询结果语句，返回查询结果（object）。
    //    /// </summary>
    //    /// <param name="SQLString">计算查询结果语句</param>
    //    /// <returns>查询结果（object）</returns>
    //    public static object GetSingle(string connectionString, string SQLString)
    //    {
    //        using (OracleConnection connection = new OracleConnection(connectionString))
    //        {
    //            using (OracleCommand cmd = new OracleCommand(SQLString, connection))
    //            {
    //                try
    //                {
    //                    connection.Open();
    //                    object obj = cmd.ExecuteScalar();
    //                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
    //                    {
    //                        return null;
    //                    }
    //                    else
    //                    {
    //                        return obj;
    //                    }
    //                }
    //                catch (OracleException ex)
    //                {
    //                    throw new Exception(ex.Message);
    //                }
    //                finally
    //                {
    //                    if (connection.State != ConnectionState.Closed)
    //                    {
    //                        connection.Close();
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    public static bool Exists(string connectionString, string strOracle)
    //    {
    //        object obj = OracleHelper.GetSingle(connectionString, strOracle);
    //        int cmdresult;
    //        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
    //        {
    //            cmdresult = 0;
    //        }
    //        else
    //        {
    //            cmdresult = int.Parse(obj.ToString());
    //        }
    //        if (cmdresult == 0)
    //        {
    //            return false;
    //        }
    //        else
    //        {
    //            return true;
    //        }
    //    }

    //    /// <summary>
    //    /// Execute an OracleCommand (that returns no resultset) against an existing database transaction 
    //    /// using the provided parameters.
    //    /// </summary>
    //    /// <remarks>
    //    /// e.g.:  
    //    ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
    //    /// </remarks>
    //    /// <param name="trans">an existing database transaction</param>
    //    /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    //    /// <param name="commandText">the stored procedure name or PL/SQL command</param>
    //    /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
    //    /// <returns>an int representing the number of rows affected by the command</returns>
    //    public static int ExecuteNonQuery(OracleTransaction trans, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
    //    {
    //        OracleCommand cmd = new OracleCommand();
    //        PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
    //        int val = cmd.ExecuteNonQuery();
    //        cmd.Parameters.Clear();
    //        return val;
    //    }

    //    /// <summary>
    //    /// Execute an OracleCommand (that returns no resultset) against an existing database connection 
    //    /// using the provided parameters.
    //    /// </summary>
    //    /// <remarks>
    //    /// e.g.:  
    //    ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
    //    /// </remarks>
    //    /// <param name="conn">an existing database connection</param>
    //    /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    //    /// <param name="commandText">the stored procedure name or PL/SQL command</param>
    //    /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
    //    /// <returns>an int representing the number of rows affected by the command</returns>
    //    public static int ExecuteNonQuery(OracleConnection connection, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
    //    {
    //        OracleCommand cmd = new OracleCommand();

    //        PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
    //        int val = cmd.ExecuteNonQuery();
    //        cmd.Parameters.Clear();
    //        return val;
    //    }
    //    /// <summary>
    //    /// Execute an OracleCommand (that returns no resultset) against an existing database connection 
    //    /// using the provided parameters.
    //    /// </summary>
    //    /// <remarks>
    //    /// e.g.:  
    //    ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
    //    /// </remarks>
    //    /// <param name="conn">an existing database connection</param>
    //    /// <param name="commandText">the stored procedure name or PL/SQL command</param>
    //    /// <returns>an int representing the number of rows affected by the command</returns>
    //    public static int ExecuteNonQuery(string connectionString, string cmdText)
    //    {
    //        OracleCommand cmd = new OracleCommand();
    //        OracleConnection connection = new OracleConnection(connectionString);
    //        PrepareCommand(cmd, connection, null, CommandType.Text, cmdText, null);
    //        int val = cmd.ExecuteNonQuery();
    //        cmd.Parameters.Clear();
    //        return val;
    //    }

    //    /// <summary>
    //    /// Execute a select query that will return a result set
    //    /// </summary>
    //    /// <param name="connString">Connection string</param>
    //    /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    //    /// <param name="commandText">the stored procedure name or PL/SQL command</param>
    //    /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
    //    /// <returns></returns>
    //    public static OracleDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
    //    {
    //        OracleCommand cmd = new OracleCommand();
    //        OracleConnection conn = new OracleConnection(connectionString);
    //        try
    //        {
    //            //Prepare the command to execute
    //            PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
    //            OracleDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
    //            cmd.Parameters.Clear();
    //            return rdr;
    //        }
    //        catch
    //        {
    //            conn.Close();
    //            throw;
    //        }
    //    }

    //    /// <summary>
    //    /// Execute an OracleCommand that returns the first column of the first record against the database specified in the connection string 
    //    /// using the provided parameters.
    //    /// </summary>
    //    /// <remarks>
    //    /// e.g.:  
    //    ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
    //    /// </remarks>
    //    /// <param name="connectionString">a valid connection string for a SqlConnection</param>
    //    /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    //    /// <param name="commandText">the stored procedure name or PL/SQL command</param>
    //    /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
    //    /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
    //    public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
    //    {
    //        OracleCommand cmd = new OracleCommand();

    //        using (OracleConnection conn = new OracleConnection(connectionString))
    //        {
    //            PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
    //            object val = cmd.ExecuteScalar();
    //            cmd.Parameters.Clear();
    //            return val;
    //        }
    //    }

    //    ///    <summary>
    //    ///    Execute    a OracleCommand (that returns a 1x1 resultset)    against    the    specified SqlTransaction
    //    ///    using the provided parameters.
    //    ///    </summary>
    //    ///    <param name="transaction">A    valid SqlTransaction</param>
    //    ///    <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
    //    ///    <param name="commandText">The stored procedure name    or PL/SQL command</param>
    //    ///    <param name="commandParameters">An array of    OracleParamters used to execute the command</param>
    //    ///    <returns>An    object containing the value    in the 1x1 resultset generated by the command</returns>
    //    public static object ExecuteScalar(OracleTransaction transaction, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
    //    {
    //        if (transaction == null)
    //            throw new ArgumentNullException("transaction");
    //        if (transaction != null && transaction.Connection == null)
    //            throw new ArgumentException("The transaction was rollbacked    or commited, please    provide    an open    transaction.", "transaction");

    //        // Create a    command    and    prepare    it for execution
    //        OracleCommand cmd = new OracleCommand();

    //        PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);

    //        // Execute the command & return    the    results
    //        object retval = cmd.ExecuteScalar();

    //        // Detach the SqlParameters    from the command object, so    they can be    used again
    //        cmd.Parameters.Clear();
    //        return retval;
    //    }

    //    /// <summary>
    //    /// Execute an OracleCommand that returns the first column of the first record against an existing database connection 
    //    /// using the provided parameters.
    //    /// </summary>
    //    /// <remarks>
    //    /// e.g.:  
    //    ///  Object obj = ExecuteScalar(conn, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
    //    /// </remarks>
    //    /// <param name="conn">an existing database connection</param>
    //    /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    //    /// <param name="commandText">the stored procedure name or PL/SQL command</param>
    //    /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
    //    /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
    //    public static object ExecuteScalar(OracleConnection connectionString, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
    //    {
    //        OracleCommand cmd = new OracleCommand();

    //        PrepareCommand(cmd, connectionString, null, cmdType, cmdText, commandParameters);
    //        object val = cmd.ExecuteScalar();
    //        cmd.Parameters.Clear();
    //        return val;
    //    }

    //    /// <summary>
    //    /// Add a set of parameters to the cached
    //    /// </summary>
    //    /// <param name="cacheKey">Key value to look up the parameters</param>
    //    /// <param name="commandParameters">Actual parameters to cached</param>
    //    public static void CacheParameters(string cacheKey, params OracleParameter[] commandParameters)
    //    {
    //        parmCache[cacheKey] = commandParameters;
    //    }

    //    /// <summary>
    //    /// Fetch parameters from the cache
    //    /// </summary>
    //    /// <param name="cacheKey">Key to look up the parameters</param>
    //    /// <returns></returns>
    //    public static OracleParameter[] GetCachedParameters(string cacheKey)
    //    {
    //        OracleParameter[] cachedParms = (OracleParameter[])parmCache[cacheKey];

    //        if (cachedParms == null)
    //            return null;

    //        // If the parameters are in the cache
    //        OracleParameter[] clonedParms = new OracleParameter[cachedParms.Length];

    //        // return a copy of the parameters
    //        for (int i = 0, j = cachedParms.Length; i < j; i++)
    //            clonedParms[i] = (OracleParameter)((ICloneable)cachedParms[i]).Clone();

    //        return clonedParms;
    //    }
    //    /// <summary>
    //    /// Internal function to prepare a command for execution by the database
    //    /// </summary>
    //    /// <param name="cmd">Existing command object</param>
    //    /// <param name="conn">Database connection object</param>
    //    /// <param name="trans">Optional transaction object</param>
    //    /// <param name="cmdType">Command type, e.g. stored procedure</param>
    //    /// <param name="cmdText">Command test</param>
    //    /// <param name="commandParameters">Parameters for the command</param>
    //    private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, CommandType cmdType, string cmdText, OracleParameter[] commandParameters)
    //    {
    //        //Open the connection if required
    //        if (conn.State != ConnectionState.Open)
    //            conn.Open();

    //        //Set up the command
    //        cmd.Connection = conn;
    //        cmd.CommandText = cmdText;
    //        cmd.CommandType = cmdType;

    //        //Bind it to the transaction if it exists
    //        if (trans != null)
    //            cmd.Transaction = trans;

    //        // Bind the parameters passed in
    //        if (commandParameters != null)
    //        {
    //            foreach (OracleParameter parm in commandParameters)
    //                cmd.Parameters.Add(parm);
    //        }
    //    }

    //    /// <summary>
    //    /// Converter to use boolean data type with Oracle
    //    /// </summary>
    //    /// <param name="value">Value to convert</param>
    //    /// <returns></returns>
    //    public static string OraBit(bool value)
    //    {
    //        if (value)
    //            return "Y";
    //        else
    //            return "N";
    //    }

    //    /// <summary>
    //    /// Converter to use boolean data type with Oracle
    //    /// </summary>
    //    /// <param name="value">Value to convert</param>
    //    /// <returns></returns>
    //    public static bool OraBool(string value)
    //    {
    //        if (value.Equals("Y"))
    //            return true;
    //        else
    //            return false;
    //    }
    //}
}