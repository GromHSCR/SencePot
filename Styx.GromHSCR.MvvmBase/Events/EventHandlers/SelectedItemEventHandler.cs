using Styx.GromHSCR.MvvmBase.Events.EventHandlerArgs;
using Styx.GromHSCR.MvvmBase.ViewModels;

namespace Styx.GromHSCR.MvvmBase.Events.EventHandlers
{
	public delegate void SelectedItemEventHandler<TViewModel>(object sender, SelectedItemEventHandlerArgs<TViewModel> args) where TViewModel : ViewModelBase;
}
