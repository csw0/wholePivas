using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace ChargeInterface.ChargeTJXKService
{
    public class MakeJson
    {
        #region JSON转换
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Str"></param>
        /// <param name="reqMsgSerial">发送方UUID</param>
        /// <returns></returns>
        public static List<Dictionary<string, object>> JsonToDataTable(string Str, out string reqMsgSerial)
        {
            int info = 0;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, object> jsonData = new Dictionary<string, object>();
            Dictionary<string, object> rows = new Dictionary<string, object>();
            List<Dictionary<string, object>> datas = new List<Dictionary<string, object>>();
            reqMsgSerial = "";
            try
            {
                jsonData = (Dictionary<string, object>)jss.DeserializeObject(Str);
                //header信息||  headers["channelFlag"] !=  || headers["sysCode"] != "HIS" || headers["msgType"] != 
                Dictionary<string, object> headers = (Dictionary<string, object>)jsonData["headers"];

                reqMsgSerial = headers["msgSerial"].ToString();
                //传入同步数据
                Dictionary<string, object> payload = (Dictionary<string, object>)jsonData["payload"];
                Dictionary<string, object> rows1 = (Dictionary<string, object>)payload["response"];

                foreach (var key in rows1.Keys)
                {
                    if (null == rows1[key])
                    {
                        rows.Add(key, "");
                    }
                    else
                    {
                        rows.Add(key, rows1[key]);
                    }
                }
                //若传入脚本中存在item则为数组
                if (rows.ContainsKey("items") == true)
                {
                    var items = (object[])rows["items"];
                    if (items.Length != 0)
                    {
                        foreach (var item in items)
                        {
                            Dictionary<string, object> rowsTmp = new Dictionary<string, object>();
                            foreach (var key in rows.Keys)
                            {
                                rowsTmp.Add(key, rows[key]);
                            }
                            rowsTmp.Remove("items");
                            Dictionary<string, object> dic = (Dictionary<string, object>)item;
                            foreach (var key in dic.Keys)
                            {
                                if (null == dic[key])
                                {
                                    rowsTmp.Add(key, "");
                                }
                                else
                                {
                                    rowsTmp.Add(key, dic[key]);
                                }
                            }
                            datas.Add(rowsTmp);
                        }
                    }
                }
                else
                {
                    datas.Add(rows);
                }
            }
            catch
            {
                info = 1;
                throw new Exception(info.ToString());
            }

            return datas;
        }
        #endregion
    }
}
