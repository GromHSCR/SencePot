using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.Entities
{
	[Table("LegalEntities")]
	public class LegalEntity : BaseEntity
	{
		public string Name { get; set; }

		public Guid OrganizationId { get; set; }

		public virtual Organization Organization { get; set; }

		public string INN { get; set; }
	}
}
