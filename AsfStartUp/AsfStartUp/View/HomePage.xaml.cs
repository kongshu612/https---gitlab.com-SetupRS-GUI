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
using AsfStartUp.ViewModel;
using System.Threading;

namespace AsfStartUp.View
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        public HomePage()
        {
            InitializeComponent();
        }
        void win_FirstRun(object sender, EventArgs e)
        {
            HomePageViewModel hpvm = this.DataContext as HomePageViewModel;
            Thread t = new Thread(()=>hpvm.InstallUpdate());
            t.Start();
        }
    }
}
