using System;
using Microsoft.Dynamics.AX.Metadata.MetaModel;

namespace D365FONinjaDevTools.AddItemsToFolder.ElementTypes
{
    public class SecurityRole : ElementType
    {
        protected override Type GetElementType()
        {
            return typeof(AxSecurityRole);
        }

        protected override ElementNames GetElementTypesAction()
        {
            return MetaService.GetSecurityRoleNames;
        }

        protected override void Create()
        {
            var element = new AxSecurityRole { Name = ElementName };
            MetaService.CreateSecurityRole(element, Model);
        }
    }
}