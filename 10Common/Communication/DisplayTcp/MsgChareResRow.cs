using System.Runtime.InteropServices;

namespace Communication.DisplayTcp
{
    /// <summary>
    /// 计费结果行，50+2个字节
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct MsgChareResRow
    {
        /// <summary>
        /// 行起始地址，类似帧type，但不同，2个字节
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
        public byte[] RowAddress;

        /// <summary>
        /// 计费后的消息，25个汉字，50个字节
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.I1)]
        public byte[] ChargeMessage;
    }
}
