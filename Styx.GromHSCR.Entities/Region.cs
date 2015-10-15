﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.Entities
{
    public class Region : BaseEntity
    {
        public Guid CityId { get; set; }

        public virtual City City { get; set; }

        public string Name { get; set; }

        public virtual List<Street> Streets { get; set; }
    }
}
