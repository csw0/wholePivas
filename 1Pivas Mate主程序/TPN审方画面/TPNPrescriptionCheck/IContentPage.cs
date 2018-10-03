using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPNReview
{
    interface IContentPage
    {
        void init(Action _refParent);
        void setPatient(PatientModel _pnt);
        void clear();
    }
}
