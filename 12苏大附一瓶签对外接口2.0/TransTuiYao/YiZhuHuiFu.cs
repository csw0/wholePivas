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
    class YiZhuHuiFu
    {
        ServiceReference1.HisServiceClient wls = new HisServiceClient();
        DB_Help DB = new DB_Help();

        public void twotwofivetwo()
        {
            try
            {
                string str = " select distinct top 100 labelno,groupno from IVRecord(nolock) "+
                    "where labelover = 0 and DATEDIFF(dd,GETDATE(),InfusionDT) >= -1 and "+
                    "LabelNo in (select LabelNo from IVRecordToTY) ";
                //string str = " select distinct top 100 labelno from IVRecord where labelover < 0 and DATEDIFF(dd,GETDATE(),InfusionDT) >= 0 and LabelNo not in (select LabelNo from IVRecordToTY) ";
                //string str = " select distinct top 100 labelno from IVRecord where labelover < 0 and (labelno like '20171010%' or labelno like '20171011%') and LabelNo not in (select LabelNo from IVRecordToTY) ";
                //string str = " select labelno from IVRecord where labelover < 0 and LabelNo in ('20171010102387') and LabelNo not in (select LabelNo from IVRecordToTY) ";
                DataSet ds = DB.GetPIVAsDB(str);
                if (ds.Tables.Count > 0 && !(ds == null) && ds.Tables[0].Rows.Count > 0)
                {
                    string sendstr = makestr_ii(ds);
                    //调应webservice
                    string s = wls.HisTransData(sendstr);
                    DataSet dsxml = new DataSet();
                    StringReader read = new StringReader(s);
                    XmlTextReader readxml = new XmlTextReader(read);
                    dsxml.ReadXml(readxml);
                    DataTable dt1 = dsxml.Tables[0];
                    int i = int.Parse(dt1.Rows[0]["RETCODE"].ToString());
                    if (i == 1)
                    {
                        delete_all(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                //暂未处理，输出异常到log
            }
        }

        private string makestr_ii(DataSet ds)
        {
            string wlhc = "";
            wlhc += "<ROOT>\n";
            wlhc += "<OPSYSTEM>HIS</OPSYSTEM>\n";
            wlhc += "<OPWINID></OPWINID>\n";
            wlhc += "<OPTYPE>2253</OPTYPE>\n";
            wlhc += "<OPIP>192.168.0.101</OPIP>\n";
            wlhc += "<OPMANNO>112</OPMANNO>\n";
            wlhc += "<OPMANNAME>测试员</OPMANNAME>\n";
            foreach (DataRow r in ds.Tables[0].Rows)
            {

                wlhc += "<CONSIS_ORDER_MSTVW>\n";
                wlhc += "<ADVICE_CODE>";
                wlhc += r["labelno"].ToString().Trim();
                wlhc += "</ADVICE_CODE>\n";
                wlhc += "<STATUS>N</STATUS>\n";
                wlhc += "</CONSIS_ORDER_MSTVW>\n";
            }
            wlhc += "</ROOT>";
            return wlhc;
        }

        private void delete_all(DataSet ds)
        {
            try
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    string str = " delete from IVRecordToTY where LabelNo = '" + r["labelno"].ToString().Trim() + "' ";
                    string str1 = " delete from IVRecordToCN where LabelNo = '" + r["labelno"].ToString().Trim() + "' ";
                    string strCC = " delete from IVRecordToCC where LabelNo = '" + r["labelno"].ToString().Trim() + "' ";
                    DB.SetPIVAsDB(str);
                    DB.SetPIVAsDB(str1);
                    DB.SetPIVAsDB(strCC);
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}
