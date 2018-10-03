using System;
using System.Collections;
using System.Windows.Forms;
using System.Text;
using System.Text.RegularExpressions;

namespace BLPublic
{
    /// <summary>
    /// 无参数无返回值委托函数
    /// 主要用于 Dispatcher.Invoke 函数里
    /// eg: this.Dispatcher.Invoke(new common.DelegateVoid(() => { ...; }));
    /// </summary>
    public delegate void DelegateVoid();

    public class BLCrypt
    {
        public static string Encrypt(string _text, string _key)
        {
            if (1 >= _text.Trim().Length) return "";
            int key_len = _key.Length;
            char[] key = null;
            if (0 == key_len)
                key = "Think Space".ToCharArray();
            else
                key = _key.ToCharArray();
            char[] txt = _text.ToCharArray();
            int txt_len = txt.Length;
            int keyPos = 0;
            int srcPos = 0;
            int srcAsc = 0;
            int offset = 0;
            int tmpSrcAsc = 0;
            StringBuilder rt = new StringBuilder();
            Random rnd = new Random();
            offset = rnd.Next(0, 255);
            rt.Append(string.Format("{0,2:X2}", offset));

            while (srcPos < txt_len)
            {
                srcAsc = Convert.ToInt32(txt[srcPos]);
                srcAsc = (srcAsc + offset) % 255;
                tmpSrcAsc = srcAsc ^ ((int)(key[keyPos]));
                rt.Append(string.Format("{0,2:X2}", tmpSrcAsc));

                offset = tmpSrcAsc;
                srcPos++;
                keyPos++;
                if (keyPos >= key_len)
                    keyPos = 0;
            }
            return rt.ToString();
        }

        public static string Dencrypt(string _text, string _key)
        {
            if (1 >= _text.Trim().Length) return "";

            int key_len = _key.Length;
            char[] key = null;

            if (0 == key_len)
                key = "Think Space".ToCharArray();
            else
                key = _key.ToCharArray();

            char[] txt = _text.ToCharArray();
            int txt_len = txt.Length;
            StringBuilder rt = new StringBuilder();

            int keyPos = 0;
            int srcPos = 0;
            int srcAsc = 0;
            int tmpSrcAsc = 0;

            int hex = int.Parse(string.Format("{0}{1}", txt[0], txt[1]),
                                System.Globalization.NumberStyles.AllowHexSpecifier);
            srcPos = 2;
            do
            {
                srcAsc = int.Parse(string.Format("{0}{1}", txt[srcPos++], txt[srcPos]),
                                   System.Globalization.NumberStyles.AllowHexSpecifier);
                if (keyPos >= key_len)
                    keyPos = 0;

                tmpSrcAsc = srcAsc ^ ((int)(key[keyPos]));

                if (tmpSrcAsc <= hex)
                    tmpSrcAsc = 255 + tmpSrcAsc - hex;
                else
                    tmpSrcAsc = tmpSrcAsc - hex;

                rt.Append((char)tmpSrcAsc);

                hex = srcAsc;
                srcPos++;
                keyPos++;
            }
            while (srcPos < txt_len);

            return rt.ToString();
        }
    }

    public class Utils
    {
        /// <summary>
        /// 判断字符串是否是数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumeric(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }

        /// <summary>
        /// 判断字符串是否是整型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInt(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*$");
        }

        /// <summary>
        /// 定时执行任务
        /// </summary>
        /// <param name="interval">定时时长(毫秒)</param>
        /// <param name="action">执行任务(使用委托)</param>
        public static void setTimeout(double interval, Action action) 
        { 
            System.Timers.Timer timer = new System.Timers.Timer(interval); 
            timer.Elapsed += delegate(object sender, System.Timers.ElapsedEventArgs e) 
            { 
                timer.Enabled = false; 
                action(); 
            }; 
            timer.Enabled = true; 
        }

