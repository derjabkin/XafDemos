using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors;
using ReportsV1.Module.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportsV1.Module.Win.Controllers
{
    public class ConfirmationController : ViewController<DetailView>
    {
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            foreach (var viewItem in View.Items)
            {
                var editor = viewItem as DXPropertyEditor;
                if (editor != null)
                {
                    editor.Control.Validating += Control_Validating;
                }
            }
        }

        private IMemberInfo GetConfirmableMember(BaseEdit edit)
        {
            var editor = View.Items.OfType<DXPropertyEditor>().FirstOrDefault(c => c.Control == edit);
            if (editor != null && editor.MemberInfo.FindAttribute<ConfirmableValueAttribute>() != null)
                return editor.MemberInfo;
            else
                return null;
        }

        void Control_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

            var edit = (BaseEdit)sender;
            var member = GetConfirmableMember(edit);
            ConfirmValueEdit(e, edit, member, View.CurrentObject);

        }

        internal static void ConfirmValueEdit(CancelEventArgs e, BaseEdit edit, IMemberInfo member, object obj)
        {
            if (member != null)
            {
                var type = obj.GetType();
                var methodInfo = type.GetMethod("ShouldConfirm" + member.Name, BindingFlags.Instance | BindingFlags.Public);

                if (methodInfo == null || (bool)methodInfo.Invoke(obj, new[] { edit.EditValue }))
                    e.Cancel =
                        XtraMessageBox.Show("Wollen Sie den Wert wirklich eingeben?", "Bestätigung",
                        MessageBoxButtons.YesNo) != DialogResult.Yes;
            }
        }

        void Control_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            XtraMessageBox.Show("Bestätigung");
        }
    }
}
