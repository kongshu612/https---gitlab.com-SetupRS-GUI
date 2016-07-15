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
using System.Diagnostics;
using System.IO;
using AsfStartUp.View;

namespace AsfStartUp.ViewModel
{
    public class HomePageViewModel:ViewModelBase
    {

        #region private members
        ViewModelBase _CurrentData;
        MainViewModel _mainViewData;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
            CreateShortCut();
        }
        #endregion

        #region private functions
        private bool CheckUpdate()
        {
            return Update.CheckUpdate();
        }
        public void InstallUpdate(bool mode=false)
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
                }
            }
            else if(Update.IsError)
            {
                return;
            }
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
        }
        private void LoadTreeNodeInfo(string seqxmlFile)
        {
            List<string> resolvedPath = ResolvePath(seqxmlFile);
            int length = seqxmlFile.Split('\\').Count();
            string TestsFolderPath = seqxmlFile.Remove(seqxmlFile.IndexOf('\\' + seqxmlFile.Split('\\')[length - 3]));
            _mainViewData.LoadTreeNodeInfo(TestsFolderPath);
            _mainViewData.SelectTargetNode(resolvedPath);
        }
        private List<string> ResolvePath(string seqxmlFile)
        {
            List<string> structs = new List<string>();
            List<string> total = seqxmlFile.Split('\\').ToList();
            for(int i=total.Count-3;i<total.Count;i++)
            {
                structs.Add(total[i]);
            }
            return structs;
        }

        private void OpenASequence()
        {
        //    System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Title = "Select the sequence env file: sequencexx_Env.xml";
         //   ofd.InitialDirectory = @"c:\";
            ofd.Filter = "Sequence env File (sequencex_Env.xml)|*sequence*_Env.xml";
            Nullable<bool> result =  ofd.ShowDialog();
            if(result==true)
            {
                CurrentData = _mainViewData;
                string seqEnvName = ofd.FileName.Split('\\').Last();
                LoadTreeNodeInfo(ofd.FileName.Remove(ofd.FileName.IndexOf("\\" + seqEnvName)));
            }
       //     System.Windows.Forms.DialogResult dr =  fbd.ShowDialog();
        }
        private void ExecuteRunCmd(string param)
        {
            CallSetUpRS_ViewModel csuvm = new CallSetUpRS_ViewModel();
            csuvm.ExecuteCommand(param);
        }
        //private void test()
        //{
        //    MainViewModel mvm = ServiceLocator.Current.GetInstance<MainViewModel>();
        //    mvm.SelectedNode = mvm.TreeNodes[0].ChildNodes[0].ChildNodes[0];
        //}
        //private void test2()
        //{
        //    MainViewModel mvm = ServiceLocator.Current.GetInstance<MainViewModel>();
        //    mvm.SelectedNode = mvm.TreeNodes[0].ChildNodes[0].ChildNodes[1];
        //}
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
                case "OpenASequence": OpenASequence(); break;
                case "JsonMode": ExecuteRunCmd("JsonMode"); break;
                case "DevMode": ExecuteRunCmd("DevMode"); break;
                case "CompleteASFRun": ExecuteRunCmd("CompleteASFRun"); break;
                case "Update": InstallUpdate(true); break;
              //  case "Aboutme":test2(); break;
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
