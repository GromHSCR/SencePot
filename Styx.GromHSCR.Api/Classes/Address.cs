using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Styx.GromHSCR.Api.Interfaces;

namespace Styx.GromHSCR.DocumentParserBase.Models
{
	public class Address : IAddress
	{
		public Guid Id { get; set; }
		public Guid CityId { get; set; }
		public Guid? RegionId { get; set; }
		public Guid StreetId { get; set; }
		public Guid HouseId { get; set; }
		public Guid? HousingId { get; set; }
		public Guid? BuildingId { get; set; }
		public Guid? HousePrefixId { get; set; }
		public ICity City { get; set; }
		public IRegion Region { get; set; }
		public IStreet Street { get; set; }
		public IHouse House { get; set; }
		public IBuilding Building { get; set; }
		public IHousing Housing { get; set; }
		public IHousePrefix HousePrefix { get; set; }
	}
}
