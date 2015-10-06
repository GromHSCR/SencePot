using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace SenceRep.GromHSCR.DocumentBase.AssemblyCatalogs
{
	public class DocumentBaseCatalog: AssemblyCatalog
	{
		public DocumentBaseCatalog()
			: base(Assembly.GetExecutingAssembly())
		{
		}
	}
}
