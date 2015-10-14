using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using Styx.GromHSCR.DataService.AssemblyCatalogs;

namespace Styx.GromHSCR.Service.AssemblyCatalogs
{
	public class ServicesCatalog : AssemblyCatalog
	{
		public ServicesCatalog()
			: base(Assembly.GetExecutingAssembly())
		{
			var catalog = new AggregateCatalog(new DataServicesCatalog());
			var container = new CompositionContainer(catalog);
			var batch = new CompositionBatch();
			batch.AddPart(this);
			container.Compose(batch); 
		}
	}
}
