using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Win.SystemModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsV1.Module.Win
{
    public class CancelDeleteController : ViewController
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            DeleteObjectsViewController deleteController = Frame.GetController<DeleteObjectsViewController>();
            deleteController.DeleteAction.Executing += DeleteAction_Executing;
            deleteController.DeleteAction.ConfirmationMessage = string.Empty;

            WinModificationsController modificationsController = Frame.GetController<WinModificationsController>();
            modificationsController.ModificationsHandlingMode = ModificationsHandlingMode.AutoCommit;
        }

        void DeleteAction_Executing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
    }
}
