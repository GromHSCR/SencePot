using System;
using System.Windows.Media;

namespace Styx.GromHSCR.ActionBase.VisualActions
{
	public class VisualActionContextualGroup
	{
		public string Title { get; set; }

		public string Name { get; set; }

		public Type ContextualObjectType { get; set; }

		public Brush BackgroundAndBorder { get; set; }
	}
}
