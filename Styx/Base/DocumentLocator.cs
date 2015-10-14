/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:RedKassaPromoter.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows;
using Styx.Documents;
using Styx.GromHSCR;
using Styx.GromHSCR.CompostionBase;
using LogManager = DevExpress.Xpo.Logger.LogManager;

namespace Styx.Base
{
	/// <summary>
	/// This class contains static references to all the view models in the
	/// application and provides an entry point for the bindings.
	/// <para>
	/// See http://www.galasoft.ch/mvvm
	/// </para>
	/// </summary>

	[Export(typeof(DocumentLocator))]
	[PartCreationPolicy(CreationPolicy.Shared)]
	public class DocumentLocator
	{

        //private static readonly ILog _log = LogManager.GetLogger(typeof(Program));

		[Import(typeof(IPrintService))]
		private IPrintService _printService;

		[Import]
		private IProgramInit _programInit;

		public static string GetProgressManagerText()
		{
			if (Application.Current.MainWindow.DataContext != null)
			{
				var mainDocument = Application.Current.MainWindow.DataContext as MainDocument;
				if (mainDocument != null)
				{
					if (mainDocument.LoadingProgress != null && mainDocument.LoadingProgress.ProgressText != null)
						return mainDocument.LoadingProgress.ProgressText;
				}
			}
			return "";
		}

		public DocumentLocator()
		{
			_programInit = Composition.GetExportedValue<IProgramInit>();
			//Composition.ComposeParts(this);
			Initialization();
		}

		private void Initialization()
		{
			Update();
		}

		async public void Update()
		{
			_programInit.IsInit = true;
			var task = new Task(() =>
			{
				
			});
			task.Start();

			try
			{
				await task;
			}
			catch (Exception e)
			{
                //_log.Debug(e.Message);
			}
			finally
			{

				_programInit.IsInit = false;
			}
		}
	}
}