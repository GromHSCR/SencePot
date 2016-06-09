using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.Api.Interfaces
{
	public interface IEntry : IBaseItem
	{
		Guid HouseId { get; set; }

		IHouse House { get; set; }

		int Number { get; set; }
	}
}
