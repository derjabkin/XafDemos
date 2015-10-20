using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsV1.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Person : BaseObject
    {
        public Person(Session session) : base(session)
        {
            
        }


        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { SetPropertyValue("FirstName", ref firstName, value); }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { SetPropertyValue("LastName", ref lastName, value); }
        }
    }

}
