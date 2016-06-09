using System.Linq;
using Windows.UI.Xaml.Controls;
using SencePot.Common;
using SencePot.Views;

namespace SencePot
{
    public sealed partial class Shell : UserControl
    {
        public Shell()
        {
            InitializeComponent();

            var vm = new ShellViewModel();
            vm.MenuItems.Add(new MenuItem { Icon = "", Title = "Welcome", PageType = typeof(WelcomePage) });
            vm.MenuItems.Add(new MenuItem { Icon = "", Title = "Page 1", PageType = typeof(Page1) });
            vm.MenuItems.Add(new MenuItem { Icon = "", Title = "Page 2", PageType = typeof(Page2) });
            vm.MenuItems.Add(new MenuItem { Icon = "", Title = "Page 3", PageType = typeof(Page3) });

            // select the first menu item
            vm.SelectedMenuItem = vm.MenuItems.First();

            ViewModel = vm;
        }

        public ShellViewModel ViewModel { get; private set; }

        public Frame RootFrame => Frame;
    }
}