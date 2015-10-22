using System;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win;
using System.Collections.Generic;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.DC;
using ReportsV1.Module.BusinessObjects;
using DevExpress.ExpressApp.SystemModule;

namespace ReportsV1.Win {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/DevExpressExpressAppWinWinApplicationMembersTopicAll.aspx
    public partial class ReportsV1WindowsFormsApplication : WinApplication {

        private NonPersistentObjectSpaceProvider nonPersistentProvider;

        public ReportsV1WindowsFormsApplication() {
            InitializeComponent();
        }

        protected override void OnLoggedOn(LogonEventArgs args)
        {
            base.OnLoggedOn(args);
            AboutInfo.Instance.Description = "asdasddas";
        }
        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {
            args.ObjectSpaceProvider = new XPObjectSpaceProvider(args.ConnectionString, args.Connection, false);

            NonPersistentEntityStore entityStore = new NonPersistentEntityStore((TypesInfo)TypesInfo);
            entityStore.RegisterEntity(typeof(IPerson));
            nonPersistentProvider = new NonPersistentObjectSpaceProvider(TypesInfo, entityStore);
            args.ObjectSpaceProviders.Add(nonPersistentProvider);

        }
        protected override IObjectSpace CreateObjectSpaceCore(Type objectType)
        {

            if (objectType == typeof(IPerson))
            {
                var os = (NonPersistentObjectSpace)nonPersistentProvider.CreateObjectSpace();
                os.ObjectsGetting += os_ObjectsGetting;
                return os;
            }
            return base.CreateObjectSpaceCore(objectType);
        }

        void os_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            if (e.ObjectType == typeof(IPerson))
            {
                var innerOs = CreateObjectSpace(typeof(Person));
                e.Objects = innerOs.GetObjects(typeof(Person));
                e.Handled = true;

            }
        }
        private void ReportsV1WindowsFormsApplication_CustomizeLanguagesList(object sender, CustomizeLanguagesListEventArgs e) {
            string userLanguageName = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            if(userLanguageName != "en-US" && e.Languages.IndexOf(userLanguageName) == -1) {
                e.Languages.Add(userLanguageName);
            }
        }

        
        private void ReportsV1WindowsFormsApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e) {
#if EASYTEST
            e.Updater.Update();
            e.Handled = true;
#else
            if(System.Diagnostics.Debugger.IsAttached) {
                e.Updater.Update();
                e.Handled = true;
            }
            else {
                throw new InvalidOperationException(
                    "The application cannot connect to the specified database, because the latter doesn't exist or its version is older than that of the application.\r\n" +
                    "This error occurred  because the automatic database update was disabled when the application was started without debugging.\r\n" +
                    "To avoid this error, you should either start the application under Visual Studio in debug mode, or modify the " +
                    "source code of the 'DatabaseVersionMismatch' event handler to enable automatic database update, " +
                    "or manually create a database using the 'DBUpdater' tool.\r\n" +
                    "Anyway, refer to the 'Update Application and Database Versions' help topic at http://help.devexpress.com/#Xaf/CustomDocument2795 " +
                    "for more detailed information. If this doesn't help, please contact our Support Team at http://www.devexpress.com/Support/Center/");
            }
#endif
        }
    }
}
