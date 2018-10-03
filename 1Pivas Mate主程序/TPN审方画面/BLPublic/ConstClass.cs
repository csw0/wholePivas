using System;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Collections;

namespace BLPublic
{
    public class CheckType
    {
        public static bool IsFloat(string txt)
        {
            try {
                double.Parse(txt);
                return true;
            }
            catch {
                return false;
            }
        }

        public static bool IsInt(string txt)
        {
            try {
                int.Parse(txt);
                return true;
            }
            catch {
                return false;
            }
        }

        public static bool IsDateTime(string txt)
        {
            try {
                DateTime.Parse(txt);
                return true;
            }
            catch {
                return false;
            }
        }

    }

    public class CodeNameObj
    {
        private string code = null;
        private string name = null;

        public CodeNameObj(string ACode, string AName)
        {
            this.code = ACode;
            this.name = AName;
        }

        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public override string ToString()
        {
            return this.name;
        }
    }

    public class FileLog
    {
        private string file_name = null;
        private string file_error = null;
        private FileStream file_strm = null;
        private StreamWriter file_wrt = null;

        ~FileLog()
        {
            try
            {
                if (null != this.file_strm)
                {
                    this.file_strm.Close();
                    this.file_strm = null;
                }

                if (null != this.file_wrt)
                {
                    this.file_wrt.Close();
                    this.file_wrt = null;
                }
            }
            catch
            {
                //
            }
        }

        public bool Init(string _file_name)
        {
            this.file_name = _file_name;
            try
            {
                FileInfo file_info = new FileInfo(this.file_name);

                if (!file_info.Exists)
                    this.file_strm = file_info.Create();
                else
                {
                    this.file_strm = file_info.OpenWrite();
                    this.file_strm.Seek(0, SeekOrigin.End);
                }
                file_info = null;

                this.file_wrt = new StreamWriter(this.file_strm, Encoding.Unicode);
            }
            catch(Exception ex)
            {
                this.file_error = "打开日志文件失败:" + ex.Message;
                return false;
            }
            return true;
        }

        public bool writeLog(string _txt)
        {
            if (null != this.file_wrt)
            try
            {
                this.file_wrt.WriteLine(DateTime.Now.ToString("[yyyy-M-d H:mm:ss]") + _txt);
                this.file_wrt.Flush();
            }
            catch (Exception ex)
            {
                this.file_error = "写日志失败:" + ex.Message;
                return false;
            }
            return true;
        }
    }

    //ListView 列排序
    public class ListViewItemComparer : System.Collections.IComparer
    {
        public int column;
        public SortOrder order;

        public ListViewItemComparer(int _column, SortOrder _so)
        {
            this.order = _so;
            this.column = _column;
        }

        public ListViewItemComparer(int _column)
        {
            this.order = SortOrder.Ascending;
            this.column = _column;
        }

        public int Compare(object x, object y)
        {
            if (this.order == SortOrder.Descending)
                return String.Compare(((ListViewItem)y).SubItems[column].Text, ((ListViewItem)x).SubItems[column].Text);
            else
                return String.Compare(((ListViewItem)x).SubItems[column].Text, ((ListViewItem)y).SubItems[column].Text);
        }
    }
    
}
