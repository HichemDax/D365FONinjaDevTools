using System;
using System.Windows.Forms;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;

namespace D365FONinjaDevTools.SetIndex
{
    public partial class CreateIndex : Form
    {
        public CreateIndex()
        {
            InitializeComponent();
            CreateIndexVm = new CreateIndexVm();
            IndexTxt.DataBindings.Add(nameof(IndexTxt.Text), CreateIndexVm, nameof(CreateIndexVm.IndexName), false,
                DataSourceUpdateMode.OnPropertyChanged);
            AllowDuplicate.DataBindings.Add(nameof(AllowDuplicate.Checked), CreateIndexVm,
                nameof(CreateIndexVm.AllowDuplicates), false,
                DataSourceUpdateMode.OnPropertyChanged);
            AlternateKey.DataBindings.Add(nameof(AlternateKey.Checked), CreateIndexVm,
                nameof(CreateIndexVm.AlternativeKey), false,
                DataSourceUpdateMode.OnPropertyChanged);
        }

        public bool CreateConfirmed { get; set; }
        public CreateIndexVm CreateIndexVm { get; set; }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(IndexTxt.Text))
            {
                CoreUtility.DisplayError("Index name invalid");
                return;
            }

            CreateConfirmed = true;
            Close();
        }
    }


    public class CreateIndexVm
    {
        public string IndexName { get; set; }
        public bool AllowDuplicates { get; set; }
        public bool AlternativeKey { get; set; }
    }
}