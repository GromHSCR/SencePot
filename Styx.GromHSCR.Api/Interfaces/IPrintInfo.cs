using System;
using System.Collections.Generic;

namespace Styx.GromHSCR.Api.Interfaces
{
	public interface IPrintInfo : IBaseItem
	{
		int Number { get; set; }

		Guid AddressId { get; set; }

		IAddress Address { get; set; }

		Guid HeatCounterId { get; set; }

		IHeatCounter HeatCounter { get; set; }

		Guid? OrganizationId { get; set; }

		IOrganization Organization { get; set; }

		Guid? ContractId { get; set; }

		IContract Contract { get; set; }

		IEnumerable<IDailyData> DailyDatas { get; set; }

		IEnumerable<IValidationResult> ValidationResults { get; set; }

		IEnumerable<IEntry> Entries { get; set; }

		//Gmax
		decimal? G1max { get; set; }

		//Gmin
		decimal? G1min { get; set; }

		//G2max
		decimal? G2max { get; set; }

		//G2min
		decimal? G2min { get; set; }

		//G2max
		decimal? G3max { get; set; }

		//G2min
		decimal? G3min { get; set; }

		decimal? Glinear { get; set; }

		decimal? Greturn { get; set; }

		decimal? Kv { get; set; }

		decimal? Fmax { get; set; }

		TimeSpan? CurrentWorkTime { get; set; }

		decimal? CurrentDayEndTotalEnergy { get; set; }

		decimal? CurrentV1 { get; set; }

		decimal? CurrentV2 { get; set; }

		DateTime PrintStartDate { get; set; }

		DateTime PrintEndDate { get; set; }

		DateTime LoadDateTime { get; set; }

	}
}
