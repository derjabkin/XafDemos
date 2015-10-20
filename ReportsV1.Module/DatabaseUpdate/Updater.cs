using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.XtraReports.UI;
using System.IO;
using DevExpress.Persistent.Base.ReportsV2;

namespace ReportsV1.Module.DatabaseUpdate
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppUpdatingModuleUpdatertopic.aspx
    public class Updater : ModuleUpdater
    {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion)
        {
        }
        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();

            var oldReports = ObjectSpace.GetObjects<ReportData>();

            XPObjectSpace os = (XPObjectSpace)ObjectSpace;

            foreach (var oldReport in oldReports)
            {
                string dataTypeName = oldReport.DataTypeName;
                var typeInfo = os.TypesInfo.FindTypeInfo(dataTypeName);
                if (typeInfo != null)
                {
                    var report = new ReportDataV2(os.Session, typeInfo.Type)
                    {
                        DisplayName = oldReport.ReportName,
                        Content = AddCollectionDataSource(oldReport.Content, dataTypeName)
                    };
                }
            }
            ObjectSpace.CommitChanges();
        }

        private byte[] AddCollectionDataSource(byte[] content, string typeName)
        {
            XtraReport report = new XtraReport();


            using (MemoryStream stream = new MemoryStream(content))
            {
                report.LoadLayout(stream);
            }

            report.DataSource = new CollectionDataSource() { ObjectTypeName = typeName, Name = "collectionDataSource" };

            using (MemoryStream stream = new MemoryStream())
            {
                report.SaveLayout(stream);
                return stream.ToArray();
            }
        }

        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            base.UpdateDatabaseBeforeUpdateSchema();
        }
    }
}
