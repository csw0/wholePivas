using PIVAsCommon;
using PIVAsCommon.Extensions;
using System;
using System.Runtime.InteropServices;

namespace Communication.PLCCom
{
    public class WinAPI_ServialPort
    {
        #region WINAPI常量
        /// <summary>
        /// 写标志
        /// </summary>
        private const uint GENERIC_READ = 0x80000000;
        /// <summary>
        /// 读标志
        /// </summary>
        private const uint GENERIC_WRITE = 0x40000000;

        /// <summary>
        /// 打开已存在
        /// </summary>
        private const int OPEN_EXISTING = 3;

        /// <summary>
        /// 无效句柄
        /// </summary>
        private const int INVALID_HANDLE_VALUE = -1;

        /// <summary>
        /// -PURGE_TXABORT:停止目前所有的传输工作立即返回不管是否完成传输动作。
        /// -PURGE_RXABORT:停止目前所有的读取工作立即返回不管是否完成读取动作。 
        /// -PURGE_TXCLEAR:清除发送缓冲区的所有数据。 
        /// -PURGE_RXCLEAR:清除接收缓冲区的所有数据。
        /// </summary>
        private const int PURGE_TXABORT = 0x0001;
        private const int PURGE_RXABORT = 0x0002;
        private const int PURGE_TXCLEAR = 0x0004;
        private const int PURGE_RXCLEAR = 0x0008;

        private const uint FILE_FLAG_WRITE_THROUGH = 0x80000000;
        #endregion

        #region 成员变量
        /// <summary>
                             /// 端口名称(COM1,COM2...COM4...)
                             /// </summary>
        public string PortNum;

        /// <summary>
        /// 波特率9600
        /// </summary>
        public int BaudRate;

        /// <summary>
        /// 数据位4-8
        /// </summary>
        public byte ByteSize;

        /// <summary>
        /// 奇偶校验0-4=no,odd,even,mark,space
        /// </summary>
        public byte Parity;

        /// <summary>
        /// 发送缓冲区大小
        /// </summary>
        public int BytesToWrite;
        /// <summary>
        /// 接收缓冲区大小
        /// </summary>
        public int BytesToRead;

        /// <summary>
        /// 停止位
        /// </summary>
        public byte StopBits; // 0,1,2 = 1, 1.5, 2

        /// <summary>
        /// 超时长
        /// </summary>
        public int ReadTimeout;

        /// <summary>
        /// COM口句柄
        /// </summary>
        private int hComm = INVALID_HANDLE_VALUE;

        /// <summary>
        /// 串口是否已经打开
        /// </summary>
        public bool Opened = false;
        #endregion

