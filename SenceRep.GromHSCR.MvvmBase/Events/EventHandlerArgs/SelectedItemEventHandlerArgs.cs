using SenceRep.GromHSCR.MvvmBase.ViewModels;

namespace SenceRep.GromHSCR.MvvmBase.Events.EventHandlerArgs
{
	public class SelectedItemEventHandlerArgs<TViewModel> where TViewModel : ViewModelBase
	{
		public TViewModel SelectedItem { get; set; }
	}
}
