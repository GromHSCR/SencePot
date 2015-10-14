using System.Collections.Generic;
using System.ComponentModel.Composition;
using AutoMapper;
using Styx.GromHSCR.Api.Interfaces;
using Styx.GromHSCR.Entities;
using Styx.GromHSCR.Repositories.Repositories;

namespace Styx.GromHSCR.DataService
{
	[Export(typeof(IPrintDataService))]
	[PartCreationPolicy(CreationPolicy.Any)]
	public class PrintDataService : IPrintDataService
	{
		public IEnumerable<IPrintInfo> GetAllPrintInfos()
		{
			IEnumerable<IPrintInfo> result;
			using (var rep = new Repository<PrintInfo>())
			{
				var prints = rep.GetAll();

				result = Mapper.Map<IEnumerable<PrintInfo>, IEnumerable<IPrintInfo>>(prints);
			}
			return result;
		}
	}
}
