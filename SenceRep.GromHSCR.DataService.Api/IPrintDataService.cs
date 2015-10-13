using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SenceRep.GromHSCR.Api.Interfaces;

namespace SenceRep.GromHSCR.DataService.Api
{
    public interface IPrintDataService
    {
        IEnumerable<IPrintInfo> GetAllPrintInfos();
    }
}
