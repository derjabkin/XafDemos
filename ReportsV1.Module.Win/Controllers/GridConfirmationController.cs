using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsV1.Module.Win.Controllers
{
    public class GridConfirmationController : ViewController<ListView>
    {
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
             GridListEditor editor = View.Editor as GridListEditor;
             if (editor != null)
             {
                 editor.GridView.ShownEditor += GridView_ShownEditor;
             }
        }

        void GridView_ShownEditor(object sender, EventArgs e)
        {
            GridView gridView = (GridView)sender;
            gridView.ActiveEditor.Validating += ActiveEditor_Validating;
        }

        void ActiveEditor_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GridListEditor editor = (GridListEditor)View.Editor;
            var obj = editor.GridView.GetFocusedRow();
            var memberInfo = View.ObjectTypeInfo.FindMember(editor.GridView.FocusedColumn.FieldName);
            ConfirmationController.ConfirmValueEdit(e, (BaseEdit)sender, memberInfo, obj);
        }

    }
}
