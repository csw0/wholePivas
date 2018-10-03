using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PrePlugIn
{
    public class PreAddin
    {
        public PreAddin()
        {

        }

        public void Run(string GroupNo)
        {
            MessageBox.Show(GroupNo);
        }
    }
}
