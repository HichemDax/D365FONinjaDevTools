//------------------------------------------------------------------------------
// <copyright file="AddItemCommand.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using D365FONinjaDevTools.Kernel;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
using Microsoft.Dynamics.Framework.Tools.ProjectSystem.Wizards.WorkflowWizard;
using Microsoft.VisualStudio.Shell;
using Exception = System.Exception;


namespace D365FONinjaDevTools.AddItemsToFolder
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class AddItemCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;
        
        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("5ed27ab2-7007-4c3d-a535-88720e97b49f");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package _package;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddItemCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private AddItemCommand(Package package)
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
        public static AddItemCommand Instance
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
            Instance = new AddItemCommand(package);
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
            if (LocalUtils.MyDte?.SelectedItems.Count != 1)
                return;

            var projectItem = LocalUtils.MyDte.SelectedItems.Item(1).ProjectItem;

            var canShow = AotElementCreateEngin.CheckIsMatch(projectItem);

            menuCommand.Visible = canShow;

        }

        private void OnProjectContextMenuInvokeHandler(object sender, EventArgs e)
        {
            

            var selectedItem = LocalUtils.MyDte.SelectedItems.Item(1)?.ProjectItem;
           
            try
            {
                var name = PromptForFileName();
                if (string.IsNullOrEmpty(name))
                    return;
                AotElementCreateEngin.AddAotElement(selectedItem, name);
            }
            catch (Exception ex)
            {
                CoreUtility.HandleExceptionWithErrorMessage(ex);
            }
        }


        private string PromptForFileName()
        {
            var dialog = new FileNameDialog();
            var result = dialog.ShowDialog();
            return result.HasValue && result.Value ? dialog.Input : string.Empty;
        }
    }
}
