using System.Windows;

namespace SenceRep.Controls
{
	/// <summary>
	/// Interaction logic for SeanceNameAndDate.xaml
	/// </summary>
	public partial class SeanceNameAndDate
	{
		public SeanceNameAndDate()
		{
			InitializeComponent();
		}

		public static readonly DependencyProperty SeanceDateProperty = DependencyProperty.Register("SeanceDate", typeof(string), typeof(SeanceNameAndDate));

		public string SeanceDate
		{
			get
			{
				return (string)GetValue(SeanceDateProperty);
			}
			set
			{
				SetValue(SeanceDateProperty, value);
			}
		}

		public static readonly DependencyProperty SeanceNameProperty = DependencyProperty.Register("SeanceName", typeof(string), typeof(SeanceNameAndDate));

		public string SeanceName
		{
			get
			{
				return (string)GetValue(SeanceNameProperty);
			}
			set
			{
				SetValue(SeanceNameProperty, value);
			}
		}
	}
}
