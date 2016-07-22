using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using System.Collections;
using AsfStartUp.Model;
using AsfStartUp.Auxiliary;
using System.Xml.Linq;

namespace AsfStartUp.ViewModel
{
    public class GeneralConfigure_ViewModel:ViewModelBase
    {
        //#region private members
        //private string _ASFRootPath;

        //#endregion

        //#region public properties
        //#endregion

        //#region private methods
        //#endregion

        //#region public Commands
        //#endregion

        //#region Constuctors
        //#endregion


        #region private members
        private GeneralCommon_ViewModel _CurrentViewModel;
        private List<GeneralCommon_ViewModel> _DataCollections;

        #endregion

        #region public properties
        public ObservableCollection<GeneralDisplayData> GeneralData
        {
            get
            {
                return _CurrentViewModel.GeneralData;
            }
            set
            {
                _CurrentViewModel.GeneralData = value;
                RaisePropertyChanged("GeneralData");
            }
        }
        public GeneralCommon_ViewModel CurrentViewModel
        {
            get
            {
                return _CurrentViewModel;
            }
            set
            {
                _CurrentViewModel = value;
                RaisePropertyChanged("CurrentViewModel");
            }
        }

        public int Count
        {
            get
            {
                return _DataCollections.Count;
            }
        }
        public string Header
        {
            get
            {
                return _CurrentViewModel.Header; 
            }
        }
        #endregion

        #region private methods
        //void LoadGeneralInfo()
        //{
        //    _GeneralConfigure = new ObservableCollection<GeneralDisplayData>();
        //    _GeneralConfigure.Add(new GeneralDisplayData("XA/XD Environment only", true, CustomerType.Bool));
        //    _GeneralConfigure.Add(new GeneralDisplayData("CLEANUP_ON_START", false, CustomerType.Bool));
        //    _GeneralConfigure.Add(new GeneralDisplayData("CLEANUP_ON_EXIT", false, CustomerType.Bool));
        //    _GeneralConfigure.Add(new GeneralDisplayData("ONEREPORT_SUBMIT", false, CustomerType.Bool));
        //    _GeneralConfigure.Add(new GeneralDisplayData("Enironment Name", "EnvXDOnPremBasic", CustomerType.Text));
        //    _GeneralConfigure.Add(new GeneralDisplayData("Enironment Name", "Main", CustomerType.Text));
        //}
        //void LoadHypervisorInfo()
        //{
        //    _HyperVisorConfigure = new ObservableCollection<GeneralDisplayData>();
        //    _HyperVisorConfigure.Add(new GeneralDisplayData("Type", "SkyNet", CustomerType.Text));
        //    _HyperVisorConfigure.Add(new GeneralDisplayData("Name", "Cloud9Asf", CustomerType.Text));
        //    _HyperVisorConfigure.Add(new GeneralDisplayData("URL", "http://dummy", CustomerType.Text));
        //    _HyperVisorConfigure.Add(new GeneralDisplayData("Network", "dummy", CustomerType.Text));
        //    _HyperVisorConfigure.Add(new GeneralDisplayData("UserName", "dummy", CustomerType.Text));
        //    _HyperVisorConfigure.Add(new GeneralDisplayData("Password", "dummy", CustomerType.Text));
        //    _HyperVisorConfigure.Add(new GeneralDisplayData("HypStorageName", "dummy", CustomerType.Text));

