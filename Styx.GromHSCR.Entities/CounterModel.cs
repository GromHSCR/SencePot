using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.Entities
{
	[Table("CounterModels")]
	public class CounterModel : BaseEntity
	{
		public string Name { get; set; }

		public string Version { get; set; }

		public DateTime LastCheckDate { get; set; }

		public HeatType HeatType { get; set; }

		public virtual IEnumerable<HeatCounter> Counters { get; set; }
	}
}
