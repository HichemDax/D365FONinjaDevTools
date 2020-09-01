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
using Microsoft.Dynamics.Framework.Tools.ProjectSystem.Build.ViewModel;
using Microsoft.Dynamics.Framework.Tools.ProjectSystem.Build.View;

namespace D365FONinjaDevTools.AddPackageReference
{
    internal sealed class BuildCurrentModelCommand
    {

        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0X0400;

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
        private BuildCurrentModelCommand(Package package)
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
        public static BuildCurrentModelCommand Instance
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
            Instance = new BuildCurrentModelCommand(package);
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

        private void OnProjectContextMenuInvokeHandler(object sender, EventArgs e)
        {
            ModelInfo myModel = LocalUtils.GetActiveProjectNode().GetProjectsModelInfo();
            BuildModelViewModel buildVm = new BuildModelViewModel();
            BuildModelInfoViewModel modelVm = buildVm.Models.FirstOrDefault(model => model.ModelInfo.DisplayName == myModel.DisplayName);
            modelVm.IsChecked = true;
            ConstructorInfo[] constructors = typeof(BuildModelView).GetConstructors(
                            BindingFlags.NonPublic | BindingFlags.Instance);
            BuildModelView buildView = (BuildModelView)constructors[0].Invoke( new []{ buildVm } );
            buildView.ShowModal();
        }

        





    }

}
