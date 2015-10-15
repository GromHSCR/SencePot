using System.Collections.Generic;
using Styx.GromHSCR.Api.Interfaces;

namespace Styx
{
    public interface IPrintInfoDataService
    {
        IEnumerable<IPrintInfo> GetAllPrintInfos();
    }
}
