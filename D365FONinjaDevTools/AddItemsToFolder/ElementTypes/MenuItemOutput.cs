using System;
using Microsoft.Dynamics.AX.Metadata.MetaModel;

namespace D365FONinjaDevTools.AddItemsToFolder.ElementTypes
{
    public class MenuItemOutput : ElementType
    {
        public override Type GetElementType()
        {
            return typeof(AxMenuItemOutput);
        }

        protected override ElementNames GetElementTypesAction()
        {
            return MetaService.GetMenuItemOutputNames;
        }

        protected override void Create()
        {
            var element = new AxMenuItemOutput { Name = ElementName };
            MetaService.CreateMenuItemOutput(element, Model);
        }
    }
}