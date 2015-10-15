using System.Collections.Generic;
using System.ComponentModel.Composition;
using Styx.GromHSCR.Api.Interfaces;

namespace Styx
{
	[Export(typeof(IPrintInfoService))]
	[PartCreationPolicy(CreationPolicy.Any)]
	public class PrintInfoService : IPrintInfoService
	{
		[Import(typeof(IPrintInfoDataService))]
		private IPrintInfoDataService PrintInfoDataService { get; set; }

		public IEnumerable<IPrintInfo> GetAllPrintInfos()
		{
			return PrintInfoDataService.GetAllPrintInfos();
		}
	}
}
