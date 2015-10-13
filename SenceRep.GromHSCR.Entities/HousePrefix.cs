using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenceRep.GromHSCR.Entities
{
    public class HousePrefix : BaseEntity
    {
        public Guid HouseId { get; set; }

        public virtual House House { get; set; }

        public string Prefix { get; set; }
    }
}
