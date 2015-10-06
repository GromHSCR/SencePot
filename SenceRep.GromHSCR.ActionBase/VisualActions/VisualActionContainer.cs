using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;
using SenceRep.GromHSCR.ActionBase.Common;

namespace SenceRep.GromHSCR.ActionBase.VisualActions
{
	[ContentProperty("GroupTabs")]
	public class VisualActionContainer : IVisualActionProvider
	{
		public VisualActionContainer()
		{
			ContextualGroups = new List<VisualActionContextualGroup>();
			GroupTabs = new List<VisualActionGroupTab>();
			Priority = VisualActionPriority.Normal;
		}

		public List<VisualActionContextualGroup> ContextualGroups { get; set; }

		public List<VisualActionGroupTab> GroupTabs { get; private set; }

		public VisualActionPriority Priority { get; set; }

		public IEnumerable<VisualAction> ActionsWithGlobalShortcuts
		{
			get
			{
				return
					from groupTab in this.GroupTabs
					from @group in groupTab.Groups
					from action in @group.Actions
					where action.GlobalShortcut != null
					select action;
			}
		}
	}
}
