using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.Entities
{
	public class CounterModel : BaseEntity
	{
		public string Name { get; set; }

		public string Version { get; set; }

		public DateTime LastCheckDate { get; set; }

		public HeatType HeatType { get; set; }

		public virtual IEnumerable<HeatCounter> Counters { get; set; }
	}
}
