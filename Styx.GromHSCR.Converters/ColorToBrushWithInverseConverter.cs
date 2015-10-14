using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Styx.GromHSCR.Converters
{
	public class ColorToBrushWithInverseConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
#if !SL
			var color = new Color();
			if (value is System.Drawing.Color)
			{
				var mColor = (System.Drawing.Color)value;

				color = Color.FromArgb(mColor.A, (byte)(255 - mColor.R), (byte)(255 - mColor.G), (byte)(255 - mColor.B));
			}
			else
			{
				color = (Color)value;
				color = new Color { A = color.A, B = (byte)(255 - color.B), R = (byte)(255 - color.R), G = (byte)(255 - color.G) };
			}
			return new SolidColorBrush(color);
#else
               return new SolidColorBrush((Color)value);
#          endif
		}

		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((SolidColorBrush)value).Color;
		}
	}
}
