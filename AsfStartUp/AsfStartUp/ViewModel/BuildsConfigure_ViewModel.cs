using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections;
using AsfStartUp.Auxiliary;
using System.Xml.Linq;
using Microsoft.Practices.ServiceLocation;

namespace AsfStartUp.ViewModel
{
    public class BuildsConfigure_ViewModel:ViewModelBase
    {
        
        #region private members
        private ObservableCollection<GeneralCommon_ViewModel> _Builds;
        private ObservableCollection<GeneralCommon_ViewModel> _ValidateBuilds;
        private GeneralCommon_ViewModel _SelectedBuild;
        private string BuildFilePath;
       // private const string BuildFilePath = @"C:\asf\Tests\environments\Setup\Config\Builds.xml";
        #endregion

        #region public properties
        public ObservableCollection<GeneralCommon_ViewModel> Builds
        {
            get
            {
                return _Builds;
            }
            set
            {
                _Builds = value;
                RaisePropertyChanged("Builds");
            }
        }
        public ObservableCollection<GeneralCommon_ViewModel> ValidateBuilds
        {
            get
            {
                return _ValidateBuilds;
            }
            set
            {
                _ValidateBuilds = value;
                RaisePropertyChanged("ValidateBuilds");
            }
        }
        public GeneralCommon_ViewModel SelectedBuild
        {
            get
            {
                return _SelectedBuild;
            }
            set
            {
                _SelectedBuild = value;
                RaisePropertyChanged("SelectedBuild");
                ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region private methods
        private void PopulateData(string filePath)
        {
            BuildFilePath = filePath.TrimEnd('\\') + @"\Tests\environments\Setup\Config\Builds.xml";
            Builds = new ObservableCollection<GeneralCommon_ViewModel>();
            // ObservableCollection<Hashtable> buildsRawData = AsfStartUp.Auxiliary.BuildsAccess.LoadBuilds(BuildFilePath);
            ObservableCollection<XElement> buildsRawData = AsfStartUp.Auxiliary.BuildsAccess.LoadBuilds(BuildFilePath);
            var tmp = buildsRawData.Select(b =>
            {
                Builds.Add(new BuildConfigure_ViewModel(b));
                return b;
            }
            ).ToArray();
            SelectedBuild = Builds.FirstOrDefault();
        }
        //private void UpdateBuildData(Hashtable ht)
        //{
        //   // BuildsAccess.UpdateBuild(ht,BuildFilePath);
        //}
        #endregion

        #region public Commands
        private bool CanExecuteDeleteCommand()
        {
            return SelectedBuild != null;
        }
        private void ExecuteDeleteCommand()
        {
            Builds.Remove(SelectedBuild);
            SelectedBuild = Builds.Count > 0 ? Builds[Builds.Count - 1] : null;
        }
        private ICommand _DeleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                _DeleteCommand = _DeleteCommand ?? new RelayCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
                return _DeleteCommand;
            }
        }

        private bool CanExecuteAddCommand()
        {
            return true;
        }
        private void ExecuteAddCommand()
        {
            Builds.Add(new BuildConfigure_ViewModel());
            SelectedBuild = Builds.LastOrDefault();
        }
        private ICommand _AddCommand;
        public ICommand AddCommand
        {
            get
            {
                _AddCommand = _AddCommand ?? new RelayCommand(ExecuteAddCommand, CanExecuteAddCommand);
                return _AddCommand;
            }
        }
        #endregion

        #region public methods
        public void UpdateBuildSet()
        {
            var RoleBuildsTemplateInstance = ServiceLocator.Current.GetInstance<OSBuildConfigure_ViewModel>();
            _ValidateBuilds = new ObservableCollection<GeneralCommon_ViewModel>();
            var tmp = RoleBuildsTemplateInstance.GeneralData.Select(e =>
            {
                Role_OSBuild_ViewModel rosvm = e.CValue as Role_OSBuild_ViewModel;
                if (rosvm._SelectedBuild != null && _ValidateBuilds.FirstOrDefault(t=>t==rosvm._SelectedBuild) ==null)
                {
                    _ValidateBuilds.Add(rosvm._SelectedBuild);
                }
                return e;
            }).ToArray();
            RaisePropertyChanged("ValidateBuilds");
        }
        #endregion