        //}
        //void LoadDomainInfo()
        //{
        //    _GeneralData = new ObservableCollection<GeneralDisplayData>();
        //    GeneralData.Add(new GeneralDisplayData("DOMAIN_ADMINISTRATOR_USER", "Administrator@bvt.local", CustomerType.Text));
        //    GeneralData.Add(new GeneralDisplayData("DOMAIN_ADMINISTRATOR_PASSWORD",  "citrix", CustomerType.Text));
        //    GeneralData.Add(new GeneralDisplayData("DOMAIN_ADMINISTRATOR_PREFIX",  "Administrator", CustomerType.Text));
        //    GeneralData.Add(new GeneralDisplayData("DOMAIN_USER_PREFIX",  "user", CustomerType.Text));
        //    GeneralData.Add(new GeneralDisplayData("DOMAIN_USER_COUNT", "4", CustomerType.Text));
        //    GeneralData.Add(new GeneralDisplayData("DOMAIN_USER_PASSWORD", "Password123", CustomerType.Text));
        //    GeneralData.Add(new GeneralDisplayData("DOMAIN_DNS_NAME", "bvt.local", CustomerType.Text));
        //    GeneralData.Add(new GeneralDisplayData("HOSTNAME_PREFIX", "", CustomerType.Text));
        //    GeneralData.Add(new GeneralDisplayData("REUSE_ENVIRONMENT", false,CustomerType.Bool));
        //}
        //void LoadMailInfo()
        //{
        //    _GeneralData = new ObservableCollection<GeneralDisplayData>();
        //    GeneralData.Add(new GeneralDisplayData("SEND_MESSAGE_TO", "owen.wei@citrix.com", CustomerType.Text));
        //    GeneralData.Add(new GeneralDisplayData("SEND_MESSAGE_FROM", "NJAS@citrix.com", CustomerType.Text));
        //    GeneralData.Add(new GeneralDisplayData("SEND_ADMIN_ADDRESS", "owen.wei@citrix.com", CustomerType.Text));
        //    GeneralData.Add(new GeneralDisplayData("MESSAGE_BODY_EMAIL", "Queries please contact: owen.wei@citrix.com", CustomerType.Text));
        //    GeneralData.Add(new GeneralDisplayData("SEND_MESSAGE_TO", true, CustomerType.Bool));
        //}
        #endregion

        #region public methods
        public void LoadDisplayData(int index)
        {
            CurrentViewModel = _DataCollections[index];
        }
        #endregion

        #region Constuctors
        public GeneralConfigure_ViewModel()
        {
            _DataCollections = new List<GeneralCommon_ViewModel>();
            _DataCollections.Add(new OSBuildConfigure_ViewModel());
            _DataCollections.Add(new GeneralInfoConfigure_ViewModel());
            _DataCollections.Add(new HypervisorConfigure_ViewModel());
            _DataCollections.Add(new MailConfigure_ViewModel());
            _DataCollections.Add(new DomainConfigure_ViewModel());
            CurrentViewModel = _DataCollections[0];
           // Messenger.Default.Register<PropertyMessage>(this, pm => RaisePropertyChanged(pm.PropertyName));
        }
        #endregion
    }

    public abstract class GeneralCommon_ViewModel:ViewModelBase
    {
        #region private members
        private ObservableCollection<GeneralDisplayData> _GeneralData;
        private string _Header;

        #endregion

        #region public properties
        public ObservableCollection<GeneralDisplayData> GeneralData
        {
            get
            {
                return _GeneralData;
            }
            set
            {
                _GeneralData = value;
                RaisePropertyChanged("GeneralData");
            }
        }
        public string Header
        {
            get
            {
                return _Header;
            }
            set
            {
                _Header = value;
                RaisePropertyChanged("Header");
            }
        }
    }
    public class GeneralInfoConfigure_ViewModel : GeneralCommon_ViewModel
    {
        #region public methods
        public void LoadGeneralInfo(string filePath)
        {
            filePath = filePath + @"\Tests\environments\Setup\Config\SetupRS.xml";
            ObservableCollection<GeneralDisplayData> t = new ObservableCollection<GeneralDisplayData>();
            ObservableCollection<XElement> GeneralRawInfo = AsfStartUp.Auxiliary.GeneralAccess.LoadGeneralInfo(filePath);
            var tmp = GeneralRawInfo.Select(e =>
            {
                t.Add(new GeneralDisplayData(e,Type.GetType("AsfStartUp.Auxiliary.GeneralAccess")));
                return e;
            }).ToArray();
            //foreach(var each in GeneralRawInfo.Keys)
            //{
            //    GeneralData.Add(new GeneralDisplayData(each.ToString(), bool.Parse(GeneralRawInfo[each].ToString()), CustomerType.Bool));
            //}
            GeneralData = new ObservableCollection<GeneralDisplayData>(t.OrderBy(e => e.CType).ToArray());
            Header = "General Configure";
           // PropertyMessageSetter.RefleshUI(new PropertyMessage("Header"));
        }
        #endregion
        #region private methods
        //void LoadGeneralInfo()
        //{
        //    GeneralData = new ObservableCollection<GeneralDisplayData>();
        //    GeneralData.Add(new GeneralDisplayData("XA/XD Environment only", true, CustomerType.Bool));
        //    GeneralData.Add(new GeneralDisplayData("CLEANUP_ON_START", false, CustomerType.Bool));
        //    GeneralData.Add(new GeneralDisplayData("CLEANUP_ON_EXIT", false, CustomerType.Bool));
        //    GeneralData.Add(new GeneralDisplayData("ONEREPORT_SUBMIT", false, CustomerType.Bool));
        //    GeneralData.Add(new GeneralDisplayData("Enironment Name", "EnvXDOnPremBasic", CustomerType.Text));
        //    GeneralData.Add(new GeneralDisplayData("Automation Branch", "Main", CustomerType.Text));
        //    Header = "General Configure";
        //}
        #endregion

