using System.Runtime.InteropServices;

namespace Communication.DisplayTcp
{
    /// <summary>
    /// 发送到屏消息，患者行信息，每行的固定样式，长度66个字节
    /// 每个屏的起始地址是0x1000
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 2)]
    public struct MsgPatientRow
    {
        /// <summary>
        /// 行起始地址，类似帧type，但不同，2个字节
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
        public byte[] RowAddress;

        #region Data部分
        /// <summary>
        /// 患者所在病区名称，32个字节
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
        public byte[] WardName;

        /// <summary>
        /// 患者名字，10个字节
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
        public byte[] PatientName;

        /// <summary>
        /// 瓶签:固定标识,6个字节
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
        public byte[] LabelNoMark;

        /// <summary>
        /// 14位标准瓶签或长度不定的第三方瓶签，16个字节
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
        public byte[] LabelNo;
        #endregion
    }
}
