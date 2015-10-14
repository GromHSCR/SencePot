using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Styx.GromHSCR.Helpers
{
	public static class ObjectHelpercs
	{
		public static string GetPropertyNameByObject<T>(this object source, Expression<Func<T>> propertyExpression)
		{
			if (source == null) throw new ArgumentNullException("source");
			if (propertyExpression == null)
				throw new ArgumentNullException("propertyExpression");
			var memberExpression = propertyExpression.Body as MemberExpression;
			if (memberExpression == null)
				throw new ArgumentException("Invalid argument", "propertyExpression");
			var propertyInfo = memberExpression.Member as PropertyInfo;
			if (propertyInfo == null)
				throw new ArgumentException("Argument is not a property", "propertyExpression");

			return propertyInfo.Name;
		}
	}
}
