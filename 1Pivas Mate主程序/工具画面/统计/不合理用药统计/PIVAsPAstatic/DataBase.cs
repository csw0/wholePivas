using System;
using System.Text;
using System.Data;
using PIVAsCommon.Helper;

namespace PIVAsPAstatic
{
    class DataBase
    {
        DB_Help db = new DB_Help();
        /// <summary>
        /// 获取人工处方不合理类型
        /// </summary>
        /// <returns></returns>
        public DataSet getRGDataItem(string date)
        {
            DataSet ds = null;
            string sql = "SELECT DISTINCT 1,CensorItem FROM Prescription p INNER JOIN CPResultRG cpr ON p.PrescriptionID = cpr.PrescriptionID INNER JOIN DEmployee de ON p.DoctorCode = de.AccountID "
               +"where p.InceptDT "+ date;
            try 
            {
                ds = db.GetPIVAsDB(sql);
            }
            catch (Exception e) { }
            return ds;
        }
        /// <summary>
        /// 获取系统不合理类型
        /// </summary>
        /// <returns></returns>
        public DataSet getSYSDataItem(string date)
        {
            DataSet ds = null;
            StringBuilder str = new StringBuilder();
            str.Append("SELECT DISTINCT 1,CensorItem FROM Prescription p ");
            str.Append("left join CPRecord cpr on cpr.PrescriptionID=p.PrescriptionID ");
            str.Append("INNER JOIN CPResult cp ON cp.CheckRecID=cpr.CPRecordID ");
            str.Append("where p.InceptDT " + date);
        
            try
            {
                ds = db.GetPIVAsDB(str.ToString());
            }
            catch (Exception e) { }
            return ds;
        }

        /// <summary>
        /// 按照医生统计
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public DataSet getDataCountByDoctor(DataSet ds1, string type, string date)
        {
            DataSet ds = null;
            StringBuilder str = new StringBuilder();
            if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {

                str.AppendLine("DECLARE @PAStaticTable TABLE ");
                str.AppendLine("(员工编号 varchar(16),员工姓名 varchar(20), 总计 int");

                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    str.AppendLine(",[" + ds1.Tables[0].Rows[i][1].ToString() + "] int");
                }

                str.AppendLine(") INSERT INTO @PAStaticTable ");
                str.AppendLine("SELECT DISTINCT ISNULL(a.AccountID, 0) AS AccountID,ISNULL(DEmployeeName, '') AS aa, 0");

                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    str.AppendLine(", ISNULL([" + ds1.Tables[0].Rows[i][1].ToString() + "],0) AS '" + ds1.Tables[0].Rows[i][1].ToString() + "'");
                }

