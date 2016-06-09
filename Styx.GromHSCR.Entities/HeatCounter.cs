using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.Entities
{
	[Table("HeatCounters")]
    public class HeatCounter : BaseEntity
    {
        public int Number { get; set; }

        public Guid AddressId { get; set; }

		public Guid CounterModelId { get; set; }

        public CounterModel CounterModel { get; set; }

        public virtual Address Address { get; set; }

    }
}
