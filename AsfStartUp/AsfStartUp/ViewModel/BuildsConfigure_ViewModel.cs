using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AsfStartUp.ViewModel
{
    public class BuildsConfigure_ViewModel:ViewModelBase
    {
        #region private members
        private ObservableCollection<GeneralCommon_ViewModel> _Builds;
        private GeneralCommon_ViewModel _SelectedBuild;
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
        public GeneralCommon_ViewModel SelectedBuild
        {
            get
            {
                return _SelectedBuild;
            }
            set
            {
                _SelectedBuild = value;
                RaisePropertyChanged("");
                ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region private methods
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

        #region Constuctors
        public BuildsConfigure_ViewModel()
        {
            Builds = new ObservableCollection<GeneralCommon_ViewModel>();
            Builds.Add(new BuildConfigure_ViewModel());
            Builds.Add(new BuildConfigure_ViewModel());
            SelectedBuild = Builds[0];
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
            set
            {
                RaisePropertyChanged("BuildName");
            }
        }
        #endregion
        #region private methods
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
        #endregion
    }
}
