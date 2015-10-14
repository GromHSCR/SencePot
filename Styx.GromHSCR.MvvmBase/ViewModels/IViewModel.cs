using Styx.GromHSCR.MvvmBase.Common;

namespace Styx.GromHSCR.MvvmBase.ViewModels
{
	public interface IViewModel
	{
		bool IsModified { get; }

		bool IsValid { get; }

		StatusViewModel StatusViewModel { get; } 
	}
}