using System;
using System.Linq;

namespace XafLookupNonPersistent.Module.BusinessObjects
{
    using DevExpress.ExpressApp;
    using DevExpress.ExpressApp.DC;
    using DevExpress.ExpressApp.Model;

    [DomainComponent]
    public class PropertyObject : NonPersistentBaseObject
    {
        bool selected;
        private string name;
        private string _value;
        public override void OnSaving()
        {
            base.OnSaving();
            // ...
        }
        //[ModelDefault("AllowEdit","false")]
        public string Name
        {
            get { return name; }
            set { SetPropertyValue(ref name, value); }
        }
        public void SetName(string Name)
        {
            this.Name= Name;
        }
        public void SetValue(string Value)
        {
            this.Value = Value;
        }
        [ModelDefault("AllowEdit", "false")]
        public string Value
        {
            get { return _value; }
            set { SetPropertyValue(ref _value, value); }
        }

        public bool Selected
        {
            get => selected;
            set => SetPropertyValue(ref selected, value, nameof(Selected));
        }
    }
}
