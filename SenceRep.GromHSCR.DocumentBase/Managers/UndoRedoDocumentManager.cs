using System.Collections.Generic;
using SenceRep.GromHSCR.DocumentBase.Documents;

namespace SenceRep.GromHSCR.DocumentBase.Managers
{
	public class UndoRedoDocumentManager
	{
		public UndoRedoManager CurrentDocumentUndoRedoManager { get; set; }

		public Dictionary<IDocument, UndoRedoManager> DocumentsUndoRedoManagersDictionary { get; set; }

		public UndoRedoDocumentManager()
		{
			DocumentsUndoRedoManagersDictionary = new Dictionary<IDocument, UndoRedoManager>();
		}
	}

}
