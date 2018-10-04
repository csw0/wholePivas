using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Communication.WindowsScreen
{
    /// <summary>
    /// 基类
    /// </summary>
    public abstract class MsgBase
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public short MsgType { get; set; }
    }
}
