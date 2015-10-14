using Styx.GromHSCR.MvvmBase.Common;

namespace Styx.GromHSCR.MvvmBase.ViewModels
{
	public interface IViewModel<out TModel> where TModel : new()
	{
		TModel Model { get; }

		StatusViewModel StatusViewModel { get; set; }
	}
}