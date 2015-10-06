using System;
using System.Globalization;

namespace SenceRep.GromHSCR.Converters
{
	public class BitmapToImageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var bitmapValue = (value as Bitmap);
			return bitmapValue == null ? null : bitmapValue.ToBitmapImage();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (Bitmap)value;
		}
	}
}