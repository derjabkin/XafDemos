using DevExpress.ExpressApp;
using DevExpress.Xpo;
using ReportsV1.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReportsV1.Module.Controllers
{
    public class PersonPrefetchController : ViewController<ListView>
    {
        public PersonPrefetchController()
        {
            TargetObjectType = typeof(Person);
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            CollectionSource collectionSource = View.CollectionSource as CollectionSource;
            if (collectionSource != null)
            {
                var property = collectionSource.GetType()
                    .GetProperty("OriginalCollection", BindingFlags.Instance | BindingFlags.NonPublic);

                if (property != null)
                {

                    XPCollection originalCollection = (XPCollection)property.GetValue(collectionSource, null);
                    originalCollection.PreFetch("Communication");
                }
            }
        }
    }
}
