using System;

namespace Styx.GromHSCR.Helpers
{
	public static class ActionHelper
	{
		public static void InvokeIfNotNull(this Action source)
		{
			if (source == null) return;

			source();
		}
	}
}
