using System;
using Microsoft.Dynamics.AX.Metadata.MetaModel;

namespace D365FONinjaDevTools.AddItemsToFolder.ElementTypes
{
    public class Table : ElementType
    {
        public override Type GetElementType()
        {
            return typeof(AxTable);
        }

        protected override ElementNames GetElementTypesAction()
        {
            return MetaService.GetTableNames;
        }

        protected override void Create()
        {
            var element = new AxTable { Name = ElementName };
            MetaService.CreateTable(element, Model);
        }
    }
}