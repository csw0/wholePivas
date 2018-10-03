using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLPublic
{
    class DefaultValue4DBNull
    {
        public static object DefaultValue(Type fieldType)
        {
            // 针对DB中不同类型数据的NULL做默认值
            string typeName = fieldType.ToString();

            switch (typeName)
            {
                // Oracle的数字类型（number、Integer），默认值0
                case "System.Decimal":
                    return new decimal(0);

                // 浮点类型（Float和Double）
                case "System.Double":
                    return 0D;

                // SQL Server的数字类型，默认值0
                case "System.Int32":
                    return 0;

                case "System.Int64":
                    return 0L;

                // 字符串类型（各种char、clob），默认值空白字符串""
                case "System.String":
                    return "";

                // 二进制数组（blob），默认值空数组，new byte[0]
                case "System.Byte[]":
                    return new byte[0];

                // 日期（date），默认最小值(0001-01-01)
                case "System.DateTime":
                    return DateTime.MinValue;

                // 没有处理的类型，抛出异常，显示类型名称，方便测试修改
                default:
                    string errorMessage = "对于DB中的空值，找不到对应的数据类型"
                                        + "数据类型：" + typeName;
                    throw new Exception(errorMessage);
            }
        }
    }
}
