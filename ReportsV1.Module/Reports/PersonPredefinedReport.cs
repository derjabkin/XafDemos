using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace ReportsV1.Module.Reports
{
    public partial class PersonPredefinedReport : DevExpress.XtraReports.UI.XtraReport
    {
        public PersonPredefinedReport()
        {
            InitializeComponent();
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel1.BackColor = Color.Red;
        }

    }
}
