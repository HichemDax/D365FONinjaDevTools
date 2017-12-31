using System;
using Microsoft.Dynamics.AX.Metadata.MetaModel;

namespace D365FONinjaDevTools.AddItemsToFolder.ElementTypes
{
    public class QuerySimple : ElementType
    {
        protected override ElementNames GetElementTypesAction()
        {
            return MetaService.GetQueryNames;
        }

        protected override Type GetElementType()
        {
            return typeof(AxQuerySimple);
        }

        protected override void Create()
        {
            var element = new AxQuerySimple {Name = ElementName};
            MetaService.CreateQuery(element, Model);
        }
    }
}