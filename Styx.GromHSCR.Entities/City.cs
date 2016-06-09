using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Styx.GromHSCR.Entities
{
	[Table("Cities")]
    public class City : BaseEntity
    {
        public string Name { get; set; }

        public virtual List<Region> Regions { get; set; }
    }
}