        #region public Commands
        #endregion

        #region Constuctors
        public GeneralInfoConfigure_ViewModel()
        {
            Messenger.Default.Register<RootPathMessage>(this, rpm => LoadGeneralInfo(rpm.RootPath));
            Header = "Please Provide ASF Root Path in the first page";
        }
        #endregion

    }
    public class HypervisorConfigure_ViewModel:GeneralCommon_ViewModel
    {
        #region public methods
        public void LoadHypervisorInfo(string filePath,HypervisorAccess.ATRType _asfType)
        {
            GeneralData = new ObservableCollection<GeneralDisplayData>();
            filePath += @"\Tests\environments\Setup\Config\Hypervisor.xml";
            ObservableCollection<XElement> HypervisorRawInfo = AsfStartUp.Auxiliary.HypervisorAccess.LoadHypervisorInfo(filePath, _asfType);
            ObservableCollection<GeneralDisplayData> t = new ObservableCollection<GeneralDisplayData>();
            var tmp = HypervisorRawInfo.Select(e =>
            {
                t.Add(new GeneralDisplayData(e,Type.GetType("AsfStartUp.Auxiliary.HypervisorAccess")));
                return e;
            }).ToArray();
            //foreach(var each in HypervisorRawInfo.Keys)
            //{
            //    bool tmp;
            //    if(bool.TryParse(HypervisorRawInfo[each].ToString(),out tmp))
            //    {
            //        GeneralData.Add(new GeneralDisplayData(each.ToString(), tmp, CustomerType.Bool));
            //    }
            //    else
            //    {
            //        GeneralData.Add(new GeneralDisplayData(each.ToString(), HypervisorRawInfo[each].ToString(), CustomerType.Text));
            //    }
            //}
           // var tmp1 = GeneralData;
            GeneralData = new ObservableCollection<GeneralDisplayData>(t.OrderBy(e => e.CType).ToArray());
            Header = "Hypervisor Configure";
           // PropertyMessageSetter.RefleshUI(new PropertyMessage("Header"));
        }
        #endregion
        #region private methods
        //void LoadGeneralInfo()
        //{
        //    GeneralData.Add(new GeneralDisplayData("Type", "SkyNet", CustomerType.Text));
        //    GeneralData.Add(new GeneralDisplayData("Name", "Cloud9Asf", CustomerType.Text));
        //    GeneralData.Add(new GeneralDisplayData("URL", "http://dummy", CustomerType.Text));
        //    GeneralData.Add(new GeneralDisplayData("Network", "dummy", CustomerType.Text));
        //    GeneralData.Add(new GeneralDisplayData("UserName", "dummy", CustomerType.Text));
        //    GeneralData.Add(new GeneralDisplayData("Password", "dummy", CustomerType.Text));
        //    GeneralData.Add(new GeneralDisplayData("HypStorageName", "dummy", CustomerType.Text));
        //    Header = "Hypervisor Configure";
        //}
        #endregion

