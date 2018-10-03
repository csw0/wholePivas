using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PIVAsDBhelp;
using System.Xml;
using System.IO;
using TransTuiYao.ServiceReference1;

namespace TransTuiYao
{
    class CangWai
    {
        ServiceReference1.HisServiceClient wls = new HisServiceClient();
        DB_Help DB = new DB_Help();

        public void twowToFiveTwo()
        {
            try
            {
                CWTrue();
                CWFalse();
            }
            catch (Exception ex)
            {
                //暂未处理，输出异常到log
            }
        }

        private void CWTrue()
        {
            string str1 = " select distinct top 1 labelno,TeamNumber,groupno,WardCode,WardName from IVRecord(nolock) where ivstatus > 10 and labelover >= 0 and DATEDIFF(dd,GETDATE(),InfusionDT) >= -1 and LabelNo not in (select LabelNo from IVRecordToCC) ";
            //string str1 = " select distinct top 100 labelno,groupno from IVRecord where labelover < 0 and (labelno like '20171010%' or labelno like '20171011%') and LabelNo not in (select LabelNo from IVRecordToCN) ";
            DataSet ds1 = DB.GetPIVAsDB(str1);
            if (ds1.Tables.Count > 0 && !(ds1 == null) && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    string wlhc1 = "<ROOT>";
                    wlhc1 += "<OPSYSTEM>HIS</OPSYSTEM>";
                    wlhc1 += "<OPWINID></OPWINID>";
                    wlhc1 += "<OPTYPE>2252</OPTYPE>";
                    wlhc1 += "<OPIP>192.168.0.101</OPIP>";
                    wlhc1 += "<OPMANNO>112</OPMANNO>";
                    wlhc1 += "<OPMANNAME>测试员</OPMANNAME>";
                    wlhc1 += "<CONSIS_ORDER_MSTVW>";
                    wlhc1 += "<ORDER_NO>" + r["groupno"].ToString().Trim() + "</ORDER_NO>";
                    wlhc1 += "<ORDERSUB_NO>" + r["groupno"].ToString().Trim() + "</ORDERSUB_NO>";
                    wlhc1 += "<ADVICE_BATCH>" + r["TeamNumber"].ToString().Trim() + "</ADVICE_BATCH>";
                    wlhc1 += "<WARD_CODE>" + r["WardCode"].ToString().Trim() + "</WARD_CODE>";
                    wlhc1 += "<WARD_NAME>" + r["WardName"].ToString().Trim() + "</WARD_NAME>";
                    wlhc1 += "<ADVICE_CODE>" + r["labelno"].ToString().Trim() + "</ADVICE_CODE>";
                    wlhc1 += "<ALLOWIND>Y</ALLOWIND>";
                    wlhc1 += "</CONSIS_ORDER_MSTVW>";
                    wlhc1 += "</ROOT>";

                    string s1 = wls.HisTransData(wlhc1);
                    DataSet dsxml1 = new DataSet();
                    StringReader read1 = new StringReader(s1);
                    XmlTextReader readxml1 = new XmlTextReader(read1);
                    dsxml1.ReadXml(readxml1);
                    DataTable dt2 = dsxml1.Tables[0];
                    int i = int.Parse(dt2.Rows[0]["RETCODE"].ToString());
                    if (i == 1)
                    {
                        updateDB_CC(r["labelno"].ToString().Trim());
                    }
                }
            }
        }

        private void CWFalse()
        {
            string str1 = " select distinct top 1 labelno,TeamNumber,groupno,WardCode,WardName from IVRecord(nolock) where labelover < 0 and DATEDIFF(dd,GETDATE(),InfusionDT) >= -1 and LabelNo not in (select LabelNo from IVRecordToCC) ";
            //string str1 = " select distinct top 100 labelno,groupno from IVRecord where labelover < 0 and (labelno like '20171010%' or labelno like '20171011%') and LabelNo not in (select LabelNo from IVRecordToCN) ";
            DataSet ds1 = DB.GetPIVAsDB(str1);
            if (ds1.Tables.Count > 0 && !(ds1 == null) && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    string wlhc1 = "<ROOT>";
                    wlhc1 += "<OPSYSTEM>HIS</OPSYSTEM>";
                    wlhc1 += "<OPWINID></OPWINID>";
                    wlhc1 += "<OPTYPE>2252</OPTYPE>";
                    wlhc1 += "<OPIP>192.168.0.101</OPIP>";
                    wlhc1 += "<OPMANNO>112</OPMANNO>";
                    wlhc1 += "<OPMANNAME>测试员</OPMANNAME>";
                    wlhc1 += "<CONSIS_ORDER_MSTVW>";
                    wlhc1 += "<ORDER_NO>" + r["groupno"].ToString().Trim() + "</ORDER_NO>";
                    wlhc1 += "<ORDERSUB_NO>" + r["groupno"].ToString().Trim() + "</ORDERSUB_NO>";
                    wlhc1 += "<ADVICE_BATCH>" + r["TeamNumber"].ToString().Trim() + "</ADVICE_BATCH>";
                    wlhc1 += "<WARD_CODE>" + r["WardCode"].ToString().Trim() + "</WARD_CODE>";
                    wlhc1 += "<WARD_NAME>" + r["WardName"].ToString().Trim() + "</WARD_NAME>";
                    wlhc1 += "<ADVICE_CODE>" + r["labelno"].ToString().Trim() + "</ADVICE_CODE>";
                    wlhc1 += "<ALLOWIND>N</ALLOWIND>";
                    wlhc1 += "</CONSIS_ORDER_MSTVW>";
                    wlhc1 += "</ROOT>";

                    string s1 = wls.HisTransData(wlhc1);
                    DataSet dsxml1 = new DataSet();
                    StringReader read1 = new StringReader(s1);
                    XmlTextReader readxml1 = new XmlTextReader(read1);
                    dsxml1.ReadXml(readxml1);
                    DataTable dt2 = dsxml1.Tables[0];
                    int i = int.Parse(dt2.Rows[0]["RETCODE"].ToString());
                    if (i == 1)
                    {
                        updateDB_CC(r["labelno"].ToString().Trim());
                    }
                }
            }
        }

        private void updateDB_CC(string s)
        {
            DB_Help DB = new DB_Help();
            try
            {
                string ustr = " insert into IVRecordToCC(LabelNo,CCDT) values('" + s + "', GETDATE()) ";
                DB.SetPIVAsDB(ustr);
            }
            catch (Exception ex)
            {

            }
        }

    }
}
