/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:AsfStartUp"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace AsfStartUp.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ConfigureRootPath_ViewModel>();
            SimpleIoc.Default.Register<BuildsConfigure_ViewModel>();
            SimpleIoc.Default.Register<OSBuildConfigure_ViewModel>();
            SimpleIoc.Default.Register<GeneralInfoConfigure_ViewModel>();
            SimpleIoc.Default.Register<HypervisorConfigure_ViewModel>();
            SimpleIoc.Default.Register<MailConfigure_ViewModel>();
            SimpleIoc.Default.Register<DomainConfigure_ViewModel>();
            SimpleIoc.Default.Register<GeneralConfigure_ViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public ConfigureRootPath_ViewModel RootPathInfoVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ConfigureRootPath_ViewModel>();
            }
        }
        public BuildsConfigure_ViewModel BuildsInfoVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<BuildsConfigure_ViewModel>();
            }
        }
        public OSBuildConfigure_ViewModel OSBuildInfoVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OSBuildConfigure_ViewModel>();
            }
        }
        public GeneralInfoConfigure_ViewModel GeneralInfoVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<GeneralInfoConfigure_ViewModel>();
            }
        }
        public HypervisorConfigure_ViewModel HypervisorInfoVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<HypervisorConfigure_ViewModel>();
            }
        }
        public MailConfigure_ViewModel MailInfoVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MailConfigure_ViewModel>();
            }
        }
        public DomainConfigure_ViewModel DomainInfoVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DomainConfigure_ViewModel>();
            }
        }
        public GeneralConfigure_ViewModel GeneralConfigure
        {
            get
            {
                return ServiceLocator.Current.GetInstance<GeneralConfigure_ViewModel>();
            }
        }


        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}