        #region public Commands
        #endregion

        #region Constuctors
        public HypervisorConfigure_ViewModel()
        {
            Messenger.Default.Register<RootPathMessage>(this, rpm => LoadHypervisorInfo(rpm.RootPath,rpm.ASFType));
            Header = "Please Provide ASF Root Path in the first page";
        }
        #endregion

    }
    public class DomainConfigure_ViewModel : GeneralCommon_ViewModel
    {
        #region public methods
        public void LoadDomainInfo(string filePath)
        {
            filePath += @"\Tests\environments\Setup\Config\SetupRS.xml";
            ObservableCollection<XElement> DomainInfo= AsfStartUp.Auxiliary.DomainAccess.LoadDomainInfo(filePath);
            ObservableCollection<GeneralDisplayData> t = new ObservableCollection<GeneralDisplayData>();
            var tmp = DomainInfo.Select(e =>
            {
                t.Add(new GeneralDisplayData(e,Type.GetType("AsfStartUp.Auxiliary.DomainAccess")));
                return e;
            }).ToArray();
            //foreach(string each in DomainInfo.Keys)
            //{
            //    bool tmp;
            //    if(bool.TryParse(DomainInfo[each].ToString(),out tmp))
            //    {
            //        GeneralData.Add(new GeneralDisplayData(each, tmp, CustomerType.Bool));
            //    }
            //    else
            //    {
            //        GeneralData.Add(new GeneralDisplayData(each, DomainInfo[each].ToString(), CustomerType.Text));
            //    }
            //}
            //var tmp1 = GeneralData;
            GeneralData = new ObservableCollection<GeneralDisplayData>(t.OrderBy(e => e.CType).ToArray());
            Header = "Domain Configure";
           // PropertyMessageSetter.RefleshUI(new PropertyMessage("Header"));
        }
        #endregion

        #region public Commands
        #endregion

        #region Constuctors
        public DomainConfigure_ViewModel()
        {
            Messenger.Default.Register<RootPathMessage>(this, rpm => LoadDomainInfo(rpm.RootPath));
            Header = "Please Provide ASF Root Path in the first page";
        }
        #endregion

    }
    public class MailConfigure_ViewModel : GeneralCommon_ViewModel
    {
        #region public methods
        public void LoadMailInfo(string filePath)
        {
            GeneralData = new ObservableCollection<GeneralDisplayData>();
            filePath += @"\Tests\environments\Setup\Config\SetupRS.xml";
            ObservableCollection<XElement> MailRawInfo = AsfStartUp.Auxiliary.MailAccess.LoadMailInfo(filePath);
            ObservableCollection<GeneralDisplayData> t = new ObservableCollection<GeneralDisplayData>();
            var tmp = MailRawInfo.Select(e =>
            {
                t.Add(new GeneralDisplayData(e,Type.GetType("AsfStartUp.Auxiliary.MailAccess")));
                return e;
            }).ToArray();
            //foreach(var each in MailRawInfo.Keys)
            //{
            //    bool tmp;
            //    if(bool.TryParse(MailRawInfo[each].ToString(),out tmp))
            //    {
            //        GeneralData.Add(new GeneralDisplayData(each.ToString(), tmp, CustomerType.Bool));
            //    }
            //    else
            //    {
            //        GeneralData.Add(new GeneralDisplayData(each.ToString(), MailRawInfo[each].ToString(), CustomerType.Text));
            //    }
            //}
            //var tmp1 = GeneralData;
            GeneralData = new ObservableCollection<GeneralDisplayData>(t.OrderBy(e => e.CType).ToArray());
            Header = "Mail Configure";
            //PropertyMessageSetter.RefleshUI(new PropertyMessage("Header"));
        }
        #endregion
        #region private methods
        void LoadGeneralInfo()
        {
            GeneralData = new ObservableCollection<GeneralDisplayData>();
            GeneralData.Add(new GeneralDisplayData("SEND_MESSAGE_TO", "owen.wei@citrix.com", CustomerType.Text));
            GeneralData.Add(new GeneralDisplayData("SEND_MESSAGE_FROM", "NJAS@citrix.com", CustomerType.Text));
            GeneralData.Add(new GeneralDisplayData("SEND_ADMIN_ADDRESS", "owen.wei@citrix.com", CustomerType.Text));
            GeneralData.Add(new GeneralDisplayData("MESSAGE_BODY_EMAIL", "Queries please contact: owen.wei@citrix.com", CustomerType.Text));
            GeneralData.Add(new GeneralDisplayData("SEND_MESSAGE_TO", true, CustomerType.Bool));
            Header = "Mail Configure";
        }
        #endregion

