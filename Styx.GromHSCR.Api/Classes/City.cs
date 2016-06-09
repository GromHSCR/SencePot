using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Styx.GromHSCR.Api.Interfaces;

namespace Styx.GromHSCR.DocumentParserBase.Models
{
	public class City : ICity
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public IEnumerable<IRegion> Regions { get; set; }
	}
}
