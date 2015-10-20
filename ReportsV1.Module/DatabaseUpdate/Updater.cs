using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;

namespace ReportsV1.Module.DatabaseUpdate {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppUpdatingModuleUpdatertopic.aspx
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion) {
        }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();

            var oldReports = ObjectSpace.GetObjects<ReportData>();

            XPObjectSpace os = (XPObjectSpace)ObjectSpace;

            foreach(var oldReport in oldReports)
            {
                var report = new ReportDataV2(os.Session, os.TypesInfo.FindTypeInfo(oldReport.DataTypeName).Type);
                report.DisplayName = oldReport.ReportName;
                report.Content = oldReport.Content;
            }
            ObjectSpace.CommitChanges();
        }
        public override void UpdateDatabaseBeforeUpdateSchema() {
            base.UpdateDatabaseBeforeUpdateSchema();
        }
    }
}
