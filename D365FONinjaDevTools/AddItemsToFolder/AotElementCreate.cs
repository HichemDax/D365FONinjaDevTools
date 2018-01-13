using System;
using System.Collections.Generic;
using D365FONinjaDevTools.Kernel;
using Dynamics.AX.Application;
using Microsoft.Dynamics.AX.Metadata.MetaModel;
using Exception = System.Exception;
using VSProjectFolderNode = Microsoft.Dynamics.Framework.Tools.ProjectSystem.VSProjectFolderNode;
using VSProjectNode = Microsoft.Dynamics.Framework.Tools.ProjectSystem.VSProjectNode;

namespace D365FONinjaDevTools.AddItemsToFolder
{
    public class AotElementCreate
    {
        private static readonly VSProjectNode Project;
        private static readonly ModelSaveInfo Model;
        private static string _name;

        static AotElementCreate()
        {
            Project = LocalUtils.GetActiveProjectNode();

            if (Project == null)
                throw new InvalidOperationException("No project selected.");

            var model = Project.GetProjectsModelInfo();

            Model = new ModelSaveInfo
            {
                Id = model.Id,
                Layer = model.Layer
            };
        }

        public static void CreateExtension(string name, dynamic baseElement)
        {
            _name = name;
            var myClass =  NAxClass.Construct(name, baseElement);
            LocalUtils.MetaService.CreateClass(myClass, Model);
            AddAotElement<AxClass>();
        }


        public static void CreateMenuItem(UtilElementType type, string name, string label = "")
        {
            if (Project == null)
                throw new InvalidOperationException("No project selected.");

            _name = name;
            switch (type)
            {
                case UtilElementType.DisplayTool:
                    var existsDisplayTool = LocalUtils.MetaService.GetMenuItemDisplayNames().Contains(name);

                    if (!existsDisplayTool)
                    {
                        var displayMenu = new AxMenuItemDisplay {Name = name, Object = name, Label = label};

                        LocalUtils.MetaService.CreateMenuItemDisplay(displayMenu, Model);
                        AddAotElement<AxMenuItemDisplay>("Display Menu Items");
                    }

                    break;
                case UtilElementType.OutputTool:
                    var existsOutputTool = LocalUtils.MetaService.GetMenuItemDisplayNames().Contains(name);

                    if (!existsOutputTool)
                    {
                        var outputMenu = new AxMenuItemOutput {Name = name, Object = name, Label = name.Convert()};
                        LocalUtils.MetaService.CreateMenuItemOutput(outputMenu, Model);
                        AddAotElement<AxMenuItemOutput>("Output Menu Items");
                    }

                    break;
                case UtilElementType.ActionTool:
                    var existsActionTool = LocalUtils.MetaService.GetMenuItemDisplayNames().Contains(name);

                    if (!existsActionTool)
                    {
                        var actionMenu = new AxMenuItemAction {Name = name, Object = name, Label = name.Convert()};
                        LocalUtils.MetaService.CreateMenuItemAction(actionMenu, Model);
                        AddAotElement<AxMenuItemAction>("Action Menu Items");
                    }
                    break;

                default:
                    throw new Exception("Element not supported");
            }
        }

        private static void AddAotElement<TAotElementType>(string folderName = "")
        {
            dynamic selectedItem;

            if (folderName.Length != 0)
                selectedItem = Project.CreateFolderNodes(folderName) as VSProjectFolderNode;

            else
            {
                selectedItem = LocalUtils.MyDte.SelectedItems.Item(1).ProjectItem.Object;
                var isFolder = selectedItem is VSProjectFolderNode;
                if (!isFolder)
                    selectedItem = selectedItem.Parent;
            }

            if (selectedItem is VSProjectFolderNode)
                Project.AddModelElementsToProject(
                    new List<MetadataReference> {new MetadataReference(_name, typeof(TAotElementType))},
                    selectedItem.ID, true);
        }
    }
}