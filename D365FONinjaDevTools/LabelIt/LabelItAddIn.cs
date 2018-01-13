using System;
using System.ComponentModel.Composition;
using D365FONinjaDevTools.Kernel;
using D365FONinjaDevTools.Parameters;
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
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(Menu.IMenuItemDisplay))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IBaseEnum))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IBaseEnumValue))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(Menu.IMenu))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IBaseField))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IFieldContainer))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IFieldDate))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IFieldEnum))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IFieldGroup))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IFieldGuid))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IFieldInt))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IFieldReal))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IFieldTime))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IFieldUtcDateTime))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(BaseField))]
    public class LabelItAddin : DesignerMenuBase
    {
        #region Member variables

        private const string addinName = "DesignerLabelsHelper";

        #endregion

        #region Callbacks
        
        public override void OnClick(AddinDesignerEventArgs e)
        {
            if (ProjectParameters.Instance == null)
                ProjectParameters.Contruct();

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

                if (e.SelectedElement is IBaseField)
                {
                    var edt = e.SelectedElement as IBaseField;
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

        public override string Caption
        {
            get { return "Label it"; }
        }

        public override string Name
        {
            get { return addinName; }
        }

        #endregion
    }
}