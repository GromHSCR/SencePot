using System.Collections.Generic;

namespace Styx.GromHSCR.DocumentBase.Managers
{
	public interface IUndoRedoManager
	{
		IUndoRedoManager Instance();

		void Undo();

		void Redo();

		void Clear();

		void StartTransaction(UndoTransaction tran);

		void EndTransaction(UndoTransaction tran);

		IList<string> GetUndoStackInformation();

		IList<string> GetRedoStackInformation();

		Stack<IUndoRedoRecord> RedoStack { get; set; }

		Stack<IUndoRedoRecord> UndoStack { get; set; }
	}
}
