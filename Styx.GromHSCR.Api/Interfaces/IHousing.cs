using System;

namespace Styx.GromHSCR.Api.Interfaces
{
	public interface IHousing : IBaseItem
	{
		Guid HouseId { get; set; }

		IHouse House { get; set; }

		int Number { get; set; }
	}
}
