using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Xpf.Core;
using Microsoft.Win32;

namespace Styx.GromHSCR.DocumentBase.Dialogs
{
	[Export(typeof(IDialogService))]
	[PartCreationPolicy(CreationPolicy.Any)]
	public class DialogService : IDialogService
	{
		public MessageBoxResult Show(string messageBoxText)
		{
			return Application.Current != null ?
				DXMessageBox.Show(Application.Current.MainWindow, messageBoxText) :
				DXMessageBox.Show(messageBoxText);
		}

		public MessageBoxResult Show(string messageBoxText, string caption)
		{
			return Application.Current != null ?
				DXMessageBox.Show(Application.Current.MainWindow, messageBoxText, caption) :
				DXMessageBox.Show(messageBoxText, caption);
		}

		public MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button)
		{
			return Application.Current != null ?
				DXMessageBox.Show(Application.Current.MainWindow, messageBoxText, caption, button) :
				DXMessageBox.Show(messageBoxText, caption, button);
		}

		public MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
		{
			return Application.Current != null ?
				DXMessageBox.Show(Application.Current.MainWindow, messageBoxText, caption, button, icon) :
				DXMessageBox.Show(messageBoxText, caption, button, icon);
		}

		public MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
		{
			return Application.Current != null ?
				DXMessageBox.Show(Application.Current.MainWindow, messageBoxText, caption, button, icon, defaultResult) :
				DXMessageBox.Show(messageBoxText, caption, button, icon, defaultResult);
		}

		public OpenFileDialog CreateOpenFileDialog()
		{
			return new OpenFileDialog();
		}

		public SaveFileDialog CreateSaveFileDialog()
		{
			return new SaveFileDialog();
		}

		public bool? ShowDialog(Window window)
		{
			if (window == null) throw new ArgumentNullException("window");

			window.Owner = Application.Current != null ? Application.Current.MainWindow : null;

			return window.ShowDialog();
		}

		public Task<MessageBoxResult> ShowCustomAsync(Window dialogWindow, Window owner, DialogViewModel viewModel)
		{
			var tcs = new TaskCompletionSource<MessageBoxResult>();

			viewModel.ResultEvalueted += (sender, args) =>
			                             {
				                             dialogWindow.Close();
				                             tcs.SetResult(args.Result);
			                             };
			dialogWindow.Owner = owner;
			dialogWindow.DataContext = viewModel;
			dialogWindow.ShowDialog();

			return tcs.Task;
		}
	}
}
