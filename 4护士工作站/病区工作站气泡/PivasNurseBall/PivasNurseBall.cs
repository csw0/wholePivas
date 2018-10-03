using System;
using System.Windows.Forms;
using System.Data;
using PIVAsCommon.Helper;

namespace PivasNurseBall
{
    public class PivasNurseBall
    {
       
      private  string[,] fn = new string[3, 4];//第3行为按钮的名字，空则按钮不显示，非空按钮显示
      
      public string[,] GetInfor(string wardCode)
        {
            string sql = "exec QfInfoSelect '" + wardCode + "' ,0 ,'','',''";
            DB_Help db = new DB_Help();
            DataSet ds = db.GetPIVAsDB(sql);
            try
            {
                if (ds == null || ds.Tables[0].Rows.Count <= 0)
                {
                    fn[0, 0] = "欠费病人";
                    fn[0, 1] = "欠费瓶签数";
                    fn[0, 2] = "";
                    fn[0, 3] = "";
                    fn[1, 0] = "0";
                    fn[1, 1] = "0";
                    fn[1, 2] = "";
                    fn[1, 3] = "";
                    fn[2, 0] = "查询";
                    fn[2, 1] = "";
                    fn[2, 2] = "";
                    fn[2, 3] = "";
                }
                else
                {
                    fn[0, 0] = ds.Tables[0].Rows[0][0].ToString();
                    fn[0, 1] = ds.Tables[0].Rows[0][1].ToString();
                    fn[0, 2] = ds.Tables[0].Rows[0][2].ToString();
                    fn[0, 3] = ds.Tables[0].Rows[0][3].ToString();
                    fn[1, 0] = ds.Tables[0].Rows[0][4].ToString();
                    fn[1, 1] = ds.Tables[0].Rows[0][5].ToString();
                    fn[1, 2] = ds.Tables[0].Rows[0][6].ToString();
                    fn[1, 3] = ds.Tables[0].Rows[0][7].ToString();
                    fn[2, 0] = ds.Tables[0].Rows[0][8].ToString();
                    fn[2, 1] = ds.Tables[0].Rows[0][9].ToString();
                    fn[2, 2] = ds.Tables[0].Rows[0][10].ToString();
                    fn[2, 3] = ds.Tables[0].Rows[0][11].ToString();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           


            return fn;
        }
    /// <summary>
    /// 护士站气泡第一行按钮调用
    /// </summary>
    /// <param name="string1"></param>
    /// <param name="string2"></param>
    /// <param name="string3"></param>
    /// <param name="string4"></param>
    /// <param name="string5"></param>
    /// <param name="?"></param>
      public void Extra1(string string1,string string2,string string3,string string4,string string5,string string6) 
      {
          QfInFoByWard qf=new QfInFoByWard (@string1 );
          qf.Show();

      
      }
         /// <summary>
    /// 护士站气泡第二行按钮调用
    /// </summary>
    /// <param name="string1"></param>
    /// <param name="string2"></param>
    /// <param name="string3"></param>
    /// <param name="string4"></param>
    /// <param name="string5"></param>
    /// <param name="?"></param>
      public void Extra2(string string1,string string2,string string3,string string4,string string5,string string6) 
      {
        MessageBox .Show ("!");
      
      }
         /// <summary>
    /// 护士站气泡第三行按钮调用
    /// </summary>
    /// <param name="string1"></param>
    /// <param name="string2"></param>
    /// <param name="string3"></param>
    /// <param name="string4"></param>
    /// <param name="string5"></param>
    /// <param name="?"></param>
      public void Extra3(string string1,string string2,string string3,string string4,string string5,string string6) 
      {
        
      MessageBox .Show ("!");
      }
         /// <summary>
    /// 护士站气泡第四行按钮调用
    /// </summary>
    /// <param name="string1"></param>
    /// <param name="string2"></param>
    /// <param name="string3"></param>
    /// <param name="string4"></param>
    /// <param name="string5"></param>
    /// <param name="?"></param>
      public void Extra4(string string1,string string2,string string3,string string4,string string5,string string6) 
      {
        MessageBox .Show ("!");
      
      }

      /// <summary>
      /// 护士站第一个额外按钮调用
      /// </summary>
      /// <param name="string1">病区code</param>
      /// <param name="string2">病区name</param>
      /// <param name="string3">员工id</param>
      /// <param name="string4"></param>
      /// <param name="string5"></param>
      /// <param name="string6"></param>
      public void Btn1(string string1, string string2, string string3, string string4, string string5, string string6)
      {
          //MessageBox.Show("aaa bbb ccc");
          QfInFoByWard qf = new QfInFoByWard(@string1);
          qf.Show();
      }
      /// <summary>
      /// 护士站第二个额外按钮调用
      /// </summary>
      /// <param name="string1">病区code</param>
      /// <param name="string2">病区name</param>
      /// <param name="string3">员工id</param>
      /// <param name="string4"></param>
      /// <param name="string5"></param>
      /// <param name="string6"></param>
      public void Btn2(string string1, string string2, string string3, string string4, string string5, string string6)
      {
          QfInFoByWard qf = new QfInFoByWard(@string1);
          qf.Show();

      }
      /// <summary>
      /// 护士站第三个额外按钮调用
      /// </summary>
      /// <param name="string1">病区code</param>
      /// <param name="string2">病区name</param>
      /// <param name="string3">员工id</param>
      /// <param name="string4"></param>
      /// <param name="string5"></param>
      /// <param name="string6"></param>
      public void Btn3(string string1, string string2, string string3, string string4, string string5, string string6)
      {
          QfInFoByWard qf = new QfInFoByWard(@string1);
          qf.Show();
      }
    }
}
