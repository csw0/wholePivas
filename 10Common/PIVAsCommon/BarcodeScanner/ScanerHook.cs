using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace PIVAsCommon.BarcodeScanner
{
    public class ScanerHook
    {
        public delegate void ScanerDelegate(ScanerCodes codes);
        public event ScanerDelegate ScanerEvent;

        //private const int WM_KEYDOWN = 0x100;//KEYDOWN
        //private const int WM_KEYUP = 0x101;//KEYUP
        //private const int WM_SYSKEYDOWN = 0x104;//SYSKEYDOWN
        //private const int WM_SYSKEYUP = 0x105;//SYSKEYUP
        //private static int HookProc(int nCode, Int32 wParam, IntPtr lParam);
        private int hKeyboardHook = 0;//声明键盘钩子处理的初始值
        private ScanerCodes codes = new ScanerCodes();//13为键盘钩子
        //定义成静态，这样不会抛出回收异常
        private static HookProc hookproc;

        delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //设置钩子
        private static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //卸载钩子
        private static extern bool UnhookWindowsHookEx(int idHook);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //继续下个钩子
        private static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);

        [DllImport("user32", EntryPoint = "GetKeyNameText")]
        public static extern int GetKeyNameText(int IParam, StringBuilder lpBuffer, int nSize);
        [DllImport("user32", EntryPoint = "GetKeyboardState")]
        //获取按键的状态
        public static extern int GetKeyboardState(byte[] pbKeyState);
        [DllImport("user32", EntryPoint = "ToAscii")]
        //ToAscii职能的转换指定的虚拟键码和键盘状态的相应字符或字符
        private static extern bool ToAscii(int VirtualKey, int ScanCode, byte[] lpKeySate, ref uint lpChar, int uFlags);

        //int VirtualKey //[in] 指定虚拟关键代码进行翻译。
        //int uScanCode, // [in] 指定的硬件扫描码的关键须翻译成英文。高阶位的这个值设定的关键，如果是（不压）
        //byte[] lpbKeyState, // [in] 指针，以256字节数组，包含当前键盘的状态。每个元素（字节）的数组包含状态的一个关键。如果高阶位的字节是一套，关键是下跌（按下）。在低比特，如/果设置表明，关键是对切换。在此功能，只有肘位的CAPS LOCK键是相关的。在切换状态的NUM个锁和滚动锁定键被忽略。
        //byte[] lpwTransKey, // [out] 指针的缓冲区收到翻译字符或字符。
        //uint fuState); // [in] Specifies whether a menu is active. This parameter must be 1 if a menu is active, or 0 otherwise.

        [DllImport("kernel32.dll")]
        //使用WINDOWS API函数代替获取当前实例的函数,防止钩子失效
        public static extern IntPtr GetModuleHandle(string name);

        public ScanerHook()
        {
        }
        public bool Start()
        {
            if (hKeyboardHook == 0)
            {
                hookproc = new HookProc(KeyboardHookProc);
                //GetModuleHandle 函数 替代 Marshal.GetHINSTANCE  
                //防止在 framework4.0中 注册钩子不成功  
                IntPtr modulePtr = GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName);
                //WH_KEYBOARD_LL=13  
                //全局钩子 WH_KEYBOARD_LL  
                //  hKeyboardHook = SetWindowsHookEx(13, hookproc, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);  
                hKeyboardHook = SetWindowsHookEx(13, hookproc, modulePtr, 0);
            }
            return (hKeyboardHook != 0);
        }
        public bool Stop()
        {
            if (hKeyboardHook != 0)
            {
                bool retKeyboard = UnhookWindowsHookEx(hKeyboardHook);
                hKeyboardHook = 0;
                return retKeyboard;
            }
            return true;
        }
        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            EventMsg msg = (EventMsg)Marshal.PtrToStructure(lParam, typeof(EventMsg));
            codes.Add(msg);
            if (ScanerEvent != null && msg.message == 13 && msg.paramH > 0 && !string.IsNullOrEmpty(codes.Result))
            {
                ScanerEvent(codes);
            }
            return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
        }
    }
}