                str.AppendLine(" FROM ");
                if (type == "0")
                {
                    str.AppendLine("(SELECT de.AccountID, de.DEmployeeName FROM Prescription p INNER JOIN CPResultRG cpr ON p.PrescriptionID = cpr.PrescriptionID INNER JOIN DEmployee de ON p.DoctorCode = de.AccountID WHERE (InceptDT " + date + ")) a ");

                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        str.AppendLine("LEFT JOIN ");
                        str.AppendLine("(SELECT p.DoctorCode, COUNT(distinct p.PrescriptionID) [" + ds1.Tables[0].Rows[i][1].ToString() + "] FROM Prescription p INNER JOIN CPResultRG cpr ON p.PrescriptionID = cpr.PrescriptionID WHERE (InceptDT " + date);
                        str.Append(" AND cpr.CensorItem = '" + ds1.Tables[0].Rows[i][1].ToString() + "') GROUP BY DoctorCode) c" + i.ToString() + " ");
                        str.AppendLine("ON a.AccountID = c" + i.ToString() + ".DoctorCode ");
                    }

                }
                else
                {
                    str.AppendLine("(SELECT de.AccountID, de.DEmployeeName FROM Prescription p  ");
                    str.AppendLine("left join CPRecord cpr on cpr.PrescriptionID=p.PrescriptionID  INNER JOIN CPResult cp ON cp.CheckRecID=cpr.CPRecordID");
                    str.AppendLine(" INNER JOIN DEmployee de ON p.DoctorCode = de.AccountID WHERE (InceptDT " + date + ")) a ");

                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        str.AppendLine("LEFT JOIN ");
                        str.AppendLine("(SELECT p.DoctorCode, COUNT(distinct p.PrescriptionID) [" + ds1.Tables[0].Rows[i][1].ToString() + "] FROM Prescription p ");
                        str.AppendLine("left join CPRecord cpr on cpr.PrescriptionID=p.PrescriptionID  INNER JOIN CPResult cp ON cp.CheckRecID=cpr.CPRecordID ");
                        str.AppendLine(" WHERE (InceptDT " + date + " AND cp.CensorItem = '" + ds1.Tables[0].Rows[i][1].ToString() + "') GROUP BY DoctorCode) c" + i.ToString() + " ");
                        str.AppendLine("ON a.AccountID = c" + i.ToString() + ".DoctorCode ");
                    }

                }
                str.AppendLine("UPDATE @PAStaticTable SET 总计 = ");

                for (int i = 0; i < ds1.Tables[0].Rows.Count - 1; i++)
                {
                    str.AppendLine("[" + ds1.Tables[0].Rows[i][1].ToString() + "] + ");
                }
                str.AppendLine("[" + ds1.Tables[0].Rows[ds1.Tables[0].Rows.Count - 1][1].ToString() + "]");

                str.AppendLine("SELECT * FROM @PAStaticTable ");
            }
            else
            {
                return ds;            
            }
            string sql = str.ToString();
            try 
            {
                ds = db.GetPIVAsDB(sql);
            }
            catch (Exception e) { }
            return ds;
        }
        /// <summary>
        /// 获取不合理类型
        /// </summary>
        /// <param name="accountID"></param>
        /// <param name="type">0是人工审方的不合理类型，1是系统审方的不合理类型</param>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataSet getItemByDoctor(string accountID,string type, string date)
        {
            DataSet ds = null;
            StringBuilder str = new StringBuilder();
            str.Append("SELECT DISTINCT CensorItem FROM Prescription p ");
            if (type == "0")
            {
                str.Append("INNER JOIN CPResultRG cpr ON p.PrescriptionID = cpr.PrescriptionID ");
            }
            else
            {
                str.Append("inner join CPRecord cpr on cpr.PrescriptionID=p.PrescriptionID ");
                str.Append("inner join CPResult cp on cp.CheckRecID=cpr.CPRecordID ");
                str.Append(" ");
            }
            str.Append("INNER JOIN DEmployee de ON p.DoctorCode = de.AccountID where AccountID = '");
            str.Append(accountID);
            str.Append("' AND InceptDT ");
            str.Append(date);
            try
            {
                ds = db.GetPIVAsDB(str.ToString());
            }
            catch (Exception e) { }
            return ds;
        }
        /// <summary>
        /// 显示右边的明细
        /// </summary>
        /// <param name="accountID"></param>
        /// <param name="item"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataSet getItemDetailByDoctor(string accountID, string item, string date,string type)
        {
            DataSet ds = null;
            StringBuilder str = new StringBuilder();
            str.Append("SELECT distinct p.PrescriptionID, dd1.DrugName, dd2.DrugName, cpr.Description, cpr.ReferenName,   ");
            if (type == "0")
            {
                str.Append("d.DEmployeeName FROM Prescription p ");
                str.Append("INNER JOIN CPResultRG cpr ON p.PrescriptionID = cpr.PrescriptionID ");
                str.Append("LEFT JOIN CPRecord cp ON p.PrescriptionID = cp.PrescriptionID  ");
                str.Append("LEFT JOIN DEmployee d ON cp.CheckDCode = d.DEmployeeID  ");
            }
            else
            {
                str.Append("'' FROM Prescription p ");
                str.Append("LEFT JOIN CPRecord cp ON p.PrescriptionID = cp.PrescriptionID  ");
                str.Append("left join CPResult cpr on cpr.CheckRecID=cp.CPRecordID ");
            }
            str.Append("LEFT JOIN DDrug dd1 ON dd1.DrugCode = cpr.DrugACode  ");
            str.Append("LEFT JOIN DDrug dd2 ON dd2.DrugCode = cpr.DrugBCode  ");
            str.Append("WHERE p.DoctorCode = '");
            str.Append(accountID);
            str.Append("' AND cpr.CensorItem = '");
            str.Append(item);
            str.Append("' AND InceptDT ");
            str.Append(date);
            str.Append(" ");
            
            try 
            {
                ds = db.GetPIVAsDB(str.ToString());
            }
            catch (Exception e) { }
            return ds;
        }
        /// <summary>
        /// 按照病区统计
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public DataSet getDataCountWard(DataSet ds1,string type, string date)
        {
            DataSet ds = null;
            StringBuilder str = new StringBuilder();
            if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                str.AppendLine("DECLARE @PAStaticTable TABLE ");
                str.AppendLine("(病区编号 varchar(32),病区名 varchar(64), 总计 int");

                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    str.AppendLine(", [" + ds1.Tables[0].Rows[i][1].ToString() + "] int");
                }
                str.AppendLine(") INSERT INTO @PAStaticTable ");
                str.AppendLine("SELECT DISTINCT ISNULL(a.WardCode, 0) AS AccountID,ISNULL(WardName, '') AS aa, 0");
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    str.AppendLine(", ISNULL([" + ds1.Tables[0].Rows[i][1].ToString() + "],0) AS '" + ds1.Tables[0].Rows[i][1].ToString() + "'");
                }
             
                str.AppendLine(" FROM ");
                if (type == "0")
                {
                    str.AppendLine("(SELECT DISTINCT dw.WardCode, dw.WardName FROM Prescription p ");
                    str.Append("INNER JOIN CPResultRG cpr ON p.PrescriptionID = cpr.PrescriptionID ");
                    str.Append("INNER JOIN DWard dw ON p.WardCode = dw.WardCode WHERE (InceptDT " + date + ")) a ");

                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        str.AppendLine("LEFT JOIN ");
                        str.AppendLine("(SELECT p.WardCode, COUNT(distinct p.PrescriptionID) [" + ds1.Tables[0].Rows[i][1].ToString() + "]");
                        str.Append(" FROM Prescription p INNER JOIN CPResultRG cpr ON p.PrescriptionID = cpr.PrescriptionID ");
                        //str.Append("INNER JOIN DWard dw ON p.WardCode = dw.WardCode ");
                        str.Append(" WHERE (cpr.CensorItem = '" + ds1.Tables[0].Rows[i][1].ToString() + "' and InceptDT " + date + ") GROUP BY p.WardCode) c" + i.ToString() + " ");
                        str.AppendLine("ON a.WardCode = c" + i.ToString() + ".WardCode ");
                    }
                }
                else
                {
                    str.AppendLine("(SELECT DISTINCT dw.WardCode, dw.WardName FROM Prescription p ");
                    str.Append("LEFT JOIN CPRecord cpr ON p.PrescriptionID = cpr.PrescriptionID ");
                    str.Append("left join CPResult cp on cp.CheckRecID=cpr.CPRecordID ");
                    str.Append("INNER JOIN DWard dw ON p.WardCode = dw.WardCode WHERE (InceptDT " + date + ")) a ");

                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        str.AppendLine("LEFT JOIN ");
                        str.AppendLine("(SELECT p.WardCode, COUNT(distinct p.PrescriptionID) [" + ds1.Tables[0].Rows[i][1].ToString() + "]");
                        str.Append(" FROM Prescription p left join CPRecord cpr on cpr.PrescriptionID=p.PrescriptionID ");
                        str.Append("INNER JOIN CPResult cp ON cp.CheckRecID=cpr.CPRecordID ");
                        str.Append(" WHERE (cp.CensorItem = '" + ds1.Tables[0].Rows[i][1].ToString() + "' and InceptDT " + date + ") GROUP BY p.WardCode) c" + i.ToString() + " ");
                        str.AppendLine("ON a.WardCode = c" + i.ToString() + ".WardCode ");
                    }
                }
    
                str.AppendLine("UPDATE @PAStaticTable SET 总计 = ");
                for (int i = 0; i < ds1.Tables[0].Rows.Count - 1; i++)
                {
                    str.AppendLine("[" + ds1.Tables[0].Rows[i][1].ToString() + "] + ");
                }
                str.AppendLine("[" + ds1.Tables[0].Rows[ds1.Tables[0].Rows.Count - 1][1].ToString() + "]");

                str.AppendLine("SELECT * FROM @PAStaticTable ");
            }
            else
            {
                return ds;
            }
            string sql = str.ToString();
            try
            {
                ds = db.GetPIVAsDB(sql);
            }
            catch (Exception e) { }
            return ds;
        }
        /// <summary>
        /// 获取类型
        /// </summary>
        /// <param name="wardCode"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataSet getItemByWard(string wardCode,string type, string date)
        {
            DataSet ds = null;
            StringBuilder str = new StringBuilder();
            str.Append("SELECT DISTINCT CensorItem FROM Prescription p  ");
            if (type == "0")
            {
                str.Append("INNER JOIN CPResultRG cpr ON p.PrescriptionID = cpr.PrescriptionID  ");
            }
            else
            {
                str.Append("inner join CPRecord cpr on cpr.PrescriptionID=p.PrescriptionID ");
                str.Append("inner join CPResult cp on cp.CheckRecID=cpr.CPRecordID ");
            }
            str.Append("INNER JOIN DWard dw ON p.WardCode = dw.WardCode where p.WardCode = '");
            str.Append(wardCode);
            str.Append("' AND InceptDT ");
            str.Append(date);

            try
            {
                ds = db.GetPIVAsDB(str.ToString());
            }
            catch (Exception e) { }
            return ds;
        }
        /// <summary>
        /// 获取明细
        /// </summary>
        /// <param name="WardCode"></param>
        /// <param name="item"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataSet getItemDetailByWard(string WardCode, string item, string date,string type)
        {
            DataSet ds = null;
            StringBuilder str = new StringBuilder();
            str.Append("SELECT distinct p.PrescriptionID, dd1.DrugName, dd2.DrugName, cpr.Description, cpr.ReferenName,   ");
            if (type == "0")
            {
                str.Append("d.DEmployeeName  FROM Prescription p  ");
                str.Append("INNER JOIN CPResultRG cpr ON p.PrescriptionID = cpr.PrescriptionID  ");
                str.Append("inner JOIN CPRecord cp ON p.PrescriptionID = cp.PrescriptionID  ");
                str.Append("left JOIN DEmployee d ON cp.CheckDCode = d.DEmployeeID  ");
                str.Append(" ");

            }
            else
            {
                str.Append("'' FROM Prescription p ");
                str.Append("inner JOIN CPRecord cp ON p.PrescriptionID = cp.PrescriptionID   ");
                str.Append("inner join CPResult cpr on cpr.CheckRecID=cp.CPRecordID  ");
                str.Append("  ");
            }
            //str.Append("INNER JOIN DWard dw ON p.WardCode = dw.WardCode   ");
            str.Append("LEFT JOIN DDrug dd1 ON dd1.DrugCode = cpr.DrugACode  ");
            str.Append("LEFT JOIN DDrug dd2 ON dd2.DrugCode = cpr.DrugBCode   ");
            str.Append("WHERE p.WardCode = '" + WardCode + "'  AND cpr.CensorItem = '" + item + "'and InceptDT ");
            str.Append(date);
            try
            {
                ds = db.GetPIVAsDB(str.ToString());
            }
            catch (Exception e) { }
            return ds;
        }

        public DataSet getChartCount(string date)
        {
            DataSet ds = null;
            StringBuilder str = new StringBuilder();
            str.AppendLine("DECLARE @ChartStaticTable TABLE ");
            str.AppendLine("(Item varchar(20),Total int ) ");
            DataSet ds1 = getRGDataItem(date);
            if (ds1 != null || ds1.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    str.AppendLine("INSERT INTO @ChartStaticTable VALUES (");
                    str.AppendLine("'" + ds1.Tables[0].Rows[i][1].ToString() + "', ");
                    str.AppendLine("(SELECT COUNT(*) FROM Prescription p INNER JOIN CPResultRG cpr ON p.PrescriptionID = cpr.PrescriptionID INNER JOIN DEmployee de ON p.DoctorCode = de.AccountID AND cpr.CensorItem = '" + ds1.Tables[0].Rows[i][1].ToString() + "' WHERE (InceptDT "+date+ ")))");
                }
            }
            str.AppendLine(" SELECT * FROM @ChartStaticTable ");
            string sql = str.ToString();
            try
            {
                ds = db.GetPIVAsDB(sql);
            }
            catch (Exception e) { }
            return ds;
        }
    }
}
