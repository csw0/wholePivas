using System;

namespace Communication
{
    /// <summary>
    /// TCP连接的消息模型类
    /// </summary>
    public class TCPMessage
    {
        public TCPMessage(string serverIp, int serverPort, string moxaData)
        {
            this.serverIp = serverIp;
            this.serverPort = serverPort;
            this.tcpData = moxaData;
        }

        public TCPMessage()
        {
        }

        private string serverIp = String.Empty;
        public string ServerIp
        {
            get { return serverIp; }
            set { serverIp = value; }
        }

        private int serverPort = -1;
        public int ServerPort
        {
            get { return serverPort; }
            set { serverPort = value; }
        }

        private string tcpData = String.Empty;
        public string TcpData
        {
            get { return tcpData; }
            set { tcpData = value; }
        }
    }
}
