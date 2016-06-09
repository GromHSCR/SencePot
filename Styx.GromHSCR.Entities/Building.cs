using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.Entities
{
	[Table("Buildings")]
    public class Building : BaseEntity
    {
        public Guid HouseId { get; set; }

        public virtual House House { get; set; }

        public int Number { get; set; }
    }
}
