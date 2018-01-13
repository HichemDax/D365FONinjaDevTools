using System;
using Microsoft.Dynamics.AX.Metadata.MetaModel;

namespace D365FONinjaDevTools.AddItemsToFolder.ElementTypes
{
    public class MenuItemAction : ElementType
    {
        public override Type GetElementType()
        {
            return typeof(AxMenuItemAction);
        }

        protected override ElementNames GetElementTypesAction()
        {
            return MetaService.GetMenuItemActionNames;
        }

        protected override void Create()
        {
            var element = new AxMenuItemAction { Name = ElementName };
            MetaService.CreateMenuItemAction(element, Model);
        }
    }
}