using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Communication.WindowsScreen
{
    /// <summary>
    /// 药师登录状态实体
    /// </summary>
    public class MsgLoginResult : MsgBase
    {
        /// <summary>
        /// 药师登录状态，0未登录状态，1登录状态
        /// </summary>
        public short Status { get; set; }
    }
}
