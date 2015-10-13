using System;
using System.Collections.Generic;

namespace SenceRep.GromHSCR.Api.Interfaces
{
	interface IOrganization : IBaseItem
	{
		string Name { get; set; }

		Guid LegalEntityId { get; set; }

		ILegalEntity LegalEntity { get; set; }

		IEnumerable<IContract> Contracts { get; set; } 

	}
}
