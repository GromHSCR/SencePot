using System;
using System.Collections.Generic;

namespace SenceRep.GromHSCR.Api.Interfaces
{
	public interface IStreet : IBaseItem
	{
		Guid RegionId { get; set; }

		Guid CityId { get; set; }

		ICity City { get; set; }

		IRegion Region { get; set; }

		string Name { get; set; }

		IEnumerable<IHouse> Houses { get; set; }
	}
}
