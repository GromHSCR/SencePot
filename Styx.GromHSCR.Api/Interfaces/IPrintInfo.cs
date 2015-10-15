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

		decimal? Gmax { get; set; }

		decimal? Gmin { get; set; }

		decimal? Glinear { get; set; }

		decimal? Greturn { get; set; }

		decimal? Kv { get; set; }

		decimal? Fmax { get; set; }

		DateTime PrintStartDate { get; set; }

		DateTime PrintEndDate { get; set; }



	}
}
