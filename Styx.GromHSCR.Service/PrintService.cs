using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Styx.GromHSCR.Api.Interfaces;
using Styx.GromHSCR.DataService.Api;
using Styx.GromHSCR.Service.Api;

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