        /// <summary>
        /// 设备控制块结构体类型
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct DCB
        {
            /// <summary>
            /// DCB长度
            /// </summary>
            public int DCBlength;

            /// <summary>
            /// 指定当前波特率
            /// </summary>
            public int BaudRate;

            /// <summary>
            /// 指定是否允许二进制模式
            /// </summary>
            public int fBinary;

            /// <summary>
            /// 指定是否允许奇偶校验
            /// </summary>
            public int fParity;

            /// <summary>
            /// 指定CTS是否用于检测发送控制，当为TRUE是CTS为OFF，发送将被挂起。
            /// </summary>
            public int fOutxCtsFlow;

            /// <summary>
            /// 指定CTS是否用于检测发送控制
            /// </summary>
            public int fOutxDsrFlow;
            /// <summary>
            /// DTR_CONTROL_DISABLE值将DTR置为OFF, DTR_CONTROL_ENABLE值将DTR置为ON, DTR_CONTROL_HANDSHAKE允许DTR"握手"
            /// </summary>
            public int fDtrControl;

            /// <summary>
            /// 当该值为TRUE时DSR为OFF时接收的字节被忽略
            /// </summary>
            public int fDsrSensitivity;
            /// <summary>
            /// 指定当接收缓冲区已满,并且驱动程序已经发送出XoffChar字符时发送是否停止。
            /// TRUE时，在接收缓冲区接收到缓冲区已满的字节XoffLim且驱动程序已经发送出
            /// XoffChar字符中止接收字节之后，发送继续进行。　FALSE时，在接收缓冲区接
            /// 收到代表缓冲区已空的字节XonChar且驱动程序已经发送出恢复发送的XonChar之
            /// 后，发送继续进行。XOFF continues Tx
            /// </summary>
            public int fTXContinueOnXoff;

            /// <summary>
            /// TRUE时，接收到XoffChar之后便停止发送接收到XonChar之后将重新开始 XON/XOFF
            /// out flow control
            /// </summary>
            public int fOutX;
            /// <summary>
            /// TRUE时，接收缓冲区接收到代表缓冲区满的XoffLim之后，XoffChar发送出去接收
            /// 缓冲区接收到代表缓冲区空的XonLim之后，XonChar发送出去 XON/XOFF in flow control
            /// </summary>
            public int fInX;

            /// <summary>
            /// 该值为TRUE且fParity为TRUE时，用ErrorChar 成员指定的字符代替奇偶校验错误
            /// 的接收字符 enable error replacement
            /// </summary>
            public int fErrorChar;

            /// <summary>
            /// eTRUE时，接收时去掉空（0值）字节 enable null stripping
            /// </summary>
            public int fNull;

            /// <summary>
            /// RTS flow control RTS_CONTROL_DISABLE时,RTS置为OFF RTS_CONTROL_ENABLE时, RTS置为ON
            /// RTS_CONTROL_HANDSHAKE时,当接收缓冲区小于半满时RTS为ON当接收缓冲区超过四分之
            /// 三满时RTS为OFF RTS_CONTROL_TOGGLE时,当接收缓冲区仍有剩余字节时RTS为ON ,否则
            /// 缺省为OFF
            /// </summary>
            public int fRtsControl;

            /// <summary>
            /// TRUE时,有错误发生时中止读和写操作 abort on error
            /// </summary>
            public int fAbortOnError;
            /// <summary>
            /// 未使用
            /// </summary>
            public int fDummy2;
            /// <summary>
            /// 标志位
            /// </summary>
            public uint flags;
            /// <summary>
            /// 未使用,必须为0
            /// </summary>
            public ushort wReserved;

            /// <summary>
            /// 指定在XON字符发送这前接收缓冲区中可允许的最小字节数
            /// </summary>
            public ushort XonLim;

            /// <summary>
            /// 指定在XOFF字符发送这前接收缓冲区中可允许的最小字节数
            /// </summary>
            public ushort XoffLim;

            /// <summary>
            /// 指定端口当前使用的数据位
            /// </summary>
            public byte ByteSize;

            /// <summary>
            /// 指定端口当前使用的奇偶校验方法,可能为:EVENPARITY,MARKPARITY,NOPARITY,ODDPARITY 0-4=no,odd,even,mark,space
            /// </summary>
            public byte Parity;

            /// <summary>
            /// 指定端口当前使用的停止位数,可能为:ONESTOPBIT,ONE5STOPBITS,TWOSTOPBITS 0,1,2 = 1, 1.5, 2
            /// </summary>
            public byte StopBits;

            /// <summary>
            /// 指定用于发送和接收字符XON的值 Tx and Rx XON character
            /// </summary>
            public char XonChar;

            /// <summary>
            /// 指定用于发送和接收字符XOFF值 Tx and Rx XOFF character
            /// </summary>
            public char XoffChar;

            /// <summary>
            /// 本字符用来代替接收到的奇偶校验发生错误时的值
            /// </summary>
            public char ErrorChar;

            /// <summary>
            /// 当没有使用二进制模式时,本字符可用来指示数据的结束
            /// </summary>
            public char EofChar;

            /// <summary>
            /// 当接收到此字符时,会产生一个事件
            /// </summary>
            public char EvtChar;

            /// <summary>
            /// 未使用
            /// </summary>
            public ushort wReserved1;
        }

        /// <summary>
        /// 串口超时时间结构体类型
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct COMMTIMEOUTS
        {
            public int ReadIntervalTimeout;
            public int ReadTotalTimeoutMultiplier;
            public int ReadTotalTimeoutConstant;
            public int WriteTotalTimeoutMultiplier;
            public int WriteTotalTimeoutConstant;
        }

