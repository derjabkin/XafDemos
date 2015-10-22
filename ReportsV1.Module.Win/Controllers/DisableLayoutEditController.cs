using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win.Layout;
using DevExpress.Xpo;
using ReportsV1.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsV1.Module.Win.Controllers
{
    public class DisableLayoutEditController : ViewController<DetailView>
    {
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            XafLayoutControl layout = (XafLayoutControl)View.Control;
            layout.AllowCustomization = false;

            
        }
    }
}
