using System;

namespace SenceRep.GromHSCR.Api.Interfaces
{
   public interface IBuilding : IBaseItem
    {
        Guid HouseId { get; set; }

        IHouse House { get; set; }

        int Number { get; set; }
    }
}
