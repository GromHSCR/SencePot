using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.Entities
{
	[Table("Entries")]
	public class Entry : BaseEntity
	{
		public Guid HouseId { get; set; }

		public House House { get; set; }

		public int Number { get; set; }
	}
}
