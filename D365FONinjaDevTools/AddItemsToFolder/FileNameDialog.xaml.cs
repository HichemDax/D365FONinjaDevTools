using System.Windows;

namespace D365FONinjaDevTools.AddItemsToFolder
{
    public partial class FileNameDialog : Window
    {
        public FileNameDialog()
        {
            InitializeComponent();

            txtName.Focus();
        }

        public string Input => txtName.Text.Trim();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
