using System;

namespace SenceRep.GromHSCR.Converters
{
	public class TypeToBoolConverterEx : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null || parameter == null)
				return false;

			bool res = ((Type)parameter).IsInstanceOfType(value);
			return res;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
