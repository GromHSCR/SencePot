using System;
using System.Windows;
using log4net;
using RedKassa.Promoter.CompostionBase;

namespace RedKassa.Promoter
{
	class Program
	{
		private static readonly ILog _log = LogManager.GetLogger(typeof(Program));

		[STAThreadAttribute]
		static void Main(string[] args)
		{
			log4net.Config.XmlConfigurator.Configure();

			_log.Debug("start");
			try
			{
				Composition.RegisterCatalogsFromConfig();

				var app = Composition.ComposeParts(Composition.Resolve<Application>());
				app.Run();
			}
			catch (Exception exception)
			{
				_log.Error("Фатальная ошибка", exception);
			}
		}
	}
}
