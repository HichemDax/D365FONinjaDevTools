using System;
using System.Windows.Forms;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;

namespace D365FONinjaDevTools.Parameters
{
    public partial class Parameters : Form
    {
        

        public Parameters()
        {
            InitializeComponent();

            try
            {
                ProjectParameters.Contruct();

                ProjectExtensionTB.DataBindings.Add(nameof(ProjectExtensionTB.Text), ProjectParameters.Instance,
                    nameof(ProjectParameters.Instance.Extension), false,
                    DataSourceUpdateMode.OnPropertyChanged);
            }
            catch (Exception ee)
            {
                CoreUtility.HandleExceptionWithErrorMessage(ee);
            }
        }

        public string Xml { get; set; }

        private void SaveParameters_Click(object sender, EventArgs e)
        {
            ProjectParameters.Instance.Save();
        }
    }
}