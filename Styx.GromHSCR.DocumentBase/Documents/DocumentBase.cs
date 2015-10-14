using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Styx.GromHSCR.DocumentBase.Managers;
using Styx.GromHSCR.Helpers;
using Styx.GromHSCR.Localization;
using Styx.GromHSCR.MvvmBase.ViewModels;

namespace Styx.GromHSCR.DocumentBase.Documents
{
	public abstract class DocumentBase : ViewModelBase, IDocumentBase
	{
		private bool _isBusy;

		private List<Action> _actionsForInit;

		protected abstract void RefreshExecute();

		protected abstract void SaveChanges(Action callback);

		protected override void InitializationCommands()
		{
			base.InitializationCommands();

			SaveCommand = CreateCommand(SaveExecute, CanSaveExecute);
			RefreshCommand = CreateCommand(RefreshCommandExecute, CanRefreshExecute);
		}

		protected virtual void RefreshCommandExecute()
		{
			ExecuteWithCheckForUnsaved(RefreshExecute);
		}

		protected override void Initialization()
		{
			base.Initialization();

			if (_actionsForInit == null) return;
			foreach (var action in _actionsForInit)
			{
				action.ExecuteIfNotNull();
			}

			_actionsForInit.Clear();
		}

		protected void ExecuteInInit(Action action)
		{
			if (action == null) return;

			if (_actionsForInit == null)
				_actionsForInit = new List<Action>();
			
			_actionsForInit.Add(action);
		}

		protected virtual void SaveExecute()
		{
			SaveChanges(null);
		}

		protected void ExecuteWithCheckForUnsaved(Action callback)
		{
			if (IsModified)
			{
				var msgResult = MessageBox.Show(Dialog.MsgConfirmUnsavedData, Dialog.CaptionConfirm,
					MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Yes);

				if (msgResult != MessageBoxResult.Yes && msgResult != MessageBoxResult.No) return;

				if (msgResult == MessageBoxResult.Yes)
					SaveChanges(callback);
				else
					callback.InvokeIfNotNull();
			}
			else
				callback.InvokeIfNotNull();
		}

		protected async void ExecuteOperationAsync(Action action, Action onResult = null, Action<Exception> onFailed = null)
		{
			if (action == null) throw new ArgumentNullException("action");
			IsBusy = true;
			try
			{
				await Task.Factory.StartNew(action);

				if (onResult == null) return;

				IsBusy = false;
				CommandManager.InvalidateRequerySuggested();
				onResult();
				CommandManager.InvalidateRequerySuggested();
			}
			catch (Exception ex)
			{
				if (onFailed != null)
					onFailed(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}

		protected async void ExecuteOperationAsync<TResult>(Func<TResult> function, Action<TResult> onResult = null, Action<Exception> onFailed = null)
		{
			if (function == null) throw new ArgumentNullException("function");
			IsBusy = true;
			try
			{
				var resultSync = Task<TResult>.Factory.StartNew(function);
				if (onResult == null) return;

				var result = await resultSync;
				IsBusy = false;
				CommandManager.InvalidateRequerySuggested();
				onResult(result);
				CommandManager.InvalidateRequerySuggested();
			}
			catch (Exception ex)
			{
				if (onFailed != null)
					onFailed(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}

		protected virtual bool CanRefreshExecute()
		{
			return !IsBusy;
		}

		protected virtual bool CanSaveExecute()
		{
			return !IsBusy && IsModified;
		}

		public ICommand SaveCommand { get; protected set; }

		public ICommand RefreshCommand { get; protected set; }

		[Import]
		public IDocumentManager DocumentManager { get; set; }

		public bool IsBusy
		{
			get { return _isBusy; }
			set
			{
				if (_isBusy == value) return;

				RaisePropertyChanging("IsBusy");
				_isBusy = value;
				RaisePropertyChanged("IsBusy");
			}
		}
	}
}
