using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChargeInterface.ChargeTJXKService
{
    public class MakeHeader
    {
        //目的系统代码
        private const string destination = "HIS";

        //报文类型
        private const string msgType = "noBroadcast";

        //请求渠道标识
        private const string requestChannelFlag = "HISCHANNEL";

        //应答渠道标识
        private const string responseChannelFlag = "PIVSCHANNEL";

        //发送方系统代码
        private const string sysCode = "PIVS";

        /// <summary>
        /// 请求报文头部
        /// </summary>
        /// <param name="payload">有效荷载</param>
        /// <returns>请求信息json</returns>
        public static string requestHeader(string msgCode, string payload)
        {
            StringBuilder requestHeaderJson = new StringBuilder();
            requestHeaderJson.Append("{");
            requestHeaderJson.Append("\"headers\":");
            requestHeaderJson.Append("{");
            requestHeaderJson.Append("\"sendTime\":\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\"");
            requestHeaderJson.Append(",\"sendType\":\"request\"");
            requestHeaderJson.Append(",\"msgSerial\":\"" + Guid.NewGuid() + "\"");
            requestHeaderJson.Append(",\"destination\":\"" + destination + "\"");
            requestHeaderJson.Append(",\"msgCode\":\"" + msgCode + "\"");
            requestHeaderJson.Append(",\"channelFlag\":\"" + requestChannelFlag + "\"");
            requestHeaderJson.Append(",\"msgType\":\"" + msgType + "\"");
            requestHeaderJson.Append(",\"sysCode\":\"" + sysCode + "\"");
            requestHeaderJson.Append("}");
            requestHeaderJson.Append(",\"payload\":" + payload);
            requestHeaderJson.Append("}");
            return requestHeaderJson.ToString();
        }

        /// <summary>
        /// 应答报文
        /// "headers" : {
        ///"sendTime" : "2014-06-09 10:02:09",
        ///"sendType" : "response",
        ///"msgSerial" : "119a6599-cd2b-4514-a4ab-0bb678de9cb8",
        ///"reqMsgSerial" : "bd8a4b6f-eb9a-45c5-a3f0-198619a2a6ed",
        ///"destination" : "HIS",
        ///"msgCode" : "PACS00000011",
        ///"channelFlag" : "PACSCHANNEL",
        ///"msgType" : "noBroadcast",
        ///"sysCode" : "PACS"
        ///}
        /// </summary>
        /// <param name="reqMsgSerial">对应请求报文流水号</param>
        /// <param name="err">错误信息</param>
        /// <param name="payload">有效荷载</param>
        /// <returns>应答信息json</returns>
        /// 
        ///
        public static string responseHeader(string reqMsgSerial, string msgCode, string err, string payload)
        {
            StringBuilder responseHeaderJson = new StringBuilder();
            responseHeaderJson.Append("{");
            responseHeaderJson.Append("\"header\":");
            responseHeaderJson.Append("{");
            responseHeaderJson.Append("\"sendTime\":\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\"");
            responseHeaderJson.Append(",\"sendType\":\"response\",");
            responseHeaderJson.Append(",\"msgSerial\":\"" + Guid.NewGuid().ToString() + "\"");
            responseHeaderJson.Append(",\"reqMsgSerial\":\"" + reqMsgSerial + "\"");
            responseHeaderJson.Append(",\"destination\":\"" + destination + "\"");
            responseHeaderJson.Append(",\"msgCode\":\"" + msgCode + "\"");
            responseHeaderJson.Append(",\"channelFlag\":\"" + responseChannelFlag + "\"");
            responseHeaderJson.Append(",\"msgType\":\"" + msgType + "\"");
            responseHeaderJson.Append(",\"sysCode\":\"" + sysCode + "\"");
            if (err.Trim() != "")
            {
                responseHeaderJson.Append(",\"err\":\"" + err + "\"");
            }
            responseHeaderJson.Append("}");
            responseHeaderJson.Append(",\"payload\":" + payload);
            responseHeaderJson.Append("}");
            return responseHeaderJson.ToString();
        }

        public static string makePayLoad(string codeName, string code, string result, string resultText)
        {
            StringBuilder responsePayload = new StringBuilder();
            responsePayload.Append("{");
            responsePayload.Append("\"response\":{");
            responsePayload.Append("\"" + codeName + "\":\"" + code + "\"");
            responsePayload.Append(",\"result\":\"" + result + "\"");
            responsePayload.Append(",\"resultText\":\"" + resultText + "\"");
            responsePayload.Append("}");
            responsePayload.Append(",\"errInfo\":{");
            responsePayload.Append("\"errCode\":\"" + result + "\"");
            responsePayload.Append("\"msgErrType\":\"" + resultText + "\"");
            responsePayload.Append("\"position\":\"" + GetHostAddress() + "\"");
            responsePayload.Append("}");
            responsePayload.Append("}");
            return responsePayload.ToString();
        }

        //XML串
        public static string makeRequestPayLoad(string label, string text)
        {
            StringBuilder responsePayload = new StringBuilder();
            responsePayload.Append("{");
            responsePayload.Append("\"" + @text + "\"");
            responsePayload.Append("}");
            return responsePayload.ToString();
        }

        private static string GetHostAddress()
        {
            IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
            for (int i = 0; i < ipe.AddressList.Length; i++)
            {
                if (ipe.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    return ipe.AddressList[i].ToString();
                }
            }
            return "";
        }
    }
}
