using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;


namespace AsfStartUp.Auxiliary
{
     public class Update
    {
        public const string remoteServerPath= @"\\eng.citrite.net\global\TestSampleFiles\AsfStartUpServer";
        public const string localVersionFile = @"currentVersion.txt";
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static bool IsForce;
        public static bool IsError;
        public static bool CheckUpdate()
        {
            log.InfoFormat("Check if we can access to the remote Path {0}", remoteServerPath);
            if(!Directory.Exists(remoteServerPath))
            {
                log.ErrorFormat("we can not access to the remote Path {0}", remoteServerPath);
                MessageBox.Show(@"We Can Not Connect to the remote Server, Please make sure you can access to the path: \\eng.citrite.net\global","Update Failure",MessageBoxButton.OK,MessageBoxImage.Warning);
                IsError = true;
                return false;
            }
            string donetxt = Directory.EnumerateFiles(remoteServerPath, "done.txt").FirstOrDefault();
            if(string.IsNullOrEmpty(donetxt))
            {
                log.InfoFormat("Currently, the remote server is not ready");
                return false;
            }
            string latestVersion= GetCurrentVersion(); 
            var tmp = File.ReadLines(donetxt).Select(l =>
            {
                if(l.Contains("version"))
                {
                    latestVersion = l.Replace("version", " ").Trim();
                }
                if(l.Contains("ForceMode"))
                {
                    IsForce = bool.Parse(l.Replace("ForceMode", " ").Trim());
                }
                return l;
            }).ToArray();
            string currentVersion = GetCurrentVersion();
            log.InfoFormat("Current version is {0}, latest version is {1}", currentVersion, latestVersion);
            if(string.Compare(latestVersion,currentVersion)!=0)
            {
                return true;
            }
            return false;
        }

        public static bool InstallUpdate()
        {
            MessageBox.Show("update installed successfully");
            return true;
        }

        public static string GetCurrentVersion()
        {
            return File.ReadAllText(localVersionFile);
        }
    }
}
