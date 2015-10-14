using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.Entities
{
	public class CounterModel : BaseEntity
	{
		public string Name { get; set; }

		public string Version { get; set; }

		public DateTime Date { get; set; }

		public HeatType HeatType { get; set; }
	}
}
