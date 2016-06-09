using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.Entities
{
	[Table("DailyDatas")]
	public class DailyData : BaseEntity
	{
		public Guid PrintInfoId { get; set; }

		public virtual PrintInfo PrintInfo { get; set; }

		public bool IsValid { get; set; }

		//EnergyHeat
		public decimal? Q1 { get; set; }

		//EnergyHotWater
		public decimal? Q2 { get; set; }

		public decimal? V1 { get; set; }

		public decimal? V2 { get; set; }

		public decimal? V3 { get; set; }

		//TemperatureLinear
		public decimal? T1 { get; set; }

		//TemperatureReturn
		public decimal? T2 { get; set; }

		public decimal? T3 { get; set; }

		//M1
		public decimal? M1 { get; set; }

		//M2
		public decimal? M2 { get; set; }

		//Mg
		public decimal? M3 { get; set; }

		//TemperatureCold
		public decimal? TemperatureConstantCold { get; set; }

		//PressureLinear
		public decimal? P1 { get; set; }

		//PressureReturn
		public decimal? P2 { get; set; }

		public decimal? P3 { get; set; }

		public int WorkingTime { get; set; }

		public int NotWorkingTime { get; set; }

		public string ErrorCode { get; set; }

		public DateTime CurrentDateTime { get; set; }

	}
}
