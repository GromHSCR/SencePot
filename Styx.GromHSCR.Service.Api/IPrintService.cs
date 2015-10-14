using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Styx.GromHSCR.Api.Interfaces;

namespace Styx.GromHSCR.Service.Api
{
    public interface IPrintService
    {
        IEnumerable<IPrintInfo> GetAllPrintInfos();
    }
}
