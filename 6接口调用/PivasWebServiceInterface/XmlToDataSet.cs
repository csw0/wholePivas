using System.Data;
using System.IO;
using System.Text;
using System.Xml;

namespace PivasWebServiceInterface
{
    public class XmlToDataSet
    {
        public DataSet getDS(string xml) {

            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xml);
                //从stream装载到XmlTextReader
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
         }
        public string getxml(DataSet ds)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" ?>");
            sb.Append("<DataTable>");

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                sb.Append("<Row>");
                foreach (DataColumn col in ds.Tables[0].Columns)
                {
                    sb.AppendFormat("<{0}>{1}</{0}>", col.ColumnName, row[col.ColumnName]);
                }
                sb.Append("</Row>");
            }
            sb.Append("</DataTable>");


            return sb.ToString();


        }
    }
}