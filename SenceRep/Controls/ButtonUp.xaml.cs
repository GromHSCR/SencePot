using SenceRep.GromHSCR.Helpers;

namespace SenceRep.GromHSCR.Controls
{
	/// <summary>
	/// Interaction logic for ButtonUp.xaml
	/// </summary>
	public partial class ButtonUp
	{
		public ButtonUp()
		{
			InitializeComponent();
			this.Image.Source = Properties.Resources.Up_16.ToBitmapImage();
		}
	}
}
