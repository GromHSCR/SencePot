using System;
using System.Windows;
using System.Windows.Input;
using SenceRep.GromHSCR.MvvmBase.ViewModels;

namespace SenceRep.GromHSCR.DocumentBase.Dialogs
{
	public class DialogViewModel: ViewModelBase
	{
		public string Title { get; set; }
		public string Message { get; set; }
		public bool ShowCancel { get; private set; }
		public bool IsInputTextRequired { get; private set; }

		private string _inputText;
		public string InputText
		{
			get { return _inputText; }
			set { Set(() => InputText, ref _inputText, value); }
		}

		public ICommand OkCommand { get; private set; }
		public ICommand CancelCommand { get; private set; }

		public DialogViewModel(string title = "", string message="", bool showCancel = true, bool isInputTextRequired = false)
		{
			Title = title;
			Message = message;
			IsInputTextRequired = isInputTextRequired;
			ShowCancel = showCancel;

			OkCommand = CreateCommand(OkCommandExecute, CanOkCommandExecute);
			CancelCommand = CreateCommand(CancelCommandExecute, CanCancelCommandExecute);
		}

		private bool CanCancelCommandExecute()
		{
			return ShowCancel;
		}

		private void CancelCommandExecute()
		{
			OnResultEvalueted(new MessageBoxExitEventArgs(MessageBoxResult.Cancel));
		}

		private bool CanOkCommandExecute()
		{
			return !IsInputTextRequired || (IsInputTextRequired && !String.IsNullOrWhiteSpace(InputText));
		}

		private void OkCommandExecute()
		{
			OnResultEvalueted(new MessageBoxExitEventArgs(MessageBoxResult.OK));
		}

		public event EventHandler<MessageBoxExitEventArgs> ResultEvalueted;

		protected virtual void OnResultEvalueted(MessageBoxExitEventArgs e)
		{
			var handler = ResultEvalueted;
			if (handler != null) handler(this, e);
		}
	}

	public class MessageBoxExitEventArgs : EventArgs
	{
		public MessageBoxResult Result { get; private set; }

		public MessageBoxExitEventArgs(MessageBoxResult result)
		{
			Result = result;
		}
	}
}