using System;
using System.Text;
using System.Linq;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.DomainLogics;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.ReportsV2;

namespace ReportsV1.Module
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppModuleBasetopic.aspx.
    public sealed partial class ReportsV1Module : ModuleBase
    {
        public ReportsV1Module()
        {
            InitializeComponent();
            BaseObject.OidInitializationMode = OidInitializationMode.AfterConstruction;
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
        {
            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
            var predefinedReportsUpdater = new PredefinedReportsUpdater(Application, objectSpace, versionFromDB);
            string reportName = Application.Model.Options.Application.Title;
            predefinedReportsUpdater.AddPredefinedReport<Reports.PersonPredefinedReport>(reportName, 
                typeof(Person));
            return new ModuleUpdater[] { updater, predefinedReportsUpdater };

        }
        public override void Setup(XafApplication application)
        {
            base.Setup(application);
            AdditionalExportedTypes.Add(typeof(ReportData));
            // Manage various aspects of the application UI and behavior at the module level.
        }
        public override void CustomizeTypesInfo(ITypesInfo typesInfo)
        {
            base.CustomizeTypesInfo(typesInfo);
            CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo);
        }

        //protected override IEnumerable<Type> GetDeclaredExportedTypes()
        //{
        //    return base.GetDeclaredExportedTypes().Concat(new[] { typeof(ReportData) });
        //}
    }
}
