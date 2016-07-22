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
using System.IO;
using System.Diagnostics;

namespace AsfStartUp.View
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

        private void win_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            string localTmpFolder = System.IO.Path.Combine(Directory.GetParent(Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString()).ToString(), "ASFStartUpNew");
            if (Directory.Exists(localTmpFolder))
            {
                string updateScript = System.IO.Path.Combine(localTmpFolder, "update.ps1");
                ProcessStartInfo psi = new ProcessStartInfo("powershell.exe");
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                psi.WorkingDirectory = Directory.GetParent(Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString()).ToString();
                psi.Arguments = updateScript + "-restart $false";
                log.DebugFormat("call powershell with parameters: [ {0}]", updateScript);
                Process powershellInstance = Process.Start(psi);
            }
        }
    }
}
