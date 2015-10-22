using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;

namespace UnitTests
{
    public abstract class XpoTestBase
    {

        private SimpleDataLayer dataLayer;

        protected SimpleDataLayer DataLayer
        {
            get
            {
                if (dataLayer == null)
                    dataLayer = CreateDataLayer();

                return dataLayer;
            }
        }
        private SimpleDataLayer CreateDataLayer()
        {
            return new SimpleDataLayer(new InMemoryDataStore());
        }

        protected UnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(DataLayer);
        }

        protected virtual void InitializeTestCore()
        {

        }

      
        [TestInitialize]
        public void InitializeTest()
        {
            dataLayer = null;
            InitializeTestCore();
        }
    }
}
