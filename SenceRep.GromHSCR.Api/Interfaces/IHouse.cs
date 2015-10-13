using System;
using System.Collections.Generic;

namespace SenceRep.GromHSCR.Api.Interfaces
{
   public interface IHouse : IBaseItem
    {
        Guid StreetId { get; set; }

        IStreet Street { get; set; }

        int Number { get; set; }

        IEnumerable<IHousing> Housings { get; set; }

        IEnumerable<IBuilding> Buildings { get; set; }

        IEnumerable<IHousePrefix> HousePrefixes { get; set; }
    }
}
