using System;
using System.ComponentModel.Composition;
using System.Linq;
using D365FONinjaDevTools.Kernel;
using Microsoft.Dynamics.AX.Metadata.Core.MetaModel;
using Microsoft.Dynamics.AX.Metadata.MetaModel;
using Microsoft.Dynamics.Framework.Tools.Extensibility;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Tables;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;

namespace D365FONinjaDevTools.SetIndex
{
    
    [Export(typeof(IDesignerMenu))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(BaseField))]
    public class SetAsIndex : DesignerMenuBase
    {
        #region Member variables

        private const string addinName = "Indexer";

        #endregion

        #region Callbacks

        /// <summary>
        ///     Called when user clicks on the add-in menu
        /// </summary>
        /// <param name="e">The context of the VS tools and metadata</param>
        public override void OnClick(AddinDesignerEventArgs e)
        {
            try
            {
                var field = (BaseField) e.SelectedElement;

                var dialog = new CreateIndex();
                dialog.CreateIndexVm.IndexName = field.Name + "_Idx";
                dialog.ShowDialog();


                var axTable = (AxTable) field.Table.GetMetadataType();

                var index = new AxTableIndex();

                index.Name = dialog.CreateIndexVm.IndexName;
                index.AllowDuplicates = dialog.CreateIndexVm.AllowDuplicates ? NoYes.Yes : NoYes.No;
                index.AlternateKey = dialog.CreateIndexVm.AlternativeKey ? NoYes.Yes : NoYes.No;


                e.SelectedElements.OfType<BaseField>()
                    .ToList().ForEach(f =>
                    {
                        var fieldIndex = new AxTableIndexField
                        {
                            DataField = f.Name,
                            Name = f.Name
                        };
                        index.AddField(fieldIndex);
                    });

                // Add the method to the class
                axTable.AddIndex(index);

                var fieldNameCamelCase = field.Name.ToCamelCase();
                var tableNameCamelCase = axTable.Name.ToCamelCase();

                var source =
                    $@"public static {axTable.Name} findBy{field.Name} ({field.ExtendedDataType} _{fieldNameCamelCase}, boolean _forUpdate = false)
                {{
                    {axTable.Name} {tableNameCamelCase};

                    {tableNameCamelCase}.selectForUpdate(_forUpdate);

                    if(_{fieldNameCamelCase})
                    select firstonly {tableNameCamelCase} where {tableNameCamelCase}.{field
                        .Name} == _{fieldNameCamelCase};

                    return {tableNameCamelCase};
                }}";


                axTable.AddMethod(BuildMethod(field.Name, source));
                var metaModelProviders = ServiceLocator.GetService(typeof(IMetaModelProviders)) as IMetaModelProviders;
                var metaModelService = metaModelProviders.CurrentMetaModelService;
               
                var model = DesignMetaModelService.Instance.CurrentMetadataProvider.Tables.GetModelInfo(axTable.Name)
                        .FirstOrDefault();

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
            var axMethod = new AxMethod
            {
                Name = $"findBy{fieldName}",
                Source = source
            };

            return axMethod;
        }

        #region Properties

        /// <summary>
        ///     [DesignerMenuExportMetadata(AutomationNodeType = typeof(ITable))]
        ///     Caption for the menu item. This is what users would see in the menu.
        /// </summary>
        public override string Caption
        {
            get { return "Set as index"; }
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