using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;


namespace PivasRevPre
{
    public partial class CurrentDrug : UserControl
    {
        public CurrentDrug()
        {
            InitializeComponent();
        }
        delegate void FOO(IntPtr handel, int drug, string constr, int a, int b);

       // [DllImport("libDrugManual", CallingConvention = CallingConvention.StdCall)]
        //public static extern void ShowManual1(IntPtr handel, int drug, string constr, int a, int b);

        public string UniPreID;
        DB_Help DB = new DB_Help();
        public void setDrug(DataRow R)
        {
            try
            {
                lblDrugName.Text = R["DrugName"].ToString().Trim();
                if (R["PiShi"].ToString() == "True")
                    lblPiShi.Text = "皮试";
                else
                    lblPiShi.Text = "";
                lblSpec.Text = R["Spec"].ToString();
                //label2.Text = R["FregCode"].ToString();
                label2.Text = (float.Parse(R["Dosage"].ToString())).ToString().Trim() + R["DosageUnit"].ToString().Trim();
                UniPreID = R["UniPreparationID"].ToString();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblDrugName_Click(object sender, EventArgs e)
        {
            this.Parent.Focus();
        }

        /// <summary>
        /// 调用药品说明书（20140612陆卓春修改）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblDrugName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                //this.Parent.Focus();
                ////ShowManual1(this.Handle, Convert.ToInt32(UniPreID), "Provider=SQLOLEDB.1;Persist Security Info=False;" + DB.DatebasePIVAsInfo(), -1, -1);
               
                //    int hModule = DLLWrapper.LoadLibrary("libDrugManual.dll");
                //    if (hModule == 0)
                //    {
                //        MessageBox.Show("加载药品说明书失败！");
                //        return;
                //    }
                   
                //    FOO foo = (FOO)DLLWrapper.GetFunctionAddress(hModule, "ShowManual1", typeof(FOO));
                //    if (foo == null)
                //    {
                //        MessageBox.Show("无相关药品说明书");
                //        DLLWrapper.FreeLibrary(hModule);
                //        return;
                //    }
                //    string s = "Provider=SQLOLEDB.1;Persist Security Info=False;" + DB.DatebasePIVAsInfo();
                    
                //    foo(this.Handle, Convert.ToInt32(UniPreID), s, -1, -1);
                    
                //    //MessageBox.Show("Provider=SQLOLEDB.1;" + DB.DatebasePIVAsInfo());
                //    //foo(this.Handle, Convert.ToInt32(UniPreID), "Persist Security Info=False;" + DB.DatebasePIVAsInfo(), -1, -1);
                //    DLLWrapper.FreeLibrary(hModule);

                if (string.IsNullOrEmpty(UniPreID))
                {
                    MessageBox.Show("无匹配药品,请维护", "提示");
                }
                else
                {
                    LibDrugManual.RichFrom mf = new LibDrugManual.RichFrom();
                    mf.UniPreparationID = UniPreID;
                    mf.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
    }
    
}
