using System;
using Microsoft.Dynamics.AX.Metadata.MetaModel;

namespace D365FONinjaDevTools.AddItemsToFolder.ElementTypes
{
    public class MenuItemDisplay : ElementType
    {
        protected override ElementNames GetElementTypesAction()
        {
            return MetaService.GetMenuItemDisplayNames;
        }

        public override Type GetElementType()
        {
            return typeof(AxMenuItemDisplay);
        }

        protected override void Create()
        {
            var element = new AxMenuItemDisplay {Name = ElementName };
            MetaService.CreateMenuItemDisplay(element, Model);
        }
    }
}