        /// <summary>
        /// 获取出生日期对应的年龄
        /// </summary>
        /// <param name="_birthday"></param>
        /// <param name="month">输出年龄月份数</param>
        /// <returns></returns>
        public static string getAge(DateTime _birthday, out int month)
        {
            month = 0;
            int year = DateTime.Now.Year - _birthday.Year;
            if (200 < year)
                return "-";

            if (3 <= year)
            {
                month = year * 12;
                return year.ToString() + "岁";
            }

            month = DateTime.Now.Month - _birthday.Month;
            month = year * 12 + month;
            if (DateTime.Now.Day - _birthday.Day < -15)
                month--;
            else if (DateTime.Now.Day - _birthday.Day > 14)
                month++;

            if (2 > month)
            {
                TimeSpan age = DateTime.Now.Subtract(_birthday);
                return age.TotalDays.ToString() + "天";
            }
            else if (24 > month)
                return month.ToString() + "月";
            else
                return year.ToString() + "岁";
        }

        public static string getAge(DateTime _birthday)
        {
            int month = 0;
            return getAge(_birthday, out month);
        }

        /// <summary>
        /// 获取年龄
        /// </summary>
        /// <param name="_birthday"></param>
        /// <returns></returns>
        public static string getAgeStr(DateTime _birthday)
        {
            int month = 0;
            return getAge(_birthday, out month);
        }

        /// <summary>
        /// 去掉小数点后面多余的0
        /// </summary>
        /// <param name="_txt"></param>
        /// <returns></returns>
        public static string trimZero(string _txt)
        {
            if (string.IsNullOrWhiteSpace(_txt))
                return "";

            if (!_txt.Contains("."))
                return _txt;

            int p = _txt.IndexOf('.');
            int i = 0;
            for (i = _txt.Length - 1; i > p; i--)
                if ('0' != _txt[i])
                    break;

            if (i == p)
                return _txt.Substring(0, p);
            else
                return _txt.Substring(0, i + 1);
        }


        /// <summary>
        /// 布尔转1/0
        /// </summary>
        public static int bool2Bit(bool _bl)
        {
            return _bl ? 1 : 0;
        }
    }

    /// <summary>
    /// 绑定编码和名称类， 只显示名称
    /// </summary>
    public class CodeNameItem
    {
        public CodeNameItem(string _code, string _name)
        {
            Code = _code;
            Name = _name;
        }

        public string Code { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// 根据编码查找
        /// </summary>
        /// <param name="_lst">查找列表</param>
        /// <param name="_code">要查找的编码</param>
        /// <returns></returns>
        public static int FindItemByCode(IList _lst, string _code)
        {
            if ((null == _lst) || (0 == _lst.Count) || (null == _code))
                return -1;

            for (int i = _lst.Count - 1; i >= 0; i -- )
            {
                if ((_lst[i] is CodeNameItem) && _code.Equals(((CodeNameItem)_lst[i]).Code))
                    return i;
            }

            return -1;
        }
    }

    public class IDCodeItem
    {
        public IDCodeItem(int _id, string _code)
        {
            ID = _id;
            Code = _code;
        }
        public int ID { get; set; }
        public string Code { get; set; }
    }

    /// <summary>
    /// 绑定标识、编码和名称类，只显示名称
    /// </summary>
    public class IDCodeNameItem : CodeNameItem
    {
        public IDCodeNameItem(string _id, string _code, string _name): base(_code, _name)
        {
            ID = _id; 
        }

        public string ID { get; set; }

        /// <summary>
        /// 根据标识查找
        /// </summary>
        /// <param name="_lst">查找列表</param>
        /// <param name="_id">要查找的标识</param>
        /// <returns></returns>
        public static int FindItemByID(IList _lst, string _id)
        {
            if ((null == _lst) || (0 == _lst.Count) || (null == _id))
                return -1;

            for (int i = _lst.Count - 1; i >= 0; i--)
            {
                if ((_lst[i] is IDCodeNameItem) && _id.Equals(((IDCodeNameItem)_lst[i]).ID))
                    return i;
            }

            return -1;
        }
    }
}
