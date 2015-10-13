using System;
using System.Collections.Generic;

namespace SenceRep.GromHSCR.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }

        public virtual IEnumerable<Region> Regions { get; set; }
    }
}