        #region Constuctors
        public BuildsConfigure_ViewModel()
        {
            Messenger.Default.Register<RootPathMessage>(this, rpm => PopulateData(rpm.RootPath));
        }
        #endregion
    }

    public class BuildConfigure_ViewModel:GeneralCommon_ViewModel
    {
        #region public Properties
        public string BuildName
        {
            get
            {
                return GeneralData.Where(g => g.CKey == "PRODUCT_RELEASE").Select(d => d.CValue).FirstOrDefault().ToString();
            }
        }
        public string BuildNumber
        {
            get
            {
                return GeneralData.Where(g => g.CKey == "BUILD_NUMBER").Select(d => d.CValue).FirstOrDefault().ToString();
            }
        }
        public string BuildPath
        {
            get
            {
                return GeneralData.Where(g=>g.CKey== "XD_SOURCE_DIR").Select(d => d.CValue).FirstOrDefault().ToString();
            }
        }
        public List<string> Subdirectories
        {
            get
            {
                List<string> _subdirectories = new List<string>();
                var tmp = GeneralData.Where(g => g.CKey == "SUBDIR_TO_SYNC").Select(d => d.CValue.ToString()).FirstOrDefault();
                if(tmp!=null)
                {
                    _subdirectories.AddRange(new List<string>(tmp.ToString().Split(',')));
                }
                var sub = GeneralData.Where(g => g.CKey == "EXTRAS_SUBDIR_TO_SYNC").Select(d => d.CValue.ToString()).FirstOrDefault();
                if(sub!=null)
                {
                    _subdirectories.AddRange(new List<string>(sub.ToString().Split(',')));
                }
                return _subdirectories;
            }
        }
        public bool IsSync
        {
            get
            {
                return bool.Parse(GeneralData.Where(g => g.CKey == "SYNC_DIRS").Select(d => d.CValue.ToString()).FirstOrDefault().ToString());
            }
        }

        #endregion

        #region public methods
        public void OnPropertyChanged()
        {
            RaisePropertyChanged("");
        }
        private void LoadBuildInfo()
        {
            GeneralData = new ObservableCollection<GeneralDisplayData>();
            GeneralData.Add(new GeneralDisplayData("PRODUCT_RELEASE", "Blade", CustomerType.Text));
            GeneralData.Add(new GeneralDisplayData("XD_SOURCE_DIR", @"\\eng.citrite.net\ftl\Downloads\layouts\teamcityftl\xdonprem\blade\7.8.0.232\layout", CustomerType.Text));
            GeneralData.Add(new GeneralDisplayData("BUILD_NUMBER", "7.8.0.232", CustomerType.Text));
            GeneralData.Add(new GeneralDisplayData("SUBDIR_TO_SYNC", "Image-Full", CustomerType.Text));
            GeneralData.Add(new GeneralDisplayData("EXTRAS_SUBDIR_TO_SYNC", @"Extras\Tools\LACI,Extras\Info", CustomerType.Text));
            GeneralData.Add(new GeneralDisplayData("SYNC_DIRS", true, CustomerType.Bool));
            Header = "Build Configure";
        }
        #endregion

        #region public Commands
        #endregion

        #region Constuctors
        public BuildConfigure_ViewModel()
        {
            LoadBuildInfo();            
        }
        public BuildConfigure_ViewModel(Hashtable buildRawData)
        {
            GeneralData = new ObservableCollection<GeneralDisplayData>();
            foreach(string each in buildRawData.Keys)
            {
                bool res;
                if(bool.TryParse(buildRawData[each].ToString(),out res))
                {
                    GeneralData.Add(new GeneralDisplayData(each, res, CustomerType.Bool));
                }
                else
                {
                    GeneralData.Add(new GeneralDisplayData(each, buildRawData[each].ToString(), CustomerType.Text));
                }
            }
        }
        public BuildConfigure_ViewModel(XElement _element)
        {
            GeneralData = new ObservableCollection<GeneralDisplayData>();
            var tmp = _element.Elements().Select(e =>
            {
                GeneralData.Add(new GeneralDisplayData(e,Type.GetType("AsfStartUp.Auxiliary.BuildsAccess")));
                return e;
            }).ToArray();
        }
        #endregion
    }
}
