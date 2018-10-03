using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PIVAsCommon
{
    /// <summary>
    /// pivas主界面各模块菜单管理接口
    /// </summary>
    public interface IMenuManager : IDisposable
    {
        /// <summary>
        /// 选中前，做一些界面初始化工作
        /// </summary>
        void menuBeforeSelect();

        /// <summary>
        /// 选中后，做一些加载工作；暂时不用
        /// </summary>
        void menuAfterSelect();
    }
}
