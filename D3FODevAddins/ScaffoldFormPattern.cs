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
    using Microsoft.Dynamics.AX.Metadata.Extensions.TreeNode.Nodes;

    /// <summary>
    /// TODO: Say a few words about what your AddIn is going to do
    /// </summary>
    [Export(typeof(IDesignerMenu))]
    // TODO: This addin will show when user right clicks on a form root node or table root node. 
    // If you need to specify any other element, change this AutomationNodeType value.
    // You can specify multiple DesignerMenuExportMetadata attributes to meet your needs
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IForm))]
    public class FormPatternScaffolder : DesignerMenuBase
    {
        #region Member variables
        private const string addinName = "FormPatternScaffolder";
        #endregion
      

        #region Properties
        /// <summary>[DesignerMenuExportMetadata(AutomationNodeType = typeof(ITable))]
        /// Caption for the menu item. This is what users would see in the menu.
        /// </summary>
        public override string Caption
        {
            get
            {
                return AddinResources.ScaffoldFormControls;
            }
        }

        /// <summary>
        /// Unique name of the add-in
        /// </summary>
        public override string Name
        {
            get
            {
                return FormPatternScaffolder.addinName;
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
                var metaModelProviders = ServiceLocator.GetService(typeof(IMetaModelProviders)) as IMetaModelProviders;
                var metaModelService = metaModelProviders.CurrentMetaModelService;

                IForm form = (IForm)e.SelectedElement;
                AxForm axForm = (AxForm)form.GetMetadataType();

                switch (axForm.Design.Pattern)
                {
                    case "SimpleList":
                    case "ListPage":
                        var filterGroup = new AxFormGroupControl() { Name = "FilterGroup", Pattern = "CustomAndQuickFilters" };
                        filterGroup.AddControl(new AxFormControl { Name = "QuickFilter"  ,FormControlExtension = new AxFormControlExtension() { Name = "QuickFilterControl" } });
                        axForm.Design.AddControl(new AxFormActionPaneControl() { Name = "MainActionPane" });
                        axForm.Design.AddControl(filterGroup);
                        axForm.Design.AddControl(new AxFormGridControl() { Name = "MainGrid" });
                        AxTable table = metaModelService.GetTable(form.Name);
                        if (table != null)
                        {
                            var ds = new AxFormDataSourceRoot { Name = table.Name, Table = table.Name };
                            axForm.DataSources.Add(ds);
                        }

                        break;
                    default:
                        break;
                }
                

                
                ModelInfo model = DesignMetaModelService.Instance.CurrentMetadataProvider.Forms.GetModelInfo(axForm.Name).FirstOrDefault();
                metaModelService.UpdateForm(axForm, new ModelSaveInfo(model));
                
            }
                
            catch (Exception ex)
            {
                CoreUtility.HandleExceptionWithErrorMessage(ex);
            }
        }


        #endregion

       

    }

  


}
