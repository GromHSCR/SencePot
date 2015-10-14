using System.Windows;
using System.Windows.Media;
using DevExpress.Xpf.Core;

namespace Styx.GromHSCR.Localization
{
	public abstract class DialogWindowBase : DXWindow
	{
		protected DialogWindowBase()
		{
			WindowStyle = WindowStyle.ToolWindow;
			WindowStartupLocation = WindowStartupLocation.CenterScreen;
			SizeToContent = SizeToContent.WidthAndHeight;
			ResizeMode = ResizeMode.NoResize;
			SetValue(TextOptions.TextFormattingModeProperty, TextFormattingMode.Display);
		}
	}
}
