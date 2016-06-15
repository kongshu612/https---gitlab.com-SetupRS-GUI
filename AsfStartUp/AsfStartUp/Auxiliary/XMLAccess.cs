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
        public static XElement Root;
        public static string FilePath;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //public static ObservableCollection<Hashtable> LoadBuilds(string buildFilePath)
        //{
        //    ObservableCollection<Hashtable> Builds = new ObservableCollection<Hashtable>();
        //    XElement root = XElement.Load(buildFilePath);
        //    var tmp = root.Descendants("Build").Select(b =>
        //    {
        //        Hashtable ht = new Hashtable();
        //        foreach (XElement each in b.Elements())
        //        {
        //            ht.Add(each.Name.ToString(), each.Value.ToString());
        //        }
        //        Builds.Add(ht);
        //        return b;
        //    }).ToArray();
        //    return Builds;
        //}
        public static ObservableCollection<XElement> LoadBuilds(string buildFilePath)
        {
            ObservableCollection<XElement> Builds = new ObservableCollection<XElement>();
            FilePath = buildFilePath;
            log.InfoFormat("Load build info from file {0}", buildFilePath);
            Root = XElement.Load(buildFilePath);
            var tmp = Root.Descendants("Build").Select(b =>
            {
                Builds.Add(b);
                return b;
            }).ToArray();
            return Builds;
        }
        //}
        //public static void UpdateBuild(Hashtable _build,string buildFilePath)
        //{
        //    XElement root = XElement.Load(buildFilePath);
        //    var tmp = root.Descendants("Build").Where(e =>
        //    {
        //        return e.Element("PRODUCT_RELEASE").Value.ToString() == _build["PRODUCT_RELEASE"].ToString();
        //    }
        //    ).FirstOrDefault();
        //    if(tmp==null)
        //    {
        //        log.Info("Add a new Build into the Builds XML");
        //    }
        //    else
        //    {
        //        log.Info("Modify the Build");
        //        foreach(var each in _build.Keys)
        //        {
        //            tmp.Element(each.ToString()).SetValue(_build[each].ToString());
        //        }
        //    }
        //    root.Save(buildFilePath);
        //}
        public static void SaveChanges()
        {
            try
            {
                Root.Save(FilePath);
            }
            catch(Exception e)
            {
                log.Error("Eorror occur while save build info changes into xml", e);
            }
        }
    }
    public class DomainAccess
    {
        public static XElement Root;
        public static string FilePath;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static ObservableCollection<XElement> LoadDomainInfo(string _FilePath)
        {
            // Hashtable DomainInfo = new Hashtable();
            ObservableCollection<XElement> DomainInfo = new ObservableCollection<XElement>();
            //XElement root = XElement.Load(FilePath);
            FilePath = _FilePath;
            Root = XElement.Load(FilePath);
            var tmp = Root.Descendants("DomainVars").Elements().Select(e =>
            {
                //DomainInfo.Add(e.Name.ToString(), e.Value.ToString());
                DomainInfo.Add(e);
                return e;
            }).ToList();
            return DomainInfo;
        }
        public static void SaveChanges()
        {
            try
            {
                log.InfoFormat("save DomainInfo changes into xml: {0}", FilePath);
                Root.Save(FilePath);
            }
            catch(Exception e)
            {
                log.Error("error occur while save domain info changes into xml", e);
            }
        }
    }
    public class MailAccess
    {
        public static XElement Root;
        public static string FilePath;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //public static Hashtable LoadMailInfo(string FilePath)
        //{
        //    Hashtable MailInfo = new Hashtable();
        //    XElement root = XElement.Load(FilePath);
        //    var tmp = root.Descendants("AsfEnvParam").Elements().Where(e => e.Name.ToString().Contains("SEND")).Select(e =>
        //      {
        //          MailInfo.Add(e.Name.ToString(), e.Value.ToString());
        //          return e;
        //      }
        //    ).ToList();
        //    return MailInfo;
        //}
        public static ObservableCollection<XElement> LoadMailInfo(string _filePath)
        {
            ObservableCollection<XElement> MailInfo = new ObservableCollection<XElement>();
            FilePath = _filePath;
            Root = XElement.Load(FilePath);
            log.DebugFormat("load mail info from xml {0}", FilePath);
            var tmp = Root.Descendants("AsfEnvParam").Elements().Where(e => e.Name.ToString().Contains("SEND")).Select(e =>
            {
                MailInfo.Add(e);
                return e;
            }
            ).ToList();
            return MailInfo;
        }
        public static void SaveChanges()
        {
            try
            {
                log.InfoFormat("save MailInfo changes into xml: {0}", FilePath);
                Root.Save(FilePath);
            }
            catch (Exception e)
            {
                log.Error("error occur while save MailInfo into xml", e);
            }
        }
    }
    public class GeneralAccess
    {
        public static XElement Root;
        public static string FilePath;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //public static Hashtable LoadGeneralInfo(string FilePath)
        //{
        //    Hashtable GeneralInfo = new Hashtable();
        //    XElement root = XElement.Load(FilePath);
        //    bool tmp;
        //    var ttmp = root.Descendants("AsfEnvParam").Elements().Where(e => bool.TryParse(e.Value.ToString(), out tmp) && e.Name.ToString() != "SEND_EMAIL").Select(e =>
        //             {
        //                 GeneralInfo.Add(e.Name.ToString(), e.Value.ToString());
        //                 return e;
        //             }
        //        ).ToArray();
        //    return GeneralInfo;
        //}
        public static ObservableCollection<XElement> LoadGeneralInfo(string _filePath)
        {
            ObservableCollection<XElement> GeneralInfo = new ObservableCollection<XElement>();
            FilePath = _filePath;
            Root = XElement.Load(FilePath);
            bool tmp;
            var ttmp = Root.Descendants("AsfEnvParam").Elements().Where(e => bool.TryParse(e.Value.ToString(), out tmp) && e.Name.ToString() != "SEND_EMAIL").Select(e =>
            {
                GeneralInfo.Add(e);
                return e;
            }
                ).ToArray();
            return GeneralInfo;
        }
        public static void SaveChanges()
        {
            try
            {
                log.InfoFormat("save GeneralInfo changes into xml: {0}", FilePath);
                Root.Save(FilePath);
            }
            catch (Exception e)
            {
                log.Error("error occur while save GeneralInfo into xml", e);
            }
        }
    }
    public class HypervisorAccess
    {
        public static XElement Root;
        public static string FilePath;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //public static Hashtable LoadHypervisorInfo(string FilePath)
        //{
        //    Hashtable HypervisorInfo = new Hashtable();
        //    XElement root = XElement.Load(FilePath);
        //    var tmp = root.Descendants("Hypervisor").Elements().Select(e =>
        //    {
        //        HypervisorInfo.Add(e.Name.ToString(), e.Value.ToString());
        //        return e;
        //    }
        //    ).ToArray();
        //    return HypervisorInfo;
        //}
        public static ObservableCollection<XElement> LoadHypervisorInfo(string _filePath)
        {
            ObservableCollection<XElement> HypervisorInfo = new ObservableCollection<XElement>();
            FilePath = _filePath;
            Root = XElement.Load(FilePath);
            var tmp = Root.Descendants("Hypervisor").Elements().Select(e =>
            {
                HypervisorInfo.Add(e);
                return e;
            }
            ).ToArray();
            return HypervisorInfo;
        }
        public static void SaveChanges()
        {
            try
            {
                log.InfoFormat("save HypervisorInfo changes into xml: {0}", FilePath);
                Root.Save(FilePath);
            }
            catch (Exception e)
            {
                log.Error("error occur while save HypervisorInfo into xml", e);
            }
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
