using System.Collections.Generic;
using System.Windows.Input;
using SenceRep.GromHSCR.DocumentBase.Documents;

namespace SenceRep.GromHSCR.DocumentBase.Managers
{
	public interface IDocumentManager
	{
		TDocument CreateDocument<TDocument>() where TDocument : IDocument;

		TDocument CreateDocument<TDocument>(params object[] args) where TDocument : IDocument;

		TDocument CreateDocumentAndAddDocument<TDocument>() where TDocument : IDocument;

		TDocument CreateDocumentAndAddDocument<TDocument>(params object[] args) where TDocument : IDocument;

		ICommand CreateDocumentCommand<TDocument>() where TDocument : IDocument;

		ICommand CreateDocumentCommand<TDocument>(params object[] args) where TDocument : IDocument;

		IDocument ActiveDocument { get; set; }

		IEnumerable<IDocument> Documents { get; }

		UndoRedoDocumentManager GetUndoRedoDocumentManager();

		UndoRedoManager GetCurrentUndoRedoManager();

		void AddUndoRedoManagerIntoUndoRedoDocumentManager(IDocument document, UndoRedoManager undoRedoManager);

		void AddDocument(IDocument document);

		void CloseAllDocuments();
	}
}