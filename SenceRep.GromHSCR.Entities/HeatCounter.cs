using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenceRep.GromHSCR.Entities
{
    public class HeatCounter : BaseEntity
    {
        public int Number { get; set; }

        public Guid AddressId { get; set; }

        public HeatType HeatType { get; set; }

        public virtual Address Address { get; set; }

    }
}
