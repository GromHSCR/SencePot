using System.Collections.Generic;
using Styx.GromHSCR.ActionBase.Common;

namespace Styx.GromHSCR.ActionBase.VisualActions
{
	public interface IVisualActionProvider
	{
		List<VisualActionContextualGroup> ContextualGroups { get; }
		List<VisualActionGroupTab> GroupTabs { get; }
		VisualActionPriority Priority { get; }
		IEnumerable<VisualAction> ActionsWithGlobalShortcuts { get; }  
	}
}