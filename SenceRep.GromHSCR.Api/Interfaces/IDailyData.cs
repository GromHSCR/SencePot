using System;

namespace SenceRep.GromHSCR.Api.Interfaces
{
	public interface IDailyData : IBaseItem
	{
		Guid PrintInfoId { get; set; }

		IPrintInfo PrintInfo { get; set; }

		bool IsValid { get; set; }

		decimal? Energy { get; set; }

		decimal? V1 { get; set; }

		decimal? V2 { get; set; }

		decimal? TemperatureLinear { get; set; }

		decimal? TemperatureReturn { get; set; }

		decimal? TemperatureCold { get; set; }

		decimal? PressureLinear { get; set; }

		decimal? PressureReturn { get; set; }

		TimeSpan WorkTime { get; set; }

		string ErrorCode { get; set; }

		decimal? CurrentDayEndTotalEnergy { get; set; }

		decimal? CurrentV1 { get; set; }

		decimal? CurrentV2 { get; set; }

		TimeSpan? CurrentWorkTime { get; set; }

		DateTime CurrentDateTime { get; set; }

	}
}
