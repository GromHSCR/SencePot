using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SenceRep.GromHSCR.Api.Interfaces;
using SenceRep.GromHSCR.DataService.Api;
using SenceRep.GromHSCR.Service.Api;

namespace SenceRep.GromHSCR.Service
{
    [Export(typeof(IPrintService))]
    [PartCreationPolicy(CreationPolicy.Any)]
    public class PrintService : IPrintService
    {
        [Import]
        private IPrintDataService PrintDataService { get; set; }


        public IEnumerable<IPrintInfo> GetAllPrintInfos()
        {
            return PrintDataService.GetAllPrintInfos();
        }
    }
}
