using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;


namespace BLPublic
{
    public class PinYin
    {
        public static string getCode(string _zh)
        {
            string rt = "";

            byte[] array = new byte[2]; 
            int chrAsc = 0;
            int i1 = 0;
            int i2 = 0;
            char[] noWChar = _zh.ToCharArray();

            for (int j = 0; j < noWChar.Length; j++)
            {
                // 中文字符
                if ((0x4e00 < noWChar[j]) && (noWChar[j] < 0x9fa5))
                {
                    array = System.Text.Encoding.Default.GetBytes(noWChar[j].ToString());
                    i1 = (short)(array[0]);
                    i2 = (short)(array[1]);
                    chrAsc = i1 * 256 + i2 - 65536;
                    if (chrAsc > 0 && chrAsc < 160)
                    {
                        rt += noWChar[j];
                    }
                    else if (chrAsc == -9254) // 修正"圳"字
                        rt += "z";
                    else
                        rt += getOneCode(chrAsc);
                }
                // 非中文字符
                else
                {
                    rt += noWChar[j].ToString();
                }
            }

            return rt;
        }

        private static string getOneCode(int i)
        {
            if (i < 0xB0A1) return "*";
            if (i < 0xB0C5) return "a";
            if (i < 0xB2C1) return "b";
            if (i < 0xB4EE) return "c";
            if (i < 0xB6EA) return "d";
            if (i < 0xB7A2) return "e";
            if (i < 0xB8C1) return "f";
            if (i < 0xB9FE) return "g";
            if (i < 0xBBF7) return "h";
            if (i < 0xBFA6) return "g";
            if (i < 0xC0AC) return "k";
            if (i < 0xC2E8) return "l";
            if (i < 0xC4C3) return "m";
            if (i < 0xC5B6) return "n";
            if (i < 0xC5BE) return "o";
            if (i < 0xC6DA) return "p";
            if (i < 0xC8BB) return "q";
            if (i < 0xC8F6) return "r";
            if (i < 0xCBFA) return "s";
            if (i < 0xCDDA) return "t";
            if (i < 0xCEF4) return "w";
            if (i < 0xD1B9) return "x";
            if (i < 0xD4D1) return "y";
            if (i < 0xD7FA) return "z";

            return "*";
        }
    }


    public class WBCode
	{
		//private TDLLGetZhCode = function(AZh: PAnsiChar; ACodeTpy: UCHAR; out Code: PAnsiChar): BOOL; stdcall;
		private delegate bool GetZhCode(string _Zh, byte _codeType, ref string __code);

		[DllImport("Kernel32")]
        private static extern int GetProcAddress(int handle, String funcname);  
        [DllImport("Kernel32")]
        private static extern int LoadLibrary(String funcname);  
        [DllImport("Kernel32")]
        private static extern int FreeLibrary(int handle);

		private int hndDll = 0;
		private GetZhCode getWBCode = null;

		public WBCode()
		{
            this.hndDll = LoadLibrary("libZHCode.dll");
            if (0 < this.hndDll)
            {
                int addr = GetProcAddress(this.hndDll, "GetZhCode");
                if (0 < addr)
                {
                    this.getWBCode = (GetZhCode)Marshal.GetDelegateForFunctionPointer(new IntPtr(addr), typeof(GetZhCode));
                }
            }
		}

		~WBCode()
		{
			if (0 < this.hndDll)
				FreeLibrary(this.hndDll);
		}
		
		public string getCode(string _str)
		{
            string code = "";
			string rt = "";
            if (null != this.getWBCode)
            {
                this.getWBCode(_str, 4, ref code);
                rt = code;
            }

			return rt;
		}
	}
}
