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
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using log4net;
using SenceRep.GromHSCR.CompostionBase;
using SenceRep.GromHSCR.Documents;
using SenceRep.GromHSCR.Interfaces;

namespace SenceRep.GromHSCR.Base
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

		private static readonly ILog _log = LogManager.GetLogger(typeof(Program));

		[Import(typeof(IPromoterService))]
		private IPromoterService _promoterService;

		[Import(typeof(IPlaceGroupService))]
		private IPlaceGroupService _placeGroupService;

		[Import(typeof(IAgentService))]
		private IAgentService _agentService;

		[Import]
		private IProgramInit _programInit;

		[Import(typeof(ILegalEntityService))]
		private ILegalEntityService _legalEntityService;
		//	
		public static IPromoter DefaultPromoter { get; private set; }

		public static IEnumerable<ILegalEntity> LegalEntities { get; private set; }

		public static IEnumerable<IManager> Managers { get; private set; }

		public static IEnumerable<IPlaceGroupSuperLight> PlaceGroups { get; private set; }

		public static IEnumerable<IAgent> Agents { get; private set; }
		public static IEnumerable<IPromoter> Promoters { get; private set; }

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
			_promoterService = Composition.GetExportedValue<IPromoterService>();
			_placeGroupService = Composition.GetExportedValue<IPlaceGroupService>();
			_agentService = Composition.GetExportedValue<IAgentService>();
			_legalEntityService = Composition.GetExportedValue<ILegalEntityService>();
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
				UpdatePromoters();
				UpdateAgents();
				UpdatePlaceGroups();
				UpdateLegalEntities();
			});
			task.Start();

			try
			{
				await task;
			}
			catch (Exception e)
			{
				_log.Debug(e.Message);
			}
			finally
			{

				_programInit.IsInit = false;
			}
		}

		public void UpdatePlaceGroups()
		{
			PlaceGroups = _placeGroupService.GetAllSuperLight();
		}

		public void UpdateLegalEntities()
		{
			LegalEntities = _legalEntityService.GetAll();
		}

		public void UpdatePromoters()
		{
			var promoterId = Guid.Parse(ConfigurationManager.AppSettings["DefaultPromoterId"]);
			if (promoterId != Guid.Empty)
			{
				DefaultPromoter = _promoterService.GetById(promoterId);
				LegalEntities = DefaultPromoter.LegalEntities;
				Managers = DefaultPromoter.Managers;
			}
			Promoters = _promoterService.GetAll();
		}

		public void UpdateAgents()
		{
			Agents = _agentService.GetAllAgents();
		}
	}
}