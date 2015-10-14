using System.Collections.Generic;
using Styx.GromHSCR.Api.Interfaces;

namespace Styx
{
    public interface IPrintService
    {
        IEnumerable<IPrintInfo> GetAllPrintInfos();
    }
}
