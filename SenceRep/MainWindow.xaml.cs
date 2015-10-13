using System;

namespace SenceRep
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		/// <summary>
		/// Initializes a new instance of the MainWindow class.
		/// </summary>
		public MainWindow()
		{
			Title = String.Format("RedKassa {0} - Организатор", GetPublishedVersion());
			InitializeComponent();
		}

		private string GetPublishedVersion()
		{
			if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
			{
				return System.Deployment.Application.ApplicationDeployment.CurrentDeployment.
					CurrentVersion.ToString();
			}
			return "";
		}
	}
}