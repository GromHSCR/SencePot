using System.Collections.Generic;
using Styx.GromHSCR.Api.Interfaces;

namespace Styx
{
    public interface IPrintInfoService
    {
        IEnumerable<IPrintInfo> GetAllPrintInfos();
    }
}
