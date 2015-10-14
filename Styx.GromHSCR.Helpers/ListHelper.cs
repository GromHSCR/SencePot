using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Styx.GromHSCR.Helpers
{
	public static class ListHelper
	{
		public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source)
		{
			return source ?? Enumerable.Empty<T>();
		}

		public static void RemoveRange(this IList source, IEnumerable itemsToRemove)
		{
			foreach (var item in itemsToRemove.Cast<object>().ToList())
				source.Remove(item);
		}
	}
}
