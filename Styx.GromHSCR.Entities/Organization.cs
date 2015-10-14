using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.Entities
{
	public class Organization : BaseEntity
	{
		public string Name { get; set; }

		public Guid LegalEntityId { get; set; }

		public virtual LegalEntity LegalEntity { get; set; }

		public virtual IEnumerable<Contract> Contracts { get; set; } 

	}
}
