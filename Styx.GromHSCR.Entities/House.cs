using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.Entities
{
    public class House : BaseEntity
    {
        public Guid StreetId { get; set; }

        public virtual Street Street { get; set; }

        public int Number { get; set; }

        public virtual IEnumerable<Housing> Housings { get; set; }

        public virtual IEnumerable<Building> Buildings { get; set; }

        public virtual IEnumerable<HousePrefix> HousePrefixes { get; set; }
    }
}
