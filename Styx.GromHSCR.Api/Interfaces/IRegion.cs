using System;
using System.Collections.Generic;

namespace Styx.GromHSCR.Api.Interfaces
{
   public interface IRegion : IBaseItem
    {
        Guid CityId { get; set; }

        ICity City { get; set; }

        string Name { get; set; }

        IEnumerable<IStreet> Streets { get; set; }
    }
}
