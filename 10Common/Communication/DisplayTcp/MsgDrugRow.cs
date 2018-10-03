using System.Runtime.InteropServices;

namespace Communication.DisplayTcp
{
    /// <summary>
    /// 发送到屏消息，药品类型，每行的固定样式，长度120个字节
    /// 每个屏的起始地址是0x1000
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 2)]
    public struct MsgDrugRow
    {
        /// <summary>
        /// 行起始地址，类似帧type，但不同，2个字节
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
        public byte[] RowAddress;

        #region Data部分
        /// <summary>
        /// 药品序号，2->8个字节
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
        public byte[] DrugIndex;

        /// <summary>
        /// 药品名称，50个字节
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.I1)]
        public byte[] DrugName;

        /// <summary>
        /// 药品规格，36个字节
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 36, ArraySubType = UnmanagedType.I1)]
        public byte[] DrugSpec;

        /// <summary>
        /// 药品剂量，24->18个字节
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 18, ArraySubType = UnmanagedType.I1)]
        public byte[] DrugDose;

        /// <summary>
        /// 药品数量，6个字节
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
        public byte[] DrugCount;
        #endregion
    }
}
