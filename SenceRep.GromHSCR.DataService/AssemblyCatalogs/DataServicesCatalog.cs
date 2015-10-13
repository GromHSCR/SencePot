using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using SenceRep.GromHSCR.Repositories;

namespace SenceRep.GromHSCR.DataService.AssemblyCatalogs
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
