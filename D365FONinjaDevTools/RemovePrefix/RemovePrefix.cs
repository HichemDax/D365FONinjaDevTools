using System;
using System.ComponentModel.Composition;
using System.Linq;
using D365FONinjaDevTools.Parameters;
using Microsoft.Dynamics.AX.Metadata.MetaModel;
using Microsoft.Dynamics.Framework.Tools.Extensibility;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Tables;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;

namespace D365FONinjaDevTools.RemovePrefix
{
    [Export(typeof(IDesignerMenu))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(ITable))]
    public class RemovePrefix : DesignerMenuBase
    {
        #region Member variables

        private const string addinName = "TablePrefixRemover";

        #endregion

        #region Callbacks

        /// <summary>
        ///     Called when user clicks on the add-in menu
        /// </summary>
        /// <param name="e">The context of the VS tools and metadata</param>
        public override void OnClick(AddinDesignerEventArgs e)
        {
            ProjectParameters.Contruct();

            try
            {
                var metaModelProviders = CoreUtility.ServiceProvider.GetService(typeof(IMetaModelProviders)) as IMetaModelProviders;
                var metaModelService = metaModelProviders?.CurrentMetaModelService;


                var table = (ITable) e.SelectedElement;
                var axTable = (AxTable)table.GetMetadataType();
                var extension = ProjectParameters.Instance?.Extension;

                if (string.IsNullOrWhiteSpace(extension))
                    throw new Exception("Please specify the prefix in the setting");

                foreach (var axTableField in axTable.Fields)
                {
                    int index = axTableField.Name.IndexOf(extension, StringComparison.Ordinal);
                    string cleanPath = index < 0
                        ? axTableField.Name
                        : axTableField.Name.Remove(index, extension.Length);
                    axTableField.Name = cleanPath;
                }

                var model = DesignMetaModelService.Instance
                    .CurrentMetadataProvider.Tables
                    .GetModelInfo(axTable.Name)
                    .FirstOrDefault();

                metaModelService.UpdateTable(axTable, new ModelSaveInfo(model));
            }

            catch (Exception ex)
            {
                CoreUtility.HandleExceptionWithErrorMessage(ex);
            }
        }

       
        #endregion

        #region Properties

        /// <summary>
        ///     [DesignerMenuExportMetadata(AutomationNodeType = typeof(ITable))]
        ///     Caption for the menu item. This is what users would see in the menu.
        /// </summary>
        public override string Caption
        {
            get { return "Remove prefix"; }
        }

        /// <summary>
        ///     Unique name of the add-in
        /// </summary>
        public override string Name
        {
            get { return addinName; }
        }

        #endregion
    }
}