using System;
using System.Collections.Generic;
using D365FONinjaDevTools.Kernel;
using Dynamics.AX.Application;
using Microsoft.Dynamics.AX.Metadata.MetaModel;

namespace D365FONinjaDevTools.AddItemsToFolder
{
    public class FolderType
    {
        private static readonly List<Tuple<Type, UtilElementType>> Types = new List<Tuple<Type, UtilElementType>>();
        static FolderType()
        {
            Types.Add(new Tuple<Type, UtilElementType> (typeof(AxMenuItemDisplay), UtilElementType.DisplayTool) );
            Types.Add(new Tuple<Type, UtilElementType> (typeof(AxMenuItemOutput), UtilElementType.OutputTool) );
            Types.Add(new Tuple<Type, UtilElementType> (typeof(AxMenuItemAction), UtilElementType.ActionTool) );
            Types.Add(new Tuple<Type, UtilElementType> (typeof(AxQuerySimple), UtilElementType.Query) );
            Types.Add(new Tuple<Type, UtilElementType> (typeof(AxEnum), UtilElementType.Enum) );
            Types.Add(new Tuple<Type, UtilElementType> (typeof(AxTable), UtilElementType.Table) );
            Types.Add(new Tuple<Type, UtilElementType> (typeof(AxClass), UtilElementType.Class) );
            Types.Add(new Tuple<Type, UtilElementType> (typeof(AxReport), UtilElementType.SSRSReport) );
            Types.Add(new Tuple<Type, UtilElementType> (typeof(AxForm), UtilElementType.Form) );
            Types.Add(new Tuple<Type, UtilElementType> (typeof(AxMenu), UtilElementType.Menu) );
            Types.Add(new Tuple<Type, UtilElementType> (typeof(AxSecurityDuty), UtilElementType.SecDuty) );
            Types.Add(new Tuple<Type, UtilElementType> (typeof(AxSecurityPolicy), UtilElementType.SecPolicy) );
            Types.Add(new Tuple<Type, UtilElementType> (typeof(AxSecurityPrivilege), UtilElementType.SecPrivilege) );
            Types.Add(new Tuple<Type, UtilElementType> (typeof(AxSecurityRole), UtilElementType.SecRole) );
        }

        public static Tuple<Type, UtilElementType> FindTypeFromFolderName( string folderName)
        {
            foreach (var type in Types)
            {
                var pluralName =  LocalUtils.ElementService.GetPluralName(type.Item1);
                if (pluralName == folderName)
                    return type;
            }

            return null;
        }


        public static Tuple<Type, UtilElementType> FindTypeFromElement(string element)
        {
            foreach (var type in Types)
            {
                var pluralName = LocalUtils.ElementService.GetPluralName(type.Item1);
                if (pluralName == element)
                    return type;
            }

            return null;
        }
    }
}