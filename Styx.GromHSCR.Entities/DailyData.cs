using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.Entities
{
	public class DailyData : BaseEntity
	{
		public Guid PrintInfoId { get; set; }

		public virtual PrintInfo PrintInfo { get; set; }

		public bool IsValid { get; set; }

		public decimal? Energy { get; set; }

		public decimal? V1 { get; set; }

		public decimal? V2 { get; set; }

		public decimal? TemperatureLinear { get; set; }

		public decimal? TemperatureReturn { get; set; }

		public decimal? TemperatureCold { get; set; }

		public decimal? PressureLinear { get; set; }

		public decimal? PressureReturn { get; set; }

		public int WorkTime { get; set; }

		public string ErrorCode { get; set; }

		public decimal? CurrentDayEndTotalEnergy { get; set; }

		public decimal? CurrentV1 { get; set; }

		public decimal? CurrentV2 { get; set; }

		public int? CurrentWorkTime { get; set; }

		public DateTime CurrentDateTime { get; set; }

	}
}
