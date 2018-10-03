using System.Runtime.InteropServices;

namespace Communication.DisplayTcp
{
    /// <summary>
    /// 发送到屏消息，药师行信息，每行的固定样式，长度34个字节
    /// 每个屏的起始地址是0x1000
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 2)]
    public struct MsgDEmployeeRow
    {
        /// <summary>
        /// 行起始地址，类似帧type，但不同，2个字节
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
        public byte[] RowAddress;

        #region Data部分
        /// <summary>
        /// 药师名字标记，固定，药师:,6个字节
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
        public byte[] EmployeeNameMark;

        /// <summary>
        /// 配液量标记，固定，配置量:,8个字节
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
        public byte[] MixCountMark;

        /// <summary>
        /// 显示时间，格式HHmmss MMdd 6个字节，BCD编码，
        /// 四位二进制标识一个0~9，一个字节表示00~99；用五个字节可以把时间显示出来
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
        public byte[] ShowTime;

        /// <summary>
        /// 药师名字，10个字节
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
        public byte[] EmployeeName;

        /// <summary>
        /// 该药师分配的配液数量，4个字节
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
        public byte[] MixCount;
        #endregion
    }
}
