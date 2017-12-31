using System;
using Microsoft.Dynamics.AX.Metadata.MetaModel;

namespace D365FONinjaDevTools.AddItemsToFolder.ElementTypes
{
    public class Enum : ElementType
    {
        protected override ElementNames GetElementTypesAction()
        {
            return MetaService.GetEnumNames;
        }

        protected override Type GetElementType()
        {
            return typeof(AxEnum);
        }

        protected override void Create()
        {
            var element = new AxEnum {Name = ElementName };
            MetaService.CreateEnum(element, Model);
        }
    }
}