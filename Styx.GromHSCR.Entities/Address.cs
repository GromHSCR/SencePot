using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.Entities
{
    public class Address : BaseEntity
    {
        public Guid CityId { get; set; }

        public Guid RegionId { get; set; }

        public Guid StreetId { get; set; }

        public Guid HouseId { get; set; }

        public Guid? HousingId { get; set; }

        public Guid? BuildingId { get; set; }

        public Guid? HousePrefixId { get; set; }

        public virtual City City { get; set; }

        public virtual Region Region { get; set; }

        public virtual Street Street { get; set; }

        public virtual House House { get; set; }

        public virtual Building Building { get; set; }

        public virtual Housing Housing { get; set; }
    }
}
