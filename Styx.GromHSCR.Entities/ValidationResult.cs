using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.Entities
{
    public class ValidationResult : BaseEntity
    {
        public Guid PrintInfoId { get; set; }

        public virtual PrintInfo PrintInfo { get; set; }

        public string Comment { get; set; }

        public DateTime ValidationDateTime { get; set; }

    }
}
