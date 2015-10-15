using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.Entities
{
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

		public decimal? Gmax { get; set; }

		public decimal? Gmin { get; set; }

		public decimal? Glinear { get; set; }

		public decimal? Greturn { get; set; }

		public decimal? Kv { get; set; }

		public decimal? Fmax { get; set; }

		public DateTime PrintStartDate { get; set; }

		public DateTime PrintEndDate { get; set; }

        public DateTime LoadDateTime { get; set; }

	}
}
