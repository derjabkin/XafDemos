using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsV1.Module.NonPersistent
{
    public class ReportsObjectSpaceProvider : NonPersistentObjectSpaceProvider, IObjectSpaceProvider
    {
        private readonly ITypesInfo typesInfo;
        private readonly NonPersistentEntityStore entityStore;

        public ReportsObjectSpaceProvider(ITypesInfo typesInfo, NonPersistentEntityStore nonPersistentEntityStore)
             : base(typesInfo, nonPersistentEntityStore)
        {
            this.typesInfo = typesInfo;
            this.entityStore = nonPersistentEntityStore;
        }

        IObjectSpace IObjectSpaceProvider.CreateObjectSpace()
        {
            return new ReportsObjectSpace(typesInfo, entityStore, null);
        }


    }
}
