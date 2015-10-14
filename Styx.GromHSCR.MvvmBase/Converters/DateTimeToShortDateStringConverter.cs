﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Styx.GromHSCR.MvvmBase.Converters
{
	public class DateTimeToShortDateStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((DateTime) value).ToShortDateString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}