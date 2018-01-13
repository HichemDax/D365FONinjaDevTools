using System;
using Microsoft.Dynamics.AX.Metadata.MetaModel;

namespace D365FONinjaDevTools.AddItemsToFolder.ElementTypes
{
    public class SecurityDuty : ElementType
    {
        protected override ElementNames GetElementTypesAction()
        {
            return MetaService.GetSecurityDutyNames;
        }

        public override Type GetElementType()
        {
            return typeof(AxSecurityDuty);
        }

        protected override void Create()
        {
            var element = new AxSecurityDuty {Name = ElementName };
            MetaService.CreateSecurityDuty(element, Model);
        }
    }
}