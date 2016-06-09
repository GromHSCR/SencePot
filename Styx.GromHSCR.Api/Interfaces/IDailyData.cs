using System;

namespace Styx.GromHSCR.Api.Interfaces
{
	public interface IDailyData : IBaseItem
	{
		 Guid PrintInfoId { get; set; }

		 IPrintInfo PrintInfo { get; set; }

		 bool IsValid { get; set; }

		//EnergyHeat
		 decimal? Q1 { get; set; }

		//EnergyHotWater
		 decimal? Q2 { get; set; }

		 decimal? V1 { get; set; }

		 decimal? V2 { get; set; }

		 decimal? V3 { get; set; }

		//TemperatureLinear
		 decimal? T1 { get; set; }

		//TemperatureReturn
		 decimal? T2 { get; set; }

		 decimal? T3 { get; set; }

		//M1
		 decimal? M1 { get; set; }

		//M2
		 decimal? M2 { get; set; }

		//Mg
		 decimal? M3 { get; set; }

		//TemperatureCold
		 decimal? TemperatureConstantCold { get; set; }

		//PressureLinear
		 decimal? P1 { get; set; }

		//PressureReturn
		 decimal? P2 { get; set; }

		 decimal? P3 { get; set; }

		 TimeSpan WorkingTime { get; set; }

		 TimeSpan NotWorkingTime { get; set; }

		 string ErrorCode { get; set; }

		 DateTime CurrentDateTime { get; set; }

	}
}
