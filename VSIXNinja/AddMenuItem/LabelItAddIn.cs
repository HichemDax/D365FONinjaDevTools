using System;
using System.ComponentModel.Composition;
using D365FONinjaDevExtensions.AddItemsToFolder;
using Microsoft.Dynamics.Framework.Tools.Extensibility;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Classes;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Forms;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Reports;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;

namespace D365FONinjaDevExtensions.AddMenuItem
{
    [Export(typeof(IDesignerMenu))]
   
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(ClassItem))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IForm))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IReport))]
    public class AddMenuItem : DesignerMenuBase
    {
        #region Member variables

        private const string addinName = "DesignerMenuItemsHelper";
     
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
               
                if (e.SelectedElement is IForm)
                {
                    var form = e.SelectedElement as IForm;
                    AotElementCreate.CreateMenuItem(Dynamics.AX.Application.UtilElementType.DisplayTool,
                        form.Name, form.FormDesign.Caption);
                }
                if (e.SelectedElement is ClassItem)
                {
                    AotElementCreate.CreateMenuItem(Dynamics.AX.Application.UtilElementType.ActionTool,
                        (e.SelectedElement as ClassItem).Name);
                }
                if (e.SelectedElement is IReport)
                {
                    AotElementCreate.CreateMenuItem(Dynamics.AX.Application.UtilElementType.OutputTool,
                        (e.SelectedElement as IReport).Name);
                }



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
            get { return "Create menu item"; }
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