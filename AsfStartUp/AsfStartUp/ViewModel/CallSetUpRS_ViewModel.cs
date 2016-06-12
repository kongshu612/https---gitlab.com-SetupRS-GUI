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

namespace AsfStartUp.ViewModel
{
    public class CallSetUpRS_ViewModel
    {
        private string ASFRootPath;
        private string FilePath;
        private string SequenceNumber;
        private string TestSuite;
        private string cmdToPowerShell;
        private string jsonFileFolder;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
            string cmd = @"cd " + ASFRootPath + @"; " + FilePath + @" -TestSuite " + TestSuite + @" -SequenceNumber " + SequenceNumber + @" -JsonPath " + jsonFileFolder;// + @"; exit 0";
            ProcessStartInfo psi = new ProcessStartInfo("powershell.exe");
            psi.WindowStyle = ProcessWindowStyle.Normal;
            psi.Arguments = cmd;
            log.DebugFormat("call powershell with parameters: [ {0}]", cmd);
            Process powershellInstance =  Process.Start(psi);
            powershellInstance.WaitForExit();
        }
        public void GenerateJsonStep2()
        {
            string jsonFile = Directory.EnumerateFiles(jsonFileFolder, "*").FirstOrDefault();
            if(string.IsNullOrEmpty(jsonFile))
            {
                log.DebugFormat("Don't find the json template under Path: {0}", jsonFileFolder);
                return;
            }
            JsonAccess.UpdateJsonTemplate(jsonFile, ServiceLocator.Current.GetInstance<OSBuildConfigure_ViewModel>().GeneralData);
        }


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
            ConfigureRootPath_ViewModel crpvm = ServiceLocator.Current.GetInstance<ConfigureRootPath_ViewModel>();
            ASFRootPath = crpvm.ASFRootPath;
            FilePath = ASFRootPath + @"\Tests\environments\Setup\SetupRs.ps1";
            SequenceNumber = crpvm.SelectedSequence.TrimStart(new char[] { 'S', 'e', 'q' });
            TestSuite = crpvm.SelectedComponent;
            jsonFileFolder = @"c:\programdata\ASFStartUp\JsonPath";
        }
    }
}
