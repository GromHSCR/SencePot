using System.Collections.Generic;
using System.Windows.Markup;

namespace SenceRep.GromHSCR.ActionBase.VisualActions
{
	[ContentProperty("Actions")]
	public class VisualActionGroup
	{
		public VisualActionGroup()
		{
			Actions = new List<VisualAction>();
		}

		public string Title { get; set; }

		public List<VisualAction> Actions { get; private set; }
	}
}
