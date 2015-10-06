using System;
using System.Globalization;

namespace SenceRep.GromHSCR.Converters
{
	public class TruncateStringConverter : IValueConverter
	{
		const int MAX_LENTH = 25;

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string str = (value ?? String.Empty).ToString();
			return (str.Length <= MAX_LENTH) ? str.Trim() : str.Substring(0, MAX_LENTH).Trim() + " ...";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		public static string Convert(string value)
		{
			return new TruncateStringConverter().Convert(value, typeof(string), null, CultureInfo.CurrentUICulture) as string;
		}
	}
}
