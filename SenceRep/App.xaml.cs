using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Threading;
using Common.Logging;
using GalaSoft.MvvmLight.Threading;
using SenceRep.Base;
using SenceRep.Documents;
using SenceRep.GromHSCR.CompostionBase;

namespace SenceRep
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	[Export(typeof(Application))]
	public partial class App
	{
		[Import]
		public DocumentLocator DocumentLocator;

		[Import]
		public ILog Log { get; set; }

		public App()
		{
			//Promoter.Log.Logger.StartYandexMetrica(22726);
			InitializeComponent();
			DispatcherUnhandledException += AppDispatcherUnhandledException;
			DispatcherHelper.Initialize();
		}

		void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			Log.Error(e.Exception.Message, e.Exception);
			e.Handled = true;
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			MainWindow = CreateMainWindow();
			MainWindow.Show();
		}

		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);
			//Promoter.Log.Logger.StopYandexMetrica();
		}

		private Window CreateMainWindow()
		{
			var mainDocument = new MainDocument();
			Composition.ComposeParts(mainDocument);

			mainDocument.Init();

			var wnd = new MainWindow { DataContext = mainDocument };
			return wnd;
		}
	}
}