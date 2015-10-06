using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedKassa.Promoter.MvvmBase.Recorders
{
	public interface IUndoRedoRecordable
	{
		bool IsUndoRedoRecordable { get; }
	}
}
