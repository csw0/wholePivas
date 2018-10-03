using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace recipemonitorlist
{

    /// <summary>
    /// 显示与取反的布尔型关联 
    /// </summary>
    public class NotBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((value is Boolean) && (true == (bool)value))
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((value is Visibility) && (Visibility.Visible == (Visibility)value))
                return false;
            else
                return true;
        }
    }

    /// <summary>
    /// 时间转为字符串,0时间时为-
    /// </summary>
    public class ConverterDateTime2Str : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is DateTime)
            {
                if (DateTime.MinValue >= (DateTime)value)
                    return "-";
                else
                    return ((DateTime)value).ToString("yyyy-M-d H:mm:ss");
            }
            else
                return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    /// <summary>
    /// 医嘱状态
    /// </summary>
    public class OrdersStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((null == value) || !(value is int))
                return "<未知>";
            else switch ((int)value)
                {
                    case 3:
                    case 5: return "不合格";
                    case 0: return "合格";
                    case -1: return "未审";
                    default: return "<未知>";
                }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return 0;
        }
    }

    /// <summary>
    /// 清除小数点后面多余的0
    /// </summary>
    public class ClearZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (null == value)
                return "";
            else
                return BLPublic.Utils.trimZero(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return 0;
        }
    }

    /// <summary>
    /// TPN项目检查结果颜色
    /// </summary>
    public class TPNItemChkColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((null == value) || !(value is string))
                return new System.Windows.Media.SolidColorBrush(SystemColors.WindowTextColor);
            else if ("不合格".Equals(value) || "*".Equals(value))
                return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
            else if ("合格".Equals(value))
                return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green);
            else
                return new System.Windows.Media.SolidColorBrush(SystemColors.WindowTextColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "";
        }
    }
}
