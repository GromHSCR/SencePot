﻿using System;
using System.Windows.Data;

namespace Styx.GromHSCR.Converters
{
	public class TypeIsMatchConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null || parameter == null)
				return null;

			return ((Type)parameter).IsInstanceOfType(value) ? value : null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
