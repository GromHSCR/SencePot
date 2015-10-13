using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SenceRep.GromHSCR.Api.Interfaces;
using SenceRep.GromHSCR.DataService.Api;
using SenceRep.GromHSCR.Entities;
using SenceRep.GromHSCR.Repositories.Repositories;

namespace SenceRep.GromHSCR.DataService
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

                result = Mapper.Map<IEnumerable<IPrintInfo>>(prints);
            }
        }
    }
}
