using System;

namespace Styx.GromHSCR.Api.Interfaces
{
	public interface IHeatCounter : IBaseItem
	{
		int Number { get; set; }

		Guid AddressId { get; set; }

		ICounterModel CounterModel { get; set; }

		IAddress Address { get; set; }

	}
}
