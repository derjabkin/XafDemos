using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportsV1.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class PersonCommunication : BaseObject
    {
        public PersonCommunication(Session session)
            : base(session)
        {

        }

        private Person person;
        [Association]
        public Person Person
        {
            get { return person; }
            set { SetPropertyValue("Person", ref person, value); }
        }

        private string type;
        public string Type
        {
            get { return type; }
            set { SetPropertyValue("Type", ref type, value); }
        }

        private string communictationValue;
        public string CommunictionValue
        {
            get { return communictationValue; }
            set
            {
                if (SetPropertyValue("CommunictionValue", ref communictationValue, value) && !IsLoading && Person != null)
                {
                    Person.Calculate();
                }
            }
        }

    }
}
