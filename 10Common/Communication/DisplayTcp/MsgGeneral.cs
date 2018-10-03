using System.Runtime.InteropServices;

namespace Communication.DisplayTcp
{
    /// <summary>
    /// 发送到屏的消息，通用样式，4个字节
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct MsgGeneral
    {
        /// <summary>
        /// 帧头，固定为0x5AA5
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
        public byte[] FrameHead;

        /// <summary>
        /// 帧长度，从FrameConst(含)到帧尾的长度，最大值为255
        /// </summary>
        public byte FrameLen;

        /// <summary>
        /// 帧常量标记，固定为0x82
        /// </summary>
        public byte FrameConst;
    }
}
