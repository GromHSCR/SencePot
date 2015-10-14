using System.Windows.Input;
using Styx.GromHSCR.DocumentBase.Managers;
using Styx.GromHSCR.MvvmBase.Initializations;

namespace Styx.GromHSCR.DocumentBase.Documents
{
	public interface IDocumentBase : IInitialization
	{
		ICommand SaveCommand { get; }

		ICommand RefreshCommand { get; }

		IDocumentManager DocumentManager { get; set; }

		bool IsBusy { get; set; }
	}
}