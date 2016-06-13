using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;
using AsfStartUp.Auxiliary;
using System.Windows;

namespace AsfStartUp.ViewModel
{
    public class HomePageViewModel:ViewModelBase
    {
        #region private members
        ViewModelBase _CurrentData;
        MainViewModel _mainViewData;
        #endregion

        #region public properties
        public ViewModelBase CurrentData
        {
            get
            {
                return _CurrentData;
            }
            set
            {
                _CurrentData = value;
                RaisePropertyChanged("CurrentData");
            }
        }
        #endregion

        #region Constructors
        public HomePageViewModel()
        {
            _mainViewData = ServiceLocator.Current.GetInstance<MainViewModel>();
        }
        #endregion

        #region private functions
        private bool CheckUpdate()
        {
            return Update.CheckUpdate();
        }
        public void InstallUpdate()
        {
            if (Update.CheckUpdate())
            {
                if (Update.IsForce)
                {
                    if (MessageBox.Show("Please Update software, Yes: Update online, No: exit this app", "Update Software", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        Application.Current.Shutdown();
                    }
                }
                else if (MessageBox.Show("New Version Found, Do you want to update it?", "Update Software", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    return;
                }
                if (!Update.InstallUpdate())
                {
                    MessageBox.Show("Error Occur, while update");
                    return;
                }
            }
            else if(Update.IsError)
            {
                return;
            }
            else
            {
                MessageBox.Show("No Update Found");
            }
        }
        #endregion

        #region public Commands
        private bool CanExecuteMenuCommand(string param)
        {
            return true;
        }
        private void ExecuteMenuCommand(string param)
        {
            switch(param)
            {
                case "SetUp": CurrentData = _mainViewData; break;
                case "Update": InstallUpdate(); break;
            }
        }
        private ICommand _MenuCommand;
        public ICommand MenuCommand
        {
            get
            {
                _MenuCommand = _MenuCommand ?? new RelayCommand<string>(ExecuteMenuCommand, CanExecuteMenuCommand);
                return _MenuCommand;
            }
        }
        #endregion
    }
}
