using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Threading;
using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Data;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using AsfStartUp.Auxiliary;
using AsfStartUp.View;
using System.Windows;
using System.Linq;
using System.IO;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

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
        private ObservableCollection<TreeNode> _treeNodes;
        private TreeNode _selectedNode;
        private string _asfRootPath;
        private string _sequenceName;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
                RaisePropertyChanged("StatusMessage");
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
        public ObservableCollection<TreeNode> TreeNodes
        {
            get
            {
                return _treeNodes;
            }
            set
            {
                _treeNodes = value;
                RaisePropertyChanged("TreeNodes");
            }
        }
        public string ASFRootPath
        {
            get
            {
                return _asfRootPath;
            }
            set
            {
                if(value!=null)
                {
                    _asfRootPath = value;
                    RootPathSetter.SetRootPath(new RootPathMessage(_asfRootPath, ASFType));
                }
            }
        }
        public string ComponentName { get; set; }
        public string SequenceName
        {
            get
            {
                return _sequenceName;
            }
            set
            {
                if(null!=value)
                {
                    _sequenceName = value;
                    if(SelectedNode!=null)
                        SequenceSelectedMessageSetter.SetSequenceSelected(new SequenceSelectedMessage(Directory.EnumerateFiles( SelectedNode.FullPath,"Sequence*_env.xml").First(), ASFRootPath + @"\Tests\environments\Setup\Config\Template.xml"));
                }
            }
        }
        public HypervisorAccess.ATRType ASFType { get; set; }

        public ViewModelBase StatusViewModel
        {
            get
            {
                return _StatusViewModel;
            }
            set
            {
                _StatusViewModel = value;
                RaisePropertyChanged("StatusViewModel");
            }
        }

        public TreeNode SelectedNode
        {
            get
            {
                return _selectedNode;
            }
            set
            {
                if (_selectedNode != value)
                {
                    _selectedNode = value;
                    SelectTargetNode(ResolvePath(_selectedNode.FullPath));
                }
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
                RaisePropertyChanged("Index");
                ((RelayCommand<string>)BackCommand).RaiseCanExecuteChanged();
                ((RelayCommand<string>)NextCommand).RaiseCanExecuteChanged();

            }
        }
        #endregion

        #region private methods
        /// <summary>
        /// Determine the type of ASF, Currently support ASF-TestRunner, ASF-Controller. 
        /// Now Hardcode,logic need to add.
        /// </summary>
        private void DetermineASFType()
        {
            if (Directory.EnumerateDirectories(@"c:\").Where(t => t.IndexOf("ASFTestRunner") >= 0).FirstOrDefault() != null)
            {
                log.Info("the machine is Onelab");
                ASFType = HypervisorAccess.ATRType.Onelab;
            }
            else
            {
                log.Info("the machine isnot Onelab");
                ASFType = HypervisorAccess.ATRType.Xenserver;
            }
        }
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
        private List<string> ResolvePath(string seqxmlFile)
        {
            List<string> structs = new List<string>();
            List<string> total = seqxmlFile.Split('\\').ToList();
            for (int i = total.Count - 3; i < total.Count; i++)
            {
                structs.Add(total[i]);
            }
            return structs;
        }
        private bool CompareRootPath(string oldValue, string newValue)
        {
            return oldValue.Substring(0, oldValue.IndexOf(@"\Tests")).ToUpper() != newValue.Substring(0, newValue.IndexOf(@"\Tests")).ToUpper();
        }
        #endregion

        #region public methods
        /// <summary>
        /// Remove the readonly of the Configuration Folder Path
        /// </summary>
        /// <param name="FolderPath"></param>
        public void RemoveReadonlyPropertyofConfigurationFolder(string FolderPath)
        {
            if(Directory.Exists(FolderPath))
            {
                log.InfoFormat("the target folder exist {0}", FolderPath);
                var di = new DirectoryInfo(FolderPath);
                var tmp = di.GetFiles("*", SearchOption.AllDirectories).Select(e =>
                {
                    log.InfoFormat("remove the readonly property for file {0}", e.Name);
                    e.Attributes &= ~FileAttributes.ReadOnly;
                    return e;
                }).ToArray();
            }
        }
        public void LoadTreeNodeInfo(string _testsFolderPath)
        {
            TreeNodes = new ObservableCollection<TreeNode>();
            if(Directory.Exists(_testsFolderPath+@"\Regression"))
            {
                TreeNode _parent = null ;
                var tmp = Directory.EnumerateDirectories(_testsFolderPath + @"\Regression").Where(e => !e.ToLower().Contains("common")).Select(e =>
                    {
                        if (Directory.EnumerateDirectories(e, "Seq*").Count() > 0 && Directory.EnumerateDirectories(e, "Common").Count() == 1)
                        {
                            _parent = _parent ?? new TreeNode(_testsFolderPath + @"\Regression");
                            TreeNode tn = new TreeNode(e,_parent);
                            var t = Directory.EnumerateDirectories(e).Where(c => !c.ToLower().Contains("common")).Select(c =>
                              {
                                  tn.ChildNodes.Add(new TreeNode(c,tn,true));
                                  return c;
                              }).ToArray();
                            _parent.ChildNodes.Add(tn);
                        }
                        return e;
                    }).ToArray();
                TreeNodes.Add(_parent);
            }
            if (Directory.Exists(_testsFolderPath + @"\componentbvts"))
            {
                TreeNode _parent = null;
                var tmp = Directory.EnumerateDirectories(_testsFolderPath + @"\componentbvts").Where(e => !e.ToLower().Contains("common")).Select(e =>
                {
                    if (Directory.EnumerateDirectories(e,"Seq*").Count() >0 && Directory.EnumerateDirectories(e,"Common").Count()==1)
                    {
                        _parent = _parent ?? new TreeNode(_testsFolderPath + @"\componentbvts");
                        TreeNode tn = new TreeNode(e,_parent);
                        var t = Directory.EnumerateDirectories(e).Where(c => !c.ToLower().Contains("common")).Select(c =>
                        {
                            if(Directory.EnumerateFiles(c,"Sequence*_seq.xml").Count()==1)
                                tn.ChildNodes.Add(new TreeNode(c,tn,true));
                            return c;
                        }).ToArray();
                        _parent.ChildNodes.Add(tn);
                    }
                    return e;
                 }).ToArray();
                TreeNodes.Add(_parent);
            }
            ASFRootPath = _testsFolderPath.Replace(@"\Tests","");
            RemoveReadonlyPropertyofConfigurationFolder(ASFRootPath + @"\Tests\Environments");
        }

        public bool SelectTargetNode(List<string> LeafPath)
        {
            TreeNode first = TreeNodes.Where(e => e.DisplayName.ToUpper() == LeafPath[0].ToUpper()).FirstOrDefault();
            if (first == null)
                return false;
            TreeNode second = first.ChildNodes.Where(e => e.DisplayName.ToUpper() == LeafPath[1].ToUpper()).FirstOrDefault();
            if (second == null)
                return false;
            TreeNode third = second.ChildNodes.Where(e => e.DisplayName.ToUpper() == LeafPath[2].ToUpper()).FirstOrDefault();
            if (third == null)
                return false;
            third.IsExpanded = true;
            third.IsSelected = true;
            ASFRootPath = _asfRootPath;
            ComponentName = second.DisplayName;
            SequenceName = third.DisplayName;
            string EnvFile = Directory.EnumerateFiles(third.FullPath, "Sequence*_Env.xml").FirstOrDefault();
           // SequenceSelectedMessageSetter.SetSequenceSelected(new SequenceSelectedMessage(Directory.EnumerateFiles(third.FullPath, "Sequence*_Env.xml").FirstOrDefault(), ASFRootPath + @"\Tests\environments\Setup\Config\Template.xml"));
            return true;
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
            if (Index == 6 )
            {
                HomePage hp = Application.Current.MainWindow as HomePage;
                CallSetUpRS_ViewModel csuvm = new CallSetUpRS_ViewModel();
                CallSetupRSView csrsv = new CallSetupRSView();
                csrsv.DataContext = csuvm;
                csrsv.Owner = hp;
                csrsv.ShowDialog();

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
                return !string.IsNullOrEmpty(ServiceLocator.Current.GetInstance<ConfigureRootPath_ViewModel>().SelectedSequence);
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
            Messenger.Default.Register<TreeNode>(this,tn => SelectedNode = tn);
            DetermineASFType();
        }

        #endregion

    }
    public class TreeViewModel:ViewModelBase
    {
        public ObservableCollection<TreeNode> TreeNodes { get; set; }
        public TreeViewModel(ObservableCollection<TreeNode> _nodes)
        {
            TreeNodes = _nodes;
        }
    }
    public class TreeNode:ViewModelBase
    {
        #region Data
        private TreeNode _parent;
        private bool _isSelected;
        private bool _isExpanded;
        private bool  _isLeaf;
        #endregion
        public string FullPath { get; set; }
        public string DisplayName { get; set; }
        public ObservableCollection<TreeNode> ChildNodes { get; set; }
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if(_isSelected!=value&&_isLeaf)
                {
                    _isSelected = value;
                    RaisePropertyChanged("IsSelected");
                }
            }
        }
        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }
            set
            {
                if (value && _parent != null)
                    _parent.IsExpanded = value;
                if(_isExpanded!=value)
                {
                    _isExpanded = value;
                    RaisePropertyChanged("IsExpanded");
                }
            }
        }

        public TreeNode(string _fullPath):this(_fullPath,null)
        {

        }
        public TreeNode(string _fullPath,TreeNode parent,bool isleaf=false)
        {
            _isLeaf = isleaf;
            FullPath = _fullPath;
            DisplayName = _fullPath.Split('\\').Last();
            ChildNodes = new ObservableCollection<TreeNode>();
            _parent = parent;
            _isExpanded = false;
            _isSelected = false;
        }

    }

    public class TreeViewExtend
    {

        public static readonly DependencyProperty SIProperty = DependencyProperty
              .RegisterAttached(
               "SI", typeof(object), typeof(TreeViewExtend),
               new PropertyMetadata(new object(), OnSelectedItemChanged));
        public static object GetSI(TreeView treeView)
        {
            return treeView.GetValue(SIProperty);
        }

        public static void SetSI(TreeView treeView, object value)
        {
            treeView.SetValue(SIProperty, value);
        }

        private static void OnSelectedItemChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs args)
        {
            var treeView = d as TreeView;
            if (treeView == null)
            {
                return;
            }
            treeView.SelectedItemChanged -= TreeViewItemChanged;
            var treeViewItem = SelectTreeViewItemForBinding(args.NewValue,
                treeView);
            if (treeViewItem != null)
            {
                treeView.SetValue(SIProperty, treeViewItem);
            }
            treeView.SelectedItemChanged += TreeViewItemChanged;
        }

        private static void TreeViewItemChanged(object sender,
            RoutedPropertyChangedEventArgs<object> e)
        {
            ((TreeView)sender).SetValue(SIProperty, e.NewValue);
        }

        private static TreeViewItem SelectTreeViewItemForBinding(
            object dataItem, ItemsControl ic)
        {
            if (ic == null || dataItem == null)
            {
                return null;
            }
            IItemContainerGenerator generator = ic.ItemContainerGenerator;
            using (generator.StartAt(generator.GeneratorPositionFromIndex(-1),
                GeneratorDirection.Forward))
            {
                foreach (var t in ic.Items)
                {
                    bool isNewlyRealized;
                    var tvi = generator.GenerateNext(out isNewlyRealized);
                    if (isNewlyRealized)
                    {
                        generator.PrepareItemContainer(tvi);
                    }
                    if (t == dataItem)
                    {
                        return tvi as TreeViewItem;
                    }

                    var tmp = SelectTreeViewItemForBinding(dataItem,
                        tvi as ItemsControl);
                    if (tmp != null)
                    {
                        return tmp;
                    }
                    ((TreeViewItem)tvi).IsExpanded = false;
                }
            }
            
            return null;
        }
    }

}