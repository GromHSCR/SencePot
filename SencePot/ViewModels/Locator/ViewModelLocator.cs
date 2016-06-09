using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using SencePot.Common;
using SencePot.Views;

namespace SencePot.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var navigationService = CreateNavigationService();
            SimpleIoc.Default.Register(() => navigationService);

            SimpleIoc.Default.Register<IDialogService, DialogService>();

            if (ViewModelBase.IsInDesignModeStatic)
            {
                //SimpleIoc.Default.Register<IDataService, Design.DesignDataService>();
            }

            SimpleIoc.Default.Register<WelcomeViewModel>();
            SimpleIoc.Default.Register<Page1ViewModel>();
        }

        public WelcomeViewModel Welcome => ServiceLocator.Current.GetInstance<WelcomeViewModel>();
        public Page1ViewModel Page1 => ServiceLocator.Current.GetInstance<Page1ViewModel>();

        private INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationServiceUwp();
            navigationService.Configure("Page1", typeof(Page1));
            // navigationService.Configure("key1", typeof(OtherPage1));
            // navigationService.Configure("key2", typeof(OtherPage2));

            return navigationService;
        }
    }
}