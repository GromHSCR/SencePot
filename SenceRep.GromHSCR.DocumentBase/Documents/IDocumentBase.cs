using System.Windows.Input;
using SenceRep.GromHSCR.DocumentBase.Managers;

namespace SenceRep.GromHSCR.DocumentBase.Documents
{
	public interface IDocumentBase : IInitialization
	{
		ICommand SaveCommand { get; }

		ICommand RefreshCommand { get; }

		IDocumentManager DocumentManager { get; set; }

		bool IsBusy { get; set; }
	}
}