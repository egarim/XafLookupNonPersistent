using DevExpress.Persistent.BaseImpl;
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

    [DefaultClassOptions]
    public class TestClassDetail : BaseObject
    {
        public TestClassDetail(Session session) : base(session)
        { }


        TestClass testClass;
        string valueTest;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ValueTest
        {
            get => valueTest;
            set => SetPropertyValue(nameof(ValueTest), ref valueTest, value);
        }
        
        [Association("TestClass-TestClassDetails")]
        public TestClass TestClass
        {
            get => testClass;
            set => SetPropertyValue(nameof(TestClass), ref testClass, value);
        }
    }
    [DefaultClassOptions]
    public class TestClass : BaseObject
    {
        public TestClass(Session session) : base(session)
        { }


        DateTime date;
        string address;
        string name;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Address
        {
            get => address;
            set => SetPropertyValue(nameof(Address), ref address, value);
        }
        [NonCloneable()]
        public DateTime Date
        {
            get => date;
            set => SetPropertyValue(nameof(Date), ref date, value);
        }
        [Association("TestClass-TestClassDetails"),DevExpress.Xpo.Aggregated]
        public XPCollection<TestClassDetail> TestClassDetails
        {
            get
            {
                return GetCollection<TestClassDetail>(nameof(TestClassDetails));
            }
        }
    }

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
                XPCustomObject xPCustomObject=this.CurrentInstance as XPCustomObject;
                //TODO we need to remove service members https://supportcenter.devexpress.com/ticket/details/q237276/what-is-servicefield
                List<PropertyObject> list = new List<PropertyObject>();
                TypeInfo.Members.Where(x => (x.IsPersistent && x.FindAttribute<NonCloneableAttribute>()==null && x.IsKey==false && x.FindAttribute<BrowsableAttribute>()?.Browsable!=false && !x.IsService && x.BindingName!= "GCRecord") || (x.IsList && x.IsPublic)).ToList().ForEach(x =>
                {
                  
                    var ServiceField = x as DevExpress.Xpo.Metadata.Helpers.ServiceField;
                    if (ServiceField == null)
                    {
                        var PropertyObject = ObjectSpace.CreateObject<PropertyObject>();
                        PropertyObject.Name = x.Name;
                        if(x.IsList)
                        {
                            var Collection = xPCustomObject.GetMemberValue(x.Name) as XPBaseCollection;
                            PropertyObject.Value = Collection.Count.ToString();
                        }
                        else
                        {
                            PropertyObject.Value = xPCustomObject.GetMemberValue(x.Name)?.ToString();
                        }
                       
                        list.Add(PropertyObject);
                    }
                });
                
                return list;   
            }
        }
    }
    // ...
    [DomainComponent]
    public class MainObject : NonPersistentBaseObject
    {
        string filterValues;
        ITypeInfo typeInfo;
        PropertyObject propertyObject;
        private String name;
        private String description;
        public override void OnSaving()
        {
            base.OnSaving();
            // ...
        }
        
        [Size(SizeAttribute.Unlimited)]
        public string FilterValues
        {
            get => filterValues;
            set => SetPropertyValue(ref filterValues, value, nameof(FilterValues));
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
                        PropertyObject.Value = x.DisplayName;
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
        bool selected;
        private String name;
        private String _value;
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
        public String Value
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
