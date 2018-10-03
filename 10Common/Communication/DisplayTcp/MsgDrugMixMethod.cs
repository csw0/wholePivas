using System.Runtime.InteropServices;

namespace Communication.DisplayTcp
{
    /// <summary>
    /// 时间结构体，202个字节
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct MsgDrugMixMethod
    {
        /// <summary>
        /// 行起始地址，类似帧type，但不同，2个字节
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
        public byte[] RowAddress;

        /// <summary>
        /// 配置方法，第一行，25个汉字，50个字节
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.I1)]
        public byte[] MixMethodRow1;
        /// <summary>
        /// 配置方法，第二行，25个汉字，50个字节
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.I1)]
        public byte[] MixMethodRow2;
        /// <summary>
        /// 配置方法，第三行，25个汉字，50个字节
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.I1)]
        public byte[] MixMethodRow3;
        /// <summary>
        /// 配置方法，第四行，25个汉字，50个字节
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.I1)]
        public byte[] MixMethodRow4;
    }
}
