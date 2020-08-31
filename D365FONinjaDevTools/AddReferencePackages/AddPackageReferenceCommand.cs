using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using D365FONinjaDevTools.AddItemsToFolder;
using D365FONinjaDevTools.Kernel;
using Microsoft.Dynamics.AX.Metadata.Core.Messaging;
using Microsoft.Dynamics.AX.Metadata.MetaModel;
using Microsoft.Dynamics.Framework.Tools.Core;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
using Microsoft.Dynamics.Framework.Tools.ProjectSystem.ModelManagement;
using Microsoft.Dynamics.Framework.Tools.ProjectSystem.Wizard;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace D365FONinjaDevTools.AddPackageReference
{
    internal sealed class AddPackageReferenceCommand
    {

        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0X0300;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("5ed27ab2-7007-4c3d-a535-88720e97b49f");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package _package;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPackageReferenceCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private AddPackageReferenceCommand(Package package)
        {
            if (package == null)
                throw new ArgumentNullException(nameof(package));

            this._package = package;

            OleMenuCommandService commandService = CommandService();
            if (commandService != null)
            {
                var menuCommandId = new CommandID(CommandSet, CommandId);
                var menuItem = new OleMenuCommand(OnProjectContextMenuInvokeHandler, null,
                   OnProjectMenuBeforeQueryStatus, menuCommandId);
                commandService.AddCommand(menuItem);
            }
        }

        private OleMenuCommandService CommandService()
        {
            return ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static AddPackageReferenceCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider => _package;

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new AddPackageReferenceCommand(package);
        }


        private void OnProjectMenuBeforeQueryStatus(object sender, EventArgs e)
        {
            var menuCommand = sender as OleMenuCommand;
            if (menuCommand == null)
                return;


            var project = LocalUtils.GetActiveProject();
            if (project == null)
            {
                menuCommand.Visible = false;
                return;
            }

            if (project.Kind != GuidUtils.D365OperationsProject.ToString("B"))
            {
                menuCommand.Visible = false;
                return;
            }

        }

        private UpdateModelWizardDialog dialog;
        private void OnProjectContextMenuInvokeHandler(object sender, EventArgs e)
        {
            ModelInfo model = LocalUtils.GetActiveProjectNode().GetProjectsModelInfo();

            CreateModelInfo modelInfo = new CreateModelInfo();
            var pages = new ReadOnlyCollection<WizardPageBase>((IList<WizardPageBase>)new List<WizardPageBase>()
            {
                new UpdateModelParametersPageViewModel(modelInfo),
                new CreateModelSelectDependentModelsPageViewModel(modelInfo),
                new CreateModelSummaryPageViewModel(modelInfo)
            });

            var vm = new UpdateModelViewModel();
             vm.RequestClose += new EventHandler(this.UpdateModelViewModel_RequestClose);


            dialog = new UpdateModelWizardDialog();

            this.SetPrivatePropertyValue(dialog, "UpdateModelViewModel", vm);
            this.SetPrivatePropertyValue(vm, "CreateModelInfo", modelInfo);
            this.SetPrivateFieldValue(vm, "pages", pages);

            dialog.DataContext = vm;

            modelInfo.SelectedModel = model;
            this.SetPrivatePropertyValue(vm, "CurrentPage", vm.Pages[1]);

            dialog.ShowModal();

        }

        private void UpdateModelViewModel_RequestClose(object sender, EventArgs e)
        {
            bool flag = dialog.Result == null;
            if (dialog.Result != null)
            {
                try
                {
                    DesignMetaModelService.Instance.UpdateModel(dialog.Result.ToModelInfo());
                    flag = true;
                    (CoreUtility.ServiceProvider.GetService(typeof(IAgentWrapperService)) as IAgentWrapperService).RestartXppcAgent();
                }
                catch (Exception ex)
                {
                    CoreUtility.HandleExceptionWithErrorMessage(ex);
                }
            }
            if (!flag)
                return;
            dialog.Close();
        }

        public void SetPrivateFieldValue<T>(object obj, string propName, T val)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            Type t = obj.GetType();
            FieldInfo fi = null;
            while (fi == null && t != null)
            {
                fi = t.GetField(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                t = t.BaseType;
            }
            if (fi == null) throw new ArgumentOutOfRangeException("propName", string.Format("Field {0} was not found in Type {1}", propName, obj.GetType().FullName));
            fi.SetValue(obj, val);
        }

        public void SetPrivatePropertyValue<T>(object obj, string propName, T val)
        {
            Type t = obj.GetType();
            if (t.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) == null)
                throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));
            t.InvokeMember(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance, null, obj, new object[] { val });
        }

        private static void AddToProjectFromErrorList(object sender, EventArgs e)
        {

        }

        public static HashSet<string> CollectElementPathsFromToolWindowList(
            ToolWindowKind toolWindowKind)
        {
            HashSet<string> stringSet = new HashSet<string>();
            IVsTaskList2 vsTaskList2 = (IVsTaskList2)null;
            switch (toolWindowKind)
            {
                case ToolWindowKind.ErrorList:
                    vsTaskList2 = CoreUtility.ServiceProvider.GetService(typeof(SVsErrorList)) as IVsTaskList2;
                    break;
                case ToolWindowKind.TaskList:
                    vsTaskList2 = CoreUtility.ServiceProvider.GetService(typeof(SVsTaskList)) as IVsTaskList2;
                    break;
            }
            IVsEnumTaskItems ppEnum = (IVsEnumTaskItems)null;
            if (vsTaskList2 != null && ErrorHandler.Succeeded(vsTaskList2.EnumSelectedItems(out ppEnum)))
            {
                IVsTaskItem[] rgelt = new IVsTaskItem[1];
                uint[] pceltFetched = new uint[1];
                while (ppEnum.Next(1U, rgelt, pceltFetched) == 0)
                {
                    if (rgelt != null && ((IEnumerable<IVsTaskItem>)rgelt).Count<IVsTaskItem>() > 0 && rgelt[0] != null)
                    {
                        string pbstrMkDocument = (string)null;
                        if (ErrorHandler.Succeeded(rgelt[0].Document(out pbstrMkDocument)) && !string.IsNullOrWhiteSpace(pbstrMkDocument))
                        {
                            if (pbstrMkDocument.EndsWith(".xpp", StringComparison.OrdinalIgnoreCase))
                                stringSet.Add(CoreUtility.GetService<IMetadataInfoProvider>().GetXmlArtifactFilePath(pbstrMkDocument));
                            else if (pbstrMkDocument.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                                stringSet.Add(pbstrMkDocument);
                        }
                    }
                }
            }
            return stringSet;
        }





    }

}
