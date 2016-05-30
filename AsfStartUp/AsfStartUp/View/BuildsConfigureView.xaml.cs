using System;
using System.Collections.Generic;
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

namespace AsfStartUp.View
{
    /// <summary>
    /// Interaction logic for BuildsConfigureView.xaml
    /// </summary>
    public partial class BuildsConfigureView : UserControl
    {
        public BuildsConfigureView()
        {
            InitializeComponent();
        }
        private void ListViewItem_MouseDoubleClick(object sender,MouseButtonEventArgs e)
        {
            BuildConfigureView bcv = new BuildConfigureView();
            bcv.DataContext = lst_Builds.SelectedItem;
            bcv.Owner = Application.Current.MainWindow;
            bcv.ShowDialog();
        }
    }
}
