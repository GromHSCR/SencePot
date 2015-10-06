using System;

namespace SenceRep.GromHSCR.Converters
{
	public class WaitCursorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return (value != null && value.Equals(true)) ? Cursors.Wait : Cursors.Arrow;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
