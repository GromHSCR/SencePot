using System;
using System.Globalization;
using System.Windows.Data;

namespace Styx.GromHSCR.MvvmBase.Converters
{
	
		public class ZeroToBoolConverter : IValueConverter
		{
			public object Convert(int value, Type targetType, object parameter, CultureInfo culture)
			{
				return (value == 0);
			}

			public object Convert(decimal value, Type targetType, object parameter, CultureInfo culture)
			{
				return (value == 0);
			}

			public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			{
				if (value is int)
					return Convert((int) value, targetType, parameter, culture);
				if (value is decimal)
					return Convert((decimal)value, targetType, parameter, culture);
				return false;
			}

			public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			{
				throw new InvalidOperationException("IsNullConverter can only be used OneWay.");
			}
		}
}
