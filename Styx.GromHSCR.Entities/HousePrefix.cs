using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.Entities
{
	[Table("HousePrefixes")]
    public class HousePrefix : BaseEntity
    {
        public Guid HouseId { get; set; }

        public virtual House House { get; set; }

        public string Prefix { get; set; }
    }
}
