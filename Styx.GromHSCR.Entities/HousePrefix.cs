﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.Entities
{
    public class HousePrefix : BaseEntity
    {
        public Guid HouseId { get; set; }

        public virtual House House { get; set; }

        public string Prefix { get; set; }
    }
}
