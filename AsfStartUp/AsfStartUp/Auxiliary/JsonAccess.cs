using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AsfStartUp.ViewModel;
using AsfStartUp.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using System.IO;

namespace AsfStartUp.Auxiliary
{
    public class JsonAccess
    {
        public static JObject root;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static string filePath;
        private static void UpdateRoleOS(Role_OSBuild_ViewModel rovm)
        {
            var tmp = root["Environment"]["Roles"].Where(t => t["Settings"]["HostType"].ToString() == "AsfManaged" && (t["Role"].ToString()==rovm.RoleName||t["Role"].ToString()=="DC-1")).Select(e=>
            {
                e["Settings"]["TemplateName"] = rovm._SelectedOS.TemplateName;
                return e;
            }).ToArray();
            if (rovm.RoleName != "DC-1")
                root["Environment"]["Workflow"]["Sequences"][2]["Data"]["PRODUCT_VERSION_" + rovm.RoleName.Remove(rovm.RoleName.IndexOf('-'), 1)] = rovm.SelectedBuild;
            else
                root["Environment"]["Workflow"]["Sequences"][2]["Data"]["PRODUCT_VERSION_DOMAINCONTROLLER"] = rovm.SelectedBuild;
        }
        private static void UpdateBuilds(List<BuildConfigure_ViewModel> _selectedBuilds)
        {
            JArray Builds = root["Environment"]["Builds"] as JArray;
            JObject WorkflowData = root["Environment"]["Workflow"]["Sequences"][2]["Data"] as JObject;
            Builds.Clear();
            var tmp = _selectedBuilds.Select(e =>
            {
                log.DebugFormat("insert the build {0} into json file", e.BuildName);
                log.InfoFormat("insert the build {0} into json file", e.BuildName);
                JObject t = new JObject();
                t["BuildId"] = e.BuildName;
                t["BuildNumber"] = e.BuildNumber;
                t["BuildPath"] = e.BuildPath;
                t["ReportingName"] = "";
                t["Subdirectories"] = new JArray(e.Subdirectories);
                t["SyncDirs"] = bool.Parse(e.IsSync.ToString());
                Builds.Add(t);
                var BuildFriendlyNameList = WorkflowData.Properties().Where(dataProperty => dataProperty.Name.IndexOf("_BUILD_FRIENDLY_NAME") >= 0);
                if (BuildFriendlyNameList.Where(b=>b.Name.IndexOf(e.BuildName.ToUpper())>=0).FirstOrDefault() == null)
                {
                    // here a hardcode, as in BuildConfigure_ViewModel, we put the Image-Full as the first, so here,just use the first.
                    WorkflowData[e.BuildName.ToUpper() + "_BUILD_FRIENDLY_NAME"] = e.BuildName + @"\" + e.Subdirectories.FirstOrDefault();
                    WorkflowData[e.BuildName.ToUpper() + "_LACI_FRIENDLY_NAME"] = e.BuildName + @"\" + e.Subdirectories.Where(sn => sn.IndexOf("LACI") >= 0).FirstOrDefault();
                    log.DebugFormat("insert the build friendlyName {0} into json file", e.BuildName);
                    log.InfoFormat("insert the build lacifriendlyName {0} into json file", e.BuildName);
                }
                return e;
            }).ToArray();
        }
        public static bool UpdateJsonTemplate(string _jsonFilePath, ObservableCollection<GeneralDisplayData> _roleOSBuild)
        {
            filePath = _jsonFilePath;
            try
            {
                root = JObject.Parse(File.ReadAllText(_jsonFilePath));
            }
            catch(Exception e)
            {
                log.ErrorFormat("Parse Json File error, {0}", e);
                return false;
            }
            List<BuildConfigure_ViewModel> _buildsList = new List<BuildConfigure_ViewModel>();
            var tmp = _roleOSBuild.Select(r =>
            {
                Role_OSBuild_ViewModel rovm = r.CValue as Role_OSBuild_ViewModel;
                if(rovm==null)
                {
                    log.Debug("Convert to Role_OSBuild_ViewModel error");
                    return r;
                }
                UpdateRoleOS(rovm);
                if(!_buildsList.Contains(rovm._SelectedBuild)&& rovm._SelectedBuild!=null)
                {
                    _buildsList.Add(rovm._SelectedBuild);
                }
                return r;
            }).ToArray();
            UpdateBuilds(_buildsList);
            File.WriteAllText(filePath, root.ToString());
            return true;
        }

    }
}
