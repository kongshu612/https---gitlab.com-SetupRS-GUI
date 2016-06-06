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
using System.Windows.Shapes;
using Microsoft.Practices.ServiceLocation;
using AsfStartUp.ViewModel;

namespace AsfStartUp.View
{
    /// <summary>
    /// Interaction logic for BuildConfigureView.xaml
    /// </summary>
    public partial class BuildConfigureView : Window
    {
        public BuildConfigureView()
        {
            InitializeComponent();
        }

        private void win_Closed(object sender, EventArgs e)
        {
            BuildsConfigure_ViewModel bscvm = ServiceLocator.Current.GetInstance<BuildsConfigure_ViewModel>();
            ((BuildConfigure_ViewModel)bscvm.SelectedBuild).OnPropertyChanged();
        }
    }
}
