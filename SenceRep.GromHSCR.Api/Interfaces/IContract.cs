using System;

namespace SenceRep.GromHSCR.Api.Interfaces
{
	interface IContract : IBaseItem
	{
		int Number { get; set; }

		Guid? OrganizationId { get; set; }

		IOrganization Organization { get; set; }
	}
}
