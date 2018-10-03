using System;
using System.IO;

namespace PivasWebServiceInterface
{
    public class WriteLog
    {
          public void writeLog(string fileName, string content)
        {
            String p = System.AppDomain.CurrentDomain.BaseDirectory;
            string path = p + @"\log\" + DateTime.Now.ToString("yyyy-MM-dd");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string FILE_NAME = path + @"\" + fileName + ".log";//按日期建立不同的文件

            StreamWriter sw;
            try
            {
                if (File.Exists(FILE_NAME))
                {
                    sw = File.AppendText(FILE_NAME);
                }
                else
                {
                    sw = File.CreateText(FILE_NAME);
                }
                content = "【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "】 " + content + "\r\n";
                sw.WriteLine(content);
                sw.Close();
            }
            catch
            { }
        }
    }

    }
