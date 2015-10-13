using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenceRep.GromHSCR.Entities
{
	public class CounterModel : BaseEntity
	{
		public string Name { get; set; }

		public string Version { get; set; }

		public HeatType HeatType { get; set; }

	}
}
