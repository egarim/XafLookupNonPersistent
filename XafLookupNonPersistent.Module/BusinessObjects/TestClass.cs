using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace XafLookupNonPersistent.Module.BusinessObjects
{
    using DevExpress.Persistent.Base;
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
        [Association("TestClass-TestClassDetails"), Aggregated]
        public XPCollection<TestClassDetail> TestClassDetails
        {
            get
            {
                return GetCollection<TestClassDetail>(nameof(TestClassDetails));
            }
        }
    }
}
