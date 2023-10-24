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
            base.CopyMemberValue(memberInfo, sourceObject, targetObject);
        }
        public override bool IsMemberCloneable(XPMemberInfo memberInfo)
        {
            var Names= _CloneableProperties.Select(x => x.Name).ToList();
            if (Names.Contains(memberInfo.Name))
            {
                return true;
            }
            else
            {
                return base.IsMemberCloneable(memberInfo);
            }
           
        }
    }
}
