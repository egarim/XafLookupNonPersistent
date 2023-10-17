using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace XafLookupNonPersistent.Module.BusinessObjects
{
    using DevExpress.Persistent.Base;
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
}
