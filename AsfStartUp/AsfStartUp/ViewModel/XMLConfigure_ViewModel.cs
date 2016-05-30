using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;

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
                RaisePropertyChanged("");
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
                return _CurrentViewModel.Header; ;
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
                RaisePropertyChanged("");
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
            }
        }
    }
    public class GeneralInfoConfigure_ViewModel : GeneralCommon_ViewModel
    {
        #region private methods
        void LoadGeneralInfo()
        {
            GeneralData = new ObservableCollection<GeneralDisplayData>();
            GeneralData.Add(new GeneralDisplayData("XA/XD Environment only", true, CustomerType.Bool));
            GeneralData.Add(new GeneralDisplayData("CLEANUP_ON_START", false, CustomerType.Bool));
            GeneralData.Add(new GeneralDisplayData("CLEANUP_ON_EXIT", false, CustomerType.Bool));
            GeneralData.Add(new GeneralDisplayData("ONEREPORT_SUBMIT", false, CustomerType.Bool));
            GeneralData.Add(new GeneralDisplayData("Enironment Name", "EnvXDOnPremBasic", CustomerType.Text));
            GeneralData.Add(new GeneralDisplayData("Automation Branch", "Main", CustomerType.Text));
            Header = "General Configure";
        }
        #endregion

        #region public Commands
        #endregion

        #region Constuctors
        public GeneralInfoConfigure_ViewModel()
        {
            GeneralData = new ObservableCollection<GeneralDisplayData>();
            LoadGeneralInfo();
        }
        #endregion

    }
    public class HypervisorConfigure_ViewModel:GeneralCommon_ViewModel
    {
        #region private methods
        void LoadGeneralInfo()
        {
            GeneralData.Add(new GeneralDisplayData("Type", "SkyNet", CustomerType.Text));
            GeneralData.Add(new GeneralDisplayData("Name", "Cloud9Asf", CustomerType.Text));
            GeneralData.Add(new GeneralDisplayData("URL", "http://dummy", CustomerType.Text));
            GeneralData.Add(new GeneralDisplayData("Network", "dummy", CustomerType.Text));
            GeneralData.Add(new GeneralDisplayData("UserName", "dummy", CustomerType.Text));
            GeneralData.Add(new GeneralDisplayData("Password", "dummy", CustomerType.Text));
            GeneralData.Add(new GeneralDisplayData("HypStorageName", "dummy", CustomerType.Text));
            Header = "Hypervisor Configure";
        }
        #endregion

        #region public Commands
        #endregion

        #region Constuctors
        public HypervisorConfigure_ViewModel()
        {
            GeneralData = new ObservableCollection<GeneralDisplayData>();
            LoadGeneralInfo();
        }
        #endregion

    }
    public class DomainConfigure_ViewModel : GeneralCommon_ViewModel
    {
        #region private methods
        void LoadGeneralInfo()
        {
            GeneralData = new ObservableCollection<GeneralDisplayData>();
            GeneralData.Add(new GeneralDisplayData("DOMAIN_ADMINISTRATOR_USER", "Administrator@bvt.local", CustomerType.Text));
            GeneralData.Add(new GeneralDisplayData("DOMAIN_ADMINISTRATOR_PASSWORD", "citrix", CustomerType.Text));
            GeneralData.Add(new GeneralDisplayData("DOMAIN_ADMINISTRATOR_PREFIX", "Administrator", CustomerType.Text));
            GeneralData.Add(new GeneralDisplayData("DOMAIN_USER_PREFIX", "user", CustomerType.Text));
            GeneralData.Add(new GeneralDisplayData("DOMAIN_USER_COUNT", "4", CustomerType.Text));
            GeneralData.Add(new GeneralDisplayData("DOMAIN_USER_PASSWORD", "Password123", CustomerType.Text));
            GeneralData.Add(new GeneralDisplayData("DOMAIN_DNS_NAME", "bvt.local", CustomerType.Text));
            GeneralData.Add(new GeneralDisplayData("HOSTNAME_PREFIX", "", CustomerType.Text));
            GeneralData.Add(new GeneralDisplayData("REUSE_ENVIRONMENT", false, CustomerType.Bool));
            Header = "Domain Configure";
        }
        #endregion

        #region public Commands
        #endregion

        #region Constuctors
        public DomainConfigure_ViewModel()
        {
            GeneralData = new ObservableCollection<GeneralDisplayData>();
            LoadGeneralInfo();
        }
        #endregion

    }
    public class MailConfigure_ViewModel : GeneralCommon_ViewModel
    {

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
            GeneralData = new ObservableCollection<GeneralDisplayData>();
            LoadGeneralInfo();
        }
        #endregion

    }
    public class OSBuildConfigure_ViewModel:GeneralCommon_ViewModel
    {


        #region private methods
        private Role_OSBuild_ViewModel ConsturctRoleOSBuild(string roleName)
        {
            Role_OSBuild_ViewModel rovm = new Role_OSBuild_ViewModel();
            ObservableCollection<string> OSList = new ObservableCollection<string>() { "Win7SP1x86", "Win7SP1x64", "Win81x64", "Win81x86", "Win10x86", "Win10x64", "Win2K8SP1x64", "Win2K12R2x64" };
            rovm.OSList = new ObservableCollection<string>(OSList.Where(o => {
                if(roleName.Contains("TS")|| roleName.Contains("DDC"))
                {
                    return o.Contains("2K");
                }
                if(roleName.Contains("VDA"))
                {
                    return !o.Contains("2K");
                }
                return true;
            }).ToList());
            rovm.SelectedOS = rovm.OSList.Where(l => l == "Win10x64").FirstOrDefault() == null ? "Win2K12R2x64" : "Win10x64";
            rovm.BuildsList = new ObservableCollection<string>(ServiceLocator.Current.GetInstance<BuildsConfigure_ViewModel>().Builds.Select(b =>
            {
                BuildConfigure_ViewModel t = b as BuildConfigure_ViewModel;
                return t.BuildName;
            }).ToList<string>());
            rovm.SelectedBuild = rovm.BuildsList[0];
            return rovm;
        }
        void LoadRoleOSBuildInfo()
        {
            GeneralData = new ObservableCollection<GeneralDisplayData>();
            GeneralData.Add(new GeneralDisplayData("VDA-1", ConsturctRoleOSBuild("VDA-1"), CustomerType.Combo));
            GeneralData.Add(new GeneralDisplayData("TSVDA-1", ConsturctRoleOSBuild("TSVDA-1"), CustomerType.Combo));
            GeneralData.Add(new GeneralDisplayData("CLI-1", ConsturctRoleOSBuild("CLI-1"), CustomerType.Combo));
            GeneralData.Add(new GeneralDisplayData("DDC-1", ConsturctRoleOSBuild("DDC-1"), CustomerType.Combo));
            GeneralData.Add(new GeneralDisplayData("VDA-2", ConsturctRoleOSBuild("VDA-2"), CustomerType.Combo));
            Header = "Role OS Build Configure";
        }
        #endregion

        #region public Commands
        #endregion

        #region Constuctors
        public OSBuildConfigure_ViewModel()
        {
            GeneralData = new ObservableCollection<GeneralDisplayData>();
            LoadRoleOSBuildInfo();
        }
        #endregion
    }
    public class Role_OSBuild_ViewModel:ViewModelBase
    {
        #region private members
        private string _RoleName;
        private ObservableCollection<string> _OSList;
        private string _SelectedOS;
        private ObservableCollection<string> _BuildsList;
        private BuildConfigure_ViewModel _SelectedBuild;

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
                return _OSList;
            }
            set
            {
                _OSList = value;
                RaisePropertyChanged("OSList");
            }
        }
        public string SelectedOS
        {
            get
            {
                return _SelectedOS;
            }
            set
            {
                _SelectedOS = value;
                RaisePropertyChanged("SelectedOS");
            }
        }
        public ObservableCollection<string> BuildsList
        {
            get
            {
                return new ObservableCollection<string>(ServiceLocator.Current.GetInstance<BuildsConfigure_ViewModel>().Builds.Select(b =>
                {
                    BuildConfigure_ViewModel t = b as BuildConfigure_ViewModel;
                    return t.BuildName;
                }).ToList<string>());
            }
            set
            {
                _BuildsList = value;
                RaisePropertyChanged("BuildsList");
            }
        }
        public string SelectedBuild
        {
            get
            {
                return _SelectedBuild == null ? null : _SelectedBuild.BuildName;
            }
            set
            {
                string SBName = value;
                GeneralCommon_ViewModel gcvm = ServiceLocator.Current.GetInstance<BuildsConfigure_ViewModel>().Builds.Where(p => { return ((BuildConfigure_ViewModel)p).BuildName == SBName; }).FirstOrDefault();
                _SelectedBuild = gcvm == null ? null : gcvm as BuildConfigure_ViewModel;
                RaisePropertyChanged("SelectedBuild");
            }
        }
        #endregion

        #region private methods
        #endregion

        #region public commands
        #endregion

        #region constuctors
        public Role_OSBuild_ViewModel()
        {

        }
        #endregion

    }
    public class GeneralDisplayData:ViewModelBase
    {
        #region private members
        private string _CKey;
        private object _CValue;
        private CustomerType _CType;

        #endregion

        #region public properties
        public string CKey
        {
            get
            {
                return _CKey;
            }
            set
            {
                _CKey = value;
                RaisePropertyChanged("");
            }
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
                RaisePropertyChanged("");
            }
        }
        public CustomerType CType
        {
            get
            {
                return _CType;
            }
            set
            {
                _CType = value;
                RaisePropertyChanged("");
            }
        }
        #endregion

        #region private methods
        #endregion

        #region public commands
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
        #endregion
    }
    public enum CustomerType{Text,Bool,Combo};
}
#endregion