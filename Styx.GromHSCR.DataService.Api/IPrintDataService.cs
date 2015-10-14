using System.Collections.Generic;
using Styx.GromHSCR.Api.Interfaces;

namespace Styx
{
    public interface IPrintDataService
    {
        IEnumerable<IPrintInfo> GetAllPrintInfos();
    }
}
