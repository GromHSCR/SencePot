using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.Entities
{
    public class Housing : BaseEntity
    {
        public Guid HouseId { get; set; }

        public virtual House House { get; set; }

        public int Number { get; set; }
    }
}
