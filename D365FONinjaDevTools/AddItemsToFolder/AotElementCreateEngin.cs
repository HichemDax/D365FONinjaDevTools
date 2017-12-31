using System.Collections.Generic;
using D365FONinjaDevTools.AddItemsToFolder.ElementTypes;
using EnvDTE;

namespace D365FONinjaDevTools.AddItemsToFolder
{
    public class AotElementCreateEngin
    {
        public List<ElementType> ElementTypes { get; set; } = new List<ElementType>();

        public AotElementCreateEngin()
        {
            ElementTypes.Add(new Class());
            ElementTypes.Add(new Enum());
            ElementTypes.Add(new Form());
            ElementTypes.Add(new Menu());
            ElementTypes.Add(new MenuItemDisplay());
            ElementTypes.Add(new MenuItemAction());
            ElementTypes.Add(new MenuItemOutput());
            ElementTypes.Add(new QuerySimple());
            ElementTypes.Add(new SecurityDuty());
            ElementTypes.Add(new SecurityPrivilege());
            ElementTypes.Add(new SecurityRole());
            ElementTypes.Add(new SsrsReport());
            ElementTypes.Add(new Table());
        }

        public void AddAotElement(ProjectItem selectedItem, string elementName)
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
    }
}
