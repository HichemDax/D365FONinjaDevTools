using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using D365FONinjaDevTools.AddItemsToFolder.ElementTypes;
using D365FONinjaDevTools.Kernel;
using EnvDTE;
using Microsoft.Dynamics.Framework.Tools.ProjectSystem;

namespace D365FONinjaDevTools.AddItemsToFolder
{
    public class AotElementCreateEngin
    {
        public static List<ElementType> ElementTypes { get; set; } = new List<ElementType>();

        static AotElementCreateEngin()
        {
            foreach (var item in GetEnumerableOfType<ElementType>())
                ElementTypes.Add(item);
        }

        public static IEnumerable<T> GetEnumerableOfType<T>() where T : class
        {
            List<T> objects = new List<T>();
            foreach (Type type in
                Assembly.GetAssembly(typeof(T)).GetTypes()
                    .Where(myType => myType.IsClass
                                     && !myType.IsAbstract
                                     && myType.IsSubclassOf(typeof(T))))
            {
                objects.Add((T) Activator.CreateInstance(type));
            }
            return objects;
        }

        public static void AddAotElement(ProjectItem selectedItem, string elementName)
        {
            foreach (var elementType in ElementTypes)
            {
                if (elementType.IsMatch(selectedItem))
                {
                    elementType.CreateAndAddToProject(elementName);
                    break;
                }
            }
        }


        public static bool CheckIsMatch(ProjectItem selectedItem)
        {
            var folderNode = selectedItem?.Object as VSProjectFolderNode;

            if (folderNode == null)
                return false;

            if (FindTypeFromFolderName(folderNode.Caption) != null)
                return true;

            return false;
        }

        public static Type FindTypeFromFolderName(string folderName)
        {
            foreach (var elementType in ElementTypes)
            {
                var pluralName = LocalUtils.ElementService.GetPluralName(elementType.GetElementType());
                if (pluralName == folderName)
                    return elementType.GetType();
            }
            return null;
        }

        public static bool CheckIsMatchWithType(string folderName, Type axElementType)
        {
            foreach (var elementType in ElementTypes)
            {
                var pluralName = LocalUtils.ElementService.GetPluralName(elementType.GetElementType());
                if (pluralName == folderName)
                    return elementType.GetElementType() == axElementType;
            }

            throw new Exception(nameof(CheckIsMatchWithType));
        }
    }
}