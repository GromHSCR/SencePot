using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using SenceRep.GromHSCR.ActionBase.Common;

namespace SenceRep.GromHSCR.ActionBase.VisualActions
{
	[ContentProperty("ChildActions")]
	public class VisualAction
	{
		public VisualAction()
		{
			ChildActions = new List<VisualAction>();
		}

		public bool IsToggle { get; set; }

		public string TogglePropertyName { get; set; }

		public string GroupName { get; set; }

		public string CommandName { get; set; }

		public object CommandParameter { get; set; }

		public Bitmap Icon { get; set; }

		public string Title { get; set; }

		public TextWrapping TitleWrapping { get; set; }

		public SizeDefinition SizeDefinition { get; set; }

		public List<VisualAction> ChildActions { get; set; }

		public int? CanExecutePriviledge { get; set; }

		public int? VisibleForPriviledge { get; set; }

		public string KeyTip { get; set; }

		public string ScreenTipText { get; set; }

		public KeyGesture GlobalShortcut { get; set; }

		public KeyGesture Shortcut { get; set; }
		public Visibility Visibility { get; set; }

		public bool CanExecute(int canExecutePriviledge)
		{
			if (!CanExecutePriviledge.HasValue) return true;

			return CanExecutePriviledge <= canExecutePriviledge;
		}

		public bool VisibleFor(int visibleForPriviledge)
		{
			if (!VisibleForPriviledge.HasValue) return true;

			return VisibleForPriviledge <= visibleForPriviledge;
		}
	}
}
