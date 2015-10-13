using System.Collections.Generic;

namespace SenceRep.GromHSCR.Api.Interfaces
{
	public interface ICity : IBaseItem
	{
		string Name { get; set; }

		IEnumerable<IRegion> Regions { get; set; }
	}
}
