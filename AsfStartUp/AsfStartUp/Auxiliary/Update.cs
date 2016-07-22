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
        public const string localVersionFile =  @"currentVersion.txt";
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
            Version latestVersion=new Version(); 
            var tmp = File.ReadLines(donetxt).Select(l =>
            {
                if(l.Contains("version"))
                {
                    latestVersion = new Version(l.Replace("version", " ").Trim());
                }
                if(l.Contains("ForceMode"))
                {
                    IsForce = bool.Parse(l.Replace("ForceMode", " ").Trim());
                }
                return l;
            }).ToArray();
            Version currentVersion = GetCurrentVersion();
            log.InfoFormat("Current version is {0}, latest version is {1}", currentVersion, latestVersion);
            return latestVersion.CompareTo(currentVersion) > 0;
        }

        private static bool DirectoryCopy(string sourceDirName,string destDirName)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, true);
            }
            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destDirName, subdir.Name);
                if (!DirectoryCopy(subdir.FullName, temppath))
                    return false;
            }
            return true;
        }

        public static bool DownloadPackets()
        {
            string remoteFolder = Path.Combine(remoteServerPath, "AsfStartUp");
            string localTmpFolder = Path.Combine(Directory.GetParent(Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString()).ToString(),"ASFStartUpNew");
            log.InfoFormat("the Remote path: {0}", remoteFolder);
            log.InfoFormat("the local tmp path:{0}", localTmpFolder);
            return DirectoryCopy(remoteFolder, localTmpFolder);
        }

        public static bool InstallUpdate()
        {
            if(!DownloadPackets())
            {
                MessageBox.Show("Error Occur while download the remote files.Please try again.");
                return false;
            }
            return true;
        }

        public static Version GetCurrentVersion()
        {
            string versionFile = Path.Combine(Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString(), localVersionFile);
            return new Version(File.ReadAllText(versionFile));
        }
    }
}