        #region public Commands
        #endregion

        #region Constuctors
        public MailConfigure_ViewModel()
        {
            Messenger.Default.Register<RootPathMessage>(this, rpm => LoadMailInfo(rpm.RootPath));
            Header = "Please Provide ASF Root Path in the first page";
        }
        #endregion

    }
    /// <summary>
    /// This Class is an abstract class to provide Os Build info by each Role.
    /// </summary>
    public class OSBuildConfigure_ViewModel:GeneralCommon_ViewModel
    {
        #region private members
        private ObservableCollection<OSInfo> _OSList;
        #endregion

        #region public Properties
        #endregion

        #region private methods
        private void LoadOSBuildInfo(string templateFile, string envFile)
        {
            GeneralData = new ObservableCollection<GeneralDisplayData>();
            _OSList = AsfStartUp.Auxiliary.OSAccess.LoadOsInfo(templateFile);
            ObservableCollection<AsfRoleInfo> tmp = AsfStartUp.Auxiliary.EnvAccess.LoadRoleInfo(envFile);
            LoadRoleOSBuildInfo(tmp);
            var tmp1 = GeneralData;
            GeneralData = new ObservableCollection<GeneralDisplayData>(tmp1.OrderBy(e => e.CType).ToArray());
            Header = "Role Template Build Configure";
            // low performance. we need to increase the performance. use reference count
            ServiceLocator.Current.GetInstance<BuildsConfigure_ViewModel>().UpdateBuildSet();
        }
        //private Role_OSBuild_ViewModel ConsturctRoleOSBuild(string roleName)
        //{
        //    ObservableCollection<OSInfo> osList = new ObservableCollection<OSInfo>();
        //    if (roleName.Contains("TSVDA") || roleName.Contains("DC") || roleName.Contains("SF"))
        //        osList = new ObservableCollection<OSInfo>(_OSList.Where(o => o.OSType == OSInfo.OperatingSystemType.win_server).ToList());
        //    else if (roleName.Contains("VDA"))
        //        osList = new ObservableCollection<OSInfo>(_OSList.Where(o => o.OSType == OSInfo.OperatingSystemType.win_desktop).ToList());
        //    else
        //        osList = new ObservableCollection<OSInfo>(_OSList.Where(o => o.OSType != OSInfo.OperatingSystemType.linux).ToList());
        //    return new Role_OSBuild_ViewModel(roleName, osList);
        //}
        private ObservableCollection<OSInfo> LoadOSSet(AsfRoleInfo e)
        {
            ObservableCollection<OSInfo> osList = new ObservableCollection<OSInfo>();
            if (e.RoleName.Contains("TSVDA") || e.RoleName.Contains("DC") || e.RoleName.Contains("SF"))
            {
                if (string.IsNullOrEmpty(e.TemplateName))
                {
                    osList = new ObservableCollection<OSInfo>(_OSList.Where(o => o.OSType == OSInfo.OperatingSystemType.win_server && !e.TemplateBlackList.Contains(o.DisplayName)).ToList());
                }
                else
                {
                    osList = new ObservableCollection<OSInfo>(_OSList.Where(o => o.DisplayName == e.TemplateName).ToList());
                }
            }
            else if (e.RoleName.Contains("VDA"))
            {
                if (string.IsNullOrEmpty(e.TemplateName))
                {
                    osList = new ObservableCollection<OSInfo>(_OSList.Where(o => o.OSType == OSInfo.OperatingSystemType.win_desktop && !e.TemplateBlackList.Contains(o.DisplayName)).ToList());
                }
                else
                {
                    osList = new ObservableCollection<OSInfo>(_OSList.Where(o => o.DisplayName == e.TemplateName).ToList());
                }
            }
            else
            {
                if (string.IsNullOrEmpty(e.TemplateName))
                {
                    osList = new ObservableCollection<OSInfo>(_OSList.Where(o => o.OSType != OSInfo.OperatingSystemType.linux && !e.TemplateBlackList.Contains(o.DisplayName)).ToList());
                }
                else
                {
                    osList = new ObservableCollection<OSInfo>(_OSList.Where(o => o.DisplayName == e.TemplateName).ToList());
                }
            }
            return osList;
        }
        private void LoadRoleOSBuildInfo(ObservableCollection<AsfRoleInfo> roleList)
        {
            GeneralData = new ObservableCollection<GeneralDisplayData>();
            var tmp = roleList.Select(e =>
            {
                ObservableCollection<OSInfo> osList = LoadOSSet(e);
                ObservableCollection<string> validateBuildSet = new ObservableCollection<string>();
                if(!string.IsNullOrEmpty(e.ProductVersion))
                {
                    validateBuildSet.Add(e.ProductVersion);
                }
                else
                {
                    validateBuildSet = e.SupportedBuildCollection;
                }
                GeneralData.Add(new GeneralDisplayData(e.RoleName,new Role_OSBuild_ViewModel(e.RoleName,osList,validateBuildSet),CustomerType.Combo));              
                return e;
            }).ToList();
        }
        //void LoadRoleOSBuildInfo()
        //{
        //    GeneralData = new ObservableCollection<GeneralDisplayData>();
        //    GeneralData.Add(new GeneralDisplayData("VDA-1", ConsturctRoleOSBuild("VDA-1"), CustomerType.Combo));
        //    GeneralData.Add(new GeneralDisplayData("TSVDA-1", ConsturctRoleOSBuild("TSVDA-1"), CustomerType.Combo));
        //    GeneralData.Add(new GeneralDisplayData("CLI-1", ConsturctRoleOSBuild("CLI-1"), CustomerType.Combo));
        //    GeneralData.Add(new GeneralDisplayData("DDC-1", ConsturctRoleOSBuild("DDC-1"), CustomerType.Combo));
        //    GeneralData.Add(new GeneralDisplayData("VDA-2", ConsturctRoleOSBuild("VDA-2"), CustomerType.Combo));
        //    Header = "Role OS Build Configure";
        //}
        #endregion

