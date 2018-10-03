using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace EmployeeManage
{
    public partial class rowEmployee : UserControl
    {
        public rowEmployee()
        {
            InitializeComponent();
        }


        public static string oldECode;
        public static string oldAccount;
        public static string oldPosition;
        public static string oldEName;
        public static string oldPas;
        public static string oldIsvalid;
        public static int EID;
        public static rowEmployee RowName;
     
           
        public void add(DataRow row)
        {
            try
            {
                Account.Text = row["AccountID"].ToString();
                Position.Text = row["Position"].ToString();
                EName.Text = row["DEmployeeName"].ToString();
                ECode.Text = row["DEmployeeCode"].ToString();
                ID.Text = row["DEmployeeID"].ToString();
                Pas.Text = row["Pas"].ToString();
               // MessageBox.Show(row["IsValid"].ToString());
                if ("True"==row["IsValid"].ToString())
                {
                    IsValid.Text = "是";
                }
                else
                {
                    IsValid.Text = "否";
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void delete_Click(object sender, EventArgs e)
        {
            deleteEmployee form = new deleteEmployee();

            oldAccount = Account.Text.ToString();
            oldPosition = Position.Text.ToString();
            oldEName = EName.Text.ToString();
            oldECode = ECode.Text.ToString();

            form.ShowDialog(this);
        }

        public void addstr(string str1,string str2,string str3, string str4,string Isvalid)
        {
            Account.Text=str1;
            Position.Text=str2;
            EName.Text=str3;
            ECode.Text=str4;
            IsValid.Text= Isvalid;
        }

        private void rowEmployee_Click(object sender, EventArgs e)
        {
            updateEmployee form3 = new updateEmployee();
            
            oldAccount = Account.Text.ToString();
            oldPosition = Position.Text.ToString();
            oldEName = EName.Text.ToString();
            oldECode = ECode.Text.ToString();
            oldPas = Pas.Text.ToString();
            oldIsvalid = IsValid.Text.ToString();
            EID = Convert.ToInt32(ID.Text.ToString());
            RowName = this;

            form3.ShowDialog(this);
        }

        public void updatestr(string str1, string str2, string str3,string str4,string Isvalid)
        {
            RowName.Account.Text = str1;
            RowName.Position.Text = str2;
            RowName.EName.Text = str3;
            RowName.EName.Text = str4;
            RowName.IsValid.Text = Isvalid;
        }
             
    }
}
