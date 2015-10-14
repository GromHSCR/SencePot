using System.Windows.Input;
using System.Windows.Media;
using Styx.GromHSCR.DocumentBase.Dialogs;

namespace Styx.GromHSCR.DocumentBase.Documents
{
	public interface IDocument : IDocumentBase
	{
		IDialogService DialogService { get; set; }

		ICommand CloseAllCommand { get; }

		ICommand CloseCommand { get; }

		bool Closing();

		void Closed();

		bool IsActive { get; set; }

		bool IsModified { get; }
		
		bool IsSelected { get; set; }

		bool IsVisible { get; set; }

		string Title { get; set; }

		ImageSource IconSource { get; set; }
	}
}