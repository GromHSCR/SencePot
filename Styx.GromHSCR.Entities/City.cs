using System;
using System.Collections.Generic;

namespace Styx.GromHSCR.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }

        public virtual List<Region> Regions { get; set; }
    }
}
