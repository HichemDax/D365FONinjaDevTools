//------------------------------------------------------------------------------
// <copyright file="ExtendElementsCommand.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Globalization;
using D365FONinjaDevTools.AddItemsToFolder;
using D365FONinjaDevTools.Kernel;
using Microsoft.Dynamics.AX.Metadata.MetaModel;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
using Microsoft.Dynamics.Framework.Tools.ProjectSupport.Automation;
using Microsoft.Dynamics.Framework.Tools.ProjectSystem;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace D365FONinjaDevTools.ExtendElement
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class ExtendElementsCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0200;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("5ed27ab2-7007-4c3d-a535-88720e97b49f");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendElementsCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private ExtendElementsCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;


            OleMenuCommandService commandService =
                this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
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
        public static ExtendElementsCommand Instance { get; private set; }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get { return this.package; }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new ExtendElementsCommand(package);
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
            {
                menuCommand.Visible = false;
                return;
            }

            if (!(LocalUtils.MyDte.SelectedItems.Item(1).ProjectItem is OAVSProjectFileItem))
            {
                menuCommand.Visible = false;
                return;
            }

            OAVSProjectFileItem _class = LocalUtils.MyDte.SelectedItems.Item(1).ProjectItem as OAVSProjectFileItem;
            dynamic property = (_class?.Object as VSProjectFileNode)?.NodeProperties;
            menuCommand.Visible = property?.ItemType == "Class Item";
        }

        private void OnProjectContextMenuInvokeHandler(object sender, EventArgs e)
        {
            dynamic toExtendElement = LocalUtils.MyDte.SelectedItems.Item(1)?.ProjectItem;
            try
            {
                var name = PromptForFileName();
                if (string.IsNullOrEmpty(name))
                    return;
                AotElementCreate.CreateExtension(name, toExtendElement);
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