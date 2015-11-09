using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsV1.Module.BusinessObjects
{
    public class PersonDetail : BaseObject
    {
        public PersonDetail(Session session)
            : base(session)
        {

        }

        private string detailProperty;
        public string DetailProperty
        {
            get { return detailProperty; }
            set { SetPropertyValue("DetailProperty", ref detailProperty, value); }
        }
    }
}
