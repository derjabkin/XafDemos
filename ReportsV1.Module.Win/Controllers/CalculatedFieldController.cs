using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraGrid.Columns;
using ReportsV1.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsV1.Module.Win.Controllers
{
    public class CalculatedFieldController : ViewController<ListView>
    {
        private readonly ParametrizedAction unboundParameterAction;
        public CalculatedFieldController()
        {
            TargetObjectType = typeof(Person);
            unboundParameterAction = new ParametrizedAction(this, "UnboundParameterAction", DevExpress.Persistent.Base.PredefinedCategory.View, typeof(int))
            {
                Caption = "View Parameter"
            };

            RegisterActions(unboundParameterAction);
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            GridListEditor editor = View.Editor as GridListEditor;
            if (editor != null)
            {

                var column = new GridColumn()
                {
                    Visible = true,
                    UnboundType = DevExpress.Data.UnboundColumnType.Integer,
                    FieldName = "Unbound",
                    Name = "Unbound"
                };
                editor.GridView.Columns.Add(column);
                editor.GridView.CustomUnboundColumnData += GridView_CustomUnboundColumnData;
            }
        }

        void GridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                e.Value = 22 + (unboundParameterAction.Value as int? ?? 0);
            }
        }
    }
}
