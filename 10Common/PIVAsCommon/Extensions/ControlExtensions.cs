using System;
using System.Windows.Forms;

namespace PIVAsCommon.Extensions
{
    public static class ControlExtensions
    {
        public static void SafeAction(this Control ctrl, Action action)
        {
            if (ctrl == null || ctrl.IsDisposed || action == null) return;
            if (ctrl.InvokeRequired)
            {
                ctrl.Invoke(action);
            }
            else
            {
                action();
            }
        }

        public static void SafeAction<T>(this Control ctrl, Action<T> action, T value)
        {
            if (ctrl == null || ctrl.IsDisposed || !ctrl.Created || action == null) return;
            if (ctrl.InvokeRequired)
            {
                ctrl.Invoke(action, value);
            }
            else
            {
                action(value);
            }
        }       
    }
}
