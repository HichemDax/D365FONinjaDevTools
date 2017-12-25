namespace LabelsHelper
{
    using System;
    using System.Linq;
    using System.ComponentModel.Composition;
    using Microsoft.Dynamics.AX.Metadata.Core;
    using Microsoft.Dynamics.Framework.Tools.Extensibility;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Forms;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Tables;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.BaseTypes;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Menus;
    using System.Globalization;
    using System.IO;
    using Microsoft.Dynamics.AX.Metadata.MetaModel;
    using Microsoft.Dynamics.AX.Metadata.Core.MetaModel;
    using XppDevs.AddinsDemo;
    using Microsoft.Dynamics.Framework.Tools.ProjectSystem;
    using Microsoft.Dynamics.AX.Metadata.Service;

    /// <summary>
    /// TODO: Say a few words about what your AddIn is going to do
    /// </summary>
    [Export(typeof(IDesignerMenu))]
    // TODO: This addin will show when user right clicks on a form root node or table root node. 
    // If you need to specify any other element, change this AutomationNodeType value.
    // You can specify multiple DesignerMenuExportMetadata attributes to meet your needs
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(BaseField))]
    public class SetAsIndex : DesignerMenuBase
    {
        #region Member variables
        private const string addinName = "Indexer";
        #endregion
      

        #region Properties
        /// <summary>[DesignerMenuExportMetadata(AutomationNodeType = typeof(ITable))]
        /// Caption for the menu item. This is what users would see in the menu.
        /// </summary>
        public override string Caption
        {
            get
            {
                return AddinResources.SetAsIndex;
            }
        }

        /// <summary>
        /// Unique name of the add-in
        /// </summary>
        public override string Name
        {
            get
            {
                return SetAsIndex.addinName;
            }
        }
        #endregion

        #region Callbacks
        /// <summary>
        /// Called when user clicks on the add-in menu
        /// </summary>
        /// <param name="e">The context of the VS tools and metadata</param>
        public override void OnClick(AddinDesignerEventArgs e)
        {
            
            try
            {
                var dialog = new CreateIndex();
                dialog.ShowDialog();

                var field = (BaseField)e.SelectedElement;

                AxTable axTable = (AxTable)field.Table.GetMetadataType();

                AxTableIndex index = new AxTableIndex();

                index.Name = dialog.CreateIndexVm.IndexName;
                index.AllowDuplicates = dialog.CreateIndexVm.AllowDuplicates ? NoYes.Yes : NoYes.No;
                index.AlternateKey = dialog.CreateIndexVm.AlternativeKey ? NoYes.Yes : NoYes.No;


                e.SelectedElements.OfType<BaseField>()
                    .ToList().ForEach(f => 
                    {
                        var fieldIndex = new AxTableIndexField()
                        {
                            DataField = f.Name,
                            Name = f.Name
                        };
                        index.AddField(fieldIndex);
                });

                // Add the method to the class
                axTable.AddIndex(index);

var source = $@"public static {axTable.Name} findBy{field.Name} ({field.ExtendedDataType} _{LocalUtils.toCamelCase(field.Name)}, boolean _forUpdate = false)
{{
    {axTable.Name} {LocalUtils.toCamelCase(axTable.Name)};

    {LocalUtils.toCamelCase(axTable.Name)}.selectForUpdate(_forUpdate);

    if(_{LocalUtils.toCamelCase(field.Name)})
    select firstonly {LocalUtils.toCamelCase(axTable.Name)} where {LocalUtils.toCamelCase(axTable.Name)}.{field.Name} == _{LocalUtils.toCamelCase(field.Name)};

    return {LocalUtils.toCamelCase(axTable.Name)};
}}";

                // Add the method to the class
                axTable.AddMethod(BuildMethod(field.Name,source));

                // Prepare objects needed for saving
                var metaModelProviders = ServiceLocator.GetService(typeof(IMetaModelProviders)) as IMetaModelProviders;
                var metaModelService = metaModelProviders.CurrentMetaModelService;
                // Getting the model will likely have to be more sophisticated, such as getting the model of the project and checking
                // if the object has the same model.
                // But this shold do for demonstration.
                ModelInfo model = DesignMetaModelService.Instance.CurrentMetadataProvider.Tables.GetModelInfo(axTable.Name).FirstOrDefault<ModelInfo>();

                // Update the file
                metaModelService.UpdateTable(axTable, new ModelSaveInfo(model));
            }
            catch (Exception ex)
            {
                CoreUtility.HandleExceptionWithErrorMessage(ex);
            }
        }


        #endregion
        private AxMethod BuildMethod(string fieldName, string source)
        {
            AxMethod axMethod = new AxMethod()
            {
                Name = $"findBy{fieldName}",
                Source = source
            };



            return axMethod;
        }


    }

  


}
