using System;

namespace Styx.GromHSCR.Helpers
{
	public static class DelegateHelper
	{
		public static void ExecuteIfNotNull(this Action action)
		{
			if (action != null)
				action();
		}

		public static void ExecuteIfNotNull<T>(this Action<T> action, T param)
		{
			if (action != null)
				action(param);
		}

		public static void ExecuteIfNotNull<TParam1, TParam2>(this Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2)
		{
			if (action != null)
				action(param1, param2);
		}
	}
}