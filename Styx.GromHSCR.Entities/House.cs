using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.Entities
{
	[Table("Houses")]
    public class House : BaseEntity
    {
        public Guid StreetId { get; set; }

        public virtual Street Street { get; set; }

        public int Number { get; set; }

        public virtual List<Housing> Housings { get; set; }

        public virtual List<Building> Buildings { get; set; }

		public virtual List<HousePrefix> HousePrefixes { get; set; }

		public virtual List<Entry> Entries { get; set; }
    }
}
