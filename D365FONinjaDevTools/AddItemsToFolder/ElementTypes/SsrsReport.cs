using System;
using Microsoft.Dynamics.AX.Metadata.MetaModel;

namespace D365FONinjaDevTools.AddItemsToFolder.ElementTypes
{
    public class SsrsReport : ElementType
    {
        public override Type GetElementType()
        {
            return typeof(AxReport);
        }

        protected override ElementNames GetElementTypesAction()
        {
            return MetaService.GetReportNames;
        }

        protected override void Create()
        {
            var element = new AxReport { Name = ElementName };
            MetaService.CreateReport(element, Model);
        }
    }
}