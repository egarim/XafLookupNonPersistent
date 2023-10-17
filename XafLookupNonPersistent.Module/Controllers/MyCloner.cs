using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using System;
using System.Linq;
using XafLookupNonPersistent.Module.BusinessObjects;

namespace XafLookupNonPersistent.Module.Controllers
{
    public class MyCloner : Cloner
    {
        List<PropertyObject> _CloneableProperties;
        public MyCloner(List<PropertyObject> CloneableProperties)
        {
            _CloneableProperties = CloneableProperties;
        }
        public override void CopyMemberValue(
            XPMemberInfo memberInfo, IXPSimpleObject sourceObject, IXPSimpleObject targetObject)
        {
            if (!memberInfo.IsAssociation)
            {
                base.CopyMemberValue(memberInfo, sourceObject, targetObject);
            }
        }
    }
}
