using System;
using System.ComponentModel.Composition;
using D365FONinjaDevTools.Kernel;
using Microsoft.Dynamics.Framework.Tools.Extensibility;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.BaseTypes;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Forms;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Tables;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;

namespace D365FONinjaDevTools.LabelIt
{
    using Menu = Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Menus;


    [Export(typeof(IDesignerMenu))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(ITable))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IForm))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IEdtBase))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IFormControl))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(Menu.IMenuItemDisplay))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IBaseEnum))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IBaseEnumValue))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(Menu.IMenu))]
    public class LabelItAddin : DesignerMenuBase
    {
        #region Member variables

        private const string addinName = "DesignerLabelsHelper";
     
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
                if (e.SelectedElement is ITable)
                {
                    var table = e.SelectedElement as ITable;
                    table.Label = table.Name.Convert();
                }
                if (e.SelectedElement is IForm)
                {
                    var form = e.SelectedElement as IForm;
                    form.FormDesign.Caption = form.Name.Convert();
                }

                if (e.SelectedElement is IFormControl)
                {
                    var control = e.SelectedElement as IFormControl;
                    control.FormDesign.Caption = control.Name.Convert();
                }

                if (e.SelectedElement is IBaseEnum)
                {
                    var @enum = e.SelectedElement as IBaseEnum;
                    @enum.Label = @enum.Name.Convert();
                }
                if (e.SelectedElement is IEdtBase)
                {
                    var edt = e.SelectedElement as IEdtBase;
                    edt.Label = edt.Name.Convert();
                }


                if (e.SelectedElement is Menu.IMenuItemDisplay)
                {
                    var menu = e.SelectedElement as Menu.IMenuItemDisplay;
                    menu.Label = menu.Name.Convert();
                }

                if (e.SelectedElement is Menu.IMenuItemOutput)
                {
                    var menu = e.SelectedElement as Menu.IMenuItemOutput;
                    menu.Label = menu.Name.Convert();
                }

                if (e.SelectedElement is Menu.IMenu)
                {
                    var menu = e.SelectedElement as Menu.IMenu;
                    menu.Label = menu.Name.Convert();
                }

                if (e.SelectedElement is IBaseEnumValue)
                {
                    var menu = e.SelectedElement as IBaseEnumValue;
                    menu.Label = menu.Name.Convert();
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
            get { return "Label it"; }
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