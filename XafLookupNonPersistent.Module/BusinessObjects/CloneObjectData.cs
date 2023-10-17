using DevExpress.Xpo;
using System;
using System.Linq;

namespace XafLookupNonPersistent.Module.BusinessObjects
{
    using DevExpress.ExpressApp;
    using DevExpress.ExpressApp.DC;
    using DevExpress.Persistent.Base;
    using System.ComponentModel;
    // ...
    [DomainComponent]
    public class CloneObjectData : NonPersistentBaseObject
    {
        ITypeInfo typeInfo;
        PropertyObject propertyObject;

        public override void OnSaving()
        {
            base.OnSaving();
            // ...
        }
        [Browsable(false)]
        public object CurrentInstance { get; set; }
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


        public List<PropertyObject> CloneableProperties
        {
            get
            {
                XPCustomObject xPCustomObject = CurrentInstance as XPCustomObject;
                //TODO we need to remove service members https://supportcenter.devexpress.com/ticket/details/q237276/what-is-servicefield
                List<PropertyObject> list = new List<PropertyObject>();
                TypeInfo.Members.Where(x => x.IsPersistent  && x.IsKey == false && x.FindAttribute<BrowsableAttribute>()?.Browsable != false && !x.IsService && x.BindingName != "GCRecord" || x.IsList && x.IsPublic).ToList().ForEach(x =>
                {
                    //&& x.FindAttribute<NonCloneableAttribute>() == null
                    var ServiceField = x as DevExpress.Xpo.Metadata.Helpers.ServiceField;
                    if (ServiceField == null)
                    {
                        var PropertyObject = ObjectSpace.CreateObject<PropertyObject>();
                        PropertyObject.Name = x.Name;
                        if (x.IsList)
                        {
                            var Collection = xPCustomObject.GetMemberValue(x.Name) as XPBaseCollection;
                            PropertyObject.Value = Collection.Count.ToString();
                        }
                        else
                        {
                            PropertyObject.Value = xPCustomObject.GetMemberValue(x.Name)?.ToString();
                        }
                        if(x.FindAttribute<NonCloneableAttribute>() == null)
                        {
                            PropertyObject.Selected=true;
                        }
                        else
                        {
                            PropertyObject.Selected = false;
                        }
                        list.Add(PropertyObject);
                    }
                });

                return list;
            }
        }
    }
}
