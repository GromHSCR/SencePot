using System.Collections.Generic;
using System.ComponentModel.Composition;
using Styx.GromHSCR.Api.Interfaces;

namespace Styx.GromHSCR.Service
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
