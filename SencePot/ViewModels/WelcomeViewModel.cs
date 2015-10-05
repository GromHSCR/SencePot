using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace SencePot.ViewModels
{
    public class WelcomeViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private string _helloWorld;

        public WelcomeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            HelloWorld = IsInDesignMode
                ? "Runs in design mode"
                : "Runs in runtime mode";
            GotoPage1Command = new RelayCommand(() => _navigationService.NavigateTo("Page1"));
        }

        public string HelloWorld
        {
            get { return _helloWorld; }
            set { Set(() => HelloWorld, ref _helloWorld, value); }
        }

        public RelayCommand GotoPage1Command { get; private set; }
    }
}