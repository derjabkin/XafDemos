using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using ReportsV1.Module.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsV1.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Person : BaseObject, IPerson
    {
        public Person(Session session)
            : base(session)
        {
        }


        //[RuleFromBoolProperty(TargetContextIDs = "Refresh")]
        public bool IsValid
        {
            get
            {
                return FirstName != null && FirstName.Length > 3;
            }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (firstName != value && !IsLoading)
                {
                    if (value == null || value.Length < 3)
                        throw new UserFriendlyException("Wert ist falsch");
                    SetPropertyValue("FirstName", ref firstName, value);
                }
            }
        }

        private string lastName;
        [ConfirmableValue]
        public string LastName
        {
            get { return lastName; }
            set { SetPropertyValue("LastName", ref lastName, value); }
        }

        public bool ShouldConfirmLastName(string value)
        {
            return value == null || value.Length < 4;
        }


        public string ServerModeTestProperty
        {
            get { return string.Format(CultureInfo.CurrentCulture, "{0} - {1}", LastName, FirstName); }

        }

        public int CalculatedField
        {
            get { return 0; }
        }
        [PersistentAlias("LastName + '-' + FirstName")]
        public string PersistentAliasProperty
        {
            get { return (string)EvaluateAlias("PersistentAliasProperty"); }
        }
        string IPerson.FullName
        {
            get { return string.Format(CultureInfo.CurrentCulture, "{0} - {1}", LastName, FirstName); }
        }

    }

}
