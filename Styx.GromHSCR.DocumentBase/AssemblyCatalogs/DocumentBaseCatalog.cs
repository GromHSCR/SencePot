using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace Styx.GromHSCR.DocumentBase.AssemblyCatalogs
{
	public class DocumentBaseCatalog: AssemblyCatalog
	{
		public DocumentBaseCatalog()
			: base(Assembly.GetExecutingAssembly())
		{
		}
	}
}
