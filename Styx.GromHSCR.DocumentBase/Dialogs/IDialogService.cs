using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace Styx.GromHSCR.DocumentBase.Dialogs
{
	public interface IDialogService
	{
		MessageBoxResult Show(string messageBoxText);

		MessageBoxResult Show(string messageBoxText, string caption);

		MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button);

		MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon);

		MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult);

		OpenFileDialog CreateOpenFileDialog();

		SaveFileDialog CreateSaveFileDialog();

		bool? ShowDialog(Window window);

		Task<MessageBoxResult> ShowCustomAsync(Window dialogWindow, Window owner, DialogViewModel viewModel);
	}
}