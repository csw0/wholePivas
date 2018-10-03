using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PIVAS_MATE
{
    /// <summary>
    /// pivas中所有画面枚举
    /// </summary>
    public enum PageType : byte
    {
        SynNo = 1,//同步
        Review = 2,//审方
        Batch = 3,
        Check = 4,
        Print = 5,
        Tool = 6,
        TpnReview = 7//Tpn审方
    }

    /// <summary>
    /// 画面按钮的效果
    /// </summary>
    public enum PageLabelEffect : byte
    {
        MouseLeave = 1,//鼠标离开后的效果
        MouseMove = 2,//鼠标移动中的效果
        MouseDown = 3,//鼠标按下后的效果
    }
}
