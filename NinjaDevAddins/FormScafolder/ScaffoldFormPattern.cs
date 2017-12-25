using System;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Dynamics.AX.Metadata.MetaModel;
using Microsoft.Dynamics.Framework.Tools.Extensibility;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Forms;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;

namespace D365FONinjaDevAddins.FormScafolder
{
    [Export(typeof(IDesignerMenu))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IForm))]
    public class FormPatternScaffolder : DesignerMenuBase
    {
        #region Member variables

        private const string addinName = "FormPatternScaffolder";

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
                var metaModelProviders = ServiceLocator.GetService(typeof(IMetaModelProviders)) as IMetaModelProviders;
                var metaModelService = metaModelProviders.CurrentMetaModelService;


                var form = (IForm) e.SelectedElement;
                var axForm = (AxForm) form.GetMetadataType();


                switch (axForm.Design.Pattern)
                {
                    case "SimpleList":
                    case "ListPage":

                        axForm.Design.AddControl(new AxFormActionPaneControl {Name = "MainActionPane"});
                        axForm.Design.AddControl(GetFilterGroup());
                        axForm.Design.AddControl(new AxFormGridControl {Name = "MainGrid"});
                        //var table = metaModelService.GetTable(form.Name);
                       /* if (table != null)
                        {
                            var ds = new AxFormDataSourceRoot {Name = table.Name, Table = table.Name};
                            axForm.DataSources.Add(ds);
                        }
*/
                        break;

                    
                    case "DetailsMaster":

                        //<Main>
                        axForm.Design.AddControl(new AxFormActionPaneControl {Name = "MainActionPane"});
                        var myNavigationListGroup = new AxFormGroupControl {Name = "NavigationListGroup"};

                        // <NavigationListGroup>
                        myNavigationListGroup.AddControl(new AxFormControl
                        {
                            Name = "NavListQuickFilter",
                            FormControlExtension = new AxFormControlExtension { Name = "QuickFilterControl" }
                        });

                        myNavigationListGroup.AddControl(new AxFormGridControl {Name = "MainGrid"});
                        // </NavigationListGroup>

                        axForm.Design.AddControl(myNavigationListGroup);
                        var myPanelTab = new AxFormTabControl {Name = "PanelTab"};


                        var gridPanel = new AxFormTabPageControl {Name = "GridPanel"};
                        gridPanel.AddControl(GetFilterGroup("DetailsFilterGroup", "DetailsQuickFilter"));
                        gridPanel.AddControl(new AxFormGridControl {Name = "DetailsGrid"});
                        gridPanel.AddControl(new AxFormCommandButtonControl {Name = "DetailsButtonCommand"});

                        var detailsPanel = new AxFormTabPageControl {Name = "DetailsPanel"};

                        var titleGroup = new AxFormGroupControl {Name = "TitleGroup"};
                            titleGroup.AddControl(new AxFormStringControl {Name = "HeaderTitle"});

                        detailsPanel.AddControl(titleGroup);

                        var detailsPanelTab = new AxFormTabControl { Name = "DetailsTab" };
                        detailsPanelTab.AddControl(new AxFormTabPageControl {Name = "DetailsTabPage"});
                        detailsPanel.AddControl(detailsPanelTab);

                        myPanelTab.AddControl(detailsPanel);
                        myPanelTab.AddControl(gridPanel);
                        axForm.Design.AddControl(myPanelTab);
                        //</Main>

                        break;

                    case "SimpleListDetails":
                        //<Main>
                        axForm.Design.AddControl(new AxFormActionPaneControl { Name = "MainActionPane" });
                        var myNavigationListGroup1 = new AxFormGroupControl { Name = "NavigationListGroup" };

                        // <NavigationListGroup>
                        myNavigationListGroup1.AddControl(new AxFormControl
                        {
                            Name = "NavListQuickFilter",
                            FormControlExtension = new AxFormControlExtension { Name = "QuickFilterControl" }
                        });
                        myNavigationListGroup1.AddControl(new AxFormGridControl { Name = "MainGrid" });
                        // </NavigationListGroup>

                        axForm.Design.AddControl(myNavigationListGroup1);

                        var detailsHeader = new AxFormGroupControl { Name = "DetailsHeaderGroup" };
                        axForm.Design.AddControl(detailsHeader);

                        var myPanelTab1 = new AxFormTabControl { Name = "DetailsTab" };

                        myPanelTab1.AddControl(new AxFormTabPageControl {Name = "DetailsTabPage" });
                        axForm.Design.AddControl(myPanelTab1);

                        //</Main>

                        break;
                }


                var model =
                    DesignMetaModelService.Instance.CurrentMetadataProvider.Forms.GetModelInfo(axForm.Name)
                        .FirstOrDefault();


                metaModelService.UpdateForm(axForm, new ModelSaveInfo(model));
            }

            catch (Exception ex)
            {
                CoreUtility.HandleExceptionWithErrorMessage(ex);
            }
        }

        private static AxFormGroupControl GetFilterGroup(string filterGroup = "FilterGroup",
            string quickFilter = "QuickFilter")
        {
            var filterGrp = new AxFormGroupControl
            {
                Name = filterGroup,
                Pattern = "CustomAndQuickFilters"
            };
            filterGrp.AddControl(new AxFormControl
            {
                Name = quickFilter,
                FormControlExtension = new AxFormControlExtension {Name = "QuickFilterControl" }
            });
            return filterGrp;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     [DesignerMenuExportMetadata(AutomationNodeType = typeof(ITable))]
        ///     Caption for the menu item. This is what users would see in the menu.
        /// </summary>
        public override string Caption
        {
            get { return AddinResources.ScaffoldFormControls; }
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