using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using System.Diagnostics;
using System.IO;
using System.Windows;
using AsfStartUp.Auxiliary;
using System.Windows.Input;
using AsfStartUp.View;
using System.Threading;

namespace AsfStartUp.ViewModel
{
    public class CallSetUpRS_ViewModel:ViewModelBase
    {
        private string ASFRootPath;
        private string FilePath;
        private string SequenceNumber;
        private string TestSuite;
        private string jsonFileFolder;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void ExecuteCommand(string param)
        {
            Thread t = new Thread(() => this.backgroundWorker(param));
            t.Start();
            if (param == "JsonMode")
                ShowProgress(0, 100);
            else
                ShowProgress(0, 200);
        }
        public void GenerateJsonStep1()
        {
            if (Directory.Exists(jsonFileFolder))
            {
                var tmp = Directory.EnumerateFiles(jsonFileFolder,"*").Select(e =>
                {
                    File.Delete(e);
                    return e;
                }).ToArray();
            }
            string cmd = @"cd " + ASFRootPath + @"; " + FilePath + @" -TestSuite " + TestSuite + @" -SequenceNumber " + SequenceNumber + @" -JsonPath " + jsonFileFolder;
            ProcessStartInfo psi = new ProcessStartInfo("powershell.exe");
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.Arguments = cmd;
            log.DebugFormat("call powershell with parameters: [ {0}]", cmd);
            Process powershellInstance =  Process.Start(psi);
            powershellInstance.WaitForExit();
            if(powershellInstance.ExitCode!=0)
            {
                log.DebugFormat("the exit code of call setuprs is {0}", powershellInstance.ExitCode);
                MessageBox.Show("Error occur while generating json file");
            }
            log.Debug("step1 complete");
        }
        public void GenerateJsonStep2()
        {
            log.Debug("start the step2 to modify the json file");
            string jsonFile = Directory.EnumerateFiles(jsonFileFolder, "*").FirstOrDefault();
            if(string.IsNullOrEmpty(jsonFile))
            {
                log.DebugFormat("Don't find the json template under Path: {0}", jsonFileFolder);
                return;
            }
            JsonAccess.UpdateJsonTemplate(jsonFile, ServiceLocator.Current.GetInstance<OSBuildConfigure_ViewModel>().GeneralData);
            log.Debug("step2 completed");
        }

        private void WriteBackToASFDB()
        { }
        private void RemoveUserWorkFlow()
        { }
        private void ShowProgress(int min, int max)
        {
            ProgressBarView pbv = new ProgressBarView();
            pbv.DataContext = new ProgressBarViewModel(min, max);
            pbv.Owner = Application.Current.MainWindow;
            //foreach(Window each in Application.Current.Windows)
            //{
            //    if(each.Title== "Select Setup Mode")
            //    {
            //        pbv.Owner = each;
            //        break;
            //    }
            //}
            pbv.ShowDialog();
        }
        private void backgroundWorker(string param)
        {
            log.InfoFormat("start call background worker by using mode: {0}", param);
            GenerateJsonStep1();
            GenerateJsonStep2();
            string jsonFile = Directory.EnumerateFiles(jsonFileFolder, "*").FirstOrDefault();
            if (string.IsNullOrEmpty(jsonFile))
            {
                log.DebugFormat("Don't find the json template under Path: {0}", jsonFileFolder);
                return;
            }
            string cmd = @"-noexit cd " + ASFRootPath + @"; ipmo asf;Initialize-asfproject -InstallTests:$false -path " + jsonFile;
            switch (param)
            {
                case "DevMode":  break;
                case "CleanEnv": RemoveUserWorkFlow(); cmd += @"; invoke-asfworkflow"; break;
                case "CompleteASFRun":  cmd += @"; invoke-asfworkflow"; break;
                default:  break;
            }
            if (param == "JsonMode")
            {
                ProgressMessageSetter.SendProgressMessage(new ProgressMessage(100));
                return;
            }
            log.DebugFormat("call powershell with parameters: [ {0}]", cmd);
            ProcessStartInfo psi = new ProcessStartInfo("powershell.exe");
            psi.Arguments = cmd;
            Process.Start(psi);
            Thread.Sleep(3000);
            ProgressMessageSetter.SendProgressMessage(new ProgressMessage(200));
        }

        #region public Command
        private ICommand _GenerateCommand;
        private void ExecuteGenerateCommand(string param)
        {
            Thread t = new Thread(() => this.backgroundWorker(param));
            t.Start();
            if (param == "JsonMode")
                ShowProgress(0, 100);
            else
                ShowProgress(0, 200);
        }
        private bool CanExecuteGenerateCommand(string param)
        {
            return true;

        }
        public ICommand GenerateCommand
        {
            get
            {
                _GenerateCommand = _GenerateCommand ?? new RelayCommand<string>(ExecuteGenerateCommand, CanExecuteGenerateCommand);
                return _GenerateCommand;
            }
        }
        #endregion


        public CallSetUpRS_ViewModel(string _asfRootPath, string _sequenceNumber, string _testSuite)
        {
            ASFRootPath = _asfRootPath;
            FilePath = ASFRootPath + @"\Tests\environments\Setup\SetupRs.ps1";
            SequenceNumber = _sequenceNumber;
            TestSuite = _testSuite;
            jsonFileFolder = @"c:\programdata\ASFStartUp\JsonPath";
        }
        public CallSetUpRS_ViewModel()
        {
            MainViewModel mvm = ServiceLocator.Current.GetInstance<MainViewModel>();
            ASFRootPath = mvm.ASFRootPath;
            FilePath = ASFRootPath + @"\Tests\environments\Setup\SetupRs.ps1";
            SequenceNumber = mvm.SequenceName.TrimStart(new char[] { 'S', 'e', 'q' });
            TestSuite = mvm.ComponentName;
            jsonFileFolder = @"c:\programdata\ASFStartUp\JsonPath";
        }
    }
}
