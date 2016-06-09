using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace SencePot.ViewModels
{
    public class Page1ViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private string _helloWorld;

        public Page1ViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            HelloWorld = IsInDesignMode
                ? "Runs in design mode"
                : "Runs in runtime mode";
            GoBackCommand = new RelayCommand(() => _navigationService.GoBack());
        }

        public string HelloWorld
        {
            get { return _helloWorld; }
            set { Set(() => HelloWorld, ref _helloWorld, value); }
        }

        public RelayCommand GoBackCommand { get; private set; }
    }
}