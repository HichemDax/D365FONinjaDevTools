//------------------------------------------------------------------------------
// <copyright file="AddItemCommand.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using D365FONinjaDevTools.Kernel;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
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

        private string _folderName;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddItemCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private AddItemCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this._package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandId = new CommandID(CommandSet, CommandId);
                var menuItem = new OleMenuCommand(OnProjectContextMenuInvokeHandler, null,
                   OnProjectMenuBeforeQueryStatus, menuCommandId);
                commandService.AddCommand(menuItem);
            }
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
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this._package;
            }
        }

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
            {
                return;
            }

            if (LocalUtils.GetActiveProject().Kind != "{fc65038c-1b2f-41e1-a629-bed71d161fff}")
            {
                
                menuCommand.Visible = false;
                return;
            }

            if (LocalUtils.MyDte?.SelectedItems.Count != 1)
            {
                return;
            }

            _folderName = LocalUtils.MyDte.SelectedItems.Item(1)?.ProjectItem?.Name;

            var typeTuple = FolderNameToElementTypeConverter.FindTypeFromFolderName(_folderName);

            var canAdd = !string.IsNullOrEmpty(_folderName)
                && typeTuple != null;

            menuCommand.Visible = canAdd;

            menuCommand.Text = "Add new";

        }

        private void OnProjectContextMenuInvokeHandler(object sender, EventArgs e)
        {
            var selectedItem = LocalUtils.MyDte.SelectedItems.Item(1)?.ProjectItem;
            var engin = new AotElementCreateEngin();
            try
            {
                var name = PromptForFileName();
                if (string.IsNullOrEmpty(name))
                    return;
                engin.AddAotElement(selectedItem, name);
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
