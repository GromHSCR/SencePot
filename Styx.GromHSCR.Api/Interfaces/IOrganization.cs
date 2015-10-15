using System;
using System.Collections.Generic;

namespace Styx.GromHSCR.Api.Interfaces
{
	public interface IOrganization : IBaseItem
	{
		string Name { get; set; }

		IEnumerable<ILegalEntity> LegalEntities { get; set; }

		IEnumerable<IContract> Contracts { get; set; } 

	}
}
