using System;

namespace Communication
{
    /// <summary>
    /// TCP连接的消息模型类，byte[] tcpData接收的数据类型不同
    /// </summary>
    public class TCPMessageEx
    {
        public TCPMessageEx(string serverIp, int serverPort, byte[] moxaData)
        {
            this.serverIp = serverIp;
            this.serverPort = serverPort;
            this.tcpData = moxaData;
        }

        public TCPMessageEx()
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

        private byte[] tcpData = null;
        public byte[] TcpData
        {
            get { return tcpData; }
            set { tcpData = value; }
        }
    }
}
