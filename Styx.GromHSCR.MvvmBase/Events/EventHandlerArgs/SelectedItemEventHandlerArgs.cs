using Styx.GromHSCR.MvvmBase.ViewModels;

namespace Styx.GromHSCR.MvvmBase.Events.EventHandlerArgs
{
	public class SelectedItemEventHandlerArgs<TViewModel> where TViewModel : ViewModelBase
	{
		public TViewModel SelectedItem { get; set; }
	}
}
