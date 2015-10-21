using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ReportsV2.Web;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsV1.Module.Web.Controllers
{
    public class PrintHistoryController : ViewController<DetailView>
    {
        public PrintHistoryController()
        {
            TargetViewId = "ReportViewer_DetailView_V2";
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            var item = View.Items.OfType<ReportViewerDetailItem>().FirstOrDefault();
            item.ControlCreated += item_ControlCreated;
            foreach (var viewItem in View.Items)
            {

            }
        }

        void item_ControlCreated(object sender, EventArgs e)
        {
            ReportViewerDetailItem item = (ReportViewerDetailItem)sender;
            item.ReportViewer.Report.AfterPrint += Report_AfterPrint;
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

        }
        void Report_AfterPrint(object sender, EventArgs e)
        
        {
            XtraReport report = (XtraReport)sender;
        }
    }
}
