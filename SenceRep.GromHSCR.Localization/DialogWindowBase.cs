﻿using DevExpress.Xpf.Core;

namespace SenceRep.GromHSCR.Localization
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
