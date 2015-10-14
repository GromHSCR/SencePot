using System;
using System.Globalization;
using System.Windows.Data;

namespace Styx.GromHSCR.MvvmBase.Converters
{
	public class DateTimeToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((DateTime)value).ToString("dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return DateTime.ParseExact((string) value, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
		}
	}
}