using System;
using D365FONinjaDevTools.Kernel;
using Microsoft.Dynamics.AX.Metadata.MetaModel;

namespace D365FONinjaDevTools.AddItemsToFolder.ElementTypes
{
    public class Class : ElementType
    {
        protected override Type GetElementType()
        {
            return typeof(AxClass);
        }

        protected override ElementNames GetElementTypesAction()
        {
            return MetaService.GetClassNames;
        }

        protected override void Create()
        {
            var element = new AxClass { Name = ElementName };
            MetaService.CreateClass(element, Model);
        }
    }
}