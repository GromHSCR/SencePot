﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenceRep.GromHSCR.Entities
{
	public class Contract : BaseEntity
	{
		public int Number { get; set; }

		public Guid? OrganizationId { get; set; }

		public virtual Organization Organization { get; set; }
	}
}
