using System;
using System.Collections.Generic;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace TPNReview
{
    public class UIHelper
    {
        private static DispatcherOperationCallback exitFrameCallback = new DispatcherOperationCallback(ExitFrame);
        
        //刷新界面
        public static void DoEvents()
        {
            DispatcherFrame nestedFrame = new DispatcherFrame();
            DispatcherOperation exitOperation = 
                Dispatcher.CurrentDispatcher.BeginInvoke(
                DispatcherPriority.Background,
                exitFrameCallback, nestedFrame);
            Dispatcher.PushFrame(nestedFrame);

            if (exitOperation.Status != DispatcherOperationStatus.Completed)
            {
                exitOperation.Abort();
            }
        }
        private static object ExitFrame(object state)
        {
            DispatcherFrame frame = state as DispatcherFrame;
            frame.Continue = false;
            return null;
        }
    }
}
