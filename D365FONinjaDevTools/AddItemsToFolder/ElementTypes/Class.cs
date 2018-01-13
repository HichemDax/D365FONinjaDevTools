using System;
using Microsoft.Dynamics.AX.Metadata.MetaModel;

namespace D365FONinjaDevTools.AddItemsToFolder.ElementTypes
{
    public class Class : ElementType
    {
        public override Type GetElementType()
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

    public class EdtDate : ElementType
    {
        public override Type GetElementType()
        {
            return typeof(AxEdtDate);
        }

        protected override ElementNames GetElementTypesAction()
        {
            return MetaService.GetExtendedDataTypeNames;
        }

        protected override void Create()
        {
            var element = new AxEdtDate { Name = ElementName,  };
            MetaService.CreateExtendedDataType(element, Model);
        }
    }


    public class EdtContainer : ElementType
    {
        public override Type GetElementType()
        {
            return typeof(AxEdtContainer);
        }

        protected override ElementNames GetElementTypesAction()
        {
            return MetaService.GetExtendedDataTypeNames;
        }

        protected override void Create()
        {
            var element = new AxEdtContainer { Name = ElementName, };
            MetaService.CreateExtendedDataType(element, Model);
        }
    }

    public class EdtEnum : ElementType
    {
        public override Type GetElementType()
        {
            return typeof(AxEdtEnum);
        }

        protected override ElementNames GetElementTypesAction()
        {
            return MetaService.GetExtendedDataTypeNames;
        }

        protected override void Create()
        {
            var element = new AxEdtEnum { Name = ElementName, };
            MetaService.CreateExtendedDataType(element, Model);
        }
    }

    public class EdtGuid : ElementType
    {
        public override Type GetElementType()
        {
            return typeof(AxEdtGuid);
        }

        protected override ElementNames GetElementTypesAction()
        {
            return MetaService.GetExtendedDataTypeNames;
        }

        protected override void Create()
        {
            var element = new AxEdtGuid { Name = ElementName, };
            MetaService.CreateExtendedDataType(element, Model);
        }
    }

    public class EdtInt : ElementType
    {
        public override Type GetElementType()
        {
            return typeof(AxEdtInt);
        }

        protected override ElementNames GetElementTypesAction()
        {
            return MetaService.GetExtendedDataTypeNames;
        }

        protected override void Create()
        {
            var element = new AxEdtInt { Name = ElementName, };
            MetaService.CreateExtendedDataType(element, Model);
        }
    }

    public class EdtInt64 : ElementType
    {
        public override Type GetElementType()
        {
            return typeof(AxEdtInt64);
        }

        protected override ElementNames GetElementTypesAction()
        {
            return MetaService.GetExtendedDataTypeNames;
        }

        protected override void Create()
        {
            var element = new AxEdtInt64 { Name = ElementName, };
            MetaService.CreateExtendedDataType(element, Model);
        }
    }

    public class EdtReal : ElementType
    {
        public override Type GetElementType()
        {
            return typeof(AxEdtReal);
        }

        protected override ElementNames GetElementTypesAction()
        {
            return MetaService.GetExtendedDataTypeNames;
        }

        protected override void Create()
        {
            var element = new AxEdtReal { Name = ElementName, };
            MetaService.CreateExtendedDataType(element, Model);
        }
    }

    public class EdtString : ElementType
    {
        public override Type GetElementType()
        {
            return typeof(AxEdtString);
        }

        protected override ElementNames GetElementTypesAction()
        {
            return MetaService.GetExtendedDataTypeNames;
        }

        protected override void Create()
        {
            var element = new AxEdtString { Name = ElementName, };
            MetaService.CreateExtendedDataType(element, Model);
        }
    }



}