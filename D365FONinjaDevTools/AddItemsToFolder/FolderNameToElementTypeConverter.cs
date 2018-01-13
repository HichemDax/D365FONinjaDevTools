using System;
using System.Collections.Generic;
using D365FONinjaDevTools.Kernel;
using Microsoft.Dynamics.AX.Metadata.MetaModel;

namespace D365FONinjaDevTools.AddItemsToFolder
{
  /*  public class FolderNameToElementTypeConverter
    {
        private static readonly List<Tuple<Type, AxElementType>> Types = new List<Tuple<Type, AxElementType>>();
        static FolderNameToElementTypeConverter()
        {
            Types.Add(new Tuple<Type, AxElementType> (typeof(AxMenuItemDisplay), AxElementType.MenuItemDisplay) );
            Types.Add(new Tuple<Type, AxElementType> (typeof(AxMenuItemOutput), AxElementType.MenuItemOutput) );
            Types.Add(new Tuple<Type, AxElementType> (typeof(AxMenuItemAction), AxElementType.MenuItemAction) );
            Types.Add(new Tuple<Type, AxElementType> (typeof(AxQuerySimple), AxElementType.QuerySimple) );
            Types.Add(new Tuple<Type, AxElementType> (typeof(AxEnum), AxElementType.Enum) );
            Types.Add(new Tuple<Type, AxElementType> (typeof(AxTable), AxElementType.Table) );
            Types.Add(new Tuple<Type, AxElementType> (typeof(AxClass), AxElementType.Class) );
            Types.Add(new Tuple<Type, AxElementType> (typeof(AxReport), AxElementType.Report) );
            Types.Add(new Tuple<Type, AxElementType> (typeof(AxForm), AxElementType.Form) );
            Types.Add(new Tuple<Type, AxElementType> (typeof(AxMenu), AxElementType.Menu) );
            Types.Add(new Tuple<Type, AxElementType> (typeof(AxSecurityDuty), AxElementType.SecurityDuty) );
            Types.Add(new Tuple<Type, AxElementType> (typeof(AxSecurityPolicy), AxElementType.SecurityPolicy) );
            Types.Add(new Tuple<Type, AxElementType> (typeof(AxSecurityPrivilege), AxElementType.SecurityPrivilege) );
            Types.Add(new Tuple<Type, AxElementType> (typeof(AxSecurityRole), AxElementType.SecurityRole) );
            Types.Add(new Tuple<Type, AxElementType> (typeof(AxEdtDate), AxElementType.EdtDate) );
            Types.Add(new Tuple<Type, AxElementType> (typeof(AxEdtDate), AxElementType.EdtDate) );
            Types.Add(new Tuple<Type, AxElementType> (typeof(AxEdtDate), AxElementType.EdtDate) );
            Types.Add(new Tuple<Type, AxElementType> (typeof(AxEdtDate), AxElementType.EdtDate) );
            Types.Add(new Tuple<Type, AxElementType> (typeof(AxEdtDate), AxElementType.EdtDate) );
            Types.Add(new Tuple<Type, AxElementType> (typeof(AxEdtDate), AxElementType.EdtDate) );
            Types.Add(new Tuple<Type, AxElementType> (typeof(AxEdtDate), AxElementType.EdtDate) );
        }

        public static Tuple<Type, AxElementType> FindTypeFromFolderName( string folderName)
        {
            foreach (var type in Types)
            {
                var pluralName =  LocalUtils.ElementService.GetPluralName(type.Item1);
                if (pluralName == folderName)
                    return type;
            }

            return null;
        }


        public static Tuple<Type, AxElementType> FindTypeFromElement(string element)
        {
            foreach (var type in Types)
            {
                var pluralName = LocalUtils.ElementService.GetPluralName(type.Item1);
                if (pluralName == element)
                    return type;
            }

            return null;
        }
    }*/
}