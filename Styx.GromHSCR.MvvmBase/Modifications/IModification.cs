using System;

namespace Styx.GromHSCR.MvvmBase.Modifications
{
	public interface IModification
	{
		event EventHandler<EventArgs> IsModifiedChanged;

		bool IsModified { get; }
	}
}