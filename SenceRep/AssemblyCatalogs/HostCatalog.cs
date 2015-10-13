using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace SenceRep.AssemblyCatalogs
{
	public class HostCatalog: AssemblyCatalog
	{
		public HostCatalog()
			: base(Assembly.GetExecutingAssembly())
		{
		}
	}
}