        /// <summary>
        /// 湓出缓冲区结构体类型
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct OVERLAPPED
        {
            public int Internal;
            public int InternalHigh;
            public int Offset;
            public int OffsetHigh;
            public int hEvent;
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="lpFileName">要打开的串口名称</param>
        /// <param name="dwDesiredAccess">指定串口的访问方式，一般设置为可读可写方式</param>
        /// <param name="dwShareMode">指定串口的共享模式，串口不能共享，所以设置为0</param>
        /// <param name="lpSecurityAttributes">设置串口的安全属性，WIN9X下不支持，应设为NULL</param>
        /// <param name="dwCreationDisposition">对于串口通信，创建方式只能为OPEN_EXISTING</param>
        /// <param name="dwFlagsAndAttributes">指定串口属性与标志，设置为FILE_FLAG_OVERLAPPED(重叠I/O操作)，指定串口以异步方式通信</param>
        /// <param name="hTemplateFile">对于串口通信必须设置为NULL</param>
        [DllImport("kernel32.dll")]
        private static extern int CreateFile(string lpFileName, uint dwDesiredAccess, int dwShareMode,
            int lpSecurityAttributes, int dwCreationDisposition, uint dwFlagsAndAttributes, int hTemplateFile);

        /// <summary>
        /// 得到串口状态
        /// </summary>
        /// <param name="hFile">通信设备句柄</param>
        /// <param name="lpDCB">设备控制块DCB</param>
        [DllImport("kernel32.dll")]
        private static extern bool GetCommState(int hFile, ref DCB lpDCB);
        /// <summary>
        /// 建立串口设备控制块
        /// </summary>
        /// <param name="lpDef">设备控制字符串</param>
        /// <param name="lpDCB">设备控制块</param>
        [DllImport("kernel32.dll")]
        private static extern bool BuildCommDCB(string lpDef, ref DCB lpDCB);

        /// <summary>
        /// 设置串口状态
        /// </summary>
        /// <param name="hFile">通信设备句柄</param>
        /// <param name="lpDCB">设备控制块</param>
        [DllImport("kernel32.dll")]
        private static extern bool SetCommState(int hFile, ref DCB lpDCB);

        /// <summary>
        /// 读取串口超时时间
        /// </summary>
        /// <param name="hFile">通信设备句柄</param>
        /// <param name="lpCommTimeouts">超时时间</param>
        [DllImport("kernel32.dll")]
        private static extern bool GetCommTimeouts(int hFile, ref COMMTIMEOUTS lpCommTimeouts);

        /// <summary>
        /// 设置串口超时时间
        /// </summary>
        /// <param name="hFile">通信设备句柄</param>
        /// <param name="lpCommTimeouts">超时时间</param>
        [DllImport("kernel32.dll")]
        private static extern bool SetCommTimeouts(int hFile, ref COMMTIMEOUTS lpCommTimeouts);

        /// <summary>
        /// 读取串口数据
        /// </summary>
        /// <param name="hFile">通信设备句柄</param>
        /// <param name="lpBuffer">数据缓冲区</param>
        /// <param name="nNumberOfBytesToRead">多少字节等待读取</param>
        /// <param name="lpNumberOfBytesRead">读取多少字节</param>
        /// <param name="lpOverlapped">溢出缓冲区</param>
        [DllImport("kernel32.dll")]
        private static extern bool ReadFile(int hFile, byte[] lpBuffer,
            int nNumberOfBytesToRead, ref int lpNumberOfBytesRead, ref OVERLAPPED lpOverlapped);

        /// <summary>
        /// 写串口数据
        /// </summary>
        /// <param name="hFile">通信设备句柄</param>
        /// <param name="lpBuffer">数据缓冲区</param>
        /// <param name="nNumberOfBytesToWrite">多少字节等待写入</param>
        /// <param name="lpNumberOfBytesWritten">已经写入多少字节</param>
        /// <param name="lpOverlapped">溢出缓冲区</param>
        [DllImport("kernel32.dll")]
        private static extern bool WriteFile(int hFile, byte[] lpBuffer,
            int nNumberOfBytesToWrite, ref int lpNumberOfBytesWritten, ref OVERLAPPED lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FlushFileBuffers(int hFile);

        /// <summary>
        ///  清空缓冲区
        /// </summary>
        /// <param name="hFile"></param>
        /// <param name="dwFlags"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool PurgeComm(int hFile, uint dwFlags);

        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <param name="hObject">通信设备句柄</param>
        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(int hObject);

        /// <summary>
        /// 得到串口最后一次返回的错误
        /// </summary>
        [DllImport("kernel32.dll")]
        private static extern uint GetLastError();

        /// <summary>
        /// 设置缓冲区
        /// </summary>
        /// <param name="hFile"></param>
        /// <param name="dwInQueue">输入缓冲区字节大小</param>
        /// <param name="dwOutQueue">输出缓冲区字节大小</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern bool SetupComm(int hFile, int dwInQueue, int dwOutQueue);

        /// <summary>
        /// 建立与串口的连接
        /// </summary>
        public bool Open()
        {
            DCB dcbCommPort = new DCB();
            COMMTIMEOUTS ctoCommPort = new COMMTIMEOUTS();
            // 打开串口
            //hComm = CreateFile(PortNum, GENERIC_READ | GENERIC_WRITE, 0, 0, OPEN_EXISTING, 0, 0);
            hComm = CreateFile(PortNum, GENERIC_READ | GENERIC_WRITE, 0, 0, OPEN_EXISTING, FILE_FLAG_WRITE_THROUGH, 0);
            if (hComm == INVALID_HANDLE_VALUE)
            {
                return false;
            }
            // 设置通信超时时间
            GetCommTimeouts(hComm, ref ctoCommPort);
            ctoCommPort.ReadTotalTimeoutConstant = ReadTimeout;
            ctoCommPort.ReadTotalTimeoutMultiplier = 0;
            ctoCommPort.WriteTotalTimeoutMultiplier = 0;
            ctoCommPort.WriteTotalTimeoutConstant = 0;
            SetCommTimeouts(hComm, ref ctoCommPort);
            // 设置串口
            GetCommState(hComm, ref dcbCommPort);
            dcbCommPort.fOutxCtsFlow = 524800;
            dcbCommPort.BaudRate = BaudRate;
            dcbCommPort.flags = 0;
            dcbCommPort.flags |= 1;
            if (Parity > 0)
            {
                dcbCommPort.flags |= 2;
            }
            dcbCommPort.Parity = Parity;
            dcbCommPort.ByteSize = ByteSize;
            dcbCommPort.StopBits = StopBits;
            dcbCommPort.fOutxCtsFlow = 524800;
            if (!SetCommState(hComm, ref dcbCommPort))
                return false;

            if (!SetupComm(hComm, BytesToRead, BytesToWrite))
                return false;

            Opened = true;
            return true;
        }
        /// <summary>
        /// 关闭串口,结束通讯
        /// </summary>
        public bool Close()
        {
            if (hComm != INVALID_HANDLE_VALUE)
            {
                CloseHandle(hComm);
                Opened = false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 读取串口返回的数据
        /// </summary>
        /// <param name="NumBytes">数据长度</param>
        public byte[] Read(int NumBytes)
        {
            byte[] BufBytes;
            byte[] OutBytes;
            BufBytes = new byte[NumBytes];
            if (hComm != INVALID_HANDLE_VALUE)
            {
                OVERLAPPED ovlCommPort = new OVERLAPPED();
                int BytesRead = 0;

                ReadFile(hComm, BufBytes, NumBytes, ref BytesRead, ref ovlCommPort);
                OutBytes = new byte[BytesRead];
                Array.Copy(BufBytes, OutBytes, BytesRead);
                return OutBytes;
            }
            else
            {
                return new byte[0];
            }
        }

        /// <summary>
        /// 清空发送缓冲区数据
        /// </summary>
        /// <returns></returns>
        public bool ClearPortData()
        {
            if (hComm != INVALID_HANDLE_VALUE)
            {
                //PurgeComm(hComm, PURGE_RXCLEAR | PURGE_TXCLEAR | PURGE_RXABORT | PURGE_TXABORT);
                return PurgeComm(hComm, PURGE_TXCLEAR);
            }
            return false;
        }

        /// <summary>
        /// 向串口写数据
        /// </summary>
        /// <param name="WriteBytes">数据数组</param>
        public int Write(byte[] WriteBytes)
        {
            if (hComm != INVALID_HANDLE_VALUE)
            {
                OVERLAPPED ovlCommPort = new OVERLAPPED();
                int BytesWritten = 0;
                WriteFile(hComm, WriteBytes, WriteBytes.Length,ref BytesWritten, ref ovlCommPort);
                //if (!FlushFileBuffers(hComm))
                //{
                //    uint rtn = GetLastError();
                //    InternalLogger.Log.Error("写com数据出错：" + rtn.ToString());
                //}
                return BytesWritten;
            }
            else
                return 0;
        }

        /// <summary>
        /// 第一种模式，所有端口一起发
        /// </summary>
        /// <param name="plcAddress">PLC地址</param>
        /// <param name="thirdFrame">1~8端口;一个bit一个端口</param>
        /// <param name="fourthFrame">9~16端口；一个bit一个端口</param>
        /// <returns></returns>
        public void SendDataAllOut(byte plcAddress, byte thirdFrame, byte fourthFrame)
        {
            try
            {
                if (hComm != INVALID_HANDLE_VALUE && Opened)
                {
                    byte[] buf = new byte[6];
                    buf[0] = plcAddress;
                    buf[1] = 1;//第一种模式，所有端口一起发
                    buf[2] = thirdFrame;
                    buf[3] = fourthFrame;
                    int crcCode = CRC8(buf, 4);
                    buf[4] = (byte)crcCode;
                    buf[5] = (byte)(crcCode >> 8);

                    ClearPortData();
                    int sendByteLen = Write(buf);

                    if (sendByteLen == 6)
                        InternalLogger.Log.Info("发送数据(" + buf.BytesToHexString() + ")到PLC=" + plcAddress.ToString());
                    else
                        InternalLogger.Log.Warn("因发送plc的数据长度" + sendByteLen + "不足，导致发送失败");
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("发送数据到Com口出错：" + ex.Message);
            }
        }

        /// <summary>
        /// CRC校验算法
        /// </summary>
        /// <param name="ptr">字节数组</param>
        /// <param name="len">需生成校验码的数组长度，此值需小于等于ptr的长度</param>
        /// <returns></returns>
        private int CRC8(byte[] ptr, int len)
        {
            int crc = 0xFFFF;
            for (int j = 0; j < len; j++)
            {
                crc ^= ptr[j];
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x01) > 0)
                    {
                        crc = (crc >> 1) ^ 0xA001;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }
            return crc;
        }
    }
}
