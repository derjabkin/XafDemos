using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win.SystemModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsV1.Module.Controllers
{
    public class CustomLockingController : ViewController
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            LockController lockController = Frame.GetController<LockController>();
            lockController.CustomProcessSimultaneousModificationsException += lockController_CustomProcessSimultaneousModificationsException;
        }

        void lockController_CustomProcessSimultaneousModificationsException(object sender, CustomProcessSimultaneousModificationsExceptionEventArgs e)
        {
            e.Handled = true;
        }
    }
}
