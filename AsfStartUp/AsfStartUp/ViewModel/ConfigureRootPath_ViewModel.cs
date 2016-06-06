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
using AsfStartUp.Auxiliary;

namespace AsfStartUp.ViewModel
{
    public class ConfigureRootPath_ViewModel:ViewModelBase
    {

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
                return _SelectedComponent==null? null : _SelectedComponent.Split('\\').Last();
            }
            set
            {
                _SelectedComponent = _Components.FirstOrDefault(e => e.Split('\\').Last() == value);
                RaisePropertyChanged("SelectedComponent");
                RefleshSequences();
            }
        }

        public string SelectedSequence
        {
            get
            {
                return _SelectedSequence==null? null : _SelectedSequence.Split('\\').Last();
            }
            set
            {
                _SelectedSequence = _Sequences.FirstOrDefault(e => e.Split('\\').Last() == value);
                string EnvFile = Directory.EnumerateFiles(_SelectedSequence, "Sequence*Env*").FirstOrDefault();
                if(EnvFile==null)
                {
                    MessageBox.Show("Do not find the Env file with the name Sequencexxx_Env.xml, under ${_SelectedSequence}. Or the env name is invalidated. Please check");
                    return;
                }
                SequenceSelectedMessageSetter.SetSequenceSelected(new SequenceSelectedMessage(EnvFile,ASFRootPath+ @"\Tests\environments\Setup\Config\Template.xml"));
                RaisePropertyChanged("SelectedSequence");
            }
        }

        public ObservableCollection<string> Components
        {
            get
            {
                return _Components==null? null : new ObservableCollection<string>(_Components.Select(e=>e.Split('\\').Last()).ToArray());
            }
            set
            {
                _Components = value;
                _SelectedComponent = _Components.FirstOrDefault();
                RaisePropertyChanged("Components");
            }
        }

        public ObservableCollection<string> Sequences
        {
            get
            {
                return _Sequences==null? null : new ObservableCollection<string>(_Sequences.Select(e=>e.Split('\\').Last()).ToArray());
            }
            set
            {
                _Sequences = value;
                SelectedSequence = Sequences.FirstOrDefault();
                RaisePropertyChanged("Sequences");
            }
        }
        #endregion

        #region private methods
        private void RefleshSequences()
        {
            Sequences = new ObservableCollection<string>(Directory.EnumerateDirectories(_SelectedComponent, "Seq*").ToList());
        }
        private bool IsRootPathValidate()
        {
            List<string> subFolders = new List<string>() { "REGRESSION", "TESTAPI", "ENVIRONMENTS" };
            string TestsPath = ASFRootPath + @"\Tests\";
            if(!Directory.Exists(TestsPath))
            {
                //MessageBox.Show("Do not find the Tests folder under ${ASFRootPath}");
                return false;
            }
            int count = Directory.EnumerateDirectories(TestsPath).Select(s => s.Split('\\').Last()).Where(s => subFolders.Contains(s.ToUpper())).Count();
            return count == 3;
        }
        #endregion

        #region public Commands
        private bool CanExecuteLoadCommand()
        {
            return !string.IsNullOrEmpty(ASFRootPath);
        }
        private void ExecuteLoadCommand()
        {   
            if(!IsRootPathValidate())
            {
                MessageBox.Show("Invalidate ASF Root Path. Please make sure Regression, TestAPI, Environments Folders exist under ASF Root Path: ${ASFRootPath}");
                return;
            }
            RootPathSetter.SetRootPath(new RootPathMessage(ASFRootPath));      
            Components= new ObservableCollection<string>(Directory.EnumerateDirectories(ASFRootPath + @"\Tests\regression").Where(c => !c.Contains("Common")).ToList());
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
