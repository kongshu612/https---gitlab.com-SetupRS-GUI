using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;
using AsfStartUp.Auxiliary;
using System.Windows;
<<<<<<< HEAD
using System.Diagnostics;
using System.IO;
using AsfStartUp.View;
=======
>>>>>>> origin/master

namespace AsfStartUp.ViewModel
{
    public class HomePageViewModel:ViewModelBase
    {
<<<<<<< HEAD

        #region private members
        ViewModelBase _CurrentData;
        MainViewModel _mainViewData;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
=======
        #region private members
        ViewModelBase _CurrentData;
        MainViewModel _mainViewData;
>>>>>>> origin/master
        #endregion

        #region public properties
        public ViewModelBase CurrentData
        {
            get
            {
                return _CurrentData;
            }
            set
            {
                _CurrentData = value;
                RaisePropertyChanged("CurrentData");
            }
        }
        #endregion

        #region Constructors
        public HomePageViewModel()
        {
            _mainViewData = ServiceLocator.Current.GetInstance<MainViewModel>();
<<<<<<< HEAD
            CreateShortCut();
=======
>>>>>>> origin/master
        }
        #endregion

        #region private functions
        private bool CheckUpdate()
        {
            return Update.CheckUpdate();
        }
<<<<<<< HEAD
        public void InstallUpdate(bool mode=false)
=======
        public void InstallUpdate()
>>>>>>> origin/master
        {
            if (Update.CheckUpdate())
            {
                if (Update.IsForce)
                {
                    if (MessageBox.Show("Please Update software, Yes: Update online, No: exit this app", "Update Software", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        Application.Current.Shutdown();
                    }
                }
                else if (MessageBox.Show("New Version Found, Do you want to update it?", "Update Software", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    return;
                }
<<<<<<< HEAD
                if(Update.InstallUpdate())
                {
                    string localTmpFolder = Path.Combine(Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).Parent.ToString(), "ASFStartUpTmp");
                    string updateScript = Path.Combine(localTmpFolder, "update.ps1");
                    ProcessStartInfo psi = new ProcessStartInfo("powershell.exe");
                    psi.WindowStyle = ProcessWindowStyle.Hidden;
                    psi.Arguments = updateScript;
                    log.DebugFormat("call powershell with parameters: [ {0}]", updateScript);
                    Process powershellInstance = Process.Start(psi);
                    Application.Current.Shutdown();
=======
                if (!Update.InstallUpdate())
                {
                    MessageBox.Show("Error Occur, while update");
                    return;
>>>>>>> origin/master
                }
            }
            else if(Update.IsError)
            {
                return;
            }
<<<<<<< HEAD
            else if(mode)
            {
                MessageBox.Show("No Update Found","Update Info");
            }
        }

        private void ShowAboutme()
        {
            Aboutme am = new Aboutme();
            am.Owner = Application.Current.MainWindow;
            am.ShowDialog();
        }
        private void CreateShortCut()
        {
            string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string shortCutFullPath = deskDir + "\\ASFStartUp.url";
            if(File.Exists(shortCutFullPath))
            {
                log.Info("ShortCut already created.");
                return;
            }
            using (StreamWriter writer = new StreamWriter(shortCutFullPath))
            {
                string app = System.Reflection.Assembly.GetExecutingAssembly().Location;
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine("URL=file:///" + app);
                writer.WriteLine("IconIndex=0");
                string icon = app.Replace('\\', '/');
                writer.WriteLine("IconFile=" + icon);
                writer.Flush();
            }
            return;
=======
            else
            {
                MessageBox.Show("No Update Found");
            }
>>>>>>> origin/master
        }
        #endregion

        #region public Commands
        private bool CanExecuteMenuCommand(string param)
        {
            return true;
        }
        private void ExecuteMenuCommand(string param)
        {
            switch(param)
            {
                case "SetUp": CurrentData = _mainViewData; break;
<<<<<<< HEAD
                case "Update": InstallUpdate(true); break;
                case "Aboutme":ShowAboutme(); break;
=======
                case "Update": InstallUpdate(); break;
>>>>>>> origin/master
            }
        }
        private ICommand _MenuCommand;
        public ICommand MenuCommand
        {
            get
            {
                _MenuCommand = _MenuCommand ?? new RelayCommand<string>(ExecuteMenuCommand, CanExecuteMenuCommand);
                return _MenuCommand;
            }
        }
        #endregion
    }
}
