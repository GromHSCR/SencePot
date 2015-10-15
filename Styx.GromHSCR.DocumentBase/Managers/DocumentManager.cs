using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Styx.GromHSCR.CompositionBase;
using Styx.GromHSCR.DocumentBase.Documents;
using Styx.GromHSCR.DocumentBase.Messages;
using Styx.GromHSCR.MvvmBase.ViewModels;

namespace Styx.GromHSCR.DocumentBase.Managers
{
	[Export(typeof(IDocumentManager))]
	[PartCreationPolicy(CreationPolicy.Any)]
	public class DocumentManager : ViewModelBase, IDocumentManager
	{
		private readonly ObservableCollection<IDocument> _documents;

		private IDocument _activeDocument;

		private IDocument _lastActiveDocument;

		public UndoRedoDocumentManager UndoRedoDocumentManager = new UndoRedoDocumentManager();

		private void CloseAllDocuments(CloseAllDocumentsMessage obj)
		{
			CloseAllDocuments();
		}

		private void CloseDocument(CloseDocumentMessage closeDocumentMessage)
		{
			if (closeDocumentMessage == null) throw new ArgumentNullException("closeDocumentMessage");

			if (closeDocumentMessage.Document == null) return;

			if (ActiveDocument == closeDocumentMessage.Document && LastActiveDocument != null)
				ActiveDocument = LastActiveDocument;

			UndoRedoDocumentManager.DocumentsUndoRedoManagersDictionary.Remove(closeDocumentMessage.Document);
			if (!_documents.Remove(closeDocumentMessage.Document)) return;
			closeDocumentMessage.Document.Closed();

			if (!_documents.Any())
			{
				LastActiveDocument = null;
				ActiveDocument = null;
				return;
			}

			ActiveDocument = LastActiveDocument;
		}

		private void InitDocument(IDocument document)
		{
			if (document == null) throw new ArgumentNullException("document");

			Composition.ComposeParts(document);

			document.DocumentManager = this;

			document.Initialization();
		}

		public TDocument CreateDocumentAndAddDocument<TDocument>() where TDocument : IDocument
		{
			var doc = CreateDocument<TDocument>();
			AddDocument(doc);
			return doc;
		}

		public TDocument CreateDocumentAndAddDocument<TDocument>(params object[] args) where TDocument : IDocument
		{
			var doc = CreateDocument<TDocument>(args);
			AddDocument(doc);
			return doc;
		}

		public TDocument CreateDocument<TDocument>() where TDocument : IDocument
		{
			var document = Activator.CreateInstance<TDocument>();
			InitDocument(document);
			return document;
		}

		public TDocument CreateDocument<TDocument>(params object[] args) where TDocument : IDocument
		{
			var document = (TDocument)Activator.CreateInstance(typeof(TDocument), args);
			InitDocument(document);
			return document;
		}

		public ICommand CreateDocumentCommand<TDocument>() where TDocument : IDocument
		{
			return new RelayCommand(() => this.AddDocument(this.CreateDocument<TDocument>()));
		}

		public ICommand CreateDocumentCommand<TDocument>(params object[] args) where TDocument : IDocument
		{
			return new RelayCommand(() => this.AddDocument(this.CreateDocument<TDocument>(args)));
		}

		public void AddUndoRedoManagerIntoUndoRedoDocumentManager(IDocument document, UndoRedoManager undoRedoManager)
		{
			UndoRedoDocumentManager.DocumentsUndoRedoManagersDictionary.Add(document, undoRedoManager);
		}

		public UndoRedoDocumentManager GetUndoRedoDocumentManager()
		{
			return UndoRedoDocumentManager;
		}

		public UndoRedoManager GetCurrentUndoRedoManager()
		{
			return UndoRedoDocumentManager.CurrentDocumentUndoRedoManager;
		}

		public DocumentManager()
		{
			_documents = new ObservableCollection<IDocument>();

			MessengerInstance.Register<CloseDocumentMessage>(this, CloseDocument);
			MessengerInstance.Register<CloseAllDocumentsMessage>(this, CloseAllDocuments);
		}

		public virtual IDocument LastActiveDocument
		{
			get { return _lastActiveDocument; }
			set
			{
				if (_lastActiveDocument == value) return;

				_lastActiveDocument = value;
				RaisePropertyChanged("LastActiveDocument");
			}
		}

		public virtual IDocument ActiveDocument
		{
			get { return _activeDocument; }
			set
			{
				if (_activeDocument != null) LastActiveDocument = _activeDocument;
				if (_activeDocument == value) return;

				_activeDocument = value;
				if (value != null && UndoRedoDocumentManager.DocumentsUndoRedoManagersDictionary.ContainsKey(value))
					UndoRedoDocumentManager.CurrentDocumentUndoRedoManager =
						UndoRedoDocumentManager.DocumentsUndoRedoManagersDictionary[value];
				RaisePropertyChanged("ActiveDocument");
			}
		}

		public virtual IEnumerable<IDocument> Documents { get { return _documents; } }

		public virtual void AddDocument(IDocument document)
		{
			if (document == null) throw new ArgumentNullException("document");

			_documents.Add(document);
			ActiveDocument = _documents.Last();

			document.RefreshCommand.Execute(null);
		}

		public virtual void CloseAllDocuments()
		{
			for (var index = _documents.Count - 1; index >= 0; index--)
			{
				var document = _documents[index];
				document.CloseCommand.Execute(null);
			}
		}
	}
}
