using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace PIVAsCommon.Helper
{
    public class XmlSerializeHelper
    {
        /// <summary>
        /// DataTable转换成Xml
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>Xml</returns>
        public static string SerializeDataTableXml(DataTable dt)
        {
            StringBuilder strXml = new StringBuilder();
            try
            {
                strXml.AppendLine("<XmlTable>");
                foreach (DataRow dr in dt.Rows)
                {
                    strXml.AppendLine("<rows>");
                    foreach (DataColumn dc in dt.Columns)
                    {
                        strXml.AppendLine("<" + dc.ColumnName + ">" + dr[dc] + "</" + dc.ColumnName + ">");
                    }
                    strXml.AppendLine("</rows>");
                }
                strXml.AppendLine("</XmlTable>");
                return strXml.ToString();
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("DataTable转换成Xml出错" + ex.Message);
                return strXml.ToString();
            }
        }

        /// <summary>
        /// DataTable转换成Xml
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="syncode">同步的类别</param>
        /// <returns></returns>
        public static string SerializeDataTableXml(DataTable dt, string syncode)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0 && !string.IsNullOrEmpty(syncode))
                {
                    StringBuilder strXml = new StringBuilder();
                    strXml.AppendLine("<XmlTable>");
                    foreach (DataRow dr in dt.Rows)
                    {
                        strXml.AppendLine("<rows>");
                        foreach (DataColumn dc in dt.Columns)
                        {
                            strXml.AppendLine("<" + dc.ColumnName + ">" + dr[dc].ToString().Replace("<", "(").Replace(">", ")") 
                                + "</" + dc.ColumnName + ">");
                        }
                        strXml.AppendLine("</rows>");
                    }
                    strXml.AppendLine("</XmlTable>");
                    if (!Directory.Exists(Application.StartupPath + "\\HisDBlog\\" + DateTime.Now.ToString("yyyy-MM-dd")))
                    {
                        Directory.CreateDirectory(Application.StartupPath + "\\HisDBlog\\" + DateTime.Now.ToString("yyyy-MM-dd"));
                    }
                    File.AppendAllText(Application.StartupPath + "\\HisDBlog\\" + DateTime.Now.ToString("yyyy-MM-dd") 
                        + "\\" + DateTime.Now.ToString("HH-mm-ss") + "_" + syncode + ".log", strXml.ToString());
                    return strXml.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("拼接xml字符串出错" + ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// XML转换成DataSet
        /// </summary>
        /// <param name="outXml">XMl</param>
        /// <returns></returns>
        public static DataSet XmlToDataTable(string outXml)
        {
            DataSet ds = new DataSet();
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(outXml);
                XmlNodeReader reader = new XmlNodeReader(xml);

                ds.ReadXml(reader);
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("XML转换成DataSet出错：" + ex.Message);
            }
            return ds;
        }
    }
}
