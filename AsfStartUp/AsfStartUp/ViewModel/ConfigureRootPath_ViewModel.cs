using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.IO;

namespace AsfStartUp.ViewModel
{
    public class ConfigureRootPath_ViewModel:ViewModelBase
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
        private string _ASFRootPath;
        private ObservableCollection<string> _Components;
        private ObservableCollection<string> _Sequences;
        private string _SelectedComponent;
        private string _SelectedSequence;
        #endregion

        #region public properties
        public string ASFRootPath
        {
            get
            {
                return _ASFRootPath;
            }
            set
            {
                _ASFRootPath = value;
                RaisePropertyChanged("ASFRootPath");
                ((RelayCommand)LoadCommand).RaiseCanExecuteChanged();
              //  CommandManager.InvalidateRequerySuggested();
            }
        }

        public string SelectedComponent
        {
            get
            {
                return _SelectedComponent;
            }
            set
            {
                _SelectedComponent = value;
                RaisePropertyChanged("");
                RefleshSequences();
            }
        }

        public string SelectedSequence
        {
            get
            {
                return _SelectedSequence;
            }
            set
            {
                _SelectedSequence = value;
                RaisePropertyChanged("");
            }
        }

        public ObservableCollection<string> Components
        {
            get
            {
                return _Components;
            }
            set
            {
                _Components = value;
                RaisePropertyChanged("");
            }
        }

        public ObservableCollection<string> Sequences
        {
            get
            {
                return _Sequences;
            }
            set
            {
                _Sequences = value;
                RaisePropertyChanged("");
            }
        }
        #endregion

        #region private methods
        private void RefleshSequences()
        {
            string seqRootPath = ASFRootPath + @"\Tests\Regression\" + SelectedComponent;
            Sequences = new ObservableCollection<string>(Directory.EnumerateDirectories(seqRootPath, "Seq*").Select(s => s.Split('\\').Last()).ToList());
        }
        #endregion

        #region public Commands
        private bool CanExecuteLoadCommand()
        {
            return !string.IsNullOrEmpty(ASFRootPath);
        }
        private void ExecuteLoadCommand()
        {            
            Components= new ObservableCollection<string>(Directory.EnumerateDirectories(ASFRootPath + @"\Tests\regression").Where(c => !c.Contains("Common")).Select(c => c.Split('\\').Last()).ToList());
        }
        private ICommand _LoadCommand;
        public ICommand LoadCommand
        {
            get
            {
                _LoadCommand = _LoadCommand ?? new RelayCommand(ExecuteLoadCommand, CanExecuteLoadCommand);
                return _LoadCommand;
            }
        }

        #endregion

        #region Constuctors
        public ConfigureRootPath_ViewModel()
        { }
        #endregion
    }
}
