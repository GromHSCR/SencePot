using System;
using System.Collections.Generic;

namespace SenceRep.GromHSCR.DocumentBase.Managers
{
    /// <summary>
    /// This acts as a container for multiple undo/redo records.
    /// </summary>
    public class UndoTransaction : IDisposable, IUndoRedoRecord
    {
        private string _name;
        private List<IUndoRedoRecord> _undoRedoOperations = new List<IUndoRedoRecord>();
        public UndoTransaction(string name="")
        {
            _name = name;
            UndoRedoManager.GetCurrentUndoRedoManager().StartTransaction(this);
        }


        public string Name
        {
            get { return _name; }
        }

        public void Dispose()
        {
            UndoRedoManager.GetCurrentUndoRedoManager().EndTransaction(this);
        }

         public void AddUndoRedoOperation(IUndoRedoRecord operation)
         {
             _undoRedoOperations.Insert(0, operation);
         }

        public int OperationsCount
        {
            get { return _undoRedoOperations.Count; }
        }

        #region Implementation of IUndoRedoRecord

        public void Execute()
        {
            _undoRedoOperations.ForEach((a)=>a.Execute());
        }



        #endregion
    }
}
