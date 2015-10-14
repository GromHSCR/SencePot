namespace Styx.GromHSCR.MvvmBase.ViewModels
{
	public abstract class SelectableViewModelBase<TModel> : ViewModelBase<TModel>, ISelectableViewModel where TModel : new()
	{
		protected bool IsSelectedNotRaisePropertyChanged;

		protected SelectableViewModelBase()
		{
		}

		protected SelectableViewModelBase(TModel model)
			: base(model)
		{
		}

		public bool IsSelected
		{
			get { return IsSelectedNotRaisePropertyChanged; }
			set
			{
				if (IsSelectedNotRaisePropertyChanged == value) return;
				RaisePropertyChanging("IsSelected");
				IsSelectedNotRaisePropertyChanged = value;
				RaisePropertyChanged("IsSelected");
			}
		}
	}
}
