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
    using XppDevs.AddinsDemo;
    using Microsoft.Dynamics.Framework.Tools.ProjectSystem;
    using Microsoft.Dynamics.AX.Metadata.Service;
    using System.Text.RegularExpressions;

    /// <summary>
    /// TODO: Say a few words about what your AddIn is going to do
    /// </summary>
    [Export(typeof(IDesignerMenu))]
    // TODO: This addin will show when user right clicks on a form root node or table root node. 
    // If you need to specify any other element, change this AutomationNodeType value.
    // You can specify multiple DesignerMenuExportMetadata attributes to meet your needs
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(ITable))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IForm))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IEdtBase))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IFormControl))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IMenuItemDisplay))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IBaseEnum))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IBaseEnumValue))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Menus.IMenu))]


    public class LabelItAddin : DesignerMenuBase
    {
        #region Member variables
        private const string addinName = "DesignerLabelsHelper";
        #endregion
      

        #region Properties
        /// <summary>[DesignerMenuExportMetadata(AutomationNodeType = typeof(ITable))]
        /// Caption for the menu item. This is what users would see in the menu.
        /// </summary>
        public override string Caption
        {
            get
            {
                return AddinResources.LabelIt;
            }
        }

        /// <summary>
        /// Unique name of the add-in
        /// </summary>
        public override string Name
        {
            get
            {
                return LabelItAddin.addinName;
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
                
                if (e.SelectedElement is ITable)
                {
                    var table = e.SelectedElement as ITable;
                    table.Label = AddinsUtility.Convert(table.Name);
                }
                if (e.SelectedElement is IForm)
                {
                    var form = e.SelectedElement as IForm;
                    form.FormDesign.Caption = AddinsUtility.Convert(form.Name);
                }

                if (e.SelectedElement is IFormControl)
                {
                    var control = e.SelectedElement as IFormControl;
                    control.FormDesign.Caption = AddinsUtility.Convert(control.Name);
                }

                if (e.SelectedElement is IBaseEnum)
                {
                    var @enum = e.SelectedElement as IBaseEnum;
                    @enum.Label = AddinsUtility.Convert(@enum.Name);
                }
                if (e.SelectedElement is IEdtBase)
                {
                    var edt = e.SelectedElement as IEdtBase;
                    edt.Label = AddinsUtility.Convert(edt.Name);
                }


                if (e.SelectedElement is IMenuItemDisplay)
                {
                    var menu = e.SelectedElement as IMenuItemDisplay;
                    menu.Label = AddinsUtility.Convert(menu.Name);
                }

                if (e.SelectedElement is IMenuItemOutput)
                {
                    var menu = e.SelectedElement as IMenuItemOutput;
                    menu.Label = AddinsUtility.Convert(menu.Name);
                }

                if (e.SelectedElement is Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Menus.IMenu)
                {
                    var menu = e.SelectedElement as Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Menus.IMenu;
                    menu.Label = AddinsUtility.Convert(menu.Name);
                }

                if (e.SelectedElement is IBaseEnumValue)
                {
                    var menu = e.SelectedElement as IBaseEnumValue;
                    menu.Label = AddinsUtility.Convert(menu.Name);
                }

            }
            catch (Exception ex)
            {
                CoreUtility.HandleExceptionWithErrorMessage(ex);
            }
        }


        #endregion

       

    }

    public static class AddinsUtility
    {
        public static VSProjectNode Project;
        public static IMetaModelProviders MetaModelProviders;
        public static IMetaModelService MetaModelService;

      
        public static string Convert(this string name)
        {
            Project = LocalUtils.GetActiveProject();
            MetaModelProviders = ServiceLocator.GetService(typeof(IMetaModelProviders)) as IMetaModelProviders;
            MetaModelService = MetaModelProviders.CurrentMetaModelService;

            string extension;
            if (string.IsNullOrEmpty(name))
                throw new InvalidOperationException($"Cannot convert '{name}'");
          

            using (StreamReader sr = new StreamReader($"C:\\{Project.Name}ProjectPrefix.txt"))
            {
                extension = sr.ReadLine();
            };

            string englishLabelFile = MetaModelService.GetLabelFileNames().FirstOrDefault(lableFileName => lableFileName.StartsWith(extension) && lableFileName.Contains("en-US"));

            AxLabelFile lableFile = MetaModelService.GetLabelFile(englishLabelFile);
            var labelKey = name.Replace(extension,"");

            var lableTxt = System.Text.RegularExpressions.Regex.Replace(labelKey, "((?<=[a-z])[A-Z]|[A-Z](?=[a-z]))", " $1").Trim().ToLower().UppercaseFirst();

            AppendLabelFile(lableFile, labelKey, lableTxt);


            return $"@{extension}Labels:{labelKey}";
        }

        static string UppercaseFirst(this string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        static void AppendLabelFile(AxLabelFile labelFile,string labelKey , string text)
        {
            labelFile.LocalPath();

           // CoreUtility.DisplayInfo(labelFile.LocalPath());
            
            bool lableExists;
            using (StreamReader sr = new StreamReader(labelFile.LocalPath()))
            {
                var labelsTxt = sr.ReadToEnd();
                lableExists = Regex.IsMatch(labelKey, "\\bthe\\b");
            }

            if (lableExists)
                return;
            using (StreamWriter sw = File.AppendText(labelFile.LocalPath()))
            {
                sw.WriteLine($"{labelKey}={text}");
            }

            
        }

    }


}
