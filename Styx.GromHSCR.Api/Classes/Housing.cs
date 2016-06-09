using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Styx.GromHSCR.Api.Interfaces;

namespace Styx.GromHSCR.DocumentParserBase.Models
{
	public class Housing : IHousing
	{
		public Guid Id { get; set; }
		public Guid HouseId { get; set; }
		public IHouse House { get; set; }
		public int Number { get; set; }
	}
}
