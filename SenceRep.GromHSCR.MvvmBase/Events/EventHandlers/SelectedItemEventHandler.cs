using SenceRep.GromHSCR.MvvmBase.Events.EventHandlerArgs;
using SenceRep.GromHSCR.MvvmBase.ViewModels;

namespace SenceRep.GromHSCR.MvvmBase.Events.EventHandlers
{
	public delegate void SelectedItemEventHandler<TViewModel>(object sender, SelectedItemEventHandlerArgs<TViewModel> args) where TViewModel : ViewModelBase;
}
