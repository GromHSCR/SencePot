using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Styx.GromHSCR.Api.Interfaces;

namespace Styx.GromHSCR.DataService.Api
{
    public interface IPrintDataService
    {
        IEnumerable<IPrintInfo> GetAllPrintInfos();
    }
}
