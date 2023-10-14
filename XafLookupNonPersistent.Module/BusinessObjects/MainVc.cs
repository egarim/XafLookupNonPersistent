using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XafLookupNonPersistent.Module.BusinessObjects
{
    public class MainVc : ViewController
    {
        PopupWindowShowAction CustomizeCloneAction;
        PopupWindowShowAction ShowPopup;
        public MainVc() : base()
        {
            // Target required Views (use the TargetXXX properties) and create their Actions.
            ShowPopup = new PopupWindowShowAction(this, "ShowPopup", "View");
            ShowPopup.Execute += ShowPopup_Execute;
            ShowPopup.CustomizePopupWindowParams += ShowPopup_CustomizePopupWindowParams;

            CustomizeCloneAction = new PopupWindowShowAction(this, "Customize clone", "View");
            CustomizeCloneAction.Execute += CustomizeCloneAction_Execute;
            CustomizeCloneAction.CustomizePopupWindowParams += CustomizeCloneAction_CustomizePopupWindowParams;
            

        }
        private void CustomizeCloneAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var selectedPopupWindowObjects = e.PopupWindowViewSelectedObjects;
            var selectedSourceViewObjects = e.SelectedObjects;
            // Execute your business logic (https://docs.devexpress.com/eXpressAppFramework/112723/).
        }
        private void CustomizeCloneAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var Os = this.Application.CreateObjectSpace(typeof(CloneObjectData));
            var MainInstance = Os.CreateObject<CloneObjectData>();
            MainInstance.TypeInfo = this.View.ObjectTypeInfo;
            var View = Application.CreateDetailView(Os, MainInstance);
            e.View = View;
        }
        private void ShowPopup_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var selectedPopupWindowObjects = e.PopupWindowViewSelectedObjects;
            var selectedSourceViewObjects = e.SelectedObjects;
            // Execute your business logic (https://docs.devexpress.com/eXpressAppFramework/112723/).
        }
        private void ShowPopup_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var Os = this.Application.CreateObjectSpace(typeof(MainObject));
            var MainInstance = Os.CreateObject<MainObject>();
            MainInstance.TypeInfo = this.View.ObjectTypeInfo;
           

          
            var View = Application.CreateDetailView(Os, MainInstance);
            e.View = View;
            // Set the e.View parameter to a newly created view (https://docs.devexpress.com/eXpressAppFramework/112723/).
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
    }
}