        #region public Commands
        #endregion

        #region Constuctors
        public OSBuildConfigure_ViewModel()
        {
            Messenger.Default.Register<SequenceSelectedMessage>(this, ssm =>LoadOSBuildInfo(ssm.TemplateFilePath,ssm.SequenceEnvFilePath) );
            Header = "Please Provide ASF Root Path in the first page";
        }
        #endregion
    }
    public class Role_OSBuild_ViewModel:ViewModelBase
    {
        #region public members
        public OSInfo _SelectedOS;
        public BuildConfigure_ViewModel _SelectedBuild;
        #endregion

        #region private members
        private string _RoleName;
        private ObservableCollection<OSInfo> _OSList;
        private ObservableCollection<string> _buildsList;
        #endregion

        #region public properties
        public string RoleName
        {
            get
            {
                return _RoleName;
            }
            set
            {
                _RoleName = value;
                RaisePropertyChanged("RoleName");
            }
        }
        public ObservableCollection<string> OSList
        {
            get
            {
                return new ObservableCollection<string>(_OSList.Select(e => e.DisplayName).ToList());
            }
        }
        public string SelectedOS
        {
            get
            {
                return _SelectedOS.DisplayName;
            }
            set
            {
                string osName = value as string;
                _SelectedOS = _OSList.FirstOrDefault(e => e.DisplayName == osName);
                RaisePropertyChanged("SelectedOS");
            }
        }
        public ObservableCollection<string> BuildsList
        {
            get
            {
                return _buildsList;
            }
        }
        public string SelectedBuild
        {
            get
            {
                return _SelectedBuild == null ? "" : _SelectedBuild.BuildName;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    string SBName = value;
                    GeneralCommon_ViewModel gcvm = ServiceLocator.Current.GetInstance<BuildsConfigure_ViewModel>().Builds.Where(p => { return ((BuildConfigure_ViewModel)p).BuildName == SBName; }).FirstOrDefault();
                    _SelectedBuild = gcvm == null ? null : gcvm as BuildConfigure_ViewModel;
                    // low performance. we need to increase the performance. use reference count
                    ServiceLocator.Current.GetInstance<BuildsConfigure_ViewModel>().UpdateBuildSet();
                    RaisePropertyChanged("SelectedBuild");
                }
            }
        }
        #endregion

