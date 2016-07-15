using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using AsfStartUp.Auxiliary;
using AsfStartUp.ViewModel;

namespace AsfStartUp.View
{
    /// <summary>
    /// Interaction logic for SetUpView.xaml
    /// </summary>
    public partial class SetUpView : UserControl
    {
        public SetUpView()
        {
            InitializeComponent();
           // Messenger.Default.Register<PropertyMessage>(this, pm => { this.btn_Next.Content = pm.Btn_Text; });
           
        }
        private void TreeViewSelectedItemChanged(object sender, RoutedEventArgs rea)
        {
            TreeViewItem tvi = sender as TreeViewItem;
            if(tvi!=null)
            {
                tvi.BringIntoView();
                TreeNode tn = tvi.DataContext as TreeNode;
                SendSelectedNodeMessage.SendMessage(tn);
                rea.Handled = true;
            }
        }
    }
}
