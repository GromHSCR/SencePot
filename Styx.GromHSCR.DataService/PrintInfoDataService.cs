using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AutoMapper;
using Styx.GromHSCR.Api.Interfaces;
using Styx.GromHSCR.Entities;
using Styx.GromHSCR.Repositories.Context;
using Styx.GromHSCR.Repositories.Repositories;

namespace Styx
{
	[Export(typeof(IPrintInfoDataService))]
	[PartCreationPolicy(CreationPolicy.Any)]
	public class PrintInfoDataService : IPrintInfoDataService
	{
		public IEnumerable<IPrintInfo> GetAllPrintInfos()
		{
			List<IPrintInfo> result;

			using (var rep = new Repository<PrintInfo>())
			{
				var list = rep.GetAll().ToList();
				result = Mapper.Map<List<PrintInfo>, List<IPrintInfo>>(list);
			}
			return result;
		}

        public IEnumerable<IPrintInfo> Get500LatestPrintInfos()
        {
            List<IPrintInfo> result;

            using (var rep = new Repository<PrintInfo>())
            {
                var list = rep.GetAll().OrderBy(p => p.LoadDateTime).Take(500).ToList();
                result = Mapper.Map<List<PrintInfo>, List<IPrintInfo>>(list);
            }
            return result;
        }

		public IPrintInfo GetById(Guid id)
		{
			IPrintInfo result;
			using (var rep = new Repository<PrintInfo>())
			{
				var eventObj = rep.GetById(id);
				result = Mapper.DynamicMap<IPrintInfo>(eventObj);
			}
			return result;
		}

		public void AddPrintInfo(IPrintInfo printInfo)
		{
			if (printInfo == null) throw new ArgumentNullException("printInfo");
			using (var context = new DefaultContext())
			{
				using (var dbContextTransaction = context.Database.BeginTransaction())
				{
					try
					{
						if (printInfo.Address != null)
						{

							var address = new Address
							{
								Id = printInfo.Address.Id,

							};
							var repAddress = new Repository<Address>(context);
						}


						dbContextTransaction.Commit();
					}
					catch (Exception)
					{
						dbContextTransaction.Rollback();
					}
				}
			}
		}
	}
}
