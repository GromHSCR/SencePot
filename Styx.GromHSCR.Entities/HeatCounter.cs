using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.Entities
{
    public class HeatCounter : BaseEntity
    {
        public int Number { get; set; }

        public Guid AddressId { get; set; }

        public CounterModel CounterModel { get; set; }

        public virtual Address Address { get; set; }

    }
}
