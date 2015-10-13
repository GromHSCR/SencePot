using System;
using System.Windows.Data;

namespace SenceRep.GromHSCR.Converters
{
	public class BoolToDisabledConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return !(value != null && value.Equals(true));
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
