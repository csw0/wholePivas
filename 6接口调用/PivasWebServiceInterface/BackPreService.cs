using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace PivasWebServiceInterface
{
    public class BackPreService
    {
        public int BackPre(string p_group_no, string prm_EXEC_DOCTOR, string prm_jujueyy, out string PRM_DATABUFFER, out string PRM_APPCODE)
        {
            PRM_DATABUFFER = string.Empty;
            PRM_APPCODE = string.Empty;
            OleDbConnection connection = new OleDbConnection(
                "Provider=MSDAORA.1;Password=his3;User ID=his3;Data Source=zsk;   Persist Security Info=True");
            OleDbCommand command = new OleDbCommand();
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "pkg_sy_jizhang.zy_ShenHeZT";
                OleDbParameter[] parameterArray = new OleDbParameter[] { new OleDbParameter("p_group_no", OleDbType.VarChar, 50), new OleDbParameter("prm_EXEC_DOCTOR", OleDbType.VarChar, 500), new OleDbParameter("prm_jujueyy", OleDbType.VarChar, 500), new OleDbParameter("PRM_DATABUFFER", OleDbType.VarChar, 50), new OleDbParameter("PRM_APPCODE", OleDbType.VarChar, 200) };
                for (int i = 0; i < (parameterArray.Length - 2); i++)
                {
                    parameterArray[i].Direction = ParameterDirection.Input;
                }
                parameterArray[3].Direction = ParameterDirection.Output;
                parameterArray[4].Direction = ParameterDirection.Output;
                parameterArray[0].Value = p_group_no;
                parameterArray[1].Value = prm_EXEC_DOCTOR;
                parameterArray[2].Value = prm_jujueyy;
                for (int j = 0; j < parameterArray.Length; j++)
                {
                    command.Parameters.Add(parameterArray[j]);
                }
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                command.Connection = connection;
                command.ExecuteNonQuery();
                PRM_DATABUFFER = parameterArray[3].Value.ToString();
                PRM_APPCODE = parameterArray[4].Value.ToString();
            }
            catch (Exception exception)
            {
                PRM_DATABUFFER = "pivas:" + exception.Message;
                return 0;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Dispose();
                command.Parameters.Clear();
                command.Dispose();
            }
            return 1;
        }
    }
}