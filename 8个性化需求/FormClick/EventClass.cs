using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;


namespace FormClick
{
    public class EventClass
    {
        public void NameEvent(string AccountID,string inPatient)
        {
            ProcessStartInfo pf = new ProcessStartInfo();
            
            pf.FileName = "iexplore.exe";
            pf.Arguments = "http://172.20.10.233:8001/Portal/Auth/LoginFromPC?userCode=" + AccountID + "&password=123&inPatient=" + inPatient + "";
            //  pf.UserName = "395574374@qq.com";


            //  SecureString password = new SecureString();  
            //char[] pass = "z521488768".ToCharArray();
            //for (int i = 0; i < pass.Length; i++)
            //{
            //    password.AppendChar(pass[i]);
            //}

            // pf.Password = password;
            //pf.UseShellExecute = false;
            Process.Start(pf);

           // "http://172.20.10.233:8001/Portal/Auth/LoginFromPC?userCode=admin&password=123&outPatient=25448";
            
        }

    }
}
