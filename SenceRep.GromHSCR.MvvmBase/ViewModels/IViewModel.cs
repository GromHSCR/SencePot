using SenceRep.GromHSCR.MvvmBase.Common;

namespace SenceRep.GromHSCR.MvvmBase.ViewModels
{
	public interface IViewModel
	{
		bool IsModified { get; }

		bool IsValid { get; }

		StatusViewModel StatusViewModel { get; } 
	}
}