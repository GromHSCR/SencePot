using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Styx.GromHSCR.Helpers;
using Styx.GromHSCR.MvvmBase.Common;
using Styx.GromHSCR.MvvmBase.ViewModels;

namespace Styx.GromHSCR.DocumentBase.Documents
{
	public abstract class Document<TViewModel> : Document where TViewModel : ViewModelBase, new()
	{
		private TViewModel _viewModel;
		private string _propertyNameForTitleBinding;
		private PropertyChangedEventHandler _bindingForTitlePropertyChangedEventHandler;

		private void ResetChanges()
		{
			IsModified = false;
			ViewModel.IsModified = false;
			ViewModel.StatusViewModel = StatusViewModel.Unchanged;
			StatusViewModel = StatusViewModel.Unchanged;
			RefreshCommand.Execute(null);
		}

		private void ViewModelOnIsModifiedChanged(object sender, EventArgs eventArgs)
		{
			IsModified = ViewModel.IsModified;
		}

		private void OnResultRefresh(TViewModel result)
		{
			if (result == null) throw new ArgumentNullException("result");

			if (ViewModel != null && _bindingForTitlePropertyChangedEventHandler != null)
			{
				ViewModel.PropertyChanged -= _bindingForTitlePropertyChangedEventHandler;
			}

			ViewModel = result;
			UpdateBindingTitleToViewModelProperty();
		}

		private void UpdateBindingTitleToViewModelProperty()
		{
			if (string.IsNullOrWhiteSpace(_propertyNameForTitleBinding)) return;

			if (!ViewModel.ViewModelProperties.ContainsKey(_propertyNameForTitleBinding)) throw new ArgumentException(string.Format("{0} not found in ViewModel", _propertyNameForTitleBinding));

			var viewModelProperty = ViewModel.ViewModelProperties[_propertyNameForTitleBinding];

			Title = (viewModelProperty.GetValue(ViewModel) ?? string.Empty).ToString();

			_bindingForTitlePropertyChangedEventHandler = (sender, args) =>
			{
				if (args.PropertyName != _propertyNameForTitleBinding) return;
				Title = viewModelProperty.GetValue(ViewModel).ToString();
			};

			ViewModel.PropertyChanged += _bindingForTitlePropertyChangedEventHandler;
		}

		protected void BindingTitleToViewModelProperty<T>(Expression<Func<T>> propertyExpression)
		{
			if (propertyExpression == null) throw new ArgumentNullException("propertyExpression");

			_propertyNameForTitleBinding = ViewModel.GetPropertyNameByObject(propertyExpression);

			UpdateBindingTitleToViewModelProperty();
		}

		protected override void RefreshExecute()
		{
			Refresh(OnResultRefresh);
		}

		protected override void SaveChanges(Action callback)
		{
			switch (ViewModel.StatusViewModel)
			{
				case StatusViewModel.Change:
					ExecuteOperationAsync(() => Update(ViewModel), () =>
					{
						ResetChanges();
						callback.ExecuteIfNotNull();
					}, exception =>
					{
						throw exception;
					});
					break;
				case StatusViewModel.Add:
					ExecuteOperationAsync(() => Add(ViewModel, OnResultRefresh), () =>
					{
						ResetChanges();
						callback.ExecuteIfNotNull();
					}, exception =>
					{
						throw exception;
					});
					break;
				case StatusViewModel.Delete:
					ExecuteOperationAsync(() => Delete(ViewModel), null, exception =>
					{
						throw exception;
					});
					Delete(ViewModel);
					break;
			}
		}


		protected abstract void Add(TViewModel viewModel, Action<TViewModel> onResult);

		protected abstract void Delete(TViewModel viewModel);

		protected abstract void Refresh(Action<TViewModel> onResult);

		protected abstract void Update(TViewModel viewModel);

		protected Document(TViewModel viewModel)
		{
			if (viewModel == null) throw new ArgumentNullException("viewModel");
			ViewModel = viewModel;
		}

		protected Document(bool isNewDocument)
		{
			if (isNewDocument)
			{
				ViewModel = new TViewModel();
				StatusViewModel = StatusViewModel.Add;
			}
			else
			 ExecuteInInit(() => RefreshCommand.Execute(null));
		}

		protected Document()
		{
			ExecuteInInit(() => RefreshCommand.Execute(null));
		}

		protected override bool CanSaveExecute()
		{
			return base.CanSaveExecute() && ViewModel.IsValid && ViewModel.IsModified && (ViewModel.StatusViewModel == StatusViewModel.Add || ViewModel.StatusViewModel == StatusViewModel.Change);
		}

		protected override bool CanRefreshExecute()
		{
			return base.CanRefreshExecute() && (ViewModel == null || ViewModel.StatusViewModel != StatusViewModel.Add && ViewModel.StatusViewModel != StatusViewModel.Delete);
		}

		public TViewModel ViewModel
		{
			get { return _viewModel; }
			set
			{
				if (_viewModel == value) return;

				RaisePropertyChanging("ViewModel");

				if (value != null)
				{
					value.IsModifiedChanged += ViewModelOnIsModifiedChanged;
				}
				else if (_viewModel != null)
				{
					_viewModel.IsModifiedChanged -= ViewModelOnIsModifiedChanged;
				}

				_viewModel = value;
				RaisePropertyChanged("ViewModel");
			}
		}
	}
}
