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
using System.Collections.Generic;

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

            List<ReportData> reportsToDelete = new List<ReportData>();
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

                reportsToDelete.Add(oldReport);
                
            }

            foreach (var r in reportsToDelete)
                ObjectSpace.Delete(r);
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

        private static void ExchangeDataBindings(XtraReport report)
        {
            foreach (Band band in report.Bands)
            {
                foreach (var control in band.Controls)
                {
                    XRLabel label = control as XRLabel;
                    if (label != null)
                    {
                        // [FristName]   -  [LastName]
                        label.Visible = false;
                        var binding = label.DataBindings.FirstOrDefault();
                        if (binding != null)
                        {
                            if (binding.DataMember == "FirstName")
                            {
                                label.DataBindings.Clear();
                                label.DataBindings.Add("Text", report.DataSource, "FirstName1");
                            }
                        }
                    }
                }
            }
        }

        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            base.UpdateDatabaseBeforeUpdateSchema();
        }
    }
}
