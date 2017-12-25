using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XppDevs.AddinsDemo
{
    public partial class CreateIndex : Form
    {
        public CreateIndex()
        {
            InitializeComponent();
            CreateIndexVm = new CreateIndexVm();
            IndexTxt.DataBindings.Add(nameof(IndexTxt.Text), CreateIndexVm, nameof(CreateIndexVm.IndexName),false,
                                DataSourceUpdateMode.OnPropertyChanged);
            AllowDuplicate.DataBindings.Add(nameof(AllowDuplicate.Checked), CreateIndexVm, nameof(CreateIndexVm.AllowDuplicates), false,
                                DataSourceUpdateMode.OnPropertyChanged);
            AlternateKey.DataBindings.Add(nameof(AlternateKey.Checked), CreateIndexVm, nameof(CreateIndexVm.AlternativeKey), false,
                                DataSourceUpdateMode.OnPropertyChanged);

            CreateIndexVm.IndexName = "_Idx";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public bool CreateConfirmed { get; set; }
        public CreateIndexVm CreateIndexVm { get; set; }

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
