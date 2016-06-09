using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.Entities
{
	[Table("PrintInfos")]
	public class PrintInfo : BaseEntity
	{
		public int Number { get; set; }

		public Guid AddressId { get; set; }

		public virtual Address Address { get; set; }

		public Guid HeatCounterId { get; set; }

		public virtual HeatCounter HeatCounter { get; set; }

		public Guid? OrganizationId { get; set; }

		public virtual Organization Organization { get; set; }

		public Guid? ContractId { get; set; }

		public virtual Contract Contract { get; set; }

		public virtual IEnumerable<DailyData> DailyDatas { get; set; }

		public virtual IEnumerable<ValidationResult> ValidationResults { get; set; }

		//Gmax
		public decimal? G1max { get; set; }

		//Gmin
		public decimal? G1min { get; set; }

		//G2max
		public decimal? G2max { get; set; }

		//G2min
		public decimal? G2min { get; set; }

		//G2max
		public decimal? G3max { get; set; }

		//G2min
		public decimal? G3min { get; set; }

		public decimal? Glinear { get; set; }

		public decimal? Greturn { get; set; }

		public decimal? Kv { get; set; }

		public decimal? Fmax { get; set; }

		public int? CurrentWorkTime { get; set; }

		public decimal? CurrentDayEndTotalEnergy { get; set; }

		public decimal? CurrentV1 { get; set; }

		public decimal? CurrentV2 { get; set; }

		public DateTime PrintStartDate { get; set; }

		public DateTime PrintEndDate { get; set; }

		public DateTime LoadDateTime { get; set; }

	}
}
