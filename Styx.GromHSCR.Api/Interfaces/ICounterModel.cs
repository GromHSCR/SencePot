using System;

namespace Styx.GromHSCR.Api.Interfaces
{
	public interface ICounterModel : IBaseItem
	{
		string Name { get; set; }

		string Version { get; set; }

		DateTime Date { get; set; }

		HeatType HeatType { get; set; }
	}
}
