using System.Collections.Generic;
using SenceRep.GromHSCR.ActionBase.Common;

namespace SenceRep.GromHSCR.ActionBase.VisualActions
{
	public interface IVisualActionProvider
	{
		List<VisualActionContextualGroup> ContextualGroups { get; }
		List<VisualActionGroupTab> GroupTabs { get; }
		VisualActionPriority Priority { get; }
		IEnumerable<VisualAction> ActionsWithGlobalShortcuts { get; }  
	}
}