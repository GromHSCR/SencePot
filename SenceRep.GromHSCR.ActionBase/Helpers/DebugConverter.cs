﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace SenceRep.GromHSCR.ActionBase.Helpers
{
	public class DebugConverter: IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}
	}
}
