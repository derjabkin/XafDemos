using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ReportsV2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsV1.Module.BusinessObjects
{
    public class PersonReportParametersObject : ReportParametersObjectBase
    {
        const string theUltimativeUserName = "Admin";

        private readonly IObjectSpaceCreator provider;
        public PersonReportParametersObject(IObjectSpaceCreator provider)
            : base(provider)
        {
            this.provider = provider;

            using (var objectSpace = provider.CreateObjectSpace(typeof(Person)))
            {
                var p = FindParameters(objectSpace);

                if (p != null)
                    Name = p.Name;

            }
        }

        private static PersonSearchParameters FindParameters(DevExpress.ExpressApp.IObjectSpace objectSpace)
        {
            var p = objectSpace.FindObject<PersonSearchParameters>(
                new BinaryOperator("UserName", theUltimativeUserName));
            return p;
        }


        public string Name { get; set; }

        protected override DevExpress.ExpressApp.IObjectSpace CreateObjectSpace()
        {
            return provider.CreateObjectSpace(typeof(Person));
        }

        private void SaveCriteria()
        {
            using(var objectSpace = CreateObjectSpace())
            {
                var p = FindParameters(objectSpace);
                if (p == null)
                {
                    p = objectSpace.CreateObject<PersonSearchParameters>();
                    p.UserName = theUltimativeUserName;
                }

                p.Name = Name;
                objectSpace.CommitChanges();
            }
        }
        public override DevExpress.Data.Filtering.CriteriaOperator GetCriteria()
        {
            //CriteriaOperator.Parse("FirstName" LIKE ? OR LastName LIKE ?")
            SaveCriteria();
            string name = Name + '%';
            return CriteriaOperator.Or(
                new BinaryOperator("FirstName", name, BinaryOperatorType.Like),
                new BinaryOperator("LastName", name, BinaryOperatorType.Like));
        }

        public override DevExpress.Xpo.SortProperty[] GetSorting()
        {
            return null;
        }
    }
}
