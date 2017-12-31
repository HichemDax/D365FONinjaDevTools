using System;
using Microsoft.Dynamics.AX.Metadata.MetaModel;

namespace D365FONinjaDevTools.AddItemsToFolder.ElementTypes
{
    public class Menu : ElementType
    {
        protected override Type GetElementType()
        {
            return typeof(AxMenu);
        }

        protected override ElementNames GetElementTypesAction()
        {
            return MetaService.GetMenuNames;
        }

        protected override void Create()
        {
            var element = new AxMenu { Name = ElementName };
            MetaService.CreateMenu(element, Model);
        }
    }
}