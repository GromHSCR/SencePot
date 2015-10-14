using System;

namespace RedKassa.Promoter.MvvmBase.Attributes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public class UndoRedoOperationAttribute : Attribute
	{
		public bool IsUndoRedoRecordable { get; private set; }

		public UndoRedoOperationAttribute(bool isOn = true)
		{
			IsUndoRedoRecordable = isOn;
		}

	}
}
