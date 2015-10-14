using System.Collections.Generic;
using Styx.GromHSCR.DocumentBase.Documents;

namespace Styx.GromHSCR.DocumentBase.Managers
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
