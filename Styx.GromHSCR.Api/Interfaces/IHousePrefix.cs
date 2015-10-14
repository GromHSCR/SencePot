using System;

namespace Styx.GromHSCR.Api.Interfaces
{
	public interface IHousePrefix : IBaseItem
	{
		Guid HouseId { get; set; }

		IHouse House { get; set; }

		string Prefix { get; set; }
	}
}
