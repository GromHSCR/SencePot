using System;

namespace Styx.GromHSCR.Api.Interfaces
{
	public interface IAddress : IBaseItem
	{
		Guid CityId { get; set; }

		Guid RegionId { get; set; }

		Guid StreetId { get; set; }

		Guid HouseId { get; set; }

		Guid? HousingId { get; set; }

		Guid? BuildingId { get; set; }

		Guid? HousePrefixId { get; set; }

		ICity City { get; set; }

		IRegion Region { get; set; }

		IStreet Street { get; set; }

		IHouse House { get; set; }

		IBuilding Building { get; set; }

		IHousing Housing { get; set; }
	}
}
