using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Styx.GromHSCR.Api.Interfaces;

namespace Styx.GromHSCR.DocumentParserBase.Models
{
	public class HousePrefix : IHousePrefix
	{
		public Guid Id { get; set; }
		public Guid HouseId { get; set; }
		public IHouse House { get; set; }
		public string Prefix { get; set; }
	}
}
