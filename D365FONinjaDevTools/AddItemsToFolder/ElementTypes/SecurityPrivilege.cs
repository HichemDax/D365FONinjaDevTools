using System;
using Microsoft.Dynamics.AX.Metadata.MetaModel;

namespace D365FONinjaDevTools.AddItemsToFolder.ElementTypes
{
    public class SecurityPrivilege : ElementType
    {
       
        protected override Type GetElementType()
        {
            return typeof(AxSecurityPrivilege);
        }

        protected override ElementNames GetElementTypesAction()
        {
            return MetaService.GetSecurityPrivilegeNames;
        }


        protected override void Create()
        {
            var element = new AxSecurityPrivilege {Name = ElementName };
            MetaService.CreateSecurityPrivilege(element, Model);
        }
    }
}