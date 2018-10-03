using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.ComponentModel;


namespace TPNReview
{
    public class Converter
    {
        public static System.Windows.Visibility bool2Visibe(bool _bl)
        {
            return _bl ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }
    }

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

    public class Exists2OpConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((null == value) || string.IsNullOrWhiteSpace(value.ToString()))
                return "+";
            else
                return "-";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return 0;
        }
    }

    public class SexIsManConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((null == value) || !(value is string))
                return false;
            else if ("男".Equals(value) || "m".Equals(value))
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "";
        }
    }

    public class SexIsWomanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((null == value) || !(value is string))
                return false;
            else if ("女".Equals(value) || "f".Equals(value))
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "";
        }
    }

    public class ConverterModOrdersTypeTip : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (null == value)
                return "-";
            else if ((bool)value)
                return "修改为普通医嘱";
            else
                return "修改为营养医嘱";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return false;
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
    /// 整型和布尔型转换
    /// </summary>
    public class ConverterInt2Bool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int)
            {
                if (0 == (int)value)
                    return false;
                else
                    return true;
            }
            else if (value is bool)
                return value;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value)
                    return 1;
                else
                    return 0;
            }
            else if (value is int)
                return value;
            else
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

    /// <summary>
    /// 转换操作类型
    /// </summary>
    public class ConverterOperateType : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (null == value)
                return "";
            else if ("note".Equals(value))
                return "笔记";
            else if ("custody".Equals(value))
                return "监护";
            else if ("intervene".Equals(value))
                return "干预";
            else if ("analysis".Equals(value))
                return "评估";
            else
                return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return false;
        }
    }

    public class ListViewClick
    {
        public static void ColumnSort_Click(object sender, RoutedEventArgs e)
        {
            if (!(e.OriginalSource is GridViewColumnHeader))
                return;

            GridViewColumn clickedColumn = (e.OriginalSource as GridViewColumnHeader).Column;   //得到单击的列
            if (clickedColumn != null)
            {
                string bindingProperty = (clickedColumn.DisplayMemberBinding as Binding).Path.Path; //得到单击列所绑定的属性

                SortDescriptionCollection sdc = ((ListView)sender).Items.SortDescriptions;
                ListSortDirection sortDirection = ListSortDirection.Ascending;
                if (sdc.Count > 0)
                {
                    SortDescription sd = sdc[0];
                    sortDirection = (ListSortDirection)((((int)sd.Direction) + 1) % 2);  //判断此列当前的排序方式:升序0,倒序1,并取反进行排序。
                    sdc.Clear();
                }

                sdc.Add(new SortDescription(bindingProperty, sortDirection));
            }
        }
    }
}
