using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenceRep.GromHSCR.Entities
{
    public class Region : BaseEntity
    {
        public Guid CityId { get; set; }

        public virtual City City { get; set; }

        public string Name { get; set; }

        public virtual IEnumerable<Street> Streets { get; set; }
    }
}
