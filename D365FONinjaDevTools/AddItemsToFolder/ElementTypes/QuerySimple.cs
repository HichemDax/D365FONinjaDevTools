using System;
using Microsoft.Dynamics.AX.Metadata.Core.Collections;
using Microsoft.Dynamics.AX.Metadata.MetaModel;

namespace D365FONinjaDevTools.AddItemsToFolder.ElementTypes
{
    public class QuerySimple : ElementType
    {
        protected override ElementNames GetElementTypesAction()
        {
            return MetaService.GetQueryNames;
        }

        public override Type GetElementType()
        {
            return typeof(AxQuerySimple);
        }

        protected override void Create()
        {
            var element = new AxQuerySimple
            {
                Name = ElementName, Methods = new KeyedObjectCollection<AxMethod>()
                {
                    new AxMethod()
                    {
                        Name = "classDeclaration",
                        Source = "[Query]\r\npublic class " + ElementName + " extends QueryRun \r\n{\r\n}",
                    }
                }
            };
            MetaService.CreateQuery(element, Model);
        }
    }
}