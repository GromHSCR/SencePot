using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Styx.GromHSCR.Api.Interfaces;

namespace Styx.GromHSCR.DocumentParserBase.Models
{
	public class Region : IRegion
	{
		public Guid Id { get; set; }
		public Guid CityId { get; set; }
		public ICity City { get; set; }
		public string Name { get; set; }
		public IEnumerable<IStreet> Streets { get; set; }
	}
}
