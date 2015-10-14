using System.Collections.Generic;
using System.Windows.Markup;

namespace Styx.GromHSCR.ActionBase.VisualActions
{
	[ContentProperty("Groups")]
	public sealed class VisualActionGroupTab
	{
		public VisualActionGroupTab()
		{
			Groups = new List<VisualActionGroup>();
			this.IsVisible = true;
		}

		public object Header { get; set; }

		public List<VisualActionGroup> Groups { get; private set; }

		public bool IsContextual { get; set; }

		public bool IsVisible { get; set; }

		public string ContextualGroupName { get; set; }

		public string KeyTip { get; set; }
	}
}
