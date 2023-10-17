using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.CloneObject;
using DevExpress.Persistent.Base;
using DevExpress.Xpo.Metadata;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XafLookupNonPersistent.Module.BusinessObjects;

namespace XafLookupNonPersistent.Module.Controllers
{
    public class MyCloner : Cloner
    {
        public override void CopyMemberValue(
            XPMemberInfo memberInfo, IXPSimpleObject sourceObject, IXPSimpleObject targetObject)
        {
            if (!memberInfo.IsAssociation)
            {
                base.CopyMemberValue(memberInfo, sourceObject, targetObject);
            }
        }
    }
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
            CustomizeCloneAction.TargetViewType = ViewType.DetailView;



        }
        private void CustomizeCloneAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var selectedPopupWindowObjects = e.PopupWindowViewSelectedObjects;
            var selectedSourceViewObjects = e.SelectedObjects;
            // Execute your business logic (https://docs.devexpress.com/eXpressAppFramework/112723/).
        }
        private void CustomizeCloneAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var Os = Application.CreateObjectSpace(typeof(CloneObjectData));
            var MainInstance = Os.CreateObject<CloneObjectData>();
            MainInstance.TypeInfo = this.View.ObjectTypeInfo;
            MainInstance.CurrentInstance = this.View.CurrentObject;
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
            var Os = Application.CreateObjectSpace(typeof(MainObject));
            var MainInstance = Os.CreateObject<MainObject>();
            MainInstance.TypeInfo = this.View.ObjectTypeInfo;



            var View = Application.CreateDetailView(Os, MainInstance);
            e.View = View;
            // Set the e.View parameter to a newly created view (https://docs.devexpress.com/eXpressAppFramework/112723/).
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            var cloneObjectController = Frame.GetController<CloneObjectViewController>();
            if (cloneObjectController != null)
            {
                cloneObjectController.CustomCloneObject += cloneObjectController_CustomCloneObject;
            }
            // Perform various tasks depending on the target View.
        }

        void cloneObjectController_CustomCloneObject(object sender, CustomCloneObjectEventArgs e)
        {
            var cloner = new MyCloner();
            e.TargetObjectSpace = e.CreateDefaultTargetObjectSpace();
            object objectFromTargetObjectSpace = e.TargetObjectSpace.GetObject(e.SourceObject);
            e.ClonedObject = cloner.CloneTo(objectFromTargetObjectSpace, e.TargetType);
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
