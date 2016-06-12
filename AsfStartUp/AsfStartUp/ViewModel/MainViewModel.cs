using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Threading;
using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Data;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using AsfStartUp.Auxiliary;

namespace AsfStartUp.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        #region private members
        private string _StatusMessage;
       // private ViewModelBase _CurrentViewModel;
        private ViewModelBase _StatusViewModel;
        private string _CurrentTime;
       // private List<ViewModelBase> _ViewModelCollections;
        private int _index;
        #endregion

        #region public properties
        public string StatusMessage
        {
            get
            {
                return _StatusMessage;
            }
            set
            {
                _StatusMessage = value;
                RaisePropertyChanged("");
            }
        }
        public string CurrentTime
        {
            get
            {
                return _CurrentTime;
            }
            set
            {
                _CurrentTime = value;
                RaisePropertyChanged("");
            }
        }

        //public ViewModelBase CurrentViewModel
        //{
        //    get
        //    {
        //        return _CurrentViewModel;
        //    }
        //    set
        //    {
        //        _CurrentViewModel = value;
        //        RaisePropertyChanged("CurrentViewModel");
        //    }
        //}

        public ViewModelBase StatusViewModel
        {
            get
            {
                return _StatusViewModel;
            }
            set
            {
                _StatusViewModel = value;
                RaisePropertyChanged("");
            }
        }

        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                _index = value;
                RaisePropertyChanged("");
                ((RelayCommand<string>)BackCommand).RaiseCanExecuteChanged();
                ((RelayCommand<string>)NextCommand).RaiseCanExecuteChanged();

            }
        }
        #endregion

        #region private methods
        private void UpdateTime(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToLongDateString() + "  " + DateTime.Now.ToLongTimeString();
        }
        private void InitializeTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(UpdateTime);
            timer.Start();
        }
        #endregion

        #region public Commmands
        private ICommand _BackCommand;
        private void ExecuteBackCommand(string param)
        {
            Index--;
        }
        private bool CanExecuteBackCommand(string param)
        {
            return Index > 0;

        }
        public ICommand BackCommand
        {
            get
            {
                _BackCommand = _BackCommand ?? new RelayCommand<string>(ExecuteBackCommand, CanExecuteBackCommand);
                return _BackCommand;
            }
        }

        private ICommand _NextCommand;
        private void ExecuteNextCommand(string param)
        {
            if (Index == 6)
            {
                CallSetUpRS_ViewModel csuvm = new CallSetUpRS_ViewModel();
                csuvm.GenerateJsonStep1();
                csuvm.GenerateJsonStep2();
            }
            else
            {
                Index++;
            }
        }
        private bool CanExecuteNextCommand(string param)
        {
            if (Index == 6)
            {
                PropertyMessageSetter.RefleshUI(new PropertyMessage("Generate"));
            }
            else
            {
                PropertyMessageSetter.RefleshUI(new PropertyMessage("Next >"));
            }

            return true;
        }
        public ICommand NextCommand
        {
            get
            {
                _NextCommand = _NextCommand ?? new RelayCommand<string>(ExecuteNextCommand, CanExecuteNextCommand);
                return _NextCommand;
            }
        }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            InitializeTimer();
            Index = 0;
            
        }

        #endregion

    }
}