using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using Styx.GromHSCR.Repositories;

namespace Styx.GromHSCR.DataService.AssemblyCatalogs
{
	public class DataServicesCatalog : AssemblyCatalog
	{
		public DataServicesCatalog()
			: base(Assembly.GetExecutingAssembly())
		{
			MapInitializer.Initialize();
			DbManager.Initialize();
		}
	}
}
