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
        private readonly IObjectSpaceCreator provider;
        public PersonReportParametersObject(IObjectSpaceCreator provider)
            : base(provider)
        {
            this.provider = provider;
        }

        public string Name { get; set; }

        protected override DevExpress.ExpressApp.IObjectSpace CreateObjectSpace()
        {
            return provider.CreateObjectSpace(typeof(Person));
        }

        public override DevExpress.Data.Filtering.CriteriaOperator GetCriteria()
        {
            //CriteriaOperator.Parse("FirstName" LIKE ? OR LastName LIKE ?")

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