        #region private methods
        #endregion

        #region public commands
        #endregion

        #region constuctors
        public Role_OSBuild_ViewModel(string roleName, ObservableCollection<OSInfo> osList,ObservableCollection<string> builds)
        {
            _RoleName = roleName;
            _OSList = osList;
            _buildsList = builds;
            _SelectedOS = _OSList.Where(e=> { return e.DisplayName.IndexOf("16") == -1; }).FirstOrDefault();
            SelectedBuild = BuildsList==null? null :BuildsList.FirstOrDefault();
        }
        #endregion

    }
    public class GeneralDisplayData:ViewModelBase,IComparable<GeneralDisplayData>,IEquatable<GeneralDisplayData>
    {
        public XElement _Element;
        #region private members
        private string _CKey;
        private object _CValue;
        private CustomerType _CType;
        private Type XMLAccessType;

        #endregion

        #region public properties
        public string CKey
        {
            get
            {
                if(_Element!=null)
                    return _Element.Name.ToString();
                else
                    return _CKey;
            }
            //set
            //{
            //    if(_Element!=null)
            //        _Element.Name = value;
            //    else
            //        _CKey = value;
            //    RaisePropertyChanged("CKey");
            //}
        }
        public object CValue
        {
            get
            {
                return _CValue;
            }
            set
            {
                _CValue = value;
                if(_Element!=null)
                {
                    _Element.SetValue(_CValue.ToString().ToLower());
                    XMLAccessType.GetMethod("SaveChanges").Invoke(null,new object[] { });
                }
                RaisePropertyChanged("CValue");
            }
        }
        public CustomerType CType
        {
            get
            {
                return _CType;
            }
            //set
            //{
            //    _CType = value;
            //    RaisePropertyChanged("CType");
            //}
        }
        #endregion

        #region private methods
        #endregion

        #region public methods
        public int CompareTo(GeneralDisplayData other)
        {
            if (this.CType == other.CType)
                return 0;
            else
                return this.CType.CompareTo(other.CType);
        }
        public bool Equals(GeneralDisplayData other)
        {
            return this.CKey == other.CKey && this.CType == other.CType && this.CValue == other.CValue;
        }
        #endregion

        #region constuctors
        public GeneralDisplayData(string keyv,object valuev,CustomerType typev)
        {
            _CKey = keyv;
            _CValue = valuev;
            _CType = typev;
        }
        public GeneralDisplayData()
        { }
        public GeneralDisplayData(XElement _element,Type _xmlAccessType)
        {
            _CKey = _element.Name.ToString();
            bool tmp;
            if(bool.TryParse(_element.Value.ToString(), out tmp))
            {
                _CValue = tmp;
                _CType = CustomerType.Bool;
            }
            else
            {
                _CValue = _element.Value.ToString();
                _CType = CustomerType.Text;
            }
            _Element = _element;
            XMLAccessType = _xmlAccessType;
        }
        #endregion
    }
    public enum CustomerType{Text,Bool,Combo};
}
#endregion