using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XafLookupNonPersistent.Module.BusinessObjects
{
    using DevExpress.ExpressApp;
    using DevExpress.ExpressApp.DC;
    using DevExpress.Persistent.Base;
    using System.ComponentModel;

    // ...
    [DomainComponent]
    public class MainObject : NonPersistentBaseObject
    {
        ITypeInfo typeInfo;
        PropertyObject propertyObject;
        private String name;
        private String description;
        public override void OnSaving()
        {
            base.OnSaving();
            // ...
        }
        public String Name
        {
            get { return name; }
            set { SetPropertyValue(ref name, value); }
        }
        public String Description
        {
            get { return description; }
            set { SetPropertyValue(ref description, value); }
        }
        [DataSourceProperty("RefProperties")]
        public PropertyObject PropertyObject
        {
            get => propertyObject;
            set => SetPropertyValue<PropertyObject>(ref propertyObject, value, nameof(PropertyObject));
        }
        [Browsable(false)]
        public ITypeInfo TypeInfo
        {
            get => typeInfo;
            set
            {
                if (typeInfo == value)
                    return;
                typeInfo = value;
                OnPropertyChanged(nameof(TypeInfo));
            }
        }
        
        [Browsable(false)]
        public List<PropertyObject> RefProperties
        {
            get
            {

                //TODO we need to remove service members https://supportcenter.devexpress.com/ticket/details/q237276/what-is-servicefield
                List<PropertyObject> list = new List<PropertyObject>();
                TypeInfo.Members.Where(x => x.IsPersistent).ToList().ForEach(x =>
                {
                    var ServiceField = x as DevExpress.Xpo.Metadata.Helpers.ServiceField;
                    if(ServiceField==null)
                    {
                        var PropertyObject = ObjectSpace.CreateObject<PropertyObject>();
                        PropertyObject.Name = x.Name;
                        PropertyObject.Description = x.DisplayName;
                        list.Add(PropertyObject);
                    }
                  
                    //list.Add(PropertyObject);
                });
                //PropertyObject1.Name = "1";
                //PropertyObject2.Name = "2";
                //list.Add(PropertyObject1);
                //list.Add(PropertyObject2);
                return list;
                //return this.ObjectSpace.CreateCollection(typeof(PropertyObject)).Cast<PropertyObject>().ToList();
            }
        }
    }
    [DomainComponent]
    public class PropertyObject : NonPersistentBaseObject
    {
        private String name;
        private String description;
        public override void OnSaving()
        {
            base.OnSaving();
            // ...
        }
        public String Name
        {
            get { return name; }
            set { SetPropertyValue(ref name, value); }
        }
        public String Description
        {
            get { return description; }
            set { SetPropertyValue(ref description, value); }
        }
    }
}
