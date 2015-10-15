using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.Entities
{
	public class Organization : BaseEntity
	{
		public string Name { get; set; }

		public virtual List<LegalEntity> LegalEntities { get; set; }

		public virtual List<Contract> Contracts { get; set; } 

	}
}
