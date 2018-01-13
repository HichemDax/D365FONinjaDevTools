using System;
using Microsoft.Dynamics.AX.Metadata.MetaModel;

namespace D365FONinjaDevTools.AddItemsToFolder.ElementTypes
{
    public class Form : ElementType
    {
        public override Type GetElementType()
        {
            return typeof(AxForm);
        }

        protected override ElementNames GetElementTypesAction()
        {
            return MetaService.GetFormNames;
        }

        protected override void Create()
        {
            var element = new AxForm { Name = ElementName };
            MetaService.CreateForm(element, Model);
        }
    }
}