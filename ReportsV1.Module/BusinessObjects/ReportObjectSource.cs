using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsV1.Module.BusinessObjects
{
    [NonPersistent]
    public class ReportObjectSource : BaseObject
    {
        public ReportObjectSource(Session session) : base(session)
        {

        }

        public string Data { get { return "Testdaten"; } }
    }
}
