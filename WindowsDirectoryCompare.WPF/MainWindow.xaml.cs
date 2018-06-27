using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WindowsDirectoryCompare.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string directory1, directory2;
        Gat.Controls.OpenDialogView openDialog;
        Gat.Controls.OpenDialogViewModel vm;

        public MainWindow()
        {
            InitializeComponent();

            openDialog = new Gat.Controls.OpenDialogView();
            vm = (Gat.Controls.OpenDialogViewModel)openDialog.DataContext;
            vm.IsDirectoryChooser = true;
        }

        private void btnDirectoryBrowse1_Click(object sender, RoutedEventArgs e)
        {
            bool? result = vm.Show();
            if (result == true)
            {
                // Get selected file path
                directory1 = vm.SelectedFilePath;
                txtDirectory1.Text = directory1;
            }
            else
            {
                directory1 = string.Empty;
            }
        }

        private void btnDirectoryBrowse2_Click(object sender, RoutedEventArgs e)
        {
            bool? result = vm.Show();
            if (result == true)
            {
                // Get selected file path
                directory2 = vm.SelectedFilePath;
                txtDirectory2.Text = directory2;
            }
            else
            {
                directory2 = string.Empty;
            }

            string[] subDirectories1 = Directory.GetDirectories(directory1, "*", SearchOption.AllDirectories);
            string[] subDirectories2 = Directory.GetDirectories(directory2, "*", SearchOption.AllDirectories);
            List<string> sd1List = new List<string>();
            List<string> sd2List = new List<string>();

            foreach (string s in subDirectories1)
            {
                sd1List.Add(s.Remove(0, directory1.Length + 1));
            }

            foreach (string s in subDirectories2)
            {
                sd2List.Add(s.Remove(0, directory2.Length + 1));
            }

            var subDirectories1Differences = sd1List.Except(sd2List);
            var subDirectories2Differences = sd2List.Except(sd1List);

            lbxDirectory1.ItemsSource = subDirectories1Differences;
            lbxDirectory2.ItemsSource = subDirectories2Differences;
        }
    }
}
