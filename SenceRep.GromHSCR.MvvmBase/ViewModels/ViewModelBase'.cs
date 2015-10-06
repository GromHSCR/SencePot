using SenceRep.GromHSCR.MvvmBase.Common;

namespace SenceRep.GromHSCR.MvvmBase.ViewModels
{
	public abstract class ViewModelBase<TModel> : ViewModelBase, IViewModel<TModel> where TModel : new()
	{
		protected ViewModelBase(TModel model)
		{
			Model = model;
			StatusViewModel = StatusViewModel.Unchanged;
		}

		protected ViewModelBase()
		{
			Model = new TModel();
			StatusViewModel = StatusViewModel.Add;
		}

		public TModel Model { get; protected set; }
	}
}
