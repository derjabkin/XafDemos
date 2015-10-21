using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsV1.Module.NonPersistent
{
    public class ReportsObjectSpace : NonPersistentObjectSpace
    {
        private IObjectSpace innerObjectSpace;
        public ReportsObjectSpace(ITypesInfo typesInfo, IEntityStore entityStore, IObjectSpace innerObjectSpace) 
            : base(typesInfo, entityStore)
        {
            ObjectsGetting += ReportsObjectSpace_ObjectsGetting;
            this.innerObjectSpace = innerObjectSpace;
        }

        void ReportsObjectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
        }

        public override void Dispose()
        {
            base.Dispose();
            if (innerObjectSpace != null)
            {
                innerObjectSpace.Dispose();
                innerObjectSpace = null;
            }
        }

    }
}
