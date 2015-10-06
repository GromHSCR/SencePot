using SenceRep.GromHSCR.MvvmBase.Common;

namespace SenceRep.GromHSCR.MvvmBase.ViewModels
{
	public interface IViewModel<out TModel> where TModel : new()
	{
		TModel Model { get; }

		StatusViewModel StatusViewModel { get; set; }
	}
}