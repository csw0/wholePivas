using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BLPublic
{
    /// <summary>
    /// IDataReader记录集操作, 主要判断字段是否为NULL和字段类型
    /// </summary>
    public class BLDataReader
    {
        private IDataReader dr = null;
        private Dictionary<string, int> Fields = null;
        private int fieldIndx = -1;

        public BLDataReader(IDataReader _dr)
        {
            this.dr = _dr;
            initField();
        }

        /// <summary>
        /// 下一条记录(默认在-1，读取数据之前都必须先调用此方法)
        /// </summary>
        /// <returns></returns>
        public bool next()
        {
            return dr.Read();
        }

        /// <summary>
        /// 判断字段内容是否为NULL
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public bool isNull(string _name)
        {
            setField(_name); 
            return this.dr.IsDBNull(fieldIndx);
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public string getString(string _name)
        {
            setField(_name);
             
            if (this.dr.IsDBNull(fieldIndx))
                return "";
            else
                return this.dr.GetValue(fieldIndx).ToString(); //.GetString(fieldIndx)只有为字符字段时才能使用
        }

        /// <summary>
        /// 获取整型数据
        /// </summary>
        /// <param name="_name">字段名</param>
        /// <returns></returns>
        public int getInt(string _name)
        {
            setField(_name);
            return (int)this.getLong(_name); 
        }

        /// <summary>
        /// 获取长整型数据
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public long getLong(string _name)
        {
            setField(_name);

            if (this.dr.IsDBNull(fieldIndx))
                return 0;
            else if (typeof(Int16) == this.dr.GetFieldType(fieldIndx))
                return this.dr.GetInt16(fieldIndx);
            else if (typeof(Int32) == this.dr.GetFieldType(fieldIndx))
                return this.dr.GetInt32(fieldIndx);
            else if (typeof(Int64) == this.dr.GetFieldType(fieldIndx))
                return this.dr.GetInt64(fieldIndx);
            else if (typeof(string) == this.dr.GetFieldType(fieldIndx))
            {
                try
                { 
                    return Convert.ToInt32(this.dr.GetString(fieldIndx));
                }
                catch(Exception ex)
                { }
            }
            
            return 0;
        }

        /// <summary>
        /// 获取浮点型数据
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public double getFloat(string _name)
        {
            setField(_name);

            if (this.dr.IsDBNull(fieldIndx))
                return 0.0;
            else if (typeof(Double) == this.dr.GetFieldType(fieldIndx))
                return this.dr.GetDouble(fieldIndx);
            else if (typeof(Single) == this.dr.GetFieldType(fieldIndx))
                return this.dr.GetFloat(fieldIndx);
            else
                return Convert.ToDouble(this.dr.GetValue(fieldIndx).ToString());
        }

        /// <summary>
        /// 获取布尔类型数据
        /// </summary>
        /// <param name="_name">字段名</param>
        /// <returns></returns>
        public bool getBool(string _name)
        {
            setField(_name);

            if (this.dr.IsDBNull(fieldIndx))
                return false;
            else
                return this.dr.GetBoolean(fieldIndx);
        }

        /// <summary>
        /// 获取时间
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public DateTime getDateTime(string _name)
        {
            setField(_name);

            if (this.dr.IsDBNull(fieldIndx))
                return DateTime.MinValue;
            else
                return this.dr.GetDateTime(fieldIndx);
        }

        /// <summary>
        /// 关闭记录集
        /// </summary>
        public void close()
        {
            if (null != this.dr)
                this.dr.Close();

            this.dr = null;

            if (null != this.Fields)
                this.Fields.Clear();
        }

        private void setField(string _name)
        { 
            if (!this.Fields.ContainsKey(_name))
                throw new Exception("字段" + _name + "不在记录中");

            fieldIndx = this.Fields[_name];
        }

        private void initField()
        {
            this.Fields = new Dictionary<string, int>();

            if (null == this.dr)
                return;

            for (int i = this.dr.FieldCount - 1; i >= 0; i--)
                if (!this.Fields.ContainsKey(this.dr.GetName(i)))
                    this.Fields.Add(this.dr.GetName(i), i);
        }
    } 
}
