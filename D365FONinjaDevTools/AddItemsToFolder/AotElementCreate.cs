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

        public static void Create(UtilElementType type, string name, dynamic baseElement = null)
        {
            _name = name;
            switch (type)
            {
                case UtilElementType.DisplayTool:
                    var existsDisplayTool = LocalUtils.MetaService.GetMenuItemDisplayNames().Contains(name);

                    if (!existsDisplayTool)
                    {
                        var displayMenu = new AxMenuItemDisplay {Name = name};
                        LocalUtils.MetaService.CreateMenuItemDisplay(displayMenu, Model);
                        AddAotElement<AxMenuItemDisplay>();
                    }

                    break;
                case UtilElementType.OutputTool:
                    var existsOutputTool = LocalUtils.MetaService.GetMenuItemDisplayNames().Contains(name);

                    if (!existsOutputTool)
                    {
                        var outputMenu = new AxMenuItemOutput {Name = name};
                        LocalUtils.MetaService.CreateMenuItemOutput(outputMenu, Model);
                        AddAotElement<AxMenuItemOutput>();
                    }

                    break;
                case UtilElementType.ActionTool:
                    var existsActionTool = LocalUtils.MetaService.GetMenuItemDisplayNames().Contains(name);

                    if (!existsActionTool)
                    {
                        var actionMenu = new AxMenuItemAction {Name = name};
                        LocalUtils.MetaService.CreateMenuItemAction(actionMenu, Model);
                        AddAotElement<AxMenuItemAction>();
                    }
                    break;

                case UtilElementType.Query:
                    var existsQuery = LocalUtils.MetaService.GetQueryNames().Contains(name);

                    if (!existsQuery)
                    {
                        var query = new AxQuerySimple {Name = name};
                        LocalUtils.MetaService.CreateQuery(query, Model);
                        AddAotElement<AxQuerySimple>();
                    }
                    break;

                case UtilElementType.Enum:
                    var Enum = LocalUtils.MetaService.GetEnumNames().Contains(name);

                    if (!Enum)
                    {
                        var edtEnum = new AxEnum {Name = name};
                        LocalUtils.MetaService.CreateEnum(edtEnum, Model);
                        AddAotElement<AxEnum>();
                    }
                    break;
                case UtilElementType.ExtendedType:


                    break;
                case UtilElementType.Table:
                    var existsTable = LocalUtils.MetaService.GetTableNames().Contains(name);

                    if (!existsTable)
                    {
                        var table = new AxTable {Name = name};
                        LocalUtils.MetaService.CreateTable(table, Model);
                        AddAotElement<AxTable>();
                    }
                    break;
                case UtilElementType.Class:
                    var existsClass = LocalUtils.MetaService.GetClassNames().Contains(name);

                    if (!existsClass)
                    {
                        var axClass = NAxClass.Construct(name);
                        LocalUtils.MetaService.CreateClass(axClass, Model);
                        AddAotElement<AxClass>();
                    }
                    break;
                case UtilElementType.SSRSReport:
                    var existsSsrsReport = LocalUtils.MetaService.GetReportNames().Contains(name);

                    if (!existsSsrsReport)
                    {
                        var srsReport = new AxReport {Name = name};
                        LocalUtils.MetaService.CreateReport(srsReport, Model);
                        AddAotElement<AxReport>();
                    }
                    break;

                case UtilElementType.Form:
                    var existsForm = LocalUtils.MetaService.GetFormNames().Contains(name);

                    if (!existsForm)
                    {
                        var axForm = new AxForm {Name = name};
                        LocalUtils.MetaService.CreateForm(axForm, Model);
                        AddAotElement<AxForm>();
                    }
                    break;

                case UtilElementType.Menu:
                    var existsMenu = LocalUtils.MetaService.GetMenuNames().Contains(name);

                    if (!existsMenu)
                    {
                        var axMenu = new AxMenu {Name = name};
                        LocalUtils.MetaService.CreateMenu(axMenu, Model);
                        AddAotElement<AxMenu>();
                    }
                    break;

                case UtilElementType.SecDuty:
                    var existsSecDuty = LocalUtils.MetaService.GetSecurityDutyNames().Contains(name);

                    if (!existsSecDuty)
                    {
                        var axDuty = new AxSecurityDuty {Name = name};
                        LocalUtils.MetaService.CreateSecurityDuty(axDuty, Model);
                        AddAotElement<AxSecurityDuty>();
                    }
                    break;
                case UtilElementType.SecPolicy:
                    var existsSecPolicy = LocalUtils.MetaService.GetSecurityPolicyNames().Contains(name);

                    if (!existsSecPolicy)
                    {
                        var axPolicy = new AxSecurityPolicy {Name = name};
                        LocalUtils.MetaService.CreateSecurityPolicy(axPolicy, Model);
                        AddAotElement<AxSecurityPolicy>();
                    }
                    break;
                case UtilElementType.SecPrivilege:
                    var existsSecPrivilege = LocalUtils.MetaService.GetSecurityPrivilegeNames().Contains(name);

                    if (!existsSecPrivilege)
                    {
                        var privilege = new AxSecurityPrivilege {Name = name};
                        LocalUtils.MetaService.CreateSecurityPrivilege(privilege, Model);
                        AddAotElement<AxSecurityPrivilege>();
                    }
                    break;
                case UtilElementType.SecRole:
                    var existsSecRole = LocalUtils.MetaService.GetSecurityRoleNames().Contains(name);

                    if (!existsSecRole)
                    {
                        var role = new AxSecurityRole {Name = name};
                        LocalUtils.MetaService.CreateSecurityRole(role, Model);
                        AddAotElement<AxSecurityRole>();
                    }
                    break;

                default:
                    throw new Exception("Element not supported");
            }
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