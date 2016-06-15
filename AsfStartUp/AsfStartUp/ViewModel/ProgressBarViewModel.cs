using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
<<<<<<< HEAD
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Messaging;
using System.Threading;
using System.Windows;
using AsfStartUp.Auxiliary;

namespace AsfStartUp.ViewModel
{
    public class ProgressBarViewModel:ViewModelBase
    {
        #region private members
        private int _minValue;
        private int _maxValue;
        private int _currentValue;
        #endregion

        #region public properties
        public int MinValue
        {
            get
            {
                return _minValue;
            }
            set
            {
                _minValue = value;
                RaisePropertyChanged("MinValue");
            }
        }
        public int MaxValue
        {
            get
            {
                return _maxValue;
            }
            set
            {
                _maxValue = value;
                RaisePropertyChanged("MaxValue");
            }
        }
        public int CurrentValue
        {
            get
            {
                return _currentValue;
            }
            set
            {
                _currentValue = value;
                RaisePropertyChanged("CurrentValue");
            }
        }
        #endregion

        #region Constructors
        public ProgressBarViewModel(int min, int max)
        {
            MinValue = min;
            MaxValue = max;
            CurrentValue = min;
            InitializeTimer();
            Messenger.Default.Register<ProgressMessage>(this,e=>UpdateProgress(e.CurrentValue));
        }
        #endregion

        #region private methods

        private void UpdateProgress(int cv)
        {
            CurrentValue = cv;
            if(CurrentValue==MaxValue)
            {
                Thread.Sleep(500);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    foreach (Window each in Application.Current.Windows)
                    {
                        if (each.Title == "Progress")
                        {
                            each.Close();
                            return;
                        }
                    }
                });
            }
        }
        private void UpdateProgress(object sender, EventArgs e)
        {
            if(CurrentValue<MaxValue-1)
            {
                CurrentValue++;
            }
        }
        private void InitializeTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0,0,50);
            timer.Tick += new EventHandler(UpdateProgress);
            timer.Start();
        }
        #endregion
=======

namespace AsfStartUp.ViewModel
{
    public class ProgressBarViewModel
    {

>>>>>>> origin/master
    }
}
