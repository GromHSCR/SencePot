using System.Collections.Generic;

namespace Styx.GromHSCR.Api.Interfaces
{
	public interface ICity : IBaseItem
	{
		string Name { get; set; }

		IEnumerable<IRegion> Regions { get; set; }
	}
}
