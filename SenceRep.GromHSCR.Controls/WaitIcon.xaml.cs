using System;

namespace SenceRep.GromHSCR.Controls
{
	/// <summary>
	/// Interaction logic for WaitIcon.xaml
	/// </summary>
	public partial class WaitIcon
	{
		public WaitIcon()
		{
			InitializeComponent();
		}
	}

	class DoubleDiv2Converter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is double)
				return ((double)value) / 2;

			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
