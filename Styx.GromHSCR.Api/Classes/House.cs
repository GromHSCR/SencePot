using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Styx.GromHSCR.Api.Interfaces;

namespace Styx.GromHSCR.DocumentParserBase.Models
{
	public class House : IHouse
	{
		public Guid Id { get; set; }
		public Guid StreetId { get; set; }
		public IStreet Street { get; set; }
		public int Number { get; set; }
		public IEnumerable<IHousing> Housings { get; set; }
		public IEnumerable<IBuilding> Buildings { get; set; }
		public IEnumerable<IHousePrefix> HousePrefixes { get; set; }
		public IEnumerable<IEntry> Entries { get; set; }
	}
}
