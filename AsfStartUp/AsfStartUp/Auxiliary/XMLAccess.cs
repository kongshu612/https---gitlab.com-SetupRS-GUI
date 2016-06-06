using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AsfStartUp.ViewModel;
using System.Collections;
using System.Collections.ObjectModel;
using AsfStartUp.Model;

namespace AsfStartUp.Auxiliary
{
    public class BuildsAccess
    {
        public static ObservableCollection<Hashtable> LoadBuilds(string buildFilePath)
        {
            ObservableCollection<Hashtable> Builds = new ObservableCollection<Hashtable>();
            XElement root = XElement.Load(buildFilePath);
            var tmp = root.Descendants("Build").Select(b =>
            {
                Hashtable ht = new Hashtable();
                foreach (XElement each in b.Elements())
                {
                    ht.Add(each.Name.ToString(), each.Value.ToString());
                }
                Builds.Add(ht);
                return b;
            }).ToArray();
            return Builds;
        }
    }
    public class DomainAccess
    {
        public static Hashtable LoadDomainInfo(string FilePath)
        {
            Hashtable DomainInfo = new Hashtable();
            XElement root = XElement.Load(FilePath);
            var tmp = root.Descendants("DomainVars").Elements().Select(e =>
            {
                DomainInfo.Add(e.Name.ToString(), e.Value.ToString());
                return e;
            }).ToList();
            return DomainInfo;
        }
    }
    public class MailAccess
    {
        public static Hashtable LoadMailInfo(string FilePath)
        {
            Hashtable MailInfo = new Hashtable();
            XElement root = XElement.Load(FilePath);
            var tmp = root.Descendants("AsfEnvParam").Elements().Where(e => e.Name.ToString().Contains("SEND")).Select(e =>
              {
                  MailInfo.Add(e.Name.ToString(), e.Value.ToString());
                  return e;
              }
            ).ToList();
            return MailInfo;
        }
    }
    public class GeneralAccess
    {
        public static Hashtable LoadGeneralInfo(string FilePath)
        {
            Hashtable GeneralInfo = new Hashtable();
            XElement root = XElement.Load(FilePath);
            bool tmp;
            var ttmp = root.Descendants("AsfEnvParam").Elements().Where(e => bool.TryParse(e.Value.ToString(), out tmp) && e.Name.ToString() != "SEND_EMAIL").Select(e =>
                     {
                         GeneralInfo.Add(e.Name.ToString(), e.Value.ToString());
                         return e;
                     }
                ).ToArray();
            return GeneralInfo;
        }
    }
    public class HypervisorAccess
    {
        public static Hashtable LoadHypervisorInfo(string FilePath)
        {
            Hashtable HypervisorInfo = new Hashtable();
            XElement root = XElement.Load(FilePath);
            var tmp = root.Descendants("Hypervisor").Elements().Select(e =>
            {
                HypervisorInfo.Add(e.Name.ToString(), e.Value.ToString());
                return e;
            }
            ).ToArray();
            return HypervisorInfo;
        }
    }
    public class OSAccess
    {
        public static ObservableCollection<OSInfo> LoadOsInfo(string FilePath)
        {
            ObservableCollection<OSInfo> OsList = new ObservableCollection<OSInfo>();
            XElement root = XElement.Load(FilePath);
            var tmp = root.Descendants("Desktop").Elements("Template").Select(e =>
            {
                OsList.Add(new OSInfo(e.Elements("Name").FirstOrDefault().Value.ToString(), e.Elements("UIName").FirstOrDefault().Value.ToString(), int.Parse(e.Elements("Priority").FirstOrDefault().Value.ToString()), OSInfo.OperatingSystemType.win_desktop));
                return e;
            }
            ).ToArray();
            tmp = root.Descendants("Server").Elements("Template").Select(e =>
            {
                OsList.Add(new OSInfo(e.Elements("Name").FirstOrDefault().Value.ToString(), e.Elements("UIName").FirstOrDefault().Value.ToString(), int.Parse(e.Elements("Priority").FirstOrDefault().Value.ToString()), OSInfo.OperatingSystemType.win_server));
                return e;
            }
            ).ToArray();
            tmp = root.Descendants("LinuxTemplates").Elements("Template").Select(e =>
            {
                OsList.Add(new OSInfo(e.Elements("Name").FirstOrDefault().Value.ToString(), e.Elements("UIName").FirstOrDefault().Value.ToString(), int.Parse(e.Elements("Priority").FirstOrDefault().Value.ToString()), OSInfo.OperatingSystemType.linux));
                return e;
            }
            ).ToArray();
            return OsList;
        }
    }
    public class EnvAccess
    {
        public static ObservableCollection<AsfRoleInfo> LoadRoleInfo(string FilePath)
        {
            ObservableCollection<AsfRoleInfo> RolesInfo = new ObservableCollection<AsfRoleInfo>();
            XElement root = XElement.Load(FilePath);
            if (root.Descendants("Env0Host").ToList().Count !=0)
            {
                ObservableCollection<string> reusedRoles = new ObservableCollection<string>(root.Descendants("Env0Host").Elements("Roles").FirstOrDefault().Value.ToString().Split(','));
                var tmp = reusedRoles.Select(e =>
                {
                    RolesInfo.Add(new AsfRoleInfo(e.Trim()));
                    return e;
                }).ToArray();
            }
            var tmp1 = root.Descendants("Hosts").Elements("Host").Select(e =>
            {
                string role = e.Elements("Role").FirstOrDefault().Value.ToString();
                if (e.Elements("UnsupportedOS").FirstOrDefault() != null)
                {
                    ObservableCollection<string> templateBlackList = new ObservableCollection<string>(e.Element("UnsupportedOS").Value.ToString().Split(','));
                    RolesInfo.Add(new AsfRoleInfo(role, templateBlackList, e.Elements("ProductVersion").FirstOrDefault()==null?"": e.Elements("ProductVersion").FirstOrDefault().Value.ToString()));                    
                }
                else if (e.Elements("TemplateName").FirstOrDefault() != null)
                {
                    string templateName = e.Element("TemplateName").Value.ToString();
                    RolesInfo.Add(new AsfRoleInfo(role, templateName, e.Elements("ProductVersion").FirstOrDefault()==null?"": e.Elements("ProductVersion").FirstOrDefault().Value.ToString()));
                }
                else
                RolesInfo.Add(new AsfRoleInfo(role));
                return e;
            }
            ).ToArray();

            return RolesInfo;
        }
    }

}
