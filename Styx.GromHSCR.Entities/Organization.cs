using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.Entities
{
	[Table("Organizations")]
	public class Organization : BaseEntity
	{
		public string Name { get; set; }

		public virtual List<LegalEntity> LegalEntities { get; set; }

		public virtual List<Contract> Contracts { get; set; } 

	}
}
