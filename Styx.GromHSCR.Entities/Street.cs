using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.Entities
{
	[Table("Streets")]
    public class Street : BaseEntity
    {
        public Guid RegionId { get; set; }

        public Guid CityId { get; set; }

        public virtual City City { get; set; }

        public virtual Region Region { get; set; }

        public string Name { get; set; }

        public virtual IEnumerable<House> Houses { get; set; }
    }
}
