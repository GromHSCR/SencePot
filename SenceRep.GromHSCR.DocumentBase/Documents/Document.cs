using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using SenceRep.GromHSCR.DocumentBase.Dialogs;
using SenceRep.GromHSCR.DocumentBase.Managers;
using SenceRep.GromHSCR.DocumentBase.Messages;
using SenceRep.GromHSCR.Localization;

namespace SenceRep.GromHSCR.DocumentBase.Documents
{
	public abstract class Document : DocumentBase, IDocument
	{
		private bool _isActive;
		private string _title;
		private bool _isSelected;
		private bool _isVisible;
		private ImageSource _iconSource;

		public volatile UndoRedoManager UndoRedoManager = new UndoRedoManager();


		protected override void Initialization()
		{
			base.Initialization();
			UndoRedoManager.AppDocumentManager = DocumentManager;
			DocumentManager.AddUndoRedoManagerIntoUndoRedoDocumentManager(this, UndoRedoManager);
		}

		protected override void InitializationCommands()
		{
			base.InitializationCommands();

			CloseAllCommand = CreateCommand(CloseAllExecute);
			CloseCommand = CreateCommand(CloseExecute, CanCloseExecute);
			UndoCommand = CreateCommand(UndoExecute, CanUndoExecute);
			RedoCommand = CreateCommand(RedoExecute, CanRedoExecute);
		}

		protected virtual void RedoExecute()
		{
		}

		protected virtual bool CanRedoExecute()
		{
			return false;
		}

		protected virtual void UndoExecute()
		{
		}

		protected virtual bool CanUndoExecute()
		{
			return false;
		}

		protected virtual void CloseAllExecute()
		{
			MessengerInstance.Send(new CloseAllDocumentsMessage(this));
		}

		protected virtual bool CanCloseExecute()
		{
			return !IsBusy;
		}

		protected virtual void CloseExecute()
		{
			if (!Closing()) return;

			var closeMessageSendAction = new Action(() => MessengerInstance.Send(new CloseDocumentMessage(this)));

			if (IsModified)
			{
				var msgResult = MessageBox.Show(Dialog.MsgConfirmUnsavedData, Dialog.CaptionConfirm,
					MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Yes);

				if (msgResult != MessageBoxResult.Yes && msgResult != MessageBoxResult.No) return;

				if (msgResult == MessageBoxResult.Yes)
					SaveChanges(closeMessageSendAction);
				else
					closeMessageSendAction.Invoke();
			}
			else
				closeMessageSendAction.Invoke();
		}

		[Import]
		public IDialogService DialogService { get; set; }

		public ICommand CloseAllCommand { get; protected set; }

		public ICommand CloseCommand { get; protected set; }

		public ICommand UndoCommand { get; protected set; }
		public ICommand RedoCommand { get; protected set; }

		public virtual bool Closing()
		{
			return true;
		}

		public virtual void Closed() { }

		public bool IsActive
		{
			get { return _isActive; }
			set
			{
				if (_isActive == value) return;

				RaisePropertyChanging("IsActive");
				_isActive = value;
				RaisePropertyChanged("IsActive");
			}
		}

		public bool IsSelected
		{
			get { return _isSelected; }
			set
			{
				if (_isSelected == value) return;

				RaisePropertyChanging("IsSelected");
				_isSelected = value;
				RaisePropertyChanged("IsSelected");
			}
		}

		public bool IsVisible
		{
			get { return _isVisible; }
			set
			{
				if (_isVisible == value) return;

				RaisePropertyChanging("IsVisible");
				_isVisible = value;
				RaisePropertyChanged("IsVisible");
			}
		}

		public ImageSource IconSource
		{
			get { return _iconSource; }
			set
			{
				if (Equals(_iconSource, value)) return;

				RaisePropertyChanging("IconSource");
				_iconSource = value;
				RaisePropertyChanged("IconSource");
			}
		}

		public string Title
		{
			get { return _title; }
			set
			{
				if (_title == value) return;

				RaisePropertyChanging("Title");
				_title = value;
				RaisePropertyChanged("Title");
			}
		}

		private bool _isShowNotificationForDocument = true;
		public bool IsShowNotificationForDocument
		{
			get { return _isShowNotificationForDocument; }
			set
			{
				if (_isShowNotificationForDocument == value) return;
				RaisePropertyChanging("IsShowNotificationForDocument");
				_isShowNotificationForDocument = value;
				RaisePropertyChanged("IsShowNotificationForDocument");
			}
		}
	}
}
