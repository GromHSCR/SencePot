using Styx.GromHSCR.Helpers;

namespace Styx.Controls
{
	/// <summary>
	/// Interaction logic for ButtonDown.xaml
	/// </summary>
	public partial class ButtonDown
	{
		public ButtonDown()
		{
			InitializeComponent();
			this.Image.Source = Properties.Resources.Down_16.ToBitmapImage();
		}
	}
}
